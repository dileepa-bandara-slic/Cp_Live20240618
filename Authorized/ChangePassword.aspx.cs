using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Authorized_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.Name == "")
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblStatusMesg.Text = "";
            ULCustomer customer = new ULCustomer();

            bool passwdChanged = customer.ChangePassword(Page.User.Identity.Name, txtOldPasswd.Text, txtPassword.Text);

            if (passwdChanged)
            {
                Session.Abandon();
                Request.Cookies.Clear();
                FormsAuthentication.SignOut();
                Response.Redirect("../ReloginRequired.aspx");
            }
            else
            {
                lblStatusMesg.Text = "Password Change failed. Please verify your old password is entered correctly.";
            }
        }
    }
}