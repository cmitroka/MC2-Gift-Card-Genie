﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.296.
// 
#pragma warning disable 1591

namespace AppAdminSite.localhost {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WebServiceSoap", Namespace="http://tempuri.org/")]
    public partial class WebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendFeedbackOperationCompleted;
        
        private System.Threading.SendOrPostCallback IsRegisteredLMIFEOperationCompleted;
        
        private System.Threading.SendOrPostCallback SavePCCredsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetPCCredsOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendSystemCommandOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetRegInfoForLMIFEOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WebService() {
            this.Url = global::AppAdminSite.Properties.Settings.Default.AppAdminSite_localhost_WebService;
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
        public event SendFeedbackCompletedEventHandler SendFeedbackCompleted;
        
        /// <remarks/>
        public event IsRegisteredLMIFECompletedEventHandler IsRegisteredLMIFECompleted;
        
        /// <remarks/>
        public event SavePCCredsCompletedEventHandler SavePCCredsCompleted;
        
        /// <remarks/>
        public event GetPCCredsCompletedEventHandler GetPCCredsCompleted;
        
        /// <remarks/>
        public event SendSystemCommandCompletedEventHandler SendSystemCommandCompleted;
        
        /// <remarks/>
        public event GetRegInfoForLMIFECompletedEventHandler GetRegInfoForLMIFECompleted;
        
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendFeedback", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SendFeedback(string Feedback, string Email) {
            object[] results = this.Invoke("SendFeedback", new object[] {
                        Feedback,
                        Email});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SendFeedbackAsync(string Feedback, string Email) {
            this.SendFeedbackAsync(Feedback, Email, null);
        }
        
        /// <remarks/>
        public void SendFeedbackAsync(string Feedback, string Email, object userState) {
            if ((this.SendFeedbackOperationCompleted == null)) {
                this.SendFeedbackOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendFeedbackOperationCompleted);
            }
            this.InvokeAsync("SendFeedback", new object[] {
                        Feedback,
                        Email}, this.SendFeedbackOperationCompleted, userState);
        }
        
        private void OnSendFeedbackOperationCompleted(object arg) {
            if ((this.SendFeedbackCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendFeedbackCompleted(this, new SendFeedbackCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IsRegisteredLMIFE", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string IsRegisteredLMIFE(string Login) {
            object[] results = this.Invoke("IsRegisteredLMIFE", new object[] {
                        Login});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void IsRegisteredLMIFEAsync(string Login) {
            this.IsRegisteredLMIFEAsync(Login, null);
        }
        
        /// <remarks/>
        public void IsRegisteredLMIFEAsync(string Login, object userState) {
            if ((this.IsRegisteredLMIFEOperationCompleted == null)) {
                this.IsRegisteredLMIFEOperationCompleted = new System.Threading.SendOrPostCallback(this.OnIsRegisteredLMIFEOperationCompleted);
            }
            this.InvokeAsync("IsRegisteredLMIFE", new object[] {
                        Login}, this.IsRegisteredLMIFEOperationCompleted, userState);
        }
        
        private void OnIsRegisteredLMIFEOperationCompleted(object arg) {
            if ((this.IsRegisteredLMIFECompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.IsRegisteredLMIFECompleted(this, new IsRegisteredLMIFECompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SavePCCreds", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SavePCCreds(string Version, string Login, string Password, string PCName, string SpecificLogin, string SpecificPass) {
            object[] results = this.Invoke("SavePCCreds", new object[] {
                        Version,
                        Login,
                        Password,
                        PCName,
                        SpecificLogin,
                        SpecificPass});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SavePCCredsAsync(string Version, string Login, string Password, string PCName, string SpecificLogin, string SpecificPass) {
            this.SavePCCredsAsync(Version, Login, Password, PCName, SpecificLogin, SpecificPass, null);
        }
        
        /// <remarks/>
        public void SavePCCredsAsync(string Version, string Login, string Password, string PCName, string SpecificLogin, string SpecificPass, object userState) {
            if ((this.SavePCCredsOperationCompleted == null)) {
                this.SavePCCredsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSavePCCredsOperationCompleted);
            }
            this.InvokeAsync("SavePCCreds", new object[] {
                        Version,
                        Login,
                        Password,
                        PCName,
                        SpecificLogin,
                        SpecificPass}, this.SavePCCredsOperationCompleted, userState);
        }
        
        private void OnSavePCCredsOperationCompleted(object arg) {
            if ((this.SavePCCredsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SavePCCredsCompleted(this, new SavePCCredsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetPCCreds", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetPCCreds(string Version, string Login, string Password, string PCName) {
            object[] results = this.Invoke("GetPCCreds", new object[] {
                        Version,
                        Login,
                        Password,
                        PCName});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetPCCredsAsync(string Version, string Login, string Password, string PCName) {
            this.GetPCCredsAsync(Version, Login, Password, PCName, null);
        }
        
        /// <remarks/>
        public void GetPCCredsAsync(string Version, string Login, string Password, string PCName, object userState) {
            if ((this.GetPCCredsOperationCompleted == null)) {
                this.GetPCCredsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPCCredsOperationCompleted);
            }
            this.InvokeAsync("GetPCCreds", new object[] {
                        Version,
                        Login,
                        Password,
                        PCName}, this.GetPCCredsOperationCompleted, userState);
        }
        
        private void OnGetPCCredsOperationCompleted(object arg) {
            if ((this.GetPCCredsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetPCCredsCompleted(this, new GetPCCredsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendSystemCommand", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SendSystemCommand(string TheCommand) {
            object[] results = this.Invoke("SendSystemCommand", new object[] {
                        TheCommand});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SendSystemCommandAsync(string TheCommand) {
            this.SendSystemCommandAsync(TheCommand, null);
        }
        
        /// <remarks/>
        public void SendSystemCommandAsync(string TheCommand, object userState) {
            if ((this.SendSystemCommandOperationCompleted == null)) {
                this.SendSystemCommandOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendSystemCommandOperationCompleted);
            }
            this.InvokeAsync("SendSystemCommand", new object[] {
                        TheCommand}, this.SendSystemCommandOperationCompleted, userState);
        }
        
        private void OnSendSystemCommandOperationCompleted(object arg) {
            if ((this.SendSystemCommandCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendSystemCommandCompleted(this, new SendSystemCommandCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetRegInfoForLMIFE", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void GetRegInfoForLMIFE(string PID, string TransactionID) {
            this.Invoke("GetRegInfoForLMIFE", new object[] {
                        PID,
                        TransactionID});
        }
        
        /// <remarks/>
        public void GetRegInfoForLMIFEAsync(string PID, string TransactionID) {
            this.GetRegInfoForLMIFEAsync(PID, TransactionID, null);
        }
        
        /// <remarks/>
        public void GetRegInfoForLMIFEAsync(string PID, string TransactionID, object userState) {
            if ((this.GetRegInfoForLMIFEOperationCompleted == null)) {
                this.GetRegInfoForLMIFEOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetRegInfoForLMIFEOperationCompleted);
            }
            this.InvokeAsync("GetRegInfoForLMIFE", new object[] {
                        PID,
                        TransactionID}, this.GetRegInfoForLMIFEOperationCompleted, userState);
        }
        
        private void OnGetRegInfoForLMIFEOperationCompleted(object arg) {
            if ((this.GetRegInfoForLMIFECompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetRegInfoForLMIFECompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void SendFeedbackCompletedEventHandler(object sender, SendFeedbackCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendFeedbackCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendFeedbackCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void IsRegisteredLMIFECompletedEventHandler(object sender, IsRegisteredLMIFECompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IsRegisteredLMIFECompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal IsRegisteredLMIFECompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void SavePCCredsCompletedEventHandler(object sender, SavePCCredsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SavePCCredsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SavePCCredsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetPCCredsCompletedEventHandler(object sender, GetPCCredsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetPCCredsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetPCCredsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void SendSystemCommandCompletedEventHandler(object sender, SendSystemCommandCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendSystemCommandCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendSystemCommandCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetRegInfoForLMIFECompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591