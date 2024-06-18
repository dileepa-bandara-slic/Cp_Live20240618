using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using System.Data.Odbc;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

/// <summary>
/// Summary description for Proposal
/// </summary>
public class Proposal
{

    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);

    public string policyType { get; private set; }
    public string refNo { get; private set; }
    public string fullName { get; private set; }
    public string Address1 { get; private set; }
    public string Address2 { get; private set; }
    public string Address3 { get; private set; }
    public string Address4 { get; private set; }
    public string mobileNo { get; private set; }
    public string homeNo { get; private set; }
    public string officeNo { get; private set; }
    public string email { get; private set; }
    public string nic { get; private set; }
    public string locAddress1 { get; private set; }
    public string locAddress2 { get; private set; }
    public string locAddress3 { get; private set; }
    public string locAddress4 { get; private set; }
    public string assignee { get; private set; }
    public string damagedBefore { get; private set; }
    public string rejectedBefore { get; private set; }
    public string rejectReason { get; private set; }
    public string plan { get; private set; }
    public double sumAssured { get; private set; }
    public double annualPremium { get; private set; }
    public double adminFee { get; private set; }
    public double polFee { get; private set; }
    public double NBT { get; private set; }
    public double VAT { get; private set; }
    public string comenmentDate { get; private set; }
    public string status { get; private set; }
    public string entryDate { get; private set; }
    public string userName { get; private set; }
    public string title { get; private set; }
    public string productID { get; private set; }
    public int PG_RET_CODE { get; private set; }
    public int PG_RSN_CODE { get; private set; }
    public string product_Name { get; private set; }
    public string endDate { get; private set; }
    public string damageReason { get; private set; }
    public string profession { get; private set; }

    public string policy_no { get; private set; }
    public string nature_of_occupation { get; private set; }
    public string employer_name { get; private set; }
    public string passport { get; private set; }


	public Proposal()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Proposal( string proposal_ID)
    {
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string sql = "SELECT POL_TYPE, REF_NO, FULL_NAME, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, MOBILE_NUMBER, HOME_NUMBER, OFFICE_NUMBER, EMAIL, NIC, LOC_ADRS1, LOC_ADRS2, LOC_ADRS3, LOC_ADRS4, "+
                         " ASSIGNEE, DAMAGED_BEFORE, REJCTED_BEFORE, REJCT_REASON, PLAN, SUM_ASSURD, ANU_PREMIUM, ADMIN_FEE, POL_FEE, NBT, VAT, to_char(COM_DATE, 'yyyy/mm/dd') AS COM_DATE, STATUS, "+
                         " to_char(ENTRY_DATE, 'yyyy/mm/dd') AS ENTRY_DATE, USERNAME, TITLE, D.PRODUCT_ID, PG_RET_CODE, PG_RSN_CODE, PTSNA, to_char(END_DATE, 'yyyy/mm/dd') AS END_DATE," +
                         " DMG_REASON, PROFESSION, POLICY_NUMBER, NATUR_OF_OCCUP, EMPLOYER_NAME, PASSPORT_NO FROM SLIC_NET.PROPOSAL_DETAILS D, GENPAY.POLTYP P WHERE REF_NO = :refno AND P.PTDEP = D.POL_TYPE AND P.PTTYP = D.PRODUCT_ID";

            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {
                OracleParameter opRefNo = new OracleParameter();
                opRefNo.Value = proposal_ID;
                opRefNo.ParameterName = "refno";

                cmd.Parameters.Add(opRefNo);

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    policyType = reader[0].ToString().Trim();
                    refNo = reader[1].ToString().Trim();
                    fullName = reader[2].ToString().Trim();
                    Address1 = reader[3].ToString().Trim();
                    Address2 = reader[4].ToString().Trim();
                    Address3 = reader[5].ToString().Trim();
                    Address4 = reader[6].ToString().Trim();
                    mobileNo = reader[7].ToString().Trim();
                    homeNo = reader[8].ToString().Trim();
                    officeNo = reader[9].ToString().Trim();
                    email = reader[10].ToString().Trim();
                    nic = reader[11].ToString().Trim();
                    locAddress1 = reader[12].ToString().Trim();
                    locAddress2 = reader[13].ToString().Trim();
                    locAddress3 = reader[14].ToString().Trim();
                    locAddress4 = reader[15].ToString().Trim();
                    assignee = reader[16].ToString().Trim();
                    damagedBefore = reader[17].ToString().Trim();
                    rejectedBefore = reader[18].ToString().Trim();
                    rejectReason = reader[19].ToString().Trim();
                    plan = reader[20].ToString().Trim();

                    if (!reader.IsDBNull(21))
                    {
                        sumAssured = Convert.ToDouble(reader[21].ToString().Trim());
                    }
                    else
                    {
                        sumAssured = 0;
                    }

                    if (!reader.IsDBNull(22))
                    {
                        annualPremium = Convert.ToDouble(reader[22].ToString().Trim());
                    }
                    else
                    {
                        annualPremium = 0;
                    }

                    if (!reader.IsDBNull(23))
                    {
                        adminFee = Convert.ToDouble(reader[23].ToString().Trim());
                    }
                    else
                    {
                        adminFee = 0;
                    }

                    if (!reader.IsDBNull(24))
                    {
                        polFee = Convert.ToDouble(reader[24].ToString().Trim());
                    }
                    else
                    {
                        polFee = 0;
                    }

                    if (!reader.IsDBNull(25))
                    {
                        NBT = Convert.ToDouble(reader[25].ToString().Trim());
                    }
                    else
                    {
                        NBT = 0;
                    }

                    if (!reader.IsDBNull(26))
                    {
                        VAT = Convert.ToDouble(reader[26].ToString().Trim());
                    }
                    else
                    {
                        VAT = 0;
                    }

                    comenmentDate = reader[27].ToString().Trim();
                    status = reader[28].ToString().Trim();
                    entryDate = reader[29].ToString().Trim();
                    userName = reader[30].ToString().Trim();
                    title = reader[31].ToString().Trim();
                    productID = reader[32].ToString().Trim();

                    if (!reader.IsDBNull(33))
                    {
                        try
                        {
                            PG_RET_CODE = Convert.ToInt32(reader[33].ToString().Trim());
                        }
                        catch
                        {
                            PG_RET_CODE = 0;
                        }

                    }
                    else
                    {
                        PG_RET_CODE = 0;
                    }

                    if (!reader.IsDBNull(34))
                    {
                        try
                        {
                            PG_RSN_CODE = Convert.ToInt32(reader[34].ToString().Trim());
                        }
                        catch
                        {
                            PG_RSN_CODE = 0;
                        }
                    }
                    else
                    {
                        PG_RSN_CODE = 0;
                    }

                    product_Name = reader[35].ToString().Trim();
                    endDate = reader[36].ToString().Trim();
                    if (!reader.IsDBNull(37))
                    {
                        damageReason = reader[37].ToString().Trim();
                    }
                    if (!reader.IsDBNull(38))
                    {
                        profession = reader[38].ToString().Trim();
                    }


                    if (!reader.IsDBNull(39))
                    {
                        policy_no = reader[39].ToString().Trim();
                    }

                    if (!reader.IsDBNull(40))
                    {
                        nature_of_occupation = reader[40].ToString().Trim();
                    }

                    if (!reader.IsDBNull(41))
                    {
                        employer_name = reader[41].ToString().Trim();
                    }

                    if (!reader.IsDBNull(42))
                    {
                        passport = reader[42].ToString().Trim();
                    }

                }
                reader.Close();
                cmd.Parameters.Clear();
            }
        }
        catch (Exception ex)
        {
            log logger = new log();
            logger.write_log("Failed at Proposal constructor: " + ex.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
    }

    public bool insert_proposal(string polType, string refNo, string fullName, string adrs1, string adrs2, string adrs3, string adrs4,
                            string mobileNumbr, string homeNumbr, string ofcNumber, string email, string nic, string locAdrs1, 
                            string locAdrs2, string locAdrs3, string locAdrs4, string assignee, string dmgBefore, string rejBefore, 
                            string rejReason, string plan, double sumAssurd, double anuPrem, double admnFee, double polFee, double nbt, double vat,
                            string comDate, string status, string username, string title, string prodId, string endDate, string damgReason, string profession)
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
                string instPropDetails = "Insert into SLIC_NET.PROPOSAL_DETAILS(POL_TYPE, REF_NO, FULL_NAME, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, " +
                                                                                " MOBILE_NUMBER, HOME_NUMBER, OFFICE_NUMBER, EMAIL, NIC, LOC_ADRS1, " +
                                                                                " LOC_ADRS2, LOC_ADRS3, LOC_ADRS4, ASSIGNEE, DAMAGED_BEFORE, REJCTED_BEFORE, " +
                                                                                " REJCT_REASON, PLAN, SUM_ASSURD, ANU_PREMIUM, ADMIN_FEE, POL_FEE, NBT, VAT, COM_DATE, STATUS, ENTRY_DATE, USERNAME, TITLE, PRODUCT_ID, END_DATE, DMG_REASON, PROFESSION)" +
                                                        " VALUES(:polTyp, :refNo, :fullNam, :adrs1, :adrs2, :adrs3, :adrs4, :mobNo, :homeNo, :ofcNo, :email, " +
                                                        " :nic, :lcAdrs1, :lcAdrs2, :lcAdrs3, :lcAdrs4, :assignee, :dmgBefore, :rejBefore, :rejResn, :plan, :sumAssurd, " +
                                                        " :anuPrem, :admnFee, :polFee, :nbt, :vat, :comDate, :status, sysdate, :username, :title, :prodId, :endDate, :dmgRsn, :prof)";

                cmd.CommandText = instPropDetails;

                OracleParameter oPolTyp = new OracleParameter();
                oPolTyp.DbType = DbType.String;
                oPolTyp.Value = polType;
                oPolTyp.ParameterName = "polTyp";

                OracleParameter oRefNo = new OracleParameter();
                oRefNo.DbType = DbType.String;
                oRefNo.Value = refNo;
                oRefNo.ParameterName = "refNo";

                OracleParameter oName = new OracleParameter();
                oName.DbType = DbType.String;
                oName.Value = fullName;
                oName.ParameterName = "fullNam";

                OracleParameter oAdrs1 = new OracleParameter();
                oAdrs1.DbType = DbType.String;
                oAdrs1.Value = adrs1;
                oAdrs1.ParameterName = "adrs1";

                OracleParameter oAdrs2 = new OracleParameter();
                oAdrs2.DbType = DbType.String;
                oAdrs2.Value = adrs2;
                oAdrs2.ParameterName = "adrs2";

                OracleParameter oAdrs3 = new OracleParameter();
                oAdrs3.DbType = DbType.String;
                oAdrs3.Value = adrs3;
                oAdrs3.ParameterName = "adrs3";

                OracleParameter oAdrs4 = new OracleParameter();
                oAdrs4.DbType = DbType.String;
                oAdrs4.Value = adrs4;
                oAdrs4.ParameterName = "adrs4";

                OracleParameter oMobNo = new OracleParameter();
                oMobNo.DbType = DbType.String;
                oMobNo.Value = mobileNumbr;
                oMobNo.ParameterName = "mobNo";

                OracleParameter oHomNo = new OracleParameter();
                oHomNo.DbType = DbType.String;
                oHomNo.Value = homeNumbr;
                oHomNo.ParameterName = "homeNo";

                OracleParameter oOfcNo = new OracleParameter();
                oOfcNo.DbType = DbType.String;
                oOfcNo.Value = ofcNumber;
                oOfcNo.ParameterName = "ofcNo";

                OracleParameter oEmail = new OracleParameter();
                oEmail.DbType = DbType.String;
                oEmail.Value = email;
                oEmail.ParameterName = "email";

                OracleParameter oNic = new OracleParameter();
                oNic.DbType = DbType.String;
                oNic.Value = nic;
                oNic.ParameterName = "nic";

                OracleParameter oLcAdrs1 = new OracleParameter();
                oLcAdrs1.DbType = DbType.String;
                oLcAdrs1.Value = locAdrs1;
                oLcAdrs1.ParameterName = "lcAdrs1";

                OracleParameter oLcAdrs2 = new OracleParameter();
                oLcAdrs2.DbType = DbType.String;
                oLcAdrs2.Value = locAdrs2;
                oLcAdrs2.ParameterName = "lcAdrs2";

                OracleParameter oLcAdrs3 = new OracleParameter();
                oLcAdrs3.DbType = DbType.String;
                oLcAdrs3.Value = locAdrs3;
                oLcAdrs3.ParameterName = "lcAdrs3";

                OracleParameter oLcAdrs4 = new OracleParameter();
                oLcAdrs4.DbType = DbType.String;
                oLcAdrs4.Value = locAdrs4;
                oLcAdrs4.ParameterName = "lcAdrs4";

                OracleParameter oAssignee = new OracleParameter();
                oAssignee.DbType = DbType.String;
                oAssignee.Value = assignee;
                oAssignee.ParameterName = "assignee";

                OracleParameter oDmgBefore = new OracleParameter();
                oDmgBefore.DbType = DbType.String;
                oDmgBefore.Value = dmgBefore;
                oDmgBefore.ParameterName = "dmgBefore";

                OracleParameter oRejBefore = new OracleParameter();
                oRejBefore.DbType = DbType.String;
                oRejBefore.Value = rejBefore;
                oRejBefore.ParameterName = "rejBefore";

                OracleParameter oRejResn = new OracleParameter();
                oRejResn.DbType = DbType.String;
                oRejResn.Value = rejReason;
                oRejResn.ParameterName = "rejResn";

                OracleParameter oPlan = new OracleParameter();
                oPlan.DbType = DbType.String;
                oPlan.Value = plan;
                oPlan.ParameterName = "plan";

                OracleParameter oSumAssurd = new OracleParameter();
                oSumAssurd.DbType = DbType.Double;
                oSumAssurd.Value = sumAssurd;
                oSumAssurd.ParameterName = "sumAssurd";

                OracleParameter oAnuPrem = new OracleParameter();
                oAnuPrem.DbType = DbType.Double;
                oAnuPrem.Value = anuPrem;
                oAnuPrem.ParameterName = "anuPrem";

                OracleParameter oAdmnFee = new OracleParameter();
                oAdmnFee.DbType = DbType.Double;
                oAdmnFee.Value = admnFee;
                oAdmnFee.ParameterName = "admnFee";

                OracleParameter oPolFee = new OracleParameter();
                oPolFee.DbType = DbType.Double;
                oPolFee.Value = polFee;
                oPolFee.ParameterName = "polFee";

                OracleParameter oNbt = new OracleParameter();
                oNbt.DbType = DbType.Double;
                oNbt.Value = nbt;
                oNbt.ParameterName = "nbt";

                OracleParameter oVat = new OracleParameter();
                oVat.DbType = DbType.Double;
                oVat.Value = vat;
                oVat.ParameterName = "vat";

                OracleParameter oComDate = new OracleParameter();
                oComDate.DbType = DbType.DateTime;
                oComDate.Value = comDate;
                oComDate.ParameterName = "comDate";

                OracleParameter oStatus = new OracleParameter();
                oStatus.DbType = DbType.String;
                oStatus.Value = status;
                oStatus.ParameterName = "status";                

                OracleParameter oUsername = new OracleParameter();
                oUsername.Value = username;
                oUsername.ParameterName = "username";

                OracleParameter oTitle = new OracleParameter();
                oTitle.DbType = DbType.String;
                oTitle.Value = title;
                oTitle.ParameterName = "title";

                OracleParameter oProdId = new OracleParameter();
                oProdId.DbType = DbType.String;
                oProdId.Value = prodId;
                oProdId.ParameterName = "prodId";

                OracleParameter oEndDate = new OracleParameter();
                oEndDate.DbType = DbType.DateTime;
                oEndDate.Value = endDate;
                oEndDate.ParameterName = "endDate";

                OracleParameter oDmgRsn = new OracleParameter();
                oDmgRsn.Value = damgReason;
                oDmgRsn.ParameterName = "dmgRsn";

                OracleParameter oProf = new OracleParameter();
                oProf.Value = profession;
                oProf.ParameterName = "prof";

                cmd.Parameters.Add(oPolTyp);
                cmd.Parameters.Add(oRefNo);
                cmd.Parameters.Add(oName);
                cmd.Parameters.Add(oAdrs1);
                cmd.Parameters.Add(oAdrs2);
                cmd.Parameters.Add(oAdrs3);
                cmd.Parameters.Add(oAdrs4);
                cmd.Parameters.Add(oMobNo);
                cmd.Parameters.Add(oHomNo);
                cmd.Parameters.Add(oOfcNo);
                cmd.Parameters.Add(oEmail);
                cmd.Parameters.Add(oNic);
                cmd.Parameters.Add(oLcAdrs1);
                cmd.Parameters.Add(oLcAdrs2);
                cmd.Parameters.Add(oLcAdrs3);
                cmd.Parameters.Add(oLcAdrs4);
                cmd.Parameters.Add(oAssignee);
                cmd.Parameters.Add(oDmgBefore);
                cmd.Parameters.Add(oRejBefore);
                cmd.Parameters.Add(oRejResn);
                cmd.Parameters.Add(oPlan);
                cmd.Parameters.Add(oSumAssurd);
                cmd.Parameters.Add(oAnuPrem);
                cmd.Parameters.Add(oAdmnFee);
                cmd.Parameters.Add(oPolFee);
                cmd.Parameters.Add(oNbt);
                cmd.Parameters.Add(oVat);
                cmd.Parameters.Add(oComDate);
                cmd.Parameters.Add(oStatus);
                //cmd.Parameters.Add(oEntryDate);
                cmd.Parameters.Add(oUsername);
                cmd.Parameters.Add(oTitle);
                cmd.Parameters.Add(oProdId);
                cmd.Parameters.Add(oEndDate);
                cmd.Parameters.Add(oDmgRsn);
                cmd.Parameters.Add(oProf);

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
            logger.write_log("Failed at Proposal - insert_proposal: " + e.ToString());
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

    public bool update_paid_proposal(string propNo, string status, string retCode, string resCode)
    {
        bool returnValue = false;

        string polTyp = propNo.Substring(6, 3);
        if (polTyp.Contains("MP/"))
        {
            polTyp = "MP";
        }
        string polNo =  generate_policyNum("G", Convert.ToInt32(DateTime.Today.ToString("yyyy")), polTyp);

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

                if (status == "A")
                {
                    string updatePayment = "Update SLIC_NET.PROPOSAL_DETAILS Set STATUS = :status, PG_RET_CODE = :retCode, PG_RSN_CODE = :resCode, ENTRY_DATE = sysdate, POLICY_NUMBER = :polNum" +
                                             " where REF_NO = :propNo";

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

                    OracleParameter oPropNo = new OracleParameter();
                    oPropNo.DbType = DbType.String;
                    oPropNo.Value = propNo;
                    oPropNo.ParameterName = "propNo";

                    OracleParameter oPolNo = new OracleParameter();
                    oPolNo.DbType = DbType.String;
                    oPolNo.Value = polNo;
                    oPolNo.ParameterName = "polNum";

                    cmd.Parameters.Add(oStatus);
                    cmd.Parameters.Add(oRetCode);
                    cmd.Parameters.Add(oResCode);
                    cmd.Parameters.Add(oPropNo);
                    cmd.Parameters.Add(oPolNo);

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    string getPropDetails = "";

                    string refNo = "";
                    string comDate = "";
                    string title = "";
                    string name1 = "";
                    string name2 = "";
                    string adrs1 = "";
                    string adrs2 = "";
                    string adrs3 = "";
                    string adrs4 = "";
                    string polType = "";
                    double sumIns = 0;
                    double basicPremium = 0;
                    double vat = 0;
                    double nbt = 0;
                    double admnFee = 0;
                    double polFee = 0;
                    string startDate = "";
                    string endDate = "";
                    string currency = "LKR";


                    if (propNo.Contains("HIP"))
                    {
                        getPropDetails = "select a.REF_NO, to_char(a.COM_DATE, 'yyyy/mm/dd'), a.TITLE, substr(a.FULL_NAME, 1, 50), substr(a.FULL_NAME, 51, 100)," +
                                        " a.ADDRESS1, a.ADDRESS2, a.ADDRESS3, a.ADDRESS4, a.PRODUCT_ID, a.SUM_ASSURD, b.BASIC_ANU_PREM," +
                                        " a.VAT, a.ADMIN_FEE, a.POL_FEE, to_char(a.COM_DATE, 'yyyy/mm/dd'), to_char(a.END_DATE, 'yyyy/mm/dd'), a.NBT" +
                                        " from SLIC_NET.PROPOSAL_DETAILS a, SLIC_NET.PACKAGE_PARAMETERS b" +
                                        " where a.PRODUCT_ID = b.POL_TYPE" +
                                        " and a.PLAN = b.CATEGORY" +
                                        " and a.COM_DATE between b.effect_from and b.effect_to" +
                                        " and a.REF_NO = :refNo";

                    }
                    if (propNo.Contains("MP")) //if (propNo.Contains("AMP")) product changed from AMP
                    {
                        getPropDetails = "select a.QUOT_NUMBER, to_char(b.COM_DATE, 'yyyy/mm/dd'), a.STATUS, substr(a.NAME_1, 1, 50), substr(a.NAME_2, 51, 100)," +
                                         " a.ADDRESS1, a.ADDRESS2, a.ADDRESS3, a.ADDRESS4, a.TYPE, a.PLAN_LIMIT, a.NET_PREMIUM," +
                                         " a.VAT, a.ADMIN_FEE, a.POLICY_FEE, to_char(b.COM_DATE, 'yyyy/mm/dd'), to_char(b.END_DATE, 'yyyy/mm/dd'), a.NBT" +
                                         " from SLIGEN.QUOT_MAST a, SLIC_NET.PROPOSAL_DETAILS b" +
                                         " where a.QUOT_NUMBER = b.REF_NO" +
                                         " and a.QUOT_NUMBER = :refNo";
                    }
                    if (propNo.Contains("GTI"))
                    {
                        getPropDetails = "select a.REFNO, to_char(a.DEPART_DATE, 'yyyy/mm/dd'), a.TITLE, substr(a.FULL_NAME, 1, 50), substr(a.FULL_NAME, 51, 100)," +
                                           " a.ADDRESS1, a.ADDRESS2, a.ADDRESS3, a.ADDRESS4, 'GTI', a.SUM_INS_USD, a.NET_PREMIUM_RS," +
                                           " a.VAT_RS, a.ADMIN_FEE_RS, a.POLICY_FEE_RS, to_char(a.DEPART_DATE, 'yyyy/mm/dd'), to_char(a.RETURN_DATE, 'yyyy/mm/dd'), a.NBT_RS" +
                                           " from SLIC_NET.GT_PROP_MAST a" +
                                           " where a.REFNO = :refNo";
                    }

                    cmd.CommandText = getPropDetails;
                    cmd.Parameters.AddWithValue("refNo", propNo);

                    OracleDataReader propDetReader = cmd.ExecuteReader();

                    while (propDetReader.Read())
                    {
                        if (!propDetReader.IsDBNull(0))
                        {
                            refNo = propDetReader.GetString(0);
                        }
                        if (!propDetReader.IsDBNull(1))
                        {
                            comDate = propDetReader.GetString(1);
                        }
                        if (!propDetReader.IsDBNull(2))
                        {
                            title = propDetReader.GetString(2);
                        }
                        if (!propDetReader.IsDBNull(3))
                        {
                            name1 = propDetReader.GetString(3);
                        }
                        if (!propDetReader.IsDBNull(4))
                        {
                            name2 = propDetReader.GetString(4);
                        }
                        if (!propDetReader.IsDBNull(5))
                        {
                            adrs1 = propDetReader.GetString(5);
                        }
                        if (!propDetReader.IsDBNull(6))
                        {
                            adrs2 = propDetReader.GetString(6);
                        }
                        if (!propDetReader.IsDBNull(7))
                        {
                            adrs3 = propDetReader.GetString(7);
                        }
                        if (!propDetReader.IsDBNull(8))
                        {
                            adrs4 = propDetReader.GetString(8);
                        }
                        if (!propDetReader.IsDBNull(9))
                        {
                            polType = propDetReader.GetString(9);
                        }
                        if (!propDetReader.IsDBNull(10))
                        {
                            sumIns = propDetReader.GetDouble(10);
                        }
                        if (!propDetReader.IsDBNull(11))
                        {
                            basicPremium = propDetReader.GetDouble(11);
                        }
                        if (!propDetReader.IsDBNull(12))
                        {
                            vat = propDetReader.GetDouble(12);
                        }
                        if (!propDetReader.IsDBNull(13))
                        {
                            admnFee = propDetReader.GetDouble(13);
                        }
                        if (!propDetReader.IsDBNull(14))
                        {
                            polFee = propDetReader.GetDouble(14);
                        }
                        if (!propDetReader.IsDBNull(15))
                        {
                            startDate = propDetReader.GetString(15);
                        }
                        if (!propDetReader.IsDBNull(16))
                        {
                            endDate = propDetReader.GetString(16);
                        }
                        if (!propDetReader.IsDBNull(17))
                        {
                            nbt = propDetReader.GetDouble(17);
                        }
                    }

                    propDetReader.Close();
                    cmd.Parameters.Clear();

                    string insertToMomas = "Insert into GENPAY.MOMAS(FMPOL, FMDCO, FMSTA, FMNAM, FMNAM2, FMAD1, FMAD2, FMAD3, FMAD4," +
                                            " FMPTP, FMSUM, FMPRM, FMVAT, FMCES, FMPOF, FMDST, FMDEX, FMBRN, FMCUR, FMNBL, FMDEPT, FMTYP)" +
                                            " VALUES(:refNo, :comDate, :title, :name1, :name2, :adrs1, :adrs2, :adrs3, :adrs4, :polType, " +
                                            " :sumIns, :basicPrem, :vat, :admnFee, :polFee, :startDate, :endDate, :brn, :currType, :nbt, 'G', '1')";

                    cmd.CommandText = insertToMomas;

                    OracleParameter oRef = new OracleParameter();
                    oRef.DbType = DbType.String;
                    oRef.Value = refNo;
                    oRef.ParameterName = "refNo";

                    OracleParameter oComDt = new OracleParameter();
                    oComDt.DbType = DbType.Date;
                    oComDt.Value = comDate;
                    oComDt.ParameterName = "comDate";

                    OracleParameter oTitle = new OracleParameter();
                    oTitle.DbType = DbType.String;
                    oTitle.Value = title;
                    oTitle.ParameterName = "title";

                    OracleParameter oName1 = new OracleParameter();
                    oName1.DbType = DbType.String;
                    oName1.Value = name1;
                    oName1.ParameterName = "name1";

                    OracleParameter oName2 = new OracleParameter();
                    oName2.DbType = DbType.String;
                    oName2.Value = name2;
                    oName2.ParameterName = "name2";

                    OracleParameter oAdrs1 = new OracleParameter();
                    oAdrs1.DbType = DbType.String;
                    oAdrs1.Value = adrs1;
                    oAdrs1.ParameterName = "adrs1";

                    OracleParameter oAdrs2 = new OracleParameter();
                    oAdrs2.DbType = DbType.String;
                    oAdrs2.Value = adrs2;
                    oAdrs2.ParameterName = "adrs2";

                    OracleParameter oAdrs3 = new OracleParameter();
                    oAdrs3.DbType = DbType.String;
                    oAdrs3.Value = adrs3;
                    oAdrs3.ParameterName = "adrs3";

                    OracleParameter oAdrs4 = new OracleParameter();
                    oAdrs4.DbType = DbType.String;
                    oAdrs4.Value = adrs4;
                    oAdrs4.ParameterName = "adrs4";

                    OracleParameter oPolType = new OracleParameter();
                    oPolType.DbType = DbType.String;
                    oPolType.Value = polType;
                    oPolType.ParameterName = "polType";

                    OracleParameter oSumIns = new OracleParameter();
                    oSumIns.DbType = DbType.Double;
                    oSumIns.Value = sumIns;
                    oSumIns.ParameterName = "sumIns";

                    OracleParameter oBasicPrem = new OracleParameter();
                    oBasicPrem.DbType = DbType.Double;
                    oBasicPrem.Value = basicPremium;
                    oBasicPrem.ParameterName = "basicPrem";

                    OracleParameter oVat = new OracleParameter();
                    oVat.DbType = DbType.Double;
                    oVat.Value = vat;
                    oVat.ParameterName = "vat";

                    OracleParameter oAdmnFee = new OracleParameter();
                    oAdmnFee.DbType = DbType.Double;
                    oAdmnFee.Value = admnFee;
                    oAdmnFee.ParameterName = "admnFee";

                    OracleParameter oPolFee = new OracleParameter();
                    oPolFee.DbType = DbType.Double;
                    oPolFee.Value = polFee;
                    oPolFee.ParameterName = "polFee";

                    OracleParameter oStartDt = new OracleParameter();
                    oStartDt.DbType = DbType.Date;
                    oStartDt.Value = startDate;
                    oStartDt.ParameterName = "startDate";

                    OracleParameter oEndDate = new OracleParameter();
                    oEndDate.DbType = DbType.Date;
                    oEndDate.Value = endDate;
                    oEndDate.ParameterName = "endDate";

                    OracleParameter oBranch = new OracleParameter();
                    oBranch.DbType = DbType.Int32;
                    oBranch.Value = 337;
                    oBranch.ParameterName = "brn";

                    OracleParameter oCurrType = new OracleParameter();
                    oCurrType.DbType = DbType.String;
                    oCurrType.Value = currency;
                    oCurrType.ParameterName = "currType";

                    OracleParameter oNbt = new OracleParameter();
                    oNbt.DbType = DbType.Double;
                    oNbt.Value = nbt;
                    oNbt.ParameterName = "nbt";

                    cmd.Parameters.Add(oRef);
                    cmd.Parameters.Add(oComDt);
                    cmd.Parameters.Add(oTitle);
                    cmd.Parameters.Add(oName1);
                    cmd.Parameters.Add(oName2);
                    cmd.Parameters.Add(oAdrs1);
                    cmd.Parameters.Add(oAdrs2);
                    cmd.Parameters.Add(oAdrs3);
                    cmd.Parameters.Add(oAdrs4);
                    cmd.Parameters.Add(oPolType);
                    cmd.Parameters.Add(oSumIns);
                    cmd.Parameters.Add(oBasicPrem);
                    cmd.Parameters.Add(oVat);
                    cmd.Parameters.Add(oAdmnFee);
                    cmd.Parameters.Add(oPolFee);
                    cmd.Parameters.Add(oStartDt);
                    cmd.Parameters.Add(oEndDate);
                    cmd.Parameters.Add(oBranch);
                    cmd.Parameters.Add(oCurrType);
                    cmd.Parameters.Add(oNbt);

                    cmd.ExecuteNonQuery();
                }

                else if (status == "F")
                {
                    string updatePayment = "Update SLIC_NET.PROPOSAL_DETAILS Set STATUS = :status, PG_RET_CODE = :retCode, PG_RSN_CODE = :resCode, ENTRY_DATE = sysdate" +
                                           " where REF_NO = :propNo";

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

                    OracleParameter oPropNo = new OracleParameter();
                    oPropNo.DbType = DbType.String;
                    oPropNo.Value = propNo;
                    oPropNo.ParameterName = "propNo";                                      

                    cmd.Parameters.Add(oStatus);
                    cmd.Parameters.Add(oRetCode);
                    cmd.Parameters.Add(oResCode);
                    cmd.Parameters.Add(oPropNo);
                    
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }


                trans.Commit();
                returnValue = true;
            }

        }
        catch (Exception e)
        {
            trans.Rollback();
            log logger = new log();
            logger.write_log("Failed at Proposal - update_paid_proposal: " + e.ToString());
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

    public bool getHPParameters(string plan, string comDate, double sumIns, out double sumInsured, out double baseAnuPrem, out double admnFee, out double polFee, out double nbt, out double vat, out double totAnuPrem)
    {
        bool returnValue = false;
        baseAnuPrem = 0;
        admnFee = 0;
        polFee = 0;
        nbt = 0;
        vat = 0;
        totAnuPrem = 0;
        sumInsured = 0;
        try
        {            

            oconn.Open();
            OracleCommand cmd = oconn.CreateCommand();
            string getPackageParas = "Select BASIC_ANU_PREM, POL_FEE, SUM_INSURED" +
                                     " from SLIC_NET.PACKAGE_PARAMETERS" +
                                     " where (CATEGORY = :plan OR SUM_INSURED = :sumIns)" +
                                     " and POL_TYPE = 'HIP'" +
                                     " and sysdate between EFFECT_FROM and EFFECT_TO";

            using (cmd)
            {
                cmd.CommandText = getPackageParas;
                cmd.Parameters.AddWithValue("plan", plan.Trim());

                OracleParameter oSumIns = new OracleParameter();
                oSumIns.DbType = DbType.Double;
                oSumIns.Value = sumIns;
                oSumIns.ParameterName = "sumIns";
                cmd.Parameters.Add(oSumIns);

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
                cmd.Parameters.AddWithValue("requestDate", DateTime.ParseExact(comDate, "yyyy/MM/dd", CultureInfo.InvariantCulture));
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
        catch(Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at Proposal - getHPParameters: " + e.ToString());
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

    public string generate_proposalID(string dpt, int year, string type)
    {
        string result = "";
        string id = "";
        //string n = "";
        try
        {
            oconn.Open();
            string sql = "SELECT * FROM SLIC_NET.PROP_REF_NUMBERS WHERE DEPARTMENT = :dept AND YEAR = :yr AND TYP = :polType ";
           // string sql = "SELECT * FROM SLIC_NET.PROP_REF_NUMBERS WHERE DEPARTMENT = '" + dpt + "' AND YEAR = " + year + " AND TYP = '" + type + "'";
            int rows = 0;
            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {
                //cmd.Parameters.AddWithValue("dept", dpt);
              //  cmd.Parameters.AddWithValue("year", year);
              //  cmd.Parameters.AddWithValue("polType", type);

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
                result = update_propSeq(sql, dpt, year, type).ToString();
            }
            else
            {
                result = insert_newSeq(dpt, year, type).ToString();
            }

            string seq = result.ToString().PadLeft(5, '0');

            id = dpt + "/999/" + type + "/" + DateTime.Today.ToString("yy") + "/" + seq;

            result = id;
        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at Proposal - generate_proposalID " + e.ToString());
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

    private int update_propSeq(string sql, string dpt, int year, string type)
    {
        int seqNo = 0;
        try
        {
            seqNo = get_max_propNo(sql , dpt, year, type);
            string sql2 = "UPDATE SLIC_NET.PROP_REF_NUMBERS SET REFNO = :seqNo WHERE DEPARTMENT = :dept AND YEAR = :year AND TYP = :polType";
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
            logger.write_log("Failed at Proposal - update_propSeq " + e.ToString());
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

    private int get_max_propNo(string sql, string dpt, int year, string type)
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
            logger.write_log("Failed at Proposal - get_max_propNo " + e.ToString());
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
            string sql2 = "INSERT INTO SLIC_NET.PROP_REF_NUMBERS VALUES (:dept, :year , :polType," + 1 + ")";

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
            logger.write_log("Failed at Proposal - insert_newSeq " + e.ToString());
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

    public string generate_policyNum(string dpt, int year, string type)
    {
        string result = "";
        string id = "";
        //string n = "";
        try
        {
            oconn.Open();
            string sql = "SELECT * FROM SLIC_NET.POL_REF_NUMBERS WHERE DEPARTMENT = :dept AND YEAR = :yr AND TYP = :polType ";
            // string sql = "SELECT * FROM SLIC_NET.PROP_REF_NUMBERS WHERE DEPARTMENT = '" + dpt + "' AND YEAR = " + year + " AND TYP = '" + type + "'";
            int rows = 0;
            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {
                //cmd.Parameters.AddWithValue("dept", dpt);
                //  cmd.Parameters.AddWithValue("year", year);
                //  cmd.Parameters.AddWithValue("polType", type);

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

                OracleDataReader polNoReader = cmd.ExecuteReader();

                while (polNoReader.Read())
                {
                    rows++;
                }
                polNoReader.Close();
                cmd.Parameters.Clear();
            }

            if (rows > 0)
            {
                result = update_polSeq(sql, dpt, year, type).ToString();
            }
            else
            {
                result = insert_newPolSeq(dpt, year, type).ToString();
            }

            if (type == "MP") // product changed from AMP
            {
                string seq = result.ToString().PadLeft(5, '0');
                id = dpt + "/999/" + type + "/" + DateTime.Today.ToString("yy") + "/" + seq;
            }
            else if (type == "GTI")
            {
                string seq = result.ToString().PadLeft(6, '0');
                id = dpt + type + DateTime.Today.ToString("yy") + "999" + "" + "1" + seq;
            }

            result = id;
        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at Proposal - generate_policyNum " + e.ToString());
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

    private int update_polSeq(string sql, string dpt, int year, string type)
    {
        int seqNo = 0;
        try
        {
            seqNo = get_max_polNo(sql, dpt, year, type);
            string sql2 = "UPDATE SLIC_NET.POL_REF_NUMBERS SET REFNO = :seqNo WHERE DEPARTMENT = :dept AND YEAR = :year AND TYP = :polType";
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
            logger.write_log("Failed at Proposal - update_polSeq " + e.ToString());
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

    private int get_max_polNo(string sql, string dpt, int year, string type)
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
            logger.write_log("Failed at Proposal - get_max_polNo " + e.ToString());
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

    private int insert_newPolSeq(string dpt, int year, string type)
    {
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string sql2 = "INSERT INTO SLIC_NET.POL_REF_NUMBERS VALUES (:dept, :year , :polType," + 1 + ")";

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
            logger.write_log("Failed at Proposal - insert_newPolSeq " + e.ToString());
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
    
    public bool send_pay_receipt_email()
    {
        bool retValue = false;
        try
        {
            string sumInsLabel = "";

            if (productID == "GTI")
            {
                sumInsLabel = "Sum Assured (USD):";
            }
            else
            {
                sumInsLabel = "Sum Assured (Rs.):";
            }

            string subject = "SLIC Payment Confirmation";
            #region
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
            //                            getFullAddress() +
            //                        "</td>" +
            //                    "</tr>" +
            //                    "<tr>" +
            //                        "<td colspan=\"2\">" +
            //                            "Dear Customer,</td>" +
            //                    "</tr>" +
            //                    "<tr>" +
            //                        "<td colspan=\"2\">" +
            //                            "We have received your payment of Rs." +
            //                             annualPremium.ToString("N2") +
            //                            "&nbsp;under the reference no:&nbsp;" +
            //                            refNo +
            //                            "&nbsp;regarding premium payment for " +
            //                            product_Name + " (Plan - " + plan + ")" +
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
            //                            "Policy Type:" +
            //                            "</td>" +
            //                        "<td class=\"width: 80%\">" +
            //                            product_Name + " (Plan - " + plan + ")" +
            //                        "</td>" +
            //                    "</tr>"+
            //                    "<tr>" +
            //                       "<td class=\"width: 20%\">" +
            //                           "Sum Assured:" +
            //                           "</td>" +
            //                       "<td class=\"width: 80%\">" +
            //                           sumAssured.ToString("N2") +
            //                       "</td>" +
            //                   "</tr>" +
            //                   "<tr>" +
            //                       "<td class=\"width: 20%\">" +
            //                           "Customer Name:" +
            //                           "</td>" +
            //                       "<td class=\"width: 80%\">" +
            //                           fullName +
            //                       "</td>" +
            //                   "</tr>" +
            //                   "<tr>" +
            //                       "<td class=\"width: 20%\">" +
            //                           "Premium:" +
            //                           "</td>" +
            //                       "<td class=\"width: 80%\">" +
            //                           annualPremium.ToString("N2") +
            //                       "</td>" +
            //                   "</tr>" +
            //                   "<tr>" +
            //                       "<td class=\"width: 20%\">" +
            //                           "Cover period:</td>" +
            //                       "<td class=\"width: 80%\">" +
            //                           getCoverPeriod() +
            //                       "</td>" +
            //                   "</tr>" +
            //                   "<tr>" +
            //                       "<td colspan=\"2\">" +
            //                          " &nbsp;</td>" +
            //                   "</tr>"+
            //                   "<tr>" +
            //                       "<td colspan=\"2\">" +
            //                           "Policy schedule will be posted to the above address in due course.</td>" +
            //                   "</tr>" +
            //                   "<tr>" +
            //                       "<td colspan=\"2\">" +
            //                           "Thanking you,<br> Sri Lanka Insurance.</td>" +
            //                   "</tr>" +
            //                   "<tr>" +
            //                       "<td colspan=\"2\">" +
            //                           "&nbsp;</td>" +
            //                   "</tr>" +
            //               "</tbody></table>";
            #endregion
            string content1 = "<table>" +
                                "<tbody>" +
                                "<tr>" +
                                    "<td colspan=\"2\">" +
                                        "Dear Customer,</td>" +
                                "</tr>" +
                                 "<tr>" +
                                   "<td colspan=\"2\">" +
                                      " &nbsp;</td>" +
                               "</tr>" +
                                "<tr>" +
                                    "<td colspan=\"2\">" +
                                        "Your payment of Rs." +
                                         annualPremium.ToString("N2") +
                                        "&nbsp;under the reference no:&nbsp;" +
                                        refNo +
                                        "&nbsp;regarding premium payment for the policy: " +
                                        product_Name + " (Plan - " + plan + ") has been received.</td>" +
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
                                        policy_no +
                                    "</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td class=\"width: 20%\">" +
                                        "Policy Type:" +
                                        "</td>" +
                                    "<td class=\"width: 80%\">" +
                                        product_Name + " (Plan - " + plan + ")" +
                                    "</td>" +
                                "</tr>" +
                                "<tr>" +
                                   "<td class=\"width: 20%\">" +
                                       sumInsLabel +
                                       "</td>" +
                                   "<td class=\"width: 80%\">" +
                                       sumAssured.ToString("N2") +
                                   "</td>" +
                               "</tr>" +
                               "<tr>" +
                                   "<td class=\"width: 20%\">" +
                                       "Customer Name:" +
                                       "</td>" +
                                   "<td class=\"width: 80%\">" +
                                       fullName +
                                   "</td>" +
                               "</tr>" +
                               "<tr>" +
                                   "<td class=\"width: 20%\">" +
                                       "Premium (Rs.):" +
                                       "</td>" +
                                   "<td class=\"width: 80%\">" +
                                       annualPremium.ToString("N2") +
                                   "</td>" +
                               "</tr>" +
                               "<tr>" +
                                   "<td class=\"width: 20%\">" +
                                       "Cover period:</td>" +
                                   "<td class=\"width: 80%\">" +
                                       getCoverPeriod() +
                                   "</td>" +
                               "</tr>" +
                               "<tr>" +
                                   "<td>" +
                                      " &nbsp;</td>" +
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

            if (refNo.Contains("G/999/GTI/"))
            {
                string jjkj = "<tr><td colspan=\"2\">Your payment receipt will be posted to the registered address in due course.</td></tr><tr><td colspan=\"2\">If you are required with a physical policy document, please <a href='Http://www.srilankainsurance.net/ContactUs.aspx'><span style='font-weight:bold; color:#8C8C8C;'>contact us.</span></a></td></tr>";
                content1 = content1 + jjkj;
            }
            else if (refNo.Contains("G/999/AMP/"))
            {
                string jjkj = "<tr><td colspan=\"2\">Policy schedule will be posted to the above address in due course.</td></tr>";
                //content1 = content1 + jjkj;

                jjkj = "<tr><td colspan=\"2\">Healthplus card will be sent by post to the given address within 15 working days.</td></tr>";
                content1 = content1 + jjkj;

            }

                               content1 = content1+"<tr>" +
                                   "<td colspan=\"2\">" +
                                       "The Policy is valid only if the bank transfer is successful.</td>" +
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
            /* "Dear Customer, <br/> We have received your payment of Rs. " + annualPremium.ToString("N2") +
                              " under the reference no: " + refNo + " regarding premium payment for " + product_Name + " (Plan - " + plan + ") policy.";*/

            string content2 = content1;

            Db_Email emailSender = new Db_Email();
            retValue = emailSender.send_html_email(email, subject, content1, content2);
            LogMail logger = new LogMail();
            logger.write_log("To: " + email + " Subject: " + subject);

           // retValue = true;
        }
        catch(Exception e)
        {
            retValue = false;
            log logger = new log();
            logger.write_log("Failed at Proposal - send_pay_receipt_email " + e.ToString());
        }
        return retValue;
    }

    public string getFullAddress()
    {
        string address = "";

        address = Address1;
        if (Address2 != "")
        {
            address = address + "<br/>" + Address2;
        }
        if (Address3 != "")
        {
            address = address + "<br/>" + Address3;
        }
        if (Address4 != "")
        {
            address = address + "<br/>" + Address4;
        }

        return address;
    }

    public string getFullAddress2()
    {
        string address = "";

        address = Address1;
        if (Address2 != "")
        {
            address = address + ", " + Address2;
        }
        if (Address3 != "")
        {
            address = address + ", " + Address3;
        }
        if (Address4 != "")
        {
            address = address + ", " + Address4;
        }

        return address;
    }

    public string getCoverPeriod()
    {
        string coverPeriod = "";

        string startDate = comenmentDate;
        string endDate = this.endDate;//(DateTime.ParseExact(startDate, "yyyy/MM/dd", CultureInfo.InvariantCulture).AddYears(1)).ToString("yyyy/MM/dd");

        coverPeriod = "From " + startDate + " To " + endDate;
        return coverPeriod;
    }


    //-----------------------------------AMP related methods--------------------------------------------------------------
    public string getAllAMPPremiums(DataTable dtMemDetails, out double premium_planA, out double premium_planB, out double premium_planC, out double premium_planD, out double premium_planE)
    {
        string mesg = "success";
        premium_planA = 0;
        premium_planB = 0;
        premium_planC = 0;
        premium_planD = 0;
        premium_planE = 0;

        try
        {            
            string planCode = "";
            double planLimit = 0;
            double famDiscPercent = 0;
            double reInsLoading = 0.00;
            double slicDiscount = 0.00;
            double nbtAmount = 0;
            double vatAmount = 0;
            double adminFeePerc = 0;
            double adminFee = 0;
            double polFee = 0;

            double reInsBasePrem = 0.00;
            double reInsMatPrem = 0.00;
            double maternSlic = 0.00;
            double basePremium = 0.00;
            double bmiLoading = 0.00;
            double mobidLoading = 0.00;
            double mobRate = 0.00;
            double bmiRate = 0.00;
            double finalPremTotal = 0;
            double maleAge = 0;
            double femaleAge = 0;
            int childCount = 0;

            Parameters paras = new Parameters();
            reInsLoading = paras.re_loadning;
            adminFeePerc = paras.admin_fee;
            polFee = paras.pol_fee;

            DataSet dsPlans = new DataSet();
            dsPlans = getAMPPlans();

            DataTable dtPlans = new DataTable();

            if (dsPlans != null)
            {
                oconn.Open();
                dtPlans = dsPlans.Tables[0];

                foreach (DataRow planRow in dtPlans.Rows)
                {
                    planCode = planRow.ItemArray[0].ToString();
                    planLimit = double.Parse(planRow.ItemArray[1].ToString());
                    famDiscPercent = 0;
                    finalPremTotal = 0;
                    nbtAmount = 0;
                    vatAmount = 0;

                    /*
                    string getFamDiscRate = "Select DISC_RATE from SLIGEN.AMP_DISC_RATES" +
                                            " where NUMLIVES = :memCount" +
                                            " and PLAN = :plan" +
                                            " and sysdate between EFFECT_FROM and EFFECT_TO";

                    using (OracleCommand cmd = new OracleCommand(getFamDiscRate, oconn))
                    {
                        cmd.Parameters.Add("memCount", OdbcType.Int);
                        cmd.Parameters["memCount"].Value = dtMemDetails.Rows.Count;

                        cmd.Parameters.Add("plan", OdbcType.VarChar);
                        cmd.Parameters["plan"].Value = planLimit;

                        OracleDataReader famDiscntReader = cmd.ExecuteReader();

                        while (famDiscntReader.Read())
                        {
                            if (!famDiscntReader.IsDBNull(0))
                            {
                                famDiscPercent = famDiscntReader.GetDouble(0);
                            }
                        }
                        famDiscntReader.Close();
                    }  
                    */

                    DataRow[] foundChild = dtMemDetails.Select("Category = 'Child'");
                    childCount = foundChild.Length;

                    foreach (DataRow memRow in dtMemDetails.Rows)
                    {
                        reInsBasePrem = 0.00;
                        reInsMatPrem = 0.00;
                        maternSlic = 0.00;
                        basePremium = 0.00;
                        bmiLoading = 0.00;
                        mobidLoading = 0.00;
                        mobRate = 0.00;
                        bmiRate = 0.00;

                        /*
                        string getPremiums = "Select MUNICH_RE_PREM, MUNICH_MAT_PREM, SLI_MAT_PREM" +
                                                " from SLIGEN.AMP_PREMIUMS" +
                                                " where GENDER = :gender" +
                                                " and AGE = :age" +
                                                " and PLAN_SUM = :planLim" +
                                                " and sysdate between EFFECT_FROM and EFFECT_TO";

                        using (OracleCommand cmd = new OracleCommand(getPremiums, oconn))
                        {
                            cmd.Parameters.Add("gender", OdbcType.VarChar);
                            cmd.Parameters["gender"].Value = memRow["Gender"].ToString().Substring(0,1);

                            cmd.Parameters.Add("age", OdbcType.Double);
                            cmd.Parameters["age"].Value = Math.Ceiling(double.Parse(memRow["Age"].ToString()));

                            cmd.Parameters.Add("planLim", OdbcType.Double);
                            cmd.Parameters["planLim"].Value = planLimit;

                            OracleDataReader premiumReader = cmd.ExecuteReader();

                            while (premiumReader.Read())
                            {
                                if (!premiumReader.IsDBNull(0))
                                {
                                    reInsBasePrem = premiumReader.GetDouble(0);
                                }
                                if (!premiumReader.IsDBNull(1))
                                {
                                    reInsMatPrem = premiumReader.GetDouble(1);
                                }
                                if (!premiumReader.IsDBNull(2))
                                {
                                    maternSlic = premiumReader.GetDouble(2);
                                }
                            }
                            premiumReader.Close();
                        }

                        bmiRate = double.Parse(memRow["BmiRate"].ToString());

                        bmiLoading = reInsBasePrem * bmiRate;
                        mobidLoading = reInsBasePrem * mobRate;

                        basePremium = reInsBasePrem + bmiLoading + mobidLoading + reInsMatPrem;

                        double floDiscontdAmt = basePremium * famDiscPercent;
                        double finalPremium = (floDiscontdAmt / (1 - (reInsLoading - slicDiscount))) + maternSlic;

                        finalPremTotal = finalPremTotal + finalPremium;
                        */
                        string memCategory = memRow["Category"].ToString();
                        string memGender = memRow["Gender"].ToString().Substring(0, 1);
                        double ageInYrs = Math.Ceiling(double.Parse(memRow["Age"].ToString()));

                        if (memCategory == "Main Life" || memCategory == "Spouse")
                        {
                            if (memGender == "M")
                            {
                                maleAge = ageInYrs;
                            }
                            else if (memGender == "F")
                            {
                                femaleAge = ageInYrs;
                            }
                        }

                    }

                    string getPremiums = "Select PREMIUM" +
                                         " from SLIGEN.AMP_PREMIUMS_V2" +
                                         " where M_AGE_FROM <= " + maleAge +
                                         " and M_AGE_TO >= " + maleAge +
                                         " and F_AGE_FROM <= " + femaleAge +
                                         " and F_AGE_TO >= " + femaleAge +
                                         " and CHILD_COUNT = " + childCount +
                                         " and PLAN_SUM = " + planLimit.ToString() +
                                         " and sysdate between EFFECT_FROM and EFFECT_TO";

                    using (OracleCommand cmd = new OracleCommand(getPremiums, oconn))
                    {
                        OracleDataReader premiumReader = cmd.ExecuteReader();                        

                        while (premiumReader.Read())
                        {
                            if (!premiumReader.IsDBNull(0))
                            {
                                finalPremTotal = premiumReader.GetDouble(0);
                            }

                        }
                        premiumReader.Close();
                    }                         

                    adminFee = finalPremTotal * adminFeePerc;

                    //-----------------NBL and VAT Calculation--------------------------------        
                    using (OracleCommand cmd = new OracleCommand("GENPAY.CALCULATE_NBL_AND_VAT", oconn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("taxLiableAmount", finalPremTotal + adminFee + polFee);
                        cmd.Parameters.AddWithValue("requestDate", DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture));
                        cmd.Parameters.Add("nblAmount", OracleType.Number).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("vatAmount", OracleType.Number).Direction = ParameterDirection.Output;

                        OracleDataReader dr = cmd.ExecuteReader();

                        nbtAmount = double.Parse(cmd.Parameters["nblAmount"].Value.ToString());
                        vatAmount = double.Parse(cmd.Parameters["vatAmount"].Value.ToString());

                        dr.Close();
                    }
                    //------------------------------------------------------------------------------- 

                    if (planCode == "A")
                    {
                        premium_planA = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                    }
                    else if (planCode == "B")
                    {
                        premium_planB = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                    }
                    else if (planCode == "C")
                    {
                        premium_planC = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                    }
                    else if (planCode == "D")
                    {
                        premium_planD = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                    }
                    else if (planCode == "E")
                    {
                        premium_planE = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                    }
                    else
                    {
                        mesg = "Premiums cannot be calculated - internal error.";
                    }

                }
            }
            else
            {
                mesg = "Premiums cannot be calculated - internal error.";
            }              

        }
        catch (Exception e)
        {
            // Log your error
            mesg = "Error while calculating premium.";
            log logger = new log();
            logger.write_log("Failed at Proposal - getAllAMPPremiums: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }


        return mesg;
    }

    public DataSet getAMPPlans()
    {
        DataSet dsPlans = new DataSet();
        //dsPlans = null;
        try
        {
            oconn.Open();
            string getPlans = "Select CODE, LIMIT" +
                              " from SLIGEN.AMP_PLANS_V2" +  // product change from AMP
                              " where sysdate between EFFECT_FROM and EFFECT_TO";

            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPlans, oconn))
            {                
                dsPlans.Clear();
                dataAdd.Fill(dsPlans);
            }           

        }
        catch (Exception e)
        {
            // Log your error         
            dsPlans = null;
            log logger = new log();
            logger.write_log("Failed at Proposal - getAMPPlans: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }

        return dsPlans;
    }

    public string createAMPQuotation(string uname, DataTable dtMemDetails, string plan, out string quotNo)
    {
        string mesg = "success";
        quotNo = "";

        double planLimit = 0;
        double famDiscPercent = 0;
        double reInsLoading = 0.00;
        double slicDiscount = 0.00;
        double nbtAmount = 0;
        double vatAmount = 0;
        double adminFeePerc = 0;
        double adminFee = 0;
        double polFee = 0;

        double reInsBasePrem = 0.00;
        double reInsMatPrem = 0.00;
        double maternSlic = 0.00;
        double basePremium = 0.00;
        double bmiLoading = 0.00;
        double mobidLoading = 0.00;
        double mobRate = 0.00;
        double bmiRate = 0.00;
        double finalPremTotal = 0;
        double baseRateTotal = 0.00;
        double maternSlicTotal = 0.00;
        double fltDiscAmtTotal = 0.00;
        double totFinalPremium = 0;
        double reInsCeedRate = 0.00;
        double reInsCedAmt = 0;
        double slicRetention = 0;
        double taxExpenses = 0;
        double slicRetenForRe = 0;
        double maleAge = 0;
        double femaleAge = 0;
        int childCount = 0;

        try
        {
            oconn.Open();
            string getPlan = "Select LIMIT" +
                              " from SLIGEN.AMP_PLANS_V2" +
                              " where sysdate between EFFECT_FROM and EFFECT_TO" +
                              " and CODE = :planCode";

            using (OracleCommand cmd = new OracleCommand(getPlan, oconn))
            {
                cmd.Parameters.Add("planCode", OdbcType.VarChar);
                cmd.Parameters["planCode"].Value = plan;

                OracleDataReader planReader = cmd.ExecuteReader();

                while (planReader.Read())
                {
                    if (!planReader.IsDBNull(0))
                    {
                        planLimit = planReader.GetDouble(0);
                    }
                }
                planReader.Close();
            }

            Parameters paras = new Parameters();
            reInsLoading = paras.re_loadning;
            adminFeePerc = paras.admin_fee;
            polFee = paras.pol_fee;
            reInsCeedRate = paras.re_ceed_rate;

            if (planLimit != 0)
            {
                /*
                string getFamDiscRate = "Select DISC_RATE from SLIGEN.AMP_DISC_RATES" +
                                            " where NUMLIVES = :memCount" +
                                            " and PLAN = :plan" +
                                            " and sysdate between EFFECT_FROM and EFFECT_TO";

                using (OracleCommand cmd = new OracleCommand(getFamDiscRate, oconn))
                {
                    cmd.Parameters.Add("memCount", OdbcType.Int);
                    cmd.Parameters["memCount"].Value = dtMemDetails.Rows.Count;

                    cmd.Parameters.Add("plan", OdbcType.VarChar);
                    cmd.Parameters["plan"].Value = planLimit;

                    OracleDataReader famDiscntReader = cmd.ExecuteReader();

                    while (famDiscntReader.Read())
                    {
                        if (!famDiscntReader.IsDBNull(0))
                        {
                            famDiscPercent = famDiscntReader.GetDouble(0);
                        }
                    }
                    famDiscntReader.Close();
                }
                */

                DataRow[] foundChild = dtMemDetails.Select("Category = 'Child'");
                childCount = foundChild.Length;

                foreach (DataRow memRow in dtMemDetails.Rows)
                {
                    reInsBasePrem = 0.00;
                    reInsMatPrem = 0.00;
                    maternSlic = 0.00;
                    basePremium = 0.00;
                    bmiLoading = 0.00;
                    mobidLoading = 0.00;
                    mobRate = 0.00;
                    bmiRate = 0.00;

                    /*
                    string getPremiums = "Select MUNICH_RE_PREM, MUNICH_MAT_PREM, SLI_MAT_PREM" +
                                            " from SLIGEN.AMP_PREMIUMS" +
                                            " where GENDER = :gender" +
                                            " and AGE = :age" +
                                            " and PLAN_SUM = :planLim" +
                                            " and sysdate between EFFECT_FROM and EFFECT_TO";

                    using (OracleCommand cmd = new OracleCommand(getPremiums, oconn))
                    {
                        cmd.Parameters.Add("gender", OdbcType.VarChar);
                        cmd.Parameters["gender"].Value = memRow["Gender"].ToString().Substring(0,1);

                        cmd.Parameters.Add("age", OdbcType.Double);
                        cmd.Parameters["age"].Value = double.Parse(memRow["Age"].ToString());

                        cmd.Parameters.Add("planLim", OdbcType.Double);
                        cmd.Parameters["planLim"].Value = planLimit;

                        OracleDataReader premiumReader = cmd.ExecuteReader();

                        while (premiumReader.Read())
                        {
                            if (!premiumReader.IsDBNull(0))
                            {
                                reInsBasePrem = premiumReader.GetDouble(0);
                            }
                            if (!premiumReader.IsDBNull(1))
                            {
                                reInsMatPrem = premiumReader.GetDouble(1);
                            }
                            if (!premiumReader.IsDBNull(2))
                            {
                                maternSlic = premiumReader.GetDouble(2);
                            }
                        }
                        premiumReader.Close();
                    }

                    bmiRate = double.Parse(memRow["BmiRate"].ToString());

                    bmiLoading = reInsBasePrem * bmiRate;
                    mobidLoading = reInsBasePrem * mobRate;

                    basePremium = reInsBasePrem + bmiLoading + mobidLoading + reInsMatPrem;

                    double floDiscontdAmt = basePremium * famDiscPercent;
                    double finalPremium = (floDiscontdAmt / (1 - (reInsLoading - slicDiscount))) + maternSlic;

                    memRow["BaseRate"] = basePremium;
                    memRow["MaternSlic"] = maternSlic;
                    memRow["FlDiscountedAmt"] = floDiscontdAmt;
                    memRow["FinalPremium"] = finalPremium;
                    memRow["BmiRate"] = bmiRate;
                    memRow["BmiLoading"] = bmiLoading;
                    memRow["MobLoading"] = mobidLoading;

                    baseRateTotal = baseRateTotal + basePremium;
                    maternSlicTotal = maternSlicTotal + maternSlic;
                    fltDiscAmtTotal = fltDiscAmtTotal + floDiscontdAmt;
                    finalPremTotal = finalPremTotal + finalPremium;
                    */

                    string memCategory = memRow["Category"].ToString();
                    string memGender = memRow["Gender"].ToString().Substring(0, 1);
                    double ageInYrs = Math.Ceiling(double.Parse(memRow["Age"].ToString()));

                    if (memCategory == "Main Life" || memCategory == "Spouse")
                    {
                        if (memGender == "M")
                        {
                            maleAge = ageInYrs;
                        }
                        else if (memGender == "F")
                        {
                            femaleAge = ageInYrs;
                        }
                    }

                }

                string getPremiums = "Select PREMIUM" +
                                         " from SLIGEN.AMP_PREMIUMS_V2" +
                                         " where M_AGE_FROM <= " + maleAge +
                                         " and M_AGE_TO >= " + maleAge +
                                         " and F_AGE_FROM <= " + femaleAge +
                                         " and F_AGE_TO >= " + femaleAge +
                                         " and CHILD_COUNT = " + childCount +
                                         " and PLAN_SUM = " + planLimit.ToString() +
                                         " and sysdate between EFFECT_FROM and EFFECT_TO";

                using (OracleCommand cmd = new OracleCommand(getPremiums, oconn))
                {
                    OracleDataReader premiumReader = cmd.ExecuteReader();

                    while (premiumReader.Read())
                    {
                        if (!premiumReader.IsDBNull(0))
                        {
                            finalPremTotal = premiumReader.GetDouble(0);
                        }

                    }
                    premiumReader.Close();
                }

                adminFee = finalPremTotal * adminFeePerc;

                //-----------------NBL and VAT Calculation--------------------------------        
                using (OracleCommand cmd = new OracleCommand("GENPAY.CALCULATE_NBL_AND_VAT", oconn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("taxLiableAmount", finalPremTotal + adminFee + polFee);
                    cmd.Parameters.AddWithValue("requestDate", DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture));
                    cmd.Parameters.Add("nblAmount", OracleType.Number).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("vatAmount", OracleType.Number).Direction = ParameterDirection.Output;

                    OracleDataReader dr = cmd.ExecuteReader();

                    nbtAmount = double.Parse(cmd.Parameters["nblAmount"].Value.ToString());
                    vatAmount = double.Parse(cmd.Parameters["vatAmount"].Value.ToString());

                    dr.Close();
                }
                //------------------------------------------------------------------------------- 

                totFinalPremium = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                reInsCedAmt = 0; // fltDiscAmtTotal * reInsCeedRate;
                slicRetention = 0; // finalPremTotal - reInsCedAmt;
                taxExpenses = adminFee + polFee + nbtAmount + vatAmount;
                slicRetenForRe = 0; // fltDiscAmtTotal - reInsCedAmt;

                CustProfile profile = new CustProfile(uname);

                AMP_Quotation_mast mastEntry = new AMP_Quotation_mast();
                string name = profile.O_firstName +  " " + profile.O_lastName;

                bool success = mastEntry.Insert_rec("G", "MP", profile.O_title, name, "", profile.O_addrss1, profile.O_addrss2, profile.O_addrss3,
                                    profile.O_addrss4, profile.O_homeNumber, profile.O_mobileNumber, dtMemDetails.Rows.Count, famDiscPercent, finalPremTotal,
                                    adminFee, polFee, nbtAmount, vatAmount, totFinalPremium, reInsCedAmt, slicRetention, taxExpenses, "9999", profile.O_nicNo, 999,
                                    planLimit, 0.00, planLimit, plan, dtMemDetails, "", slicDiscount, slicRetenForRe, reInsLoading, out quotNo);
                if (!success)
                {
                    mesg = "Quotation cannot be created due to internal error";
                }

            }
            else
            {
                mesg = "Quotation cannot be created - internal error.";
            }

        }
        catch (Exception e)
        {
            mesg = "Error while creating quotation.";
            // Log your error         
            log logger = new log();
            logger.write_log("Failed at createAMPQuotation: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }

        return mesg;
    }

    public string updateAMPQuotation(string plan, string quotNo)
    {
        string mesg = "success";        

        double planLimit = 0;
        double famDiscPercent = 0;
        double reInsLoading = 0.00;
        double slicDiscount = 0.00;
        double nbtAmount = 0;
        double vatAmount = 0;
        double adminFeePerc = 0;
        double adminFee = 0;
        double polFee = 0;

        double reInsBasePrem = 0.00;
        double reInsMatPrem = 0.00;
        double maternSlic = 0.00;
        double basePremium = 0.00;
        double bmiLoading = 0.00;
        double mobidLoading = 0.00;
        double mobRate = 0.00;
        double bmiRate = 0.00;
        double finalPremTotal = 0;
        double baseRateTotal = 0.00;
        double maternSlicTotal = 0.00;
        double fltDiscAmtTotal = 0.00;
        double totFinalPremium = 0;
        double reInsCeedRate = 0.00;
        double reInsCedAmt = 0;
        double slicRetention = 0;
        double taxExpenses = 0;
        double slicRetenForRe = 0;
        double maleAge = 0;
        double femaleAge = 0;
        int childCount = 0;

        try
        {
            oconn.Open();
            string getPlan = "Select LIMIT" +
                              " from SLIGEN.AMP_PLANS_V2" +
                              " where sysdate between EFFECT_FROM and EFFECT_TO" +
                              " and CODE = :planCode";

            using (OracleCommand cmd = new OracleCommand(getPlan, oconn))
            {
                cmd.Parameters.Add("planCode", OdbcType.VarChar);
                cmd.Parameters["planCode"].Value = plan;

                OracleDataReader planReader = cmd.ExecuteReader();

                while (planReader.Read())
                {
                    if (!planReader.IsDBNull(0))
                    {
                        planLimit = planReader.GetDouble(0);
                    }
                }
                planReader.Close();
            }

            DataTable dtMemDetails = new DataTable();
            dtMemDetails.Columns.Add(new DataColumn("QuotNo", typeof(string)));
            dtMemDetails.Columns.Add(new DataColumn("MemId", typeof(string)));
            dtMemDetails.Columns.Add(new DataColumn("Category", typeof(string)));
            dtMemDetails.Columns.Add(new DataColumn("Gender", typeof(string)));
            dtMemDetails.Columns.Add(new DataColumn("Dob", typeof(string)));
            dtMemDetails.Columns.Add(new DataColumn("Age", typeof(double)));
            dtMemDetails.Columns.Add(new DataColumn("Height", typeof(double)));
            dtMemDetails.Columns.Add(new DataColumn("Weight", typeof(double)));
            dtMemDetails.Columns.Add(new DataColumn("BmiVal", typeof(double)));
            dtMemDetails.Columns.Add(new DataColumn("MobRate", typeof(double)));
            dtMemDetails.Columns.Add(new DataColumn("BaseRate", typeof(double)));
            dtMemDetails.Columns.Add(new DataColumn("MaternSlic", typeof(double)));
            dtMemDetails.Columns.Add(new DataColumn("FlDiscountedAmt", typeof(double)));
            dtMemDetails.Columns.Add(new DataColumn("FinalPremium", typeof(double)));
            dtMemDetails.Columns.Add(new DataColumn("BmiRate", typeof(double)));
            dtMemDetails.Columns.Add(new DataColumn("BmiLoading", typeof(double)));
            dtMemDetails.Columns.Add(new DataColumn("MobLoading", typeof(double)));

            string getQuotDetails = "Select QT_ID, MEM_ID, decode(MEM_TYPE, 'M', 'Main Life', 'S', 'Spouse', 'C', 'Child'), GENDER, to_char(DOB, 'yyyy/mm/dd'), AGE, HEIGHT_CM, WEIGHT_KG," +
                                    " BMI_VALUE, BMI_RATE" +
                                    " from SLIGEN.AMP_QT_MEM_DETAILS" +
                                    " where QT_ID = :quotNo";

            using (OracleCommand cmd = new OracleCommand(getQuotDetails, oconn))
            {
                cmd.Parameters.Add("quotNo", OdbcType.VarChar);
                cmd.Parameters["quotNo"].Value = quotNo;

                OracleDataReader quotReader = cmd.ExecuteReader();

                while (quotReader.Read())
                {
                    DataRow drCurrentRow = null;
                    drCurrentRow = dtMemDetails.NewRow();

                    if (!quotReader.IsDBNull(0))
                    {
                        drCurrentRow["QuotNo"] = quotReader.GetString(0);
                    }
                    if (!quotReader.IsDBNull(1))
                    {
                        drCurrentRow["MemId"] = quotReader.GetString(1);
                    }
                    if (!quotReader.IsDBNull(2))
                    {
                        drCurrentRow["Category"] = quotReader.GetString(2);
                    }
                    if (!quotReader.IsDBNull(3))
                    {
                        drCurrentRow["Gender"] = quotReader.GetString(3);
                    }
                    if (!quotReader.IsDBNull(4))
                    {
                        drCurrentRow["Dob"] = quotReader.GetString(4);
                    }
                    if (!quotReader.IsDBNull(5))
                    {
                        drCurrentRow["Age"] = quotReader.GetDouble(5);
                    }
                    if (!quotReader.IsDBNull(6))
                    {
                        drCurrentRow["Height"] = quotReader.GetDouble(6);
                    }
                    if (!quotReader.IsDBNull(7))
                    {
                        drCurrentRow["Weight"] = quotReader.GetDouble(7);
                    }                    
                    if (!quotReader.IsDBNull(8))
                    {
                        drCurrentRow["BmiVal"] = quotReader.GetDouble(8);
                    }
                    if (!quotReader.IsDBNull(9))
                    {
                        drCurrentRow["BmiRate"] = quotReader.GetDouble(9);
                    }

                    dtMemDetails.Rows.Add(drCurrentRow);
                }
                quotReader.Close();

            }

            Parameters paras = new Parameters();
            reInsLoading = paras.re_loadning;
            adminFeePerc = paras.admin_fee;
            polFee = paras.pol_fee;
            reInsCeedRate = paras.re_ceed_rate;

            if (planLimit != 0)
            {
                /*
                string getFamDiscRate = "Select DISC_RATE from SLIGEN.AMP_DISC_RATES" +
                                            " where NUMLIVES = :memCount" +
                                            " and PLAN = :plan" +
                                            " and sysdate between EFFECT_FROM and EFFECT_TO";

                using (OracleCommand cmd = new OracleCommand(getFamDiscRate, oconn))
                {
                    cmd.Parameters.Add("memCount", OdbcType.Int);
                    cmd.Parameters["memCount"].Value = dtMemDetails.Rows.Count;

                    cmd.Parameters.Add("plan", OdbcType.VarChar);
                    cmd.Parameters["plan"].Value = planLimit;

                    OracleDataReader famDiscntReader = cmd.ExecuteReader();

                    while (famDiscntReader.Read())
                    {
                        if (!famDiscntReader.IsDBNull(0))
                        {
                            famDiscPercent = famDiscntReader.GetDouble(0);
                        }
                    }
                    famDiscntReader.Close();
                }
                */

                DataRow[] foundChild = dtMemDetails.Select("Category = 'Child'");
                childCount = foundChild.Length;

                foreach (DataRow memRow in dtMemDetails.Rows)
                {
                    reInsBasePrem = 0.00;
                    reInsMatPrem = 0.00;
                    maternSlic = 0.00;
                    basePremium = 0.00;
                    bmiLoading = 0.00;
                    mobidLoading = 0.00;
                    mobRate = 0.00;
                    bmiRate = 0.00;

                    /*
                    string getPremiums = "Select MUNICH_RE_PREM, MUNICH_MAT_PREM, SLI_MAT_PREM" +
                                            " from SLIGEN.AMP_PREMIUMS" +
                                            " where GENDER = :gender" +
                                            " and AGE = :age" +
                                            " and PLAN_SUM = :planLim" +
                                            " and sysdate between EFFECT_FROM and EFFECT_TO";

                    using (OracleCommand cmd = new OracleCommand(getPremiums, oconn))
                    {
                        cmd.Parameters.Add("gender", OdbcType.VarChar);
                        cmd.Parameters["gender"].Value = memRow["Gender"].ToString().Substring(0,1);

                        cmd.Parameters.Add("age", OdbcType.Double);
                        cmd.Parameters["age"].Value = double.Parse(memRow["Age"].ToString());

                        cmd.Parameters.Add("planLim", OdbcType.Double);
                        cmd.Parameters["planLim"].Value = planLimit;

                        OracleDataReader premiumReader = cmd.ExecuteReader();

                        while (premiumReader.Read())
                        {
                            if (!premiumReader.IsDBNull(0))
                            {
                                reInsBasePrem = premiumReader.GetDouble(0);
                            }
                            if (!premiumReader.IsDBNull(1))
                            {
                                reInsMatPrem = premiumReader.GetDouble(1);
                            }
                            if (!premiumReader.IsDBNull(2))
                            {
                                maternSlic = premiumReader.GetDouble(2);
                            }
                        }
                        premiumReader.Close();
                    }

                    bmiRate = double.Parse(memRow["BmiRate"].ToString());

                    bmiLoading = reInsBasePrem * bmiRate;
                    mobidLoading = reInsBasePrem * mobRate;

                    basePremium = reInsBasePrem + bmiLoading + mobidLoading + reInsMatPrem;

                    double floDiscontdAmt = basePremium * famDiscPercent;
                    double finalPremium = (floDiscontdAmt / (1 - (reInsLoading - slicDiscount))) + maternSlic;

                    memRow["BaseRate"] = basePremium;
                    memRow["MaternSlic"] = maternSlic;
                    memRow["FlDiscountedAmt"] = floDiscontdAmt;
                    memRow["FinalPremium"] = finalPremium;
                    memRow["BmiRate"] = bmiRate;
                    memRow["BmiLoading"] = bmiLoading;
                    memRow["MobRate"] = bmiRate;
                    memRow["MobLoading"] = mobidLoading;

                    baseRateTotal = baseRateTotal + basePremium;
                    maternSlicTotal = maternSlicTotal + maternSlic;
                    fltDiscAmtTotal = fltDiscAmtTotal + floDiscontdAmt;
                    finalPremTotal = finalPremTotal + finalPremium;

                    */

                    string memCategory = memRow["Category"].ToString();
                    string memGender = memRow["Gender"].ToString().Substring(0, 1);
                    double ageInYrs = Math.Ceiling(double.Parse(memRow["Age"].ToString()));

                    if (memCategory == "Main Life" || memCategory == "Spouse")
                    {
                        if (memGender == "M")
                        {
                            maleAge = ageInYrs;
                        }
                        else if (memGender == "F")
                        {
                            femaleAge = ageInYrs;
                        }
                    }
                }

                string getPremiums = "Select PREMIUM" +
                                         " from SLIGEN.AMP_PREMIUMS_V2" +
                                         " where M_AGE_FROM <= " + maleAge +
                                         " and M_AGE_TO >= " + maleAge +
                                         " and F_AGE_FROM <= " + femaleAge +
                                         " and F_AGE_TO >= " + femaleAge +
                                         " and CHILD_COUNT = " + childCount +
                                         " and PLAN_SUM = " + planLimit.ToString() +
                                         " and sysdate between EFFECT_FROM and EFFECT_TO";

                using (OracleCommand cmd = new OracleCommand(getPremiums, oconn))
                {
                    OracleDataReader premiumReader = cmd.ExecuteReader();

                    while (premiumReader.Read())
                    {
                        if (!premiumReader.IsDBNull(0))
                        {
                            finalPremTotal = premiumReader.GetDouble(0);
                        }

                    }
                    premiumReader.Close();
                }


                adminFee = finalPremTotal * adminFeePerc;

                //-----------------NBL and VAT Calculation--------------------------------        
                using (OracleCommand cmd = new OracleCommand("GENPAY.CALCULATE_NBL_AND_VAT", oconn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("taxLiableAmount", finalPremTotal + adminFee + polFee);
                    cmd.Parameters.AddWithValue("requestDate", DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture));
                    cmd.Parameters.Add("nblAmount", OracleType.Number).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("vatAmount", OracleType.Number).Direction = ParameterDirection.Output;

                    OracleDataReader dr = cmd.ExecuteReader();

                    nbtAmount = double.Parse(cmd.Parameters["nblAmount"].Value.ToString());
                    vatAmount = double.Parse(cmd.Parameters["vatAmount"].Value.ToString());

                    dr.Close();
                }
                //------------------------------------------------------------------------------- 

                totFinalPremium = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                //reInsCedAmt = fltDiscAmtTotal * reInsCeedRate;
               // slicRetention = finalPremTotal - reInsCedAmt;
                taxExpenses = adminFee + polFee + nbtAmount + vatAmount;
               // slicRetenForRe = fltDiscAmtTotal - reInsCedAmt;                

                AMP_Quotation_mast mastUpdate = new AMP_Quotation_mast();

                bool success = mastUpdate.Update_rec(quotNo, famDiscPercent, finalPremTotal, adminFee, polFee, nbtAmount, vatAmount, totFinalPremium, reInsCedAmt, slicRetention, taxExpenses, 
                                                     planLimit, 0.00, planLimit, plan, dtMemDetails, slicDiscount, slicRetenForRe, reInsLoading);
                if (!success)
                {
                    mesg = "Quotation cannot be created due to internal error";
                }

            }
            else
            {
                mesg = "Quotation cannot be created - internal error.";
            }

        }
        catch (Exception e)
        {
            mesg = "Error while updating quotation.";
            // Log your error         
            log logger = new log();
            logger.write_log("Failed at updateAMPQuotation: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }

        return mesg;
    }

    public string getAMPQuotationDetails(string quotNo, out double height, out double weight, out string gender, out double premium, out string plan, out double planLimit, out DataTable dtQtMemDetails)
    {
        string mesg = "success";
        height = 0;
        weight = 0;
        gender = "";
        premium = 0;
        plan = "";
        planLimit = 0;
        dtQtMemDetails = null;

        try
        {           
            oconn.Open();
            string getQuot = "Select FINAL_PREMIUM, PLAN, PLAN_LIMIT" +
                              " from SLIGEN.QUOT_MAST" +
                              " where QUOT_NUMBER = :quotNo";

            using (OracleCommand cmd = new OracleCommand(getQuot, oconn))
            {
                cmd.Parameters.Add("quotNo", OdbcType.VarChar);
                cmd.Parameters["quotNo"].Value = quotNo;

                OracleDataReader quotReader = cmd.ExecuteReader();

                while (quotReader.Read())
                {
                    if (!quotReader.IsDBNull(0))
                    {
                        premium = quotReader.GetDouble(0);
                    }
                    if (!quotReader.IsDBNull(1))
                    {
                        plan = quotReader.GetString(1);
                    }
                    if (!quotReader.IsDBNull(2))
                    {
                        planLimit = quotReader.GetDouble(2);
                    }
                }

                quotReader.Close();
            }

            if (plan != "")
            {
                string getMainLDetails = "Select HEIGHT_CM, WEIGHT_KG, decode(GENDER, 'F', 'Female', 'M', 'Male') GENDER" +
                                        " from SLIGEN.AMP_QT_MEM_DETAILS" +
                                        " where QT_ID = :quotNo" +
                                        " and MEM_TYPE = 'M'";

                using (OracleCommand cmd = new OracleCommand(getMainLDetails, oconn))
                {
                    cmd.Parameters.Add("quotNo", OdbcType.VarChar);
                    cmd.Parameters["quotNo"].Value = quotNo;

                    OracleDataReader mainDetReader = cmd.ExecuteReader();

                    while (mainDetReader.Read())
                    {
                        if (!mainDetReader.IsDBNull(0))
                        {
                            height = mainDetReader.GetDouble(0);
                        }
                        if (!mainDetReader.IsDBNull(1))
                        {
                            weight = mainDetReader.GetDouble(1);
                        }
                        if (!mainDetReader.IsDBNull(2))
                        {
                            gender = mainDetReader.GetString(2);
                        }
                    }

                    mainDetReader.Close();
                }

                DataTable dtMemDetails = new DataTable();
                dtMemDetails.Columns.Add(new DataColumn("QuotNo", typeof(string)));
                dtMemDetails.Columns.Add(new DataColumn("MemId", typeof(string)));
                dtMemDetails.Columns.Add(new DataColumn("Category", typeof(string)));
                dtMemDetails.Columns.Add(new DataColumn("Gender", typeof(string)));
                dtMemDetails.Columns.Add(new DataColumn("Dob", typeof(string)));
                dtMemDetails.Columns.Add(new DataColumn("Age", typeof(double)));
                dtMemDetails.Columns.Add(new DataColumn("Height", typeof(double)));
                dtMemDetails.Columns.Add(new DataColumn("Weight", typeof(double)));


                string getQuotDetails = "Select QT_ID, MEM_ID, decode(MEM_TYPE,'S', 'Spouse', 'C', 'Child') MEM_TYPE, decode(GENDER, 'F', 'Female', 'M', 'Male') GENDER, to_char(DOB, 'yyyy/mm/dd') DOB, AGE, HEIGHT_CM, WEIGHT_KG" +
                                        " from SLIGEN.AMP_QT_MEM_DETAILS" +
                                        " where QT_ID = :quotNo" +
                                        " and MEM_TYPE != 'M'" +
                                        " order by MEM_ID";

                using (OracleCommand cmd = new OracleCommand(getQuotDetails, oconn))
                {
                    cmd.Parameters.Add("quotNo", OdbcType.VarChar);
                    cmd.Parameters["quotNo"].Value = quotNo;

                    OracleDataReader quotReader = cmd.ExecuteReader();

                    while (quotReader.Read())
                    {
                        DataRow drCurrentRow = null;
                        drCurrentRow = dtMemDetails.NewRow();

                        if (!quotReader.IsDBNull(0))
                        {
                            drCurrentRow["QuotNo"] = quotReader.GetString(0);
                        }
                        if (!quotReader.IsDBNull(1))
                        {
                            drCurrentRow["MemId"] = quotReader.GetString(1);
                        }
                        if (!quotReader.IsDBNull(2))
                        {
                            drCurrentRow["Category"] = quotReader.GetString(2);
                        }
                        if (!quotReader.IsDBNull(3))
                        {
                            drCurrentRow["Gender"] = quotReader.GetString(3);
                        }
                        if (!quotReader.IsDBNull(4))
                        {
                            drCurrentRow["Dob"] = quotReader.GetString(4);
                        }
                        if (!quotReader.IsDBNull(5))
                        {
                            drCurrentRow["Age"] = quotReader.GetDouble(5);
                        }
                        if (!quotReader.IsDBNull(6))
                        {                            
                            drCurrentRow["Height"] = quotReader.GetDouble(6);
                        }
                        if (!quotReader.IsDBNull(7))
                        {
                            drCurrentRow["Weight"] = quotReader.GetDouble(7);
                        }

                        dtMemDetails.Rows.Add(drCurrentRow);
                    }

                    quotReader.Close();
                }

                if (dtMemDetails.Rows.Count > 0)
                {
                    dtQtMemDetails = dtMemDetails;
                }
            }
            else
            {
                mesg = "Quotation not found.";
            }
        }
        catch (Exception e)
        {
            mesg = "Error while getting quotation details.";
            // Log your error         
            log logger = new log();
            logger.write_log("Failed at getAMPQuotationDetails: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }

        return mesg;
    }

    public bool insert_AMP_proposal(string username, string quotNo, string plan, double premium, string comDate, string startDate, string endDate, double sumIns, string title, 
                                    string name, string address, string gender, double height, double weight, string mobileNumber, string homeNumber, string ofcNumber, string email, string nic, string ppNo, string occupation, 
                                    string naturOfOccup, string emplName, GridView gvMembers)                            
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
            string adrs1 = "";
            string adrs2 = "";
            string adrs3 = "";
            string adrs4 = "";            

            address =  address.Replace("<br/>&nbsp;&nbsp", "#");
            string[] adrsArray = new string[4];
            adrsArray = address.Split('#');
            if (adrsArray.Length > 0)
            {
                adrs1 = adrsArray[0];
            }
            if (adrsArray.Length > 1)
            {
                adrs2 = adrsArray[1];
            }
            if (adrsArray.Length > 2)
            {
                adrs3 = adrsArray[2];
            }
            if (adrsArray.Length > 3)
            {
                adrs4 = adrsArray[3];
            }


            using (cmd)
            {
                string instPropDetails = "Insert into SLIC_NET.PROPOSAL_DETAILS(POL_TYPE, REF_NO, FULL_NAME, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, " +
                                                                                " MOBILE_NUMBER, HOME_NUMBER, OFFICE_NUMBER, EMAIL, NIC, LOC_ADRS1, " +
                                                                                " LOC_ADRS2, LOC_ADRS3, LOC_ADRS4, ASSIGNEE, DAMAGED_BEFORE, REJCTED_BEFORE, " +
                                                                                " REJCT_REASON, PLAN, SUM_ASSURD, ANU_PREMIUM, ADMIN_FEE, POL_FEE, NBT, VAT, COM_DATE, " +
                                                                                " STATUS, ENTRY_DATE, USERNAME, TITLE, PRODUCT_ID, END_DATE, PROFESSION, NATUR_OF_OCCUP, EMPLOYER_NAME, PASSPORT_NO)" +
                                                        " VALUES(:polTyp, :refNo, :fullNam, :adrs1, :adrs2, :adrs3, :adrs4, :mobNo, :homNo, :ofcNo, :email, " +
                                                        " :nic, :lcAdrs1, :lcAdrs2, :lcAdrs3, :lcAdrs4, null, :dmgBefore, :rejBefore, null, :plan, :sumAssurd, " +
                                                        " :anuPrem, :admnFee, :polFee, :nbt, :vat, :comDate, :status, sysdate, :username, :title, :prodId, :endDate, :prof, :naturOfOccup, :emplName, :ppNo)";

                
                cmd.CommandText = instPropDetails;

                OracleParameter oPolTyp = new OracleParameter();
                oPolTyp.DbType = DbType.String;
                oPolTyp.Value = "G";
                oPolTyp.ParameterName = "polTyp";

                OracleParameter oRefNo = new OracleParameter();
                oRefNo.DbType = DbType.String;
                oRefNo.Value = quotNo;
                oRefNo.ParameterName = "refNo";

                OracleParameter oName = new OracleParameter();
                oName.DbType = DbType.String;
                oName.Value = name;
                oName.ParameterName = "fullNam";

                OracleParameter oAdrs1 = new OracleParameter();
                oAdrs1.DbType = DbType.String;
                oAdrs1.Value = adrs1;
                oAdrs1.ParameterName = "adrs1";

                OracleParameter oAdrs2 = new OracleParameter();
                oAdrs2.DbType = DbType.String;
                oAdrs2.Value = adrs2;
                oAdrs2.ParameterName = "adrs2";

                OracleParameter oAdrs3 = new OracleParameter();
                oAdrs3.DbType = DbType.String;
                oAdrs3.Value = adrs3;
                oAdrs3.ParameterName = "adrs3";

                OracleParameter oAdrs4 = new OracleParameter();
                oAdrs4.DbType = DbType.String;
                oAdrs4.Value = adrs4;
                oAdrs4.ParameterName = "adrs4";

                OracleParameter oMobNo = new OracleParameter();
                oMobNo.DbType = DbType.String;
                oMobNo.Value = mobileNumber;
                oMobNo.ParameterName = "mobNo";

                OracleParameter oHomNo = new OracleParameter();
                oHomNo.DbType = DbType.String;
                oHomNo.Value = homeNumber;
                oHomNo.ParameterName = "homNo";

                OracleParameter oOfcNo = new OracleParameter();
                oOfcNo.DbType = DbType.String;
                oOfcNo.Value = ofcNumber;
                oOfcNo.ParameterName = "ofcNo";

                OracleParameter oEmail = new OracleParameter();
                oEmail.DbType = DbType.String;
                oEmail.Value = email;
                oEmail.ParameterName = "email";

                OracleParameter oNic = new OracleParameter();
                oNic.DbType = DbType.String;
                oNic.Value = nic;
                oNic.ParameterName = "nic";

                OracleParameter oLcAdrs1 = new OracleParameter();
                oLcAdrs1.DbType = DbType.String;
                oLcAdrs1.Value = "Not applicable";
                oLcAdrs1.ParameterName = "lcAdrs1";

                OracleParameter oLcAdrs2 = new OracleParameter();
                oLcAdrs2.DbType = DbType.String;
                oLcAdrs2.Value = "Not applicable";
                oLcAdrs2.ParameterName = "lcAdrs2";

                OracleParameter oLcAdrs3 = new OracleParameter();
                oLcAdrs3.DbType = DbType.String;
                oLcAdrs3.Value = "Not applicable";
                oLcAdrs3.ParameterName = "lcAdrs3";

                OracleParameter oLcAdrs4 = new OracleParameter();
                oLcAdrs4.DbType = DbType.String;
                oLcAdrs4.Value = "Not applicable";
                oLcAdrs4.ParameterName = "lcAdrs4";

                OracleParameter oDmgBefore = new OracleParameter();
                oDmgBefore.DbType = DbType.String;
                oDmgBefore.Value = "N";
                oDmgBefore.ParameterName = "dmgBefore";

                OracleParameter oRejBefore = new OracleParameter();
                oRejBefore.DbType = DbType.String;
                oRejBefore.Value = "N";
                oRejBefore.ParameterName = "rejBefore";

                OracleParameter oPlan = new OracleParameter();
                oPlan.DbType = DbType.String;
                oPlan.Value = plan;
                oPlan.ParameterName = "plan";

                OracleParameter oSumAssurd = new OracleParameter();
                oSumAssurd.DbType = DbType.Double;
                oSumAssurd.Value = sumIns;
                oSumAssurd.ParameterName = "sumAssurd";

                OracleParameter oAnuPrem = new OracleParameter();
                oAnuPrem.DbType = DbType.Double;
                oAnuPrem.Value = premium;
                oAnuPrem.ParameterName = "anuPrem";

                OracleParameter oAdmnFee = new OracleParameter();
                oAdmnFee.DbType = DbType.Double;
                oAdmnFee.Value = 0;
                oAdmnFee.ParameterName = "admnFee";

                OracleParameter oPolFee = new OracleParameter();
                oPolFee.DbType = DbType.Double;
                oPolFee.Value = 0;
                oPolFee.ParameterName = "polFee";

                OracleParameter oNbt = new OracleParameter();
                oNbt.DbType = DbType.Double;
                oNbt.Value = 0;
                oNbt.ParameterName = "nbt";

                OracleParameter oVat = new OracleParameter();
                oVat.DbType = DbType.Double;
                oVat.Value = 0;
                oVat.ParameterName = "vat";

                OracleParameter oComDate = new OracleParameter();
                oComDate.DbType = DbType.DateTime;
                oComDate.Value = comDate;
                oComDate.ParameterName = "comDate";

                OracleParameter oStatus = new OracleParameter();
                oStatus.DbType = DbType.String;
                oStatus.Value = "P";
                oStatus.ParameterName = "status";

                OracleParameter oUsername = new OracleParameter();
                oUsername.Value = username;
                oUsername.ParameterName = "username";

                OracleParameter oTitle = new OracleParameter();
                oTitle.DbType = DbType.String;
                oTitle.Value = title;
                oTitle.ParameterName = "title";

                OracleParameter oProdId = new OracleParameter();
                oProdId.DbType = DbType.String;
                oProdId.Value = "MP"; // new product change 2021/10/29
                oProdId.ParameterName = "prodId";

                OracleParameter oEndDate = new OracleParameter();
                oEndDate.DbType = DbType.DateTime;
                oEndDate.Value = endDate;
                oEndDate.ParameterName = "endDate";

                OracleParameter oProf = new OracleParameter();
                oProf.Value = occupation;
                oProf.ParameterName = "prof";

                OracleParameter oNaturOccup = new OracleParameter();
                oNaturOccup.Value = naturOfOccup;
                oNaturOccup.ParameterName = "naturOfOccup";

                OracleParameter oEmplName = new OracleParameter();
                oEmplName.Value = emplName;
                oEmplName.ParameterName = "emplName";

                OracleParameter oPPNum = new OracleParameter();
                oPPNum.DbType = DbType.String;
                oPPNum.Value = ppNo;
                oPPNum.ParameterName = "ppNo";

                cmd.Parameters.Add(oPolTyp);
                cmd.Parameters.Add(oRefNo);
                cmd.Parameters.Add(oName);
                cmd.Parameters.Add(oAdrs1);
                cmd.Parameters.Add(oAdrs2);
                cmd.Parameters.Add(oAdrs3);
                cmd.Parameters.Add(oAdrs4);
                cmd.Parameters.Add(oMobNo);
                cmd.Parameters.Add(oHomNo);
                cmd.Parameters.Add(oOfcNo);
                cmd.Parameters.Add(oEmail);
                cmd.Parameters.Add(oNic);
                cmd.Parameters.Add(oLcAdrs1);
                cmd.Parameters.Add(oLcAdrs2);
                cmd.Parameters.Add(oLcAdrs3);
                cmd.Parameters.Add(oLcAdrs4);
                cmd.Parameters.Add(oDmgBefore);
                cmd.Parameters.Add(oRejBefore);
                cmd.Parameters.Add(oPlan);
                cmd.Parameters.Add(oSumAssurd);
                cmd.Parameters.Add(oAnuPrem);
                cmd.Parameters.Add(oAdmnFee);
                cmd.Parameters.Add(oPolFee);
                cmd.Parameters.Add(oNbt);
                cmd.Parameters.Add(oVat);
                cmd.Parameters.Add(oComDate);
                cmd.Parameters.Add(oStatus);
                //cmd.Parameters.Add(oEntryDate);
                cmd.Parameters.Add(oUsername);
                cmd.Parameters.Add(oTitle);
                cmd.Parameters.Add(oProdId);
                cmd.Parameters.Add(oEndDate);
                cmd.Parameters.Add(oProf);
                cmd.Parameters.Add(oNaturOccup);
                cmd.Parameters.Add(oEmplName);
                cmd.Parameters.Add(oPPNum);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();


                foreach (GridViewRow row in gvMembers.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string memId = row.Cells[0].Text.Trim();
                        string memName = ((TextBox)row.FindControl("txtName")).Text.Trim();
                        string memNic = ((TextBox)row.FindControl("txtNIC")).Text.Trim();
                        string memPP = ((TextBox)row.FindControl("txtPP")).Text.Trim();

                        string updateMember = "Update SLIGEN.AMP_QT_MEM_DETAILS" +
                                              " set NAME = :name," +
                                              " NIC = :nic," +
                                              " PP_NO = :ppNo" +
                                              " where QT_ID = :quotNo" +
                                              " and MEM_ID = :memId";

                        cmd.CommandText = updateMember;

                        OracleParameter oQtNo = new OracleParameter();
                        oQtNo.DbType = DbType.String;
                        oQtNo.Value = quotNo;
                        oQtNo.ParameterName = "quotNo";

                        OracleParameter oMemId = new OracleParameter();
                        oMemId.DbType = DbType.String;
                        oMemId.Value = memId;
                        oMemId.ParameterName = "memId";

                        OracleParameter oMemName = new OracleParameter();
                        oMemName.DbType = DbType.String;
                        oMemName.Value = memName;
                        oMemName.ParameterName = "name";

                        OracleParameter oNicNum = new OracleParameter();
                        oNicNum.DbType = DbType.String;
                        oNicNum.Value = memNic;
                        oNicNum.ParameterName = "nic";

                        OracleParameter oPPNo = new OracleParameter();
                        oPPNo.DbType = DbType.String;
                        oPPNo.Value = memPP;
                        oPPNo.ParameterName = "ppNo";

                        cmd.Parameters.Add(oQtNo);
                        cmd.Parameters.Add(oMemId);
                        cmd.Parameters.Add(oMemName);
                        cmd.Parameters.Add(oNicNum);
                        cmd.Parameters.Add(oPPNo);

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        
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
            logger.write_log("Failed at insert_AMP_proposal: " + e.ToString());
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

    //-----------------------------------Globe trotter related methods-------------------------------------------------

    public ArrayList getCountryList()
    {
        ArrayList arrCountries = new ArrayList();

        try
        {
            oconn.Open();
            string getCountries = "select COUNTRY_ID, COUNTRY_NAME from SLIC_NET.COUNTRIES" +
                                  " Order by COUNTRY_NAME";

            using (OracleCommand cmd = new OracleCommand(getCountries, oconn))
            {
                OracleDataReader cntReader = cmd.ExecuteReader();

                while (cntReader.Read())
                {
                    if (!cntReader.IsDBNull(0))
                    {
                        arrCountries.Add(new ListItem(cntReader.GetString(1), cntReader.GetString(0)));
                    }
                }
                cntReader.Close();
            }

        }
        catch (Exception e)
        {
            arrCountries = null;
            log logger = new log();
            logger.write_log("Failed at getCountryList: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }

        return arrCountries;
    }

    public bool isValidCountryCode(string cntryCode)
    {
        bool returnValue = false;
        try
        {
            oconn.Open();
            string getCountry = "select count(*) from SLIC_NET.COUNTRIES" +
                                " where COUNTRY_ID = :country";

            using (OracleCommand cmd = new OracleCommand(getCountry, oconn))
            {
                OracleParameter oCountry = new OracleParameter();
                oCountry.DbType = DbType.String;
                oCountry.Value = cntryCode;
                oCountry.ParameterName = "country";

                cmd.Parameters.Add(oCountry);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    returnValue = true;
                }
            }

        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at isValidCountryCode: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }

        return returnValue;
    }

    public string validateVisitCountrs(string destination, DataTable visitingCountries)
    {
        string mesg = "success";

        if (isValidCountryCode(destination))
        {
            if (visitingCountries.Rows.Count > 0)
            {
                if (visitingCountries.Rows.Count <= 4)
                {
                    string fromCountry = "";
                    string toCountry = "";
                    DataRow visitCntRow = null;
                    DataRow visitCntPrevRow = null;

                    for (int i = 0; i <= visitingCountries.Rows.Count - 1; i++)
                    {
                        fromCountry = "";
                        toCountry = "";
                        visitCntRow = null;
                        visitCntPrevRow = null;

                        visitCntRow = visitingCountries.Rows[i];
                        if (i > 0)
                        {
                            visitCntPrevRow = visitingCountries.Rows[i - 1];
                        }
                        fromCountry = visitCntRow["FromCountry"].ToString().Trim();
                        toCountry = visitCntRow["ToCountry"].ToString().Trim();

                        if (isValidCountryCode(fromCountry) && isValidCountryCode(toCountry))
                        {
                            if (visitCntPrevRow != null)
                            {
                                if (fromCountry != visitCntPrevRow["ToCountry"].ToString())
                                {
                                    mesg = "Conflict in order of countries found";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            mesg = "Invalid Country code found";
                            break;
                        }

                    }
                }
                else
                {
                    mesg = "Visiting countries should not be more than 4";
                }
            }
        }
        else
        {
            mesg = "Destination is invalid";
        }

        return mesg;
    }

    public bool isCountryInContinent(string country, string continent)
    {
        bool returnValue = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string getCountry = "select count(*) from SLIC_NET.COUNTRIES" +
                                " where COUNTRY_ID = :country"+
                                " and CONTINENT = :continent";

            using (OracleCommand cmd = new OracleCommand(getCountry, oconn))
            {
                OracleParameter oCountry = new OracleParameter();
                oCountry.DbType = DbType.String;
                oCountry.Value = country;
                oCountry.ParameterName = "country";

                OracleParameter oContinent = new OracleParameter();
                oContinent.DbType = DbType.String;
                oContinent.Value = continent;
                oContinent.ParameterName = "continent";

                cmd.Parameters.Add(oCountry);
                cmd.Parameters.Add(oContinent);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    returnValue = true;
                }
            }

        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at isCountryInContinent: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }

        return returnValue;
    }

    public DataTable getGTValidPlans(string destination, DataTable visitingCountries)
    {
        DataSet dsPlans = new DataSet();
        DataTable dtPlans = new DataTable();

        //dsPlans = null;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string getPlans = "Select CODE, EXCLUDE1, EXCLUDE2, CONTINENT" +
                              " from SLIC_NET.GLOBE_TROT_SCHEMES";

            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPlans, oconn))
            {
                
                dsPlans.Clear();
                dataAdd.Fill(dsPlans);
            }

            if (dsPlans != null)
            {
                string planCode = "";
                string planExcl1 = "";
                string planExcl2 = "";
                string continent = "";
                string toCountry = "";

                dtPlans = dsPlans.Tables[0];

                for (int i = dtPlans.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow planRow = dtPlans.Rows[i];
                    planCode = planRow.ItemArray[0].ToString().Trim();
                    planExcl1 = planRow.ItemArray[1].ToString().Trim();
                    planExcl2 = planRow.ItemArray[2].ToString().Trim();
                    continent = planRow.ItemArray[3].ToString().Trim();

                    if (destination == planExcl1 || destination == planExcl2)
                    {
                        //dr.Delete();
                        dtPlans.Rows.Remove(planRow);
                    }
                    else if (continent != "ALL" && !isCountryInContinent(destination, continent))
                    {
                        dtPlans.Rows.Remove(planRow);
                    }
                    else
                    {
                        if (visitingCountries.Rows.Count > 0)
                        {
                            foreach (DataRow visitCtryRow in visitingCountries.Rows)
                            {
                                toCountry = visitCtryRow.ItemArray[2].ToString().Trim();

                                if (toCountry == planExcl1 || toCountry == planExcl2)
                                {
                                    //dr.Delete();
                                    dtPlans.Rows.Remove(planRow);
                                    break;
                                }
                                else if (continent != "ALL" && !isCountryInContinent(toCountry, continent))
                                {
                                    dtPlans.Rows.Remove(planRow);
                                    break;
                                }
                            }
                        }
                    }
                }

            }
        }
        catch (Exception e)
        {
            // Log your error         
            dtPlans = null;
            log logger = new log();
            logger.write_log("Failed at getGTValidPlans: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }

        return dtPlans;
    }

    public bool isValidGTPlan(string plan, string destination, DataTable visitingCountries)
    {
        bool retValue = false;
       
        DataSet dsPlans = new DataSet();
        DataTable dtPlans = new DataTable();

        //dsPlans = null;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string getPlans = "Select CODE, EXCLUDE1, EXCLUDE2, CONTINENT" +
                              " from SLIC_NET.GLOBE_TROT_SCHEMES" +
                              " where CODE = :plan";

            using (OracleCommand cmd = new OracleCommand(getPlans, oconn))
            {
                OracleParameter oPlan = new OracleParameter();
                oPlan.DbType = DbType.String;
                oPlan.Value = plan;
                oPlan.ParameterName = "plan";

                cmd.Parameters.Add(oPlan);

                using (OracleDataAdapter dataAdd = new OracleDataAdapter(cmd))
                {
                    dsPlans.Clear();
                    dataAdd.Fill(dsPlans);
                }
            }

            if (dsPlans != null)
            {
                string planCode = "";
                string planExcl1 = "";
                string planExcl2 = "";
                string continent = "";
                string toCountry = "";

                dtPlans = dsPlans.Tables[0];
                DataRow planRow = dtPlans.Rows[0];
                planCode = planRow.ItemArray[0].ToString().Trim();
                planExcl1 = planRow.ItemArray[1].ToString().Trim();
                planExcl2 = planRow.ItemArray[2].ToString().Trim();
                continent = planRow.ItemArray[3].ToString().Trim();

                if (!(destination == planExcl1 || destination == planExcl2))
                {
                    if (continent == "ALL" || isCountryInContinent(destination, continent))
                    {
                        if (visitingCountries.Rows.Count > 0)
                        {
                            foreach (DataRow visitCtryRow in visitingCountries.Rows)
                            {
                                toCountry = visitCtryRow.ItemArray[0].ToString().Trim();

                                if (toCountry == planExcl1 || toCountry == planExcl2)
                                {
                                    retValue = false;
                                    break;
                                }
                                else if (continent != "ALL" && !isCountryInContinent(toCountry, continent))
                                {
                                    retValue = false;
                                    break;
                                }
                                else
                                {
                                    retValue = true;
                                }
                            }
                        }
                        else
                        {
                            retValue = true;
                        }
                    }
                }              
                
            }
        }
        catch (Exception e)
        {
            // Log your error         
            dtPlans = null;
            log logger = new log();
            logger.write_log("Failed at isValidGTPlan: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }

        return retValue;
    }

    public string getAllGTPremiums(string uname, DataTable dtMemDetails, string destination, string fromDate, string toDate, string coverType, DataTable visitCntryList, GridView gvPlanDetails)
    {
        string mesg = "success";
        double premium_gb500 = 0;
        double premium_gb100 = 0;
        double premium_gb50 = 0;
        double premium_st100 = 0;
        double premium_st50 = 0;
        double premium_as25 = 0;

        try
        {
            string planCode = "";
            double grpDiscRate = 0;
            DateTime leaveDate;
            DateTime returnDate;
            double noOfDays = 0;
            double nbtAmount = 0;
            double vatAmount = 0;
            double adminFeePerc = 0;
            double adminFee = 0;
            double polFee = 0;

            double basePremium = 0.00;
            double finalPremTotal = 0;

            GTI_Parameters paras = new GTI_Parameters();
            adminFeePerc = paras.admin_fee;
            polFee = paras.pol_fee;

            leaveDate = DateTime.ParseExact(fromDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            returnDate = DateTime.ParseExact(toDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            noOfDays = (returnDate - leaveDate).TotalDays + 1;

            DataTable dtPlans = new DataTable();
            dtPlans = getGTValidPlans(destination, visitCntryList);

            if (dtPlans.Rows.Count > 0)
            {
                oconn.Open();                

                foreach (DataRow planRow in dtPlans.Rows)
                {
                    planCode = planRow.ItemArray[0].ToString();                    
                    finalPremTotal = 0;
                    nbtAmount = 0;
                    vatAmount = 0;

                    if (coverType == "G")
                    {
                        string getGrpDiscRate = "Select DISC_RATE from SLIC_NET.GLOBE_TROT_DISCOUNTS" +
                                                " where NUM_PERSONS = (select max(NUM_PERSONS) from SLIC_NET.GLOBE_TROT_DISCOUNTS where NUM_PERSONS  < :memCount)" +
                                                " and sysdate between EFFECT_FROM and EFFECT_TO";

                        using (OracleCommand cmd = new OracleCommand(getGrpDiscRate, oconn))
                        {
                            cmd.Parameters.Add("memCount", OdbcType.Int);
                            cmd.Parameters["memCount"].Value = dtMemDetails.Rows.Count;                            

                            OracleDataReader grpDiscntReader = cmd.ExecuteReader();

                            while (grpDiscntReader.Read())
                            {
                                if (!grpDiscntReader.IsDBNull(0))
                                {
                                    grpDiscRate = grpDiscntReader.GetDouble(0);
                                }
                            }
                            grpDiscntReader.Close();
                        }
                    }

                    foreach (DataRow memRow in dtMemDetails.Rows)
                    {                        
                        basePremium = 0.00;

                        string getPremium = "Select PREMIUM" +
                                            " from SLIC_NET.GLOBE_TROT_PREMIUMS" +
                                            " where PLAN = :plan" +
                                            " and AGE_FROM <= :age" +
                                            " and AGE_TO >= :age" +
                                            " and DAYS_FROM <= :noOfDays" +
                                            " and DAYS_TO >= :noOfDays";

                        using (OracleCommand cmd = new OracleCommand(getPremium, oconn))
                        {
                            OracleParameter oPlan = new OracleParameter();
                            oPlan.DbType = DbType.String;
                            oPlan.Value = planCode;
                            oPlan.ParameterName = "plan";

                            OracleParameter oAge = new OracleParameter();
                            oAge.DbType = DbType.Double;
                            oAge.Value = double.Parse(memRow["Age"].ToString());
                            oAge.ParameterName = "age";

                            OracleParameter oNoOfDays = new OracleParameter();
                            oNoOfDays.DbType = DbType.Double;
                            oNoOfDays.Value = noOfDays;
                            oNoOfDays.ParameterName = "noOfDays";

                            cmd.Parameters.Add(oPlan);
                            cmd.Parameters.Add(oAge);
                            cmd.Parameters.Add(oNoOfDays);

                            OracleDataReader premiumReader = cmd.ExecuteReader();

                            while (premiumReader.Read())
                            {
                                if (!premiumReader.IsDBNull(0))
                                {
                                    basePremium = premiumReader.GetDouble(0);
                                }
                                
                            }
                            premiumReader.Close();
                        }

                        if (coverType == "F")
                        {
                            if (memRow["Category"].ToString().Substring(0, 1) == "C")
                            {
                                if (double.Parse(memRow["Age"].ToString()) < 17)
                                {
                                    basePremium = basePremium / 2;
                                }
                            }
                        }

                        finalPremTotal = finalPremTotal + basePremium;

                    }
                   
                    if (coverType == "G")
                    {
                        finalPremTotal = finalPremTotal - (finalPremTotal * grpDiscRate);
                    }

                    //exchange rate convertion
                    double dollarValue = getDollarRate();

                    if (dollarValue != 0)
                    {
                        finalPremTotal = finalPremTotal * dollarValue;
                    }
                    else
                    {
                        mesg = "Exchange Rate convertion error";
                    }

                    if (mesg == "success")
                    {
                        adminFee = finalPremTotal * adminFeePerc;

                        //-----------------NBL and VAT Calculation--------------------------------        
                        using (OracleCommand cmd = new OracleCommand("GENPAY.CALCULATE_NBL_AND_VAT", oconn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("taxLiableAmount", finalPremTotal + adminFee + polFee);
                            cmd.Parameters.AddWithValue("requestDate", DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture));
                            cmd.Parameters.Add("nblAmount", OracleType.Number).Direction = ParameterDirection.Output;
                            cmd.Parameters.Add("vatAmount", OracleType.Number).Direction = ParameterDirection.Output;

                            OracleDataReader dr = cmd.ExecuteReader();

                            nbtAmount = double.Parse(cmd.Parameters["nblAmount"].Value.ToString());
                            vatAmount = double.Parse(cmd.Parameters["vatAmount"].Value.ToString());

                            dr.Close();
                        }
                        //------------------------------------------------------------------------------- 

                        if (planCode == "GB500")
                        {
                            premium_gb500 = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                        }
                        else if (planCode == "GB100")
                        {
                            premium_gb100 = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                        }
                        else if (planCode == "GB50")
                        {
                            premium_gb50 = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                        }
                        else if (planCode == "ST100")
                        {
                            premium_st100 = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                        }
                        else if (planCode == "ST50")
                        {
                            premium_st50 = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                        }
                        else if (planCode == "AS25")
                        {
                            premium_as25 = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                        }
                        else
                        {
                            mesg = "Premiums cannot be calculated - internal error.";
                        }
                    }

                }

                string refNo = "";
                if (mesg == "success")
                {
                    mesg = createGTProposal(uname, dtMemDetails, destination, fromDate, toDate, coverType, visitCntryList, "GB500", out refNo);
                }
                if (mesg == "success")
                {
                    string getPlanDetails = "Select BENEFIT, SUM_GB500, EXC_GB500, SUM_GB100, EXC_GB100, SUM_GB50, EXC_GB50," +
                                            " SUM_ST100, EXC_ST100, SUM_ST50, EXC_ST50, SUM_AS25, EXC_AS25" +
                                            " from SLIC_NET.GLOBE_TROT_BENEFITS";

                    OracleDataAdapter dataAdd = new OracleDataAdapter(getPlanDetails, oconn);
                    DataSet ds = new DataSet();
                    ds.Clear();
                    dataAdd.Fill(ds);
                    gvPlanDetails.DataSource = ds.Tables[0];
                    gvPlanDetails.DataBind();

                    GridViewRow gvHeaderRow = gvPlanDetails.HeaderRow;
                    gvHeaderRow.HorizontalAlign = HorizontalAlign.Center;
                    gvHeaderRow.Cells[0].HorizontalAlign = HorizontalAlign.Center; //------------- Not working!!

                    EncryptDecrypt dc = new EncryptDecrypt();
                    Dictionary<string, string> qs = new Dictionary<string, string>();

                    GridViewRow gvHeader0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    gvHeader0.Attributes["id"] = "top_rows";
                    TableCell header0Cell0 = new TableCell()
                    {
                        Text = "",
                        HorizontalAlign = HorizontalAlign.Left
                    };           
                    
                    TableHeaderCell header0Cell1 = new TableHeaderCell()
                    {
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    qs.Add("action", "print");
                    qs.Add("refNo", refNo);
                    qs.Add("plan", "GB500");
                    HyperLink hlPrGB500 = new HyperLink();
                    hlPrGB500.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/GT_Prop_print.aspx", qs);
                    hlPrGB500.Text = "Print";
                    hlPrGB500.Width = 50;
                    hlPrGB500.CssClass = "btn btn-print";
                    header0Cell1.Controls.Add(hlPrGB500);
                    //header0Cell1.BackColor = System.Drawing.Color.FromArgb(235, 194, 194);

                    TableHeaderCell header0Cell2 = new TableHeaderCell()
                    {
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    qs.Remove("plan");
                    qs.Add("plan", "GB100");
                    HyperLink hlPrGB100 = new HyperLink();
                    hlPrGB100.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/GT_Prop_print.aspx", qs);
                    hlPrGB100.Text = "Print";
                    hlPrGB100.CssClass = "btn btn-print";
                    hlPrGB100.Width = 50;
                    header0Cell2.Controls.Add(hlPrGB100);

                    TableHeaderCell header0Cell3 = new TableHeaderCell()
                    {
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    qs.Remove("plan");
                    qs.Add("plan", "GB50");
                    HyperLink hlPrGB50 = new HyperLink();
                    hlPrGB50.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/GT_Prop_print.aspx", qs);
                    hlPrGB50.Text = "Print";
                    hlPrGB50.CssClass = "btn btn-print";
                    hlPrGB50.Width = 50;
                    header0Cell3.Controls.Add(hlPrGB50);

                    TableHeaderCell header0Cell4 = new TableHeaderCell()
                    {
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    qs.Remove("plan");
                    qs.Add("plan", "ST100");
                    HyperLink hlPrST100 = new HyperLink();
                    hlPrST100.CssClass = "btn btn-print";
                    hlPrST100.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/GT_Prop_print.aspx", qs);
                    hlPrST100.Text = "Print";
                    hlPrST100.Width = 50;
                    header0Cell4.Controls.Add(hlPrST100);

                    TableHeaderCell header0Cell5 = new TableHeaderCell()
                    {
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    qs.Remove("plan");
                    qs.Add("plan", "ST50");
                    HyperLink hlPrST50 = new HyperLink();
                    hlPrST50.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/GT_Prop_print.aspx", qs);
                    hlPrST50.Text = "Print";
                    hlPrST50.CssClass = "btn btn-print";
                    hlPrST50.Width = 50;
                    header0Cell5.Controls.Add(hlPrST50);

                    TableHeaderCell header0Cell6 = new TableHeaderCell()
                    {
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    qs.Remove("plan");
                    qs.Add("plan", "AS25");
                    HyperLink hlPrAS25 = new HyperLink();
                    hlPrAS25.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/GT_Prop_print.aspx", qs);
                    hlPrAS25.Text = "Print";
                    hlPrAS25.Width = 50;
                    hlPrAS25.CssClass = "btn btn-print";
                    header0Cell6.Controls.Add(hlPrAS25);

                    header0Cell0.Style.Add("border-top-style", "none");
                    gvHeader0.Cells.Add(header0Cell0);
                    gvHeader0.Cells.Add(header0Cell1);
                    gvHeader0.Cells.Add(header0Cell2);
                    gvHeader0.Cells.Add(header0Cell3);
                    gvHeader0.Cells.Add(header0Cell4);
                    gvHeader0.Cells.Add(header0Cell5);
                    gvHeader0.Cells.Add(header0Cell6);

                    gvHeader0.Font.Bold = true;

                    GridViewRow gvHeader1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    gvHeader1.Attributes["id"] = "top_rows";
                    TableHeaderCell header1Cell0 = new TableHeaderCell()
                    {
                        Text = "",
                        HorizontalAlign = HorizontalAlign.Left
                    };
                    TableHeaderCell header1Cell1 = new TableHeaderCell()
                    {
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    
                    qs.Remove("action");
                    qs.Add("action", "buy");
                    qs.Remove("plan");
                    qs.Add("plan", "GB500");
                    HyperLink hlByGB500 = new HyperLink();
                    hlByGB500.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/GT_Prop_print.aspx", qs);
                    hlByGB500.Text = "Buy";
                    hlByGB500.Width = 50;
                    hlByGB500.CssClass = "btn btn-xx";
                    header1Cell1.Controls.Add(hlByGB500);
                    //header1Cell1.BackColor = System.Drawing.Color.FromArgb(235, 194, 194);

                    TableHeaderCell header1Cell2 = new TableHeaderCell()
                    {
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    qs.Remove("plan");
                    qs.Add("plan", "GB100");
                    HyperLink hlByGB100 = new HyperLink();
                    hlByGB100.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/GT_Prop_print.aspx", qs);
                    hlByGB100.Text = "Buy";
                    hlByGB100.CssClass = "btn btn-xx";
                    hlByGB100.Width = 50;
                    header1Cell2.Controls.Add(hlByGB100);

                    TableHeaderCell header1Cell3 = new TableHeaderCell()
                    {
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    qs.Remove("plan");
                    qs.Add("plan", "GB50");
                    HyperLink hlByGB50 = new HyperLink();
                    hlByGB50.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/GT_Prop_print.aspx", qs);
                    hlByGB50.Text = "Buy";
                    hlByGB50.CssClass = "btn btn-xx";
                    hlByGB50.Width = 50;
                    header1Cell3.Controls.Add(hlByGB50);

                    TableHeaderCell header1Cell4 = new TableHeaderCell()
                    {
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    qs.Remove("plan");
                    qs.Add("plan", "ST100");
                    HyperLink hlByST100 = new HyperLink();
                    hlByST100.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/GT_Prop_print.aspx", qs);
                    hlByST100.Text = "Buy";
                    hlByST100.Width = 50;
                    hlByST100.CssClass = "btn btn-xx";
                    header1Cell4.Controls.Add(hlByST100);

                    TableHeaderCell header1Cell5 = new TableHeaderCell()
                    {
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    qs.Remove("plan");
                    qs.Add("plan", "ST50");
                    HyperLink hlByST50 = new HyperLink();
                    hlByST50.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/GT_Prop_print.aspx", qs);
                    hlByST50.Text = "Buy";
                    hlByST50.CssClass = "btn btn-xx";
                    hlByST50.Width = 50;
                    header1Cell5.Controls.Add(hlByST50);

                    TableHeaderCell header1Cell6 = new TableHeaderCell()
                    {
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    qs.Remove("plan");
                    qs.Add("plan", "AS25");
                    HyperLink hlByAS25 = new HyperLink();
                    hlByAS25.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/GT_Prop_print.aspx", qs);
                    hlByAS25.Text = "Buy";
                    hlByAS25.Width = 50;
                    hlByAS25.CssClass = "btn btn-xx";
                    header1Cell6.Controls.Add(hlByAS25);

                    header1Cell0.Style.Add("border-bottom-style", "none");
                    gvHeader1.Cells.Add(header1Cell0);
                    gvHeader1.Cells.Add(header1Cell1);
                    gvHeader1.Cells.Add(header1Cell2);
                    gvHeader1.Cells.Add(header1Cell3);
                    gvHeader1.Cells.Add(header1Cell4);
                    gvHeader1.Cells.Add(header1Cell5);
                    gvHeader1.Cells.Add(header1Cell6);

                    gvHeader1.Font.Bold = true;                    

                    GridViewRow gvHeader2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    gvHeader2.Attributes["id"] = "top_rows";
                    gvHeader2.Font.Bold = true;

                    TableHeaderCell header2Cell0 = new TableHeaderCell()
                    {
                        Text = "Premium (Rs.)",
                        HorizontalAlign = HorizontalAlign.Left,

                    };
                    //header2Cell0.Font.Bold = true;

                    TableHeaderCell header2Cell1 = new TableHeaderCell()
                    {
                        Text = premium_gb500.ToString("N2"),
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2,
                        //BackColor = System.Drawing.Color.FromArgb(235, 194, 194),
                    };
                    //header2Cell1.Font.Bold = true;

                    TableHeaderCell header2Cell2 = new TableHeaderCell()
                    {
                        Text = premium_gb100.ToString("N2"),
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    TableHeaderCell header2Cell3 = new TableHeaderCell()
                    {
                        Text = premium_gb50.ToString("N2"),
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    TableHeaderCell header2Cell4 = new TableHeaderCell()
                    {
                        Text = premium_st100.ToString("N2"),
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    TableHeaderCell header2Cell5 = new TableHeaderCell()
                    {
                        Text = premium_st50.ToString("N2"),
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    TableHeaderCell header2Cell6 = new TableHeaderCell()
                    {
                        Text = premium_as25.ToString("N2"),
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };

                    gvHeader2.Cells.Add(header2Cell0);
                    gvHeader2.Cells.Add(header2Cell1);
                    gvHeader2.Cells.Add(header2Cell2);
                    gvHeader2.Cells.Add(header2Cell3);
                    gvHeader2.Cells.Add(header2Cell4);
                    gvHeader2.Cells.Add(header2Cell5);
                    gvHeader2.Cells.Add(header2Cell6);


                    GridViewRow gvHeader3 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    gvHeader3.Attributes["id"] = "top_rows";
                    gvHeader3.Font.Bold = true;

                    TableHeaderCell header3Cell0 = new TableHeaderCell()
                    {
                        Text = "Plan",
                        HorizontalAlign = HorizontalAlign.Left,

                    };


                    TableHeaderCell header3Cell1 = new TableHeaderCell()
                    {
                        Text = "Global 500",
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2,   
                        //BackColor = System.Drawing.Color.FromArgb(235, 194, 194),
                    };


                    TableHeaderCell header3Cell2 = new TableHeaderCell()
                    {
                        Text = "Global 100",
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    TableHeaderCell header3Cell3 = new TableHeaderCell()
                    {
                        Text = "Global 50",
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    TableHeaderCell header3Cell4 = new TableHeaderCell()
                    {
                        Text = "Standard 100",
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    TableHeaderCell header3Cell5 = new TableHeaderCell()
                    {
                        Text = "Standard 50",
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };
                    TableHeaderCell header3Cell6 = new TableHeaderCell()
                    {
                        Text = "Asia 25",
                        HorizontalAlign = HorizontalAlign.Center,
                        ColumnSpan = 2
                    };

                    gvHeader3.Cells.Add(header3Cell0);
                    gvHeader3.Cells.Add(header3Cell1);
                    gvHeader3.Cells.Add(header3Cell2);
                    gvHeader3.Cells.Add(header3Cell3);
                    gvHeader3.Cells.Add(header3Cell4);
                    gvHeader3.Cells.Add(header3Cell5);
                    gvHeader3.Cells.Add(header3Cell6);

                    //gvPlanDetails.Columns[1].HeaderStyle.BackColor = System.Drawing.Color.FromArgb(235, 194, 194);
                    //gvPlanDetails.Columns[2].HeaderStyle.BackColor = System.Drawing.Color.FromArgb(235, 194, 194);
                    //gvPlanDetails.Columns[1].ItemStyle.BackColor = System.Drawing.Color.FromArgb(235, 194, 194);
                    //gvPlanDetails.Columns[2].ItemStyle.BackColor = System.Drawing.Color.FromArgb(235, 194, 194);

                    if (premium_gb500 == 0)
                    {
                        gvHeader0.Cells[1].Visible = false;
                        gvHeader1.Cells[1].Visible = false;
                        gvHeader2.Cells[1].Visible = false;
                        gvHeader3.Cells[1].Visible = false;

                        gvPlanDetails.Columns[1].Visible = false;
                        gvPlanDetails.Columns[2].Visible = false;
                    }

                    if (premium_gb100 == 0)
                    {
                        gvHeader0.Cells[2].Visible = false;
                        gvHeader1.Cells[2].Visible = false;
                        gvHeader2.Cells[2].Visible = false;
                        gvHeader3.Cells[2].Visible = false;

                        gvPlanDetails.Columns[3].Visible = false;
                        gvPlanDetails.Columns[4].Visible = false;
                    }

                    if (premium_gb50 == 0)
                    {
                        gvHeader0.Cells[3].Visible = false;
                        gvHeader1.Cells[3].Visible = false;
                        gvHeader2.Cells[3].Visible = false;
                        gvHeader3.Cells[3].Visible = false;

                        gvPlanDetails.Columns[5].Visible = false;
                        gvPlanDetails.Columns[6].Visible = false;
                    }

                    if (premium_st100 == 0)
                    {
                        gvHeader0.Cells[4].Visible = false;
                        gvHeader1.Cells[4].Visible = false;
                        gvHeader2.Cells[4].Visible = false;
                        gvHeader3.Cells[4].Visible = false;

                        gvPlanDetails.Columns[7].Visible = false;
                        gvPlanDetails.Columns[8].Visible = false;
                    }

                    if (premium_st50 == 0)
                    {
                        gvHeader0.Cells[5].Visible = false;
                        
                        gvHeader1.Cells[5].Visible = false;
                        gvHeader2.Cells[5].Visible = false;
                        gvHeader3.Cells[5].Visible = false;

                        gvPlanDetails.Columns[9].Visible = false;
                        gvPlanDetails.Columns[10].Visible = false;
                    }

                    if (premium_as25 == 0)
                    {
                        gvHeader0.Cells[6].Visible = false;
                        gvHeader1.Cells[6].Visible = false;
                        gvHeader2.Cells[6].Visible = false;
                        gvHeader3.Cells[6].Visible = false;
                        
                        gvPlanDetails.Columns[11].Visible = false;
                        gvPlanDetails.Columns[12].Visible = false;
                    }
                    //gvPlanDetails.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                    gvPlanDetails.Controls[0].Controls.AddAt(0, gvHeader0);
                    gvPlanDetails.Controls[0].Controls.AddAt(0, gvHeader1);
                    gvPlanDetails.Controls[0].Controls.AddAt(0, gvHeader2);
                    gvPlanDetails.Controls[0].Controls.AddAt(0, gvHeader3);

                    
                }
                else
                {
                    mesg = "Premiums cannot be calculated - internal error1.";
                }

            }
            else
            {
                mesg = "Premiums cannot be calculated - internal error2.";
            }

        }
        catch (Exception e)
        {
            // Log your error
            mesg = "Error while calculating GT premium.";
            log logger = new log();
            logger.write_log("Failed at getAllGTPremiums: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }


        return mesg;
    }

    public string createGTProposal(string uname, DataTable dtMemDetails, string destination, string fromDate, string toDate, string coverType, DataTable visitCntryList, string plan, out string refNo)
    {
        string mesg = "success";
        refNo = "";

        double nbtAmount = 0;
        double vatAmount = 0;
        double grpDiscRate = 0;
        DateTime leaveDate;
        DateTime returnDate;
        double noOfDays = 0;
        double adminFeePerc = 0;
        double adminFee = 0;
        double polFee = 0;

        double basePremium = 0.00;
        double net_premium_usd = 0;
        double net_premium_rs = 0;
        double totFinalPremium = 0;
        double taxExpenses = 0;
        string visitCntr1 = "";
        string visitCntr2 = "";
        string visitCntr3 = "";
        string visitCntr4 = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            
            GTI_Parameters paras = new GTI_Parameters();
            adminFeePerc = paras.admin_fee;
            polFee = paras.pol_fee;

            leaveDate = DateTime.ParseExact(fromDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            returnDate = DateTime.ParseExact(toDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            noOfDays = (returnDate - leaveDate).TotalDays + 1;

            //if (plan is valid)------------
            //{
            if (coverType == "G")
            {
                string getGrpDiscRate = "Select DISC_RATE from SLIC_NET.GLOBE_TROT_DISCOUNTS" +
                                        " where NUM_PERSONS = (select max(NUM_PERSONS) from SLIC_NET.GLOBE_TROT_DISCOUNTS where NUM_PERSONS  < :memCount)" +
                                        " and sysdate between EFFECT_FROM and EFFECT_TO";

                using (OracleCommand cmd = new OracleCommand(getGrpDiscRate, oconn))
                {
                    cmd.Parameters.Add("memCount", OdbcType.Int);
                    cmd.Parameters["memCount"].Value = dtMemDetails.Rows.Count;

                    OracleDataReader grpDiscntReader = cmd.ExecuteReader();

                    while (grpDiscntReader.Read())
                    {
                        if (!grpDiscntReader.IsDBNull(0))
                        {
                            grpDiscRate = grpDiscntReader.GetDouble(0);
                        }
                    }
                    grpDiscntReader.Close();
                }
            }

                foreach (DataRow memRow in dtMemDetails.Rows)
                {
                    basePremium = 0.00;

                    string getPremium = "Select PREMIUM" +
                                        " from SLIC_NET.GLOBE_TROT_PREMIUMS" +
                                        " where PLAN = :plan" +
                                        " and AGE_FROM <= :age" +
                                        " and AGE_TO >= :age" +
                                        " and DAYS_FROM <= :noOfDays" +
                                        " and DAYS_TO >= :noOfDays";

                    using (OracleCommand cmd = new OracleCommand(getPremium, oconn))
                    {
                        OracleParameter oPlan = new OracleParameter();
                        oPlan.DbType = DbType.String;
                        oPlan.Value = plan;
                        oPlan.ParameterName = "plan";

                        OracleParameter oAge = new OracleParameter();
                        oAge.DbType = DbType.Double;
                        oAge.Value = double.Parse(memRow["Age"].ToString());
                        oAge.ParameterName = "age";

                        OracleParameter oNoOfDays = new OracleParameter();
                        oNoOfDays.DbType = DbType.Double;
                        oNoOfDays.Value = noOfDays;
                        oNoOfDays.ParameterName = "noOfDays";

                        cmd.Parameters.Add(oPlan);
                        cmd.Parameters.Add(oAge);
                        cmd.Parameters.Add(oNoOfDays);

                        OracleDataReader premiumReader = cmd.ExecuteReader();

                        while (premiumReader.Read())
                        {
                            if (!premiumReader.IsDBNull(0))
                            {
                                basePremium = premiumReader.GetDouble(0);
                            }

                        }
                        premiumReader.Close();
                    }

                    if (coverType == "F")
                    {
                        if (memRow["Category"].ToString().Substring(0, 1) == "C")
                        {
                            if (double.Parse(memRow["Age"].ToString()) < 17)
                            {
                                basePremium = basePremium / 2;
                            }
                        }
                    }

                    memRow["BasePremium"] = basePremium;
                    net_premium_usd = net_premium_usd + basePremium;
                }                

                if (coverType == "G")
                {
                    net_premium_usd = net_premium_usd - (net_premium_usd * grpDiscRate);
                }

                //exchange rate convertion
                double dollarValue = getDollarRate();

                if (dollarValue != 0)
                {
                    net_premium_rs = net_premium_usd * dollarValue;
                }
                else
                {
                    mesg = "Exchange Rate convertion error";
                }

                if (mesg == "success")
                {
                    adminFee = net_premium_rs * adminFeePerc;

                    //-----------------NBL and VAT Calculation--------------------------------        
                    using (OracleCommand cmd = new OracleCommand("GENPAY.CALCULATE_NBL_AND_VAT", oconn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("taxLiableAmount", net_premium_rs + adminFee + polFee);
                        cmd.Parameters.AddWithValue("requestDate", DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture));
                        cmd.Parameters.Add("nblAmount", OracleType.Number).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("vatAmount", OracleType.Number).Direction = ParameterDirection.Output;

                        OracleDataReader dr = cmd.ExecuteReader();

                        nbtAmount = double.Parse(cmd.Parameters["nblAmount"].Value.ToString());
                        vatAmount = double.Parse(cmd.Parameters["vatAmount"].Value.ToString());

                        dr.Close();
                    }
                    //------------------------------------------------------------------------------- 

                    totFinalPremium = net_premium_rs + adminFee + polFee + nbtAmount + vatAmount;
                    taxExpenses = adminFee + polFee + nbtAmount + vatAmount;

                    CustProfile profile = new CustProfile(uname);

                    GT_Proposal_mast mastEntry = new GT_Proposal_mast();
                    string name = profile.O_firstName + " "  + profile.O_othNames + " " + profile.O_lastName;

                    foreach (DataRow cntyRow in visitCntryList.Rows)
                    {
                        if (visitCntr1 == "")
                        {
                            visitCntr1 = cntyRow.ItemArray[2].ToString().Trim();
                        }
                        else if (visitCntr2 == "")
                        {
                            visitCntr2 = cntyRow.ItemArray[2].ToString().Trim();
                        }
                        else if (visitCntr3 == "")
                        {
                            visitCntr3 = cntyRow.ItemArray[2].ToString().Trim();
                        }
                        else if (visitCntr4 == "")
                        {
                            visitCntr4 = cntyRow.ItemArray[2].ToString().Trim();
                        }
                    }
                    bool success = mastEntry.Insert_rec(profile.O_title, name, profile.O_addrss1, profile.O_addrss2, profile.O_addrss3,
                                        profile.O_addrss4, profile.O_homeNumber, profile.O_mobileNumber, profile.O_officeNumber, dtMemDetails.Rows.Count, grpDiscRate, plan,
                                        destination, fromDate, toDate, visitCntr1, visitCntr2, visitCntr3, visitCntr4, "to be updated", "to be updated", "", "", "", "", "to be updated", "", coverType,
                                        net_premium_usd, net_premium_rs, adminFee, polFee, nbtAmount, vatAmount, totFinalPremium, taxExpenses, noOfDays, dtMemDetails, dollarValue, out refNo);
                    if (!success)
                    {
                        mesg = "Quotation cannot be created due to internal error";
                    }
                }
            //}
            //else
            //{
            //    mesg = "Quotation cannot be created - internal error.";
            //}

        }
        catch (Exception e)
        {
            mesg = "Error while creating GT quotation.";
            // Log your error         
            log logger = new log();
            logger.write_log("Failed at createGTProposal: " + e.ToString());
           //System.Web.Response.Redirect("");
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }

        return mesg;
    }

    public string updateGTProposal(string plan, string refNo)
    {
        string mesg = "success";

        double nbtAmount = 0;
        double vatAmount = 0;
        double grpDiscRate = 0;        
        string coverType = "";
        double noOfDays = 0;
        double adminFeePerc = 0;
        double adminFee = 0;
        double polFee = 0;

        double basePremium = 0.00;
        double net_premium_usd = 0;
        double net_premium_rs = 0;
        double totFinalPremium = 0;
        double taxExpenses = 0;
        string destination = "";
        DataTable visitContrys = new DataTable();

        try
        {
            visitContrys.Columns.Add(new DataColumn("ToCountry", typeof(string)));

            oconn.Open();
            //check if plan is valid ----- this maybe necessary as the plan is updated

            string getPropDetails = "Select NO_OF_DAYS, GT_TYPE, GROUP_DISC_RATE, DESTINATION, VISIT_CTRY1, VISIT_CTRY2, VISIT_CTRY3, VISIT_CTRY4" +
                                    " from SLIC_NET.GT_PROP_MAST" +
                                    " where REFNO = :refNo";

            using (OracleCommand cmd = new OracleCommand(getPropDetails, oconn))
            {
                cmd.Parameters.Add("refNo", OdbcType.VarChar);
                cmd.Parameters["refNo"].Value = refNo;

                OracleDataReader propReader = cmd.ExecuteReader();

                while (propReader.Read())
                {
                    if (!propReader.IsDBNull(0))
                    {
                        noOfDays = propReader.GetDouble(0);
                    }                   
                    if (!propReader.IsDBNull(1))
                    {
                        coverType = propReader.GetString(1);                                                                                                                                                           
                    }
                    if (!propReader.IsDBNull(2))
                    {
                        grpDiscRate = propReader.GetDouble(2);
                    }
                    if (!propReader.IsDBNull(3))
                    {
                        destination = propReader.GetString(3);
                    }
                    if (!propReader.IsDBNull(4))
                    {
                        DataRow drCurrentRow = null;
                        drCurrentRow = visitContrys.NewRow();
                        drCurrentRow["ToCountry"] = propReader.GetString(4);
                        visitContrys.Rows.Add(drCurrentRow);
                    }
                }
                propReader.Close();
            }

            if (isValidGTPlan(plan, destination, visitContrys))
            {
                DataTable dtMemDetails = new DataTable();
                dtMemDetails.Columns.Add(new DataColumn("RefNo", typeof(string)));
                dtMemDetails.Columns.Add(new DataColumn("MemId", typeof(string)));
                dtMemDetails.Columns.Add(new DataColumn("Category", typeof(string)));
                dtMemDetails.Columns.Add(new DataColumn("Gender", typeof(string)));
                dtMemDetails.Columns.Add(new DataColumn("Dob", typeof(string)));
                dtMemDetails.Columns.Add(new DataColumn("Age", typeof(double)));
                dtMemDetails.Columns.Add(new DataColumn("BasePremium", typeof(double)));

                if (oconn.State != ConnectionState.Open)
                {
                    oconn.Open();
                }

                double sumInsured = 0;
                string getSumIns = "Select SUM_INS_USD from SLIC_NET.GLOBE_TROT_SCHEMES" +
                                   " where CODE = :plan";

                using (OracleCommand cmd = new OracleCommand(getSumIns, oconn))
                {
                    cmd.Parameters.Add("plan", OdbcType.VarChar);
                    cmd.Parameters["plan"].Value = plan;

                    OracleDataReader sumInsReader = cmd.ExecuteReader();

                    while (sumInsReader.Read())
                    {
                        if (!sumInsReader.IsDBNull(0))
                        {
                            sumInsured = sumInsReader.GetDouble(0);
                        }
                    }
                    sumInsReader.Close();
                }

                string getMemDetails = "Select REF_NO, MEM_ID, MEM_TYPE, GENDER, to_char(DOB, 'yyyy/mm/dd'), AGE" +
                                        " from SLIC_NET.GT_MEM_DETAILS" +
                                        " where REF_NO = :refNo";

                using (OracleCommand cmd = new OracleCommand(getMemDetails, oconn))
                {
                    cmd.Parameters.Add("refNo", OdbcType.VarChar);
                    cmd.Parameters["refNo"].Value = refNo;

                    OracleDataReader memReader = cmd.ExecuteReader();

                    while (memReader.Read())
                    {
                        DataRow drCurrentRow = null;
                        drCurrentRow = dtMemDetails.NewRow();

                        if (!memReader.IsDBNull(0))
                        {
                            drCurrentRow["RefNo"] = memReader.GetString(0);
                        }
                        if (!memReader.IsDBNull(1))
                        {
                            drCurrentRow["MemId"] = memReader.GetString(1);
                        }
                        if (!memReader.IsDBNull(2))
                        {
                            drCurrentRow["Category"] = memReader.GetString(2);
                        }
                        if (!memReader.IsDBNull(3))
                        {
                            drCurrentRow["Gender"] = memReader.GetString(3);
                        }
                        if (!memReader.IsDBNull(4))
                        {
                            drCurrentRow["Dob"] = memReader.GetString(4);
                        }
                        if (!memReader.IsDBNull(5))
                        {
                            drCurrentRow["Age"] = memReader.GetDouble(5);
                        }

                        dtMemDetails.Rows.Add(drCurrentRow);
                    }
                    memReader.Close();

                }

                GTI_Parameters paras = new GTI_Parameters();
                adminFeePerc = paras.admin_fee;
                polFee = paras.pol_fee;


                foreach (DataRow memRow in dtMemDetails.Rows)
                {
                    basePremium = 0.00;

                    string getPremium = "Select PREMIUM" +
                                        " from SLIC_NET.GLOBE_TROT_PREMIUMS" +
                                        " where PLAN = :plan" +
                                        " and AGE_FROM <= :age" +
                                        " and AGE_TO >= :age" +
                                        " and DAYS_FROM <= :noOfDays" +
                                        " and DAYS_TO >= :noOfDays";

                    using (OracleCommand cmd = new OracleCommand(getPremium, oconn))
                    {
                        OracleParameter oPlan = new OracleParameter();
                        oPlan.DbType = DbType.String;
                        oPlan.Value = plan;
                        oPlan.ParameterName = "plan";

                        OracleParameter oAge = new OracleParameter();
                        oAge.DbType = DbType.Double;
                        oAge.Value = double.Parse(memRow["Age"].ToString());
                        oAge.ParameterName = "age";

                        OracleParameter oNoOfDays = new OracleParameter();
                        oNoOfDays.DbType = DbType.Double;
                        oNoOfDays.Value = noOfDays;
                        oNoOfDays.ParameterName = "noOfDays";

                        cmd.Parameters.Add(oPlan);
                        cmd.Parameters.Add(oAge);
                        cmd.Parameters.Add(oNoOfDays);

                        OracleDataReader premiumReader = cmd.ExecuteReader();

                        while (premiumReader.Read())
                        {
                            if (!premiumReader.IsDBNull(0))
                            {
                                basePremium = premiumReader.GetDouble(0);
                            }
                        }
                        premiumReader.Close();
                    }

                    if (coverType == "F")
                    {
                        if (memRow["Category"].ToString().Substring(0, 1) == "C")
                        {
                            if (double.Parse(memRow["Age"].ToString()) < 17)
                            {
                                basePremium = basePremium / 2;
                            }
                        }
                    }

                    memRow["BasePremium"] = basePremium;
                    net_premium_usd = net_premium_usd + basePremium;
                }

                if (coverType == "G")
                {
                    net_premium_usd = net_premium_usd - (net_premium_usd * grpDiscRate);
                }

                //exchange rate convertion
                double dollarValue = getDollarRate();

                if (dollarValue != 0)
                {
                    net_premium_rs = net_premium_usd * dollarValue; 
                }
                else
                {
                    mesg = "Exchange Rate convertion error";
                }

                if (mesg == "success")
                {
                    adminFee = net_premium_rs * adminFeePerc;

                    //-----------------NBL and VAT Calculation--------------------------------        
                    using (OracleCommand cmd = new OracleCommand("GENPAY.CALCULATE_NBL_AND_VAT", oconn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("taxLiableAmount", net_premium_rs + adminFee + polFee);
                        cmd.Parameters.AddWithValue("requestDate", DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture));
                        cmd.Parameters.Add("nblAmount", OracleType.Number).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("vatAmount", OracleType.Number).Direction = ParameterDirection.Output;

                        OracleDataReader dr = cmd.ExecuteReader();

                        nbtAmount = double.Parse(cmd.Parameters["nblAmount"].Value.ToString());
                        vatAmount = double.Parse(cmd.Parameters["vatAmount"].Value.ToString());

                        dr.Close();
                    }
                    //------------------------------------------------------------------------------- 

                    totFinalPremium = net_premium_rs + adminFee + polFee + nbtAmount + vatAmount;
                    taxExpenses = adminFee + polFee + nbtAmount + vatAmount;

                    GT_Proposal_mast mastUpdate = new GT_Proposal_mast();

                    bool success = mastUpdate.Update_rec(refNo, plan, grpDiscRate, net_premium_usd, adminFee, polFee, nbtAmount, vatAmount, totFinalPremium, taxExpenses,
                                                         net_premium_rs, sumInsured, dtMemDetails, dollarValue);
                    if (!success)
                    {
                        mesg = "Quotation cannot be created due to internal error";
                    }
                }
            }
            else
            {
                mesg = "Quotation cannot be created due to internal error";
            }
        }
        catch (Exception e)
        {
            mesg = "Error while updating GT proposal.";
            // Log your error         
            log logger = new log();
            logger.write_log("Failed at updateGTProposal: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }
        return mesg;
    }

    public bool confirm_GT_proposal(string username, string refNo, string plan, double premium, string comDate, string endDate, string title,
                                    string fullName, string address, string mobileNumber, string homeNumber, string ofcNumber, string email, string nic,
                                    string travelPurpse, string contctName, string cntAdrs1, string cntAdrs2, string cntAdrs3, string cntAdrs4, 
                                    string contNo1, string contNo2, double sumIns_Usd, GridView gvMembers, string passport)
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
            string adrs1 = "";
            string adrs2 = "";
            string adrs3 = "";
            string adrs4 = "";

            address = address.Replace("<br/>&nbsp;&nbsp", "#");
            string[] adrsArray = new string[4];
            adrsArray = address.Split('#');
            if (adrsArray.Length > 0)
            {
                adrs1 = adrsArray[0];
            }
            if (adrsArray.Length > 1)
            {
                adrs2 = adrsArray[1];
            }
            if (adrsArray.Length > 2)
            {
                adrs3 = adrsArray[2];
            }
            if (adrsArray.Length > 3)
            {
                adrs4 = adrsArray[3];
            }


            using (cmd)
            {
                string instPropDetails = "Insert into SLIC_NET.PROPOSAL_DETAILS(POL_TYPE, REF_NO, FULL_NAME, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, " +
                                                                                " MOBILE_NUMBER, HOME_NUMBER, OFFICE_NUMBER, EMAIL, NIC, LOC_ADRS1, " +
                                                                                " LOC_ADRS2, LOC_ADRS3, LOC_ADRS4, ASSIGNEE, DAMAGED_BEFORE, REJCTED_BEFORE, " +
                                                                                " REJCT_REASON, PLAN, SUM_ASSURD, ANU_PREMIUM, ADMIN_FEE, POL_FEE, NBT, VAT, COM_DATE, " +
                                                                                " STATUS, ENTRY_DATE, USERNAME, TITLE, PRODUCT_ID, END_DATE, PASSPORT_NO)" +
                                                        " VALUES(:polTyp, :refNo, :fullNam, :adrs1, :adrs2, :adrs3, :adrs4, :mobNo, :homNo, :ofcNo, :email, " +
                                                        " :nic, :lcAdrs1, :lcAdrs2, :lcAdrs3, :lcAdrs4, null, :dmgBefore, :rejBefore, null, :plan, :sumAssurd, " +
                                                        " :anuPrem, :admnFee, :polFee, :nbt, :vat, :comDate, :status, sysdate, :username, :title, :prodId, :endDate, :ppno)";


                cmd.CommandText = instPropDetails;

                OracleParameter oPolTyp = new OracleParameter();
                //oPolTyp.DbType = DbType.String;
                oPolTyp.Value = "G";
                oPolTyp.ParameterName = "polTyp";

                OracleParameter oRefNo = new OracleParameter();
                //oRefNo.DbType = DbType.String;
                oRefNo.Value = refNo;
                oRefNo.ParameterName = "refNo";

                OracleParameter oName = new OracleParameter();
                //oName.DbType = DbType.String;
                oName.Value = fullName;
                oName.ParameterName = "fullNam";

                OracleParameter oAdrs1 = new OracleParameter();
                //oAdrs1.DbType = DbType.String;
                oAdrs1.Value = adrs1;
                oAdrs1.ParameterName = "adrs1";

                OracleParameter oAdrs2 = new OracleParameter();
                //oAdrs2.DbType = DbType.String;
                oAdrs2.Value = adrs2;
                oAdrs2.ParameterName = "adrs2";

                OracleParameter oAdrs3 = new OracleParameter();
                //oAdrs3.DbType = DbType.String;
                oAdrs3.Value = adrs3;
                oAdrs3.ParameterName = "adrs3";

                OracleParameter oAdrs4 = new OracleParameter();
                //oAdrs4.DbType = DbType.String;
                oAdrs4.Value = adrs4;
                oAdrs4.ParameterName = "adrs4";

                OracleParameter oMobNo = new OracleParameter();
                //oMobNo.DbType = DbType.String;
                oMobNo.Value = mobileNumber;
                oMobNo.ParameterName = "mobNo";

                OracleParameter oHomNo = new OracleParameter();
                //oHomNo.DbType = DbType.String;
                oHomNo.Value = homeNumber;
                oHomNo.ParameterName = "homNo";

                OracleParameter oOfcNo = new OracleParameter();
                //oOfcNo.DbType = DbType.String;
                oOfcNo.Value = ofcNumber;
                oOfcNo.ParameterName = "ofcNo";

                OracleParameter oEmail = new OracleParameter();
                //oEmail.DbType = DbType.String;
                oEmail.Value = email;
                oEmail.ParameterName = "email";

                OracleParameter oNic = new OracleParameter();
                //oNic.DbType = DbType.String;
                oNic.Value = (String.IsNullOrEmpty(nic)?"0000000000":nic);
                oNic.ParameterName = "nic";

                OracleParameter oLcAdrs1 = new OracleParameter();
                //oLcAdrs1.DbType = DbType.String;
                oLcAdrs1.Value = "Not applicable";
                oLcAdrs1.ParameterName = "lcAdrs1";

                OracleParameter oLcAdrs2 = new OracleParameter();
                //oLcAdrs2.DbType = DbType.String;
                oLcAdrs2.Value = "Not applicable";
                oLcAdrs2.ParameterName = "lcAdrs2";

                OracleParameter oLcAdrs3 = new OracleParameter();
                //oLcAdrs3.DbType = DbType.String;
                oLcAdrs3.Value = "Not applicable";
                oLcAdrs3.ParameterName = "lcAdrs3";

                OracleParameter oLcAdrs4 = new OracleParameter();
                oLcAdrs4.DbType = DbType.String;
                oLcAdrs4.Value = "Not applicable";
                oLcAdrs4.ParameterName = "lcAdrs4";

                OracleParameter oDmgBefore = new OracleParameter();
                //oDmgBefore.DbType = DbType.String;
                oDmgBefore.Value = "N";
                oDmgBefore.ParameterName = "dmgBefore";

                OracleParameter oRejBefore = new OracleParameter();
                oRejBefore.DbType = DbType.String;
                oRejBefore.Value = "N";
                oRejBefore.ParameterName = "rejBefore";

                OracleParameter oPlan = new OracleParameter();
                //oPlan.DbType = DbType.String;
                oPlan.Value = plan;
                oPlan.ParameterName = "plan";

                OracleParameter oSumAssurd = new OracleParameter();
                oSumAssurd.DbType = DbType.Double;
                oSumAssurd.Value = sumIns_Usd;
                oSumAssurd.ParameterName = "sumAssurd";

                OracleParameter oAnuPrem = new OracleParameter();
                oAnuPrem.DbType = DbType.Double;
                oAnuPrem.Value = premium;
                oAnuPrem.ParameterName = "anuPrem";

                OracleParameter oAdmnFee = new OracleParameter();
                oAdmnFee.DbType = DbType.Double;
                oAdmnFee.Value = 0;
                oAdmnFee.ParameterName = "admnFee";

                OracleParameter oPolFee = new OracleParameter();
                oPolFee.DbType = DbType.Double;
                oPolFee.Value = 0;
                oPolFee.ParameterName = "polFee";

                OracleParameter oNbt = new OracleParameter();
                oNbt.DbType = DbType.Double;
                oNbt.Value = 0;
                oNbt.ParameterName = "nbt";

                OracleParameter oVat = new OracleParameter();
                oVat.DbType = DbType.Double;
                oVat.Value = 0;
                oVat.ParameterName = "vat";

                OracleParameter oComDate = new OracleParameter();
                oComDate.DbType = DbType.Date;
                oComDate.Value = comDate;
                oComDate.ParameterName = "comDate";

                OracleParameter oStatus = new OracleParameter();
                oStatus.DbType = DbType.String;
                oStatus.Value = "P";
                oStatus.ParameterName = "status";

                OracleParameter oUsername = new OracleParameter();
                oUsername.Value = username;
                oUsername.ParameterName = "username";

                OracleParameter oTitle = new OracleParameter();
                //oTitle.DbType = DbType.String;
                oTitle.Value = title;
                oTitle.ParameterName = "title";

                OracleParameter oProdId = new OracleParameter();
                //oProdId.DbType = DbType.String;
                oProdId.Value = "GTI";
                oProdId.ParameterName = "prodId";

                OracleParameter oEndDate = new OracleParameter();
                oEndDate.DbType = DbType.Date;
                oEndDate.Value = endDate;
                oEndDate.ParameterName = "endDate";

                OracleParameter oppno = new OracleParameter();
                oppno.Value = passport;
                oppno.ParameterName = "ppno";

                cmd.Parameters.Add(oPolTyp);
                cmd.Parameters.Add(oRefNo);
                cmd.Parameters.Add(oName);
                cmd.Parameters.Add(oAdrs1);
                cmd.Parameters.Add(oAdrs2);
                cmd.Parameters.Add(oAdrs3);
                cmd.Parameters.Add(oAdrs4);
                cmd.Parameters.Add(oMobNo);
                cmd.Parameters.Add(oHomNo);
                cmd.Parameters.Add(oOfcNo);
                cmd.Parameters.Add(oEmail);
                cmd.Parameters.Add(oNic);
                cmd.Parameters.Add(oLcAdrs1);
                cmd.Parameters.Add(oLcAdrs2);
                cmd.Parameters.Add(oLcAdrs3);
                cmd.Parameters.Add(oLcAdrs4);
                cmd.Parameters.Add(oDmgBefore);
                cmd.Parameters.Add(oRejBefore);
                cmd.Parameters.Add(oPlan);
                cmd.Parameters.Add(oSumAssurd);
                cmd.Parameters.Add(oAnuPrem);
                cmd.Parameters.Add(oAdmnFee);
                cmd.Parameters.Add(oPolFee);
                cmd.Parameters.Add(oNbt);
                cmd.Parameters.Add(oVat);
                cmd.Parameters.Add(oComDate);
                cmd.Parameters.Add(oStatus);
                //cmd.Parameters.Add(oEntryDate);
                cmd.Parameters.Add(oUsername);
                cmd.Parameters.Add(oTitle);
                cmd.Parameters.Add(oProdId);
                cmd.Parameters.Add(oEndDate);
                cmd.Parameters.Add(oppno);           

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                string updateGTPropMast = "Update SLIC_NET.GT_PROP_MAST" +
                                        " set TRAVEL_PURPOSE = :travelPrps," +
                                        " CONTCT_NAME = :contName," +
                                        " CONTCT_ADRS1 = :contAdrs1, " +
                                        " CONTCT_ADRS2 = :contAdrs2, " +
                                        " CONTCT_ADRS3 = :contAdrs3, " +
                                        " CONTCT_ADRS4 = :contAdrs4, " +
                                        " CONTCT_NO1 = :contNo1, " +
                                        " CONTCT_NO2 = :contNo2" +
                                        " where REFNO = :refNo";

                cmd.CommandText = updateGTPropMast;

                OracleParameter oTravelPrps = new OracleParameter();
                oTravelPrps.DbType = DbType.String;
                oTravelPrps.Value = travelPurpse;
                oTravelPrps.ParameterName = "travelPrps";

                OracleParameter oContName = new OracleParameter();
                oContName.DbType = DbType.String;
                oContName.Value = contctName;
                oContName.ParameterName = "contName";

                OracleParameter oContAdrs1 = new OracleParameter();
                oContAdrs1.DbType = DbType.String;
                oContAdrs1.Value = cntAdrs1;
                oContAdrs1.ParameterName = "contAdrs1";

                OracleParameter oContAdrs2 = new OracleParameter();
                oContAdrs2.DbType = DbType.String;
                oContAdrs2.Value = cntAdrs2;
                oContAdrs2.ParameterName = "contAdrs2";

                OracleParameter oContAdrs3 = new OracleParameter();
                oContAdrs3.DbType = DbType.String;
                if (String.IsNullOrEmpty(cntAdrs3))
                {
                    oContAdrs3.Value = DBNull.Value;
                }
                else
                {
                    oContAdrs3.Value = cntAdrs3;
                }
                oContAdrs3.ParameterName = "contAdrs3";

                OracleParameter oContAdrs4 = new OracleParameter();
                oContAdrs4.DbType = DbType.String;
                if (String.IsNullOrEmpty(cntAdrs4))
                {
                    oContAdrs4.Value = DBNull.Value;
                }
                else
                {
                    oContAdrs4.Value = cntAdrs4;
                }
                oContAdrs4.ParameterName = "contAdrs4";

                OracleParameter oContNo1 = new OracleParameter();
                oContNo1.DbType = DbType.String;
                oContNo1.Value = contNo1;
                oContNo1.ParameterName = "contNo1";

                OracleParameter oContNo2 = new OracleParameter();
                oContNo2.DbType = DbType.String;
                if (String.IsNullOrEmpty(cntAdrs4))
                {
                    oContNo2.Value = DBNull.Value;
                }
                else
                {
                    oContNo2.Value = contNo2;
                }
                oContNo2.ParameterName = "contNo2";       
         


                ///////////////////////////////////////////////
                OracleParameter orefNo11 = new OracleParameter();
                orefNo11.Value = refNo;
                orefNo11.ParameterName = "refNo";


                cmd.Parameters.Add(oTravelPrps);
                cmd.Parameters.Add(oContName);
                cmd.Parameters.Add(oContAdrs1);
                cmd.Parameters.Add(oContAdrs2);
                cmd.Parameters.Add(oContAdrs3);
                cmd.Parameters.Add(oContAdrs4);
                cmd.Parameters.Add(oContNo1);
                cmd.Parameters.Add(oContNo2);
                cmd.Parameters.Add(orefNo11);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                ///////////////////////////////////////////////



                foreach (GridViewRow row in gvMembers.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string memId = row.Cells[0].Text.Trim();
                        string memName = ((TextBox)row.FindControl("txtName")).Text.Trim();
                        string memPP = ((TextBox)row.FindControl("txtPP")).Text.Trim();
                        string memTitle = ((DropDownList)row.FindControl("ddTitle")).SelectedValue.Trim();

                        string updateMember = "Update SLIC_NET.GT_MEM_DETAILS" +
                                              " set NAME = :name," +
                                              " PP_NO = :ppNo," +
                                              " TITLE = :title" +
                                              " where REF_NO = :refNo" +
                                              " and MEM_ID = :memId";

                        cmd.CommandText = updateMember;

                        OracleParameter oRefNo2 = new OracleParameter();
                        oRefNo2.DbType = DbType.String;
                        oRefNo2.Value = refNo;
                        oRefNo2.ParameterName = "refNo";

                        OracleParameter oMemId = new OracleParameter();
                        oMemId.DbType = DbType.String;
                        oMemId.Value = memId;
                        oMemId.ParameterName = "memId";

                        OracleParameter oMemName = new OracleParameter();
                        oMemName.DbType = DbType.String;
                        oMemName.Value = memName;
                        oMemName.ParameterName = "name";

                        OracleParameter oPPNum = new OracleParameter();
                        oPPNum.DbType = DbType.String;
                        oPPNum.Value = memPP;
                        oPPNum.ParameterName = "ppNo";

                        OracleParameter oMemTitle = new OracleParameter();
                        oMemTitle.DbType = DbType.String;
                        oMemTitle.Value = memTitle;
                        oMemTitle.ParameterName = "title";

                        cmd.Parameters.Add(oRefNo2);
                        cmd.Parameters.Add(oMemId);
                        cmd.Parameters.Add(oMemName);
                        cmd.Parameters.Add(oPPNum);
                        cmd.Parameters.Add(oMemTitle);                       

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

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
            logger.write_log("Failed at confirm_GT_proposal: " + e.ToString());
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

    public void fillTravelPurposeDdl(DropDownList ddlTrPurposes)
    {
        DataTable dtTrPurposes = new DataTable();
        try
        {
            oconn.Open();
            string getTrPurposes = "select VISIT_CODE, VISIT_REASON from SLIC_NET.GLOBE_TROT_PURPOSES";

            using (OracleDataAdapter adapter = new OracleDataAdapter(getTrPurposes, oconn))
            {
                adapter.Fill(dtTrPurposes);

                ddlTrPurposes.DataSource = dtTrPurposes;
                ddlTrPurposes.DataTextField = "VISIT_REASON";
                ddlTrPurposes.DataValueField = "VISIT_CODE";
                ddlTrPurposes.DataBind();
            }

        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at fillTravelPurposeDdl: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }
    }

    public void fillDdlTitles(GridView gvMembers)
    {
        DataTable dtTitles = new DataTable();
        try
        {
            oconn.Open();
            string getTitles = "select CODE, DESCRIPTION from SLIC_NET.TITLES";

            using (OracleDataAdapter adapter = new OracleDataAdapter(getTitles, oconn))
            {
                adapter.Fill(dtTitles);

                foreach (GridViewRow row in gvMembers.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        DropDownList ddlTitles = ((DropDownList)row.FindControl("ddTitle"));

                        ddlTitles.DataSource = dtTitles;
                        ddlTitles.DataTextField = "CODE";
                        ddlTitles.DataValueField = "DESCRIPTION";
                        ddlTitles.DataBind();
                        ddlTitles.SelectedValue = "Select";
                    }

                }
                
            }

        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at fillDdlTitles: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }
    }

    public double getDollarRate()
    {
        double dollarValue = 0;

        try{

            //oconn.Open();
            string getDollarRate = "Select CURAT from LPAY.CURRDATE" +
                                   " where CUDATE = (Select max(CUDATE) from LPAY.CURRDATE where CUTYPE = 'USD')" +
                                   " and CUTYPE = 'USD'";

            using (OracleCommand cmd = new OracleCommand(getDollarRate, oconn))
            {
                OracleDataReader dolRatReader = cmd.ExecuteReader();

                while (dolRatReader.Read())
                {
                    dollarValue = dolRatReader.GetDouble(0);
                }
                dolRatReader.Close();
            }

        }
        catch (Exception e)
        {
            dollarValue = 0;
            // Log your error         
            log logger = new log();
            logger.write_log("Failed at getDollarRate: " + e.ToString());
        }
        finally
        {
            //if (oconn != null) oconn.Close();
        }

        return dollarValue;
    }

    public double getDollarRate_Opn()
    {
        double dollarValue = 0;

        try
        {

            oconn.Open();
            string getDollarRate = "Select CURAT from LPAY.CURRDATE" +
                                   " where CUDATE = (Select max(CUDATE) from LPAY.CURRDATE where CUTYPE = 'USD')" +
                                   " and CUTYPE = 'USD'";

            using (OracleCommand cmd = new OracleCommand(getDollarRate, oconn))
            {
                OracleDataReader dolRatReader = cmd.ExecuteReader();

                while (dolRatReader.Read())
                {
                    dollarValue = dolRatReader.GetDouble(0);
                }
                dolRatReader.Close();
            }

        }
        catch (Exception e)
        {
            dollarValue = 0;
            // Log your error         
            log logger = new log();
            logger.write_log("Failed at getDollarRate_Opn: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }

        return dollarValue;
    }

}


