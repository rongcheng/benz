﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.239
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.239 版自动生成。
// 
#pragma warning disable 1591

namespace QJVRMS.Business.LogWS {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="LogServiceSoap", Namespace="http://tempuri.org/")]
    public partial class LogService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetNotPassResourcesOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetLogsOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public LogService() {
            this.Url = global::QJVRMS.Business.Properties.Settings.Default.QJVRMS_Business_LogWS_LogService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event HelloWorldCompletedEventHandler HelloWorldCompleted;
        
        /// <remarks/>
        public event AddCompletedEventHandler AddCompleted;
        
        /// <remarks/>
        public event GetNotPassResourcesCompletedEventHandler GetNotPassResourcesCompleted;
        
        /// <remarks/>
        public event GetLogsCompletedEventHandler GetLogsCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HelloWorld() {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void HelloWorldAsync() {
            this.HelloWorldAsync(null);
        }
        
        /// <remarks/>
        public void HelloWorldAsync(object userState) {
            if ((this.HelloWorldOperationCompleted == null)) {
                this.HelloWorldOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHelloWorldOperationCompleted);
            }
            this.InvokeAsync("HelloWorld", new object[0], this.HelloWorldOperationCompleted, userState);
        }
        
        private void OnHelloWorldOperationCompleted(object arg) {
            if ((this.HelloWorldCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HelloWorldCompleted(this, new HelloWorldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Add", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool Add(System.Guid id, System.Guid userId, string userName, string eventType, string eventResult, string eventContent, string ip, System.DateTime addDate) {
            object[] results = this.Invoke("Add", new object[] {
                        id,
                        userId,
                        userName,
                        eventType,
                        eventResult,
                        eventContent,
                        ip,
                        addDate});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void AddAsync(System.Guid id, System.Guid userId, string userName, string eventType, string eventResult, string eventContent, string ip, System.DateTime addDate) {
            this.AddAsync(id, userId, userName, eventType, eventResult, eventContent, ip, addDate, null);
        }
        
        /// <remarks/>
        public void AddAsync(System.Guid id, System.Guid userId, string userName, string eventType, string eventResult, string eventContent, string ip, System.DateTime addDate, object userState) {
            if ((this.AddOperationCompleted == null)) {
                this.AddOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddOperationCompleted);
            }
            this.InvokeAsync("Add", new object[] {
                        id,
                        userId,
                        userName,
                        eventType,
                        eventResult,
                        eventContent,
                        ip,
                        addDate}, this.AddOperationCompleted, userState);
        }
        
        private void OnAddOperationCompleted(object arg) {
            if ((this.AddCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddCompleted(this, new AddCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetNotPassResources", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetNotPassResources(string userName, System.DateTime startDate, System.DateTime endDate, int pageSize, int pageIndex) {
            object[] results = this.Invoke("GetNotPassResources", new object[] {
                        userName,
                        startDate,
                        endDate,
                        pageSize,
                        pageIndex});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetNotPassResourcesAsync(string userName, System.DateTime startDate, System.DateTime endDate, int pageSize, int pageIndex) {
            this.GetNotPassResourcesAsync(userName, startDate, endDate, pageSize, pageIndex, null);
        }
        
        /// <remarks/>
        public void GetNotPassResourcesAsync(string userName, System.DateTime startDate, System.DateTime endDate, int pageSize, int pageIndex, object userState) {
            if ((this.GetNotPassResourcesOperationCompleted == null)) {
                this.GetNotPassResourcesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetNotPassResourcesOperationCompleted);
            }
            this.InvokeAsync("GetNotPassResources", new object[] {
                        userName,
                        startDate,
                        endDate,
                        pageSize,
                        pageIndex}, this.GetNotPassResourcesOperationCompleted, userState);
        }
        
        private void OnGetNotPassResourcesOperationCompleted(object arg) {
            if ((this.GetNotPassResourcesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetNotPassResourcesCompleted(this, new GetNotPassResourcesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetLogs", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetLogs(string userName, string logType, System.DateTime startDate, System.DateTime endDate, int pageSize, int pageIndex) {
            object[] results = this.Invoke("GetLogs", new object[] {
                        userName,
                        logType,
                        startDate,
                        endDate,
                        pageSize,
                        pageIndex});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetLogsAsync(string userName, string logType, System.DateTime startDate, System.DateTime endDate, int pageSize, int pageIndex) {
            this.GetLogsAsync(userName, logType, startDate, endDate, pageSize, pageIndex, null);
        }
        
        /// <remarks/>
        public void GetLogsAsync(string userName, string logType, System.DateTime startDate, System.DateTime endDate, int pageSize, int pageIndex, object userState) {
            if ((this.GetLogsOperationCompleted == null)) {
                this.GetLogsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetLogsOperationCompleted);
            }
            this.InvokeAsync("GetLogs", new object[] {
                        userName,
                        logType,
                        startDate,
                        endDate,
                        pageSize,
                        pageIndex}, this.GetLogsOperationCompleted, userState);
        }
        
        private void OnGetLogsOperationCompleted(object arg) {
            if ((this.GetLogsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetLogsCompleted(this, new GetLogsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void HelloWorldCompletedEventHandler(object sender, HelloWorldCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HelloWorldCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HelloWorldCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void AddCompletedEventHandler(object sender, AddCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetNotPassResourcesCompletedEventHandler(object sender, GetNotPassResourcesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetNotPassResourcesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetNotPassResourcesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetLogsCompletedEventHandler(object sender, GetLogsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetLogsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetLogsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591