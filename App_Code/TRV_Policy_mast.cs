using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;

/// <summary>
/// Summary description for TRV_Policy_mast
/// </summary>
public class TRV_Policy_mast
{
    //OracleConnection objOraCon = new OracleConnection();
    //OracleCommand objOraCom = new OracleCommand();
    OracleConnection oconn = new OracleConnection();
    OracleCommand objOraCom = new OracleCommand();
    OracleConnection oconnect = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);

    public string polNO { get; private set; }
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
    public List<TRV_Policy_mem> members = new List<TRV_Policy_mem>();// {get; private set;}
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

    public double USD_RATE { get; private set; }
    public double FINAL_PREMIUM_USD { get; private set; }
    public string IP_ADDRESS { get; private set; }
    public string ENTERED_BY { get; private set; }
    public int AGENT_CODE { get; private set; }
    public int DEBTOR_CODE { get; private set; }

    public string CURR_TYPE { get; private set; }

    public string CLIENT_ID { get; private set; }

    public string TRV_TYPE { get; set; }
    public int SLI_NO1 { get; set; }
    public int SLI_NO2 { get; set; }

    public int SER_BRCD { get; set; }

    public int SCH_PRINT { get; set; }

    public int SCH_BRANCH { get; set; }

    public string SVAT_NO { get; set; }

    public double SVAT_AMOUNT { get; set; }
    public string VAT_WAVED { get; set; }

    public int NO_OF_DAYS { get; set; }

    public string AGNT_RATE_CODE { get; set; }

    public ArrayList arrDest = new ArrayList();

    public string vat_No { get; set; }

    public double DISC_RATE { get; set; }
    public double DISC_AMNT { get; set; }


    DataManager dm = new DataManager();
    private DataTable dtRefData;

    //public string minAge { get; set; }
    //public string maxAge { get; set; }
    //public int maxDays { get; set; }

    public TRV_Policy_mast()
    {
       
    }
    //public void connectDB()
    //{
    //    objOraCon.ConnectionString = ConfigurationManager.AppSettings.Get("OracleDB").ToString();
    //    if (objOraCon.State != ConnectionState.Open)
    //        objOraCon.Open();
    //}
    //public void disconnectDB()
    //{
    //    if (objOraCon.State != ConnectionState.Closed)
    //    {
    //        objOraCon.Close();
    //    }
    //}

    public void connectDB()
    {
        oconn.ConnectionString = ConfigurationManager.AppSettings.Get("OracleDB").ToString();
        if (oconn.State != ConnectionState.Open)
            oconn.Open();
    }
    public void disconnectDB()
    {
        if (oconn.State != ConnectionState.Closed)
        {
            oconn.Close();
        }
    }

    public TRV_Policy_mast(string pNO,string POL_TYPE)
    {

        string sql = "Select POLNO, DESTINATION, to_char(DEPART_DATE , 'yyyy/mm/dd') AS DEPART_DATE, to_char(RETURN_DATE , 'yyyy/mm/dd') AS RETURN_DATE, " +
                     " VISIT_CTRY1, VISIT_CTRY2, VISIT_CTRY3, VISIT_CTRY4, TRAVEL_PURPOSE, CONTCT_NAME, CONTCT_ADRS1, CONTCT_ADRS2, CONTCT_ADRS3, CONTCT_ADRS4, " +
                     " CONTCT_NO1, CONTCT_NO2, TRV_TYPE, FULL_NAME, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, MOBILE_NUMBER, HOME_NUMBER, OFFICE_NUMBER, PLAN, " +
                     " TITLE, NUM_OF_PERSONS, GROUP_DISC_RATE, NET_PREMIUM_USD, ADMIN_FEE_RS, POLICY_FEE_RS, NBT_RS, VAT_RS, FINAL_PREMIUM_RS, TAXES_EXPENSES_RS, " +
                     " SUM_INS_USD, NET_PREMIUM_RS, to_char(ENTERED_DATE , 'yyyy/mm/dd') AS ENTERED_DATE,USD_RATE,FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE," +
                     " DEBTOR_CODE,CURR_TYPE,CLIENT_ID,TRV_TYPE,SLI_NO1,SLI_NO2,SER_BRCD,SCH_PRINT,NVL((SCH_BRANCH),0) AS SCH_BRANCH, " +
                     " SVAT_NO, SVAT_AMOUNT,VAT_WAVED,NVL((NO_OF_DAYS),0) AS NO_OF_DAYS,AGNT_RATE_CODE,vat_No,NVL((DISC_RATE),0) AS DISC_RATE,NVL((DISC_AMNT),0) AS DISC_AMNT FROM SLIGEN.TRV_POL_MAST" +
                     " WHERE POLNO = '" + pNO.Trim() + "' AND POL_TYPE='" + POL_TYPE + "'";

        if (dm.existRecored(sql) > 0)
        {
            DataSet ds = new DataSet();
            ds = dm.getrow(sql);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                polNO = pNO.Trim();
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
                add_1 = row[18].ToString().TrimEnd(',');
                add_2 = row[19].ToString().TrimEnd(',');
                add_3 = row[20].ToString().TrimEnd(',');
                add_4 = row[21].ToString().TrimEnd(',');
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
                USD_RATE = Convert.ToDouble(row[39].ToString().Trim());
                FINAL_PREMIUM_USD = Convert.ToDouble(row[40].ToString().Trim());
                IP_ADDRESS = row[41].ToString().Trim();
                ENTERED_BY = row[42].ToString().Trim();
                AGENT_CODE = Convert.ToInt32(row[43].ToString().Trim());
                DEBTOR_CODE = Convert.ToInt32(row[44].ToString().Trim());
                CURR_TYPE = row[45].ToString().Trim();
                CLIENT_ID = row[46].ToString().Trim();
                TRV_TYPE = row[47].ToString().Trim();
                SLI_NO1 = Convert.ToInt32(row[48].ToString().Trim());
                SLI_NO2 = Convert.ToInt32(row[49].ToString().Trim());
                SER_BRCD = Convert.ToInt32(row[50].ToString().Trim());
                SCH_PRINT = Convert.ToInt32(row[51].ToString().Trim());
                SCH_BRANCH = Convert.ToInt32(row[52].ToString().Trim());
                SVAT_NO = row[53].ToString().Trim();
                SVAT_AMOUNT = Convert.ToDouble(row[54].ToString().Trim());
                VAT_WAVED = row[55].ToString().Trim();
                NO_OF_DAYS = Convert.ToInt32(row[56].ToString().Trim());
                AGNT_RATE_CODE = row[57].ToString().Trim();
                vat_No = row[58].ToString().Trim();

                DISC_RATE = Convert.ToDouble(row[59].ToString().Trim());
                DISC_AMNT = Convert.ToDouble(row[60].ToString().Trim());

                if (destination.Contains(","))
                {
                    string [] dest = destination.Split(',');

                    for (int i = 0; i < dest.Length; i++)
                    {
                        arrDest.Add(get_country_name(dest[i]));
                    }
                }
                else
                {
                    if(!String.IsNullOrEmpty(destination))
                    {
                        arrDest.Add(get_country_name(destination));
                    }
                }
            }
            this.get_members(pNO);
        }
        //polNO = pNO;
        dm.connclose();
    }

    

    public void getVatTypes(DropDownList dropDownList3)
    {
        DataTable dtTrPurposes = new DataTable();
        try
        {
            oconn.Open();
            string GetVatTypes = "SELECT '0' code ,'--- SELECT VAT ---' as description from dual union select code, description from sligen.trv_vat_types";


            using (OracleDataAdapter adapter = new OracleDataAdapter(GetVatTypes, oconn))
            {
                adapter.Fill(dtTrPurposes);

                dropDownList3.DataSource = dtTrPurposes;
                dropDownList3.DataTextField = "Description";
                dropDownList3.DataValueField = "Code";
                dropDownList3.DataBind();
            }

        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at getVatTypes: " + e.ToString());
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }
    }

    private void get_members(string ref_id)
    {
        TRV_Policy_mem mem;
        string sql = "select MEM_ID from SLIGEN.TRV_POL_MEM_DETAILS WHERE POL_NO = '" + ref_id.Trim() + "'  ";
        if (dm.existRecored(sql) > 0)
        {
            DataSet ds = new DataSet();
            ds = dm.getrow(sql);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                string member_id = row[0].ToString().Trim();
                mem = new TRV_Policy_mem(member_id, ref_id);
                members.Add(mem);
            }
        }
        dm.connclose();
    }

    public string get_country_name(string cid)
    {
        string result = "";

        string sql = "select COUNTRY_NAME from SLIGEN.TRV_COUNTRIES  WHERE UPPER(TRIM(COUNTRY_ID)) = '" + cid.Trim().ToUpper() + "'  ";
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

        string sql = "select DESCRIPTION from SLIGEN.TRV_SCHEMES  WHERE UPPER(TRIM(CODE)) = '" + cid.Trim().ToUpper() + "'  ";
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


    public string getFullAddress2()
    {
        string address = "";

        address = add_1;
        if (add_2 != "")
        {
            address = address + ", " + add_2;
        }
        if (add_3 != "")
        {
            address = address + ", " + add_3;
        }
        if (add_4 != "")
        {
            address = address + ", " + add_4;
        }

        return address;
    }

    public string loadClientType(string clientMod)
    {
        string ClientType = "";
        //if (oconn.State != ConnectionState.Open)
        //{
        //    oconn.Open();
        //}
        connectDB();
        OracleCommand cmd = oconn.CreateCommand();
        OracleTransaction trans = oconn.BeginTransaction();
        cmd.Transaction = trans;

        try
        {
            using (cmd)
            {
                cmd.CommandText = "select * from CLIENTDB.CORPORATE_CUSTOMER where CUSTOMER_ID =  '" + clientMod + "'";
                OracleDataReader drr = cmd.ExecuteReader();

                if (drr.HasRows)
                {
                    ClientType = "C";

                }
                else
                {

                    cmd.CommandText = "select * from CLIENTDB.PERSONAL_CUSTOMER where CUSTOMER_ID  = '" + clientMod + "'";
                    OracleDataReader drrr = cmd.ExecuteReader();

                    if (drrr.HasRows)
                    {
                        ClientType = "P";

                    }

                }
                //return ClientType;
            }
        }
        catch (Exception e)
        {
            trans.Rollback();
        }
        finally
        {
            //if (oconn.State == ConnectionState.Open)
            //{
            //    oconn.Close();
            //}
            disconnectDB();
        }

        return ClientType;
    }

    public string getAgtName(int agtcd, out string Name)
    {
        oconnect.Open();
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
                Name = objOraRdr[0].ToString();
            }
            else
            {
                Name = "";
            }
        }
        objOraRdr.Close();
        oconnect.Close();
        return Name;
    }

    //public void getLimits(string scheme, string PolType)
    //{
    //    //DateTime fromDt = DateTime.ParseExact(fromdate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
    //    //DateTime toDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture), "yyyy/MM/dd", CultureInfo.InvariantCulture);
    //    //int months = ((toDate.Year - fromDt.Year * 12) + (toDate.Month - fromDt.Month));

    //    //// substract 1 month if end month is not completed
    //    //if (toDate.Day < fromDt.Day)
    //    //{
    //    //    months--;
    //    //}

    //    //double totalyears = months / 12d;
    //    //double Calcage = Math.Round(totalyears);


    //    try
    //    {

    //        this.connectDB();
    //        string getSumAssured = "select MIN_AGE_MON,MAX_AGE,MAX_TRAV_DAYS FROM sligen.trv_schemes where UPPER(TRIM(CODE)) = '" + scheme.ToUpper() + "' and POL_TYPE='" + PolType + "'";

    //        using (OracleCommand cmd = new OracleCommand(getSumAssured, oconnect))
    //        {
    //            OracleDataReader cntReader = cmd.ExecuteReader();

    //            while (cntReader.Read())
    //            {
    //                if (!cntReader.IsDBNull(0))
    //                {
    //                    minAge = cntReader.GetDouble(0).ToString();
    //                }
    //                if (!cntReader.IsDBNull(1))
    //                {
    //                    maxAge = cntReader.GetDouble(1).ToString();
    //                }
    //                if (!cntReader.IsDBNull(2))
    //                {
    //                    maxDays = int.Parse(cntReader.GetDouble(2).ToString());
    //                }
    //            }
    //            cntReader.Close();
    //        }

    //    }
    //    catch (Exception e)
    //    {

    //        log logger = new log();
    //        logger.write_log("Failed at Get Limits: " + e.ToString());
    //    }
    //    finally
    //    {
    //        this.disconnectDB();
    //    }


    //}
}