using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GCGCommon;
using System.IO;
namespace DVB
{
    static class GCGMethods
    {
        public static void WriteTextBoxLog(TextBox tb, string topmessage)
        {
            string temp = tb.Text;
            string newmsg = topmessage + "\r\n" + tb.Text;
            tb.Text = newmsg;
        }
        public static string SimInput(WebBrowser webBrowser1, HTMLEnumTagNames zHTMLEnumTagNames, HTMLEnumAttributes zHTMLEnumAttributes, string attName)
        {
            string retVal = "";
            retVal = DoClickMember(webBrowser1, zHTMLEnumTagNames, zHTMLEnumAttributes, attName, "", -1);
            return retVal;
        }

        public static string SimInput(WebBrowser webBrowser1, HTMLEnumTagNames zHTMLEnumTagNames, HTMLEnumAttributes zHTMLEnumAttributes, string attName, string FillWithVal)
        {
            string retVal = "";
            retVal = DoClickMember(webBrowser1, zHTMLEnumTagNames, zHTMLEnumAttributes, attName, FillWithVal, -1);
            return retVal;
        }

        public static string SimInput(WebBrowser webBrowser1, HTMLEnumTagNames zHTMLEnumTagNames, HTMLEnumAttributes zHTMLEnumAttributes, string attName, string FillWithVal, int FoundElemCntPos)
        {
            string retVal = "";
            retVal = DoClickMember(webBrowser1, zHTMLEnumTagNames, zHTMLEnumAttributes, attName, FillWithVal, FoundElemCntPos);
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
                    endloc = EnitreHTML.IndexOf(StringInStop, startloc + 1);
                    //string temphtml = ReplaceNonPrintableCharacters(EnitreHTML, "");
                    int a = startloc + StringInStart.Length;
                    int b = (startloc + StringInStart.Length);
                    int c= endloc - b;
                    c = endloc-startloc;
                    string temphtml = EnitreHTML.Substring(a, c);
                    temphtml = temphtml.Replace("\r", "");
                    temphtml = temphtml.Replace("\n", "");
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
        private static string DoClickMember(WebBrowser webBrowser1, HTMLEnumTagNames zHTMLEnumTagNames, HTMLEnumAttributes zHTMLEnumAttributes, string attName, string FillWithVal, int FoundElemCntPos)
        {
            int pFoundElemCntPos = 0;
            string attribute = "id";
            string status = attName + " was never clicked.";
            HtmlElementCollection col = null;
            try
            {
                col = webBrowser1.Document.GetElementsByTagName(zHTMLEnumTagNames.ToString());
                attribute = zHTMLEnumAttributes.ToString();

                foreach (HtmlElement element in col)
                {
                    string temptext = element.GetAttribute(attribute);
                    System.Diagnostics.Debug.WriteLine("InnerText: " + element.GetAttribute("InnerText"));
                    System.Diagnostics.Debug.WriteLine("OutterHTML: " + element.GetAttribute("OutterHTML"));
                    System.Diagnostics.Debug.WriteLine("Name: " + element.GetAttribute("name"));
                    System.Diagnostics.Debug.WriteLine("ID: " + element.GetAttribute("id"));
                    if (temptext == "Check Balance")
                    {
                        System.Diagnostics.Debug.WriteLine("Found");
                    }
                    string temp = attName;
                    bool TryIt = false;
                    if (temp.Contains("*%") == true)
                    {
                        //do a wildcard search
                        temp = temp.Replace("*%", "");
                        if (element.GetAttribute(attribute).Contains(temp) == true)
                        {
                            pFoundElemCntPos = pFoundElemCntPos + 1;
                            if (pFoundElemCntPos == FoundElemCntPos) TryIt = true;
                            if (FoundElemCntPos == -1) TryIt = true;
                        }
                    }
                    else
                    {
                        string CItest = element.GetAttribute(attribute).ToUpper();
                        string CItemp = temp.ToUpper();
                        if (CItest == CItemp)
                        {
                            pFoundElemCntPos = pFoundElemCntPos + 1;
                            if (pFoundElemCntPos == FoundElemCntPos) TryIt = true;
                            if (FoundElemCntPos == -1) TryIt = true;

                        }
                    }

                    if (TryIt == true)
                    {
                        try
                        {
                            if (FillWithVal == "")
                            {
                                //element.InvokeMember("keypress");

                                if (zHTMLEnumTagNames == HTMLEnumTagNames.formNOTDONE)
                                {
                                    element.InvokeMember("submit");
                                    status = "";
                                }
                                else
                                {
                                    element.InvokeMember("click");
                                    status = "";
                                }
                            }
                            else
                            {
                                element.SetAttribute("value", FillWithVal);
                                status = "";
                            }
                        }
                        catch (Exception ex1)
                        {
                            status = "ClickMember ex1 " + ex1.Message;
                            return status;
                        }
                        //return status;
                    }
                }
            }
            catch (Exception ex)
            {
                status = "ClickMember ex " + ex.Message;
            }
            return status;
        }
    }
}
