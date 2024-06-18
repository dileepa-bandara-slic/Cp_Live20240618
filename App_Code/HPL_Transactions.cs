using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;

    public class HPL_Transactions
    {
        bool Sucess_State;
        int Error_number;
        string _Error_Message;
        string _KeyOut;

        /// <param name="Sucess_State"></param>
        /// Return true if Query Execuit Else false
        public bool Trans_Sucess_State
        {
            get { return this.Sucess_State; }
            set { this.Sucess_State = value; }
        }

        /// <param name="Error_Code"></param>
        /// Return Orale Error Code
        public int Error_Code
        {
            get { return this.Error_number; }
            set { this.Error_number = value; }
        }

        /// <param name="Error_Code"></param>
        /// Return Orale Error_Message
        public string Error_Message
        {
            get { return this._Error_Message; }
            set { this._Error_Message = value; }
        }

        public string KeyOut
        {
            get { return this._KeyOut; }
            set { this._KeyOut = value; }
        }

        public string BuyProduct(HPL_Proposal hPL_Proposal)
        {
            OracleConnectionManager oracleConnectionManager = new OracleConnectionManager();

            OracleConnection con = new OracleConnection();

            con = oracleConnectionManager.GetConnection();
            OracleTransaction transaction = null;
            con.Open();

            string result = string.Empty;
            try
            {
                transaction = con.BeginTransaction();

                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = string.Format("{0}", ConfigurationManager.AppSettings["exec_proposal_entry"]);

                cmd.Parameters.Add("PIN_POL_TYPE", OracleType.VarChar).Value = hPL_Proposal.polTyp;
                cmd.Parameters.Add("PIN_TITLE", OracleType.VarChar).Value = hPL_Proposal.title;
                cmd.Parameters.Add("PIN_FULL_NAME", OracleType.VarChar).Value = hPL_Proposal.fullNam;
                cmd.Parameters.Add("PIN_ADDRESS1", OracleType.VarChar).Value = hPL_Proposal.adrs1;
                cmd.Parameters.Add("PIN_ADDRESS2", OracleType.VarChar).Value = hPL_Proposal.adrs2;
                cmd.Parameters.Add("PIN_ADDRESS3", OracleType.VarChar).Value = hPL_Proposal.adrs3;
                cmd.Parameters.Add("PIN_ADDRESS4", OracleType.VarChar).Value = hPL_Proposal.adrs4;
                cmd.Parameters.Add("PIN_MOBILE_NUMBER", OracleType.VarChar).Value = hPL_Proposal.mobNo;
                cmd.Parameters.Add("PIN_HOME_NUMBER", OracleType.VarChar).Value = hPL_Proposal.homeNo;
                cmd.Parameters.Add("PIN_OFFICE_NUMBER", OracleType.VarChar).Value = hPL_Proposal.ofcNo;
                cmd.Parameters.Add("PIN_EMAIL", OracleType.VarChar).Value = hPL_Proposal.email;
                cmd.Parameters.Add("PIN_NIC", OracleType.VarChar).Value = hPL_Proposal.nic;
                cmd.Parameters.Add("PIN_LOC_ADRS1", OracleType.VarChar).Value = hPL_Proposal.lcAdrs1;
                cmd.Parameters.Add("PIN_LOC_ADRS2", OracleType.VarChar).Value = hPL_Proposal.lcAdrs2;
                cmd.Parameters.Add("PIN_LOC_ADRS3", OracleType.VarChar).Value = hPL_Proposal.lcAdrs3;
                cmd.Parameters.Add("PIN_LOC_ADRS4", OracleType.VarChar).Value = hPL_Proposal.lcAdrs4;
                cmd.Parameters.Add("PIN_ASSIGNEE", OracleType.VarChar).Value = hPL_Proposal.assignee;
                cmd.Parameters.Add("PIN_SUSTAINED", OracleType.VarChar).Value = hPL_Proposal.sustained;
                cmd.Parameters.Add("PIN_DECLINNED", OracleType.VarChar).Value = hPL_Proposal.declinned;
                cmd.Parameters.Add("PIN_DAMAGED_BEFORE", OracleType.VarChar).Value = hPL_Proposal.dmgBefore;
                cmd.Parameters.Add("PIN_REJCTED_BEFORE", OracleType.VarChar).Value = hPL_Proposal.rejBefore;
                cmd.Parameters.Add("PIN_REJCT_REASON", OracleType.VarChar).Value = hPL_Proposal.rejResn;
                cmd.Parameters.Add("PIN_PLAN", OracleType.VarChar).Value = hPL_Proposal.plan;
                cmd.Parameters.Add("PIN_STATUS", OracleType.VarChar).Value = hPL_Proposal.status;
                cmd.Parameters.Add("PIN_USERNAME", OracleType.VarChar).Value = hPL_Proposal.username;
                cmd.Parameters.Add("PIN_PRODUCT_ID", OracleType.VarChar).Value = hPL_Proposal.prodId;
                cmd.Parameters.Add("PIN_PROFESSION", OracleType.VarChar).Value = hPL_Proposal.prof;
                cmd.Parameters.Add("PIN_PAY_METHOD", OracleType.VarChar).Value = hPL_Proposal.payMethod;

                if (!String.IsNullOrEmpty(hPL_Proposal.agtcode) && (int.Parse(hPL_Proposal.agtcode) > 0))
                    cmd.Parameters.Add("PIN_AGT_CODE", OracleType.Number).Value = hPL_Proposal.agtcode;

                else
                    cmd.Parameters.Add("PIN_AGT_CODE", OracleType.Number).Value = 0;

                //THIS PARAMETER MAY BE USED TO RETURN RESULT OF PROCEDURE CALL
                cmd.Parameters.Add("RTN_SUCCESS", OracleType.VarChar, 35);
                cmd.Parameters["RTN_SUCCESS"].Direction = ParameterDirection.Output;

                cmd.Transaction = transaction;
                int execution = cmd.ExecuteNonQuery();

                if (execution > 0)
                {
                    //RETURN VALUE
                    if (cmd.Parameters["RTN_SUCCESS"].Value.ToString() != "#")
                    {
                        result = cmd.Parameters["RTN_SUCCESS"].Value.ToString();
                    }
                    else
                    {
                        result = string.Empty;
                    }
                }

                else
                    result = string.Empty;

                transaction.Commit();
            }

            catch (OracleException ex)
            {
                result = string.Empty;
                Error_Code = ex.ErrorCode;
                Error_Message = ex.Message;

                HPL_OrclLog orclLog = new HPL_OrclLog();
                orclLog.WriteLog("Error In Create Document Sub-Category User :: " + hPL_Proposal.username + " :: " + ex.ToString());
                transaction.Rollback();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return result;
        }

        

        public List<HPL_CustomerPolicy> GetUserProfile(string orcl_executor)
        {
            OracleConnectionManager orcl_con = new OracleConnectionManager();
            List<HPL_CustomerPolicy> us_info = new List<HPL_CustomerPolicy>();
            using (OracleConnection connection = orcl_con.GetConnection())
            {
                using (OracleCommand cmd = new OracleCommand(orcl_executor, connection))
                {
                    try
                    {
                        connection.Open();

                        using (OracleDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                if (sdr.HasRows)
                                {
                                    us_info.Add(new HPL_CustomerPolicy());

                                    us_info[us_info.Count - 1].cus_Name = sdr["CUSNAME"].ToString();
                                    us_info[us_info.Count - 1].commencement_Date = sdr["COM_DATE"].ToString();
                                    us_info[us_info.Count - 1].policy_no= sdr["POLICY_NUMBER"].ToString();
                                    us_info[us_info.Count - 1].Sum_Assured = sdr["SUM_ASSURD"].ToString();
                                    us_info[us_info.Count - 1].Premium = sdr["ANU_PREMIUM"].ToString();
                                    break;
                                }
                            }
                            Trans_Sucess_State = true;
                            sdr.Dispose();
                        }
                    }
                    catch (OracleException ex)
                    {
                        //throw new Exception(ex.Message);
                        Trans_Sucess_State = false;
                        Error_Code = ex.ErrorCode;
                        Error_Message = ex.Message;

                        HPL_OrclLog orclLog = new HPL_OrclLog();
                        orclLog.WriteLog("Error :: Getting Policy Information." + ex.ToString());
                    }
                    finally
                    {
                        // always call Close when done reading.
                        connection.Close();
                        connection.Dispose();
                    }
                }
                return us_info;
            }
        }


        public bool PurchaseProductUpdate(string  refno, string pay_FinalCode)
        {
            OracleConnectionManager orcl_con = new OracleConnectionManager();
            OracleConnection con = new OracleConnection();

            con = orcl_con.GetConnection();
            OracleTransaction transaction = null;
            con.Open();

            //bool result = false;
            try
            {
                transaction = con.BeginTransaction();

                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ConfigurationManager.AppSettings["exec_purchase_fynalization"];

                cmd.Parameters.Add(new OracleParameter("PIN_POL_REFNO", OracleType.VarChar)).Value = refno;
                cmd.Parameters.Add(new OracleParameter("PIN_RSP_CODE", OracleType.Number)).Value = int.Parse(pay_FinalCode);

                cmd.Transaction = transaction;
                int execution = cmd.ExecuteNonQuery();

                if (execution > 0) Trans_Sucess_State = true;

                else Trans_Sucess_State = false;

                transaction.Commit();
            }

            catch (OracleException ex)
            {
                Trans_Sucess_State = false;
                Error_Code = ex.ErrorCode;
                Error_Message = ex.Message;

                HPL_OrclLog orclLog = new HPL_OrclLog();
                orclLog.WriteLog("Error :: Policy Payment Confirmation." + ex.ToString());
                transaction.Rollback();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return Trans_Sucess_State;
        }



        public List<HPL_PaymentFynalization> ReturnPurchasePolicyInfo(string orcl_executor)
        {
            OracleConnectionManager orcl_con = new OracleConnectionManager();
            List<HPL_PaymentFynalization> usp_info = new List<HPL_PaymentFynalization>();
            HPL_SQL hPL_SQL = new HPL_SQL();

            using (OracleConnection connection = orcl_con.GetConnection())
            {
                using (OracleCommand cmd = new OracleCommand(orcl_executor, connection))
                {
                    try
                    {
                        connection.Open();

                        using (OracleDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                if (sdr.HasRows)
                                {
                                    usp_info.Add(new HPL_PaymentFynalization());
                                    usp_info[usp_info.Count - 1].HPLPolicy = sdr["POLICYNO"].ToString();
                                    usp_info[usp_info.Count - 1].HPLRefNo = sdr["REFNO"].ToString();
                                    usp_info[usp_info.Count - 1].HPLCus_Name = sdr["CNAME"].ToString();
                                    usp_info[usp_info.Count - 1].HPLSumAssured = sdr["SUM_ASSURD"].ToString();
                                    usp_info[usp_info.Count - 1].HPLPremium = sdr["ANU_PREMIUM"].ToString();
                                    usp_info[usp_info.Count - 1].HPLCoverPeriod = sdr["COV_PERIOD"].ToString();
                                    usp_info[usp_info.Count - 1].HPLDateOfPayment = sdr["PAID_DATE"].ToString();
                                    usp_info[usp_info.Count - 1].HPLEmail = sdr["EMAIL"].ToString();
                                    usp_info[usp_info.Count - 1].HPLContactNo = sdr["MOBILE_NUMBER"].ToString();

                                    usp_info[usp_info.Count - 1].HPL_ADDRESS1 = sdr["ADDRESS1"].ToString();
                                    usp_info[usp_info.Count - 1].HPL_ADDRESS2 = sdr["ADDRESS2"].ToString();
                                    usp_info[usp_info.Count - 1].HPL_ADDRESS3 = sdr["ADDRESS3"].ToString();
                                    usp_info[usp_info.Count - 1].HPL_ADDRESS4 = sdr["ADDRESS4"].ToString();

                                    usp_info[usp_info.Count - 1].HPL_RL_ADDRESS1 = sdr["RISK_ADRS1"].ToString();
                                    usp_info[usp_info.Count - 1].HPL_RL_ADDRESS2 = sdr["RISK_ADRS2"].ToString();
                                    usp_info[usp_info.Count - 1].HPL_RL_ADDRESS3 = sdr["RISK_ADRS3"].ToString();
                                    usp_info[usp_info.Count - 1].HPL_RL_ADDRESS4 = sdr["RISK_ADRS4"].ToString();

                                    usp_info[usp_info.Count - 1].HPL_PLAN = sdr["PLAN_TYPE"].ToString();

                                    usp_info[usp_info.Count - 1].HPL_LLBPFF = sdr["LL_BPFF"].ToString();
                                    usp_info[usp_info.Count - 1].HPL_LLCEPE = sdr["LL_CEPE"].ToString();
                                    usp_info[usp_info.Count - 1].HPL_LLSRD = sdr["LL_SRD"].ToString();
                                    usp_info[usp_info.Count - 1].HPL_LLPRB= sdr["LL_SPRB"].ToString();
                                    usp_info[usp_info.Count - 1].HPL_LLSB= sdr["LL_SB"].ToString();
                                    usp_info[usp_info.Count - 1].HPL_LLSD= sdr["LLED"].ToString();
                                    usp_info[usp_info.Count - 1].HPL_TOT_SUM_INS = sdr["TOT_SUM_INS"].ToString();

                                    usp_info[usp_info.Count - 1].HPL_AGTCODE = int.Parse(sdr["AGTCODE"].ToString());
                                    break;
                                }
                            }

                            Trans_Sucess_State = true;
                            sdr.Dispose();
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new Exception(ex.Message);
                        Trans_Sucess_State = false;
                        Error_Code = ex.ErrorCode;
                        Error_Message = ex.Message;

                        HPL_OrclLog orclLog = new HPL_OrclLog();
                        orclLog.WriteLog("Error :: Reading Record. Please Contact SLIC."+ ex.ToString());
                    }
                    finally
                    {
                         //always call Close when done reading.
                        connection.Close();
                        connection.Dispose();
                    }
                }
                return usp_info;
            }
        }


        public int Get_AgencyConfirmation(int agency)
        {
            HPL_SQL hpl_SQL = new HPL_SQL();
            int count = 0;
            OracleConnectionManager orcl_con = new OracleConnectionManager();

            using (OracleConnection connection = orcl_con.GetConnection())
            {
                try
                {
                    OracleCommand command = new OracleCommand();
                    command.CommandText = hpl_SQL.GetAgencyConfirmation(agency);
                    command.Connection = connection;
                    connection.Open();
                    count = Convert.ToInt32(command.ExecuteScalar());
                    Trans_Sucess_State = true;
                }

                catch (OracleException ex)
                {
                    count = 0;
                    Trans_Sucess_State = false;

                    Error_Code = ex.ErrorCode;
                    Error_Message = ex.Message;

                    HPL_OrclLog log_tr = new HPL_OrclLog();
                    log_tr.WriteLog("Can't Read  Agency Data" + " ::> " + ex.ToString());
                }
                finally
                {
                    // always call Close when done reading.        
                    connection.Close();
                    connection.Dispose();
                }
            }
            return count;
        }
    }
