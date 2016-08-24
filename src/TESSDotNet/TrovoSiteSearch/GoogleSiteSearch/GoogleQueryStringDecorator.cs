using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrovoSiteSearch.GoogleSiteSearch
{
    public class GoogleQueryStringDecorator
    {
        public GoogleSearchQueryParameterName ParameterName { protected get; set; }

        public string ParameterValue { protected get; set; }

        public GoogleQueryStringDecorator  BaseQueryString { get; set; }
        
        public GoogleQueryStringDecorator()
        {

        }

        public GoogleQueryStringDecorator(GoogleQueryStringDecorator queryStringToDecorate)
        {
            BaseQueryString = queryStringToDecorate;
        }

        public string GenerateQueryString()
        {
            if (BaseQueryString == null)
            {
                return string.Format("{0}={1}", ParameterName.ToString(), ParameterValue);
            }
            else
            {
                return string.Format("{0}&{1}={2}", BaseQueryString.GenerateQueryString(), ParameterName.ToString(), ParameterValue);
            }
        }

    }
}
