using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.Odbc;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.OracleClient;

/// <summary>
/// Summary description for InfoValidator
/// </summary>
public class InfoValidator
{
    OdbcConnection db2conn = new OdbcConnection(ConfigurationManager.AppSettings["DB2"]);
    OracleConnection oraconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);

	public InfoValidator()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int IsRegisteredUsername(string username)
    {
        int returnValue = -1;

        try
        {
            if (oraconn.State != ConnectionState.Open)
            {
                oraconn.Open();
            }
           
            string getUser = "select count(*) from ULWEB.WEBUSERS where UPPER(USERNAME) = :username";

            using (OracleCommand com = new OracleCommand(getUser, oraconn))
            {
                com.Parameters.AddWithValue("username", username.ToUpper());
                               
                int count = Convert.ToInt32(com.ExecuteScalar());

                if (count == 0)
                {
                    returnValue = 0;
                }
                else if (count > 0)
                {
                    returnValue = 1;
                }
            }

        }
        catch (Exception e)
        {
            returnValue = -1;
            log logger = new log();
            logger.write_log("Failed at IsRegisteredUsername: " + e.ToString());
        }
        finally
        {
            if (oraconn.State == ConnectionState.Open)
            {
                oraconn.Close();
            }
        }

        return returnValue;
    }
    #region Validate Agency Code
    public void validateagtcd(string agencyCode, out int status, out string message)
    {
        status = -1;
        message = "";
        try
        {
            if (agencyCode.ToString().Length <= 6)
            {
                if (filter_IDs(agencyCode.ToString()))
                {
                    Match match = Regex.Match(agencyCode.ToString(), @"[0-9]", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        status = 0;
                    }
                    else
                    {
                        message = "Format is invalid";
                    }
                }
                else
                {
                    message = "Contains invalid characters";
                }
            }
            else
            {
                message = "Should not be more than 6 characters";
            }
        }
        catch
        {
            message = "Code is not a valid one";
        }

    }

    #endregion
    public int IsRegisteredUser(string username)
    {
        int returnValue = -1;

        try
        {
            if (oraconn.State != ConnectionState.Open)
            {
                oraconn.Open();
            }
            string getUser = "select count(*) from ULWEB.WEBUSERS where UPPER(USERNAME) = :username and ACTIVE_FLAG = 'Y' and USERTYPE = 0";

            using (OracleCommand com = new OracleCommand(getUser, oraconn))
            {
                com.Parameters.AddWithValue("username", username.ToUpper());                              

                int count = Convert.ToInt32(com.ExecuteScalar());

                if (count == 0)
                {
                    returnValue = 0;
                }
                else if (count > 0)
                {
                    returnValue = 1;
                }
            }

        }
        catch (Exception e)
        {
            returnValue = -1;
            log logger = new log();
            logger.write_log("Failed at IsRegisteredUser: " + e.ToString());
        }
        finally
        {
            if (oraconn.State == ConnectionState.Open)
            {
                oraconn.Close();
            }
        }

        return returnValue;
    }

    public bool IsRegisteredNIC(string nic)
    {
        bool returnValue = true;

        try
        {
            if (oraconn.State != ConnectionState.Open)
            {
                oraconn.Open();
            }

            string getNic = "select count(*) from ULWEB.WEBUSERS where upper(NIC_NO) = :nic";

            using (OracleCommand com = new OracleCommand(getNic, oraconn))
            {
                com.Parameters.AddWithValue("nic", nic.ToUpper());
                                
                int count = Convert.ToInt32(com.ExecuteScalar());

                if (count == 0)
                {
                    returnValue = false;
                }
            }

        }
        catch (Exception e)
        {
            // Log your error
            returnValue = true;
            log logger = new log();
            logger.write_log("Failed at IsRegisteredNIC: " + e.ToString());
        }
        finally
        {
            if (oraconn.State == ConnectionState.Open)
            {
                oraconn.Close();
            }
        }

        return returnValue;
    }

    public bool checkNICPPForAML(string nic, string PP, string mobileNo)
    {        
        string result = "";
        bool returnValue = true;

        try
        {
            if (oraconn.State != ConnectionState.Open)
            {
                oraconn.Open();
            }

            using (OracleCommand cmd = new OracleCommand("SLICCOMMON.PROC_AML_ONBOARD_LOG", oraconn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("sysName", "CUST_PORTAL");
                cmd.Parameters.AddWithValue("brCode", "Online-999 ");
                if (nic != "")
                {
                    cmd.Parameters.AddWithValue("nicNumber", nic);
                }
                else
                {
                    cmd.Parameters.AddWithValue("nicNumber", "");
                }
                if (PP != "")
                {
                    cmd.Parameters.AddWithValue("passportNumber", PP);
                }
                else
                {
                    cmd.Parameters.AddWithValue("passportNumber", "");
                }

                cmd.Parameters.AddWithValue("polNumber", "");
                if (mobileNo != "")
                {
                    cmd.Parameters.AddWithValue("phoneNumber", mobileNo);
                }
                else
                {
                    cmd.Parameters.AddWithValue("phoneNumber", "");
                }

                cmd.Parameters.Add("responseVal", OracleType.VarChar, 200).Direction = ParameterDirection.Output;
                OracleDataReader dr = cmd.ExecuteReader();
                result = cmd.Parameters["responseVal"].Value.ToString();
                dr.Close();
            }
        }
        catch (Exception e)
        {
            // Log your error
            returnValue = true;
            log logger = new log();
            logger.write_log("Failed at checkNICForAML: " + e.ToString());
        }
        finally
        {
            if (oraconn.State == ConnectionState.Open)
            {
                oraconn.Close();
            }
        }

        if (result == "AML Listed Person")
        {
            log logger = new log();
            logger.write_log("AML Listed Person");
            returnValue = true;
        }
        else
        {
            log logger = new log();
            logger.write_log("Not AML Listed Person");
            returnValue = false;
        }
        return returnValue;
    }

    public bool IsRegisteredNIC(string nic, string username)
    {
        bool returnValue = true;

        try
        {
            if (oraconn.State != ConnectionState.Open)
            {
                oraconn.Open();
            }
            string getNic = "select count(*) from ULWEB.WEBUSERS where upper(NIC_NO) = :nic and USERNAME <> :username";

            using (OracleCommand com = new OracleCommand(getNic, oraconn))
            {
                com.Parameters.AddWithValue("nic", nic.ToUpper());
                com.Parameters.AddWithValue("username", username);

                int count = Convert.ToInt32(com.ExecuteScalar());

                if (count == 0)
                {
                    returnValue = false;
                }
            }

        }
        catch (Exception e)
        {
            // Log your error
            returnValue = true;
            log logger = new log();
            logger.write_log("Failed at IsRegisteredNIC (2) : " + e.ToString());
        }
        finally
        {
            if (oraconn.State == ConnectionState.Open)
            {
                oraconn.Close();
            }
        }

        return returnValue;
    }

    /// <summary>
    /// 
    /// 
    /// 
    public bool IsRegisteredPPNo(string Pno)
    {
        bool returnValue = true;

        try
        {
            if (oraconn.State != ConnectionState.Open)
            {
                oraconn.Open();
            }
            string getNic = "select count(*) from ULWEB.WEBUSERS where UPPER(PASSPORT_NO) = :pno";

            using (OracleCommand com = new OracleCommand(getNic, oraconn))
            {
                com.Parameters.AddWithValue("pno", Pno.Trim().ToUpper());
                                
                int count = Convert.ToInt32(com.ExecuteScalar());

                if (count == 0)
                {
                    returnValue = false;
                }
            }

        }
        catch (Exception e)
        {
            // Log your error
            returnValue = true;
            log logger = new log();
            logger.write_log("Failed at IsRegisteredPPNo: " + e.ToString());
        }
        finally
        {
            if (oraconn.State == ConnectionState.Open)
            {
                oraconn.Close();
            }
        }

        return returnValue;
    }

    public bool IsRegisteredPPNo(string Pno, string username)
    {
        bool returnValue = true;

        try
        {
            if (oraconn.State != ConnectionState.Open)
            {
                oraconn.Open();
            }
            string getNic = "select count(*) from ULWEB.WEBUSERS where UPPER(PASSPORT_NO) = :pno and UPPER(USERNAME) <> :username";

            using (OracleCommand com = new OracleCommand(getNic, oraconn))
            {
                com.Parameters.AddWithValue("pno", Pno.Trim().ToUpper());
                com.Parameters.AddWithValue("username", username.Trim().ToUpper());                            

                int count = Convert.ToInt32(com.ExecuteScalar());

                if (count == 0)
                {
                    returnValue = false;
                }
            }

        }
        catch (Exception e)
        {
            // Log your error
            returnValue = true;
            log logger = new log();
            logger.write_log("Failed at IsRegisteredPPNo: " + e.ToString());
        }
        finally
        {
            if (oraconn.State == ConnectionState.Open)
            {
                oraconn.Close();
            }
        }

        return returnValue;
    }
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>


    public int IsRegisteredEmail(string email)
    {
        int returnValue = -1;

        try
        {
            if (oraconn.State != ConnectionState.Open)
            {
                oraconn.Open();
            }

            string getEmail = "select count(*) from ULWEB.WEBUSERS where lower(EMAIL) = :email";

            using (OracleCommand com = new OracleCommand(getEmail, oraconn))
            {
                com.Parameters.AddWithValue("email", email.ToLower());
                               
                int count = Convert.ToInt32(com.ExecuteScalar());

                if (count == 0)
                {
                    returnValue = 0;
                }
                else if (count > 0)
                {
                    returnValue = 1;
                }
            }

        }
        catch (Exception e)
        {
            // Log your error
            returnValue = -1;
            log logger = new log();
            logger.write_log("Failed at IsRegisteredEmail: " + e.ToString());
        }
        finally
        {
            if (oraconn.State == ConnectionState.Open)
            {
                oraconn.Close();
            }
        }

        return returnValue;
    }

    public int IsRegisteredEmail(string email, string username)
    {
        int returnValue = -1;

        try
        {
            if (oraconn.State != ConnectionState.Open)
            {
                oraconn.Open();
            }
            string getEmail = "select count(*) from ULWEB.WEBUSERS where lower(EMAIL) = :email and USERNAME <> :username";

            using (OracleCommand com = new OracleCommand(getEmail, oraconn))
            {
                com.Parameters.AddWithValue("email", email.ToLower());
                com.Parameters.AddWithValue("username", username);
              
                int count = Convert.ToInt32(com.ExecuteScalar());

                if (count == 0)
                {
                    returnValue = 0;
                }
                else if (count > 0)
                {
                    returnValue = 1;
                }
            }

        }
        catch (Exception e)
        {
            // Log your error
            returnValue = -1;
            log logger = new log();
            logger.write_log("Failed at IsRegisteredEmail (2): " + e.ToString());
        }
        finally
        {
            if (oraconn.State == ConnectionState.Open)
            {
                oraconn.Close();
            }
        }

        return returnValue;
    }

    public int IsRegisteredEmailForWeb(string email)
    {
        int returnValue = -1;

        try
        {
            if (oraconn.State != ConnectionState.Open)
            {
                oraconn.Open();
            }
            string getEmail = "select count(*) from ULWEB.WEBUSERS where lower(EMAIL) = :email and  ACTIVE_FLAG = 'Y' and USERTYPE = 0";

            using (OracleCommand com = new OracleCommand(getEmail, oraconn))
            {
                com.Parameters.AddWithValue("email", email.ToLower());
               
                int count = Convert.ToInt32(com.ExecuteScalar());

                if (count == 0)
                {
                    returnValue = 0;
                }
                else if (count > 0)
                {
                    returnValue = 1;
                }
            }

        }
        catch (Exception e)
        {
            // Log your error
            returnValue = -1;
            log logger = new log();
            logger.write_log("Failed at IsRegisteredEmailForWeb: " + e.ToString());
        }
        finally
        {
            if (oraconn.State == ConnectionState.Open)
            {
                oraconn.Close();
            }
        }

        return returnValue;
    }

    public bool checkNICWithDob(string dob, string gender, string nic) // dob should be in yyyy/mm/dd format
    {
        try
        {
            if (nic.Length == 12)
            {
                nic = reverseNicFormat(nic);
            }
            
            if (nic.Length == 10)
            {
                if (nic[9].ToString().ToUpper() == "V" || nic[9].ToString().ToUpper() == "X")
                {
                    string[] arr = dob.Split('/');
                    int year = Convert.ToInt32(arr[0].Trim());
                    int month = Convert.ToInt32(arr[1].Trim());
                    int day = Convert.ToInt32(arr[2].Trim());
                    int leepYear = year % 4;
                    int day_count = get_day_count(day, month, Convert.ToDateTime(dob));
                    string our_ID = "";

                    if (gender.ToUpper().Equals("F"))
                    {
                        our_ID = year.ToString().Trim().Substring(2, 2) + (day_count + 500).ToString();
                    }
                    else
                    {
                        if (day_count < 10)
                        {
                            our_ID = year.ToString().Trim().Substring(2, 2) + "00" + day_count;
                        }
                        else if (day_count < 100)
                        {
                            our_ID = year.ToString().Trim().Substring(2, 2) + "0" + day_count;
                        }
                        else
                        {
                            our_ID = year.ToString().Trim().Substring(2, 2) + "" + day_count;
                        }

                    }

                    if (nic.Trim().Substring(0, 5).Equals(our_ID.Trim()))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch(Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at checkNICWithDob: " + e.ToString());
            return false;
        }
    }

    public string reverseNicFormat(string nic)
    {
        string nic_pt1 = "", nic_pt2 = "";
        string reversed_nic = "";
        
        if (nic.Trim().Length == 10)
        {
            nic_pt1 = nic.Trim().Substring(0, 5);
            nic_pt2 = nic.Trim().Substring(5, 4);
            reversed_nic = "19" + nic_pt1 + "0" + nic_pt2;

        }
        else if (nic.Trim().Length == 12)
        {
            nic_pt1 = nic.Trim().Substring(2, 5);
            nic_pt2 = nic.Trim().Substring(8, 4);
            reversed_nic = nic_pt1 + nic_pt2 + "V";
            //nic_error.Text = old_nic;
        }
        
        return reversed_nic;
    }

    private int get_day_count(int day, int month, DateTime dt)
    {
        int count = 0;

        count = (dt - Convert.ToDateTime(dt.Year.ToString() + "/01/01")).Days;

        if (dt.Year % 4 == 0)
        {

        }
        else
        {
            if (count >= 59)
            {
                count = count + 1;
            }

        }

        count = count + 1;

        return count;
    }

    public bool checkBeginTrailSpace(string text)
    {
        bool retValue = false;
        int length = text.Length;

        string trimText = text.Trim();

        int trimLength = trimText.Length;

        if (length == trimLength)
        {
            retValue = true;
        }

        return retValue;
    }

    public bool checkMiddleSpace(string text)
    {
        bool retValue = false;
        if (!(text.Contains(" ")))
        {
            retValue = true;
        }

        return retValue;
    }

    public bool filter_IDs(string input)
    {
        bool result = true;
        //char[] badStuff = new char[] { '\'', '=', ';', '-' };  
        char[] badStuff = new char[] { '\'', '=', ';' };
        int i = 0;
        i = input.IndexOfAny(badStuff);

        if (input.Contains("--"))
        {
            result = false;
        }
        else
        {
            if (i != -1)
            {
                result = false;
            }
        }
        return result;
    }

    public bool filter_IDs_without_singq(string input)
    {
        bool result = true;
        char[] badStuff = new char[] { '=', ';' };
        int i = 0;
        i = input.IndexOfAny(badStuff);

        if (input.Contains("--"))
        {
            result = false;
        }
        else
        {
            if (i != -1)
            {
                result = false;
            }
        }
        return result;
    }

    public void validateAllFields(string userName, string password, string title, string firstName, string lastname, string otherNames,
                                 string nic, string dateOfBirth, string gender, string addrss1, string addrss2, string addrss3, string addrss4,
                                 string city, string postalCode, string country, string email, string mobileNo, string homeNo, string ofcNo,
                                 out int retStatus, out string retDescription)
    {

        retStatus = 0;
        retDescription = "";


    }

    public void validateUsername(string username, out int status, out string message)
    {
        status = -1;
        message = "";
        
        if (checkBeginTrailSpace(username) && checkMiddleSpace(username))
        {
            if (filter_IDs(username))
            {
                if (username.Length <= 15)
                {
                    if (IsRegisteredUsername(username) == 0)
                    {
                        status = 0;
                    }
                    else
                    {
                        message = "Username already exists";
                    }                    
                }
                else
                {
                    message = "Username should not be greater than 15 characters";
                }
            }
            else
            {
                message = "Username contains invalid characters";
            }
        }
        else
        {
            message = "Username cannot contain space characters";
        }
    }

    public void validatePassword(string password, out int status, out string message)
    {
        status = -1;
        message = "";

        if (filter_IDs(password))
        {
            if (password.Length <= 20)
            {
                Match match = Regex.Match(password, @"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,20})$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    status = 0;
                }
                else
                {
                    message = "Password should conform to password rules";
                }
            }
            else
            {
                message = "Password should not be more than 20 characters";
            }
        }
        else
        {
            message = "Password contains invalid characters";
        }

    }

    public void validatePasswordMatch(string password1, string password2, out int status, out string message)
    {
        status = -1;
        message = "";

        if (password1 == password2)
        {
            status = 0;
        }
        else
        {
            message = "Password does not match with confirm password";
        }
    }

    public void validateTitle(string title, out int status, out string message)
    {
        status = -1;
        message = "";

        if (title.Trim() == "Select")
        {
            message = "Title should be selected";
        }
        else
        {
            if (filter_IDs(title))
            {
                status = 0;                
            }
            else
            {
                message = "Title contains invalid characters";
            }
        }
    }

    public void validateFirstname(string firstname, out int status, out string message)
    {
        status = -1;
        message = "";

        if (firstname.Length <= 25)
        {
            if (filter_IDs_without_singq(firstname))
            {
                status = 0;
            }
            else
            {
                message = "Firstname contains invalid characters";
            }
        }
        else
        {
            message = "Firstname should not be more than 25 characters long";
        }       
    }

    public void validateLastname(string lastname, out int status, out string message)
    {
        status = -1;
        message = "";

        if (lastname.Length <= 50)
        {
            if (filter_IDs_without_singq(lastname))
            {
                status = 0;
            }
            else
            {
                message = "Lastname contains invalid characters";
            }
        }
        else
        {
            message = "Lastname should not be more than 50 characters long";
        }
    }

    public void validateOthernames(string othernames, out int status, out string message)
    {
        status = -1;
        message = "";

        if (othernames.Length <= 100)
        {
            if (filter_IDs_without_singq(othernames))
            {
                status = 0;
            }
            else
            {
                message = "Other names contain invalid characters";
            }
        }
        else
        {
            message = "Other names should not be more than 100 characters long";
        }
    }

    public void validateNIC(string nic, out int status, out string message)
    {
        status = -1;
        message = "";

        Match match = Regex.Match(nic, @"^[0-9]{9}[V,X,v,x]$|[1-2]{1}[0-9]{11}$", RegexOptions.IgnoreCase);
        if (match.Success)
        {
            if (IsRegisteredNIC(nic))
            {
                message = "This NIC is already registered";
            }
            else
            {
                status = 0;
            }
        }
        else
        {
            message = "NIC is invalid";
        }
    }

    public void validateNICUpdate(string nic, string username, out int status, out string message)
    {
        status = -1;
        message = "";

        Match match = Regex.Match(nic, @"^[0-9]{9}[V,X,v,x]$|[1-2][0-9]{11}$", RegexOptions.IgnoreCase);
        if (match.Success)
        {
            if (IsRegisteredNIC(nic, username))
            {
                message = "This NIC is already registered";
            }
            else
            {
                status = 0;
            }
        }
        else
        {
            message = "NIC is invalid";
        }
    }


    public void validatePPnoUpdate(string PPno, string citizen, string username, out int status, out string message)
    {
        status = -1;
        message = "";

        if (citizen.ToUpper().Equals("Y"))
        {
            if (String.IsNullOrEmpty(PPno))
            {
                status = 0;
            }
            else
            {
                if (IsRegisteredPPNo(PPno, username))
                {
                    message = "This Passport number is already registered.";
                }
                else
                {
                    status = 0;
                }
            }
        }
        else if (citizen.ToUpper().Equals("N"))
        {
            if (String.IsNullOrEmpty(PPno))
            {
                message = "Passport number is required.";
            }
            else
            {
                if (IsRegisteredPPNo(PPno, username))
                {
                    message = "This Passport number is already registered.";
                }
                else
                {
                    status = 0;
                }
            }
        }


    }

    public void validatePPnoEntry(string PPno, string citizen, string username, out int status, out string message)
    {
        status = -1;
        message = "";

        if (citizen.ToUpper().Equals("Y"))
        {
            if (String.IsNullOrEmpty(PPno))
            {
                status = 0;
            }
            else
            {
                if (IsRegisteredPPNo(PPno))
                {
                    message = "This Passport number is already registered.";
                }
                else
                {
                    status = 0;
                }
            }
        }
        else if (citizen.ToUpper().Equals("N"))
        {
            if (String.IsNullOrEmpty(PPno))
            {
                message = "Passport number is required.";
            }
            else
            {
                if (IsRegisteredPPNo(PPno))
                {
                    message = "This Passport number is already registered.";
                }
                else
                {
                    status = 0;
                }
            }
        }


    }


    public void validateDateofBirth(string dateofbirth, out int status, out string message)
    {
        status = -1;
        message = "";

        try
        {
            DateTime dob = DateTime.ParseExact(dateofbirth, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (dob < DateTime.Now)
            {
                status = 0;
            }
            else
            {
                message = "Date of birth should be less than today";
            }
        }
        catch
        {
            message = "Date of birth is not a valid date";
        }

    }

    public void validateGender(string gender, string dateofbirth, string nic, string title, string srilankan, out int status, out string message)
    {
        status = -1;
        message = "";

        if (gender != "")
        {
            if (validateGenderWithTitle(gender, title))
            {
                if (srilankan == "Y")
                {

                    if (gender == "M" || gender == "F")
                    {
                        if (checkNICWithDob(dateofbirth, gender, nic))
                        {
                            status = 0;
                        }
                        else
                        {
                            message = "NIC, Date of birth and gender does not match";
                        }
                    }
                    else
                    {
                        message = "Gender is invalid";
                    }
                }
                else
                {
                    status = 0;
                }
            }
            else
            {
                message = "Gender and Title does not match.";
            }
        }
        else
        {
            message = "Gender should be selected";
        }

    }

    public bool validateGenderWithTitle(string gender, string title)
    {
        bool retVal = true;
        string[] invalidFemaleTitles = {"Mr.", "Master"};
        string[] invalidMaleTitles = {"Mrs.", "Miss.", "Dr.(Miss.)", "Dr.(Mrs.)"};
        
        if (gender == "F")
        {
            foreach (string _str in invalidFemaleTitles)
            {
                if (title == _str)
                {
                    retVal = false;
                }
            }
        }

        if (gender == "M")
        {
            foreach (string _str in invalidMaleTitles)
            {
                if (title == _str)
                {
                    retVal = false;
                }
            }
        }

        return retVal;
    }

    public void validateAddressLine1(string adrs1, out int status, out string message)
    {
        status = -1;
        message = "";

        if (adrs1.Length <= 30)
        {
            if (filter_IDs_without_singq(adrs1))
            {
                status = 0;
            }
            else
            {
                message = "Address Line 1 contains invalid characters";
            }
        }
        else
        {
            message = "Address Line 1 should not be more than 30 characters";
        }

    }

    public void validateAddressLine2(string adrs2, out int status, out string message)
    {
        status = -1;
        message = "";

        if (adrs2.Length <= 30)
        {
            if (filter_IDs_without_singq(adrs2))
            {
                status = 0;
            }
            else
            {
                message = "Address Line 2 contains invalid characters";
            }
        }
        else
        {
            message = "Address Line 2 should not be more than 30 characters";
        }

    }

    public void validateAddressLine3(string adrs3, out int status, out string message)
    {
        status = -1;
        message = "";

        if (adrs3.Length <= 30)
        {
            if (filter_IDs_without_singq(adrs3))
            {
                status = 0;
            }
            else
            {
                message = "Address Line 3 contains invalid characters";
            }
        }
        else
        {
            message = "Address Line 3 should not be more than 30 characters";
        }

    }

    public void validateAddressLine4(string adrs4, out int status, out string message)
    {
        status = -1;
        message = "";

        if (adrs4.Length <= 30)
        {
            if (filter_IDs_without_singq(adrs4))
            {
                status = 0;
            }
            else
            {
                message = "Address Line 4 contains invalid characters";
            }
        }
        else
        {
            message = "Address Line 4 should not be more than 30 characters";
        }
    }

    public void validateOccupation(string ocupation, out int status, out string message)
    {
        status = -1;
        message = "";

        if (ocupation.Length <= 30)
        {
            if (filter_IDs_without_singq(ocupation))
            {
                status = 0;
            }
            else
            {
                message = "Occupation contains invalid characters";
            }
        }
        else
        {
            message = "Occupation should not be more than 30 characters";
        }
    }

    public void validateCityTown(string city, out int status, out string message)
    {
        status = -1;
        message = "";

        if (city.Length <= 30)
        {
            if (filter_IDs_without_singq(city))
            {
                status = 0;
            }
            else
            {
                message = "City or Town contains invalid characters";
            }
        }
        else
        {
            message = "City or Town should not be more than 30 characters";
        }
    }

    public void validatePostaclCode(string postalCode, out int status, out string message)
    {
        status = -1;
        message = "";

        if (postalCode.Length <= 10 && postalCode != "")
        {
            if (filter_IDs(postalCode))
            {
                status = 0;
            }
            else
            {
                message = "Postal code contains invalid characters";
            }
        }
        else
        {
            message = "Postal code should not be more than 10 characters long";
        }
    }

    public void validateCountry(string country, out int status, out string message)
    {
        status = -1;
        message = "";

        if (country.Length <= 2)
        {
            if (filter_IDs(country))
            {
                status = 0;
            }
            else
            {
                message = "Country contains invalid characters";
            }
        }
        else
        {
            message = "Country code is invalid";
        }
    }

    public void validateEmail(string email, out int status, out string message)
    {
        status = -1;
        message = "";

        if (checkBeginTrailSpace(email) && checkMiddleSpace(email))
        {
            if (email.Length <= 100)
            {
                //if (filter_IDs(email))
               // {
                    Match match = Regex.Match(email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        if (IsRegisteredEmail(email) == 0)
                        {
                            status = 0;
                        }
                        else
                        {
                            message = "This email is already registered";
                        }
                    }
                    else
                    {
                        message = "Email is not valid";
                    }
               // }
              //  else
               // {
                //    message = "Email contains invalid characters";
               // }
            }
            else
            {
                message = "Email should not be more than 100 characters long";
            }
        }
        else
        {
            message = "Email should not contain space characters";
        }

    }

    public void validateEmailUpdate(string email, string username, out int status, out string message)
    {
        status = -1;
        message = "";

        if (checkBeginTrailSpace(email) && checkMiddleSpace(email))
        {
            if (email.Length <= 100)
            {
                //if (filter_IDs(email))
                // {
                Match match = Regex.Match(email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    if (IsRegisteredEmail(email, username) == 0)
                    {
                        status = 0;
                    }
                    else
                    {
                        message = "This email is already registered";
                    }
                }
                else
                {
                    message = "Email is not valid";
                }
                // }
                //  else
                // {
                //    message = "Email contains invalid characters";
                // }
            }
            else
            {
                message = "Email should not be more than 100 characters long";
            }
        }
        else
        {
            message = "Email should not contain space characters";
        }

    }

    public void validateMobileNumber(string mobileNo, string country, out int status, out string message)
    {
        status = -1;
        message = "";

        if (checkBeginTrailSpace(mobileNo) && checkMiddleSpace(mobileNo))
        {
            if (country == "LK")
            {
                if (mobileNo.Length <= 10)
                {
                    if (filter_IDs(mobileNo))
                    {
                        Match match = Regex.Match(mobileNo, @"07[0-9]{8}", RegexOptions.IgnoreCase);
                        if (match.Success)
                        {                                 
                             status = 0;                            
                        }                        
                        else
                        {
                            message = "Mobile number is invalid";
                        }
                    }
                    else
                    {
                        message = "Mobile number contains invalid characters";
                    }
                }
                else
                {
                    message = "Mobile number should not be more than 10 characters";
                }
            }
            else
            {
                if (filter_IDs(mobileNo))
                {                    
                    status = 0;                   
                }
                else
                {
                    message = "Mobile number contains invalid characters";
                }
            }
        }
        else
        {
            message = "Mobile number should not contain space characters";
        }
    }

    public void validateHomeNumber(string homeNo, out int status, out string message)
    {
        status = -1;
        message = "";

        if (checkBeginTrailSpace(homeNo) && checkMiddleSpace(homeNo))
        {
            if (homeNo.Length <= 15)
            {
                if (filter_IDs(homeNo))
                {
                    Match match = Regex.Match(homeNo, @"063[0-9]{7}|022[0-9]{7}|036[0-9]{7}|055[0-9]{7}|057[0-9]{7}|065[0-9]{7}|032[0-9]{7}|011[0-9]{7}|091[0-9]{7}|033[0-9]{7}|047[0-9]{7}|051[0-9]{7}|021[0-9]{7}|067[0-9]{7}|034[0-9]{7}|081[0-9]{7}|035[0-9]{7}|037[0-9]{7}|023[0-9]{7}|066[0-9]{7}|041[0-9]{7}|054[0-9]{7}|031[0-9]{7}|052[0-9]{7}|038[0-9]{7}|027[0-9]{7}|045[0-9]{7}|026[0-9]{7}|025[0-9]{7}|024[0-9]{7}|07[1|2|5|6|7|8][0-9]{7}", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        status = 0;
                    }
                    else
                    {
                        message = "Home number is invalid";
                    }
                }
                else
                {
                    message = "Home number contains invalid characters";
                }
            }
            else
            {
                message = "Home number should not be more than 15 characters";
            }
        }
        else
        {
            message = "Home number should not contain space characters";
        }
    }

    public void validateOfficeNumber(string officeNo, out int status, out string message)
    {
        status = -1;
        message = "";

        if (checkBeginTrailSpace(officeNo) && checkMiddleSpace(officeNo))
        {
            if (officeNo.Length <= 20)
            {
                if (filter_IDs(officeNo))
                {
                    Match match = Regex.Match(officeNo, @"063[0-9]{7}|022[0-9]{7}|036[0-9]{7}|055[0-9]{7}|057[0-9]{7}|065[0-9]{7}|032[0-9]{7}|011[0-9]{7}|091[0-9]{7}|033[0-9]{7}|047[0-9]{7}|051[0-9]{7}|021[0-9]{7}|067[0-9]{7}|034[0-9]{7}|081[0-9]{7}|035[0-9]{7}|037[0-9]{7}|023[0-9]{7}|066[0-9]{7}|041[0-9]{7}|054[0-9]{7}|031[0-9]{7}|052[0-9]{7}|038[0-9]{7}|027[0-9]{7}|045[0-9]{7}|026[0-9]{7}|025[0-9]{7}|024[0-9]{7}|07[1|2|5|6|7|8][0-9]{7}", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        status = 0;
                    }
                    else
                    {
                        message = "Office number is invalid";
                    }
                }
                else
                {
                    message = "Office number contains invalid characters";
                }
            }
            else
            {
                message = "Office number should not be more than 20 characters";
            }
        }
        else
        {
            message = "Office number should not contain space characters";
        }
    }

    public void validatePolNumber(string polNo, out int status, out string message)
    {
        status = -1;
        message = "";

        if (checkBeginTrailSpace(polNo) && checkMiddleSpace(polNo))
        {
            if (polNo.Length <= 18)
            {
                if (filter_IDs(polNo))
                {                    
                    status = 0;                    
                }
                else
                {
                    message = "Policy number contains invalid characters";
                }
            }
            else
            {
                message = "Policy number should not be more than 18 characters";
            }
        }
        else
        {
            message = "Policy number should not contain space characters";
        }
    }

    public void validateLifePolNumber(string polNo, out int status, out string message)
    {
        status = -1;
        message = "";

        if (checkBeginTrailSpace(polNo) && checkMiddleSpace(polNo))
        {
            if (polNo.Length <= 8)
            {
                if (filter_IDs(polNo))
                {
                    status = 0;
                }
                else
                {
                    message = "Policy number contains invalid characters";
                }
            }
            else
            {
                message = "Policy number should not be more than 8 characters";
            }
        }
        else
        {
            message = "Policy number should not contain space characters";
        }
    }

    public void validateLifeLoanNumber(string loanNo, out int status, out string message)
    {
        status = -1;
        message = "";

        if (checkBeginTrailSpace(loanNo) && checkMiddleSpace(loanNo))
        {
            if (loanNo.Length <= 11)
            {
                if (filter_IDs(loanNo))
                {
                    status = 0;
                }
                else
                {
                    message = "Loan number contains invalid characters";
                }
            }
            else
            {
                message = "Loan number should not be more than 11 characters";
            }
        }
        else
        {
            message = "Loan number should not contain space characters";
        }
    }

    public void checkAmountField(string amount, out int status, out string message)
    {
        status = -1;
        message = "";

        try
        {
            double validAmt = double.Parse(amount);
            status = 0;
        }
        catch
        {
            message = "Amount entered is not valid.";
        }

    }    

    //-----------------------------------Validations for AMP Quotation-------------------------------------------
    
    public void validateCategory(string category, out int status, out string message)
    {
        status = -1;
        message = "";

        if (category.Length == 1)
        {
            if (filter_IDs(category))
            {
                status = 0;
            }
            else
            {
                message = "Category contains invalid characters";
            }
        }
        else
        {
            message = "Category code is invalid";
        }
    }

    public void validateGenderBasic(string uname, string category, string gender, out int status, out string message)
    {
        status = -1;
        message = "";

        if (gender != "")
        {
            if (gender == "M" || gender == "F")
            {
                if (category == "M")
                {
                    if (IsRegisteredGender(uname, gender))
                    {
                        status = 0;
                    }
                    else
                    {                        
                        message = "Gender is not matching with that of registered user.";
                    }
                }
                else if (category == "S")
                {
                    if (!IsRegisteredGender(uname, gender))
                    {
                        status = 0;
                    }
                    else
                    {
                        message = "Gender is same as main life.";
                    }
                }
                else
                {
                    status = 0;
                }                
            }
            else
            {
                message = "Gender is invalid";
            }
        }
        else
        {
            message = "Gender should be selected";
        }
    }

    public void validateDobForAMP(string uname, string category, string dateofbirth, out int status, out string message)
    {
        status = -1;
        message = "";
        double age = 0.00;
        try
        {
            DateTime dob = DateTime.ParseExact(dateofbirth, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (dob < DateTime.Now)
            {
                string dob_ = dob.ToString("yyyyMMdd");

                if (category == "M")
                {
                    if (IsRegisteredDob(uname, dob_))
                    {
                        message = IsAgeWithinRange(category, dob, out age);
                        if (message == "success")
                        {
                            status = 0;
                        }
                    }
                    else
                    {
                        message = "Date of Birth is not matching with that of registered user.";
                    }
                }                
                else
                {
                    message = IsAgeWithinRange(category, dob, out age);
                    if (message == "success")
                    {
                        status = 0;
                    }
                }           
            }
            else
            {
                message = "Date of birth should be less than today";
            }
        }
        catch
        {
            message = "Date of birth is not a valid date";
        }

    }

    public void validateHeight(string height, out int status, out string message)
    {
        status = -1;
        message = "";

        try
        {
            double height_ = double.Parse(height);
            if (height_ > 0)
            {
                status = 0;
            }
        }
        catch
        {
            message = "Height is not valid";
        }
    }

    public void validateWeight(string weight, out int status, out string message)
    {
        status = -1;
        message = "";

        try
        {
            double weight_ = double.Parse(weight);
            if (weight_ > 0)
            {
                status = 0;
            }
        }
        catch
        {
            message = "Weight is not valid";
        }
    }

    public bool IsRegisteredGender(string uname, string gender)
    {
        bool returnValue = true;

        try
        {
            if (oraconn.State != ConnectionState.Open)
            {
                oraconn.Open();
            }
            string getGender = "select count(*) from ULWEB.WEBUSERS where UPPER(USERNAME) = :uname and GENDER = :gender";

            using (OracleCommand com = new OracleCommand(getGender, oraconn))
            {
                com.Parameters.AddWithValue("uname", uname);
                com.Parameters.AddWithValue("gender", gender);
                                
                int count = Convert.ToInt32(com.ExecuteScalar());

                if (count == 0)
                {
                    returnValue = false;
                }
            }

        }
        catch (Exception e)
        {
            // Log your error
            returnValue = true;
            log logger = new log();
            logger.write_log("Failed at IsRegisteredGender: " + e.ToString());
        }
        finally
        {
            if (oraconn.State == ConnectionState.Open)
            {
                oraconn.Close();
            }
        }

        return returnValue;
    }

    public bool IsRegisteredDob(string uname, string dob)
    {
        bool returnValue = true;

        try
        {
            if (oraconn.State != ConnectionState.Open)
            {
                oraconn.Open();
            }
            string getGender = "select count(*) from ULWEB.WEBUSERS where UPPER(USERNAME) = :uname and DATE_OF_BIRTH = :dob";

            using (OracleCommand com = new OracleCommand(getGender, oraconn))
            {
                com.Parameters.AddWithValue("uname", uname);
                com.Parameters.AddWithValue("dob", dob);
                
                int count = Convert.ToInt32(com.ExecuteScalar());

                if (count == 0)
                {
                    returnValue = false;
                }
            }

        }
        catch (Exception e)
        {
            // Log your error
            returnValue = true;
            log logger = new log();
            logger.write_log("Failed at IsRegisteredDob: " + e.ToString());
        }
        finally
        {
            if (oraconn.State == ConnectionState.Open)
            {
                oraconn.Close();
            }
        }

        return returnValue;
    }

    public string IsAgeWithinRange(string category, DateTime dob, out double age)
    {
        string mesg = "success";
        double ageMaxMain = 0.00;
        double ageMinMain = 0.00;
        double ageMaxChild = 0.00;
        double ageMinChild = 0.00;
        string catDesc = "";
        age = 0;

        try
        {
            oraconn.Open();

            double ageInYrs = 0;
            ageInYrs = Math.Round((DateTime.Now - dob).Days / 365.25, 3);
            if (ageInYrs > 1)
            {
                ageInYrs = Math.Floor(ageInYrs);
            }
            else
            {
                ageInYrs = Math.Round(ageInYrs, 2);
            }

            age = ageInYrs;

            string getAgeLimits = "Select NEW_BUS_MAX_MAIN, NEW_BUS_MIN_MAIN, NEW_BUS_MAX_CHILD, NEW_BUS_MIN_CHILD" +
                                  " from SLIC_NET.AMP_AGE_LIMITS_WEB" +
                                  " where sysdate between EFFECT_FROM and EFFECT_TO";

            using (OracleCommand cmd = new OracleCommand(getAgeLimits, oraconn))
            {
                OracleDataReader ageLimReader = cmd.ExecuteReader();
                while (ageLimReader.Read())
                {
                    if (!ageLimReader.IsDBNull(0))
                    {
                        ageMaxMain = ageLimReader.GetDouble(0);
                    }
                    if (!ageLimReader.IsDBNull(1))
                    {
                        ageMinMain = ageLimReader.GetDouble(1);
                    }
                    if (!ageLimReader.IsDBNull(2))
                    {
                        ageMaxChild = ageLimReader.GetDouble(2);
                    }
                    if (!ageLimReader.IsDBNull(3))
                    {
                        ageMinChild = ageLimReader.GetDouble(3);
                    }
                }
                ageLimReader.Close();

                if (category == "M" || category == "S")
                {
                    if (ageInYrs < ageMinMain || ageInYrs > ageMaxMain)
                    {
                        if (category == "M")
                        {
                            catDesc = "Main Life";
                        }
                        else if (category == "S")
                        {
                            catDesc = "Spouse";
                        }
                        mesg = catDesc + " age should be between " + ageMinMain + " years and " + ageMaxMain + " years.";

                    }
                }
                else if (category == "C")
                {
                    if (ageInYrs < ageMinChild || ageInYrs > ageMaxChild)
                    {
                        mesg = "Child age should be between " + ageMinChild*12 + " months and " + ageMaxChild + " years.";
                    }
                }
                else
                {
                    mesg = "Internal Error.";
                }
            }

        }
        catch (Exception e)
        {
            // Log your error
            mesg = "Error while calculating age.";
            log logger = new log();
            logger.write_log("Failed at IsAgeWithinRange: " + e.ToString());
        }
        finally
        {
            if (oraconn != null) oraconn.Close();
        }

        return mesg;
    }

    public string IsBmiWithinRange(double height, double weight, out double bmiVal, out double bmiRate)
    {
        string mesg = "success";
        bmiVal = 0.00;
        bmiRate = 0.00;

        try
        {
            oraconn.Open();

            height = height / 100;
            bmiVal = Math.Round(weight / (height * height), 2);

            string getBmiRate = "Select RATE from SLIGEN.AMP_BMI_VALUES" +
                                " where (MIN_VALUE <= :bmiVal" +
                                " and MAX_VALUE > :bmiVal" +
                                ") and sysdate between EFFECT_FROM and EFFECT_TO";

            using (OracleCommand cmd = new OracleCommand(getBmiRate, oraconn))
            {
                cmd.Parameters.Add("bmiVal", OdbcType.Double);
                cmd.Parameters["bmiVal"].Value = bmiVal;

                OracleDataReader BmiRateReader = cmd.ExecuteReader();
                if (BmiRateReader.HasRows)
                {
                    while (BmiRateReader.Read())
                    {
                        if (!BmiRateReader.IsDBNull(0))
                        {
                            bmiRate = BmiRateReader.GetDouble(0);
                        }
                    }
                }
                else
                {
                    mesg = "BMI values not defined";
                }
                BmiRateReader.Close();               
            }

            if (bmiRate == 9)
            {
                mesg = "Quotation cannot be issued for this member";
            }
            else if (bmiRate == 8)
            {
                mesg = "Quotation cannot be issued for this member";                
            }
            
        }
        catch (Exception e)
        {
            // Log your error
            mesg = "Error while calculating BMI.";
            log logger = new log();
            logger.write_log("Failed at IsBmiWithinRange: " + e.ToString());
        }
        finally
        {
            if (oraconn != null) oraconn.Close();
        }

        return mesg;
    }

    public string IsChildBmiWithinRange(double age, double height, double weight)
    {
        string mesg = "success";        

        try
        {
            oraconn.Open();            

            string getValidRanges = "Select * from SLIC_NET.AMP_CHILD_RANGES" +
                                    " where AGE_MIN <= :age" +
                                    " and AGE_MAX > :age" +
                                    " and HEIGHT_MIN < :height" +
                                    " and HEIGHT_MAX > :height" +
                                    " and WEIGHT_MIN <= :weight" +
                                    " and WEIGHT_MAX >= :weight";

            using (OracleCommand cmd = new OracleCommand(getValidRanges, oraconn))
            {
                cmd.Parameters.Add("age", OdbcType.Double);
                cmd.Parameters["age"].Value = age;

                cmd.Parameters.Add("height", OdbcType.Double);
                cmd.Parameters["height"].Value = height;

                cmd.Parameters.Add("weight", OdbcType.Double);
                cmd.Parameters["weight"].Value = weight;

                OracleDataReader validRangeReader = cmd.ExecuteReader();
                if (!validRangeReader.HasRows)
                {
                    mesg = "Quotation cannot be issued for this member";
                }
                validRangeReader.Close();
            }            

        }
        catch (Exception e)
        {
            // Log your error
            mesg = "Error while calculating Child BMI.";
            log logger = new log();
            logger.write_log("Failed at IsChildBmiWithinRange: " + e.ToString());
        }
        finally
        {
            if (oraconn != null) oraconn.Close();
        }

        return mesg;
    }

    public string getAge(string category, string dob, out double age)
    {
        string mesg = "success";
        age = 0.00;
        try
        {
            DateTime dtob = DateTime.ParseExact(dob, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            mesg = IsAgeWithinRange(category, dtob, out age);

        }
        catch (Exception e)
        {
            mesg = "Error occured while getting age";
            log logger = new log();
            logger.write_log("Failed at getAge: " + e.ToString());
        }
        return mesg;
    }

    public void validateNatureOccupation(string natureOcc, out int status, out string message)
    {
        status = -1;
        message = "";

        if (natureOcc != "")
        {
            if (natureOcc.Length <= 100)
            {
                if (filter_IDs(natureOcc))
                {
                    status = 0;
                }
                else
                {
                    message = "Nature of Occupation contains invalid characters";
                }
            }
            else
            {
                message = "Maximum length for Nature of Occupation exceeded";
            }
        }       
    }

    public void validateStartDate(string startDate, out int status, out string message)
    {
        status = -1;
        message = "";

        try
        {
            DateTime startDt = DateTime.ParseExact(startDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (startDt >= DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture) 
                && startDt <= DateTime.ParseExact(DateTime.Today.AddDays(15).ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture))
            {
                status = 0;
            }
            else if (startDt < DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture))
            {
                message = "Start Date cannot be less than today.";
            }
            else if (startDt > DateTime.ParseExact(DateTime.Today.AddDays(15).ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture))
            {
                message = "Start Date should be within 15 days from today.";
            }
            else
            {
                message = "Invalid Start Date found.";
            }
        }
        catch
        {
            message = "Start Date is not a valid date";
        }
    }

    public void validateEmplName(string emplName, out int status, out string message)
    {
        status = -1;
        message = "";

        if (emplName != "")
        {
            if (emplName.Length <= 100)
            {
                if (filter_IDs(emplName))
                {
                    status = 0;
                }
                else
                {
                    message = "Employer name contains invalid characters";
                }
            }
            else
            {
                message = "Maximum length for Employer name exceeded";
            }
        }
    }

    public void validateMemberNIC(string nic, out int status, out string message)
    {
        status = -1;
        message = "";

        Match match = Regex.Match(nic, @"[0-9]{9}[V,X,v,x]$|[1-2][0-9]{11}$", RegexOptions.IgnoreCase);
        if (match.Success)
        {
            status = 0;
        }
        else
        {
            message = "NIC is invalid";
        }
    }

    public void validateMemberName(string name, out int status, out string message)
    {
        status = -1;
        message = "";

        if (name != "")
        {
            if (name.Length <= 100)
            {
                if (filter_IDs(name))
                {
                    status = 0;
                }
                else
                {
                    message = "Name contains invalid characters";
                }
            }
            else
            {
                message = "Maximum length for name exceeded";
            }
            
        }
        else
        {
            message = "Member name should be entered";
        }
    }

    public void validateMemberPP(string PPNo, out int status, out string message)
    {
        status = -1;
        message = "";

        if (PPNo.Length <= 20)
        {
            if (filter_IDs(PPNo))
            {
                status = 0;
            }
            else
            {
                message = "Passport Number contains invalid characters";
            }
        }
        else
        {
            message = "Maximum length for Passport exceeded";
        }
    }

    //--------------------------------------------------------------------------------------------------------------

    //-----------------------------Validations for Globe trotter----------------------------------------------------

    public void validatePlanType(string planType, out int status, out string message)
    {
        status = -1;
        message = "";

        if (planType.Length == 1)
        {
            if (filter_IDs(planType))
            {
                status = 0;
            }
            else
            {
                message = "Plan Type contains invalid characters";
            }
        }
        else
        {
            message = "Plan Type is invalid";
        }
    }

    public void validateDobForGT(string uname, string category, string dateofbirth, out int status, out string message)
    {
        status = -1;
        message = "";
        double age = 0.00;
        try
        {
            DateTime dob = DateTime.ParseExact(dateofbirth, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (dob < DateTime.Now)
            {
                string dob_ = dob.ToString("yyyyMMdd");

                if (category == "M")
                {
                    if (!IsRegisteredDob(uname, dob_))
                    {
                        message = "Date of Birth is not matching with that of registered user.";
                    }
                    else
                    {
                        age = Math.Round((DateTime.Now - dob).Days / 365.25, 2);
                        if (age <= 80)
                        {
                            if (age <= 70)
                            {
                                if ((category == "M" || category == "S") && age < 18)
                                {
                                    message = "Main Life/ Spouse should not be less than 18.";
                                }
                                else
                                {
                                    status = 0;
                                }
                            }
                            else
                            {
                                message = "Declaration of health must be completed for persons above 70 years.";
                            }
                        }
                        else
                        {
                            message = "Date of Birth is not valid for Globe Trotter.";
                        }
                    }
                }
                else
                {
                    age = Math.Round((DateTime.Now - dob).Days / 365.25, 2);
                    if (age <= 80)
                    {
                        if (age <= 70)
                        {
                            if ((category == "M" || category == "S") && age < 18)
                            {
                                message = "Main Life/ Spouse should not be less than 18.";
                            }
                            else
                            {
                                status = 0;
                            }
                        }
                        else
                        {
                            message = "Declaration of health must be completed for persons above 70 years.";
                        }
                    }
                    else
                    {
                        message = "Date of Birth is not valid for Globe Trotter.";
                    }
                }                
            }
            else
            {
                message = "Date of birth should be less than today";
            }
        }
        catch
        {
            message = "Date of birth is not a valid date";
        }

    }

    public string getAgeForGT(string uname, string category, string dateofbirth, string toDate, out double ageOnArrival)
    {
        string mesg = "Internal error occured";
        //age = 0.00;
        ageOnArrival = 0.00;
        try
        {
            DateTime dob = DateTime.ParseExact(dateofbirth, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime toDt = DateTime.ParseExact(toDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            //age = Math.Round((DateTime.Now - dob).Days / 365.25);
            ageOnArrival = Math.Floor((toDt - dob).Days / 365.25);

            if (dob < DateTime.Now)
            {
                string dob_ = dob.ToString("yyyyMMdd");

                if (category == "M")
                {
                    if (IsRegisteredDob(uname, dob_))
                    {
                        if (ageOnArrival >= 18)
                        {
                            if (ageOnArrival < 70)
                            {
                                mesg = "success";
                            }
                            else if (ageOnArrival <= 80)
                            {
                                mesg = "Your travel insurance policy could be issued on receipt of the medical report duly completed & signed by your family doctor or a qualified doctor.";
                            }
                            else
                            {
                                mesg = "Sorry we do not have travel insurance policy for persons above 80 years of age.";
                            }
                        }
                        else
                        {
                            mesg = "Sorry. Main Life should be more than or equal to 18 years old.";
                        }
                    }
                    else
                    {
                        mesg = "Date of Birth is not matching with that of registered user.";
                    }
                }
                else if (category != "C")
                {
                    if (ageOnArrival >= 18)
                    {
                        if (ageOnArrival < 70)
                        {
                            mesg = "success";
                        }
                        else if (ageOnArrival <= 80)
                        {
                            mesg = "Your travel insurance policy could be issued on receipt of the medical report duly completed & signed by your family doctor or a qualified doctor.";
                        }
                        else
                        {
                            mesg = "Sorry we do not have travel insurance policy for persons above 80 years of age.";
                        }
                    }
                    else
                    {
                        mesg = "Sorry. A member should be more than or equal to 18 years old.";
                    }
                }
                else if (category == "C")
                {
                    if (ageOnArrival >= 1)
                    {
                        if (ageOnArrival <= 50)
                        {
                            mesg = "success";
                        }
                        else
                        {
                            mesg = "A child should be less than 50 years old.";
                        }
                    }
                    else
                    {
                        mesg = "Sorry. Child should be more than or equal to 1 year old.";
                    }
                }
            }
            else
            {
                mesg = "Date of birth should be less than today";
            }
        }
        catch
        {
            mesg = "Date of birth is not a valid date";
        }

        return mesg;

    }

    public void validateDestination(string destination, out int status, out string message)
    {
        status = -1;
        message = "";

        if (destination.Length >= 2 && destination.Length <= 3)
        {
            if (filter_IDs(destination))
            {
                status = 0;
            }
            else
            {
                message = "Destination contains invalid characters";
            }
        }
        else
        {
            message = "Destination is invalid";
        }
    }

    public void validateFromDate(string fromDate, out int status, out string message)
    {
        status = -1;
        message = "";
        
        try
        {
            DateTime fromDt = DateTime.ParseExact(fromDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime currDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture), "yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (fromDt >= currDate)
            {
                status = 0;
            }
            else
            {
                message = "From Date should be greater than or equal to current date";
            }
        }
        catch
        {
            message = "From Date is not a valid date";
        }
    }

    public void validateToDate(string fromDate, string toDate, out int status, out string message)
    {
        status = -1;
        message = "";

        try
        {
            DateTime fromDt = DateTime.ParseExact(fromDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime toDt = DateTime.ParseExact(toDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime currDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture), "yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (toDt >= fromDt)
            {
                if ((toDt - fromDt).Days <= 180)
                {
                    status = 0;
                }
                else
                {
                    message = "Maximum duration per trip is 180 days.";
                }
            }
            else
            {
                message = "To Date should be greater than or equal to From date";
            }
        }
        catch
        {
            message = "To Date is not a valid date";
        }
    }

    public void validateTravelPurpose(string travlPurpose, out int status, out string message)
    {
        status = -1;
        message = "";

        if (travlPurpose.Length == 2)
        {
            if (filter_IDs(travlPurpose))
            {
                status = 0;
            }
            else
            {
                message = "Travel Purpose contains invalid characters";
            }
        }
        else
        {
            message = "Travel Purpose is invalid";
        }
    }

    public void validateContctName(string contName, out int status, out string message)
    {
        status = -1;
        message = "";

        if (contName.Length <= 100)
        {
            if (filter_IDs_without_singq(contName))
            {
                status = 0;
            }
            else
            {
                message = "Contact Name contains invalid characters";
            }
        }
        else
        {
            message = "Contact Name should not be more than 100 characters long";
        }       
    }

    public void validateContAddressLine(string contAdrs, out int status, out string message)
    {
        status = -1;
        message = "";

        if (contAdrs.Length <= 30)
        {
            if (filter_IDs_without_singq(contAdrs))
            {
                status = 0;
            }
            else
            {
                message = "Address Line contains invalid characters";
            }
        }
        else
        {
            message = "Address Line should not be more than 30 characters";
        }

    }

    public void validateContactNumber(string cntactNumber, out int status, out string message)
    {
        status = -1;
        message = "";

        if (checkBeginTrailSpace(cntactNumber) && checkMiddleSpace(cntactNumber))
        {
            if (cntactNumber.Length <= 10)
            {
                if (filter_IDs(cntactNumber))
                {
                    Match match = Regex.Match(cntactNumber, @"07[0-9]{8}", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        status = 0;
                    }
                    else
                    {
                        message = "Contact number should be a valid mobile number";
                    }
                }
                else
                {
                    message = "Contact number contains invalid characters";
                }
            }
            else
            {
                message = "Contact number should not be more than 10 characters";
            }
        }
        else
        {
            message = "Contact number should not contain space characters";
        }
    }

    public void validateGTMemberPPNo(string ppno, string uname, int i ,out int status, out string message)
    {
        status = -1;
        message = "";

        //Match match = Regex.Match(ppno, @"[A-Z][0-9]*", RegexOptions.IgnoreCase);

        

        if (ppno.Length <= 20)
        {
            if (i == 1)
            {
                if (IsRegisteredPPNo(ppno, uname))
                {
                    message = "This Passport number is already registered.";
                }
                else
                {
                    status = 0;
                }
            }
            else
            {
                status = 0;
            }
        }
        else
        {
            message = "Passport Number is invalid";
        }
    }

    public void validateGTMemberName(string name, out int status, out string message)
    {
        status = -1;
        message = "";

        if (name != "")
        {
            if (name.Length <= 100)
            {
                if (filter_IDs(name))
                {
                    status = 0;
                }
                else
                {
                    message = "Name contains invalid characters";
                }
            }
            else
            {
                message = "Maximum length for name exceeded";
            }

        }
        else
        {
            message = "Member name should be entered";
        }
    }
    //--------------------------------------------------------------------------------------------------------------

    public void validateNIC_PolicyRevival(string nic, out int status, out string message)
    {
        status = -1;
        message = "";

        Match match = Regex.Match(nic, @"^[0-9]{9}[V,X,v,x]$|[1-2]{1}[0-9]{11}$", RegexOptions.IgnoreCase);
        if (match.Success)
        {
            //if (IsRegisteredNIC(nic))
            //{
            //    message = "This NIC is already registered";
            //}
            //else
            //{
            //    status = 0;
            //}
        }
        else
        {
            message = "NIC is invalid";
        }
    }
}