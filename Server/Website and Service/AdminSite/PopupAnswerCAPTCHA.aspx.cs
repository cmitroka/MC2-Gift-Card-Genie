using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class PopupAnswerCAPTCHA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == true) { return; }
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            string AllSettings = GCWS.GetGlobalSettings();
            string[] AllSettingsArray = GCGCommon.SupportMethods.SplitByString(AllSettings, GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL));

            string prevrs = Page.Request["rsVal"];
            string[] pieces = GCGCommon.SupportMethods.SplitByString(prevrs, GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL));
            if (Request.Url.ToString().Contains("localhost"))
            {
                //AllSettingsArray[3] = @"http://localhost/CAPTCHAs/";
                //http://localhost/CAPTCHAs/20121114_211449689-1rx.bmp
                Image1.ImageUrl = @"http://localhost/CAPTCHAs/" +pieces[0] + ".bmp";
            }
            else
            {
                Image1.ImageUrl = AllSettingsArray[3] + pieces[0] + ".bmp";
            }
            Image1.ToolTip = pieces[0];
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            string UUID = "WebsiteRequest";
            string tempBal = GCWS.ContinueRequest(UUID, Image1.ToolTip, txtCAPTCHA.Text);
            string[] pieces0 = GCGCommon.SupportMethods.SplitByString(tempBal, GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL));
            string rsDetails = GCGReponseHandler.HandleRs(pieces0[0], pieces0[1]);
            if (rsDetails == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ABC", "<Script>alert('Balance Response: " + pieces0[1] + "');</Script>");
            }
            else
            {
                Response.Redirect(rsDetails);
            }
        }
    }
}