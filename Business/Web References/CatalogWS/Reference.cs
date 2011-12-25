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

namespace QJVRMS.Business.CatalogWS {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="CatalogServiceSoap", Namespace="http://qjDataAccess.org/")]
    public partial class CatalogService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetCatalogOperationCompleted;
        
        private System.Threading.SendOrPostCallback CreateCatalogOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteCatalogOperationCompleted;
        
        private System.Threading.SendOrPostCallback ModifyCatalogOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetCatalogsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetCatalogTableByParentIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTopCatalogOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAllCatalogOperationCompleted;
        
        private System.Threading.SendOrPostCallback CheckCatalogRightOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetCatalogByMethodOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetCategoryPicCountOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public CatalogService() {
            this.Url = global::QJVRMS.Business.Properties.Settings.Default.QJVRMS_Business_CatalogWS_CatalogService;
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
        public event GetCatalogCompletedEventHandler GetCatalogCompleted;
        
        /// <remarks/>
        public event CreateCatalogCompletedEventHandler CreateCatalogCompleted;
        
        /// <remarks/>
        public event DeleteCatalogCompletedEventHandler DeleteCatalogCompleted;
        
        /// <remarks/>
        public event ModifyCatalogCompletedEventHandler ModifyCatalogCompleted;
        
        /// <remarks/>
        public event GetCatalogsCompletedEventHandler GetCatalogsCompleted;
        
        /// <remarks/>
        public event GetCatalogTableByParentIdCompletedEventHandler GetCatalogTableByParentIdCompleted;
        
        /// <remarks/>
        public event GetTopCatalogCompletedEventHandler GetTopCatalogCompleted;
        
        /// <remarks/>
        public event GetAllCatalogCompletedEventHandler GetAllCatalogCompleted;
        
        /// <remarks/>
        public event CheckCatalogRightCompletedEventHandler CheckCatalogRightCompleted;
        
        /// <remarks/>
        public event GetCatalogByMethodCompletedEventHandler GetCatalogByMethodCompleted;
        
        /// <remarks/>
        public event GetCategoryPicCountCompletedEventHandler GetCategoryPicCountCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetCatalog", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetCatalog(System.Guid catalogId) {
            object[] results = this.Invoke("GetCatalog", new object[] {
                        catalogId});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetCatalogAsync(System.Guid catalogId) {
            this.GetCatalogAsync(catalogId, null);
        }
        
        /// <remarks/>
        public void GetCatalogAsync(System.Guid catalogId, object userState) {
            if ((this.GetCatalogOperationCompleted == null)) {
                this.GetCatalogOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCatalogOperationCompleted);
            }
            this.InvokeAsync("GetCatalog", new object[] {
                        catalogId}, this.GetCatalogOperationCompleted, userState);
        }
        
        private void OnGetCatalogOperationCompleted(object arg) {
            if ((this.GetCatalogCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCatalogCompleted(this, new GetCatalogCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/CreateCatalog", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Guid CreateCatalog(string catalogName, System.Guid parentCatalogId, string descrption) {
            object[] results = this.Invoke("CreateCatalog", new object[] {
                        catalogName,
                        parentCatalogId,
                        descrption});
            return ((System.Guid)(results[0]));
        }
        
        /// <remarks/>
        public void CreateCatalogAsync(string catalogName, System.Guid parentCatalogId, string descrption) {
            this.CreateCatalogAsync(catalogName, parentCatalogId, descrption, null);
        }
        
        /// <remarks/>
        public void CreateCatalogAsync(string catalogName, System.Guid parentCatalogId, string descrption, object userState) {
            if ((this.CreateCatalogOperationCompleted == null)) {
                this.CreateCatalogOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateCatalogOperationCompleted);
            }
            this.InvokeAsync("CreateCatalog", new object[] {
                        catalogName,
                        parentCatalogId,
                        descrption}, this.CreateCatalogOperationCompleted, userState);
        }
        
        private void OnCreateCatalogOperationCompleted(object arg) {
            if ((this.CreateCatalogCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateCatalogCompleted(this, new CreateCatalogCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/DeleteCatalog", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool DeleteCatalog(System.Guid catalogId) {
            object[] results = this.Invoke("DeleteCatalog", new object[] {
                        catalogId});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteCatalogAsync(System.Guid catalogId) {
            this.DeleteCatalogAsync(catalogId, null);
        }
        
        /// <remarks/>
        public void DeleteCatalogAsync(System.Guid catalogId, object userState) {
            if ((this.DeleteCatalogOperationCompleted == null)) {
                this.DeleteCatalogOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteCatalogOperationCompleted);
            }
            this.InvokeAsync("DeleteCatalog", new object[] {
                        catalogId}, this.DeleteCatalogOperationCompleted, userState);
        }
        
        private void OnDeleteCatalogOperationCompleted(object arg) {
            if ((this.DeleteCatalogCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteCatalogCompleted(this, new DeleteCatalogCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/ModifyCatalog", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ModifyCatalog(System.Guid catalogId, string catalogName, string catalogOrder, string descri) {
            object[] results = this.Invoke("ModifyCatalog", new object[] {
                        catalogId,
                        catalogName,
                        catalogOrder,
                        descri});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ModifyCatalogAsync(System.Guid catalogId, string catalogName, string catalogOrder, string descri) {
            this.ModifyCatalogAsync(catalogId, catalogName, catalogOrder, descri, null);
        }
        
        /// <remarks/>
        public void ModifyCatalogAsync(System.Guid catalogId, string catalogName, string catalogOrder, string descri, object userState) {
            if ((this.ModifyCatalogOperationCompleted == null)) {
                this.ModifyCatalogOperationCompleted = new System.Threading.SendOrPostCallback(this.OnModifyCatalogOperationCompleted);
            }
            this.InvokeAsync("ModifyCatalog", new object[] {
                        catalogId,
                        catalogName,
                        catalogOrder,
                        descri}, this.ModifyCatalogOperationCompleted, userState);
        }
        
        private void OnModifyCatalogOperationCompleted(object arg) {
            if ((this.ModifyCatalogCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ModifyCatalogCompleted(this, new ModifyCatalogCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetCatalogs", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetCatalogs(string catalogid) {
            object[] results = this.Invoke("GetCatalogs", new object[] {
                        catalogid});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetCatalogsAsync(string catalogid) {
            this.GetCatalogsAsync(catalogid, null);
        }
        
        /// <remarks/>
        public void GetCatalogsAsync(string catalogid, object userState) {
            if ((this.GetCatalogsOperationCompleted == null)) {
                this.GetCatalogsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCatalogsOperationCompleted);
            }
            this.InvokeAsync("GetCatalogs", new object[] {
                        catalogid}, this.GetCatalogsOperationCompleted, userState);
        }
        
        private void OnGetCatalogsOperationCompleted(object arg) {
            if ((this.GetCatalogsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCatalogsCompleted(this, new GetCatalogsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetCatalogTableByParentId", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetCatalogTableByParentId(System.Guid parentId) {
            object[] results = this.Invoke("GetCatalogTableByParentId", new object[] {
                        parentId});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetCatalogTableByParentIdAsync(System.Guid parentId) {
            this.GetCatalogTableByParentIdAsync(parentId, null);
        }
        
        /// <remarks/>
        public void GetCatalogTableByParentIdAsync(System.Guid parentId, object userState) {
            if ((this.GetCatalogTableByParentIdOperationCompleted == null)) {
                this.GetCatalogTableByParentIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCatalogTableByParentIdOperationCompleted);
            }
            this.InvokeAsync("GetCatalogTableByParentId", new object[] {
                        parentId}, this.GetCatalogTableByParentIdOperationCompleted, userState);
        }
        
        private void OnGetCatalogTableByParentIdOperationCompleted(object arg) {
            if ((this.GetCatalogTableByParentIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCatalogTableByParentIdCompleted(this, new GetCatalogTableByParentIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetTopCatalog", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetTopCatalog() {
            object[] results = this.Invoke("GetTopCatalog", new object[0]);
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetTopCatalogAsync() {
            this.GetTopCatalogAsync(null);
        }
        
        /// <remarks/>
        public void GetTopCatalogAsync(object userState) {
            if ((this.GetTopCatalogOperationCompleted == null)) {
                this.GetTopCatalogOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTopCatalogOperationCompleted);
            }
            this.InvokeAsync("GetTopCatalog", new object[0], this.GetTopCatalogOperationCompleted, userState);
        }
        
        private void OnGetTopCatalogOperationCompleted(object arg) {
            if ((this.GetTopCatalogCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTopCatalogCompleted(this, new GetTopCatalogCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetAllCatalog", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetAllCatalog() {
            object[] results = this.Invoke("GetAllCatalog", new object[0]);
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetAllCatalogAsync() {
            this.GetAllCatalogAsync(null);
        }
        
        /// <remarks/>
        public void GetAllCatalogAsync(object userState) {
            if ((this.GetAllCatalogOperationCompleted == null)) {
                this.GetAllCatalogOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAllCatalogOperationCompleted);
            }
            this.InvokeAsync("GetAllCatalog", new object[0], this.GetAllCatalogOperationCompleted, userState);
        }
        
        private void OnGetAllCatalogOperationCompleted(object arg) {
            if ((this.GetAllCatalogCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAllCatalogCompleted(this, new GetAllCatalogCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/CheckCatalogRight", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CheckCatalogRight(System.Guid userID, System.Guid cataID) {
            object[] results = this.Invoke("CheckCatalogRight", new object[] {
                        userID,
                        cataID});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void CheckCatalogRightAsync(System.Guid userID, System.Guid cataID) {
            this.CheckCatalogRightAsync(userID, cataID, null);
        }
        
        /// <remarks/>
        public void CheckCatalogRightAsync(System.Guid userID, System.Guid cataID, object userState) {
            if ((this.CheckCatalogRightOperationCompleted == null)) {
                this.CheckCatalogRightOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckCatalogRightOperationCompleted);
            }
            this.InvokeAsync("CheckCatalogRight", new object[] {
                        userID,
                        cataID}, this.CheckCatalogRightOperationCompleted, userState);
        }
        
        private void OnCheckCatalogRightOperationCompleted(object arg) {
            if ((this.CheckCatalogRightCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckCatalogRightCompleted(this, new CheckCatalogRightCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetCatalogByMethod", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetCatalogByMethod(System.Guid userId, int method) {
            object[] results = this.Invoke("GetCatalogByMethod", new object[] {
                        userId,
                        method});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetCatalogByMethodAsync(System.Guid userId, int method) {
            this.GetCatalogByMethodAsync(userId, method, null);
        }
        
        /// <remarks/>
        public void GetCatalogByMethodAsync(System.Guid userId, int method, object userState) {
            if ((this.GetCatalogByMethodOperationCompleted == null)) {
                this.GetCatalogByMethodOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCatalogByMethodOperationCompleted);
            }
            this.InvokeAsync("GetCatalogByMethod", new object[] {
                        userId,
                        method}, this.GetCatalogByMethodOperationCompleted, userState);
        }
        
        private void OnGetCatalogByMethodOperationCompleted(object arg) {
            if ((this.GetCatalogByMethodCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCatalogByMethodCompleted(this, new GetCatalogByMethodCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://qjDataAccess.org/GetCategoryPicCount", RequestNamespace="http://qjDataAccess.org/", ResponseNamespace="http://qjDataAccess.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetCategoryPicCount() {
            object[] results = this.Invoke("GetCategoryPicCount", new object[0]);
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetCategoryPicCountAsync() {
            this.GetCategoryPicCountAsync(null);
        }
        
        /// <remarks/>
        public void GetCategoryPicCountAsync(object userState) {
            if ((this.GetCategoryPicCountOperationCompleted == null)) {
                this.GetCategoryPicCountOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCategoryPicCountOperationCompleted);
            }
            this.InvokeAsync("GetCategoryPicCount", new object[0], this.GetCategoryPicCountOperationCompleted, userState);
        }
        
        private void OnGetCategoryPicCountOperationCompleted(object arg) {
            if ((this.GetCategoryPicCountCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCategoryPicCountCompleted(this, new GetCategoryPicCountCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void GetCatalogCompletedEventHandler(object sender, GetCatalogCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCatalogCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetCatalogCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void CreateCatalogCompletedEventHandler(object sender, CreateCatalogCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateCatalogCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateCatalogCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Guid Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Guid)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void DeleteCatalogCompletedEventHandler(object sender, DeleteCatalogCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteCatalogCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteCatalogCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void ModifyCatalogCompletedEventHandler(object sender, ModifyCatalogCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ModifyCatalogCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ModifyCatalogCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetCatalogsCompletedEventHandler(object sender, GetCatalogsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCatalogsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetCatalogsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetCatalogTableByParentIdCompletedEventHandler(object sender, GetCatalogTableByParentIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCatalogTableByParentIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetCatalogTableByParentIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetTopCatalogCompletedEventHandler(object sender, GetTopCatalogCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTopCatalogCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTopCatalogCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetAllCatalogCompletedEventHandler(object sender, GetAllCatalogCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAllCatalogCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAllCatalogCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void CheckCatalogRightCompletedEventHandler(object sender, CheckCatalogRightCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckCatalogRightCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CheckCatalogRightCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetCatalogByMethodCompletedEventHandler(object sender, GetCatalogByMethodCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCatalogByMethodCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetCatalogByMethodCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetCategoryPicCountCompletedEventHandler(object sender, GetCategoryPicCountCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCategoryPicCountCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetCategoryPicCountCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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