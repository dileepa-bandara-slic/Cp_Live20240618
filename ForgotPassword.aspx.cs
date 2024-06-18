using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string username = "";
            string email = "";
            string fieldValue = "";
            string fieldCode = "";
            string mobileNo = "";
            lblErrMesg.Text = "";
            lblSuccessMesg.Text = "";
            lblErrMesg.Text = "";

            for (int i = 0; i < 1000000000; i++) ;

            //if(rdUsername.Value == "Username")
            //    username = txtUnameEmail.Text.Trim();

            //if (rdPassword.Value == "Email")
            //    email = txtUnameEmail.Text.Trim();


            if (RadioButton1.Checked)
            {
                username = txtUnameEmail.Text.Trim();
                mobileNo = txtPhoneNo.Text.Trim();

                if (username != "" && mobileNo != "")
                {
                    ULCustomer dm = new ULCustomer();
                    if (!dm.checkUsernameExist(username))
                    {
                        lblErrMesg.Text = "Invalid username"; //Invalid username - does not exist in username column 
                    }
                    else
                    {
                        string retrievedMobileNo = dm.getMobileNo(username);

                        if (mobileNo.Equals(retrievedMobileNo))
                        {
                            InfoValidator userNameValidator = new InfoValidator();
                            if (userNameValidator.IsRegisteredUser(username) == 1)
                            {
                                fieldValue = username;
                                fieldCode = "UN";
                            }
                            if (fieldCode == "UN")
                            {
                                ULCustomer guest = new ULCustomer();
                                bool pwRecoverySet = guest.PasswordRecoverySet(fieldValue, fieldCode);
                                if (pwRecoverySet)
                                {
                                    //txtUsername.Enabled = false;
                                    //txtEmail.Enabled = false;
                                    //txtUsername.ReadOnly = true;
                                    //txtEmail.ReadOnly = true;
                                    //btnSubmit.Enabled = false;
                                    //lblSuccessMesg.Text = "Email sent successfully. Please check your email for instructions.";
                                    Response.Redirect("ForgotPwSuccess.aspx", false);
                                }
                                else
                                {
                                    lblErrMesg.Text = "Error occured during password recovery process";
                                    ImageValidator.Text = " ";
                                    UsernameValidator.Text = " ";
                                    txtimgcode.Text = "";
                                }
                            }
                            else
                            {
                                lblErrMesg.Text = "Error 11"; //Username is not found in database or your account is not activated
                                ImageValidator.Text = " ";
                                UsernameValidator.Text = " ";
                                txtimgcode.Text = "";
                            }
                        }
                        else
                        {
                            lblErrMesg.Text = "Invalid Phone Number"; //Phone Number doesn't match
                            ImageValidator.Text = " ";
                            UsernameValidator.Text = " ";
                            txtimgcode.Text = "";
                        }
                    }
                }                  
                else
                {
                    lblErrMesg.Text = "Please enter both Username and Mobile Number.";
                    ImageValidator.Text = " ";
                    UsernameValidator.Text = " ";
                    txtimgcode.Text = "";
                }
            }

            else if(RadioButton2.Checked)
            {
                email = txtUnameEmail.Text.Trim();
                mobileNo = txtPhoneNo.Text.Trim();

                if (email != "" && mobileNo != "")
                {
                    ULCustomer dm = new ULCustomer();
                    if (dm.checkEmailExist(email))
                    {
                        string retrievedMobileNo = dm.getEmailMobile(email);

                        if (mobileNo.Equals(retrievedMobileNo))
                        {
                            InfoValidator emailValidator = new InfoValidator();
                            if (emailValidator.IsRegisteredEmailForWeb(email) == 1)
                            {
                                fieldValue = email;
                                fieldCode = "EM";
                            }
                            if (fieldCode == "EM")
                            {
                                ULCustomer guest = new ULCustomer();
                                bool pwRecoverySet = guest.PasswordRecoverySet(fieldValue, fieldCode);
                                if (pwRecoverySet)
                                {
                                    //txtUsername.Enabled = false;
                                    //txtEmail.Enabled = false;
                                    //txtUsername.ReadOnly = true;
                                    //txtEmail.ReadOnly = true;
                                    //btnSubmit.Enabled = false;
                                    //lblSuccessMesg.Text = "Email sent successfully. Please check your email for instructions.";
                                    Response.Redirect("ForgotPwSuccess.aspx", false);
                                }
                                else
                                {
                                    lblErrMesg.Text = "Error occured during password recovery process";
                                    ImageValidator.Text = " ";
                                    UsernameValidator.Text = " ";
                                    txtimgcode.Text = "";
                                }
                            }
                            else
                            {
                                lblErrMesg.Text = "Error 11"; //Email is not found in database or your account is not activated
                                ImageValidator.Text = " ";
                                UsernameValidator.Text = " ";
                                txtimgcode.Text = "";
                            }
                        }
                        else
                        {
                            lblErrMesg.Text = "Invalid Phone Number"; //Phone Number doesn't match
                            ImageValidator.Text = " ";
                            UsernameValidator.Text = " ";
                            txtimgcode.Text = "";
                        }
                        
                    }
                    else
                    {
                        lblErrMesg.Text = "Invalid Email Address"; //Invalid email - does not exist in email column 
                        ImageValidator.Text = " ";
                        UsernameValidator.Text = " ";
                        txtimgcode.Text = "";
                    }
                }
                else
                {
                    lblErrMesg.Text = "Please enter both Email and Mobile Number.";
                    ImageValidator.Text = " ";
                    UsernameValidator.Text = " ";
                    txtimgcode.Text = "";
                }
            }
  
        }
    }
    protected void checkImageCode(object source, ServerValidateEventArgs args)
    {
        if (String.IsNullOrEmpty(txtimgcode.Text))
        {
            ImageValidator.ErrorMessage = "Enter the text found in the image";
            args.IsValid = false;
        }
        else
        {
            if (this.txtimgcode.Text == this.Session["CaptchaImageText"].ToString())
            {
                args.IsValid = true;
            }
            else
            {
                ImageValidator.ErrorMessage = "Entered text doesn't match with the text found in the image";
                args.IsValid = false;
                UsernameValidator.Text = " ";
                lblErrMesg.Text = " ";
                txtimgcode.Text = "";
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string s = DateTime.Now.ToLongTimeString();
        //Image1.ImageUrl = "~/CImage.aspx?i=" + s;
        //UpdatePanel4.Update();
    }

    protected void checkUsername(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";
        string username = "";

        InfoValidator validator = new InfoValidator();

        username = txtUnameEmail.Text.Trim();
        ULCustomer dm = new ULCustomer();

        if (!dm.checkUsernameExist(username) & RadioButton1.Checked)
        {
            //validator.validateUsername(txtUnameEmail.Text.Trim(), out status, out message);

            status = validator.IsRegisteredUsername(txtUnameEmail.Text.Trim());
            if (status == 1)
            {
                args.IsValid = true;

            }
            else
            {
                args.IsValid = false;
                UsernameValidator.ErrorMessage = "Invalid Username"; //Invalid Username
                lblErrMesg.Text = " ";
                ImageValidator.Text = " ";
                txtimgcode.Text = "";

            }
        }

        //if (rdUsername.Value == "Username")
        //{

        //    validator.validateUsername(txtUnameEmail.Text.Trim(), out status, out message);

        //    if (status == 0)
        //    {
        //        args.IsValid = true;

        //    }
        //    else
        //    {
        //        args.IsValid = false;
        //        UsernameValidator.ErrorMessage = message;

        //    }
        //}
        //else
        //{
        //    if(rdPassword.Value != "Email")
        //    {
        //        lblErrMesg.Text = "Please check either Username OR Email.";
        //    }
        //}

    }
}