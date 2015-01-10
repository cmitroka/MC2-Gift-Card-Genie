using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class PopupSimulator : SecurePage
    {
        string LINEDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL);
        string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL);
        static DropDownList ddlCNum=new DropDownList();
        static DropDownList ddlCPIN = new DropDownList();
        static DropDownList ddlLogin = new DropDownList();
        static DropDownList ddlPassword = new DropDownList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true) return;
            string[] temp1;
            string temp2 = "";
            string temp3 = "";
            ddlCardType.Items.Clear();
            ddlCNum.Items.Clear();
            ddlCPIN.Items.Clear();
            ddlLogin.Items.Clear();
            ddlPassword.Items.Clear();

            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            string UUID = "WebsiteRequest";
            //string StartupInfo = GCWS.DoAppStartup(UUID);
            string StartupInfo = GCWS.DoAppStartup2(UUID,"");
            string[] StartupInfoArr = GCGCommon.SupportMethods.SplitByString(StartupInfo, POSDEL);
            if (StartupInfoArr[1].Length > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "SystemMessage", "<Script>alert('" + StartupInfoArr[1] + "');</Script>");
            }
            string vendors = GCWS.GetSupportedMerchants();
            string[] temp4 = GCGCommon.SupportMethods.SplitByString(vendors, LINEDEL);
            foreach (string vendordata in temp4)
            {
                temp1 = GCGCommon.SupportMethods.SplitByString(vendordata, POSDEL);
                if (temp1[0] == "") { break; }
                //string[] temp = new string[3] { temp1[3].ToString(), temp1[4].ToString(), temp1[5].ToString() };
                //StaticData.InsertIt(temp1[0], temp);
                ddlCardType.Items.Add(temp1[0]);
                ddlCNum.Items.Add(temp1[1]);
                ddlCPIN.Items.Add(temp1[2]);
                ddlLogin.Items.Add(temp1[3]);
                ddlPassword.Items.Add(temp1[4]);
            }
            //ConfigureScreen();
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            Console.WriteLine("cmdSubmit_Click");
            string RoughBalance = null;
            string CardBalance = null;
            string PageHTML = null;
            string FixedRequest = null;
            string UUID = "WebsiteRequest";
            //UUID = GCGCommon.SupportMethods.GetWebUUID();
            FixedRequest = "";
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            string StartupInfo = GCWS.DoAppStartup(UUID);
            string SessionIDAndAdInfo = GCWS.GetSessionIDAndAdInfo(UUID, "");

            SessionIDAndAdInfo = GCWS.GetSessionIDAndAdInfo(UUID, ddlCardType.Text);
            string[] GetSessionIDAndAdInfoArr = GCGCommon.SupportMethods.SplitByString(SessionIDAndAdInfo, GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL));
            string SessionID = GetSessionIDAndAdInfoArr[0];
            GCGCommon.SessionVerification sv = new GCGCommon.SessionVerification();
            string CheckSum = "231";
            CheckSum = sv.GetChecksumEV00(SessionID);
            string rs = GCWS.NewRequest(UUID, SessionID, CheckSum, ddlCardType.Text, txtCard.Text, txtPIN.Text, txtLogin.Text, txtPassword.Text);
            string[] pieces = GCGCommon.SupportMethods.SplitByString(rs, GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL));
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

        protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCard.Text = ddlCNum.Items[ddlCardType.SelectedIndex].ToString();
            txtPIN.Text = ddlCPIN.Items[ddlCardType.SelectedIndex].ToString();
            txtLogin.Text = ddlLogin.Items[ddlCardType.SelectedIndex].ToString();
            txtPassword.Text = ddlPassword.Items[ddlCardType.SelectedIndex].ToString();
        }

        protected void cmdClear_Click(object sender, EventArgs e)
        {
            txtCard.Text ="";
            txtPIN.Text = "";
            txtLogin.Text = "";
            txtPassword.Text = "";
        }

    }
}