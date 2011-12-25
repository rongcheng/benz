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

namespace VideoEngineConsole.WS {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="VideoStorageServiceSoap", Namespace="http://tempuri.org/")]
    public partial class VideoStorageService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddVideoOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetUnConvertedVideosOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetVideosByStatusOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetVideoInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateVideoMetaDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateVideoStatusOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddVideoToCatalogOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public VideoStorageService() {
            this.Url = global::VideoEngineConsole.Properties.Settings.Default.VideoEngineConsole_WS_VideoStorageService;
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
        public event AddVideoCompletedEventHandler AddVideoCompleted;
        
        /// <remarks/>
        public event GetUnConvertedVideosCompletedEventHandler GetUnConvertedVideosCompleted;
        
        /// <remarks/>
        public event GetVideosByStatusCompletedEventHandler GetVideosByStatusCompleted;
        
        /// <remarks/>
        public event GetVideoInfoCompletedEventHandler GetVideoInfoCompleted;
        
        /// <remarks/>
        public event UpdateVideoMetaDataCompletedEventHandler UpdateVideoMetaDataCompleted;
        
        /// <remarks/>
        public event UpdateVideoStatusCompletedEventHandler UpdateVideoStatusCompleted;
        
        /// <remarks/>
        public event AddVideoToCatalogCompletedEventHandler AddVideoToCatalogCompleted;
        
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AddVideo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool AddVideo(
                    System.Guid ID, 
                    string ItemSerialNumber, 
                    string FileName, 
                    string FilePath, 
                    string ServerFileName, 
                    string FlvFileName, 
                    string FlvFilePath, 
                    string Caption, 
                    System.DateTime StartDate, 
                    System.DateTime EndDate, 
                    System.DateTime UploadDate, 
                    System.DateTime ShotDate, 
                    string Keyword, 
                    string Description, 
                    System.DateTime UpdateTime, 
                    System.Guid UserID, 
                    int Status) {
            object[] results = this.Invoke("AddVideo", new object[] {
                        ID,
                        ItemSerialNumber,
                        FileName,
                        FilePath,
                        ServerFileName,
                        FlvFileName,
                        FlvFilePath,
                        Caption,
                        StartDate,
                        EndDate,
                        UploadDate,
                        ShotDate,
                        Keyword,
                        Description,
                        UpdateTime,
                        UserID,
                        Status});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void AddVideoAsync(
                    System.Guid ID, 
                    string ItemSerialNumber, 
                    string FileName, 
                    string FilePath, 
                    string ServerFileName, 
                    string FlvFileName, 
                    string FlvFilePath, 
                    string Caption, 
                    System.DateTime StartDate, 
                    System.DateTime EndDate, 
                    System.DateTime UploadDate, 
                    System.DateTime ShotDate, 
                    string Keyword, 
                    string Description, 
                    System.DateTime UpdateTime, 
                    System.Guid UserID, 
                    int Status) {
            this.AddVideoAsync(ID, ItemSerialNumber, FileName, FilePath, ServerFileName, FlvFileName, FlvFilePath, Caption, StartDate, EndDate, UploadDate, ShotDate, Keyword, Description, UpdateTime, UserID, Status, null);
        }
        
        /// <remarks/>
        public void AddVideoAsync(
                    System.Guid ID, 
                    string ItemSerialNumber, 
                    string FileName, 
                    string FilePath, 
                    string ServerFileName, 
                    string FlvFileName, 
                    string FlvFilePath, 
                    string Caption, 
                    System.DateTime StartDate, 
                    System.DateTime EndDate, 
                    System.DateTime UploadDate, 
                    System.DateTime ShotDate, 
                    string Keyword, 
                    string Description, 
                    System.DateTime UpdateTime, 
                    System.Guid UserID, 
                    int Status, 
                    object userState) {
            if ((this.AddVideoOperationCompleted == null)) {
                this.AddVideoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddVideoOperationCompleted);
            }
            this.InvokeAsync("AddVideo", new object[] {
                        ID,
                        ItemSerialNumber,
                        FileName,
                        FilePath,
                        ServerFileName,
                        FlvFileName,
                        FlvFilePath,
                        Caption,
                        StartDate,
                        EndDate,
                        UploadDate,
                        ShotDate,
                        Keyword,
                        Description,
                        UpdateTime,
                        UserID,
                        Status}, this.AddVideoOperationCompleted, userState);
        }
        
        private void OnAddVideoOperationCompleted(object arg) {
            if ((this.AddVideoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddVideoCompleted(this, new AddVideoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetUnConvertedVideos", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetUnConvertedVideos() {
            object[] results = this.Invoke("GetUnConvertedVideos", new object[0]);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetUnConvertedVideosAsync() {
            this.GetUnConvertedVideosAsync(null);
        }
        
        /// <remarks/>
        public void GetUnConvertedVideosAsync(object userState) {
            if ((this.GetUnConvertedVideosOperationCompleted == null)) {
                this.GetUnConvertedVideosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUnConvertedVideosOperationCompleted);
            }
            this.InvokeAsync("GetUnConvertedVideos", new object[0], this.GetUnConvertedVideosOperationCompleted, userState);
        }
        
        private void OnGetUnConvertedVideosOperationCompleted(object arg) {
            if ((this.GetUnConvertedVideosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUnConvertedVideosCompleted(this, new GetUnConvertedVideosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetVideosByStatus", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetVideosByStatus(int status) {
            object[] results = this.Invoke("GetVideosByStatus", new object[] {
                        status});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetVideosByStatusAsync(int status) {
            this.GetVideosByStatusAsync(status, null);
        }
        
        /// <remarks/>
        public void GetVideosByStatusAsync(int status, object userState) {
            if ((this.GetVideosByStatusOperationCompleted == null)) {
                this.GetVideosByStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetVideosByStatusOperationCompleted);
            }
            this.InvokeAsync("GetVideosByStatus", new object[] {
                        status}, this.GetVideosByStatusOperationCompleted, userState);
        }
        
        private void OnGetVideosByStatusOperationCompleted(object arg) {
            if ((this.GetVideosByStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetVideosByStatusCompleted(this, new GetVideosByStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetVideoInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetVideoInfo(string itemid) {
            object[] results = this.Invoke("GetVideoInfo", new object[] {
                        itemid});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetVideoInfoAsync(string itemid) {
            this.GetVideoInfoAsync(itemid, null);
        }
        
        /// <remarks/>
        public void GetVideoInfoAsync(string itemid, object userState) {
            if ((this.GetVideoInfoOperationCompleted == null)) {
                this.GetVideoInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetVideoInfoOperationCompleted);
            }
            this.InvokeAsync("GetVideoInfo", new object[] {
                        itemid}, this.GetVideoInfoOperationCompleted, userState);
        }
        
        private void OnGetVideoInfoOperationCompleted(object arg) {
            if ((this.GetVideoInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetVideoInfoCompleted(this, new GetVideoInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UpdateVideoMetaData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UpdateVideoMetaData(string serialnumber, string duration, string bitrate, string videosize) {
            object[] results = this.Invoke("UpdateVideoMetaData", new object[] {
                        serialnumber,
                        duration,
                        bitrate,
                        videosize});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateVideoMetaDataAsync(string serialnumber, string duration, string bitrate, string videosize) {
            this.UpdateVideoMetaDataAsync(serialnumber, duration, bitrate, videosize, null);
        }
        
        /// <remarks/>
        public void UpdateVideoMetaDataAsync(string serialnumber, string duration, string bitrate, string videosize, object userState) {
            if ((this.UpdateVideoMetaDataOperationCompleted == null)) {
                this.UpdateVideoMetaDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateVideoMetaDataOperationCompleted);
            }
            this.InvokeAsync("UpdateVideoMetaData", new object[] {
                        serialnumber,
                        duration,
                        bitrate,
                        videosize}, this.UpdateVideoMetaDataOperationCompleted, userState);
        }
        
        private void OnUpdateVideoMetaDataOperationCompleted(object arg) {
            if ((this.UpdateVideoMetaDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateVideoMetaDataCompleted(this, new UpdateVideoMetaDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UpdateVideoStatus", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UpdateVideoStatus(string serialnumber, int status) {
            object[] results = this.Invoke("UpdateVideoStatus", new object[] {
                        serialnumber,
                        status});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateVideoStatusAsync(string serialnumber, int status) {
            this.UpdateVideoStatusAsync(serialnumber, status, null);
        }
        
        /// <remarks/>
        public void UpdateVideoStatusAsync(string serialnumber, int status, object userState) {
            if ((this.UpdateVideoStatusOperationCompleted == null)) {
                this.UpdateVideoStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateVideoStatusOperationCompleted);
            }
            this.InvokeAsync("UpdateVideoStatus", new object[] {
                        serialnumber,
                        status}, this.UpdateVideoStatusOperationCompleted, userState);
        }
        
        private void OnUpdateVideoStatusOperationCompleted(object arg) {
            if ((this.UpdateVideoStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateVideoStatusCompleted(this, new UpdateVideoStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AddVideoToCatalog", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool AddVideoToCatalog(System.Guid[] catalogId, System.Guid itemId) {
            object[] results = this.Invoke("AddVideoToCatalog", new object[] {
                        catalogId,
                        itemId});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void AddVideoToCatalogAsync(System.Guid[] catalogId, System.Guid itemId) {
            this.AddVideoToCatalogAsync(catalogId, itemId, null);
        }
        
        /// <remarks/>
        public void AddVideoToCatalogAsync(System.Guid[] catalogId, System.Guid itemId, object userState) {
            if ((this.AddVideoToCatalogOperationCompleted == null)) {
                this.AddVideoToCatalogOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddVideoToCatalogOperationCompleted);
            }
            this.InvokeAsync("AddVideoToCatalog", new object[] {
                        catalogId,
                        itemId}, this.AddVideoToCatalogOperationCompleted, userState);
        }
        
        private void OnAddVideoToCatalogOperationCompleted(object arg) {
            if ((this.AddVideoToCatalogCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddVideoToCatalogCompleted(this, new AddVideoToCatalogCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void AddVideoCompletedEventHandler(object sender, AddVideoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddVideoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddVideoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetUnConvertedVideosCompletedEventHandler(object sender, GetUnConvertedVideosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUnConvertedVideosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUnConvertedVideosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetVideosByStatusCompletedEventHandler(object sender, GetVideosByStatusCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetVideosByStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetVideosByStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetVideoInfoCompletedEventHandler(object sender, GetVideoInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetVideoInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetVideoInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void UpdateVideoMetaDataCompletedEventHandler(object sender, UpdateVideoMetaDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateVideoMetaDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateVideoMetaDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void UpdateVideoStatusCompletedEventHandler(object sender, UpdateVideoStatusCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateVideoStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateVideoStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void AddVideoToCatalogCompletedEventHandler(object sender, AddVideoToCatalogCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddVideoToCatalogCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddVideoToCatalogCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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