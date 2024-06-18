using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Cls_Proposal
/// </summary>
public class TRV_Premium_Endorsement
{

    OracleConnection objOraCon = new OracleConnection();
    OracleCommand objOraCom = new OracleCommand();


    public string REFNO { get; set; }
    public string DESTINATION { get; set; }
    public string PREVIOUS_DEP { get; set; }
    public string PREVIOUS_ARR { get; set; }

    public int PREVIOUS_DAYS{ get; set; }
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
    public string AGNT_RATE_CODE { get; set; }
    public int SLI_NO1 { get; set; }
    public int SLI_NO2 { get; set; }
    public string CLIENT_ID { get; set; }

  

    public int BRANCH { get; set; }
    public string UPD_TYPE { get; set; }
    public int SEQ_NO { get; set; }

    public int SER_BRCD { get; set; }
    public int SCH_PRINT { get; set; }

    public int SCH_BRANCH { get; set; }

    public string NEW_DEP { get; set; }
    public string NEW_ARR { get; set; }

    public string End_type { get; set; }

    public string VAT_WAVED { get; set; }

    public string VAT_NO { get; set; }

    public string SVAT_NO { get; set; }

    public double SVAT_AMT { get; set; }

    public string VatWVValue { get; set; }

    public double SummRs { get; set; }

    public string SCH_PRINTBY { get; set; }

    public string SCH_PIP { get; set; }

    public int Extended_Type { get; set; }

    public double LOADING_RATE { get; set; }

    public double LOADING_AMNT{ get; set; }

    public string NEW_DESTINATION { get; set; }

    public string NEW_PLAN { get; set; }

    public double NEW_SUM_INS_USD { get; set; }

    //public ArrayList baseUsd = new ArrayList();

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
    public TRV_Premium_Endorsement()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public TRV_Premium_Endorsement(string refNo)
    {
        try
        {
            connectDB();

            string sql = "SELECT POLNO,DESTINATION,to_char(PREVIOUS_DEP,'yyyy/MM/dd') AS PREVIOUS_DEP,to_char(PREVIOUS_ARR,'yyyy/MM/dd') AS PREVIOUS_ARR,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4,TRAVEL_PURPOSE, " +
                         " CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3, " +
                         " ADDRESS4,MOBILE_NUMBER,HOME_NUMBER,OFFICE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS, " +
                         " POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,to_char(ENTERED_DATE,'yyyy-MM-dd') AS ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD, " +
                         " USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,ENT_BRANCH,UPD_TYPE,SEQ_NO,SER_BRCD,SCH_PRINT,NVL((SCH_BRANCH),0) AS SCH_BRANCH,PREVIOUS_DAYS,to_char(NEW_DEP,'yyyy/MM/dd') AS NEW_DEP," +
                         "to_char(NEW_ARR,'yyyy/MM/dd') AS NEW_ARR,VAT_WAVED,VAT_NO,SVAT_NO,SVAT_AMT,SCH_PRINTBY,SCH_PIP,LOADING_RATE,LOADING_AMNT,NEW_DESTINATION,NEW_PLAN,NEW_SUM_INS_USD FROM SLIGEN.TRV_POL_PRM_ENDO WHERE POLNO= :refNo ";

            using (OracleCommand cmd = new OracleCommand(sql, objOraCon))
            {
                OracleParameter opRefNo = new OracleParameter();
                opRefNo.Value = refNo;
                opRefNo.ParameterName = "refNo";

                cmd.Parameters.Add(opRefNo);

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    REFNO = reader[0].ToString();
                    DESTINATION = reader[1].ToString();
                    PREVIOUS_DEP = reader[2].ToString();
                    PREVIOUS_ARR = reader[3].ToString().Trim();
                    VISIT_CTRY1 = reader[4].ToString().Trim();
                    VISIT_CTRY2 = reader[5].ToString().Trim();
                    VISIT_CTRY3 = reader[6].ToString().Trim();
                    VISIT_CTRY4 = reader[7].ToString().Trim();
                    TRAVEL_PURPOSE = reader[8].ToString().Trim();
                    CONTCT_NAME = reader[9].ToString().Trim();
                    CONTCT_ADRS1 = reader[10].ToString().Trim();
                    CONTCT_ADRS2 = reader[11].ToString().Trim();
                    CONTCT_ADRS3 = reader[12].ToString().Trim();
                    CONTCT_ADRS4 = reader[13].ToString().Trim();
                    CONTCT_NO1 = reader[14].ToString().Trim();
                    CONTCT_NO2 = reader[15].ToString().Trim();
                    TRV_TYPE = reader[16].ToString().Trim();
                    FULL_NAME = reader[17].ToString().Trim();
                    ADDRESS1 = reader[18].ToString().Trim();
                    ADDRESS2 = reader[19].ToString().Trim();
                    ADDRESS3 = reader[20].ToString().Trim();
                    ADDRESS4 = reader[21].ToString().Trim();
                    MOBILE_NUMBER = reader[22].ToString().Trim();
                    HOME_NUMBER = reader[23].ToString().Trim();
                    OFFICE_NUMBER = reader[24].ToString().Trim();
                    PLAN = reader[25].ToString().Trim();
                    TITLE = reader[26].ToString().Trim();
                    NUM_OF_PERSONS = Convert.ToInt32(reader[27].ToString());
                    GROUP_DISC_RATE = Convert.ToDouble(reader[28].ToString());
                    NET_PREMIUM_USD = Convert.ToDouble(reader[29].ToString());
                    ADMIN_FEE_RS = Convert.ToDouble(reader[30].ToString());
                    POLICY_FEE_RS = Convert.ToDouble(reader[31].ToString());
                    NBT_RS = Convert.ToDouble(reader[32].ToString());
                    VAT_RS = Convert.ToDouble(reader[33].ToString());
                    FINAL_PREMIUM_RS = Convert.ToDouble(reader[34].ToString());
                    TAXES_EXPENSES_RS = Convert.ToDouble(reader[35].ToString());
                    ENTERED_DATE = reader[36].ToString();
                    NET_PREMIUM_RS = Convert.ToDouble(reader[37].ToString());
                    NO_OF_DAYS = Convert.ToInt32(reader[38].ToString());
                    SUM_INS_USD = Convert.ToDouble(reader[39].ToString());
                    USD_RATE = Convert.ToDouble(reader[40].ToString());
                    FINAL_PREMIUM_USD = Convert.ToDouble(reader[41].ToString());
                    IP_ADDRESS = reader[42].ToString().Trim();
                    ENTERED_BY = reader[43].ToString().Trim();
                    AGENT_CODE = Convert.ToInt32(reader[44].ToString());
                    DEBTOR_CODE = Convert.ToInt32(reader[45].ToString());
                    CURR_TYPE = reader[46].ToString().Trim();
                    AGNT_RATE_CODE = reader[47].ToString().Trim();
                    SLI_NO1 = Convert.ToInt32(reader[48].ToString().Trim());
                    SLI_NO2 = Convert.ToInt32(reader[49].ToString().Trim());
                    CLIENT_ID = reader[50].ToString().Trim();
                    BRANCH = Convert.ToInt32(reader[51].ToString().Trim());
                    UPD_TYPE = reader[52].ToString().Trim();
                    SEQ_NO = Convert.ToInt32(reader[53].ToString().Trim());
                    SER_BRCD = Convert.ToInt32(reader[54].ToString().Trim());
                    SCH_PRINT = Convert.ToInt32(reader[55].ToString().Trim());
                    SCH_BRANCH = Convert.ToInt32(reader[56].ToString().Trim());
                    PREVIOUS_DAYS = Convert.ToInt32(reader[57].ToString().Trim());
                    NEW_DEP = reader[58].ToString();
                    NEW_ARR = reader[59].ToString();
                    VAT_WAVED = reader[60].ToString();
                    VAT_NO = reader[61].ToString();
                    SVAT_NO = reader[62].ToString();
                    SVAT_AMT = Convert.ToDouble(reader[63].ToString());
                    SCH_PRINTBY = reader[64].ToString();
                    SCH_PIP = reader[65].ToString();
                    LOADING_RATE = Convert.ToDouble(reader[66].ToString());
                    LOADING_AMNT = Convert.ToDouble(reader[67].ToString());
                    NEW_DESTINATION = reader[68].ToString();
                    NEW_PLAN = reader[69].ToString();
                    NEW_SUM_INS_USD = Convert.ToDouble(reader[70].ToString());

                    SummRs = SUM_INS_USD * USD_RATE;
                }
            }

        }
        catch (Exception ex)
        {

        }
        finally
        {
            disconnectDB();
        }
    }



    public TRV_Premium_Endorsement(string refNo,string type)
    {
        try
        {
            connectDB();

            string sql = "SELECT POLNO,DESTINATION,to_char(PREVIOUS_DEP,'yyyy/MM/dd') AS PREVIOUS_DEP,to_char(PREVIOUS_ARR,'yyyy/MM/dd') AS PREVIOUS_ARR,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4,TRAVEL_PURPOSE, " +
                         " CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3, " +
                         " ADDRESS4,MOBILE_NUMBER,HOME_NUMBER,OFFICE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS, " +
                         " POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,to_char(ENTERED_DATE,'yyyy-MM-dd') AS ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD, " +
                         " USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,ENT_BRANCH,UPD_TYPE,SEQ_NO,SER_BRCD,SCH_PRINT,NVL((SCH_BRANCH),0) AS SCH_BRANCH,PREVIOUS_DAYS,to_char(NEW_DEP,'yyyy/MM/dd') AS NEW_DEP," +
                         "to_char(NEW_ARR,'yyyy/MM/dd') AS NEW_ARR,VAT_WAVED,VAT_NO,SVAT_NO,SVAT_AMT,SCH_PRINTBY,SCH_PIP,LOADING_RATE,LOADING_AMNT,NEW_DESTINATION,NEW_PLAN,NEW_SUM_INS_USD FROM SLIGEN.TRV_POL_PRM_ENDO WHERE POLNO= :refNo " +
                         " AND SEQ_NO IN (SELECT MAX(SEQ_NO) FROM SLIGEN.TRV_POL_PRM_ENDO WHERE  POLNO = :refNo AND UPD_TYPE='" + type + "') AND UPD_TYPE='" + type + "'";

            using (OracleCommand cmd = new OracleCommand(sql, objOraCon))
            {
                OracleParameter opRefNo = new OracleParameter();
                opRefNo.Value = refNo;
                opRefNo.ParameterName = "refNo";

                cmd.Parameters.Add(opRefNo);

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    REFNO = reader[0].ToString();
                    DESTINATION = reader[1].ToString();
                    PREVIOUS_DEP = reader[2].ToString();
                    PREVIOUS_ARR = reader[3].ToString().Trim();
                    VISIT_CTRY1 = reader[4].ToString().Trim();
                    VISIT_CTRY2 = reader[5].ToString().Trim();
                    VISIT_CTRY3 = reader[6].ToString().Trim();
                    VISIT_CTRY4 = reader[7].ToString().Trim();
                    TRAVEL_PURPOSE = reader[8].ToString().Trim();
                    CONTCT_NAME = reader[9].ToString().Trim();
                    CONTCT_ADRS1 = reader[10].ToString().Trim();
                    CONTCT_ADRS2 = reader[11].ToString().Trim();
                    CONTCT_ADRS3 = reader[12].ToString().Trim();
                    CONTCT_ADRS4 = reader[13].ToString().Trim();
                    CONTCT_NO1 = reader[14].ToString().Trim();
                    CONTCT_NO2 = reader[15].ToString().Trim();
                    TRV_TYPE = reader[16].ToString().Trim();
                    FULL_NAME = reader[17].ToString().Trim();
                    ADDRESS1 = reader[18].ToString().Trim();
                    ADDRESS2 = reader[19].ToString().Trim();
                    ADDRESS3 = reader[20].ToString().Trim();
                    ADDRESS4 = reader[21].ToString().Trim();
                    MOBILE_NUMBER = reader[22].ToString().Trim();
                    HOME_NUMBER = reader[23].ToString().Trim();
                    OFFICE_NUMBER = reader[24].ToString().Trim();
                    PLAN = reader[25].ToString().Trim();
                    TITLE = reader[26].ToString().Trim();
                    NUM_OF_PERSONS = Convert.ToInt32(reader[27].ToString());
                    GROUP_DISC_RATE = Convert.ToDouble(reader[28].ToString());
                    NET_PREMIUM_USD = Convert.ToDouble(reader[29].ToString());
                    ADMIN_FEE_RS = Convert.ToDouble(reader[30].ToString());
                    POLICY_FEE_RS = Convert.ToDouble(reader[31].ToString());
                    NBT_RS = Convert.ToDouble(reader[32].ToString());
                    VAT_RS = Convert.ToDouble(reader[33].ToString());
                    FINAL_PREMIUM_RS = Convert.ToDouble(reader[34].ToString());
                    TAXES_EXPENSES_RS = Convert.ToDouble(reader[35].ToString());
                    ENTERED_DATE = reader[36].ToString();
                    NET_PREMIUM_RS = Convert.ToDouble(reader[37].ToString());
                    NO_OF_DAYS = Convert.ToInt32(reader[38].ToString());
                    SUM_INS_USD = Convert.ToDouble(reader[39].ToString());
                    USD_RATE = Convert.ToDouble(reader[40].ToString());
                    FINAL_PREMIUM_USD = Convert.ToDouble(reader[41].ToString());
                    IP_ADDRESS = reader[42].ToString().Trim();
                    ENTERED_BY = reader[43].ToString().Trim();
                    AGENT_CODE = Convert.ToInt32(reader[44].ToString());
                    DEBTOR_CODE = Convert.ToInt32(reader[45].ToString());
                    CURR_TYPE = reader[46].ToString().Trim();
                    AGNT_RATE_CODE = reader[47].ToString().Trim();
                    SLI_NO1 = Convert.ToInt32(reader[48].ToString().Trim());
                    SLI_NO2 = Convert.ToInt32(reader[49].ToString().Trim());
                    CLIENT_ID = reader[50].ToString().Trim();
                    BRANCH = Convert.ToInt32(reader[51].ToString().Trim());
                    UPD_TYPE = reader[52].ToString().Trim();
                    SEQ_NO = Convert.ToInt32(reader[53].ToString().Trim());
                    SER_BRCD = Convert.ToInt32(reader[54].ToString().Trim());
                    SCH_PRINT = Convert.ToInt32(reader[55].ToString().Trim());
                    SCH_BRANCH = Convert.ToInt32(reader[56].ToString().Trim());
                    PREVIOUS_DAYS = Convert.ToInt32(reader[57].ToString().Trim());
                    NEW_DEP = reader[58].ToString();
                    NEW_ARR = reader[59].ToString();
                    VAT_WAVED = reader[60].ToString();
                    VAT_NO = reader[61].ToString();
                    SVAT_NO = reader[62].ToString();
                    SVAT_AMT = Convert.ToDouble(reader[63].ToString());
                    SCH_PRINTBY = reader[64].ToString();
                    SCH_PIP = reader[65].ToString();
                    LOADING_RATE = Convert.ToDouble(reader[66].ToString());
                    LOADING_AMNT = Convert.ToDouble(reader[67].ToString());
                    NEW_DESTINATION = reader[68].ToString();
                    NEW_PLAN = reader[69].ToString();
                    NEW_SUM_INS_USD = Convert.ToDouble(reader[70].ToString());

                    SummRs = SUM_INS_USD * USD_RATE;
                }
            }

        }
        catch (Exception ex)
        {

        }
        finally
        {
            disconnectDB();
        }
    }

    public bool InsertPremiumEndorsementData(TRV_Premium_Endorsement clsPol, TRV_Policy_mast clsMast, ArrayList usdArr)
    {
        bool returnValue = false;
        List<TRV_Policy_mem> GT_mem = clsMast.members;

        connectDB();
        OracleCommand cmd = objOraCon.CreateCommand();
        OracleTransaction trans = objOraCon.BeginTransaction();
        cmd.Transaction = trans;

        try
        {
            using (cmd)
            {

                if (clsPol.UPD_TYPE == "P")
                {

                    string updateClmMast = "";
                    int CURRENT_MAX = 0;
                    int MAX_SEQ = 0;
                    string fullName = "";
                    string comName = "";


                    DateTime newDt = DateTime.ParseExact(clsPol.NEW_DEP, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    DateTime newArr = DateTime.ParseExact(clsPol.NEW_ARR, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    DateTime currDt = DateTime.ParseExact(clsMast.departDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    DateTime currArr = DateTime.ParseExact(clsMast.returnDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                    if (clsPol.End_type == "E")
                    {
                        if (newDt.Date == currDt.Date && newArr.Date == currArr.Date)
                        {
                            string maxSeq = "SELECT NVL(MAX(SEQ_NO),0) AS CURRENT_MAX FROM SLIGEN.TRV_POL_PRM_ENDO WHERE POLNO = '" + clsPol.REFNO + "' AND UPD_TYPE = '" + clsPol.UPD_TYPE + "'";
                            cmd.CommandText = maxSeq;


                            OracleDataReader reader_2 = cmd.ExecuteReader();
                            cmd.Parameters.Clear();

                            while (reader_2.Read())
                            {
                                if (!reader_2.IsDBNull(0))
                                {
                                    CURRENT_MAX = reader_2.GetInt32(0);
                                }
                            }

                            MAX_SEQ = CURRENT_MAX + 1;

                            string keepPolMastBkp = "insert into SLIGEN.TRV_POL_MAST_HIST (POLNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4,TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER,HOME_NUMBER,OFFICE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,SER_BRCD,VAT_WAVED,VAT_NO,REF_NO,SCH_PRINT,SCH_PRINTBY,SCH_PDATE,SCH_PIP,ENT_BRANCH,SCH_BRANCH,EDIT_TAG) " +
                            " select POLNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4,TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER,HOME_NUMBER,OFFICE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,SER_BRCD,VAT_WAVED,VAT_NO,REF_NO,SCH_PRINT,SCH_PRINTBY,SCH_PDATE,SCH_PIP,ENT_BRANCH,SCH_BRANCH,'" + clsPol.UPD_TYPE + "' " +
                            " from SLIGEN.TRV_POL_MAST where POLNO = '" + clsPol.REFNO + "'";

                            cmd.CommandText = keepPolMastBkp;
                            cmd.ExecuteNonQuery();

                            clsPol.SCH_PRINT = 0;

                            string insertEndor = "insert into sligen.TRV_POL_PRM_ENDO (POLNO,SEQ_NO,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,SLI_NO1,SLI_NO2,SER_BRCD,ENT_BRANCH,UPD_TYPE,SCH_PRINT,SCH_BRANCH,PREVIOUS_DEP,PREVIOUS_ARR,PREVIOUS_DAYS,NEW_DEP,NEW_ARR,PLAN,CLIENT_ID,FULL_NAME,TRV_TYPE,LOADING_RATE) values ('" + clsPol.REFNO + "'," + MAX_SEQ + "," + clsPol.NUM_OF_PERSONS + "," + clsPol.GROUP_DISC_RATE + "," + clsPol.NET_PREMIUM_USD + "," + clsPol.ADMIN_FEE_RS + "," + clsPol.POLICY_FEE_RS + "," + clsPol.NBT_RS + "," + clsPol.VAT_RS + "," + clsPol.FINAL_PREMIUM_RS + "," + clsPol.TAXES_EXPENSES_RS + ",SYSDATE," + clsPol.NET_PREMIUM_RS + "," + clsPol.NO_OF_DAYS + "," + clsPol.SUM_INS_USD + "," + clsPol.USD_RATE + "," + clsPol.FINAL_PREMIUM_USD + ",'" + clsPol.IP_ADDRESS + "','" + clsPol.ENTERED_BY + "'," + clsPol.AGENT_CODE + "," + clsPol.DEBTOR_CODE + "," + clsPol.SLI_NO1 + "," + clsPol.SLI_NO2 + "," + clsPol.SER_BRCD + "," + clsPol.BRANCH + ",'" + clsPol.UPD_TYPE + "'," + clsPol.SCH_PRINT + "," + clsPol.SCH_BRANCH + ",TO_DATE('" + clsPol.PREVIOUS_DEP + "', 'yyyy/MM/dd'),TO_DATE('" + clsPol.PREVIOUS_ARR + "', 'yyyy/MM/dd')," + clsPol.PREVIOUS_DAYS + ",TO_DATE('" + clsPol.NEW_DEP + "', 'yyyy/MM/dd'),TO_DATE('" + clsPol.NEW_ARR + "', 'yyyy/MM/dd'),'" + clsPol.PLAN + "','" + clsPol.CLIENT_ID + "','" + clsPol.FULL_NAME + "','" + clsPol.TRV_TYPE + "'," + clsPol.LOADING_RATE + ")";

                            string insSql = "insert into SLIGEN.trv_pol_mem_details_his (pol_no, mem_id, mem_type,GENDER,DOB,AGE,NAME,PP_NO,TITLE," +
                                "  BASE_AMOUNT_USD,ENTERED_DATE) (SELECT pol_no, mem_id, mem_type, GENDER, DOB, AGE, NAME, PP_NO, TITLE," +
                                " BASE_AMOUNT_USD, ENTERED_DATE FROM SLIGEN.trv_pol_mem_details where pol_no = '" + clsPol.REFNO + "') ";

                            cmd.CommandText = insSql;

                            cmd.ExecuteNonQuery();

                            for (int i = 0; i < GT_mem.Count; i++)
                            {

                                //foreach (var mem in GT_mem)
                                //{
                                string insMember = "INSERT INTO SLIGEN.TRV_POL_MEM_DETAILS_ENDO (POL_NO,MEM_TYPE,GENDER,DOB,AGE,TITLE,NAME,PP_NO,MEM_ID,BASE_AMOUNT_USD,ENTERED_DATE,SEQ_NO,ENTERED_BY,UPD_TYPE)" +
                                    " VALUES('" + clsPol.REFNO + "','" + GT_mem[i].member_type + "','" + GT_mem[i].gender + "'," +
                                    "to_date('" + GT_mem[i].dob + "','yyyy/mm/dd')," + GT_mem[i].age + "" +
                                    ",'" + GT_mem[i].title + "','" + PrepareApostrophe(GT_mem[i].name) + "'" +
                                    ",'" + GT_mem[i].ppno + "','" + GT_mem[i].member_id + "'" +
                                    "," + usdArr[i] + ",sysdate," + MAX_SEQ + ",'" + clsPol.ENTERED_BY + "','"+clsPol.UPD_TYPE+"')";

                                cmd.CommandText = insMember;

                                cmd.ExecuteNonQuery();
                            }

                            cmd.CommandText = insertEndor;
                            cmd.ExecuteNonQuery();


                            cmd.Parameters.Clear();

                            trans.Commit();
                            returnValue = true;
                        }
                        else
                        {
                            if (clsPol.SCH_PRINT == 1)
                            {

                                string maxSeq = "SELECT NVL(MAX(SEQ_NO),0) AS CURRENT_MAX FROM SLIGEN.TRV_POL_PRM_ENDO WHERE POLNO = '" + clsPol.REFNO + "' AND UPD_TYPE = '" + clsPol.UPD_TYPE + "'";
                                cmd.CommandText = maxSeq;


                                OracleDataReader reader_2 = cmd.ExecuteReader();
                                cmd.Parameters.Clear();

                                while (reader_2.Read())
                                {
                                    if (!reader_2.IsDBNull(0))
                                    {
                                        CURRENT_MAX = reader_2.GetInt32(0);
                                    }
                                }

                                MAX_SEQ = CURRENT_MAX + 1;

                                string keepPolMastBkp = "insert into SLIGEN.TRV_POL_MAST_HIST (POLNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4,TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER,HOME_NUMBER,OFFICE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,SER_BRCD,VAT_WAVED,VAT_NO,REF_NO,SCH_PRINT,SCH_PRINTBY,SCH_PDATE,SCH_PIP,ENT_BRANCH,SCH_BRANCH,EDIT_TAG) " +
                                " select POLNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4,TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER,HOME_NUMBER,OFFICE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,SER_BRCD,VAT_WAVED,VAT_NO,REF_NO,SCH_PRINT,SCH_PRINTBY,SCH_PDATE,SCH_PIP,ENT_BRANCH,SCH_BRANCH,'" + clsPol.UPD_TYPE + "' " +
                                " from SLIGEN.TRV_POL_MAST where POLNO = '" + clsPol.REFNO + "'";

                                cmd.CommandText = keepPolMastBkp;
                                cmd.ExecuteNonQuery();

                                clsPol.SCH_PRINT = 0;

                                string insertEndor = "insert into sligen.TRV_POL_PRM_ENDO (POLNO,SEQ_NO,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,SLI_NO1,SLI_NO2,SER_BRCD,ENT_BRANCH,UPD_TYPE,SCH_PRINT,SCH_BRANCH,PREVIOUS_DEP,PREVIOUS_ARR,PREVIOUS_DAYS,NEW_DEP,NEW_ARR,PLAN,CLIENT_ID,FULL_NAME,TRV_TYPE,LOADING_RATE) values ('" + clsPol.REFNO + "'," + MAX_SEQ + "," + clsPol.NUM_OF_PERSONS + "," + clsPol.GROUP_DISC_RATE + "," + clsPol.NET_PREMIUM_USD + "," + clsPol.ADMIN_FEE_RS + "," + clsPol.POLICY_FEE_RS + "," + clsPol.NBT_RS + "," + clsPol.VAT_RS + "," + clsPol.FINAL_PREMIUM_RS + "," + clsPol.TAXES_EXPENSES_RS + ",SYSDATE," + clsPol.NET_PREMIUM_RS + "," + clsPol.NO_OF_DAYS + "," + clsPol.SUM_INS_USD + "," + clsPol.USD_RATE + "," + clsPol.FINAL_PREMIUM_USD + ",'" + clsPol.IP_ADDRESS + "','" + clsPol.ENTERED_BY + "'," + clsPol.AGENT_CODE + "," + clsPol.DEBTOR_CODE + "," + clsPol.SLI_NO1 + "," + clsPol.SLI_NO2 + "," + clsPol.SER_BRCD + "," + clsPol.BRANCH + ",'" + clsPol.UPD_TYPE + "'," + clsPol.SCH_PRINT + "," + clsPol.SCH_BRANCH + ",TO_DATE('" + clsPol.PREVIOUS_DEP + "', 'yyyy/MM/dd'),TO_DATE('" + clsPol.PREVIOUS_ARR + "', 'yyyy/MM/dd')," + clsPol.PREVIOUS_DAYS + ",TO_DATE('" + clsPol.NEW_DEP + "', 'yyyy/MM/dd'),TO_DATE('" + clsPol.NEW_ARR + "', 'yyyy/MM/dd'),'" + clsPol.PLAN + "','" + clsPol.CLIENT_ID + "','" + clsPol.FULL_NAME + "','" + clsPol.TRV_TYPE + "'," + clsPol.LOADING_RATE + ")";

                                string insSql = "insert into SLIGEN.trv_pol_mem_details_his (pol_no, mem_id, mem_type,GENDER,DOB,AGE,NAME,PP_NO,TITLE," +
                                    "  BASE_AMOUNT_USD,ENTERED_DATE) (SELECT pol_no, mem_id, mem_type, GENDER, DOB, AGE, NAME, PP_NO, TITLE," +
                                    " BASE_AMOUNT_USD, ENTERED_DATE FROM SLIGEN.trv_pol_mem_details where pol_no = '" + clsPol.REFNO + "') ";

                                cmd.CommandText = insSql;

                                cmd.ExecuteNonQuery();

                                for (int i = 0; i < GT_mem.Count; i++)
                                {

                                    //foreach (var mem in GT_mem)
                                    //{
                                    string insMember = "INSERT INTO SLIGEN.TRV_POL_MEM_DETAILS_ENDO (POL_NO,MEM_TYPE,GENDER,DOB,AGE,TITLE,NAME,PP_NO,MEM_ID,BASE_AMOUNT_USD,ENTERED_DATE,SEQ_NO,ENTERED_BY,UPD_TYPE)" +
                                        " VALUES('" + clsPol.REFNO + "','" + GT_mem[i].member_type + "','" + GT_mem[i].gender + "'," +
                                        "to_date('" + GT_mem[i].dob + "','yyyy/mm/dd')," + GT_mem[i].age + "" +
                                        ",'" + GT_mem[i].title + "','" + PrepareApostrophe(GT_mem[i].name) + "'" +
                                        ",'" + GT_mem[i].ppno + "','" + GT_mem[i].member_id + "'" +
                                        "," + usdArr[i] + ",sysdate," + MAX_SEQ + ",'" + clsPol.ENTERED_BY + "','"+clsPol.UPD_TYPE+"')";

                                    cmd.CommandText = insMember;

                                    cmd.ExecuteNonQuery();
                                }

                                cmd.CommandText = insertEndor;
                                cmd.ExecuteNonQuery();


                                cmd.Parameters.Clear();

                                trans.Commit();
                                returnValue = true;
                            }
                            else
                            {
                                string insSql = "insert into SLIGEN.trv_pol_mem_details_his (pol_no, mem_id, mem_type,GENDER,DOB,AGE,NAME,PP_NO,TITLE," +
                               "  BASE_AMOUNT_USD,ENTERED_DATE) (SELECT pol_no, mem_id, mem_type, GENDER, DOB, AGE, NAME, PP_NO, TITLE," +
                               " BASE_AMOUNT_USD, ENTERED_DATE FROM SLIGEN.trv_pol_mem_details where pol_no = '" + clsPol.REFNO + "') ";

                                cmd.CommandText = insSql;

                                cmd.ExecuteNonQuery();

                                string insertEndor = "update SLIGEN.TRV_POL_PRM_ENDO " +
                                            " set NET_PREMIUM_USD=" + clsPol.NET_PREMIUM_USD + ",FINAL_PREMIUM_USD=" + clsPol.FINAL_PREMIUM_USD + ",ADMIN_FEE_RS=" + clsPol.ADMIN_FEE_RS + ",POLICY_FEE_RS=" + clsPol.POLICY_FEE_RS + ",NBT_RS=" + clsPol.NBT_RS + ",VAT_RS=" + clsPol.VAT_RS + ",FINAL_PREMIUM_RS=" + clsPol.FINAL_PREMIUM_RS + ",NET_PREMIUM_RS=" + clsPol.NET_PREMIUM_RS + ",PREVIOUS_DEP=TO_DATE('" + clsPol.PREVIOUS_DEP + "', 'yyyy/MM/dd'),PREVIOUS_ARR=TO_DATE('" + clsPol.PREVIOUS_ARR + "', 'yyyy/MM/dd'),PREVIOUS_DAYS=" + clsPol.PREVIOUS_DAYS + ",NEW_DEP=TO_DATE('" + clsPol.NEW_DEP + "', 'yyyy/MM/dd'),NEW_ARR=TO_DATE('" + clsPol.NEW_ARR + "', 'yyyy/MM/dd'),NO_OF_DAYS=" + clsPol.NO_OF_DAYS + ",LOADING_RATE = " + clsPol.LOADING_RATE + "" +
                                            " where POLNO='" + clsPol.REFNO + "' and SEQ_NO=" + clsPol.SEQ_NO + " and UPD_TYPE = '" + clsPol.UPD_TYPE + "'";
                                // "(POLNO,SEQ_NO,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,SLI_NO1,SLI_NO2,SER_BRCD,ENT_BRANCH,UPD_TYPE,SCH_PRINT,SCH_BRANCH,PREVIOUS_DEP,PREVIOUS_ARR,PREVIOUS_DAYS,NEW_DEP,NEW_ARR) values ('" + clsPol.REFNO + "'," + MAX_SEQ + "," + clsPol.NUM_OF_PERSONS + "," + clsPol.GROUP_DISC_RATE + "," + clsPol.NET_PREMIUM_USD + "," + clsPol.ADMIN_FEE_RS + "," + clsPol.POLICY_FEE_RS + "," + clsPol.NBT_RS + "," + clsPol.VAT_RS + "," + clsPol.FINAL_PREMIUM_RS + "," + clsPol.TAXES_EXPENSES_RS + ",SYSDATE," + clsPol.NET_PREMIUM_RS + "," + clsPol.NO_OF_DAYS + "," + clsPol.SUM_INS_USD + "," + clsPol.USD_RATE + "," + clsPol.FINAL_PREMIUM_USD + ",'" + clsPol.IP_ADDRESS + "','" + clsPol.ENTERED_BY + "'," + clsPol.AGENT_CODE + "," + clsPol.DEBTOR_CODE + "," + clsPol.SLI_NO1 + "," + clsPol.SLI_NO2 + "," + clsPol.SER_BRCD + "," + clsPol.BRANCH + ",'" + clsPol.UPD_TYPE + "'," + clsPol.SCH_PRINT + "," + clsPol.SCH_BRANCH + ",TO_DATE('" + clsPol.PREVIOUS_DEP + "', 'yyyy/MM/dd'),TO_DATE('" + clsPol.PREVIOUS_ARR + "', 'yyyy/MM/dd')," + clsPol.PREVIOUS_DAYS + ",TO_DATE('" + clsPol.NEW_DEP + "', 'yyyy/MM/dd'),TO_DATE('" + clsPol.NEW_ARR + "', 'yyyy/MM/dd'))";

                                for (int i = 0; i < GT_mem.Count; i++)
                                {
                                    string upMembers = "UPDATE SLIGEN.TRV_POL_MEM_DETAILS_ENDO SET BASE_AMOUNT_USD=" + usdArr[i] + " WHERE POL_NO='" + clsPol.REFNO + "' AND SEQ_NO=" + clsPol.SEQ_NO + " AND MEM_ID='" + GT_mem[i].member_id + "' AND UPD_TYPE='"+ clsPol.UPD_TYPE+ "'";

                                    cmd.CommandText = upMembers;

                                    cmd.ExecuteNonQuery();
                                }


                                cmd.CommandText = insertEndor;
                                cmd.ExecuteNonQuery();

                                cmd.Parameters.Clear();

                                trans.Commit();
                                returnValue = true;
                            }


                        }
                    }
                    else
                    {
                        string maxSeq = "SELECT NVL(MAX(SEQ_NO),0) AS CURRENT_MAX FROM SLIGEN.TRV_POL_PRM_ENDO WHERE POLNO = '" + clsPol.REFNO + "' AND UPD_TYPE = '" + clsPol.UPD_TYPE + "'";
                        cmd.CommandText = maxSeq;


                        OracleDataReader reader_2 = cmd.ExecuteReader();
                        cmd.Parameters.Clear();

                        while (reader_2.Read())
                        {
                            if (!reader_2.IsDBNull(0))
                            {
                                CURRENT_MAX = reader_2.GetInt32(0);
                            }
                        }

                        MAX_SEQ = CURRENT_MAX + 1;

                        string keepPolMastBkp = "insert into SLIGEN.TRV_POL_MAST_HIST (POLNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4,TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER,HOME_NUMBER,OFFICE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,SER_BRCD,VAT_WAVED,VAT_NO,REF_NO,SCH_PRINT,SCH_PRINTBY,SCH_PDATE,SCH_PIP,ENT_BRANCH,SCH_BRANCH,EDIT_TAG) " +
                        " select POLNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4,TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER,HOME_NUMBER,OFFICE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,SER_BRCD,VAT_WAVED,VAT_NO,REF_NO,SCH_PRINT,SCH_PRINTBY,SCH_PDATE,SCH_PIP,ENT_BRANCH,SCH_BRANCH,'" + clsPol.UPD_TYPE + "' " +
                        " from SLIGEN.TRV_POL_MAST where POLNO = '" + clsPol.REFNO + "'";

                        cmd.CommandText = keepPolMastBkp;
                        cmd.ExecuteNonQuery();

                        clsPol.SCH_PRINT = 0;
                        string insertEndor = "insert into sligen.TRV_POL_PRM_ENDO (POLNO,SEQ_NO,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,SLI_NO1,SLI_NO2,SER_BRCD,ENT_BRANCH,UPD_TYPE,SCH_PRINT,SCH_BRANCH,PREVIOUS_DEP,PREVIOUS_ARR,PREVIOUS_DAYS,NEW_DEP,NEW_ARR,PLAN,CLIENT_ID,FULL_NAME,TRV_TYPE,LOADING_RATE) values ('" + clsPol.REFNO + "'," + MAX_SEQ + "," + clsPol.NUM_OF_PERSONS + "," + clsPol.GROUP_DISC_RATE + "," + clsPol.NET_PREMIUM_USD + "," + clsPol.ADMIN_FEE_RS + "," + clsPol.POLICY_FEE_RS + "," + clsPol.NBT_RS + "," + clsPol.VAT_RS + "," + clsPol.FINAL_PREMIUM_RS + "," + clsPol.TAXES_EXPENSES_RS + ",SYSDATE," + clsPol.NET_PREMIUM_RS + "," + clsPol.NO_OF_DAYS + "," + clsPol.SUM_INS_USD + "," + clsPol.USD_RATE + "," + clsPol.FINAL_PREMIUM_USD + ",'" + clsPol.IP_ADDRESS + "','" + clsPol.ENTERED_BY + "'," + clsPol.AGENT_CODE + "," + clsPol.DEBTOR_CODE + "," + clsPol.SLI_NO1 + "," + clsPol.SLI_NO2 + "," + clsPol.SER_BRCD + "," + clsPol.BRANCH + ",'" + clsPol.UPD_TYPE + "'," + clsPol.SCH_PRINT + "," + clsPol.SCH_BRANCH + ",TO_DATE('" + clsPol.PREVIOUS_DEP + "', 'yyyy/MM/dd'),TO_DATE('" + clsPol.PREVIOUS_ARR + "', 'yyyy/MM/dd')," + clsPol.PREVIOUS_DAYS + ",TO_DATE('" + clsPol.NEW_DEP + "', 'yyyy/MM/dd'),TO_DATE('" + clsPol.NEW_ARR + "', 'yyyy/MM/dd'),'" + clsPol.PLAN + "','" + clsPol.CLIENT_ID + "','" + clsPol.FULL_NAME + "','" + clsPol.TRV_TYPE + "'," + clsPol.LOADING_RATE + ")";


                        string insSql = "insert into SLIGEN.trv_pol_mem_details_his (pol_no, mem_id, mem_type,GENDER,DOB,AGE,NAME,PP_NO,TITLE," +
                               "  BASE_AMOUNT_USD,ENTERED_DATE) (SELECT pol_no, mem_id, mem_type, GENDER, DOB, AGE, NAME, PP_NO, TITLE," +
                               " BASE_AMOUNT_USD, ENTERED_DATE FROM SLIGEN.trv_pol_mem_details where pol_no = '" + clsPol.REFNO + "') ";

                        cmd.CommandText = insSql;

                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < GT_mem.Count; i++)
                        {

                            //foreach (var mem in GT_mem)
                            //{
                            string insMember = "INSERT INTO SLIGEN.TRV_POL_MEM_DETAILS_ENDO (POL_NO,MEM_TYPE,GENDER,DOB,AGE,TITLE,NAME,PP_NO,MEM_ID,BASE_AMOUNT_USD,ENTERED_DATE,SEQ_NO,ENTERED_BY,UPD_TYPE)" +
                                " VALUES('" + clsPol.REFNO + "','" + GT_mem[i].member_type + "','" + GT_mem[i].gender + "'," +
                                "to_date('" + GT_mem[i].dob + "','yyyy/mm/dd')," + GT_mem[i].age + "" +
                                ",'" + GT_mem[i].title + "','" + PrepareApostrophe(GT_mem[i].name) + "'" +
                                ",'" + GT_mem[i].ppno + "','" + GT_mem[i].member_id + "'" +
                                "," + usdArr[i] + ",sysdate," + MAX_SEQ + ",'" + clsPol.ENTERED_BY + "','"+ clsPol.UPD_TYPE+ "')";

                            cmd.CommandText = insMember;

                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = insertEndor;
                        cmd.ExecuteNonQuery();


                        cmd.Parameters.Clear();

                        trans.Commit();
                        returnValue = true;


                    }


                }

                if(clsPol.UPD_TYPE == "S")
                {
                    //string updateClmMast = "";
                    int CURRENT_MAX = 0;
                    int MAX_SEQ = 0;
                    //string fullName = "";
                    //string comName = "";


                    //DateTime newDt = DateTime.ParseExact(clsPol.NEW_DEP, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    //DateTime newArr = DateTime.ParseExact(clsPol.NEW_ARR, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    //DateTime currDt = DateTime.ParseExact(clsMast.departDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    //DateTime currArr = DateTime.ParseExact(clsMast.returnDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                    if (clsPol.End_type == "E")
                    {
                        //if (newDt.Date == currDt.Date && newArr.Date == currArr.Date)
                        //{



                        ////////////if (clsPol.SCH_PRINT == 0)
                        ////////////{

                        ////////////    string maxSeq = "SELECT NVL(MAX(SEQ_NO),0) AS CURRENT_MAX FROM SLIGEN.TRV_POL_PRM_ENDO WHERE POLNO = '" + clsPol.REFNO + "' AND UPD_TYPE = '" + clsPol.UPD_TYPE + "'";
                        ////////////    cmd.CommandText = maxSeq;


                        ////////////    OracleDataReader reader_2 = cmd.ExecuteReader();
                        ////////////    cmd.Parameters.Clear();

                        ////////////    while (reader_2.Read())
                        ////////////    {
                        ////////////        if (!reader_2.IsDBNull(0))
                        ////////////        {
                        ////////////            CURRENT_MAX = reader_2.GetInt32(0);
                        ////////////        }
                        ////////////    }

                        ////////////    MAX_SEQ = CURRENT_MAX + 1;

                        ////////////    string keepPolMastBkp = "insert into SLIGEN.TRV_POL_MAST_HIST (POLNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4,TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER,HOME_NUMBER,OFFICE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,SER_BRCD,VAT_WAVED,VAT_NO,REF_NO,SCH_PRINT,SCH_PRINTBY,SCH_PDATE,SCH_PIP,ENT_BRANCH,SCH_BRANCH,EDIT_TAG) " +
                        ////////////    " select POLNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4,TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER,HOME_NUMBER,OFFICE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,SER_BRCD,VAT_WAVED,VAT_NO,REF_NO,SCH_PRINT,SCH_PRINTBY,SCH_PDATE,SCH_PIP,ENT_BRANCH,SCH_BRANCH,'" + clsPol.UPD_TYPE + "' " +
                        ////////////    " from SLIGEN.TRV_POL_MAST where POLNO = '" + clsPol.REFNO + "'";

                        ////////////    cmd.CommandText = keepPolMastBkp;
                        ////////////    cmd.ExecuteNonQuery();

                        ////////////    clsPol.SCH_PRINT = 0;

                        ////////////    string insertEndor = "insert into sligen.TRV_POL_PRM_ENDO (POLNO,SEQ_NO,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,SLI_NO1,SLI_NO2,SER_BRCD,ENT_BRANCH,UPD_TYPE,SCH_PRINT,SCH_BRANCH,PLAN,CLIENT_ID,FULL_NAME,TRV_TYPE,NEW_DESTINATION,NEW_PLAN,NEW_SUM_INS_USD) values ('" + clsPol.REFNO + "'," + MAX_SEQ + "," + clsPol.NUM_OF_PERSONS + "," + clsPol.GROUP_DISC_RATE + "," + clsPol.NET_PREMIUM_USD + "," + clsPol.ADMIN_FEE_RS + "," + clsPol.POLICY_FEE_RS + "," + clsPol.NBT_RS + "," + clsPol.VAT_RS + "," + clsPol.FINAL_PREMIUM_RS + "," + clsPol.TAXES_EXPENSES_RS + ",SYSDATE," + clsPol.NET_PREMIUM_RS + "," + clsPol.NO_OF_DAYS + "," + clsPol.SUM_INS_USD + "," + clsPol.USD_RATE + "," + clsPol.FINAL_PREMIUM_USD + ",'" + clsPol.IP_ADDRESS + "','" + clsPol.ENTERED_BY + "'," + clsPol.AGENT_CODE + "," + clsPol.DEBTOR_CODE + "," + clsPol.SLI_NO1 + "," + clsPol.SLI_NO2 + "," + clsPol.SER_BRCD + "," + clsPol.BRANCH + ",'" + clsPol.UPD_TYPE + "'," + clsPol.SCH_PRINT + "," + clsPol.SCH_BRANCH + ",'" + clsPol.PLAN + "','" + clsPol.CLIENT_ID + "','" + clsPol.FULL_NAME + "','" + clsPol.TRV_TYPE + "','" + clsPol.NEW_DESTINATION + "','" + clsPol.NEW_PLAN + "'," + clsPol.NEW_SUM_INS_USD + ")";

                        ////////////    //string insSql = "insert into SLIGEN.trv_pol_mem_details_his (pol_no, mem_id, mem_type,GENDER,DOB,AGE,NAME,PP_NO,TITLE," +
                        ////////////    //    "  BASE_AMOUNT_USD,ENTERED_DATE) (SELECT pol_no, mem_id, mem_type, GENDER, DOB, AGE, NAME, PP_NO, TITLE," +
                        ////////////    //    " BASE_AMOUNT_USD, ENTERED_DATE FROM SLIGEN.trv_pol_mem_details where pol_no = '" + clsPol.REFNO + "') ";

                        ////////////    //cmd.CommandText = insSql;

                        ////////////    //cmd.ExecuteNonQuery();

                        ////////////    //for (int i = 0; i < GT_mem.Count; i++)
                        ////////////    //{

                        ////////////    //    //foreach (var mem in GT_mem)
                        ////////////    //    //{
                        ////////////    //    string insMember = "INSERT INTO SLIGEN.TRV_POL_MEM_DETAILS_ENDO (POL_NO,MEM_TYPE,GENDER,DOB,AGE,TITLE,NAME,PP_NO,MEM_ID,BASE_AMOUNT_USD,ENTERED_DATE,SEQ_NO,ENTERED_BY)" +
                        ////////////    //        " VALUES('" + clsPol.REFNO + "','" + GT_mem[i].member_type + "','" + GT_mem[i].gender + "'," +
                        ////////////    //        "to_date('" + GT_mem[i].dob + "','yyyy/mm/dd')," + GT_mem[i].age + "" +
                        ////////////    //        ",'" + GT_mem[i].title + "','" + PrepareApostrophe(GT_mem[i].name) + "'" +
                        ////////////    //        ",'" + GT_mem[i].ppno + "','" + GT_mem[i].member_id + "'" +
                        ////////////    //        "," + usdArr[i] + ",sysdate," + MAX_SEQ + ",'" + clsPol.ENTERED_BY + "')";

                        ////////////    //    cmd.CommandText = insMember;

                        ////////////    //    cmd.ExecuteNonQuery();
                        ////////////    //}

                        ////////////    cmd.CommandText = insertEndor;
                        ////////////    cmd.ExecuteNonQuery();


                        ////////////    cmd.Parameters.Clear();

                        ////////////    trans.Commit();
                        ////////////    returnValue = true;
                        ////////////}
                      


                        //}

                        //else
                        //{
                            if (clsPol.SCH_PRINT == 1)
                            {

                                string maxSeq = "SELECT NVL(MAX(SEQ_NO),0) AS CURRENT_MAX FROM SLIGEN.TRV_POL_PRM_ENDO WHERE POLNO = '" + clsPol.REFNO + "' AND UPD_TYPE = '" + clsPol.UPD_TYPE + "'";
                                cmd.CommandText = maxSeq;


                                OracleDataReader reader_2 = cmd.ExecuteReader();
                                cmd.Parameters.Clear();

                                while (reader_2.Read())
                                {
                                    if (!reader_2.IsDBNull(0))
                                    {
                                        CURRENT_MAX = reader_2.GetInt32(0);
                                    }
                                }

                                MAX_SEQ = CURRENT_MAX + 1;

                                string keepPolMastBkp = "insert into SLIGEN.TRV_POL_MAST_HIST (POLNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4,TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER,HOME_NUMBER,OFFICE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,SER_BRCD,VAT_WAVED,VAT_NO,REF_NO,SCH_PRINT,SCH_PRINTBY,SCH_PDATE,SCH_PIP,ENT_BRANCH,SCH_BRANCH,EDIT_TAG) " +
                                " select POLNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4,TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER,HOME_NUMBER,OFFICE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,SER_BRCD,VAT_WAVED,VAT_NO,REF_NO,SCH_PRINT,SCH_PRINTBY,SCH_PDATE,SCH_PIP,ENT_BRANCH,SCH_BRANCH,'" + clsPol.UPD_TYPE + "' " +
                                " from SLIGEN.TRV_POL_MAST where POLNO = '" + clsPol.REFNO + "'";

                                cmd.CommandText = keepPolMastBkp;
                                cmd.ExecuteNonQuery();

                                clsPol.SCH_PRINT = 0;

                                string insertEndor = "insert into sligen.TRV_POL_PRM_ENDO (POLNO,SEQ_NO,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,SLI_NO1,SLI_NO2,SER_BRCD,ENT_BRANCH,UPD_TYPE,SCH_PRINT,SCH_BRANCH,PLAN,CLIENT_ID,FULL_NAME,TRV_TYPE,NEW_DESTINATION,NEW_PLAN,NEW_SUM_INS_USD,PREVIOUS_DAYS,DESTINATION) values ('" + clsPol.REFNO + "'," + MAX_SEQ + "," + clsPol.NUM_OF_PERSONS + "," + clsPol.GROUP_DISC_RATE + "," + clsPol.NET_PREMIUM_USD + "," + clsPol.ADMIN_FEE_RS + "," + clsPol.POLICY_FEE_RS + "," + clsPol.NBT_RS + "," + clsPol.VAT_RS + "," + clsPol.FINAL_PREMIUM_RS + "," + clsPol.TAXES_EXPENSES_RS + ",SYSDATE," + clsPol.NET_PREMIUM_RS + "," + clsPol.NO_OF_DAYS + "," + clsPol.SUM_INS_USD + "," + clsPol.USD_RATE + "," + clsPol.FINAL_PREMIUM_USD + ",'" + clsPol.IP_ADDRESS + "','" + clsPol.ENTERED_BY + "'," + clsPol.AGENT_CODE + "," + clsPol.DEBTOR_CODE + "," + clsPol.SLI_NO1 + "," + clsPol.SLI_NO2 + "," + clsPol.SER_BRCD + "," + clsPol.BRANCH + ",'" + clsPol.UPD_TYPE + "'," + clsPol.SCH_PRINT + "," + clsPol.SCH_BRANCH + ",'" + clsPol.PLAN + "','" + clsPol.CLIENT_ID + "','" + clsPol.FULL_NAME + "','" + clsPol.TRV_TYPE + "','"+clsPol.NEW_DESTINATION+"','"+clsPol.NEW_PLAN+"',"+clsPol.NEW_SUM_INS_USD+","+clsPol.PREVIOUS_DAYS+",'"+clsPol.DESTINATION+"')";

                            string insSql = "insert into SLIGEN.trv_pol_mem_details_his (pol_no, mem_id, mem_type,GENDER,DOB,AGE,NAME,PP_NO,TITLE," +
                                "  BASE_AMOUNT_USD,ENTERED_DATE) (SELECT pol_no, mem_id, mem_type, GENDER, DOB, AGE, NAME, PP_NO, TITLE," +
                                " BASE_AMOUNT_USD, ENTERED_DATE FROM SLIGEN.trv_pol_mem_details where pol_no = '" + clsPol.REFNO + "') ";

                            cmd.CommandText = insSql;

                            cmd.ExecuteNonQuery();

                            for (int i = 0; i < GT_mem.Count; i++)
                            {

                                //foreach (var mem in GT_mem)
                                //{
                                string insMember = "INSERT INTO SLIGEN.TRV_POL_MEM_DETAILS_ENDO (POL_NO,MEM_TYPE,GENDER,DOB,AGE,TITLE,NAME,PP_NO,MEM_ID,BASE_AMOUNT_USD,ENTERED_DATE,SEQ_NO,ENTERED_BY,UPD_TYPE)" +
                                    " VALUES('" + clsPol.REFNO + "','" + GT_mem[i].member_type + "','" + GT_mem[i].gender + "'," +
                                    "to_date('" + GT_mem[i].dob + "','yyyy/mm/dd')," + GT_mem[i].age + "" +
                                    ",'" + GT_mem[i].title + "','" + PrepareApostrophe(GT_mem[i].name) + "'" +
                                    ",'" + GT_mem[i].ppno + "','" + GT_mem[i].member_id + "'" +
                                    "," + usdArr[i] + ",sysdate," + MAX_SEQ + ",'" + clsPol.ENTERED_BY + "','"+ clsPol.UPD_TYPE+ "')";

                                cmd.CommandText = insMember;

                                cmd.ExecuteNonQuery();
                            }

                            cmd.CommandText = insertEndor;
                                cmd.ExecuteNonQuery();


                                cmd.Parameters.Clear();

                                trans.Commit();
                                returnValue = true;
                            }
                            else
                            {
                               // string insSql = "insert into SLIGEN.trv_pol_mem_details_his (pol_no, mem_id, mem_type,GENDER,DOB,AGE,NAME,PP_NO,TITLE," +
                               //"  BASE_AMOUNT_USD,ENTERED_DATE) (SELECT pol_no, mem_id, mem_type, GENDER, DOB, AGE, NAME, PP_NO, TITLE," +
                               //" BASE_AMOUNT_USD, ENTERED_DATE FROM SLIGEN.trv_pol_mem_details where pol_no = '" + clsPol.REFNO + "') ";

                               // cmd.CommandText = insSql;

                               // cmd.ExecuteNonQuery();

                                string insertEndor = "update SLIGEN.TRV_POL_PRM_ENDO " +
                                            " set NET_PREMIUM_USD=" + clsPol.NET_PREMIUM_USD + ",FINAL_PREMIUM_USD=" + clsPol.FINAL_PREMIUM_USD + ",ADMIN_FEE_RS=" + clsPol.ADMIN_FEE_RS + ",POLICY_FEE_RS=" + clsPol.POLICY_FEE_RS + ",NBT_RS=" + clsPol.NBT_RS + ",VAT_RS=" + clsPol.VAT_RS + ",FINAL_PREMIUM_RS=" + clsPol.FINAL_PREMIUM_RS + ",NET_PREMIUM_RS=" + clsPol.NET_PREMIUM_RS + ",PLAN='"+ clsPol.PLAN+ "',NEW_PLAN='"+ clsPol.NEW_PLAN+ "',DESTINATION='"+ clsPol.DESTINATION+ "',NEW_DESTINATION='"+ clsPol.NEW_DESTINATION+ "',SUM_INS_USD="+ clsPol.SUM_INS_USD+ ",NEW_SUM_INS_USD="+ clsPol.NEW_SUM_INS_USD + "" +
                                            " where POLNO='" + clsPol.REFNO + "' and SEQ_NO=" + clsPol.SEQ_NO + " AND UPD_TYPE = '"+clsPol.UPD_TYPE+"'";
                            // "(POLNO,SEQ_NO,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,SLI_NO1,SLI_NO2,SER_BRCD,ENT_BRANCH,UPD_TYPE,SCH_PRINT,SCH_BRANCH,PREVIOUS_DEP,PREVIOUS_ARR,PREVIOUS_DAYS,NEW_DEP,NEW_ARR) values ('" + clsPol.REFNO + "'," + MAX_SEQ + "," + clsPol.NUM_OF_PERSONS + "," + clsPol.GROUP_DISC_RATE + "," + clsPol.NET_PREMIUM_USD + "," + clsPol.ADMIN_FEE_RS + "," + clsPol.POLICY_FEE_RS + "," + clsPol.NBT_RS + "," + clsPol.VAT_RS + "," + clsPol.FINAL_PREMIUM_RS + "," + clsPol.TAXES_EXPENSES_RS + ",SYSDATE," + clsPol.NET_PREMIUM_RS + "," + clsPol.NO_OF_DAYS + "," + clsPol.SUM_INS_USD + "," + clsPol.USD_RATE + "," + clsPol.FINAL_PREMIUM_USD + ",'" + clsPol.IP_ADDRESS + "','" + clsPol.ENTERED_BY + "'," + clsPol.AGENT_CODE + "," + clsPol.DEBTOR_CODE + "," + clsPol.SLI_NO1 + "," + clsPol.SLI_NO2 + "," + clsPol.SER_BRCD + "," + clsPol.BRANCH + ",'" + clsPol.UPD_TYPE + "'," + clsPol.SCH_PRINT + "," + clsPol.SCH_BRANCH + ",TO_DATE('" + clsPol.PREVIOUS_DEP + "', 'yyyy/MM/dd'),TO_DATE('" + clsPol.PREVIOUS_ARR + "', 'yyyy/MM/dd')," + clsPol.PREVIOUS_DAYS + ",TO_DATE('" + clsPol.NEW_DEP + "', 'yyyy/MM/dd'),TO_DATE('" + clsPol.NEW_ARR + "', 'yyyy/MM/dd'))";

                            for (int i = 0; i < GT_mem.Count; i++)
                            {
                                string upMembers = "UPDATE SLIGEN.TRV_POL_MEM_DETAILS_ENDO SET BASE_AMOUNT_USD=" + usdArr[i] + " WHERE POL_NO='" + clsPol.REFNO + "' AND SEQ_NO=" + clsPol.SEQ_NO + " AND MEM_ID='" + GT_mem[i].member_id + "' AND UPD_TYPE='"+ clsPol.UPD_TYPE+ "'";

                                cmd.CommandText = upMembers;

                                cmd.ExecuteNonQuery();
                            }


                            cmd.CommandText = insertEndor;
                            cmd.ExecuteNonQuery();

                            cmd.Parameters.Clear();

                                trans.Commit();
                                returnValue = true;
                            }


                        //}
                    }
                    else
                    {
                        string maxSeq = "SELECT NVL(MAX(SEQ_NO),0) AS CURRENT_MAX FROM SLIGEN.TRV_POL_PRM_ENDO WHERE POLNO = '" + clsPol.REFNO + "' AND UPD_TYPE = '" + clsPol.UPD_TYPE + "'";
                        cmd.CommandText = maxSeq;


                        OracleDataReader reader_2 = cmd.ExecuteReader();
                        cmd.Parameters.Clear();

                        while (reader_2.Read())
                        {
                            if (!reader_2.IsDBNull(0))
                            {
                                CURRENT_MAX = reader_2.GetInt32(0);
                            }
                        }

                        MAX_SEQ = CURRENT_MAX + 1;

                        string keepPolMastBkp = "insert into SLIGEN.TRV_POL_MAST_HIST (POLNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4,TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER,HOME_NUMBER,OFFICE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,SER_BRCD,VAT_WAVED,VAT_NO,REF_NO,SCH_PRINT,SCH_PRINTBY,SCH_PDATE,SCH_PIP,ENT_BRANCH,SCH_BRANCH,EDIT_TAG) " +
                        " select POLNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4,TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER,HOME_NUMBER,OFFICE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,SER_BRCD,VAT_WAVED,VAT_NO,REF_NO,SCH_PRINT,SCH_PRINTBY,SCH_PDATE,SCH_PIP,ENT_BRANCH,SCH_BRANCH,'" + clsPol.UPD_TYPE + "' " +
                        " from SLIGEN.TRV_POL_MAST where POLNO = '" + clsPol.REFNO + "'";

                        cmd.CommandText = keepPolMastBkp;
                        cmd.ExecuteNonQuery();

                        clsPol.SCH_PRINT = 0;
                        string insertEndor = "insert into sligen.TRV_POL_PRM_ENDO (POLNO,SEQ_NO,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,SLI_NO1,SLI_NO2,SER_BRCD,ENT_BRANCH,UPD_TYPE,SCH_PRINT,SCH_BRANCH,PLAN,CLIENT_ID,FULL_NAME,TRV_TYPE,NEW_DESTINATION,NEW_PLAN,NEW_SUM_INS_USD,PREVIOUS_DAYS,DESTINATION) values ('" + clsPol.REFNO + "'," + MAX_SEQ + "," + clsPol.NUM_OF_PERSONS + "," + clsPol.GROUP_DISC_RATE + "," + clsPol.NET_PREMIUM_USD + "," + clsPol.ADMIN_FEE_RS + "," + clsPol.POLICY_FEE_RS + "," + clsPol.NBT_RS + "," + clsPol.VAT_RS + "," + clsPol.FINAL_PREMIUM_RS + "," + clsPol.TAXES_EXPENSES_RS + ",SYSDATE," + clsPol.NET_PREMIUM_RS + "," + clsPol.NO_OF_DAYS + "," + clsPol.SUM_INS_USD + "," + clsPol.USD_RATE + "," + clsPol.FINAL_PREMIUM_USD + ",'" + clsPol.IP_ADDRESS + "','" + clsPol.ENTERED_BY + "'," + clsPol.AGENT_CODE + "," + clsPol.DEBTOR_CODE + "," + clsPol.SLI_NO1 + "," + clsPol.SLI_NO2 + "," + clsPol.SER_BRCD + "," + clsPol.BRANCH + ",'" + clsPol.UPD_TYPE + "'," + clsPol.SCH_PRINT + "," + clsPol.SCH_BRANCH + ",'" + clsPol.PLAN + "','" + clsPol.CLIENT_ID + "','" + clsPol.FULL_NAME + "','" + clsPol.TRV_TYPE + "','" + clsPol.NEW_DESTINATION + "','" + clsPol.NEW_PLAN + "'," + clsPol.NEW_SUM_INS_USD + ","+ clsPol.PREVIOUS_DAYS + ",'"+clsPol.DESTINATION + "')";


                        string insSql = "insert into SLIGEN.trv_pol_mem_details_his (pol_no, mem_id, mem_type,GENDER,DOB,AGE,NAME,PP_NO,TITLE," +
                               "  BASE_AMOUNT_USD,ENTERED_DATE) (SELECT pol_no, mem_id, mem_type, GENDER, DOB, AGE, NAME, PP_NO, TITLE," +
                               " BASE_AMOUNT_USD, ENTERED_DATE FROM SLIGEN.trv_pol_mem_details where pol_no = '" + clsPol.REFNO + "') ";

                        cmd.CommandText = insSql;

                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < GT_mem.Count; i++)
                        {

                            //foreach (var mem in GT_mem)
                            //{
                            string insMember = "INSERT INTO SLIGEN.TRV_POL_MEM_DETAILS_ENDO (POL_NO,MEM_TYPE,GENDER,DOB,AGE,TITLE,NAME,PP_NO,MEM_ID,BASE_AMOUNT_USD,ENTERED_DATE,SEQ_NO,ENTERED_BY,UPD_TYPE)" +
                                " VALUES('" + clsPol.REFNO + "','" + GT_mem[i].member_type + "','" + GT_mem[i].gender + "'," +
                                "to_date('" + GT_mem[i].dob + "','yyyy/mm/dd')," + GT_mem[i].age + "" +
                                ",'" + GT_mem[i].title + "','" + PrepareApostrophe(GT_mem[i].name) + "'" +
                                ",'" + GT_mem[i].ppno + "','" + GT_mem[i].member_id + "'" +
                                "," + usdArr[i] + ",sysdate," + MAX_SEQ + ",'" + clsPol.ENTERED_BY + "','"+ clsPol.UPD_TYPE + "')";

                            cmd.CommandText = insMember;

                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = insertEndor;
                        cmd.ExecuteNonQuery();


                        cmd.Parameters.Clear();

                        trans.Commit();
                        returnValue = true;


                    }
                }
            }


        }
        catch (Exception e)
        {
            trans.Rollback();
        }
        finally
        {
            disconnectDB();
        }

        return returnValue;
    }

    public string PrepareApostrophe(string str)
    {
        string newStr = "";
        bool pZero = false, pEnd = false, pMiddle = false;

        if (str.IndexOf("'") < 0) return str;


        pZero = str.IndexOf("'") == 0 ? true : false;
        pEnd = str.LastIndexOf("'") + 1 == str.Length ? true : false;
        pMiddle = str.Substring(1, str.Length - 2).LastIndexOf("'") > 0 ? true : false;


        newStr = pZero && pMiddle && pEnd ? "'||chr(39)||'" + str.Substring(1, str.Length - 2).Replace("'", "'||chr(39)||'") + "'||chr(39)|| '"
            : !pZero && pMiddle && pEnd ? str.Substring(0, (str.Length - 1)).Replace("'", "'||chr(39)||'") + "'||chr(39)|| '"
            : pZero && !pMiddle && pEnd ? "'||chr(39)||'" + str.Substring(1, str.Length - 2) + "'||chr(39)|| '"
            : pZero && pMiddle && !pEnd ? "'||chr(39)||'" + str.Substring(1, str.Length - 1).Replace("'", "'||chr(39)||'")
            : pZero && !pMiddle && !pEnd ? "'||chr(39)||'" + str.Substring(1, str.Length - 1)
            : !pZero && !pMiddle && pEnd ? str.Substring(0, str.Length - 1) + "'||chr(39)|| '"
            : !pZero && pMiddle && !pEnd ? str.Substring(0, str.Length).Replace("'", "'||chr(39)||'")
            : str;

        return newStr;
    }
    public bool UpdateVatSvatAmt(TRV_Premium_Endorsement preEnd)
    {
        bool result = false;

        try
        {
            //using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]))
            //{
            connectDB();
            OracleCommand cmd = objOraCon.CreateCommand();
            OracleTransaction trans = objOraCon.BeginTransaction();
            cmd.Transaction = trans;
            try
                {

                TRV_Policy_mast polMast = new TRV_Policy_mast(preEnd.REFNO,"TPI");

                    string sql = "UPDATE SLIGEN.TRV_POL_PRM_ENDO " +
                                 " set VAT_RS=" + preEnd.VAT_RS + ", SVAT_AMT=" + preEnd.SVAT_AMT + ",VAT_NO= '" + preEnd.VAT_NO+ "', SVAT_NO='" + preEnd.SVAT_NO+ "', NET_PREMIUM_RS=" + preEnd.NET_PREMIUM_RS+ ", ADMIN_FEE_RS=" + preEnd.ADMIN_FEE_RS+ ", POLICY_FEE_RS=" + preEnd.POLICY_FEE_RS + ", NBT_RS=" + preEnd.NBT_RS+ ",FINAL_PREMIUM_RS=" + preEnd.FINAL_PREMIUM_RS+ " ,VAT_WAVED='"+ preEnd.VAT_WAVED+ "' where POLNO='" + preEnd.REFNO+ "' and SEQ_NO="+ preEnd.SEQ_NO+ " and UPD_TYPE='"+preEnd.UPD_TYPE+"'";

                    cmd.CommandText = sql;
                
                    cmd.ExecuteNonQuery();


                if (preEnd.VatWVValue == "1")
                {
                    DataManager dm = new DataManager();
                    string vatWVState = "select CUSTOMER_ID from CLIENTDB.CUSTOMER_WAVED_DATA where CUSTOMER_ID='" + preEnd.CLIENT_ID + "'";

                    if (dm.existRecored(vatWVState) > 0)
                    {

                    }
                    else
                    { 
                        string insvtwv = "INSERT INTO CLIENTDB.CUSTOMER_WAVED_DATA (CUSTOMER_ID, IS_VAT_WAVED, IS_ADMIN_FEE_WAVED, ENTERED_USER_ID, ENTERED_DATE)" +
                                    " VALUES     (" + long.Parse(preEnd.CLIENT_ID) + "," + preEnd.VatWVValue + ",0,'" + preEnd.ENTERED_BY + "',sysdate)";
                        cmd.CommandText = insvtwv;

                        cmd.ExecuteNonQuery();
                    }

                    dm.connclose();
                }

                if (preEnd.UPD_TYPE == "P")
                {

                    string upMomas = "UPDATE GENPAY.MOMAS SET FMDCO = to_date('" + preEnd.NEW_DEP + "', 'yyyy/mm/dd')," +
                    " FMSUM = " + preEnd.SummRs + " ,FMPRM = " + preEnd.NET_PREMIUM_RS + ",FMVAT =" + preEnd.VAT_RS + "," +
                    " FMCES = " + preEnd.ADMIN_FEE_RS + ",FMPOF = " + preEnd.POLICY_FEE_RS + ",FMDST = to_date('" + preEnd.NEW_DEP + "', 'yyyy/mm/dd')," +
                    " FMDEX = to_date('" + preEnd.NEW_ARR + "', 'yyyy/mm/dd'),FMPDT = sysdate,FMNBL = " + preEnd.NBT_RS + "," +
                    " VAT_REG_NO = '" + preEnd.VAT_NO + "',SVAT_REG_NO = '" + preEnd.SVAT_NO + "',SVAT_AMOUNT = " + preEnd.SVAT_AMT + "" +
                    " WHERE FMPOL = '" + preEnd.REFNO + "' AND FMDEPT = 'G'";

                    cmd.CommandText = upMomas;

                    cmd.ExecuteNonQuery();

                }

                if (preEnd.UPD_TYPE == "S")
                {

                    string upMomas = "UPDATE GENPAY.MOMAS SET FMDCO = to_date('" + polMast.departDate + "', 'yyyy/mm/dd')," +
                    " FMSUM = " + preEnd.SummRs + " ,FMPRM = " + preEnd.NET_PREMIUM_RS + ",FMVAT =" + preEnd.VAT_RS + "," +
                    " FMCES = " + preEnd.ADMIN_FEE_RS + ",FMPOF = " + preEnd.POLICY_FEE_RS + ",FMDST = to_date('" + polMast.departDate + "', 'yyyy/mm/dd')," +
                    " FMDEX = to_date('" + polMast.returnDate + "', 'yyyy/mm/dd'),FMPDT = sysdate,FMNBL = " + preEnd.NBT_RS + "," +
                    " VAT_REG_NO = '" + preEnd.VAT_NO + "',SVAT_REG_NO = '" + preEnd.SVAT_NO + "',SVAT_AMOUNT = " + preEnd.SVAT_AMT + "" +
                    " WHERE FMPOL = '" + preEnd.REFNO + "' AND FMDEPT = 'G'";

                    cmd.CommandText = upMomas;

                    cmd.ExecuteNonQuery();

                }

                trans.Commit();
                result = true;

                }
                catch (Exception u)
                {
                    trans.Rollback();
                    log logger = new log();
                    logger.write_log("Failed at updating TRV_POL_PRM_ENDO " + u.ToString());
                }

            disconnectDB();
            //}
            //}
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

    //public bool Insert_rec(Cls_Premium_Endorsement proposal)
    //{
    //    bool result = false;

    //    try
    //    {
    //        //Proposal prop = new Proposal();
    //        //refID = prop.generate_proposalID("G", Convert.ToInt32(DateTime.Today.ToString("yyyy")), "GTI");

    //        //if (!String.IsNullOrEmpty(refID))
    //        //{
    //        using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]))
    //        {
    //            connection.Open();

    //            OracleCommand command = connection.CreateCommand();
    //            OracleTransaction transaction;

    //            transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
    //            command.Transaction = transaction;
    //            try
    //            {
    //                string insSql = "INSERT INTO SLIGEN.TRV_PROP_MAST (REFNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3," +
    //                               "VISIT_CTRY4,TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD," +
    //                               "ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS,FINAL_PREMIUM_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE," +
    //                               "FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,AGNT_RATE_CODE,SLI_NO1,SLI_NO2,CLIENT_ID,BRANCH)" +
    //                               "VALUES('" + proposal.REFNO + "','" + proposal.DESTINATION + "','" + proposal.DEPART_DATE + "','" + proposal.RETURN_DATE + "'," +
    //                               "'" + proposal.VISIT_CTRY1 + "','" + proposal.VISIT_CTRY2 + "','" + proposal.VISIT_CTRY3 + "'," +
    //                               "'" + proposal.VISIT_CTRY4 +  "','" + proposal.TRAVEL_PURPOSE+ "','" + proposal.CONTCT_NAME + "','" + proposal.CONTCT_ADRS1 + "','" + proposal.CONTCT_ADRS2 + "','" + proposal.CONTCT_ADRS3 + "','" + proposal.CONTCT_ADRS4 + "','" + proposal.CONTCT_NO1 +"','" + proposal.CONTCT_NO2 + "','" + proposal.TRV_TYPE + "','" + proposal.FULL_NAME + "','" + proposal.ADDRESS1 + "'," +
    //                               "'" + proposal.ADDRESS2 + "','" + proposal.ADDRESS3 + "','" + proposal.ADDRESS4 + "','" + proposal.MOBILE_NUMBER + "','" + proposal.PLAN + "','" + proposal.TITLE + "','" + proposal.NUM_OF_PERSONS + "'," +
    //                               "" + proposal.GROUP_DISC_RATE + "," + proposal.NET_PREMIUM_USD + "," +
    //                               "" + proposal.ADMIN_FEE_RS + "," + proposal.POLICY_FEE_RS + "," + proposal.NBT_RS + "," + proposal.VAT_RS + "," + proposal.FINAL_PREMIUM_RS + "," +
    //                               " sysdate," + proposal.NET_PREMIUM_RS + "," + proposal.NO_OF_DAYS + "," + proposal.SUM_INS_USD + "," + proposal.USD_RATE + "," +
    //                               "" + proposal.FINAL_PREMIUM_USD + ",'" + proposal.IP_ADDRESS + "','" + proposal.ENTERED_BY + "'," + proposal.AGENT_CODE + "," + proposal.DEBTOR_CODE + ",'" + proposal.CURR_TYPE + "','"+ proposal.AGNT_RATE_CODE + "','"+proposal.SLI_NO1+ "','"+proposal.SLI_NO2+ "','" + proposal.CLIENT_ID + "','" + proposal.BRANCH + "')";


    //                command.CommandText = insSql;



    //                command.ExecuteNonQuery();

    //                command.Parameters.Clear();

    //                transaction.Commit();
    //                result = true;

    //            }
    //            catch (Exception u)
    //            {
    //                transaction.Rollback();
    //                log logger = new log();
    //                logger.write_log("Failed at gt_proposal_mast->insert_rec: " + u.ToString());
    //            }

    //            connection.Close();
    //        }
    //        //}
    //    }
    //    catch (Exception u)
    //    {
    //        string g = u.ToString();
    //        log logger = new log();
    //        logger.write_log("Failed at gt_proposal_mast->insert_rec: " + u.ToString());
    //    }
    //    finally
    //    {

    //    }

    //    return result;

    //}

    public double getSumassured(string schemecode)
    {
        double summassured = 0;
        try
        {

            this.connectDB();
            string getSumAssured = "select sum_ins_usd FROM sligen.trv_schemes where code = '" + schemecode + "'";

            using (OracleCommand cmd = new OracleCommand(getSumAssured, objOraCon))
            {
                OracleDataReader cntReader = cmd.ExecuteReader();

                while (cntReader.Read())
                {
                    if (!cntReader.IsDBNull(0))
                    {
                        summassured = cntReader.GetDouble(0);
                    }
                }
                cntReader.Close();
            }

        }
        catch (Exception e)
        {

            log logger = new log();
            logger.write_log("Failed at getSum Assured: " + e.ToString());
        }
        finally
        {
            this.disconnectDB();
        }

        return summassured;


    }

    public double getDollarRate()
    {
        double dollarValue = 0;

        try
        {

            this.connectDB();
            string getDollarRate = "Select CURAT from LPAY.CURRDATE" +
                                   " where CUDATE = (Select max(CUDATE) from LPAY.CURRDATE where CUTYPE = 'USD')" +
                                   " and CUTYPE = 'USD'";

            using (OracleCommand cmd = new OracleCommand(getDollarRate, objOraCon))
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
            this.disconnectDB();
        }

        return dollarValue;
    }

    //public DataTable LoadSchAccordingToPlan(string plan)
    //{
    //    select 
    //}

    public ArrayList LoadSchAccordingToPlan(string plan)
    {
        ArrayList arrSchemes = new ArrayList();

        try
        {
            this.connectDB();
            //string getSchemes = "select '0' as code, 'Select Scheme' as description FROM dual " +
            //                    " union " +
            //                    " select code,trim(description) from sligen.trv_schemes" +
            //                     " ORDER by code";

            string getSchemes = "select '0' as code, 'Select Scheme' as description FROM dual " +
                    " union " +
                    " select code,trim(description)  from sligen.trv_schemes where to_number(sum_ins_usd) >=( "+
                    " select to_number(sum_ins_usd) from sligen.trv_schemes where CODE = '"+ plan + "') and CODE != '" + plan + "' order by code";

            using (OracleCommand cmd = new OracleCommand(getSchemes, objOraCon))
            {
                OracleDataReader cntReader = cmd.ExecuteReader();

                while (cntReader.Read())
                {
                    if (!cntReader.IsDBNull(0))
                    {
                        arrSchemes.Add(new ListItem(cntReader.GetString(1), cntReader.GetString(0)));
                    }
                }
                cntReader.Close();
            }

        }
        catch (Exception e)
        {
            arrSchemes = null;
            log logger = new log();
            logger.write_log("Failed at getSchemeList: " + e.ToString());
        }
        finally
        {
            this.disconnectDB();
        }

        return arrSchemes;
    }


}