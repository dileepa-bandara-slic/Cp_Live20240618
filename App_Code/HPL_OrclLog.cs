using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.IO;

    public class HPL_OrclLog
    {
        public bool WriteLog(string strMessage)
        {
            try
            {
                FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", ConfigurationManager.AppSettings["orcl_log"], "OrclHPLlog"), FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine(strMessage);
                objStreamWriter.Close();
                objFilestream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
