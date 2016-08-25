using System;
using System.Collections.Generic;
using System.Linq;

using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.Enumerations;

namespace TrovoSiteSearch
{
    public class TrovoSearchRequestFactory
    {
        public ITrovoSearchRequest GetProviderRequest(ProviderType providerType)
        {
            switch(providerType)
            {
                case ProviderType.GoogleSiteSearch:
                    return new GoogleSiteSearch.GoogleSearchRequest();

                case ProviderType.MockProvider:
                    return new MockSearch.MockSearchRequest();
                    
                default:
                    throw new InvalidOperationException("An invalid provider type was passed to the TrovoSearchRequestFactory");

            }

        }

        public Dictionary<string, string> GetConfigVariables(ProviderType providerType)
        {
            Dictionary<string, string> configSettings = new Dictionary<string, string>();

            List<string> configVariables;
            
            switch(providerType)
            {
                case ProviderType.GoogleSiteSearch:
                    configVariables = Enum.GetNames(typeof(GoogleSiteSearch.GoogleSiteSearchConfigSettings)).ToList<string>();
                    break;
                case ProviderType.MockProvider:
                    configVariables = Enum.GetNames(typeof(MockSearch.MockConfigSettings)).ToList<string>();
                    break;

                default:
                    throw new InvalidOperationException("An invalid provider type was passed to the TrovoSearchRequestFactory");
            }

            foreach (string varName in configVariables)
            {
                configSettings.Add(varName, string.Empty);
            }

            return configSettings;
        }


    }
}
