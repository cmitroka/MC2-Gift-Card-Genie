using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mshtml;
using System.Drawing;
using System.Windows.Forms;
//using System.Runtime.InteropServices;
//using SHDocVw;

namespace DVB
{
    public class WebpageLib00
    {
        public enum WhatIsIt
        {
            Zinput,
            Za,
            Zdiv,
            Zimg,
            Zlink,
            Zselect
        };
        public enum UsingIdentifier
        {
            Zname,
            Zclass,
            Zid,
            Zsrc,
            Zvalue
        };
        public enum ComparisonType
        {
            Zexact,
            Zcontains
        };

        public static mshtml.HTMLDocument ConvertIEtoHTMLDocument(SHDocVw.InternetExplorer IE)
        {
            mshtml.HTMLDocument document = null;
            try
            {
                mshtml.IHTMLDocument2 document2 = null;  //IE.Document as mshtml.IHTMLDocument2;
                document = IE.Document as mshtml.HTMLDocument;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return document;
        }
        public static string ElemLinkEnvoke(Main m, SHDocVw.InternetExplorer IE, string HREFToFind)
        {
            string retVal = "-1";
            try
            {
                mshtml.IHTMLDocument2 document2 = null;  //IE.Document as mshtml.IHTMLDocument2;
                GCGMethods.WriteTextBoxLog(m.txtLog, "ElemLinkEnvoke 1");
                mshtml.HTMLDocument document = IE.Document as mshtml.HTMLDocument;
                GCGMethods.WriteTextBoxLog(m.txtLog, "ElemLinkEnvoke 2");
                foreach (HTMLAnchorElementClass el in document.links)
                {
                    System.Diagnostics.Debug.WriteLine(el.href);
                    GCGMethods.WriteTextBoxLog(m.txtLog, "ElemLinkEnvoke 3");
                    if (el.href.Contains(HREFToFind))
                    {
                        el.click();
                        GCGMethods.WriteTextBoxLog(m.txtLog, "ElemLinkEnvoke 4");
                        retVal = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                GCGMethods.WriteTextBoxLog(m.txtLog, "ElemLinkEnvoke err: " + ex.Message);

                retVal = "-1";
            }
            return retVal;
        }
        public static string ElemInputEnvoke(SHDocVw.InternetExplorer IE, string IDorNAMEToFInd, string ValueToEnter)
        {
            string retVal = ElemInputEnvoke(IE, IDorNAMEToFInd, ValueToEnter, 0);
            return retVal;
        }

        public static string ElemFindAndAct(SHDocVw.InternetExplorer IE, WhatIsIt whatIsIt, UsingIdentifier usingIdentifier, ComparisonType comparisonType, string IDorNAMEToFInd, string ValueToEnter, int Iterations)
        {
            string retVal = "-1";
            try
            {
                string WhatItIs = whatIsIt.ToString();
                WhatItIs = WhatItIs.Substring(1, WhatItIs.Length - 1);
                mshtml.HTMLDocument doc = IE.Document as mshtml.HTMLDocument;
                HTMLDocumentClass docc = (HTMLDocumentClass)doc;
                mshtml.IHTMLElementCollection col = docc.getElementsByTagName(WhatItIs);
                foreach (IHTMLElement element in col)
                {
                    string colItemClass = "";
                    string colItemName = "";
                    string colItemID = "";
                    string colItemValue = "";
                    string colItemSrc = "";
                    try { colItemClass = (string)element.getAttribute("class"); }
                    catch (Exception ex) { }
                    try { colItemName = (string)element.getAttribute("name"); }
                    catch (Exception ex) { }
                    try { colItemID = (string)element.getAttribute("id"); }
                    catch (Exception ex) { }
                    try { colItemValue = (string)element.getAttribute("value"); }
                    catch (Exception ex) { }
                    try { colItemSrc = (string)element.getAttribute("src"); }
                    catch (Exception ex) { }
                    System.Diagnostics.Debug.WriteLine("ID: " + colItemID);
                    System.Diagnostics.Debug.WriteLine("Class: " + colItemClass);
                    System.Diagnostics.Debug.WriteLine("Name: " + colItemName);
                    System.Diagnostics.Debug.WriteLine("Value: " + colItemValue);
                    System.Diagnostics.Debug.WriteLine("Src: " + colItemSrc);
                    System.Diagnostics.Debug.WriteLine("------------------------------------------------");
                    bool FoundIt = false;
                    if (usingIdentifier == UsingIdentifier.Zid) { if (colItemID == IDorNAMEToFInd) FoundIt = true; }
                    if (usingIdentifier == UsingIdentifier.Zname) { if (colItemName == IDorNAMEToFInd) FoundIt = true; }
                    if (usingIdentifier == UsingIdentifier.Zclass) { if (colItemClass == IDorNAMEToFInd) FoundIt = true; }
                    if (usingIdentifier == UsingIdentifier.Zvalue) { if (colItemValue == IDorNAMEToFInd) FoundIt = true; }

                    if (FoundIt == true)
                    {
                        System.Diagnostics.Debug.WriteLine("FOUND IT!");
                        if (ValueToEnter == "")
                        {
                            element.click();
                            retVal = "1";
                            break;
                        }
                        else if (ValueToEnter == "focus")
                        {
                            IHTMLElement2 focusOnIt = (IHTMLElement2)element;
                            focusOnIt.focus();
                            retVal = "1";
                            break;
                        }
                        else
                        {
                            element.setAttribute("value", ValueToEnter);
                System.Diagnostics.Debug.WriteLine("ElemFindAndAct Done Searching.");
                            retVal = "1";
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                retVal = "-1";
            }
            return retVal;
        }

        public static string ElemInputEnvoke(SHDocVw.InternetExplorer IE, string IDorNAMEToFInd, string ValueToEnter, int Iterations)
        {
            //searches through all inputs, looking for a match on either the ID or Name.  If there's multiple, need to specify the number it is.
            string retVal = "-1";
            try
            {
                mshtml.HTMLDocument document = IE.Document as mshtml.HTMLDocument;
                HTMLInputElement searchTextBox = (HTMLInputElement)document.all.item(IDorNAMEToFInd, Iterations);  //finds by name
                if (ValueToEnter == "")
                {
                    searchTextBox.click();
                    retVal = "1";
                }
                else
                {
                    searchTextBox.value = ValueToEnter;
                    retVal = "1";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                retVal = "-1";
            }
            return retVal;
        }
        public static string CAPTCHAGetImage(SHDocVw.InternetExplorer IE, string SRCToFInd,string WhereToSave)
        {
            string retVal = "1";
            try
            {
                IHTMLDocument2 doc = (mshtml.IHTMLDocument2)IE.Document;
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
    }
}
