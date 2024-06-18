using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
using System.IO;

/// <summary>
/// Summary description for Revival_Life
/// </summary>
public class Revival_Life
{
    public string O_nicNo { get; private set; }
    public string O_name { get; private set; }
    public string O_status { get; private set; }    
    public string O_Mesg { get; private set; }   

    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OraLifeDB"]);

	public Revival_Life()
	{ 
		//
		// TODO: Add constructor logic here
		//
	}

    public Revival_Life(string polNo)
	{
        O_Mesg = "No record found for Policy Number";
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getDetails = "select pp.nicno, ph.pnint||' '||ph.pnsur, ph.pnsta" + 
                                " from lund.polpersonal pp, lphs.phname ph, lphs.liflaps lp " +
                                " where pp.polno = ph.pnpol " + 
                                " and pp.polno = lp.lppol " +
                                " and pp.prpertype = 1 and polno = :pol";

            using (OracleCommand com = new OracleCommand(getDetails, oconn))
            {
                com.Parameters.AddWithValue("pol", polNo.Trim());

                OracleDataReader infoReader = (OracleDataReader)com.ExecuteReader();
                
                while (infoReader.Read())
                {
                    
                    if (!infoReader.IsDBNull(0))
                    {
                        O_nicNo = infoReader.GetString(0);
                    }

                    if (!infoReader.IsDBNull(1))
                    {
                        O_name = infoReader.GetString(1);
                    }

                    if (!infoReader.IsDBNull(2))
                    {
                        O_status = infoReader.GetString(2);
                    }

                    //if (!infoReader.IsDBNull(2))
                    //{
                    //    O_premium = infoReader.GetDouble(2);
                    //}

                    //if (!infoReader.IsDBNull(3))
                    //{
                    //    O_dueFrom = infoReader.GetString(3);
                    //}

                    //if (!infoReader.IsDBNull(4))
                    //{
                    //    O_mode = infoReader.GetInt32(4);
                    //}

                    //if (!infoReader.IsDBNull(5))
                    //{
                    //    O_table = infoReader.GetInt32(5);
                    //}

                    //if (!infoReader.IsDBNull(6))
                    //{
                    //    O_comDate = infoReader.GetInt32(6);
                    //}
  
                                        
                    O_Mesg = "success";
                }
                infoReader.Close();
            }
        }
        catch (Exception e)
        {
            O_Mesg = "Error occurred while retrieving information";            
        }
        finally
        {
            oconn.Close();
        }
	}
    

    public bool saveRequestData(string userName, string email, int polNo, string policyHolderName, string policyHolderNIC, string revvl_type, string mobile, string policyHolderStatus)
    {
        bool saved = false;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }


            string sql = "insert into SLIC_NET_LIFE.ONLINE_POL_REVVL_REQSTS " +
                             "(USERNAME, EMAIL_ADDRESS, POLICY_NO, POLICY_HOLDER_NAME, POLICY_HOLDER_NIC, REQUESTED_DATE, REVIVAL_TYPE, MOBILE_NO, STATUS) " +
                             " values (:oUser, :oEmail, :oPolNum,:oPolHoldName,:oPolHoldNIC, sysdate,:oRevlTyp,:oMobile,:oPolHoldStatus) ";

            using (OracleCommand com2 = new OracleCommand(sql, oconn))
            {
                OracleParameter oUsr = new OracleParameter();
                oUsr.DbType = DbType.String;
                oUsr.Value = userName;
                oUsr.ParameterName = "oUser";

                OracleParameter oEml = new OracleParameter();
                oEml.DbType = DbType.String;
                oEml.Value = email;
                oEml.ParameterName = "oEmail";

                OracleParameter oPolNo = new OracleParameter();
                oPolNo.DbType = DbType.Int32;
                oPolNo.Value = polNo;
                oPolNo.ParameterName = "oPolNum";

                OracleParameter oName = new OracleParameter();
                oName.DbType = DbType.String;
                oName.Value = policyHolderName.ToUpper();
                oName.ParameterName = "oPolHoldName";

                OracleParameter oNIC = new OracleParameter();
                oNIC.DbType = DbType.String;
                oNIC.Value = policyHolderNIC;
                oNIC.ParameterName = "oPolHoldNIC";

                OracleParameter oRvlType = new OracleParameter();
                oRvlType.DbType = DbType.String;
                oRvlType.Value = revvl_type;
                oRvlType.ParameterName = "oRevlTyp";

                OracleParameter oMobl = new OracleParameter();
                oMobl.DbType = DbType.String;
                oMobl.Value = mobile;
                oMobl.ParameterName = "oMobile";

                OracleParameter oStatus = new OracleParameter();
                oStatus.DbType = DbType.String;
                oStatus.Value = policyHolderStatus.ToUpper();
                oStatus.ParameterName = "oPolHoldStatus";

                com2.Parameters.Add(oUsr);
                com2.Parameters.Add(oEml);
                com2.Parameters.Add(oPolNo);
                com2.Parameters.Add(oName);
                com2.Parameters.Add(oNIC);
                com2.Parameters.Add(oRvlType);
                com2.Parameters.Add(oMobl);
                com2.Parameters.Add(oStatus);

                int k = com2.ExecuteNonQuery();
                com2.Parameters.Clear();

                if (k > 0)
                {
                    saved = true;
                }
            }
        }
        catch (Exception e)
        {
            saved = false;

        }
        finally
        {
            oconn.Close();
        }

        return saved;
    }    

    public bool SendRevSMS(string mobileNum, string smsText)
    {
        bool result = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string mobileNoFormatted = "";
            if (mobileNum.Substring(0, 1) == "0")
            {
                mobileNoFormatted = "94" + mobileNum.Substring(1, mobileNum.Length - 1);
            }
            else
            {
                mobileNoFormatted = mobileNum;
            }

            string sendSms = "insert into SMS.SMS_GATEWAY (APPLICATION_ID,JOB_CATEGORY,SMS_TYPE,MOBILE_NUMBER," +
                                       " TEXT_MESSAGE,SHORT_CODE) VALUES ('ONLINE_REVIVAL','CAT151','I', :mobile, :txt, 'SLIC%20LIFE') ";

            using (OracleCommand com2 = new OracleCommand(sendSms, oconn))
            {
                OracleParameter oMobile = new OracleParameter();
                oMobile.DbType = DbType.String;
                oMobile.Value = mobileNoFormatted;
                oMobile.ParameterName = "mobile";

                OracleParameter oText = new OracleParameter();
                oText.DbType = DbType.String;
                oText.Value = smsText;
                oText.ParameterName = "txt";

                com2.Parameters.Add(oMobile);
                com2.Parameters.Add(oText);

                //cmd.ExecuteNonQuery();
                //cmd.Parameters.Clear();

                int k = com2.ExecuteNonQuery();
                com2.Parameters.Clear();

                if (k > 0)
                {
                    result = true;
                }
            }
        }
        catch (Exception e)
        {
            result = false;

        }
        finally
        {
            oconn.Close();
        }

        return result;
    }

    public DataTable get15E_reqstd_list(int polno, out string mesg)
    {
        mesg = "No 15E request found";
        DataSet dsPolicies = new DataSet();
        DataTable dtPols = new DataTable();

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolicies = "select NAME from slic_net_life.pol_revival_reqstd_docs " + 
                                " where POLICY_NO = :polnum and DOC_NAME = '15E' and REQSTD_DATE is not null " + 
                                " and REQSTD_BY is not null and RECVD_DATE is null and RECVD_BY is null";

            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
            {
                dataAdd.SelectCommand.Parameters.AddWithValue("polnum", polno);
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
            mesg = "Error occured while retrieving 15E requested list";
            log logger = new log();
            logger.write_log("Failed at get15E_reqstd_list: " + e.ToString());
        }
        finally
        {
            oconn.Close();
        }

        return dtPols;
    }

    public DataTable get9E_reqstd_list(int polno, out string mesg)
    {
        mesg = "No 9E request found";
        DataSet dsPolicies = new DataSet();
        DataTable dtPols = new DataTable();

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolicies = "select NAME from slic_net_life.pol_revival_reqstd_docs " +
                                " where POLICY_NO = :polnum and DOC_NAME = '9E' and REQSTD_DATE is not null " +
                                " and REQSTD_BY is not null and RECVD_DATE is null and RECVD_BY is null";

            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
            {
                dataAdd.SelectCommand.Parameters.AddWithValue("polnum", polno);
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
            mesg = "Error occured while retrieving 9E requested list";
            log logger = new log();
            logger.write_log("Failed at get9E_reqstd_list: " + e.ToString());
        }
        finally
        {
            oconn.Close();
        }

        return dtPols;
    }

    public DataTable getJMER_reqstd_list(int polno, out string mesg)
    {
        mesg = "No JMER request found";
        DataSet dsPolicies = new DataSet();
        DataTable dtPols = new DataTable();

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolicies = "select NAME from slic_net_life.pol_revival_reqstd_docs " +
                                " where POLICY_NO = :polnum and DOC_NAME = 'JMER' and REQSTD_DATE is not null " +
                                " and REQSTD_BY is not null and RECVD_DATE is null and RECVD_BY is null";

            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
            {
                dataAdd.SelectCommand.Parameters.AddWithValue("polnum", polno);
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
            mesg = "Error occured while retrieving JMER requested list";
            log logger = new log();
            logger.write_log("Failed at getJMER_reqstd_list: " + e.ToString());
        }
        finally
        {
            oconn.Close();
        }

        return dtPols;
    }

    public bool insert_uploaded_docs(DataTable dt_recvd_docs, string username, string policyNo) 
    {
        bool returnValue = false;

        string ret = "success";      

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
                    for (int i = 0; i < dt_recvd_docs.Rows.Count; i++)
                    {
                        BinaryReader br = new BinaryReader((Stream)dt_recvd_docs.Rows[i]["doc_data"]);
                        byte[] imagecode_aaa = br.ReadBytes((Int32)((Stream)dt_recvd_docs.Rows[i]["doc_data"]).Length);

                        String block = " INSERT INTO SLIC_NET_LIFE.POL_REVIVAL_UPLOADED_DOCS " +
                                       " (USER_NAME, POLICY_NO, DOC_TYPE, PERSON_NAME, DOC_NAME, DOC_DATA, UPLOADED_DATE, DOC_APP_TYPE) " +
                                       " VALUES ('" + username + "','" + policyNo + "','" + dt_recvd_docs.Rows[i]["doc_type"].ToString() + 
                                       "','" + dt_recvd_docs.Rows[i]["person_name"].ToString().ToUpper() + "','" + 
                                       dt_recvd_docs.Rows[i]["doc_name"].ToString() + "',  :blobtodb, sysdate, '" + 
                                       dt_recvd_docs.Rows[i]["doc_app_type"].ToString() + "')";

                        cmd.CommandText = block;
                        cmd.Connection = oconn;

                        cmd.CommandType = CommandType.Text;

                        OracleParameter param = cmd.Parameters.AddWithValue(":blobtodb", imagecode_aaa);
                        param.Direction = ParameterDirection.Input;

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();


                        string insertDocAccept = "update SLIC_NET_LIFE.POL_REVIVAL_REQSTD_DOCS " +
                                            " set RECVD_DATE = sysdate, RECVD_BY = 'portal' " +
                                            " where POLICY_NO = " + policyNo + " and DOC_NAME = '" +
                                            dt_recvd_docs.Rows[i]["doc_type"].ToString() + "' and UPPER(NAME) = '" + 
                                            dt_recvd_docs.Rows[i]["person_name"].ToString().ToUpper() + "' " +
                                            " and REQSTD_DATE is not null  and REQSTD_BY is not null " +
                                            " and RECVD_DATE is null and RECVD_BY is null ";

                        cmd.CommandText = insertDocAccept;
                        cmd.Connection = oconn;

                        cmd.CommandType = CommandType.Text;                        

                        cmd.ExecuteNonQuery();
                    }

                    trans.Commit();
                    returnValue = true;
                }

            }
            catch (Exception e)
            {
                trans.Rollback();
                log logger = new log();
                logger.write_log("Failed at insert_uploaded_docs : " + e.ToString());
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

    public DataTable get15E_reqstd_list_prpertype(int polno, out string mesg)
    {
        mesg = "No 15E request found";
        DataSet dsPolicies = new DataSet();
        DataTable dtPols = new DataTable();

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolicies = "select a.NAME, b.prpertype from slic_net_life.pol_revival_reqstd_docs a, lund.polpersonal b  " +
                                "where a.POLICY_NO = b.polno and upper(a.name) = upper(b.fullname)  and  a.POLICY_NO = :polnum and a.DOC_NAME = '15E' " + 
                                " and a.REQSTD_DATE is not null and a.REQSTD_BY is not null and a.RECVD_DATE is null " + 
                                " and a.RECVD_BY is null order by b.prpertype";

            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
            {
                dataAdd.SelectCommand.Parameters.AddWithValue("polnum", polno);
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
            mesg = "Error occured while retrieving 15E requested list";
            log logger = new log();
            logger.write_log("Failed at get15E_reqstd_list: " + e.ToString());
        }
        finally
        {
            oconn.Close();
        }

        return dtPols;
    }

    public DataTable get9E_reqstd_list_prpertype(int polno, out string mesg)
    {
        mesg = "No 9E request found";
        DataSet dsPolicies = new DataSet();
        DataTable dtPols = new DataTable();

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolicies = "select a.NAME, b.prpertype from slic_net_life.pol_revival_reqstd_docs a, lund.polpersonal b " +
                                " where a.POLICY_NO = b.polno  and upper(a.name) = upper(b.fullname)  and  a.POLICY_NO = :polnum and a.DOC_NAME = '9E' and " + 
                                " a.REQSTD_DATE is not null and a.REQSTD_BY is not null and a.RECVD_DATE is null and " + 
                                " a.RECVD_BY is null order by b.prpertype";

            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
            {
                dataAdd.SelectCommand.Parameters.AddWithValue("polnum", polno);
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
            mesg = "Error occured while retrieving 9E requested list";
            log logger = new log();
            logger.write_log("Failed at get9E_reqstd_list: " + e.ToString());
        }
        finally
        {
            oconn.Close();
        }

        return dtPols;
    }

    public DataTable getJMER_reqstd_list_prpertype(int polno, out string mesg)
    {
        mesg = "No JMER request found";
        DataSet dsPolicies = new DataSet();
        DataTable dtPols = new DataTable();

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolicies = "select a.NAME, b.prpertype from slic_net_life.pol_revival_reqstd_docs a, lund.polpersonal b " +
                                " where a.POLICY_NO = b.polno  and upper(a.name) = upper(b.fullname)  and  a.POLICY_NO = :polnum and a.DOC_NAME = 'JMER' " + 
                                " and a.REQSTD_DATE is not null and a.REQSTD_BY is not null and a.RECVD_DATE is null " + 
                                " and a.RECVD_BY is null order by b.prpertype";

            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
            {
                dataAdd.SelectCommand.Parameters.AddWithValue("polnum", polno);
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
            mesg = "Error occured while retrieving JMER requested list";
            log logger = new log();
            logger.write_log("Failed at getJMER_reqstd_list: " + e.ToString());
        }
        finally
        {
            oconn.Close();
        }

        return dtPols;
    }

    public bool insert_uploaded_sigle_doc(DataTable dt_recvd_docs, string username, string policyNo)
    {
        bool returnValue = false;

        string ret = "success";

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
                    //for (int i = 0; i < dt_recvd_docs.Rows.Count; i++)
                    //{
                        BinaryReader br = new BinaryReader((Stream)dt_recvd_docs.Rows[0]["doc_data"]);
                        byte[] imagecode_aaa = br.ReadBytes((Int32)((Stream)dt_recvd_docs.Rows[0]["doc_data"]).Length);

                        String block = " INSERT INTO SLIC_NET_LIFE.POL_REVIVAL_UPLOADED_DOCS " +
                                       " (USER_NAME, POLICY_NO, DOC_TYPE, PERSON_NAME, DOC_NAME, DOC_DATA, UPLOADED_DATE, DOC_APP_TYPE) " +
                                       " VALUES ('" + username + "','" + policyNo + "','" + dt_recvd_docs.Rows[0]["doc_type"].ToString() +
                                       "','" + dt_recvd_docs.Rows[0]["person_name"].ToString().ToUpper() + "','" +
                                       dt_recvd_docs.Rows[0]["doc_name"].ToString() + "',  :blobtodb, sysdate, '" +
                                       dt_recvd_docs.Rows[0]["doc_app_type"].ToString() + "')";

                        cmd.CommandText = block;
                        cmd.Connection = oconn;

                        cmd.CommandType = CommandType.Text;

                        OracleParameter param = cmd.Parameters.AddWithValue(":blobtodb", imagecode_aaa);
                        param.Direction = ParameterDirection.Input;

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();


                        string insertDocAccept = "update SLIC_NET_LIFE.POL_REVIVAL_REQSTD_DOCS " +
                                            " set RECVD_DATE = sysdate, RECVD_BY = 'portal' " +
                                            " where POLICY_NO = " + policyNo + " and DOC_NAME = '" +
                                            dt_recvd_docs.Rows[0]["doc_type"].ToString() + "' and UPPER(NAME) = '" +
                                            dt_recvd_docs.Rows[0]["person_name"].ToString().ToUpper() + "' " +
                                            " and REQSTD_DATE is not null  and REQSTD_BY is not null " +
                                            " and RECVD_DATE is null and RECVD_BY is null ";

                        cmd.CommandText = insertDocAccept;
                        cmd.Connection = oconn;

                        cmd.CommandType = CommandType.Text;

                        cmd.ExecuteNonQuery();
                    //}

                    trans.Commit();
                    returnValue = true;
                }

            }
            catch (Exception e)
            {
                trans.Rollback();
                log logger = new log();
                logger.write_log("Failed at insert_uploaded_docs : " + e.ToString());
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

    public bool registeredUnderUserName(string username, string polno)
    {
        bool ret = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getDetails = "Select * from SLIC_NET_LIFE.POL_DET_FOR_WEB " +
                                " where POLICY_NUMBER = :oPolNum and lower(USERNAME) = :oUserName";

            using (OracleCommand com = new OracleCommand(getDetails, oconn))
            {
                OracleParameter oPol = new OracleParameter();
                oPol.DbType = DbType.Int32;
                oPol.Value = int.Parse(polno);
                oPol.ParameterName = "oPolNum";

                OracleParameter oUsr = new OracleParameter();
                oUsr.DbType = DbType.String;
                oUsr.Value = username.Trim().ToLower();
                oUsr.ParameterName = "oUserName";

                com.Parameters.Add(oPol);
                com.Parameters.Add(oUsr);

                OracleDataReader infoReader = (OracleDataReader)com.ExecuteReader();
                if (infoReader.HasRows)
                {
                    ret = true;
                }
                infoReader.Close();
            }
        }
        catch (Exception e)
        {
            O_Mesg = "Error occurred while retrieving information";
        }
        finally
        {
            oconn.Close();
        }
        return ret;
    }

    public bool AlreadyRevivalRequested(string polno)
    {
        bool ret = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getDetails = "Select * from SLIC_NET_LIFE.ONLINE_POL_REVVL_REQSTS " +
                                " where POLICY_NO = :oPolNum and COMPLETED <> 'Y'";

            using (OracleCommand com = new OracleCommand(getDetails, oconn))
            {
                OracleParameter oPol = new OracleParameter();
                oPol.DbType = DbType.Int32;
                oPol.Value = int.Parse(polno);
                oPol.ParameterName = "oPolNum";

                com.Parameters.Add(oPol);

                OracleDataReader infoReader = (OracleDataReader)com.ExecuteReader();
                if (infoReader.HasRows)
                {
                    ret = true;
                }
                infoReader.Close();
            }
        }
        catch (Exception e)
        {
            O_Mesg = "Error occurred while retrieving information";
        }
        finally
        {
            oconn.Close();
        }
        return ret;
    }

    // Updated on 21/08/2020

    public DataTable getCovid19_reqstd_list(int polno, out string mesg)
    {
        mesg = "No Covid 19 questionnaire request found";
        DataSet dsPolicies = new DataSet();
        DataTable dtPols = new DataTable();

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolicies = "select NAME from slic_net_life.pol_revival_reqstd_docs " +
                                " where POLICY_NO = :polnum and DOC_NAME = 'COVID19' and REQSTD_DATE is not null " +
                                " and REQSTD_BY is not null and RECVD_DATE is null and RECVD_BY is null";

            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
            {
                dataAdd.SelectCommand.Parameters.AddWithValue("polnum", polno);
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
            mesg = "Error occured while retrieving Covid 19 questionnaire requested list";
            log logger = new log();
            logger.write_log("Failed at getCovid19_reqstd_list: " + e.ToString());
        }
        finally
        {
            oconn.Close();
        }

        return dtPols;
    }

    public DataTable getCovid_reqstd_list_prpertype(int polno, out string mesg)
    {
        mesg = "No Covid 19 questionnaire found";
        DataSet dsPolicies = new DataSet();
        DataTable dtPols = new DataTable();

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolicies = "select a.NAME, b.prpertype from slic_net_life.pol_revival_reqstd_docs a, lund.polpersonal b " +
                                " where a.POLICY_NO = b.polno  and  a.POLICY_NO = :polnum and a.DOC_NAME = 'COVID19' " +
                                " and a.REQSTD_DATE is not null and a.REQSTD_BY is not null and a.RECVD_DATE is null " +
                                " and a.RECVD_BY is null order by b.prpertype";

            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
            {
                dataAdd.SelectCommand.Parameters.AddWithValue("polnum", polno);
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
            mesg = "Error occured while retrieving Covid 19 questionnaire requested list";
            log logger = new log();
            logger.write_log("Failed at getCovid_reqstd_list_prpertype: " + e.ToString());
        }
        finally
        {
            oconn.Close();
        }

        return dtPols;
    }

    public DataTable get12E_reqstd_list_prpertype(int polno, out string mesg)
    {
        mesg = "No 12E request found";
        DataSet dsPolicies = new DataSet();
        DataTable dtPols = new DataTable();

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolicies = "select a.NAME, b.prpertype from slic_net_life.pol_revival_reqstd_docs a, lund.polpersonal b " +
                                " where a.POLICY_NO = b.polno  and upper(a.name) = upper(b.fullname)  and  a.POLICY_NO = :polnum and a.DOC_NAME = '12E' " +
                                " and a.REQSTD_DATE is not null and a.REQSTD_BY is not null and a.RECVD_DATE is null " +
                                " and a.RECVD_BY is null order by b.prpertype";

            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
            {
                dataAdd.SelectCommand.Parameters.AddWithValue("polnum", polno);
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
            mesg = "Error occured while retrieving 12E requested list";
            log logger = new log();
            logger.write_log("Failed at get12E_reqstd_list: " + e.ToString());
        }
        finally
        {
            oconn.Close();
        }

        return dtPols;
    }

    public DataTable getRESQ_reqstd_list_prpertype(int polno, out string mesg)
    {
        mesg = "No Travel and residential questionnaire request found";
        DataSet dsPolicies = new DataSet();
        DataTable dtPols = new DataTable();

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getPolicies = "select a.NAME, b.prpertype from slic_net_life.pol_revival_reqstd_docs a, lund.polpersonal b " +
                                " where a.POLICY_NO = b.polno  and upper(a.name) = upper(b.fullname)  and  a.POLICY_NO = :polnum and a.DOC_NAME = 'RESQ' " +
                                " and a.REQSTD_DATE is not null and a.REQSTD_BY is not null and a.RECVD_DATE is null " +
                                " and a.RECVD_BY is null order by b.prpertype";

            using (OracleDataAdapter dataAdd = new OracleDataAdapter(getPolicies, oconn))
            {
                dataAdd.SelectCommand.Parameters.AddWithValue("polnum", polno);
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
            mesg = "Error occured while retrieving Travel and residential questionnaire requested list";
            log logger = new log();
            logger.write_log("Failed at getRESQ_reqstd_list: " + e.ToString());
        }
        finally
        {
            oconn.Close();
        }

        return dtPols;
    }


}