using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class General_Authorized_Products_HPL_Payment : System.Web.UI.Page
{
    public string errorMsg { get; set; }
    EncryptDecrypt en_dec = new EncryptDecrypt();

    protected void Page_Load(object sender, EventArgs e)
    {
        CustProfile customer = new CustProfile();
        string ref_no = string.Empty;
        string cus_Name = string.Empty;
        string policy_no = string.Empty;
        string commencement_Date = string.Empty;
        string Sum_Assured = string.Empty;
        string Premium = string.Empty;

        string strReq = "";
        strReq = Request.RawUrl;
        string h = strReq.Substring(strReq.Length - 1);

        if (h == "#")
        {
            errorMsg = "Message : No URL Found. Please Try Again";
        }
        else
        {
            strReq = strReq.Substring(strReq.IndexOf('?') + 1);
            strReq = en_dec.Decrypt(strReq);

            if (strReq == "#")
            {
                errorMsg = "Message : Information Missing. Please Try Again";
            }
            else
            {
                Dictionary<string, string> paraList = new Dictionary<string, string>();
                paraList = en_dec.getParameters(strReq);

                if (!Page.IsPostBack)
                {
                    try
                    {
                        if (paraList.ContainsKey("Ref_No") && paraList.ContainsKey("Type"))
                        {
                            Ltxt_payRefNo.Text = paraList["Ref_No"];

                            this.GetCustomerPolicyInformation(Ltxt_payRefNo.Text);
                        }
                        else
                        {
                            errorMsg = "In Complete User Information. Please Update User Profile On E-Document System.";
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = string.Format("Message: {0}", ex.Message + ". " + System.DateTime.Now);

                    }
                }
                else
                {
                }
            }
        }
    }


    protected void GetCustomerPolicyInformation(string ref_no)
    {    
        HPL_Transactions getRecord = new HPL_Transactions();
        HPL_CustomerPolicy userProfile = new HPL_CustomerPolicy();
        HPL_SQL hpl_SQL = new HPL_SQL();
        try
        {
            List<HPL_CustomerPolicy> policy_info = getRecord.GetUserProfile(hpl_SQL.GetPolicyInformation(ref_no));

            if (getRecord.Trans_Sucess_State == true)
            {       
                if (policy_info.Count > 0)
                {
                    
                    if (!String.IsNullOrEmpty(policy_info[0].cus_Name) && !String.IsNullOrEmpty(policy_info[0].commencement_Date)  && !String.IsNullOrEmpty(policy_info[0].Sum_Assured) && !String.IsNullOrEmpty(policy_info[0].Premium))
                    {
                        LtxtCustomer.Text = policy_info[0].cus_Name;
                        Ltxt_pay_CommencementDate.Text = policy_info[0].commencement_Date;
                        Ltxt_sumAssured.Text = double.Parse(policy_info[0].Sum_Assured).ToString("N2");
                        Ltxt_payPremium.Text = double.Parse(policy_info[0].Premium).ToString("N2");

                        Properties prop = new Properties();

                        hidVersion.Value = prop.Version;
                        hidMerchant.Value = prop.MerchantID;
                        hidAcquiredId.Value = prop.AcquiredId;
                        hidMerRespUrl.Value = prop.MerRespUrl;
                        hidPurcAmount.Value = setNumber(double.Parse(Ltxt_sumAssured.Text.Trim()));
                        // Must be removed when go live
                        hidPurcAmount.Value = setNumber(1.00);

                        hidPurchaseCurrency.Value = prop.PurchaseCurrency;
                        hidPurchaseCurrencyExponent.Value = prop.PurchaseCurrencyExponent;
                        hidGOrderId.Value = ref_no;
                        hidSignatureMethod.Value = prop.SignatureMethod;

                        hidCaptureFlag.Value = prop.CaptureFlag;
                        hidHostUrl.Value = prop.HostUrl;

                        string valuetobehashed = prop.Password + prop.MerchantID + prop.AcquiredId + ref_no + hidPurcAmount.Value.Trim() + prop.PurchaseCurrency;
                        //string valuetobehashed = prop.Password + prop.MerchantID + prop.AcquiredId + Lit_RefNo.Text.Trim();// +hidPurcAmount.Value.Trim() + prop.PurchaseCurrency;

                        hidSignature.Value = prop.getSignature(ref_no, hidPurcAmount.Value.Trim(), prop.PurchaseCurrency); //Hash.Hash.GetHash(valuetobehashed, Hash.Hash.HashType.SHA1);
                    }
                    else
                    {
                        /*When user profile is incomplete*/
                        string msg_heading = "Error: In-Complete User Profile.";
                        string message = "Incomplete User Profile.Please Contact System Administrator & Update The Profile.";
                        //Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
                    }
                }
                else
                {
                    _message.InnerHtml = getRecord.Error_Message;
                    /*User profile not registed on e_document System*/
                    string msg_heading = "Error: In Registration.";
                    string message = "User Does Not Register On eDocument System.Please Contact System Administrator To Register.";
                   // Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
                }
            }

            else
            {
                /*Any Oracle Transaction Error*/
                string msg_heading = "Error: In User Profile.";
                string message = "User Does Not Register On eDocument System.Please Contact System Administrator To Register.";
                //Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
            }
        }

        catch (Exception ex)
        {
            string msg_heading = "Error: In User Profile.";
            string message = ex.Message;
           // Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
        }
    }

    protected void btnPay_Click(object sender, EventArgs e)
    {
        string html = null;
        string hash = null;
        string fName = string.Empty;
        string lName = string.Empty;
        string email = string.Empty;
        string add1 = string.Empty;
        string addState = string.Empty;
        string cityTown = string.Empty;
        string cntry = string.Empty;
        string poCode = string.Empty;
        string mob_no = string.Empty;     

        Properties accProp = new Properties();
        CustProfile cusPrf = new CustProfile(Page.User.Identity.Name);

        if (!String.IsNullOrEmpty(cusPrf.O_firstName))
        {
            fName = cusPrf.O_firstName;
        }
        else
        {
            fName = "NA";
        }

        if (!String.IsNullOrEmpty(cusPrf.O_lastName))
        {
            lName = cusPrf.O_lastName;
        }
        else
        {
            lName = "NA";
        }

        if (!String.IsNullOrEmpty(cusPrf.O_email))
        {
            email = cusPrf.O_email;
        }
        else
        {
            email = "NA";
        }

        if (!String.IsNullOrEmpty(cusPrf.O_addrss1))
        {
            Regex reg = new Regex("[*'\",_&#^@]");
            add1 = reg.Replace(cusPrf.O_addrss1, string.Empty);
        }
        else
        {
            add1 = "NA";
        }

        if (!String.IsNullOrEmpty(cusPrf.O_country))
        {
            addState = cusPrf.O_country; ;
        }
        else
        {
            addState = "NA";
        }


        if (!String.IsNullOrEmpty(cusPrf.O_cityTown))
        {
            cityTown = cusPrf.O_cityTown;
        }
        else
        {
            cityTown = "NA";
        }

        if (!String.IsNullOrEmpty(cusPrf.O_country))
        {
            cntry = cusPrf.O_country;
        }
        else
        {
            cntry = "NA";
        }

        //Visa Master Change 2024
        if (!String.IsNullOrEmpty(cusPrf.O_postCode))
        {
            poCode = cusPrf.O_postCode;
        }
        else
        {
            poCode = "00200";
        }


        if (!String.IsNullOrEmpty(cusPrf.O_mobileNumber) && Regex.IsMatch(cusPrf.O_mobileNumber, @"^(?:07|7(?:\+94))[1|2|4|5|6|7][0-9]{9,10}|(?:07|7)[1|2|4|5|6|7|8][0-9]{7}$"))
        {
            mob_no = cusPrf.O_mobileNumber;
        }
        else
        {
            mob_no = "NA";
        }



        if (rdbPayMethod.SelectedIndex == 0 || rdbPayMethod.SelectedIndex == 1)
        {
            if (Math.Round(Double.Parse(hidPurcAmount.Value.Trim()), 2) > 0)
            {
                html = " <HTML><body><form id='FrmHtmlCheckout' name='FrmHtmlCheckout' action='' method='post'>" +
                " <input id='access_key' type='hidden' name='access_key' value='" + accProp.AccessKey + "'>" +
                " <input id='profile_id' type='hidden' name='profile_id' value='" + accProp.ProfileId + "'>" +
                " <input id='transaction_uuid' type='hidden' name='transaction_uuid' value='" + Ltxt_payRefNo.Text.Trim() + "'>" +
                " <input id='signed_field_names' type='hidden' value='access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency,bill_to_forename,bill_to_surname,bill_to_email,bill_to_address_line1,bill_to_address_state,bill_to_address_country,bill_to_address_city,customer_ip_address,customer_browser_screen_height,customer_browser_screen_width,bill_to_address_postal_code,bill_to_phone,payer_authentication_mobile_phone' name='signed_field_names' > " +
                " <input id='unsigned_field_names' type='hidden' name='unsigned_field_names' >" +
                " <input id='signed_date_time' type='hidden' value='" + DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'") + "' name='signed_date_time'>" +
                " <input id='locale' type='hidden' value='" + accProp.Locale + "' name='locale'>" +
                " <input id='transaction_type' type='hidden' value='" + accProp.TransactionType + "' name='transaction_type'>" +
                " <input id='reference_number' type='hidden' value='" + Ltxt_payRefNo.Text.Trim() + "' name='reference_number'>" +
                " <input id='amount' type='hidden' value='" + Convert.ToString(Convert.ToDouble(Ltxt_payPremium.Text.Trim())) + "' name='amount' >" +
                " <input id='currency' type='hidden' value='" + accProp.Currency + "' name='currency'>" +
                " <input id='bill_to_forename' type='hidden' value='" + fName + "' name='bill_to_forename'>" +
                " <input id='bill_to_surname' type='hidden' value='" + lName + "' name='bill_to_surname'>" +
                " <input id='bill_to_email' type='hidden' value='" + email + "' name='bill_to_email'>" +
                " <input id='bill_to_address_line1' type='hidden' value='" + add1 + "' name='bill_to_address_line1'>" +
                " <input id='bill_to_address_state' type='hidden' value='WP-11' name='bill_to_address_state'>" +
                " <input id='bill_to_address_country' type='hidden' value='LK' name='bill_to_address_country'>" +
                " <input id='bill_to_address_city' type='hidden' value='" + cityTown + "' name='bill_to_address_city' >" +
                //Visa Master 2024
                " <input id='customer_ip_address' type='hidden' value='" + GetIpAddress() + "' name='customer_ip_address'>" +
                " <input id='customer_browser_screen_height' type='hidden' value='" + hid_height.Value.ToString() + "' name='customer_browser_screen_height'>" +
                " <input id='customer_browser_screen_width' type='hidden' value='" + hid_width.Value.ToString() + "' name='customer_browser_screen_width'>" +
                " <input id='bill_to_address_postal_code' type='hidden' value='" + poCode + "' name='bill_to_address_postal_code'>" +
                " <input id='bill_to_phone' type='hidden' value='" + mob_no + "' name='bill_to_phone'>" +
                " <input id='payer_authentication_mobile_phone' type='hidden' value='" + mob_no + "' name='payer_authentication_mobile_phone'>" +
                //Visa Master 2024 - END
                " </form>" +
                " <script language='javascript'> CheckOut(); " +
                " function CheckOut() {" +
                " document.FrmHtmlCheckout.action = 'payment_confirmation.aspx'; " +
                " document.FrmHtmlCheckout.submit(); " +
                " }</script></body></HTML>";

                log lg = new log();
                lg.write_log(html);
                Response.Write(html);

            }     
        }

        else if (rdbPayMethod.SelectedIndex == 2)
        {
            if (Math.Round(Double.Parse(hidPurcAmount.Value.Trim()), 2) > 0)
            {
                Properties accAmexProp = new Properties("General", "Amex");

                html = " <HTML><body><form id='FrmHtmlCheckout' name='FrmHtmlCheckout' action='' method='post'>" +
                     " <input id='mer_id' type='hidden' name='mer_id' value='" + accAmexProp.MerchantID + "'>" +
                     " <input id='mer_txn_id' type='hidden' name='mer_txn_id' value='" + Ltxt_payRefNo.Text.Trim() + "'>" +
                     " <input id='action' type='hidden' name='action' value='" + accAmexProp.TransactionType + "'>" +
                     " <input id='txn_amt' type='hidden' value='" + Convert.ToDouble(Ltxt_payPremium.Text.Trim()).ToString("F") + "' name='txn_amt' >" +
                     " <input id='cur' type='hidden' name='cur' value='" + accAmexProp.Currency + "'>" +
                     " <input id='lang' type='hidden' name='lang' value='" + accAmexProp.Locale + "'>" +
                     " <input id='ret_url' type='hidden' name='ret_url' value='" + accAmexProp.MerRespUrl + "'>" +
               " </form>" +
                " <script language='javascript'> CheckOut(); " +
                " function CheckOut() {" +
                " document.FrmHtmlCheckout.action = 'Payment_Confirmation_Amex.aspx'; " +
                " document.FrmHtmlCheckout.submit(); " +
                " }</script></body></HTML>";

                log lg = new log();
                lg.write_log(html);
                Response.Write(html);
            }
        }
    }


    string setNumber(double amount)
    {
        string result = "";

        double amnt = Math.Round(amount, 2);
        string number = amnt.ToString();

        bool j = number.Contains('.');

        if (j)
        {

            string[] num = number.Split('.');
            string integer = num[0];
            string decimals = num[1];

            while (integer.Length < 10)
            {
                integer = "0" + integer;
            }
            while (decimals.Length < 2)
            {
                decimals = decimals + "0";
            }
            result = integer + decimals;

        }
        else
        {
            while (number.Length < 10)
            {
                number = "0" + number;
            }
            result = number + "00";
        }

        return result;
    }

    protected string GetIpAddress()
    {
        string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipList))
            return ipList.Split(',')[0];

        return Request.ServerVariables["REMOTE_ADDR"];
    }
}