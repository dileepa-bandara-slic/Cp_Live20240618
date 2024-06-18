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
using IBM.Data.DB2.iSeries;

/// <summary>
/// Summary description for LifeCustomer
/// </summary>
public class LifeCustomer
{
    //OdbcConnection db2conn = new OdbcConnection(ConfigurationManager.AppSettings["DB2"]);
    OracleConnection oconnGen = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OraLifeDB"]);

    string domainAndPort = "www.srilankainsurance.net";
    public string name { get; private set; }
    public string address1 { get; private set; }
    public string address2 { get; private set; }
    public string address3 { get; private set; }
    public string address4 { get; private set; }
    public double sumInsured { get; private set; }
    public string tableDesc { get; private set; }
    public string term { get; private set; }
    public string mode { get; private set; }
    public string commDate { get; private set; }
    public string polStatus { get; private set; }

    public int maturity { get; private set; }
    public string maturity1 { get; private set; }

    public int pol { get; private set; }

    public string claim { get; private set; }

    public int paoad1 { get; private set; }
    public int paoad2 { get; private set; }
    public int padsja { get; private set; }
    public int ppayamt { get; private set; }
    public int pvbonus { get; private set; }
    public int pibonus { get; private set; }

    public int ploncap { get; private set; }
    public int plonint { get; private set; }
    public int dptam { get; private set; }
    public int pdprm { get; private set; }
    public int pdefprm { get; private set; }
    public int pdefint { get; private set; }


    public int lmlon { get; private set; }
    public int lmcdt { get; private set; }
    public int pdedam1 { get; private set; }
    public int pdedam2 { get; private set; }
    public int stamp_fee { get; private set; }

    public int loanNum { get; private set; }
    public string grantDate { get; private set; }
    public double grantAmt { get; private set; }
   
    public double interest { get; private set; }

    public double interst_to_pay { get; private set; }
    public double capital_to_pay { get; private set; }
    public double p_CapitalAsAtDeath { get; private set; }
    public double p_InterestAsAtDeath { get; private set; }

    public string viewStatus = "N";
    public string viewStatus1 = "N";
    public string sta { get; private set; }

    public string maturity2 { get; private set; }

    public string repay { get; private set; }
    public int cap { get; private set; }
    public int ints { get; private set; }

    public LifeCustomer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string getPolicyDetails(string polNum, GridView gvDemands, out string name, out double deposits)
    {
        string mesg = "success";
        deposits = 0;
        name = "";
        //dues = "";
        //premiumAmt = 0;
        //lateFeeAmt = 0;
        //totAmt = 0;        

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolicyDetails = "Select DEMAND, to_char(PREMIUM, '9,999,999,999.99') PREMIUM, to_char(LATEFEE, '9,999,999,999.99') LATEFEE" +
                                      " from SLIC_NET_LIFE.INFORCE_POLS_FOR_LIFE_WEB_PAY" +
                                      " where to_char(POLNO) = :polNum" +
                                      " and DEMAND not in (Select DEMAND from SLIC_NET_LIFE.PAID_POLS_FOR_LIFE_WEB_PAY where to_char(POLNO) = :polNum and PAID_DATE is not null)" +
                                      " and DEMAND is not null order by to_number(substr(DEMAND, 1, 4) || substr(DEMAND, 6, 2))";

            using (OracleCommand cmd = new OracleCommand(getPolicyDetails, oconn))
            {
                cmd.Parameters.AddWithValue("polNum", polNum);

                OracleDataReader polDetReader = cmd.ExecuteReader();
                if (polDetReader.HasRows)
                {
                    gvDemands.DataSource = polDetReader;
                    gvDemands.DataBind();
                }
                else
                {
                    gvDemands.DataSource = null;
                    gvDemands.DataBind();
                }

                polDetReader.Close();
            }

            string getName = "Select distinct NAME" +
                             " from SLIC_NET_LIFE.INFORCE_POLS_FOR_LIFE_WEB_PAY" +
                             " where to_char(POLNO) = :polNum";

            using (OracleCommand cmd = new OracleCommand(getName, oconn))
            {
                cmd.Parameters.AddWithValue("polNum", polNum);
                OracleDataReader nameReader = cmd.ExecuteReader();

                if (nameReader.HasRows)
                {
                    while (nameReader.Read())
                    {
                        if (!nameReader.IsDBNull(0))
                        { name = nameReader.GetString(0); break; }
                    }
                }
                else
                {
                    mesg = "Policy information is not available or policy is not in force.<br/>Please contact your nearest SLIC branch.";
                }
                nameReader.Close();
            }

            //string getDeposits = "Select round(sum(DPTAM), 2)" +
            //                     " from LPHS.perpetual_account" +
            //                     " where to_char(pa_pol) = :polNum";

            string getDeposits = "Select round(sum(DPTAM), 2) PAY_AMT" +
                                 " from lpay.deposit T2 " +
                                 " where to_char(T2.dppol) = :polNum" +
                                 " and T2.dpdel=0 and T2.dptam >0" +
                                 " and (T2.dpitn = 4 or T2.dpitn = 12 or T2.dpitn = 13)" +
                                 " and (T2.dpdat > TO_NUMBER(to_char(ADD_MONTHS(sysdate,-36), 'yyyymmdd')))";

            using (OracleCommand cmd = new OracleCommand(getDeposits, oconn))
            {
                cmd.Parameters.AddWithValue("polNum", polNum);
                OracleDataReader depositsReader = cmd.ExecuteReader();

                if (depositsReader.HasRows)
                {
                    while (depositsReader.Read())
                    {
                        if (!depositsReader.IsDBNull(0))
                        { deposits = depositsReader.GetDouble(0); }
                    }
                }
                depositsReader.Close();
            }

        }
        catch (Exception e)
        {
            mesg = "Error occurred while retrieving policy information" + e.ToString().Substring(0, 200);
            log logger = new log();
            logger.write_log("Failed at getPolicyDetails " + e.ToString());
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

    public bool loanExist(string polNum)
    {
        bool returnValue = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getLoanNum = "Select *" +
                                " from LPHS.LPLMAST" +
                                " where to_char(LMPOL) = :polNum AND (LMSET ='N' OR LMSET IS NULL) AND nvl(LMCD1,' ')<>'D'";

            using (OracleCommand cmd = new OracleCommand(getLoanNum, oconn))
            {
                cmd.Parameters.AddWithValue("polNum", polNum);
                OracleDataReader LoanNumReader = cmd.ExecuteReader();
                if (LoanNumReader.HasRows)
                {
                    returnValue = true;
                }
                LoanNumReader.Close();
            }
        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at loanExist: " + e.ToString());
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

    public string getLoanDetails(string loanPolNum, string flag, out string polNum, out string loanNum, out string grantDate,
                                 out double grantAmt, out string name, out string nextDueDt, out double nextDueCap, out double nextDueInt,
                                 out string lastRepaidDt, out double lastRepaidCap, out double lastRepaidInt)
    {
        string mesg = "success";
        polNum = "";
        loanNum = "";
        grantDate = "";
        grantAmt = 0;
        name = "";
        nextDueDt = "";
        nextDueCap = 0;
        nextDueInt = 0;
        lastRepaidDt = "";
        lastRepaidCap = 0;
        lastRepaidInt = 0;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getLoanDetails = "";

            if (flag == "P")
            {
                getLoanDetails = "Select to_char(LMPOL) LMPOL, to_char(to_date(LMCDT, 'yyyymmdd'), 'yyyy/mm/dd') LMCDT, LMICP, to_char(LMLON), " +
                                 " to_char(to_date(LMNID, 'yyyymmdd'), 'yyyy/mm/dd') LMNID, LMNCP, LMNIT, to_char(to_date(LMLRD, 'yyyymmdd'), 'yyyy/mm/dd') LMLRD, LMCPY, LMIPY" +
                                 " from LPHS.LPLMAST" +
                                 " where to_char(LMPOL) = :loanPolNum AND (LMSET ='N' OR LMSET IS NULL) AND nvl(LMCD1,' ')<>'D'";
            }
            else if (flag == "L")
            {
                getLoanDetails = "Select to_char(LMPOL) LMPOL, to_char(to_date(LMCDT, 'yyyymmdd'), 'yyyy/mm/dd') LMCDT, LMICP, to_char(LMLON)," +
                                 " to_char(to_date(LMNID, 'yyyymmdd'), 'yyyy/mm/dd') LMNID, LMNCP, LMNIT, to_char(to_date(LMLRD, 'yyyymmdd'), 'yyyy/mm/dd') LMLRD, LMCPY, LMIPY" +
                                 " from LPHS.LPLMAST" +
                                 " where to_char(LMLON) = :loanPolNum AND (LMSET ='N' OR LMSET IS NULL) AND nvl(LMCD1,' ')<>'D'";
            }

            using (OracleCommand cmd = new OracleCommand(getLoanDetails, oconn))
            {
                cmd.Parameters.AddWithValue("loanPolNum", loanPolNum);

                OracleDataReader loanDetReader = cmd.ExecuteReader();

                if (loanDetReader.HasRows)
                {
                    while (loanDetReader.Read())
                    {
                        if (!loanDetReader.IsDBNull(0))
                        { polNum = loanDetReader.GetString(0); }

                        if (!loanDetReader.IsDBNull(1))
                        { grantDate = loanDetReader.GetString(1); }

                        if (!loanDetReader.IsDBNull(2))
                        { grantAmt = loanDetReader.GetDouble(2); }

                        if (!loanDetReader.IsDBNull(3))
                        { loanNum = loanDetReader.GetString(3); }

                        if (!loanDetReader.IsDBNull(4))
                        { nextDueDt = loanDetReader.GetString(4); }

                        if (!loanDetReader.IsDBNull(5))
                        { nextDueCap = loanDetReader.GetDouble(5); }

                        if (!loanDetReader.IsDBNull(6))
                        { nextDueInt = loanDetReader.GetDouble(6); }

                        if (!loanDetReader.IsDBNull(7))
                        { lastRepaidDt = loanDetReader.GetString(7); }

                        if (!loanDetReader.IsDBNull(8))
                        { lastRepaidCap = loanDetReader.GetDouble(8); }

                        if (!loanDetReader.IsDBNull(9))
                        { lastRepaidInt = loanDetReader.GetDouble(9); }

                    }
                }
                else
                {
                    mesg = "Loan information is not available.<br/>Please enter correct loan number or contact your nearest SLIC branch.";
                }

                loanDetReader.Close();
            }

            if (mesg == "success" && polNum != "")
            {
                string getName = "Select pnint||' '||pnsur" +
                                 " from lphs.phname" +
                                 " where pnpol = :polNum";

                using (OracleCommand cmd = new OracleCommand(getName, oconn))
                {
                    cmd.Parameters.AddWithValue("polNum", polNum);

                    OracleDataReader nameReader = cmd.ExecuteReader();

                    if (nameReader.HasRows)
                    {
                        while (nameReader.Read())
                        {
                            if (!nameReader.IsDBNull(0))
                            { name = nameReader.GetString(0); }
                        }
                    }
                    nameReader.Close();
                }
            }

        }
        catch (Exception e)
        {
            mesg = "Error occurred while retrieving loan information";
            log logger = new log();
            logger.write_log("Failed at getLoanDetails " + e.ToString());
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

    public string getSavedPolicies(string username, GridView gridVwLife)
    {
        string mesg = "";
        try
        {
            mesg = getSavedLifePolicies(username, gridVwLife);

        }
        catch (Exception e)
        {
            mesg = "Error occurred while retrieving policy information";
        }

        return mesg;

    }

    public string getSavedLifePolicies(string username, GridView gridVw)
    {
        string mesg = "success";

        try
        {
            DataTable dt = new DataTable();


            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();
            using (cmd)
            {

                string getSavedCount = "Select POLICY_NUMBER from SLIC_NET_LIFE.POL_DET_FOR_WEB" +
                                        " where USERNAME = :userName";

                cmd.CommandText = getSavedCount;
                cmd.Parameters.AddWithValue("userName", username);
                OracleDataReader savedCntReader = cmd.ExecuteReader();

                while (savedCntReader.Read())
                {
                    DataRow dr = null;
                    dr = dt.NewRow();
                    dt.Rows.Add(dr);
                }
                savedCntReader.Close();
                cmd.Parameters.Clear();

                gridVw.DataSource = dt;
                gridVw.DataBind();

                string getSavedPols = "Select to_char(POLICY_NUMBER) from SLIC_NET_LIFE.POL_DET_FOR_WEB" +
                                  " where USERNAME = :userName" +
                                  " order by CREATED_DATE";

                cmd.CommandText = getSavedPols;
                cmd.Parameters.AddWithValue("userName", username);
                OracleDataReader savedPolReader = cmd.ExecuteReader();

                string polNum = "";
                int j = 0;
                while (savedPolReader.Read())
                {

                    if (!savedPolReader.IsDBNull(0))
                    {
                        polNum = savedPolReader.GetString(0);

                        string getPolicyDetls = "select b.pnint||' '|| b.pnsur, to_char(a.pmpol), c.tdnam, a.pmtrm," +
                                                " substr(a.pmcom, 1, 4)||'/'||substr(a.pmcom, 5, 2)||'/'||substr(a.pmcom, 7, 2) pmcom, 'Inforce' Status,b.pnsta " +
                                                " from lphs.premast a, lphs.phname b, lund.tabnam c" +
                                                " where a.pmpol = b.pnpol" +
                                                " and a.pmtbl = c.tdtabl" +
                                                " and to_char(a.pmpol) = :polNo" +
                                                " UNION" +
                                                " select b.pnint||' '|| b.pnsur, to_char(a.lppol), c.tdnam, a.lptrm," +
                                                " substr(a.lpcom, 1, 4)||'/'||substr(a.lpcom, 5, 2)||'/'||substr(a.lpcom, 7, 2) lpcom, 'Lapsed' Status,b.pnsta" +
                                                " from lphs.liflaps a, lphs.phname b, lund.tabnam c" +
                                                " where a.lppol = b.pnpol" +
                                                " and a.lptbl = c.tdtabl" +
                                                " and to_char(a.lppol) = :polNo";

                        using (OracleCommand cmd2 = new OracleCommand(getPolicyDetls, oconn))
                        {
                            cmd2.Parameters.AddWithValue("polNo", polNum);
                            OracleDataReader polDetReader = cmd2.ExecuteReader();

                            if (polDetReader.HasRows)
                            {
                                while (polDetReader.Read())
                                {
                                    Label lblName = (Label)gridVw.Rows[j].FindControl("lblName");
                                    Label lblPolNum = (Label)gridVw.Rows[j].FindControl("lblPolNum");
                                    Label lblPolTyp = (Label)gridVw.Rows[j].FindControl("lblPolTyp");
                                    Label lblTerm = (Label)gridVw.Rows[j].FindControl("lblTerm");
                                    Label lblStrtDate = (Label)gridVw.Rows[j].FindControl("lblStrtDate");
                                    Label lblStatus = (Label)gridVw.Rows[j].FindControl("lblStatus");
                                   

                                    HyperLink hyperRenew = (HyperLink)gridVw.Rows[j].FindControl("HyperLink1");
                                    HyperLink hyperViewDet = (HyperLink)gridVw.Rows[j].FindControl("HyperLink2");

                                    if (!polDetReader.IsDBNull(0))
                                    {
                                        lblName.Text = polDetReader.GetString(0);
                                    }
                                    if (!polDetReader.IsDBNull(1))
                                    {
                                        lblPolNum.Text = polDetReader.GetString(1);
                                    }
                                    if (!polDetReader.IsDBNull(2))
                                    {
                                        lblPolTyp.Text = polDetReader.GetString(2);
                                    }
                                    if (!polDetReader.IsDBNull(3))
                                    {
                                        //lblTerm.Text = polDetReader.GetInt32(3).ToString("N2"); No decimal places 
                                        lblTerm.Text = polDetReader.GetInt32(3).ToString();
                                    }
                                    if (!polDetReader.IsDBNull(4))
                                    {
                                        lblStrtDate.Text = polDetReader.GetString(4);
                                    }
                                    if (!polDetReader.IsDBNull(5))
                                    {
                                        lblStatus.Text = polDetReader.GetString(5);
                                    }
                                    if (!polDetReader.IsDBNull(6))
                                    {
                                        sta = polDetReader.GetString(6);
                                    }

                                    EncryptDecrypt en = new EncryptDecrypt();
                                    Dictionary<string, string> dc = new Dictionary<string, string>();
                                    dc.Add("PolicyNo", polDetReader.GetString(1));
                                    dc.Add("PolStatus", polDetReader.GetString(5));
                                    string linkRenew = en.url_encrypt("Renewal.aspx", dc);
                                    string linkViewDet = en.url_encrypt("ViewPolDetails.aspx", dc);

                                    hyperRenew.NavigateUrl = linkRenew;
                                    hyperViewDet.NavigateUrl = linkViewDet;

                                }
                            }
                            else
                            {
                                //gridVw.DeleteRow(j);
                                mesg = "Error occured while getting policy details. Please contact SLIC.";
                            }
                            polDetReader.Close();
                            cmd2.Parameters.Clear();

                        }

                    }
                    j++;
                }
                savedPolReader.Close();
                cmd.Parameters.Clear();
            }
        }
        catch (Exception e)
        {
            gridVw.DataSource = null;
            gridVw.DataBind();
            mesg = "Error occured while retrieving saved policy details";
            log logger = new log();
            logger.write_log("Failed at getSavedLifePolicies: " + e.ToString());
        }
        finally
        {
            oconn.Close();
        }

        return mesg;
    }

    public string getPolicies(string username, string polNumber, GridView gridVwLife)
    {
        string mesg = "";
        try
        {
            mesg = getLifePolicies(username, polNumber, gridVwLife);
        }
        catch (Exception e)
        {
            mesg = "Error occurred while retrieving policy information";
        }

        return mesg;
    }

    public string getLifePolicies(string username, string polNumber, GridView gridVw)
    {
        string mesg = "success";
        string nic = "";
        try
        {
            //  gridVw.DataSource = null;
            //  gridVw.DataBind();
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }

            string getNic = "Select NIC_NO from ULWEB.WEBUSERS where USERNAME = :username";

            using (OracleCommand com1 = new OracleCommand(getNic, oconnGen))
            {
                com1.Parameters.AddWithValue("username", username.Trim());

                OracleDataReader NicReader = (OracleDataReader)com1.ExecuteReader();

                if (NicReader.HasRows)
                {
                    while (NicReader.Read())
                    {
                        if (!NicReader.IsDBNull(0))
                        {
                            nic = NicReader.GetString(0).Trim();
                        }
                    }
                    NicReader.Close();
                }

            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while retrieving information";
            log logger = new log();
            logger.write_log("Failed at getLifePolicies-proc1: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        //put transaction
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
                string getPolicyValidNic = "Select nicno from Lund.Polpersonal" +
                                           " where to_char(polno) = :polNo" +
                                           " and prpertype = '1'";

                cmd.CommandText = getPolicyValidNic;
                cmd.Parameters.AddWithValue("polNo", polNumber);
                OracleDataReader polValidNicReader = cmd.ExecuteReader();

                if (polValidNicReader.HasRows)
                {
                    InfoValidator validator = new InfoValidator();

                    if (nic.Length == 10)
                    {
                        nic = validator.reverseNicFormat(nic);
                    }
                    while (polValidNicReader.Read())
                    {
                        if (!polValidNicReader.IsDBNull(0))
                        {
                            string polNic = polValidNicReader.GetString(0).Trim();
                            if (polNic.Length == 10)
                            {
                                polNic = validator.reverseNicFormat(polNic);
                            }
                            if (polNic != nic)
                            {
                                //mesg = "Your NIC number does not match with specified policy number.";
                                mesg = "This is to bring to your notice that we are unable to add your policy " + polNumber + " to your account, since the  NIC given in your account is unavailable or does not match with the same given for your policy. Hence, please be good enough to send scanned copy of your NIC to our email address slic_phsweb@srilankainsurance.com with mentioning your policy number to rectify this issue. ";
                            }
                        }
                        else
                        {
                            //mesg = "NIC number has not been specified for this policy number. Please contact SLIC.";
                            mesg = "This is to bring to your notice that we are unable to add your policy " + polNumber + " to your account, since the  NIC given in your account is unavailable or does not match with the same given for your policy. Hence, please be good enough to send scanned copy of your NIC to our email address slic_phsweb@srilankainsurance.com with mentioning your policy number to rectify this issue. ";
                        }
                    }
                }
                else
                {
                    mesg = "Information is not available for this policy number. Please contact SLIC.";
                }
                cmd.Parameters.Clear();

                if (mesg.Contains("success"))
                {
                    //check if policy number is already in pol_det_for_web table-- no need to map with username
                    //if it is ouput error- already added to a user
                    string getAddedPol = "Select USERNAME from SLIC_NET_LIFE.POL_DET_FOR_WEB" +
                                         " where to_char(POLICY_NUMBER) = :polNo";

                    cmd.CommandText = getAddedPol;
                    cmd.Parameters.AddWithValue("polNo", polNumber);
                    OracleDataReader addedPolReader = cmd.ExecuteReader();

                    while (addedPolReader.Read())
                    {
                        if (!addedPolReader.IsDBNull(0))
                        {
                            if (username == addedPolReader.GetString(0))
                            {
                                mesg = "This policy number is already added to your account";
                            }
                            else
                            {
                                mesg = "This policy number is already added by another user ID";
                            }
                        }
                    }
                    cmd.Parameters.Clear();
                }

                if (mesg.Contains("success"))
                {

                    bool polExist = false;
                    string getLifePolDet = "select to_char(pmpol)" +
                                            " from lphs.premast" +
                                            " where to_char(pmpol) = :polNo" +
                                            " UNION" +
                                            " select to_char(lppol)" +
                                            " from lphs.liflaps" +
                                            " where to_char(lppol) = :polNo";

                    string polNum = "";

                    cmd.CommandText = getLifePolDet;
                    cmd.Parameters.AddWithValue("polNo", polNumber);
                    OracleDataReader lifePolReader = cmd.ExecuteReader();

                    while (lifePolReader.Read())
                    {
                        polExist = true;
                        if (!lifePolReader.IsDBNull(0))
                        {
                            polNum = lifePolReader.GetString(0);
                        }
                    }
                    lifePolReader.Close();
                    cmd.Parameters.Clear();

                    if (polExist)
                    {

                        string instPolDetails = "Insert into SLIC_NET_LIFE.POL_DET_FOR_WEB(USERNAME, POLICY_NUMBER, CREATED_DATE, VIEW_STATUS, ADD_MODE)" +
                                                    " VALUES(:usernam, :polNo, sysdate, 'N', 'W')";

                        cmd.CommandText = instPolDetails;

                        OracleParameter oUser = new OracleParameter();
                        oUser.DbType = DbType.String;
                        oUser.Value = username;
                        oUser.ParameterName = "usernam";

                        OracleParameter oPol = new OracleParameter();
                        oPol.DbType = DbType.Int32;
                        oPol.Value = int.Parse(polNumber);
                        oPol.ParameterName = "polNo";

                        cmd.Parameters.Add(oUser);
                        cmd.Parameters.Add(oPol);

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        trans.Commit();

                        mesg = getSavedLifePolicies(username, gridVw);

                    }
                    else
                    {
                        mesg = "Policy information not available.<br/>Please enter correct policy number.";
                    }
                }

            }

        }
        catch (Exception e)
        {
            mesg = "Error occurred while retrieving information";
            trans.Rollback();
            log logger = new log();
            logger.write_log("Failed at getLifePolicies: " + e.ToString());
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

    public string deleteSavedPolicy(string username, GridView gridVw, int rowIndex)
    {
        string mesg = "success";
        //todo- write method to get nic seperately (for getSavedPolicies). this way getting nic for secondtime when adding new policy can be avoided.

        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        OracleCommand cmd = oconn.CreateCommand();
        OracleTransaction trans = oconn.BeginTransaction();
        cmd.Transaction = trans;

        try
        {
            Label lblPolNum = new Label();
            string deletePolicy = "";

            lblPolNum = (Label)gridVw.Rows[rowIndex].FindControl("lblPolNum");

            using (cmd)
            {
                deletePolicy = "Delete from SLIC_NET_LIFE.POL_DET_FOR_WEB" +
                                " where to_char(POLICY_NUMBER) = :polNo";

                cmd.CommandText = deletePolicy;
                cmd.Parameters.AddWithValue("polNo", lblPolNum.Text);
                //cmd.Parameters.AddWithValue("polTyp", flag);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                string instViewStatusLog = "Insert into SLIC_NET_LIFE.WEB_POL_CHANGES(POLICY_NUMBER, VIEW_STATUS, CHANGED_DATE, USERNAME)" +
                                           " Values(:polNum, 'D', sysdate, :uname)";

                cmd.CommandText = instViewStatusLog;
                OracleParameter oPol = new OracleParameter();
                oPol.DbType = DbType.String;
                oPol.Value = lblPolNum.Text;
                oPol.ParameterName = "polNum";

                OracleParameter oUser = new OracleParameter();
                oUser.DbType = DbType.String;
                oUser.Value = username;
                oUser.ParameterName = "uname";

                cmd.Parameters.Add(oPol);
                cmd.Parameters.Add(oUser);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                trans.Commit();
            }
            mesg = getSavedLifePolicies(username, gridVw);

        }
        catch (Exception e)
        {
            mesg = "Error occurred while deleting the policy";
            trans.Rollback();
            log logger = new log();
            logger.write_log("Failed at deleteSavedPolicy-Life: " + e.ToString());
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

    public string viewPolicyDetails(string polNumber, string polStat, GridView gvDemands, GridView gvDeposits)
    {
        string viewStatus = "N";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getViewStatus = "Select nvl(VIEW_STATUS, 'N')" +
                                   " from SLIC_NET_LIFE.POL_DET_FOR_WEB" +
                                   " where to_char(POLICY_NUMBER) = :polNum";

            using (OracleCommand cmd = new OracleCommand(getViewStatus, oconn))
            {
                cmd.Parameters.AddWithValue("polNum", polNumber);

                OracleDataReader viewStatReader = cmd.ExecuteReader();
                if (viewStatReader.HasRows)
                {
                    while (viewStatReader.Read())
                    {
                        if (!viewStatReader.IsDBNull(0))
                        { viewStatus = viewStatReader.GetString(0); }
                    }
                    viewStatReader.Close();
                }
            }

            if (viewStatus == "Y")
            {
                string getPolDetails = "";

                if (polStat == "Inforce")
                {
                    getPolDetails = " select pnint||' '||pnsur, pnad1, pnad2, pnad3, pnad4, pmsum, to_char(a.pmpol) pmpol, c.tdnam, to_char(a.pmtrm)," +
                                    " substr(a.pmcom, 1, 4)||'/'||substr(a.pmcom, 5, 2)||'/'||substr(a.pmcom, 7, 2) pmcom, 'Inforce' Status, decode(a.pmmod, 1, 'Yearly', 2, 'Half yearly', 3, 'Quarterly', 4, 'Monthly', 5, 'Single Premium') mode1,b.pnsta" +
                                    " from lphs.premast a, lphs.phname b, lund.tabnam c" +
                                    " where a.pmpol = b.pnpol" +
                                    " and a.pmtbl = c.tdtabl" +
                                    " and to_char(a.pmpol) = :polNum";
                }
                else if (polStat == "Lapsed")
                {
                    getPolDetails = " select pnint||' '||pnsur, pnad1, pnad2, pnad3, pnad4, lpsum, to_char(a.lppol) lppol, c.tdnam, to_char(a.lptrm)," +
                                    " substr(a.lpcom, 1, 4)||'/'||substr(a.lpcom, 5, 2)||'/'||substr(a.lpcom, 7, 2) lpcom, 'Lapsed' Status, decode(a.lpmod, 1, 'Yearly', 2, 'Half yearly', 3, 'Quarterly', 4, 'Monthly', 5, 'Single Premium') mode1,b.pnsta" +
                                    " from lphs.liflaps a, lphs.phname b, lund.tabnam c" +
                                    " where a.lppol = b.pnpol" +
                                    " and a.lptbl = c.tdtabl" +
                                    " and to_char(a.lppol) = :polNum";
                }

                using (OracleCommand cmd = new OracleCommand(getPolDetails, oconn))
                {
                    cmd.Parameters.AddWithValue("polNum", polNumber);

                    OracleDataReader polDetReader = cmd.ExecuteReader();
                    if (polDetReader.HasRows)
                    {
                        while (polDetReader.Read())
                        {
                            if (!polDetReader.IsDBNull(0))
                            { name = polDetReader.GetString(0); }
                            if (!polDetReader.IsDBNull(1))
                            { address1 = polDetReader.GetString(1); }
                            if (!polDetReader.IsDBNull(2))
                            { address2 = polDetReader.GetString(2); }
                            if (!polDetReader.IsDBNull(3))
                            { address3 = polDetReader.GetString(3); }
                            if (!polDetReader.IsDBNull(4))
                            { address4 = polDetReader.GetString(4); }
                            if (!polDetReader.IsDBNull(5))
                            { sumInsured = polDetReader.GetDouble(5); }
                            if (!polDetReader.IsDBNull(7))
                            { tableDesc = polDetReader.GetString(7); }
                            if (!polDetReader.IsDBNull(8))
                            { term = polDetReader.GetString(8); }
                            if (!polDetReader.IsDBNull(9))
                            { commDate = polDetReader.GetString(9); }
                            if (!polDetReader.IsDBNull(10))
                            { polStatus = polDetReader.GetString(10); }
                            if (!polDetReader.IsDBNull(11))
                            { mode = polDetReader.GetString(11); }
                            if (!polDetReader.IsDBNull(12))
                            { sta = polDetReader.GetString(12); }
                        }
                        polDetReader.Close();
                    }
                }

                int numInstalmnts = 12;
                /*
                if (mode == "Yearly")
                {
                    numInstalmnts = 2;
                }
                else if (mode == "Half yearly")
                {
                    numInstalmnts = 3;
                }
                else if (mode == "Quarterly")
                {
                    numInstalmnts = 5;
                }
                else if (mode == "Monthly")
                {
                    numInstalmnts = 12;
                }
                else if (mode == "Single Premium")
                {
                    numInstalmnts = 2;
                }
                */

                /* string getDemands = "select DUE_MN, PREMIUM, PAY_DATE" +
                                     " from(" +
                                     " Select substr(PDDUE, 1, 4)||'/'||substr(PDDUE, 5, 2) DUE_MN, to_char(PDPRM,'9,999,999,999.99') PREMIUM," +
                                     " decode(PDPDT, 0, 'Not Paid', substr(PDPDT, 1, 4)||'/'||substr(PDPDT, 5, 2)||'/'||substr(PDPDT, 7, 2)) PAY_DATE" +
                                     " from LPHS.DEMAND" +
                                     " where to_char(PDPOL) = :polNum" +
                                     " order by PDDUE desc) dem" +
                                     " WHERE rownum <= :installs" +
                                     " ORDER BY rownum";*/

                string getDemands = "select DUE_MN, PREMIUM, PAY_DATE" +
                                    " from(" +
                                    " Select substr(LLDUE, 1, 4)||'/'||substr(LLDUE, 5, 2) DUE_MN, to_char(LLPRM,'9,999,999,999.99') PREMIUM," +
                                    " decode(LLDAT, 0, 'Not Paid', substr(LLDAT, 1, 4)||'/'||substr(LLDAT, 5, 2)||'/'||substr(LLDAT, 7, 2)) PAY_DATE" +
                                    " from LCLM.LEDGER" +
                                    " where LLPOL = :polNum" +
                                    " order by LLDUE desc) dem" +
                                    " WHERE rownum <= :installs" +
                                    " ORDER BY rownum";

                using (OracleCommand cmd = new OracleCommand(getDemands, oconn))
                {
                    cmd.Parameters.AddWithValue("polNum", polNumber);
                    cmd.Parameters.AddWithValue("installs", numInstalmnts);

                    OracleDataReader demandReader = cmd.ExecuteReader();
                    if (demandReader.HasRows)
                    {
                        gvDemands.DataSource = demandReader;
                        gvDemands.DataBind();
                    }
                    else
                    {
                        gvDemands.DataSource = null;
                        gvDemands.DataBind();
                    }
                    demandReader.Close();
                }

                //string getDeposits = "select  to_char(PA_DAT, 'yyyy/mm/dd') PAY_DATE, to_char(PA_DTAM,'9,999,999,999.99') PAY_AMT " +
                //                    " from LPHS.PERPETUAL_ACCOUNT" +    
                //                    " where to_char(PA_POL) = :polNum" +
                //                    " and PA_DTAM <> 0" +
                //                    " order by PAY_DATE desc";

                string getDeposits = "select  to_char(to_date(DPDAT, 'yyyymmdd'), 'yyyy/mm/dd') PAY_DATE, to_char(DPTAM,'9,999,999,999.99') PAY_AMT" +
                                    " from lpay.deposit T2 " +
                                    " where to_char(T2.dppol) = :polNum" +
                                    " and T2.dpdel=0 and T2.dptam >0" +
                                    " and (T2.dpitn = 4 or T2.dpitn = 12 or T2.dpitn = 13)" +
                                    " and (T2.dpdat > TO_NUMBER(to_char(ADD_MONTHS(sysdate,-36), 'yyyymmdd')))";

                using (OracleCommand cmd = new OracleCommand(getDeposits, oconn))
                {
                    cmd.Parameters.AddWithValue("polNum", polNumber);

                    OracleDataReader depositReader = cmd.ExecuteReader();
                    if (depositReader.HasRows)
                    {
                        gvDeposits.DataSource = depositReader;
                        gvDeposits.DataBind();
                    }
                    else
                    {
                        gvDeposits.DataSource = null;
                        gvDeposits.DataBind();
                    }
                    depositReader.Close();
                }
            }
        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at viewPolicyDetails " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return viewStatus;
    }

    public string getOnlinePayments(string polNum, GridView gridView)
    {
        string message = "success";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getOnlinePayments = "select POL_NUM, PAY_AMT, ENTRY_DATE" +
                                        " from(" +
                                        " Select decode(PAY_TYPE, 'P', POL_NUM, 'L', LON_NUM) POL_NUM, to_char(PAY_AMT,'9,999,999,999.99') PAY_AMT, to_char(ENTRY_DATE, 'yyyy/mm/dd hh:mi:ss') ENTRY_DATE" +
                                        " from SLIC_NET_LIFE.RENEWAL_DETAILS" +
                                        " where to_char(POL_NUM) = :polNum" +
                                        " and STATUS = 'A'" +
                                        " order by entry_date desc) ren" +
                                        " WHERE rownum <= 10" +
                                        " ORDER BY rownum";

            using (OracleCommand cmd = new OracleCommand(getOnlinePayments, oconn))
            {
                cmd.Parameters.AddWithValue("polNum", polNum);

                OracleDataReader payDetReader = cmd.ExecuteReader();
                if (payDetReader.HasRows)
                {
                    gridView.DataSource = payDetReader;
                    gridView.DataBind();
                    gridView.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
                    gridView.HeaderRow.Cells[2].Attributes.Add("data-class", "expand");
                    gridView.HeaderRow.TableSection = TableRowSection.TableHeader;

                }
                else
                {
                    gridView.DataSource = null;
                    gridView.DataBind();
                }

                payDetReader.Close();
            }

        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at getOnlinePayments " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return message;
    }

    public string setPolicyViewStatus(string username, string polNum, string status)
    {
        string mesg = "success";

        //put transaction
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
                string updtViewStatus = "Update SLIC_NET_LIFE.POL_DET_FOR_WEB" +
                                        " set VIEW_STATUS = :status" +
                                        " where to_char(POLICY_NUMBER) = :polNum" +
                                        " and USERNAME = :uname";

                cmd.CommandText = updtViewStatus;

                OracleParameter oUser = new OracleParameter();
                oUser.DbType = DbType.String;
                oUser.Value = username;
                oUser.ParameterName = "uname";

                OracleParameter oStatus = new OracleParameter();
                oStatus.DbType = DbType.String;
                oStatus.Value = status;
                oStatus.ParameterName = "status";

                OracleParameter oPol = new OracleParameter();
                oPol.DbType = DbType.String;
                oPol.Value = polNum;
                oPol.ParameterName = "polNum";

                cmd.Parameters.Add(oUser);
                cmd.Parameters.Add(oStatus);
                cmd.Parameters.Add(oPol);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();


                string instViewStatusLog = "Insert into SLIC_NET_LIFE.WEB_POL_CHANGES(POLICY_NUMBER, VIEW_STATUS, CHANGED_DATE, USERNAME)" +
                                            "Values(:polNum, :status, sysdate, :uname)";

                cmd.CommandText = instViewStatusLog;

                OracleParameter oPol2 = new OracleParameter();
                oPol2.DbType = DbType.String;
                oPol2.Value = polNum;
                oPol2.ParameterName = "polNum";

                OracleParameter oStatus2 = new OracleParameter();
                oStatus2.DbType = DbType.String;
                oStatus2.Value = status;
                oStatus2.ParameterName = "status";

                OracleParameter oUser2 = new OracleParameter();
                oUser2.DbType = DbType.String;
                oUser2.Value = username;
                oUser2.ParameterName = "uname";

                cmd.Parameters.Add(oPol2);
                cmd.Parameters.Add(oStatus2);
                cmd.Parameters.Add(oUser2);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                trans.Commit();
            }

        }
        catch (Exception e)
        {
            mesg = "Error occurred while updating request for viewing details.";
            trans.Rollback();
            log logger = new log();
            logger.write_log("Failed at setPolicyViewStatus: " + e.ToString());
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

    public DataTable getThirdPartyPolicies(string username, out string mesg)
    {
        mesg = "No Third Party policies found";
        mesg = "No Third Party policies found";
        DataSet dsPolicies = new DataSet();
        DataTable dtPols = new DataTable();

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolicies = "Select POLICY_NUMBER, NAME, RELATIONSHIP" +
                                 " from SLIC_NET_LIFE.THIRD_PARTY_POLS where USERNAME = :userName and ACTIVE_FLAG = 'Y'";

            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
            {
                dataAdd.SelectCommand.Parameters.AddWithValue("userName", username);
                dsPolicies.Clear();
                dataAdd.Fill(dsPolicies);
            }

            if (dsPolicies != null)
            {
                dtPols = dsPolicies.Tables[0];
            }

        }
        catch (Exception e)
        {
            dtPols = null;
            mesg = "Error occured while retrieving third party policy details";
            log logger = new log();
            logger.write_log("Failed at getThirdPartyPolicies: " + e.ToString());
        }
        finally
        {
            oconn.Close();
        }

        return dtPols;
    }

    //public DataTable getRevivalPolicies(string username, out string mesg)
    //{
    //    mesg = "No policies found for revival";
    //    DataSet dsPolicies = new DataSet();
    //    DataTable dtPols = new DataTable();

    //    try
    //    {
    //        if (oconn.State != ConnectionState.Open)
    //        {
    //            oconn.Open();
    //        }

    //        string getPolicies = "Select POLICY_NUMBER, NAME, AMOUNT, EXPIRTY_DATE" +
    //                             " from SLIC_NET_LIFE.ONLINE_POLICY_REVIVALS where USERNAME = :userName and ACTIVE_FLAG = 'Y'";

    //        using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
    //        {
    //            dataAdd.SelectCommand.Parameters.AddWithValue("userName", username);
    //            dsPolicies.Clear();
    //            dataAdd.Fill(dsPolicies);
    //        }

    //        if (dsPolicies != null)
    //        {
    //            dtPols = dsPolicies.Tables[0];
    //        }

    //    }
    //    catch (Exception e)
    //    {
    //        dtPols = null;
    //        mesg = "Error occured while retrieving third party policy details";
    //        log logger = new log();
    //        logger.write_log("Failed at getThirdPartyPolicies: " + e.ToString());
    //    }
    //    finally
    //    {
    //        oconn.Close();
    //    }

    //    return dtPols;
    //}

    public DataTable getRevivalPolicies(string username, out string mesg)
    {
        mesg = "No revival policies found";
        DataSet dsPols = new DataSet();
        DataSet dsPolicies = new DataSet();
        DataTable dtPols = new DataTable();

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            //string getPolicies = "Select distinct POLNO, NAME" +
            //                     " from SLIC_NET_LIFE.ONLINE_POLICY_REVIVALS " +
            //                     " where USERNAME = :userName " +
            //                     " and ACTIVE_FLAG = 'Y' " +
            //                     " and to_number(to_char(EXPIRTY_DATE, 'YYYYMMDD')) >= to_number(to_char(SYSDATE, 'YYYYMMDD')) " +
            //                     " and COMPLETED = 'N' ";


            //using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
            //{
            //    dataAdd.SelectCommand.Parameters.AddWithValue("userName", username);
            //    dsPolicies.Clear();
            //    dataAdd.Fill(dsPolicies);
            //}

            string getPolicies = "Select distinct POLNO" + //, NAME" +
                                 " from SLIC_NET_LIFE.ONLINE_POLICY_REVIVALS " +
                                 " where USERNAME = :userName " +
                                 " and ACTIVE_FLAG = 'Y' " +
                                 " and to_number(to_char(EXPIRTY_DATE, 'YYYYMMDD')) >= to_number(to_char(SYSDATE, 'YYYYMMDD')) " +
                                 " and COMPLETED = 'N' ";


            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
            {
                dataAdd.SelectCommand.Parameters.AddWithValue("userName", username);
                dsPols.Clear();
                dataAdd.Fill(dsPols);
            }

            if (dsPols.Tables[0].Rows.Count > 0)
            {
                string getPolicies2 = "Select distinct POLNO, NAME" +
                                        " from SLIC_NET_LIFE.ONLINE_POLICY_REVIVALS " +
                                        " where USERNAME = :userName " +
                                        " and ACTIVE_FLAG = 'Y' " +
                                        " and to_number(to_char(EXPIRTY_DATE, 'YYYYMMDD')) >= to_number(to_char(SYSDATE, 'YYYYMMDD')) " +
                                        " and COMPLETED = 'N' and POLNO = :polNum and rownum = 1";

                using (OracleDataAdapter dataAdd2 = new OracleDataAdapter(getPolicies2, oconn))
                {
                    for (int i = 0; i < dsPols.Tables[0].Rows.Count; i++)
                    {
                        dataAdd2.SelectCommand.Parameters.AddWithValue("userName", username);
                        dataAdd2.SelectCommand.Parameters.AddWithValue("polNum", int.Parse(dsPols.Tables[0].Rows[i]["POLNO"].ToString()));
                        //dsPols.Clear();
                        dataAdd2.Fill(dsPolicies);
                    }
                }

                if (dsPolicies != null)
                {
                    dtPols = dsPolicies.Tables[0];
                }
            }

        }
        catch (Exception e)
        {
            dtPols = null;
            mesg = "Error occured while retrieving revival policy details";
            log logger = new log();
            logger.write_log("Failed at getRevivalPolicies: " + e.ToString());
        }
        finally
        {
            oconn.Close();
        }

        return dtPols;
    }

    public string getRevivalPolicyDetails(string polNum, string userName, GridView gvRevPolicies, out string name, out double amount_to_pay, out string expiry_date, out int seq_num, out string status)
    {
        string mesg = "success";
        amount_to_pay = 0;
        expiry_date = "";
        name = "";
        status = "";
        seq_num = 0;

        double revival_amt = 0, paid_amt = 0;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            //string getPolicyDetails = "Select NAME, to_char(AMOUNT, '9,999,999,999.99') AMOUNT, to_char(EXPIRTY_DATE, 'YYYY/MM/DD') EXPIRTY_DATE, to_char(PAID_AMOUNT, '9,999,999,999.99') PAID_AMOUNT " +
            //                          " from SLIC_NET_LIFE.ONLINE_POLICY_REVIVALS" +
            //                          " where to_char(POLNO) = :polNum" +
            //                          " and to_number(to_char(EXPIRTY_DATE, 'YYYYMMDD')) >= to_number(to_char(SYSDATE, 'YYYYMMDD'))" +
            //                          " and CREATED_DATE in " +
            //                                " (select max(CREATED_DATE) " +
            //                                " from SLIC_NET_LIFE.ONLINE_POLICY_REVIVALS " +
            //                                " where to_char(POLNO) = :polNum " +
            //                                " and to_number(to_char(EXPIRTY_DATE, 'YYYYMMDD')) >= to_number(to_char(SYSDATE, 'YYYYMMDD')))";

            /*
            string getPolicyDetails = "Select distinct POLNO, NAME" +
                                 " from SLIC_NET_LIFE.ONLINE_POLICY_REVIVALS " +
                                 " where USERNAME = :userNam " +
                                 " and ACTIVE_FLAG = 'Y' " +
                                 " and to_number(to_char(EXPIRTY_DATE, 'YYYYMMDD')) >= to_number(to_char(SYSDATE, 'YYYYMMDD')) and COMPLETED = 'N'";

            using (OracleCommand cmd = new OracleCommand(getPolicyDetails, oconn))
            {
                cmd.Parameters.AddWithValue("userNam", userName);

                OracleDataReader polDetReader = cmd.ExecuteReader();
                if (polDetReader.HasRows)
                {
                    gvRevPolicies.DataSource = polDetReader;
                    gvRevPolicies.DataBind();
                }
                else
                {
                    gvRevPolicies.DataSource = null;
                    gvRevPolicies.DataBind();
                }

                polDetReader.Close();
            }
             * */

            DataSet dsPols = new DataSet();
            DataSet dsPolicies = new DataSet();
            //DataTable dtPols = new DataTable();

            string getPolicies = "Select distinct POLNO" + //, NAME" +
                                 " from SLIC_NET_LIFE.ONLINE_POLICY_REVIVALS " +
                                 " where USERNAME = :userName " +
                                 " and ACTIVE_FLAG = 'Y' " +
                                 " and to_number(to_char(EXPIRTY_DATE, 'YYYYMMDD')) >= to_number(to_char(SYSDATE, 'YYYYMMDD')) " +
                                 " and COMPLETED = 'N' ";

            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
            {
                dataAdd.SelectCommand.Parameters.AddWithValue("userName", userName);
                dsPols.Clear();
                dataAdd.Fill(dsPols);
            }

            string getPolicies2 = "Select distinct POLNO, NAME" +
                                    " from SLIC_NET_LIFE.ONLINE_POLICY_REVIVALS " +
                                    " where USERNAME = :userName " +
                                    " and ACTIVE_FLAG = 'Y' " +
                                    " and to_number(to_char(EXPIRTY_DATE, 'YYYYMMDD')) >= to_number(to_char(SYSDATE, 'YYYYMMDD')) " +
                                    " and COMPLETED = 'N' and POLNO = :polNum and rownum = 1";

            using (OracleDataAdapter dataAdd2 = new OracleDataAdapter(getPolicies2, oconn))
            {
                for (int i = 0; i < dsPols.Tables[0].Rows.Count; i++)
                {
                    dataAdd2.SelectCommand.Parameters.AddWithValue("userName", userName);
                    dataAdd2.SelectCommand.Parameters.AddWithValue("polNum", int.Parse(dsPols.Tables[0].Rows[i]["POLNO"].ToString()));
                    //dsPols.Clear();
                    dataAdd2.Fill(dsPolicies);
                }
            }

            if (dsPolicies != null)
            {
                //dtPols = dsPolicies.Tables[0];
                gvRevPolicies.DataSource = dsPolicies.Tables[0];
                gvRevPolicies.DataBind();
            }

            //////////

            //string getName = "Select NAME, (AMOUNT - DEPOSIT_TO_USE) AS AMOUNT, to_char(EXPIRTY_DATE, 'YYYY/MM/DD') EXPIRTY_DATE, PAID_AMOUNT, SEQ_NO, STATUS " +
            string getName = "Select NAME, AMOUNT, to_char(EXPIRTY_DATE, 'YYYY/MM/DD') EXPIRTY_DATE, PAID_AMOUNT, SEQ_NO, STATUS " +
                                     " from SLIC_NET_LIFE.ONLINE_POLICY_REVIVALS" +
                                      " where to_char(POLNO) = :polNum" +
                                      " and to_number(to_char(EXPIRTY_DATE, 'YYYYMMDD')) >= to_number(to_char(SYSDATE, 'YYYYMMDD')) and COMPLETED = 'N'" +
                                      " and CREATED_DATE in " +
                                            " (select max(CREATED_DATE) " +
                                            " from SLIC_NET_LIFE.ONLINE_POLICY_REVIVALS " +
                                            " where to_char(POLNO) = :polNum " +
                                            " and to_number(to_char(EXPIRTY_DATE, 'YYYYMMDD')) >= to_number(to_char(SYSDATE, 'YYYYMMDD')) and COMPLETED = 'N')";

            using (OracleCommand cmd = new OracleCommand(getName, oconn))
            {
                cmd.Parameters.AddWithValue("polNum", polNum);
                OracleDataReader nameReader = cmd.ExecuteReader();

                if (nameReader.HasRows)
                {
                    while (nameReader.Read())
                    {
                        if (!nameReader.IsDBNull(0)) { name = nameReader.GetString(0).ToUpper(); }
                        if (!nameReader.IsDBNull(1)) { revival_amt = nameReader.GetDouble(1); }
                        if (!nameReader.IsDBNull(2)) { expiry_date = nameReader.GetString(2); }
                        if (!nameReader.IsDBNull(3)) { paid_amt = nameReader.GetDouble(3); }
                        if (!nameReader.IsDBNull(4)) { seq_num = nameReader.GetInt32(4); }
                        if (!nameReader.IsDBNull(5)) { status = nameReader.GetString(5).ToUpper(); }
                    }
                    nameReader.Close();
                }
                else
                {
                    mesg = "Policy information is not available or policy is not in force.<br/>Please contact your nearest SLIC branch.";
                }
                nameReader.Close();
            }

        }
        catch (Exception e)
        {
            mesg = "Error occurred while retrieving policy information" + e.ToString().Substring(0, 200);
            log logger = new log();
            logger.write_log("Failed at getRevivalPolicyDetails " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        if (revival_amt <= paid_amt)
        {
            amount_to_pay = 0;
        }
        else
        {
            amount_to_pay = revival_amt - paid_amt;
        }

        return mesg;
    }


    public void getmaturity(string polNumber)
    {
        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }

        //string viewStatus = "N";

        //try
        //{
            //if (oconn.State != ConnectionState.Open)
            //{
            //    oconn.Open();
            //}

            string getViewStatus = "select * from lclm.lcmmast where ppolno=:polNum";

            using (OracleCommand cmd = new OracleCommand(getViewStatus, oconn))
            {
                cmd.Parameters.AddWithValue("polNum", polNumber);

                OracleDataReader viewStatReader = cmd.ExecuteReader();
                if (viewStatReader.HasRows)
                {
                    viewStatus = "Y";
                    //while (viewStatReader.Read())
                    //{
                    //    if (!viewStatReader.IsDBNull(0))
                    //    { viewStatus = viewStatReader.GetString(0);
                    //    }
                    //}
                    viewStatReader.Close();
                }

                else {

                    viewStatus = "N";

                }
            }

            if (viewStatus == "Y")
            
            {

                //  string getViewStatus1 = "select substr(pdcom,1,4)+ pterm as m1,substr(pdcom,5,6) as m2,pclaimno,  from lclm.lcmmast where ptyp=1 and  ppolno=:polNum";

                string getViewStatus1 = "select DISTINCT(p.ppolno),substr(p.pdcom,1,4)+ p.pterm as m1,substr(p.pdcom,5,2) as m2,p.pclaimno,p.paoad1, p.paoad2, p.padsja, p.ppayamt, p.pvbonus, p.pibonus, " +
                                "p.ploncap, p.plonint,p.pdefprm,p.pdefint,p.pdedam1,p.pdedam2,p.stamp_fee,substr(p.pdcom,7,2) as m3 from lclm.lcmmast  p  " +
                                "where p.ptyp=1 and p.ppolno=:polNum and substr(p.pdcom,1,4)+ p.pterm >= 2022 and substr(p.pdcom,5,6) > 0601";

                using (OracleCommand cmd = new OracleCommand(getViewStatus1, oconn))
                {
                    cmd.Parameters.AddWithValue("polNum", polNumber);

                    OracleDataReader viewStatReader = cmd.ExecuteReader();
                    if (viewStatReader.HasRows)
                    {
                        while (viewStatReader.Read())
                        {
                            if (!viewStatReader.IsDBNull(0))

                            { pol = viewStatReader.GetInt32(0); }

                            if (!viewStatReader.IsDBNull(1))

                            { maturity = viewStatReader.GetInt32(1); }

                            if (!viewStatReader.IsDBNull(2))

                            { maturity1 = viewStatReader.GetString(2); }

                            if (!viewStatReader.IsDBNull(3))

                            { claim = viewStatReader.GetString(3); }

                            if (!viewStatReader.IsDBNull(4))

                            { paoad1 = viewStatReader.GetInt32(4); }

                            if (!viewStatReader.IsDBNull(5))

                            { paoad2 = viewStatReader.GetInt32(5); }

                            if (!viewStatReader.IsDBNull(6))

                            { padsja = viewStatReader.GetInt32(6); }

                            if (!viewStatReader.IsDBNull(7))

                            { ppayamt = viewStatReader.GetInt32(7); }

                            if (!viewStatReader.IsDBNull(8))

                            { pvbonus = viewStatReader.GetInt32(8); }

                            if (!viewStatReader.IsDBNull(9))

                            { pibonus = viewStatReader.GetInt32(9); }

                            if (!viewStatReader.IsDBNull(10))

                            { ploncap = viewStatReader.GetInt32(10); }

                            if (!viewStatReader.IsDBNull(11))

                            { plonint = viewStatReader.GetInt32(11); }

                            if (!viewStatReader.IsDBNull(12))

                            { pdefprm = viewStatReader.GetInt32(12); }

                            if (!viewStatReader.IsDBNull(13))

                            { pdefint = viewStatReader.GetInt32(13); }

                            if (!viewStatReader.IsDBNull(14))

                            { pdedam1 = viewStatReader.GetInt32(14); }

                            if (!viewStatReader.IsDBNull(15))

                            { pdedam2 = viewStatReader.GetInt32(15); }

                            if (!viewStatReader.IsDBNull(16))

                            { stamp_fee = viewStatReader.GetInt32(16); }

                        if (!viewStatReader.IsDBNull(17))

                        { maturity2 = viewStatReader.GetString(17); }

                    }
                        viewStatReader.Close();
                    }
                }

            
            string get1 = "select lmlon,lmcdt from lphs.lplmast where LMPOL=:polNum";

            using (OracleCommand cmd = new OracleCommand(get1, oconn))
            {
                cmd.Parameters.AddWithValue("polNum", polNumber);

                OracleDataReader viewStatReader1 = cmd.ExecuteReader();
                if (viewStatReader1.HasRows)
                {
                    while (viewStatReader1.Read())

                    {
                        if (!viewStatReader1.IsDBNull(0))

                        { lmlon = viewStatReader1.GetInt32(0); }

                        if (!viewStatReader1.IsDBNull(1))

                        { lmcdt = viewStatReader1.GetInt32(1); }

                    }
                    viewStatReader1.Close();
                }
            }


            string get11 = "select dptam from lpay.deposit where dppol=:polNum";

            using (OracleCommand cmd = new OracleCommand(get11, oconn))
            {
                cmd.Parameters.AddWithValue("polNum", polNumber);

                OracleDataReader viewStatReader2 = cmd.ExecuteReader();
                if (viewStatReader2.HasRows)
                {
                    while (viewStatReader2.Read())

                    {
                        if (!viewStatReader2.IsDBNull(0))

                        { dptam = viewStatReader2.GetInt32(0); }



                    }
                    viewStatReader2.Close();
                }
            }

            string get111 = "select pdprm from lphs.demand where pdpol=:polNum";

            using (OracleCommand cmd = new OracleCommand(get111, oconn))
            {
                cmd.Parameters.AddWithValue("polNum", polNumber);

                OracleDataReader viewStatReader3 = cmd.ExecuteReader();
                if (viewStatReader3.HasRows)
                {
                    while (viewStatReader3.Read())

                    {
                        if (!viewStatReader3.IsDBNull(0))

                        { pdprm = viewStatReader3.GetInt32(0); }

                    }
                    viewStatReader3.Close();
                }
            }

        }

        else
        {
                    }
    }
        

    public void getloan_det(string polNumber)
    {
        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
       
        string getViewStatus = "select * from LPHS.LPLMAST where  to_char(LMPOL)=:polNum and lmcdt > 20090501";

        using (OracleCommand cmd = new OracleCommand(getViewStatus, oconn))
        {
            cmd.Parameters.AddWithValue("polNum", polNumber);

            OracleDataReader viewStatReader = cmd.ExecuteReader();
            if (viewStatReader.HasRows)
            {
                viewStatus1 = "Y";
              
                viewStatReader.Close();
            }

            else
            {

                viewStatus1 = "N";

            }
        }

        if (viewStatus1 == "Y")
        {
            //string getLoanDetails = "Select to_char(to_date(LMCDT, 'yyyymmdd'), 'yyyy/mm/dd') LMCDT, LMICP, LMLON, " +
            //                     " lmitr,LMCPY, to_char(to_date(LMLRD, 'yyyymmdd'), 'yyyy/mm/dd') LMLRD, LMIPY" +
            //                     " from LPHS.LPLMAST" +
            //                      " where to_char(LMPOL) = :loanPolNum AND  nvl(LMCD1,' ')<>'D'";
            //" where to_char(LMPOL) = :loanPolNum AND (LMSET ='N' OR LMSET IS NULL) AND nvl(LMCD1,' ')<>'D'";

            string getLoanDetails = " Select to_char(to_date(LMCDT, 'yyyymmdd'), 'yyyy/mm/dd') LMCDT, LMICP, LMLON," +
                                    "lmitr,LMCPY, to_char(to_date(LMLRD, 'yyyymmdd'), 'yyyy/mm/dd') LMLRD, LMIPY from LPHS.LPLMAST " +
                                    " where LMCDT = (select to_number(max(to_char(to_date(LMCDT, 'yyyymmdd'), 'yyyymmdd')))" +
                                    " from LPHS.LPLMAST  where to_char(LMPOL) = :loanPolNum AND nvl(LMCD1,' ')<> 'D')" +
                                    " and to_char(LMPOL) = :loanPolNum AND nvl(LMCD2,' ')= 'N' and LMCD1 IS NULL";

            using (OracleCommand cmd = new OracleCommand(getLoanDetails, oconn))
            {
                cmd.Parameters.AddWithValue("loanPolNum", polNumber);

                OracleDataReader loanDetReader = cmd.ExecuteReader();

                if (loanDetReader.HasRows)
                {
                    while (loanDetReader.Read())
                    {

                        if (!loanDetReader.IsDBNull(0))
                        { grantDate = loanDetReader.GetString(0); }

                        if (!loanDetReader.IsDBNull(1))
                        { grantAmt = loanDetReader.GetDouble(1); }

                        if (!loanDetReader.IsDBNull(2))
                        { loanNum = loanDetReader.GetInt32(2); }

                        if (!loanDetReader.IsDBNull(3))
                        {
                            interest = loanDetReader.GetDouble(3);
                        }
                        if (!loanDetReader.IsDBNull(4))
                        {
                            cap = loanDetReader.GetInt32(4);
                        }
                        if (!loanDetReader.IsDBNull(5))
                        {
                            repay = loanDetReader.GetString(5);
                        }
                        if (!loanDetReader.IsDBNull(6))
                        {
                            ints = loanDetReader.GetInt32(6);
                        }

                    }
                }
                else
                {
                    // mesg = "Loan information is not available.<br/>Please enter correct loan number or contact your nearest SLIC branch.";
                }

                loanDetReader.Close();
            }
        }

        else {


        }
    }

    public void outstanding(int loanPolNum, int  loanNum, int interestToDate) {

      
        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }       
       
            using (OracleCommand cmd = new OracleCommand("lpay.life_functions.loanBackCalculationDisplay", oconn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("policyNumber", loanPolNum);
                cmd.Parameters.AddWithValue("loanNumber", loanNum);
                cmd.Parameters.AddWithValue("dateOfDeath", interestToDate);

                cmd.Parameters.Add("INTERESTTOBEPAID", OracleType.VarChar,50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("CAPITALTOBEPAID", OracleType.VarChar,50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("p_CapitalAsAtDeath", OracleType.VarChar,50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("p_InterestAsAtDeath", OracleType.VarChar,50).Direction = ParameterDirection.Output;

                OracleDataReader dr = cmd.ExecuteReader();

            interst_to_pay = double.Parse(cmd.Parameters["INTERESTTOBEPAID"].Value.ToString());
            capital_to_pay = double.Parse(cmd.Parameters["CAPITALTOBEPAID"].Value.ToString());
            p_CapitalAsAtDeath = double.Parse(cmd.Parameters["p_CapitalAsAtDeath"].Value.ToString());
            p_InterestAsAtDeath = double.Parse(cmd.Parameters["p_InterestAsAtDeath"].Value.ToString());


            dr.Close();

            }
        oconn.Close();
      

        //OracleCommand cmd_proc = new OracleCommand("lpay.life_functions.loanBackCalculationDisplay", oconn);

        //cmd_proc.CommandType = CommandType.StoredProcedure;

        //cmd_proc.Parameters.Clear();
        //cmd_proc.Parameters.AddWithValue("policyNumber", loanPolNum);
        //cmd_proc.Parameters.AddWithValue("loanNumber", loanNum);
        //cmd_proc.Parameters.AddWithValue("dateOfDeath", interestToDate);

        //OracleParameter vinterst_to_pay = new OracleParameter("INTERESTTOBEPAID", OracleType.VarChar, 50);
        //vinterst_to_pay.Direction = ParameterDirection.Output;
        //cmd_proc.Parameters.Add(vinterst_to_pay);

        //OracleParameter vcapital_to_pay = new OracleParameter("CAPITALTOBEPAID", OracleType.VarChar, 50);
        //vcapital_to_pay.Direction = ParameterDirection.Output;
        //cmd_proc.Parameters.Add(vcapital_to_pay);

        //OracleParameter vp_CapitalAsAtDeath = new OracleParameter("p_CapitalAsAtDeath", OracleType.VarChar, 50);
        //vp_CapitalAsAtDeath.Direction = ParameterDirection.Output;
        //cmd_proc.Parameters.Add(vp_CapitalAsAtDeath);

        //OracleParameter vp_InterestAsAtDeath = new OracleParameter("p_InterestAsAtDeath", OracleType.VarChar, 50);
        //vp_InterestAsAtDeath.Direction = ParameterDirection.Output;
        //cmd_proc.Parameters.Add(vp_InterestAsAtDeath);

        //OracleDataReader dataReader = cmd_proc.ExecuteReader(CommandBehavior.CloseConnection);

        //interst_to_pay = double.Parse(cmd_proc.Parameters["INTERESTTOBEPAID"].Value.ToString());
        //capital_to_pay = double.Parse(cmd_proc.Parameters["CAPITALTOBEPAID"].Value.ToString());
        //p_CapitalAsAtDeath = double.Parse(cmd_proc.Parameters["p_CapitalAsAtDeath"].Value.ToString());
        //p_InterestAsAtDeath = double.Parse(cmd_proc.Parameters["p_InterestAsAtDeath"].Value.ToString());
    }
 }