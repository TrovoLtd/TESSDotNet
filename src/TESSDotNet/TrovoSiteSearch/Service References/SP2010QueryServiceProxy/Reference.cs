﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrovoSiteSearch.SP2010QueryServiceProxy {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://microsoft.com/webservices/OfficeServer/QueryService", ConfigurationName="SP2010QueryServiceProxy.QueryServiceSoap")]
    public interface QueryServiceSoap {
        
        // CODEGEN: Generating message contract since the wrapper namespace (urn:Microsoft.Search) of message QueryRequest does not match the default value (http://microsoft.com/webservices/OfficeServer/QueryService)
        [System.ServiceModel.OperationContractAttribute(Action="urn:Microsoft.Search/Query", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        TrovoSiteSearch.SP2010QueryServiceProxy.QueryResponse Query(TrovoSiteSearch.SP2010QueryServiceProxy.QueryRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Microsoft.Search/Query", ReplyAction="*")]
        System.Threading.Tasks.Task<TrovoSiteSearch.SP2010QueryServiceProxy.QueryResponse> QueryAsync(TrovoSiteSearch.SP2010QueryServiceProxy.QueryRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/OfficeServer/QueryService/QueryEx", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet QueryEx(string queryXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/OfficeServer/QueryService/QueryEx", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> QueryExAsync(string queryXml);
        
        // CODEGEN: Generating message contract since the wrapper namespace (urn:Microsoft.Search) of message RegistrationRequest does not match the default value (http://microsoft.com/webservices/OfficeServer/QueryService)
        [System.ServiceModel.OperationContractAttribute(Action="urn:Microsoft.Search/Registration", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        TrovoSiteSearch.SP2010QueryServiceProxy.RegistrationResponse Registration(TrovoSiteSearch.SP2010QueryServiceProxy.RegistrationRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Microsoft.Search/Registration", ReplyAction="*")]
        System.Threading.Tasks.Task<TrovoSiteSearch.SP2010QueryServiceProxy.RegistrationResponse> RegistrationAsync(TrovoSiteSearch.SP2010QueryServiceProxy.RegistrationRequest request);
        
        // CODEGEN: Generating message contract since the wrapper namespace (urn:Microsoft.Search) of message StatusRequest does not match the default value (http://microsoft.com/webservices/OfficeServer/QueryService)
        [System.ServiceModel.OperationContractAttribute(Action="urn:Microsoft.Search/Status", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        TrovoSiteSearch.SP2010QueryServiceProxy.StatusResponse Status(TrovoSiteSearch.SP2010QueryServiceProxy.StatusRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Microsoft.Search/Status", ReplyAction="*")]
        System.Threading.Tasks.Task<TrovoSiteSearch.SP2010QueryServiceProxy.StatusResponse> StatusAsync(TrovoSiteSearch.SP2010QueryServiceProxy.StatusRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/OfficeServer/QueryService/GetPortalSearchInfo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetPortalSearchInfo();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/OfficeServer/QueryService/GetPortalSearchInfo", ReplyAction="*")]
        System.Threading.Tasks.Task<string> GetPortalSearchInfoAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/OfficeServer/QueryService/GetSearchMetadata", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetSearchMetadata();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/OfficeServer/QueryService/GetSearchMetadata", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetSearchMetadataAsync();
        
        // CODEGEN: Generating message contract since the wrapper namespace (urn:Microsoft.Search) of message RecordClickRequest does not match the default value (http://microsoft.com/webservices/OfficeServer/QueryService)
        [System.ServiceModel.OperationContractAttribute(Action="urn:Microsoft.Search/RecordClick", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        TrovoSiteSearch.SP2010QueryServiceProxy.RecordClickResponse RecordClick(TrovoSiteSearch.SP2010QueryServiceProxy.RecordClickRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Microsoft.Search/RecordClick", ReplyAction="*")]
        System.Threading.Tasks.Task<TrovoSiteSearch.SP2010QueryServiceProxy.RecordClickResponse> RecordClickAsync(TrovoSiteSearch.SP2010QueryServiceProxy.RecordClickRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/OfficeServer/QueryService/GetQuerySuggestions", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string[] GetQuerySuggestions(string queryXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/OfficeServer/QueryService/GetQuerySuggestions", ReplyAction="*")]
        System.Threading.Tasks.Task<string[]> GetQuerySuggestionsAsync(string queryXml);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Query", WrapperNamespace="urn:Microsoft.Search", IsWrapped=true)]
    public partial class QueryRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:Microsoft.Search", Order=0)]
        public string queryXml;
        
        public QueryRequest() {
        }
        
        public QueryRequest(string queryXml) {
            this.queryXml = queryXml;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="QueryResponse", WrapperNamespace="urn:Microsoft.Search", IsWrapped=true)]
    public partial class QueryResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:Microsoft.Search", Order=0)]
        public string QueryResult;
        
        public QueryResponse() {
        }
        
        public QueryResponse(string QueryResult) {
            this.QueryResult = QueryResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Registration", WrapperNamespace="urn:Microsoft.Search", IsWrapped=true)]
    public partial class RegistrationRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:Microsoft.Search", Order=0)]
        public string registrationXml;
        
        public RegistrationRequest() {
        }
        
        public RegistrationRequest(string registrationXml) {
            this.registrationXml = registrationXml;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="RegistrationResponse", WrapperNamespace="urn:Microsoft.Search", IsWrapped=true)]
    public partial class RegistrationResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:Microsoft.Search", Order=0)]
        public string RegistrationResult;
        
        public RegistrationResponse() {
        }
        
        public RegistrationResponse(string RegistrationResult) {
            this.RegistrationResult = RegistrationResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Status", WrapperNamespace="urn:Microsoft.Search", IsWrapped=true)]
    public partial class StatusRequest {
        
        public StatusRequest() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="StatusResponse", WrapperNamespace="urn:Microsoft.Search", IsWrapped=true)]
    public partial class StatusResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:Microsoft.Search", Order=0)]
        public string StatusResult;
        
        public StatusResponse() {
        }
        
        public StatusResponse(string StatusResult) {
            this.StatusResult = StatusResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="RecordClick", WrapperNamespace="urn:Microsoft.Search", IsWrapped=true)]
    public partial class RecordClickRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:Microsoft.Search", Order=0)]
        public string clickInfoXml;
        
        public RecordClickRequest() {
        }
        
        public RecordClickRequest(string clickInfoXml) {
            this.clickInfoXml = clickInfoXml;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="RecordClickResponse", WrapperNamespace="urn:Microsoft.Search", IsWrapped=true)]
    public partial class RecordClickResponse {
        
        public RecordClickResponse() {
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface QueryServiceSoapChannel : TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class QueryServiceSoapClient : System.ServiceModel.ClientBase<TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap>, TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap {
        
        public QueryServiceSoapClient() {
        }
        
        public QueryServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public QueryServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QueryServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QueryServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TrovoSiteSearch.SP2010QueryServiceProxy.QueryResponse TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap.Query(TrovoSiteSearch.SP2010QueryServiceProxy.QueryRequest request) {
            return base.Channel.Query(request);
        }
        
        public string Query(string queryXml) {
            TrovoSiteSearch.SP2010QueryServiceProxy.QueryRequest inValue = new TrovoSiteSearch.SP2010QueryServiceProxy.QueryRequest();
            inValue.queryXml = queryXml;
            TrovoSiteSearch.SP2010QueryServiceProxy.QueryResponse retVal = ((TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap)(this)).Query(inValue);
            return retVal.QueryResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<TrovoSiteSearch.SP2010QueryServiceProxy.QueryResponse> TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap.QueryAsync(TrovoSiteSearch.SP2010QueryServiceProxy.QueryRequest request) {
            return base.Channel.QueryAsync(request);
        }
        
        public System.Threading.Tasks.Task<TrovoSiteSearch.SP2010QueryServiceProxy.QueryResponse> QueryAsync(string queryXml) {
            TrovoSiteSearch.SP2010QueryServiceProxy.QueryRequest inValue = new TrovoSiteSearch.SP2010QueryServiceProxy.QueryRequest();
            inValue.queryXml = queryXml;
            return ((TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap)(this)).QueryAsync(inValue);
        }
        
        public System.Data.DataSet QueryEx(string queryXml) {
            return base.Channel.QueryEx(queryXml);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> QueryExAsync(string queryXml) {
            return base.Channel.QueryExAsync(queryXml);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TrovoSiteSearch.SP2010QueryServiceProxy.RegistrationResponse TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap.Registration(TrovoSiteSearch.SP2010QueryServiceProxy.RegistrationRequest request) {
            return base.Channel.Registration(request);
        }
        
        public string Registration(string registrationXml) {
            TrovoSiteSearch.SP2010QueryServiceProxy.RegistrationRequest inValue = new TrovoSiteSearch.SP2010QueryServiceProxy.RegistrationRequest();
            inValue.registrationXml = registrationXml;
            TrovoSiteSearch.SP2010QueryServiceProxy.RegistrationResponse retVal = ((TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap)(this)).Registration(inValue);
            return retVal.RegistrationResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<TrovoSiteSearch.SP2010QueryServiceProxy.RegistrationResponse> TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap.RegistrationAsync(TrovoSiteSearch.SP2010QueryServiceProxy.RegistrationRequest request) {
            return base.Channel.RegistrationAsync(request);
        }
        
        public System.Threading.Tasks.Task<TrovoSiteSearch.SP2010QueryServiceProxy.RegistrationResponse> RegistrationAsync(string registrationXml) {
            TrovoSiteSearch.SP2010QueryServiceProxy.RegistrationRequest inValue = new TrovoSiteSearch.SP2010QueryServiceProxy.RegistrationRequest();
            inValue.registrationXml = registrationXml;
            return ((TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap)(this)).RegistrationAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TrovoSiteSearch.SP2010QueryServiceProxy.StatusResponse TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap.Status(TrovoSiteSearch.SP2010QueryServiceProxy.StatusRequest request) {
            return base.Channel.Status(request);
        }
        
        public string Status() {
            TrovoSiteSearch.SP2010QueryServiceProxy.StatusRequest inValue = new TrovoSiteSearch.SP2010QueryServiceProxy.StatusRequest();
            TrovoSiteSearch.SP2010QueryServiceProxy.StatusResponse retVal = ((TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap)(this)).Status(inValue);
            return retVal.StatusResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<TrovoSiteSearch.SP2010QueryServiceProxy.StatusResponse> TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap.StatusAsync(TrovoSiteSearch.SP2010QueryServiceProxy.StatusRequest request) {
            return base.Channel.StatusAsync(request);
        }
        
        public System.Threading.Tasks.Task<TrovoSiteSearch.SP2010QueryServiceProxy.StatusResponse> StatusAsync() {
            TrovoSiteSearch.SP2010QueryServiceProxy.StatusRequest inValue = new TrovoSiteSearch.SP2010QueryServiceProxy.StatusRequest();
            return ((TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap)(this)).StatusAsync(inValue);
        }
        
        public string GetPortalSearchInfo() {
            return base.Channel.GetPortalSearchInfo();
        }
        
        public System.Threading.Tasks.Task<string> GetPortalSearchInfoAsync() {
            return base.Channel.GetPortalSearchInfoAsync();
        }
        
        public System.Data.DataSet GetSearchMetadata() {
            return base.Channel.GetSearchMetadata();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetSearchMetadataAsync() {
            return base.Channel.GetSearchMetadataAsync();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TrovoSiteSearch.SP2010QueryServiceProxy.RecordClickResponse TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap.RecordClick(TrovoSiteSearch.SP2010QueryServiceProxy.RecordClickRequest request) {
            return base.Channel.RecordClick(request);
        }
        
        public void RecordClick(string clickInfoXml) {
            TrovoSiteSearch.SP2010QueryServiceProxy.RecordClickRequest inValue = new TrovoSiteSearch.SP2010QueryServiceProxy.RecordClickRequest();
            inValue.clickInfoXml = clickInfoXml;
            TrovoSiteSearch.SP2010QueryServiceProxy.RecordClickResponse retVal = ((TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap)(this)).RecordClick(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<TrovoSiteSearch.SP2010QueryServiceProxy.RecordClickResponse> TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap.RecordClickAsync(TrovoSiteSearch.SP2010QueryServiceProxy.RecordClickRequest request) {
            return base.Channel.RecordClickAsync(request);
        }
        
        public System.Threading.Tasks.Task<TrovoSiteSearch.SP2010QueryServiceProxy.RecordClickResponse> RecordClickAsync(string clickInfoXml) {
            TrovoSiteSearch.SP2010QueryServiceProxy.RecordClickRequest inValue = new TrovoSiteSearch.SP2010QueryServiceProxy.RecordClickRequest();
            inValue.clickInfoXml = clickInfoXml;
            return ((TrovoSiteSearch.SP2010QueryServiceProxy.QueryServiceSoap)(this)).RecordClickAsync(inValue);
        }
        
        public string[] GetQuerySuggestions(string queryXml) {
            return base.Channel.GetQuerySuggestions(queryXml);
        }
        
        public System.Threading.Tasks.Task<string[]> GetQuerySuggestionsAsync(string queryXml) {
            return base.Channel.GetQuerySuggestionsAsync(queryXml);
        }
    }
}
