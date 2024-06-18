using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OracleClient;
using HashLibrary;
using System.Collections.Generic;
using System.Globalization;
using System.Data.Odbc;

/// <summary>
/// Summary description for LifePayment
/// </summary>
public class LifePayment
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OraLifeDB"]);
    OracleConnection oconnGen = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    OdbcConnection db2conn = new OdbcConnection(ConfigurationManager.AppSettings["DB2"]);

    string domainAndPort = "www.srilankainsurance.net";

    public string receiptNo { get; private set; }
    public string polNum { get; private set; }
    public double amount { get; private set; }
    public string entryDate { get; private set; }
    public string username { get; private set; }
    public string status { get; private set; }
    public string pgRetCode { get; private set; }
    public string pgResCode { get; private set; }
    public string custName { get; private set; }
    public string curDues { get; private set; }
    public string payType { get; private set; }
    public string loanNo { get; private set; }
    public double deposits { get; private set; }
    public double duesAmt { get; private set; }
    public double addtAmt { get; private set; }
    public double paidDuesAmt { get; private set; }
    public DataSet dsPaidDues { get; private set; }
    
	public LifePayment()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public LifePayment(string ref_no)
    {
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string sql = "SELECT RECEIPT_NO, POL_NUM, PAY_AMT, to_char(ENTRY_DATE, 'yyyy/mm/dd') AS ENTRY_DATE, USERNAME, STATUS, nvl(PG_RET_CODE, 0) PG_RET_CODE," +
                         " nvl(PG_RSN_CODE, 0) PG_RSN_CODE, CUST_NAME, CUR_DUES, PAY_TYPE, LON_NUM, DEPOSITS, DUES_AMT, ADDT_AMT, PAID_DUES_AMT FROM SLIC_NET_LIFE.RENEWAL_DETAILS WHERE RECEIPT_NO = :refno ";

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
                    amount = Convert.ToDouble(reader[2].ToString().Trim());
                    entryDate = reader[3].ToString().Trim();
                    username = reader[4].ToString().Trim();
                    status = reader[5].ToString().Trim();
                    pgRetCode = reader[6].ToString().Trim();
                    pgResCode = reader[7].ToString().Trim();
                    custName = reader[8].ToString().Trim();
                    curDues = reader[9].ToString().Trim();
                    payType = reader[10].ToString().Trim();
                    loanNo = reader[11].ToString().Trim();
                    deposits = Convert.ToDouble(reader[12].ToString().Trim());
                    duesAmt = Convert.ToDouble(reader[13].ToString().Trim());
                    addtAmt = Convert.ToDouble(reader[14].ToString().Trim());
                    paidDuesAmt = Convert.ToDouble(reader[15].ToString().Trim());
                }

                reader.Close();
            }


            string getPaidDues = "Select DEMAND, to_char(PREMIUM, '9,999,999,999.99') PREMIUM, to_char(LATEFEE, '9,999,999,999.99') LATEFEE" +
                                 " from SLIC_NET_LIFE.PAID_POLS_FOR_LIFE_WEB_PAY" +
                                 " where REF_NO = :refno and PAID_DATE is not null";

            dsPaidDues = new DataSet();

            using (OracleDataAdapter adapter = new OracleDataAdapter(getPaidDues, oconn))
            {
                OracleParameter oRef = new OracleParameter();
                oRef.Value = ref_no;
                oRef.ParameterName = "refno";

                adapter.SelectCommand.Parameters.Add(oRef);

                dsPaidDues.Clear();
                adapter.Fill(dsPaidDues);                
            }


        }
        catch (Exception ex)
        {
            log logger = new log();
            logger.write_log("Failed at LifePayment constructor: " + ex.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
    }

    public string generate_renwReceiptNo(int year, string type)
    {
        string result = "";
        string id = "";
        //string n = "";
        try
        {
            oconn.Open();
            string sql = "SELECT * FROM SLIC_NET_LIFE.RENW_REF_NUMBERS WHERE YEAR = :yr AND TYP = :polType ";
            int rows = 0;
            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {              

                OracleParameter oYear = new OracleParameter();
                oYear.DbType = DbType.Int32;
                oYear.Value = year;
                oYear.ParameterName = "yr";

                OracleParameter oPolType = new OracleParameter();
                oPolType.Value = type;
                oPolType.ParameterName = "polType";

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
                result = update_renwSeq(sql, year, type).ToString();
            }
            else
            {
                result = insert_newSeq(year, type).ToString();
            }

            string seq = result.ToString().PadLeft(5, '0');

            id = "LR/" + type + "/" + year.ToString() + "/" + seq;

            result = id;
        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at generate_renwReceiptNo " + e.ToString());
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


    private int update_renwSeq(string sql, int year, string type)
    {
        int seqNo = 0;
        try
        {
            seqNo = get_max_refNo(sql, year, type);
            string sql2 = "UPDATE SLIC_NET_LIFE.RENW_REF_NUMBERS SET REFNO = :seqNo WHERE YEAR = :year AND TYP = :polType";
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
                               
                OracleParameter oYear = new OracleParameter();
                oYear.DbType = DbType.Int32;
                oYear.Value = year;
                oYear.ParameterName = "year";

                OracleParameter oPolTyp = new OracleParameter();
                oPolTyp.Value = type;
                oPolTyp.ParameterName = "polType";

                cmd.Parameters.Add(oSeqNo);
                cmd.Parameters.Add(oYear);
                cmd.Parameters.Add(oPolTyp);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

            }
        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at update_renwSeq " + e.ToString());
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

    private int get_max_refNo(string sql, int year, string type)
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
                OracleParameter oYear = new OracleParameter();
                oYear.DbType = DbType.Int32;
                oYear.Value = year;
                oYear.ParameterName = "yr";

                OracleParameter oPolType = new OracleParameter();
                oPolType.Value = type;
                oPolType.ParameterName = "polType";

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
            logger.write_log("Failed at get_max_propNo " + e.ToString());
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

    private int insert_newSeq(int year, string type)
    {
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string sql2 = "INSERT INTO SLIC_NET_LIFE.RENW_REF_NUMBERS VALUES (:year , :polType," + 1 + ")";

            using (OracleCommand cmd = new OracleCommand(sql2, oconn))
            {
                //cmd.Parameters.AddWithValue("dept", dpt);
                //cmd.Parameters.AddWithValue("year", year);
                //cmd.Parameters.AddWithValue("polType", type);                              

                OracleParameter oYear = new OracleParameter();
                oYear.Value = year;
                oYear.ParameterName = "year";

                OracleParameter oPolType = new OracleParameter();
                oPolType.Value = type;
                oPolType.ParameterName = "polType";

                cmd.Parameters.Add(oYear);
                cmd.Parameters.Add(oPolType);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

            }

        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at insert_newSeq " + e.ToString());
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

    
    public bool insert_renewal(string polNum, double amount, string userName, string status, string receiptNo, string cusName, string demands, string payType, string loanNum, GridView gvDemands, double deposits, double duesTotalAmt, double addtAmt, double duesPaidAmt, string custEmail, string mobileNo)
    {
        bool returnValue = false;

        string ret = "success";
        if (payType == "P" && duesTotalAmt > 0)
        {
            ret = depositAdjPending(polNum);
        }

        if (ret == "success")
        {
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
                    string instRenwDetails = "Insert into SLIC_NET_LIFE.RENEWAL_DETAILS(POL_NUM, PAY_AMT, ENTRY_DATE, USERNAME, STATUS, RECEIPT_NO, CUST_NAME, CUR_DUES, PAY_TYPE, LON_NUM, DEPOSITS, DUES_AMT, ADDT_AMT, PAID_DUES_AMT)" +
                                                            " VALUES(:polNum, :amount, sysdate, :username, :status, :recptNo, :cusName, :demands, :payTyp, :loanNo, :deposit, :dueTotal, :addtPaymnt, :paidDues)";
                    // " VALUES(:polNum, :dept, :polType, :amount, sysdate, :usernam, 'P', '123', sysdate, sysdate, 'test', 'test', 1000)";

                    cmd.CommandText = instRenwDetails;

                    OracleParameter oPolNum = new OracleParameter();
                    oPolNum.DbType = DbType.String;
                    oPolNum.Value = polNum;
                    oPolNum.ParameterName = "polNum";

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

                    OracleParameter oRecptNo = new OracleParameter();
                    oRecptNo.DbType = DbType.String;
                    oRecptNo.Value = receiptNo;
                    oRecptNo.ParameterName = "recptNo";

                    OracleParameter oCusName = new OracleParameter();
                    oCusName.DbType = DbType.String;
                    oCusName.Value = cusName;
                    oCusName.ParameterName = "cusName";

                    OracleParameter oDemands = new OracleParameter();
                    oDemands.DbType = DbType.String;
                    oDemands.Value = demands;
                    oDemands.ParameterName = "demands";

                    OracleParameter oPayTyp = new OracleParameter();
                    oPayTyp.DbType = DbType.String;
                    oPayTyp.Value = payType;
                    oPayTyp.ParameterName = "payTyp";

                    OracleParameter oLoanNum = new OracleParameter();
                    oLoanNum.DbType = DbType.String;
                    oLoanNum.Value = loanNum;
                    oLoanNum.ParameterName = "loanNo";

                    OracleParameter oDeposit = new OracleParameter();
                    oDeposit.DbType = DbType.Double;
                    oDeposit.Value = deposits;
                    oDeposit.ParameterName = "deposit";

                    OracleParameter oDueTotal = new OracleParameter();
                    oDueTotal.DbType = DbType.Double;
                    oDueTotal.Value = duesTotalAmt;
                    oDueTotal.ParameterName = "dueTotal";

                    OracleParameter oAddtPaymnt = new OracleParameter();
                    oAddtPaymnt.DbType = DbType.Double;
                    oAddtPaymnt.Value = addtAmt;
                    oAddtPaymnt.ParameterName = "addtPaymnt";

                    OracleParameter oDuesPaid = new OracleParameter();
                    oDuesPaid.DbType = DbType.Double;
                    oDuesPaid.Value = duesPaidAmt;
                    oDuesPaid.ParameterName = "paidDues";                    

                    cmd.Parameters.Add(oPolNum);
                    cmd.Parameters.Add(oAmount);
                    cmd.Parameters.Add(oUsername);
                    cmd.Parameters.Add(oStatus);
                    cmd.Parameters.Add(oRecptNo);
                    cmd.Parameters.Add(oCusName);
                    cmd.Parameters.Add(oDemands);
                    cmd.Parameters.Add(oPayTyp);
                    cmd.Parameters.Add(oLoanNum);
                    cmd.Parameters.Add(oDeposit);
                    cmd.Parameters.Add(oDueTotal);
                    cmd.Parameters.Add(oAddtPaymnt);
                    cmd.Parameters.Add(oDuesPaid);                    

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    if (payType == "P" && gvDemands.Rows.Count > 0)
                    {

                        foreach (GridViewRow row in gvDemands.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                string paidDue = row.Cells[0].Text.Trim();
                                double premium = double.Parse(row.Cells[1].Text.Trim());
                                double lateFee = double.Parse(row.Cells[2].Text.Trim());
                                CheckBox cbPayDue = (CheckBox)row.FindControl("cbPayDue");

                                if (cbPayDue.Checked)
                                {
                                    string deleteDemand = "delete from SLIC_NET_LIFE.PAID_POLS_FOR_LIFE_WEB_PAY where POLNO = :polnum and DEMAND = :due";

                                    cmd.CommandText = deleteDemand;

                                    OracleParameter oPol = new OracleParameter();
                                    oPol.DbType = DbType.String;
                                    oPol.Value = polNum;
                                    oPol.ParameterName = "polnum";

                                    OracleParameter oDue1 = new OracleParameter();
                                    oDue1.DbType = DbType.String;
                                    oDue1.Value = paidDue;
                                    oDue1.ParameterName = "due";

                                    cmd.Parameters.Add(oPol);
                                    cmd.Parameters.Add(oDue1);

                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();

                                    string instPaidPols = "Insert into SLIC_NET_LIFE.PAID_POLS_FOR_LIFE_WEB_PAY(RUNDATE, POLNO, PREMIUM, LATEFEE, NAME, MOBILE_NO, NIC_NO, DEMAND)" +
                                                          " (select RUNDATE, POLNO, PREMIUM, LATEFEE, NAME, MOBILE_NO, NIC_NO, DEMAND from SLIC_NET_LIFE.INFORCE_POLS_FOR_LIFE_WEB_PAY" +
                                                          " where POLNO = :pol and DEMAND = :due)";

                                    cmd.CommandText = instPaidPols;

                                    OracleParameter oPolNum2 = new OracleParameter();
                                    oPolNum2.DbType = DbType.String;
                                    oPolNum2.Value = polNum;
                                    oPolNum2.ParameterName = "pol";

                                    OracleParameter oDue2 = new OracleParameter();
                                    oDue2.DbType = DbType.String;
                                    oDue2.Value = paidDue;
                                    oDue2.ParameterName = "due";

                                    cmd.Parameters.Add(oPolNum2);
                                    cmd.Parameters.Add(oDue2);

                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();

                                    string updateDemand = "Update slic_net_life.PAID_POLS_FOR_LIFE_WEB_PAY" +
                                                          " set REF_NO = :refNo" +
                                                          " where POLNO = :polNo" +
                                                          " and DEMAND = :due" +
                                                          " and PREMIUM = :prem" +
                                                          " and LATEFEE = :lFee";

                                    cmd.CommandText = updateDemand;

                                    OracleParameter oRefNo = new OracleParameter();
                                    oRefNo.DbType = DbType.String;
                                    oRefNo.Value = receiptNo;
                                    oRefNo.ParameterName = "refNo";

                                    OracleParameter oPolNo = new OracleParameter();
                                    oPolNo.DbType = DbType.String;
                                    oPolNo.Value = polNum;
                                    oPolNo.ParameterName = "polNo";

                                    OracleParameter oDue = new OracleParameter();
                                    oDue.DbType = DbType.String;
                                    oDue.Value = paidDue;
                                    oDue.ParameterName = "due";

                                    OracleParameter oPrem = new OracleParameter();
                                    oPrem.DbType = DbType.Double;
                                    oPrem.Value = premium;
                                    oPrem.ParameterName = "prem";

                                    OracleParameter oLFee = new OracleParameter();
                                    oLFee.DbType = DbType.Double;
                                    oLFee.Value = lateFee;
                                    oLFee.ParameterName = "lFee";

                                    cmd.Parameters.Add(oRefNo);
                                    cmd.Parameters.Add(oPolNo);
                                    cmd.Parameters.Add(oDue);
                                    cmd.Parameters.Add(oPrem);
                                    cmd.Parameters.Add(oLFee);

                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();

                                }
                            }

                        }
                    }

                    trans.Commit();
                    returnValue = true;
                }

            }
            catch (Exception e)
            {
                trans.Rollback();
                log logger = new log();
                logger.write_log("Failed at LifePayment - insert_renewal: " + e.ToString());
            }
            finally
            {
                if (oconn.State == ConnectionState.Open)
                {
                    oconn.Close();
                }
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
                string updatePayment = "Update SLIC_NET_LIFE.RENEWAL_DETAILS Set STATUS = :status, PG_RET_CODE = :retCode, PG_RSN_CODE = :resCode, ENTRY_DATE = sysdate" +
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
                cmd.Parameters.Clear();

                if (refNo.Contains("/P/") && status == "A")
                {                  
                           
                    string updateDemand = "Update slic_net_life.paid_pols_for_life_web_pay" +
                                            " set PAID_DATE = sysdate" +
                                            " where REF_NO = :refNo";

                    cmd.CommandText = updateDemand;

                    OracleParameter oRefNo2 = new OracleParameter();
                    oRefNo2.DbType = DbType.String;
                    oRefNo2.Value = receiptNo;
                    oRefNo2.ParameterName = "refNo";                  

                    cmd.Parameters.Add(oRefNo2);                  

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();                      
                }

                //if (refNo.Contains("/N/") && status == "A")
                if ((refNo.Contains("/N/") || refNo.Contains("/D/")) && status == "A")  //2022.06.02
                {
                    LifeProposal propObjt = new LifeProposal();
                    string polFeeRectNo = propObjt.getPolicyFeeRecpt(refNo);

                    if (!polFeeRectNo.Equals(""))
                    {
                        string updatePolFeeRec = "update SLIC_NET_LIFE.RENEWAL_DETAILS " +
                                                 " set STATUS = :status, PG_RET_CODE = :retCode, PG_RSN_CODE = :resCode, ENTRY_DATE = sysdate " +
                                                 " where RECEIPT_NO = :refNo2";

                        cmd.CommandText = updatePolFeeRec;

                        OracleParameter oStatus2 = new OracleParameter();
                        oStatus2.DbType = DbType.String;
                        oStatus2.Value = status;
                        oStatus2.ParameterName = "status";

                        OracleParameter oRetCode2 = new OracleParameter();
                        oRetCode2.DbType = DbType.String;
                        oRetCode2.Value = retCode;
                        oRetCode2.ParameterName = "retCode";

                        OracleParameter oResCode2 = new OracleParameter();
                        oResCode2.DbType = DbType.String;
                        oResCode2.Value = resCode;
                        oResCode2.ParameterName = "resCode";

                        OracleParameter oRefNo2 = new OracleParameter();
                        oRefNo2.DbType = DbType.String;
                        oRefNo2.Value = polFeeRectNo;
                        oRefNo2.ParameterName = "refNo2";

                        cmd.Parameters.Add(oStatus2);
                        cmd.Parameters.Add(oRetCode2);
                        cmd.Parameters.Add(oResCode2);
                        cmd.Parameters.Add(oRefNo2);

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    string insertProposalPaid = "insert into SLI_LUND.ONLINE_NEW_BUS_PAYMENTS" +
                                            " (RECEIPT_NO, PROPOSAL_NO, PAID_AMOUNT, PAID_DATE)" +
                                            "  (select RECEIPT_NO, POL_NUM, PAY_AMT, SYSDATE from SLIC_NET_LIFE.RENEWAL_DETAILS where RECEIPT_NO =:rect_no_v3)";

                    cmd.CommandText = insertProposalPaid;

                    OracleParameter oRefNo3 = new OracleParameter();
                    oRefNo3.DbType = DbType.String;
                    oRefNo3.Value = receiptNo;
                    oRefNo3.ParameterName = "rect_no_v3";

                    cmd.Parameters.Add(oRefNo3);

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    if (!polFeeRectNo.Equals(""))
                    {
                        string insertProposalPaid2 = "insert into SLI_LUND.ONLINE_NEW_BUS_PAYMENTS " +
                                                     " (RECEIPT_NO, PROPOSAL_NO, PAID_AMOUNT, PAID_DATE)" +
                                                     " (select RECEIPT_NO, POL_NUM, PAY_AMT, SYSDATE from SLIC_NET_LIFE.RENEWAL_DETAILS where RECEIPT_NO =:rect_no_v4 )";

                        cmd.CommandText = insertProposalPaid2;

                        OracleParameter oRefNo4 = new OracleParameter();
                        oRefNo4.DbType = DbType.String;
                        oRefNo4.Value = polFeeRectNo;
                        oRefNo4.ParameterName = "rect_no_v4";

                        cmd.Parameters.Add(oRefNo4);

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                //if (refNo.Contains("/N/") && status == "A")
                //{
                //    string updatePolFeeRec = "update SLIC_NET_LIFE.RENEWAL_DETAILS " + 
                //                             " set STATUS = :status, PG_RET_CODE = :retCode, PG_RSN_CODE = :resCode, ENTRY_DATE = sysdate " +
                //                             " where pol_num in (select POL_NUM from SLIC_NET_LIFE.RENEWAL_DETAILS where receipt_no =:refNo ) and receipt_no <> :refNo";

                //    cmd.CommandText = updatePolFeeRec;

                //    OracleParameter oStatus2 = new OracleParameter();
                //    oStatus2.DbType = DbType.String;
                //    oStatus2.Value = status;
                //    oStatus2.ParameterName = "status";

                //    OracleParameter oRetCode2 = new OracleParameter();
                //    oRetCode2.DbType = DbType.String;
                //    oRetCode2.Value = retCode;
                //    oRetCode2.ParameterName = "retCode";

                //    OracleParameter oResCode2 = new OracleParameter();
                //    oResCode2.DbType = DbType.String;
                //    oResCode2.Value = resCode;
                //    oResCode2.ParameterName = "resCode";

                //    OracleParameter oRefNo2 = new OracleParameter();
                //    oRefNo2.DbType = DbType.String;
                //    oRefNo2.Value = refNo;
                //    oRefNo2.ParameterName = "refNo";

                //    cmd.Parameters.Add(oStatus2);
                //    cmd.Parameters.Add(oRetCode2);
                //    cmd.Parameters.Add(oResCode2);
                //    cmd.Parameters.Add(oRefNo2);

                //    cmd.ExecuteNonQuery();
                //    cmd.Parameters.Clear();

                //    string insertProposalPaid = "insert into SLI_LUND.ONLINE_NEW_BUS_PAYMENTS" +
                //                            " (RECEIPT_NO, PROPOSAL_NO, PAID_AMOUNT, PAID_DATE)" +
                //                            " values (select RECEIPT_NO, POL_NUM, PAY_AMT, SYSDATE from SLIC_NET_LIFE.RENEWAL_DETAILS where RECEIPT_NO =:rect_no_v3)";

                //    cmd.CommandText = insertProposalPaid;

                //    OracleParameter oRefNo3 = new OracleParameter();
                //    oRefNo3.DbType = DbType.String;
                //    oRefNo3.Value = receiptNo;
                //    oRefNo3.ParameterName = "rect_no_v3";

                //    cmd.Parameters.Add(oRefNo3);

                //    cmd.ExecuteNonQuery();
                //    cmd.Parameters.Clear();

                //    string insertProposalPaid2 = "insert into SLI_LUND.ONLINE_NEW_BUS_PAYMENTS " +
                //                                 " (RECEIPT_NO, PROPOSAL_NO, PAID_AMOUNT, PAID_DATE)" +
                //                                 " values (select RECEIPT_NO, POL_NUM, PAY_AMT, SYSDATE from SLIC_NET_LIFE.RENEWAL_DETAILS " +
                //                                 " where POL_NUM in (select POL_NUM from SLIC_NET_LIFE.RENEWAL_DETAILS where receipt_no = :rect_no_v4 ) and receipt_no <> :rect_no_v4 )";

                //    cmd.CommandText = insertProposalPaid2;

                //    OracleParameter oRefNo4 = new OracleParameter();
                //    oRefNo4.DbType = DbType.String;
                //    oRefNo4.Value = receiptNo;
                //    oRefNo4.ParameterName = "rect_no_v4";

                //    cmd.Parameters.Add(oRefNo4);

                //    cmd.ExecuteNonQuery();
                //    cmd.Parameters.Clear();   
                //}

                trans.Commit();
                returnValue = true;

            }

        }
        catch (Exception e)
        {
            trans.Rollback();
            log logger = new log();
            logger.write_log("Failed at LifePayment - update_paid_renewal: " + e.ToString());
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
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }

            string getUsername = "Select EMAIL from ULWEB.WEBUSERS where UPPER(USERNAME) = UPPER(:userName)";


            using (OracleCommand com1 = new OracleCommand(getUsername, oconnGen))
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

            string subject = "SLIC Payment Receipt Notification";
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
            //    //  address +
            //                        "</td>" +
            //                    "</tr>" +
            //                    "<tr>" +
            //                        "<td colspan=\"2\">" +
            //                            "Dear Customer,</td>" +
            //                    "</tr>";


            string content1 = "<table>" +
                                "<tbody>" +
                                "<tr>" +
                                    "<td colspan=\"2\">" +
                                        "Dear Customer,</td>" +
                                "</tr>";

            if (payType == "P")
            {

                string duesPaidTable = "";
                if (dsPaidDues.Tables[0].Rows.Count > 0)
                {
                    duesPaidTable = "<table border= \"1\" width=\"60%\"><tbody> <tr><td><b>Due</b></td><td><b>Premium (Rs.)</b></td><td><b>Late fee (Rs.)</b></td></tr>";

                    foreach (DataRow row in dsPaidDues.Tables[0].Rows)
                    {
                        duesPaidTable = duesPaidTable + "<tr><td>" + row.ItemArray[0].ToString() + "</td><td>" + row.ItemArray[1].ToString() + "</td><td>" + row.ItemArray[2].ToString() + "</td></tr>";
                    }

                    duesPaidTable = duesPaidTable + "</tbody></table>";
                }
                content1 = content1 + "<tr>" +
                                        "<td colspan=\"2\">" +
                                            "We have received your payment of Rs." +
                                            amount.ToString("N2") +
                                            "&nbsp;under the reference no:&nbsp;" +
                                            receiptNo +
                                            "&nbsp;regarding premium payment for Life policy number " +
                                            polNum +
                                            ".</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td style=\"width: 80%\" colspan=\"2\">" +
                                            "&nbsp;&nbsp;</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                   " <td style=\"font-size: 14pt\" colspan=\"2\"><u>" +
                                        "Policy Details</u></td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td style=\"width: 20%\">" +
                                            "Policy Number:" +
                                            "</td>" +
                                        "<td style=\"width: 80%\">" +
                                            polNum +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                       "<td style=\"width: 20%\">" +
                                           "Customer Name:" +
                                           "</td>" +
                                       "<td style=\"width: 80%\">" +
                                           custName +
                                       "</td>" +
                                   "</tr>" +
                                   "<tr>" +
                                       "<td style=\"width: 20%\">" +
                                           "</td>" +
                                       "<td style=\"width: 80%\">" +
                                       "</td>" +
                                   "</tr>" +
                                   "<tr><td colspan = '2'>" +
                                   duesPaidTable +
                                   "</td></tr>" +
                                   "<tr>" +
                                       "<td style=\"width: 20%\">" +
                                           "</td>" +
                                       "<td style=\"width: 80%\">" +
                                       "</td>" +
                                   "</tr>" +
                                   "<tr>" +
                                       "<td style=\"width: 20%\">" +
                                           "Total Due Premium:" +
                                           "</td>" +
                                       "<td style=\"width: 80%\">" +
                                           duesAmt.ToString("N2") +
                                       "</td>" +
                                   "</tr>" +
                                   "<tr>" +
                                       "<td style=\"width: 20%\">" +
                                           "Deposits:" +
                                           "</td>" +
                                       "<td style=\"width: 80%\">" +
                                           deposits.ToString("N2") +
                                       "</td>" +
                                   "</tr>" +
                                   "<tr>" +
                                       "<td style=\"width: 20%\">" +
                                           "Paid Premium:" +
                                           "</td>" +
                                       "<td style=\"width: 80%\">" +
                                           paidDuesAmt.ToString("N2") +
                                       "</td>" +
                                   "</tr>" +
                                   "<tr>" +
                                       "<td style=\"width: 20%\">" +
                                           "Additional amount:" +
                                           "</td>" +
                                       "<td style=\"width: 80%\">" +
                                           addtAmt.ToString("N2") +
                                       "</td>" +
                                   "</tr>" +
                                   "<tr>" +
                                       "<td style=\"width: 20%\">" +
                                           "Date of Payment:" +
                                           "</td>" +
                                       "<td style=\"width: 80%\">" +
                                           entryDate +
                                       "</td>" +
                                   "</tr>" +
                                   "<tr>" +
                                       "<td colspan=\"2\">" +
                                          " &nbsp;</td>" +
                                   "</tr>" +
                                    "<tr>" +
                                        "<td colspan=\"2\">" +
                                            "Premium Receipt will be posted to your policy address in due course.</td>" +
                                    "</tr>";
            }
            else if (payType == "L")
            {
                content1 = content1 + "<tr>" +
                                        "<td colspan=\"2\">" +
                                            "We have received your payment of Rs." +
                                            amount.ToString("N2") +
                                            "&nbsp;under the reference no:&nbsp;" +
                                            receiptNo +
                                            "&nbsp;regarding Loan payment for loan number " +
                                            loanNo +
                                            " under the policy number " +
                                            polNum +
                                            ".</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td style=\"width: 80%\" colspan=\"2\">" +
                                            "&nbsp;&nbsp;</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                   " <td style=\"font-size: 14pt\" colspan=\"2\"><u>" +
                                        "Policy Details</u></td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td style=\"width: 20%\">" +
                                            "Policy Number:" +
                                            "</td>" +
                                        "<td style=\"width: 80%\">" +
                                            polNum +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                       "<td style=\"width: 20%\">" +
                                           "Customer Name:" +
                                           "</td>" +
                                       "<td style=\"width: 80%\">" +
                                           custName +
                                       "</td>" +
                                   "</tr>" +
                                   "<tr>" +
                                       "<td style=\"width: 20%\">" +
                                           "</td>" +
                                       "<td style=\"width: 80%\">" +
                                       "</td>" +
                                   "</tr>" +
                                   "<tr>" +
                                       "<td style=\"width: 20%\">" +
                                           "Date of Payment:" +
                                           "</td>" +
                                       "<td style=\"width: 80%\">" +
                                           entryDate +
                                       "</td>" +
                                   "</tr>" +
                                   "<tr>" +
                                       "<td colspan=\"2\">" +
                                          " &nbsp;</td>" +
                                   "</tr>" +
                                    "<tr>" +
                                        "<td colspan=\"2\">" +
                                            "Loan Payment Receipt will be posted to your policy address in due course.</td>" +
                                    "</tr>";
            }
            else if (payType == "N" || payType == "D" || payType == "M")
            {
                LifeProposal propObj = new LifeProposal();
                if ((propObj.checkRecptType(receiptNo)).Equals("M"))
                {
                    if (payType == "N")
                    {
                        content1 = content1 + "<tr>" +
                                            "<td colspan=\"2\">" +
                                                "We have received your payment of Rs." +
                                                amount.ToString("N2") +
                                                "&nbsp;under the reference no:&nbsp;" +
                                                receiptNo +
                                                "&nbsp;for a new proposal number " +
                                                polNum +
                                                ".</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td style=\"width: 80%\" colspan=\"2\">" +
                                                "&nbsp;&nbsp;</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                       " <td style=\"font-size: 14pt\" colspan=\"2\"><u>" +
                                            "Proposal Details</u></td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td style=\"width: 20%\">" +
                                                "Proposal Number:" +
                                                "</td>" +
                                            "<td style=\"width: 80%\">" +
                                                polNum +
                                            "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                           "<td style=\"width: 20%\">" +
                                               "Customer Name:" +
                                               "</td>" +
                                           "<td style=\"width: 80%\">" +
                                               custName +
                                           "</td>" +
                                       "</tr>" +
                                       "<tr>" +
                                           "<td style=\"width: 20%\">" +
                                               "</td>" +
                                           "<td style=\"width: 80%\">" +
                                           "</td>" +
                                       "</tr>" +
                                       "<tr>" +
                                           "<td style=\"width: 20%\">" +
                                               "Date of Payment:" +
                                               "</td>" +
                                           "<td style=\"width: 80%\">" +
                                               entryDate +
                                           "</td>" +
                                       "</tr>" +
                                       "<tr>" +
                                           "<td colspan=\"2\">" +
                                              " &nbsp;</td>" +
                                       "</tr>" +
                                        "<tr>" +
                                            "<td colspan=\"2\">" +
                                                "Proposal Payment Receipt will be posted to your policy address in due course.</td>" +
                                        "</tr>";
                    }
                    else if (payType == "D")
                    {
                        string refNum = propObj.getRefNo(polNum);
                        string name = propObj.getName(refNum);

                        subject = "SLIC-Online Early Cash Plan Notification (Reference no." + refNum + " / Proposal no." + polNum + ")";

                        content1 = "<table>" +
                                "<tbody>" +
                                "<tr>" +
                                    "<td colspan=\"2\">" +
                                        "Dear " + name + ",</td>" +
                                "</tr>";

                        //Thank you for your payment of Rs. <amount> to <ref. no> on <date>. Your Early Cash policy document will be sent to you via a separate email shortly.

                        content1 = content1 + "<tr>" +
                                   "<td colspan=\"2\">" +
                                       "&nbsp;</td>" +
                               "</tr>" +
                            "<tr>" +
                                            "<td colspan=\"2\">" +
                                                "Thank you for your payment of Rs." +
                                                amount.ToString("N2") +
                                                "&nbsp;under the reference no:&nbsp;" +
                                                refNum +
                                                "&nbsp;(Proposal no:&nbsp;" + polNum + 
                                                ")&nbsp;on " +
                                                entryDate +
                                                ".</td>" +
                                        "</tr>" +
                                        // "<tr>" +
                                        //     "<td style=\"width: 80%\" colspan=\"2\">" +
                                        //         "&nbsp;&nbsp;</td>" +
                                        // "</tr>" +
                                        // "<tr>" +
                                        //" <td style=\"font-size: 14pt\" colspan=\"2\"><u>" +
                                        //     "Proposal Details</u></td>" +
                                        // "</tr>" +
                                        "<tr>" +
                                            "<td style=\"width: 100%\">" +
                                                "Your Early Cash policy document will be sent to you via a separate email shortly" +
                                                "</td>" +
                                        //"<td style=\"width: 80%\">" +
                                        //    polNum +
                                        //"</td>" +
                                        "</tr>" +
                                         "<tr>" +
                                             "<td style=\"width: 80%\" colspan=\"2\">" +
                                                 "&nbsp;&nbsp;</td>" +
                                         "</tr>" +
                                         "<tr>" +
                                             "<td style=\"width: 80%\" colspan=\"2\">" +
                                                 "&nbsp;&nbsp;</td>" +
                                         "</tr>" +
                               "<tr>" +
                                   "<td colspan=\"2\">" +
                                       "This is a system-generated email and please do not respond to this email address. Please contact our 24x7 hotline 0112 357 357 for any inquiries.</td>" +
                               "</tr>" +                               
                               "<tr>" +
                                   "<td colspan=\"2\">" +
                                       "&nbsp;</td>" +
                               "</tr>" +
                               "<tr>" +
                                   "<td colspan=\"2\">" +
                                       "Sri Lanka Insurance Corporation</td>" +
                               "</tr>" +
                           "</tbody></table>";
                    }

                }
                else if ((propObj.checkRecptType(receiptNo)).Equals("P"))
                {
                    
                        content1 = content1 + "<tr>" +
                                                "<td colspan=\"2\">" +
                                                    "We have received your payment of Rs." +
                                                    amount.ToString("N2") +
                                                    "&nbsp;(Policy Fee)under the reference no:&nbsp;" +
                                                    receiptNo +
                                                    "&nbsp;for a new proposal number " +
                                                    polNum +
                                                    ".</td>" +
                                            "</tr>" +
                                            "<tr>" +
                                                "<td style=\"width: 80%\" colspan=\"2\">" +
                                                    "&nbsp;&nbsp;</td>" +
                                            "</tr>" +
                                            "<tr>" +
                                           " <td style=\"font-size: 14pt\" colspan=\"2\"><u>" +
                                                "Proposal Details</u></td>" +
                                            "</tr>" +
                                            "<tr>" +
                                                "<td style=\"width: 20%\">" +
                                                    "Proposal Number:" +
                                                    "</td>" +
                                                "<td style=\"width: 80%\">" +
                                                    polNum +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr>" +
                                               "<td style=\"width: 20%\">" +
                                                   "Customer Name:" +
                                                   "</td>" +
                                               "<td style=\"width: 80%\">" +
                                                   custName +
                                               "</td>" +
                                           "</tr>" +
                                           "<tr>" +
                                               "<td style=\"width: 20%\">" +
                                                   "</td>" +
                                               "<td style=\"width: 80%\">" +
                                               "</td>" +
                                           "</tr>" +
                                           "<tr>" +
                                               "<td style=\"width: 20%\">" +
                                                   "Date of Payment:" +
                                                   "</td>" +
                                               "<td style=\"width: 80%\">" +
                                                   entryDate +
                                               "</td>" +
                                           "</tr>" +
                                           "<tr>" +
                                               "<td colspan=\"2\">" +
                                                  " &nbsp;</td>" +
                                           "</tr>" +
                                            "<tr>" +
                                                "<td colspan=\"2\">" +
                                                    "Proposal Payment Receipt will be posted to your policy address in due course.</td>" +
                                            "</tr>";
                    
                }
            }
            //else if (payType == "D" || ) //2022.06.02
            //{
            //    log loggerr = new log();
            //    loggerr.write_log("D");
            //    loggerr.write_log(receiptNo);

            //    LifeProposal propObj = new LifeProposal();
            //    if ((propObj.checkRecptType(receiptNo)).Equals("M"))
            //    {
            //        loggerr.write_log("M");
            //        content1 = content1 + "<tr>" +
            //                                "<td colspan=\"2\">" +
            //                                    "We have received your payment of Rs." +
            //                                    amount.ToString("N2") +
            //                                    "&nbsp;under the reference no:&nbsp;" +
            //                                    receiptNo +
            //                                    "&nbsp;for a new proposal number " +
            //                                    polNum +
            //                                    ".</td>" +
            //                            "</tr>" +
            //                            "<tr>" +
            //                                "<td style=\"width: 80%\" colspan=\"2\">" +
            //                                    "&nbsp;&nbsp;</td>" +
            //                            "</tr>" +
            //                            "<tr>" +
            //                           " <td style=\"font-size: 14pt\" colspan=\"2\"><u>" +
            //                                "Proposal Details</u></td>" +
            //                            "</tr>" +
            //                            "<tr>" +
            //                                "<td style=\"width: 20%\">" +
            //                                    "Proposal Number:" +
            //                                    "</td>" +
            //                                "<td style=\"width: 80%\">" +
            //                                    polNum +
            //                                "</td>" +
            //                            "</tr>" +
            //                            "<tr>" +
            //                               "<td style=\"width: 20%\">" +
            //                                   "Customer Name:" +
            //                                   "</td>" +
            //                               "<td style=\"width: 80%\">" +
            //                                   custName +
            //                               "</td>" +
            //                           "</tr>" +
            //                           "<tr>" +
            //                               "<td style=\"width: 20%\">" +
            //                                   "</td>" +
            //                               "<td style=\"width: 80%\">" +
            //                               "</td>" +
            //                           "</tr>" +
            //                           "<tr>" +
            //                               "<td style=\"width: 20%\">" +
            //                                   "Date of Payment:" +
            //                                   "</td>" +
            //                               "<td style=\"width: 80%\">" +
            //                                   entryDate +
            //                               "</td>" +
            //                           "</tr>" +
            //                           "<tr>" +
            //                               "<td colspan=\"2\">" +
            //                                  " &nbsp;</td>" +
            //                           "</tr>" +
            //                            "<tr>" +
            //                                "<td colspan=\"2\">" +
            //                                    "Proposal Payment Receipt will be posted to your policy address in due course.</td>" +
            //                            "</tr>";
            //    }
            //    else if ((propObj.checkRecptType(receiptNo)).Equals("P"))
            //    {
            //        loggerr.write_log("P");
            //        content1 = content1 + "<tr>" +
            //                                "<td colspan=\"2\">" +
            //                                    "We have received your payment of Rs." +
            //                                    amount.ToString("N2") +
            //                                    "&nbsp;(Policy Fee)under the reference no:&nbsp;" +
            //                                    receiptNo +
            //                                    "&nbsp;for a new proposal number " +
            //                                    polNum +
            //                                    ".</td>" +
            //                            "</tr>" +
            //                            "<tr>" +
            //                                "<td style=\"width: 80%\" colspan=\"2\">" +
            //                                    "&nbsp;&nbsp;</td>" +
            //                            "</tr>" +
            //                            "<tr>" +
            //                           " <td style=\"font-size: 14pt\" colspan=\"2\"><u>" +
            //                                "Proposal Details</u></td>" +
            //                            "</tr>" +
            //                            "<tr>" +
            //                                "<td style=\"width: 20%\">" +
            //                                    "Proposal Number:" +
            //                                    "</td>" +
            //                                "<td style=\"width: 80%\">" +
            //                                    polNum +
            //                                "</td>" +
            //                            "</tr>" +
            //                            "<tr>" +
            //                               "<td style=\"width: 20%\">" +
            //                                   "Customer Name:" +
            //                                   "</td>" +
            //                               "<td style=\"width: 80%\">" +
            //                                   custName +
            //                               "</td>" +
            //                           "</tr>" +
            //                           "<tr>" +
            //                               "<td style=\"width: 20%\">" +
            //                                   "</td>" +
            //                               "<td style=\"width: 80%\">" +
            //                               "</td>" +
            //                           "</tr>" +
            //                           "<tr>" +
            //                               "<td style=\"width: 20%\">" +
            //                                   "Date of Payment:" +
            //                                   "</td>" +
            //                               "<td style=\"width: 80%\">" +
            //                                   entryDate +
            //                               "</td>" +
            //                           "</tr>" +
            //                           "<tr>" +
            //                               "<td colspan=\"2\">" +
            //                                  " &nbsp;</td>" +
            //                           "</tr>" +
            //                            "<tr>" +
            //                                "<td colspan=\"2\">" +
            //                                    "Proposal Payment Receipt will be posted to your policy address in due course.</td>" +
            //                            "</tr>";
            //    }

            //}

            if (payType != "D")
            {
                content1 = content1 +
                               "<tr>&nbsp;</tr><tr>" +
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

            //log logger = new log();
            logger.write_log(content2);

            //retValue = true;
        }
        catch (Exception e)
        {
            retValue = false;
            log logger = new log();
            logger.write_log("Failed at LifePayment - send_pay_receipt_email " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }
        return retValue;
    }

    public string depositAdjPending(string polNumber)
    {
        string mesg = "success";              

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getRefPaidDemands = "Select distinct REF_NO" +
                                      " from SLIC_NET_LIFE.INFORCE_POLS_FOR_LIFE_WEB_PAY a, SLIC_NET_LIFE.PAID_POLS_FOR_LIFE_WEB_PAY b" +
                                      " where a.POLNO = b.POLNO" +
                                      " and a.POLNO = :polNum " +
                                      " and a.DEMAND = b.DEMAND" +
                                      " and b.PAID_DATE is not null";

            string refNo = "";
            string paidDate = "";

            using (OracleCommand cmd = new OracleCommand(getRefPaidDemands, oconn))
            {
                cmd.Parameters.AddWithValue("polNum", polNumber);
                OracleDataReader paidRefReader = cmd.ExecuteReader();

                if (paidRefReader.HasRows)
                {
                    while (paidRefReader.Read())
                    {
                        if (!paidRefReader.IsDBNull(0))
                        { refNo = paidRefReader.GetString(0); }

                        string getDeposits = "Select to_char(ENTRY_DATE, 'dd/mm/yyyy')" +
                                             " from SLIC_NET_LIFE.RENEWAL_DETAILS" +
                                             " where RECEIPT_NO = :refNo" +
                                             " and nvl(DEPOSITS, 0) > 0";

                        using (OracleCommand cmd2 = new OracleCommand(getDeposits, oconn))
                        {
                            cmd2.Parameters.AddWithValue("refNo", refNo);
                            OracleDataReader depositReader = cmd2.ExecuteReader();

                            if (depositReader.HasRows)
                            {
                                while (depositReader.Read())
                                {
                                    if (!depositReader.IsDBNull(0))
                                    { paidDate = depositReader.GetString(0); }
                                }

                                mesg = "Regret to inform you that we are compelled to block the system since the payment done on " + paidDate + " has not been adjusted. <br/>We will SMS your contribution, once the system is updated.";

                                depositReader.Close();
                            }
                        }
                    }

                    paidRefReader.Close();
                }
            }

        }
        catch (Exception e)
        {
            mesg = "Error occurred while retrieving policy information";
            log logger = new log();
            logger.write_log("Failed at LifePayment - depositAdjPending " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return mesg;
    }

    public bool insert_revival(string polNum, double amount, string userName, string status, string receiptNo, string cusName, string payType, double duesTotalAmt, double addtAmt, string custEmail, string mobileNo, int seq_num)
    {
        bool returnValue = false;

        string ret = "success";
        //if (payType == "P" && duesTotalAmt > 0)
        //{
        //    ret = depositAdjPending(polNum);
        //}

        if (ret == "success")
        {
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
                    string instRenwDetails = "Insert into SLIC_NET_LIFE.RENEWAL_DETAILS(POL_NUM, PAY_AMT, ENTRY_DATE, USERNAME, STATUS, RECEIPT_NO, CUST_NAME, PAY_TYPE, DUES_AMT, ADDT_AMT, DEPOSITS, PAID_DUES_AMT)" +
                                                            " VALUES(:polNum, :amount, sysdate, :username, :status, :recptNo, :cusName, :payTyp, :dueTotal, :addtPaymnt, 0, 0)";

                    cmd.CommandText = instRenwDetails;

                    OracleParameter oPolNum = new OracleParameter();
                    oPolNum.DbType = DbType.Int32;
                    oPolNum.Value = int.Parse(polNum);
                    oPolNum.ParameterName = "polNum";

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

                    OracleParameter oRecptNo = new OracleParameter();
                    oRecptNo.DbType = DbType.String;
                    oRecptNo.Value = receiptNo;
                    oRecptNo.ParameterName = "recptNo";

                    OracleParameter oCusName = new OracleParameter();
                    oCusName.DbType = DbType.String;
                    oCusName.Value = cusName;
                    oCusName.ParameterName = "cusName";

                    OracleParameter oPayTyp = new OracleParameter();
                    oPayTyp.DbType = DbType.String;
                    oPayTyp.Value = payType;
                    oPayTyp.ParameterName = "payTyp";

                    OracleParameter oDueTotal = new OracleParameter();
                    oDueTotal.DbType = DbType.Double;
                    oDueTotal.Value = duesTotalAmt;
                    oDueTotal.ParameterName = "dueTotal";

                    OracleParameter oAddtPaymnt = new OracleParameter();
                    oAddtPaymnt.DbType = DbType.Double;
                    oAddtPaymnt.Value = addtAmt;
                    oAddtPaymnt.ParameterName = "addtPaymnt";

                    cmd.Parameters.Add(oPolNum);
                    cmd.Parameters.Add(oAmount);
                    cmd.Parameters.Add(oUsername);
                    cmd.Parameters.Add(oStatus);
                    cmd.Parameters.Add(oRecptNo);
                    cmd.Parameters.Add(oCusName);
                    cmd.Parameters.Add(oPayTyp);
                    cmd.Parameters.Add(oDueTotal);
                    cmd.Parameters.Add(oAddtPaymnt);

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    //string updatePaidAmount = "Update slic_net_life.ONLINE_POLICY_REVIVALS" +
                    //                                          " set PAID_AMOUNT = PAID_AMOUNT + :amount" +
                    //                                          " where POLNO = :polNo" +
                    //                                          " and SEQ_NO = :seq_nmbr";

                    //cmd.CommandText = updatePaidAmount;

                    //OracleParameter oPaidAmount = new OracleParameter();
                    //oPaidAmount.DbType = DbType.Double;
                    //oPaidAmount.Value = amount;
                    //oPaidAmount.ParameterName = "amount";

                    //OracleParameter oPolNo = new OracleParameter();
                    //oPolNo.DbType = DbType.Int32;
                    //oPolNo.Value = int.Parse(polNum);
                    //oPolNo.ParameterName = "polNo";

                    //OracleParameter oSeqNo = new OracleParameter();
                    //oSeqNo.DbType = DbType.Int32;
                    //oSeqNo.Value = seq_num;
                    //oSeqNo.ParameterName = "seq_nmbr";

                    //cmd.Parameters.Add(oPaidAmount);
                    //cmd.Parameters.Add(oPolNo);
                    //cmd.Parameters.Add(oSeqNo);

                    //cmd.ExecuteNonQuery();
                    //cmd.Parameters.Clear();                    

                    trans.Commit();
                    returnValue = true;
                }

            }
            catch (Exception e)
            {
                trans.Rollback();
                log logger = new log();
                logger.write_log("Failed at LifePayment - insert_revival: " + e.ToString());
            }
            finally
            {
                if (oconn.State == ConnectionState.Open)
                {
                    oconn.Close();
                }
            }
        }

        return returnValue;
    }

    public bool insert_loan_payments(string polNum, double amount, string userName, string status, string receiptNo, string cusName, string payType, double duesTotalAmt, double addtAmt, string custEmail, string mobileNo)
    {
        bool returnValue = false;

        string ret = "success";
        //if (payType == "P" && duesTotalAmt > 0)
        //{
        //    ret = depositAdjPending(polNum);
        //}

        if (ret == "success")
        {
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
                    string instRenwDetails = "Insert into SLIC_NET_LIFE.RENEWAL_DETAILS(POL_NUM, PAY_AMT, ENTRY_DATE, USERNAME, STATUS, RECEIPT_NO, CUST_NAME, PAY_TYPE, DUES_AMT, ADDT_AMT)" +
                                                            " VALUES(:polNum, :amount, sysdate, :username, :status, :recptNo, :cusName, :payTyp, :dueTotal, :addtPaymnt)";

                    cmd.CommandText = instRenwDetails;

                    OracleParameter oPolNum = new OracleParameter();
                    oPolNum.DbType = DbType.Int32;
                    oPolNum.Value = int.Parse(polNum);
                    oPolNum.ParameterName = "polNum";

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

                    OracleParameter oRecptNo = new OracleParameter();
                    oRecptNo.DbType = DbType.String;
                    oRecptNo.Value = receiptNo;
                    oRecptNo.ParameterName = "recptNo";

                    OracleParameter oCusName = new OracleParameter();
                    oCusName.DbType = DbType.String;
                    oCusName.Value = cusName;
                    oCusName.ParameterName = "cusName";

                    OracleParameter oPayTyp = new OracleParameter();
                    oPayTyp.DbType = DbType.String;
                    oPayTyp.Value = payType;
                    oPayTyp.ParameterName = "payTyp";

                    OracleParameter oDueTotal = new OracleParameter();
                    oDueTotal.DbType = DbType.Double;
                    oDueTotal.Value = duesTotalAmt;
                    oDueTotal.ParameterName = "dueTotal";

                    OracleParameter oAddtPaymnt = new OracleParameter();
                    oAddtPaymnt.DbType = DbType.Double;
                    oAddtPaymnt.Value = addtAmt;
                    oAddtPaymnt.ParameterName = "addtPaymnt";

                    cmd.Parameters.Add(oPolNum);
                    cmd.Parameters.Add(oAmount);
                    cmd.Parameters.Add(oUsername);
                    cmd.Parameters.Add(oStatus);
                    cmd.Parameters.Add(oRecptNo);
                    cmd.Parameters.Add(oCusName);
                    cmd.Parameters.Add(oPayTyp);
                    cmd.Parameters.Add(oDueTotal);
                    cmd.Parameters.Add(oAddtPaymnt);

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    //string updatePaidAmount = "Update slic_net_life.ONLINE_POLICY_REVIVALS" +
                    //                                          " set PAID_AMOUNT = PAID_AMOUNT + :amount" +
                    //                                          " where POLNO = :polNo" +
                    //                                          " and SEQ_NO = :seq_nmbr";

                    //cmd.CommandText = updatePaidAmount;

                    //OracleParameter oPaidAmount = new OracleParameter();
                    //oPaidAmount.DbType = DbType.Double;
                    //oPaidAmount.Value = amount;
                    //oPaidAmount.ParameterName = "amount";

                    //OracleParameter oPolNo = new OracleParameter();
                    //oPolNo.DbType = DbType.Int32;
                    //oPolNo.Value = int.Parse(polNum);
                    //oPolNo.ParameterName = "polNo";

                    //OracleParameter oSeqNo = new OracleParameter();
                    //oSeqNo.DbType = DbType.Int32;
                    //oSeqNo.Value = seq_num;
                    //oSeqNo.ParameterName = "seq_nmbr";

                    //cmd.Parameters.Add(oPaidAmount);
                    //cmd.Parameters.Add(oPolNo);
                    //cmd.Parameters.Add(oSeqNo);

                    //cmd.ExecuteNonQuery();
                    //cmd.Parameters.Clear();

                    trans.Commit();
                    returnValue = true;
                }

            }
            catch (Exception e)
            {
                trans.Rollback();
                log logger = new log();
                logger.write_log("Failed at LifePayment - insert_loan_payments: " + e.ToString());
            }
            finally
            {
                if (oconn.State == ConnectionState.Open)
                {
                    oconn.Close();
                }
            }
        }

        return returnValue;
    }

    public bool send_pay_phs_email()
    {
        bool retValue = false;
        string email = "";
        try
        {

            email = "phs@srilankainsurance.com"; // Life PHS address

            string subject = "SLIC Payment Notification - Policy Revival";

            string content1 = "<table>" +
                                "<tbody>" +
                                "<tr>" +
                                    "<td colspan=\"2\">" +
                                        "Dear Life PHS,</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td colspan=\"2\">" +
                                        "&nbsp;</td>" +
                                "</tr>";




            content1 = content1 + "<tr>" +
                                    "<td colspan=\"2\">" +
                                        custName +
                                        "&nbsp;&nbsp;(Policy No:" +
                                        polNum +
                                        ")&nbsp;paid arrears premium on&nbsp;" + entryDate +
                                        "&nbsp;for the revival.</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td colspan=\"2\">" +
                                        "Please check." +
                                        "</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td style=\"width: 80%\" colspan=\"2\">" +
                                        "&nbsp;&nbsp;</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td style=\"width: 80%\" colspan=\"2\">" +
                                        "&nbsp;&nbsp;</td>" +
                                "</tr>"
                // "<tr>" +
                //" <td style=\"font-size: 14pt\" colspan=\"2\"><u>" +
                //     "Policy Details</u></td>" +
                // "</tr>" +
                // "<tr>" +
                //     "<td style=\"width: 20%\">" +
                //         "Policy Number:" +
                //         "</td>" +
                //     "<td style=\"width: 80%\">" +
                //         polNum +
                //     "</td>" +
                // "</tr>" +
                // "<tr>" +
                //    "<td style=\"width: 20%\">" +
                //        "Customer Name:" +
                //        "</td>" +
                //    "<td style=\"width: 80%\">" +
                //        custName +
                //    "</td>" +
                //"</tr>" +
                //"<tr>" +
                //    "<td style=\"width: 20%\">" +
                //        "</td>" +
                //    "<td style=\"width: 80%\">" +
                //    "</td>" +
                //"</tr>" +
                //"<tr>" +
                //    "<td style=\"width: 20%\">" +
                //        "Date of Payment:" +
                //        "</td>" +
                //    "<td style=\"width: 80%\">" +
                //        entryDate +
                //    "</td>" +
                //"</tr>" +
                //"<tr>" +
                //    "<td colspan=\"2\">" +
                //       " &nbsp;</td>" +
                //"</tr>" +
                // "<tr>" +
                //     "<td colspan=\"2\">" +
                //         "Please do the needful for reviving the policy.</td>" +
                // "</tr>"
                               ;



            content1 = content1 +
                               "<tr>&nbsp;</tr><tr>" +
                                   "<td colspan=\"2\">" +
                                       "Thanking you,<br> Sri Lanka Insurance - Customer portal.</td>" +
                               "</tr>" +
                               "<tr>" +
                                   "<td colspan=\"2\">" +
                                       "&nbsp;</td>" +
                               "</tr>" +
                           "</tbody></table>";

            /*"Dear Customer, <br/> We have received your payment of Rs. " + amount.ToString("N2") +
                          " under the reference no: " + receiptNo + " regarding premium payment for " + polTypName + " policy.";*/


            string content2 = content1;

            Db_Email emailSender = new Db_Email();
            retValue = emailSender.send_html_email(email, subject, content1, content2);
            LogMail logger = new LogMail();
            logger.write_log("To: " + email + " Subject: " + subject);

            //log logger = new log();
            logger.write_log(content2);

            //retValue = true;
        }
        catch (Exception e)
        {
            retValue = false;
            log logger = new log();
            logger.write_log("Failed at LifePayment - send_pay_phs_email " + e.ToString());
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

    //public bool insert_proposal_payments(string propNum, double amount, string userName, string status, string receiptNo, string cusName, string payType, string custEmail, string mobileNo, string receiptNo_polFee)
    //{
    //    bool returnValue = false;

    //    string ret = "success";
    //    //if (payType == "P" && duesTotalAmt > 0)
    //    //{
    //    //    ret = depositAdjPending(polNum);
    //    //}

    //    if (ret == "success")
    //    {
    //        if (oconn.State != ConnectionState.Open)
    //        {
    //            oconn.Open();
    //        }
    //        OracleCommand cmd = oconn.CreateCommand();
    //        OracleTransaction trans = oconn.BeginTransaction();
    //        cmd.Transaction = trans;
    //        try
    //        {

    //            using (cmd)
    //            {
    //                string instRenwDetails = "Insert into SLIC_NET_LIFE.RENEWAL_DETAILS(POL_NUM, PAY_AMT, ENTRY_DATE, USERNAME, STATUS, RECEIPT_NO, CUST_NAME, PAY_TYPE, DEPOSITS, DUES_AMT, ADDT_AMT, PAID_DUES_AMT)" +
    //                                                        " VALUES(:propNm, :amount, sysdate, :username, :status, :recptNo, :cusName, :payTyp, 0, 0, 0, 0)";

    //                cmd.CommandText = instRenwDetails;

    //                OracleParameter oPolNum = new OracleParameter();
    //                oPolNum.DbType = DbType.Int32;
    //                oPolNum.Value = long.Parse(propNum);
    //                oPolNum.ParameterName = "propNm";

    //                OracleParameter oAmount = new OracleParameter();
    //                oAmount.DbType = DbType.Double;
    //                if (!receiptNo_polFee.Equals(""))
    //                {
    //                    oAmount.Value = amount - 250;
    //                }
    //                else
    //                {
    //                    oAmount.Value = amount;
    //                }
    //                oAmount.ParameterName = "amount";

    //                OracleParameter oUsername = new OracleParameter();
    //                oUsername.Value = userName;
    //                oUsername.ParameterName = "username";

    //                OracleParameter oStatus = new OracleParameter();
    //                oStatus.DbType = DbType.String;
    //                oStatus.Value = status;
    //                oStatus.ParameterName = "status";

    //                OracleParameter oRecptNo = new OracleParameter();
    //                oRecptNo.DbType = DbType.String;
    //                oRecptNo.Value = receiptNo;
    //                oRecptNo.ParameterName = "recptNo";

    //                OracleParameter oCusName = new OracleParameter();
    //                oCusName.DbType = DbType.String;
    //                oCusName.Value = cusName;
    //                oCusName.ParameterName = "cusName";

    //                OracleParameter oPayTyp = new OracleParameter();
    //                oPayTyp.DbType = DbType.String;
    //                oPayTyp.Value = payType;
    //                oPayTyp.ParameterName = "payTyp";

    //                cmd.Parameters.Add(oPolNum);
    //                cmd.Parameters.Add(oAmount);
    //                cmd.Parameters.Add(oUsername);
    //                cmd.Parameters.Add(oStatus);
    //                cmd.Parameters.Add(oRecptNo);
    //                cmd.Parameters.Add(oCusName);
    //                cmd.Parameters.Add(oPayTyp);

    //                cmd.ExecuteNonQuery();
    //                cmd.Parameters.Clear();

    //                //string insertRctData = "insert into SLIC_NET_LIFE.ONLINE_PROP_RECPT_NOS (MAIN_RECT_NO, POLICY_FEE_RECT_NO) values (:mainRctNo, :polFeeRectNo)";

    //                string insertRctData = "";
    //                if (!receiptNo_polFee.Equals(""))
    //                {
    //                    insertRctData = "insert into SLIC_NET_LIFE.ONLINE_PROP_RECPT_NOS (MAIN_RECT_NO, POLICY_FEE_RECT_NO, POLICY_FEE_AMOUNT) values (:mainRctNo, :polFeeRectNo, 250)";
    //                }
    //                else
    //                {
    //                    insertRctData = "insert into SLIC_NET_LIFE.ONLINE_PROP_RECPT_NOS (MAIN_RECT_NO, POLICY_FEE_RECT_NO) values (:mainRctNo, :polFeeRectNo)";
    //                }

    //                cmd.CommandText = insertRctData;

    //                OracleParameter oRctNo = new OracleParameter();
    //                oRctNo.DbType = DbType.String;
    //                oRctNo.Value = receiptNo;
    //                oRctNo.ParameterName = "mainRctNo";

    //                OracleParameter oPolFeeRctNo = new OracleParameter();
    //                oPolFeeRctNo.DbType = DbType.String;
    //                oPolFeeRctNo.Value = receiptNo_polFee;
    //                oPolFeeRctNo.ParameterName = "polFeeRectNo";

    //                cmd.Parameters.Add(oRctNo);
    //                cmd.Parameters.Add(oPolFeeRctNo);

    //                cmd.ExecuteNonQuery();
    //                cmd.Parameters.Clear();

    //                # region Insert policy Fee record

    //                if (!receiptNo_polFee.Equals(""))
    //                {
    //                    string instPolFeeRec = "Insert into SLIC_NET_LIFE.RENEWAL_DETAILS(POL_NUM, PAY_AMT, ENTRY_DATE, USERNAME, STATUS, RECEIPT_NO, CUST_NAME, PAY_TYPE, DEPOSITS, DUES_AMT, ADDT_AMT, PAID_DUES_AMT)" +
    //                                                            " VALUES(:propNm, :amount, sysdate, :username, :status, :recptNo, :cusName, :payTyp, 0, 0 , 0, 0)";

    //                    cmd.CommandText = instPolFeeRec;

    //                    OracleParameter oPolNum2 = new OracleParameter();
    //                    oPolNum2.DbType = DbType.Int32;
    //                    oPolNum2.Value = long.Parse(propNum);
    //                    oPolNum2.ParameterName = "propNm";

    //                    OracleParameter oAmount2 = new OracleParameter();
    //                    oAmount2.DbType = DbType.Double;
    //                    oAmount2.Value = 250;
    //                    oAmount2.ParameterName = "amount";

    //                    OracleParameter oUsername2 = new OracleParameter();
    //                    oUsername2.Value = userName;
    //                    oUsername2.ParameterName = "username";

    //                    OracleParameter oStatus2 = new OracleParameter();
    //                    oStatus2.DbType = DbType.String;
    //                    oStatus2.Value = status;
    //                    oStatus2.ParameterName = "status";

    //                    OracleParameter oRecptNo2 = new OracleParameter();
    //                    oRecptNo2.DbType = DbType.String;
    //                    oRecptNo2.Value = receiptNo_polFee;
    //                    oRecptNo2.ParameterName = "recptNo";

    //                    OracleParameter oCusName2 = new OracleParameter();
    //                    oCusName2.DbType = DbType.String;
    //                    oCusName2.Value = cusName;
    //                    oCusName2.ParameterName = "cusName";

    //                    OracleParameter oPayTyp2 = new OracleParameter();
    //                    oPayTyp2.DbType = DbType.String;
    //                    oPayTyp2.Value = payType;
    //                    oPayTyp2.ParameterName = "payTyp";

    //                    cmd.Parameters.Add(oPolNum2);
    //                    cmd.Parameters.Add(oAmount2);
    //                    cmd.Parameters.Add(oUsername2);
    //                    cmd.Parameters.Add(oStatus2);
    //                    cmd.Parameters.Add(oRecptNo2);
    //                    cmd.Parameters.Add(oCusName2);
    //                    cmd.Parameters.Add(oPayTyp2);

    //                    cmd.ExecuteNonQuery();
    //                    cmd.Parameters.Clear();
                        
    //                }

    //                # endregion

    //                trans.Commit();
    //                returnValue = true;
    //            }

    //        }
    //        catch (Exception e)
    //        {
    //            trans.Rollback();
    //            log logger = new log();
    //            logger.write_log("Failed at LifePayment - insert_proposal_payments: " + e.ToString());
    //        }
    //        finally
    //        {
    //            if (oconn.State == ConnectionState.Open)
    //            {
    //                oconn.Close();
    //            }
    //        }
    //    }

    //    return returnValue;
    //}

    public bool insert_proposal_payments(string propNum, double amount, string userName, string status, string receiptNo, string cusName, string payType, string custEmail, string mobileNo, string receiptNo_polFee)
    {
        bool returnValue = false;

        string ret = "success";
        //if (payType == "P" && duesTotalAmt > 0)
        //{
        //    ret = depositAdjPending(polNum);
        //}

	LifeProposal ltProp = new LifeProposal();
        double polFee = ltProp.getPolicyFee();

        if (ret == "success")
        {
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
                    string instRenwDetails = "Insert into SLIC_NET_LIFE.RENEWAL_DETAILS(POL_NUM, PAY_AMT, ENTRY_DATE, USERNAME, STATUS, RECEIPT_NO, CUST_NAME, PAY_TYPE, DEPOSITS, DUES_AMT, ADDT_AMT, PAID_DUES_AMT)" +
                                                            " VALUES(:propNm, :amount, sysdate, :username, :status, :recptNo, :cusName, :payTyp, 0, 0, 0, 0)";

                    cmd.CommandText = instRenwDetails;

                    OracleParameter oPolNum = new OracleParameter();
                    //oPolNum.DbType = DbType.Int32;
                    oPolNum.DbType = DbType.Int64;
                    oPolNum.Value = long.Parse(propNum);
                    oPolNum.ParameterName = "propNm";

                    OracleParameter oAmount = new OracleParameter();
                    oAmount.DbType = DbType.Double;
                    if (!receiptNo_polFee.Equals(""))
                    {
                        //oAmount.Value = amount - 250;
			oAmount.Value = amount - polFee;
                    }
                    else
                    {
                        oAmount.Value = amount;
                    }
                    oAmount.ParameterName = "amount";

                    OracleParameter oUsername = new OracleParameter();
                    oUsername.Value = userName;
                    oUsername.ParameterName = "username";

                    OracleParameter oStatus = new OracleParameter();
                    oStatus.DbType = DbType.String;
                    oStatus.Value = status;
                    oStatus.ParameterName = "status";

                    OracleParameter oRecptNo = new OracleParameter();
                    oRecptNo.DbType = DbType.String;
                    oRecptNo.Value = receiptNo;
                    oRecptNo.ParameterName = "recptNo";

                    OracleParameter oCusName = new OracleParameter();
                    oCusName.DbType = DbType.String;
                    oCusName.Value = cusName;
                    oCusName.ParameterName = "cusName";

                    OracleParameter oPayTyp = new OracleParameter();
                    oPayTyp.DbType = DbType.String;
                    oPayTyp.Value = payType;
                    oPayTyp.ParameterName = "payTyp";

                    cmd.Parameters.Add(oPolNum);
                    cmd.Parameters.Add(oAmount);
                    cmd.Parameters.Add(oUsername);
                    cmd.Parameters.Add(oStatus);
                    cmd.Parameters.Add(oRecptNo);
                    cmd.Parameters.Add(oCusName);
                    cmd.Parameters.Add(oPayTyp);

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    //string insertRctData = "insert into SLIC_NET_LIFE.ONLINE_PROP_RECPT_NOS (MAIN_RECT_NO, POLICY_FEE_RECT_NO) values (:mainRctNo, :polFeeRectNo)";

                    string insertRctData = "";
                    if (!receiptNo_polFee.Equals(""))
                    {
                        insertRctData = "insert into SLIC_NET_LIFE.ONLINE_PROP_RECPT_NOS (MAIN_RECT_NO, POLICY_FEE_RECT_NO, POLICY_FEE_AMOUNT) values (:mainRctNo, :polFeeRectNo, :polFeeAmnt)";
                    }
                    else
                    {
                        insertRctData = "insert into SLIC_NET_LIFE.ONLINE_PROP_RECPT_NOS (MAIN_RECT_NO, POLICY_FEE_RECT_NO) values (:mainRctNo, :polFeeRectNo)";
                    }

                    cmd.CommandText = insertRctData;

                    OracleParameter oRctNo = new OracleParameter();
                    oRctNo.DbType = DbType.String;
                    oRctNo.Value = receiptNo;
                    oRctNo.ParameterName = "mainRctNo";

                    OracleParameter oPolFeeRctNo = new OracleParameter();
                    oPolFeeRctNo.DbType = DbType.String;
                    oPolFeeRctNo.Value = receiptNo_polFee;
                    oPolFeeRctNo.ParameterName = "polFeeRectNo";

		    OracleParameter oPolFeeAmnt = new OracleParameter();
                    oPolFeeAmnt.DbType = DbType.Double;
                    oPolFeeAmnt.Value = polFee;
                    oPolFeeAmnt.ParameterName = "polFeeAmnt";

                    cmd.Parameters.Add(oRctNo);
                    cmd.Parameters.Add(oPolFeeRctNo);
		    //cmd.Parameters.Add(oPolFeeAmnt);

		    if (!receiptNo_polFee.Equals(""))
                    {
                        cmd.Parameters.Add(oPolFeeAmnt);
                    }

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    # region Insert policy Fee record

                    if (!receiptNo_polFee.Equals(""))
                    {
                        string instPolFeeRec = "Insert into SLIC_NET_LIFE.RENEWAL_DETAILS(POL_NUM, PAY_AMT, ENTRY_DATE, USERNAME, STATUS, RECEIPT_NO, CUST_NAME, PAY_TYPE, DEPOSITS, DUES_AMT, ADDT_AMT, PAID_DUES_AMT)" +
                                                                " VALUES(:propNm, :amount, sysdate, :username, :status, :recptNo, :cusName, :payTyp, 0, 0 , 0, 0)";

                        cmd.CommandText = instPolFeeRec;

                        OracleParameter oPolNum2 = new OracleParameter();
                        //oPolNum2.DbType = DbType.Int32;
                        oPolNum2.DbType = DbType.Int64;
                        oPolNum2.Value = long.Parse(propNum);
                        oPolNum2.ParameterName = "propNm";

                        OracleParameter oAmount2 = new OracleParameter();
                        oAmount2.DbType = DbType.Double;
                        oAmount2.Value = polFee;  //250;
                        oAmount2.ParameterName = "amount";

                        OracleParameter oUsername2 = new OracleParameter();
                        oUsername2.Value = userName;
                        oUsername2.ParameterName = "username";

                        OracleParameter oStatus2 = new OracleParameter();
                        oStatus2.DbType = DbType.String;
                        oStatus2.Value = status;
                        oStatus2.ParameterName = "status";

                        OracleParameter oRecptNo2 = new OracleParameter();
                        oRecptNo2.DbType = DbType.String;
                        oRecptNo2.Value = receiptNo_polFee;
                        oRecptNo2.ParameterName = "recptNo";

                        OracleParameter oCusName2 = new OracleParameter();
                        oCusName2.DbType = DbType.String;
                        oCusName2.Value = cusName;
                        oCusName2.ParameterName = "cusName";

                        OracleParameter oPayTyp2 = new OracleParameter();
                        oPayTyp2.DbType = DbType.String;
                        oPayTyp2.Value = "M";
                        oPayTyp2.ParameterName = "payTyp";

                        cmd.Parameters.Add(oPolNum2);
                        cmd.Parameters.Add(oAmount2);
                        cmd.Parameters.Add(oUsername2);
                        cmd.Parameters.Add(oStatus2);
                        cmd.Parameters.Add(oRecptNo2);
                        cmd.Parameters.Add(oCusName2);
                        cmd.Parameters.Add(oPayTyp2);

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                    }

                    # endregion

                    trans.Commit();
                    returnValue = true;
                }

            }
            catch (Exception e)
            {
                trans.Rollback();
                log logger = new log();
                logger.write_log("Failed at LifePayment - insert_proposal_payments: " + e.ToString());
            }
            finally
            {
                if (oconn.State == ConnectionState.Open)
                {
                    oconn.Close();
                }
            }
        }

        return returnValue;
    }


    // Updated on 2022.06.09- Loan payments

    public bool insert_loan_payments(string polNum, double amount, string userName, string status, string receiptNo, string cusName, string payType, double duesTotalAmt, double addtAmt, string custEmail, string mobileNo, long loanNum, string nic)
    {
        bool returnValue = false;

        string ret = "success";
        //if (payType == "P" && duesTotalAmt > 0)
        //{
        //    ret = depositAdjPending(polNum);
        //}

        if (ret == "success")
        {
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
                    string instRenwDetails = "Insert into SLIC_NET_LIFE.RENEWAL_DETAILS(POL_NUM, PAY_AMT, ENTRY_DATE, USERNAME, STATUS, RECEIPT_NO, CUST_NAME, PAY_TYPE, DUES_AMT, ADDT_AMT, LON_NUM, DEPOSITS, PAID_DUES_AMT)" +
                                                            " VALUES(:polNum, :amount, sysdate, :username, :status, :recptNo, :cusName, :payTyp, :dueTotal, :addtPaymnt, :lonNum, 0, 0)";

                    cmd.CommandText = instRenwDetails;

                    OracleParameter oPolNum = new OracleParameter();
                    oPolNum.DbType = DbType.Int32;
                    oPolNum.Value = int.Parse(polNum);
                    oPolNum.ParameterName = "polNum";

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

                    OracleParameter oRecptNo = new OracleParameter();
                    oRecptNo.DbType = DbType.String;
                    oRecptNo.Value = receiptNo;
                    oRecptNo.ParameterName = "recptNo";

                    OracleParameter oCusName = new OracleParameter();
                    oCusName.DbType = DbType.String;
                    oCusName.Value = cusName;
                    oCusName.ParameterName = "cusName";

                    OracleParameter oPayTyp = new OracleParameter();
                    oPayTyp.DbType = DbType.String;
                    oPayTyp.Value = payType;
                    oPayTyp.ParameterName = "payTyp";

                    OracleParameter oDueTotal = new OracleParameter();
                    oDueTotal.DbType = DbType.Double;
                    oDueTotal.Value = duesTotalAmt;
                    oDueTotal.ParameterName = "dueTotal";

                    OracleParameter oAddtPaymnt = new OracleParameter();
                    oAddtPaymnt.DbType = DbType.Double;
                    oAddtPaymnt.Value = addtAmt;
                    oAddtPaymnt.ParameterName = "addtPaymnt";

                    OracleParameter oAddtLoanNum = new OracleParameter();
                    oAddtLoanNum.DbType = DbType.Int64;
                    oAddtLoanNum.Value = loanNum;
                    oAddtLoanNum.ParameterName = "lonNum";

                    //OracleParameter oAddtNIC = new OracleParameter();
                    //oAddtNIC.DbType = DbType.Double;
                    //oAddtNIC.Value = nic;
                    //oAddtNIC.ParameterName = "addtPaymnt";

                    cmd.Parameters.Add(oPolNum);
                    cmd.Parameters.Add(oAmount);
                    cmd.Parameters.Add(oUsername);
                    cmd.Parameters.Add(oStatus);
                    cmd.Parameters.Add(oRecptNo);
                    cmd.Parameters.Add(oCusName);
                    cmd.Parameters.Add(oPayTyp);
                    cmd.Parameters.Add(oDueTotal);
                    cmd.Parameters.Add(oAddtPaymnt);
                    cmd.Parameters.Add(oAddtLoanNum);

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    //string getLoanEntered = "Select * " +
                    //                         " from SLIC_NET_LIFE.online_paid_loan_details" +
                    //                         " where POLICY_NO = :polNo " +
                    //                         " and LOAN_NO = :loanNo ";

                    //using (OracleCommand cmd2 = new OracleCommand(getLoanEntered, oconn))
                    //{
                        //cmd2.Parameters.AddWithValue("polNo", polNum);
                        //cmd2.Parameters.AddWithValue("loanNo", loanNum);
                        //OracleDataReader dtReader = cmd2.ExecuteReader();

                        //if (!dtReader.HasRows)
                        //{

                            string insertLoanDetails = "insert into slic_net_life.online_paid_loan_details (POLICY_NO, LOAN_NO, USER_NAME, EMAIL_ADDRESS, MOBILE_NUMBER, NIC, RECEIPT_NO) " +
                                                " values (:vpolNum, :vloanNo, :vuserName, :vemail, :vmobile, :vnic, :rctIn)";

                            cmd.CommandText = insertLoanDetails;

                            OracleParameter ovPolNum = new OracleParameter();
                            ovPolNum.DbType = DbType.Int32;
                            ovPolNum.Value = int.Parse(polNum);
                            ovPolNum.ParameterName = "vpolNum";

                            OracleParameter ovLoanNo = new OracleParameter();
                            ovLoanNo.DbType = DbType.Int64;
                            ovLoanNo.Value = loanNum;
                            ovLoanNo.ParameterName = "vloanNo";

                            OracleParameter ovUserName = new OracleParameter();
                            ovUserName.DbType = DbType.String;
                            ovUserName.Value = userName;
                            ovUserName.ParameterName = "vuserName";

                            OracleParameter ovEmail = new OracleParameter();
                            ovEmail.DbType = DbType.String;
                            ovEmail.Value = custEmail;
                            ovEmail.ParameterName = "vemail";

                            OracleParameter ovMobile = new OracleParameter();
                            ovMobile.DbType = DbType.String;
                            ovMobile.Value = mobileNo;
                            ovMobile.ParameterName = "vmobile";

                            OracleParameter ovNIC = new OracleParameter();
                            ovNIC.DbType = DbType.String;
                            ovNIC.Value = nic;
                            ovNIC.ParameterName = "vnic";

                    OracleParameter ovRectNum = new OracleParameter();
                    ovRectNum.DbType = DbType.String;
                    ovRectNum.Value = receiptNo;
                    ovRectNum.ParameterName = "rctIn";

                    cmd.Parameters.Add(ovPolNum);
                            cmd.Parameters.Add(ovLoanNo);
                            cmd.Parameters.Add(ovUserName);
                            cmd.Parameters.Add(ovEmail);
                            cmd.Parameters.Add(ovMobile);
                            cmd.Parameters.Add(ovNIC);
                    cmd.Parameters.Add(ovRectNum);

                    cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        //}
                        //dtReader.Close();
                    //}

                    trans.Commit();
                    returnValue = true;
                }

            }
            catch (Exception e)
            {
                trans.Rollback();
                log logger = new log();
                logger.write_log("Failed at LifePayment - insert_revival: " + e.ToString());
            }
            finally
            {
                if (oconn.State == ConnectionState.Open)
                {
                    oconn.Close();
                }
            }
        }

        return returnValue;
    }
}