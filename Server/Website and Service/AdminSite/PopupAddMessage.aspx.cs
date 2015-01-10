using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class PopupAddMessage : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UUIDIn = "";
            try
            {
                UUIDIn = Page.Request["UUIDIn"].ToString();
            }
            catch (Exception ex)
            {
            }
            TextBox t = (TextBox)DetailsView1.FindControl("TextBox1"); //DetailsView1_TextBox1
            t.Text = UUIDIn;            
        }

        protected void AccessDataSource1_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Parameters["TimeLogged"].Value = DateTime.Now.ToString();
        }

    }
}