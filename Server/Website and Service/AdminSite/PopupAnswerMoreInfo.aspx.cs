using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class PopupAnswerMoreInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == true) { return; }
            string prevrs = Page.Request["rsVal"];
            string[] pieces = GCGCommon.SupportMethods.SplitByString(prevrs, GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL));
            Label1.Text = pieces[1];
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            string prevrs = Page.Request["rsVal"];
            string UUID = "WebsiteRequest";
            string[] pieces = GCGCommon.SupportMethods.SplitByString(prevrs, GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL));
            string retrs = GCWS.ContinueRequest(UUID, pieces[0], txtAnswer.Text);
            pieces = GCGCommon.SupportMethods.SplitByString(retrs, GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL));
            string rsDetails = GCGReponseHandler.HandleRs(pieces[0], pieces[1]);
            if (rsDetails == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ABC", "<Script>alert('Balance Response: " + pieces[1] + "');</Script>");
            }
            else
            {
                Response.Redirect(rsDetails);
            }
        }
    }
}