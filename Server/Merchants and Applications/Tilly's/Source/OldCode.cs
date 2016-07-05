using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DVB
{
    class OldCode
    {
        public static string GetHTMLFromWebObject(object Something)
        {
            string retVal = "";
            string test = Something.GetType().ToString();
            System.Diagnostics.Debug.WriteLine(test);
            try
            {
                if (test == "System.Windows.Forms.WebBrowser")
                {
                    WebBrowser real = (WebBrowser)Something;
                    retVal = real.DocumentText;
                }
                else if (test == "mshtml.HTMLDocumentClass")
                {
                    IHTMLDocument2 real = (IHTMLDocument2)Something;
                    retVal = real.body.innerHTML;
                }
                else
                {
                    retVal = "-1";
                }
            }
            catch (Exception)
            {
                retVal = "-1";
            }
            return retVal;
            /*
            IHTMLDocument2 FrameDoc = null;
            mshtml.HTMLDocument htmlDoc = (HTMLDocument)wb.Document.DomDocument;
            IHTMLWindow2 htmlWindow = (IHTMLWindow2)htmlDoc.frames.item(0);
            FrameDoc = CrossFrameIE.GetDocumentFromWindow(htmlWindow);
            return FrameDoc;
            */
        }
        private static string ReplaceNonPrintableCharacters(string s, string replaceWith)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                byte b = (byte)c;
                if (b < 32)
                    result.Append(replaceWith);
                else
                    result.Append(c);
            }
            return result.ToString();
        }
        public static string ChangeFocus2(WebBrowser webBrowser1, GCGCommon.HTMLEnumAttributes idorname, string whatitscalled, FocusTypes fc)
        {
            string retVal = "-1";
            mshtml.HTMLDocument htmlDoc = (HTMLDocument)webBrowser1.Document.DomDocument;
            IHTMLWindow2 htmlWindow = (IHTMLWindow2)htmlDoc.frames.item(0);
            IHTMLDocument2 FrameDoc = CrossFrameIE.GetDocumentFromWindow(htmlWindow);
            try
            {

                mshtml.IHTMLElementCollection c = ((mshtml.HTMLDocumentClass)(FrameDoc)).getElementsByTagName("input");
                foreach (IHTMLElement div in c)
                {
                    System.Diagnostics.Debug.WriteLine("InnerText: " + div.innerHTML);
                    System.Diagnostics.Debug.WriteLine("OutterHTML: " + div.outerHTML);
                    System.Diagnostics.Debug.WriteLine("OutterText: " + div.outerText);
                    //System.Diagnostics.Debug.WriteLine("Name: " + element.GetAttribute("name"));
                    System.Diagnostics.Debug.WriteLine("ID: " + div.id);
                    if (div.outerHTML == null) continue;
                    if (div.outerHTML.Contains(whatitscalled))
                    {
                        div.click();
                        retVal = "1";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = "-1";
            }
            return retVal;
        }
        public static string ChangeFocus(WebBrowser webBrowser1, GCGCommon.HTMLEnumAttributes idorname, string whatitscalled, FocusTypes fc)
        {
            string retVal = "-1";
            mshtml.HTMLDocument htmlDoc = (HTMLDocument)webBrowser1.Document.DomDocument;
            IHTMLWindow2 htmlWindow = (IHTMLWindow2)htmlDoc.frames.item(0);
            IHTMLDocument2 FrameDoc = CrossFrameIE.GetDocumentFromWindow(htmlWindow);
            try
            {

                mshtml.IHTMLElementCollection c = ((mshtml.HTMLDocumentClass)(FrameDoc)).getElementsByTagName("input");
                foreach (IHTMLElement div in c)
                {
                    System.Diagnostics.Debug.WriteLine("InnerText: " + div.innerHTML);
                    System.Diagnostics.Debug.WriteLine("OutterHTML: " + div.outerHTML);
                    System.Diagnostics.Debug.WriteLine("OutterText: " + div.outerText);
                    //System.Diagnostics.Debug.WriteLine("Name: " + element.GetAttribute("name"));
                    System.Diagnostics.Debug.WriteLine("ID: " + div.id);
                    if (div.outerHTML == null) continue;
                    if (div.outerHTML.Contains(whatitscalled))
                    {
                        div.setAttribute("focus", "true");
                        retVal = "1";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = "-1";
            }
            return retVal;
        }
        private static bool CompareElements(HTMLEnumAttributes zHTMLEnumAttributes, HtmlElement element, string attName)
        {
            bool retVal = false;
            bool WildcardSearch = false;
            if (attName.Contains("*%"))
            {
                WildcardSearch = true;
            }
            string UattName = attName.Replace("*%", "");
            string temp;
            UattName = UattName.ToUpper();
            if (zHTMLEnumAttributes.Equals(HTMLEnumAttributes.OuterHtml))
            {
                try
                {
                    temp = element.OuterHtml.ToUpper();
                }
                catch (Exception ex)
                {
                    temp = "";
                }
            }
            else
            {
                temp = element.GetAttribute(zHTMLEnumAttributes.ToString()).ToUpper();
            }
            System.Diagnostics.Debug.WriteLine("Do Comparison");
            if (WildcardSearch == true)
            {
                if (temp.Contains(UattName))
                {
                    retVal = true;
                }
            }
            else
            {
                if (temp == UattName)
                {
                    retVal = true;
                }
            }
            return retVal;
        }

    }
}
