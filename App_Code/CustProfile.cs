using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using HashLibrary;
using System.Data.Odbc;
using IBM.Data.DB2.iSeries;
using System.Collections.Generic;
using System.Globalization;

/// <summary>
/// Summary description for CustProfile
/// </summary>
public class CustProfile
{
    //OdbcConnection db2conn = new OdbcConnection(ConfigurationManager.AppSettings["DB2"]);
    OracleConnection oconnGen = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    OracleConnection oconnLife = new OracleConnection(ConfigurationManager.AppSettings["OraLifeDB"]);

    public string O_username { get; private set; }
    public string O_title { get; private set; }
    public string O_firstName { get; private set; }
    public string O_lastName { get; private set; }
    public string O_othNames { get; private set; }
    public string O_email { get; private set; }
    public string O_nicNo { get; private set; }
    public string O_dateOfBirth { get; private set; }
    public string O_gender { get; private set; }
    public string O_addrss1 { get; private set; }
    public string O_addrss2 { get; private set; }
    public string O_addrss3 { get; private set; }
    public string O_addrss4 { get; private set; }
    public string O_cityTown { get; private set; }
    public string O_postCode { get; private set; }
    public string O_country { get; private set; }
    public string O_mobileNumber { get; private set; }
    public string O_homeNumber { get; private set; }
    public string O_officeNumber { get; private set; }

    public string O_ocupation { get; private set; }
    public string O_srilankan { get; private set; }
    public string O_passportNo { get; private set; }

    public string O_cus_Name { get; private set; }
    public string O_policy_no { get; private set; }
    public string O_commencement_Date { get; private set; }
    public string O_Sum_Assured { get; private set; }
    public string O_Premium { get; private set; }


	public CustProfile()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public CustProfile(string uname)
	{
        DateTime dob = DateTime.Now;
        string dateOfBirth = "";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }

            string getDetails = "Select TITLE, FIRST_NAME, LAST_NAME, OTHER_NAMES, EMAIL, NIC_NO, DATE_OF_BIRTH, GENDER, ADDRESS1," +
                                " ADDRESS2, ADDRESS3, ADDRESS4, TOWN_CITY, POSTAL_CODE, COUNTRY, MOBILE_NUMBER, HOME_NUMBER, OFFICE_NUMBER, DESIGNATION, LANKAN_FLAG, PASSPORT_NO" +
                                " from ULWEB.WEBUSERS where USERNAME = :username";

            using (OracleCommand com = new OracleCommand(getDetails, oconnGen))
            {
                com.Parameters.AddWithValue("username", uname.Trim());

                OracleDataReader infoReader = (OracleDataReader)com.ExecuteReader();

                while (infoReader.Read())
                {
                    O_username = uname.Trim();
                    if (!infoReader.IsDBNull(0))
                    {
                        O_title = infoReader.GetString(0);
                    }

                    if (!infoReader.IsDBNull(1))
                    {
                        O_firstName = infoReader.GetString(1);
                    }

                    if (!infoReader.IsDBNull(2))
                    {
                        O_lastName = infoReader.GetString(2);
                    }

                    if (!infoReader.IsDBNull(3))
                    {
                        O_othNames = infoReader.GetString(3);
                    }

                    if (!infoReader.IsDBNull(4))
                    {
                        O_email = infoReader.GetString(4);
                    }

                    if (!infoReader.IsDBNull(5))
                    {
                        O_nicNo = infoReader.GetString(5);
                    }
                    if (!infoReader.IsDBNull(6))
                    {
                        dateOfBirth = infoReader.GetDecimal(6).ToString();
                        dob = DateTime.ParseExact(dateOfBirth, "yyyymmdd", CultureInfo.InvariantCulture);
                        dateOfBirth = dob.ToString("yyyy/mm/dd");
                        O_dateOfBirth = dob.ToString("yyyy/mm/dd");
                    }
                    if (!infoReader.IsDBNull(7))
                    {
                        O_gender = infoReader.GetString(7);
                    }
                    if (!infoReader.IsDBNull(8))
                    {
                        O_addrss1 = infoReader.GetString(8);
                    }
                    if (!infoReader.IsDBNull(9))
                    {
                        O_addrss2 = infoReader.GetString(9);
                    }
                    if (!infoReader.IsDBNull(10))
                    {
                        O_addrss3 = infoReader.GetString(10);
                    }
                    if (!infoReader.IsDBNull(11))
                    {
                        O_addrss4 = infoReader.GetString(11);
                    }
                    if (!infoReader.IsDBNull(12))
                    {
                        O_cityTown = infoReader.GetString(12);
                    }
                    if (!infoReader.IsDBNull(13))
                    {
                        O_postCode = infoReader.GetString(13);
                    }
                    if (!infoReader.IsDBNull(14))
                    {
                        O_country = infoReader.GetString(14);
                    }
                    if (!infoReader.IsDBNull(15))
                    {
                        O_mobileNumber = infoReader.GetString(15);
                    }
                    if (!infoReader.IsDBNull(16))
                    {
                        O_homeNumber = infoReader.GetString(16);
                    }
                    if (!infoReader.IsDBNull(17))
                    {
                        O_officeNumber = infoReader.GetString(17);
                    }
                    if (!infoReader.IsDBNull(18))
                    {
                        O_ocupation = infoReader.GetString(18);
                    }
                    if (!infoReader.IsDBNull(19))
                    {
                        O_srilankan = infoReader.GetString(19);
                    }
                    if (!infoReader.IsDBNull(20))
                    {
                        O_passportNo = infoReader.GetString(20);
                    }
                    //mesg = "success";
                }
                infoReader.Close();
            }
        }
        catch (Exception e)
        {
            //mesg = "Error occured while retrieving information";
            log logger = new log();
            logger.write_log("Failed at CustProfile Constructor: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }
	}


    public string getProfileInfo(string username, out string title, out string firstName, out string lastName, out string othNames,
                                 out string email, out string nicNo, out string dateOfBirth, out string gender, out string addrss1,
                                 out string addrss2, out string addrss3, out string addrss4, out string cityTown, out string postCode,
                                 out string country, out string mobileNumber, out string homeNumber, out string officeNumber, out string occupation, out string sriLankan, out string passport)
    {
        string mesg = "Profile information is not available";
        title = "";
        firstName = "";
        lastName = "";
        othNames = "";
        email = "";
        nicNo = "";
        dateOfBirth = "";
        gender = "";
        addrss1 = "";
        addrss2 = "";
        addrss3 = "";
        addrss4 = "";
        cityTown = "";
        postCode = "";
        country = "";
        mobileNumber = "";
        homeNumber = "";
        officeNumber = "";
        occupation = "";
        sriLankan = "";
        passport = "";

        DateTime dob = DateTime.Now;

        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }

            string getDetails = "Select TITLE, FIRST_NAME, LAST_NAME, OTHER_NAMES, EMAIL, NIC_NO, DATE_OF_BIRTH, GENDER, ADDRESS1," +
                                " ADDRESS2, ADDRESS3, ADDRESS4, TOWN_CITY, POSTAL_CODE, COUNTRY, MOBILE_NUMBER, HOME_NUMBER, OFFICE_NUMBER, DESIGNATION, LANKAN_FLAG, PASSPORT_NO" +
                                " from ULWEB.WEBUSERS where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(getDetails, oconnGen))
            {
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());

                OracleDataReader infoReader = (OracleDataReader)com.ExecuteReader();

                while (infoReader.Read())
                {
                    O_username = username.Trim();
                    if (!infoReader.IsDBNull(0))
                    {
                        title = infoReader.GetString(0);
                        O_title = infoReader.GetString(0);
                    }

                    if (!infoReader.IsDBNull(1))
                    {
                        firstName = infoReader.GetString(1);
                        O_firstName = infoReader.GetString(1);
                    }

                    if (!infoReader.IsDBNull(2))
                    {
                        lastName = infoReader.GetString(2);
                        O_lastName = infoReader.GetString(2);
                    }

                    if (!infoReader.IsDBNull(3))
                    {
                        othNames = infoReader.GetString(3);
                        O_othNames = infoReader.GetString(3);
                    }

                    if (!infoReader.IsDBNull(4))
                    {
                        email = infoReader.GetString(4);
                        O_email = infoReader.GetString(4);
                    }

                    if (!infoReader.IsDBNull(5))
                    {
                        nicNo = infoReader.GetString(5);
                        O_nicNo = infoReader.GetString(5);
                    }
                    if (!infoReader.IsDBNull(6))
                    {
                        dateOfBirth = infoReader.GetDecimal(6).ToString().Trim();
                        dob = DateTime.ParseExact(dateOfBirth, "yyyymmdd", CultureInfo.InvariantCulture);
                        dateOfBirth = dob.ToString("yyyy/mm/dd");
                        O_dateOfBirth = dob.ToString("yyyy/mm/dd");
                    }
                    if (!infoReader.IsDBNull(7))
                    {
                        gender = infoReader.GetString(7);
                        O_gender = infoReader.GetString(7);
                    }
                    if (!infoReader.IsDBNull(8))
                    {
                        addrss1 = infoReader.GetString(8);
                        O_addrss1 = infoReader.GetString(8);
                    }
                    if (!infoReader.IsDBNull(9))
                    {
                        addrss2 = infoReader.GetString(9);
                        O_addrss2 = infoReader.GetString(9);
                    }
                    if (!infoReader.IsDBNull(10))
                    {
                        addrss3 = infoReader.GetString(10);
                        O_addrss3 = infoReader.GetString(10);
                    }
                    if (!infoReader.IsDBNull(11))
                    {
                        addrss4 = infoReader.GetString(11);
                        O_addrss4 = infoReader.GetString(11);
                    }
                    if (!infoReader.IsDBNull(12))
                    {
                        cityTown = infoReader.GetString(12);
                        O_cityTown = infoReader.GetString(12);
                    }
                    if (!infoReader.IsDBNull(13))
                    {
                        postCode = infoReader.GetString(13);
                        O_postCode = infoReader.GetString(13);
                    }
                    if (!infoReader.IsDBNull(14))
                    {
                        country = infoReader.GetString(14);
                        O_country = infoReader.GetString(14);
                    }
                    if (!infoReader.IsDBNull(15))
                    {
                        mobileNumber = infoReader.GetString(15);
                        O_mobileNumber = infoReader.GetString(15);
                    }
                    if (!infoReader.IsDBNull(16))
                    {
                        homeNumber = infoReader.GetString(16);
                        O_homeNumber = infoReader.GetString(16);
                    }
                    if (!infoReader.IsDBNull(17))
                    {
                        officeNumber = infoReader.GetString(17);
                        O_officeNumber = infoReader.GetString(17);
                    }

                    if (!infoReader.IsDBNull(18))
                    {
                        occupation = infoReader.GetString(18);
                        O_ocupation = infoReader.GetString(18);
                    }
                    if (!infoReader.IsDBNull(18))
                    {
                        occupation = infoReader.GetString(18);
                        O_ocupation = infoReader.GetString(18);
                    }
                    if (!infoReader.IsDBNull(18))
                    {
                        occupation = infoReader.GetString(18);
                        O_ocupation = infoReader.GetString(18);
                    }

                    if (!infoReader.IsDBNull(19))
                    {
                        sriLankan = infoReader.GetString(19);
                        O_srilankan = infoReader.GetString(19);
                    }

                    if (!infoReader.IsDBNull(20))
                    {
                        passport = infoReader.GetString(20);
                        O_passportNo = infoReader.GetString(20);
                    }

                    mesg = "success";
                }
                infoReader.Close();
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while retrieving information";
            log logger = new log();
            logger.write_log("Failed at getProfileInfo: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfileInfo(string username, string title, string firstName, string lastName, string othNames,
                                 string email, string nicNo, int dateOfBirth, string gender, string addrss1,
                                 string addrss2, string addrss3, string addrss4, string cityTown, string postCode,
                                 string country, string mobileNumber, string homeNumber, string officeNumber, string occupation)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }

            string updateProfile = "Update ulweb.webusers set TITLE = :title, FIRST_NAME = :firstName, LAST_NAME = :lastName, OTHER_NAMES = :othNames, EMAIL = :email, NIC_NO = :nicNo," +
                                    " DATE_OF_BIRTH = :dateOfBirth, GENDER = :gender, ADDRESS1 = :addrss1, ADDRESS2 = :addrss2, ADDRESS3 = :addrss3, ADDRESS4 = :addrss4, TOWN_CITY = :cityTown, " +
                                    " POSTAL_CODE = :postCode, COUNTRY = :country, MOBILE_NUMBER= :mobileNumber, HOME_NUMBER = :homeNumber, OFFICE_NUMBER = :officeNumber, DESIGNATION = :designation" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("title", title.Trim());
                com.Parameters.AddWithValue("firstName", firstName.Trim());
                com.Parameters.AddWithValue("lastName", lastName.Trim());
                com.Parameters.AddWithValue("othNames", othNames.Trim());
                com.Parameters.AddWithValue("email", email.Trim());
                com.Parameters.AddWithValue("nicNo", nicNo.Trim());
                com.Parameters.AddWithValue("dateOfBirth", dateOfBirth);
                com.Parameters.AddWithValue("gender", gender.Trim());
                com.Parameters.AddWithValue("addrss1", addrss1.Trim());
                com.Parameters.AddWithValue("addrss2", addrss2.Trim());
                com.Parameters.AddWithValue("addrss3", addrss3.Trim());
                com.Parameters.AddWithValue("addrss4", addrss4.Trim());
                com.Parameters.AddWithValue("cityTown", cityTown.Trim());
                com.Parameters.AddWithValue("postCode", postCode.Trim());
                com.Parameters.AddWithValue("country", country.Trim());
                com.Parameters.AddWithValue("mobileNumber", mobileNumber.Trim());
                com.Parameters.AddWithValue("homeNumber", homeNumber.Trim());
                com.Parameters.AddWithValue("officeNumber", officeNumber.Trim());
                com.Parameters.AddWithValue("designation", occupation.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());

                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating profile information";
            log logger = new log();
            logger.write_log("Failed at saveProfileInfo: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_title(string username, string title)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set TITLE = :title" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("title", title.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());

                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating title information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_title: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_firstname(string username, string firstName)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }

            string updateProfile = "Update ulweb.webusers set FIRST_NAME = :firstName" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("firstName", firstName.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());

                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating firstname information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_firstname: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_lastname(string username, string lastName)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set LAST_NAME = :lastName" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("lastName", lastName.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());
                              
                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating lastname information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_lastname: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_othernames(string username, string othNames)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set OTHER_NAMES = :othNames" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("othNames", othNames.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());

                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating othernames information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_othernames: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_email(string username, string email)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set EMAIL = :email" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("email", email.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());                            

                int count = com.ExecuteNonQuery();

                mesg = "success";
            }            

            if (oconnLife.State != ConnectionState.Open)
            {
                oconnLife.Open();
            }
            string updateLifeUserInfo = "Update SLIC_NET_LIFE.USER_NOTIFY_INFO set EMAIL = :email" +
                                        " where UPPER(USERNAME) = :username";

            using (OracleCommand cmd = new OracleCommand(updateLifeUserInfo, oconnLife))
            {
                cmd.Parameters.AddWithValue("email", email.Trim());
                cmd.Parameters.AddWithValue("username", username.Trim().ToUpper());

                cmd.ExecuteNonQuery();

                mesg = "success";
            }
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateGenUserInfo = "Update SLIC_NET.USER_NOTIFY_INFO set EMAIL = :email" +
                                        " where UPPER(USERNAME) = :username";

            using (OracleCommand cmd = new OracleCommand(updateGenUserInfo, oconnGen))
            {
                cmd.Parameters.AddWithValue("email", email.Trim());
                cmd.Parameters.AddWithValue("username", username.Trim().ToUpper());

                cmd.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating email information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_email: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
            oconnLife.Close();
           // oconnGen.Close();
        }

        return mesg;
    }

    public string saveProfile_nicno(string username, string nicNo)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set NIC_NO = :nicNo" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("nicNo", nicNo.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());
                
                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating NIC No. information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_nicno: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_dob(string username, string dateOfBirth)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set DATE_OF_BIRTH = :dateOfBirth" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("dateOfBirth", dateOfBirth);
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());
                              
                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating DOB information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_dob: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_gender(string username, string gender)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set GENDER = :gender" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("gender", gender.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());                              

                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating gender information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_gender: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_occupation(string username, string ocupation)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set DESIGNATION = :designation" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("designation", ocupation.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());
                               
                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating ocupation information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_occupation: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_addrss1(string username, string addrss1)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set ADDRESS1 = :addrss1" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("addrss1", addrss1.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());                              

                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating address 1 information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_addrss1: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }


    public string saveProfile_Passport(string username, string ppno)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set PASSPORT_NO = :ppno" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("ppno", ppno.Trim().ToUpper());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());                             

                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating passport information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_Passport: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }
    

    public string saveProfile_addrss2(string username, string addrss2)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set ADDRESS2 = :addrss2" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("addrss2", addrss2.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());
                             

                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating address 2 information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_addrss2: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_addrss3(string username, string addrss3)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set ADDRESS3 = :addrss3" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("addrss3", addrss3.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());                             

                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating address 3 information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_addrss3: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_addrss4(string username, string addrss4)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set ADDRESS4 = :addrss4" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("addrss4", addrss4.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());                            

                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating address 4 information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_addrss4: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_cityTown(string username, string cityTown)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set TOWN_CITY = :cityTown" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("cityTown", cityTown.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());                               

                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating City information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_cityTown: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_postCode(string username, string postCode)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set POSTAL_CODE = :postCode" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("postCode", postCode.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());
               
                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating postal code information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_postCode: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_country(string username, string country)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set COUNTRY = :country" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("country", country.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());                             

                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating country information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_country: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_mobileNum(string username, string mobileNumber)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set MOBILE_NUMBER = :mobileNumber" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("mobileNumber", mobileNumber.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());
                               
                int count = com.ExecuteNonQuery();

                mesg = "success";
            }

            if (oconnLife.State != ConnectionState.Open)
            {
                oconnLife.Open();
            }
            string updateLifeUserInfo = "Update SLIC_NET_LIFE.USER_NOTIFY_INFO set MOBILE_NO = :mobileNum" +
                                        " where UPPER(USERNAME) = :username";

            using (OracleCommand cmd = new OracleCommand(updateLifeUserInfo, oconnLife))
            {
                cmd.Parameters.AddWithValue("mobileNum", mobileNumber.Trim());
                cmd.Parameters.AddWithValue("username", username.Trim().ToUpper());

                cmd.ExecuteNonQuery();

                mesg = "success";
            }

            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateGenUserInfo = "Update SLIC_NET.USER_NOTIFY_INFO set MOBILE_NO = :mobileNum" +
                                        " where UPPER(USERNAME) = :username";

            using (OracleCommand cmd = new OracleCommand(updateGenUserInfo, oconnGen))
            {
                cmd.Parameters.AddWithValue("mobileNum", mobileNumber.Trim());
                cmd.Parameters.AddWithValue("username", username.Trim().ToUpper());

                cmd.ExecuteNonQuery();
                                
                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating mobile no. information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_mobileNum: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
            oconnLife.Close();
           // oconnGen.Close();
        }

        return mesg;
    }

    public string saveProfile_homeNum(string username, string homeNumber)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set HOME_NUMBER = :homeNumber" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("homeNumber", homeNumber.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());
                              
                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating Home no. information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_homeNum: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }

    public string saveProfile_officeNum(string username, string officeNumber)
    {
        string mesg = "Profile update was not successful";
        try
        {
            if (oconnGen.State != ConnectionState.Open)
            {
                oconnGen.Open();
            }
            string updateProfile = "Update ulweb.webusers set OFFICE_NUMBER = :officeNumber" +
                                    " where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(updateProfile, oconnGen))
            {
                com.Parameters.AddWithValue("officeNumber", officeNumber.Trim());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());                             

                int count = com.ExecuteNonQuery();

                mesg = "success";
            }
        }
        catch (Exception e)
        {
            mesg = "Error occured while updating Office no. information";
            log logger = new log();
            logger.write_log("Failed at saveProfile_officeNum: " + e.ToString());
        }
        finally
        {
            if (oconnGen.State == ConnectionState.Open)
            {
                oconnGen.Close();
            }
        }

        return mesg;
    }


    public string getFullAddress()
    {
        string address = "";

        address = O_addrss1;
        if (O_addrss2 != "")
        {
            address = address + "<br/>&nbsp;&nbsp" + O_addrss2;
        }
        if (O_addrss3 != "")
        {
            address = address + "<br/>&nbsp;&nbsp" + O_addrss3;
        }
        if (O_addrss4 != "")
        {
            address = address + "<br/>&nbsp;&nbsp" + O_addrss4;
        }

        //address = O_addrss1;
        //if (O_addrss2 != "")
        //{
        //    address = address + "<br/>&nbsp" + O_addrss2;
        //}
        //if (O_addrss3 != "")
        //{
        //    address = address + "<br/>&nbsp" + O_addrss3;
        //}
        //if (O_addrss4 != "")
        //{
        //    address = address + "<br/>&nbsp" + O_addrss4;
        //}

        return address;
    }

    

    public string getFullAddressPDF()
    {
        string address = "";

        address = O_addrss1;
        if (O_addrss2 != "")
        {
            address = address + " " + O_addrss2;
        }
        if (O_addrss3 != "")
        {
            address = address + " " + O_addrss3;
        }
        if (O_addrss4 != "")
        {
            address = address + " " + O_addrss4;
        }

        return address;
    }


    public string getFullName()
    {
        string name = "";

        name = O_title;
        if ( !String.IsNullOrEmpty(O_firstName))
        {
            name = name + " " + O_firstName;
        }
        if (!String.IsNullOrEmpty(O_othNames))
        {
            name = name + " " + O_othNames;
        }
        if (!String.IsNullOrEmpty(O_lastName))
        {
            name = name + " " + O_lastName;
        }

        return name;
    }
    



    public List<string> getJobList(string letters)
    {
        List<string> Jobs = new List<string>();
        int status = -1;
        string message = "";
        InfoValidator validator = new InfoValidator();
        validator.validateOccupation(letters, out status, out message);

        if (status == 0)
        {
            using (OracleConnection conn = new OracleConnection())
            {
                conn.ConnectionString = ConfigurationManager.AppSettings["OracleDB"];
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "select distinct DESIGNATION from ulweb.webusers where UPPER(DESIGNATION) like '" + letters.Trim().ToUpper() + "%' ";

                    //cmd.Parameters.Add("@designation", OdbcType.VarChar);
                    //cmd.Parameters["@designation"].Value = letters.Trim().ToUpper();

                    //cmd.Parameters.Add("@id", OdbcType.VarChar).Value = letters.Trim().ToUpper();

                    conn.Open();
                    using (OracleDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Jobs.Add(string.Format("{0}", sdr[0]));
                        }
                    }
                    conn.Close();
                }
            }
        }
        return Jobs;
    }

}