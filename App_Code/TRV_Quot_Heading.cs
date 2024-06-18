using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TRV_Quot_Heading
/// </summary>
public class TRV_Quot_Heading
{
    OracleConnection objOraCon = new OracleConnection();
    OracleCommand objOraCom = new OracleCommand();

    public string QUOTATION_NO { get; set; }
    public string SUFFIX { get; set; }
    public string UPPER_CONTENT { get; set; }
    public string BOTTOM_CONTENT { get; set; }
    public string CONDITIONS { get; set; }
    //public string TYPE { get;  set; }
    public Type TYPE { get; set; }
    public string ENTERED_BY { get; set; }
    public DateTime ENTERED_DATE { get; set; }

    public void connectDB()
    {
        objOraCon.ConnectionString = ConfigurationManager.AppSettings.Get("OracleDB").ToString();
        if (objOraCon.State != ConnectionState.Open)
            objOraCon.Open();
    }
    public void disconnectDB()
    {
        if (objOraCon.State != ConnectionState.Closed)
        {
            objOraCon.Close();
        }
    }

    public TRV_Quot_Heading()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public TRV_Quot_Heading(string quot_No)
    {
        try
        {
            connectDB();

            string sql = "SELECT QUOTATION_NO,SUFFIX,UPPER_CONTENT,BOTTOM_CONTENT,CONDITIONS,TYPE,ENTERED_BY,To_char(ENTERED_DATE,'yyyy-MM-dd') AS ENTERED_DATE FROM SLIGEN.TRV_QUOT_COVER_LETTER " +
                         " WHERE QUOTATION_NO = :quot_No ";

            using (OracleCommand cmd = new OracleCommand(sql, objOraCon))
            {
                OracleParameter opRefNo = new OracleParameter();
                opRefNo.Value = quot_No;
                opRefNo.ParameterName = "quot_No";

                cmd.Parameters.Add(opRefNo);

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    QUOTATION_NO = reader[0].ToString();
                    SUFFIX = reader[1].ToString();
                    UPPER_CONTENT = reader[2].ToString().Trim();
                    BOTTOM_CONTENT = reader[3].ToString().Trim();
                    CONDITIONS = reader[4].ToString().Trim();

                    Type TYPE = (Type)Enum.Parse(typeof(Type), reader[5].ToString().Trim(), true);

                    ENTERED_BY = reader[6].ToString().Trim();
                    ENTERED_DATE = Convert.ToDateTime(reader[7].ToString());

                }
                reader.Close();
            }

        }
        catch (Exception ex)
        {

        }
        finally
        {
            disconnectDB();
        }

    }

}