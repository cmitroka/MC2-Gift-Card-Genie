using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    public partial class MC2Status {
        
        private string resultCodeField;
        
        private string cLSStatusMessageField;
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string ResultCode {
            get {
                return this.resultCodeField;
            }
            set {
                this.resultCodeField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string CLSStatusMessage {
            get {
                return this.cLSStatusMessageField;
            }
            set {
                this.cLSStatusMessageField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "3.4.0.38968")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.users.com/CLS")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.users.com/CLS", IsNullable = false)]

    public partial class Merchant
    {
        private string merchantName;
        private string merchantPhone;
        private string merchantURL;
        private bool showCardNum;
        private bool showCardPIN;
        private bool showCreds;
        private bool isAutoLookup;

        public string MerchantName
        {
            get
            {
                return this.merchantName;
            }
            set
            {
                this.merchantName = value;
            }
        }
        public string MerchantPhone
        {
            get
            {
                return this.merchantPhone;
            }
            set
            {
                this.merchantPhone = value;
            }
        }
        public string MerchantURL
        {
            get
            {
                return this.merchantURL;
            }
            set
            {
                this.merchantURL = value;
            }
        }
        public bool ShowCardNum
        {
            get
            {
                return this.showCardNum;
            }
            set
            {
                this.showCardNum = value;
            }
        }
        public bool ShowCardPIN
        {
            get
            {
                return this.showCardPIN;
            }
            set
            {
                this.showCardPIN = value;
            }
        }
        public bool ShowCreds
        {
            get
            {
                return this.showCreds;
            }
            set
            {
                this.showCreds = value;
            }
        }
        public bool IsAutoLookup
        {
            get
            {
                return this.isAutoLookup;
            }
            set
            {
                this.isAutoLookup = value;
            }
        }
    }
    public partial class Template {
        private int sequenceNumberField;
        private string tellerIDField;
        private System.DateTime cUDateField;
        private bool isCRApprovalReqdEnabledField;
        public int SequenceNumber
        {
            get
            {
                return this.sequenceNumberField;
            }
            set
            {
                this.sequenceNumberField = value;
            }
        }
        public string TellerID
        {
            get {
                return this.tellerIDField;
            }
            set {
                this.tellerIDField = value;
            }
        }
        public System.DateTime CUDate {
            get {
                return this.cUDateField;
            }
            set {
                this.cUDateField = value;
            }
        }
        public bool IsCRApprovalReqdEnabled {
            get {
                return this.isCRApprovalReqdEnabledField;
            }
            set {
                this.isCRApprovalReqdEnabledField = value;
            }
        }
    }