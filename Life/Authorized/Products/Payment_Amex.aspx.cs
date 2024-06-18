using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Life_Authorized_Products_Payment_Amex : System.Web.UI.Page
{
    public string errorMsg { get; set; }
    OraDB ora = new OraDB();
    EncryptDecrypt dc = new EncryptDecrypt();
    string NIC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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



                        if (paraList.ContainsKey("Ref_No") && paraList.ContainsKey("Type"))
                        {
                            Lit_RefNo.Text = paraList["Ref_No"];

                            string busi_type = paraList["Type"];
                            if (busi_type.Trim().Equals("N"))
                            {
                                LifePayment pr = new LifePayment(Lit_RefNo.Text.Trim());

                                Lit_PolNo.Text = pr.polNum;
                                Lit_CusName.Text = pr.custName;
                                //Lit_PolType.Text = pr.polTypName;
                                //Lit_comDate.Text = pr.startDate;
                                //Lit_SumAssured.Text = pr.sumAssurd.ToString("N2");
                                //if (pr.loanNo != "")
                                //{
                                //    Lit_PolNo.Text = Lit_PolNo.Text + " (Payment for Loan No: " + pr.loanNo + ")";
                                //    Lit_PayType.Text = "Loan Payment";
                                //}
                                //else
                                //{
                                Lit_PayType.Text = "Proposal Payment";
                                //}
                                Lit_Premium.Text = pr.amount.ToString("N2");

                                Properties prop = new Properties("Life");

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
                            else if (busi_type.Trim().Equals("R"))
                            {
                                LifePayment pr = new LifePayment(Lit_RefNo.Text.Trim());

                                Lit_PolNo.Text = pr.polNum;
                                Lit_CusName.Text = pr.custName;
                                //Lit_PolType.Text = pr.polTypName;
                                //Lit_comDate.Text = pr.startDate;
                                //Lit_SumAssured.Text = pr.sumAssurd.ToString("N2");
                                if (pr.loanNo != "")
                                {
                                    Lit_PolNo.Text = Lit_PolNo.Text + " (Payment for Loan No: " + pr.loanNo + ")";
                                    Lit_PayType.Text = "Loan Payment";
                                }
                                else
                                {
                                    Lit_PayType.Text = "Premium Payment";
                                }
                                Lit_Premium.Text = pr.amount.ToString("N2");

                                Properties prop = new Properties("Life");


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
                            /******************Updated**************************/
                            else if (busi_type.Trim().Equals("RV"))
                            {
                                LifePayment pr = new LifePayment(Lit_RefNo.Text.Trim());

                                Lit_PolNo.Text = pr.polNum;
                                Lit_CusName.Text = pr.custName;
                                //Lit_PolType.Text = pr.polTypName;
                                //Lit_comDate.Text = pr.startDate;
                                //Lit_SumAssured.Text = pr.sumAssurd.ToString("N2");
                                //if (pr.loanNo != "")
                                //{
                                //    Lit_PolNo.Text = Lit_PolNo.Text + " (Payment for Loan No: " + pr.loanNo + ")";
                                //    Lit_PayType.Text = "Loan Payment";
                                //}
                                //else
                                //{
                                Lit_PayType.Text = "Revival Payment";
                                //}
                                Lit_Premium.Text = pr.amount.ToString("N2");

                                Properties prop = new Properties("Life");

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
                            //else if (busi_type.Trim().Equals("L"))
                            //{
                            //    LifePayment pr = new LifePayment(Lit_RefNo.Text.Trim());

                            //    Lit_PolNo.Text = pr.polNum;
                            //    Lit_CusName.Text = pr.custName;
                            //    //Lit_PolType.Text = pr.polTypName;
                            //    //Lit_comDate.Text = pr.startDate;
                            //    //Lit_SumAssured.Text = pr.sumAssurd.ToString("N2");
                            //    //if (pr.loanNo != "")
                            //    //{
                            //    //    Lit_PolNo.Text = Lit_PolNo.Text + " (Payment for Loan No: " + pr.loanNo + ")";
                            //    //    Lit_PayType.Text = "Loan Payment";
                            //    //}
                            //    //else
                            //    //{
                            //    Lit_PayType.Text = "Loan Payment";
                            //    //}
                            //    Lit_Premium.Text = pr.amount.ToString("N2");

                            //    Properties prop = new Properties("Life");

                            //    hidVersion.Value = prop.Version;
                            //    hidMerchant.Value = prop.MerchantID;
                            //    hidAcquiredId.Value = prop.AcquiredId;
                            //    hidMerRespUrl.Value = prop.MerRespUrl;
                            //    hidPurcAmount.Value = setNumber(pr.amount);

                            //    // Must be removed when go live
                            //    //hidPurcAmount.Value = setNumber(1.00);

                            //    hidPurchaseCurrency.Value = prop.PurchaseCurrency;
                            //    hidPurchaseCurrencyExponent.Value = prop.PurchaseCurrencyExponent;
                            //    hidGOrderId.Value = Lit_RefNo.Text.Trim();
                            //    hidSignatureMethod.Value = prop.SignatureMethod;

                            //    hidCaptureFlag.Value = prop.CaptureFlag;
                            //    hidHostUrl.Value = prop.HostUrl;

                            //    string valuetobehashed = prop.Password + prop.MerchantID + prop.AcquiredId + Lit_RefNo.Text.Trim() + hidPurcAmount.Value.Trim() + prop.PurchaseCurrency;
                            //    //string valuetobehashed = prop.Password + prop.MerchantID + prop.AcquiredId + Lit_RefNo.Text.Trim();// +hidPurcAmount.Value.Trim() + prop.PurchaseCurrency;

                            //    hidSignature.Value = prop.getSignature(Lit_RefNo.Text.Trim(), hidPurcAmount.Value.Trim(), prop.PurchaseCurrency); //Hash.Hash.GetHash(valuetobehashed, Hash.Hash.HashType.SHA1);
                            //}
                        }
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

        //if (!Page.IsPostBack)
        //{
        //    string strReq = "";
        //    errorMsg = "";
        //    strReq = Request.RawUrl;
        //    string h = strReq.Substring(strReq.Length - 1);
        //    if (strReq.Contains("?"))
        //    {

        //        if (h == "#")
        //        {
        //            errorMsg = "No url";
        //            Server.Transfer("ErrorPage.aspx");
        //        }
        //        else
        //        {
        //            strReq = strReq.Substring(strReq.IndexOf('?') + 1);
        //            strReq = dc.Decrypt(strReq);
        //            if (strReq == "#")
        //            {
        //                errorMsg = "No Parameters Passed";
        //                Server.Transfer("ErrorPage.aspx");
        //            }
        //            else
        //            {

        //                Dictionary<string, string> paraList = new Dictionary<string, string>();
        //                paraList = dc.getParameters(strReq);



        //                if (paraList.ContainsKey("Ref_No") && paraList.ContainsKey("Type"))
        //                {
        //                    Lit_RefNo.Text = paraList["Ref_No"];

        //                    string busi_type = paraList["Type"];
        //                    if (busi_type.Trim().Equals("N"))
        //                    {
        //                        //No new business for now
        //                    }
        //                    else if (busi_type.Trim().Equals("R"))
        //                    {
        //                        LifePayment pr = new LifePayment(Lit_RefNo.Text.Trim());

        //                        Lit_PolNo.Text = pr.polNum;
        //                        Lit_CusName.Text = pr.custName;
        //                        //Lit_PolType.Text = pr.polTypName;
        //                        //Lit_comDate.Text = pr.startDate;
        //                        //Lit_SumAssured.Text = pr.sumAssurd.ToString("N2");
        //                        if (pr.loanNo != "")
        //                        {
        //                            Lit_PolNo.Text = Lit_PolNo.Text + " (Payment for Loan No: " + pr.loanNo + ")";
        //                            Lit_PayType.Text = "Loan Payment";
        //                        }
        //                        else
        //                        {
        //                            Lit_PayType.Text = "Premium Payment";
        //                        }
        //                        Lit_Premium.Text = pr.amount.ToString("N2");

        //                        Properties prop = new Properties("Life");


        //                        hidVersion.Value = prop.Version;
        //                        hidMerchant.Value = prop.MerchantID;
        //                        hidAcquiredId.Value = prop.AcquiredId;
        //                        hidMerRespUrl.Value = prop.MerRespUrl;
        //                        hidPurcAmount.Value = setNumber(pr.amount);

        //                        // Must be removed when go live
        //                        //hidPurcAmount.Value = setNumber(1.00);

        //                        hidPurchaseCurrency.Value = prop.PurchaseCurrency;
        //                        hidPurchaseCurrencyExponent.Value = prop.PurchaseCurrencyExponent;
        //                        hidGOrderId.Value = Lit_RefNo.Text.Trim();
        //                        hidSignatureMethod.Value = prop.SignatureMethod;

        //                        hidCaptureFlag.Value = prop.CaptureFlag;
        //                        hidHostUrl.Value = prop.HostUrl;

        //                        string valuetobehashed = prop.Password + prop.MerchantID + prop.AcquiredId + Lit_RefNo.Text.Trim() + hidPurcAmount.Value.Trim() + prop.PurchaseCurrency;
        //                        //string valuetobehashed = prop.Password + prop.MerchantID + prop.AcquiredId + Lit_RefNo.Text.Trim();// +hidPurcAmount.Value.Trim() + prop.PurchaseCurrency;

        //                        hidSignature.Value = prop.getSignature(Lit_RefNo.Text.Trim(), hidPurcAmount.Value.Trim(), prop.PurchaseCurrency); //Hash.Hash.GetHash(valuetobehashed, Hash.Hash.HashType.SHA1);
        //                    }
        //                    /******************Updated**************************/
        //                    else if (busi_type.Trim().Equals("RV"))
        //                    {
        //                        LifePayment pr = new LifePayment(Lit_RefNo.Text.Trim());

        //                        Lit_PolNo.Text = pr.polNum;
        //                        Lit_CusName.Text = pr.custName;
        //                        //Lit_PolType.Text = pr.polTypName;
        //                        //Lit_comDate.Text = pr.startDate;
        //                        //Lit_SumAssured.Text = pr.sumAssurd.ToString("N2");
        //                        //if (pr.loanNo != "")
        //                        //{
        //                        //    Lit_PolNo.Text = Lit_PolNo.Text + " (Payment for Loan No: " + pr.loanNo + ")";
        //                        //    Lit_PayType.Text = "Loan Payment";
        //                        //}
        //                        //else
        //                        //{
        //                        Lit_PayType.Text = "Revival Payment";
        //                        //}
        //                        Lit_Premium.Text = pr.amount.ToString("N2");

        //                        Properties prop = new Properties("Life");

        //                        hidVersion.Value = prop.Version;
        //                        hidMerchant.Value = prop.MerchantID;
        //                        hidAcquiredId.Value = prop.AcquiredId;
        //                        hidMerRespUrl.Value = prop.MerRespUrl;
        //                        hidPurcAmount.Value = setNumber(pr.amount);

        //                        // Must be removed when go live
        //                        //hidPurcAmount.Value = setNumber(1.00);

        //                        hidPurchaseCurrency.Value = prop.PurchaseCurrency;
        //                        hidPurchaseCurrencyExponent.Value = prop.PurchaseCurrencyExponent;
        //                        hidGOrderId.Value = Lit_RefNo.Text.Trim();
        //                        hidSignatureMethod.Value = prop.SignatureMethod;

        //                        hidCaptureFlag.Value = prop.CaptureFlag;
        //                        hidHostUrl.Value = prop.HostUrl;

        //                        string valuetobehashed = prop.Password + prop.MerchantID + prop.AcquiredId + Lit_RefNo.Text.Trim() + hidPurcAmount.Value.Trim() + prop.PurchaseCurrency;
        //                        //string valuetobehashed = prop.Password + prop.MerchantID + prop.AcquiredId + Lit_RefNo.Text.Trim();// +hidPurcAmount.Value.Trim() + prop.PurchaseCurrency;

        //                        hidSignature.Value = prop.getSignature(Lit_RefNo.Text.Trim(), hidPurcAmount.Value.Trim(), prop.PurchaseCurrency); //Hash.Hash.GetHash(valuetobehashed, Hash.Hash.HashType.SHA1);
        //                    }
        //                    //else if (busi_type.Trim().Equals("L"))
        //                    //{
        //                    //    LifePayment pr = new LifePayment(Lit_RefNo.Text.Trim());

        //                    //    Lit_PolNo.Text = pr.polNum;
        //                    //    Lit_CusName.Text = pr.custName;
        //                    //    //Lit_PolType.Text = pr.polTypName;
        //                    //    //Lit_comDate.Text = pr.startDate;
        //                    //    //Lit_SumAssured.Text = pr.sumAssurd.ToString("N2");
        //                    //    //if (pr.loanNo != "")
        //                    //    //{
        //                    //    //    Lit_PolNo.Text = Lit_PolNo.Text + " (Payment for Loan No: " + pr.loanNo + ")";
        //                    //    //    Lit_PayType.Text = "Loan Payment";
        //                    //    //}
        //                    //    //else
        //                    //    //{
        //                    //    Lit_PayType.Text = "Loan Payment";
        //                    //    //}
        //                    //    Lit_Premium.Text = pr.amount.ToString("N2");

        //                    //    Properties prop = new Properties("Life");

        //                    //    hidVersion.Value = prop.Version;
        //                    //    hidMerchant.Value = prop.MerchantID;
        //                    //    hidAcquiredId.Value = prop.AcquiredId;
        //                    //    hidMerRespUrl.Value = prop.MerRespUrl;
        //                    //    hidPurcAmount.Value = setNumber(pr.amount);

        //                    //    // Must be removed when go live
        //                    //    //hidPurcAmount.Value = setNumber(1.00);

        //                    //    hidPurchaseCurrency.Value = prop.PurchaseCurrency;
        //                    //    hidPurchaseCurrencyExponent.Value = prop.PurchaseCurrencyExponent;
        //                    //    hidGOrderId.Value = Lit_RefNo.Text.Trim();
        //                    //    hidSignatureMethod.Value = prop.SignatureMethod;

        //                    //    hidCaptureFlag.Value = prop.CaptureFlag;
        //                    //    hidHostUrl.Value = prop.HostUrl;

        //                    //    string valuetobehashed = prop.Password + prop.MerchantID + prop.AcquiredId + Lit_RefNo.Text.Trim() + hidPurcAmount.Value.Trim() + prop.PurchaseCurrency;
        //                    //    //string valuetobehashed = prop.Password + prop.MerchantID + prop.AcquiredId + Lit_RefNo.Text.Trim();// +hidPurcAmount.Value.Trim() + prop.PurchaseCurrency;

        //                    //    hidSignature.Value = prop.getSignature(Lit_RefNo.Text.Trim(), hidPurcAmount.Value.Trim(), prop.PurchaseCurrency); //Hash.Hash.GetHash(valuetobehashed, Hash.Hash.HashType.SHA1);
        //                    //}
        //                }
        //                else
        //                {
        //                    errorMsg = "No url";
        //                    Server.Transfer("ErrorPage.aspx");
        //                }

        //            }
        //        }
        //    }
        //    else
        //    {

        //    }
        //}
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

        bool result_1 = false;

        Payment_Type update_PayMethod_obj_in_Prop_Details = new Payment_Type();
        result_1 = update_PayMethod_obj_in_Prop_Details.update_pay_method_in_renewal(Lit_RefNo.Text.Trim(), "HNBGTW");

        if (result_1)
        {
            if (Math.Round(Double.Parse(hidPurcAmount.Value.Trim()), 2) > 0)
            {
                //html = " <HTML><body><form id='FrmHtmlCheckout' name='FrmHtmlCheckout' action='' method='post'>" +
                //        " <input id='Version' type='hidden' name='Version' value='" + hidVersion.Value.Trim() + "'>" +
                //        " <input id='MerID' type='hidden' value='" + hidMerchant.Value.Trim() + "' name='MerID' > " +
                //        " <input id='AcqID' type='hidden' value='" + hidAcquiredId.Value.Trim() + "' name='AcqID' >" +
                //        " <input id='MerRespURL' type='hidden' value='" + hidMerRespUrl.Value.Trim() + "' name='MerRespURL'>" +
                //        " <input id='PurchaseAmt' type='hidden' value='" + hidPurcAmount.Value.Trim() + "' name='PurchaseAmt'>" +
                //        //" <input id='PurchaseAmt' type='hidden' value='1.00' name='PurchaseAmt'>" +    // amount is hard coded SHOULD BE REMOVED WHEN APPLYING TO LIVE
                //        " <input id='PurchaseCurrency' type='hidden' value='" + hidPurchaseCurrency.Value.Trim() + "' name='PurchaseCurrency'>" +
                //        " <input id='PurchaseCurrencyExponent' type='hidden' value='" + hidPurchaseCurrencyExponent.Value.Trim() + "' name='PurchaseCurrencyExponent'>" +
                //        " <input id='OrderID' type='hidden' value='" + hidGOrderId.Value.Trim() + "'name='OrderID' >" +
                //        " <input id='SignatureMethod' type='hidden' value='" + hidSignatureMethod.Value.Trim() + "' name='SignatureMethod'>" +
                //        " <input id='Signature' type='hidden' value='" + hidSignature.Value.Trim() + "'name='Signature'>" +
                //        " <input id='CaptureFlag' type='hidden' value='" + hidCaptureFlag.Value.Trim() + "' name='CaptureFlag' >" +



                //        " </form>" +
                //        " <script language='javascript'> CheckOut(); " +
                //        " function CheckOut() {" +
                //        " document.FrmHtmlCheckout.action = '" + hidHostUrl.Value.Trim() + "'; " +
                //        " document.FrmHtmlCheckout.submit(); " +
                //        " }</script></body></HTML>";
                ////log lg = new log();

                ////lg.write_log(html);
                //Response.Write(html);

                Properties accProp = new Properties("Life");

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
                    // add1 = cusPrf.O_addrss1;

                    Regex reg = new Regex("[*'\",_&#^@]");
                    add1 = reg.Replace(cusPrf.O_addrss1, string.Empty);
                }
                else
                {
                    add1 = "NA";
                }

                if (!String.IsNullOrEmpty(cusPrf.O_country))
                {
                    //addState = ReplaceSpace(cusPrf.O_addrss4);
                    //Regex reg = new Regex("[*'\",_&#^@]");
                    //addState = reg.Replace(cusPrf.O_addrss4, string.Empty);

                    addState = cusPrf.O_country;
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

                html = " <HTML><body><form id='FrmHtmlCheckout' name='FrmHtmlCheckout' action='' method='post'>" +
                " <input id='access_key' type='hidden' name='access_key' value='" + accProp.AccessKey + "'>" +
                " <input id='profile_id' type='hidden' name='profile_id' value='" + accProp.ProfileId + "'>" +
                //" <input id='transaction_uuid' type='hidden' name='transaction_uuid' value='" + Lit_RefNo.Text.Trim() + "'>" +
                " <input id='transaction_uuid' type='hidden' name='transaction_uuid' value='" + System.Guid.NewGuid().ToString() + "'>" +
                " <input id='signed_field_names' type='hidden' value='access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency,bill_to_forename,bill_to_surname,bill_to_email,bill_to_address_line1,bill_to_address_state,bill_to_address_country,bill_to_address_city' name='signed_field_names' > " +
                " <input id='unsigned_field_names' type='hidden' name='unsigned_field_names' >" +
                " <input id='signed_date_time' type='hidden' value='" + DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'") + "' name='signed_date_time'>" +
                " <input id='locale' type='hidden' value='" + accProp.Locale + "' name='locale'>" +
                " <input id='transaction_type' type='hidden' value='" + accProp.TransactionType + "' name='transaction_type'>" +
                " <input id='reference_number' type='hidden' value='" + Lit_RefNo.Text.Trim() + "' name='reference_number'>" +
                " <input id='amount' type='hidden' value='" + Convert.ToString(Convert.ToDouble(Lit_Premium.Text.Trim())) + "' name='amount' >" +
                " <input id='currency' type='hidden' value='" + accProp.Currency + "' name='currency'>" +
                " <input id='bill_to_forename' type='hidden' value='" + fName + "' name='bill_to_forename'>" +
                " <input id='bill_to_surname' type='hidden' value='" + lName + "' name='bill_to_surname'>" +
                " <input id='bill_to_email' type='hidden' value='" + email + "' name='bill_to_email'>" +

                //" <input id='bill_to_email' type='hidden' value='tharuprabha03@gmail.com' name='bill_to_email'>" +
                " <input id='bill_to_address_line1' type='hidden' value='" + add1 + "' name='bill_to_address_line1'>" +
                " <input id='bill_to_address_state' type='hidden' value='" + addState + "' name='bill_to_address_state'>" +
                " <input id='bill_to_address_country' type='hidden' value='" + cntry + "' name='bill_to_address_country'>" +

                " <input id='bill_to_address_city' type='hidden' value='" + cityTown + "' name='bill_to_address_city' >" + " </form>" +
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
    protected void btn_PayWithBOC_Click(object sender, EventArgs e)
    {
        BOC_Payment emailAddress_Obj = new BOC_Payment();
        hdf_Cust_Email_Address.Value = emailAddress_Obj.getLoginEmail(Page.User.Identity.Name);

        double pay_amount = Double.Parse(Lit_Premium.Text.Trim());
        double amnt = Math.Round(pay_amount, 2);
        string paymentAmount = amnt.ToString();

        /*update PAY_METHOD to 'BOCGTW' in renewal_details*/
        BOC_Payment update_PayMethod_obj = new BOC_Payment();
        update_PayMethod_obj.update_pay_method_in_renewal(Lit_RefNo.Text.Trim(), "BOCGTW");



        if (amnt > 0)
        {

            EncryptDecrypt dc = new EncryptDecrypt();
            Dictionary<string, string> qs = new Dictionary<string, string>();
            qs.Add("Ref_No", Lit_RefNo.Text.Trim());
            qs.Add("Pol_No", Lit_PolNo.Text);
            qs.Add("Amount", paymentAmount);
            qs.Add("Cust_Email", hdf_Cust_Email_Address.Value);


            Response.Redirect(dc.url_encrypt("/Life/Authorized/Products/BOC_PaymentForm.aspx", qs));
            //Response.Redirect("~/Life/Authorized/Products/BOC_Payment_Form.aspx");
        }
        else
        {
            errorMsg = "Amount should be more than 0";
            Server.Transfer("ErrorPage.aspx");

        }
    }


    protected void btn_Amex_Click(object sender, EventArgs e)
    {
        bool result_1 = false;
        
        //Response.Write(html);

        Payment_Type update_PayMethod_obj_in_Prop_Details = new Payment_Type();
        result_1 = update_PayMethod_obj_in_Prop_Details.update_pay_method_in_renewal(Lit_RefNo.Text.Trim(), "AMEXGTW");

        log lg1 = new log();

        lg1.write_log("Amex pay button clicked");

        if (result_1)
        {
            string html = null;
            string hash = null;

            if (Math.Round(Double.Parse(hidPurcAmount.Value.Trim()), 2) > 0)
            {
                Properties accAmexProp = new Properties("Life", "Amex");

                CustProfile cusPrf = new CustProfile(Page.User.Identity.Name);

                html = " <HTML><body><form id='FrmHtmlCheckout' name='FrmHtmlCheckout' action='' method='post'>" +
                     " <input id='mer_id' type='hidden' name='mer_id' value='" + accAmexProp.MerchantID + "'>" +
                     " <input id='mer_txn_id' type='hidden' name='mer_txn_id' value='" + Lit_RefNo.Text.Trim() + "'>" +
                     " <input id='action' type='hidden' name='action' value='" + accAmexProp.TransactionType + "'>" +
                     " <input id='txn_amt' type='hidden' name='txn_amt' value='1.00'>" +
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

    protected void SelectedIndexChanged(object sender, EventArgs e)
    {
        var xx = rdbPayMethod.SelectedValue;
    }

    //protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlCardType.SelectedValue == "0")
    //    {
    //        lblCardTyp.Visible = true;
    //    }

    //    if (ddlCardType.SelectedValue == "1")
    //    {
    //        lblCardTyp.Visible = false;
    //    }
    //    else if (ddlCardType.SelectedValue == "2")
    //    {
    //        lblCardTyp.Visible = false;
    //    }
    //}
}