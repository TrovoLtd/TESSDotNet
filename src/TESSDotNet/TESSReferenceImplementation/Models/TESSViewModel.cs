using System;
using System.Collections.Generic;

using TrovoSiteSearch;
using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.Enumerations;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Enumerations;

namespace TESSReferenceImplementation.Models
{
    public class TESSViewModel
    {

        /// <summary>
        /// Validation setting - even though validation should be setup in the view so you can't submit a search
        /// without a search term, this has been added in the spirit of "all input is evil".
        /// </summary>
        public bool SearchTermEntered { get; set; }

        /// <summary>
        /// Logic to display error information to the user if an error occurs
        /// </summary>
        public bool ErrorOccurred { get; set; }

        // Properties for use back in the view

        /// <summary>
        /// The unescaped search term for use for display back in the view
        /// </summary>
        public string SearchTerm { get; set; }
        public string SearchTermEscaped { get; set; }
        public int CurrentPage { get; set; }

        /// <summary>
        /// The type of error that occurred to display if an error occurs
        /// </summary>
        public string ErrorType { get; set; }

        /// <summary>
        /// The error message to display if an error occurs
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The page of results for display back in the view
        /// </summary>
        public ITrovoResultPage ResultPage { get; set; }

        public PagingInfo PageInfo { get; set; }

        /// <summary>
        /// Configuration settings for the Trovo Enhanced Site Search plugin.
        /// It is expected that these will be passed in from the controller, and set from
        /// web config or CMS plugin config settings
        /// </summary>

        public ProviderType Provider { get; set; }
        public LoggerType Logger { get; set; }

        public string SearchProviderUrl { get; set; }
        public string SearchProviderAccountId { get; set; }
        public string SearchProviderClientName { get; set; }
        public int NumberOfResultsPerPage { get; set; }
        public int NumberOfPageLinksToDisplay { get; set; }
        public bool RetainFormatting { get; set; }

        public TESSViewModel() {}

        public void Search(string q, int pageNo = 1)
        {
            if (String.IsNullOrEmpty(q))
            {
                SearchTermEntered = false;
            }
            else
            {
                ErrorOccurred = false;

                try
                {
                    TrovoSearchFacade searchFacade = new TrovoSearchFacade(Provider, Logger);

                    Dictionary<string, string> configSettings = searchFacade.ConfigSettings;

                    configSettings["SearchProviderUrl"] = SearchProviderUrl;
                    configSettings["NumberOfResultsPerPage"] = NumberOfResultsPerPage.ToString();
                    configSettings["RetainProviderFormatting"] = RetainFormatting.ToString();

                    if(Provider == ProviderType.GoogleSiteSearch)
                    {
                        configSettings["SearchProviderClientName"] = SearchProviderClientName;
                        configSettings["SearchProviderAccountId"] = SearchProviderAccountId;
                    }

                    TrovoQuery query = new TrovoQuery();

                    string _mainFilterRefinementLabelName = String.Empty;
                    string _mainFilterRefinementLabelReadableName = String.Empty;

                    TrovoFilter mainFilter = new TrovoFilter();

                    /*_mainFilterRefinementLabelName = "science";
                    _mainFilterRefinementLabelReadableName = "Science";*/
                    
                    if (!String.IsNullOrEmpty(_mainFilterRefinementLabelName))
                    {
                        mainFilter.Name = _mainFilterRefinementLabelName;
                        mainFilter.ReadableName = _mainFilterRefinementLabelReadableName;
                        mainFilter.Type = TrovoFilterType.ByURLCollectionLabel;
                        mainFilter.Mode = TrovoFilterMode.Include;
                        query.Filters.Add(mainFilter);
                    }

                    query.PageNumber = pageNo;
                    query.SearchTerm = q;

                    ResultPage = searchFacade.Search(query, null);

                    PageInfo = new PagingInfo { CurrentPage = pageNo, TotalPages = ResultPage.TotalNumberOfPages, SearchQuery = q, NumberOfLinksToDisplay= NumberOfPageLinksToDisplay };
                    
                    CurrentPage = pageNo;
                    
                    //searchFacade.Dispose();
                }
                catch(Exception ex)
                {
                    SearchTermEscaped = q;
                    ErrorOccurred = true;
                    ErrorType = ex.GetType().ToString();
                    ErrorMessage = ex.Message;
                }

            }

        }

    }
}