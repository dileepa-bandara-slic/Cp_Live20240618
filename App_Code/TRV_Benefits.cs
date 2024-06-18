using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TRV_Benefits
/// </summary>
public class TRV_Benefits
{
    OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    public DataTable DTproduct { get; set; }
    public TRV_Benefits()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public TRV_Benefits(string product)
    {
        string sql = "";
        DTproduct = null;
        switch (product.ToUpper())
        {

            case "AS25":
                sql = "select code,benefit, SUM_AS25 AS SUM_INSURED, EXC_AS25 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "AAS25":
                sql = "select code,benefit, SUM_AS25 AS SUM_INSURED, EXC_AS25 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "BSC1":
                sql = "select code,benefit, SUM_BSC1 AS SUM_INSURED, EXC_BSC1 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "ABSC1":
                sql = "select code,benefit, SUM_BSC1 AS SUM_INSURED, EXC_BSC1 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "BSC2":
                sql = "select code,benefit, SUM_BSC2 AS SUM_INSURED, EXC_BSC2 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "ABSC2":
                sql = "select code,benefit, SUM_BSC2 AS SUM_INSURED, EXC_BSC2 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "EST1":
                sql = "select code,benefit, SUM_EST1 AS SUM_INSURED, EXC_EST1 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "AEST1":
                sql = "select code,benefit, SUM_EST1 AS SUM_INSURED, EXC_EST1 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "EST2":
                sql = "select code,benefit, SUM_EST2 AS SUM_INSURED, EXC_EST2 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "AEST2":
                sql = "select code,benefit, SUM_EST2 AS SUM_INSURED, EXC_EST2 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "EST3":
                sql = "select code,benefit, SUM_EST3 AS SUM_INSURED, EXC_EST3 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "AEST3":
                sql = "select code,benefit, SUM_EST3 AS SUM_INSURED, EXC_EST3 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "STD1":
                sql = "select code,benefit, SUM_STD1 AS SUM_INSURED, EXC_STD1 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "ASTD1":
                sql = "select code,benefit, SUM_STD1 AS SUM_INSURED, EXC_STD1 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "STD2":
                sql = "select code,benefit, SUM_STD2 AS SUM_INSURED, EXC_STD2 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
                break;

            case "ASTD2":
                sql = "select code,benefit, SUM_STD2 AS SUM_INSURED, EXC_STD2 AS EXCESS from SLIGEN.TRV_BENEFITS order by code ASC";
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