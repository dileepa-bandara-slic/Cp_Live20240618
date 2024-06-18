using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Life_Authorized_Products_PayReceipt_Amex : System.Web.UI.Page
{
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!Page.IsPostBack)
    //    {
    //        if (Request.Form.Count > 0)
    //        {
    //            log lgg = new log();
    //            lgg.write_log("out");
    //            //lgg.write_log(Request.Form["ResponseCode"].ToString().Trim());
    //            lgg.write_log(Request.Form["reason_code"].ToString().Trim());

    //            if (Request.Form["signature"] != null)
    //            {
    //                Literal1.Text = Request.Form["reason_code"].ToString();
    //            }
    //        }
    //    }
    //}

    private string PTReceipt = "";
    private int intResult = 0;
    private string strErrMsg = "";

    string retCode = "";
    string resCode = "";
    string decision = "";
    public string refNo = "";
    string signature = "";
    protected bool motorDept = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Form.Count > 0)
            {
                 log logmessage = new log();
                //logmessage.write_log("Response Message");
                //logmessage.write_log(Request.Form["message"]);

                logmessage.write_log("out");

                //if (Request.Form["signature"] != null)
                if ((Request.Form["encryptedReceiptPay"] != null) && (Request.Form["encryptedReceiptPay"].Length > 0))
                {
                    //if (Request.Form["reason_code"] != null && Request.Form["req_reference_number"] != null && Request.Form["decision"] != null)
                    //{
                    logmessage.write_log(Request.Form["encryptedReceiptPay"]);

                    DecrypteData(Request.Form["encryptedReceiptPay"]);

                    if (hdfRefNo.Value != null && hdfDecision.Value != null)
                    {
                        ////retCode = Request.Form["ResponseCode"].ToString().Trim();
                        //resCode = Request.Form["reason_code"].ToString().Trim();
                        //refNo = Request.Form["req_reference_number"].ToString().Trim();
                        //signature = Request.Form["signature"].ToString().Trim();
                        //decision = Request.Form["decision"].ToString().Trim();
                        //retCode = resCode;

                        //string[] keys = Request.Form.AllKeys;

                        //for (int i = 0; i < keys.Length; i++)
                        //{
                        //    Response.Write("Form: " + Request.Form[i] + "<br>");
                        //}

                        refNo = hdfRefNo.Value;
                        decision = hdfDecision.Value;

                        //if (retCode != null && resCode != null && refNo != null)
                        //{

                            Properties properties = new Properties("Life");

                            //if (properties.isValidSignature(signature, refNo))
                            //{
                                if (refNo.Contains("/P/"))
                                {
                                    LifePayment premPayment = new LifePayment(refNo);

                                    if (premPayment.status == "P")
                                    {
                                //if (retCode == "100" && resCode == "100" && decision.ToUpper().Equals("ACCEPT"))
                                        if (decision.ToUpper().Equals("ACCEPTED"))
                                        {
                                            retCode = "1";
                                            resCode = "1";

                                            if (premPayment.update_paid_renewal(refNo, "A", retCode, resCode))
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
                                            IShroff shrof = new IShroff();
                                            retCode = shrof.getErrorCode();
                                            resCode = retCode;

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
                                //if (retCode == "100" && resCode == "100" && decision.ToUpper().Equals("ACCEPT"))
                                

                                        if (decision.ToUpper().Equals("ACCEPTED"))
                                        {
                                            retCode = "1";
                                            resCode = "1";

                                            if (loanPaymnt.update_paid_renewal(refNo, "A", retCode, resCode))
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

                                            IShroff shrof = new IShroff();
                                            retCode = shrof.getErrorCode();
                                            resCode = retCode;
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
                                        if (decision.ToUpper().Equals("ACCEPTED"))
                                        {
                                            retCode = "1";
                                            resCode = "1";

                                            if (premPayment.update_paid_renewal(refNo, "A", retCode, resCode))
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
                                            IShroff shrof = new IShroff();
                                            retCode = shrof.getErrorCode();
                                            resCode = retCode;

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
                                else if (refNo.Contains("/N/"))
                                {
                                    LifePayment premPayment = new LifePayment(refNo);

                                    if (premPayment.status == "P")
                                    {
                                        if (decision.ToUpper().Equals("ACCEPTED"))
                                        {
                                            retCode = "1";
                                            resCode = "1";

                                            if (premPayment.update_paid_renewal(refNo, "A", retCode, resCode))
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

                                                                this.ltrPolFee_PaidDate.Text = premPayment.entryDate;
                                                                this.ltrPolFee_PaidTime.Text = DateTime.Now.ToString("HH:mm:ss");
                                                                Panel5.Visible = true;
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
                                                lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                                log logger = new log();
                                                logger.write_log("Failed at PayReceipt-Pageload: Premium Pay success pay not updated");
                                            }
                                        }
                                        else
                                        {
                                            IShroff shrof = new IShroff();
                                            retCode = shrof.getErrorCode();
                                            resCode = retCode;

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

                        
                        //}
                        ////else
                        ////{
                        ////    lblPayStatus.Text = "Internal Error. Please contact SLIC";
                        ////    log logger = new log();
                        ////    logger.write_log("Failed at PayReceipt-Pageload: Parameter is null");
                        ////}

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
                    lblPayStatus.Text = "Internal Error. Please contact SLIC7";
                    log logger = new log();
                    logger.write_log("Failed at PayReceipt-Pageload: Parameter is null");
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

    private void DecrypteData(string strEncryptedReceipt)
    {
        try
        {
            IShroff objIShroff = new IShroff();
            Properties propAmex = new Properties("Life", "Amex");
            //Un comment and edit below 2 lines if your security keys are present in a custom folder 
            //other than <iPay Client Service>\keys

            string strKeySet_1 = propAmex.IPGKeyPath_1;
            objIShroff.setSecurityKeysPath(strKeySet_1);

            bool bResult = objIShroff.setEncryptedReceipt(strEncryptedReceipt);
            if (bResult)
            {
                PTReceipt = objIShroff.getPlainTextReceipt();
                DisplayInoviceDetails(PTReceipt);

                //encryptedReceipt.Text = strEncryptedReceipt;
                //txtPlainReceipt.Text = PTReceipt;

                log logger = new log();
                logger.write_log("Plain Receipt" + PTReceipt);
            }
            else
            {
                lblPayStatus.Text = "Internal Error. Please contact SLIC11";
                log logger = new log();
                logger.write_log("Failed at setEncryptedReceipt: Error Code " + objIShroff.getErrorCode() + " and Error Description : " + objIShroff.getErrorMessage());

                //Response.Write("Error Code:" + objIShroff.getErrorCode());
                //Response.Write("<BR> Error Description :" + objIShroff.getErrorMessage());
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


    private void DisplayInoviceDetails(string strReceiptDetail)
    {

        //string strhtml = "<table width=600>";
        if (intResult < 0)
        {

            lblPayStatus.Text = "Internal Error. Please contact SLIC12";
            log logger = new log();
            logger.write_log("Failed at DisplayInoviceDetails: Error Code " + intResult.ToString() + " and Error Description : " + strErrMsg);

        }
        else
        {
            XmlDocument xmlDocx = new XmlDocument();

            xmlDocx.LoadXml(strReceiptDetail);

            XmlNodeList oNL = xmlDocx.FirstChild.ChildNodes;

            foreach (XmlNode oNode in oNL)
            {

                if (oNode.Name.Equals("mer_txn_id"))
                {
                    hdfRefNo.Value = oNode.InnerText;
                }

                if (oNode.Name.Equals("txn_status"))
                {
                    hdfDecision.Value = oNode.InnerText;
                }

                if (oNode.Name.Equals("txn_amt"))
                {
                    hdfTxnAmnt.Value = oNode.InnerText;
                }

                if (oNode.Name.Equals("reason"))
                {
                    hdfReason.Value = oNode.InnerText;
                }

                if (oNode.Name.Equals("auth_code"))
                {
                    hdfAuthCode.Value = oNode.InnerText;
                }
                
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

    /*protected void btn_prop_DwnldRecpt_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        LifePayment revival = new LifePayment(this.lit_prop_recptNo.Text.Trim());
        Print_pdf_Life pdf = new Print_pdf_Life();
        pdf.print_receipt_Proposal(revival, ip, User.Identity.Name);
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
    }*/

    protected void btn_prop_DwnldRecpt_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        LifePayment revival = new LifePayment(this.lit_prop_recptNo.Text.Trim());
        Print_pdf_Life pdf = new Print_pdf_Life();
        pdf.print_receipt_Proposal(revival, ip, User.Identity.Name);
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