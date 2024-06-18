using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for LifeProposal
/// </summary>
public class LifeProposal
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OraLifeDB"]);

    public int prop_table { get; private set; }
    public string name { get; private set; }
    public string nic { get; private set; }
    public double sum_Assured { get; private set; }
    public string O_Mesg { get; private set; }     
    public double premium { get; private set; }    

	public LifeProposal()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public LifeProposal(string propNo)
    {
        O_Mesg = "No record found for this Proposal Number";
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string sql = "select A.PRTBL, A.PRSUM, nvl(B.NICNO,' ') NICNO, CONCAT(B.STATUS, B.FULLNAME), A.TOT_PRM " +
                          " from lund.promast A, lund.propersonal B " +
                          " where A.prop = b.propno and a.prop = :propNum and B.prpertype = 1";

            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {
                OracleParameter opRefNo = new OracleParameter();
                opRefNo.Value = propNo;
                opRefNo.ParameterName = "propNum";

                cmd.Parameters.Add(opRefNo);

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    prop_table = reader.GetInt32(0);
                    sum_Assured = reader.GetDouble(1);
                    nic = reader.GetString(2);
                    name = reader.GetString(3);
                    premium = reader.GetDouble(4);
                    O_Mesg = "success";
                }
                reader.Close();
                cmd.Parameters.Clear();
            }
        }
        catch (Exception ex)
        {
            log logger = new log();
            logger.write_log("Failed at LifeProposal constructor: " + ex.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
    }

    public bool checkPendingProposal(long proposal)
    {
        bool retVal = false;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            //string getPolType = "select * from lund.promast where prop = :propNum and polno = 0";
            string getPolType = "select * from lnb.lnbfle1 where lprono = :propNum";

            using (OracleCommand cmd = new OracleCommand(getPolType, oconn))
            {
                cmd.Parameters.AddWithValue("propNum", proposal);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    retVal = true; 
                }
            }

        }
        catch (Exception e)
        {
            O_Mesg = "Error occurred while retrieving information";
            log logger = new log();
            logger.write_log("Failed at LifeProposal - checkPendingProposal: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return retVal;
    }

    public string getPolicyType(int table)
    {
        string policyType = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolType = "select TDNAM from lund.tabnam where TDTABL = :tble";

            using (OracleCommand cmd = new OracleCommand(getPolType, oconn))
            {
                cmd.Parameters.AddWithValue("tble", table);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            policyType = reader.GetString(0);
                        }

                    }
                    reader.Close();
                }
            }

            if(table == 59)
            {
                policyType = "Online Early Cash";
            }

        }
        catch (Exception e)
        {
            policyType = "Error";
            log logger = new log();
            logger.write_log("Failed at LifeProposal - getPolicyType: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return policyType;
    }

    public string getPolicyFeeRecpt(string receiptNo)
    {
        string policyFeeRectNo = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolFeeRectNo = "select POLICY_FEE_RECT_NO from SLIC_NET_LIFE.ONLINE_PROP_RECPT_NOS " +
                                     " where MAIN_RECT_NO = :rect_no_v";

            using (OracleCommand cmd = new OracleCommand(getPolFeeRectNo, oconn))
            {
                cmd.Parameters.AddWithValue("rect_no_v", receiptNo);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            policyFeeRectNo = reader.GetString(0);
                        }

                    }
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            policyFeeRectNo = "Error";
            log logger = new log();
            logger.write_log("Failed at LifeProposal - getPolicyFeeRecpt: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        log logger2 = new log();
        logger2.write_log("Policy Fee Rect No " + policyFeeRectNo);
        return policyFeeRectNo;
    }

    public string checkRecptType(string receiptNo)
    {
        string RectType = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getMainRectNo = "select * from SLIC_NET_LIFE.ONLINE_PROP_RECPT_NOS " +
                                     " where MAIN_RECT_NO = :rect_no_v1";

            string getPolFeeRectNo = "select * from SLIC_NET_LIFE.ONLINE_PROP_RECPT_NOS " +
                                     " where policy_fee_rect_no = :rect_no_v2";


            using (OracleCommand cmd = new OracleCommand(getMainRectNo, oconn))
            {
                cmd.Parameters.AddWithValue("rect_no_v1", receiptNo);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            RectType = "M";
                        }

                    }
                    reader.Close();
                }
            }

            if (RectType.Equals(""))
            {
                using (OracleCommand cmd = new OracleCommand(getPolFeeRectNo, oconn))
                {
                    cmd.Parameters.AddWithValue("rect_no_v2", receiptNo);

                    OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                RectType = "P";
                            }

                        }
                        reader.Close();
                    }
                }
            }

        }
        catch (Exception e)
        {
            //policyFeeRectNo = "Error";
            log logger = new log();
            logger.write_log("Failed at LifeProposal - checkRecptType: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        log logger2 = new log();
        logger2.write_log("Policy Fee Rect Type " + RectType);
        return RectType;
    }

    public bool isOnlineNB(long proposal) //2022.06.02
    {
        bool onlineNB = false;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            //string checkOnlineNB = "select * from SLI_LUND.EARLYCASH_MASTER_TBL where PROP_NO = :propNumIn";
            string checkOnlineNB = "select * from slic_agent.onlinelifeproposal where proposal_no = :propNumIn and TABLE_NO = 59";

            using (OracleCommand cmd = new OracleCommand(checkOnlineNB, oconn))
            {
                cmd.Parameters.AddWithValue("propNumIn", proposal);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    onlineNB = true;
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            onlineNB = false;
            log logger = new log();
            logger.write_log("Failed at LifeProposal - isOnlineNB: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return onlineNB;
    }

    public string getRefNo(string proposal)
    {
        string refNumbr = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolFeeRectNo = "select proposal_id from slic_agent.onlinelifeproposal where proposal_no = :propNumbr";

            using (OracleCommand cmd = new OracleCommand(getPolFeeRectNo, oconn))
            {
                cmd.Parameters.AddWithValue("propNumbr", long.Parse(proposal));

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            refNumbr = reader.GetInt64(0).ToString();
                        }

                    }
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            refNumbr = "Error";
            log logger = new log();
            logger.write_log("Failed at LifeProposal - getRefNo: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        log logger2 = new log();
        logger2.write_log("Proposal Ref No " + refNumbr);
        return refNumbr;
    }

    public string getName(string refNo)
    {
        string name = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolFeeRectNo = "select concat(concat(title, initials), lname) from sli_lund.earlycash_master_tbl where ref_no = :refNumbr";

            using (OracleCommand cmd = new OracleCommand(getPolFeeRectNo, oconn))
            {
                cmd.Parameters.AddWithValue("refNumbr", refNo);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            name = reader.GetString(0);
                        }

                    }
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            name = "Error";
            log logger = new log();
            logger.write_log("Failed at LifeProposal - getName: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        log logger2 = new log();
        logger2.write_log("Proposal name " + name);
        return name;
    }

    public bool SendEasyCashSMS(string receiptNo)
    {
        bool result = false;
        string mobileNum = "", smsText = "";

        LifePayment paymnt = new LifePayment(receiptNo);

        string proposlNo = paymnt.polNum.ToString();
        string refNum = this.getRefNo(proposlNo);
        double amount = paymnt.amount;

        ULCustomer custmr = new ULCustomer();
        mobileNum = custmr.getMobileNumbr(paymnt.username);

        smsText = "Dear customer, Thank you for your payment of Rs. " + amount.ToString("N2") + " to " + refNum + " on " + DateTime.Today.ToString("dd/mm/yyyy") + 
                            ". You will receive the digital policy document shortly via e-mail.- SLIC";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string mobileNoFormatted = "";
            if (mobileNum.Substring(0, 1) == "0")
            {
                mobileNoFormatted = "94" + mobileNum.Substring(1, mobileNum.Length - 1);
            }
            else
            {
                mobileNoFormatted = "94" + mobileNum;
            }

            log logger3 = new log();
            logger3.write_log("mobileNoFormatted" + mobileNoFormatted);

            if (mobileNoFormatted.Length == 11)
            {

                string sendSms = "insert into SMS.SMS_GATEWAY (APPLICATION_ID,JOB_CATEGORY,SMS_TYPE,MOBILE_NUMBER," +
                                           " TEXT_MESSAGE,SHORT_CODE) VALUES ('ONLINE_EARLY_CASH','CAT151','I', :mobile, :txt, 'SLIC%20LIFE') ";

                using (OracleCommand com2 = new OracleCommand(sendSms, oconn))
                {
                    OracleParameter oMobile = new OracleParameter();
                    oMobile.DbType = DbType.String;
                    oMobile.Value = mobileNoFormatted;
                    oMobile.ParameterName = "mobile";

                    OracleParameter oText = new OracleParameter();
                    oText.DbType = DbType.String;
                    oText.Value = smsText;
                    oText.ParameterName = "txt";

                    com2.Parameters.Add(oMobile);
                    com2.Parameters.Add(oText);

                    //cmd.ExecuteNonQuery();
                    //cmd.Parameters.Clear();

                    int k = com2.ExecuteNonQuery();
                    com2.Parameters.Clear();

                    if (k > 0)
                    {
                        result = true;
                    }
                }
            }
        }
        catch (Exception e)
        {
            result = false;
            log logger4 = new log();
            logger4.write_log("Exception: " + e.Message);
        }
        finally
        {
            oconn.Close();
        }

        log logger2 = new log();
        logger2.write_log("SMS sent " + result.ToString());
        

        return result;
    }

public double getPolicyFee()
    {
        double fee = 0;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolFee = "select POLICY_FEE from slifclm.policy_fee " + 
                                    " where EFFECT_DATE_FROM <= sysdate and EFFECT_DATE_TO >= sysdate  " + 
                                    " and EFFECT_DATE_FROM in " + 
                                    " (select max(EFFECT_DATE_FROM) from slifclm.policy_fee where EFFECT_DATE_FROM <= sysdate and EFFECT_DATE_TO >= sysdate )";

            using (OracleCommand cmd = new OracleCommand(getPolFee, oconn))          
            {                

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            fee = reader.GetDouble(0);
                        }

                    }
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            name = "Error";
            log logger = new log();
            logger.write_log("Failed at LifeProposal - getPolicyFee: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        
        return fee;
    }

    public string isIdMatch(long propNo)
    {
        string checkNIC = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string checkIdMatch = "select nicno from lund.propersonal where propno = :propNum";

            using (OracleCommand cmd = new OracleCommand(checkIdMatch, oconn))
            {
                cmd.Parameters.AddWithValue("propNum", propNo);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            checkNIC = reader.GetString(0);
                        }

                    }
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            checkNIC = "Error";
            log logger = new log();
            logger.write_log("Failed at LifeProposal - isIdMatch: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        log logger2 = new log();
        logger2.write_log("NIC No " + checkNIC);
        return checkNIC;
    }

    public bool checkAgent(string loginNIC)
    {
        bool agentID = false;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string checkIdMatch = "select idnum from agent.agent where stid = 'Ag' and idnum = :loginNIC";

            using (OracleCommand cmd = new OracleCommand(checkIdMatch, oconn))
            {
                cmd.Parameters.AddWithValue("loginNIC", loginNIC);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    agentID = true;
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            agentID = false;
            log logger = new log();
            logger.write_log("Failed at LifeProposal - checkAgent: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return agentID;
    }

    public string checkAgentCode(long propNum, string loginNIC)
    {
        string agentCodeId = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string checkIdMatch = "SELECT a.idnum FROM agent.agent a "
								+ "JOIN lund.promast p ON a.agency = p.pragt "
								+ "JOIN lund.propersonal pp ON p.prop = pp.propno "
								+ "WHERE a.stid IN ('Ag', 'Or') "
								+ "AND p.prop = :proNo "
								+ "AND a.idnum = :logNIC";

            using (OracleCommand cmd = new OracleCommand(checkIdMatch, oconn))
            {
                cmd.Parameters.AddWithValue("logNIC", loginNIC);
                cmd.Parameters.AddWithValue("proNo", propNum);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            agentCodeId = reader.GetString(0);
                        }

                    }
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            agentCodeId = "Error";
			//agentCodeId = e.StackTrace;
            log logger = new log();
            logger.write_log("Failed at LifeProposal - getAgentCode: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        log logger2 = new log();
        logger2.write_log("NIC No " + agentCodeId);
        return agentCodeId;
    }
}