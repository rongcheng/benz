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

namespace QJVRMS.Business.GiftService {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="GiftServiceSoap", Namespace="http://qjDataAccess.org/")]
    public partial class GiftService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetGiftTypeListOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddGiftOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateGiftOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteGiftOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetGiftModelOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetNewIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetGiftListOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public GiftService() {
            this.Url = global::QJVRMS.Business.Properties.Settings.Default.QJVRMS_Business_GiftService_GiftService;
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
        public event GetGiftTypeListCompletedEventHandler GetGiftTypeListCompleted;
        
        /// <remarks/>
        public event AddGiftCompletedEventHandler AddGiftCompleted;
        
        /// <remarks/>
        public event UpdateGiftCompletedEventHandler UpdateGiftCompleted;
        
        /// <remarks/>
        public event DeleteGiftCompletedEventHandler DeleteGiftCompleted;
        
        /// <remarks/>
        public event GetGiftModelCompletedEventHandler GetGiftModelCompleted;
        
        /// <remarks/>
        public event GetNewIdCompletedEventHandler GetNewIdCompleted;
        
        /// <remarks/>
        public event GetGiftListCompletedEventHandler GetGiftListCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetGiftTypeList", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetGiftTypeList() {
            object[] results = this.Invoke("GetGiftTypeList", new object[0]);
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetGiftTypeListAsync() {
            this.GetGiftTypeListAsync(null);
        }
        
        /// <remarks/>
        public void GetGiftTypeListAsync(object userState) {
            if ((this.GetGiftTypeListOperationCompleted == null)) {
                this.GetGiftTypeListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetGiftTypeListOperationCompleted);
            }
            this.InvokeAsync("GetGiftTypeList", new object[0], this.GetGiftTypeListOperationCompleted, userState);
        }
        
        private void OnGetGiftTypeListOperationCompleted(object arg) {
            if ((this.GetGiftTypeListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetGiftTypeListCompleted(this, new GetGiftTypeListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/AddGift", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int AddGift(string id, string title, string typeId, int quantity, string imageId, int status, string remark) {
            object[] results = this.Invoke("AddGift", new object[] {
                        id,
                        title,
                        typeId,
                        quantity,
                        imageId,
                        status,
                        remark});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void AddGiftAsync(string id, string title, string typeId, int quantity, string imageId, int status, string remark) {
            this.AddGiftAsync(id, title, typeId, quantity, imageId, status, remark, null);
        }
        
        /// <remarks/>
        public void AddGiftAsync(string id, string title, string typeId, int quantity, string imageId, int status, string remark, object userState) {
            if ((this.AddGiftOperationCompleted == null)) {
                this.AddGiftOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddGiftOperationCompleted);
            }
            this.InvokeAsync("AddGift", new object[] {
                        id,
                        title,
                        typeId,
                        quantity,
                        imageId,
                        status,
                        remark}, this.AddGiftOperationCompleted, userState);
        }
        
        private void OnAddGiftOperationCompleted(object arg) {
            if ((this.AddGiftCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddGiftCompleted(this, new AddGiftCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/UpdateGift", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int UpdateGift(string id, string title, string typeId, int quantity, string imageId, int status, string remark) {
            object[] results = this.Invoke("UpdateGift", new object[] {
                        id,
                        title,
                        typeId,
                        quantity,
                        imageId,
                        status,
                        remark});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateGiftAsync(string id, string title, string typeId, int quantity, string imageId, int status, string remark) {
            this.UpdateGiftAsync(id, title, typeId, quantity, imageId, status, remark, null);
        }
        
        /// <remarks/>
        public void UpdateGiftAsync(string id, string title, string typeId, int quantity, string imageId, int status, string remark, object userState) {
            if ((this.UpdateGiftOperationCompleted == null)) {
                this.UpdateGiftOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateGiftOperationCompleted);
            }
            this.InvokeAsync("UpdateGift", new object[] {
                        id,
                        title,
                        typeId,
                        quantity,
                        imageId,
                        status,
                        remark}, this.UpdateGiftOperationCompleted, userState);
        }
        
        private void OnUpdateGiftOperationCompleted(object arg) {
            if ((this.UpdateGiftCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateGiftCompleted(this, new UpdateGiftCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/DeleteGift", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int DeleteGift(string id) {
            object[] results = this.Invoke("DeleteGift", new object[] {
                        id});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteGiftAsync(string id) {
            this.DeleteGiftAsync(id, null);
        }
        
        /// <remarks/>
        public void DeleteGiftAsync(string id, object userState) {
            if ((this.DeleteGiftOperationCompleted == null)) {
                this.DeleteGiftOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteGiftOperationCompleted);
            }
            this.InvokeAsync("DeleteGift", new object[] {
                        id}, this.DeleteGiftOperationCompleted, userState);
        }
        
        private void OnDeleteGiftOperationCompleted(object arg) {
            if ((this.DeleteGiftCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteGiftCompleted(this, new DeleteGiftCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetGiftModel", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetGiftModel(string id) {
            object[] results = this.Invoke("GetGiftModel", new object[] {
                        id});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetGiftModelAsync(string id) {
            this.GetGiftModelAsync(id, null);
        }
        
        /// <remarks/>
        public void GetGiftModelAsync(string id, object userState) {
            if ((this.GetGiftModelOperationCompleted == null)) {
                this.GetGiftModelOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetGiftModelOperationCompleted);
            }
            this.InvokeAsync("GetGiftModel", new object[] {
                        id}, this.GetGiftModelOperationCompleted, userState);
        }
        
        private void OnGetGiftModelOperationCompleted(object arg) {
            if ((this.GetGiftModelCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetGiftModelCompleted(this, new GetGiftModelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetNewId", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetNewId() {
            object[] results = this.Invoke("GetNewId", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetNewIdAsync() {
            this.GetNewIdAsync(null);
        }
        
        /// <remarks/>
        public void GetNewIdAsync(object userState) {
            if ((this.GetNewIdOperationCompleted == null)) {
                this.GetNewIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetNewIdOperationCompleted);
            }
            this.InvokeAsync("GetNewId", new object[0], this.GetNewIdOperationCompleted, userState);
        }
        
        private void OnGetNewIdOperationCompleted(object arg) {
            if ((this.GetNewIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetNewIdCompleted(this, new GetNewIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetGiftList", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetGiftList(string title, string typeId, string imageId) {
            object[] results = this.Invoke("GetGiftList", new object[] {
                        title,
                        typeId,
                        imageId});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetGiftListAsync(string title, string typeId, string imageId) {
            this.GetGiftListAsync(title, typeId, imageId, null);
        }
        
        /// <remarks/>
        public void GetGiftListAsync(string title, string typeId, string imageId, object userState) {
            if ((this.GetGiftListOperationCompleted == null)) {
                this.GetGiftListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetGiftListOperationCompleted);
            }
            this.InvokeAsync("GetGiftList", new object[] {
                        title,
                        typeId,
                        imageId}, this.GetGiftListOperationCompleted, userState);
        }
        
        private void OnGetGiftListOperationCompleted(object arg) {
            if ((this.GetGiftListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetGiftListCompleted(this, new GetGiftListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void GetGiftTypeListCompletedEventHandler(object sender, GetGiftTypeListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetGiftTypeListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetGiftTypeListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void AddGiftCompletedEventHandler(object sender, AddGiftCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddGiftCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddGiftCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void UpdateGiftCompletedEventHandler(object sender, UpdateGiftCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateGiftCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateGiftCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void DeleteGiftCompletedEventHandler(object sender, DeleteGiftCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteGiftCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteGiftCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetGiftModelCompletedEventHandler(object sender, GetGiftModelCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetGiftModelCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetGiftModelCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetNewIdCompletedEventHandler(object sender, GetNewIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetNewIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetNewIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetGiftListCompletedEventHandler(object sender, GetGiftListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetGiftListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetGiftListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
}

#pragma warning restore 1591