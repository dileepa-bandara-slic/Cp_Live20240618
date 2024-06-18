using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Net.Mail;


/// <summary>
/// Summary description for utils
/// </summary>
public class utils
{
    public bool IsAlphaNumeric(string text)
    {
        return Regex.IsMatch(text, "^[a-zA-Z0-9]+$");
    }

    public bool IsEmailAddress(string text)
    {
        return Regex.IsMatch(text, "w+([-+.']w+)*@w+([-.]w+)*.w+([-.]w+)*");
    }

    public bool sendEmail(string receiverEmail, string subject, string content)
    {
        bool returnValue = false;

        try
        {
            MailMessage message = new MailMessage();
            MailAddress Sender = new MailAddress(ConfigurationManager.AppSettings["smtpUser"]);
            MailAddress receiver = new MailAddress(receiverEmail);
            SmtpClient smtp = new SmtpClient()
            {
                Host = ConfigurationManager.AppSettings["smtpServer"],
                Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]),
                //EnableSsl = true,
                Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["smtpUser"], ConfigurationManager.AppSettings["smtpPass"])

            };
            message.From = Sender;
            message.To.Add(receiver);
            message.Subject = subject;
            message.Body = content;
            message.IsBodyHtml = true;
            smtp.Send(message);

            returnValue = true;
        }
        catch (Exception e)
        {
            returnValue = false;
            log logger = new log();
            logger.write_log("Failed at sendEmail: " + e.ToString());
        }
        finally
        {

        }
        return returnValue;
    }

    public void contactus( string fromAdd, string subject, string textmsg, string htmlmsg)
    {
        Db_Email dbe = new Db_Email();
        dbe.send_html_email(ConfigurationManager.AppSettings["contactEmail"], fromAdd, subject, textmsg, htmlmsg);
    }

}