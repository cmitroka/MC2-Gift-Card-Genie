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
        public static string ElemLinkEnvoke(SHDocVw.InternetExplorer IE, string HREFToFind)
        {
            string retVal = "-1";
            try
            {
                mshtml.IHTMLDocument2 document2 = null;  //IE.Document as mshtml.IHTMLDocument2;
                mshtml.HTMLDocument document = IE.Document as mshtml.HTMLDocument;
                foreach (HTMLAnchorElementClass el in document.links)
                {
                    System.Diagnostics.Debug.WriteLine(el.href);
                    if (el.href.Contains(HREFToFind))
                    {
                        el.click();
                        retVal = "1";
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
        public static string ElemInputEnvoke(SHDocVw.InternetExplorer IE, string IDorNAMEToFInd, string ValueToEnter)
        {
            string retVal=ElemInputEnvoke(IE, IDorNAMEToFInd, ValueToEnter,0);
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
                if (ValueToEnter=="")
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
        public static string CAPTCHAGetImage(SHDocVw.InternetExplorer IE, string SRCToFInd)
        {
            string retVal = "1";
            try
            {
                mshtml.HTMLDocument doc = IE.Document as mshtml.HTMLDocument;
                IHTMLControlRange imgRange = (IHTMLControlRange)((HTMLBody)doc.body).createControlRange();
                foreach (IHTMLImgElement img in doc.images)
                {
                    System.Diagnostics.Debug.WriteLine(img.src);
                    try
                    {
                        imgRange.add((IHTMLControlElement)img);
                        imgRange.execCommand("Copy", false, null);
                        using (Bitmap bmp = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap))
                        {
                            if (img.src.Contains("kaptcha"))
                            {
                                bmp.Save(@"C:\kaptcha.bmp");
                                retVal = "1";
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        retVal = "-1";
                    }
                }
                System.Diagnostics.Debug.WriteLine("Done");
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Debug.WriteLine(ex.Message);
                retVal = "-1";
            }
            return retVal;
        }
    }
}
