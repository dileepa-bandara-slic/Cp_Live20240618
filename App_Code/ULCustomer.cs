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
using System.Data.Odbc;
using IBM.Data.DB2.iSeries;
using System.Collections.Generic;
using System.Globalization;
/// <summary>
/// Summary description for ULCustomer
/// </summary>

public class ULCustomer
{
   // OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["DBConString"].ConnectionString);
    OdbcConnection db2conn = new OdbcConnection(ConfigurationManager.AppSettings["DB2"]);
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    OracleConnection oconnLife = new OracleConnection(ConfigurationManager.AppSettings["OraLifeDB"]);

    string domainAndPort = "www.srilankainsurance.net";
      

    public ULCustomer()
    {
        //
        // TODO: Add constructor logic here
        //

    }

    public bool RegisterCustomer(   string userName, string password, string title, string firstName, string lastname, string otherNames,
                                    string nic, int dateOfBirth, string gender, string addrss1, string addrss2, string addrss3, string addrss4,
                                    string city, string postalCode, string country, string email, string mobileNo, string homeNo, string ofcNo, 
                                    string job, string isSrilankan, string passportNo, DateTime createDate)
    {
        bool returnValue = false;
        //if (userName.Length <= 50 && password.Length <= 50) -- All validations done seperately
        // {  
        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        OracleCommand cmd = oconn.CreateCommand();
        OracleTransaction trans = oconn.BeginTransaction();
        cmd.Transaction = trans;

        try
        {
            //db2conn.Open();
            int count2 = 0;
            int cntGen = 0;
            string hashedUser = "";

            using (cmd)
            {
                string registerUser = "Insert into ULWEB.webusers(USR_SEQ_ID, USERNAME, PASWORD, FIRST_NAME, LAST_NAME, OTHER_NAMES, TITLE, NIC_NO, DATE_OF_BIRTH, GENDER," +
                                                                " ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, TOWN_CITY, POSTAL_CODE, COUNTRY, EMAIL, MOBILE_NUMBER," +
                                                                " HOME_NUMBER, OFFICE_NUMBER, DESIGNATION, LANKAN_FLAG, PASSPORT_NO, created_date) values "+
                                                                " (ulweb.web_01.nextval, :username, :password, :firstname, :lastname, :othernames, :title, :nicNo, "+
                                                                " :dateofbirth, :gender, :address1, :address2, :address3, :address4, :town, :postCode, :country, :email, "+
                                                                " :mobileNo, :homeNo, :officeNo, :designation, :islankan, :passport, :crtDate)";

                cmd.CommandText = registerUser;

                OracleParameter oUsername = new OracleParameter();
                oUsername.DbType = DbType.String;
                oUsername.Value = userName;
                oUsername.ParameterName = "username";

                OracleParameter oPassword = new OracleParameter();
                oPassword.DbType = DbType.String;
                oPassword.Value = Hash.Hash.Get_SHA512(password);
                oPassword.ParameterName = "password";

                OracleParameter oFirstName = new OracleParameter();
                oFirstName.DbType = DbType.String;
                oFirstName.Value = firstName;
                oFirstName.ParameterName = "firstname";

                OracleParameter oLastname = new OracleParameter();
                oLastname.DbType = DbType.String;
                oLastname.Value = lastname;
                oLastname.ParameterName = "lastname";

                OracleParameter oOtherNames = new OracleParameter();
                oOtherNames.DbType = DbType.String;
                oOtherNames.Value = otherNames;
                oOtherNames.ParameterName = "othernames";

                OracleParameter oTitle = new OracleParameter();
                oTitle.DbType = DbType.String;
                oTitle.Value = title;
                oTitle.ParameterName = "title";

                OracleParameter oNic = new OracleParameter();
                oNic.DbType = DbType.String;
                oNic.Value = nic.ToUpper();
                oNic.ParameterName = "nicNo";

                OracleParameter oDateOfBirth = new OracleParameter();
                oDateOfBirth.DbType = DbType.String;
                oDateOfBirth.Value = dateOfBirth;
                oDateOfBirth.ParameterName = "dateofbirth";

                OracleParameter oGender = new OracleParameter();
                oGender.DbType = DbType.String;
                oGender.Value = gender;
                oGender.ParameterName = "gender";

                OracleParameter oAddrss1 = new OracleParameter();
                oAddrss1.DbType = DbType.String;
                oAddrss1.Value = addrss1;
                oAddrss1.ParameterName = "address1";

                OracleParameter oAddrss2 = new OracleParameter();
                oAddrss2.DbType = DbType.String;
                oAddrss2.Value = addrss2;
                oAddrss2.ParameterName = "address2";

                OracleParameter oAddrss3 = new OracleParameter();
                oAddrss3.DbType = DbType.String;
                oAddrss3.Value = addrss3;
                oAddrss3.ParameterName = "address3";

                OracleParameter oAddrss4 = new OracleParameter();
                oAddrss4.DbType = DbType.String;
                oAddrss4.Value = addrss4;
                oAddrss4.ParameterName = "address4";

                OracleParameter oCity = new OracleParameter();
                oCity.DbType = DbType.String;
                oCity.Value = city;
                oCity.ParameterName = "town";

                OracleParameter oPostalCode = new OracleParameter();
                oPostalCode.DbType = DbType.String;
                oPostalCode.Value = postalCode;
                oPostalCode.ParameterName = "postCode";

                OracleParameter oCountry = new OracleParameter();
                oCountry.DbType = DbType.String;
                oCountry.Value = country;
                oCountry.ParameterName = "country";

                OracleParameter oEmail = new OracleParameter();
                oEmail.DbType = DbType.String;
                oEmail.Value = email.ToLower();
                oEmail.ParameterName = "email";

                OracleParameter oMobileNo = new OracleParameter();
                oMobileNo.DbType = DbType.String;
                oMobileNo.Value = mobileNo;
                oMobileNo.ParameterName = "mobileNo";

                OracleParameter oHomeNo = new OracleParameter();
                oHomeNo.DbType = DbType.String;
                oHomeNo.Value = homeNo;
                oHomeNo.ParameterName = "homeNo";

                OracleParameter oOfficeNo = new OracleParameter();
                oOfficeNo.DbType = DbType.String;
                oOfficeNo.Value = ofcNo;
                oOfficeNo.ParameterName = "officeNo";

                OracleParameter oDesignation = new OracleParameter();
                oDesignation.DbType = DbType.String;
                oDesignation.Value = job;
                oDesignation.ParameterName = "designation";

                OracleParameter oIsSrilankan = new OracleParameter();
                oIsSrilankan.DbType = DbType.String;
                oIsSrilankan.Value = isSrilankan;
                oIsSrilankan.ParameterName = "islankan";

                OracleParameter oPassportNo = new OracleParameter();
                oPassportNo.DbType = DbType.String;
                oPassportNo.Value = passportNo;
                oPassportNo.ParameterName = "passport";

                OracleParameter ocrtDate = new OracleParameter();
                ocrtDate.DbType = DbType.DateTime;
                ocrtDate.Value = createDate;
                ocrtDate.ParameterName = "crtDate";

                cmd.Parameters.Add(oUsername);
                cmd.Parameters.Add(oPassword);
                cmd.Parameters.Add(oFirstName);
                cmd.Parameters.Add(oLastname);
                cmd.Parameters.Add(oOtherNames);
                cmd.Parameters.Add(oTitle);
                cmd.Parameters.Add(oNic);
                cmd.Parameters.Add(oDateOfBirth);
                cmd.Parameters.Add(oGender);
                cmd.Parameters.Add(oAddrss1);
                cmd.Parameters.Add(oAddrss2);
                cmd.Parameters.Add(oAddrss3);
                cmd.Parameters.Add(oAddrss4);
                cmd.Parameters.Add(oCity);
                cmd.Parameters.Add(oPostalCode);
                cmd.Parameters.Add(oCountry);
                cmd.Parameters.Add(oEmail);
                cmd.Parameters.Add(oMobileNo);
                cmd.Parameters.Add(oHomeNo);
                cmd.Parameters.Add(oOfficeNo);
                cmd.Parameters.Add(oDesignation);
                cmd.Parameters.Add(oIsSrilankan);
                cmd.Parameters.Add(oPassportNo);
                cmd.Parameters.Add(ocrtDate);

                int count = cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();

                if (count > 0)
                {
                    string setRegisterLink = "Insert into ULWEB.WEBUSERS_PREREG(USERNAME, REG_CODE)values (:username, :regcode)";

                    cmd.CommandText = setRegisterLink;

                    hashedUser = Hash.Hash.Get_SHA512(userName.Trim());//Hasher.HashString(username.Trim());

                    OracleParameter oUserName = new OracleParameter();
                    oUserName.DbType = DbType.String;
                    oUserName.Value = userName;
                    oUserName.ParameterName = "username";

                    OracleParameter oRegcode = new OracleParameter();
                    oRegcode.DbType = DbType.String;
                    oRegcode.Value = hashedUser;
                    oRegcode.ParameterName = "regcode";

                    cmd.Parameters.Add(oUserName);
                    cmd.Parameters.Add(oRegcode);

                    count2 = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    string insertGenUserInfo = "Insert into SLIC_NET.USER_NOTIFY_INFO(USERNAME, EMAIL, MOBILE_NO) VALUES (:username, :email, :mobileNum)";

                    cmd.CommandText = insertGenUserInfo;

                    OracleParameter oUsrName = new OracleParameter();
                    oUsrName.DbType = DbType.String;
                    oUsrName.Value = userName;
                    oUsrName.ParameterName = "username";

                    OracleParameter oEmal = new OracleParameter();
                    oEmal.DbType = DbType.String;
                    oEmal.Value = email.ToLower();
                    oEmal.ParameterName = "email";

                    OracleParameter oMobileNum = new OracleParameter();
                    oMobileNum.DbType = DbType.String;
                    oMobileNum.Value = mobileNo;
                    oMobileNum.ParameterName = "mobileNum";

                    cmd.Parameters.Add(oUsrName);
                    cmd.Parameters.Add(oEmal);
                    cmd.Parameters.Add(oMobileNum);

                    cntGen = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    
                }
            }

            if (oconnLife.State != ConnectionState.Open)
            {
                oconnLife.Open();
            }
            string insertLifeUserInfo = "Insert into SLIC_NET_LIFE.USER_NOTIFY_INFO(USERNAME, EMAIL, MOBILE_NO) VALUES (:username, :email, :mobileNum)";

            int cntLife = 0;
            using (OracleCommand cmd2 = new OracleCommand(insertLifeUserInfo, oconnLife))
            {
                cmd2.Parameters.AddWithValue("username", userName);
                cmd2.Parameters.AddWithValue("email", email.ToLower());
                cmd2.Parameters.AddWithValue("mobileNum", mobileNo);
                
                cntLife = cmd2.ExecuteNonQuery();
            }

            if (count2 > 0 && cntLife > 0 && cntGen > 0)
            {
                string subject = "SLIC Online- Registration service";
                string content1 = "Please copy the url and navigate to it using a browser. https://" + domainAndPort + "/ConfirmRegister.aspx?regtokn=" + hashedUser +
                                    "\"> https://" + domainAndPort + "/ConfirmRegister.aspx?regtokn=" + hashedUser;
                string content2 = "Please click on following link to complete your registration." +
                                    "<br/><br/><a href=\"https://" + domainAndPort + "/ConfirmRegister.aspx?regtokn=" + hashedUser +
                                    "\"> https://" + domainAndPort + "/ConfirmRegister.aspx?regtokn=" + hashedUser + "</a>";

                Db_Email emailSender = new Db_Email();
                returnValue = emailSender.send_html_email(email, subject, content1, content2);
                LogMail logger = new LogMail();
                logger.write_log("To: " + email + " Subject: " + subject);
            }

            trans.Commit();
        }
        catch (Exception e)
        {
            trans.Rollback();
            returnValue = false;
            log logger = new log();
            logger.write_log("Failed at ULCustomer- RegisterCustomer: " + e.ToString());
        }
        finally
        {            
            if (oconnLife.State == ConnectionState.Open)
            {
                oconnLife.Close();
            }
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return returnValue;
    }

    public bool ValidateLogin(string userName, string password, int failedLoginCount)
    {
        bool returnValue = false;

        if (userName.Length <= 15 && password.Length <= 15)
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
                    string getUser = "select count(*) from ULWEB.WEBUSERS where UPPER(USERNAME) = :username and PASWORD = :password and ACTIVE_FLAG = 'Y' and USERTYPE = 0";

                    cmd.CommandText = getUser;

                    OracleParameter oUsrName = new OracleParameter();
                    oUsrName.DbType = DbType.String;
                    oUsrName.Value = userName.ToUpper();
                    oUsrName.ParameterName = "username";

                    OracleParameter oPassword = new OracleParameter();
                    oPassword.DbType = DbType.String;
                    oPassword.Value = Hash.Hash.Get_SHA512(password);
                    oPassword.ParameterName = "password";

                    cmd.Parameters.Add(oUsrName);
                    cmd.Parameters.Add(oPassword);

                    int count = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    if (count > 0)
                    {
                        string setLoginDate = "Update ULWEB.WEBUSERS set LAST_LOGIN_DATE = sysdate where UPPER(USERNAME) = :username";
                        cmd.CommandText = setLoginDate;

                        OracleParameter oUserName = new OracleParameter();
                        oUserName.DbType = DbType.String;
                        oUserName.Value = userName.ToUpper();
                        oUserName.ParameterName = "username";

                        cmd.Parameters.Add(oUserName);

                        int count1 = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        if (count1 > 0)
                        {
                            returnValue = true;
                        }


                        if (failedLoginCount > 0)
                        {
                            string setLoginCountZero = "Update ULWEB.WEBUSERS set FAILED_LOGIN_COUNT = 0 where UPPER(USERNAME) = :username";

                            cmd.CommandText = setLoginCountZero;

                            OracleParameter oUserNam = new OracleParameter();
                            oUserNam.DbType = DbType.String;
                            oUserNam.Value = userName.ToUpper();
                            oUserNam.ParameterName = "username";

                            cmd.Parameters.Add(oUserNam);

                            int count2 = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            if (count2 < 0)
                            {
                                log logger = new log();
                                logger.write_log("Failed Webusers update at ValidateLogin");
                            }

                        }
                    }
                    else
                    {
                        returnValue = false;
                        string setLoginCount = "Update ULWEB.WEBUSERS set FAILED_LOGIN_COUNT = (NVL(FAILED_LOGIN_COUNT,0) + 1), LAST_FAILED_DATE = sysdate where UPPER(USERNAME) = :username and ACTIVE_FLAG = 'Y' and USERTYPE = 0";

                        cmd.CommandText = setLoginCount;

                        OracleParameter oUser = new OracleParameter();
                        oUser.DbType = DbType.String;
                        oUser.Value = userName.ToUpper();
                        oUser.ParameterName = "username";

                        int count3 = cmd.ExecuteNonQuery();

                        if (count3 < 0)
                        {
                            // should log error - Failed login count not updated for some reason.
                        }

                        //log lg = new log();
                        log logger = new log();
                        logger.write_log("login failed for " + userName);

                    }

                }
                trans.Commit();
            }

                //  trans.Commit();

            catch (Exception e)
            {
                trans.Rollback();
                returnValue = false;// Log your error
                log logger = new log();
                logger.write_log("Failed at ULCustomer - ValidateLogin1: " + e.ToString());
            }
            finally
            {
                
                if (oconn.State == ConnectionState.Open)
                {
                    oconn.Close();
                }
            }
        }
        else
        {
            // Log error - user name not alpha-numeric or 
            // username or password exceed the length limit!
        }

        return returnValue;
    }

    public int ValidateLogin(string userName, string password, int failedLoginCount, string server_code, string client_code)
    {

        int returnValue = -1;
        string pwd_db = "";
        string hashed_pwd_db = "";
        string hashed_all = "";
        string sentPwd = "";
        InfoValidator validator = new InfoValidator();

        log logger = new log();

        if (userName.Length <= 15)
        {
            string salt = this.get_created_date(userName);

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

                    string get_nic = "select NIC_NO, PASSPORT_NO, MOBILE_NUMBER from ULWEB.WEBUSERS where UPPER(USERNAME) = :username";
                    string nic = "";
                    string ppno = "";
                    string mobileno = "";
                    cmd.CommandText = get_nic;
                    cmd.Parameters.AddWithValue("username", userName.ToUpper());

                    OracleDataReader nicReader = cmd.ExecuteReader();
                    if (nicReader.HasRows)
                    {                        
                        while (nicReader.Read())
                        {
                            if (!nicReader.IsDBNull(0))
                            {
                                nic = nicReader.GetString(0);                                
                            }
                            if (!nicReader.IsDBNull(1))
                            {
                                ppno = nicReader.GetString(1);
                            }
                            if (!nicReader.IsDBNull(2))
                            {
                                mobileno = nicReader.GetString(2);
                            }
                            break;
                        }
                        nicReader.Close();
                    }

                    if (!validator.checkNICPPForAML(nic, "", mobileno) && !validator.checkNICPPForAML("", ppno, mobileno))
                    {
                        string getUser = "select count(*) from ULWEB.WEBUSERS where UPPER(USERNAME) = :username and ACTIVE_FLAG = 'Y' and USERTYPE = 0";

                        cmd.CommandText = getUser;

                        cmd.Parameters.AddWithValue("username", userName.ToUpper());
                        int count = 0;

                        OracleDataReader cntReader = cmd.ExecuteReader();

                        if (cntReader.HasRows)
                        {
                            while (cntReader.Read())
                            {
                                count = cntReader.GetInt32(0);
                            }
                        }
                        cmd.Parameters.Clear();

                        if (count > 0)
                        {
                            string get_pwd = "select PASWORD from ULWEB.WEBUSERS where UPPER(USERNAME) = :username and ACTIVE_FLAG = 'Y' and USERTYPE = 0";

                            cmd.CommandText = get_pwd;
                            cmd.Parameters.AddWithValue("username", userName.ToUpper());

                            OracleDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    pwd_db = reader.GetString(0);
                                    break;
                                }
                            }

                            logger.write_log(password);
                            string SentPwd = Hash.Hash.Base64Decode(password);
                            logger.write_log(SentPwd);
                            SentPwd = Hash.Hash.Base64Decode(SentPwd);

                            //hashed_pwd_db = pwd_db + client_code + server_code;
                            //hashed_all = Hash.Hash.Get_SHA512(hashed_pwd_db);// this is a must

                            logger.write_log(client_code);
                            logger.write_log(server_code);
                            logger.write_log(SentPwd);

                            if (SentPwd.Contains(client_code) && SentPwd.Contains(server_code))
                            {
                                string actualPwd = SentPwd.Remove(SentPwd.Length - 6);
                                actualPwd = Hash.Hash.Base64Decode(actualPwd);
                                password = Hash.Hash.Get_SHA512(actualPwd);

                                string salt_password = Hash.Hash.Get_SHA512(actualPwd.Trim() + salt.Trim());
                                logger.write_log(actualPwd.Trim());
                                logger.write_log(salt.Trim());
                                logger.write_log(pwd_db);
                                logger.write_log(password);
                                logger.write_log(salt_password);


                                if (pwd_db.Equals(password) || pwd_db.Equals(salt_password))
                                {
                                    string setLoginDate = "Update ULWEB.WEBUSERS set LAST_LOGIN_DATE = sysdate where UPPER(USERNAME) = :username";

                                    cmd.CommandText = setLoginDate;

                                    cmd.Parameters.AddWithValue("username", userName.ToUpper());

                                    int count1 = cmd.ExecuteNonQuery();

                                    if (count1 > 0)
                                    {
                                        returnValue = 0;
                                    }


                                    if (failedLoginCount > 0)
                                    {
                                        string setLoginCountZero = "Update ULWEB.WEBUSERS set FAILED_LOGIN_COUNT = 0 where UPPER(USERNAME) = :username";

                                        cmd.CommandText = setLoginCountZero;
                                        cmd.Parameters.AddWithValue("username", userName.ToUpper());

                                        int count2 = cmd.ExecuteNonQuery();
                                        if (count2 < 0)
                                        {
                                            logger.write_log("Failed Webusers update at ValidateLogin");
                                        }

                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                    string setLoginCount = "Update ULWEB.WEBUSERS set FAILED_LOGIN_COUNT = (nvl(FAILED_LOGIN_COUNT,0) + 1), LAST_FAILED_DATE = sysdate where UPPER(USERNAME) = :username and ACTIVE_FLAG = 'Y' and USERTYPE = 0";

                                    cmd.CommandText = setLoginCount;
                                    cmd.Parameters.AddWithValue("username", userName.ToUpper());

                                    int count3 = cmd.ExecuteNonQuery();
                                    if (count3 < 0)
                                    {
                                        // should log error - Failed login count not updated for some reason.
                                    }


                                    //log lg = new log();
                                    logger.write_log("login failed for " + userName);
                                }
                            }
                            else
                            {
                                returnValue = -1;
                                string setLoginCount = "Update ULWEB.WEBUSERS set FAILED_LOGIN_COUNT = (nvl(FAILED_LOGIN_COUNT,0) + 1), LAST_FAILED_DATE = sysdate where UPPER(USERNAME) = :username and ACTIVE_FLAG = 'Y' and USERTYPE = 0";

                                cmd.CommandText = setLoginCount;
                                cmd.Parameters.AddWithValue("username", userName.ToUpper());

                                int count3 = cmd.ExecuteNonQuery();
                                if (count3 < 0)
                                {
                                    // should log error - Failed login count not updated for some reason.
                                }


                                //log lg = new log();
                                logger.write_log("login failed for " + userName);
                            }

                        }
                        else
                        {
                            returnValue = -1;
                            string setLoginCount = "Update ULWEB.WEBUSERS set FAILED_LOGIN_COUNT = (nvl(FAILED_LOGIN_COUNT,0) + 1), LAST_FAILED_DATE = sysdate where UPPER(USERNAME) = :username and ACTIVE_FLAG = 'Y' and USERTYPE = 0";

                            cmd.CommandText = setLoginCount;

                            cmd.Parameters.AddWithValue("username", userName.ToUpper());

                            int count3 = cmd.ExecuteNonQuery();

                            if (count3 < 0)
                            {
                                // should log error - Failed login count not updated for some reason.
                            }


                            //log lg = new log();

                            logger.write_log("login failed for " + userName);

                        }
                    }
                    else
                    {
                        returnValue = -2;
                    }

                }

                trans.Commit();
            }

                //  trans.Commit();

            catch (Exception e)
            {
                trans.Rollback();
                returnValue = -1;// Log your error
                logger.write_log("Failed at ULCustomer - ValidateLogin2: " + e.ToString());
            }
            finally
            {
                if (oconn.State == ConnectionState.Open)
                {
                    oconn.Close();
                }
            }
        }
        else
        {
            // Log error - user name not alpha-numeric or 
            // username or password exceed the length limit!
        }

        return returnValue;
    }
    

    #region method for KPMG
    //public int ValidateLogin(string userName, string password, int failedLoginCount, string server_code, string client_code)
    //{

    //    int returnValue = -1;
    //    string pwd_db = "";
    //    string hashed_pwd_db = "";
    //    string hashed_all = "";

    //    log logger = new log();

    //    if (userName.Length <= 15)
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
    //                string getUser = "select count(*) from ULWEB.WEBUSERS where UPPER(USERNAME) = :username and ACTIVE_FLAG = 'Y' and USERTYPE = 0";

    //                cmd.CommandText = getUser;

    //                cmd.Parameters.AddWithValue("username", userName.ToUpper());
    //                int count = 0;

    //                OracleDataReader cntReader = cmd.ExecuteReader();

    //                if (cntReader.HasRows)
    //                {
    //                    while (cntReader.Read())
    //                    {
    //                        count = cntReader.GetInt32(0);
    //                    }
    //                }
    //                cmd.Parameters.Clear();                    

    //                if (count > 0)
    //                {
    //                    string get_pwd = "select PASWORD from ULWEB.WEBUSERS where UPPER(USERNAME) = :username and ACTIVE_FLAG = 'Y' and USERTYPE = 0";

    //                    cmd.CommandText = get_pwd;
    //                    cmd.Parameters.AddWithValue("username", userName.ToUpper());

    //                    OracleDataReader reader = cmd.ExecuteReader();
    //                    if (reader.HasRows)
    //                    {
    //                        while (reader.Read())
    //                        {
    //                            pwd_db = reader.GetString(0);
    //                            break;
    //                        }
    //                    }
                        
    //                    hashed_pwd_db = pwd_db + client_code + server_code;
    //                    hashed_all = Hash.Hash.Get_SHA512(hashed_pwd_db);// this is a must


    //                    if (hashed_all.Equals(password))
    //                    {
    //                        string setLoginDate = "Update ULWEB.WEBUSERS set LAST_LOGIN_DATE = sysdate where UPPER(USERNAME) = :username";

    //                        cmd.CommandText = setLoginDate;

    //                        cmd.Parameters.AddWithValue("username", userName.ToUpper());
                                                             
    //                        int count1 = cmd.ExecuteNonQuery();
                            
    //                        if (count1 > 0)
    //                        {
    //                            returnValue = 0;
    //                        }
                                

    //                        if (failedLoginCount > 0)
    //                        {
    //                            string setLoginCountZero = "Update ULWEB.WEBUSERS set FAILED_LOGIN_COUNT = 0 where UPPER(USERNAME) = :username";

    //                            cmd.CommandText = setLoginCountZero;
    //                            cmd.Parameters.AddWithValue("username", userName.ToUpper());
                                       
    //                            int count2 = cmd.ExecuteNonQuery();
    //                            if (count2 < 0)
    //                            {
    //                                logger.write_log("Failed Webusers update at ValidateLogin");
    //                            }
                                    
    //                        }
    //                    }
    //                    else
    //                    {
    //                        returnValue = -1;
    //                        string setLoginCount = "Update ULWEB.WEBUSERS set FAILED_LOGIN_COUNT = (nvl(FAILED_LOGIN_COUNT,0) + 1), LAST_FAILED_DATE = sysdate where UPPER(USERNAME) = :username and ACTIVE_FLAG = 'Y' and USERTYPE = 0";

    //                        cmd.CommandText = setLoginCount;
    //                        cmd.Parameters.AddWithValue("username", userName.ToUpper());

    //                        int count3 = cmd.ExecuteNonQuery();
    //                        if (count3 < 0)
    //                        {
    //                            // should log error - Failed login count not updated for some reason.
    //                        }

                                
    //                        //log lg = new log();
    //                        logger.write_log("login failed for " + userName);
    //                    }
                      
    //                }
    //                else
    //                {
    //                    returnValue = -1;
    //                    string setLoginCount = "Update ULWEB.WEBUSERS set FAILED_LOGIN_COUNT = (nvl(FAILED_LOGIN_COUNT,0) + 1), LAST_FAILED_DATE = sysdate where UPPER(USERNAME) = :username and ACTIVE_FLAG = 'Y' and USERTYPE = 0";

    //                    cmd.CommandText = setLoginCount;

    //                    cmd.Parameters.AddWithValue("username", userName.ToUpper());

    //                    int count3 = cmd.ExecuteNonQuery();
                        
    //                    if (count3 < 0)
    //                    {
    //                        // should log error - Failed login count not updated for some reason.
    //                    }

                        
    //                    //log lg = new log();
                        
    //                    logger.write_log("login failed for " + userName);

    //                }
                    

    //            }

    //            trans.Commit();
    //        }

    //            //  trans.Commit();

    //        catch (Exception e)
    //        {
    //            trans.Rollback();
    //            returnValue = -1;// Log your error
    //            logger.write_log("Failed at ULCustomer - ValidateLogin2: " + e.ToString());
    //        }
    //        finally
    //        {                
    //            if (oconn.State == ConnectionState.Open)
    //            {
    //                oconn.Close();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        // Log error - user name not alpha-numeric or 
    //        // username or password exceed the length limit!
    //    }

    //    return returnValue;
    //}
    # endregion

    public string getLoginName(string userName)
    {
        string loginName = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
           
            string getUsername = "Select USERNAME from ULWEB.WEBUSERS where UPPER(USERNAME) = UPPER(:userName)";
            
            using (OracleCommand cmd = new OracleCommand(getUsername, oconn))
            {
                cmd.Parameters.AddWithValue("username", userName);

                OracleDataReader usernameReader = (OracleDataReader)cmd.ExecuteReader();

                if (usernameReader.HasRows)
                {
                    while (usernameReader.Read())
                    {
                        if (!usernameReader.IsDBNull(0))
                        {
                            loginName = usernameReader.GetString(0);
                        }

                    }
                    usernameReader.Close();
                }
            }

        }        
        catch (Exception e)
        {
            loginName = "Error";
            log logger = new log();
            logger.write_log("Failed at ULCustomer - getLoginName: " + e.ToString());
            
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return loginName;
    }

    public bool PasswordRecoverySet(string fieldValue, string fieldType)
    {
        bool returnValue = false;
        string userName = "";
        string email = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getUsername = "";

            if (fieldType == "UN")
            {
                getUsername = "Select USERNAME, EMAIL from ULWEB.WEBUSERS where UPPER(USERNAME) = UPPER(:fieldVal) and ACTIVE_FLAG = 'Y' and USERTYPE = 0";
            }
            else if (fieldType == "EM")
            {
                getUsername = "Select USERNAME, EMAIL from ULWEB.WEBUSERS where EMAIL = :fieldVal and ACTIVE_FLAG = 'Y' and USERTYPE = 0";
            }

            using (OracleCommand cmd = new OracleCommand(getUsername, oconn))
            {
                cmd.Parameters.AddWithValue("fieldVal", fieldValue);

                OracleDataReader usernameReader = (OracleDataReader)cmd.ExecuteReader();

                if (usernameReader.HasRows)
                {
                    while (usernameReader.Read()) 
                    {
                        if (!usernameReader.IsDBNull(0))
                        {
                            userName = usernameReader.GetString(0);
                        }
                        if (!usernameReader.IsDBNull(1))
                        {
                            email = usernameReader.GetString(1);
                        }
                    }
                    usernameReader.Close();
                }

                string setPassRecovery = "Insert into ULWEB.WEBUSER_PWRESET(USERNAME, REQ_DATE, RESET_CODE) values (:username, sysdate, :resetcode)";

                using (OracleCommand cmd2 = new OracleCommand(setPassRecovery, oconn))
                {
                    string hashedUser = Hash.Hash.Get_SHA512(userName.Trim() + DateTime.Now.ToString());//Hasher.HashString(username.Trim());

                    cmd2.Parameters.AddWithValue("username", userName);
                    cmd2.Parameters.AddWithValue("resetcode", hashedUser);
                            
                    int count = cmd2.ExecuteNonQuery();

                    if (count > 0)
                    {                        
                        string subject = "SLIC Web - Password Recovery service";
                        string content1 = "Please copy the url and navigate to it using a browser. https://" + domainAndPort + "/ResetPassword.aspx?token=" + hashedUser +
                                         "\"> https://" + domainAndPort + "/ResetPassword.aspx?token=" + hashedUser;

                        //Change due to Anshu aiya's request by Tharu 20190515
                        //string content2 = "Dear " + userName + ", <br/> Please click on following link to reset your password." +
                        //                 "<br/><br/><a href=\"https://" + domainAndPort + "/ResetPassword.aspx?token=" + hashedUser +
                        //                 "\"> https://" + domainAndPort + "/ResetPassword.aspx?token=" + hashedUser + "</a>";

                        string content2 = "Dear Sir/Madam, <br/> <br/> Please click on following link to reset your password. <br/> <br/> Your username :  " + userName + "" +
                                       "<br/><br/><a href=\"https://" + domainAndPort + "/ResetPassword.aspx?token=" + hashedUser +
                                       "\"> https://" + domainAndPort + "/ResetPassword.aspx?token=" + hashedUser + "</a><br/><br/> Thank you.";

                        Db_Email emailSender = new Db_Email();
                        returnValue = emailSender.send_html_email(email, subject, content1, content2);
                        LogMail logger = new LogMail();
                        logger.write_log("To: " + email + " Subject: " + subject);
                    }
                }
            }
            
        }
        catch (Exception e)
        {
            returnValue = false;
            log logger = new log();
            logger.write_log("Failed at ULCustomer - PasswordRecoverySet: " + e.ToString());
            
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

    public bool isValidToken(string token)
    {
        bool returnValue = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getValidToken = "select count(*) from ULWEB.WEBUSER_PWRESET where RESET_CODE = :token";

            using (OracleCommand cmd = new OracleCommand(getValidToken, oconn))
            {
                cmd.Parameters.AddWithValue("token", token.Trim());

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
            logger.write_log("Failed at ULCustomer - isValidToken: " + e.ToString());
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

    public bool isValidRegToken(string token)
    {
        bool returnValue = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string getValidToken = "select count(*) from ULWEB.WEBUSERS_PREREG where REG_CODE = :token";

            using (OracleCommand cmd = new OracleCommand(getValidToken, oconn))
            {
                cmd.Parameters.AddWithValue("token", token.Trim());

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
            logger.write_log("Failed at ULCustomer - isValidRegToken: " + e.ToString());
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

    public bool ResetPassword(string token, string password)
    {
        bool returnValue = false;

        //conn.Open();
        //OracleCommand cmd = conn.CreateCommand();
        //OracleTransaction trans = conn.BeginTransaction();
        //cmd.Transaction = trans;
        

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string username = "";
            string getUsername = "Select USERNAME from ULWEB.WEBUSER_PWRESET where RESET_CODE = :token";

            using (OracleCommand com1 = new OracleCommand(getUsername, oconn))
            {
                com1.Parameters.AddWithValue("token", token.Trim());

                OracleDataReader usernameReader = (OracleDataReader)com1.ExecuteReader();

                if (usernameReader.HasRows)
                {
                    while (usernameReader.Read())
                    {
                        if (!usernameReader.IsDBNull(0))
                        {
                            username = usernameReader.GetString(0);
                        }
                    }
                    usernameReader.Close();

                    string salt = this.get_created_date(username, oconn);

                    string setNewPassword = "Update ULWEB.WEBUSERS set PASWORD = :pass, LAST_RESET_DATE = sysdate, FAILED_LOGIN_COUNT = 0 where lower(trim(USERNAME)) = :username";

                    using (OracleCommand com2 = new OracleCommand(setNewPassword, oconn))
                    {
                        com2.Parameters.AddWithValue("pass", Hash.Hash.Get_SHA512(password.Trim()+salt));
                        com2.Parameters.AddWithValue("username", username.Trim().ToLower());
                        
                        int count1 = com2.ExecuteNonQuery();

                        if (count1 > 0)
                        {
                            string deleteTempRecord = "Delete from ULWEB.WEBUSER_PWRESET where RESET_CODE = :token";

                            using (OracleCommand com3 = new OracleCommand(deleteTempRecord, oconn))
                            {
                                com3.Parameters.AddWithValue("token", token.Trim());

                                int count2 = com3.ExecuteNonQuery();
                                if (count2 > 0)
                                {
                                    // trans.Commit();
                                    returnValue = true;
                                }
                            }                    

                        }
                    }
                }
            }
            
        }
        catch (Exception e)
        {
            // trans.Rollback();
            returnValue = false;
            log logger = new log();
            logger.write_log("Failed at ULCustomer - ResetPassword: " + e.ToString());
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

    public string get_created_date(string username)
    {
        string result = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string getUsername = "Select to_char(created_date, 'yyyy_Mon_Dy@HHmi') from ULWEB.WEBUSERS where lower(trim(USERNAME)) = :username";

            using (OracleCommand com1 = new OracleCommand(getUsername, oconn))
            {
                com1.Parameters.AddWithValue("username", username.Trim().ToLower());

                OracleDataReader usernameReader = (OracleDataReader)com1.ExecuteReader();

                if (usernameReader.HasRows)
                {
                    while (usernameReader.Read())
                    {
                        if (!usernameReader.IsDBNull(0))
                        {
                            result = usernameReader.GetString(0);
                        }
                    }
                    usernameReader.Close();
                }
            }
        }
        catch (Exception e)
        {
            // trans.Rollback();
            result = "";
            log logger = new log();
            logger.write_log("Failed at ULCustomer - ConfirmRegistration: " + e.ToString());
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

    public string get_created_date(string username, OracleConnection oconn)
    {
        string result = "";

        try
        {
            //if (oconn.State != ConnectionState.Open)
            //{
            //    oconn.Open();
            //}
            string getUsername = "Select to_char(created_date, 'yyyy_Mon_Dy@HHmi') from ULWEB.WEBUSERS where lower(trim(USERNAME)) = :username";

            using (OracleCommand com1 = new OracleCommand(getUsername, oconn))
            {
                com1.Parameters.AddWithValue("username", username.Trim().ToLower());

                OracleDataReader usernameReader = (OracleDataReader)com1.ExecuteReader();

                if (usernameReader.HasRows)
                {
                    while (usernameReader.Read())
                    {
                        if (!usernameReader.IsDBNull(0))
                        {
                            result = usernameReader.GetString(0);
                        }
                    }
                    usernameReader.Close();
                }
            }
        }
        catch (Exception e)
        {
            // trans.Rollback();
            result = "";
            log logger = new log();
            logger.write_log("Failed at ULCustomer - ConfirmRegistration: " + e.ToString());
        }
        finally
        {
            //if (oconn.State == ConnectionState.Open)
            //{
            //    oconn.Close();
            //}
        }

        return result;
    }


    public bool ConfirmRegistration(string token)
    {
        bool returnValue = false;

        //conn.Open();
        //OracleCommand cmd = conn.CreateCommand();
        //OracleTransaction trans = conn.BeginTransaction();
        //cmd.Transaction = trans;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string username = "";
            string getUsername = "Select USERNAME from ULWEB.WEBUSERS_PREREG where REG_CODE = :token";

            using (OracleCommand com1 = new OracleCommand(getUsername, oconn))
            {
                com1.Parameters.AddWithValue("token", token.Trim());

                OracleDataReader usernameReader = (OracleDataReader)com1.ExecuteReader();

                if (usernameReader.HasRows)
                {
                    while (usernameReader.Read())
                    {
                        if (!usernameReader.IsDBNull(0))
                        {
                            username = usernameReader.GetString(0);
                        }
                    }
                    usernameReader.Close();

                    string setMemberActive = "Update ULWEB.WEBUSERS set ACTIVE_FLAG = 'Y', LAST_UPD_DATE = sysdate where lower(trim(USERNAME)) = :username";

                    using (OracleCommand com2 = new OracleCommand(setMemberActive, oconn))
                    {
                        com2.Parameters.AddWithValue("username", username.Trim().ToLower());
                        
                        int count1 = com2.ExecuteNonQuery();

                        if (count1 > 0)
                        {
                            string deleteTempRecord = "Delete from ULWEB.WEBUSERS_PREREG where REG_CODE = :token";

                            using (OracleCommand com3 = new OracleCommand(deleteTempRecord, oconn))
                            {
                                com3.Parameters.AddWithValue("token", token.Trim());

                                int count2 = com3.ExecuteNonQuery();
                                if (count2 > 0)
                                {
                                    // trans.Commit();
                                    returnValue = true;
                                }
                            }

                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            // trans.Rollback();
            returnValue = false;
            log logger = new log();
            logger.write_log("Failed at ULCustomer - ConfirmRegistration: " + e.ToString());
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
    //---------------------------this method is not being used-----------------------------------------------------
    public bool SendRegNotification(string username) // This is not working at the moment
    {
        bool returnValue = false;

        try
        {
            string subject = "Thank you for registering for SLIC website";
            string content = "We thank you and welcome you to the SLIC Customer site!" +
                             "<br/><br/> This site provides you with the Unit Link statistics." +
                             "<br/><br/> Click <a href=\"http://localhost:51647/UnitLinkWeb/Home.aspx\" target='_blank'>here</a> to visit our site." +
                             "<br/><br/><br/> SLIC-Team.";

            utils util = new utils();

            util.sendEmail(username, subject, content); // username should be replaced by email
            returnValue = true;

            //logging of email sent - to do
        }
        catch(Exception e)
        {
            returnValue = false;
            log logger = new log();
            logger.write_log("Failed at ULCustomer - SendRegNotification: " + e.ToString());
        }
        return returnValue;
    }
    //----------------------------------------------------------------------------------------------------------------


    public bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        bool returnValue = false;
        try
        {
            string salt = this.get_created_date(username);

            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string updatePassword = "Update ULWEB.WEBUSERS set PASWORD = :newPasswd where lower(trim(USERNAME)) = :username and (PASWORD =:oldPasswd or PASWORD = :oldsaltPasswd)";

            

            using (OracleCommand com1 = new OracleCommand(updatePassword, oconn))
            {
                com1.Parameters.AddWithValue("newPasswd", Hash.Hash.Get_SHA512(newPassword.Trim()+salt));
                com1.Parameters.AddWithValue("username", username.Trim().ToLower());
                com1.Parameters.AddWithValue("oldPasswd", Hash.Hash.Get_SHA512(oldPassword.Trim()));
                com1.Parameters.AddWithValue("oldsaltPasswd", Hash.Hash.Get_SHA512(oldPassword.Trim()+salt));

                int count = com1.ExecuteNonQuery();

                if (count > 0)
                {
                    returnValue = true;
                }

            }       

        }
        catch (Exception e)
        {
            // Log your error
            returnValue = false;
            log logger = new log();
            logger.write_log("Failed at ULCustomer - ChangePassword: " + e.ToString());
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

    public int GetFailedLoginCount(string username)
    {
        int failedLoginCount = -1;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getFailedCount = "Select nvl(FAILED_LOGIN_COUNT,0) from ULWEB.WEBUSERS where UPPER(USERNAME) = UPPER(:username)";

            using (OracleCommand com = new OracleCommand(getFailedCount, oconn))
            {
                com.Parameters.AddWithValue("username", username);

                int count = Convert.ToInt32(com.ExecuteScalar());

                failedLoginCount = (int)count;
            }
            
        }
        catch(Exception e)
        {
            failedLoginCount = -1;
            log logger = new log();
            logger.write_log("Failed at ULCustomer - GetFailedLoginCount: " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return failedLoginCount;
    }

    public string getSavedPolicies(string username, GridView gridVwMot, GridView gridVwGen, GridView gridVwPend)
    {
        string mesg = "";
        try
        {
            mesg = getPendingPolicies(username, gridVwPend);

            if (mesg == "success")
            {
                mesg = getSavedMotPolicies(username, gridVwMot);
                if (mesg == "success")
                {
                    mesg = getSavedGenPolicies(username, gridVwGen);
                }
            }
        }
        catch (Exception e)
        {
            mesg = "Error occurred while retrieving policy information";
        }

        return mesg;

    }
    public string getSavedMotPolicies(string username, GridView gridVw)
    {
        string mesg = "success";
       
        string nic = "";
        try
        {
          //  gridVw.DataSource = null;
          //  gridVw.DataBind();
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getNic = "Select NIC_NO from ULWEB.WEBUSERS where lower(trim(USERNAME)) = :username";

            using (OracleCommand com1 = new OracleCommand(getNic, oconn))
            {
                com1.Parameters.AddWithValue("username", username.Trim().ToLower());

                OracleDataReader NicReader = (OracleDataReader)com1.ExecuteReader();

                if (NicReader.HasRows)
                {
                    while (NicReader.Read())
                    {
                        if (!NicReader.IsDBNull(0))
                        {
                            nic = NicReader.GetString(0);
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
            logger.write_log("Failed at ULCustomer- getMotorPolicies-proc1: " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }          
        }

        if (nic != "")
        {           
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

                    string getSavedCount = "Select POLICY_NUMBER from SLIC_NET.POL_DET_FOR_WEB" +
                                      " where lower(trim(USERNAME)) = :userName" +
                                      " and POLICY_TYPE = 'M'";

                    cmd.CommandText = getSavedCount;
                    cmd.Parameters.AddWithValue("userName", username.Trim().ToLower());
                    OracleDataReader savedCntReader = cmd.ExecuteReader();
                    
                    while(savedCntReader.Read())
                    {
                        DataRow dr = null;
                        dr = dt.NewRow();
                        dt.Rows.Add(dr);
                    }
                    savedCntReader.Close();
                    cmd.Parameters.Clear();

                    gridVw.DataSource = dt;
                    gridVw.DataBind();

                    string getSavedPols = "Select POLICY_NUMBER from SLIC_NET.POL_DET_FOR_WEB" +
                                      " where lower(trim(USERNAME)) = :userName" +
                                      " and POLICY_TYPE = 'M'" +
                                      " order by CREATED_DATE";

                    cmd.CommandText = getSavedPols;
                    cmd.Parameters.AddWithValue("userName", username.Trim().ToLower());
                    OracleDataReader savedPolReader = cmd.ExecuteReader();

                    string polNum = "";
                    int j = 0;
                    while (savedPolReader.Read())
                    {  

                        if (!savedPolReader.IsDBNull(0))
                        {
                            polNum = savedPolReader.GetString(0);
                            string getPolicyDetls = "Select  PMNAM||' '|| PMNAM2, a.PMPOL AS POLNUM, a.PMPTP, a.PMSUM AS SUM_ASSURD, to_char(a.PMDST, 'yyyy/mm/dd') AS START_DATE," +
                                                    " to_char(a.PMDEX, 'yyyy/mm/dd') AS END_DATE, a.PMVE2 AS VEHI_NUM, sum(a.PMTOT) AS PREMIUM, PMTYP" +
                                                    " from GENPAY.PAYFLE a" +
                                                    " where PMDEP = 'M'" +
                                                    " AND (PMSDP ='M' or PMSDP ='T')" +
                                                    " and ( ((PMCOD='N')  AND (PMTYP=111 or PMTYP=112  or PMTYP=211 or PMTYP=212) )" +
                                                    " OR((PMTYP=311 or PMTYP=321) AND PMCOD='1') or ( (PMTYP=121 or PMTYP=122 or PMTYP=221 or PMTYP=222)" +
                                                    " OR   ((PMTYP=311 or PMTYP=321) AND PMCOD='0'))) AND PMDEL = '0'" +
                                                    " and a.PMDEX = (Select max(PMDEX) from GENPAY.PAYFLE where PMPOL = :polNo)" +
                                                    " and a.PMPOL = :polNo" +
                                                    " group by PMNAM||' '|| PMNAM2, a.PMPOL, a.PMPTP, a.PMSUM, to_char(a.PMDST, 'yyyy/mm/dd')," +
                                                    " to_char(a.PMDEX, 'yyyy/mm/dd'), a.PMVE2, PMTYP";

                           


                            using (OracleCommand cmd2 = new OracleCommand(getPolicyDetls, oconn))
                            {
                                cmd2.Parameters.AddWithValue("polNo", polNum);
                                OracleDataReader polDetReader = cmd2.ExecuteReader();

                                Label lblName = (Label)gridVw.Rows[j].FindControl("lblName");
                                Label lblPolNum = (Label)gridVw.Rows[j].FindControl("lblPolNum");
                                Label lblPolTyp = (Label)gridVw.Rows[j].FindControl("lblPolTyp");
                                Label lblSumAssurd = (Label)gridVw.Rows[j].FindControl("lblSumAssurd");
                                Label lblStrtDate = (Label)gridVw.Rows[j].FindControl("lblStrtDate");
                                Label lblEndDate = (Label)gridVw.Rows[j].FindControl("lblEndDate");
                                Label lblVehiNo = (Label)gridVw.Rows[j].FindControl("lblVehiNo");
                                Label lblPremium = (Label)gridVw.Rows[j].FindControl("lblPremium");
                                Label lblStatus = (Label)gridVw.Rows[j].FindControl("lblStatus");

                                HyperLink hyper = (HyperLink)gridVw.Rows[j].FindControl("HyperLink1");
                                HyperLink hyperL3 = (HyperLink)gridVw.Rows[j].FindControl("HyperLink2");

                                lblPolNum.Text = polNum;
                                lblStatus.Text = "Details not available";

                                while (polDetReader.Read())
                                {
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
                                        lblSumAssurd.Text = "Rs. " + polDetReader.GetDouble(3).ToString("N2");
                                    }
                                    if (!polDetReader.IsDBNull(4))
                                    {
                                        lblStrtDate.Text = polDetReader.GetString(4);
                                    }
                                    if (!polDetReader.IsDBNull(5))
                                    {
                                        lblEndDate.Text = polDetReader.GetString(5);
                                    }
                                    if (!polDetReader.IsDBNull(6))
                                    {
                                        lblVehiNo.Text = polDetReader.GetString(6);
                                    }
                                    if (!polDetReader.IsDBNull(7))
                                    {
                                        lblPremium.Text = polDetReader.GetDouble(7).ToString("N2");
                                    }
                                    lblStatus.Text = "Active";



                                    // ========================= Changed by Dileepa 2023-11-02===============================================================
                                    //===============================================================================================================
                                    if (lblPolTyp.Text == "TP")
                                    {
                                        hyper.Text = "Renew";
                                        hyperL3.Text = "Download Documents";
                                        //hyperL3.Enabled = true;
                                        EncryptDecrypt en = new EncryptDecrypt();
                                        Dictionary<string, string> dc = new Dictionary<string, string>();
                                        dc.Add("PolicyNo", polDetReader.GetString(1));
                                        //string link = en.url_encrypt("TpDetails.aspx", dc);
                                        string link = en.url_encrypt("Tp_Renewals.aspx", dc);
                                        string link2 = en.url_encrypt("TpDetails.aspx", dc);
                                        hyper.NavigateUrl = link;
                                        hyperL3.NavigateUrl = link2;
                                    }
                                    else
                                    {
                                        EncryptDecrypt en = new EncryptDecrypt();
                                        Dictionary<string, string> dc = new Dictionary<string, string>();
                                        dc.Add("PolicyNo", polDetReader.GetString(1));
                                        dc.Add("PolicyTyp", polDetReader.GetString(2));
                                        dc.Add("EndDate", polDetReader.GetString(5));
                                        dc.Add("Dept", "M");
                                        dc.Add("pmtyp", polDetReader.GetString(8));
                                        string link = en.url_encrypt("Renewal.aspx", dc);

                                        hyper.NavigateUrl = link;
                                    }
                                    //Renewal.aspx
                                    //PolicyNo
                                    //EndDate
                                    // =================================================================================================================================
                                    //===================================================================================================================================

                                }
                                polDetReader.Close();
                                cmd2.Parameters.Clear();                       
                                
                            }

                            Label lbStatus = (Label)gridVw.Rows[j].FindControl("lblStatus");
                            Label lbEndDate = (Label)gridVw.Rows[j].FindControl("lblEndDate");
                            HyperLink hyperL = (HyperLink)gridVw.Rows[j].FindControl("HyperLink1");

                            HyperLink hyperL2 = (HyperLink)gridVw.Rows[j].FindControl("HyperLink2");
                    

                            string endDate = lbEndDate.Text;
                            if (endDate != "")
                            {
                                InfoValidator validator = new InfoValidator();

                                string oldNic = "";
                                string newNic = "";
                                if (nic.Length == 12)
                                {
                                    newNic = nic;
                                    oldNic = validator.reverseNicFormat(nic);
                                }
                                else if (nic.Length == 10)
                                {
                                    oldNic = nic;
                                    newNic = validator.reverseNicFormat(nic);
                                }

                                string getStatus = "SELECT * FROM MCOMP.MCACTUPDET WHERE PDREFER = :polNo and  to_date(PDEXDATE, 'yyyymmdd') = to_date(:endDate, 'yyyy/mm/dd') " +
                                                       " and PD_CANCEL_STATUS in ('BC', 'CA', 'CC', 'CD', 'CE') " /*and (upper(PDNIC) = :oldNic or upper(PDNIC) = :newNic) "*/;

                                using (OracleCommand cmd3 = new OracleCommand(getStatus, oconn))
                                {
                                    cmd3.Parameters.AddWithValue("polNo", polNum);
                                    cmd3.Parameters.AddWithValue("endDate", endDate);
                                   // cmd3.Parameters.AddWithValue("oldNic", oldNic.ToUpper());
                                   //  cmd3.Parameters.AddWithValue("newNic", newNic.ToUpper());
                                    OracleDataReader statusReader = cmd3.ExecuteReader();

                                    while (statusReader.Read())
                                    {
                                        lbStatus.Text = "Cancelled";
                                        lbStatus.ForeColor = System.Drawing.Color.Red;
                                        hyperL.Enabled = false;
                                        hyperL.ForeColor = System.Drawing.Color.Gray;
                                    }
                                    statusReader.Close();
                                }

                                //changed by dileepa 2023-12-18///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





                                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            

                                string policyType = "select pmptp from genpay.payfle where pmpol =:polno";
                                string polType = "";
                                using (OracleCommand cmd4 = new OracleCommand(policyType, oconn))
                                {
                                    cmd4.Parameters.AddWithValue("polno", polNum);
                                    OracleDataReader CNReader = cmd4.ExecuteReader();
                                    while (CNReader.Read())
                                    {
                                        if (CNReader[0] != null)
                                        {
                                            polType = CNReader[0].ToString();
                                        
                                            break;
                                        }
                                    }
                                    CNReader.Close();

                                }

                                if (polType =="TP")
                                {
                                    hyperL2.Enabled = true;
                                }
                                else
                                {
                                    string sqlCovernote = "select expire_date, refno from  slic_cnote.mastr_file  where policy_no =  :polno  and expire_date = (select max(expire_date) from slic_cnote.mastr_file where policy_no =  :polno2 ) order by refno desc";
                                    string exp_date_str = "";
                                    string c_note_code = "";

                                    using (OracleCommand cmd4 = new OracleCommand(sqlCovernote, oconn))
                                    {
                                        cmd4.Parameters.AddWithValue("polno", polNum);
                                        cmd4.Parameters.AddWithValue("polno2", polNum);
                                        OracleDataReader CNReader = cmd4.ExecuteReader();
                                        while (CNReader.Read())
                                        {
                                            if (CNReader[0] != null)
                                            {
                                                exp_date_str = CNReader[0].ToString();
                                                c_note_code = CNReader[1].ToString();
                                                break;
                                            }
                                        }
                                        CNReader.Close();


                                        if (String.IsNullOrEmpty(exp_date_str))
                                        {

                                            hyperL2.Enabled = false;
                                            hyperL2.ForeColor = System.Drawing.Color.Gray;
                                            hyperL2.CssClass = "disabled";
                                        }
                                        else
                                        {
                                            DateTime expire_date = Convert.ToDateTime(exp_date_str);

                                            if (expire_date > DateTime.Now)
                                            {
                                                hyperL2.Enabled = true;
                                                EncryptDecrypt en = new EncryptDecrypt();
                                                Dictionary<string, string> dc = new Dictionary<string, string>();
                                                dc.Add("CrefNo", c_note_code);
                                                string link = en.url_encrypt("Covernote_print.aspx", dc);
                                                hyperL2.NavigateUrl = link;


                                            }
                                            else
                                            {
                                                hyperL2.Enabled = false;
                                                hyperL2.ForeColor = System.Drawing.Color.Gray;
                                                hyperL2.CssClass = "disabled";
                                            }
                                        }
                                    }
                                }

                                    ///////////////////////////////////////////
                                    ///    
                                    /// //covernote stuff
                                    /// 

                          

                                // end of covernote stuff
                            
                            }

                        }
                        j++;
                    }
                    savedPolReader.Close();
                    cmd.Parameters.Clear();
                }

                gridVw.Columns[2].Visible = false;

                //Label lblPolVerify = (Label)gridVw.Rows[0].FindControl("lblPolNum"); // when policy number has been added to portal but is not currently in SLIC_CNOTE.PAYFLE_CNOTE_RENEWAL

                //if (lblPolVerify.Text == "")
                //{
                //    gridVw.DataSource = null;
                //    gridVw.DataBind();
                //}


            }
            catch (Exception e)
            {
                gridVw.DataSource = null;
                gridVw.DataBind();
                mesg = "Error occured while retrieving saved motor policy details";
                log logger = new log();
                logger.write_log("Failed at ULCustomer -getSavedMotPolicies: " + e.ToString());
            }
            finally
            {
                oconn.Close();
            }
        }
        return mesg;

    }

    public string getSavedGenPolicies(string username, GridView gridVw)
    {
        string mesg = "success";

        string nic = "";
        string ppNo = "";
        try
        {
            //  gridVw.DataSource = null;
            //  gridVw.DataBind();
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getNic = "Select NIC_NO, PASSPORT_NO from ULWEB.WEBUSERS where lower(trim(USERNAME)) = :username";

            using (OracleCommand com1 = new OracleCommand(getNic, oconn))
            {
                com1.Parameters.AddWithValue("username", username.Trim().ToLower());

                OracleDataReader NicReader = (OracleDataReader)com1.ExecuteReader();

                if (NicReader.HasRows)
                {
                    while (NicReader.Read())
                    {
                        if (!NicReader.IsDBNull(0))
                        {
                            nic = NicReader.GetString(0);
                        }
                        if (!NicReader.IsDBNull(1))
                        {
                            ppNo = NicReader.GetString(1);
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
            logger.write_log("Failed at ULCustomer- getSavedGenPolicies-proc1: " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        if (nic != "" || ppNo != "")
        {
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

                    string getSavedCount = "Select POLICY_NUMBER from SLIC_NET.POL_DET_FOR_WEB" +
                                      " where lower(trim(USERNAME)) = :userName" +
                                      " and POLICY_TYPE <> 'M' and SHE_MEM_CODE = '0'";

                    cmd.CommandText = getSavedCount;
                    cmd.Parameters.AddWithValue("userName", username.Trim().ToLower());
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

                    string getSavedPols = "Select POLICY_NUMBER from SLIC_NET.POL_DET_FOR_WEB" +
                                      " where lower(trim(USERNAME)) = :userName" +
                                      " and POLICY_TYPE <> 'M'" +
                                      " order by CREATED_DATE";

                    cmd.CommandText = getSavedPols;
                    cmd.Parameters.AddWithValue("userName", username.Trim().ToLower());
                    OracleDataReader savedPolReader = cmd.ExecuteReader();

                    string polNum = "";
                    int j = 0;
                    while (savedPolReader.Read())
                    {

                        if (!savedPolReader.IsDBNull(0))
                        {
                            polNum = savedPolReader.GetString(0);
                            string getPolicyDetls = "Select  a.PMNAM, a.PMPOL AS POLNUM, a.PMPTP, a.PMSUM AS SUM_ASSURD, to_char(a.PMDST, 'yyyy/mm/dd') AS START_DATE," +
                                                    " to_char(a.PMDEX, 'yyyy/mm/dd') AS END_DATE, a.PMPRM AS PREMIUM, PMDEP, PMPRO, PMTYP" +
                                                    " from genpay.PAYFLE a" +
                                                    " where PMDEP <> 'M' " +
                                                    " and ( ((PMCOD='N')  AND (PMTYP=111 or PMTYP=112  or PMTYP=211 or PMTYP=212) ) " +
                                                    " OR((PMTYP=311 or PMTYP=321) AND PMCOD='1') or ( (PMTYP=121 or PMTYP=122 or PMTYP=221 or PMTYP=222)" +
                                                    " OR   ((PMTYP=311 or PMTYP=321) AND PMCOD='0'))) AND PMDEL = '0' " +
                                                    " and a.PMDEX = (Select max(PMDEX) from genpay.PAYFLE where PMPOL = :polNo)" +
                                                    " and a.PMPOL = :polNo";

                            using (OracleCommand cmd2 = new OracleCommand(getPolicyDetls, oconn))
                            {
                                cmd2.Parameters.AddWithValue("polNo", polNum);
                                OracleDataReader polDetReader = cmd2.ExecuteReader();

                                while (polDetReader.Read())
                                {
                                    bool ours = false;

                                    Label lblName = (Label)gridVw.Rows[j].FindControl("lblName2");
                                    Label lblPolNum = (Label)gridVw.Rows[j].FindControl("lblPolNum2");
                                    Label lblPolTyp = (Label)gridVw.Rows[j].FindControl("lblPolTyp2");
                                    Label lblSumAssurd = (Label)gridVw.Rows[j].FindControl("lblSumAssurd2");
                                    Label lblStrtDate = (Label)gridVw.Rows[j].FindControl("lblStrtDate2");
                                    Label lblEndDate = (Label)gridVw.Rows[j].FindControl("lblEndDate2");
                                    Label lblPremium = (Label)gridVw.Rows[j].FindControl("lblPremium2");
                                    Label lblStatus = (Label)gridVw.Rows[j].FindControl("lblStatus2");

                                    HyperLink hyper = (HyperLink)gridVw.Rows[j].FindControl("HyperLink2");
                                    HyperLink hyper2 = (HyperLink)gridVw.Rows[j].FindControl("HyperLink3");


                                    EncryptDecrypt en = new EncryptDecrypt();
                                    Dictionary<string, string> dc1 = new Dictionary<string, string>();

                                    string dept = "";

                                    if (!polDetReader.IsDBNull(0))
                                    {
                                        lblName.Text = polDetReader.GetString(0);
                                    }
                                    if (!polDetReader.IsDBNull(1))
                                    {
                                        lblPolNum.Text = polDetReader.GetString(1);
                                        dc1.Add("P0lNo", polDetReader.GetString(1));
                                    }
                                    if (!polDetReader.IsDBNull(2))
                                    {
                                        lblPolTyp.Text = polDetReader.GetString(2);
                                        //if (lblPolTyp.Text != "HIP")
                                        //{
                                        //    hyper.Enabled = false;
                                        //}
                                    }
                                    if (!polDetReader.IsDBNull(3))
                                    {
                                        lblSumAssurd.Text = polDetReader.GetDouble(3).ToString("N2");
                                        dc1.Add("SM", lblSumAssurd.Text);

                                        if (lblPolTyp.Text == "TPI")
                                        {
                                            lblSumAssurd.Text = "$ " + lblSumAssurd.Text;
                                        }
                                        else
                                        {
                                            lblSumAssurd.Text = "Rs. " + lblSumAssurd.Text;
                                        }
                                    }
                                    if (!polDetReader.IsDBNull(4))
                                    {
                                        lblStrtDate.Text = polDetReader.GetString(4);
                                        dc1.Add("StaDt", lblStrtDate.Text);
                                    }
                                    if (!polDetReader.IsDBNull(5))
                                    {
                                        lblEndDate.Text = polDetReader.GetString(5);
                                        dc1.Add("EndDt", lblEndDate.Text);
                                    }                                    
                                    if (!polDetReader.IsDBNull(6))
                                    {
                                        lblPremium.Text = polDetReader.GetDouble(6).ToString("N2");
                                    }
                                    if (!polDetReader.IsDBNull(7))
                                    {
                                        dept = polDetReader.GetString(7);
                                    }

                                   

                                    if (!polDetReader.IsDBNull(8))
                                    {
                                        if (lblPolNum.Text != polDetReader.GetString(8))
                                        {
                                            if (polDetReader.GetString(8).Contains("/999/AMP/") || polDetReader.GetString(8).Contains("/999/HIP/") || polDetReader.GetString(8).Contains("/999/GTI/"))
                                                ours = true;

                                            lblPolNum.Text = lblPolNum.Text + " <b>(Ref: " + polDetReader.GetString(8) + ")</b>";
                                            dc1.Add("refN0", polDetReader.GetString(8));
                                        }
                                    }
                                    lblStatus.Text = "Active";


                                    //Renewal.aspx
                                    //PolicyNo
                                    //EndDate

                                    Dictionary<string, string> dc = new Dictionary<string, string>();
                                    dc.Add("PolicyNo", polDetReader.GetString(1));
                                    dc.Add("PolicyTyp", polDetReader.GetString(2));
                                    dc.Add("EndDate", polDetReader.GetString(5));
                                    dc.Add("Dept", dept);
                                    dc.Add("pmtyp", polDetReader.GetString(9));
                                    string link = en.url_encrypt("Renewal.aspx", dc);

                                    hyper.NavigateUrl = link;


                                    if (dc1.Count > 0)
                                    {
                                        link = en.url_encrypt("Policy_Reprint.aspx", dc1);
                                        hyper2.NavigateUrl = link;
                                        hyper2.Enabled = ours;

                                        if (!ours)
                                        {
                                            hyper2.ForeColor = System.Drawing.Color.DarkGray;
                                            hyper2.NavigateUrl = "";
                                        }
                                    }

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
                gridVw.Columns[2].Visible = false;
            }
            catch (Exception e)
            {
                gridVw.DataSource = null;
                gridVw.DataBind();
                mesg = "Error occured while retrieving saved policy details";
                log logger = new log();
                logger.write_log("Failed at ULCustomer - getSavedGenPolicies: " + e.ToString());
            }
            finally
            {
                oconn.Close();
            }
        }
        return mesg;

    }
    public string getPendingPolicies(string username, GridView gridVw)
    {
        string mesg = "success";
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            DataSet ds = new DataSet();
            string getPendingProps = "Select CASE WHEN PRODUCT_ID like 'TP%' THEN policy_number ELSE REF_NO   END AS REF_NO, POL_TYPE, PTSNA, PLAN, decode(D.PRODUCT_ID, 'TPI', '$ '||to_char(SUM_ASSURD,'9,999,999,999.99'), 'Rs. '||to_char(SUM_ASSURD,'9,999,999,999.99')) SUM_ASSURD, to_char(COM_DATE, 'yyyy/mm/dd') COM_DATE, to_char(ANU_PREMIUM,'99,999,990.99') ANU_PREMIUM" +
                                     " from SLIC_NET.PROPOSAL_DETAILS D, GENPAY.POLTYP P WHERE lower(trim(USERNAME)) = :userNam AND P.PTDEP = D.POL_TYPE AND P.PTTYP = D.PRODUCT_ID" +
                                     " AND nvl(D.STATUS,'N') = 'A' AND nvl(D.POL_ISSUED, 'D') = 'N' Order by D.ENTRY_DATE";

            using (OracleCommand cmd = new OracleCommand(getPendingProps, oconn))
            {
                //cmd.Parameters.AddWithValue("userNam", username);
                OracleDataAdapter data = new OracleDataAdapter();
                data.SelectCommand = cmd;
                data.SelectCommand.Parameters.AddWithValue("userNam", username.Trim().ToLower());
                ds.Clear();
                data.Fill(ds);
                gridVw.DataSource = ds.Tables[0];
                gridVw.DataBind();
            }
        }
        catch (Exception e)
        {
            gridVw.DataSource = null;
            gridVw.DataBind();
            mesg = "Error occured while retrieving pending policy details";
            log logger = new log();
            logger.write_log("Failed at ULCustomer - getPendingPolicies: " + e.ToString());
        }
        finally
        {
            oconn.Close();
        }

        return mesg;
    }
    public string getPolicies(string username, string polNumber, string polType, GridView gridVwMot, GridView gridVwGen)
    {
        string mesg = "";
        try
        {
            if (polType == "M")
            {
                mesg = getMotorPolicies(username, polNumber, polType, gridVwMot);
            }

            if (polType == "G")
            {
                mesg = getGenrlPolicies(username, polNumber, polType, gridVwGen);
            }
            
        }
        catch (Exception e)
        {
            mesg = "Error occurred while retrieving policy information";           
        }

        return mesg;
    }
    
    public string getMotorPolicies(string username, string polNumber, string polType, GridView gridVw)
    {
        string nic = "";
        string mesg = "success";
        polNumber = polNumber.ToUpper();

        try
        {
          //  gridVw.DataSource = null;
          //  gridVw.DataBind();
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getNic = "Select NIC_NO from ULWEB.WEBUSERS where lower(trim(USERNAME)) = :username";

            using (OracleCommand com1 = new OracleCommand(getNic, oconn))
            {
                com1.Parameters.AddWithValue("username", username.Trim().ToLower());

                OracleDataReader NicReader = (OracleDataReader)com1.ExecuteReader();

                if (NicReader.HasRows)
                {
                    while (NicReader.Read())
                    {
                        if (!NicReader.IsDBNull(0))
                        {
                            nic = NicReader.GetString(0);
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
            logger.write_log("Failed at ULCustomer - getMotorPolicies-proc1: " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }  
        }

        if (nic != "")
        {

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
                    //check if policy number is already in pol_det_for_web table-- no need to map with username
                    //if it is ouput error- already added to a user
                    string getAddedPol = "Select USERNAME from SLIC_NET.POL_DET_FOR_WEB" +
                                         " where POLICY_NUMBER = :polNo";

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
                    addedPolReader.Close();
                    cmd.Parameters.Clear();

                    if (mesg.Contains("success"))
                    {
                        bool polExist = false;
                        string polDept = "M";

                        if (polType == "M")
                        {
                            string getMotorPolDet = "Select to_char(a.PMDEX, 'yyyy/mm/dd') AS END_DATE" +
                                                    " from GENPAY.PAYFLE a" +
                                                    " where PMDEP = 'M'" +
                                                    " AND (PMSDP ='M' or PMSDP ='T')" +
                                                    " and ( ((PMCOD='N')  AND (PMTYP=111 or PMTYP=112  or PMTYP=211 or PMTYP=212) )" +
                                                    " OR((PMTYP=311 or PMTYP=321) AND PMCOD='1') or ( (PMTYP=121 or PMTYP=122 or PMTYP=221 or PMTYP=222)" +
                                                    " OR   ((PMTYP=311 or PMTYP=321) AND PMCOD='0'))) AND PMDEL = '0'" +
                                                    " and a.PMDEX = (Select max(PMDEX) from GENPAY.PAYFLE where PMPOL = :polNo)" +
                                                    " and a.PMPOL = :polNo";

                            //string name = "";
                            //string polNum = "";
                            //double sumAssurd = 0;
                            //string startDate = "";
                            string endDate = "";
                            //string vehiNum = "";
                            //double premium = 0;
                            string polStatus = "";

                            cmd.CommandText = getMotorPolDet;
                            cmd.Parameters.AddWithValue("polNo", polNumber);
                            OracleDataReader motPolReader = cmd.ExecuteReader();

                            while (motPolReader.Read())
                            {
                                polExist = true;
                                //if (!motPolReader.IsDBNull(0))
                                //{
                                //    name = motPolReader.GetString(0);
                                //}
                                //if (!motPolReader.IsDBNull(1))
                                //{
                                //    polNum = motPolReader.GetString(1);
                                //}
                                //if (!motPolReader.IsDBNull(2))
                                //{
                                //    sumAssurd = motPolReader.GetDouble(2);
                                //}
                                //if (!motPolReader.IsDBNull(3))
                                //{
                                //    startDate = motPolReader.GetString(3);
                                //}
                                if (!motPolReader.IsDBNull(0))
                                {
                                    endDate = motPolReader.GetString(0);
                                }
                                //if (!motPolReader.IsDBNull(5))
                                //{
                                //    vehiNum = motPolReader.GetString(5);
                                //}
                                //if (!motPolReader.IsDBNull(6))
                                //{
                                //    premium = motPolReader.GetDouble(6);
                                //}
                                //if (!motPolReader.IsDBNull(7))
                                //{
                                //    polDept = motPolReader.GetString(7);
                                //}
                            }

                            //gridVw.DataSource = motPolReader;
                            //gridVw.DataBind();
                            motPolReader.Close();
                            cmd.Parameters.Clear();

                            if (polExist)
                            {
                                polExist = false;

                                InfoValidator validator = new InfoValidator();

                                string newNic = "";
                                string polNic = "";
                                string newPolNic = "";

                                if (nic.Length == 12)
                                {
                                    newNic = nic;
                                }
                                else if (nic.Length == 10)
                                {
                                    newNic = validator.reverseNicFormat(nic);
                                }

                                string getStatus = "SELECT PD_CANCEL_STATUS, PDNIC FROM MCOMP.MCACTUPDET WHERE PDREFER = :polNo and  to_date(PDEXDATE, 'yyyymmdd') = to_date(:endDate, 'yyyy/mm/dd') ";

                                cmd.CommandText = getStatus;
                                cmd.Parameters.AddWithValue("polNo", polNumber);
                                cmd.Parameters.AddWithValue("endDate", endDate);
                                OracleDataReader statusReader = cmd.ExecuteReader();

                                while (statusReader.Read())
                                {
                                    if (!statusReader.IsDBNull(0))
                                    {
                                        polStatus = statusReader.GetString(0);
                                    }
                                    if (!statusReader.IsDBNull(1))
                                    {
                                        polNic = statusReader.GetString(1);
                                    }
                                }
                                statusReader.Close();
                                cmd.Parameters.Clear();

                                if (polNic.Length == 12)
                                {
                                    newPolNic = polNic;
                                }
                                else if (polNic.Length == 10)
                                {
                                    newPolNic = validator.reverseNicFormat(polNic);
                                }

                                if (newNic == newPolNic)
                                {
                                    polExist = true;
                                }
                            }
                            else
                            {
                                mesg = "Policy information is not available.<br/>Please enter correct policy number.";
                            }
                        }
                        else if (polType == "G")
                        {

                        }

                        if (polExist)
                        {

                            string instPolDetails = "Insert into SLIC_NET.POL_DET_FOR_WEB(USERNAME, POLICY_NUMBER, POLICY_TYPE, CREATED_DATE)" +
                                                        " VALUES(:usernam, :polNo, :polType, sysdate)";

                            cmd.CommandText = instPolDetails;

                            OracleParameter oUser = new OracleParameter();
                            oUser.DbType = DbType.String;
                            oUser.Value = username;
                            oUser.ParameterName = "usernam";

                            OracleParameter oPol = new OracleParameter();
                            oPol.DbType = DbType.String;
                            oPol.Value = polNumber;
                            oPol.ParameterName = "polNo";

                            OracleParameter oType = new OracleParameter();
                            oType.DbType = DbType.String;
                            oType.Value = polDept;
                            oType.ParameterName = "polType";

                            cmd.Parameters.Add(oUser);
                            cmd.Parameters.Add(oPol);
                            cmd.Parameters.Add(oType);

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            trans.Commit();

                            mesg = getSavedMotPolicies(username, gridVw);

                        }
                        else
                        {
                            if (mesg.Contains("success"))
                            {
                                mesg = "Your NIC number does not match with this policy number.<br/>Please enter correct policy number.";
                            }
                        }
                    }

                }

            }
            catch (Exception e)
            {
                mesg = "Error occurred while retrieving information";
                trans.Rollback();
                log logger = new log();
                logger.write_log("Failed at ULCustomer - getMotorPolicies-proc2: " + e.ToString());
            }
            finally
            {
                if (oconn.State == ConnectionState.Open)
                {
                    oconn.Close();
                }
            }
        }
        else
        {
            mesg = "Sorry. Motor Policies cannot be added by foreigners. (Your NIC Number has not been specified.)";
        }
        return mesg;           
    }

    public string getGenrlPolicies(string username, string polNumber, string polType, GridView gridVw)
    {
        string nic = "";
        string ppNo = "";
        string mesg = "success";
        polNumber = polNumber.ToUpper();
        try
        {
            //  gridVw.DataSource = null;
            //  gridVw.DataBind();
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getNic = "Select NIC_NO, PASSPORT_NO from ULWEB.WEBUSERS where lower(trim(USERNAME)) = :username";

            using (OracleCommand com1 = new OracleCommand(getNic, oconn))
            {
                com1.Parameters.AddWithValue("username", username.Trim().ToLower());

                OracleDataReader NicReader = (OracleDataReader)com1.ExecuteReader();

                if (NicReader.HasRows)
                {
                    while (NicReader.Read())
                    {
                        if (!NicReader.IsDBNull(0))
                        {
                            nic = NicReader.GetString(0);
                        }
                        if (!NicReader.IsDBNull(1))
                        {
                            ppNo = NicReader.GetString(1);                            
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
            logger.write_log("Failed at ULCustomer - getGenrlPolicies-proc1: " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        if (nic != "" || ppNo != "")
        {

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
                    //check if policy number is already in pol_det_for_web table-- no need to map with username
                    //if it is ouput error- already added to a user
                    string getAddedPol = "Select USERNAME from SLIC_NET.POL_DET_FOR_WEB" +
                                         " where POLICY_NUMBER = :polNo";

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
                    addedPolReader.Close();
                    cmd.Parameters.Clear();

                    if (mesg.Contains("success"))
                    {
                        bool polExist = false;
                        string polDept = "";

                        if (polType == "G")
                        {
                            string getGenPolDet = "Select PMCLCD, PMDEP" +
                                                    " from genpay.PAYFLE a" +
                                                    " where PMDEP <> 'M' " +
                                                    " and ( ((PMCOD='N')  AND (PMTYP=111 or PMTYP=112  or PMTYP=211 or PMTYP=212) ) " +
                                                    " OR((PMTYP=311 or PMTYP=321) AND PMCOD='1') or ( (PMTYP=121 or PMTYP=122 or PMTYP=221 or PMTYP=222)" +
                                                    " OR   ((PMTYP=311 or PMTYP=321) AND PMCOD='0'))) AND PMDEL = '0' " +
                                                    " and a.PMDEX is not null" +
                                                    " and a.PMDEX = (Select max(PMDEX) from genpay.PAYFLE where PMPOL = :polNo)" +
                                                    " and a.PMPOL = :polNo";

                            //string name = "";
                            //string polNum = "";
                            //double sumAssurd = 0;
                            //string startDate = "";
                            //string endDate = "";
                            //double premium = 0;
                            string clientCode = "";

                            cmd.CommandText = getGenPolDet;
                            cmd.Parameters.AddWithValue("polNo", polNumber);
                            OracleDataReader genPolReader = cmd.ExecuteReader();

                            while (genPolReader.Read())
                            {
                                polExist = true;
                                //if (!genPolReader.IsDBNull(0))
                                //{
                                //    name = genPolReader.GetString(0);
                                //}
                                //if (!genPolReader.IsDBNull(1))
                                //{
                                //    polNum = genPolReader.GetString(1);
                                //}
                                //if (!genPolReader.IsDBNull(2))
                                //{
                                //    sumAssurd = genPolReader.GetDouble(2);
                                //}
                                //if (!genPolReader.IsDBNull(3))
                                //{
                                //    startDate = genPolReader.GetString(3);
                                //}
                                //if (!genPolReader.IsDBNull(4))
                                //{
                                //    endDate = genPolReader.GetString(4);
                                //}                                
                                //if (!genPolReader.IsDBNull(5))
                                //{
                                //    premium = genPolReader.GetDouble(5);
                                //}
                                if (!genPolReader.IsDBNull(0))
                                {
                                    clientCode = genPolReader.GetString(0);
                                }
                                if (!genPolReader.IsDBNull(1))
                                {
                                    polDept = genPolReader.GetString(1);
                                }
                            }

                            //gridVw.DataSource = motPolReader;
                            //gridVw.DataBind();
                            genPolReader.Close();
                            cmd.Parameters.Clear();

                            if (polExist)
                            {
                                polExist = false;

                                InfoValidator validator = new InfoValidator();

                                string newNic = "";
                                string polNic = "";
                                string newPolNic = "";
                                string polPPNo = "";

                                if (nic.Length == 10)
                                {
                                    newNic = validator.reverseNicFormat(nic);
                                }
                                else if (nic.Length == 12)
                                {
                                    newNic = nic;
                                }

                                string getPolNic = "SELECT NIC_NO, PASSPORT_NO FROM CLIENTDB.PERSONAL_CUSTOMER WHERE CUSTOMER_ID = :clientId";

                                cmd.CommandText = getPolNic;
                                cmd.Parameters.AddWithValue("clientId", clientCode);
                                OracleDataReader polNicReader = cmd.ExecuteReader();

                                while (polNicReader.Read())
                                {
                                    if (!polNicReader.IsDBNull(0))
                                    {
                                        polNic = polNicReader.GetString(0);
                                    }
                                    if (!polNicReader.IsDBNull(1))
                                    {
                                        polPPNo = polNicReader.GetString(1);                                        
                                    }
                                }

                                polNicReader.Close();
                                cmd.Parameters.Clear();

                                if (polNic.Length == 10)
                                {
                                    newPolNic = validator.reverseNicFormat(polNic);
                                }
                                else if (polNic.Length == 12)
                                {
                                    newPolNic = polNic;
                                }

                                if (newNic != "")
                                {
                                    if (newNic == newPolNic)
                                    {
                                        polExist = true;
                                    }
                                }
                                else if (ppNo != "")
                                {                                    
                                    if (ppNo == polPPNo)
                                    {
                                        polExist = true;                                        
                                    }
                                }
                                
                            }
                            else
                            {
                                mesg = "Policy information not available.<br/>Please enter correct policy number.";
                            }
                        }                       

                        if (polExist)
                        {

                            string instPolDetails = "Insert into SLIC_NET.POL_DET_FOR_WEB(USERNAME, POLICY_NUMBER, POLICY_TYPE, CREATED_DATE)" +
                                                        " VALUES(:usernam, :polNo, :polType, sysdate)";

                            cmd.CommandText = instPolDetails;

                            OracleParameter oUser = new OracleParameter();
                            oUser.DbType = DbType.String;
                            oUser.Value = username;
                            oUser.ParameterName = "usernam";

                            OracleParameter oPol = new OracleParameter();
                            oPol.DbType = DbType.String;
                            oPol.Value = polNumber;
                            oPol.ParameterName = "polNo";

                            OracleParameter oType = new OracleParameter();
                            oType.DbType = DbType.String;
                            oType.Value = polDept;
                            oType.ParameterName = "polType";

                            cmd.Parameters.Add(oUser);
                            cmd.Parameters.Add(oPol);
                            cmd.Parameters.Add(oType);

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            trans.Commit();

                            mesg = getSavedGenPolicies(username, gridVw);

                        }
                        else
                        {
                            if (mesg.Contains("success"))
                            {
                                mesg = "Your NIC Number does not match with this policy number.<br/>Please enter correct policy number.";
                            }
                        }
                    }

                }

            }
            catch (Exception e)
            {
                mesg = "Error occurred while retrieving information";
                trans.Rollback();
                log logger = new log();
                logger.write_log("Failed at ULCustomer - getGenrlPolicies-proc2(clcode maybe 0): " + e.ToString());
            }
            finally
            {
                if (oconn.State == ConnectionState.Open)
                {
                    oconn.Close();
                }
            }
        }
        else
        {
            mesg = "Sorry. Internal error while retrieving policy details";
        }
        return mesg;
    }

    public string deleteSavedPolicy(string username, GridView gridVw, int rowIndex, string flag)
    {
        string mesg = "success";
        //todo- write method to get nic seperately (for getSavedPolicies). this way getting nic for secondtime when adding new policy can be avoided.
        try
        {
            Label lblPolNum = new Label();
            string deletePolicy = "";

            if (flag == "M")
            {
                lblPolNum = (Label)gridVw.Rows[rowIndex].FindControl("lblPolNum");

                deletePolicy = "Delete from SLIC_NET.POL_DET_FOR_WEB" +
                               " where POLICY_NUMBER = :polNo" +
                               " and POLICY_TYPE = 'M'";
            }
            else if (flag == "G")
            {
                lblPolNum = (Label)gridVw.Rows[rowIndex].FindControl("lblPolNum2");

                deletePolicy = "Delete from SLIC_NET.POL_DET_FOR_WEB" +
                               " where POLICY_NUMBER = :polNo" +
                               " and POLICY_TYPE <> 'M'";
            }            

            oconn.Open();
            using(OracleCommand cmd = new OracleCommand(deletePolicy, oconn))
            {
                cmd.Parameters.AddWithValue("polNo", lblPolNum.Text);
                //cmd.Parameters.AddWithValue("polTyp", flag);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            if (flag == "M")
            {
                mesg = getSavedMotPolicies(username, gridVw);
            }
            else if (flag == "G")
            {
                mesg = getSavedGenPolicies(username, gridVw);
            }
        }
        catch(Exception e)
        {
            mesg = "Error occurred while deleting policy";
            log logger = new log();
            logger.write_log("Failed at deleteSavedPolicy: " + e.ToString());
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

    public string getRenewalInfo(string polNo, string endDate, string dept, string polType, string pmtyp, out string insurdName, out string addrss, out string vehiNum, out string startDt,
                                 out string endDt, out double sumInsrd, out string basicPrem, out string rccPrem, out string tcPrem, out string vatAmt, out string admnAmt,
                                 out string stampFee, out string polFee, out string nbtVal, out string roadTax, out double totPremium, out double debitBalAmt, out double partialAmt, out bool payDisable, out string warnMsg, out DataSet dsCovers, out Int64 PayRefNo)
    {
        string mesg = "Next Renewal Information not available yet.";
        addrss = "";
        double totPrem = 0;
        insurdName = "";
        addrss = "";
        vehiNum = "";
        startDt = "";
        endDt = "";
        sumInsrd = 0;
        basicPrem = "0.00";
        rccPrem = "0.00";
        tcPrem = "0.00";
        vatAmt = "0.00";
        admnAmt = "0.00";
        stampFee = "0.00";
        polFee = "0.00";
        nbtVal = "0.00";
        totPremium = 0;
        debitBalAmt = 0;
        partialAmt = 0;
        payDisable = false;
        roadTax = "0.00";
        warnMsg = "";
        PayRefNo = 0;

        dsCovers = new DataSet();
        string covers = "";
        double renwValidDays = 0;

        double basicPremiumMotor = 0;
        double rccPremiumMotor = 0;
        double tcPremiumMotor = 0;
        double adminAmountMotor = 0;
        double stampFeeAmtMotor = 0;
        double policyFeeAmtMotor = 0;
        double roadTaxAmtMotor = 0;
        double vatAmtMotor = 0;
        double nbtAmtMotor = 0;

        try
        {
            DateTime renewDate = DateTime.ParseExact(endDate, "yyyy/MM/dd", CultureInfo.InvariantCulture).AddDays(1);
            mesg = mesg + "<br/><br/>Next renewal date is <u>" + renewDate.ToString("yyyy/MM/dd") + "<u/>.";
            oconn.Open();

            //bool unProcPayExist = false;
            double paidAmt = 0;
            string getUnProcdPayment = "Select Sum(PREMIUM)" +
                                       " from SLIC_NET.RENEWAL_DETAILS" +
                                       " where POL_NUM = :polNum" +
                                       " and DEPT = :dept" +
                                       " and POL_TYPE = :polTyp" +
                                       " and STATUS = 'A'" +
                                       " and PG_RET_CODE = 1" +
                                       " and PG_RSN_CODE = 1" +
                                       " and trunc(START_DATE - 1) = to_date(:endDate, 'yyyy/mm/dd')";

            using (OracleCommand cmd = new OracleCommand(getUnProcdPayment, oconn))
            {
                cmd.Parameters.AddWithValue("polNum", polNo);
                cmd.Parameters.AddWithValue("dept", dept);
                cmd.Parameters.AddWithValue("polTyp", polType);
                cmd.Parameters.AddWithValue("endDate", endDate);

                OracleDataReader paymntReader = cmd.ExecuteReader();

                while (paymntReader.Read())
                {
                    if (!paymntReader.IsDBNull(0))
                    { 
                        paidAmt = paymntReader.GetDouble(0);
                        //unProcPayExist = true;
                        //mesg = "Records indicate Renewal payment has already been done.";
                        warnMsg = "Records indicate Renewal payment of Rs." + paidAmt.ToString("N2") + " has already been done.";                    
                    }
                    
                }
                paymntReader.Close();
            }



            string getRefNo = "select pay_ref_no from slic_cnote.payment_refno where policy_no = :polNo_ref ";
            using (OracleCommand cmd = new OracleCommand(getRefNo, oconn))
            {
                cmd.Parameters.AddWithValue("polNo_ref", polNo);

                OracleDataReader paymntRefNoReader = cmd.ExecuteReader();

                while (paymntRefNoReader.Read())
                {
                    if (!paymntRefNoReader.IsDBNull(0))
                    {
                        PayRefNo = paymntRefNoReader.GetInt64(0);
                    }
                }

                paymntRefNoReader.Close();

            }


            //if (!unProcPayExist)--- Renewal is allowed multiple times (to handle part payments)
            // {
            if (dept == "M")
            {

                bool settleDebit = false;
                double daysFromDebitPay = 0;

                if (pmtyp.Substring(0, 1) == "2")      // if debit payment
                {
                    string debitOutstandingExist = "select sum(d.pmbal), max(round(trunc(sysdate) - trunc(d.pmpdt),2))" +
                                                   " from genpay.payfle p, lpay.debmast d" +
                                                   " where p.pmpol = :polNo" +
                                                   " and p.pmpol = d.pmpol" +
                                                   " and p.pmpdt = d.pmpdt" +
                                                   " and p.pmseq = d.pmseq" +
                                                   " and p.pmbrn = d.pmbrn" +
                                                   " and p.pmtyp like '2%'" +
                                                   " and d.pmdel = 0" +
                                                   " and p.pmdel = 0" +
                                                   " and p.pmdex = to_date(:endDate, 'yyyy/mm/dd')" +
                                                   " and d.pmbal > 0";

                    using (OracleCommand cmd = new OracleCommand(debitOutstandingExist, oconn))
                    {
                        cmd.Parameters.AddWithValue("polNo", polNo);
                        cmd.Parameters.AddWithValue("endDate", endDate);

                        OracleDataReader debitOutsReader = cmd.ExecuteReader();

                        //if (debitOutsReader.HasRows)
                        while (debitOutsReader.Read())
                        {
                            if (!debitOutsReader.IsDBNull(0))
                            {
                                debitBalAmt = debitOutsReader.GetDouble(0);
                                settleDebit = true;
                            }
                            if (!debitOutsReader.IsDBNull(1))
                            {
                                daysFromDebitPay = debitOutsReader.GetDouble(1);
                            }

                            //mesg = mesg + "<br/><br/>Debit Payment Settlement is pending. <br/><br/>Please contact Sales person or nearest SLIC Branch.";
                        }

                        debitOutsReader.Close();
                    }

                    //if (settleDebit)
                    //{
                    //    if (daysFromDebitPay > 10)
                    //    {
                    //        debitBalAmt = debitBalAmt + (debitBalAmt * 1.5 / 100);
                    //    }
                    //}

                }

                else
                {
                    string partialPaymntExist = " SELECT OUTSTANDING_TOTAL_AMOUNT" +
                                                 " FROM GENPAY.PAYMENT_MASTER" +
                                                 " where POLICY_ID= :polNo" +
                                                 " and policy_year = (select  max(policy_year) from GENPAY.PAYMENT_MASTER" +
                                                                     " where POLICY_ID= :polNo)" +
                                                 " and OUTSTANDING_TOTAL_AMOUNT > 0" +
                                                 " and TOTAL_AMOUNT <> OUTSTANDING_TOTAL_AMOUNT";

                    using (OracleCommand cmd = new OracleCommand(partialPaymntExist, oconn))
                    {
                        cmd.Parameters.AddWithValue("polNo", polNo);

                        OracleDataReader partialAmtReader = cmd.ExecuteReader();

                        if (partialAmtReader.HasRows)
                        {
                            while (partialAmtReader.Read())
                            {
                                if (!partialAmtReader.IsDBNull(0))
                                {
                                    partialAmt = partialAmtReader.GetDouble(0);
                                }

                                //mesg = mesg + "<br/><br/>Debit Payment Settlement is pending. <br/><br/>Please contact Sales person or nearest SLIC Branch.";
                            }
                        }
                        partialAmtReader.Close();
                    }
                }
                //string getMotRenwDetails = "select NAME, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, VEHICAL_NUMBER, to_char((POLICY_END_DATE + 1), 'yyyy/mm/dd'), to_char(add_months(POLICY_END_DATE,12), 'yyyy/mm/dd')," +
                //                        " ESTIMATEDVALUE, BASIC_PREMIUM, RCC_PREMIUM, TC_PREMIUM, VAT_AMOUNT, ADMIN_FEE_AMOUNT, STAMP_FEE," +
                //                        " POLICYFEE, NBT_VALUE, round(add_months((POLICY_END_DATE + 1),2) - sysdate,2), round(POLICY_END_DATE - POLICY_START_DATE,2), ROAD_TAX" +
                //                        " from MCOMP.MC_RENEWAL_NOTICE" +
                //                        " where policy_number = :polNo" +
                //                        " and trunc(POLICY_END_DATE) = to_date(:endDate, 'yyyy/mm/dd')";

                string getMotRenwDetails = "";

                if (!settleDebit)
                {
                    bool renewedPolicy = false;

                    string renewDetails = "Select policy_number" +
                                          " from MCOMP.MC_RENEWAL_NOTICE" +
                                          " where policy_number = :polNum";

                    using (OracleCommand cmd = new OracleCommand(renewDetails, oconn))
                    {
                        cmd.Parameters.AddWithValue("polNum", polNo);
                        OracleDataReader renewDetailsReader = cmd.ExecuteReader();

                        //if (debitOutsReader.HasRows)
                        while (renewDetailsReader.Read())
                        {
                            renewedPolicy = true;
                            break;
                        }
                        renewDetailsReader.Close();
                    }

                    if (renewedPolicy)
                    {

                        if (partialAmt > 0)
                        {
                            getMotRenwDetails = "select NAME, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, VEHICAL_NUMBER, to_char((POLICY_END_DATE + 1), 'yyyy/mm/dd'), to_char(add_months(POLICY_END_DATE,12), 'yyyy/mm/dd')," +
                                               " ESTIMATEDVALUE, BASIC_PREMIUM, RCC_PREMIUM, TC_PREMIUM, VAT_AMOUNT, ADMIN_FEE_AMOUNT, STAMP_FEE," +
                                               " POLICYFEE, NBT_VALUE, 1, round(POLICY_END_DATE - POLICY_START_DATE,2), ROAD_TAX, COVERS" +
                                               " from MCOMP.MC_RENEWAL_NOTICE" +
                                               " where policy_number = :polNo" +
                                               " and add_months(trunc(POLICY_END_DATE), 12) = to_date(:endDate, 'yyyy/mm/dd')" +
                                               " and add_months(trunc(sysdate), 1) >= trunc(POLICY_END_DATE + 1)";
                        }
                        else
                        {
                            getMotRenwDetails = "select NAME, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, VEHICAL_NUMBER, to_char((POLICY_END_DATE + 1), 'yyyy/mm/dd'), to_char(add_months(POLICY_END_DATE,12), 'yyyy/mm/dd')," +
                                                    " ESTIMATEDVALUE, BASIC_PREMIUM, RCC_PREMIUM, TC_PREMIUM, VAT_AMOUNT, ADMIN_FEE_AMOUNT, STAMP_FEE," +
                                                    " POLICYFEE, NBT_VALUE, round(trunc(POLICY_END_DATE) - trunc(sysdate),2), round(POLICY_END_DATE - POLICY_START_DATE,2), ROAD_TAX, COVERS" +
                                                    " from MCOMP.MC_RENEWAL_NOTICE" +
                                                    " where policy_number = :polNo" +
                                                    " and trunc(POLICY_END_DATE) = to_date(:endDate, 'yyyy/mm/dd')" +
                                                    " and add_months(trunc(sysdate), 1) >= trunc(POLICY_END_DATE + 1)";
                        }
                    }
                    else
                    {
                        if (partialAmt > 0)
                        {
                            getMotRenwDetails = "select PMNAM, PMAD1, PMAD2, PMAD3, PMAD4, PMVE2, to_char(PMDST, 'yyyy/mm/dd'), to_char(PMDEX, 'yyyy/mm/dd')," +
                                               " PMSUM, PMPRM, PMRCC, PMTC, PMGST, PMDLV, PMROAD," +
                                               " PMPLF, PMNBL, 1, round(PMDEX - PMDST,2), PMSTP, null" +
                                               " from GENPAY.PAYFLE" +
                                               " where pmpol = :polNo" +
                                               " and trunc(PMDEX) = to_date(:endDate, 'yyyy/mm/dd')" +
                                               " AND PMDEL = '0' order by pment desc";
                        }
                        /* else
                         {
                             getMotRenwDetails = "select PMNAM, PMAD1, PMAD2, PMAD3, PMAD4, PMVE2, to_char((PMDEX + 1), 'yyyy/mm/dd'), to_char(add_months(PMDEX,12), 'yyyy/mm/dd')," +
                                                    " PMSUM, PMPRM, PMRCC, PMTC, PMGST, PMDLV, PMROAD," +
                                                    " PMPLF, PMNBL, round(trunc(PMDEX) - trunc(sysdate),2), round(PMDEX - PMDST,2), PMSTP, null" +
                                                    " from GENPAY.PAYFLE" +
                                                    " where pmpol = :polNo" +
                                                    " and trunc(PMDEX) = to_date(:endDate, 'yyyy/mm/dd')" +
                                                    " and add_months(trunc(sysdate), 1) >= trunc(PMDEX + 1)";
                         }*/
                    }
                }
                else
                {
                    bool renewedPolicy = false;

                    string renewDetails = "Select policy_number" +
                                          " from MCOMP.MC_RENEWAL_NOTICE" +
                                          " where policy_number = :polNum";

                    using (OracleCommand cmd = new OracleCommand(renewDetails, oconn))
                    {
                        cmd.Parameters.AddWithValue("polNum", polNo);
                        OracleDataReader renewDetailsReader = cmd.ExecuteReader();

                        //if (debitOutsReader.HasRows)
                        while (renewDetailsReader.Read())
                        {
                            renewedPolicy = true;
                            break;
                        }
                        renewDetailsReader.Close();
                    }

                    //if (renewedPolicy)
                    //{
                    //    getMotRenwDetails = "select NAME, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, VEHICAL_NUMBER, to_char((POLICY_END_DATE + 1), 'yyyy/mm/dd'), to_char(add_months(POLICY_END_DATE,12), 'yyyy/mm/dd')," +
                    //                           " ESTIMATEDVALUE, BASIC_PREMIUM, RCC_PREMIUM, TC_PREMIUM, VAT_AMOUNT, ADMIN_FEE_AMOUNT, STAMP_FEE," +
                    //                           " POLICYFEE, NBT_VALUE, round(trunc(POLICY_END_DATE + 60) - trunc(sysdate),2), round(POLICY_END_DATE - POLICY_START_DATE,2), ROAD_TAX, COVERS" +
                    //                           " from MCOMP.MC_RENEWAL_NOTICE" +
                    //                           " where policy_number = :polNo" +
                    //                           " and add_months(trunc(POLICY_END_DATE), 12) = to_date(:endDate, 'yyyy/mm/dd')" +
                    //                           " and add_months(trunc(sysdate), 1) >= trunc(POLICY_END_DATE + 1)";
                    //}
                    //else
                    //{
                    getMotRenwDetails = "select PMNAM, PMAD1, PMAD2, PMAD3, PMAD4, PMVE2, to_char(PMDST, 'yyyy/mm/dd'), to_char(PMDEX, 'yyyy/mm/dd')," +
                                           " PMSUM, PMPRM, PMRCC, PMTC, PMGST, PMDLV, PMROAD," +
                                           " PMPLF, PMNBL, round(trunc(PMDST + 90) - trunc(sysdate),2), round(PMDEX - PMDST,2), PMSTP, null" +
                                           " from GENPAY.PAYFLE" +
                                           " where pmpol = :polNo" +
                                           " and trunc(PMDEX) = to_date(:endDate, 'yyyy/mm/dd')" +
                                           " AND PMDEL = '0' order by pment desc";
                    //}
                }

                using (OracleCommand cmd = new OracleCommand(getMotRenwDetails, oconn))
                {
                    cmd.Parameters.AddWithValue("polNo", polNo);
                    cmd.Parameters.AddWithValue("endDate", endDate);

                    OracleDataReader rnewReader = cmd.ExecuteReader();

                    while (rnewReader.Read())
                    {
                        if (!rnewReader.IsDBNull(0))
                        { insurdName = rnewReader.GetString(0); }
                        if (!rnewReader.IsDBNull(1))
                        { addrss = rnewReader.GetString(1); }
                        if (!rnewReader.IsDBNull(2))
                        {
                            if (addrss != "")
                            { addrss = addrss + "<br/>" + rnewReader.GetString(2); }
                            else
                            { addrss = rnewReader.GetString(2); }
                        }
                        if (!rnewReader.IsDBNull(3))
                        {
                            if (addrss != "")
                            { addrss = addrss + "<br/>" + rnewReader.GetString(3); }
                            else
                            { addrss = rnewReader.GetString(3); }
                        }
                        if (!rnewReader.IsDBNull(4))
                        {
                            if (addrss != "")
                            { addrss = addrss + "<br/>" + rnewReader.GetString(4); }
                            else
                            { addrss = rnewReader.GetString(4); }
                        }
                        if (!rnewReader.IsDBNull(5))
                        { vehiNum = rnewReader.GetString(5); }
                        if (!rnewReader.IsDBNull(6))
                        { startDt = rnewReader.GetString(6); }
                        if (!rnewReader.IsDBNull(7))
                        { endDt = rnewReader.GetString(7); }
                        if (!rnewReader.IsDBNull(8))
                        { sumInsrd = rnewReader.GetDouble(8); }
                        if (!rnewReader.IsDBNull(9))
                        { basicPrem = rnewReader.GetDouble(9).ToString("N2"); totPrem = rnewReader.GetDouble(9); basicPremiumMotor = rnewReader.GetDouble(9); }
                        if (!rnewReader.IsDBNull(10))
                        { rccPrem = rnewReader.GetDouble(10).ToString("N2"); totPrem = totPrem + rnewReader.GetDouble(10); rccPremiumMotor = rnewReader.GetDouble(10); }
                        if (!rnewReader.IsDBNull(11))
                        { tcPrem = rnewReader.GetDouble(11).ToString("N2"); totPrem = totPrem + rnewReader.GetDouble(11); tcPremiumMotor = rnewReader.GetDouble(11); }
                        if (!rnewReader.IsDBNull(12))
                        { vatAmt = rnewReader.GetDouble(12).ToString("N2"); totPrem = totPrem + rnewReader.GetDouble(12); }
                        if (!rnewReader.IsDBNull(13))
                        { admnAmt = rnewReader.GetDouble(13).ToString("N2"); totPrem = totPrem + rnewReader.GetDouble(13); adminAmountMotor = rnewReader.GetDouble(13); }
                        if (!rnewReader.IsDBNull(14))
                        { stampFee = rnewReader.GetDouble(14).ToString("N2"); totPrem = totPrem + rnewReader.GetDouble(14); stampFeeAmtMotor = rnewReader.GetDouble(14); }
                        if (!rnewReader.IsDBNull(15))
                        { polFee = rnewReader.GetDouble(15).ToString("N2"); totPrem = totPrem + rnewReader.GetDouble(15); policyFeeAmtMotor = rnewReader.GetDouble(15); }
                        if (!rnewReader.IsDBNull(16))
                        { /*nbtVal = rnewReader.GetDouble(16).ToString("N2"); totPrem = totPrem + rnewReader.GetDouble(16);*/
                            admnAmt = (adminAmountMotor + rnewReader.GetDouble(16)).ToString("N2"); totPrem = totPrem + rnewReader.GetDouble(16); //Change for SSC Levy - 05/07/2022 Vasana
                        }
                        if (!rnewReader.IsDBNull(17))
                        {
                            renwValidDays = rnewReader.GetDouble(17);
                            if (!settleDebit && renwValidDays < 0)
                            {
                                payDisable = true;
                                mesg = "Sorry, Renew Date has expired. Please <a href=\"/ContactUs.aspx\"><span style=\"font-weight:bold; color:#8C8C8C;\">contact us.</span></a>";
                            }
                            else if (settleDebit && renwValidDays < 0)
                            {
                                payDisable = true;
                                mesg = "Sorry, Debit Settlement Date has expired. Please <a href=\"/ContactUs.aspx\"><span style=\"font-weight:bold; color:#8C8C8C;\">contact us.</span></a>";
                            }
                        }
                        if (!rnewReader.IsDBNull(18))
                        {
                            double renPeriod = rnewReader.GetDouble(18);
                            if (renPeriod <= 0)
                            {
                                payDisable = true;
                                //mesg = "Sorry, Renew Period is not valid";
                                mesg = "Sorry, unable to renew. Please contact us.";
                            }
                        }
                        if (!rnewReader.IsDBNull(19))
                        {
                            roadTax = rnewReader.GetDouble(19).ToString("N2"); totPrem = totPrem + rnewReader.GetDouble(19); roadTaxAmtMotor = rnewReader.GetDouble(19);
                        }
                        if (!rnewReader.IsDBNull(20))
                        {
                            covers = rnewReader.GetString(20);
                        }

                        if (payDisable != true)
                        {
                            mesg = "success";
                        }
                    }
                    rnewReader.Close();
                }
                totPremium = totPrem;

                if (covers.Length > 0)
                {
                    covers = covers.Replace('-', '.');
                    string getCovers = "Select ID, COVER from SLIC_NET.MOTOR_COVERS" +
                                       " where ID in (" + covers + ")";

                    using (OracleDataAdapter oraAdapter = new OracleDataAdapter(getCovers, oconn))
                    {
                        dsCovers.Clear();
                        oraAdapter.Fill(dsCovers);
                    }
                }

                string getSumInsured = "Select pdsuma from mcomp.mcactupdet" +
                                       " where pdrefer = :polNo" +
                                       " and pdyear = (select max(pdyear) from mcomp.mcactupdet where pdrefer = :polNo)";

                using (OracleCommand cmd = new OracleCommand(getSumInsured, oconn))
                {
                    cmd.Parameters.AddWithValue("polNo", polNo);
                    OracleDataReader sumInsuredReader = cmd.ExecuteReader();

                    if (sumInsuredReader.HasRows)
                    {
                        while (sumInsuredReader.Read())
                        {
                            if (!sumInsuredReader.IsDBNull(0))
                            {
                                sumInsrd = sumInsuredReader.GetDouble(0);
                            }
                        }
                    }
                    sumInsuredReader.Close();
                }

            }
            else if (dept != "M")
            {
                string getGenRenwDetails = "";

                if (polType == "HIP")
                {
                    getGenRenwDetails = "select PMNAM, PMAD1, PMAD2, PMAD3, PMAD4, to_char(PMDEX, 'yyyy/mm/dd'), to_char(add_months(PMDEX,12), 'yyyy/mm/dd')," +
                                            " decode(PMSUM, 1250000, 1000000, 2500000, 2000000, PMSUM) PMSUM, round(add_months(PMDEX, 12) - sysdate,2), round(PMDEX - PMDST,2)" +
                                            " from GENPAY.PAYFLE" +
                                            " where PMPOL = :polNo" +
                                            " and trunc(PMDEX) = to_date(:endDate, 'yyyy/mm/dd')" +
                                            " and PMDEP <> 'M' " +
                                            " and ( ((PMCOD='N')  AND (PMTYP=111 or PMTYP=112  or PMTYP=211 or PMTYP=212) ) " +
                                            " OR((PMTYP=311 or PMTYP=321) AND PMCOD='1') or ( (PMTYP=121 or PMTYP=122 or PMTYP=221 or PMTYP=222)" +
                                            " OR   ((PMTYP=311 or PMTYP=321) AND PMCOD='0'))) AND PMDEL = '0' ";
                }
                else if (polType == "AMP")
                {
                    getGenRenwDetails = "select NAME_1, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, to_char(START_DATE, 'yyyy/mm/dd'), to_char(END_DATE, 'yyyy/mm/dd')," +
                                        " CURNT_PLAN_LIMIT, round(END_DATE - sysdate,2), round(END_DATE - START_DATE,2), NET_PREMIUM, ADMIN_FEE, POLICY_FEE, NBT, VAT, FINAL_PREMIUM" +
                                        " from SLIGEN.AMP_RENEWAL_NOTICE" +
                                        " where POLICY_NUMBER = :polNo" +
                                        " and trunc(START_DATE - 1) =  to_date(:endDate, 'yyyy/mm/dd')";

                }
                else if (dept == "F" || dept == "G")
                {
                    getGenRenwDetails = "select PMNAM, PMAD1, PMAD2, PMAD3, PMAD4, to_char(PMDEX, 'yyyy/mm/dd'), to_char(add_months(PMDEX,12), 'yyyy/mm/dd')," +
                                            " PMSUM, round(add_months(PMDEX, 12) - sysdate,2), round(PMDEX - PMDST,2)" +
                                            " from GENPAY.PAYFLE" +
                                            " where PMPOL = :polNo" +
                                            " and trunc(PMDEX) = to_date(:endDate, 'yyyy/mm/dd')" +
                                            " and PMDEP <> 'M' " +
                                            " and ( ((PMCOD='N')  AND (PMTYP=111 or PMTYP=112  or PMTYP=211 or PMTYP=212) ) " +
                                            " OR((PMTYP=311 or PMTYP=321) AND PMCOD='1') or ( (PMTYP=121 or PMTYP=122 or PMTYP=221 or PMTYP=222)" +
                                            " OR   ((PMTYP=311 or PMTYP=321) AND PMCOD='0'))) AND PMDEL = '0' ";
                }
                else
                {
                    getGenRenwDetails = "select PMNAM, PMAD1, PMAD2, PMAD3, PMAD4, to_char(PMDEX + 1, 'yyyy/mm/dd'), to_char(add_months(PMDEX,12), 'yyyy/mm/dd')," +
                                            " PMSUM, round(add_months(PMDEX, 12) - sysdate,2), round(PMDEX - PMDST,2)" +
                                            " from GENPAY.PAYFLE" +
                                            " where PMPOL = :polNo" +
                                            " and trunc(PMDEX) = to_date(:endDate, 'yyyy/mm/dd')" +
                                            " and PMDEP <> 'M' " +
                                            " and ( ((PMCOD='N')  AND (PMTYP=111 or PMTYP=112  or PMTYP=211 or PMTYP=212) ) " +
                                            " OR((PMTYP=311 or PMTYP=321) AND PMCOD='1') or ( (PMTYP=121 or PMTYP=122 or PMTYP=221 or PMTYP=222)" +
                                            " OR   ((PMTYP=311 or PMTYP=321) AND PMCOD='0'))) AND PMDEL = '0' ";
                }

                using (OracleCommand cmd = new OracleCommand(getGenRenwDetails, oconn))
                {
                    cmd.Parameters.AddWithValue("polNo", polNo);
                    cmd.Parameters.AddWithValue("endDate", endDate);

                    OracleDataReader rnewReader = cmd.ExecuteReader();

                    while (rnewReader.Read())
                    {
                        if (!rnewReader.IsDBNull(0))
                        { insurdName = rnewReader.GetString(0); }
                        if (!rnewReader.IsDBNull(1))
                        { addrss = rnewReader.GetString(1); }
                        if (!rnewReader.IsDBNull(2))
                        {
                            if (addrss != "")
                            { addrss = addrss + "<br/>" + rnewReader.GetString(2); }
                            else
                            { addrss = rnewReader.GetString(2); }
                        }
                        if (!rnewReader.IsDBNull(3))
                        {
                            if (addrss != "")
                            { addrss = addrss + "<br/>" + rnewReader.GetString(3); }
                            else
                            { addrss = rnewReader.GetString(3); }
                        }
                        if (!rnewReader.IsDBNull(4))
                        {
                            if (addrss != "")
                            { addrss = addrss + "<br/>" + rnewReader.GetString(4); }
                            else
                            { addrss = rnewReader.GetString(4); }
                        }
                        if (!rnewReader.IsDBNull(5))
                        { startDt = rnewReader.GetString(5); }
                        if (!rnewReader.IsDBNull(6))
                        { endDt = rnewReader.GetString(6); }
                        if (!rnewReader.IsDBNull(7))
                        { sumInsrd = rnewReader.GetDouble(7); }
                        if (!rnewReader.IsDBNull(8))
                        {
                            renwValidDays = rnewReader.GetDouble(8);
                            if (renwValidDays < 0)
                            {
                                payDisable = true;
                                mesg = "Sorry, Renew Date has expired. Please contact us.";
                            }
                        }
                        if (!rnewReader.IsDBNull(9))
                        {
                            double renPeriod = rnewReader.GetDouble(9);
                            if (renPeriod <= 0)
                            {
                                payDisable = true;
                                //mesg = "Sorry, Renew Period is not valid";
                                mesg = "Sorry, unable to renew. Please contact us.";
                            }
                        }
                        if (polType == "AMP")
                        {
                            if (!rnewReader.IsDBNull(10))
                            {
                                basicPrem = rnewReader.GetDouble(10).ToString("N2");
                            }
                            if (!rnewReader.IsDBNull(11))
                            {
                                if (!rnewReader.IsDBNull(13)) //Change for SSC Levy - 05/07/2022 Vasana
                                {
                                    admnAmt = (rnewReader.GetDouble(11) + rnewReader.GetDouble(13)).ToString("N2");
                                }
                                else
                                {
                                    admnAmt = rnewReader.GetDouble(11).ToString("N2");
                                }
                            }
                            if (!rnewReader.IsDBNull(12))
                            {
                                polFee = rnewReader.GetDouble(12).ToString("N2");
                            }
                            //if (!rnewReader.IsDBNull(13)) // Change for SSC Levy - 05/07/2022
                            //{
                            //    nbtVal = rnewReader.GetDouble(13).ToString("N2");
                            //}
                            if (!rnewReader.IsDBNull(14))
                            {
                                vatAmt = rnewReader.GetDouble(14).ToString("N2");
                            }
                            if (!rnewReader.IsDBNull(15))
                            {
                                totPremium = rnewReader.GetDouble(15);
                            }
                        }
                        if (polType != "HIP" && payDisable != true)
                        {
                            mesg = "success";
                        }
                    }
                    rnewReader.Close();
                }

                if (polType == "HIP" && payDisable == false)
                {
                    Proposal pro = new Proposal();
                    double basePrem = 0;
                    double admnFee = 0;
                    double policyFee = 0;
                    double nbt = 0;
                    double vat = 0;

                    if (pro.getHPParameters("", renewDate.ToString("yyyy/MM/dd"), sumInsrd, out sumInsrd, out basePrem, out admnFee, out policyFee, out nbt, out vat, out totPremium))
                    {
                        basicPrem = basePrem.ToString("N2");
                        admnAmt = (admnFee + nbt).ToString("N2"); //change for SSC Levy - 05/07/2022 - Vasana
                        polFee = policyFee.ToString("N2");
                        //nbtVal = nbt.ToString("N2"); //change for SSC Levy - 05/07/2022 - Vasana
                        vatAmt = vat.ToString("N2");
                        if (payDisable != true)
                        {
                            mesg = "success";
                        }
                    }
                    else
                    {
                        mesg = "Error occurred while retrieving renewal information";
                        log logger = new log();
                        logger.write_log("Failed at getRenewalInfo - while getting HPParameters");
                    }
                }

            }

            //----------------------------added due to Vat change 2019/12/07----------------------------------------
                if (dept == "M" && startDt != "")
                {
                //-----------------NBL and VAT Calculation--------------------------------           
                //DateTime.Now.ToString("yyyy/MM/dd")
                    double totPremiumAmt = basicPremiumMotor + rccPremiumMotor + tcPremiumMotor;

                    using (OracleCommand oraComm = new OracleCommand())
                    {
                        oraComm.Connection = oconn;
                        oraComm.CommandType = CommandType.StoredProcedure;
                        oraComm.CommandText = "GENPAY.CALCULATE_NBL_AND_VAT";
                        oraComm.Parameters.Clear();
                        oraComm.Parameters.AddWithValue("taxLiableAmount", totPremiumAmt + adminAmountMotor + policyFeeAmtMotor + roadTaxAmtMotor);
                        //dm.oraComm.Parameters.AddWithValue("requestDate", DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture));
                        oraComm.Parameters.AddWithValue("requestDate", DateTime.ParseExact(startDt, "yyyy/MM/dd", CultureInfo.InvariantCulture));
                        oraComm.Parameters.Add("nblAmount", OracleType.Number).Direction = ParameterDirection.Output;
                        oraComm.Parameters.Add("vatAmount", OracleType.Number).Direction = ParameterDirection.Output;

                        OracleDataReader dr = oraComm.ExecuteReader(CommandBehavior.CloseConnection);

                        nbtAmtMotor = double.Parse(oraComm.Parameters["nblAmount"].Value.ToString());// comm.Parameters("nblAmount");
                        vatAmtMotor = double.Parse(oraComm.Parameters["vatAmount"].Value.ToString()); //comm.Parameters("vatAmount");

                        //------------------------------------------------------------
                        dr.Close();
                    }

                    totPremium = totPremiumAmt + adminAmountMotor + policyFeeAmtMotor + roadTaxAmtMotor + stampFeeAmtMotor + vatAmtMotor + nbtAmtMotor;
                    vatAmt = vatAmtMotor.ToString("N2");
                    //nbtVal = nbtAmtMotor.ToString("N2"); //Change for SSC Levy - 05/07/2022 Vasana
                    admnAmt = (adminAmountMotor + nbtAmtMotor).ToString("N2");
                }
            //---------------------------------------------------------------------------------------------------------------


        }
        catch(Exception e)
        {
            mesg = "Error occurred while retrieving renewal information";
            log logger = new log();
            logger.write_log("Failed at ULCustomer - getRenewalInfo " + e.ToString());
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

    /***************** Updated by MANORI *************/

    public string getMobileNumbr(string userName)
    {
        string mobileNum = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getMobile = "Select MOBILE_NUMBER from ULWEB.WEBUSERS where UPPER(USERNAME) = UPPER(:userName)";

            using (OracleCommand cmd = new OracleCommand(getMobile, oconn))
            {
                cmd.Parameters.AddWithValue("username", userName);

                OracleDataReader mobileReader = (OracleDataReader)cmd.ExecuteReader();

                if (mobileReader.HasRows)
                {
                    while (mobileReader.Read())
                    {
                        if (!mobileReader.IsDBNull(0))
                        {
                            mobileNum = mobileReader.GetString(0);
                        }

                    }
                    mobileReader.Close();
                }
            }

        }
        catch (Exception e)
        {
            mobileNum = "Error";
            log logger = new log();
            logger.write_log("Failed at ULCustomer - getMobileNumbr: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return mobileNum;
    }

    public string getEmailAdrs(string userName)
    {
        string email = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getEmail = "Select EMAIL from ULWEB.WEBUSERS where UPPER(USERNAME) = UPPER(:userName)";

            using (OracleCommand cmd = new OracleCommand(getEmail, oconn))
            {
                cmd.Parameters.AddWithValue("username", userName);

                OracleDataReader emailReader = (OracleDataReader)cmd.ExecuteReader();

                if (emailReader.HasRows)
                {
                    while (emailReader.Read())
                    {
                        if (!emailReader.IsDBNull(0))
                        {
                            email = emailReader.GetString(0);
                        }

                    }
                    emailReader.Close();
                }
            }

        }
        catch (Exception e)
        {
            email = "Error";
            log logger = new log();
            logger.write_log("Failed at ULCustomer - getLoginName: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return email;
    }

       public string getMobileNo(string username)
    {
        string retrievedMobileNo = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string checkMobileMatch = "select mobile_number from ulweb.webusers where UPPER(username) = UPPER(:username)";

            using (OracleCommand cmd = new OracleCommand(checkMobileMatch, oconn))
            {
                cmd.Parameters.AddWithValue("username", username.ToUpper());

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            retrievedMobileNo = reader.GetString(0);
                        }

                    }
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            username = "Error";
            log logger = new log();
            //logger.write_log("mobile no does not exist" + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        log logger2 = new log();
        logger2.write_log("mobileNo " + retrievedMobileNo);
        return retrievedMobileNo;
    }

    public string getEmailMobile(string email)
    {
        string retrievedMobileNo = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string checkMobileMatch = "select mobile_number from ulweb.webusers where email =:email";

            using (OracleCommand cmd = new OracleCommand(checkMobileMatch, oconn))
            {
                cmd.Parameters.AddWithValue("email", email);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            retrievedMobileNo = reader.GetString(0);
                        }

                    }
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            email = "Error";
            log logger = new log();
            //logger.write_log("mobile no does not exist" + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        log logger2 = new log();
        logger2.write_log("mobileNo " + retrievedMobileNo);
        return retrievedMobileNo;
    }

    public bool checkUsernameExist(string username)
    {
        bool usernameExist = false;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string checkUserNameMatch = "select username from ulweb.webusers where UPPER(username) = UPPER(:username)";

            using (OracleCommand cmd = new OracleCommand(checkUserNameMatch, oconn))
            {
                cmd.Parameters.AddWithValue("username", username.ToUpper());

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    usernameExist = true;
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            usernameExist = false;
            log logger = new log();
            //logger.write_log("Username does not exist: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return usernameExist;
    }

    public bool checkEmailExist(string email)
    {
        bool emailExist = false;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string checkEmailMatch = "select email from ulweb.webusers where email = :email";

            using (OracleCommand cmd = new OracleCommand(checkEmailMatch, oconn))
            {
                cmd.Parameters.AddWithValue("email", email);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    emailExist = true;
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            emailExist = false;
            log logger = new log();
            //logger.write_log("Email does not exist: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return emailExist;
    }

    
        
}