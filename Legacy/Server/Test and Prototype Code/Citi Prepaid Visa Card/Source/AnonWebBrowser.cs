using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using GCGCommon;

namespace DVB
{
    public static class AnonWebBrowser
    {
        public static Main _m;

        public static string Use_https_proxfree(Main m, string GoToURL)
        {
            m.tmrRunning.Enabled = false;
            string OK = "-1";
            IHTMLDocument2 FrameDoc = null;
            m.webBrowser1.Navigate("https://m.proxfree.com/");
            do
            {
                FrameDoc = GCGMethods.ConvertWebBrowserToIHTMLDocument2(m.webBrowser1);
                OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.input, HTMLEnumAttributes.id, "regularInput", GoToURL);
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);               
            } while (OK=="-1");
            do
            {
                OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.button, HTMLEnumAttributes.OuterHtml, "ProxFree", "");
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
            } while (OK == "-1");
            m.tmrRunning.Enabled = true;
            OK = "1";
            return OK;
        }
        public static string Use_http_anonymouse(Main m, string GoToURL)
        {
            m.tmrRunning.Enabled = false;
            string OK = "-1";
            IHTMLDocument2 FrameDoc = null;
            m.webBrowser1.Navigate("http://anonymouse.org/anonwww.html");
            do
            {
                FrameDoc = GCGMethods.ConvertWebBrowserToIHTMLDocument2(m.webBrowser1);
                OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.input, HTMLEnumAttributes.value, "*%http", GoToURL);
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
            } while (OK == "-1");
            do
            {
                OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.input, HTMLEnumAttributes.value, "Surf anonymously", "");
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
            } while (OK == "-1");
            m.tmrRunning.Enabled = true;
            OK = "1";
            return OK;
        }
    }
}
