using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;

/// <summary>
/// Summary description for Email
/// </summary>
public class Email
{
    string  mailgateway_ip;
    string mailgateway_port;

	public Email()
	{
        mailgateway_ip = ConfigurationManager.AppSettings["smtpServer"].Trim();
        mailgateway_port = ConfigurationManager.AppSettings["smtpPort"].Trim();
	}

    public bool send_html_email(string from_add, string from_dispName, string toAdd, string subject, string bodyhtml, string bodytext, List<string> bccList)
    {
        bool result = false;

        try
        {
        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
        message.To.Add(toAdd);

        bodytext = System.Net.WebUtility.HtmlDecode(bodytext);
        message.Subject = subject;
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        message.From = new System.Net.Mail.MailAddress(from_add, from_dispName);
        
        System.Net.Mail.MailAddress bcc;
        foreach (string email in bccList)
        {
            string eml_add_bcc = email.Trim();
            bcc = new MailAddress(eml_add_bcc);
            message.Bcc.Add(bcc);
        }

        AlternateView plainView = AlternateView.CreateAlternateViewFromString(bodytext, null, "text/plain");
        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(bodyhtml, null, "text/html");

        //create the LinkedResource (embedded image)
        LinkedResource logo1 = new LinkedResource(System.Web.HttpContext.Current.Server.MapPath("~/Images/slic_logo.png"));
        logo1.ContentId = "companylogo";
        //add the LinkedResource to the appropriate view
        htmlView.LinkedResources.Add(logo1);

        //add the views
        message.AlternateViews.Add(plainView);
        message.AlternateViews.Add(htmlView);

        SmtpClient smtp = new SmtpClient(mailgateway_ip, Convert.ToInt32( mailgateway_port));

        smtp.Send(message);
        result = true;
        }
        catch (Exception ee)
        {
            result = false;

        }
        return result;
    }

}