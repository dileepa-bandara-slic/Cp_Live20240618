using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Life_Authorized_Products_PayReceipt : System.Web.UI.Page
{
 
    string retCode = "";
    string resCode = "";
    string decision = "";
    public string refNo = "";
    string signature = "";
    protected bool motorDept = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        log logmessage2 = new log();
        logmessage2.write_log("AAAA");
        if (!Page.IsPostBack)
        {
            if (Request.Form.Count > 0)
            {
                IDictionary<string, string> param_life = new Dictionary<string, string>();

                foreach (var key in Request.Form.AllKeys)
                {
                    param_life.Add(key, Request.Params[key]);
                }

                 log logmessage = new log();
                 logmessage.write_log("Response Message");
                 logmessage.write_log(Request.Form["message"]);

                if (Request.Form["signature"] != null)
                {
                                       

                    if (Request.Form["reason_code"] != null && Request.Form["req_reference_number"] != null && Request.Form["decision"] != null)
                    {
                        //retCode = Request.Form["ResponseCode"].ToString().Trim();
                        resCode = Request.Form["reason_code"].ToString().Trim();
                        refNo = Request.Form["req_reference_number"].ToString().Trim();
                        signature = Request.Form["signature"].ToString().Trim();
                        decision = Request.Form["decision"].ToString().Trim();
                        retCode = resCode;

                        //string[] keys = Request.Form.AllKeys;

                        //for (int i = 0; i < keys.Length; i++)
                        //{
                        //    Response.Write("Form: " + Request.Form[i] + "<br>");
                        //}

                        if (retCode != null && resCode != null && refNo != null)
                        {

                            Properties properties = new Properties("Life");

                            if (signature.Equals(Security.sign(param_life, "L")))
                            {
                                if (refNo.Contains("/P/"))
                                {
                                    LifePayment premPayment = new LifePayment(refNo);

                                    if (premPayment.status == "P")
                                    {
                                        if (retCode == "100" && resCode == "100" && decision.ToUpper().Equals("ACCEPT"))
                                        {
                                            string newRetCode = "1";
                                            string newResCode = "1";

                                            if (premPayment.update_paid_renewal(refNo, "A", newRetCode, newResCode))
                                            {
                                                premPayment = new LifePayment(refNo);

                                                if (premPayment.status == "A")
                                                {
                                                    litPremRefNo.Text = premPayment.receiptNo;
                                                    litAmount.Text = premPayment.amount.ToString("N2");
                                                    litPolNo.Text = litPolNum.Text = premPayment.polNum;
                                                    litCustName.Text = premPayment.custName;
                                                    litTotDueAmt.Text = premPayment.duesAmt.ToString("N2");
                                                    litDeposits.Text = premPayment.deposits.ToString("N2");
                                                    litPaidDuesAmt.Text = premPayment.paidDuesAmt.ToString("N2");
                                                    litAddtAmt.Text = premPayment.addtAmt.ToString("N2");
                                                    if (premPayment.dsPaidDues != null)
                                                    {
                                                        gvDemands.DataSource = premPayment.dsPaidDues.Tables[0];
                                                        gvDemands.DataBind();
                                                        if (premPayment.dsPaidDues.Tables[0].Rows.Count > 0)
                                                        {
                                                            gvDemands.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                                                            gvDemands.HeaderRow.TableSection = TableRowSection.TableHeader;
                                                        }
                                                    }
                                                    litPayDate.Text = premPayment.entryDate;
                                                    Panel1.Visible = true;

                                                    bool ret = premPayment.send_pay_receipt_email();
                                                    if (ret)
                                                    {
                                                        lblPayStatus.Text = "Confirmation of payment has been emailed to you.";
                                                        lblPayStatus.ForeColor = System.Drawing.Color.Green;
                                                    }
                                                }
                                                else
                                                {
                                                    lblPayStatus.Text = "Sorry, Payment was not successful";
                                                    log logger = new log();
                                                    logger.write_log("Failed at PayReceipt-Pageload: Premium Pay success status not updated");
                                                }
                                            }
                                            else
                                            {
                                                lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                                log logger = new log();
                                                logger.write_log("Failed at PayReceipt-Pageload: Premium Pay success pay not updated");
                                            }
                                        }
                                        else
                                        {
                                            if (premPayment.update_paid_renewal(refNo, "F", retCode, resCode))
                                            {
                                                log logger = new log();
                                                logger.write_log(getReasonCodeContent(retCode));
                                                lblPayStatus.Text = "Sorry, Payment was not successful";
                                                lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                            }
                                            else
                                            {
                                                lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                                log logger = new log();
                                                logger.write_log(getReasonCodeContent(retCode));
                                                logger.write_log("Failed at PayReceipt-Pageload: Premium Pay failure not updated");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                        log logger = new log();
                                        logger.write_log("Failed at PayReceipt-Pageload: Premium Pay Status Invalid");
                                    }
                                }
                                else if (refNo.Contains("/L/"))
                                {
                                    LifePayment loanPaymnt = new LifePayment(refNo);

                                    if (loanPaymnt.status == "P")
                                    {
                                        if (retCode == "100" && resCode == "100" && decision.ToUpper().Equals("ACCEPT"))
                                        {
                                            string newRetCode = "1";
                                            string newResCode = "1";

                                            if (loanPaymnt.update_paid_renewal(refNo, "A", newRetCode, newResCode))
                                            {
                                                loanPaymnt = new LifePayment(refNo);

                                                if (loanPaymnt.status == "A")
                                                {
                                                    litLoanRefNo.Text = loanPaymnt.receiptNo;
                                                    litAmount2.Text = loanPaymnt.amount.ToString("N2");
                                                    litLoanNo.Text = loanPaymnt.loanNo;
                                                    litPolNo2.Text = litRnPolNum.Text = loanPaymnt.polNum;
                                                    litLoanCusNam.Text = loanPaymnt.custName;
                                                    litPayDate2.Text = loanPaymnt.entryDate;

                                                    Panel2.Visible = true;

                                                    bool ret = loanPaymnt.send_pay_receipt_email();
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
                                                    logger.write_log("Failed at PayReceipt-Pageload: Loan pay success status not updated");
                                                }
                                            }
                                            else
                                            {
                                                lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                                log logger = new log();
                                                logger.write_log("Failed at PayReceipt-Pageload: Loan pay success pay not updated");
                                            }
                                        }
                                        else
                                        {
                                            if (loanPaymnt.update_paid_renewal(refNo, "F", retCode, resCode))
                                            {
                                                log logger = new log();
                                                logger.write_log(getReasonCodeContent(retCode));
                                                lblPayStatus.Text = "Sorry, Payment was not successful";
                                                lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                            }
                                            else
                                            {
                                                lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                                log logger = new log();
                                                logger.write_log(getReasonCodeContent(retCode));
                                                logger.write_log("Failed at PayReceipt-Pageload: Loan pay failure not updated");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                        log logger = new log();
                                        logger.write_log("Failed at PayReceipt-Pageload: Loan pay Status Invalid");
                                    }
                                }
                                /**************Updated***********************/
                                else if (refNo.Contains("/R/"))
                                {
                                    LifePayment premPayment = new LifePayment(refNo);

                                    if (premPayment.status == "P")
                                    {
                                        if (retCode == "100" && resCode == "100" && decision.ToUpper().Equals("ACCEPT"))
                                        {

                                            string newRetCode = "1";
                                            string newResCode = "1";

                                            if (premPayment.update_paid_renewal(refNo, "A", newRetCode, newResCode))
                                            {
                                                premPayment = new LifePayment(refNo);

                                                if (premPayment.status == "A")
                                                {
                                                    //litPremRefNo.Text = premPayment.receiptNo;
                                                    //litAmount.Text = premPayment.amount.ToString("N2");
                                                    //litPolNo.Text = litPolNum.Text = premPayment.polNum;
                                                    //litCustName.Text = premPayment.custName;
                                                    ltrRefNo_rev.Text = premPayment.receiptNo;
                                                    ltrPayAmount_rev.Text = premPayment.amount.ToString("N2");
                                                    ltrPolNo_rev.Text = premPayment.polNum;
                                                    ltrCustomerName_rev.Text = premPayment.custName;
                                                    ltrPaidAmount_rev.Text = "Rs." + premPayment.amount.ToString("N2");

                                                    CustProfile customer = new CustProfile(premPayment.username);
                                                    ltrAdrsName_rev.Text = premPayment.custName;
                                                    ltrAdress1_rev.Text = customer.O_addrss1;
                                                    ltrAdress2_rev.Text = customer.O_addrss2;
                                                    ltrAdress3_rev.Text = customer.O_addrss3;
                                                    ltrAdress4_rev.Text = customer.O_addrss4;

                                                    //litTotDueAmt.Text = premPayment.duesAmt.ToString("N2");
                                                    //litDeposits.Text = premPayment.deposits.ToString("N2");
                                                    //litPaidDuesAmt.Text = premPayment.paidDuesAmt.ToString("N2");
                                                    //litAddtAmt.Text = premPayment.addtAmt.ToString("N2");
                                                    //if (premPayment.dsPaidDues != null)
                                                    //{
                                                    //    gvDemands.DataSource = premPayment.dsPaidDues.Tables[0];
                                                    //    gvDemands.DataBind();
                                                    //    if (premPayment.dsPaidDues.Tables[0].Rows.Count > 0)
                                                    //    {
                                                    //        gvDemands.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                                                    //        gvDemands.HeaderRow.TableSection = TableRowSection.TableHeader;
                                                    //    }
                                                    //}
                                                    this.ltrPayDate_rev.Text = premPayment.entryDate;
                                                    this.ltrPayTime_rev.Text = DateTime.Now.ToString("HH:mm:ss");
                                                    Panel3.Visible = true;

                                                    bool ret = premPayment.send_pay_receipt_email();
                                                    if (ret)
                                                    {
                                                        bool ret2 = premPayment.send_pay_phs_email();
                                                        lblPayStatus.Text = "Confirmation of payment has been emailed to you.";
                                                        lblPayStatus.ForeColor = System.Drawing.Color.Green;
                                                    }
                                                }
                                                else
                                                {
                                                    lblPayStatus.Text = "Sorry, Payment was not successful1";
                                                    log logger = new log();
                                                    logger.write_log("Failed at PayReceipt-Pageload: Premium Pay success status not updated");
                                                }
                                            }
                                            else
                                            {
                                                lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                                log logger = new log();
                                                logger.write_log("Failed at PayReceipt-Pageload: Premium Pay success pay not updated");
                                            }
                                        }
                                        else
                                        {
                                            if (premPayment.update_paid_renewal(refNo, "F", retCode, resCode))
                                            {
                                                log logger = new log();
                                                logger.write_log(getReasonCodeContent(retCode));
                                                lblPayStatus.Text = "Sorry, Payment was not successful";
                                                lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                            }
                                            else
                                            {
                                                lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                                log logger = new log();
                                                logger.write_log(getReasonCodeContent(retCode));
                                                logger.write_log("Failed at PayReceipt-Pageload: Premium Pay failure not updated");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                        log logger = new log();
                                        logger.write_log("Failed at PayReceipt-Pageload: Premium Pay Status Invalid");
                                    }
                                }
                                //else if (refNo.Contains("/N/"))
                                else if (refNo.Contains("/N/") || refNo.Contains("/D/")) //2022.06.02
                                {
                                    LifePayment premPayment = new LifePayment(refNo);

                                    log loggerrR = new log();
                                    loggerrR.write_log(refNo + " " + premPayment.payType);

                                    if (premPayment.status == "P")
                                    {
                                        log loggerr = new log();
                                        loggerr.write_log(refNo + " " + resCode + " " + decision);

                                        if (retCode == "100" && resCode == "100" && decision.ToUpper().Equals("ACCEPT"))
                                        {

                                            string newRetCode = "1";
                                            string newResCode = "1";



                                            if (premPayment.update_paid_renewal(refNo, "A", newRetCode, newResCode))
                                            {
                                                premPayment = new LifePayment(refNo);

                                                if (premPayment.status == "A")
                                                {
                                                    //litPremRefNo.Text = premPayment.receiptNo;
                                                    //litAmount.Text = premPayment.amount.ToString("N2");
                                                    //litPolNo.Text = litPolNum.Text = premPayment.polNum;
                                                    //litCustName.Text = premPayment.custName;
                                                    this.lit_prop_recptNo.Text = premPayment.receiptNo;
                                                    this.lit_prop_amount.Text = premPayment.amount.ToString("N2");
                                                    this.lit_prop_propNo.Text = premPayment.polNum;
                                                    this.lit_prop_CustName.Text = premPayment.custName;
                                                    this.lit_prop_PaidAmt.Text = "Rs." + premPayment.amount.ToString("N2");

                                                    CustProfile customer = new CustProfile(premPayment.username);
                                                    this.lit_prop_addrName.Text = premPayment.custName;
                                                    this.lit_prop_addr1.Text = customer.O_addrss1;
                                                    this.lit_prop_addr2.Text = customer.O_addrss2;
                                                    this.lit_prop_addr3.Text = customer.O_addrss3;
                                                    this.lit_prop_addr4.Text = customer.O_addrss4;

                                                    this.lit_prop_Date.Text = premPayment.entryDate;
                                                    this.lit_prop_Time.Text = DateTime.Now.ToString("HH:mm:ss");
                                                    Panel4.Visible = true;

                                                    bool ret = premPayment.send_pay_receipt_email();

                                                    LifeProposal propObjct2 = new LifeProposal();
                                                    if (refNo.Contains("/D/"))
                                                    {
                                                        bool sendSms = propObjct2.SendEasyCashSMS(refNo);

                                                    }

                                                    if (ret)
                                                    {
                                                        LifeProposal propObjct = new LifeProposal();
                                                        String polFeeRctNo = propObjct.getPolicyFeeRecpt(refNo);
                                                        if (!polFeeRctNo.Equals(""))
                                                        {
                                                            LifePayment premPayment2 = new LifePayment(polFeeRctNo);
                                                            if (premPayment2.status == "A")
                                                            {
                                                                //litPremRefNo.Text = premPayment.receiptNo;
                                                                //litAmount.Text = premPayment.amount.ToString("N2");
                                                                //litPolNo.Text = litPolNum.Text = premPayment.polNum;
                                                                //litCustName.Text = premPayment.custName;
                                                                this.ltrPolFee_RctNo.Text = premPayment2.receiptNo;
                                                                this.ltrPolFee_Amount.Text = premPayment2.amount.ToString("N2");
                                                                this.ltrPolFee_PropNo.Text = premPayment2.polNum;
                                                                this.ltrPolFee_CustName.Text = premPayment2.custName;
                                                                this.ltrPolFee_Amount2.Text = "Rs." + premPayment2.amount.ToString("N2");

                                                                CustProfile customer2 = new CustProfile(premPayment2.username);
                                                                this.ltrPolFee_addrName.Text = premPayment2.custName;
                                                                this.ltrPolFee_addr1.Text = customer2.O_addrss1;
                                                                this.ltrPolFee_addr2.Text = customer2.O_addrss2;
                                                                this.ltrPolFee_addr3.Text = customer2.O_addrss3;
                                                                this.ltrPolFee_addr4.Text = customer2.O_addrss4;

                                                                this.ltrPolFee_PaidDate.Text = premPayment2.entryDate;
                                                                this.ltrPolFee_PaidTime.Text = DateTime.Now.ToString("HH:mm:ss");
                                                                Panel5.Visible = true;

                                                                bool ret2 = premPayment2.send_pay_receipt_email();
                                                            }
                                                        }
                                                        lblPayStatus.Text = "Confirmation of payment has been emailed to you.";
                                                        lblPayStatus.ForeColor = System.Drawing.Color.Green;
                                                    }
                                                }
                                                else
                                                {
                                                    lblPayStatus.Text = "Sorry, Payment was not successful1";
                                                    log logger = new log();
                                                    logger.write_log("Failed at PayReceipt-Pageload: Premium Pay success status not updated");
                                                }
                                            }
                                            else
                                            {
                                                lblPayStatus.Text = "Internal Error. Please contact SLIC1";
                                                log logger = new log();
                                                logger.write_log("Failed at PayReceipt-Pageload: Premium Pay success pay not updated");
                                            }
                                        }
                                        else
                                        {
                                            if (premPayment.update_paid_renewal(refNo, "F", retCode, resCode))
                                            {
                                                log logger = new log();
                                                logger.write_log(getReasonCodeContent(retCode));
                                                lblPayStatus.Text = "Sorry, Payment was not successful22";
                                                lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                            }
                                            else
                                            {
                                                lblPayStatus.Text = "Internal Error. Please contact SLIC2";
                                                log logger = new log();
                                                logger.write_log(getReasonCodeContent(retCode));
                                                logger.write_log("Failed at PayReceipt-Pageload: Premium Pay failure not updated");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lblPayStatus.Text = "Internal Error. Please contact SLIC3";
                                        log logger = new log();
                                        logger.write_log("Failed at PayReceipt-Pageload: Premium Pay Status Invalid");
                                    }
                                }
                                //else if (refNo.Contains("/L/"))
                                //{
                                //    LifePayment premPayment = new LifePayment(refNo);

                                //    if (premPayment.status == "P")
                                //    {
                                //        if (retCode == "1" && resCode == "1")
                                //        {
                                //            if (premPayment.update_paid_renewal(refNo, "A", retCode, resCode))
                                //            {
                                //                premPayment = new LifePayment(refNo);

                                //                if (premPayment.status == "A")
                                //                {
                                //                    litPremRefNo.Text = premPayment.receiptNo;
                                //                    litAmount.Text = premPayment.amount.ToString("N2");
                                //                    litPolNo.Text = litPolNum.Text = premPayment.polNum;
                                //                    litCustName.Text = premPayment.custName;
                                //                    litTotDueAmt.Text = premPayment.duesAmt.ToString("N2");
                                //                    litDeposits.Text = premPayment.deposits.ToString("N2");
                                //                    litPaidDuesAmt.Text = premPayment.paidDuesAmt.ToString("N2");
                                //                    litAddtAmt.Text = premPayment.addtAmt.ToString("N2");
                                //                    if (premPayment.dsPaidDues != null)
                                //                    {
                                //                        gvDemands.DataSource = premPayment.dsPaidDues.Tables[0];
                                //                        gvDemands.DataBind();
                                //                        if (premPayment.dsPaidDues.Tables[0].Rows.Count > 0)
                                //                        {
                                //                            gvDemands.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                                //                            gvDemands.HeaderRow.TableSection = TableRowSection.TableHeader;
                                //                        }
                                //                    }
                                //                    litPayDate.Text = premPayment.entryDate;
                                //                    Panel1.Visible = true;

                                //                    bool ret = premPayment.send_pay_receipt_email();
                                //                    if (ret)
                                //                    {
                                //                        lblPayStatus.Text = "Confirmation of payment has been emailed to you.";
                                //                        lblPayStatus.ForeColor = System.Drawing.Color.Green;
                                //                    }
                                //                }
                                //                else
                                //                {
                                //                    lblPayStatus.Text = "Sorry, Payment was not successful";
                                //                    log logger = new log();
                                //                    logger.write_log("Failed at PayReceipt-Pageload: Premium Pay success status not updated");
                                //                }
                                //            }
                                //            else
                                //            {
                                //                lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                //                log logger = new log();
                                //                logger.write_log("Failed at PayReceipt-Pageload: Premium Pay success pay not updated");
                                //            }
                                //        }
                                //        else
                                //        {
                                //            if (premPayment.update_paid_renewal(refNo, "F", retCode, resCode))
                                //            {
                                //                lblPayStatus.Text = "Sorry, Payment was not successful";
                                //                lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                //            }
                                //            else
                                //            {
                                //                lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                //                log logger = new log();
                                //                logger.write_log("Failed at PayReceipt-Pageload: Premium Pay failure not updated");
                                //            }
                                //        }
                                //    }
                                //    else
                                //    {
                                //        lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                //        log logger = new log();
                                //        logger.write_log("Failed at PayReceipt-Pageload: Premium Pay Status Invalid");
                                //    }
                                //}
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
                            lblPayStatus.Text = "Internal Error. Please contact SLIC";
                            log logger = new log();
                            logger.write_log("Failed at PayReceipt-Pageload: Parameter is null");
                        }

                    }
                    else
                    {
                        lblPayStatus.Text = "Internal Error. Please contact SLIC";
                        log logger = new log();
                        logger.write_log("Failed at PayReceipt-Pageload: Parameters not found");
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
                            if (refNo.Contains("/P/"))
                            {
                                LifePayment premPayment = new LifePayment(refNo);

                                if (premPayment.status == "P")
                                {
                                    if (premPayment.update_paid_renewal(refNo, "K", retCode, resCode))
                                    {
                                        logger.write_log(getReasonCodeContent(retCode));
                                        lblPayStatus.Text = "Sorry, Payment was not successful";
                                        lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        logger.write_log(getReasonCodeContent(retCode));
                                        lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                        logger.write_log("Failed at PayReceipt-Pageload: Premium Pay failure not updated");
                                    }
                                }
                            }
                            else if (refNo.Contains("/L/"))
                            {
                                LifePayment loanPaymnt = new LifePayment(refNo);

                                if (loanPaymnt.status == "P")
                                {
                                    if (loanPaymnt.update_paid_renewal(refNo, "K", retCode, resCode))
                                    {
                                        logger.write_log(getReasonCodeContent(retCode));
                                        lblPayStatus.Text = "Sorry, Payment was not successful";
                                        lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        logger.write_log(getReasonCodeContent(retCode));
                                        lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                        logger.write_log("Failed at PayReceipt-Pageload: Loan pay failure not updated");
                                    }
                                }
                            }
                            /************Updated by MANORI ***************/
                            else if (refNo.Contains("/R/"))
                            {
                                LifePayment revvlPaymnt = new LifePayment(refNo);

                                if (revvlPaymnt.status == "P")
                                {
                                    if (revvlPaymnt.update_paid_renewal(refNo, "K", retCode, resCode))
                                    {
                                        logger.write_log(getReasonCodeContent(retCode));
                                        lblPayStatus.Text = "Sorry, Payment was not successful";
                                        lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        logger.write_log(getReasonCodeContent(retCode));
                                        lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                        logger.write_log("Failed at PayReceipt-Pageload: Loan pay failure not updated");
                                    }
                                }
                            }
                            else if (refNo.Contains("/N/"))
                            {
                                LifePayment newBusPaymnt = new LifePayment(refNo);

                                if (newBusPaymnt.status == "P")
                                {
                                    if (newBusPaymnt.update_paid_renewal(refNo, "K", retCode, resCode))
                                    {
                                        logger.write_log(getReasonCodeContent(retCode));
                                        lblPayStatus.Text = "Sorry, Payment was not successful3";
                                        lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        logger.write_log(getReasonCodeContent(retCode));
                                        lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                        logger.write_log("Failed at PayReceipt-Pageload: Proposal pay failure not updated");
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
                            logger.write_log("Failed at PayReceipt-Pageload: Signature Parameter is null");
                        }
                    }

                    //Response.Redirect("./Default.aspx", false);
                }
            }
            else
            {
                lblPayStatus.Text = "Internal Error. Please contact SLIC";
                log logger = new log();
                logger.write_log("Failed at PayReceipt-Pageload: No parameteres found");
            }

        }
    }

    protected void btnReceipt_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        LifePayment pro = new LifePayment(litPremRefNo.Text.Trim());
        Print_pdf_Life pdf = new Print_pdf_Life();
        pdf.print_receipt(pro, ip, User.Identity.Name, gvDemands);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        LifePayment rnwl = new LifePayment(litLoanRefNo.Text.Trim());
        Print_pdf_Life pdf = new Print_pdf_Life();
        pdf.print_receipt_Loan(rnwl, ip, User.Identity.Name);

    }

    protected void btnPDF_rev_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        LifePayment revival = new LifePayment(this.ltrRefNo_rev.Text.Trim());
        Print_pdf_Life pdf = new Print_pdf_Life();
        pdf.print_receipt_Revival(revival, ip, User.Identity.Name);
    }


    protected void btn_prop_DwnldRecpt_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        LifePayment revival = new LifePayment(this.lit_prop_recptNo.Text.Trim());
        Print_pdf_Life pdf = new Print_pdf_Life();
        pdf.print_receipt_Proposal(revival, ip, User.Identity.Name);
    }

    private string getReasonCodeContent(string reasCode)
    {
        int reasonCode = int.Parse((string)reasCode);
        switch (reasonCode)
        {
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

    protected void btn_pol_fee_DwnldRecpt_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //LifePayment revival = new LifePayment(this.lit_prop_recptNo.Text.Trim());
        LifeProposal propObj = new LifeProposal();
        string polFeeRect = propObj.getPolicyFeeRecpt(this.lit_prop_recptNo.Text.Trim());

        if (!polFeeRect.Equals(""))
        {
            Print_pdf_Life pdf = new Print_pdf_Life();
            LifePayment polFee = new LifePayment(polFeeRect);
            pdf.print_receipt_PolFee(polFee, ip, User.Identity.Name);
        }
    }
}