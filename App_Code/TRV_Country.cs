using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TRV_Country
/// </summary>
public class TRV_Country
{
    OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    public DataTable dtCountry = new DataTable();
    public TRV_Country()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool check_schengen(string country_code)
    {
        log lg = new log();
        lg.write_log(country_code);
        bool result = false;

        try
        {
            conn.Open();
            string sql = "select * from SLIGEN.TRV_COUNTRIES where country_id = :cid and  schenegen = 'Y'";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = conn;

            OracleParameter cno_01 = new OracleParameter();
            cno_01.Value = country_code.Trim();
            cno_01.ParameterName = "cid";

            cmd.Parameters.Add(cno_01);
            cmd.ExecuteReader();

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result = true;
                break;
            }

            if (!reader.IsClosed)
                reader.Close();
        }
        catch (Exception ex)
        {

            lg.write_log(ex.ToString());
            result = false;
        }
        finally
        {
            conn.Close();

        }
        lg.write_log(result.ToString());
        return result;
    }
    public TRV_Country(string Destination)
    {
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            string sql = "select  country_name from SLIGEN.TRV_COUNTRIES where country_id in (" + Destination+") ";
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                OracleDataAdapter da = new OracleDataAdapter(sql, conn);          

                da.Fill(dtCountry);
            }
        }
        catch (Exception Exc)
        {

        }
    }
}