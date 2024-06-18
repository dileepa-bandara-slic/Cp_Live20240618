using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Payment_Type
/// </summary>
public class Payment_Type
{

    OracleConnection oconnGen = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);

    OracleConnection oconnLife = new OracleConnection(ConfigurationManager.AppSettings["OraLifeDB"]);
    public Payment_Type()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public bool update_pay_method_in_renewal(string refNo, string Amex_PayMethod)
    {
        bool returnValue = false;
        if (oconnLife.State != ConnectionState.Open)
        {
            oconnLife.Open();
        }
        OracleCommand cmd = oconnLife.CreateCommand();
        OracleTransaction trans_update_pay_method = oconnLife.BeginTransaction();
        cmd.Transaction = trans_update_pay_method;
        try
        {
            using (cmd)
            {
                string updatePayment = "Update SLIC_NET_LIFE.RENEWAL_DETAILS Set PAY_METHOD = :Amex_PayMethod where RECEIPT_NO = :refNo";

                cmd.CommandText = updatePayment;

                OracleParameter oPaymentMethod = new OracleParameter();
                oPaymentMethod.DbType = DbType.String;
                oPaymentMethod.Value = Amex_PayMethod;
                oPaymentMethod.ParameterName = "Amex_PayMethod";

                OracleParameter oRefNo = new OracleParameter();
                oRefNo.DbType = DbType.String;
                oRefNo.Value = refNo;
                oRefNo.ParameterName = "refNo";

                cmd.Parameters.Add(oPaymentMethod);
                cmd.Parameters.Add(oRefNo);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                # region Update payment method of policy fee receipt.

                if (refNo.Contains("/N/"))
                {
                    LifeProposal prop = new LifeProposal();
                    string polFeeRectNo = prop.getPolicyFeeRecpt(refNo);

                    string updatePaymentPolFee = "Update SLIC_NET_LIFE.RENEWAL_DETAILS Set PAY_METHOD = :Amex_PayMethod2 where RECEIPT_NO = :refNo2";

                    cmd.CommandText = updatePaymentPolFee;

                    OracleParameter oPayMethod = new OracleParameter();
                    oPayMethod.DbType = DbType.String;
                    oPayMethod.Value = Amex_PayMethod;
                    oPayMethod.ParameterName = "Amex_PayMethod2";

                    OracleParameter oPolFeeRefNo = new OracleParameter();
                    oPolFeeRefNo.DbType = DbType.String;
                    oPolFeeRefNo.Value = polFeeRectNo;
                    oPolFeeRefNo.ParameterName = "refNo2";

                    cmd.Parameters.Add(oPayMethod);
                    cmd.Parameters.Add(oPolFeeRefNo);

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }

                # endregion


                trans_update_pay_method.Commit();
                returnValue = true;

            }

        }
        catch (Exception e)
        {
            trans_update_pay_method.Rollback();
            log logger = new log();
            logger.write_log("Failed at LifePayment - update_pay_method in renewal details: " + e.ToString());
        }
        finally
        {
            if (oconnLife.State == ConnectionState.Open)
            {
                oconnLife.Close();
            }
        }

        return returnValue;
    }


    public bool update_gen_pay_method_in_renewal(string refNo, string AMEX_PayMethod)
    {
        bool returnValue = false;
        if (oconnGen.State != ConnectionState.Open)
        {
            oconnGen.Open();
        }
        OracleCommand cmd = oconnGen.CreateCommand();
        OracleTransaction trans_update_pay_method = oconnGen.BeginTransaction();
        cmd.Transaction = trans_update_pay_method;
        try
        {
            using (cmd)
            {
                string updatePayment = "Update SLIC_NET.RENEWAL_DETAILS Set PAY_METHOD = :AMEX_PayMethod where RECEIPT_NO = :refNo";

                cmd.CommandText = updatePayment;

                OracleParameter oPaymentMethod = new OracleParameter();
                oPaymentMethod.DbType = DbType.String;
                oPaymentMethod.Value = AMEX_PayMethod;
                oPaymentMethod.ParameterName = "AMEX_PayMethod";

                OracleParameter oRefNo = new OracleParameter();
                oRefNo.DbType = DbType.String;
                oRefNo.Value = refNo;
                oRefNo.ParameterName = "refNo";

                cmd.Parameters.Add(oPaymentMethod);
                cmd.Parameters.Add(oRefNo);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();


                trans_update_pay_method.Commit();
                returnValue = true;

            }

        }
        catch (Exception e)
        {
            trans_update_pay_method.Rollback();
            log logger = new log();
            logger.write_log("Failed at GeneralPayment - update_pay_method in renewal details: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return returnValue;
    }


    public bool update_gen_pay_method_in_proposal_details(string refNo, string AMEX_PayMethod)
    {
        bool returnValue = false;
        if (oconnGen.State != ConnectionState.Open)
        {
            oconnGen.Open();
        }
        OracleCommand cmd = oconnGen.CreateCommand();
        OracleTransaction trans_update_pay_method_prop_details = oconnGen.BeginTransaction();
        cmd.Transaction = trans_update_pay_method_prop_details;
        try
        {
            using (cmd)
            {
                string updatePayment = "Update SLIC_NET.PROPOSAL_DETAILS Set PAY_METHOD = :AMEX_PayMethod where REF_NO = :refNo";

                cmd.CommandText = updatePayment;

                OracleParameter o_GEN_PaymentMethod = new OracleParameter();
                o_GEN_PaymentMethod.DbType = DbType.String;
                o_GEN_PaymentMethod.Value = AMEX_PayMethod;
                o_GEN_PaymentMethod.ParameterName = "AMEX_PayMethod";

                OracleParameter o_GEN_RefNo = new OracleParameter();
                o_GEN_RefNo.DbType = DbType.String;
                o_GEN_RefNo.Value = refNo;
                o_GEN_RefNo.ParameterName = "refNo";

                cmd.Parameters.Add(o_GEN_PaymentMethod);
                cmd.Parameters.Add(o_GEN_RefNo);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();


                trans_update_pay_method_prop_details.Commit();
                returnValue = true;

            }

        }
        catch (Exception e)
        {
            trans_update_pay_method_prop_details.Rollback();
            log logger = new log();
            logger.write_log("Failed at GeneralPayment - update_paid_method_in_proposal_details: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return returnValue;
    }
}