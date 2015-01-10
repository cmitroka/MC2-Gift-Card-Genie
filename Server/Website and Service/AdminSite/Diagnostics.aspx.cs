using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GCSite
{
    public partial class Diagnostics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            string tempStatus = GCWS.CheckWebConfig();
            txtError.Text = tempStatus;
            try
            {
                string FILE_NAME2 = System.Configuration.ConfigurationManager.AppSettings["BalanceRequestPath"].ToString();
                lblBalance.Text = "OK";
            }
            catch (Exception ex)
            {
            }
            try
            {
                string FILE_NAME2 = System.Configuration.ConfigurationManager.AppSettings["BalanceRequestPath"].ToString();
                lblRequestPath.Text = "OK";
            }
            catch (Exception ex)
            {
            }
            try
            {
                string FILE_NAME2 = System.Configuration.ConfigurationManager.AppSettings["PathToMerchantEXEs"].ToString();
                lblPathToMerchantEXEs.Text = "OK";
            }
            catch (Exception ex)
            {
            }

            try
            {
                string FILE_NAME2 = System.Configuration.ConfigurationManager.AppSettings["PathToRqRs"].ToString();
                lblPathToRqRs.Text = "OK";
            }
            catch (Exception ex)
            {
            }

            try
            {
                string FILE_NAME2 = System.Configuration.ConfigurationManager.AppSettings["Always Fail"].ToString();
                lblAlwaysFail.Text = "OK";
            }
            catch (Exception ex)
            {
            }

        }
    }
}