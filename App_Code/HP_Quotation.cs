using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
using System.Globalization;

/// <summary>
/// Summary description for HP_Quotation
/// </summary>
public class HP_Quotation
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);

    public double baseAnuPrem { get; private set; }
    public double polFee { get; private set; }
    public double sumInsured { get; private set; }
    public double sumInsured_Cont { get; private set; }
    public double admnFee { get; private set; }
    public double nbt { get; private set; }
    public double vat { get; private set; }
    public double totAnuPrem { get; private set; }
    public bool returnValue = false;
   

	public HP_Quotation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public HP_Quotation(string pro_id)
    {
        baseAnuPrem = 0;
        polFee = 0;
        sumInsured = 0;
        sumInsured_Cont = 0;
        admnFee = 0;
        nbt = 0;
        vat = 0;
        totAnuPrem = 0;
        Proposal pro = new Proposal(pro_id);

        try
        {

            oconn.Open();
            OracleCommand cmd = oconn.CreateCommand();
            string getPackageParas = "Select BASIC_ANU_PREM, POL_FEE, SUM_INSURED, sumins_conts" +
                                     " from SLIC_NET.PACKAGE_PARAMETERS" +
                                     " where CATEGORY = :plan " +
                                     " and POL_TYPE = 'HIP'" +
                                     " and sysdate between EFFECT_FROM and EFFECT_TO";

            using (cmd)
            {
                cmd.CommandText = getPackageParas;
                cmd.Parameters.AddWithValue("plan", pro.plan.Trim());

                //OracleParameter oSumIns = new OracleParameter();
                //oSumIns.DbType = DbType.Double;
                //oSumIns.Value = sumIns;
                //oSumIns.ParameterName = "sumIns";
                //cmd.Parameters.Add(oSumIns);

                OracleDataReader parasReader = cmd.ExecuteReader();
                while (parasReader.Read())
                {
                    if (!parasReader.IsDBNull(0))
                    {
                        baseAnuPrem = parasReader.GetDouble(0);
                    }
                    if (!parasReader.IsDBNull(1))
                    {
                        polFee = parasReader.GetDouble(1);
                    }
                    if (!parasReader.IsDBNull(2))
                    {
                        sumInsured = parasReader.GetDouble(2);
                    }
                    if (!parasReader.IsDBNull(3))
                    {
                        sumInsured_Cont = parasReader.GetDouble(3);
                    }
                }
                parasReader.Close();
                cmd.Parameters.Clear();

                string getAdminFee = "select ADMINFEE from GENPAY.POLTYP where PTDEP='G' AND PTTYP='HIP'";

                cmd.CommandText = getAdminFee;

                OracleDataReader admFeeReader = cmd.ExecuteReader();
                while (admFeeReader.Read())
                {
                    if (!admFeeReader.IsDBNull(0))
                    {
                        admnFee = Math.Round(baseAnuPrem * admFeeReader.GetDouble(0) / 100, 2);
                    }
                }
                admFeeReader.Close();
                cmd.Parameters.Clear();

                //-----------------NBL and VAT Calculation--------------------------------           
                //DateTime.Now.ToString("yyyy/MM/dd")

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GENPAY.CALCULATE_NBL_AND_VAT";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("taxLiableAmount", baseAnuPrem + admnFee + polFee);
                cmd.Parameters.AddWithValue("requestDate", DateTime.ParseExact(pro.entryDate, "yyyy/MM/dd", CultureInfo.InvariantCulture));
                cmd.Parameters.Add("nblAmount", OracleType.Number).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("vatAmount", OracleType.Number).Direction = ParameterDirection.Output;

                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                nbt = double.Parse(cmd.Parameters["nblAmount"].Value.ToString());// comm.Parameters("nblAmount");
                vat = double.Parse(cmd.Parameters["vatAmount"].Value.ToString()); //comm.Parameters("vatAmount");

                //------------------------------------------------------------
                dr.Close();


                totAnuPrem = baseAnuPrem + admnFee + polFee + nbt + vat;

                if (totAnuPrem > 0)
                {
                    returnValue = true;
                }
            }
        }
        catch (Exception e)
        {
            log lg = new log();
            lg.write_log("Failed at HP Quotation Constructor " + e.ToString());
        }
        finally
        {
 
        }
    }
}