﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;
using GCGCommon;
using System.IO;
using mshtml;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
namespace DVB
{
    static class GCGMethods
    {
        private const int WM_ACTIVATEAPP = 0x001C;
        [DllImportAttribute("User32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName,string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;

        public static void CloseWindow(string ImageName, string NameInTitlebar)
        {
            int iHandle = FindWindow(ImageName, NameInTitlebar);
            if (iHandle > 0)
            {
                // close the window using API        
                SendMessage(iHandle, WM_SYSCOMMAND, SC_CLOSE, 0);
            }  
        }
        public static void WriteTextBoxLog(TextBox tb, string topmessage)
        {
            string temp = tb.Text;
            string newmsg = topmessage + "\r\n" + tb.Text;
            tb.Text = newmsg;
        }
        public static string SimInput2(IHTMLDocument2 iHTMLDocument2, HTMLEnumTagNames zHTMLEnumTagNames, HTMLEnumAttributes zHTMLEnumAttributes, string attName, string FillWithVal)
        {
            string retVal = "";
            retVal = DoElementAction2(iHTMLDocument2, zHTMLEnumTagNames, zHTMLEnumAttributes, attName, FillWithVal);
            return retVal;
        }
        public static string SimInput2(WebBrowser webBrowser1, HTMLEnumTagNames zHTMLEnumTagNames, HTMLEnumAttributes zHTMLEnumAttributes, string attName, string FillWithVal)
        {
            string retVal = "";
            retVal = DoElementAction2(webBrowser1, zHTMLEnumTagNames, zHTMLEnumAttributes, attName, FillWithVal);
            return retVal;
        }
        public static string SimInput(WebBrowser webBrowser1, HTMLEnumTagNames zHTMLEnumTagNames, HTMLEnumAttributes zHTMLEnumAttributes, string attName, string FillWithVal)
        {
            string retVal = "";
            retVal = DoElementAction(webBrowser1, zHTMLEnumTagNames, zHTMLEnumAttributes, attName, FillWithVal, 1);
            return retVal;
        }

        public static string SimInput(WebBrowser webBrowser1, HTMLEnumTagNames zHTMLEnumTagNames, HTMLEnumAttributes zHTMLEnumAttributes, string attName, string FillWithVal, int FoundElemCntPos)
        {
            string retVal = "";
            retVal = DoElementAction(webBrowser1, zHTMLEnumTagNames, zHTMLEnumAttributes, attName, FillWithVal, FoundElemCntPos);
            return retVal;
        }
        public static bool WriteFile(string TextFileLocation, string WhatToWrite, bool OpenAfterwards)
        {
            try
            {
                StreamWriter sw = new StreamWriter(TextFileLocation);
                sw.WriteLine(WhatToWrite);
                sw.Close();
                if (OpenAfterwards == true)
                {
                    System.Diagnostics.Process notePad = new System.Diagnostics.Process();
                    notePad.StartInfo.FileName = "notepad.exe";
                    notePad.StartInfo.Arguments = TextFileLocation;
                    notePad.Start();
                }
                return true;
            }
            catch (Exception) { return false; }
        }
        public static string RoughExtract(string StringInStart, string StringInStop, string EnitreHTML)
        {
            EnitreHTML = EnitreHTML.ToUpper();
            StringInStart = StringInStart.ToUpper();
            StringInStop = StringInStop.ToUpper();
            int index = 0;
            int startloc = 0;
            int endloc = 0;
            string retVal = "";
            try
            {
                do
                {
                    startloc = EnitreHTML.IndexOf(StringInStart, startloc + 1);
                    if (startloc == -1) break;
                    int a = startloc + StringInStart.Length;
                    endloc = EnitreHTML.IndexOf(StringInStop, a + 1);
                    if (StringInStop == "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")
                    {
                        endloc = EnitreHTML.Length;
                    }
                    int b = endloc - a;
                    string temphtml = EnitreHTML.Substring(a, b);
                    retVal = temphtml;
                } while (true);
            }
            catch (Exception ex)
            {
                retVal = "";
            }
            return retVal;
        }
        private static string ReplaceNonPrintableCharacters(string s, string replaceWith)
        {
          StringBuilder result = new StringBuilder();
          for (int i=0; i<s.Length; i++)
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

        public static string ReturnStandardBalance(string StringIn)
        {
            string retVal = "";
            try
            {
                retVal = System.Text.RegularExpressions.Regex.Replace(StringIn, "[^.0-9]", "");
                string decCheck;
                do
                {
                    decCheck = retVal.Substring(retVal.Length - 1, 1);
                    if (decCheck == ".")
                    {
                        retVal = retVal.Substring(0, retVal.Length - 1);
                    }
                    else
                    {
                        break;
                    }
                } while (1 == 1);
                decimal test = Convert.ToDecimal(retVal);
                retVal = String.Format("{0:c}", test);
                return retVal;
            }
            catch (Exception ex)
            {
                retVal = "";
            }
            return retVal;
        }
        public static IHTMLDocument2 ConvertWebBrowserToIHTMLDocument2(WebBrowser wb, int frameindex)
        {
            IHTMLDocument2 FrameDoc = null;
            FrameDoc = WebBrowserToIHTMLDocument2(wb, frameindex);
            return FrameDoc;
        }
        public static IHTMLDocument2 ConvertWebBrowserToIHTMLDocument2(WebBrowser wb)
        {
            IHTMLDocument2 FrameDoc = null;
            FrameDoc = WebBrowserToIHTMLDocument2(wb, -1);
            return FrameDoc;
        }

        private static IHTMLDocument2 WebBrowserToIHTMLDocument2(WebBrowser wb, int frameindex)
        {
            IHTMLDocument2 FrameDoc =null;
            try
            {
                mshtml.HTMLDocument htmlDoc = (HTMLDocument)wb.Document.DomDocument;
                if (frameindex >= 0)
                {
                    IHTMLWindow2 htmlWindow = (IHTMLWindow2)htmlDoc.frames.item(frameindex);
                    FrameDoc = CrossFrameIE.GetDocumentFromWindow(htmlWindow);
                }
                else
                {
                    FrameDoc = (IHTMLDocument2)htmlDoc;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return FrameDoc;
        }
        public static string GetHTMLFromWebObject(object Something)
        {
            string retVal="";
            string test=Something.GetType().ToString();
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

        public static string GetPlainTextFromHTML(string HTMLIn)
        {
            string retVal = "";
            try
            {
                //retVal = FrameDoc.body.innerHTML;
                string temp = System.Text.RegularExpressions.Regex.Replace(HTMLIn, "(&lt;(((?!/&gt;).)*)/&gt;)|(&lt;(((?!&gt;).)*)&gt;)|(<(((?!/>).)*)/>)|(<(((?!>).)*)>)", String.Empty);
                retVal = temp;
            }
            catch (Exception ex)
            {
            }
            return retVal;
        }
        private static string DoElementAction2(IHTMLDocument2 FrameDoc, HTMLEnumTagNames HTMLEnumTagNames, HTMLEnumAttributes zHTMLEnumAttributes, string attName, string FillWithVal)
        {
            string retVal = "-1";
            try
            {

                mshtml.IHTMLElementCollection c = ((mshtml.HTMLDocumentClass)(FrameDoc)).getElementsByTagName(HTMLEnumTagNames.ToString());
                foreach (IHTMLElement div in c)
                {
                    System.Diagnostics.Debug.WriteLine("InnerText: " + div.innerHTML);
                    System.Diagnostics.Debug.WriteLine("OutterHTML: " + div.outerHTML);
                    System.Diagnostics.Debug.WriteLine("OutterText: " + div.outerText);
                    //System.Diagnostics.Debug.WriteLine("Name: " + element.GetAttribute("name"));
                    System.Diagnostics.Debug.WriteLine("ID: " + div.id);
                    if (div.outerHTML == null) continue;
                    if (div.outerHTML.Contains(attName))
                    {
                        if (FillWithVal == "")
                        {
                            div.click();
                            retVal = "1";
                            break;
                        }
                        else
                        {
                            div.setAttribute("value", FillWithVal);
                            retVal = "1";
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = "-1";
            }
            return retVal;
        }
        private static string DoElementAction2(WebBrowser webBrowser1, HTMLEnumTagNames HTMLEnumTagNames, HTMLEnumAttributes zHTMLEnumAttributes, string attName, string FillWithVal)
        {
            string retVal = "-1";
            mshtml.HTMLDocument htmlDoc = (HTMLDocument)webBrowser1.Document.DomDocument;
            IHTMLWindow2 htmlWindow = (IHTMLWindow2)htmlDoc.frames.item(0);
            IHTMLDocument2 FrameDoc = CrossFrameIE.GetDocumentFromWindow(htmlWindow);
            try
            {

                mshtml.IHTMLElementCollection c = ((mshtml.HTMLDocumentClass)(FrameDoc)).getElementsByTagName(HTMLEnumTagNames.ToString());
                foreach (IHTMLElement div in c)
                {
                    System.Diagnostics.Debug.WriteLine("InnerText: " + div.innerHTML);
                    System.Diagnostics.Debug.WriteLine("OutterHTML: " + div.outerHTML);
                    System.Diagnostics.Debug.WriteLine("OutterText: " + div.outerText);
                    //System.Diagnostics.Debug.WriteLine("Name: " + element.GetAttribute("name"));
                    System.Diagnostics.Debug.WriteLine("ID: " + div.id);
                    if (div.outerHTML == null) continue;
                    if (div.outerHTML.Contains(attName))
                    {
                        if (FillWithVal == "")
                        {
                            div.click();
                            retVal = "1";
                            break;
                        }
                        else
                        {
                            div.setAttribute("value", FillWithVal);
                            retVal = "1";
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = "-1";
            }
            return retVal;
        }
        private static string DoElementAction(WebBrowser webBrowser1, HTMLEnumTagNames zHTMLEnumTagNames, HTMLEnumAttributes zHTMLEnumAttributes, string attName, string FillWithVal, int FoundElemCntPos)
        {
            int pFoundElemCntPos = 0;
            string attribute = "id";
            string status = attName + " was never clicked.";
            HtmlElement elementtoacton = null;
            HtmlElementCollection col = null;
            bool TryIt = false;
            try
            {
                col = webBrowser1.Document.GetElementsByTagName(zHTMLEnumTagNames.ToString());
                attribute = zHTMLEnumAttributes.ToString();

                foreach (HtmlElement element in col)
                {
                    string temptext = element.GetAttribute(attribute);
                    System.Diagnostics.Debug.WriteLine("InnerText: " + element.GetAttribute("InnerText"));
                    System.Diagnostics.Debug.WriteLine("OutterHTML: " + element.OuterText);
                    System.Diagnostics.Debug.WriteLine("Name: " + element.GetAttribute("name"));
                    System.Diagnostics.Debug.WriteLine("ID: " + element.GetAttribute("id"));
                    if (temptext == "Check Balance")
                    {
                        System.Diagnostics.Debug.WriteLine("Found");
                    }
                    bool TempTryIt = false;
                    TempTryIt = CompareElements(zHTMLEnumAttributes, element, attName);
                    if (TempTryIt == true)
                    {
                        pFoundElemCntPos = pFoundElemCntPos + 1;
                        if (pFoundElemCntPos == FoundElemCntPos)
                        {
                            TryIt = true;
                            elementtoacton = element;
                            break;
                        }
                    }
                }

                if (TryIt == true)
                {
                    try
                    {
                        if (FillWithVal == "")
                        {
                            elementtoacton.InvokeMember("click");
                            status = "1";
                        }
                        else
                        {
                            elementtoacton.SetAttribute("value", FillWithVal);
                            status = "1";
                        }
                    }
                    catch (Exception ex1)
                    {
                        status = "ClickMember ex1 " + ex1.Message;
                    }
                }


                }
            catch (Exception ex)
            {
                status = "ClickMember ex " + ex.Message;
            }
            return status;
        }
        private static bool CompareElements(HTMLEnumAttributes zHTMLEnumAttributes, HtmlElement element, string attName)
        {
            bool retVal=false;
            bool WildcardSearch=false;
            if (attName.Contains("*%"))
            {
                WildcardSearch=true;
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

        public static string GetWBDocumentText(WebBrowser wb)
        {
            string retVal = "";
            string temp = "";
            try
            {
                temp = GCGCommon.SupportMethods.ReturnTextOnlyFromHTML(wb.DocumentText);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            temp=HttpUtility.HtmlDecode(temp);
            retVal = temp;
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
        public static string LaunchIt(string CompleteLine)
        {
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                //p.StartInfo.FileName = gloPathToMerchantEXEs + "\\" + pCardType + ".exe";
                //p.StartInfo.Arguments = @"""" + MakeRqFile + @""" 1";
                p.StartInfo.FileName = CompleteLine;
                p.StartInfo.CreateNoWindow = false;
                Console.WriteLine(p.StartInfo.FileName);
                bool isStarted = p.Start();
                if (isStarted == false) { return CompleteLine + " couldn't start"; }
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static int IsRunning(string pProcessName)
        {
            int processcount = 0;
            Process[] pname = Process.GetProcessesByName(pProcessName);
            foreach (Process theprocess in pname)
            {
                if (theprocess.ProcessName == pProcessName)
                {
                    processcount++;
                }
            }
            return processcount;
        }
        public static void KillProcess(string NameOfProcess)
        {
            //Tools | Internet Options | Advanced | Browsing | Enable automatice crash recovery*
            System.Diagnostics.Process[] Ps = System.Diagnostics.Process.GetProcessesByName(NameOfProcess);
            foreach (System.Diagnostics.Process process in Ps)
            {
                process.Kill();
            }
        }

    }
}