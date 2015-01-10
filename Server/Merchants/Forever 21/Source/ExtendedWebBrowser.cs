using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DVB
{
    public class ExtendedWebBrowser : WebBrowser
    {
        bool renavigating = false;

        public string UserAgent { get; set; }

        public ExtendedWebBrowser()
        {
            DocumentCompleted += SetupBrowser;

            //this will cause SetupBrowser to run (we need a document object)
            Navigate("about:blank");
        }

        void SetupBrowser(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            DocumentCompleted -= SetupBrowser;
            SHDocVw.WebBrowser xBrowser = (SHDocVw.WebBrowser)ActiveXInstance;
            xBrowser.BeforeNavigate2 += BeforeNavigate;
            DocumentCompleted += PageLoaded;
        }

        void PageLoaded(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        void BeforeNavigate(object pDisp, ref object url, ref object flags, ref object targetFrameName,
            ref object postData, ref object headers, ref bool cancel)
        {
            //UserAgent = "Mozilla/5.0+(compatible;+MSIE+10.0;+Windows+NT+6.1;+WOW64;+Trident/6.1)";
            if (!string.IsNullOrEmpty(UserAgent))
            {
                if (!renavigating)
                {
                    headers += string.Format("User-Agent: {0}\r\n", UserAgent);
                    renavigating = true;
                    cancel = true;
                    string sUrl = url.ToString();
                    if (sUrl == "javascript:false;") sUrl = "";
                    if (sUrl == "about:blank") sUrl="";
                    Navigate(sUrl, (string)targetFrameName, (byte[])postData, (string)headers);
                }
                else
                {
                    renavigating = false;
                }
            }
        }
    }
}