using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


    public class log
    {

        public log()
        {

        }

        public void write_log(string msg)
        {


            try
            {
                var file = @"D:\\WebLogs\\error.log";
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

