using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class General_Authorized_Products_PayReceipt : System.Web.UI.Page
{
  
    string retCode = "";
    string resCode = "";
    string refNo = "";
    string signature = "";
    string decision = "";
    protected bool motorDept = false;
    protected bool globTrotter = false;
    protected bool amppolicy = false;
    protected bool travelprot = false;
    string x;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Form.Count > 0)
            {             
                IDictionary<string, string> param_gen = new Dictionary<string, string>();

                foreach (var key in Request.Form.AllKeys)
                {
                    param_gen.Add(key, Request.Params[key]);
                }

                log lgg = new log();
                lgg.write_log("out");
                //lgg.write_log(Request.Form["ResponseCode"].ToString().Trim());
                lgg.write_log(Request.Form["reason_code"].ToString().Trim());
                lgg.write_log(Request.Form["decision"].ToString().Trim());

                if (Request.Form["signature"] != null)
                {

                    lgg.write_log(Request.Form["signature"]);

                    if (Request.Form["reason_code"] != null && Request.Form["req_reference_number"] != null && Request.Form["decision"] != null)
                    {

                        //retCode = Request.Form["ResponseCode"].ToString().Trim();
                        resCode = Request.Form["reason_code"].ToString().Trim();
                        refNo = Request.Form["req_reference_number"].ToString().Trim();
                        signature = Request.Form["signature"].ToString().Trim();
                        decision = Request.Form["decision"].ToString().Trim();
                        retCode = resCode;

                        ////retCode = "1";
                        ////resCode = "1";
                        ////refNo = "M/M11/R/2017/00035";
                        ////signature = "";

                        ////log lgg = new log();
                        //lgg.write_log(resCode);
                        //lgg.write_log(retCode);
                        //lgg.write_log("Hash Generated:");
                        //lgg.write_log(Hash.Hash.GetHash("JCk7d4S6" + "10353900" + "415738" + refNo + retCode, Hash.Hash.HashType.SHA1));
                        //lgg.write_log("Signature Returned:");
                        //lgg.write_log(signature);

                        lgg.write_log("Signature Generated:");
                        lgg.write_log(Security.sign(param_gen, "G"));

                        Properties properties = new Properties();

                        if (signature.Equals(Security.sign(param_gen, "G")))
                        {
                            if (refNo.Contains("/999/"))
                            {
                                if (refNo.Contains("TPI") || (refNo.Contains("TPM")))
                                {
                                    TRV_PolMast trvpolMast = new TRV_PolMast(refNo);
                                    if (trvpolMast.status == "P")
                                    {
                                        if (retCode == "100" && resCode == "100" && decision.ToUpper().Equals("ACCEPT"))
                                        {
                                            string newRetCode = "1";
                                            string newResCode = "1";

                                            if (trvpolMast.update_paid_proposal(refNo, "A", newRetCode, newResCode, trvpolMast.policy_no, User.Identity.Name))
                                            {
                                                trvpolMast = new TRV_PolMast(refNo);
                                                if (trvpolMast.status == "A")
                                                {
                                                    if (trvpolMast.policy_no.Contains("GTP"))
                                                    {
                                                        travelprot = true;
                                                        hyprPolSch.NavigateUrl = "/General/Authorized/Products/Documents/TRV_PB.pdf";
                                                        hyprPolSch.Text = "Download Policy Booklet";
                                                        btnPolDoc.Text = "Download Policy Schedule";

                                                    }
                                                    TRV_PolMast trv_polmast2 = new TRV_PolMast(trvpolMast.policy_no, refNo);
                                                    litPolNumber.Text = trvpolMast.policy_no;
                                                    litPropNo.Text = trvpolMast.refNo;
                                                    litAmount.Text = litPremium.Text = (trv_polmast2.netprmrs + trv_polmast2.adminFee + trv_polmast2.polFee + trv_polmast2.VAT + trv_polmast2.NBT).ToString("N2");
                                                    litPolType.Text = litPolType2.Text = trvpolMast.product_Name + " (Plan - " + trvpolMast.plan + ")";
                                                    litSumAssurd.Text = trvpolMast.sumAssured.ToString("N2");
                                                    litCustName.Text = trvpolMast.fullName;
                                                    litAddress.Text = trvpolMast.getFullAddress();
                                                    litCovPeriod.Text = trvpolMast.getCoverPeriod();
                                                    litPayDate.Text = trvpolMast.entryDate;
                                                    Panel1.Visible = true;


                                                    #region Generate Cleint ID for Travel Protect custoemrs who had completed the payment for the policy
                                                    //CustProfile cp = new CustProfile(User.Identity.Name);
                                                    //TRV_Client Client=new TRV_Client();
                                                    //Client.full_name = cp.O_firstName + " " + cp.O_lastName;
                                                    //Client.last_name = cp.O_lastName;
                                                    //Client.initials = cp.O_firstName;
                                                    //Client.status = cp.O_title;
                                                    //Client.passport_no = cp.O_passportNo;
                                                    //Client.nic_no = cp.O_nicNo;
                                                    //Client.dob = cp.O_dateOfBirth;
                                                    //Client.profession = cp.O_ocupation;
                                                    //Client.mobileno = cp.O_mobileNumber;
                                                    //Client.home_add1 = cp.O_addrss1;
                                                    //Client.home_add2 = cp.O_addrss2;
                                                    //Client.home_add3 = cp.O_addrss3;
                                                    //Client.home_add4 = cp.O_addrss4;
                                                    //Client.UserID = cp.O_username;                                                 

                                                    //bool clientgenerated = Client.GenerateClient(Client);
                                                    //lblPayStatus.Text = Client.error;


                                                    #endregion

                                                    //if (clientgenerated)
                                                    //{

                                                    bool tpi_ret = trvpolMast.send_tpi_pay_receipt_email();
                                                    if (tpi_ret)
                                                    {
                                                        lblPayStatus.Text = "Travel Protect Payment receipt details have been emailed to you.";
                                                        lblPayStatus.ForeColor = System.Drawing.Color.Green;
                                                    }
                                                    //}
                                                    //else
                                                    //{
                                                    //    //lblPayStatus.Text = "Travel Protect client error.";
                                                    //    lblPayStatus.ForeColor = System.Drawing.Color.Green;
                                                    //}
                                                }
                                                else
                                                {
                                                    lblPayStatus.Text = "Sorry, Travel Protect Payment was not successful";
                                                    log logger = new log();
                                                    logger.write_log("Failed at PayReceipt-Pageload: Proposal success status not updated");
                                                }
                                            }
                                            else
                                            {
                                                lblPayStatus.Text = "Travel Protect Internal Error. Please contact SLIC " + trvpolMast.emesg;
                                                log logger = new log();
                                                logger.write_log("Failed at PayReceipt-Pageload: Proposal success pay not updated");
                                            }
                                        }
                                        else
                                        {
                                            log logger = new log();

                                            logger.write_log(getReasonCodeContent(retCode));
                                            lblPayStatus.Text = "Sorry, Payment was not successful";
                                           
                                            logger.write_log("Failed at PayReceipt-Pageload: Proposal success status not updated");
                                        }
                                    }
                                    else
                                    {
                                        lblPayStatus.Text = "Internal Error in Travel Protect. Please contact SLIC";
                                        log logger = new log();
                                        logger.write_log("Failed at TPI PayReceipt-Pageload: Proposal Status Invalid");
                                    }

                                }

                                /************************************************************************************************************************************/
                                /*R.M Sanjeewa Kumara Ranaweera*/
                                /*2023/09/11*/
                                /************************************************************************************************************************************/
                                else if (refNo.Contains("/HIP/"))
                                {
                                    if (retCode == "100" && resCode == "100" && decision.ToUpper().Equals("ACCEPT"))
                                    {
                                        HPL_Transactions hpl_Transaction = new HPL_Transactions();
                                        HPL_SQL hplSQL = new HPL_SQL();

                                        bool SucessTrans = false;

                                        try
                                        {
                                            SucessTrans = hpl_Transaction.PurchaseProductUpdate(refNo, retCode);

                                            if (hpl_Transaction.Trans_Sucess_State == true)
                                            {
                                                if (hpl_Transaction.Trans_Sucess_State == true && SucessTrans == true)
                                                {
                                                    this.Load_HPL_PaymentConfirmInfo(refNo);
                                                    //string newRetCode = "1";
                                                    //string newResCode = "1";
                                                    //pnl_HPLReciept.Visible = true;
                                                }
                                                else
                                                {
                                                    string msg_heading = "Error: Failure Transaction";
                                                    string message = "Payment Process Can't be Complete. Please Contact SLIC.";
                                                    Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
                                                }
                                            }
                                            else
                                            {
                                                string msg_heading = "Transaction Error: In Transaction.";
                                                string message = "SQL Transaction Error.Please Contact SLIC.";
                                                Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            string msg_heading = "Error: In Home Protect Lite insurance product purchasing. Contact SLIC";
                                            string message = ex.Message;
                                            Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
                                        }
                                    }
                                }
                                /************************************************************************************************************************************/
                                /************************************************************************************************************************************/
                                
                                else
                                {
                                    Proposal prop = new Proposal(refNo);

                                    if (prop.status == "P")
                                    {
                                        //if (retCode == "1" && resCode == "1")
                                        if (retCode == "100" && resCode == "100" && decision.ToUpper().Equals("ACCEPT"))
                                        {
                                            string newRetCode = "1";
                                            string newResCode = "1";

                                            if (prop.update_paid_proposal(refNo, "A", newRetCode, newResCode))
                                            {
                                                prop = new Proposal(refNo);

                                                if (prop.status == "A")
                                                {
                                                    if (refNo.Contains("G/999/GTI/"))
                                                    {
                                                        globTrotter = true;
                                                        hyprPolSch.NavigateUrl = "/General/Authorized/Products/Documents/GTI.pdf";
                                                        hyprPolSch.Text = "Download Policy Booklet";
                                                        btnPolDoc.Text = "Download Policy Schedule";

                                                    }
                                                    else if (refNo.Contains("G/999/MP/"))
                                                    {
                                                        amppolicy = true;
                                                        hyprPolSch.NavigateUrl = "/General/Authorized/Products/Documents/AMP_Policy_book.pdf";
                                                        hyprPolSch.Text = "Download Policy Booklet";
                                                        btnPolDoc.Text = "Download Policy Schedule";
                                                    }

                                                    litPolNumber.Text = prop.policy_no;
                                                    litPropNo.Text = prop.refNo;
                                                    litAmount.Text = litPremium.Text = prop.annualPremium.ToString("N2");
                                                    litPolType.Text = litPolType2.Text = prop.product_Name + " (Plan - " + prop.plan + ")";
                                                    litSumAssurd.Text = prop.sumAssured.ToString("N2");
                                                    litCustName.Text = prop.fullName;
                                                    litAddress.Text = prop.getFullAddress();
                                                    litCovPeriod.Text = prop.getCoverPeriod();
                                                    litPayDate.Text = prop.entryDate;
                                                    Panel1.Visible = true;

                                                    bool ret = prop.send_pay_receipt_email();
                                                    if (ret)
                                                    {
                                                        lblPayStatus.Text = "Payment receipt details have been emailed to you.";
                                                        lblPayStatus.ForeColor = System.Drawing.Color.Green;
                                                    }
                                                }
                                                else
                                                {
                                                    lblPayStatus.Text = "Sorry, Payment was not successful";
                                                    log logger = new log();
                                                    logger.write_log("Failed at PayReceipt-Pageload: Proposal success status not updated");
                                                }
                                            }
                                            else
                                            {
                                                lblPayStatus.Text = "Internal Error. Please contact SLIC1" + refNo + "*" + retCode + "*" + resCode;
                                                log logger = new log();
                                                logger.write_log("Failed at PayReceipt-Pageload: Proposal success pay not updated");
                                            }
                                        }
                                        else
                                        {
                                            if (prop.update_paid_proposal(refNo, "F", retCode, resCode))
                                            {
                                                log logger = new log();
                                                logger.write_log(getReasonCodeContent(retCode));
                                                lblPayStatus.Text = "Sorry, Payment was not successful";
                                                lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                            }
                                            else
                                            {
                                                lblPayStatus.Text = "Internal Error. Please contact SLIC2";
                                                log logger = new log();
                                                logger.write_log(getReasonCodeContent(retCode));
                                                logger.write_log("Failed at PayReceipt-Pageload: Proposal pay failure not updated");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lblPayStatus.Text = "Internal Error. Please contact SLIC3";
                                        log logger = new log();
                                        logger.write_log("Failed at PayReceipt-Pageload: Proposal Status Invalid");
                                    }
                                }

                            }

                            else if (refNo.Contains("/R/") || refNo.Contains("/D/"))
                            {
                                Renewal renw = new Renewal(refNo);

                                if (renw.status == "P")
                                {
                                //if (retCode == "1" && resCode == "1")
                                    if (retCode == "100" && resCode == "100" && decision.ToUpper().Equals("ACCEPT"))
                                    {
                                        string newRetCode = "1";
                                        string newResCode = "1";

                                        if (renw.update_paid_renewal(refNo, "A", newRetCode, newResCode))
                                        {
                                            renw = new Renewal(refNo);

                                            if (renw.status == "A")
                                            {
                                                if (renw.dept == "M")
                                                {
                                                    bool retVal = renw.updateRevLicenDetails();
                                                    if (retVal == false)
                                                    {
                                                        log logger = new log();
                                                        logger.write_log("Failed at PayReceipt-Pageload: Revenue License details not updated");
                                                    }

                                                    Button2.Visible = true;

                                                    DateTime today = DateTime.Now;

                                                    CustProfile cp = new CustProfile(Page.User.Identity.Name);
                                                    string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];


                                                    Covernote_Info ci = new Covernote_Info(renw.polNum.Trim());

                                                    DateTime pol_end_date = Convert.ToDateTime(ci.policy_end_date);
                                                    DateTime effective_date = new DateTime();
                                                    string com_time = "";

                                                    if (pol_end_date > today)
                                                    {
                                                        effective_date = pol_end_date;
                                                        com_time = "00:00";
                                                    }
                                                    else
                                                    {
                                                        effective_date = today;
                                                        com_time = DateTime.Now.ToString("HH:mm");
                                                    }
                                                    DateTime expire_date = effective_date.AddDays(15);

                                                    string pendingReason = "Pending Certificate#Luxury/Semi Luxury Tax if Applicable.";

                                                    Covernote cn = new Covernote();
                                                    HdnCoNum.Value = cn.insertCN(999, effective_date.ToString("yyyy-MM-dd"), 15, expire_date.ToString("yyyy-MM-dd"), ci.Cus_name, cp.O_title, "", renw.receiptNo, Page.User.Identity.Name, ip, "", "", "M", "", "", 0, "N", ci.address1, ci.address2, ci.address3, ci.address4, "R", " ", " ", renw.polNum, "", com_time, renw.vehiNum, "", ci.cylinder_capacity, "NA", "Comprehensive Policy", "NA", "", ci.SA, ci.rcc_tag, ci.tc_tag, "Comprehensive", ci.province, pendingReason, renw.amount, DateTime.Today.ToString("yyyy-MM-dd"), "N", cp.O_mobileNumber);

                                                    if (HdnCoNum.Value.Length >= 11)
                                                    {
                                                        Button2.Visible = true;
                                                        cn.cover_note_email(HdnCoNum.Value, Page.User.Identity.Name, ip, false, cp.O_email);
                                                    }

                                                    litRnCovPeriod.Text = "From: " + renw.startDate + " to: " + renw.endDate;
                                                }
                                                else
                                                {
                                                    litRnCovPeriod.Text = "Starts from: " + renw.startDate;
                                                }
                                                litRefNo.Text = renw.receiptNo;
                                                litAmount2.Text = litRnPremium.Text = renw.amount.ToString("N2");
                                                litPolTyp2.Text = renw.polTypName;
                                                litRnPolNum.Text = renw.polNum;
                                                litRnSumAssurd.Text = renw.sumAssurd.ToString("N2");
                                                litRnCusNam.Text = renw.custName;
                                                litAddress2.Text = renw.address;
                                              //  litRnCovPeriod.Text = "From: " + renw.startDate + " to: " + renw.endDate;
                                                litPayDate2.Text = renw.entryDate;

                                                if (renw.dept == "M")
                                                {
                                                    litVehiNum.Text = renw.vehiNum;
                                                    motorDept = true;
                                                }
                                                if (DateTime.Now <= DateTime.ParseExact(renw.startDate, "yyyy/MM/dd", CultureInfo.InvariantCulture))
                                                {
                                                    lblNoClmMesg.Visible = true;
                                                }
                                                Panel2.Visible = true;

                                                bool ret = renw.send_pay_receipt_email();
                                                if (ret)
                                                {
                                                    lblPayStatus.Text = "Payment receipt details have been emailed to you.";
                                                    lblPayStatus.ForeColor = System.Drawing.Color.Green;
                                                }
                                            }
                                            else
                                            {
                                                lblPayStatus.Text = "Sorry, Payment was not successful";
                                                log logger = new log();
                                                logger.write_log("Failed at PayReceipt-Pageload: Renewal success status not updated");
                                            }
                                        }
                                        else
                                        {
                                            lblPayStatus.Text = "Internal Error. Please contact SLIC4";
                                            log logger = new log();
                                            logger.write_log("Failed at PayReceipt-Pageload: Renewal success pay not updated");
                                        }
                                    }
                                    else
                                    {
                                        if (renw.update_paid_renewal(refNo, "F", retCode, resCode))
                                        {
                                            log logger = new log();
                                            logger.write_log(getReasonCodeContent(retCode));
                                            lblPayStatus.Text = "Sorry, Payment was not successful";
                                            lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                        }
                                        else
                                        {
                                            lblPayStatus.Text = "Internal Error. Please contact SLIC5";
                                            log logger = new log();
                                            logger.write_log(getReasonCodeContent(retCode));
                                            logger.write_log("Failed at PayReceipt-Pageload: Renewal pay failure not updated");
                                        }
                                    }
                                }
                                else
                                {
                                    lblPayStatus.Text = "Internal Error. Please contact SLIC6";
                                    log logger = new log();
                                    logger.write_log("Failed at PayReceipt-Pageload: Renewal Status Invalid");
                                }
                            }
                        }
                        else
                        {
                            lblPayStatus.Text = "Internal Error. Please contact SLIC";
                            log logger = new log();
                            logger.write_log("Failed at PayReceipt-Pageload: Signature does not match");
                        }
                    }
                    else
                    {
                        lblPayStatus.Text = "Internal Error. Please contact SLIC7";
                        log logger = new log();
                        logger.write_log("Failed at PayReceipt-Pageload: Parameter is null");
                    }
                }
                else
                {
                    if (Request.Form["reason_code"] != null && Request.Form["req_reference_number"] != null && Request.Form["decision"] != null)
                    {
                        decision = Request.Form["decision"].ToString().Trim();
                        resCode = Request.Form["reason_code"].ToString().Trim();
                        refNo = Request.Form["req_reference_number"].ToString().Trim();
                        retCode = resCode;

                        log logger = new log();
                        try
                        {
                            if (refNo.Contains("/999/"))
                            {
                                Proposal prop = new Proposal(refNo);

                                if (prop.status == "P")
                                {
                                    if (prop.update_paid_proposal(refNo, "K", retCode, resCode))
                                    {
                                        logger.write_log(getReasonCodeContent(retCode));
                                        lblPayStatus.Text = "Sorry, Payment was not successful";
                                        lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {

                                        logger.write_log(getReasonCodeContent(retCode));
                                        lblPayStatus.Text = "Internal Error. Please contact SLIC8";
                                        logger.write_log("Failed at PayReceipt-Pageload: Proposal pay failure not updated");
                                    }
                                }
                            }
                            else if (refNo.Contains("/R/"))
                            {
                                Renewal renw = new Renewal(refNo);

                                if (renw.status == "P")
                                {
                                    if (renw.update_paid_renewal(refNo, "K", retCode, resCode))
                                    {
                                        logger.write_log(getReasonCodeContent(retCode));
                                        lblPayStatus.Text = "Sorry, Payment was not successful";
                                        lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        logger.write_log(getReasonCodeContent(retCode));
                                        lblPayStatus.Text = "Internal Error. Please contact SLIC9";
                                        logger.write_log("Failed at PayReceipt-Pageload: Renewal pay failure not updated");
                                    }
                                }
                            }
                            else
                            {
                                logger.write_log("Failed at PayReceipt-Pageload: invalid ref. no returned");
                            }
                        }
                        catch
                        {
                            logger.write_log(getReasonCodeContent(retCode));
                            logger.write_log("Failed at PayReceipt-Pageload: Signature Parameter is null");
                        }
                        lblPayStatus.Text = "Error occured while processing payment. Please contact SLIC";

                        //Response.Redirect("./Default.aspx", false);
                    }
                }
            }
            else
            {
                lblPayStatus.Text = "Internal Error. Please contact SLIC10";
                log logger = new log();
                logger.write_log("Failed at PayReceipt-Pageload: No parameteres found");
            }

        }
    }


    protected void btnReceipt_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        Proposal pro = new Proposal(litPropNo.Text.Trim());
        Print_pdf pdf = new Print_pdf();
        pdf.print_receipt(pro, ip, User.Identity.Name);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        Renewal rnwl = new Renewal(litRefNo.Text.Trim());
        Print_pdf pdf = new Print_pdf();
        pdf.print_receipt(rnwl, ip, User.Identity.Name);

    }


    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        if (HdnCoNum.Value.Length >= 11)
        {
            EncryptDecrypt en = new EncryptDecrypt();
            Dictionary<string, string> dc = new Dictionary<string, string>();

            dc.Add("CrefNo", HdnCoNum.Value);
            string link = en.url_encrypt("/General/Authorized/Covernote_print.aspx", dc);
            Response.Redirect(link);
        }
    }
    protected void btnPolDoc_Click(object sender, EventArgs e)
    {
        EncryptDecrypt en = new EncryptDecrypt();

        if (litPropNo.Text.Contains("G/999/GTI/"))
        {
            btnPolDoc.Text = "Download Policy Document";

            Dictionary<string, string> dc = new Dictionary<string, string>();
            dc.Add("refN0", litPropNo.Text.Trim());
            dc.Add("P0lNo", litPropNo.Text.Trim());
            string link2 = en.url_encrypt("/General/Authorized/Quotation_Reprint.aspx", dc);

            Response.Redirect(link2);


        }
        else if (litPropNo.Text.Contains("G/999/MP/"))
        {


            btnPolDoc.Text = "Download Proposal Document";

            Dictionary<string, string> dc = new Dictionary<string, string>();
            dc.Add("refN0", litPropNo.Text.Trim());
            dc.Add("SA", litSumAssurd.Text.Trim());
            string link2 = en.url_encrypt("/General/Authorized/Quotation_Reprint.aspx", dc);

            Response.Redirect(link2);

        }
        else if (litPolNumber.Text.Contains("GTP"))
        {
            lblPayStatus.Text = litPolNumber.Text;
            string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("polNo", litPolNumber.Text);
            dic.Add("refN0", litPropNo.Text.Trim());
            dic.Add("Enter_by", User.Identity.Name);
            dic.Add("ip", ip);
            dic.Add("BR", "999");
            dic.Add("repr_State", "false");

            TRV_Receipt receipt = new TRV_Receipt();
            receipt.polno = litPolNumber.Text;
            receipt.user = User.Identity.Name;
            receipt.printtag = 1;
            receipt.ip = ip;
            receipt.SCH_BRANCH = 999;
            lblPayStatus.Text = litPolNumber.Text;
            bool sucess = receipt.Insert_DataSet(receipt);

            if (sucess == true)
            {

                //Session["polNo"] = txt_polno.Text.ToString();
                //Response.Redirect("~/Print/QUOT_HEADING.aspx", false);
                string link2 = en.url_encrypt("/General/Authorized/Quotation_Reprint.aspx", dic);
                lblPayStatus.Text = litPolNumber.Text + "=" + sucess + link2;
                Response.Redirect(link2);
            }
            else
            {
                lblPayStatus.Text = "Internal Error in Travel Protect Schedule Print. Please contact SLIC";
            }

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (litPropNo.Text.Contains("G/999/GTI/"))
        {
            hyprPolSch.Text = "Download Policy Schedule";
            hyprPolSch.NavigateUrl = " ";
        }
        else if (litPropNo.Text.Contains("G/999/MP/"))
        {
            hyprPolSch.Text = "Download Policy Booklet";
            hyprPolSch.NavigateUrl = "/General/Authorized/Products/Documents/AMP_Policy_book.pdf";
        }
        else if (litPropNo.Text.Contains("G/999/TPI/"))
        {
            hyprPolSch.Text = "Download Policy Booklet1";
            hyprPolSch.NavigateUrl = "/General/Authorized/Products/Documents/Travel_Policy.pdf";
        }
    }

    private string getReasonCodeContent(string reasCode)
    {
        int reasonCode = int.Parse((string)reasCode);
        switch (reasonCode) {
            //// Success
            //case 100:
            //    return ("\nRequest ID: " + reply["requestID"]);
            // Missing field or fields
            case 101:
                return (" required fields are missing ");
            // Invalid field or fields
            case 102:
                return (" some fields are invalid ");
            // Insufficient funds
            case 204:
                return (" Insufficient funds in the account. Please use a different card.");
                // Add additional reason codes here that you must handle more
                
        default:

                return (reasonCode.ToString());
        }
    }

    //Code Developed by Sanjeewa
    //Target : Home Protect Lite
    //Date : 11/09/2023
    #region Home Protect Lite
    
    /*********************************************************************************************/
    //Developed By Sanjeewa

    protected void Load_HPL_PaymentConfirmInfo(string refNo)
    {
        HPL_Transactions hpl_Transaction = new HPL_Transactions();
        HPL_SQL hplSQL = new HPL_SQL();

        List<HPL_PaymentFynalization> cuspolInfo = hpl_Transaction.ReturnPurchasePolicyInfo(hplSQL.GetPaymentConfirmationInfo(refNo));

        if (hpl_Transaction.Trans_Sucess_State == true)
        {
            if (cuspolInfo.Count > 0)
            {
                if (!String.IsNullOrEmpty(cuspolInfo[0].HPLPolicy) && !String.IsNullOrEmpty(cuspolInfo[0].HPLRefNo) && !String.IsNullOrEmpty(cuspolInfo[0].HPLCus_Name) && !String.IsNullOrEmpty(cuspolInfo[0].HPLSumAssured) && 
                    !String.IsNullOrEmpty(cuspolInfo[0].HPLPremium) && !String.IsNullOrEmpty(cuspolInfo[0].HPLCoverPeriod) && !String.IsNullOrEmpty(cuspolInfo[0].HPLDateOfPayment))
                {
                    lit_title_payAmount.Text = double.Parse(cuspolInfo[0].HPLPremium).ToString("N2");
                    it_title_trfno.Text = cuspolInfo[0].HPLRefNo.Trim();

                    ltl_pay_POLNo.Text = cuspolInfo[0].HPLPolicy;
                    ltl_pay_RefNo.Text = cuspolInfo[0].HPLRefNo;
                    ltl_pay_Name.Text = cuspolInfo[0].HPLCus_Name;
                    ltl_pay_SumAsured.Text = double.Parse(cuspolInfo[0].HPLSumAssured).ToString("N2");
                    ltl_pay_Premium.Text = double.Parse(cuspolInfo[0].HPLPremium).ToString("N2");
                    ltl_pay_CoverPeriod.Text = cuspolInfo[0].HPLCoverPeriod;
                    ltl_pay_PaidDate.Text = cuspolInfo[0].HPLDateOfPayment;

                    pnl_HPLReciept.Visible = true;

                    if(!String.IsNullOrEmpty(cuspolInfo[0].HPLEmail))
                        this.SendMail(cuspolInfo[0].HPLEmail);
                }
                else
                {
                    /*When user profile is incomplete*/
                    string msg_heading = "Error: In-Complete Customer Payment Information.";
                    string message = "In-Complete Customer Payment Information. Please Contact SLIC.";
                    Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
                }
            }
            else
            {
                /*User profile not registed on e_document System*/
                string msg_heading = "Error: In Payment Profile";
                string message = "No record found to display in customer payment profile. contact SLIC";
                Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
            }
        }
        else
        {
            /*Any Oracle Transaction Error*/
            string msg_heading = "Error: In Transaction.";
            string message = "Transaction Error.Please Contact SLIC.";
            Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
        }                                             
    }


    protected void SendMail(string e_mail)
    {
        string body = string.Empty;
        try
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(Server.MapPath("~/HPL_Email/hpl_payment_Confirmation.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{SYSTEM_DATE}", DateTime.Now.ToString("dd:MM:yyyy HH:mm:ss tt"));
            body = body.Replace("{POLICYNO}", ltl_pay_POLNo.Text);
            body = body.Replace("{PREMIUM}", ltl_pay_Premium.Text);
            body = body.Replace("{REFERENCENO}", ltl_pay_RefNo.Text);
            
            body = body.Replace("{POHNAME}", ltl_pay_Name.Text);
            body = body.Replace("{SUMASSURED}", ltl_pay_SumAsured.Text);
            body = body.Replace("{PREMIUM}", ltl_pay_Premium.Text);
            body = body.Replace("{COVERPERIOD}", ltl_pay_CoverPeriod.Text);
            body = body.Replace("{PAIDDATE}", ltl_pay_PaidDate.Text);

            this.SendHtmlFormattedEmail(body, e_mail);

        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "message('Error Message', 'Message : Error In Editing Template. " + ex.Message + "', 'swal-modal-error','swal-button-error', 1, 1);", true);
        }
    }

    protected void SendHtmlFormattedEmail(string body, string cus_email)
    {
        Regex pattern = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        if (pattern.IsMatch(cus_email))
        {
            try
            {
                MailMessage message = new MailMessage();

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                message.AlternateViews.Add(htmlView);

                MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["smtpUser"]);
                message.From = fromAddress;
                message.Subject = "Electronic notice of policy payment - Home Protect Lite";
                message.To.Add(cus_email);
                //message.CC.Add("");
                message.Body = body;


                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;

                SmtpClient sendClient = new SmtpClient();
                sendClient.Host = ConfigurationManager.AppSettings["smtpServer"];
                sendClient.Port = int.Parse(ConfigurationManager.AppSettings["smtpPort"]);

                sendClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["smtpUser"];
                //NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                sendClient.UseDefaultCredentials = true;
                sendClient.Credentials = NetworkCred;
                message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                sendClient.Send(message);

                //LogEmail_Status log_tr = new LogEmail_Status();
                //log_tr.write_log(polno_ + " ::: " + cus_email + " ::: " + DateTime.Now.ToString("dd:MM:yyyy HH:mm:ss tt") + " ::: " + H_epf.Value.Trim(), "CMBC");

            }
            catch (Exception ex)
            {
               // ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "message('Error Message', 'Message : Error Sending E-Mail. " + ex.Message + "', 'swal-modal-error','swal-button-error', 1, 1);", true);
            }
        }
    }

    protected void btn_DownloadPaymentReceipt_Click(object sender, EventArgs e)
    {
        HPL_Transactions hpl_Transaction = new HPL_Transactions();
        HPL_SQL hplSQL = new HPL_SQL();

        List<HPL_PaymentFynalization> cuspolInfo = hpl_Transaction.ReturnPurchasePolicyInfo(hplSQL.GetPaymentConfirmationInfo(ltl_pay_RefNo.Text.Trim()));

        if (hpl_Transaction.Trans_Sucess_State == true)
        {
            if (cuspolInfo.Count > 0)
            {
                if (!String.IsNullOrEmpty(cuspolInfo[0].HPLPolicy) && !String.IsNullOrEmpty(cuspolInfo[0].HPLRefNo) && !String.IsNullOrEmpty(cuspolInfo[0].HPLCus_Name) && !String.IsNullOrEmpty(cuspolInfo[0].HPLSumAssured) &&
                    !String.IsNullOrEmpty(cuspolInfo[0].HPLPremium) && !String.IsNullOrEmpty(cuspolInfo[0].HPLCoverPeriod) && !String.IsNullOrEmpty(cuspolInfo[0].HPLDateOfPayment))
                {
                    this.PrintHPL_Receipt(cuspolInfo);
                }
                else
                {
                    /*When user profile is incomplete*/
                    string msg_heading = "Error: In Printing Payment Confirmation Receipt.";
                    string message = "Can't Print Payment Confirmation Receipt With Incomplete Information. Please Contact SLIC.";
                    Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
                }
            }
            else
            {
                /*User profile not registed on e_document System*/
                string msg_heading = "Error: In Printing Payment Confirmation Receipt";
                string message = "No record found. Can't Print Payment Confirmation Receipt";
                Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
            }
        }
        else
        {
            /*Any Oracle Transaction Error*/
            string msg_heading = "Error: In Transaction.";
            string message = "Transaction Error Payment Confirmation Receipt.Please Contact SLIC.";
            Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
        }                                             
    }

    protected void btn_PolicyShadule_Click(object sender, EventArgs e)
    {
        HPL_Transactions hpl_Transaction = new HPL_Transactions();
        HPL_SQL hplSQL = new HPL_SQL();

        List<HPL_PaymentFynalization> cuspolInfo = hpl_Transaction.ReturnPurchasePolicyInfo(hplSQL.GetPaymentConfirmationInfo(ltl_pay_RefNo.Text.Trim()));

        if (hpl_Transaction.Trans_Sucess_State == true)
        {
            if (cuspolInfo.Count > 0)
            {
                if (!String.IsNullOrEmpty(cuspolInfo[0].HPLPolicy) && !String.IsNullOrEmpty(cuspolInfo[0].HPLRefNo) && !String.IsNullOrEmpty(cuspolInfo[0].HPLCus_Name) && !String.IsNullOrEmpty(cuspolInfo[0].HPLSumAssured) &&
                    !String.IsNullOrEmpty(cuspolInfo[0].HPLPremium) && !String.IsNullOrEmpty(cuspolInfo[0].HPLCoverPeriod) && !String.IsNullOrEmpty(cuspolInfo[0].HPLDateOfPayment))
                {
                    this.PrintPolicy_PolicySchedule(cuspolInfo);
                }
                else
                {
                    /*When user profile is incomplete*/
                    string msg_heading = "Error: In Policy Schedule.";
                    string message = "Can't Print / Download Policy Schedule With Incomplete Information. Please Contact SLIC.";
                    Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
                }
            }
            else
            {
                /*User profile not registed on e_document System*/
                string msg_heading = "Error: In Policy Schedule";
                string message = "No record found. Can't Print Policy Schedule.";
                Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
            }
        }
        else
        {
            /*Any Oracle Transaction Error*/
            string msg_heading = "Error: In Transaction.";
            string message = "Transaction Error in Policy Schedule.Please Contact SLIC.";
            Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
        }                                            
    }

    protected void PrintHPL_Receipt(List<HPL_PaymentFynalization> printReceipt)
    {
        Document document = new Document(PageSize.A4, 30f, 30f, 30f, 30f);
        Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        {
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            Phrase phrase = null;
            PdfPCell cell = null;
            PdfPTable table = null;
            Color color = null;

            document.Open();

            #region Header
            //Header Table
            table = new PdfPTable(2);
            table.TotalWidth = 500f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 0.3f, 0.7f });

            //Company Logo
            cell = ImageCell("~/General/GenImages/slic_gen_Logo.png", 30f, PdfPCell.ALIGN_RIGHT);
            table.AddCell(cell);

            //Company Name and Address
            phrase = new Phrase();
            phrase.Add(new Chunk("Sri Lanka Insurance Corporation General (Ltd).\n\n", FontFactory.GetFont("Arial", 16, Font.BOLD, Color.DARK_GRAY)));
            phrase.Add(new Chunk("Rakshana Mandiraya,\n", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            phrase.Add(new Chunk("No 21,\n", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            phrase.Add(new Chunk("Vauxhall Street,\n", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            phrase.Add(new Chunk("Colombo 02", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 0);
            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            table.AddCell(cell);

            //Separater Line
            color = new Color(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
            DrawLine(writer, 25f, document.Top - 79f, document.PageSize.Width - 25f, document.Top - 79f, color);
            DrawLine(writer, 25f, document.Top - 80f, document.PageSize.Width - 25f, document.Top - 80f, color);
            document.Add(table);


            table = new PdfPTable(2);
            table.SetWidths(new float[] { 2f, 6f });
            table.TotalWidth = 540f;
            table.LockedWidth = true;
            table.SpacingBefore = 20f;
            table.HorizontalAlignment = Element.ALIGN_LEFT;

            cell = PhraseCell(new Phrase("First Loss Home Insurance Policy Payment Receipt", FontFactory.GetFont("Arial", 12, Font.UNDERLINE, Color.BLACK)), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            table.AddCell(cell);
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 15f;
            table.AddCell(cell);

            //Detail Headding
            cell = PhraseCell(new Phrase("Dear Customer,\n\nWe acknowledge your payment of (Rs.) " + double.Parse(printReceipt[0].HPLPremium.Trim()).ToString("N") + " in respect 0f above insurance under the reference no: " + printReceipt[0].HPLRefNo.Trim() + " has been received.\n\n", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0);
            cell.Colspan = 2;
            table.AddCell(cell);
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 15f;
            table.AddCell(cell);
            #endregion

            #region Customer Information
            //Customer Policy Details- Policy Number
            table.AddCell(PhraseCell(new Phrase("Policy Number ", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase(printReceipt[0].HPLPolicy.Trim(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //Customer Policy Details- Customer Reference Number
            table.AddCell(PhraseCell(new Phrase("Reference Number ", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase(printReceipt[0].HPLRefNo.Trim(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //Customer Policy Details- Policy Holder Name
            table.AddCell(PhraseCell(new Phrase("Name of the Customer ", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase(printReceipt[0].HPLCus_Name.Trim(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //Customer Policy Details- Premises
            table.AddCell(PhraseCell(new Phrase("Total Sum Assured (Rs.) ", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase(double.Parse(printReceipt[0].HPLSumAssured.Trim()).ToString("N2"), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //Customer Policy Details- Risk Location
            table.AddCell(PhraseCell(new Phrase("Premium (Rs.) ", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase(double.Parse(printReceipt[0].HPLPremium.Trim()).ToString("N2"), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);


            //Customer Policy Details-Period of Insurance
            table.AddCell(PhraseCell(new Phrase("Cover Period", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase(printReceipt[0].HPLCoverPeriod.Trim(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);


            //--
            table.AddCell(PhraseCell(new Phrase("Date of Payment ", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase(System.DateTime.Now.ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);
            document.Add(table);

            // Create Table 1---------------------------------------------------
            PdfPTable table1 = null;
            table1 = new PdfPTable(1);
            table1.SetWidths(new float[] { 10f });
            table1.TotalWidth = 540f;
            table1.LockedWidth = true;
            table1.SpacingBefore = 5f;
            table1.HorizontalAlignment = Element.ALIGN_LEFT;

            //Date of Birth
            table1.AddCell(PhraseCell(new Phrase("Receipt will be posted to the address mentioned in the proposal form in due course.", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            //Date of Birth
            table1.AddCell(PhraseCell(new Phrase("Insurance Policy is valid only if the bank transfer is successful.", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            document.Add(table1);
            #endregion

            document.Close();
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=eReceipt"+printReceipt[0].HPLRefNo.Trim()+".pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
        }
    }

    protected void PrintPolicy_PolicySchedule(List<HPL_PaymentFynalization> printReceipt)
    {
        Document document = new Document(PageSize.A4, 30f, 30f, 30f, 30f);
        Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        {
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            Phrase phrase = null;
            PdfPCell cell = null;
            PdfPTable table = null;
            Color color = null;

            document.Open();

            #region Header
            //Header Table
            table = new PdfPTable(2);
            table.TotalWidth = 500f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 0.3f, 0.7f });

            //Company Logo
            cell = ImageCell("~~/General/GenImages/slic_gen_Logo.png", 30f, PdfPCell.ALIGN_RIGHT);
            table.AddCell(cell);

            //Company Name and Address
            phrase = new Phrase();
            phrase.Add(new Chunk("Sri Lanka Insurance Corporation General (Ltd).\n\n", FontFactory.GetFont("Arial", 16, Font.BOLD, Color.DARK_GRAY)));
            phrase.Add(new Chunk("Rakshana Mandiraya,\n", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            phrase.Add(new Chunk("No 21,\n", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            phrase.Add(new Chunk("Vauxhall Street,\n", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            phrase.Add(new Chunk("Colombo 02", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 0);
            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            table.AddCell(cell);

            //Separater Line
            color = new Color(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
            DrawLine(writer, 25f, document.Top - 79f, document.PageSize.Width - 25f, document.Top - 79f, color);
            DrawLine(writer, 25f, document.Top - 80f, document.PageSize.Width - 25f, document.Top - 80f, color);
            document.Add(table);


            table = new PdfPTable(2);
            table.SetWidths(new float[] { 2f, 6f });
            table.TotalWidth = 540f;
            table.LockedWidth = true;
            table.SpacingBefore = 20f;
            table.HorizontalAlignment = Element.ALIGN_LEFT;

            //Detail Headding
            cell = PhraseCell(new Phrase("First Loss Home Insurance Policy Schedule", FontFactory.GetFont("Arial", 12, Font.UNDERLINE, Color.BLACK)), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            table.AddCell(cell);
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 15f;
            table.AddCell(cell);
            #endregion

            #region Customer Information
            //Customer Policy Details- Policy Number
            table.AddCell(PhraseCell(new Phrase("Policy Number :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase(printReceipt[0].HPLPolicy.Trim(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //Customer Policy Details- Customer Reference Number
            table.AddCell(PhraseCell(new Phrase("Customer Id :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase(printReceipt[0].HPLRefNo.Trim(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //Customer Policy Details- Policy Holder Name
            table.AddCell(PhraseCell(new Phrase("Name of the Insured :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase(printReceipt[0].HPLCus_Name.Trim(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //Customer Policy Details- Policy Holder Address
            table.AddCell(PhraseCell(new Phrase("Address :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            phrase = new Phrase(new Chunk(this.ManageAddress(printReceipt[0].HPL_ADDRESS1, printReceipt[0].HPL_ADDRESS2, printReceipt[0].HPL_ADDRESS3, printReceipt[0].HPL_ADDRESS4), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
            table.AddCell(PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //Customer Policy Details- Premises
            table.AddCell(PhraseCell(new Phrase("The Premises :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase("Private House", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //Customer Policy Details- Risk Location
            table.AddCell(PhraseCell(new Phrase("Situated at :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase(this.ManageAddress(printReceipt[0].HPL_RL_ADDRESS1, printReceipt[0].HPL_RL_ADDRESS2, printReceipt[0].HPL_RL_ADDRESS3, printReceipt[0].HPL_RL_ADDRESS4), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //-----
            table.AddCell(PhraseCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase("But excluding any garden or yard and any outbuilding not communicating with main building", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //Customer Policy Details-Period of Insurance
            table.AddCell(PhraseCell(new Phrase("Period of Insurance :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase("(a) " + printReceipt[0].HPLCoverPeriod.Trim() + " Commencing and expiring at 4:00 p.m. standard time.\n", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);


            //--
            table.AddCell(PhraseCell(new Phrase("", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase("(b) Any subsequent period for which the Insured shall pay and the Corporation shall agree to accept a renewal premium", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //Customer Policy Details- Currency Type
            table.AddCell(PhraseCell(new Phrase("Currency Type :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase("LKR", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //Customer Policy Details- Purchased Plan
            table.AddCell(PhraseCell(new Phrase("Plan Number :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase(printReceipt[0].HPL_PLAN.Trim(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //Customer Policy Details- Premium
            table.AddCell(PhraseCell(new Phrase("Premium Rs. :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table.AddCell(PhraseCell(new Phrase("(All inclusive Rs. "+ double.Parse(printReceipt[0].HPLPremium.ToString()).ToString("N2")+")", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 4f;
            table.AddCell(cell);

            //-------------
            table.AddCell(PhraseCell(new Phrase("THE PROPERTY INSURED", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 12f;
            table.AddCell(cell);

            document.Add(table);
            #endregion

            #region Customer Information - Other
            // Create Table 1---------------------------------------------------
            PdfPTable table1 = null;
            table1 = new PdfPTable(1);
            table1.SetWidths(new float[] { 10f });
            table1.TotalWidth = 540f;
            table1.LockedWidth = true;
            table1.SpacingBefore = 5f;
            table1.HorizontalAlignment = Element.ALIGN_LEFT;

            //Date of Birth
            table1.AddCell(PhraseCell(new Phrase("The Building (The building including landlords fixture and fittings, boundary wall, fences, gates excluding Air Conditioners therein.)", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            document.Add(table1);


            // Create Table ---------------------------------------------------
            PdfPTable table2 = null;
            table2 = new PdfPTable(2);
            table2.SetWidths(new float[] { 8f, 2f });
            table2.TotalWidth = 425f;
            table2.LockedWidth = true;
            table2.SpacingBefore = 5f;
            table2.HorizontalAlignment = Element.ALIGN_LEFT;

            //Date of Birth
            table2.AddCell(PhraseCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 1));
            table2.AddCell(PhraseCell(new Phrase("Sum Insured (Rs.)", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER, 1));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 1);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            table2.AddCell(PhraseCell(new Phrase("Full reinstatement value of the building , permanent fixtures and fittings", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 1));
            table2.AddCell(PhraseCell(new Phrase(printReceipt[0].HPL_LLBPFF.ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_CENTER, 1));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 1);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            document.Add(table2);

            // Create Table 3---------------------------------------------------
            PdfPTable table3 = null;
            table3 = new PdfPTable(1);
            table3.SetWidths(new float[] { 10f });
            table3.TotalWidth = 540f;
            table3.LockedWidth = true;
            table3.SpacingBefore = 5f;
            table3.HorizontalAlignment = Element.ALIGN_LEFT;

            //Date of Birth
            table3.AddCell(PhraseCell(new Phrase("The Contents : Household Goods including air conditioner (Except after mentioned). The Property of the Proposer or member of the Proposer's Family normally residing with the Proposer.", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_JUSTIFIED, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            document.Add(table3);

            // Create Table 4---------------------------------------------------
            PdfPTable table4 = null;
            table4 = new PdfPTable(2);
            table4.SetWidths(new float[] { 8f, 2f });
            table4.TotalWidth = 425f;
            table4.LockedWidth = true;
            table4.SpacingBefore = 5f;
            table4.HorizontalAlignment = Element.ALIGN_LEFT;

            //Date of Birth
            table4.AddCell(PhraseCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 1));
            table4.AddCell(PhraseCell(new Phrase("Sum Insured (Rs.)", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER, 1));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 1);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            table4.AddCell(PhraseCell(new Phrase("Full reinstatement value of the contents excluding personal effects such as jewellery, money, professional equipment", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 1));
            table4.AddCell(PhraseCell(new Phrase(printReceipt[0].HPL_LLCEPE.ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_CENTER, 1));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 1);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            document.Add(table4);

            // Create Table 5---------------------------------------------------
            PdfPTable table5 = null;
            table5 = new PdfPTable(1);
            table5.SetWidths(new float[] { 10f });
            table5.TotalWidth = 540f;
            table5.LockedWidth = true;
            table5.SpacingBefore = 5f;
            table5.HorizontalAlignment = Element.ALIGN_LEFT;

            table5.AddCell(PhraseCell(new Phrase("The insurance on Contents does not cover any part of the structure or ceiling of the Buildings, nor any property to be insured under buildings, nor does it cover Jewellery, gold, gems & personal effects, other valuables, and antiques , Works of arts, Professional Equipments, Deeds, Bonds, Bills of Exchange, Promissory Notes, Manuscripts, Medals, Coins, Motor Vehicles and Accessories and the like. ", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_JUSTIFIED, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            document.Add(table5);


            // Create Table 6---------------------------------------------------
            PdfPTable table6 = null;
            table6 = new PdfPTable(2);
            table6.SetWidths(new float[] { 8f, 3f });
            table6.TotalWidth = 380f;
            table6.LockedWidth = true;
            table6.SpacingBefore = 5f;
            table6.HorizontalAlignment = Element.ALIGN_LEFT;

            table6.AddCell(PhraseCell(new Phrase("Extensions", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 1));
            table6.AddCell(PhraseCell(new Phrase("Limit of liability (Rs.)", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER, 1));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 1);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            table6.AddCell(PhraseCell(new Phrase("Sub Limit -Removal of debris", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 1));
            table6.AddCell(PhraseCell(new Phrase(double.Parse(printReceipt[0].HPL_LLSRD.ToString()).ToString("N2"), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_CENTER, 1));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 1);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            table6.AddCell(PhraseCell(new Phrase("Sub Limit -Professional fee for reinstating the building", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 1));
            table6.AddCell(PhraseCell(new Phrase(double.Parse(printReceipt[0].HPL_LLPRB.ToString()).ToString("N2"), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_CENTER, 1));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 1);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            table6.AddCell(PhraseCell(new Phrase("Sub Limit -Burglary", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 1));
            table6.AddCell(PhraseCell(new Phrase(double.Parse(printReceipt[0].HPL_LLSB.Trim()).ToString("N2"), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_CENTER, 1));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 1);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            table6.AddCell(PhraseCell(new Phrase("Sub Limit - Electrical Damage Cover", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 1));
            table6.AddCell(PhraseCell(new Phrase(double.Parse(printReceipt[0].HPL_LLSD.Trim()).ToString("N2"), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_CENTER, 1));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 1);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            document.Add(table6);

            // Create Table 7---------------------------------------------------
            PdfPTable table7 = null;
            table7 = new PdfPTable(2);
            table7.SetWidths(new float[] { 2f, 6f });
            table7.TotalWidth = 540f;
            table7.LockedWidth = true;
            table7.SpacingBefore = 15f;
            table7.HorizontalAlignment = Element.ALIGN_LEFT;

            table7.AddCell(PhraseCell(new Phrase("Total Sum Insured :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table7.AddCell(PhraseCell(new Phrase("(Rs.) "+ double.Parse(printReceipt[0].HPLSumAssured.Trim()).ToString("N2"), FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            table7.AddCell(PhraseCell(new Phrase("\nExcess/Deductibles", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table7.AddCell(PhraseCell(new Phrase("", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            document.Add(table7);


            // Create Table 8---------------------------------------------------
            PdfPTable table8 = null;
            table8 = new PdfPTable(1);
            table8.SetWidths(new float[] { 10f });
            table8.TotalWidth = 540f;
            table8.LockedWidth = true;
            table8.SpacingBefore = 5f;
            table8.HorizontalAlignment = Element.ALIGN_LEFT;

            table8.AddCell(PhraseCell(new Phrase("1. 10% ,minimum of Rs.5,000.00 - of each and every claim in respect of Malicious Damage claims", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table8.AddCell(PhraseCell(new Phrase("2. 0% or Rs.10,000.00 - whichever is higher on each and every claim in respect of Natural perils claims ", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            table8.AddCell(PhraseCell(new Phrase("3. Rs.1,000.00 - of each and every claim in respect of All other claims.", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            if (printReceipt[0].HPL_AGTCODE > 0)          
                table8.AddCell(PhraseCell(new Phrase("\nIntermediary : " + printReceipt[0].HPL_AGTCODE, FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            else
                table8.AddCell(PhraseCell(new Phrase("\nIntermediary : -", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));


            table8.AddCell(PhraseCell(new Phrase("In witness whereof this Policy has been signed at Head Office ,"+System.DateTime.Now, FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            table8.AddCell(PhraseCell(new Phrase("\n\n\nThis is a computer generated document. No signature is required,\n\nSri Lanka Insurance Corporation Ltd.\nDate of issue : " + System.DateTime.Now, FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT, 0));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER, 0);
            cell.Colspan = 2;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            document.Add(table8);
            #endregion Customer Information - Other

            document.Close();
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=ePolicy_Schedule" + printReceipt[0].HPLRefNo.Trim() + ".pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
        }
    }

    private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, Color color)
    {
        PdfContentByte contentByte = writer.DirectContent;
        contentByte.SetColorStroke(color);
        contentByte.MoveTo(x1, y1);
        contentByte.LineTo(x2, y2);
        contentByte.Stroke();
    }

    private static PdfPCell PhraseCell(Phrase phrase, int align, int border)
    {
        PdfPCell cell = new PdfPCell(phrase);
        if (border == 0)
            cell.BorderColor = Color.WHITE;
        else
            cell.BorderColor = Color.LIGHT_GRAY;

        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 2f;
        cell.PaddingTop = 0f;
        return cell;
    }

    private static PdfPCell ImageCell(string path, float scale, int align)
    {
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
        image.ScalePercent(scale);
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 0f;
        return cell;
    }

    protected string ManageAddress(string varAdd1, string varAdd2, string varAdd3, string varAdd4 )
    {
        string Address = string.Empty;
        Address += varAdd1;

        if (!String.IsNullOrEmpty(varAdd2))
            Address +="\n" + varAdd2;

        if(!String.IsNullOrEmpty(varAdd3))
            Address +="\n" + varAdd3;

        if (!String.IsNullOrEmpty(varAdd4))
            Address += "\n" + varAdd4;

        return Address;
    }
    /*********************************************************************************************/
    #endregion 
}