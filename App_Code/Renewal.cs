using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using System.Data.Odbc;
using System.Data;
using System.Globalization;

/// <summary>
/// Summary description for Renewal
/// </summary>
public class Renewal
{
    //OdbcConnection db2conn = new OdbcConnection(ConfigurationManager.AppSettings["DB2"]);
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);

    public string receiptNo { get; private set; }
    public string polNum { get; private set; }
    public string dept { get; private set; }
    public string polType { get; private set; }
    public double amount { get; private set; }
    public string entryDate { get; private set; }
    public string username { get; private set; }
    public string status { get; private set; }
    public string pgRetCode { get; private set; }
    public string pgResCode { get; private set; }
    public string nic { get; private set; }
    public string startDate { get; private set; }
    public string endDate { get; private set; }
    public string custName { get; private set; }
    public double sumAssurd { get; private set; }
    public string polTypName { get; private set; }
    public string address { get; private set; }
    public string vehiNum { get; private set; }
    
	public Renewal()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Renewal(string ref_no)
    {
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string sql = "SELECT RECEIPT_NO, POL_NUM, DEPT, POL_TYPE, PREMIUM, to_char(ENTRY_DATE, 'yyyy/mm/dd') AS ENTRY_DATE, USERNAME, STATUS, nvl(PG_RET_CODE, 0) PG_RET_CODE," +
                         " nvl(PG_RSN_CODE, 0) PG_RSN_CODE, to_char(START_DATE, 'yyyy/mm/dd') START_DATE, to_char(END_DATE, 'yyyy/mm/dd') AS END_DATE, CUST_NAME, SUM_ASSURD, PTSNA," +
                         " ADDRESS, VEHI_NUM FROM SLIC_NET.RENEWAL_DETAILS R, GENPAY.POLTYP P WHERE RECEIPT_NO = :refno AND P.PTDEP = R.DEPT AND P.PTTYP =R.POL_TYPE";

            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {
                OracleParameter opRefNo = new OracleParameter();
                opRefNo.Value = ref_no;
                opRefNo.ParameterName = "refno";

                cmd.Parameters.Add(opRefNo);

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    receiptNo = reader[0].ToString().Trim();
                    polNum = reader[1].ToString().Trim();
                    dept = reader[2].ToString().Trim();
                    polType = reader[3].ToString().Trim();
                    amount = Convert.ToDouble(reader[4].ToString().Trim());
                    entryDate = reader[5].ToString().Trim();
                    username = reader[6].ToString().Trim();
                    status = reader[7].ToString().Trim();
                    pgRetCode = reader[8].ToString().Trim();
                    pgResCode = reader[9].ToString().Trim();
                    startDate = reader[10].ToString().Trim();
                    endDate = reader[11].ToString().Trim();
                    custName = reader[12].ToString().Trim();
                    sumAssurd = Convert.ToDouble(reader[13].ToString().Trim());
                    polTypName = reader[14].ToString().Trim();
                    address = reader[15].ToString().Trim();
                    vehiNum = reader[16].ToString().Trim();
                }

                reader.Close();
            }
        }
        catch (Exception ex)
        {
            log logger = new log();
            logger.write_log("Failed at Renewal constructor: " + ex.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
    }

    public string generate_renwReceiptNo(string dpt, int year, string type, bool debitSettle = false)
    {
        string result = "";
        string id = "";
        //string n = "";
        try
        {
            oconn.Open();
            string sql = "SELECT * FROM SLIC_NET.RENW_REF_NUMBERS WHERE DEPARTMENT = :dept AND YEAR = :yr AND TYP = :polType ";
            int rows = 0;
            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {               

                OracleParameter oDept = new OracleParameter();
                oDept.Value = dpt;
                oDept.ParameterName = "dept";

                OracleParameter oYear = new OracleParameter();
                oYear.DbType = DbType.Int32;
                oYear.Value = year;
                oYear.ParameterName = "yr";

                OracleParameter oPolType = new OracleParameter();
                oPolType.Value = type;
                oPolType.ParameterName = "polType";

                cmd.Parameters.Add(oDept);
                cmd.Parameters.Add(oYear);
                cmd.Parameters.Add(oPolType);

                OracleDataReader refNoReader = cmd.ExecuteReader();

                while (refNoReader.Read())
                {
                    rows++;
                }
                refNoReader.Close();
                cmd.Parameters.Clear();
            }

            if (rows > 0)
            {
                result = update_renwSeq(sql, dpt, year, type).ToString();
            }
            else
            {
                result = insert_newSeq(dpt, year, type).ToString();
            }

            string seq = result.ToString().PadLeft(5, '0');

            if (!debitSettle)
            {
                id = dpt + "/" + type + "/R/" + year.ToString() + "/" + seq;
            }
            else
            {
                id = dpt + "/" + type + "/D/" + year.ToString() + "/" + seq;
            }
            result = id;
        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at Renewal - generate_renwReceiptNo " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return result;
    }


    private int update_renwSeq(string sql, string dpt, int year, string type)
    {
        int seqNo = 0;
        try
        {
            seqNo = get_max_refNo(sql, dpt, year, type);
            string sql2 = "UPDATE SLIC_NET.RENW_REF_NUMBERS SET REFNO = :seqNo WHERE DEPARTMENT = :dept AND YEAR = :year AND TYP = :polType";
            // string sql2 = "UPDATE SLIC_NET.PROP_REF_NUMBERS SET REFNO = '" + seqNo + "' WHERE DEPARTMENT = '" + dpt + "' AND YEAR = " + year + " AND TYP = '" + type + "'";
            oconn.Open();

            using (OracleCommand cmd = new OracleCommand(sql2, oconn))
            {
                //cmd.Parameters.AddWithValue("seqNo", seqNo);
                //cmd.Parameters.AddWithValue("dept", dpt);
                //cmd.Parameters.AddWithValue("year", year);
                // cmd.Parameters.AddWithValue("polType", type);                             

                OracleParameter oSeqNo = new OracleParameter();
                oSeqNo.DbType = DbType.Int32;
                oSeqNo.Value = seqNo;
                oSeqNo.ParameterName = "seqNo";

                OracleParameter oDpt = new OracleParameter();
                oDpt.Value = dpt;
                oDpt.ParameterName = "dept";

                OracleParameter oYear = new OracleParameter();
                oYear.DbType = DbType.Int32;
                oYear.Value = year;
                oYear.ParameterName = "year";

                OracleParameter oPolTyp = new OracleParameter();
                oPolTyp.Value = type;
                oPolTyp.ParameterName = "polType";

                cmd.Parameters.Add(oSeqNo);
                cmd.Parameters.Add(oDpt);
                cmd.Parameters.Add(oYear);
                cmd.Parameters.Add(oPolTyp);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

            }
        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at Renewal - update_renwSeq " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return seqNo;
    }

    private int get_max_refNo(string sql, string dpt, int year, string type)
    {
        int result = 0;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {
                OracleParameter oDept = new OracleParameter();
                oDept.Value = dpt;
                oDept.ParameterName = "dept";

                OracleParameter oYear = new OracleParameter();
                oYear.DbType = DbType.Int32;
                oYear.Value = year;
                oYear.ParameterName = "yr";

                OracleParameter oPolType = new OracleParameter();
                oPolType.Value = type;
                oPolType.ParameterName = "polType";

                cmd.Parameters.Add(oDept);
                cmd.Parameters.Add(oYear);
                cmd.Parameters.Add(oPolType);

                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = Convert.ToInt32(reader["REFNO"]);
                    break;
                }
            }
            result++;
        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at Renewal - get_max_propNo " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return result;
    }

    private int insert_newSeq(string dpt, int year, string type)
    {
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string sql2 = "INSERT INTO SLIC_NET.RENW_REF_NUMBERS VALUES (:dept, :year , :polType," + 1 + ")";

            using (OracleCommand cmd = new OracleCommand(sql2, oconn))
            {
                //cmd.Parameters.AddWithValue("dept", dpt);
                //cmd.Parameters.AddWithValue("year", year);
                //cmd.Parameters.AddWithValue("polType", type);

                OracleParameter oDept = new OracleParameter();
                oDept.Value = dpt;
                oDept.ParameterName = "dept";

                OracleParameter oYear = new OracleParameter();
                oYear.Value = year;
                oYear.ParameterName = "year";

                OracleParameter oPolType = new OracleParameter();
                oPolType.Value = type;
                oPolType.ParameterName = "polType";

                cmd.Parameters.Add(oDept);
                cmd.Parameters.Add(oYear);
                cmd.Parameters.Add(oPolType);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

            }

        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at Renewal - insert_newSeq " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return 1;
    }

    public bool insert_renewal(string polNum, string dept, string polType, double amount, string userName, string status, string nic,
                                string startDate, string endDate, string receiptNo, string cusName, double sumAssurd, string address, string vehiNum)
    {
        bool returnValue = false;
        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        OracleCommand cmd = oconn.CreateCommand();
        OracleTransaction trans = oconn.BeginTransaction();
        cmd.Transaction = trans;
        try
        {
            using (cmd)
            {
                string instRenwDetails = "Insert into SLIC_NET.RENEWAL_DETAILS(POL_NUM, DEPT, POL_TYPE, PREMIUM, ENTRY_DATE, USERNAME, STATUS, START_DATE, END_DATE, RECEIPT_NO, CUST_NAME, SUM_ASSURD, ADDRESS, VEHI_NUM)" +
                                                        " VALUES(:polNum, :dept, :polType, :amount, sysdate, :username, :status, :startDate, :endDate, :recptNo, :cusName, :sumAssurd, :address, :vehiNo)";
                                                       // " VALUES(:polNum, :dept, :polType, :amount, sysdate, :usernam, 'P', '123', sysdate, sysdate, 'test', 'test', 1000)";

                cmd.CommandText = instRenwDetails;

                OracleParameter oPolNum = new OracleParameter();
                oPolNum.DbType = DbType.String;
                oPolNum.Value = polNum;
                oPolNum.ParameterName = "polNum";

                OracleParameter oDept = new OracleParameter();
                oDept.DbType = DbType.String;
                oDept.Value = dept;
                oDept.ParameterName = "dept";

                OracleParameter oPolType = new OracleParameter();
                oPolType.DbType = DbType.String;
                oPolType.Value = polType;
                oPolType.ParameterName = "polType";

                OracleParameter oAmount = new OracleParameter();
                oAmount.DbType = DbType.Double;
                oAmount.Value = amount;
                oAmount.ParameterName = "amount";

                OracleParameter oUsername = new OracleParameter();
                oUsername.Value = userName;
                oUsername.ParameterName = "username";

                OracleParameter oStatus = new OracleParameter();
                oStatus.DbType = DbType.String;
                oStatus.Value = status;
                oStatus.ParameterName = "status";                              

                OracleParameter oStartDate = new OracleParameter();
                oStartDate.DbType = DbType.DateTime;
                oStartDate.Value = startDate;
                oStartDate.ParameterName = "startDate";

                OracleParameter oEndDate = new OracleParameter();
                oEndDate.DbType = DbType.DateTime;
                oEndDate.Value = endDate;
                oEndDate.ParameterName = "endDate";

                OracleParameter oRecptNo = new OracleParameter();
                oRecptNo.DbType = DbType.String;
                oRecptNo.Value = receiptNo;
                oRecptNo.ParameterName = "recptNo";

                OracleParameter oCusName = new OracleParameter();
                oCusName.DbType = DbType.String;
                oCusName.Value = cusName;
                oCusName.ParameterName = "cusName";

                OracleParameter oSumAssurd = new OracleParameter();
                oSumAssurd.DbType = DbType.Double;
                oSumAssurd.Value = sumAssurd;
                oSumAssurd.ParameterName = "sumAssurd";

                OracleParameter oAddress = new OracleParameter();
                oAddress.DbType = DbType.String;
                oAddress.Value = address;
                oAddress.ParameterName = "address";

                OracleParameter oVehiNum = new OracleParameter();
                oVehiNum.DbType = DbType.String;
                oVehiNum.Value = vehiNum;
                oVehiNum.ParameterName = "vehiNo";

                cmd.Parameters.Add(oPolNum);
                cmd.Parameters.Add(oDept);
                cmd.Parameters.Add(oPolType);
                cmd.Parameters.Add(oAmount);
                cmd.Parameters.Add(oUsername);
                cmd.Parameters.Add(oStatus);
                cmd.Parameters.Add(oStartDate);
                cmd.Parameters.Add(oEndDate);
                cmd.Parameters.Add(oRecptNo);
                cmd.Parameters.Add(oCusName);
                cmd.Parameters.Add(oSumAssurd);
                cmd.Parameters.Add(oAddress);
                cmd.Parameters.Add(oVehiNum);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                trans.Commit();
                returnValue = true;
            }

        }
        catch (Exception e)
        {
            trans.Rollback();
            log logger = new log();
            logger.write_log("Failed at Proposal - insert_renewal: " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return returnValue;
    }

    public bool update_paid_renewal(string refNo, string status, string retCode, string resCode)
    {

        bool returnValue = false;
        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        OracleCommand cmd = oconn.CreateCommand();
        OracleTransaction trans = oconn.BeginTransaction();
        cmd.Transaction = trans;
        try
        {
            using (cmd)
            {
                string updatePayment = "Update SLIC_NET.RENEWAL_DETAILS Set STATUS = :status, PG_RET_CODE = :retCode, PG_RSN_CODE = :resCode, ENTRY_DATE = sysdate" +
                                         " where RECEIPT_NO = :refNo";

                cmd.CommandText = updatePayment;

                OracleParameter oStatus = new OracleParameter();
                oStatus.DbType = DbType.String;
                oStatus.Value = status;
                oStatus.ParameterName = "status";

                OracleParameter oRetCode = new OracleParameter();
                oRetCode.DbType = DbType.String;
                oRetCode.Value = retCode;
                oRetCode.ParameterName = "retCode";

                OracleParameter oResCode = new OracleParameter();
                oResCode.DbType = DbType.String;
                oResCode.Value = resCode;
                oResCode.ParameterName = "resCode";

                OracleParameter oRefNo = new OracleParameter();
                oRefNo.DbType = DbType.String;
                oRefNo.Value = refNo;
                oRefNo.ParameterName = "refNo";

                cmd.Parameters.Add(oStatus);
                cmd.Parameters.Add(oRetCode);
                cmd.Parameters.Add(oResCode);
                cmd.Parameters.Add(oRefNo);

                cmd.ExecuteNonQuery();               

                trans.Commit();
                returnValue = true;
            }

        }
        catch (Exception e)
        {
            trans.Rollback();
            log logger = new log();
            logger.write_log("Failed at Renewal - update_paid_renewal: " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return returnValue;
    }
    
    public bool send_pay_receipt_email()
    {
        bool retValue = false;
        string email = "";
        string agentMoble = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getUsername = "Select EMAIL from ULWEB.WEBUSERS where UPPER(USERNAME) = UPPER(:userName)";


            using (OracleCommand com1 = new OracleCommand(getUsername, oconn))
            {
                com1.Parameters.AddWithValue("userName", username);

                OracleDataReader usernameReader = (OracleDataReader)com1.ExecuteReader();

                if (usernameReader.HasRows)
                {
                    while (usernameReader.Read())
                    {
                        if (!usernameReader.IsDBNull(0))
                        {
                            email = usernameReader.GetString(0);
                        }

                    }
                    usernameReader.Close();
                }
            }

            string getAgentMobile = "select phmob from genpay.momas, agent.agent" +
                                    " where fmagt = agency and upper(fmpol) = upper(:polNo)";


            using (OracleCommand com2 = new OracleCommand(getAgentMobile, oconn))
            {
                com2.Parameters.AddWithValue("polNo", polNum);

                OracleDataReader agentMobileReader = (OracleDataReader)com2.ExecuteReader();

                if (agentMobileReader.HasRows)
                {
                    while (agentMobileReader.Read())
                    {
                        if (!agentMobileReader.IsDBNull(0))
                        {
                            agentMoble = agentMobileReader.GetString(0);
                        }

                    }
                    agentMobileReader.Close();
                }
            }

            string subject =    "SLIC Payment Confirmaton";
            //string content1 = "<table class=\"style2\" style=\"border: thin solid #000000\">" +
            //                    "<tbody><tr>" +
            //                    "<td colspan=\"2\" style=\"text-align: center\">" +
            //                    "<img id=\"ContentPlaceHolder1_ContentPlaceHolder1_Image2\" src=\"http://www.srilankainsurance.lk/mail/logo.png\" style=\"text-align: center\">" +
            //                    "</td>" +
            //                    "</tr>" +
            //                    "<tr>" +
            //                        "<td class=\"font-size: 12pt\" style=\"text-align: center\" colspan=\"2\">" +
            //                            "Sri Lanka Insurance Corporation</td>" +
            //                    "</tr>" +
            //                    "<tr>" +
            //                        "<td class=\"font-size: 16pt\" colspan=\"2\" style=\"text-align: center\">" +
            //                            "&nbsp;</td>" +
            //                    "</tr>" +
            //                    "<tr>" +
            //                        "<td class=\"font-size: 20pt\" colspan=\"2\" style=\"text-align: center\">" +
            //                            "<strong>Online Payment Receipt</strong></td>" +
            //                    "</tr>" +
            //                    "<tr>" +
            //                        "<td colspan=\"2\">" +
            //                            "&nbsp;</td>" +
            //                    "</tr>" +
            //                    "<tr>" +
            //                        "<td colspan=\"2\">" +
            //                            address +
            //                        "</td>" +
            //                    "</tr>" +
            //                    "<tr>" +
            //                        "<td colspan=\"2\">" +
            //                            "Dear Customer,</td>" +
            //                    "</tr>" +
            //                    "<tr>" +
            //                        "<td colspan=\"2\">" +
            //                            "We have received your payment of Rs." +
            //                            amount.ToString("N2") +
            //                            "&nbsp;under the reference no:&nbsp;" +
            //                            receiptNo +
            //                            "&nbsp;regarding renewal payment for " +
            //                            polTypName +
            //                            "&nbsp;policy.</td>" +
            //                    "</tr>" +
            //                    "<tr>" +
            //                        "<td class=\"width: 80%\" colspan=\"2\">" +
            //                            "&nbsp;&nbsp;</td>" +
            //                    "</tr>" +
            //                    "<tr>" +
            //                       " <td class=\"font-size: 14pt\" colspan=\"2\">" +
            //                            "Policy Details:</td>" +
            //                    "</tr>" +
            //                    "<tr>" +
            //                        "<td class=\"width: 20%\">" +
            //                            "Policy Number:" +
            //                            "</td>" +
            //                        "<td class=\"width: 80%\">" +
            //                            polNum +
            //                        "</td>" +
            //                    "</tr>";

            string content1 = "<table>" +
                                "<tbody>" +
                                "<tr>" +
                                    "<td colspan=\"2\">" +
                                        "Dear Customer,</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td colspan=\"2\">" +
                                        "Your payment of Rs." +
                                        amount.ToString("N2") +
                                        "&nbsp;under the reference no:&nbsp;" +
                                        receiptNo +
                                        "&nbsp;regarding renewal payment for the policy: " +
                                        polTypName +
                                        " has been received.</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td class=\"width: 80%\" colspan=\"2\">" +
                                        "&nbsp;&nbsp;</td>" +
                                "</tr>" +
                                "<tr>" +
                                   " <td class=\"font-size: 14pt\" colspan=\"2\"><u>" +
                                        "Policy Details</u></td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td class=\"width: 20%\">" +
                                        "Policy Number:" +
                                        "</td>" +
                                    "<td class=\"width: 80%\">" +
                                        polNum +
                                    "</td>" +
                                "</tr>";

            string vehiNumDetails = "<tr>" +
                                      "<td class=\"width: 20%\">" +
                                          "Vehicle Number:</td>" +
                                      "<td class=\"width: 80%\">" +
                                          vehiNum +
                                      "</td>" +
                                  "</tr>";

            if (dept == "M")
            {
                content1 = content1 + vehiNumDetails;
            }
            content1 = content1 + "<tr>" +
                                   "<td class=\"width: 20%\">" +
                                       "Sum Assured (Rs.):" +
                                       "</td>" +
                                   "<td class=\"width: 80%\">" +
                                       sumAssurd.ToString("N2") +
                                   "</td>" +
                               "</tr>" +
                               "<tr>" +
                                   "<td class=\"width: 20%\">" +
                                       "Customer Name:" +
                                       "</td>" +
                                   "<td class=\"width: 80%\">" +
                                       custName +
                                   "</td>" +
                               "</tr>" +
                               "<tr>" +
                                   "<td class=\"width: 20%\">" +
                                       "Premium (Rs.):" +
                                       "</td>" +
                                   "<td class=\"width: 80%\">" +
                                       amount.ToString("N2") +
                                   "</td>" +
                               "</tr>";
            if (dept == "M")
            {
                content1 = content1 +
                               "<tr>" +
                                   "<td class=\"width: 20%\">" +
                                       "Cover period:</td>" +
                                   "<td class=\"width: 80%\">" +
                                       "From: " + startDate + " to: " + endDate +
                                   "</td>" +
                               "</tr>";
            }
            else
            {
                content1 = content1 +
                               "<tr>" +
                                   "<td class=\"width: 20%\">" +
                                       "Cover period:</td>" +
                                   "<td class=\"width: 80%\">" +
                                       "Starts from: " + startDate +
                                   "</td>" +
                               "</tr>";
            }

            content1 = content1 +
                               "<tr>" +
                                    "<td class=\"width: 20%\">" +
                                        "</td>" +
                                    "<td class=\"width: 80%\">" +
                                    "</td>" +
                               "</tr>" +
                                "<tr>" +
                                   "<td class=\"width: 20%\">" +
                                       "Date of Payment:" +
                                       "</td>" +
                                   "<td class=\"width: 80%\">" +
                                       entryDate +
                                   "</td>" +
                               "</tr>" +
                               "<tr>" +
                                   "<td colspan=\"2\">" +
                                      " &nbsp;</td>" +
                               "</tr>";

            string noClaimMesg = "<tr>" +
                                   "<td colspan=\"2\">" +
                                   "<b>Please note that if any claim is made prior to the renewal date of the policy, there will be a revision in the premium paid.</b>" +
                                   "</td>" +
                               "</tr>";
            if (DateTime.Now <= DateTime.ParseExact(startDate, "yyyy/MM/dd", CultureInfo.InvariantCulture))
            {
                content1 = content1 + noClaimMesg;
            }

            if (dept == "M")
            {
                content1 = content1 + "<tr>" +
                                    "<td colspan=\"2\">" +
                                        "If luxury/ semi luxury taxes are not applicable, renewal receipt and original certificate of insured will be posted to your policy address in due course.</td>" +
                                "</tr>" +
                                "<tr>&nbsp;</tr>";
                if (agentMoble != null && agentMoble != "")
                {
                    content1 = content1 + "<tr>" +
                       "<td colspan=\"2\">" +
                           "If you have any queries regarding your motor policy, please contact advisor - " + agentMoble + " or SLIC Call Center - 0112357357, 0117357357.</td>" +
                   "</tr>";
                }
                else
                {
                    content1 = content1 + "<tr>" +
                       "<td colspan=\"2\">" +
                           "If you have any queries regarding your motor policy, please contact SLIC Call Center - 0112357357, 0117357357.</td>" +
                   "</tr>";
                }

                content1 = content1 + "<tr>" +
                                    "<td colspan=\"2\">" +
                                        "Thanking you,<br> Sri Lanka Insurance.</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td colspan=\"2\">" +
                                        "&nbsp;</td>" +
                                "</tr>" +
                            "</tbody></table>";
            }
            else
            {
                content1 = content1 + "<tr>" +
                                       "<td colspan=\"2\">" +
                                           "Renewal Receipt will be posted to your policy address in due course.</td>" +
                                   "</tr>" +
                                   "<tr>&nbsp;</tr>" +
                                   "<tr>" +
                                       "<td colspan=\"2\">" +
                                           "Thanking you,<br> Sri Lanka Insurance.</td>" +
                                   "</tr>" +
                                   "<tr>" +
                                       "<td colspan=\"2\">" +
                                           "&nbsp;</td>" +
                                   "</tr>" +
                               "</tbody></table>";
            }
                /*"Dear Customer, <br/> We have received your payment of Rs. " + amount.ToString("N2") +
                              " under the reference no: " + receiptNo + " regarding premium payment for " + polTypName + " policy.";*/


             string content2 = content1;

            Db_Email emailSender = new Db_Email();
            retValue = emailSender.send_html_email(email, subject, content1, content2);
            LogMail logger = new LogMail();
            logger.write_log("To: " + email + " Subject: " + subject);

            //retValue = true;
        }
        catch (Exception e)
        {
            retValue = false;
            log logger = new log();
            logger.write_log("Failed at Renewal - send_pay_receipt_email " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return retValue;
    }

    public bool updateRevLicenDetails()
    {
        bool retValue = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string chassisNo = "";
            int motorType = 0;

            string vehiPre = vehiNum.Substring(0, 3).Trim();
            string vehiPost = vehiNum.Substring(3, 5).Trim();
            string seqNo = receiptNo.Substring(receiptNo.Length - 5, 5);

            string getChassisNo = "select mvchas from mcomp.mcactuvdet where trim(mvpolno)= :polNo and(MVSTRM=0)";
            using (OracleCommand cmd = new OracleCommand(getChassisNo, oconn))
            {      
                OracleParameter oPolNo = new OracleParameter();
                oPolNo.Value = polNum;
                oPolNo.ParameterName = "polNo";
                                
                cmd.Parameters.Add(oPolNo);
                OracleDataReader chassiReader = cmd.ExecuteReader();
                while (chassiReader.Read())
                {
                    if (!chassiReader.IsDBNull(0))
                    {
                        chassisNo = chassiReader.GetString(0);
                    }
                }
                chassiReader.Close();
                cmd.Parameters.Clear();
            }

            if (chassisNo != "")
            {
                motorType = 1;
            }
            else
            {
                motorType = 3;
            }
            int rows = 0;
            string getRevLicenDetails = "Select * from Mcomp.icta_motor_detail" +
                                        " where reg_id_pre = trim(substr(:vehiNum, 1, 3))" +
                                        " and reg_id_post = trim(substr(:vehiNum, 4, 8)) ";

            using (OracleCommand cmd = new OracleCommand(getRevLicenDetails, oconn))
            {              
                OracleParameter oVehiNum = new OracleParameter();
                oVehiNum.Value = vehiNum;
                oVehiNum.ParameterName = "vehiNum";
                                
                cmd.Parameters.Add(oVehiNum);

                OracleDataReader detReader = cmd.ExecuteReader();
                
                while (detReader.Read())
                {
                    rows++;
                }
                detReader.Close();
                cmd.Parameters.Clear();
            }

            if (rows > 0)
            {                
                string updateRevLicDetails = "Update MCOMP.ICTA_MOTOR_DETAIL set POLICY_ID = :polNo," +
                                             " policy_start_date = to_date(:startDate,'yyyy/mm/dd'), policy_end_date = to_date(:endDate,'yyyy/mm/dd'), " +
                                             " entered_user_id = :seqNo, changed_date = sysdate" + 
                                             " where reg_id_pre = :vehiPre" +
                                             " AND reg_id_post= :vehiPost";

                using (OracleCommand cmd = new OracleCommand(updateRevLicDetails, oconn))
                {

                    OracleParameter oPolNum = new OracleParameter();
                    oPolNum.Value = polNum;
                    oPolNum.ParameterName = "polNo";

                    OracleParameter oStartDate = new OracleParameter();
                    oStartDate.Value = startDate;
                    oStartDate.ParameterName = "startDate";

                    OracleParameter oEndDate = new OracleParameter();
                    oEndDate.Value = endDate;
                    oEndDate.ParameterName = "endDate";

                    OracleParameter oSeqNo = new OracleParameter();
                    oSeqNo.Value = seqNo;
                    oSeqNo.ParameterName = "seqNo";

                    OracleParameter oVehiPre = new OracleParameter();
                    oVehiPre.Value = vehiPre;
                    oVehiPre.ParameterName = "vehiPre";

                    OracleParameter oVehiPost = new OracleParameter();
                    oVehiPost.Value = vehiPost;
                    oVehiPost.ParameterName = "vehiPost";

                    cmd.Parameters.Add(oPolNum);
                    cmd.Parameters.Add(oStartDate);
                    cmd.Parameters.Add(oEndDate);
                    cmd.Parameters.Add(oSeqNo);
                    cmd.Parameters.Add(oVehiPre);
                    cmd.Parameters.Add(oVehiPost);

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    retValue = true;
                }                
            }
            else
            {
                string instRevLicDetails = "Insert into Mcomp.icta_motor_detail (reg_id_pre, reg_id_post, chassis_id, policy_id," +
                                           " policy_year, policy_short_period_id, is_a_cover_note, is_canceled, entered_date, entered_user_id," +
                                           " changed_date, motor_system_id, policy_start_date, policy_end_date)" +
                                           " values( :vehiPre, :vehiPost, :chasiId, :polNum, to_char(to_date(:startDate,'yyyy/mm/dd'), 'yyyy'),99, 0, 0," +
                                           " sysdate, :seqNo, sysdate, :motorTyp, to_date(:startDate,'yyyy/mm/dd'),to_date(:endDate,'yyyy/mm/dd'))";

                using (OracleCommand cmd = new OracleCommand(instRevLicDetails, oconn))
                {

                    OracleParameter oVehiPre = new OracleParameter();
                    oVehiPre.Value = vehiPre;
                    oVehiPre.ParameterName = "vehiPre";

                    OracleParameter oVehiPost = new OracleParameter();
                    oVehiPost.Value = vehiPost;
                    oVehiPost.ParameterName = "vehiPost";

                    OracleParameter oChassisNo = new OracleParameter();
                    oChassisNo.Value = chassisNo;
                    oChassisNo.ParameterName = "chasiId";

                    OracleParameter oPolNum = new OracleParameter();
                    oPolNum.Value = polNum;
                    oPolNum.ParameterName = "polNum";

                    OracleParameter oStartDate = new OracleParameter();
                    oStartDate.Value = startDate;
                    oStartDate.ParameterName = "startDate";

                    OracleParameter oSeqNo = new OracleParameter();
                    oSeqNo.Value = seqNo;
                    oSeqNo.ParameterName = "seqNo";

                    OracleParameter oMotorType = new OracleParameter();
                    oMotorType.Value = motorType;
                    oMotorType.ParameterName = "motorTyp";

                    OracleParameter oEndDate = new OracleParameter();
                    oEndDate.Value = endDate;
                    oEndDate.ParameterName = "endDate";

                    cmd.Parameters.Add(oVehiPre);
                    cmd.Parameters.Add(oVehiPost);
                    cmd.Parameters.Add(oChassisNo);
                    cmd.Parameters.Add(oPolNum);
                    cmd.Parameters.Add(oStartDate);
                    cmd.Parameters.Add(oSeqNo);
                    cmd.Parameters.Add(oMotorType);
                    cmd.Parameters.Add(oEndDate);

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    retValue = true;
                }
                
            }                      

        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at Renewal - updateRevLicenDetails " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return retValue;
    }
}