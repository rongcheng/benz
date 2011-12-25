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

namespace QJVRMS.Business.FunctionWS {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="FunctionServiceSoap", Namespace="http://qjDataAccess.org/")]
    public partial class FunctionService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetFunctionTableListOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTopFunctionListOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetUserFunctionRightOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetOwnFunctionOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFunctionListOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteFunctionByFunctionIDOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateFunctionOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddFunctionOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public FunctionService() {
            this.Url = global::QJVRMS.Business.Properties.Settings.Default.QJVRMS_Business_FunctionWS_FunctionService;
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
        public event GetFunctionTableListCompletedEventHandler GetFunctionTableListCompleted;
        
        /// <remarks/>
        public event GetTopFunctionListCompletedEventHandler GetTopFunctionListCompleted;
        
        /// <remarks/>
        public event GetUserFunctionRightCompletedEventHandler GetUserFunctionRightCompleted;
        
        /// <remarks/>
        public event GetOwnFunctionCompletedEventHandler GetOwnFunctionCompleted;
        
        /// <remarks/>
        public event GetFunctionListCompletedEventHandler GetFunctionListCompleted;
        
        /// <remarks/>
        public event DeleteFunctionByFunctionIDCompletedEventHandler DeleteFunctionByFunctionIDCompleted;
        
        /// <remarks/>
        public event UpdateFunctionCompletedEventHandler UpdateFunctionCompleted;
        
        /// <remarks/>
        public event AddFunctionCompletedEventHandler AddFunctionCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetFunctionTableList", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetFunctionTableList() {
            object[] results = this.Invoke("GetFunctionTableList", new object[0]);
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetFunctionTableListAsync() {
            this.GetFunctionTableListAsync(null);
        }
        
        /// <remarks/>
        public void GetFunctionTableListAsync(object userState) {
            if ((this.GetFunctionTableListOperationCompleted == null)) {
                this.GetFunctionTableListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFunctionTableListOperationCompleted);
            }
            this.InvokeAsync("GetFunctionTableList", new object[0], this.GetFunctionTableListOperationCompleted, userState);
        }
        
        private void OnGetFunctionTableListOperationCompleted(object arg) {
            if ((this.GetFunctionTableListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFunctionTableListCompleted(this, new GetFunctionTableListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetTopFunctionList", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetTopFunctionList() {
            object[] results = this.Invoke("GetTopFunctionList", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetTopFunctionListAsync() {
            this.GetTopFunctionListAsync(null);
        }
        
        /// <remarks/>
        public void GetTopFunctionListAsync(object userState) {
            if ((this.GetTopFunctionListOperationCompleted == null)) {
                this.GetTopFunctionListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTopFunctionListOperationCompleted);
            }
            this.InvokeAsync("GetTopFunctionList", new object[0], this.GetTopFunctionListOperationCompleted, userState);
        }
        
        private void OnGetTopFunctionListOperationCompleted(object arg) {
            if ((this.GetTopFunctionListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTopFunctionListCompleted(this, new GetTopFunctionListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetUserFunctionRight", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool GetUserFunctionRight(System.Guid userID) {
            object[] results = this.Invoke("GetUserFunctionRight", new object[] {
                        userID});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void GetUserFunctionRightAsync(System.Guid userID) {
            this.GetUserFunctionRightAsync(userID, null);
        }
        
        /// <remarks/>
        public void GetUserFunctionRightAsync(System.Guid userID, object userState) {
            if ((this.GetUserFunctionRightOperationCompleted == null)) {
                this.GetUserFunctionRightOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUserFunctionRightOperationCompleted);
            }
            this.InvokeAsync("GetUserFunctionRight", new object[] {
                        userID}, this.GetUserFunctionRightOperationCompleted, userState);
        }
        
        private void OnGetUserFunctionRightOperationCompleted(object arg) {
            if ((this.GetUserFunctionRightCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUserFunctionRightCompleted(this, new GetUserFunctionRightCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetOwnFunction", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetOwnFunction(System.Guid operatorId, int method) {
            object[] results = this.Invoke("GetOwnFunction", new object[] {
                        operatorId,
                        method});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetOwnFunctionAsync(System.Guid operatorId, int method) {
            this.GetOwnFunctionAsync(operatorId, method, null);
        }
        
        /// <remarks/>
        public void GetOwnFunctionAsync(System.Guid operatorId, int method, object userState) {
            if ((this.GetOwnFunctionOperationCompleted == null)) {
                this.GetOwnFunctionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetOwnFunctionOperationCompleted);
            }
            this.InvokeAsync("GetOwnFunction", new object[] {
                        operatorId,
                        method}, this.GetOwnFunctionOperationCompleted, userState);
        }
        
        private void OnGetOwnFunctionOperationCompleted(object arg) {
            if ((this.GetOwnFunctionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetOwnFunctionCompleted(this, new GetOwnFunctionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetFunctionList", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetFunctionList() {
            object[] results = this.Invoke("GetFunctionList", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetFunctionListAsync() {
            this.GetFunctionListAsync(null);
        }
        
        /// <remarks/>
        public void GetFunctionListAsync(object userState) {
            if ((this.GetFunctionListOperationCompleted == null)) {
                this.GetFunctionListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFunctionListOperationCompleted);
            }
            this.InvokeAsync("GetFunctionList", new object[0], this.GetFunctionListOperationCompleted, userState);
        }
        
        private void OnGetFunctionListOperationCompleted(object arg) {
            if ((this.GetFunctionListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFunctionListCompleted(this, new GetFunctionListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/DeleteFunctionByFunctionID", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool DeleteFunctionByFunctionID(System.Guid FunctionID) {
            object[] results = this.Invoke("DeleteFunctionByFunctionID", new object[] {
                        FunctionID});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteFunctionByFunctionIDAsync(System.Guid FunctionID) {
            this.DeleteFunctionByFunctionIDAsync(FunctionID, null);
        }
        
        /// <remarks/>
        public void DeleteFunctionByFunctionIDAsync(System.Guid FunctionID, object userState) {
            if ((this.DeleteFunctionByFunctionIDOperationCompleted == null)) {
                this.DeleteFunctionByFunctionIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteFunctionByFunctionIDOperationCompleted);
            }
            this.InvokeAsync("DeleteFunctionByFunctionID", new object[] {
                        FunctionID}, this.DeleteFunctionByFunctionIDOperationCompleted, userState);
        }
        
        private void OnDeleteFunctionByFunctionIDOperationCompleted(object arg) {
            if ((this.DeleteFunctionByFunctionIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteFunctionByFunctionIDCompleted(this, new DeleteFunctionByFunctionIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/UpdateFunction", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UpdateFunction(System.Guid funId, string name, string url, string desc, int oflag, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] System.Nullable<System.Guid> parentFunctionId) {
            object[] results = this.Invoke("UpdateFunction", new object[] {
                        funId,
                        name,
                        url,
                        desc,
                        oflag,
                        parentFunctionId});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateFunctionAsync(System.Guid funId, string name, string url, string desc, int oflag, System.Nullable<System.Guid> parentFunctionId) {
            this.UpdateFunctionAsync(funId, name, url, desc, oflag, parentFunctionId, null);
        }
        
        /// <remarks/>
        public void UpdateFunctionAsync(System.Guid funId, string name, string url, string desc, int oflag, System.Nullable<System.Guid> parentFunctionId, object userState) {
            if ((this.UpdateFunctionOperationCompleted == null)) {
                this.UpdateFunctionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateFunctionOperationCompleted);
            }
            this.InvokeAsync("UpdateFunction", new object[] {
                        funId,
                        name,
                        url,
                        desc,
                        oflag,
                        parentFunctionId}, this.UpdateFunctionOperationCompleted, userState);
        }
        
        private void OnUpdateFunctionOperationCompleted(object arg) {
            if ((this.UpdateFunctionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateFunctionCompleted(this, new UpdateFunctionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/AddFunction", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool AddFunction(string name, string url, string desc, int orderflag, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] System.Nullable<System.Guid> parentFunctionId) {
            object[] results = this.Invoke("AddFunction", new object[] {
                        name,
                        url,
                        desc,
                        orderflag,
                        parentFunctionId});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void AddFunctionAsync(string name, string url, string desc, int orderflag, System.Nullable<System.Guid> parentFunctionId) {
            this.AddFunctionAsync(name, url, desc, orderflag, parentFunctionId, null);
        }
        
        /// <remarks/>
        public void AddFunctionAsync(string name, string url, string desc, int orderflag, System.Nullable<System.Guid> parentFunctionId, object userState) {
            if ((this.AddFunctionOperationCompleted == null)) {
                this.AddFunctionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddFunctionOperationCompleted);
            }
            this.InvokeAsync("AddFunction", new object[] {
                        name,
                        url,
                        desc,
                        orderflag,
                        parentFunctionId}, this.AddFunctionOperationCompleted, userState);
        }
        
        private void OnAddFunctionOperationCompleted(object arg) {
            if ((this.AddFunctionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddFunctionCompleted(this, new AddFunctionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void GetFunctionTableListCompletedEventHandler(object sender, GetFunctionTableListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFunctionTableListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFunctionTableListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetTopFunctionListCompletedEventHandler(object sender, GetTopFunctionListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTopFunctionListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTopFunctionListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetUserFunctionRightCompletedEventHandler(object sender, GetUserFunctionRightCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUserFunctionRightCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUserFunctionRightCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetOwnFunctionCompletedEventHandler(object sender, GetOwnFunctionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetOwnFunctionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetOwnFunctionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetFunctionListCompletedEventHandler(object sender, GetFunctionListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFunctionListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFunctionListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void DeleteFunctionByFunctionIDCompletedEventHandler(object sender, DeleteFunctionByFunctionIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteFunctionByFunctionIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteFunctionByFunctionIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void UpdateFunctionCompletedEventHandler(object sender, UpdateFunctionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateFunctionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateFunctionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void AddFunctionCompletedEventHandler(object sender, AddFunctionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddFunctionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddFunctionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
}

#pragma warning restore 1591