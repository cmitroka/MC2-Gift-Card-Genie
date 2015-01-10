using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class PopupIAMerchantUploader : SecurePage
    {
        protected void TestFileUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
                try
                {
                    string loc = System.Configuration.ConfigurationManager.AppSettings["PathToMerchantEXEs"].ToString() + "\\";
                    FileUpload1.SaveAs(loc + 
                         FileUpload1.FileName);
                    Label1.Text = "Upload of " + loc + FileUpload1.FileName + " successful!<br>File name: " +
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

    }
}