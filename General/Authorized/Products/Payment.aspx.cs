using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class General_Authorized_Products_Payment : System.Web.UI.Page
{
    public string errorMsg { get; set; }
    OraDB ora = new OraDB();
    EncryptDecrypt dc = new EncryptDecrypt();
    string NIC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
		log lg = new log();

                lg.write_log("Payment Page Load");

            string strReq = "";
            errorMsg = "";
            strReq = Request.RawUrl;
            string h = strReq.Substring(strReq.Length - 1);
            if (strReq.Contains("?"))
            {

                if (h == "#")
                {
                    errorMsg = "No url";
                    Server.Transfer("ErrorPage.aspx");
                }
                else
                {
                    strReq = strReq.Substring(strReq.IndexOf('?') + 1);
                    strReq = dc.Decrypt(strReq);
                    if (strReq == "#")
                    {
                        errorMsg = "No Parameters Passed";
                        Server.Transfer("ErrorPage.aspx");
                    }
                    else
                    {

                        Dictionary<string, string> paraList = new Dictionary<string, string>();
                        paraList = dc.getParameters(strReq);


             
                        if (paraList.ContainsKey("Ref_No") && paraList.ContainsKey("Type") && paraList.ContainsKey("Pol_No"))
                        {
                            Lit_RefNo.Text = paraList["Ref_No"];
                            hdf_busi_type.Value = paraList["Type"];
                            string busi_type = paraList["Type"];
                            hdf_polNo.Value = paraList["Pol_No"];
                            if (busi_type.Trim().Equals("N"))
                            {


                                Proposal pr = new Proposal(Lit_RefNo.Text.Trim());

                                Lit_CusName.Text = pr.title + " " + pr.fullName;
                                Lit_PolType.Text = pr.product_Name;
                                Lit_comDate.Text = pr.comenmentDate;
                                if (pr.productID == "GTI")
                                {
                                    Lit_SumAssured.Text = "USD " + pr.sumAssured.ToString("N2");
                                }
                                else
                                {
                                    Lit_SumAssured.Text = "Rs. " + pr.sumAssured.ToString("N2");
                                }

                                Lit_Premium.Text = pr.annualPremium.ToString("N2");

                                TRV_Proposal pr2 = new TRV_Proposal(Lit_RefNo.Text.Trim());

                                if (pr2.policyType == "TPI" || pr2.policyType == "TPM")
                                {
                                    Lit_CusName.Text = pr2.title + " " + pr2.fullName;
                                    Lit_PolType.Text = pr2.product_Name;
                                    Lit_comDate.Text = pr2.comenmentDate;
                                    /*TRV_Client client = new TRV_Client();
                                    client.GenerateClient(client);
                                    Label1.Text ="Hello"+ client.error;*/
                                }
                                /*
                                Lit_CusName.Text = pr.title + " " + pr.fullName;
                                Lit_PolType.Text = pr.product_Name;
                                Lit_comDate.Text = pr.comenmentDate;*/

                                if (pr.productID == "GTI")
                                {

                                    Lit_SumAssured.Text = "USD " + pr.sumAssured.ToString("N2");
                                    Lit_Premium.Text = pr.annualPremium.ToString("N2");
                                }
                                else if (pr.productID == "TPI" || pr.productID == "TPM")
                                {
                                    Lit_SumAssured.Text = "USD " + pr2.sumAssured.ToString("N2");
                                    if (Page.User.Identity.Name.ToLower() == "vasanaw" || Page.User.Identity.Name.ToLower() == "anshu")
                                    {
                                        double test = 10.00;
                                        Lit_Premium.Text = test.ToString("N2");
                                    }
                                    else
                                    {
                                        Lit_Premium.Text = pr2.annualPremium.ToString("N2");
                                    }
                                }
                                else
                                {
                                    Lit_SumAssured.Text = "Rs. " + pr.sumAssured.ToString("N2");
                                    //Lit_Premium.Text = pr.annualPremium.ToString("N2");
                                    if (Page.User.Identity.Name.ToLower() == "vasanaw" || Page.User.Identity.Name.ToLower() == "anshu")
                                    {
                                        double test = 10.00;
                                        Lit_Premium.Text = test.ToString("N2");
                                    }
                                    else
                                    {
                                        Lit_Premium.Text = pr.annualPremium.ToString("N2");
                                    }
                                }

                                //if (pr2.policyType == "TPI")
                                //{
                                //    Lit_SumAssured.Text = "USD " + pr.sumAssured.ToString("N2");
                                //    Lit_Premium.Text = pr.annualPremium.ToString("N2");
                                //}
                                ////else
                                ////{
                                ////    Lit_SumAssured.Text = "Rs. " + pr.sumAssured.ToString("N2");
                                ////}


                                Lit_PayType.Text = "New Purchase";
                                Properties prop = new Properties();


                                hidVersion.Value = prop.Version;
                                hidMerchant.Value = prop.MerchantID;
                                hidAcquiredId.Value = prop.AcquiredId;
                                hidMerRespUrl.Value = prop.MerRespUrl;
                                hidPurcAmount.Value = setNumber(pr.annualPremium);
                                // Must be removed when go live
                                //hidPurcAmount.Value = setNumber(1.00);

                                hidPurchaseCurrency.Value = prop.PurchaseCurrency;
                                hidPurchaseCurrencyExponent.Value = prop.PurchaseCurrencyExponent;
                                hidGOrderId.Value = Lit_RefNo.Text.Trim();
                                hidSignatureMethod.Value = prop.SignatureMethod;

                                hidCaptureFlag.Value = prop.CaptureFlag;
                                hidHostUrl.Value = prop.HostUrl;

                                string valuetobehashed = prop.Password + prop.MerchantID + prop.AcquiredId + Lit_RefNo.Text.Trim() + hidPurcAmount.Value.Trim() + prop.PurchaseCurrency;
                                //string valuetobehashed = prop.Password + prop.MerchantID + prop.AcquiredId + Lit_RefNo.Text.Trim();// +hidPurcAmount.Value.Trim() + prop.PurchaseCurrency;

                                hidSignature.Value = prop.getSignature(Lit_RefNo.Text.Trim(), hidPurcAmount.Value.Trim(), prop.PurchaseCurrency); //Hash.Hash.GetHash(valuetobehashed, Hash.Hash.HashType.SHA1);
                            }
                            else if (busi_type.Trim().Equals("R"))
                            {
                                Renewal pr = new Renewal(Lit_RefNo.Text.Trim());

                                Lit_CusName.Text = pr.custName;
                                Lit_PolType.Text = pr.polTypName;
                                Lit_comDate.Text = pr.startDate;
                                Lit_SumAssured.Text = pr.sumAssurd.ToString("N2");
                                Lit_Premium.Text = pr.amount.ToString("N2");
                                Lit_PayType.Text = "Renewal";
                                Properties prop = new Properties();


                                hidVersion.Value = prop.Version;
                                hidMerchant.Value = prop.MerchantID;
                                hidAcquiredId.Value = prop.AcquiredId;
                                hidMerRespUrl.Value = prop.MerRespUrl;
                                hidPurcAmount.Value = setNumber(pr.amount);
                                // Must be removed when go live
                                //hidPurcAmount.Value = setNumber(1.00);

                                hidPurchaseCurrency.Value = prop.PurchaseCurrency;
                                hidPurchaseCurrencyExponent.Value = prop.PurchaseCurrencyExponent;
                                hidGOrderId.Value = Lit_RefNo.Text.Trim();
                                hidSignatureMethod.Value = prop.SignatureMethod;

                                hidCaptureFlag.Value = prop.CaptureFlag;
                                hidHostUrl.Value = prop.HostUrl;

                                string valuetobehashed = prop.Password + prop.MerchantID + prop.AcquiredId + Lit_RefNo.Text.Trim() + hidPurcAmount.Value.Trim() + prop.PurchaseCurrency;
                                //string valuetobehashed = prop.Password + prop.MerchantID + prop.AcquiredId + Lit_RefNo.Text.Trim();// +hidPurcAmount.Value.Trim() + prop.PurchaseCurrency;

                                hidSignature.Value = prop.getSignature(Lit_RefNo.Text.Trim(), hidPurcAmount.Value.Trim(), prop.PurchaseCurrency); //Hash.Hash.GetHash(valuetobehashed, Hash.Hash.HashType.SHA1);
                            }
                        }
                        //}
                        else
                        {
                            errorMsg = "No url";
                            Server.Transfer("ErrorPage.aspx");
                        }

                    }
                }
            }
            else
            {

            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string html = null;
        string hash = null;

        string fName = "";
        string lName = "";
        string email = "";
        string add1 = "";
        string addState = "";
        string cityTown = "";
        string cntry = "";
        //Visa Master 2024
        string poCode = string.Empty;
        string mob_no = string.Empty;          

        bool result_1 = false;
        bool result_2 = false;
     
        if (hdf_busi_type.Value.Trim().Equals("R"))
        {

            /*update PAY_METHOD to 'BOCGTW' in renewal_details*/
            Payment_Type update_PayMethod_obj = new Payment_Type();
            result_1 = update_PayMethod_obj.update_gen_pay_method_in_renewal(Lit_RefNo.Text.Trim(), "HNBGTW");
        }

        if (hdf_busi_type.Value.Trim().Equals("N"))
        {

            /*update PAY_METHOD to 'BOCGTW' in proposal_details*/
            Payment_Type update_PayMethod_obj_in_Prop_Details = new Payment_Type();
            result_2 = update_PayMethod_obj_in_Prop_Details.update_gen_pay_method_in_proposal_details(Lit_RefNo.Text.Trim(), "HNBGTW");
        }

        if (result_1 || result_2)
        {

            if (Math.Round(Double.Parse(hidPurcAmount.Value.Trim()), 2) > 0)
            {
                //html = " <HTML><body><form id='FrmHtmlCheckout' name='FrmHtmlCheckout' action='' method='post'>" +
                //        " <input id='Version' type='hidden' name='Version' value='" + hidVersion.Value.Trim() + "'>" +
                //        " <input id='MerID' type='hidden' value='" + hidMerchant.Value.Trim() + "' name='MerID' > " +
                //        " <input id='AcqID' type='hidden' value='" + hidAcquiredId.Value.Trim() + "' name='AcqID' >" +
                //        " <input id='MerRespURL' type='hidden' value='" + hidMerRespUrl.Value.Trim() + "' name='MerRespURL'>" +
                //        " <input id='PurchaseAmt' type='hidden' value='" + hidPurcAmount.Value.Trim() + "' name='PurchaseAmt'>" +
                //        " <input id='PurchaseCurrency' type='hidden' value='" + hidPurchaseCurrency.Value.Trim() + "' name='PurchaseCurrency'>" +
                //        " <input id='PurchaseCurrencyExponent' type='hidden' value='" + hidPurchaseCurrencyExponent.Value.Trim() + "' name='PurchaseCurrencyExponent'>" +
                //        " <input id='OrderID' type='hidden' value='" + hidGOrderId.Value.Trim() + "'name='OrderID' >" +
                //        " <input id='SignatureMethod' type='hidden' value='" + hidSignatureMethod.Value.Trim() + "' name='SignatureMethod'>" +
                //        " <input id='Signature' type='hidden' value='" + hidSignature.Value.Trim() + "'name='Signature'>" +
                //        " <input id='CaptureFlag' type='hidden' value='" + hidCaptureFlag.Value.Trim() + "' name='CaptureFlag' >" + " </form>" +
                //        " <script language='javascript'> CheckOut(); " +
                //        " function CheckOut() {" +
                //        " document.FrmHtmlCheckout.action = '" + hidHostUrl.Value.Trim() + "'; " +
                //        " document.FrmHtmlCheckout.submit(); " +
                //        " }</script></body></HTML>";
                //log lg = new log();

                //lg.write_log(html);


                //Response.Write(html);
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
                    //add1 = cusPrf.O_addrss1;

                    Regex reg = new Regex("[*'\",_&#^@]");
                    add1 = reg.Replace(cusPrf.O_addrss1, string.Empty);
                }
                else
                {
                    add1 = "NA";
                }

                if (!String.IsNullOrEmpty(cusPrf.O_country))
                {
                    //addState = cusPrf.O_addrss4;

                    //Regex reg = new Regex("[*'\",_&#^@]");
                    //addState = reg.Replace(cusPrf.O_addrss4, string.Empty);
					
					addState = cusPrf.O_country;;
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

                //Visa Master 2024
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
                //End - Visa Master 2024

                html = " <HTML><body><form id='FrmHtmlCheckout' name='FrmHtmlCheckout' action='' method='post'>" +
                " <input id='access_key' type='hidden' name='access_key' value='" + accProp.AccessKey + "'>" +
                " <input id='profile_id' type='hidden' name='profile_id' value='" + accProp.ProfileId + "'>" +
                    //" <input id='transaction_uuid' type='hidden' name='transaction_uuid' value='" + System.Guid.NewGuid().ToString() + "'>" +
                " <input id='transaction_uuid' type='hidden' name='transaction_uuid' value='" + Lit_RefNo.Text.Trim() + "'>" +
                " <input id='signed_field_names' type='hidden' value='access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency,bill_to_forename,bill_to_surname,bill_to_email,bill_to_address_line1,bill_to_address_state,bill_to_address_country,bill_to_address_city,customer_ip_address,customer_browser_screen_height,customer_browser_screen_width,bill_to_address_postal_code,bill_to_phone,payer_authentication_mobile_phone' name='signed_field_names' > " +
                " <input id='unsigned_field_names' type='hidden' name='unsigned_field_names' >" +
                " <input id='signed_date_time' type='hidden' value='" + DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'") + "' name='signed_date_time'>" +
                " <input id='locale' type='hidden' value='" + accProp.Locale + "' name='locale'>" +
                " <input id='transaction_type' type='hidden' value='" + accProp.TransactionType + "' name='transaction_type'>" +
                " <input id='reference_number' type='hidden' value='" + Lit_RefNo.Text.Trim() + "' name='reference_number'>" +
                " <input id='amount' type='hidden' value='" + Convert.ToString(Convert.ToDouble(Lit_Premium.Text.Trim())) + "' name='amount' >" +
                " <input id='currency' type='hidden' value='" + accProp.Currency + "' name='currency'>" +
                    //" <input id='currency' type='hidden' value='LKR' name='currency'>" +
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
                //End-Visa Master 2024

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



    protected void btn_BOC_Click(object sender, EventArgs e)
    {
        bool result_1 = false;
        bool result_2 = false;

        BOC_Payment emailAddress_Obj = new BOC_Payment();
        //hdf_cust_EmailAddress.Value = emailAddress_Obj.getLoginEmail(Page.User.Identity.Name);
        string userEmail = emailAddress_Obj.getLoginEmail(Page.User.Identity.Name);

        double pay_amount = Double.Parse(Lit_Premium.Text.Trim());
        double amnt = Math.Round(pay_amount, 2);
        string paymentAmount = amnt.ToString();

        if (hdf_busi_type.Value.Trim().Equals("R"))
        {

            /*update PAY_METHOD to 'BOCGTW' in renewal_details*/
            BOC_Payment update_PayMethod_obj = new BOC_Payment();
            result_1 = update_PayMethod_obj.update_gen_pay_method_in_renewal(Lit_RefNo.Text.Trim(), "BOCGTW");
        }

        if (hdf_busi_type.Value.Trim().Equals("N"))
        {

            /*update PAY_METHOD to 'BOCGTW' in proposal_details*/
            BOC_Payment update_PayMethod_obj_in_Prop_Details = new BOC_Payment();
            result_2 = update_PayMethod_obj_in_Prop_Details.update_gen_pay_method_in_proposal_details(Lit_RefNo.Text.Trim(), "BOCGTW");
        }

        if (result_1 || result_2)
        {

            if (amnt > 0)
            {

                EncryptDecrypt dc = new EncryptDecrypt();
                Dictionary<string, string> qs = new Dictionary<string, string>();
                qs.Add("Ref_No", Lit_RefNo.Text.Trim());
                qs.Add("Pol_No", hdf_polNo.Value);
                qs.Add("Amount", paymentAmount);
                qs.Add("Cust_Email", userEmail);


                Response.Redirect(dc.url_encrypt("~/General/Authorized/Products/BOC_PaymentForm.aspx", qs));
                //Response.Redirect("~/Life/Authorized/Products/BOC_Payment_Form.aspx");
            }
            else
            {
                errorMsg = "Amount should be more than 0";
                Server.Transfer("ErrorPage.aspx");

            }
        }

    }

    protected void rdbPayMethod_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void btn_Amex_Click(object sender, EventArgs e)
    {
        bool result_1 = false;
        bool result_2 = false;
        //BOC_Payment emailAddress_Obj = new BOC_Payment();
        ////hdf_cust_EmailAddress.Value = emailAddress_Obj.getLoginEmail(Page.User.Identity.Name);
        //string userEmail = emailAddress_Obj.getLoginEmail(Page.User.Identity.Name);

        //double pay_amount = Double.Parse(Lit_Premium.Text.Trim());
        //double amnt = Math.Round(pay_amount, 2);
        //string paymentAmount = amnt.ToString();

        if (hdf_busi_type.Value.Trim().Equals("R"))
        {

            /*update PAY_METHOD to 'BOCGTW' in renewal_details*/
            Payment_Type update_PayMethod_obj = new Payment_Type();
            result_1 = update_PayMethod_obj.update_gen_pay_method_in_renewal(Lit_RefNo.Text.Trim(), "AMEXGTW");
        }

        if (hdf_busi_type.Value.Trim().Equals("N"))
        {

            /*update PAY_METHOD to 'BOCGTW' in proposal_details*/
            Payment_Type update_PayMethod_obj_in_Prop_Details = new Payment_Type();
            result_2 = update_PayMethod_obj_in_Prop_Details.update_gen_pay_method_in_proposal_details(Lit_RefNo.Text.Trim(), "AMEXGTW");
        }

        if (result_1 || result_2)
        {
            string html = null;
            string hash = null;

            if (Math.Round(Double.Parse(hidPurcAmount.Value.Trim()), 2) > 0)
            {
                Properties accAmexProp = new Properties("General", "Amex");

                CustProfile cusPrf = new CustProfile(Page.User.Identity.Name);

                html = " <HTML><body><form id='FrmHtmlCheckout' name='FrmHtmlCheckout' action='' method='post'>" +
                     " <input id='mer_id' type='hidden' name='mer_id' value='" + accAmexProp.MerchantID + "'>" +
                     " <input id='mer_txn_id' type='hidden' name='mer_txn_id' value='" + Lit_RefNo.Text.Trim() + "'>" +
                     " <input id='action' type='hidden' name='action' value='" + accAmexProp.TransactionType + "'>" +
                     " <input id='txn_amt' type='hidden' value='" + Convert.ToDouble(Lit_Premium.Text.Trim()).ToString("F") + "' name='txn_amt' >" +
                     //" <input id='txn_amt' type='hidden' name='txn_amt' value='1.00'>" +
                     " <input id='cur' type='hidden' name='cur' value='" + accAmexProp.Currency + "'>" +
                     " <input id='lang' type='hidden' name='lang' value='" + accAmexProp.Locale + "'>" +
                     " <input id='ret_url' type='hidden' name='ret_url' value='" + accAmexProp.MerRespUrl + "'>" +
               " </form>" +
                " <script language='javascript'> CheckOut(); " +
                " function CheckOut() {" +
                " document.FrmHtmlCheckout.action = 'Payment_Confirmation_Amex.aspx'; " +
                    //" document.FrmHtmlCheckout.action = 'reqMsg.aspx'; " +
                " document.FrmHtmlCheckout.submit(); " +
                " }</script></body></HTML>";

                log lg = new log();

                lg.write_log(html);
                Response.Write(html);
            }
        }
    }


    protected string GetIpAddress()
    {
        string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipList))
            return ipList.Split(',')[0];

        return Request.ServerVariables["REMOTE_ADDR"];
    }
}