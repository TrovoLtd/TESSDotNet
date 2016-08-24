using System;
using System.Collections.Generic;
using System.Linq;

using TrovoSiteSearch.Enumerations;
using TrovoSiteSearch.Interfaces;

namespace TrovoSiteSearch.GoogleSiteSearch
{
    public class GoogleQueryStringBuilder
    {
        private const string _GOOGLE_CLIENT_NAME = "google-csbe";
        private const string _GOOGLE_XML_OUTPUT = "xml_no_dtd";
        private string _initialQueryTerm;
        private string _searchProviderClientName, _searchProviderAccountId, _numberOfResultsPerPage;

        public ITrovoQuery Query { get; set; }
        
        public GoogleQueryStringBuilder(ITrovoQuery query, Dictionary<string, string> configSettings)
        {
            Query = query;

            _searchProviderClientName = configSettings[GoogleSiteSearchConfigSettings.SearchProviderClientName.ToString()];
            _searchProviderAccountId = configSettings[GoogleSiteSearchConfigSettings.SearchProviderAccountId.ToString()];
            _numberOfResultsPerPage = configSettings[GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString()];

            _initialQueryTerm = Query.SearchTerm;
        }
        
        public GoogleQueryStringDecorator BuildQuery()
        {
            HandleNOT();
            
            GoogleQueryStringDecorator startDecorator = BuildResultListSizeAndStartResultNumberDecorators();

            return startDecorator;
        }

        private void HandleNOT()
        {
            if (Query.SearchTerm != null && Query.SearchTerm.Contains("+NOT+")) Query.SearchTerm = Query.SearchTerm.Replace("+NOT+", "+-");
        }

        private GoogleQueryStringDecorator BuildResultListSizeAndStartResultNumberDecorators()
        {
            GoogleQueryStringDecorator queryDecorator = BuildQueryDecorator();

            GoogleQueryStringDecorator defaultFiltersDecorator = AddDefaultFilters(queryDecorator);

            GoogleQueryStringDecorator safeSearchFiltersDecorator = AddSafeSearchFilters(defaultFiltersDecorator);

            GoogleQueryStringDecorator numDecorator = new GoogleQueryStringDecorator(safeSearchFiltersDecorator);
            numDecorator.ParameterName = GoogleSearchQueryParameterName.num;
            numDecorator.ParameterValue = _numberOfResultsPerPage;

            GoogleQueryStringDecorator startDecorator = new GoogleQueryStringDecorator(numDecorator);
            startDecorator.ParameterName = GoogleSearchQueryParameterName.start;
            startDecorator.ParameterValue = CalculateStartResultNumber();

            return startDecorator;
        }

        private GoogleQueryStringDecorator AddSafeSearchFilters(GoogleQueryStringDecorator decoratorToAddFiltersTo)
        {
            if (Query.Filters.Any(filter => filter.Type == TrovoFilterType.AdultContent))
            {
                var adultFilter = Query.Filters.First(filter => filter.Type == TrovoFilterType.AdultContent);
                
                GoogleQueryStringDecorator filterDecorator = new GoogleQueryStringDecorator(decoratorToAddFiltersTo);
                filterDecorator.ParameterName = GoogleSearchQueryParameterName.safe;

                if(adultFilter.Mode == TrovoFilterMode.Exclude)
                {
                    filterDecorator.ParameterValue = "high";
                }
                else
                {
                    filterDecorator.ParameterValue = "off";
                }
                
                return filterDecorator;
            }
            else
            {
                return decoratorToAddFiltersTo;
            }
        }

        private GoogleQueryStringDecorator AddDefaultFilters(GoogleQueryStringDecorator decoratorToAddFiltersTo)
        {
            if (Query.Filters.Any(filter => (filter.Type == TrovoFilterType.ProviderDefaults && filter.Mode == TrovoFilterMode.Exclude)))
            {
                GoogleQueryStringDecorator filterDecorator = new GoogleQueryStringDecorator(decoratorToAddFiltersTo);
                filterDecorator.ParameterName = GoogleSearchQueryParameterName.filter;
                filterDecorator.ParameterValue = "0";

                return filterDecorator;
            }
            else
            {
                return decoratorToAddFiltersTo;
            }
        }

        /// <summary>
        /// The starting result is:
        /// The current page number - 1
        /// Multiplied by the number of results required per page
        /// Plus one (to give the first answer on the page)
        /// </summary>
        /// <returns></returns>

        private string CalculateStartResultNumber()
        {
            return (((Query.PageNumber - 1) * Int32.Parse(_numberOfResultsPerPage))).ToString();
        }

        private GoogleQueryStringDecorator BuildQueryDecorator()
        {
            GoogleQueryStringDecorator accountIdDecorator = BuildAccountIdQueryDecorator();
            GoogleQueryStringDecorator queryDecorator = new GoogleQueryStringDecorator(accountIdDecorator);
            queryDecorator.ParameterName = GoogleSearchQueryParameterName.q;
            

            if (Query.Filters.Count > 0)
            {
                foreach (ITrovoFilter filter in Query.Filters)
                {
                    AppendQueryFilters(filter);
                }
            }

            queryDecorator.ParameterValue = Query.SearchTerm;

            return queryDecorator;
        }

        /// <summary>
        /// Adds a specific query filter to the query string
        /// </summary>
        /// <remarks>
        /// Just breaking on the default is usually bad practice, but in this instance there's a possibility
        /// that a client might pass in a set of filters that are unusable by Google but which make sense to
        /// another provider. Therefore it's appropriate for an "unrecognised" filter type to just be ignored.
        /// </remarks>
        /// <param name="filter"></param>
        
        private void AppendQueryFilters(ITrovoFilter filter)
        {
            switch (filter.Type)
            {
                case TrovoFilterType.ByURLCollectionLabel:
                    AppendLabelFilterToQuery(filter);
                    break;
                case TrovoFilterType.ByDocumentType:
                    AppendFiletypeFilterToQuery(filter);
                    break;
                case TrovoFilterType.InDocumentTitle:
                    AppendInTitleFilterToQuery(filter);
                    break;
                case TrovoFilterType.InDocumentUrl:
                    AppendInUrlFilterToQuery(filter);
                    break;
                default:
                    break;
            }
            
        }

        private void AppendInUrlFilterToQuery(ITrovoFilter filter)
        {
            string inurlType = string.Empty;

            if (filter.Mode == TrovoFilterMode.UseSingleTerm)
            {
                inurlType = "inurl";
            }
            else if (filter.Mode == TrovoFilterMode.UseAllTerms)
            {
                inurlType = "allinurl";
            }
            else
            {
                throw new ArgumentException(String.Format("An InUrl search filter was applied with an incorrect mode of {0}. The mode must be either UseSingleTerm or UseAllTerms", filter.Mode.ToString()));
            }

            if (Query.SearchTerm.Length > 8 && Query.SearchTerm.Substring(0, 8).Equals("intitle:"))
            {
                Query.SearchTerm = String.Format("{0}+{1}:{2}", Query.SearchTerm, inurlType, _initialQueryTerm);
            }
            else
            {
                Query.SearchTerm = String.Format("{0}:{1}", inurlType, Query.SearchTerm);
            }

        }

        private void AppendInTitleFilterToQuery(ITrovoFilter filter)
        {
            string intitleType = string.Empty;

            if (filter.Mode == TrovoFilterMode.UseSingleTerm)
            {
                intitleType = "intitle";
            }
            else if (filter.Mode == TrovoFilterMode.UseAllTerms)
            {
                intitleType = "allintitle";
            }
            else
            {
                throw new ArgumentException(String.Format("An InTitle search filter was applied with an incorrect mode of {0}. The mode must be either UseSingleTerm or UseAllTerms", filter.Mode.ToString()));
            }

            if (Query.SearchTerm.Length > 6 && Query.SearchTerm.Substring(0, 6).Equals("inurl:"))
            {
                Query.SearchTerm = String.Format("{0}+{1}:{2}", Query.SearchTerm, intitleType, _initialQueryTerm);
            }
            else
            {
                Query.SearchTerm = String.Format("{0}:{1}", intitleType, Query.SearchTerm);
            }
        
        }

        private void AppendFiletypeFilterToQuery(ITrovoFilter filter)
        {
            string includeOrExclude = string.Empty;

            if (filter.Mode == TrovoFilterMode.Include)
            {
                includeOrExclude = "filetype";
            }
            else
            {
                includeOrExclude = "-filetype";
            }

            Query.SearchTerm += String.Format("+{0}:{1}", includeOrExclude, filter.Name);
        }

        private void AppendLabelFilterToQuery(ITrovoFilter filter)
        {
            string includeOrExclude = string.Empty;
            
            if (filter.Mode == TrovoFilterMode.Include)
            {
                includeOrExclude = "more";
            }
            else
            {
                includeOrExclude = "less";
            }

            Query.SearchTerm += String.Format("+{0}:{1}", includeOrExclude, filter.Name);
        }

        private GoogleQueryStringDecorator BuildAccountIdQueryDecorator()
        {
            GoogleQueryStringDecorator baseDecorator = BuildBaseDecoratorWithClientAndOutput();
            GoogleQueryStringDecorator accountIdDecorator = new GoogleQueryStringDecorator(baseDecorator);
            accountIdDecorator.ParameterName = GoogleSearchQueryParameterName.cx;
            accountIdDecorator.ParameterValue = _searchProviderAccountId;

            return accountIdDecorator;
        }

        private GoogleQueryStringDecorator BuildBaseDecoratorWithClientAndOutput()
        {
            GoogleQueryStringDecorator clientDecorator = new GoogleQueryStringDecorator();
            clientDecorator.ParameterName = GoogleSearchQueryParameterName.client;
            clientDecorator.ParameterValue = _GOOGLE_CLIENT_NAME;

            GoogleQueryStringDecorator outputDecorator = new GoogleQueryStringDecorator(clientDecorator);
            outputDecorator.ParameterName = GoogleSearchQueryParameterName.output;
            outputDecorator.ParameterValue = _GOOGLE_XML_OUTPUT;
            return outputDecorator;
        }
    }
}
