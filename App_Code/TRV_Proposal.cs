using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.OracleClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for TRV_Proposal
/// </summary>
public class TRV_Proposal
{
    OracleCommand objOraCom = new OracleCommand();
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
    public double DiscRate { get; set; }
    public double DiscAmount { get; set; }
    DataTable dtPlanformatted = new DataTable();
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    public TRV_Proposal()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public TRV_Proposal(string proposal_ID)
    {
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string sql = "SELECT A.POL_TYPE, A.REFNO, A.FULL_NAME, A.ADDRESS1, A.ADDRESS2, A.ADDRESS3, A.ADDRESS4, A.MOBILE_NUMBER, A.HOME_NUMBER, A.OFFICE_NUMBER, A.PLAN, A.SUM_INS_USD," +
                         " A.FINAL_PREMIUM_RS, A.ADMIN_FEE_RS, A.POLICY_FEE_RS, A.NBT_RS, A.VAT_RS, to_char(A.ENTERED_DATE, 'yyyy/mm/dd') AS ENTRY_DATE, A.TITLE ," +
                         " to_char(A.DEPART_DATE, 'yyyy/mm/dd') AS DEPART_DATE , to_char(A.RETURN_DATE, 'yyyy/mm/dd') AS RETURN_DATE" +
                         " FROM SLIGEN.TRV_PROP_MAST A, GENPAY.POLTYP P WHERE REFNO = :refno AND P.PTDEP ='G'  AND P.PTTYP = A.POL_TYPE";

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
                    plan = reader[10].ToString().Trim();

                    if (!reader.IsDBNull(11))
                    {
                        sumAssured = Convert.ToDouble(reader[11].ToString().Trim());
                    }
                    else
                    {
                        sumAssured = 0;
                    }

                    if (!reader.IsDBNull(12))
                    {
                        annualPremium = Convert.ToDouble(reader[12].ToString().Trim());
                    }
                    else
                    {
                        annualPremium = 0;
                    }

                    if (!reader.IsDBNull(13))
                    {
                        adminFee = Convert.ToDouble(reader[13].ToString().Trim());
                    }
                    else
                    {
                        adminFee = 0;
                    }

                    if (!reader.IsDBNull(14))
                    {
                        polFee = Convert.ToDouble(reader[14].ToString().Trim());
                    }
                    else
                    {
                        polFee = 0;
                    }

                    if (!reader.IsDBNull(15))
                    {
                        NBT = Convert.ToDouble(reader[15].ToString().Trim());
                    }
                    else
                    {
                        NBT = 0;
                    }

                    if (!reader.IsDBNull(16))
                    {
                        VAT = Convert.ToDouble(reader[16].ToString().Trim());
                    }
                    else
                    {
                        VAT = 0;
                    }

                    
                    entryDate = reader[17].ToString().Trim();                     
                    title = reader[18].ToString().Trim();
                    comenmentDate = reader[19].ToString().Trim();

                    if (policyType == "TPI")
                    {
                        product_Name = "TRAVEL PROTECT";
                    }
                    else if(policyType == "TPM")
                    {
                        product_Name = "TRAVEL PROTECT ANNUAL MULTI TRIPS";
                    }
                    endDate = reader[20].ToString().Trim();
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

    public TRV_Proposal(string proposal_ID,int i)
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


    public ArrayList getCountryList()
    {
        ArrayList arrCountries = new ArrayList();

        try
        {
            oconn.Open();
            string getCountries = "select COUNTRY_ID, COUNTRY_NAME from SLIGEN.TRV_COUNTRIES WHERE COUNTRY_ID<>'LK'" +
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

    public bool check_schengen(string country_code)
    {
        log lg = new log();
        lg.write_log(country_code);
        bool result = false;
        ArrayList arrSchCountries = new ArrayList();
        try
        {
            oconn.Open();
            string SchCountries = "select * from SLIGEN.trv_countries where country_id in ( "+ country_code + " ) and  schenegen = 'Y'";
            using (OracleCommand cmd = new OracleCommand(SchCountries, oconn))
            {
                OracleDataReader cntReader = cmd.ExecuteReader();

                while (cntReader.Read())
                {
                    if (!cntReader.IsDBNull(0))
                    {
                        arrSchCountries.Add(new ListItem(cntReader.GetString(1), cntReader.GetString(0)));
                    }
                    result = true;
                }
                cntReader.Close();
            }
        }
        catch (Exception ex)
        {

            lg.write_log(ex.ToString());
            result = false;
        }
        finally
        {
            oconn.Close();

        }
        lg.write_log(result.ToString());
        return result;
    }

    public string getAllTRVPremiums(string uname, DataTable dtMemDetails, string destination, string fromDate, string toDate, string coverType, DataTable visitCntryList, GridView gvPlanDetails,int agentcode)
    {
        string mesg = "success";

        double adminFeePerc = 0.00;
        double polFee = 0.00;
        DateTime leaveDate;
        DateTime returnDate;
        double noOfDays = 0;
        double finalPremTotal = 0.00;
        double nbtAmount = 0.00;
        double vatAmount = 0.00;
        string planCode = "";
        double grpDiscRate = 0.00;
        double basePremium = 0.00;
        double adminFee = 0.00;

        double premium_as25 = 0.00; 
        double premium_est1 = 0.00;
        double premium_est2 = 0.00;
        double premium_est3 = 0.00;
        double premium_std1 = 0.00;
        double premium_std2 = 0.00;
        double premium_bsc1 = 0.00;
        double premium_bsc2 = 0.00;




        try
        {
            TRV_Parameters paras = new TRV_Parameters();
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
                double dollarValue = getDollarRate();

                foreach (DataRow planRow in dtPlans.Rows)
                {
                    planCode = planRow.ItemArray[0].ToString();
                    finalPremTotal = 0;
                    nbtAmount = 0;
                    vatAmount = 0;

                    if (coverType == "G" || coverType == "F")
                    {
                        string getGrpDiscRate = "Select DISC_RATE from SLIGEN.TRV_DISCOUNTS" +
                                              " where NUM_PERSONS = (select max(NUM_PERSONS) from SLIGEN.TRV_DISCOUNTS where NUM_PERSONS  < :memCount)" +
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

                        DateTime dob = DateTime.Parse(memRow["DOB"].ToString());

                        // Get difference in total months.
                        int months = ((DateTime.Now.Year - dob.Year) * 12) + (DateTime.Now.Month - dob.Month);

                        // substract 1 month if end month is not completed
                        if (DateTime.Now.Day < dob.Day)
                        {
                            months--;
                        }

                        double totalyears = months / 12d;
                        double age = Math.Round(totalyears);

                        string getPremium = "Select PREMIUM" +
                                            " from SLIGEN.TRV_PREMIUM" +
                                            " where PLAN = :plan" +
                                            " and DAYS = :noOfDays ";

                        using (OracleCommand cmd = new OracleCommand(getPremium, oconn))
                        {
                            OracleParameter oPlan = new OracleParameter();
                            oPlan.DbType = DbType.String;
                            oPlan.Value = planCode;
                            oPlan.ParameterName = "plan";

                            OracleParameter oNoOfDays = new OracleParameter();
                            oNoOfDays.DbType = DbType.Double;
                            oNoOfDays.Value = noOfDays;
                            oNoOfDays.ParameterName = "noOfDays";

                            cmd.Parameters.Add(oPlan);
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
                                if (double.Parse(memRow["Age"].ToString()) < 18)
                                {
                                    basePremium = basePremium / 2;
                                }
                            }

                            if (age >= 70 && totalyears < 80)
                            {
                                basePremium = basePremium * 2;
                            }
                            //if (age < 70 && age >= 18)
                            //{
                            //    basePremium = basePremium;
                            //}

                        }

                        if( coverType == "I" || coverType == "G")
                        {
                            if (age >= 70 && totalyears < 80)
                            {
                                basePremium = basePremium * 2;
                            }
                        }

                        finalPremTotal = finalPremTotal + basePremium;
                    }

                    if (coverType == "G")
                    {

                        finalPremTotal = finalPremTotal - (finalPremTotal * grpDiscRate);
                    }



                    if (dollarValue != 0)
                    {
                        finalPremTotal = finalPremTotal * dollarValue;
                    }
                    else
                    {
                        mesg = "Exchange Rate convertion error";
                    }

                    if (coverType == "G")
                    {
                        int memCount = dtMemDetails.Rows.Count;
                        int count = 0;
                        if (memCount > 10 && memCount < 20)
                        {
                            count = 10;
                             GetDicountParams(count, "TPI");
                            DiscRate = DiscRate;
                            DiscAmount = finalPremTotal * (DiscRate / 100);
                        }
                        else if (memCount > 20 && memCount < 50)
                        {
                            count = 20;
                            GetDicountParams(count, "TPI");
                            DiscRate = DiscRate;
                            DiscAmount = finalPremTotal * (DiscRate / 100);
                        }
                        else if (memCount > 50 && memCount < 100)
                        {
                            count = 50;
                            GetDicountParams(count, "TPI");
                            DiscRate = DiscRate;
                            DiscAmount = finalPremTotal * (DiscRate / 100);
                        }
                        else if (memCount > 100)
                        {
                            count = 100;
                            GetDicountParams(count, "TPI");
                            DiscRate = DiscRate;
                            DiscAmount = finalPremTotal * (DiscRate / 100);
                        }
                        finalPremTotal = finalPremTotal - DiscAmount;
                    }
                    if (mesg == "success")
                    {
                        adminFee = finalPremTotal * (adminFeePerc/100);

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

                        if (planCode == "AS25")
                        {
                            premium_as25 = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                        }
                        else if (planCode == "EST1")
                        {
                            premium_est1 = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                        }
                        else if (planCode == "EST2")
                        {
                            premium_est2 = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                        }
                        else if (planCode == "EST3")
                        {
                            premium_est3 = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                        }
                        else if (planCode == "STD1")
                        {
                            premium_std1 = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                        }
                        else if (planCode == "STD2")
                        {
                            premium_std2 = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                        }
                        else if (planCode == "BSC1")
                        {
                            premium_bsc1 = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                        }
                        else if (planCode == "BSC2")
                        {
                            premium_bsc2 = finalPremTotal + adminFee + polFee + nbtAmount + vatAmount;
                        }
                        else
                        {
                            mesg = "Premiums cannot be calculated - internal error.";
                        }
                    }
                }
                //--------------------------------------
                string refNo = "";

                if (mesg == "success")
                {
                    mesg = createTRVProposal(uname, dtMemDetails, destination, fromDate, toDate, coverType, visitCntryList, "AS25",agentcode, out refNo);
                }
                if (mesg == "success")
                {
                    
                    this.DrawTable(gvPlanDetails,refNo, premium_as25,premium_bsc1,premium_bsc2,premium_std1,premium_std2,premium_est1,premium_est2,premium_est3);


                }

            }
            else
            {
                mesg = "Premiums cannot be calculated - internal error2.";
            }
        }
        catch (Exception exc)
        {

        }
        return mesg;
    }

    public void GetDicountParams(int MemCount, string PolType)
    {
        try
        {
            if (oconn.State == ConnectionState.Closed)
            {
                oconn.Open();
            }
            string GetDiscRate = "select disc_rate from sligen.trv_discounts WHERE num_persons = " + MemCount + "" +
                                   " and pol_type = '" + PolType + "' and sysdate >= effect_from and effect_to<= sysdate  ";

            using (OracleCommand cmd = new OracleCommand(GetDiscRate, oconn))
            {
                OracleDataReader cntReader = cmd.ExecuteReader();

                while (cntReader.Read())
                {
                    if (!cntReader.IsDBNull(0))
                    {
                        DiscRate = cntReader.GetDouble(0);
                    }

                }
                cntReader.Close();
            }

        }
        catch (Exception e)
        {

            log logger = new log();
            logger.write_log("Failed at Get Limits: " + e.ToString());
        }
        finally
        {
             
        }
    }
    public void DrawTable(GridView gvPlanDetails,string Refno, double premium_as25, double premium_bsc1, double premium_bsc2, double premium_std1, double premium_std2, double premium_est1, double premium_est2, double premium_est3)
    {

        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("QuN0", Refno);
        dic.Add("Enter_by", "CUST");
        dic.Add("ip", ip);
        dic.Add("repr_State", "false");
        dic.Add("polTy", "TPI");



        //string getPlanDetails = "Select BENEFIT, SUM_AS25, EXC_AS25, SUM_BSC1, EXC_BSC1, SUM_BSC2, EXC_BSC2," +
        //                                   " SUM_STD1,EXC_STD1,SUM_STD2,EXC_STD2, SUM_EST1, EXC_EST1, SUM_EST2, EXC_EST2, SUM_EST3, EXC_EST3" +
        //                                   " from SLIGEN.TRV_BENEFITS ORDER BY CODE ";

        string getPlanDetails = "Select CODE,BENEFIT, " +
                                " CASE WHEN    code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE SUM_AS25 END AS SUM_AS25, " +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE EXC_AS25 END AS EXC_AS25, " +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE SUM_BSC1 END AS SUM_BSC1,  " +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE EXC_BSC1 END AS EXC_BSC1, " +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE SUM_BSC2 END AS SUM_BSC2, " +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE EXC_BSC2 END AS EXC_BSC2," +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE SUM_STD1 END AS SUM_STD1, " +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE EXC_STD1 END AS EXC_STD1," +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE SUM_STD2 END AS SUM_STD2, " +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE EXC_STD2 END AS EXC_STD2, " +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE SUM_EST1 END AS SUM_EST1, " +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE EXC_EST1 END AS EXC_EST1 , " +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE SUM_EST2 END AS SUM_EST2, " +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE EXC_EST2 END AS EXC_EST2 ," +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE SUM_EST3 END AS SUM_EST3, " +
                                " CASE WHEN  code = '1000' or code = '2000' or code = '3000' or code = '4000' or code = '5000' THEN '' ELSE EXC_EST3 END AS EXC_EST3" +
                                " from SLIGEN.TRV_BENEFITS order by CODE";

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
        Dictionary<string, string> qs2 = new Dictionary<string, string>();




        GridViewRow gvHeader0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        gvHeader0.Attributes["id"] = "top_rows";
        TableCell header0Cell0 = new TableCell()
        {
            Text = "",
            HorizontalAlign = HorizontalAlign.Left
        };

        // Header row for AS25
        TableHeaderCell header0Cell1 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };

        qs.Add("action", "print");
        qs.Add("refNo", Refno);
        qs.Add("plan", "AS25");
        qs.Add("premium", premium_as25.ToString("N2"));
        HyperLink hlPrAS25 = new HyperLink();
        hlPrAS25.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlPrAS25.Text = "Print Quotation";
        hlPrAS25.Width = 50;
        hlPrAS25.CssClass = "btn btn-print";
        header0Cell1.Controls.Add(hlPrAS25);

        // Header row for BSC1
        TableHeaderCell header0Cell2 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        qs.Remove("plan");
        qs.Add("plan", "BSC1");
        qs.Remove("premium");
        qs.Add("premium", premium_bsc1.ToString("N2"));
        HyperLink hlPrBSC1 = new HyperLink();
        hlPrBSC1.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlPrBSC1.Text = "Print Quotation";
        hlPrBSC1.CssClass = "btn btn-print";
        hlPrBSC1.Width = 50;
        header0Cell2.Controls.Add(hlPrBSC1);

        // Header row for BSC2
        TableHeaderCell header0Cell3 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        qs.Remove("plan");
        qs.Add("plan", "BSC2");
        qs.Remove("premium");
        qs.Add("premium", premium_bsc2.ToString("N2"));
         
        HyperLink hlPrBSC2 = new HyperLink();
        hlPrBSC2.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlPrBSC2.Text = "Print Quotation";
        hlPrBSC2.CssClass = "btn btn-print";
        hlPrBSC2.Width = 50;
        header0Cell3.Controls.Add(hlPrBSC2);


        // Header row for STD1
        TableHeaderCell header0Cell4 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        
        qs.Remove("plan");
        qs.Add("plan", "STD1");
        qs.Remove("premium");
        qs.Add("premium", premium_std1.ToString("N2"));

        HyperLink hlPrSTD1 = new HyperLink();
        hlPrSTD1.CssClass = "btn btn-print";
        hlPrSTD1.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlPrSTD1.Text = "Print Quotation";
        hlPrSTD1.Width = 50;
        header0Cell4.Controls.Add(hlPrSTD1);


        // Header row for STD2
        TableHeaderCell header0Cell5 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        qs.Remove("plan");
        qs.Add("plan", "STD2");
        qs.Remove("premium");
        qs.Add("premium", premium_std2.ToString("N2"));
        //qs.Add("premium", "0");
        HyperLink hlPrSTD2 = new HyperLink();
        hlPrSTD2.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlPrSTD2.Text = "Print Quotation";
        hlPrSTD2.CssClass = "btn btn-print";
        hlPrSTD2.Width = 50;
        header0Cell5.Controls.Add(hlPrSTD2);


        // Header row for EST1
        TableHeaderCell header0Cell6 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        qs.Remove("plan");
        qs.Add("plan", "EST1");
        qs.Remove("premium");
        qs.Add("premium", premium_est1.ToString("N2"));

        // qs.Add("premium", "0");
        HyperLink hlPrEST1 = new HyperLink();
        hlPrEST1.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlPrEST1.Text = "Print Quotation";
        hlPrEST1.Width = 50;
        hlPrEST1.CssClass = "btn btn-print";
        header0Cell6.Controls.Add(hlPrEST1);


        // Header row for EST2
        TableHeaderCell header0Cell7 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        qs.Remove("plan");
        qs.Add("plan", "EST2");
        qs.Remove("premium");
        qs.Add("premium", premium_est2.ToString("N2"));
        //qs.Add("premium", "0");
        HyperLink hlPrEST2 = new HyperLink();
        hlPrEST2.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlPrEST2.Text = "Print Quotation";
        hlPrEST2.Width = 50;
        hlPrEST2.CssClass = "btn btn-print";
        header0Cell7.Controls.Add(hlPrEST2);

        // Header row for EST3
        TableHeaderCell header0Cell8 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        qs.Remove("plan");
        qs.Add("plan", "EST3");
        qs.Remove("premium");
        qs.Add("premium", premium_est3.ToString("N2"));
        HyperLink hlPrEST3 = new HyperLink();
        hlPrEST3.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlPrEST3.Text = "Print Quotation";
        hlPrEST3.Width = 50;
        hlPrEST3.CssClass = "btn btn-print";
        header0Cell8.Controls.Add(hlPrEST3);

        header0Cell0.Style.Add("border-top-style", "none");
        gvHeader0.Cells.Add(header0Cell0);
        gvHeader0.Cells.Add(header0Cell1);
        gvHeader0.Cells.Add(header0Cell2);
        gvHeader0.Cells.Add(header0Cell3);
        gvHeader0.Cells.Add(header0Cell4);
        gvHeader0.Cells.Add(header0Cell5);
        gvHeader0.Cells.Add(header0Cell6);
        gvHeader0.Cells.Add(header0Cell7);
        gvHeader0.Cells.Add(header0Cell8);

        gvHeader0.Font.Bold = true;


        GridViewRow gvHeader1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        gvHeader1.Attributes["id"] = "top_rows";
        TableHeaderCell header1Cell0 = new TableHeaderCell()
        {
            Text = "",
            HorizontalAlign = HorizontalAlign.Left
        };

        // Hiperlink for AS25
        TableHeaderCell header1Cell1 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };

        qs.Remove("action");
        qs.Add("action", "buy");
        qs.Remove("plan");
        qs.Add("plan", "AS25");
        qs.Remove("premium");
        qs.Add("premium", premium_as25.ToString("N2"));

        HyperLink hlByAS25 = new HyperLink();
       // hlByAS25.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlByAS25.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Questionnaire2.aspx", qs  );
        hlByAS25.Text = "Buy";
        hlByAS25.Width = 50;
        hlByAS25.CssClass = "btn btn-xx";
        header1Cell1.Controls.Add(hlByAS25);

        // Hiperlink for BSC1
        TableHeaderCell header1Cell2 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        qs.Remove("plan");
        qs.Add("plan", "BSC1");
        qs.Remove("premium");
        qs.Add("premium", premium_bsc1.ToString("N2"));
        //qs.Add("premium", premium_bsc1.ToString("N2"));
        HyperLink hlByBSC1 = new HyperLink();
        //hlByBSC1.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlByBSC1.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Questionnaire2.aspx", qs);
        hlByBSC1.Text = "Buy";
        hlByBSC1.CssClass = "btn btn-xx";
        hlByBSC1.Width = 50;
        header1Cell2.Controls.Add(hlByBSC1);

        // Hiperlink for BSC2
        TableHeaderCell header1Cell3 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        qs.Remove("plan");
        qs.Add("plan", "BSC2");
        qs.Remove("premium");
        qs.Add("premium", premium_bsc2.ToString("N2"));
        HyperLink hlByBSC2 = new HyperLink();
        //hlByBSC2.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlByBSC2.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Questionnaire2.aspx", qs);
        hlByBSC2.Text = "Buy";
        hlByBSC2.CssClass = "btn btn-xx";
        hlByBSC2.Width = 50;
        header1Cell3.Controls.Add(hlByBSC2);

        // Hiperlink for STD1
        TableHeaderCell header1Cell4 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        qs.Remove("plan");
        qs.Add("plan", "STD1");
        qs.Remove("premium");
        qs.Add("premium", premium_std1.ToString("N2"));
        // qs.Add("premium", premium_std1.ToString("N2"));
        HyperLink hlBySTD1 = new HyperLink();
        //hlBySTD1.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlBySTD1.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Questionnaire2.aspx", qs);
        hlBySTD1.Text = "Buy";
        hlBySTD1.CssClass = "btn btn-xx";
        hlBySTD1.Width = 50;
        header1Cell4.Controls.Add(hlBySTD1);

        // Hiperlink for STD2
        TableHeaderCell header1Cell5 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        qs.Remove("plan");
        qs.Add("plan", "STD2");
        qs.Remove("premium");
        qs.Add("premium", premium_std2.ToString("N2"));
        // qs.Add("premium", premium_std2.ToString("N2"));
        HyperLink hlBySTD2 = new HyperLink();
      //   hlBySTD2.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
       hlBySTD2.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Questionnaire2.aspx", qs);
        hlBySTD2.Text = "Buy";
        hlBySTD2.CssClass = "btn btn-xx";
        hlBySTD2.Width = 50;
        header1Cell5.Controls.Add(hlBySTD2);


        // Hiperlink for EST1
        TableHeaderCell header1Cell6 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        qs.Remove("plan");
        qs.Add("plan", "EST1");
        qs.Remove("premium");
        qs.Add("premium", premium_est1.ToString("N2"));
        //  qs.Add("premium", premium_est1.ToString("N2"));
        HyperLink hlByEST1 = new HyperLink();
        //hlByEST1.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlByEST1.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Questionnaire2.aspx", qs);
        hlByEST1.Text = "Buy";
        hlByEST1.CssClass = "btn btn-xx";
        hlByEST1.Width = 50;
        header1Cell6.Controls.Add(hlByEST1);

        // Hiperlink for EST2
        TableHeaderCell header1Cell7 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        qs.Remove("plan");
        qs.Add("plan", "EST2");
        qs.Remove("premium");
        qs.Add("premium", premium_est2.ToString("N2"));
        // qs.Add("premium", premium_est2.ToString("N2"));
        HyperLink hlByEST2 = new HyperLink();
        //hlByEST2.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlByEST2.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Questionnaire2.aspx", qs);
        hlByEST2.Text = "Buy";
        hlByEST2.CssClass = "btn btn-xx";
        hlByEST2.Width = 50;
        header1Cell7.Controls.Add(hlByEST2);

        // Hiperlink for EST3
        TableHeaderCell header1Cell8 = new TableHeaderCell()
        {
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        qs.Remove("plan");
        qs.Add("plan", "EST3");
        qs.Remove("premium");
        qs.Add("premium", premium_est3.ToString("N2"));
        // qs.Add("premium", premium_est3.ToString("N2"));
        HyperLink hlByEST3 = new HyperLink();
        //hlByEST3.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_Print.aspx", qs);
        hlByEST3.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Questionnaire2.aspx", qs);
        hlByEST3.Text = "Buy";
        hlByEST3.CssClass = "btn btn-xx";
        hlByEST3.Width = 50;
        header1Cell8.Controls.Add(hlByEST3);

        header1Cell0.Style.Add("border-bottom-style", "none");
        gvHeader1.Cells.Add(header1Cell0);
        gvHeader1.Cells.Add(header1Cell1);
        gvHeader1.Cells.Add(header1Cell2);
        gvHeader1.Cells.Add(header1Cell3);
        gvHeader1.Cells.Add(header1Cell4);
        gvHeader1.Cells.Add(header1Cell5);
        gvHeader1.Cells.Add(header1Cell6);
        gvHeader1.Cells.Add(header1Cell7);
        gvHeader1.Cells.Add(header1Cell8);
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
            Text = premium_as25.ToString("N2"),
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2,
            //BackColor = System.Drawing.Color.FromArgb(235, 194, 194),
        };
        //header2Cell1.Font.Bold = true;

        TableHeaderCell header2Cell2 = new TableHeaderCell()
        {
            Text = premium_bsc1.ToString("N2"),
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        TableHeaderCell header2Cell3 = new TableHeaderCell()
        {
            Text = premium_bsc2.ToString("N2"),
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        TableHeaderCell header2Cell4 = new TableHeaderCell()
        {
            Text = premium_std1.ToString("N2"),
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        TableHeaderCell header2Cell5 = new TableHeaderCell()
        {
            Text = premium_std2.ToString("N2"),
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        TableHeaderCell header2Cell6 = new TableHeaderCell()
        {
            Text = premium_est1.ToString("N2"),
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        TableHeaderCell header2Cell7 = new TableHeaderCell()
        {
            Text = premium_est2.ToString("N2"),
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        TableHeaderCell header2Cell8 = new TableHeaderCell()
        {
            Text = premium_est3.ToString("N2"),
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
        gvHeader2.Cells.Add(header2Cell7);
        gvHeader2.Cells.Add(header2Cell8);


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
            Text = "ASIA 25 EXCLUDING JAPAN",
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2,
            //BackColor = System.Drawing.Color.FromArgb(235, 194, 194),
        };


        TableHeaderCell header3Cell2 = new TableHeaderCell()
        {
            Text = "SILVER -  WORLD WIDE  EXCL USA/ CANADA",
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        TableHeaderCell header3Cell3 = new TableHeaderCell()
        {
            Text = "SILVER -  WORLD WIDE(Government Staff Only)",
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2,
            ForeColor = System.Drawing.Color.Red
        };
        TableHeaderCell header3Cell4 = new TableHeaderCell()
        {
            Text = "PLATINUM - WORLD WIDE  EXCL USA/CANADA",
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        TableHeaderCell header3Cell5 = new TableHeaderCell()
        {
            Text = "PLATINUM - WORLD WIDE",
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        TableHeaderCell header3Cell6 = new TableHeaderCell()
        {
            Text = "GOLD - ASIA EXCLUDING JAPAN",
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        TableHeaderCell header3Cell7 = new TableHeaderCell()
        {
            Text = "GOLD - WORLD WIDE  EXCL USA/CANADA",
            HorizontalAlign = HorizontalAlign.Center,
            ColumnSpan = 2
        };
        TableHeaderCell header3Cell8 = new TableHeaderCell()
        {
            Text = "GOLD - WORLD WIDE",
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
        gvHeader3.Cells.Add(header3Cell7);
        gvHeader3.Cells.Add(header3Cell8);

        if (premium_as25 == 0)
        {
            gvHeader0.Cells[1].Visible = false;
            gvHeader1.Cells[1].Visible = false;
            gvHeader2.Cells[1].Visible = false;
            gvHeader3.Cells[1].Visible = false;

            gvPlanDetails.Columns[1].Visible = false;
            gvPlanDetails.Columns[2].Visible = false;
             
        }

        if (premium_bsc1 == 0)
        {
            gvHeader0.Cells[2].Visible = false;
            gvHeader1.Cells[2].Visible = false;
            gvHeader2.Cells[2].Visible = false;
            gvHeader3.Cells[2].Visible = false;

            gvPlanDetails.Columns[3].Visible = false;
            gvPlanDetails.Columns[4].Visible = false;
        }

        if (premium_bsc2 == 0)
        {
            gvHeader0.Cells[3].Visible = false;
            gvHeader1.Cells[3].Visible = false;
            gvHeader2.Cells[3].Visible = false;
            gvHeader3.Cells[3].Visible = false;

            gvPlanDetails.Columns[5].Visible = false;
            gvPlanDetails.Columns[6].Visible = false;
        }

        if (premium_std1 == 0)
        {
            gvHeader0.Cells[4].Visible = false;
            gvHeader1.Cells[4].Visible = false;
            gvHeader2.Cells[4].Visible = false;
            gvHeader3.Cells[4].Visible = false;

            gvPlanDetails.Columns[7].Visible = false;
            gvPlanDetails.Columns[8].Visible = false;
        }

        if (premium_std2 == 0)
        {
            gvHeader0.Cells[5].Visible = false;

            gvHeader1.Cells[5].Visible = false;
            gvHeader2.Cells[5].Visible = false;
            gvHeader3.Cells[5].Visible = false;

            gvPlanDetails.Columns[9].Visible = false;
            gvPlanDetails.Columns[10].Visible = false;
        }

        if (premium_est1 == 0)
        {
            gvHeader0.Cells[6].Visible = false;
            gvHeader1.Cells[6].Visible = false;
            gvHeader2.Cells[6].Visible = false;
            gvHeader3.Cells[6].Visible = false;

            gvPlanDetails.Columns[11].Visible = false;
            gvPlanDetails.Columns[12].Visible = false;
        }
        if (premium_est2 == 0)
        {
            gvHeader0.Cells[7].Visible = false;
            gvHeader1.Cells[7].Visible = false;
            gvHeader2.Cells[7].Visible = false;
            gvHeader3.Cells[7].Visible = false;

            gvPlanDetails.Columns[13].Visible = false;
            gvPlanDetails.Columns[14].Visible = false;
        }
        if (premium_est3 == 0)
        {
            gvHeader0.Cells[8].Visible = false;
            gvHeader1.Cells[8].Visible = false;
            gvHeader2.Cells[8].Visible = false;
            gvHeader3.Cells[8].Visible = false;

            gvPlanDetails.Columns[15].Visible = false;
            gvPlanDetails.Columns[16].Visible = false;
        }

        gvPlanDetails.Controls[0].Controls.AddAt(0, gvHeader1);
        gvPlanDetails.Controls[0].Controls.AddAt(0, gvHeader0);        
        gvPlanDetails.Controls[0].Controls.AddAt(0, gvHeader2);
        gvPlanDetails.Controls[0].Controls.AddAt(0, gvHeader3);
    }

    public string updateTRVProposal(string plan, string refNo)
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
            visitContrys.Columns.Add(new DataColumn("Country", typeof(string)));

            oconn.Open();
            //check if plan is valid ----- this maybe necessary as the plan is updated

            string getPropDetails = "Select NO_OF_DAYS, TRV_TYPE, GROUP_DISC_RATE, DESTINATION, VISIT_CTRY1, VISIT_CTRY2, VISIT_CTRY3, VISIT_CTRY4" +
                                    " from SLIGEN.TRV_QUOT_MAST" +
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
                        DataRow drCurrentRow = null;
                        drCurrentRow = visitContrys.NewRow();
                        drCurrentRow["Country"] = propReader.GetString(3);
                        visitContrys.Rows.Add(drCurrentRow);
                    }
                    if (!propReader.IsDBNull(4))
                    {
                        DataRow drCurrentRow = null;
                        drCurrentRow = visitContrys.NewRow();
                        drCurrentRow["Country"] = propReader.GetString(4);
                        visitContrys.Rows.Add(drCurrentRow);
                    }
                }
                propReader.Close();
            }

            if (isValidTRVPlan(plan, destination, visitContrys,"TPI"))
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
                string getSumIns = "Select SUM_INS_USD from SLIGEN.TRV_SCHEMES" +
                                   " where CODE = :plan AND POL_TYPE='TPI' ";

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
                                        " from SLIGEN.TRV_QUOT_MEM_DETAILS" +
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

                TRV_Parameters  paras = new TRV_Parameters();
                adminFeePerc = paras.admin_fee;
                polFee = paras.pol_fee;


                foreach (DataRow memRow in dtMemDetails.Rows)
                {
                    basePremium = 0.00;

                    DateTime dob = DateTime.Parse(memRow["DOB"].ToString());

                    // Get difference in total months.
                    int months = ((DateTime.Now.Year - dob.Year) * 12) + (DateTime.Now.Month - dob.Month);

                    // substract 1 month if end month is not completed
                    if (DateTime.Now.Day < dob.Day)
                    {
                        months--;
                    }

                    double totalyears = months / 12d;
                    double age = Math.Round(totalyears);

                    string getPremium = "Select PREMIUM" +
                                        " from SLIGEN.TRV_PREMIUM" +
                                        " where PLAN = :plan" +
                                        " AND DAYS = :noOfDays ";

                    using (OracleCommand cmd = new OracleCommand(getPremium, oconn))
                    {
                        OracleParameter oPlan = new OracleParameter();
                        oPlan.DbType = DbType.String;
                        oPlan.Value = plan;
                        oPlan.ParameterName = "plan";
                        
                        OracleParameter oNoOfDays = new OracleParameter();
                        oNoOfDays.DbType = DbType.Double;
                        oNoOfDays.Value = noOfDays;
                        oNoOfDays.ParameterName = "noOfDays";

                        cmd.Parameters.Add(oPlan);
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

                    if (age >= 70 && totalyears < 80)
                    {
                        basePremium = basePremium * 2;
                    }

                    memRow["BasePremium"] = basePremium;
                    net_premium_usd = net_premium_usd + basePremium;
                }

                if (coverType == "G" ||coverType=="F")
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
                if (coverType == "G")
                {
                    int memCount = dtMemDetails.Rows.Count;
                    int count = 0;
                    if (memCount > 10 && memCount < 20)
                    {
                        count = 10;
                        GetDicountParams(count, "TPI");
                        DiscRate = DiscRate;
                        DiscAmount = net_premium_rs * (DiscRate / 100);
                    }
                    else if (memCount > 20 && memCount < 50)
                    {
                        count = 20;
                        GetDicountParams(count, "TPI");
                        DiscRate = DiscRate;
                        DiscAmount = net_premium_rs * (DiscRate / 100);
                    }
                    else if (memCount > 50 && memCount < 100)
                    {
                        count = 50;
                        GetDicountParams(count, "TPI");
                        DiscRate = DiscRate;
                        DiscAmount = net_premium_rs * (DiscRate / 100);
                    }
                    else if (memCount > 100)
                    {
                        count = 100;
                        GetDicountParams(count, "TPI");
                        DiscRate = DiscRate;
                        DiscAmount = net_premium_rs * (DiscRate / 100);
                    }
                    net_premium_rs = net_premium_rs - DiscAmount;
                }
                if (mesg == "success")
                {
                    adminFee = net_premium_rs * (adminFeePerc/100);

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

                    TRV_Prop_mast mastUpdate = new TRV_Prop_mast();

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

    public bool isValidTRVPlan(string plan, string destination, DataTable visitingCountries,string PolType)
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
                              " from SLIGEN.TRV_SCHEMES" +
                              " where CODE = :plan and POL_TYPE= :poltype";

            using (OracleCommand cmd = new OracleCommand(getPlans, oconn))
            {
                OracleParameter oPlan = new OracleParameter();
                oPlan.DbType = DbType.String;
                oPlan.Value = plan;
                oPlan.ParameterName = "plan";

                OracleParameter oPolType = new OracleParameter();
                oPolType.DbType = DbType.String;
                oPolType.Value = PolType;
                oPolType.ParameterName = "poltype";

                cmd.Parameters.Add(oPlan);
                cmd.Parameters.Add(oPolType);

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
                string[] arrVisitCntry = destination.Split(',');

                for (int cnt = 0; cnt < arrVisitCntry.Length; cnt++)
                {
                    string destcode = arrVisitCntry[cnt];
                    if (!(destcode == planExcl1 || destcode == planExcl2))
                    {
                        if (continent == "WW" || isCountryInContinent(destcode, continent))
                        {
                            if (arrVisitCntry.Length > 0)
                            {
                                foreach (DataRow visitCtryRow in visitingCountries.Rows)
                                {
                                    toCountry = destcode;

                                    if (toCountry == planExcl1 || toCountry == planExcl2)
                                    {
                                        retValue = false;
                                        break;
                                    }
                                    else if (continent != "WW" && !isCountryInContinent(toCountry, continent))
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

    public string createTRVProposal(string uname, DataTable dtMemDetails, string destination, string fromDate, string toDate, string coverType, DataTable visitCntryList, string plan,int agentcode, out string refNo)
    {
        string mesg = "success";
        refNo = "";

        double adminFeePerc = 0.00;
        double polFee = 0.00;
        double noOfDays = 0.00;
        double grpDiscRate = 0.00;
        double basePremium = 0.00;
        double net_premium_usd = 0.00;
        double net_premium_rs = 0.00;
        double adminFee = 0.00;
        double nbtAmount = 0.00;
        double vatAmount = 0.00;
        double totFinalPremium = 0.00;
        double taxExpenses = 0.00;       
        double sumAss = 0.00;
        double discRate = 0.00;
        double discAmount = 0.00;

        string Destination = "";
        string IP_ADDRESS = "";

        DateTime leaveDate;
        DateTime returnDate;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            TRV_Parameters paras = new TRV_Parameters();
            adminFeePerc = paras.admin_fee;
            polFee = paras.pol_fee;

            leaveDate = DateTime.ParseExact(fromDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            returnDate = DateTime.ParseExact(toDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            noOfDays = (returnDate - leaveDate).TotalDays + 1;
            double dollarValue = getDollarRate();

            if (coverType == "G")
            {
                string getGrpDiscRate = "Select DISC_RATE from SLIGEN.TRV_DISCOUNTS" +
                                        " where NUM_PERSONS = (select max(NUM_PERSONS) from SLIGEN.TRV_DISCOUNTS where NUM_PERSONS  < :memCount)" +
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

            string getSumIns = "Select SUM_INS_USD " +
                                    " from SLIGEN.TRV_SCHEMES" +
                                    " where CODE = :plan" +
                                    " and POL_TYPE = 'TPI'";

            using (OracleCommand cmd2 = new OracleCommand(getSumIns, oconn))
            {
                OracleParameter oPPlan = new OracleParameter();
                oPPlan.DbType = DbType.String;
                oPPlan.Value = plan;
                oPPlan.ParameterName = "plan";

                cmd2.Parameters.Add(oPPlan);


                OracleDataReader sumReader = cmd2.ExecuteReader();

                while (sumReader.Read())
                {
                    if (!sumReader.IsDBNull(0))
                    {
                        sumAss = sumReader.GetDouble(0);
                    }
                }

                sumReader.Close();

            }
            foreach (DataRow memRow in dtMemDetails.Rows)
            {
                basePremium = 0.00;

                DateTime dob = DateTime.Parse(memRow["DOB"].ToString());

                // Get difference in total months.
                int months = ((DateTime.Now.Year - dob.Year) * 12) + (DateTime.Now.Month - dob.Month);

                // substract 1 month if end month is not completed
                if (DateTime.Now.Day < dob.Day)
                {
                    months--;
                }

                double totalyears = months / 12d;
                double age = Math.Round(totalyears);

                string getPremium = "Select PREMIUM " +
                                    " from SLIGEN.TRV_PREMIUM" +
                                    " where PLAN = :plan" +
                                    " and DAYS = :noOfDays";

                using (OracleCommand cmd = new OracleCommand(getPremium, oconn))
                {
                    OracleParameter oPlan = new OracleParameter();
                    oPlan.DbType = DbType.String;
                    oPlan.Value = plan;
                    oPlan.ParameterName = "plan";


                    OracleParameter oNoOfDays = new OracleParameter();
                    oNoOfDays.DbType = DbType.Double;
                    oNoOfDays.Value = noOfDays;
                    oNoOfDays.ParameterName = "noOfDays";

                    cmd.Parameters.Add(oPlan);
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

                if (age >= 70 && totalyears < 80)
                {
                    basePremium = basePremium * 2;
                }

                memRow["BasePremium"] = basePremium;
                net_premium_usd = net_premium_usd + basePremium;
            }
            
            if (coverType == "G")
            {
                net_premium_usd = net_premium_usd - (net_premium_usd * grpDiscRate);
            }            

            if (dollarValue != 0)
            {
                net_premium_rs = net_premium_usd * dollarValue;
            }
            else
            {
                mesg = "Exchange Rate convertion error";
            }
            if (coverType == "G")
            {
                int memCount = dtMemDetails.Rows.Count;
                int count = 0;
                if (memCount > 10 && memCount < 20)
                {
                    count = 10;
                    GetDicountParams(count, "TPI");
                    DiscRate = DiscRate;
                    DiscAmount = net_premium_rs * (DiscRate / 100);
                }
                else if (memCount > 20 && memCount < 50)
                {
                    count = 20;
                    GetDicountParams(count, "TPI");
                    DiscRate = DiscRate;
                    DiscAmount = net_premium_rs * (DiscRate / 100);
                }
                else if (memCount > 50 && memCount < 100)
                {
                    count = 50;
                    GetDicountParams(count, "TPI");
                    DiscRate = DiscRate;
                    DiscAmount = net_premium_rs * (DiscRate / 100);
                }
                else if (memCount > 100)
                {
                    count = 100;
                    GetDicountParams(count, "TPI");
                    DiscRate = DiscRate;
                    DiscAmount = net_premium_rs * (DiscRate / 100);
                }
                net_premium_rs = net_premium_rs - DiscAmount;
            }
            if (mesg == "success")
            {
                adminFee = net_premium_rs * (adminFeePerc/100);

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

                TRV_Prop_mast mastEntry = new TRV_Prop_mast();
                string name = profile.O_firstName + " " + profile.O_othNames + " " + profile.O_lastName;

                string Fdestination = "";
                foreach (DataRow cntyRow in visitCntryList.Rows)
                {
                    if (Fdestination == "")
                    {
                        Fdestination = cntyRow.ItemArray[1].ToString().Trim();
                    }
                    else
                    {
                        Fdestination = Fdestination + ","+ cntyRow.ItemArray[1].ToString().Trim();
                    }
                }
                destination = Fdestination;
                IP_ADDRESS = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                bool success = mastEntry.Insert_rec(name, profile.O_addrss1, profile.O_addrss2, profile.O_addrss3,profile.O_addrss4,
                                                    profile.O_mobileNumber,profile.O_title, dtMemDetails.Rows.Count, grpDiscRate, plan,
                                                    destination, fromDate, toDate,  "to be updated", coverType,
                                                    net_premium_usd, net_premium_rs, adminFee, polFee, nbtAmount, vatAmount, 
                                                    totFinalPremium, noOfDays, dtMemDetails, dollarValue, sumAss, net_premium_usd,IP_ADDRESS,
                                                    discRate,discAmount,uname,agentcode, out refNo);
                if (!success)
                {
                    mesg = "Quotation cannot be created due to internal error";
                }
            }

        }
        catch(Exception exc)
        {
            mesg = "Error while creating TRV quotation.";
            // Log your error         
            log logger = new log();
            logger.write_log("Failed at create TRVProposal: " + exc.Message);
        }

        return mesg;
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
                              " from SLIGEN.TRV_SCHEMES WHERE POL_TYPE='TPI' ORDER BY CODE";

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
                dtPlanformatted.Columns.Add("CODE", typeof(string));
                dtPlanformatted.PrimaryKey = new DataColumn[] { dtPlanformatted.Columns["CODE"] };
                dtPlanformatted.Columns.Add("EXCLUDE1", typeof(string));
                dtPlanformatted.Columns.Add("EXCLUDE2", typeof(string));
                dtPlanformatted.Columns.Add("CONTINENT", typeof(string));

                DataTable dtvisitcountryPK = new DataTable();
                dtvisitcountryPK.Columns.Add("COUNTRYID", typeof(string));
                dtvisitcountryPK.PrimaryKey = new DataColumn[] { dtvisitcountryPK.Columns["COUNTRYID"] };

                for (int count = 0; count < visitingCountries.Rows.Count; count++)
                {
                    DataRow cntryrow = dtvisitcountryPK.NewRow();
                    cntryrow[0] = visitingCountries.Rows[count].ItemArray[1].ToString();
                    dtvisitcountryPK.Rows.Add(cntryrow);
                }
               

                for (int i = 0; i < dtPlans.Rows.Count; i++)
                {
                    DataRow planRow = dtPlans.Rows[i];
                    planCode = planRow.ItemArray[0].ToString().Trim();
                    planExcl1 = planRow.ItemArray[1].ToString().Trim();
                    planExcl2 = planRow.ItemArray[2].ToString().Trim();
                    continent = planRow.ItemArray[3].ToString().Trim();

                    for (int visitcnt = 0; visitcnt < visitingCountries.Rows.Count; visitcnt++)
                    {
                        string[] arrcntry = visitingCountries.Rows[visitcnt].ItemArray[1].ToString().Split(',');
                        for (int x = 0; x < arrcntry.Length; x++)
                        {
                            try
                            {
                                if (arrcntry[x] == planExcl1 || arrcntry[x] == planExcl2)
                                {
                                    if (dtPlanformatted.Rows.Count > 0)
                                    {
                                        for (int row = 0; row < dtPlanformatted.Rows.Count; row++)
                                        {
                                            if (dtPlanformatted.Rows.Contains(planCode))
                                            {
                                                dtPlanformatted.Rows.RemoveAt(row);
                                            }
                                        }
                                    }
                                    visitcnt = visitingCountries.Rows.Count;
                                    break;
                                }
                                else if (continent != "WW" && !isCountryInContinent(arrcntry[x], continent))
                                {
                                    if (dtPlanformatted.Rows.Count > 0)

                                    {
                                        for (int row = 0; row < dtPlanformatted.Rows.Count ; row++)
                                        {
                                            if (dtPlanformatted.Rows.Contains(planCode))
                                            {
                                                dtPlanformatted.Rows.RemoveAt(row);
                                            }
                                        }
                                        //dtPlanformatted.Rows.Remove(planRow);
                                    }
                                    visitcnt = visitingCountries.Rows.Count;
                                    break;
                                }
                                else
                                {
                                    if (!dtPlanformatted.Rows.Contains(planCode) && 
                                         ( !dtvisitcountryPK.Rows.Contains(planExcl1) && (!dtvisitcountryPK.Rows.Contains(planExcl2)))
                                        )
                                    {
                                        DataRow row = dtPlanformatted.NewRow();
                                        row[0] = planCode;
                                        row[1] = planExcl1;
                                        row[2] = planExcl2;
                                        row[3] = continent;
                                        dtPlanformatted.Rows.Add(row);
                                        break;
                                    }
                                }
                                break;
                            }
                            catch (Exception exc)
                            { }

                        }
                    }
                   
                }
                 
                dtPlans.Rows.Clear();
                dtPlans = dtPlanformatted;
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

    public bool isCountryInContinent(string country, string continent)
    {
        bool returnValue = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string getCountry = "select count(*) from SLIGEN.TRV_COUNTRIES" +
                                " where COUNTRY_ID = :country" +
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

    public double getDollarRate()
    {
        double dollarValue = 0;

        try
        {

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



    public void fillTravelPurposeDdl(DropDownList ddlTrPurposes)
    {
        DataTable dtTrPurposes = new DataTable();
        try
        {
            oconn.Open();
            string getTrPurposes = "select VISIT_CODE, VISIT_REASON from SLIGEN.trv_purposes";

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
            string getTitles = " SELECT ' SELECT ' as TITLE from DUAL UNION select TITLE from BCOMMON.TITLE order by title asc";

            using (OracleDataAdapter adapter = new OracleDataAdapter(getTitles, oconn))
            {
                adapter.Fill(dtTitles);

                foreach (GridViewRow row in gvMembers.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        DropDownList ddTitle = ((DropDownList)row.FindControl("ddTitle"));

                        ddTitle.DataSource = dtTitles;
                        ddTitle.DataTextField = "TITLE";
                        ddTitle.DataValueField = "TITLE";
                        ddTitle.DataBind();

                         
                        // ddlTitles.SelectedValue = "Select";
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

    public void fillproplTitles(DropDownList ddltitle)
    {
        DataTable dtTitles = new DataTable();
        try
        {
            oconn.Open();
            string getTitles = "SELECT ' SELECT ' as TITLE from DUAL UNION select TITLE from BCOMMON.TITLE order by title asc";

            using (OracleDataAdapter adapter = new OracleDataAdapter(getTitles, oconn))
            {
                adapter.Fill(dtTitles);


                ddltitle.DataSource = dtTitles;
                ddltitle.DataTextField = "TITLE";
                ddltitle.DataValueField = "TITLE";
                ddltitle.DataBind();
                 
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



        #region Get AgencyName

        public string getAgtName(int agtcd, out string Name)
    {
        oconn.Open();
        int i = 0;
        Name = "";
        string sql = "select  trim(int)||Name as AgentName  from agent.agent where agency=" + agtcd + " and stcd in (0,3,4,6,7,10)";
        objOraCom.CommandType = CommandType.Text;
        objOraCom.CommandText = sql;
        objOraCom.Connection = oconn;
        OracleDataReader objOraRdr = objOraCom.ExecuteReader();

        while (objOraRdr.Read())
        {
            if (!objOraRdr.IsDBNull(0))
            {
                Name = objOraRdr[0].ToString().Trim();
            }
            else
            {
                Name = "";
            }
        }
        objOraRdr.Close();
        oconn.Close();
        return Name;
    }

    #endregion

    public string getTRVQuotationDetails(string quotNo,string planselected, out string gender, out double premium,    out DataTable dtQtMemDetails, out string plandesc)
    {
        string mesg = "success";
        
        gender = "";
        premium = 0;
         
        plandesc = "";
        dtQtMemDetails = null;

        try
        {
            oconn.Open();
            string getQuot = "Select m.net_premium_rs, plan , s.description" +
                              " FROM  SLIGEN.trv_quot_mast M , SLIGEn.trv_schemes S " +
                              " where refno = :quotNo and S.CODE= :plan";

            using (OracleCommand cmd = new OracleCommand(getQuot, oconn))
            {
                cmd.Parameters.Add("quotNo", OdbcType.VarChar);
                cmd.Parameters["quotNo"].Value = quotNo;

                cmd.Parameters.Add("plan", OdbcType.VarChar);
                cmd.Parameters["plan"].Value = planselected;

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
                        plandesc = quotReader.GetString(2);
                    }
                }

                quotReader.Close();
            }

            if (plan != "")
            {
                string getMainLDetails = "Select  decode(GENDER, 'F', 'Female', 'M', 'Male') GENDER " +
                                        " from SLIGEN.trv_quot_mem_details" +
                                        " where ref_no = :quotNo" +
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
                            gender = mainDetReader.GetString(0);
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
                

                string getQuotDetails = "Select REF_NO, MEM_ID, decode(MEM_TYPE,'S', 'Spouse', 'C', 'Child','O','Other') MEM_TYPE, decode(GENDER, 'F', 'Female', 'M', 'Male') GENDER, to_char(DOB, 'yyyy/mm/dd') DOB, AGE" +
                                        " from SLIGEN.trv_quot_mem_details" +
                                        " where ref_no = :quotNo" +
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
            logger.write_log("Failed at getTRVQuotationDetails: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }

        return mesg;
    }



}