using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;

/// <summary>
/// Summary description for LogMail
/// </summary>
public class LogMail
{
	public LogMail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void write_log(string msg)
    {
        try
        {
            string logDirectory = ConfigurationManager.AppSettings["logDir"];
            var file = @logDirectory + "\\mails.log";
            var fs = File.Open(file, FileMode.Append, FileAccess.Write, FileShare.Read);
            var sw = new StreamWriter(fs);
            sw.AutoFlush = true;
            sw.Write(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss tt"));
            sw.Write(" : ");
            sw.WriteLine(msg);
            //sw.WriteLine("**********************************************************");

            sw.Close();
        }
        catch
        {

        }

    }
}