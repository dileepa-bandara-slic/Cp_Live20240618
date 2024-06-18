using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TRV_Prop_mast
/// </summary>
public class TRV_Prop_mast
{
    public string refID { get; private set; }
    public string DESTINATION { get; set; }
    public string DEPART_DATE { get; set; }
    public string RETURN_DATE { get; set; }
    public string VISIT_CTRY1 { get; set; }
    public string VISIT_CTRY2 { get; set; }
    public string VISIT_CTRY3 { get; set; }
    public string VISIT_CTRY4 { get; set; }
    public string TRAVEL_PURPOSE { get; set; }
    public string CONTCT_NAME { get; set; }
    public string CONTCT_ADRS1 { get; set; }
    public string CONTCT_ADRS2 { get; set; }
    public string CONTCT_ADRS3 { get; set; }
    public string CONTCT_ADRS4 { get; set; }
    public string CONTCT_NO1 { get; set; }
    public string CONTCT_NO2 { get; set; }
    public string TRV_TYPE { get; set; }
    public string CNAME { get; set; }
    public string CADDRESS1 { get; set; }
    public string CADDRESS2 { get; set; }
    public string CADDRESS3 { get; set; }
    public string CADDRESS4 { get; set; }
    public string FULL_NAME { get; set; }
    public string ADDRESS1 { get; set; }
    public string ADDRESS2 { get; set; }
    public string ADDRESS3 { get; set; }
    public string ADDRESS4 { get; set; }
    public string MOBILE_NUMBER { get; set; }
    public string HOME_NUMBER { get; set; }
    public string OFFICE_NUMBER { get; set; }
    public string PLAN { get; set; }
    public string TITLE { get; set; }
    public int NUM_OF_PERSONS { get; set; }
    public double GROUP_DISC_RATE { get; set; }
    public double NET_PREMIUM_USD { get; set; }
    public double ADMIN_FEE_RS { get; set; }
    public double POLICY_FEE_RS { get; set; }
    public double NBT_RS { get; set; }
    public double VAT_RS { get; set; }
    public double FINAL_PREMIUM_RS { get; set; }
    public double TAXES_EXPENSES_RS { get; set; }
    public string ENTERED_DATE { get; set; }
    public double NET_PREMIUM_RS { get; set; }
    public int NO_OF_DAYS { get; set; }
    public double SUM_INS_USD { get; set; }
    public double USD_RATE { get; set; }
    public double FINAL_PREMIUM_USD { get; set; }
    public string IP_ADDRESS { get; set; }
    public string ENTERED_BY { get; set; }
    public int AGENT_CODE { get; set; }
    public int DEBTOR_CODE { get; set; }
    public string CURR_TYPE { get; set; }
    public double DiscountRate { get; set; }
    public double DiscountAmt { get; set; }

    public List<TRV_Proposal_mem> members = new List<TRV_Proposal_mem>();
    public ArrayList arrDest = new ArrayList();
    public DataTable dtMem = new DataTable();

    OracleConnection objOraCon = new OracleConnection();
    OracleCommand objOraCom = new OracleCommand();

    public TRV_Prop_mast()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void connectDB()
    {
         
        objOraCon.ConnectionString = ConfigurationManager.AppSettings.Get("OracleDB").ToString();
        if (objOraCon.State != ConnectionState.Open)
            objOraCon.Open();
    }
    public void disconnectDB()
    {
        if (objOraCon.State != ConnectionState.Closed)
        {
            objOraCon.Close();
        }
    }
    public TRV_Prop_mast(string refNo)
    {
        this.connectDB();
        try
        {

            string sql = "Select REFNO, DESTINATION, to_char(DEPART_DATE , 'yyyy/mm/dd') AS DEPART_DATE, to_char(RETURN_DATE , 'yyyy/mm/dd') AS RETURN_DATE, " +
                         " VISIT_CTRY1, VISIT_CTRY2, VISIT_CTRY3, VISIT_CTRY4, TRAVEL_PURPOSE, CONTCT_NAME, CONTCT_ADRS1, CONTCT_ADRS2, CONTCT_ADRS3, CONTCT_ADRS4, " +
                         "  TRV_TYPE, FULL_NAME, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, MOBILE_NUMBER, HOME_NUMBER, OFFICE_NUMBER, PLAN, " +
                         " TITLE, NUM_OF_PERSONS, GROUP_DISC_RATE, NET_PREMIUM_USD, ADMIN_FEE_RS, POLICY_FEE_RS, NBT_RS, VAT_RS, FINAL_PREMIUM_RS, TAXES_EXPENSES_RS, " +
                         " SUM_INS_USD, NET_PREMIUM_RS, to_char(ENTERED_DATE , 'yyyy/mm/dd') AS ENTERED_DATE,USD_RATE,FINAL_PREMIUM_USD," +
                         " IP_ADDRESS,ENTERED_BY,nvl(AGENT_CODE,0),nvl(DEBTOR_CODE,0),CURR_TYPE,DISC_AMNT,DISC_RATE,NO_OF_DAYS " +
                         " FROM SLIGEN.TRV_QUOT_MAST" +
                         " WHERE REFNO = :refNo ";
            using (OracleCommand cmd = new OracleCommand(sql, objOraCon))
            {
                OracleParameter opRefNo = new OracleParameter();
                opRefNo.Value = refNo;
                opRefNo.ParameterName = "refNo";

                cmd.Parameters.Add(opRefNo);

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    refID = refNo.Trim();
                    DESTINATION = reader[1].ToString();
                    DEPART_DATE = reader[2].ToString();
                    RETURN_DATE = reader[3].ToString();
                    VISIT_CTRY1 = reader[4].ToString();
                    VISIT_CTRY2 = reader[5].ToString();
                    VISIT_CTRY3 = reader[6].ToString();
                    VISIT_CTRY4 = reader[7].ToString();
                    TRAVEL_PURPOSE = reader[8].ToString();
                    CNAME = reader[9].ToString();
                    CADDRESS1 = reader[10].ToString();
                    CADDRESS2 = reader[11].ToString();
                    CADDRESS3 = reader[12].ToString();
                    CADDRESS4 = reader[13].ToString();                    
                    TRV_TYPE = reader[14].ToString();
                    FULL_NAME = reader[15].ToString();
                    ADDRESS1 = reader[16].ToString();
                    ADDRESS2 = reader[17].ToString();
                    ADDRESS3 = reader[18].ToString();
                    ADDRESS4 = reader[19].ToString();
                    MOBILE_NUMBER = reader[20].ToString();
                    HOME_NUMBER = reader[21].ToString();
                    OFFICE_NUMBER = reader[22].ToString();
                    PLAN = reader[23].ToString();
                    TITLE = reader[24].ToString();
                    NUM_OF_PERSONS= Convert.ToInt32(reader[25].ToString());
                    GROUP_DISC_RATE = Convert.ToDouble(reader[26].ToString());
                    NET_PREMIUM_USD = Convert.ToDouble(reader[27].ToString());
                    ADMIN_FEE_RS = Convert.ToDouble(reader[28].ToString());
                    POLICY_FEE_RS = Convert.ToDouble(reader[29].ToString());
                    NBT_RS = Convert.ToDouble(reader[30].ToString());
                    VAT_RS = Convert.ToDouble(reader[31].ToString());

                    FINAL_PREMIUM_RS = Convert.ToDouble(reader[32].ToString());
                    string taxEx = reader[33].ToString();
                    if (!String.IsNullOrEmpty(taxEx))
                    {
                        TAXES_EXPENSES_RS = Convert.ToDouble(reader[33].ToString());
                    }
                    else
                    {
                        TAXES_EXPENSES_RS = 0;
                    }

                    SUM_INS_USD = Convert.ToDouble(reader[34].ToString());
                    NET_PREMIUM_RS = Convert.ToDouble(reader[35].ToString());
                    ENTERED_DATE = reader[36].ToString();
                    USD_RATE = Convert.ToDouble(reader[37].ToString());
                    FINAL_PREMIUM_USD = Convert.ToDouble(reader[38].ToString());
                    IP_ADDRESS = reader[39].ToString();
                    ENTERED_BY = reader[40].ToString();
                    AGENT_CODE = Convert.ToInt32(reader[41].ToString());
                    DEBTOR_CODE = Convert.ToInt32(reader[42].ToString());
                    CURR_TYPE = reader[43].ToString();
                    DiscountAmt = Convert.ToDouble(reader[44].ToString());
                    DiscountRate = Convert.ToDouble(reader[45].ToString());
                    NO_OF_DAYS= Convert.ToInt32(reader[46].ToString());
                }
                reader.Close();

                if (DESTINATION.Contains(","))
                {
                    string[] dest = DESTINATION.Split(',');

                    for (int i = 0; i < dest.Length; i++)
                    {
                        arrDest.Add(get_country_name(dest[i]));
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(DESTINATION))
                    {
                        arrDest.Add(get_country_name(DESTINATION));
                    }
                }
            }
            this.get_members(refID);
        }
        catch (Exception exc)
        {

        }
        finally { this.disconnectDB(); }
            
        
    }

    private void get_members(string ref_id)
    {
        this.connectDB();
        string sql = "select MEM_ID from SLIGEN.TRV_QUOT_MEM_DETAILS WHERE REF_NO = '" + ref_id.Trim() + "'  ";
        objOraCom.CommandType = CommandType.Text;
        objOraCom.CommandText = sql;
        objOraCom.Connection = objOraCon;
        OracleDataReader objOraRdr = objOraCom.ExecuteReader();
        while (objOraRdr.Read())
        {
            string member_id = objOraRdr[0].ToString().Trim();
            TRV_Proposal_mem member = new TRV_Proposal_mem(member_id);
                members.Add(member);
            
        }
        this.disconnectDB();
    }

    public string get_country_name(string cid)
    {
        try
        {
            this.connectDB();
        }
        catch { }
        string result = "";

        string sql = "select COUNTRY_NAME from SLIGEN.TRV_COUNTRIES WHERE UPPER(TRIM(COUNTRY_ID)) = '" + cid.Trim().ToUpper() + "'  ";

        objOraCom.CommandType = CommandType.Text;
        objOraCom.CommandText = sql;
        objOraCom.Connection = objOraCon;
        OracleDataReader objOraRdr = objOraCom.ExecuteReader();
        while (objOraRdr.Read())
        {
            if (!objOraRdr.IsDBNull(0))
            {
                result =  objOraRdr[0].ToString();
            }

        }
        objOraRdr.Close();

        this.disconnectDB();


        return result;
    }

    public string get_scheme_name(string cid, string PolType)
    {
        string result = "";
        this.connectDB();
        string sql = "select DESCRIPTION from SLIGEN.TRV_SCHEMES  WHERE UPPER(TRIM(CODE)) = '" + cid.Trim().ToUpper() + "'  and POL_TYPE='" + PolType.Trim() + "' ";
        objOraCom.CommandType = CommandType.Text;
        objOraCom.CommandText = sql;
        objOraCom.Connection = objOraCon;
        OracleDataReader objOraRdr = objOraCom.ExecuteReader();
        while (objOraRdr.Read())
        {
            result = objOraRdr[0].ToString();

        }
        objOraRdr.Close();

        this.disconnectDB();


        return result;
    }

    public bool Insert_rec( string p_fullName, string p_add_1, string p_add_2, string p_add_3, string p_add_4,
                           string p_mobile_tp, string p_title, int p_no_persons, double p_groupDiscRate, string p_plan,
                           string p_destination, string p_departDate, string p_returnDate, string p_travlePurpose, 
                           string p_coverType, double p_netPremium_usd, double p_netPremium_rs,
                           double p_adminFee_rs, double p_policyFee_rs, double p_nbt_rs, double p_vat_rs, double p_finalPremium_rs, 
                           double p_numOfDays, DataTable dtMemTable, double p_dollarValue, double p_SumUsd, double p_finalPrmUsd, string p_iP,
                           double p_discRate, double p_discAmt,string uname,int agentcode, out string refNum)
    {
        bool result = false;
        refID = "";
        refNum = "";
        int newMaxNo = 0;

        try
        {
             
            TRV_Ref refData = new TRV_Ref();
            int lastTwoDigit = Convert.ToInt32(System.DateTime.Now.ToString("yy"));


            var maxNo = refData.GetMaxSeqNo(Convert.ToInt32("999"), lastTwoDigit, "TPI");
            newMaxNo = maxNo + 1;

            bool genState = refData.UpdateMaxSeqNo(Convert.ToInt32("999"), lastTwoDigit, newMaxNo, "TPI");

            if (genState == true)
            {
                TRV_Ref refNo = new TRV_Ref(Convert.ToInt32("999"), lastTwoDigit, "TPI");


                refID = refNo.SUFX1 + "/999/" + refNo.SUFX2 + "/" + refNo.YEAR + "/" + refNo.SEQ;

            }

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
                        
                        string insSql = "INSERT INTO SLIGEN.TRV_QUOT_MAST (REFNO,DESTINATION, DEPART_DATE , RETURN_DATE ,TRAVEL_PURPOSE,TRV_TYPE,FULL_NAME," +
                                        " ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER,TITLE,PLAN,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD," +
                                        "ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE," +
                                        "FINAL_PREMIUM_USD,IP_ADDRESS,BRANCH,POL_TYPE,DISC_RATE,DISC_AMNT,ENTERED_BY,AGENT_CODE,DEBTOR_CODE)" +
                                        "VALUES(:refNo,:dest,:deptDt,:retnDt,:travlPrps,:trvtype,:cntName," +
                                        " :cntAdrs1,:cntAdrs2,:cntAdrs3,:cntAdrs4,:cntNo1,:title,:plan,:numPersns,:discRt, :netPremUsd," +
                                        " :admnFee, :polFee, :nbt, :vat,:finalPrem,sysdate, :netPremRs, :numDays,:sumUsd,:dollarVal," +
                                        " :finalprmUsd,:IpAdd, 999,'TPI',:discRate,:discAmt, :uname, :agentcd,0)";

                        command.CommandText = insSql;

                        OracleParameter oRefNo = new OracleParameter();
                        oRefNo.Value = refID;
                        oRefNo.ParameterName = "refNo";

                        OracleParameter oDest = new OracleParameter();
                        oDest.Value = p_destination.Replace("'","");
                        oDest.ParameterName = "dest";

                        OracleParameter oDeptDate = new OracleParameter();
                        oDeptDate.DbType = DbType.DateTime;
                        oDeptDate.Value =DateTime.Parse( p_departDate.ToString()).ToString("yyyy-MM-dd");
                        oDeptDate.ParameterName = "deptDt";

                        OracleParameter oRetnDate = new OracleParameter();
                        oRetnDate.DbType = DbType.DateTime;
                        oRetnDate.Value = DateTime.Parse(p_returnDate.ToString()).ToString( "yyyy-MM-dd");
                        oRetnDate.ParameterName = "retnDt";

                        OracleParameter oTravlPrps = new OracleParameter();
                        oTravlPrps.DbType = DbType.String;
                        oTravlPrps.Value = p_travlePurpose;
                        oTravlPrps.ParameterName = "travlPrps";

                        OracleParameter oTrvtype = new OracleParameter();
                        oTrvtype.DbType = DbType.String;
                        oTrvtype.Value = p_coverType;
                        oTrvtype.ParameterName = "trvtype";

                        

                        OracleParameter oCntName = new OracleParameter();
                        oCntName.Value = p_fullName;
                        oCntName.ParameterName = "cntName";

                        OracleParameter oCntAdrs1 = new OracleParameter();
                        oCntAdrs1.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_add_1))
                        {
                            oCntAdrs1.Value = DBNull.Value;
                        }
                        else
                        {
                            oCntAdrs1.Value = p_add_1;
                        }
                        oCntAdrs1.ParameterName = "cntAdrs1";

                        OracleParameter oCntAdrs2 = new OracleParameter();
                        oCntAdrs2.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_add_2))
                        {
                            oCntAdrs2.Value = DBNull.Value;
                        }
                        else
                        {
                            oCntAdrs2.Value = p_add_2;
                        }
                        oCntAdrs2.ParameterName = "cntAdrs2";

                        OracleParameter oCntAdrs3 = new OracleParameter();
                        oCntAdrs3.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_add_3))
                        {
                            oCntAdrs3.Value = DBNull.Value;
                        }
                        else
                        {
                            oCntAdrs3.Value = p_add_3;
                        }
                        oCntAdrs3.ParameterName = "cntAdrs3";

                        OracleParameter oCntAdrs4 = new OracleParameter();
                        oCntAdrs4.DbType = DbType.String;
                        if (String.IsNullOrEmpty(p_add_4))
                        {
                            oCntAdrs4.Value = DBNull.Value;
                        }
                        else
                        {
                            oCntAdrs4.Value = p_add_4;
                        }
                        oCntAdrs4.ParameterName = "cntAdrs4";

                        OracleParameter oCntNo1 = new OracleParameter();
                        oCntNo1.Value = p_mobile_tp;
                        oCntNo1.ParameterName = "cntNo1";

                        OracleParameter oTitle = new OracleParameter();
                        oTitle.Value = p_title;
                        oTitle.ParameterName = "title";

                        OracleParameter oPlan = new OracleParameter();
                        oPlan.Value = p_plan;
                        oPlan.ParameterName = "plan";

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

                        OracleParameter oNetPremRs = new OracleParameter();
                        oNetPremRs.DbType = DbType.Double;
                        oNetPremRs.Value = p_netPremium_rs;
                        oNetPremRs.ParameterName = "netPremRs";

                        OracleParameter oNumDays = new OracleParameter();
                        oNumDays.DbType = DbType.Double;
                        oNumDays.Value = p_numOfDays;
                        oNumDays.ParameterName = "numDays";

                        OracleParameter oSumUSD = new OracleParameter();
                        oSumUSD.DbType = DbType.Double;
                        oSumUSD.Value = p_SumUsd;
                        oSumUSD.ParameterName = "sumUsd";

                        OracleParameter oDollarVal = new OracleParameter();
                        oDollarVal.DbType = DbType.Double;
                        oDollarVal.Value = p_dollarValue;
                        oDollarVal.ParameterName = "dollarVal";

                        OracleParameter oFinalPrmUsd = new OracleParameter();
                        oFinalPrmUsd.DbType = DbType.Double;
                        oFinalPrmUsd.Value = p_finalPrmUsd;
                        oFinalPrmUsd.ParameterName = "finalprmUsd";

                        OracleParameter oIP = new OracleParameter();
                        oIP.DbType = DbType.String;
                        oIP.Value = p_iP;
                        oIP.ParameterName = "IpAdd";


                        OracleParameter oDisntcRate = new OracleParameter();
                        oDisntcRate.DbType = DbType.Double;
                        oDisntcRate.Value = p_discRate;
                        oDisntcRate.ParameterName = "discRate";


                        OracleParameter oDisntAmt = new OracleParameter();
                        oDisntAmt.DbType = DbType.Double;
                        oDisntAmt.Value = p_discAmt;
                        oDisntAmt.ParameterName = "discAmt";

                        OracleParameter oUname = new OracleParameter();
                        oUname.DbType = DbType.String;
                        oUname.Value = uname;
                        oUname.ParameterName = "uname";

                        OracleParameter oAgent = new OracleParameter();
                        oAgent.DbType = DbType.Int32;
                        oAgent.Value = agentcode;
                        oAgent.ParameterName = "agentcd";


                        command.Parameters.Add(oRefNo);
                        command.Parameters.Add(oDest);
                        command.Parameters.Add(oDeptDate);
                        command.Parameters.Add(oRetnDate);
                        command.Parameters.Add(oTravlPrps);
                        command.Parameters.Add(oTrvtype);
                        command.Parameters.Add(oCntName);
                        command.Parameters.Add(oCntAdrs1);
                        command.Parameters.Add(oCntAdrs2);
                        command.Parameters.Add(oCntAdrs3);
                        command.Parameters.Add(oCntAdrs4);
                        command.Parameters.Add(oCntNo1);
                        command.Parameters.Add(oTitle);
                        command.Parameters.Add(oPlan);
                        command.Parameters.Add(oNumPrsns);
                        command.Parameters.Add(oDiscRate);
                        command.Parameters.Add(oNetPremUsd);
                        command.Parameters.Add(oAdmnFee);
                        command.Parameters.Add(oPolFee);
                        command.Parameters.Add(oNbt);
                        command.Parameters.Add(oVat);
                        command.Parameters.Add(oFinalPrem);
                        command.Parameters.Add(oNetPremRs);
                        command.Parameters.Add(oNumDays);
                        command.Parameters.Add(oSumUSD);
                        command.Parameters.Add(oDollarVal);
                        command.Parameters.Add(oFinalPrmUsd);
                        command.Parameters.Add(oIP);
                        command.Parameters.Add(oDisntcRate);
                        command.Parameters.Add(oDisntAmt);
                        command.Parameters.Add(oUname);
                        command.Parameters.Add(oAgent);


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


                                string sqlMem = "INSERT INTO SLIGEN.TRV_QUOT_MEM_DETAILS (REF_NO, MEM_ID, MEM_TYPE, GENDER, DOB, AGE, BASE_AMOUNT_USD,ENTERED_DATE) " +
                                  "Values (:refid, :memid, :mtype, :gen, :dob, :age, :base, sysdate)";

                                command.CommandText = sqlMem;

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
                        logger.write_log("Failed at trv_proposal_mast->insert_rec: " + u.ToString());
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

                    string sql = "UPDATE SLIGEN.TRV_QUOT_MAST" +
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
                                 " USD_RATE = :dollarVal," +
                                 " FINAL_PREMIUM_USD= :netPremUsd"  +
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

                        string updsql = "UPDATE SLIGEN.TRV_QUOT_MEM_DETAILS" +
                              " SET MEM_TYPE = :mtype," +
                              " GENDER = :gen," +
                              " DOB = :dob," +
                              " AGE = :age," +
                              " BASE_AMOUNT_USD = :base," +
                              " ENTERED_DATE = sysdate" +
                              " WHERE REF_NO = :refId" +
                              " AND MEM_ID = :memid";

                        command.CommandText = updsql;

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
                    logger.write_log("Failed at trv_proposal_mast->update_rec: " + u.ToString());
                }
            }

        }
        catch (Exception u)
        {
            string g = u.ToString();
            log logger = new log();
            logger.write_log("Failed at trv_proposal_mast->update_rec: " + u.ToString());
        }
        finally
        {

        }

        return result;

    }

    public double get_SumIns(string planid, string poltype)
    {
        double sumins = 0.00;
        try
        {
            this.connectDB();
       


        string sql = "select sum_ins_usd from sligen.trv_schemes where pol_type= :poltype and UPPER(TRIM(CODE)) = :planid";

        objOraCom.CommandType = CommandType.Text;
        objOraCom.CommandText = sql;
        objOraCom.Connection = objOraCon;

        objOraCom.Parameters.Add("poltype", OdbcType.VarChar);
        objOraCom.Parameters["poltype"].Value = poltype;

        objOraCom.Parameters.Add("planid", OdbcType.VarChar);
        objOraCom.Parameters["planid"].Value = planid.ToUpper();

        OracleDataReader quotReader = objOraCom.ExecuteReader();

        while (quotReader.Read())
        {
            if (!quotReader.IsDBNull(0))
            {
                sumins = quotReader.GetDouble(0);
            }
        }

        quotReader.Close();
            this.disconnectDB();
        }
        catch { }

        return sumins;

    }

}