using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Timers;
using System.Drawing;
using System.Windows.Forms;
using GCGCommon;
using System.IO;
using mshtml;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Diagnostics;
namespace DVB
{
    static class GCGMethods
    {
        public enum FocusTypes { RemoveFocus, Focus, Click };
        public enum HTMLTagNames { Za, Zarea, Zbutton, Zdiv, Zform, Zhr, Zimg, Zinput, Zselect };
        public enum HTMLAttributes { Zalt, Zclass, Zhref, Zid, Zname, ZinnerHTML, ZInnerText, ZouterHtml, ZOuterText, Zsrc, Zvalue, };

        public static Bitmap CaptureRegionAsBMP(int LeftPosition, int TopPosition, int WidthSize, int HeightSize)
        {
            Bitmap bmp = null;
            Rectangle rect = new Rectangle(LeftPosition, TopPosition, WidthSize, HeightSize);
            bmp = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            return bmp;
        }
        public static void WriteTextBoxLog(TextBox tb, string topmessage)
        {
            string temp = tb.Text;
            string newmsg = topmessage + "\r\n" + tb.Text;
            tb.Text = newmsg;
        }
        public static string SimInput(IHTMLDocument2 iHTMLDocument2, HTMLTagNames zHTMLTagNames, HTMLAttributes zHTMLAttributes, string attName, string FillWithVal, int FoundInLoop)
        {
            string retVal = "";
            retVal = DoElementAction(iHTMLDocument2, zHTMLTagNames, zHTMLAttributes, attName, FillWithVal, FoundInLoop);
            return retVal;
        }
        public static string SimInput(IHTMLDocument2 iHTMLDocument2, HTMLTagNames zHTMLTagNames, HTMLAttributes zHTMLAttributes, string attName, string FillWithVal)
        {
            string retVal = "";
            retVal = DoElementAction(iHTMLDocument2, zHTMLTagNames, zHTMLAttributes, attName, FillWithVal,1);
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
                    return retVal;
                } while (true);
            }
            catch (Exception ex)
            {
                retVal = "";
            }
            return retVal;
        }
        public static string RoughExtractAlphaNumOnly(string StringInStart, string StringInStop, string EnitreHTML)
        {
            string CSEnitreHTML = EnitreHTML;
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
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
                    string temphtml = CSEnitreHTML.Substring(a, b);
                    temphtml = rgx.Replace(temphtml, "");
                    retVal = temphtml;
                    return retVal;
                } while (true);
            }
            catch (Exception ex)
            {
                retVal = "";
            }
            return retVal;
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
        public static int FindWhatFrameItsIn(SHDocVw.InternetExplorer myIE, string SearchFor)
        {
            int retVal = -999;
            bool FoundIt = false;
            IHTMLDocument2 FrameDoc2 = null;
            try
            {
                mshtml.HTMLDocument htmlDoc = (HTMLDocument)myIE.Document;
                for (int i = 0; i < htmlDoc.frames.length; i++)
                {
                    IHTMLWindow2 htmlWindow = (IHTMLWindow2)htmlDoc.frames.item(i);
                    FrameDoc2 = CrossFrameIE.GetDocumentFromWindow(htmlWindow);
                    string HTMLTest = FrameDoc2.body.innerHTML;
                    if (HTMLTest==null)
                    {
                        System.Diagnostics.Debug.WriteLine("No HTML in frame " + i);
                        continue;
                    }
                    HTMLTest = HTMLTest.ToUpper();
                    if (HTMLTest.Contains(SearchFor.ToUpper()))
                    {
                        FoundIt = true;
                        System.Diagnostics.Debug.WriteLine("IS in frame " + i);
                        retVal = i;
                        break;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Not in frame " + i);
                    }
                }
                if (FoundIt == false)
                {
                    FrameDoc2 = (IHTMLDocument2)htmlDoc;
                    string HTMLTest = FrameDoc2.body.innerHTML;
                    try
                    {
                        HTMLTest = HTMLTest.ToUpper();
                        if (HTMLTest.Contains(SearchFor.ToUpper()))
                        //GCGCommon.SupportMethods.WriteFile("C:\\test", HTMLTest, true);
                        {
                            FoundIt = true;
                            retVal = -1;
                            System.Diagnostics.Debug.WriteLine("IS in base document");
                        }
                    }
                    catch (Exception ex1)
                    {
                        System.Diagnostics.Debug.WriteLine("No HTML Content");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retVal;
        }
        public static IHTMLDocument2 ConvertIEToIHTMLDocument2(SHDocVw.InternetExplorer myIE, string SearchFor)
        {
            bool FoundIt=false;
            IHTMLDocument2 FrameDoc2 = null;
            try
            {
                mshtml.HTMLDocument htmlDoc = (HTMLDocument)myIE.Document;
                for (int i = 0; i < htmlDoc.frames.length; i++)
                {
                    IHTMLWindow2 htmlWindow = (IHTMLWindow2)htmlDoc.frames.item(i);
                    FrameDoc2 = CrossFrameIE.GetDocumentFromWindow(htmlWindow);
                    string HTMLTest = "";
                    HTMLTest = FrameDoc2.body.innerHTML;
                    if (HTMLTest == null)
                    {
                        System.Diagnostics.Debug.WriteLine("Not sure if it's in frame " + i);
                        HTMLTest = "";
                        FoundIt = true;
                        break;
                    }
                    HTMLTest=HTMLTest.ToUpper();
                    //GCGCommon.SupportMethods.WriteFile("C:\\test_"+i, HTMLTest, true);
                    if (HTMLTest.Contains(SearchFor.ToUpper()))
                    {
                        FoundIt = true;
                        //GCGMethods.WriteFile("C:\\HTMLTest.txt", HTMLTest, true);
                        System.Diagnostics.Debug.WriteLine("IS in frame " + i);
                        break;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Not in frame "+i);
                    }
                }
                if (FoundIt == false)
                {
                    FrameDoc2 = (IHTMLDocument2)htmlDoc;
                    string HTMLTest = FrameDoc2.body.innerHTML;
                    HTMLTest = HTMLTest.ToUpper();
                    if (HTMLTest.Contains(SearchFor.ToUpper()))
                    //GCGCommon.SupportMethods.WriteFile("C:\\test", HTMLTest, true);
                    {
                        FoundIt = true;
                        System.Diagnostics.Debug.WriteLine("IS in base document");
                    }
                }            
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            if (FoundIt == true)
            {
                return FrameDoc2;
            }
            else
            {
                return null;
            }
        }
        
        public static IHTMLDocument2 ConvertIEToIHTMLDocument2(SHDocVw.InternetExplorer myIE, int frameindex)
        {
            IHTMLDocument2 FrameDoc2 = null;
            try
            {   
                mshtml.HTMLDocument htmlDoc = (HTMLDocument)myIE.Document;
                System.Diagnostics.Debug.WriteLine(htmlDoc.frames.length);
                if (frameindex >= 0)
                {
                    IHTMLWindow2 htmlWindow = (IHTMLWindow2)htmlDoc.frames.item(frameindex);
                    FrameDoc2 = CrossFrameIE.GetDocumentFromWindow(htmlWindow);
                }
                else
                {
                    FrameDoc2 = (IHTMLDocument2)htmlDoc;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return FrameDoc2;
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

        public static string GetHTML(IHTMLDocument2 FrameDoc)
        {
            string retVal = "";
            try
            {
                retVal = FrameDoc.body.innerHTML;
            }
            catch (Exception ex)
            {
            }
            return retVal;
        }
        public static string GetHTMLFromIHTMLDocument2(IHTMLDocument2 FrameDoc)
        {
            string retVal = "";
            try
            {
                retVal = FrameDoc.body.innerHTML;
            }
            catch (Exception ex)
            {
            }
            return retVal;
        }
        public static string ElementExists(IHTMLDocument2 FrameDoc, HTMLTagNames HTMLTagNames, HTMLAttributes zHTMLAttributes, string attName)
        {
            string retVal = "-1";
            attName=attName.ToUpper();
            int foreachcount=0;
            string HTMLEnumAttribute = zHTMLAttributes.ToString();
            if (zHTMLAttributes.ToString() == "classs") HTMLEnumAttribute = "className";
            try
            {

                mshtml.IHTMLElementCollection c = ((mshtml.HTMLDocumentClass)(FrameDoc)).getElementsByTagName(HTMLTagNames.ToString());
                foreach (IHTMLElement div in c)
                {
                    foreachcount++;
                    System.Diagnostics.Debug.WriteLine("foreachcount: " + foreachcount.ToString() + " of " + c.length.ToString());
                    System.Diagnostics.Debug.WriteLine("ID: " + div.id);
                    bool TryIt = false;
                    string testUcaseVal = "ALWAYSFAILATTHISPOINT";
                    if (HTMLEnumAttribute.ToUpper() == "OUTERHTML") attName = "*%" + attName;
                    try
                    {
                        testUcaseVal = div.getAttribute(HTMLEnumAttribute).ToString().ToUpper();
                        System.Diagnostics.Debug.WriteLine(HTMLEnumAttribute + ": " + testUcaseVal);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    TryIt = CompareElements(attName, testUcaseVal);
                    if (TryIt == true)
                    {
                        retVal = "1";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return retVal;
        }
        private static string DoElementAction(IHTMLDocument2 FrameDoc, HTMLTagNames HTMLTagNames, HTMLAttributes zHTMLAttributes, string attName, string FillWithVal, int FoundInLoop)
        {
            int FoundInLoopCount = 0;
            int foreachcount=0;
            string retVal = "-1";
            attName=attName.ToUpper();
            string pHTMLTagNames=HTMLTagNames.ToString().Substring(1,HTMLTagNames.ToString().Length-1);
            string pHTMLEnumAttribute = zHTMLAttributes.ToString().Substring(1, zHTMLAttributes.ToString().Length - 1);
            try
            {

                mshtml.IHTMLElementCollection c = ((mshtml.HTMLDocumentClass)(FrameDoc)).getElementsByTagName(pHTMLTagNames);
                foreach (IHTMLElement div in c)
                {
                    foreachcount++;
                    if (attName == "RELOAD-LINK")
                    {
                        if (foreachcount < 550) continue;
                    }
                    if (c.length == 0) break;
                    System.Diagnostics.Debug.WriteLine("foreachcount: " + foreachcount.ToString() + " of " + c.length.ToString());
                    System.Diagnostics.Debug.WriteLine("OutterHTML: " + div.outerHTML);
                    System.Diagnostics.Debug.WriteLine("OutterText: " + div.outerText);
                    System.Diagnostics.Debug.WriteLine("InnerHTML: " + div.innerText);
                    System.Diagnostics.Debug.WriteLine("InnerText: " + div.innerHTML);

                    //System.Diagnostics.Debug.WriteLine("Name: " + element.GetAttribute("name"));
                    System.Diagnostics.Debug.WriteLine("ID: " + div.id);
                    bool TryIt = false;
                    string testUcaseVal = "ALWAYSFAILATTHISPOINT";
                    if (pHTMLEnumAttribute.ToUpper() == "OUTERHTML") attName = "*%" + attName;
                    //{
                    //    if (div.outerHTML.ToUpper().Contains(attName)) TryIt = true;
                    //}
                    //else
                    //{
                        try
                        {
                            testUcaseVal = div.getAttribute(pHTMLEnumAttribute).ToString().ToUpper();
                            System.Diagnostics.Debug.WriteLine(pHTMLEnumAttribute + ": " + testUcaseVal);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }
                    if (testUcaseVal.Contains(attName))
                    {
                        bool tempTryIt=true;
                    }
                    TryIt = CompareElements(attName, testUcaseVal);
                    //}
                    if (div.outerHTML == null) continue;
                    if (TryIt == true)
                    {
                        string Xcoord = "";
                        string Ycoord = "";
                        Xcoord = div.offsetLeft.ToString();
                        Ycoord = div.offsetTop.ToString();
                        FoundInLoopCount++;
                        if (FoundInLoopCount < FoundInLoop) continue; ;
                        if (FillWithVal == "")
                        {
                            div.click();
                            retVal = "1";
                            break;
                        }
                        else if (FillWithVal == "focus")
                        {
                            IHTMLElement2 focusOnIt = (IHTMLElement2)div;
                            focusOnIt.focus();
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
        public static string CAPTCHAGetImage(IHTMLDocument2 IE, string SRCToFInd, string WhereToSave)
        {
            string retVal = "1";
            try
            {
                IHTMLDocument2 doc = IE;
                IHTMLControlRange imgRange = (mshtml.IHTMLControlRange)((mshtml.HTMLBody)doc.body).createControlRange();
                foreach (mshtml.IHTMLImgElement imgx in doc.images)
                {
                    System.Diagnostics.Debug.WriteLine(imgx.nameProp);
                    string ImageDetails = imgx.src.ToUpper();
                    System.Diagnostics.Debug.WriteLine(imgx.src.ToUpper());
                    string CAPTCHAName = SRCToFInd.ToUpper();
                    if (ImageDetails.Contains(CAPTCHAName))
                    {
                        imgRange.add((mshtml.IHTMLControlElement)imgx);
                        imgRange.execCommand("Copy", false, null);
                        Bitmap bmp = null;
                        bmp = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                        bmp.Save(WhereToSave);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("CAPTCHAGetImage failed");
                //throw;
            }
            return retVal;
        }

        private static bool CompareElements(string LookFor, string SearchInThis)
        {
            bool retVal = false;
            bool WildcardSearch = false;
            LookFor = LookFor.ToUpper();
            SearchInThis = SearchInThis.ToUpper();
            if (LookFor.Contains("*%"))
            {
                WildcardSearch = true;
            }
            LookFor = LookFor.Replace("*%", "");
            string temp;
            System.Diagnostics.Debug.WriteLine("Do Comparison");
            if (WildcardSearch == true)
            {
                if (SearchInThis.Contains(LookFor))
                {
                    retVal = true;
                }
            }
            else
            {
                if (LookFor == SearchInThis)
                {
                    retVal = true;
                }
            }
            return retVal;
        }
        public static string LaunchIE(string BaseWebpage)
        {
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                //p.StartInfo.FileName = gloPathToMerchantEXEs + "\\" + pCardType + ".exe";
                p.StartInfo.Arguments = BaseWebpage;
                p.StartInfo.FileName = "iexplore.exe";
                p.StartInfo.CreateNoWindow = false;
                Console.WriteLine(p.StartInfo.FileName);
                bool isStarted = p.Start();
                if (isStarted == false) { return  " couldn't start"; }
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string LaunchItWArg(string PathToEXE, string Arg)
        {
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.Arguments = Arg;
                p.StartInfo.FileName = PathToEXE;
                p.StartInfo.CreateNoWindow = false;
                Console.WriteLine(p.StartInfo.FileName);
                bool isStarted = p.Start();
                if (isStarted == false) { return " couldn't start"; }
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string LaunchIt(string CompleteLine)
        {
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                //p.StartInfo.FileName = gloPathToMerchantEXEs + "\\" + pCardType + ".exe";
                //p.StartInfo.Arguments = @"http://www.google.com";
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
        /// <summary>Allows you to modify the HTML, and therefore behavior, or elements in the webpage.</summary>
        /// <param name="HTMLTagType">HTMLEnumTagType can be things like a, input, button, img, hr, div, select, area, ...</param>
        /// <param name="HTMLAttribute">HTMLAttribute can be things like id, name, src, value, InnerText, OuterHtml, classs, OuterText, href, alt, ...</param>
        public static string ModifyHTML(IHTMLDocument2 FrameDoc, string HTMLTagType, string HTMLAttribute, string AttributeValue, string ReplaceWith)
        {
            int foreachcount = 0;
            string retVal = "1";
            bool tempTryIt;
            AttributeValue = AttributeValue.ToUpper();
            try
            {
                mshtml.IHTMLElementCollection elements = ((mshtml.HTMLDocumentClass)(FrameDoc)).getElementsByTagName(HTMLTagType);
                foreach (IHTMLElement element in elements)
                {
                    tempTryIt = false;
                    foreachcount++;
                    System.Diagnostics.Debug.WriteLine("element innerHTML details: " + element.innerHTML);
                    System.Diagnostics.Debug.WriteLine("element outerHTML details: " + element.outerHTML);
                    string testUcaseVal = "ALWAYSFAILATTHISPOINT";
                    try
                    {
                        testUcaseVal = element.getAttribute(HTMLAttribute).ToString().ToUpper();
                    }
                    catch (Exception ex)
                    {
                    }
                    if (AttributeValue.Contains("*%"))
                    {
                        if (testUcaseVal.Contains(AttributeValue))
                        {
                            tempTryIt = true;
                        }
                    }
                    else
                    {
                        if (testUcaseVal == AttributeValue)
                        {
                            tempTryIt = true;
                        }
                    }
                    if (tempTryIt == true)
                    {
                        element.outerHTML = ReplaceWith;
                        return "1";
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = "-1";
            }
            return retVal;
        }

    }
}
