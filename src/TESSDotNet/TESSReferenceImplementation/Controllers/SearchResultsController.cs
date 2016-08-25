using System.Configuration;
using System.Web;
using System.Web.Mvc;

using TESSReferenceImplementation.Models;
using TrovoCrossCutting.Logging.Enumerations;
using TrovoSiteSearch.Enumerations;


namespace TESSReferenceImplementation.Controllers
{
    public class SearchResultsController : Controller
    {
        // GET: SearchResults

        public ViewResult Index(string q = null, int pageNo = 1)
        {
            TESSViewModel model = new TESSViewModel();

            if (string.IsNullOrEmpty(q))
            {
                model.SearchTermEntered = false;
            }
            else
            {
                model.SearchTermEntered = true;

                if (ConfigurationManager.AppSettings["providerType"].Equals("GoogleSiteSearch"))
                {
                    model.Provider = ProviderType.GoogleSiteSearch;
                }
                else if (ConfigurationManager.AppSettings["providerType"].Equals("MockProvider"))
                {
                    model.Provider = ProviderType.MockProvider;
                }


                if (ConfigurationManager.AppSettings["loggerType"].Equals("NullLogger"))
                {
                    model.Logger = LoggerType.NullLogger;
                }
                else if (ConfigurationManager.AppSettings["loggerType"].Equals("EnterpriseLibrary5Logger"))
                {
                    model.Logger = LoggerType.EnterpriseLibrary5Logger;
                }
                else if (ConfigurationManager.AppSettings["loggerType"].Equals("Log4Net"))
                {
                    model.Logger = LoggerType.Log4Net;
                }

                model.SearchProviderUrl = ConfigurationManager.AppSettings["searchProviderUrl"];
                model.SearchProviderAccountId = ConfigurationManager.AppSettings["searchProviderAccountId"];
                model.SearchProviderClientName = ConfigurationManager.AppSettings["searchProviderClientName"];

                model.NumberOfResultsPerPage = 20;
                model.NumberOfPageLinksToDisplay = 20;
                model.RetainFormatting = false;

                model.SearchTerm = q;

                model.Search(HttpUtility.UrlEncode(q), pageNo);

            }

            return View(model);
        }

    }
}