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
    public partial class Support : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private bool SendMail()
        {
            var fromAddress = new MailAddress("temp1@mc2techservices.com", "GCG Support Page");
            var toAddress = new MailAddress("service@mc2techservices.com", "GCG Support Page");
            const string fromPassword = "temppass";
            const string subject = "Support Request";
            string body = "Name: " + txtName.Text + Environment.NewLine + "Email: " + txtEmail.Text + Environment.NewLine + txtComment.Text;
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
                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            return true;
        }
        protected void cmdSend_Click(object sender, EventArgs e)
        {
            //SendMail();
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            //AppWebService.WebService GCWS = new AppWebService.WebService();
            AppSite.AppWebReference.WebService GCWS = new AppSite.AppWebReference.WebService();
            string a=GCWS.RecordFeedback("Web", txtName.Text, txtEmail.Text, txtComment.Text);
            if (1==1)
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