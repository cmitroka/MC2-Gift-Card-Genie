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
            Request.Path.ToString();
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            string AllSettings = GCWS.GetGlobalSettings();
            string[] AllSettingsArray = GCGCommon.SupportMethods.SplitByString(AllSettings, GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL));

            string prevrs = Page.Request["rsVal"];
            string[] pieces = GCGCommon.SupportMethods.SplitByString(prevrs, GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL));
            //string BaseURL = GCGCommon.SupportMethods.GetBaseURL(Request.Url.ToString());

            Image1.ImageUrl = @"CAPTCHAs/" +pieces[0] + ".bmp";
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