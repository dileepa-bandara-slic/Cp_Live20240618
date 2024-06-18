using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

/// <summary>
/// Summary description for TRV_PolMast
/// </summary>
public class TRV_PolMast
{
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
    public string plan { get; private set; }
    public double sumAssured { get; private set; }
    public double annualPremium { get; private set; }
    public double adminFee { get; private set; }
    public double polFee { get; private set; }
    public double NBT { get; private set; }
    public double VAT { get; private set; }
    public string comenmentDate { get; private set; }
    public string entryDate { get; private set; }
    public string title { get; private set; }
    public string product_Name { get; private set; }
    public string endDate { get; private set; }
    public string username { get; set; }
    public string status { get; set; }
    public string productID { get; set; }
    public int PG_RET_CODE { get; set; }
    public int PG_RSN_CODE { get; set; }
    public string damageReason { get; set; }
    public string profession { get; set; }
    public string policy_no { get; set; }
    public string nature_of_occupation { get; set; }
    public string employer_name { get; set; }
    public string passport { get; set; }
    public string email { get; set; }
    public string nic { get; set; }
    public string locAddress1 { get; set; }
    public string locAddress2 { get; set; }
    public string locAddress3 { get; set; }
    public string locAddress4 { get; set; }
    public string assignee { get; set; }
    public string damagedBefore { get; set; }
    public string rejectedBefore { get; set; }
    public string rejectReason { get; set; }
    public string emesg { get; set; }
    public double finalprmrs { get; set; }
    public double netprmrs { get; set; }

    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
	public TRV_PolMast()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public TRV_PolMast(string proposal_ID)
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
                    username = reader[30].ToString().Trim();
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

    public bool update_paid_proposal(string propNo, string status, string retCode, string resCode,string polino,string uname)
    {
        bool returnValue = false;
        string getPropDetails = "";

        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        OracleCommand cmd = oconn.CreateCommand();
        OracleTransaction trans = oconn.BeginTransaction();
        cmd.Transaction = trans;
        string insSql = "";

        try
        {
            using (cmd)
            {

                if (status == "A")
                {
                   

                    #region Update Prop Details
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

                    #endregion

                    #region Get Pol Mast
                    

                    string refNo = "";
                    string polno = "";
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
                    int agtcode = 0;

                    if (propNo.Contains("TPI"))
                    {
                        getPropDetails = "SELECT A.polno, to_char(a.depart_date, 'yyyy/mm/dd'),a.title,a.full_name,a.address1,a.ADDRESS2," +
                                         " a.ADDRESS3, a.ADDRESS4,a.pol_type,a.sum_ins_usd,a.NET_PREMIUM_RS," +
                                         " a.vat_rs,a.admin_fee_rs,a.policy_fee_rs,to_char(a.DEPART_DATE, 'yyyy/mm/dd')," +
                                         " to_char(a.RETURN_DATE, 'yyyy/mm/dd'),a.nbt_rs,a.AGENT_CODE  FROM SLIGEN.TRV_POL_MAST A " +
                                         " where a.ref_no = :refNo";
                    }

                    cmd.CommandText = getPropDetails;
                    cmd.Parameters.AddWithValue("refNo", propNo);

                    OracleDataReader propDetReader = cmd.ExecuteReader();

                    while (propDetReader.Read())
                    {
                        if (!propDetReader.IsDBNull(0))
                        {
                            polno = propDetReader.GetString(0);
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
                            adrs1 = propDetReader.GetString(4);
                        }
                        if (!propDetReader.IsDBNull(5))
                        {
                            adrs2 = propDetReader.GetString(5);
                        }
                        if (!propDetReader.IsDBNull(6))
                        {
                            adrs3 = propDetReader.GetString(6);
                        }
                        if (!propDetReader.IsDBNull(7))
                        {
                            adrs4 = propDetReader.GetString(7);
                        }
                        if (!propDetReader.IsDBNull(8))
                        {
                            polType = propDetReader.GetString(8);
                        }
                        if (!propDetReader.IsDBNull(9))
                        {
                            sumIns = propDetReader.GetDouble(9);
                        }
                        if (!propDetReader.IsDBNull(10))
                        {
                            basicPremium = propDetReader.GetDouble(10);
                        }
                        if (!propDetReader.IsDBNull(11))
                        {
                            vat = propDetReader.GetDouble(11);
                        }
                        if (!propDetReader.IsDBNull(12))
                        {
                            admnFee = propDetReader.GetDouble(12);
                        }
                        if (!propDetReader.IsDBNull(13))
                        {
                            polFee = propDetReader.GetDouble(13);
                        }
                        if (!propDetReader.IsDBNull(14))
                        {
                            startDate = propDetReader.GetString(14);
                        }
                        if (!propDetReader.IsDBNull(15))
                        {
                            endDate = propDetReader.GetString(15);
                        }
                        if (!propDetReader.IsDBNull(16))
                        {
                            nbt = propDetReader.GetDouble(16);
                        }
                        if (!propDetReader.IsDBNull(17))
                        {
                            agtcode = propDetReader.GetInt32(17);
                        }
                    }

                    propDetReader.Close();
                    cmd.Parameters.Clear();
                    #endregion

                    #region Update MOMAS
                    string insertToMomas = "";
                    if (agtcode > 0)
                    {
                        insertToMomas = "Insert into GENPAY.MOMAS(FMPOL, FMDCO, FMSTA, FMNAM, FMAD1, FMAD2, FMAD3, FMAD4," +
                                               " FMPTP, FMSUM, FMPRM, FMVAT, FMCES, FMPOF, FMDST, FMDEX, FMBRN, FMCUR, FMNBL, FMDEPT, FMTYP,FMAGT,FMRAT)" +
                                               " VALUES(:polno, :comDate, :title, :name1, :adrs1, :adrs2, :adrs3, :adrs4, :polType, " +
                                               " :sumIns, :basicPrem, :vat, :admnFee, :polFee, :startDate, :endDate, :brn, :currType, :nbt, 'G', '1', :agtcd,'X')";
                    }
                    else
                    {
                        insertToMomas = "Insert into GENPAY.MOMAS(FMPOL, FMDCO, FMSTA, FMNAM, FMAD1, FMAD2, FMAD3, FMAD4," +
                                               " FMPTP, FMSUM, FMPRM, FMVAT, FMCES, FMPOF, FMDST, FMDEX, FMBRN, FMCUR, FMNBL, FMDEPT, FMTYP,FMAGT)" +
                                               " VALUES(:polno, :comDate, :title, :name1, :adrs1, :adrs2, :adrs3, :adrs4, :polType, " +
                                               " :sumIns, :basicPrem, :vat, :admnFee, :polFee, :startDate, :endDate, :brn, :currType, :nbt, 'G', '1', :agtcd)";
                    }
                    cmd.CommandText = insertToMomas;

                    OracleParameter oRef = new OracleParameter();
                    oRef.DbType = DbType.String;
                    oRef.Value = polno;
                    oRef.ParameterName = "polno";

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
                    /*
                    OracleParameter oName2 = new OracleParameter();
                    oName2.DbType = DbType.String;
                    oName2.Value = name2;
                    oName2.ParameterName = "name2";*/

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
                    oBranch.Value = 402;
                    oBranch.ParameterName = "brn";

                    OracleParameter oCurrType = new OracleParameter();
                    oCurrType.DbType = DbType.String;
                    oCurrType.Value = currency;
                    oCurrType.ParameterName = "currType";

                    OracleParameter oNbt = new OracleParameter();
                    oNbt.DbType = DbType.Double;
                    oNbt.Value = nbt;
                    oNbt.ParameterName = "nbt";

                    OracleParameter oagtcd = new OracleParameter();
                    oagtcd.DbType = DbType.Int32;
                    oagtcd.Value = agtcode;
                    oagtcd.ParameterName = "agtcd";

                    cmd.Parameters.Add(oRef);
                    cmd.Parameters.Add(oComDt);
                    cmd.Parameters.Add(oTitle);
                    cmd.Parameters.Add(oName1);
                    /* cmd.Parameters.Add(oName2);*/
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
                    cmd.Parameters.Add(oagtcd);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    #endregion


                    string sch_policy_no = polno;
                    string Entryuser = username;
                    int printtag = 1;
                    string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    int SCH_BRANCH = 999;
                     

                      insSql = "UPDATE SLIGEN.TRV_POL_MAST SET SCH_PRINT= :printtag , SCH_PRINTBY= :entryuser" +
                                    ",SCH_PDATE=sysdate,SCH_PIP= :printip, SCH_BRANCH= 999, SER_BRCD=402 where POLNO= :polno and  SCH_PRINT=0 ";


                    cmd.CommandText = insSql;

                    OracleParameter oprinttag = new OracleParameter();
                    oprinttag.DbType = DbType.Int32;
                    oprinttag.Value = printtag;
                    oprinttag.ParameterName = "printtag";

                    OracleParameter ouser = new OracleParameter();
                    ouser.DbType = DbType.String;
                    ouser.Value = Entryuser;
                    ouser.ParameterName = "entryuser";

                    OracleParameter oip = new OracleParameter();
                    oip.DbType = DbType.String;
                    oip.Value = ip;
                    oip.ParameterName = "printip";

                    OracleParameter opolno = new OracleParameter();
                    opolno.DbType = DbType.String;
                    opolno.Value = sch_policy_no;
                    opolno.ParameterName = "polno";

                    cmd.Parameters.Add(oprinttag);
                    cmd.Parameters.Add(ouser);
                    cmd.Parameters.Add(oip);
                    cmd.Parameters.Add(opolno);

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
            emesg = e.Message +"-"+ insSql;
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

    public string getFullAddress()
    {
        string address = "";

        address = Address1;
        if (Address2 != "")
        {
            address = address + "<br/>" + Address1;
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

    public string getCoverPeriod()
    {
        string coverPeriod = "";

        string startDate = comenmentDate;
        string endDate = this.endDate;//(DateTime.ParseExact(startDate, "yyyy/MM/dd", CultureInfo.InvariantCulture).AddYears(1)).ToString("yyyy/MM/dd");

        coverPeriod = "From " + startDate + " To " + endDate;
        return coverPeriod;
    }

    public bool send_tpi_pay_receipt_email()
    {
        bool retValue = false;
        try
        {
            string sumInsLabel = "";

            if (productID == "TPI" || productID == "TPM")
            {
                sumInsLabel = "Sum Assured (USD):";
            }
             

            string subject = "SLIC Payment Confirmation";
             
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

            if (refNo.Contains("GTP"))
            {
                string jjkj = "<tr><td colspan=\"2\">Your payment receipt will be posted to the registered address in due course.</td></tr><tr><td colspan=\"2\">If you are required with a physical policy document, please <a href='Http://www.srilankainsurance.net/ContactUs.aspx'><span style='font-weight:bold; color:#8C8C8C;'>contact us.</span></a></td></tr>";
                content1 = content1 + jjkj;
            }
            

            content1 = content1 + "<tr>" +
                "<td colspan=\"2\">" +
                    "The Policy is valid only if the bank transfer is successful.</td>" +
            "</tr>" +
            "<tr>&nbsp;</tr>" +
            "<tr>" +
                "<td colspan=\"2\">" +
                    "Thanking you,<br> Sri Lanka Insurance Corporation General (Ltd).</td>" +
            "</tr>" +
            "<tr>" +
                "<td colspan=\"2\">" +
                    "&nbsp;</td>" +
            "</tr>" +
        "</tbody></table>";
        
            string content2 = content1;

            Db_Email emailSender = new Db_Email();
            retValue = emailSender.send_html_email(email, subject, content1, content2);
            LogMail logger = new LogMail();
            logger.write_log("To: " + email + " Subject: " + subject);

            // retValue = true;
        }
        catch (Exception e)
        {
            retValue = false;
            log logger = new log();
            logger.write_log("Failed at Proposal - send_pay_receipt_email " + e.ToString());
        }
        return retValue;
    }

    public TRV_PolMast(string Polno, string refno)
    {
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string sql = "SELECT pol_type,ref_no,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4 ,MOBILE_NUMBER,PLAN,SUM_INS_USD,FINAL_PREMIUM_RS, "+
                         " NET_PREMIUM_RS,NBT_RS,VAT_RS,ADMIN_FEE_RS,POLICY_FEE_RS from SLIGEN.TRV_POL_MAST WHERE POLNO= :polno ";
            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {
                OracleParameter opPolno = new OracleParameter();
                opPolno.Value = Polno;
                opPolno.ParameterName = "polno";

                cmd.Parameters.Add(opPolno);

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
                    plan = reader[8].ToString().Trim();

                    if (!reader.IsDBNull(9))
                    {
                        sumAssured = Convert.ToDouble(reader[9].ToString().Trim());
                    }
                    else
                    {
                        sumAssured = 0;
                    }

                    if (!reader.IsDBNull(10))
                    {
                        finalprmrs = Convert.ToDouble(reader[10].ToString().Trim());
                    }
                    else
                    {
                        finalprmrs = 0;
                    }
                    if (!reader.IsDBNull(11))
                    {
                        netprmrs = Convert.ToDouble(reader[11].ToString().Trim());
                    }
                    else
                    {
                        netprmrs = 0;
                    }
                    if (!reader.IsDBNull(12))
                    {
                        NBT = Convert.ToDouble(reader[12].ToString().Trim());
                    }
                    else
                    {
                        NBT = 0;
                    }

                    if (!reader.IsDBNull(13))
                    {
                        VAT = Convert.ToDouble(reader[13].ToString().Trim());
                    }
                    else
                    {
                        VAT = 0;
                    }

                    if (!reader.IsDBNull(14))
                    {
                        adminFee = Convert.ToDouble(reader[14].ToString().Trim());
                    }
                    else
                    {
                        adminFee = 0;
                    }

                    if (!reader.IsDBNull(15))
                    {
                        polFee = Convert.ToDouble(reader[15].ToString().Trim());
                    }
                    else
                    {
                        polFee = 0;
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
}