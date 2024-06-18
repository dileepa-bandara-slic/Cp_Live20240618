using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Life_Authorized_MakePayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;

            //if (rblPayType.SelectedValue == "P")
            //{
            //    lblPol_LoanNum.Text = "Policy Number";
            //    polNumRequired.ErrorMessage = "Policy Number is required";
            //    polNumRequired.ErrorMessage = "Policy Number is required";
            //}
            //else if (rblPayType.SelectedValue == "L")
            //{
            //    lblPol_LoanNum.Text = "Loan Number";
            //    polNumRequired.ErrorMessage = "Loan Number is required";
            //    polNumRequired.ErrorMessage = "Loan Number is required";
            //}

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
        
        LifeCustomer customer = new LifeCustomer();
        DataTable dtPols = new DataTable();
        string mesg = "";
        dtPols = customer.getThirdPartyPolicies(Page.User.Identity.Name, out mesg);
        //gvPolicies.EmptyDataText = mesg;
        gvPolicies.DataSource = dtPols;
        gvPolicies.DataBind();


        if (gvPolicies.Rows.Count > 0)
        {
            Panel3.Visible = true;
            gvPolicies.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            gvPolicies.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone");
            gvPolicies.HeaderRow.Cells[3].Attributes.Add("data-hide", "phone");

            gvPolicies.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        else
        {
            Panel3.Visible = false;
        }
    }

    /*
    protected void rblPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;

        if (rblPayType.SelectedValue == "P")
        {
            lblPol_LoanNum.Text = "Policy Number";
            txtPol_Loan.MaxLength = 8;
            txtPol_Loan.Text = "";
            polNumRequired.ErrorMessage = "Policy Number is required";
            polNumRequired.ErrorMessage = "Policy Number is required";
        }
        else if (rblPayType.SelectedValue == "L")
        {
            lblPol_LoanNum.Text = "Loan Number";
            txtPol_Loan.MaxLength = 11;
            txtPol_Loan.Text = "";
            polNumRequired.ErrorMessage = "Loan Number is required";
            polNumRequired.ErrorMessage = "Loan Number is required";
        }
    }
    */
    /*
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //txtPayPremAmt.Text = "0";
        // txtPayLoanAmt.Text = "0";
        PayPremValidator.IsValid = true;
        PayLoanValidator.IsValid = true;

        if (Page.IsValid)
        {
            Panel1.Visible = false;
            txtAddtAmt.Text = "";
            btnPayPrem.Enabled = false;
            Panel2.Visible = false;
            txtPayLoanAmt.Text = "";
            btnPayLoan.Enabled = false;

            LifeCustomer customer = new LifeCustomer();
            string message = "";

            if (rblPayType.SelectedValue == "P")
            {
                string name = "";
                double payableDueAmt = 0;
                double totDueAmt = 0;
                double deposits = 0;

                message = customer.getPolicyDetails(txtPol_Loan.Text.Trim(), gvDemands, out name, out deposits);

                if (message != "success")
                {
                    PolNumValidator.IsValid = false;
                    PolNumValidator.ErrorMessage = message;
                }
                else
                {
                    litPolNumber.Text = txtPol_Loan.Text.Trim();
                    litName.Text = name;
                    litTotDueAmt.Text = totDueAmt.ToString("N2");
                    litPayableDue.Text = payableDueAmt.ToString("N2");
                    litDeposits.Text = deposits.ToString("N2");
                    Panel1.Visible = true;
                    btnPayPrem.Enabled = true;
                }
            }
            else if (rblPayType.SelectedValue == "L")
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

                message = customer.getLoanDetails(txtPol_Loan.Text.Trim(), "L", out polNum, out loanNum, out grantDate, out grantAmt, out name,
                                                  out nextDueDt, out nextDueCap, out nextDueInt, out lastRepaidDt, out lastRepaidCap, out lastRepaidInt);

                if (message != "success")
                {
                    PolNumValidator.IsValid = false;
                    PolNumValidator.ErrorMessage = message;
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

            if (gvDemands.Rows.Count > 0)
            {
                gvDemands.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
                gvDemands.HeaderRow.Cells[2].Attributes.Add("data-class", "expand");

                gvDemands.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }*/

    /*
    protected void checkPolLoanNum(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        if (rblPayType.SelectedValue == "P")
        {
            validator.validateLifePolNumber(txtPol_Loan.Text, out status, out message);
        }
        else if (rblPayType.SelectedValue == "L")
        {
            validator.validateLifeLoanNumber(txtPol_Loan.Text, out status, out message);
        }

        if (status == 0)
        {
            args.IsValid = true;
            btnSubmit.Focus();
        }
        else
        {
            args.IsValid = false;
            PolNumValidator.ErrorMessage = message;
            txtPol_Loan.Focus();
        }
    }*/

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


        if (Math.Round(payAmt,2) <= 0)
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
                    }
                    else
                    {
                        PayPremValidator.IsValid = false;
                        PayPremValidator.ErrorMessage = "An error occured, please resubmit with correct details or contact SLIC.";
                        // txtPayPremAmt.Focus();
                    }
                }
                else
                {
                    PayPremValidator.IsValid = false;
                    PayPremValidator.ErrorMessage = "An error occured, please resubmit with correct details or contact SLIC.";
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


    protected void btnPayLoan_Click(object sender, EventArgs e)
    {
        double payLoanAmt = 0;

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

            if (Math.Round((totPremium + totLateFee), 2) == Math.Round(double.Parse(litTotDueAmt.Text), 2) && totPayable == Math.Round(double.Parse(litPayableDue.Text), 2) && PayPremValidator.IsValid)
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
    
    protected void rblPayType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void checkPolLoanNum(object source, ServerValidateEventArgs args)
    {

    }
    protected void gvPolicies_SelectedIndexChanged(object sender, EventArgs e)
    {
        string polNum = gvPolicies.SelectedRow.Cells[0].Text;
        log logger = new log();
        logger.write_log("went in 1: " + gvPolicies.SelectedRow.Cells[0].Text);
        //txtPayPremAmt.Text = "0";
        // txtPayLoanAmt.Text = "0";        
        PayPremValidator.IsValid = true;
        PayLoanValidator.IsValid = true;
        rblPayType.SelectedValue = "P";
        
        //if (Page.IsValid)
        //{
            Panel1.Visible = false;
            txtAddtAmt.Text = "";
            btnPayPrem.Enabled = false;
            Panel2.Visible = false;
            txtPayLoanAmt.Text = "";
            btnPayLoan.Enabled = false;
            
            LifeCustomer customer = new LifeCustomer();
            string message = "";
            
            if (rblPayType.SelectedValue == "P")
            {
                string name = "";
                double payableDueAmt = 0;
                double totDueAmt = 0;
                double deposits = 0;

                message = customer.getPolicyDetails(polNum, gvDemands, out name, out deposits);
                
                if (message != "success")
                {
                    PolNumValidator.IsValid = false;
                    PolNumValidator.ErrorMessage = message;
                }
                else
                {                    
                    litPolNumber.Text = polNum;
                    litName.Text = name;
                    litTotDueAmt.Text = totDueAmt.ToString("N2");
                    litPayableDue.Text = payableDueAmt.ToString("N2");
                    litDeposits.Text = deposits.ToString("N2");
                    Panel1.Visible = true;
                    btnPayPrem.Enabled = true;
                }
            }
            else if (rblPayType.SelectedValue == "L")
            {
                string poliNum = "";
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

                message = customer.getLoanDetails(polNum, "L", out poliNum, out loanNum, out grantDate, out grantAmt, out name,
                                                  out nextDueDt, out nextDueCap, out nextDueInt, out lastRepaidDt, out lastRepaidCap, out lastRepaidInt);

                if (message != "success")
                {
                    PolNumValidator.IsValid = false;
                    PolNumValidator.ErrorMessage = message;
                }
                else
                {
                    litLoanNumber.Text = loanNum;
                    litPolNum.Text = poliNum;
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

            if (gvDemands.Rows.Count > 0)
            {
                gvDemands.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
                gvDemands.HeaderRow.Cells[2].Attributes.Add("data-class", "expand");

                gvDemands.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        //}
    }
}