using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Web;


    public class OracleConnectionManager
    {
        public OracleConnection GetConnection()
        {
            var conString = System.Configuration.ConfigurationManager.ConnectionStrings["OrclConnection"];
            string strConnString = conString.ConnectionString;
            return new OracleConnection(strConnString);
        }
    }
