using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TRV_Contacts
/// </summary>
public class TRV_Contacts
{
    public DataTable dtContct = new DataTable();
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    public TRV_Contacts()
    {
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string sql = "SELECT COMPANY, ADDRESS_1 , ADDRESS_2 ,ADDRESS_3,ADDRESS_4,ADDRESS_5,TELE_NO,FAX_NO,TOLL_FREE,SLIC_HELP,EMAIL " +
                         " FROM SLIGEN.TRV_TPA_COMPANY WHERE(EXPIRY_DATE >= sysdate)";
            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {
                OracleDataAdapter da = new OracleDataAdapter(sql, oconn);
                da.Fill(dtContct);
            }
        }
        catch (Exception Exc)
        {

        }
    }
    public TRV_Contacts(string Purp)
    {
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string sql = "SELECT VISIT_CODE,VISIT_REASON " +
                         " FROM SLIGEN.TRV_PURPOSES  order by visit_reason asc";
            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {
                OracleDataAdapter da = new OracleDataAdapter(sql, oconn);
                da.Fill(dtContct);
            }
        }
        catch (Exception Exc)
        {

        }
    }
}