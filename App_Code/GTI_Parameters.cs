using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

/// <summary>
/// Summary description for GTI_Parameters
/// </summary>
public class GTI_Parameters
{
    public double admin_fee { get; set; }
    public double pol_fee { get; set; }

    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);

	public GTI_Parameters()
	{
        string sql = "select * from SLIC_NET.GTI_PARAMETERS where sysdate between effect_from and effect_to ";

        DataSet ds = new DataSet();
        using (OracleDataAdapter dataAdd = new OracleDataAdapter(sql, oconn))
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
	}
}