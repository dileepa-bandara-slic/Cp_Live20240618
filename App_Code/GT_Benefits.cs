using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

/// <summary>
/// Summary description for GT_Benefits
/// </summary>
public class GT_Benefits
{
    OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    public DataTable DTproduct { get; set; }

    public GT_Benefits()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public GT_Benefits(string product)
    {
        string sql = "";
        DTproduct = null;
        switch (product.ToUpper())
        {
            case "GB500":
                sql = "select benefit, sum_GB500 AS SUM_INSURED, exc_GB500 AS EXCESS from SLIC_NET.GLOBE_TROT_BENEFITS";
                break;

            case "GB100":
                sql = "select benefit, sum_GB100 AS SUM_INSURED, exc_GB100 AS EXCESS from SLIC_NET.GLOBE_TROT_BENEFITS";
                break;

            case "GB50":
                sql = "select benefit, sum_GB50 AS SUM_INSURED, exc_GB50 AS EXCESS from SLIC_NET.GLOBE_TROT_BENEFITS";
                break;

            case "ST100":
                sql = "select benefit, sum_ST100 AS SUM_INSURED, exc_ST100 AS EXCESS from SLIC_NET.GLOBE_TROT_BENEFITS";
                break;

            case "ST50":
                sql = "select benefit, sum_ST50 AS SUM_INSURED, exc_ST50 AS EXCESS from SLIC_NET.GLOBE_TROT_BENEFITS";
                break;

            case "AS25":
                sql = "select benefit, sum_AS25 AS SUM_INSURED, exc_AS25 AS EXCESS from SLIC_NET.GLOBE_TROT_BENEFITS";
                break;

        }
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                DataTable dt = new DataTable();
                OracleDataAdapter oad = new OracleDataAdapter(cmd);
                oad.Fill(dt);

                if (dt.Rows.Count > 0)
                    DTproduct = dt;
            }

        }
        catch (Exception es)
        {
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        
    }
}