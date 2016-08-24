using System.Collections.Generic;
using TrovoCrossCutting.Logging.Interfaces;

namespace TrovoSiteSearch.Interfaces
{
    public interface ITrovoSearchRequest
    {

        ILogWriter LogWriter { get; set; }

        string QueryString { get; set; }
        
        Dictionary<string, string> ConfigSettings { get; set; }

        ITrovoResultPage executeSearch(ITrovoQuery query, ITrovoSearchCommand command);

        ITrovoResultPage executeSearch(ITrovoQuery query);
    }
}
