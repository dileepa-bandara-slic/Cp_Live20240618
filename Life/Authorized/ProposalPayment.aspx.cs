using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProposalPayment : System.Web.UI.Page
{
    public string loginNIC;
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    private void ClearTextValues()
    {
        this.lit_name.Text = "";
        this.lit_nic.Text = "";
        this.lit_poltype.Text = "";
        this.lit_SumAssured.Text = "";
        this.btn_submit.Enabled = false;
        this.txt_amount.Text = "";
        this.txt_policy_fee.Text = "";
    }

    protected void txtProposalNumber_TextChanged(object sender, EventArgs e)
    {
        this.lblError.Text = "";
        if (!this.txt_propNum.Text.Equals(""))
        {
            CustProfile profile = new CustProfile(Page.User.Identity.Name);
            //CustProfile profile = new CustProfile("NiroshaPP55");
            string loginNIC = profile.O_nicNo;

            LifeProposal lfPropObj4 = new LifeProposal(this.txt_propNum.Text.Trim());
            if (loginNIC.Equals(lfPropObj4.isIdMatch(long.Parse(this.txt_propNum.Text.Trim())))) //if prop nic match login nic
            {
                // Validate to be only numeric
                LifeProposal lfPropObj = new LifeProposal(this.txt_propNum.Text.Trim());

                if (lfPropObj.O_Mesg.Equals("success"))
                {
                    //if (lfPropObj.checkPendingProposal(long.Parse(this.txt_propNum.Text.Trim())))
                    //{
                    this.lit_name.Text = lfPropObj.name;
                    this.lit_nic.Text = lfPropObj.nic; 
                    this.lit_poltype.Text = lfPropObj.getPolicyType(lfPropObj.prop_table);
                    this.lit_SumAssured.Text = lfPropObj.sum_Assured.ToString("N2");
                    this.btn_submit.Enabled = true;
                    this.txt_policy_fee.Text = lfPropObj.getPolicyFee().ToString("N2");

                    if (lfPropObj.prop_table == 59)
                    {
                        this.txt_amount.Text = lfPropObj.premium.ToString("N2");
                        //this.txt_amount.Enabled = false;
                    }

                    //}
                    //else
                    //{
                    //    this.lblError.Text = "This is not a pending policy.";
                    //    this.lit_name.Text = "";
                    //    this.lit_nic.Text = "";
                    //    this.lit_poltype.Text = "";
                    //    this.lit_SumAssured.Text = "";
                    //    this.btn_submit.Focus();
                    //    this.btn_submit.Enabled = false;
                    //}
                }
                else
                {
                    this.propCustomVal.IsValid = false;
                    this.lit_name.Text = "";
                    this.lit_nic.Text = "";
                    this.lit_poltype.Text = "";
                    this.lit_SumAssured.Text = "";
                    this.btn_submit.Focus();
                    this.btn_submit.Enabled = false;
                    this.txt_amount.Text = "";
                    this.txt_amount.Enabled = true;
                }
            }
            else //if nic not match login nic check agent.agent nic
            {
                LifeProposal lfPropObj5 = new LifeProposal(this.txt_propNum.Text.Trim());
                if (lfPropObj5.checkAgent(loginNIC)) //if nic in agent.agent table
                {
                    LifeProposal lfPropObj6 = new LifeProposal(this.txt_propNum.Text.Trim());
					
					//this.lblError.Text = long.Parse(this.txt_propNum.Text.Trim()) + "/ " + loginNIC;
					//this.lblError.Text = lfPropObj6.checkAgentCode(long.Parse(this.txt_propNum.Text.Trim()), (loginNIC));
					
                    if (loginNIC.Equals(lfPropObj6.checkAgentCode(long.Parse(this.txt_propNum.Text.Trim()), (loginNIC)))) //if agent code match
                    {
                        // Validate to be only numeric
                        LifeProposal lfPropObj = new LifeProposal(this.txt_propNum.Text.Trim());

                        if (lfPropObj.O_Mesg.Equals("success"))
                        {
                            //if (lfPropObj.checkPendingProposal(long.Parse(this.txt_propNum.Text.Trim())))
                            //{
                            this.lit_name.Text = lfPropObj.name;
                            this.lit_nic.Text = lfPropObj.nic;
                            this.lit_poltype.Text = lfPropObj.getPolicyType(lfPropObj.prop_table);
                            this.lit_SumAssured.Text = lfPropObj.sum_Assured.ToString("N2");
                            this.btn_submit.Enabled = true;
                            this.txt_policy_fee.Text = lfPropObj.getPolicyFee().ToString("N2");

                            if (lfPropObj.prop_table == 59)
                            {
                                this.txt_amount.Text = lfPropObj.premium.ToString("N2");
                                //this.txt_amount.Enabled = false;
                            }

                            //}
                            //else
                            //{
                            //    this.lblError.Text = "This is not a pending policy.";
                            //    this.lit_name.Text = "";
                            //    this.lit_nic.Text = "";
                            //    this.lit_poltype.Text = "";
                            //    this.lit_SumAssured.Text = "";
                            //    this.btn_submit.Focus();
                            //    this.btn_submit.Enabled = false;
                            //}
                        }
                        else
                        {
                            this.propCustomVal.IsValid = false;
                            this.lit_name.Text = "";
                            this.lit_nic.Text = "";
                            this.lit_poltype.Text = "";
                            this.lit_SumAssured.Text = "";
                            this.btn_submit.Focus();
                            this.btn_submit.Enabled = false;
                            this.txt_amount.Text = "";
                            this.txt_amount.Enabled = true;
                        }
                    }
                    else
                    {
                        this.lblError.Text = "Invalid Payment E10"; //Agent codes does't match
                        ClearTextValues();
                    }
                }
                else
                {
                    this.lblError.Text = "Invalid Payment E11"; //NIC not in agent.agent table
                    ClearTextValues();
                }

            }
            
        }
        else
        {
            this.propNoRequired.IsValid = false;
            this.lit_name.Text = "";
            this.lit_nic.Text = "";
            this.lit_poltype.Text = "";
            this.lit_SumAssured.Text = "";
            this.btn_submit.Focus();
            this.btn_submit.Enabled = false;
            this.txt_amount.Text = "";
            this.txt_amount.Enabled = true;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        double payAmount=0;

        if(this.txt_amount.Text.Equals(""))
        {
            this.txt_amount.Text = "0.00";
        }

        try
        {
            payAmount = double.Parse(this.txt_amount.Text.Trim());
            if (this.cbx_policy_fee.Checked)
            {
                payAmount = payAmount + double.Parse(this.txt_policy_fee.Text.Trim());
            }
        }
        catch
        {
            this.customValAmount.IsValid = false;
            this.customValAmount.ErrorMessage = "Invalid Amount";
        }

        if (Math.Round(payAmount, 2) <= 0)
        {
            this.customValAmount.IsValid = false;
            this.customValAmount.ErrorMessage = "Paying amount should be greater than 0.";
        }
        else
        {
            LifeProposal lfPropObj2 = new LifeProposal(this.txt_propNum.Text.Trim());
            if(Math.Round(double.Parse(this.txt_amount.Text.Trim()), 2) > lfPropObj2.premium)
            {
                //this.customValAmount.IsValid = false;
                //this.customValAmount.ErrorMessage = "Paying amount should not be greater than premium amount.";
            }
        }

        if (Page.IsValid)
        {
            LifePayment directPayment = new LifePayment();

            //////if (duesTotal > 0)
            //////{
            //////    mesg = directPayment.depositAdjPending(litPolNumber.Text.Trim());
            //////}

            string recptNum_polFee = "";

            //2022.06.02
            string recptNum = "";            
            string paymntCode = "";

            LifeProposal lfPropObj3 = new LifeProposal(this.txt_propNum.Text.Trim());
            if (lfPropObj3.isOnlineNB(long.Parse(this.txt_propNum.Text.Trim())))
            {                
                paymntCode = "D";
            }
            else
            {
                paymntCode = "N";
            }
            //2022.06.02

            //recptNum = directPayment.generate_renwReceiptNo(DateTime.Today.Year, "N");

            recptNum = directPayment.generate_renwReceiptNo(DateTime.Today.Year, paymntCode);

            if (this.cbx_policy_fee.Checked)
            {
                recptNum_polFee = directPayment.generate_renwReceiptNo(DateTime.Today.Year, "M");
            }

            if (!String.IsNullOrEmpty(recptNum))
            {
                CustProfile profile = new CustProfile(Page.User.Identity.Name);

                bool insertedPropData = false;

                if (directPayment.insert_proposal_payments(this.txt_propNum.Text.Trim(), payAmount, Page.User.Identity.Name, "P",
                                        recptNum, this.lit_name.Text.Trim(), paymntCode, profile.O_email, profile.O_mobileNumber, recptNum_polFee))
                {
                    EncryptDecrypt dc = new EncryptDecrypt();
                    Dictionary<string, string> qs = new Dictionary<string, string>();
                    qs.Add("Ref_No", recptNum.Trim());
                    qs.Add("Type", paymntCode); // N-new businees, R-renewals, RV-revivals, D-Digital (Ex: Early Cash)
                    Response.Redirect(dc.url_encrypt("Products/Payment.aspx", qs));
                }
                else
                {
                    this.CustomValAmount2.IsValid = false;
                    this.CustomValAmount2.ErrorMessage = "An error occured, please resubmit with correct details or contact SLIC.";                    
                }
            }
            else
            {
                this.CustomValAmount2.IsValid = false;
                this.CustomValAmount2.ErrorMessage = "An error occured, please resubmit with correct details or contact SLIC.";
            }
        }
    }

    protected void checkAddtAmt(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();

        validator.checkAmountField(this.txt_amount.Text, out status, out message);
        if (status == 0)
        {
            args.IsValid = true;
            this.btn_submit.Focus();
        }
        else
        {
            args.IsValid = false;
            this.customValAmount.ErrorMessage = message;
            this.btn_submit.Focus();
        }
    }
    
}