using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for TRV_InfoValidator
/// </summary>
public class TRV_InfoValidator
{
    public TRV_InfoValidator()
    {
        //
        // TODO: Add constructor logic here
        //
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
                     
                        age = Math.Round((DateTime.Now - dob).Days / 365.25, 2);
                        if (age <= 80)
                        {
                            if (age <= 80)
                            {
                                
                                    status = 0;
                                
                            }
                            else
                            {
                                //message = "Declaration of health must be completed for persons above 70 years.";
                            }
                        }
                        else
                        {
                            message = "Age should be less than 80 for a Travel Policy.";
                        }
                    
                }
                else
                {
                    age = Math.Round((DateTime.Now - dob).Days / 365.25, 2);
                    if (age <= 80)
                    {
                        if (age <= 80)
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
                            message = "Age should be less than 80 for a Travel Policy.";
                        }
                    }
                    else
                    {
                        message = "Age should be less than 80 for a Travel Policy.";
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

    public string validateMinMaxAgeForTRV(string uname, string category, string minage, string maxage, string dateofbirth, string deptdate,string arrivedate, out double ageOnArrival)
    {
        string mesg = "Internal error occured";
         
        double Calcage = 0;
        double minCalcAge = 0;
        double MaxCalcAge = 0;

        int minCalcMn = ((DateTime.Parse(deptdate).Year - DateTime.Parse(dateofbirth).Year) * 12) + (DateTime.Parse(deptdate).Month - DateTime.Parse(dateofbirth).Month);
        int maxCalcMn = ((DateTime.Parse(arrivedate).Year - DateTime.Parse(dateofbirth).Year) * 12) + (DateTime.Parse(arrivedate).Month - DateTime.Parse(dateofbirth).Month);
        
        //2021-12-29 Age Limits Changed
        //DateTime maxEndDate = DateTime.Parse(dateofbirth).AddYears(80);
        int maxAge = Convert.ToInt32(maxage);
        DateTime maxEndDate = DateTime.Parse(dateofbirth).AddYears(maxAge);

        DateTime mindeptDate = DateTime.Parse(dateofbirth).AddMonths(6);


        if (DateTime.Parse(deptdate).Day < DateTime.Parse(dateofbirth).Day)
        {
            minCalcMn--;
        }
        if (DateTime.Parse(deptdate).Day < DateTime.Parse(dateofbirth).Day)
        {
            maxCalcMn--;
        }
        double minTotYears = minCalcMn / 12d;
        double MaxTotYears = maxCalcMn / 12d;

        minCalcAge = Math.Round(minTotYears);
        MaxCalcAge = Math.Round(MaxTotYears);
        ageOnArrival = MaxCalcAge;
        if (DateTime.Parse(arrivedate) > maxEndDate)
        {
            mesg = "Age is more than " + maxAge + " Years to Arrival Date ";
        }
        else if ((DateTime.Parse(deptdate) < mindeptDate))
        {
            mesg = "For infants less than 06 months, please contact Sri Lanka Insurance Corporation General Ltd., Hot Line - 011 235 7357, to contact Head office. ";
        }
        else
        {
            mesg = "success";
        }
         
        /*
        //age = 0.00;
        ageOnArrival = 0.00;
        try
        {
            DateTime dob = DateTime.ParseExact(dateofbirth, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime toDt = DateTime.ParseExact(toDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            //age = Math.Round((DateTime.Now - dob).Days / 365.25);
            // ageOnArrival = Math.Round((toDt - dob).Days / 365.25);
            ageOnArrival = (toDt - dob).TotalDays / 365.25;
            if (dob < DateTime.Now)
            {
                string dob_ = dob.ToString("yyyyMMdd");

                if (category == "M")
                {


                    if (ageOnArrival < 80)
                    {
                        mesg = "success";
                    }
                    else
                    {
                        mesg = "Sorry we do not have travel insurance policy for persons above 80 years of age.";
                    }

                }
                else if (category != "C")
                {
                    if (ageOnArrival < 80 && ageOnArrival >= 1)
                    {
                        mesg = "success";
                    }
                    else if (ageOnArrival == 0)
                    {
                        mesg = "All the Members Should be greater than or equal to 1 Year.";
                    }
                    else
                    {
                        mesg = "Sorry we do not have travel insurance policy for persons above 80 years of age.";
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
                        mesg = "Sorry. Child should be more than or equal to 6 months old.";
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
        */
        return mesg;

    } 
    public string getAgeForTRV(string uname, string category, string dateofbirth, string toDate, out double ageOnArrival)
    {
        string mesg = "Internal error occured";
        //age = 0.00;
        ageOnArrival = 0.00;
        try
        {
            DateTime dob = DateTime.ParseExact(dateofbirth, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime toDt = DateTime.ParseExact(toDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            //age = Math.Round((DateTime.Now - dob).Days / 365.25);
           // ageOnArrival = Math.Round((toDt - dob).Days / 365.25);
            ageOnArrival = (toDt - dob).TotalDays / 365.25;
            if (dob < DateTime.Now)
            {
                string dob_ = dob.ToString("yyyyMMdd");

                if (category == "M")
                {


                    if (ageOnArrival < 80)
                    {
                        mesg = "success";
                    }
                    else
                    {
                        mesg = "Sorry we do not have travel insurance policy for persons above 80 years of age.";
                    }

                }
                else if (category != "C")
                {
                    if (ageOnArrival < 80 && ageOnArrival >= 1)
                    {
                        mesg = "success";
                    }
                    else if (ageOnArrival == 0)
                    {
                        mesg = "All the Members Should be greater than or equal to 1 Year.";
                    }
                    else
                    {
                        mesg = "Sorry we do not have travel insurance policy for persons above 80 years of age.";
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
                        mesg = "Sorry. Child should be more than or equal to 6 months old.";
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

    public void validateNIC(string nic, out int status, out string message)
    {
        status = -1;
        message = "";

        Match match = Regex.Match(nic, @"^[0-9]{9}[V,X,v,x]$|[1-2]{1}[0-9]{11}$", RegexOptions.IgnoreCase);
        if (match.Success)
        {           
                status = 0;          
        }
        else
        {
            message = "NIC is invalid";
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
                    status = 0;
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
                   
                        status = 0;                   
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

    public bool checkNICWithDob(string dob, string gender, string nic, out int status) // dob should be in yyyy/mm/dd format
    {
        status = -1;
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
                        status = 0;
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
        catch (Exception e)
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

    public bool validateGenderWithTitle(string gender, string title)
    {
        bool retVal = true;
        string[] invalidFemaleTitles = { "Mr.", "Master." };
        string[] invalidMaleTitles = { "Mrs.", "Miss.", "Dr.(Miss.)", "Dr.(Mrs.)" };

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
}