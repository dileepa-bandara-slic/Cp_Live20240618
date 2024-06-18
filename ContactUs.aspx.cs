using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;

public partial class ContactUs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            Db_Email email = new Db_Email();
            if (email.send_html_email(ConfigurationManager.AppSettings["contactEmail"], txt_emailAdd.Text, "Client Portal : "+txt_subject.Text, txt_mail_body.Text, txt_mail_body.Text))
            {
                message.Text = "Email Sent Successfully.";
                txt_emailAdd.Text = "";
                txt_mail_body.Text = "";
                txt_senderName.Text = "";
                txt_subject.Text = "";
            }
            else
            {
                message.Text = "An error occurred while sending the email.";
            }
        }
    }

    protected void ChangeImage_Click(object sender, EventArgs e)
    {
        string s = DateTime.Now.ToLongTimeString();
        Image1.ImageUrl = "~/CImage.aspx?i=" + s;
        //UpdatePanel4.Update();
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
            }
        }
    }

}