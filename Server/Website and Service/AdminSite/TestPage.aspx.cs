using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class TestPage : System.Web.UI.Page
    {
        string LINEDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL);
        string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL);
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Page_Load");
            System.Diagnostics.Debug.WriteLine(Request.Params.Count);
            /*
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            string a = GCWS.BackupData("UUID", "A-B-C-D-E-F*1-2-3-4-5-6*");
            string SessionIDAndAdInfo = GCWS.GetSessionIDAndAdInfo("UUID", "");
            string[] GetSessionIDAndAdInfoArr = GCGCommon.SupportMethods.SplitByString(SessionIDAndAdInfo, GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL));
            string SessionID = GetSessionIDAndAdInfoArr[0];
            string CheckSum = GCGCommon.SupportMethods.GetChecksum(SessionID);

            string test = GCWS.RetrieveData("UUID", a);

            string b = GCWS.LogPurchase(SessionID, CheckSum, "C","Gift");
            return;
            */
        }
        public string GetPublicIP()
        {
            string ip = "";
            ip = Context.Request.UserHostAddress;
            if (ip == string.Empty)
            {
                ip = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            if (ip == string.Empty)
            {
                System.Net.WebClient webClient = new System.Net.WebClient();
                ip= webClient.DownloadString("http://myip.ozymo.com/");
            }

            return ip;
            /*
            String direction = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    direction = stream.ReadToEnd();
                }
            }

            //Search for the ip in the html 
            int first = direction.IndexOf("Address: ") + 9;
            int last = direction.LastIndexOf("</body>");
            direction = direction.Substring(first, last - first);

            return direction;
            */
        }

        protected void TestLogUser_Click(object sender, EventArgs e)
        {
            string retVal = "";
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            //string IP = GetPublicIP();
            //DF700EFC-F1E8-4245-9A96-542500D26F54
            retVal = GCWS.DoAppStartup(TextBox1.Text);
            Response.Write(retVal);
            
            //string[] a = GCGCommon.SupportMethods.SplitByString(retVal, POSDEL);
            //string a = GCWS.LogPurchase("A", "A", "A", "Gift");

            //string b=GCWS.LogConsideredBuying("A", "A");
            //Response.Write(retVal);

            //AppAdminSite.BusinessLogic bc = new AppAdminSite.BusinessLogic();
            //bc.ProcessResponse("C://TextRS.txt", "111");




        }

        protected void GetMerchCount_Click(object sender, EventArgs e)
        {
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            string retVal = GCWS.GetURLForEXE("BBandB", "");
            Response.Write(retVal);

        }


        protected void TestLogFeedback_Click(object sender, EventArgs e)
        {

            //GCWebService temp = new GCWebService();
            //temp.RecordFeedback("A", "B", "C", "D");
            //return;
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            string retVal = GCWS.RecordFeedback("UDID","NameTest","ContactInfoTest","FeedbackTest");
            Response.Write(retVal);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            string retVal = GCWS.RetrieveData("X", "a");
            Response.Write(retVal);
        }

        protected void TestFileUpload_Click(object sender, EventArgs e)
        {

            if (FileUpload1.HasFile)
                try
                {
                    string loc = System.Configuration.ConfigurationManager.AppSettings["PathToMerchantEXEs"].ToString();
                    FileUpload1.SaveAs(loc +
                         FileUpload1.FileName);
                    Label1.Text = "File name: " +
                         FileUpload1.PostedFile.FileName + "<br>" +
                         FileUpload1.PostedFile.ContentLength + " kb<br>" +
                         "Content type: " +
                         FileUpload1.PostedFile.ContentType;
                }
                catch (Exception ex)
                {
                    Label1.Text = "ERROR: " + ex.Message.ToString();
                }
            else
            {
                Label1.Text = "You have not specified a file.";
            }

        }

        protected void URLfromEXE_Click(object sender, EventArgs e)
        {
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            string retVal = GCWS.GetURLForEXE("BBandB", "");
            Response.Write(retVal);

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            string retVal = GCWS.SaveURLForEXE("BBandB", "AAAAA", "");
            Response.Write(retVal);
        }

        protected void SimpleWebserviceCall_Click(object sender, EventArgs e)
        {
            WebRequest webRequest = WebRequest.Create("http://localhost:55555/WebService.asmx");
            HttpWebRequest httpRequest = (HttpWebRequest)webRequest;
            httpRequest.Method = "POST";
            httpRequest.ContentType = "text/xml; charset=utf-8";
            httpRequest.Headers.Add("SOAPAction: \"gcg.mc2techservices.com/DoAppStartup\"");
            //httpRequest.ProtocolVersion = HttpVersion.Version11;
            //httpRequest.Credentials = CredentialCache.DefaultCredentials;
            Stream requestStream = httpRequest.GetRequestStream();
            //Create Stream and Complete Request             
            StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.ASCII);

            string soapRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n" +
            "<soap:Body>\r\n" +
            "<DoAppStartup xmlns=\"gcg.mc2techservices.com\">\r\n" +
            "<UUID>string</UUID>\r\n" +
            "</DoAppStartup>\r\n" +
            "</soap:Body>\r\n" +
            "</soap:Envelope>";
            DVB.GCGMethods.WriteFile("C:\\out.txt",soapRequest,true);
            streamWriter.Write(soapRequest.ToString());
            streamWriter.Close();
            //Get the Response    
            HttpWebResponse wr = (HttpWebResponse)httpRequest.GetResponse();
            StreamReader srd = new StreamReader(wr.GetResponseStream());
            string resulXmlFromWebService = srd.ReadToEnd();
            System.Diagnostics.Debug.Write(resulXmlFromWebService);
            //return resulXmlFromWebService;
        }

        protected void SaveByteData2WebserviceCall_Click(object sender, EventArgs e)
        {
            byte[] DIByteArray = File.ReadAllBytes(TextBox2.Text);
            string DIBase64String = Convert.ToBase64String(DIByteArray);
            string URL = "";

            if (Request.Url.ToString().Contains("localhost"))
            {
                URL = "http://localhost:55555/WebService.asmx";
            }
            else
            {
                URL = "https://gcg.mc2techservices.com/WebService.asmx";
            }

            WebRequest webRequest = WebRequest.Create(URL);
            HttpWebRequest httpRequest = (HttpWebRequest)webRequest;
            httpRequest.Method = "POST";
            httpRequest.ContentType = "text/xml; charset=utf-8";
            httpRequest.Headers.Add("SOAPAction: \"gcg.mc2techservices.com/ImageToOCR\"");

            Stream requestStream = httpRequest.GetRequestStream();
            StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.ASCII);

            string soapRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n" +
            "<soap:Body>\r\n" +
            "<ImageToOCR xmlns=\"gcg.mc2techservices.com\">\r\n" +
            "<dataIn>" + DIBase64String + "</dataIn>\r\n" +
            "</ImageToOCR>\r\n" +
            "</soap:Body>\r\n" +
            "</soap:Envelope>";
            //DVB.GCGMethods.WriteFile("C:\\out.txt", soapRequest, true);
            streamWriter.Write(soapRequest.ToString());
            streamWriter.Close();
            //Get the Response    
            HttpWebResponse wr = (HttpWebResponse)httpRequest.GetResponse();
            StreamReader srd = new StreamReader(wr.GetResponseStream());
            string resulXmlFromWebService = srd.ReadToEnd();
            System.Diagnostics.Debug.Write(resulXmlFromWebService);
            string temp = DVB.GCGMethods.RoughExtract("<ImageToOCRResult>", "</ImageToOCRResult>", resulXmlFromWebService);
            //<ImageToOCRResult>string</ImageToOCRResult>
            TextBox2.Text = temp;
        }

        protected void Send_Click(object sender, EventArgs e)
        {
            if (FileUpload2.HasFile)
                try
                {
                    string loc = @"C:\GCG\Tesseract-OCR\OCRthis.jpg";
                    FileUpload2.SaveAs(loc);
                    Label2.Text = "Upload of " + loc + FileUpload2.FileName + " successful!<br>File name: " +
                         FileUpload2.PostedFile.FileName + "<br>" +
                         FileUpload2.PostedFile.ContentLength + " kb<br>" +
                         "Content type: " +
                         FileUpload2.PostedFile.ContentType;
                    AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
                    byte[] a = null;
                    string retVal = "A"; // GCWS.ImageToOCR(a);
                    
                    Label2.Text = Label2.Text + "<hr>" + retVal;

                }
                catch (Exception ex)
                {
                    Label2.Text = "ERROR: " + ex.Message.ToString();
                }
            else
            {
                Label2.Text = "You have not specified a file.";
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            string retVal = "";
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            retVal = GCWS.SaveWDDataForEXE("EXE001", "URL", "CardNum", "CardPIN", "CredLogin", "CredPass", "CJMGCG");
            System.Diagnostics.Debug.WriteLine(retVal);
            retVal = GCWS.SaveWDDataForEXE01("EXE1001", "CleanName", "URL", "CardNum", "CardPIN", "CredLogin", "CredPass", "SupportCode", "59", "CJMGCG");
            System.Diagnostics.Debug.WriteLine(retVal);
            Response.Write(retVal);

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            string retVal = "";
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            retVal = GCWS.GetWDDataForEXE("Wawazzz","CJMGCG");
            System.Diagnostics.Debug.WriteLine(retVal);
            Response.Write(retVal);


        }
    }
}