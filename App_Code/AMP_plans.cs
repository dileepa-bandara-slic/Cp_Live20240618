using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for AMP_plans
/// </summary>
public class AMP_plans
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    public string plan_code { get; private set; }
    public string plan_description { get; private set; }
    public double plan_limit { get; private set; }

	public AMP_plans()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public AMP_plans(string code)
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public AMP_plans(string code, string entry_date)
    {
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string sql = "select * from sligen.amp_plans where code = :cod and  to_date (:entry, 'yyyy/mm/dd') BETWEEN effect_from and effect_to";
            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {
                OracleParameter opcode = new OracleParameter();
                opcode.Value = code;
                opcode.ParameterName = "cod";

                OracleParameter opentry = new OracleParameter();
                opentry.Value = entry_date;
                opentry.ParameterName = "entry";

                cmd.Parameters.Add(opcode);
                cmd.Parameters.Add(opentry);

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    plan_code = reader[0].ToString().Trim();
                    plan_description = reader[1].ToString().Trim();
                    if (!reader.IsDBNull(2))
                    {
                        plan_limit = Convert.ToDouble(reader[2].ToString().Trim());
                    }
                    else
                    {
                        plan_limit = 0;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            log logger = new log();
            logger.write_log("Failed at AMP plan constructor: " + ex.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
    }
}