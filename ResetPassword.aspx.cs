using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtPassword.ReadOnly = true;
        txtConfirmPassword.ReadOnly = true;
        lblErrMesg.Text = "";

        try
        {
            if (Request.QueryString["token"] == null || Request.QueryString["token"].ToString() == "")
            {
                lblErrMesg.Text = "Your password reset link is missing the token. Please double check that you copied and pasted the entire URL sent in your password request email.";
            }
            else if (Request.QueryString["token"].ToString() != "")
            {
                string resetToken = Request.QueryString["token"].ToString();
                ULCustomer guest = new ULCustomer();
                if (guest.isValidToken(resetToken))
                {
                    txtPassword.ReadOnly = false;
                    txtConfirmPassword.ReadOnly = false;
                    ViewState["resetToken"] = resetToken;
                }
                else
                {
                    lblErrMesg.Text = "Your password reset token is invalid or has expired <br> Request a new password link";
                    hlForgotPass.Text = "Here";
                    hlForgotPass.NavigateUrl = "ForgotPassword.aspx";

                }

            }
        }
        catch (Exception ex)
        {
            log logger = new log();
            logger.write_log("Failed at ResetPassword-Pageload: " + ex.ToString());
            lblErrMesg.Text = "Process failed due to intenral error";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string resetToken = (string)ViewState["resetToken"];
        lblSuccessMesg.Text = "";
        lblPostErrMesg.Text = "";

        if (Page.IsValid)
        {
            try
            {
                ULCustomer user = new ULCustomer();
                bool passwordResetDone = user.ResetPassword(resetToken, txtPassword.Text);

                if (passwordResetDone)
                {
                    lblSuccessMesg.Text = "Password reset successful";
                    hlLogin.Text = "Login here";
                    hlLogin.NavigateUrl = "Login.aspx";
                    txtPassword.ReadOnly = true;
                    txtConfirmPassword.ReadOnly = true;
                }
                else
                {
                    lblPostErrMesg.Text = "Password Reset failed";
                }
            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at ResetPassword: " + ex.ToString());
                lblErrMesg.Text = "Process failed due to intenral error";
            }
        }
    }
}