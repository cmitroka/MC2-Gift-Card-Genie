using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace AppAdminSite
{
    public partial class AddMerchantRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private bool SendMail()
        {
            var fromAddress = new MailAddress("gcgsupport@mc2techservices.com", "GCG Support Page");
            var toAddress = new MailAddress("gcgsupport@mc2techservices.com", "GCG Support Page");
            const string fromPassword = "gcgpass1";
            const string subject = "Support Request";
            string body = "New Merchant Request for" + txtMerchant.Text;
            //const string body = "Body";
            var smtp = new SmtpClient
            {
                //Host ="smtp.googlemail.com" Port=465
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            return true;
        }
        protected void cmdSend_Click(object sender, EventArgs e)
        {
            //AppWebService.WebService GCWS = new AppWebService.WebService();
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            AppSite.AppWebReference.WebService GCWS = new AppSite.AppWebReference.WebService();
            string result = GCWS.AddMerchantRequest(txtMerchant.Text, txtURL.Text, txtCardNum.Text, txtCardPIN.Text, txtOtherInfo.Text);
            if (Convert.ToInt16(result)>0)
            {
                Response.Redirect("ThankYou.aspx");
            }
            else
            {
                Response.Redirect("SupportError.aspx");
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}