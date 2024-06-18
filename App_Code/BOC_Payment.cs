using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for BOC_Payment
/// </summary>
public class BOC_Payment
{
	public BOC_Payment()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string getLoginEmail(string userName)
    {
        OracleConnection oconn_getLoginEmail = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
        string emailAddress = "";

        try
        {
            if (oconn_getLoginEmail.State != ConnectionState.Open)
            {
                oconn_getLoginEmail.Open();
            }

            string getEmailAddress = "Select EMAIL from ULWEB.WEBUSERS where UPPER(USERNAME) = UPPER(:userName)";

            using (OracleCommand cmd = new OracleCommand(getEmailAddress, oconn_getLoginEmail))
            {
                cmd.Parameters.AddWithValue("username", userName);

                OracleDataReader emailAddressReader = (OracleDataReader)cmd.ExecuteReader();

                if (emailAddressReader.HasRows)
                {
                    while (emailAddressReader.Read())
                    {
                        if (!emailAddressReader.IsDBNull(0))
                        {
                            emailAddress = emailAddressReader.GetString(0);
                        }

                    }
                    emailAddressReader.Close();
                }
            }

        }
        catch (Exception e)
        {
            emailAddress = "Error";
            log logger = new log();
            logger.write_log("Failed at ULCustomer - getEmail: " + e.ToString());

        }
        finally
        {
            if (oconn_getLoginEmail.State == ConnectionState.Open)
            {
                oconn_getLoginEmail.Close();
            }
        }

        return emailAddress;
    }


    //Life
    public bool update_pay_method_in_renewal(string refNo, string BOC_PayMethod)
    {
        OracleConnection oconn_update_pay_method = new OracleConnection(ConfigurationManager.AppSettings["OraLifeDB"]);
        bool returnValue = false;
        if (oconn_update_pay_method.State != ConnectionState.Open)
        {
            oconn_update_pay_method.Open();
        }
        OracleCommand cmd = oconn_update_pay_method.CreateCommand();
        OracleTransaction trans_update_pay_method = oconn_update_pay_method.BeginTransaction();
        cmd.Transaction = trans_update_pay_method;
        try
        {
            using (cmd)
            {
                string updatePayment = "Update SLIC_NET_LIFE.RENEWAL_DETAILS Set PAY_METHOD = :BOC_PayMethod where RECEIPT_NO = :refNo";

                cmd.CommandText = updatePayment;

                OracleParameter oPaymentMethod = new OracleParameter();
                oPaymentMethod.DbType = DbType.String;
                oPaymentMethod.Value = BOC_PayMethod;
                oPaymentMethod.ParameterName = "BOC_PayMethod";

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
            logger.write_log("Failed at LifePayment - update_paid_renewal: " + e.ToString());
        }
        finally
        {
            if (oconn_update_pay_method.State == ConnectionState.Open)
            {
                oconn_update_pay_method.Close();
            }
        }

        return returnValue;
    }

    public bool Insert_BOCPaymentDetails_Life(string orderID, string orderAmount, string order_Currency, string order_reference, string sessionID, string succesIndicator)
    {
        bool BOCPaymentResult = false;

        try
        {

            using (OracleConnection connection_BOCPaymntData = new OracleConnection(ConfigurationManager.AppSettings["OraLifeDB"]))
            {
                connection_BOCPaymntData.Open();

                OracleCommand command = connection_BOCPaymntData.CreateCommand();
                OracleTransaction transaction_BOC;

                transaction_BOC = connection_BOCPaymntData.BeginTransaction(IsolationLevel.ReadCommitted);
                command.Transaction = transaction_BOC;
                try
                {
                    string insSeqNo = "INSERT INTO SLIC_NET_LIFE.BOC_IPG_PAYMENTS (ORDER_ID,ORDER_AMOUNT,ORDER_CURRENCY,ORDER_REFERENCE,SESSION_ID,SUCCESS_INDICATOR,ENTRY_DATE) VALUES ('" + orderID + "','" + orderAmount + "','" + order_Currency + "','" + order_reference + "','" + sessionID + "','" + succesIndicator + "',SYSDATE)";


                    command.CommandText = insSeqNo;

                    command.ExecuteNonQuery();

                    transaction_BOC.Commit();
                    BOCPaymentResult = true;

                }
                catch (Exception u)
                {
                    transaction_BOC.Rollback();
                    log logger_SeqNo = new log();
                    logger_SeqNo.write_log("Failed at boc_ipg_payments->insert_rec: " + u.ToString());
                }

                connection_BOCPaymntData.Close();
            }

        }
        catch (Exception u)
        {
            string g = u.ToString();
            log logger = new log();
            logger.write_log("Failed at boc_ipg_payments->insert_rec:" + u.ToString());
        }
        finally
        {

        }

        return BOCPaymentResult;

    }

    public ArrayList get_Order_Details(string OrderIDVal)
    {
        OracleConnection oconn_getOrderData = new OracleConnection(ConfigurationManager.AppSettings["OraLifeDB"]);

        ArrayList arr_OrderData = new ArrayList();

        try
        {
            oconn_getOrderData.Open();
            string getOrderValues = "SELECT SESSION_ID,SUCCESS_INDICATOR FROM SLIC_NET_LIFE.BOC_IPG_PAYMENTS where ORDER_ID = '" + OrderIDVal + "'";

            using (OracleCommand cmd = new OracleCommand(getOrderValues, oconn_getOrderData))
            {
                OracleDataReader cntReader = cmd.ExecuteReader();
                if (cntReader.HasRows)
                {
                    while (cntReader.Read())
                    {

                        arr_OrderData.Add(new ListItem(cntReader.GetString(0)));
                        arr_OrderData.Add(new ListItem(cntReader.GetString(1)));


                    }
                }
                cntReader.Close();
            }

        }
        catch (Exception e)
        {
            arr_OrderData = null;
            log logger = new log();
            logger.write_log("Failed at getOrderValues: " + e.ToString());
        }
        finally
        {
            oconn_getOrderData.Close();
        }

        return arr_OrderData;
    }


    //General
    public bool update_gen_pay_method_in_renewal(string refNo, string BOC_PayMethod)
    {
        OracleConnection oconn_update__gen_pay_method = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
        bool returnValue = false;
        if (oconn_update__gen_pay_method.State != ConnectionState.Open)
        {
            oconn_update__gen_pay_method.Open();
        }
        OracleCommand cmd = oconn_update__gen_pay_method.CreateCommand();
        OracleTransaction trans_update_pay_method = oconn_update__gen_pay_method.BeginTransaction();
        cmd.Transaction = trans_update_pay_method;
        try
        {
            using (cmd)
            {
                string updatePayment = "Update SLIC_NET.RENEWAL_DETAILS Set PAY_METHOD = :BOC_PayMethod where RECEIPT_NO = :refNo";

                cmd.CommandText = updatePayment;

                OracleParameter oPaymentMethod = new OracleParameter();
                oPaymentMethod.DbType = DbType.String;
                oPaymentMethod.Value = BOC_PayMethod;
                oPaymentMethod.ParameterName = "BOC_PayMethod";

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
            logger.write_log("Failed at LifePayment - update_paid_renewal: " + e.ToString());
        }
        finally
        {
            if (oconn_update__gen_pay_method.State == ConnectionState.Open)
            {
                oconn_update__gen_pay_method.Close();
            }
        }

        return returnValue;
    }

    public bool Insert_BOCPaymentDetails_Gen(string orderID, string orderAmount, string order_Currency, string order_reference, string sessionID, string succesIndicator)
    {
        bool BOCPaymentResult = false;

        try
        {

            using (OracleConnection connection_BOCPaymntData = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]))
            {
                connection_BOCPaymntData.Open();

                OracleCommand command = connection_BOCPaymntData.CreateCommand();
                OracleTransaction transaction_BOC;

                transaction_BOC = connection_BOCPaymntData.BeginTransaction(IsolationLevel.ReadCommitted);
                command.Transaction = transaction_BOC;
                try
                {
                    string insSeqNo = "INSERT INTO SLIC_NET.BOC_IPG_PAYMENTS (ORDER_ID,ORDER_AMOUNT,ORDER_CURRENCY,ORDER_REFERENCE,SESSION_ID,SUCCESS_INDICATOR,ENTRY_DATE) VALUES ('" + orderID + "','" + orderAmount + "','" + order_Currency + "','" + order_reference + "','" + sessionID + "','" + succesIndicator + "', SYSDATE)";


                    command.CommandText = insSeqNo;

                    command.ExecuteNonQuery();

                    transaction_BOC.Commit();
                    BOCPaymentResult = true;

                }
                catch (Exception u)
                {
                    transaction_BOC.Rollback();
                    log logger_SeqNo = new log();
                    logger_SeqNo.write_log("Failed at boc_ipg_payments->insert_rec: " + u.ToString());
                }

                connection_BOCPaymntData.Close();
            }

        }
        catch (Exception u)
        {
            string g = u.ToString();
            log logger = new log();
            logger.write_log("Failed at boc_ipg_payments->insert_rec:" + u.ToString());
        }
        finally
        {

        }

        return BOCPaymentResult;

    }

    public ArrayList get_General_Order_Details(string OrderIDVal)
    {
        OracleConnection oconn_getOrderData_Gen = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);

        ArrayList arr_OrderData = new ArrayList();

        try
        {
            oconn_getOrderData_Gen.Open();
            string getOrderValues = "SELECT SESSION_ID,SUCCESS_INDICATOR FROM SLIC_NET.BOC_IPG_PAYMENTS where ORDER_ID = '" + OrderIDVal + "'";

            using (OracleCommand cmd = new OracleCommand(getOrderValues, oconn_getOrderData_Gen))
            {
                OracleDataReader cntReader = cmd.ExecuteReader();
                if (cntReader.HasRows)
                {
                    while (cntReader.Read())
                    {

                        arr_OrderData.Add(new ListItem(cntReader.GetString(0)));
                        arr_OrderData.Add(new ListItem(cntReader.GetString(1)));


                    }
                }
                cntReader.Close();
            }

        }
        catch (Exception e)
        {
            arr_OrderData = null;
            log logger = new log();
            logger.write_log("Failed at getOrderValues: " + e.ToString());
        }
        finally
        {
            oconn_getOrderData_Gen.Close();
        }

        return arr_OrderData;
    }

    public bool update_gen_pay_method_in_proposal_details(string refNo, string BOC_PayMethod)
    {
        OracleConnection oconn_update__gen_pay_method_prop_details = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
        bool returnValue = false;
        if (oconn_update__gen_pay_method_prop_details.State != ConnectionState.Open)
        {
            oconn_update__gen_pay_method_prop_details.Open();
        }
        OracleCommand cmd = oconn_update__gen_pay_method_prop_details.CreateCommand();
        OracleTransaction trans_update_pay_method_gen_prop_details = oconn_update__gen_pay_method_prop_details.BeginTransaction();
        cmd.Transaction = trans_update_pay_method_gen_prop_details;
        try
        {
            using (cmd)
            {
                string updatePayment = "Update SLIC_NET.PROPOSAL_DETAILS Set PAY_METHOD = :BOC_PayMethod where REF_NO = :refNo";

                cmd.CommandText = updatePayment;

                OracleParameter o_GEN_PaymentMethod = new OracleParameter();
                o_GEN_PaymentMethod.DbType = DbType.String;
                o_GEN_PaymentMethod.Value = BOC_PayMethod;
                o_GEN_PaymentMethod.ParameterName = "BOC_PayMethod";

                OracleParameter o_GEN_RefNo = new OracleParameter();
                o_GEN_RefNo.DbType = DbType.String;
                o_GEN_RefNo.Value = refNo;
                o_GEN_RefNo.ParameterName = "refNo";

                cmd.Parameters.Add(o_GEN_PaymentMethod);
                cmd.Parameters.Add(o_GEN_RefNo);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();


                trans_update_pay_method_gen_prop_details.Commit();
                returnValue = true;

            }

        }
        catch (Exception e)
        {
            trans_update_pay_method_gen_prop_details.Rollback();
            log logger = new log();
            logger.write_log("Failed at GeneralPayment - update_paid_method_in_proposal_details: " + e.ToString());
        }
        finally
        {
            if (oconn_update__gen_pay_method_prop_details.State == ConnectionState.Open)
            {
                oconn_update__gen_pay_method_prop_details.Close();
            }
        }

        return returnValue;
    }
}