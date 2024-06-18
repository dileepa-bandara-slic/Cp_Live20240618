using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TRV_TPA_Company
/// </summary>
public class TRV_TPA_Company
{
    OracleConnection objOraCon = new OracleConnection();

    public string POL_TYPE { get; set; }
    public string COMPANY { get; set; }
    public string ADDRESS_1 { get; set; }
    public string ADDRESS_2 { get; set; }
    public string ADDRESS_3 { get; set; }
    public string ADDRESS_4 { get; set; }
    public string ADDRESS_5 { get; set; }
    public string TELE_NO { get; set; }
    public string FAX_NO { get; set; }
    public string TOLL_FREE { get; set; }
    public string SLIC_HELP { get; set; }
    public string EMAIL { get; set; }
    public DateTime EFFECT_DATE { get; set; }
    public DateTime EXPIRY_DATE { get; set; }
    public DateTime ENTERED_DATE { get; set; }
    public string ENTERED_BY { get; set; }
    public TRV_TPA_Company()
    {
        //
        // TODO: Add constructor logic here
        //
    }
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


    public TRV_TPA_Company(DateTime currDate)
    {
        try
        {
            connectDB();

              List<string> cuDate = currDate.ToString().Split(' ').ToList();

            string sql = "SELECT POL_TYPE,COMPANY,ADDRESS_1,ADDRESS_2,ADDRESS_3,ADDRESS_4,ADDRESS_5,TELE_NO,FAX_NO,TOLL_FREE,SLIC_HELP,EMAIL,EFFECT_DATE,EXPIRY_DATE,ENTERED_DATE,ENTERED_BY FROM SLIGEN.TRV_TPA_COMPANY " +
                         " WHERE EFFECT_DATE in (select max(EFFECT_DATE) from sligen.TRV_TPA_COMPANY where EFFECT_DATE <= to_date('" + cuDate[0] + "','MM/dd/yyyy') and EXPIRY_DATE >= to_date('" + cuDate[0] + "','MM/dd/yyyy') )";


            using (OracleCommand cmd = new OracleCommand(sql, objOraCon))
            {
           
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    POL_TYPE = reader[0].ToString();
                    COMPANY = reader[1].ToString();
                    ADDRESS_1 = reader[2].ToString().Trim();
                    ADDRESS_2 = reader[3].ToString().Trim();
                    ADDRESS_3 = reader[4].ToString().Trim();

                    ADDRESS_4 = reader[5].ToString().Trim();
                    ADDRESS_5 = reader[6].ToString().Trim();
                    TELE_NO = reader[7].ToString().Trim();
                    FAX_NO = reader[8].ToString().Trim();
                    TOLL_FREE = reader[9].ToString().Trim();

                    SLIC_HELP = reader[10].ToString().Trim();
                    EMAIL = reader[11].ToString().Trim();
                    EFFECT_DATE = Convert.ToDateTime(reader[12].ToString().Trim());
                    EXPIRY_DATE = Convert.ToDateTime(reader[13].ToString().Trim());
                    ENTERED_DATE = Convert.ToDateTime(reader[14].ToString().Trim());
                    ENTERED_BY = reader[15].ToString().Trim();
                }
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