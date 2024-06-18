using System;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;

/// <summary>
/// Summary description for TRV_Parameters
/// </summary>
public class TRV_Parameters
{
    OracleConnection objOraCon = new OracleConnection();
    OracleCommand objOraCom = new OracleCommand();
    public double admin_fee { get; set; }
    public double pol_fee { get; set; }

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

    public TRV_Parameters()
    {
        string sql = "select * from SLIGEN.TRV_PARAMETERS where sysdate between effect_from and effect_to ";
        this.connectDB();
        DataSet ds = new DataSet();
        using (OracleDataAdapter dataAdd = new OracleDataAdapter(sql, objOraCon))
        {
            ds.Clear();
            dataAdd.Fill(ds);
        }
        DataTable dt = ds.Tables[0];

        foreach (DataRow row in dt.Rows)
        {
            admin_fee = Convert.ToDouble(row[0].ToString().Trim());
            pol_fee = Convert.ToDouble(row[1].ToString().Trim());
        }
        this.disconnectDB();
    }

    public TRV_Parameters(string PolType)
    {
        string sql = "select * from SLIGEN.TRV_PARAMETERS where sysdate between effect_from and effect_to ";
        this.connectDB();
        DataSet ds = new DataSet();
        using (OracleDataAdapter dataAdd = new OracleDataAdapter(sql, objOraCon))
        {
            ds.Clear();
            dataAdd.Fill(ds);
        }
        DataTable dt = ds.Tables[0];

        foreach (DataRow row in dt.Rows)
        {
            admin_fee = Convert.ToDouble(row[0].ToString().Trim());
            pol_fee = Convert.ToDouble(row[4].ToString().Trim());
        }
        this.disconnectDB();
    }

}