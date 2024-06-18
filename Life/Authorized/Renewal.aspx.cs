using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Life_Authorized_Renewal : System.Web.UI.Page
{
    EncryptDecrypt enc = new EncryptDecrypt();
    public string errorMsg { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        string strReq = "";
        if (!Page.IsPostBack)
        {
            strReq = Request.RawUrl;
            string h = strReq.Substring(strReq.Length - 1);
            if (h == "#")
            {
                errorMsg = "No url";
                Server.Transfer("ErrorPage.aspx");
            }
            else
            {
                strReq = strReq.Substring(strReq.IndexOf('?') + 1);

                strReq = enc.Decrypt(strReq);
                if (strReq == "#")
                {
                    errorMsg = "No Parameters Passed";
                    Server.Transfer("ErrorPage.aspx");
                }
                else
                {
                    Dictionary<string, string> paraList = new Dictionary<string, string>();
                    paraList = enc.getParameters(strReq);

                    if (paraList.ContainsKey("PolicyNo"))
                    {
                        string polNo = paraList["PolicyNo"];
                        //PayPremValidator.IsValid = true;
                        PayLoanValidator.IsValid = true;
                        litPolNumber.Text = polNo;

                        Panel1.Visible = false;
                       // txtPayPremAmt.Text = "";
                        btnPayPrem.Enabled = false;
                        Panel2.Visible = false;
                        txtPayLoanAmt.Text = "";
                        btnPayLoan.Enabled = false;

                        LifeCustomer customer = new LifeCustomer();
                        string message = "";

                        //if (!customer.loanExist(polNo))
                        //{
                            //rblPayType.Items.Remove((rblPayType.Items.FindByValue("L")));
                            rblPayType.Visible = false;
                        //}

                        string name = "";
                        double totDueAmt = 0;
                        double payableDueAmt = 0;
                        double deposits = 0;

                        message = customer.getOnlinePayments(litPolNumber.Text.Trim(), gvOnlPaymnts);
                        if (message == "success")
                        {
                            message = customer.getPolicyDetails(litPolNumber.Text.Trim(), gvDemands, out name, out deposits);
                        }
                        if (message != "success")
                        {
                            lblErrMesg.Text = message;
                        }
                        else
                        {
                            litPolNumber.Text = litPolNumber.Text.Trim();
                            litName.Text = name;
                            litTotDueAmt.Text = totDueAmt.ToString("N2");
                            litPayableDue.Text = payableDueAmt.ToString("N2");
                            //litTotAmt.Text = totalAmt.ToString("N2");
                            litDeposits.Text = deposits.ToString("N2");
                            Panel1.Visible = true;
                            btnPayPrem.Enabled = true;
                        }

                    }
                    else
                    {
                        errorMsg = "No valid Parameters Passed";
                        Server.Transfer("ErrorPage.aspx");
                    }
                }
            }

            if (gvDemands.Rows.Count > 0)
            {
                gvDemands.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
                gvDemands.HeaderRow.Cells[2].Attributes.Add("data-class", "expand");

                gvDemands.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        if (gvDemands.Rows.Count > 0)
        {
            gvDemands.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            gvDemands.HeaderRow.Cells[2].Attributes.Add("data-class", "expand");

            gvDemands.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

    }

    protected void rblPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;

        LifeCustomer customer = new LifeCustomer();
        string message = "";

        if (rblPayType.SelectedValue == "P")
        {
            string name = "";            
            double payableDueAmt = 0;
            double totDueAmt = 0;
            double deposits = 0;

            message = customer.getPolicyDetails(litPolNumber.Text.Trim(), gvDemands, out name, out deposits);

            if (message != "success")
            {
                lblErrMesg.Text = message;
            }
            else
            {
                litPolNumber.Text = litPolNumber.Text.Trim();
                litName.Text = name;
                litTotDueAmt.Text = totDueAmt.ToString("N2");
                litPayableDue.Text = payableDueAmt.ToString("N2");
                //litTotAmt.Text = totalAmt.ToString("N2");
                litDeposits.Text = deposits.ToString("N2");
                Panel1.Visible = true;
                btnPayPrem.Enabled = true;
            }
        }
        else if (rblPayType.SelectedValue == "L")
        {
            if (customer.loanExist(litPolNumber.Text))
            {
                string polNum = "";
                string loanNum = "";
                string grantDate = "";
                double grantAmt = 0;
                string name = "";
                string nextDueDt = "";
                double nextDueCap = 0;
                double nextDueInt = 0;
                string lastRepaidDt = "";
                double lastRepaidCap = 0;
                double lastRepaidInt = 0;

                message = customer.getLoanDetails(litPolNumber.Text.Trim(), "P", out polNum, out loanNum, out grantDate, out grantAmt, out name,
                                                  out nextDueDt, out nextDueCap, out nextDueInt, out lastRepaidDt, out lastRepaidCap, out lastRepaidInt);

                if (message != "success")
                {
                    lblErrMesg.Text = message;
                }
                else
                {
                    litLoanNumber.Text = loanNum;
                    litPolNum.Text = polNum;
                    litName2.Text = name;
                    litGrantDate.Text = grantDate;
                    litGrantAmt.Text = grantAmt.ToString("N2");
                    litNextDueDt.Text = nextDueDt;
                    litNextDueCap.Text = nextDueCap.ToString("N2");
                    litNextDueInt.Text = nextDueInt.ToString("N2");
                    litLastRepdDt.Text = lastRepaidDt;
                    litLastRepdCap.Text = lastRepaidCap.ToString("N2");
                    litLastRepdInt.Text = lastRepaidInt.ToString("N2");
                    Panel2.Visible = true;
                    btnPayLoan.Enabled = true;
                }
            }
            else
            {
                lblErrMesg.Text = "Loan number not found - Internal error";
            }
        }

        if (gvDemands.Rows.Count > 0)
        {
            gvDemands.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            gvDemands.HeaderRow.Cells[2].Attributes.Add("data-class", "expand");

            gvDemands.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    //protected void checkPayPremAmt(object source, ServerValidateEventArgs args)
    //{
    //    int status = -1;
    //    string message = "";


    //    InfoValidator validator = new InfoValidator();

    //    validator.checkAmountField(txtPayPremAmt.Text, out status, out message);
    //    if (status == 0)
    //    {
    //        args.IsValid = true;
    //        btnPayPrem.Focus();
    //    }
    //    else
    //    {
    //        args.IsValid = false;
    //        PayPremValidator.ErrorMessage = message;
    //        txtPayPremAmt.Focus();
    //    }

    //}

    protected void checkPayLoanAmt(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();

        validator.checkAmountField(txtPayLoanAmt.Text, out status, out message);
        if (status == 0)
        {
            args.IsValid = true;
            btnPayLoan.Focus();
        }
        else
        {
            args.IsValid = false;
            PayLoanValidator.ErrorMessage = message;
            txtPayLoanAmt.Focus();
        }

    }

    protected void btnPayPrem_Click(object sender, EventArgs e)
    {
        double payAmt = 0;
        double deposits = 0;
        double duesPaid = 0;
        double addtAmt = 0;
        double duesTotal = 0;

        if (lblErrMesg.Text == "")
        {
            if (txtAddtAmt.Text.Trim() == "")
            {
                txtAddtAmt.Text = "0.00";
            }

            try
            {                
                deposits = double.Parse(litDeposits.Text.Trim());
                duesTotal = double.Parse(litTotDueAmt.Text.Trim());
                duesPaid = double.Parse(litPayableDue.Text.Trim());
                addtAmt = double.Parse(txtAddtAmt.Text.Trim());
                payAmt = duesPaid + addtAmt;
            }
            catch
            {
                addtAmtValidator.IsValid = false;
                addtAmtValidator.ErrorMessage = "Invalid amount.";                
            }

            if (Math.Round(payAmt, 2) <= 0)
            {
                addtAmtValidator.IsValid = false;
                addtAmtValidator.ErrorMessage = "Total paid amount should be greater than 0.";
            }

            bool ret = validateDemandsAmt();

            if (Page.IsValid && ret)
            {
                LifePayment directPayment = new LifePayment();

                string mesg = "success";
                if (duesTotal > 0)
                {
                    mesg = directPayment.depositAdjPending(litPolNumber.Text.Trim());
                }

                if (mesg == "success")
                {
                    string recptNo = directPayment.generate_renwReceiptNo(Convert.ToInt32(DateTime.Today.ToString("yyyy")), "P");

                    if (!String.IsNullOrEmpty(recptNo))
                    {
                        CustProfile profile = new CustProfile(Page.User.Identity.Name);

                        if (directPayment.insert_renewal(litPolNumber.Text.Trim(), payAmt, Page.User.Identity.Name, "P",
                                            recptNo, litName.Text.Trim(), "", "P", "", gvDemands, deposits, duesTotal, addtAmt, duesPaid, profile.O_email, profile.O_mobileNumber))
                        {
                            //Lit_msg.Text = "Should be navigated to the next page. Proposal id : " + proid;

                            EncryptDecrypt dc = new EncryptDecrypt();
                            Dictionary<string, string> qs = new Dictionary<string, string>();
                            qs.Add("Ref_No", recptNo.Trim());
                            qs.Add("Type", "R"); // N-new businees, R-renewals
                            Response.Redirect(dc.url_encrypt("Products/Payment.aspx", qs));
                            //Server.Transfer(dc.url_encrypt("Products/Payment.aspx", qs));
                        }
                        else
                        {
                            PayPremValidator.IsValid = false;
                            PayPremValidator.ErrorMessage = "An error occured, please resubmit with correct details.";
                            // txtPayPremAmt.Focus();
                        }
                    }
                    else
                    {
                        PayPremValidator.IsValid = false;
                        PayPremValidator.ErrorMessage = "An error occured, please resubmit with correct details.";
                        // txtPayPremAmt.Focus();
                    }
                }
                else
                {
                    PayPremValidator.IsValid = false;
                    PayPremValidator.ErrorMessage = mesg;
                }
            }
        }
    }


    protected void btnPayLoan_Click(object sender, EventArgs e)
    {
        double payLoanAmt = 0;

        if (lblErrMesg.Text == "")
        {
            if (txtPayLoanAmt.Text.Trim() == "")
            {
                PayLoanValidator.IsValid = false;
                PayLoanValidator.ErrorMessage = "Amount should be entered.";
                txtPayLoanAmt.Focus();
            }
            else
            {
                if (txtPayLoanAmt.Visible == true)
                {
                    try
                    {
                        payLoanAmt = double.Parse(txtPayLoanAmt.Text.Trim());
                    }
                    catch
                    {
                        PayLoanValidator.IsValid = false;
                        PayLoanValidator.ErrorMessage = "Invalid amount.";
                        txtPayLoanAmt.Focus();
                    }
                }
                else
                {
                    PayLoanValidator.IsValid = false;
                    PayLoanValidator.ErrorMessage = "An error occured, please resubmit with correct details.";
                    txtPayLoanAmt.Focus();
                }
            }

            if (Page.IsValid)
            {
                LifePayment directPayment = new LifePayment();
                string recptNo = directPayment.generate_renwReceiptNo(Convert.ToInt32(DateTime.Today.ToString("yyyy")), "L");

                if (!String.IsNullOrEmpty(recptNo))
                {
                    CustProfile profile = new CustProfile(Page.User.Identity.Name);
                    if (directPayment.insert_renewal(litPolNum.Text.Trim(), payLoanAmt, Page.User.Identity.Name, "P",
                                        recptNo, litName2.Text.Trim(), "", "L", litLoanNumber.Text.Trim(), null, 0, 0, 0, 0, profile.O_email, profile.O_mobileNumber))
                    {
                        //Lit_msg.Text = "Should be navigated to the next page. Proposal id : " + proid;

                        EncryptDecrypt dc = new EncryptDecrypt();
                        Dictionary<string, string> qs = new Dictionary<string, string>();
                        qs.Add("Ref_No", recptNo.Trim());
                        qs.Add("Type", "R"); // N-new businees, R-renewals
                        Response.Redirect(dc.url_encrypt("Products/Payment.aspx", qs));
                    }
                    else
                    {
                        PayLoanValidator.IsValid = false;
                        PayLoanValidator.ErrorMessage = "An error occured, please resubmit with correct details.";
                        txtPayLoanAmt.Focus();
                    }
                }
                else
                {
                    PayLoanValidator.IsValid = false;
                    PayLoanValidator.ErrorMessage = "An error occured, please resubmit with correct details.";
                    txtPayLoanAmt.Focus();
                }
            }
        }
    }

    protected void cbPayDue_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chkPayDue = (CheckBox)sender;
            //GridViewRow gvr = (GridViewRow)ss.Parent.Parent;
            //string Key = chkPayDue.Attributes["Key"].ToString();
            // use the key here 
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);

            double premium = double.Parse(row.Cells[1].Text);
            double lateFee = double.Parse(row.Cells[2].Text);
            double totPaidDue = double.Parse(litTotDueAmt.Text);
            double payableDueAmt = double.Parse(litPayableDue.Text);
            double deposits = double.Parse(litDeposits.Text);
                        

            if (chkPayDue.Checked)
            {
                totPaidDue = totPaidDue + (premium + lateFee);
            }
            else
            {
                totPaidDue = totPaidDue - (premium + lateFee);
            }

            if (totPaidDue < 0)
            {
                totPaidDue = 0;
            }

            litTotDueAmt.Text = totPaidDue.ToString("N2");
            payableDueAmt = totPaidDue - deposits;

            if (payableDueAmt < 0)
            {
                payableDueAmt = 0;
            }

            litPayableDue.Text = payableDueAmt.ToString("N2");
            //litTotAmt.Text = totPaidDue.ToString("N2");

        }
        catch
        {
            PayPremValidator.IsValid = false;
            PayPremValidator.ErrorMessage = "An error occured, please contact SLIC.";
        }

    }

    public bool validateDemandsAmt()
    {
        bool retVal = false;

        double totPremium = 0;
        double totLateFee = 0;

        try
        {
            int prevChkIndex = -1;
            foreach (GridViewRow row in gvDemands.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string paidDue = row.Cells[0].Text.Trim();
                    double premium = double.Parse(row.Cells[1].Text.Trim());
                    double lateFee = double.Parse(row.Cells[2].Text.Trim());
                    CheckBox cbPayDue = (CheckBox)row.FindControl("cbPayDue");


                    if (cbPayDue.Checked && prevChkIndex == (row.RowIndex - 1))
                    {
                        prevChkIndex = row.RowIndex;
                        totPremium = totPremium + premium;
                        totLateFee = totLateFee + lateFee;
                    }
                    else if (cbPayDue.Checked && prevChkIndex != (row.RowIndex - 1))
                    {
                        PayPremValidator.IsValid = false;
                        PayPremValidator.ErrorMessage = "Please select dues in order, starting from first due.";
                        retVal = false;
                        break;
                    }
                }
            }

            double deposits = double.Parse(litDeposits.Text);

            double totPayable = Math.Round((totPremium + totLateFee - deposits), 2);

            if (totPayable < 0)
            {
                totPayable = 0;
            }

            if (Math.Round((totPremium + totLateFee),2) == Math.Round(double.Parse(litTotDueAmt.Text),2) && totPayable == Math.Round(double.Parse(litPayableDue.Text), 2) && PayPremValidator.IsValid)
            {
                retVal = true;
            }
            else
            {
                retVal = false;
            }

        }
        catch
        {
            retVal = false;
        }

        return retVal;
    }

    protected void checkAddtAmt(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";


        InfoValidator validator = new InfoValidator();

        validator.checkAmountField(txtAddtAmt.Text, out status, out message);
        if (status == 0)
        {
            args.IsValid = true;
            btnPayPrem.Focus();
        }
        else
        {
            args.IsValid = false;
            addtAmtValidator.ErrorMessage = message;
            txtAddtAmt.Focus();
        }
    }
}