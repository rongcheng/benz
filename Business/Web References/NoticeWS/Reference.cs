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

namespace QJVRMS.Business.NoticeWS {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="NoticesServiceSoap", Namespace="http://tempuri.org/")]
    public partial class NoticesService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetNoticesOperationCompleted;
        
        private System.Threading.SendOrPostCallback ShowNoticesOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetNoticeOperationCompleted;
        
        private System.Threading.SendOrPostCallback EditNoticeOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteNoticeOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public NoticesService() {
            this.Url = global::QJVRMS.Business.Properties.Settings.Default.QJVRMS_Business_NoticeWS_NoticesService;
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
        public event GetNoticesCompletedEventHandler GetNoticesCompleted;
        
        /// <remarks/>
        public event ShowNoticesCompletedEventHandler ShowNoticesCompleted;
        
        /// <remarks/>
        public event GetNoticeCompletedEventHandler GetNoticeCompleted;
        
        /// <remarks/>
        public event EditNoticeCompletedEventHandler EditNoticeCompleted;
        
        /// <remarks/>
        public event DeleteNoticeCompletedEventHandler DeleteNoticeCompleted;
        
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetNotices", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetNotices(int pageSize, int pageIndex, ref int totalRecord) {
            object[] results = this.Invoke("GetNotices", new object[] {
                        pageSize,
                        pageIndex,
                        totalRecord});
            totalRecord = ((int)(results[1]));
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetNoticesAsync(int pageSize, int pageIndex, int totalRecord) {
            this.GetNoticesAsync(pageSize, pageIndex, totalRecord, null);
        }
        
        /// <remarks/>
        public void GetNoticesAsync(int pageSize, int pageIndex, int totalRecord, object userState) {
            if ((this.GetNoticesOperationCompleted == null)) {
                this.GetNoticesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetNoticesOperationCompleted);
            }
            this.InvokeAsync("GetNotices", new object[] {
                        pageSize,
                        pageIndex,
                        totalRecord}, this.GetNoticesOperationCompleted, userState);
        }
        
        private void OnGetNoticesOperationCompleted(object arg) {
            if ((this.GetNoticesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetNoticesCompleted(this, new GetNoticesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ShowNotices", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable ShowNotices() {
            object[] results = this.Invoke("ShowNotices", new object[0]);
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void ShowNoticesAsync() {
            this.ShowNoticesAsync(null);
        }
        
        /// <remarks/>
        public void ShowNoticesAsync(object userState) {
            if ((this.ShowNoticesOperationCompleted == null)) {
                this.ShowNoticesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnShowNoticesOperationCompleted);
            }
            this.InvokeAsync("ShowNotices", new object[0], this.ShowNoticesOperationCompleted, userState);
        }
        
        private void OnShowNoticesOperationCompleted(object arg) {
            if ((this.ShowNoticesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ShowNoticesCompleted(this, new ShowNoticesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetNotice", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetNotice(string noticeId) {
            object[] results = this.Invoke("GetNotice", new object[] {
                        noticeId});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetNoticeAsync(string noticeId) {
            this.GetNoticeAsync(noticeId, null);
        }
        
        /// <remarks/>
        public void GetNoticeAsync(string noticeId, object userState) {
            if ((this.GetNoticeOperationCompleted == null)) {
                this.GetNoticeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetNoticeOperationCompleted);
            }
            this.InvokeAsync("GetNotice", new object[] {
                        noticeId}, this.GetNoticeOperationCompleted, userState);
        }
        
        private void OnGetNoticeOperationCompleted(object arg) {
            if ((this.GetNoticeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetNoticeCompleted(this, new GetNoticeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EditNotice", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool EditNotice(System.Guid noticeId, string noticeName, string noticeContent, string creator, string type) {
            object[] results = this.Invoke("EditNotice", new object[] {
                        noticeId,
                        noticeName,
                        noticeContent,
                        creator,
                        type});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void EditNoticeAsync(System.Guid noticeId, string noticeName, string noticeContent, string creator, string type) {
            this.EditNoticeAsync(noticeId, noticeName, noticeContent, creator, type, null);
        }
        
        /// <remarks/>
        public void EditNoticeAsync(System.Guid noticeId, string noticeName, string noticeContent, string creator, string type, object userState) {
            if ((this.EditNoticeOperationCompleted == null)) {
                this.EditNoticeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEditNoticeOperationCompleted);
            }
            this.InvokeAsync("EditNotice", new object[] {
                        noticeId,
                        noticeName,
                        noticeContent,
                        creator,
                        type}, this.EditNoticeOperationCompleted, userState);
        }
        
        private void OnEditNoticeOperationCompleted(object arg) {
            if ((this.EditNoticeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EditNoticeCompleted(this, new EditNoticeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DeleteNotice", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool DeleteNotice(System.Guid noticeId) {
            object[] results = this.Invoke("DeleteNotice", new object[] {
                        noticeId});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteNoticeAsync(System.Guid noticeId) {
            this.DeleteNoticeAsync(noticeId, null);
        }
        
        /// <remarks/>
        public void DeleteNoticeAsync(System.Guid noticeId, object userState) {
            if ((this.DeleteNoticeOperationCompleted == null)) {
                this.DeleteNoticeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteNoticeOperationCompleted);
            }
            this.InvokeAsync("DeleteNotice", new object[] {
                        noticeId}, this.DeleteNoticeOperationCompleted, userState);
        }
        
        private void OnDeleteNoticeOperationCompleted(object arg) {
            if ((this.DeleteNoticeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteNoticeCompleted(this, new DeleteNoticeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void GetNoticesCompletedEventHandler(object sender, GetNoticesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetNoticesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetNoticesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        
        /// <remarks/>
        public int totalRecord {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void ShowNoticesCompletedEventHandler(object sender, ShowNoticesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ShowNoticesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ShowNoticesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetNoticeCompletedEventHandler(object sender, GetNoticeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetNoticeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetNoticeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void EditNoticeCompletedEventHandler(object sender, EditNoticeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EditNoticeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EditNoticeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void DeleteNoticeCompletedEventHandler(object sender, DeleteNoticeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteNoticeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteNoticeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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