using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net.Mail;

/// <summary>
/// Summary description for Db_Email
/// </summary>
public class Db_Email
{
    //OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["OraLifeDB"]);
    
    public OracleCommand cmd = new OracleCommand(); 

	public Db_Email()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool send_html_email(string toAdd,  string subject, string textmsg, string htmlmsg)
    {
        bool result = false;
        string sql = " sli_lund.send_html_email";

        try
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_to", OracleType.VarChar).Value = toAdd;
            cmd.Parameters.Add("p_from", OracleType.VarChar).Value = ConfigurationManager.AppSettings["smtpUser"]; 
            cmd.Parameters.Add("p_subject", OracleType.VarChar).Value = subject;
            cmd.Parameters.Add("p_text_msg", OracleType.VarChar).Value = textmsg;
            cmd.Parameters.Add("p_html_msg", OracleType.VarChar).Value = htmlmsg;
            cmd.Parameters.Add("p_smtp_host", OracleType.VarChar).Value = ConfigurationManager.AppSettings["smtpServer"];
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                result = true;
            }
        }
        catch (Exception e)
        {
            result = false;
            log logger = new log();
            logger.write_log("Failed at send_html_email: " + e.ToString());
        }
        finally
        {
            conn.Close();
        }
        return result;
    }

    public bool send_html_email(string toAdd, string fromAdd, string subject, string textmsg, string htmlmsg)
    {
        bool result = false;
        string sql = " sli_lund.send_html_email";

        try
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_to", OracleType.VarChar).Value = toAdd;
            cmd.Parameters.Add("p_from", OracleType.VarChar).Value = fromAdd;
            cmd.Parameters.Add("p_subject", OracleType.VarChar).Value = subject;
            cmd.Parameters.Add("p_text_msg", OracleType.VarChar).Value = textmsg;
            cmd.Parameters.Add("p_html_msg", OracleType.VarChar).Value = htmlmsg;
            cmd.Parameters.Add("p_smtp_host", OracleType.VarChar).Value = ConfigurationManager.AppSettings["smtpServer"];
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                result = true;
            }
        }
        catch (Exception e)
        {
            result = false;
            log logger = new log();
            logger.write_log("Failed at send_html_email: " + e.ToString());
        }
        finally
        {
            conn.Close();
        }
        return result;
    }

    public bool send_app_email(string toAdd, string subject, string textmsg, string htmlmsg, MemoryStream ms, string attachment_name, string bccEmail = null)
    {
        bool result = false;
        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

        string body = "";
        string sub = subject;
        try
        {
            body = htmlmsg.Replace("\"", "'");
            body = System.Net.WebUtility.HtmlDecode(body);
            message.Subject = sub;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["smtpUser"]);

            message.To.Add(toAdd);
            message.Bcc.Add(ConfigurationManager.AppSettings["smtpUser"]);

            if(bccEmail != null)
                message.Bcc.Add(bccEmail);

            AlternateView plainView = AlternateView.CreateAlternateViewFromString(textmsg, null, "text/plain");
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString("<html><body>" + body + "<br/><br/><br/>" + "<p align = 'left' style='font-size:11pt;'>Thanking you,</p><img src=cid:companylogo></body></html>", null, "text/html");

            //create the LinkedResource (embedded image)
            LinkedResource logo1 = new LinkedResource(System.Web.HttpContext.Current.Server.MapPath("~/Images/slic_logo.png"));
            logo1.ContentId = "companylogo";
            //add the LinkedResource to the appropriate view
            htmlView.LinkedResources.Add(logo1);

            //add the views
            message.AlternateViews.Add(plainView);
            message.AlternateViews.Add(htmlView);


            object userState = message;
            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"], 25);
            message.Attachments.Add(new Attachment(ms, attachment_name));
            smtp.Send(message);
            result = true;
        }
        catch (Exception s222)
        {
            string d = s222.ToString();
            log lg = new log();
            lg.write_log(d);
        }
        return result;
    }

    public bool send_html_email_withLogo(string toAdd, string subject, string textmsg, string htmlmsg)
    {
        bool result = false;
        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

        string body = "";
        string sub = subject;
        try
        {
            body = htmlmsg.Replace("\"", "'");
            body = System.Net.WebUtility.HtmlDecode(body);
            message.Subject = sub;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["smtpUser"]);

            message.To.Add(toAdd);
            message.Bcc.Add(ConfigurationManager.AppSettings["smtpUser"]);

            //if (bccEmail != null)
            //    message.Bcc.Add(bccEmail);

            AlternateView plainView = AlternateView.CreateAlternateViewFromString(textmsg, null, "text/plain");
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString("<html><body>" + body + "<br/><br/><br/>" + "<p align = 'left' style='font-size:11pt;'>Thanking you,</p><img src=cid:companylogo></body></html>", null, "text/html");

            //create the LinkedResource (embedded image)
            LinkedResource logo1 = new LinkedResource(System.Web.HttpContext.Current.Server.MapPath("~/Images/slic_logo.png"));
            logo1.ContentId = "companylogo";
            //add the LinkedResource to the appropriate view
            htmlView.LinkedResources.Add(logo1);

            //add the views
            message.AlternateViews.Add(plainView);
            message.AlternateViews.Add(htmlView);


            object userState = message;
            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"], 25);
            //message.Attachments.Add(new Attachment(ms, attachment_name));
            smtp.Send(message);
            result = true;
        }
        catch (Exception s222)
        {
            string d = s222.ToString();
            log lg = new log();
            lg.write_log(d);
        }
        return result;
    }
}