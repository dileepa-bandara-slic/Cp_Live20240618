using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Data.OracleClient;

/// <summary> 
/// Summary description for DB2 
/// </summary> 
public class DB2
{
    OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    public OracleCommand odcom = new OracleCommand();

    public DB2()
    {
        // 
        // TODO: Add constructor logic here 
        // 
    }

    public DataSet getrows(string sql, DataSet ds)
    {
        ds.Clear();
        try
        {
            conn.Open();
            odcom.Connection = conn;
            OracleDataAdapter data = new OracleDataAdapter(sql, conn);
            ds.Clear();
            data.Fill(ds);
        }
        catch
        {
        }
        finally
        {
            conn.Close();
        }
        return ds;
    }

    public bool isExists(string sql)
    {
        bool result = false;
        conn.Open();
        odcom.Connection = conn;
        odcom.CommandText = sql;
        OracleDataReader reader = odcom.ExecuteReader();
        while (reader.Read())
        {
            result = true;
            break;
        }
        conn.Close();
        return result;
    }


}