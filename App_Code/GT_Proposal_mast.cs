using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using System.Globalization;

/// <summary>
/// Summary description for GT_Quotation_mast
/// </summary>
public class GT_Proposal_mast
{
    public string refID { get; private set; }
    public string title { get; private set; }
    public string fullName { get; private set; }
    public string add_1 { get; private set; }
    public string add_2 { get; private set; }
    public string add_3 { get; private set; }
    public string add_4 { get; private set; }
    public string home_tp { get; private set; }
    public string mobile_tp { get; private set; }
    public string office_tp { get; private set; }
    public int no_persons { get; private set; }
    public double groupDiscRate { get; private set; }
    public double groupDiscAmnt { get; private set; }
    public string Enrty_Date { get; private set; }
    public List<GT_Proposal_mem> members = new List<GT_Proposal_mem>();// {get; private set;}
    public string plan { get; private set; }
    public string destination { get; private set; }
    public string departDate { get; private set; }
    public string returnDate { get; private set; }
    public string visitCntry1 { get; private set; }
    public string visitCntry2 { get; private set; }
    public string visitCntry3 { get; private set; }
    public string visitCntry4 { get; private set; }
    public string travlePurpose { get; private set; }
    public string cntactName { get; private set; }
    public string cntactAdrs1 { get; private set; }
    public string cntactAdrs2 { get; private set; }
    public string cntactAdrs3 { get; private set; }
    public string cntactAdrs4 { get; private set; }
    public string cntact_tp1 { get; private set; }
    public string cntact_tp2 { get; private set; }
    public string coverType { get; private set; }
    public double netPremium_usd { get; private set; }
    public double netPremium_rs { get; private set; }
    public double adminFee_rs { get; private set; }
    public double policyFee_rs { get; private set; }
    public double nbt_rs { get; private set; }
    public double vat_rs { get; private set; }
    public double finalPremium_rs { get; private set; }
    public double taxExpenses_rs { get; private set; }
    public double sumIns_usd { get; private set; } 
    
    DataManager dm = new DataManager();

	public GT_Proposal_mast()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public GT_Proposal_mast(string refNo)
    {
        string sql = "Select REFNO, DESTINATION, to_char(DEPART_DATE , 'yyyy/mm/dd') AS DEPART_DATE, to_char(RETURN_DATE , 'yyyy/mm/dd') AS RETURN_DATE, " +
                     " VISIT_CTRY1, VISIT_CTRY2, VISIT_CTRY3, VISIT_CTRY4, TRAVEL_PURPOSE, CONTCT_NAME, CONTCT_ADRS1, CONTCT_ADRS2, CONTCT_ADRS3, CONTCT_ADRS4, " +
                     " CONTCT_NO1, CONTCT_NO2, GT_TYPE, FULL_NAME, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, MOBILE_NUMBER, HOME_NUMBER, OFFICE_NUMBER, PLAN, " +
                     " TITLE, NUM_OF_PERSONS, GROUP_DISC_RATE, NET_PREMIUM_USD, ADMIN_FEE_RS, POLICY_FEE_RS, NBT_RS, VAT_RS, FINAL_PREMIUM_RS, TAXES_EXPENSES_RS, " +
                     " SUM_INS_USD, NET_PREMIUM_RS, to_char(ENTERED_DATE , 'yyyy/mm/dd') AS ENTERED_DATE" +
                     " FROM SLIC_NET.GT_PROP_MAST" +
                     " WHERE REFNO = '" + refNo.Trim() + "' ";

        if (dm.existRecored(sql) > 0)
        {
            DataSet ds = new DataSet();
            ds = dm.getrow(sql);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                refID = refNo.Trim();
                destination = row[1].ToString().Trim();
                departDate = row[2].ToString().Trim();
                returnDate = row[3].ToString().Trim();
                visitCntry1 = row[4].ToString().Trim();
                visitCntry2 = row[5].ToString().Trim();
                visitCntry3 = row[6].ToString().Trim();
                visitCntry4 = row[7].ToString().Trim();
                travlePurpose = row[8].ToString().Trim();
                cntactName = row[9].ToString().Trim();
                cntactAdrs1 = row[10].ToString().Trim();
                cntactAdrs2 = row[11].ToString().Trim();
                cntactAdrs3 = row[12].ToString().Trim();
                cntactAdrs4 = row[13].ToString().Trim();
                cntact_tp1 = row[14].ToString().Trim();
                cntact_tp2 = row[15].ToString().Trim();
                coverType = row[16].ToString().Trim();
                fullName = row[17].ToString().Trim();
                add_1 = row[18].ToString().Trim();
                add_2 = row[19].ToString().Trim();
                add_3 = row[20].ToString().Trim();
                add_4 = row[21].ToString().Trim();
                mobile_tp = row[22].ToString().Trim();
                home_tp = row[23].ToString().Trim();
                office_tp = row[24].ToString().Trim();
                plan = row[25].ToString().Trim();
                title = row[26].ToString().Trim();                
                no_persons = Convert.ToInt32(row[27].ToString().Trim());
                groupDiscRate = Convert.ToDouble(row[28].ToString().Trim());
                netPremium_usd = Convert.ToDouble(row[29].ToString().Trim());
                adminFee_rs = Convert.ToDouble(row[30].ToString().Trim());
                policyFee_rs = Convert.ToDouble(row[31].ToString().Trim());
                nbt_rs = Convert.ToDouble(row[32].ToString().Trim());
                vat_rs = Convert.ToDouble(row[33].ToString().Trim());
                finalPremium_rs = Convert.ToDouble(row[34].ToString().Trim());
                taxExpenses_rs = Convert.ToDouble(row[35].ToString().Trim());
                sumIns_usd = Convert.ToDouble(row[36].ToString().Trim());  
                netPremium_rs = Convert.ToDouble(row[37].ToString().Trim());
                Enrty_Date = row[38].ToString().Trim();
            }
            this.get_members(refID);
        }

        dm.connclose();
    }

    private void get_members(string ref_id)
    {
        GT_Proposal_mem mem;
        string sql = "select MEM_ID from SLIC_NET.GT_MEM_DETAILS  WHERE REF_NO = '" + ref_id.Trim() + "'  ";
        if (dm.existRecored(sql) > 0)
        {
            DataSet ds = new DataSet();
            ds = dm.getrow(sql);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                string member_id = row[0].ToString().Trim();
                mem = new GT_Proposal_mem(member_id);
                members.Add(mem);
            }
        }
        dm.connclose();
    }

    public bool Insert_rec(string p_title, string p_fullName, string p_add_1, string p_add_2, string p_add_3, string p_add_4, string p_home_tp,
                           string p_mobile_tp, string p_office_tp, int p_no_persons, double p_groupDiscRate, string p_plan,
                           string p_destination, string p_departDate, string p_returnDate, string p_visitCntry1, string p_visitCntry2, string p_visitCntry3,
                           string p_visitCntry4, string p_travlePurpose, string p_cntactName, string p_cntactAdrs1, string p_cntactAdrs2, string p_cntactAdrs3,
                           string p_cntactAdrs4, string p_cntact_tp1, string p_cntact_tp2, string p_coverType, double p_netPremium_usd, double p_netPremium_rs,
                           double p_adminFee_rs, double p_policyFee_rs, double p_nbt_rs, double p_vat_rs, double p_finalPremium_rs, double p_taxExpenses_rs,
                           double p_numOfDays, DataTable dtMemTable, double p_dollarValue, out string refNum)
    {
        bool result = false;
        refID = "";
        refNum = "";

        try
        {
            Proposal prop = new Proposal();
            refID = prop.generate_proposalID("G", Convert.ToInt32(DateTime.Today.ToString("yyyy")), "GTI");

            if (!String.IsNullOrEmpty(refID))
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]))
                {
                    connection.Open();

                    OracleCommand command = connection.CreateCommand();
                    OracleTransaction transaction;

                    transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    command.Transaction = transaction;
                    try
                    {

                        string sql = "INSERT INTO SLIC_NET.GT_PROP_MAST (REFNO, DESTINATION, DEPART_DATE, RETURN_DATE, VISIT_CTRY1, VISIT_CTRY2, VISIT_CTRY3, VISIT_CTRY4, TRAVEL_PURPOSE, CONTCT_NAME, CONTCT_ADRS1, CONTCT_ADRS2, CONTCT_ADRS3, CONTCT_ADRS4," +
                                     " CONTCT_NO1, CONTCT_NO2, GT_TYPE, FULL_NAME, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, MOBILE_NUMBER, HOME_NUMBER, OFFICE_NUMBER, PLAN, TITLE, NUM_OF_PERSONS, GROUP_DISC_RATE, NET_PREMIUM_USD, ADMIN_FEE_RS, POLICY_FEE_RS," +
                                     " NBT_RS, VAT_RS, FINAL_PREMIUM_RS, TAXES_EXPENSES_RS, ENTERED_DATE, NET_PREMIUM_RS, NO_OF_DAYS, USD_RATE) " +
                                      "Values (:refNo, :dest, :deptDt, :retnDt, :visCty1, :visCty2, :visCty3, :visCty4, :travlPrps, :cntName, :cntAdrs1, :cntAdrs2, :cntAdrs3,  :cntAdrs4,  :cntNo1, :cntNo2, :gtType, :fullName, :adrs1, :adrs2, :adrs3, :adrs4," +
                                      " :mobNo, :homNo, :ofcNo , :plan, :title, :numPersns, :discRt, :netPremUsd, :admnFee, :polFee, :nbt, :vat, :finalPrem, :taxExp, sysdate, :netPremRs, :numDays, :dollarVal)";                        

                        command.CommandText = sql;

                        OracleParameter oRefNo = new OracleParameter();
                        oRefNo.Value = refID;
                        oRefNo.ParameterName = "refNo";

                        OracleParameter oDest = new OracleParameter();
                        oDest.Value = p_destination;
                        oDest.ParameterName = "dest";

                        OracleParameter oDeptDate = new OracleParameter();
                        oDeptDate.DbType = DbType.DateTime;
                        oDeptDate.Value = p_departDate;
                        oDeptDate.ParameterName = "deptDt";

                        OracleParameter oRetnDate = new OracleParameter();
                        oRetnDate.DbType = DbType.DateTime;
                        oRetnDate.Value = p_returnDate;
                        oRetnDate.ParameterName = "retnDt";

                        OracleParameter oVisCnty1 = new OracleParameter();
                        oVisCnty1.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_visitCntry1))
                        {
                            oVisCnty1.Value = DBNull.Value;
                        }
                        else
                        {
                            oVisCnty1.Value = p_visitCntry1;
                        }
                        oVisCnty1.ParameterName = "visCty1";

                        OracleParameter oVisCnty2 = new OracleParameter();
                        oVisCnty2.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_visitCntry2))
                        {
                            oVisCnty2.Value = DBNull.Value;
                        }
                        else
                        {
                            oVisCnty2.Value = p_visitCntry2;
                        }
                        oVisCnty2.ParameterName = "visCty2";

                        OracleParameter oVisCnty3 = new OracleParameter();
                        oVisCnty3.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_visitCntry3))
                        {
                            oVisCnty3.Value = DBNull.Value;
                        }
                        else
                        {
                            oVisCnty3.Value = p_visitCntry3;
                        }
                        oVisCnty3.ParameterName = "visCty3";

                        OracleParameter oVisCnty4 = new OracleParameter();
                        oVisCnty4.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_visitCntry4))
                        {
                            oVisCnty4.Value = DBNull.Value;
                        }
                        else
                        {
                            oVisCnty4.Value = p_visitCntry4;
                        }
                        oVisCnty4.ParameterName = "visCty4";

                        OracleParameter oTravlPrps = new OracleParameter();
                        oTravlPrps.Value = p_travlePurpose;
                        oTravlPrps.ParameterName = "travlPrps";

                        OracleParameter oCntName = new OracleParameter();
                        oCntName.Value = p_cntactName;
                        oCntName.ParameterName = "cntName";

                        OracleParameter oCntAdrs1 = new OracleParameter();
                        oCntAdrs1.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_cntactAdrs1))
                        {
                            oCntAdrs1.Value = DBNull.Value;
                        }
                        else
                        {
                            oCntAdrs1.Value = p_cntactAdrs1;
                        }
                        oCntAdrs1.ParameterName = "cntAdrs1";

                        OracleParameter oCntAdrs2 = new OracleParameter();
                        oCntAdrs2.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_cntactAdrs2))
                        {
                            oCntAdrs2.Value = DBNull.Value;
                        }
                        else
                        {
                            oCntAdrs2.Value = p_cntactAdrs2;
                        }
                        oCntAdrs2.ParameterName = "cntAdrs2";

                        OracleParameter oCntAdrs3 = new OracleParameter();
                        oCntAdrs3.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_cntactAdrs3))
                        {
                            oCntAdrs3.Value = DBNull.Value;
                        }
                        else
                        {
                            oCntAdrs3.Value = p_cntactAdrs3;
                        }
                        oCntAdrs3.ParameterName = "cntAdrs3";

                        OracleParameter oCntAdrs4 = new OracleParameter();
                        oCntAdrs4.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_cntactAdrs4))
                        {
                            oCntAdrs4.Value = DBNull.Value;
                        }
                        else
                        {
                            oCntAdrs4.Value = p_cntactAdrs4;
                        }
                        oCntAdrs4.ParameterName = "cntAdrs4";

                        OracleParameter oCntNo1 = new OracleParameter();
                        oCntNo1.Value = p_cntact_tp1;
                        oCntNo1.ParameterName = "cntNo1";

                        OracleParameter oCntNo2 = new OracleParameter();
                        oCntNo2.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_cntact_tp2))
                        {
                            oCntNo2.Value = DBNull.Value;
                        }
                        else
                        {
                            oCntNo2.Value = p_cntact_tp2;
                        }
                        oCntNo2.ParameterName = "cntNo2";

                        OracleParameter oGtType = new OracleParameter();
                        oGtType.Value = p_coverType;
                        oGtType.ParameterName = "gtType";

                        OracleParameter oFullName = new OracleParameter();
                        oFullName.Value = p_fullName;
                        oFullName.ParameterName = "fullName";

                        OracleParameter oAdrs1 = new OracleParameter();
                        oAdrs1.Value = p_add_1;
                        oAdrs1.ParameterName = "adrs1";

                        OracleParameter oAdrs2 = new OracleParameter();
                        oAdrs2.Value = p_add_2;
                        oAdrs2.ParameterName = "adrs2";

                        OracleParameter oAdrs3 = new OracleParameter();
                        oAdrs3.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_add_3))
                        {
                            oAdrs3.Value = DBNull.Value;
                        }
                        else
                        {
                            oAdrs3.Value = p_add_3;
                        }
                        oAdrs3.ParameterName = "adrs3";

                        OracleParameter oAdrs4 = new OracleParameter();
                        oAdrs4.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_add_4))
                        {
                            oAdrs4.Value = DBNull.Value;
                        }
                        else
                        {
                            oAdrs4.Value = p_add_4;
                        }
                        oAdrs4.ParameterName = "adrs4";

                        OracleParameter oMobNo = new OracleParameter();
                        oMobNo.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_mobile_tp))
                        {
                            oMobNo.Value = DBNull.Value;
                        }
                        else
                        {
                            oMobNo.Value = p_mobile_tp;
                        }
                        oMobNo.ParameterName = "mobNo";

                        OracleParameter oHomNo = new OracleParameter();
                        oHomNo.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_home_tp))
                        {
                            oHomNo.Value = DBNull.Value;
                        }
                        else
                        {
                            oHomNo.Value = p_home_tp;
                        }
                        oHomNo.ParameterName = "homNo";

                        OracleParameter oOfcNo = new OracleParameter();
                        oOfcNo.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_office_tp))
                        {
                            oOfcNo.Value = DBNull.Value;
                        }
                        else
                        {
                            oOfcNo.Value = p_office_tp;
                        }
                        oOfcNo.ParameterName = "ofcNo";

                        OracleParameter oPlan = new OracleParameter();
                        oPlan.Value = p_plan;
                        oPlan.ParameterName = "plan";

                        OracleParameter oTitle = new OracleParameter();
                        oTitle.Value = p_title;
                        oTitle.ParameterName = "title";

                        OracleParameter oNumPrsns = new OracleParameter();
                        oNumPrsns.DbType = DbType.Double;
                        oNumPrsns.Value = p_no_persons;
                        oNumPrsns.ParameterName = "numPersns";

                        OracleParameter oDiscRate = new OracleParameter();
                        oDiscRate.DbType = DbType.Double;
                        oDiscRate.Value = p_groupDiscRate;
                        oDiscRate.ParameterName = "discRt";

                        OracleParameter oNetPremUsd = new OracleParameter();
                        oNetPremUsd.DbType = DbType.Double;
                        oNetPremUsd.Value = p_netPremium_usd;
                        oNetPremUsd.ParameterName = "netPremUsd";

                        OracleParameter oAdmnFee = new OracleParameter();
                        oAdmnFee.DbType = DbType.Double;
                        oAdmnFee.Value = p_adminFee_rs;
                        oAdmnFee.ParameterName = "admnFee";

                        OracleParameter oPolFee = new OracleParameter();
                        oPolFee.DbType = DbType.Double;
                        oPolFee.Value = p_policyFee_rs;
                        oPolFee.ParameterName = "polFee";

                        OracleParameter oNbt = new OracleParameter();
                        oNbt.DbType = DbType.Double;
                        oNbt.Value = p_nbt_rs;
                        oNbt.ParameterName = "nbt";

                        OracleParameter oVat = new OracleParameter();
                        oVat.DbType = DbType.Double;
                        oVat.Value = p_vat_rs;
                        oVat.ParameterName = "vat";

                        OracleParameter oFinalPrem = new OracleParameter();
                        oFinalPrem.DbType = DbType.Double;
                        oFinalPrem.Value = p_finalPremium_rs;
                        oFinalPrem.ParameterName = "finalPrem";

                        OracleParameter oTaxExp = new OracleParameter();
                        oTaxExp.DbType = DbType.Double;
                        oTaxExp.Value = p_taxExpenses_rs;
                        oTaxExp.ParameterName = "taxExp";

                        OracleParameter oNetPremRs = new OracleParameter();
                        oNetPremRs.DbType = DbType.Double;
                        oNetPremRs.Value = p_netPremium_rs;
                        oNetPremRs.ParameterName = "netPremRs";

                        OracleParameter oNumDays = new OracleParameter();
                        oNumDays.DbType = DbType.Double;
                        oNumDays.Value = p_numOfDays;
                        oNumDays.ParameterName = "numDays";

                        OracleParameter oDollarVal = new OracleParameter();
                        oDollarVal.DbType = DbType.Double;
                        oDollarVal.Value = p_dollarValue;
                        oDollarVal.ParameterName = "dollarVal";

                        command.Parameters.Add(oRefNo);
                        command.Parameters.Add(oDest);
                        command.Parameters.Add(oDeptDate);
                        command.Parameters.Add(oRetnDate);
                        command.Parameters.Add(oVisCnty1);
                        command.Parameters.Add(oVisCnty2);
                        command.Parameters.Add(oVisCnty3);
                        command.Parameters.Add(oVisCnty4);
                        command.Parameters.Add(oTravlPrps);
                        command.Parameters.Add(oCntName);
                        command.Parameters.Add(oCntAdrs1);
                        command.Parameters.Add(oCntAdrs2);
                        command.Parameters.Add(oCntAdrs3);
                        command.Parameters.Add(oCntAdrs4);
                        command.Parameters.Add(oCntNo1);
                        command.Parameters.Add(oCntNo2);
                        command.Parameters.Add(oGtType);
                        command.Parameters.Add(oFullName);
                        command.Parameters.Add(oAdrs1);
                        command.Parameters.Add(oAdrs2);
                        command.Parameters.Add(oAdrs3);
                        command.Parameters.Add(oAdrs4);
                        command.Parameters.Add(oMobNo);
                        command.Parameters.Add(oHomNo);
                        command.Parameters.Add(oOfcNo);
                        command.Parameters.Add(oPlan);
                        command.Parameters.Add(oTitle);
                        command.Parameters.Add(oNumPrsns);
                        command.Parameters.Add(oDiscRate);
                        command.Parameters.Add(oNetPremUsd);
                        command.Parameters.Add(oAdmnFee);
                        command.Parameters.Add(oPolFee);
                        command.Parameters.Add(oNbt);
                        command.Parameters.Add(oVat);
                        command.Parameters.Add(oFinalPrem);
                        command.Parameters.Add(oTaxExp);
                        command.Parameters.Add(oNetPremRs);
                        command.Parameters.Add(oNumDays);
                        command.Parameters.Add(oDollarVal);

                        command.ExecuteNonQuery();

                        command.Parameters.Clear();

                        int i = 0;
                        foreach (DataRow row in dtMemTable.Rows)
                        {
                            i++;
                            if (i <= p_no_persons)
                            {
                                string category = row["Category"].ToString().Substring(0, 1);
                                string gender = row["Gender"].ToString().Substring(0, 1);
                                string birthDate = row["Dob"].ToString();
                                double age = double.Parse(row["Age"].ToString());
                                double baseRate = double.Parse(row["BasePremium"].ToString());


                                sql = "INSERT INTO SLIC_NET.GT_MEM_DETAILS (REF_NO, MEM_ID, MEM_TYPE, GENDER, DOB, AGE, BASE_AMOUNT_USD, ENTERED_DATE) " +
                                  "Values (:refid, :memid, :mtype, :gen, :dob, :age, :base, sysdate)";

                                command.CommandText = sql;

                                OracleParameter oRefid = new OracleParameter();
                                oRefid.Value = refID;
                                oRefid.ParameterName = "refid";

                                OracleParameter omemid = new OracleParameter();
                                omemid.Value = refID + "_" + i.ToString();
                                omemid.ParameterName = "memid";

                                OracleParameter omtype = new OracleParameter();
                                omtype.Value = category;
                                omtype.ParameterName = "mtype";

                                OracleParameter ogen = new OracleParameter();
                                ogen.Value = gender;
                                ogen.ParameterName = "gen";

                                OracleParameter odob = new OracleParameter();
                                odob.DbType = DbType.Date;
                                odob.Value = birthDate;
                                odob.ParameterName = "dob";

                                OracleParameter oage = new OracleParameter();
                                oage.DbType = DbType.Double;
                                oage.Value = age;
                                oage.ParameterName = "age";

                                OracleParameter obase = new OracleParameter();
                                obase.DbType = DbType.Double;
                                obase.Value = baseRate;
                                obase.ParameterName = "base";

                                command.Parameters.Add(oRefid);
                                command.Parameters.Add(omemid);
                                command.Parameters.Add(omtype);
                                command.Parameters.Add(ogen);
                                command.Parameters.Add(odob);
                                command.Parameters.Add(oage);
                                command.Parameters.Add(obase);                                

                                command.ExecuteNonQuery();

                                command.Parameters.Clear();
                            }
                        }

                        transaction.Commit();
                        result = true;
                        refNum = refID;
                        
                    }
                    catch (Exception u)
                    {
                        transaction.Rollback();
                        log logger = new log();
                        logger.write_log("Failed at gt_proposal_mast->insert_rec: " + u.ToString());
                    }
                }
            }
        }
        catch (Exception u)
        {
            string g = u.ToString();
            log logger = new log();
            logger.write_log("Failed at gt_proposal_mast->insert_rec: " + u.ToString());
        }
        finally
        {

        }

        return result;

    }

    public bool Update_rec(string p_refID, string p_plan, double p_groupDiscRate, double p_netPremium_usd, double p_adminFee_rs, 
                           double p_policyFee_rs, double p_nbt_rs, double p_vat_rs, double p_finalPremium_rs, double p_taxExpenses_rs,
                           double p_netPremium_rs, double p_sumIns, DataTable dtMemTable, double p_dollarValue)
    {
        bool result = false;

        try
        {

            using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]))
            {
                connection.Open();

                OracleCommand command = connection.CreateCommand();
                OracleTransaction transaction;

                transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                command.Transaction = transaction;
                try
                {

                    string sql = "UPDATE SLIC_NET.GT_PROP_MAST" +
                                 " SET PLAN = :plan," +
                                 " GROUP_DISC_RATE = :discRate, " +
                                 " NET_PREMIUM_USD = :netPremUsd," +
                                 " ADMIN_FEE_RS = :admnFee," +
                                 " POLICY_FEE_RS = :polFee," +
                                 " NBT_RS = :nbt," +
                                 " VAT_RS = :vat," +
                                 " FINAL_PREMIUM_RS = :finalPrem," +
                                 " TAXES_EXPENSES_RS = :taxExp," +
                                 " NET_PREMIUM_RS = :netPremRs," +
                                 " ENTERED_DATE = sysdate," +
                                 " SUM_INS_USD = :sumIns," +
                                 " USD_RATE = :dollarVal" +
                                 " where REFNO = :refNo";

                    command.CommandText = sql;

                    OracleParameter oRefno = new OracleParameter();
                    oRefno.Value = p_refID;
                    oRefno.ParameterName = "refNo";

                    OracleParameter oPlan = new OracleParameter();
                    oPlan.Value = p_plan;
                    oPlan.ParameterName = "plan";

                    OracleParameter odscRate = new OracleParameter();
                    odscRate.DbType = DbType.Double;
                    odscRate.Value = p_groupDiscRate;
                    odscRate.ParameterName = "discRate";

                    OracleParameter onetPremUsd = new OracleParameter();
                    onetPremUsd.DbType = DbType.Double;
                    onetPremUsd.Value = p_netPremium_usd;
                    onetPremUsd.ParameterName = "netPremUsd";

                    OracleParameter oadmnFee = new OracleParameter();
                    oadmnFee.DbType = DbType.Double;
                    oadmnFee.Value = p_adminFee_rs;
                    oadmnFee.ParameterName = "admnFee";

                    OracleParameter opolFee = new OracleParameter();
                    opolFee.DbType = DbType.Double;
                    opolFee.Value = p_policyFee_rs;
                    opolFee.ParameterName = "polFee";

                    OracleParameter onbt = new OracleParameter();
                    onbt.DbType = DbType.Double;
                    onbt.Value = p_nbt_rs;
                    onbt.ParameterName = "nbt";

                    OracleParameter ovat = new OracleParameter();
                    ovat.DbType = DbType.Double;
                    ovat.Value = p_vat_rs;
                    ovat.ParameterName = "vat";

                    OracleParameter ofnlPrem = new OracleParameter();
                    ofnlPrem.DbType = DbType.Double;
                    ofnlPrem.Value = p_finalPremium_rs;
                    ofnlPrem.ParameterName = "finalPrem";

                    OracleParameter oTaxExp = new OracleParameter();
                    oTaxExp.DbType = DbType.Double;
                    oTaxExp.Value = p_taxExpenses_rs;
                    oTaxExp.ParameterName = "taxExp";

                    OracleParameter oNetPremRs = new OracleParameter();
                    oNetPremRs.DbType = DbType.Double;
                    oNetPremRs.Value = p_netPremium_rs;
                    oNetPremRs.ParameterName = "netPremRs";

                    OracleParameter oSumInsUsd = new OracleParameter();
                    oSumInsUsd.DbType = DbType.Double;
                    oSumInsUsd.Value = p_sumIns;
                    oSumInsUsd.ParameterName = "sumIns";

                    OracleParameter oDollarValue = new OracleParameter();
                    oDollarValue.DbType = DbType.Double;
                    oDollarValue.Value = p_dollarValue;
                    oDollarValue.ParameterName = "dollarVal";                    

                    command.Parameters.Add(oRefno);
                    command.Parameters.Add(oPlan);
                    command.Parameters.Add(odscRate);
                    command.Parameters.Add(onetPremUsd);
                    command.Parameters.Add(oadmnFee);
                    command.Parameters.Add(opolFee);
                    command.Parameters.Add(onbt);
                    command.Parameters.Add(ovat);
                    command.Parameters.Add(ofnlPrem);                    
                    command.Parameters.Add(oTaxExp);
                    command.Parameters.Add(oNetPremRs);
                    command.Parameters.Add(oSumInsUsd);
                    command.Parameters.Add(oDollarValue); 

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    int i = 0;
                    foreach (DataRow row in dtMemTable.Rows)
                    {
                        i++;
                        string memId = row["MemId"].ToString();
                        string category = row["Category"].ToString().Substring(0, 1);
                        string gender = row["Gender"].ToString().Substring(0, 1);
                        string birthDate = row["Dob"].ToString();
                        double age = double.Parse(row["Age"].ToString());
                        double baseRate = double.Parse(row["BasePremium"].ToString());                        

                        sql = "UPDATE SLIC_NET.GT_MEM_DETAILS" +
                              " SET MEM_TYPE = :mtype," +
                              " GENDER = :gen," +
                              " DOB = :dob," +
                              " AGE = :age," +
                              " BASE_AMOUNT_USD = :base," +                             
                              " ENTERED_DATE = sysdate" +
                              " WHERE REF_NO = :refId" +
                              " AND MEM_ID = :memid";

                        command.CommandText = sql;

                        OracleParameter orefId = new OracleParameter();
                        orefId.Value = p_refID;
                        orefId.ParameterName = "refId";

                        OracleParameter omemid = new OracleParameter();
                        omemid.Value = memId;
                        omemid.ParameterName = "memid";

                        OracleParameter omtype = new OracleParameter();
                        omtype.Value = category;
                        omtype.ParameterName = "mtype";

                        OracleParameter ogen = new OracleParameter();
                        ogen.Value = gender;
                        ogen.ParameterName = "gen";

                        OracleParameter odob = new OracleParameter();
                        odob.DbType = DbType.Date;
                        odob.Value = birthDate;
                        odob.ParameterName = "dob";

                        OracleParameter oage = new OracleParameter();
                        oage.DbType = DbType.Double;
                        oage.Value = age;
                        oage.ParameterName = "age";

                        OracleParameter obase = new OracleParameter();
                        obase.DbType = DbType.Double;
                        obase.Value = baseRate;
                        obase.ParameterName = "base";                        

                        command.Parameters.Add(orefId);
                        command.Parameters.Add(omemid);
                        command.Parameters.Add(omtype);
                        command.Parameters.Add(ogen);
                        command.Parameters.Add(odob);
                        command.Parameters.Add(oage);
                        command.Parameters.Add(obase);
                        
                        command.ExecuteNonQuery();

                        command.Parameters.Clear();

                    }

                    if (i > 0)
                    {
                        transaction.Commit();
                        result = true;
                    }

                }
                catch (Exception u)
                {
                    transaction.Rollback();
                    string g = u.ToString();
                    log logger = new log();
                    logger.write_log("Failed at gt_proposal_mast->update_rec: " + u.ToString());
                }
            }

        }
        catch (Exception u)
        {
            string g = u.ToString();
            log logger = new log();
            logger.write_log("Failed at gt_proposal_mast->update_rec: " + u.ToString());
        }
        finally
        {

        }

        return result;

    }

    public string get_country_name(string cid)
    {
        string result = "";

        string sql = "select COUNTRY_NAME from SLIC_NET.COUNTRIES  WHERE UPPER(TRIM(COUNTRY_ID)) = '" + cid.Trim().ToUpper() + "'  ";
        if (dm.existRecored(sql) > 0)
        {
            DataSet ds = new DataSet();
            ds = dm.getrow(sql);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                result = row[0].ToString().Trim();
            }
        }
        dm.connclose();


        return result;
    }

    public string get_scheme_name(string cid)
    {
        string result = "";

        string sql = "select DESCRIPTION from SLIC_NET.GLOBE_TROT_SCHEMES  WHERE UPPER(TRIM(CODE)) = '" + cid.Trim().ToUpper() + "'  ";
        if (dm.existRecored(sql) > 0)
        {
            DataSet ds = new DataSet();
            ds = dm.getrow(sql);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                result = row[0].ToString().Trim();
            }
        }
        dm.connclose();


        return result;
    }
}