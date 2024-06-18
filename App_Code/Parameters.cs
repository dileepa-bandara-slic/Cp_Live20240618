using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

/// <summary>
/// Summary description for Parameters
/// </summary>
public class Parameters
{
    public double re_loadning { get; set; }
    public double re_ceed_rate { get; set; }
    public int sick_wait_mnths { get; set; }
    public int mat_wait_mnths { get; set; }
    public int quot_val_days { get; set; }
    public double admin_fee { get; set; }
    public double pol_fee { get; set; }

    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    DataManager dm = new DataManager();
    public Parameters()
    {
        string sql = "select * from SLIGEN.AMP_PARAMETERS where sysdate between effect_from and effect_to ";

        DataSet ds = new DataSet();
        using (OracleDataAdapter dataAdd = new OracleDataAdapter(sql, oconn))
        {
            ds.Clear();
            dataAdd.Fill(ds);
        }
        DataTable dt = ds.Tables[0];

        foreach (DataRow row in dt.Rows)
        {
            re_loadning = Convert.ToDouble(row[0].ToString().Trim());
            re_ceed_rate = Convert.ToDouble(row[1].ToString().Trim());
            sick_wait_mnths = Convert.ToInt32(row[2].ToString().Trim());
            mat_wait_mnths = Convert.ToInt32(row[3].ToString().Trim());
            quot_val_days = Convert.ToInt32(row[4].ToString().Trim());
            admin_fee = Convert.ToDouble(row[7].ToString().Trim());
            pol_fee = Convert.ToDouble(row[8].ToString().Trim());
        }
    } 
  
    public Parameters(string date)
    {
        string sql = "select * from SLIGEN.AMP_PARAMETERS where to_date('" + date + "', 'yyyy/mm/dd') between effect_from and effect_to ";
        if (dm.existRecored(sql) > 0)
        {
            DataSet ds = new DataSet();
            ds = dm.getrow(sql);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                re_loadning = Convert.ToDouble(row[0].ToString().Trim());
                re_ceed_rate = Convert.ToDouble(row[1].ToString().Trim());
                sick_wait_mnths = Convert.ToInt32(row[2].ToString().Trim());
                mat_wait_mnths = Convert.ToInt32(row[3].ToString().Trim());
                quot_val_days = Convert.ToInt32(row[4].ToString().Trim());
                admin_fee = Convert.ToDouble(row[7].ToString().Trim());
                pol_fee = Convert.ToDouble(row[8].ToString().Trim());
            }

        }
        dm.connclose();
    }
}