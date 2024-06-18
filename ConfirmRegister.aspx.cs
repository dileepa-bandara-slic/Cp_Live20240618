using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ConfirmRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrMesg.Text = "";
        lblPostErrMesg.Text = "";
        lblSuccessMesg.Text = "";
        hlRegister.Visible = false;
        try
        {
            if (Request.QueryString["regtokn"] == null || Request.QueryString["regtokn"].ToString() == "")
            {
                lblErrMesg.Text = "Your registration link is missing the token. Please double check that you copied and pasted the entire URL sent in your password request email.";

            }
            else if (Request.QueryString["regtokn"].ToString() != "")
            {
                string regToken = Request.QueryString["regtokn"].ToString();
                ULCustomer guest = new ULCustomer();
                if (guest.isValidRegToken(regToken))
                {
                    bool registerConfirmed = guest.ConfirmRegistration(regToken);

                    if (registerConfirmed)
                    {
                        lblSuccessMesg.Text = "Your Registration has been successfully completed";
                        hlLogin.Text = "Login here";
                        hlLogin.NavigateUrl = "Login.aspx";
                    }
                    else
                    {
                        lblPostErrMesg.Text = "Registration failed due to internal error";
                    }
                }
                else
                {
                    lblErrMesg.Text = "Your registration token is invalid or has expired. <br> Sign up for a new account ";
                    hlRegister.Visible = true;
                    hlRegister.Text = "Click Here";
                    hlRegister.NavigateUrl = "Register.aspx";

                }
            }
        }
        catch (Exception ex)
        {
            log logger = new log();
            logger.write_log("Failed at ConfirmRegister: " + ex.ToString());
            lblPostErrMesg.Text = "Registration failed due to some internal error";
        }
    }
}