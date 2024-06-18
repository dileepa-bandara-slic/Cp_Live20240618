using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

public partial class Life_Authorized_PolicyRevivalRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ULCustomer custmr = new ULCustomer();
            this.tbxMobileNum.Text = custmr.getMobileNumbr(Page.User.Identity.Name);
            this.tbxMobileNum.Enabled = false;
        }
    }

    protected void checkNIC(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        if (!this.txtPolicyHolderNIC.Text.Trim().Equals(""))
        {
            InfoValidator validator = new InfoValidator();
            validator.validateNIC_PolicyRevival(this.txtPolicyHolderNIC.Text.Trim().ToUpper(), out status, out message);

            if (status == 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
                NICValidator.ErrorMessage = message;
                txtPolicyHolderNIC.Focus();
            }
        }
    }

    protected void txtPolicyHolderNIC_TextChanged(object sender, EventArgs e)
    {
        NICValidator.Validate();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        //Page.Validate("GP102");
        //if (!Page.IsValid)
        //{

            string polNo = this.txtPolicyNumber.Text;
            string polHolderName = this.txtPolicyHolderName.Text;
            string polHolderStatus = this.txtPolicyHolderStatus.Text;
            string polHolderNIC = this.txtPolicyHolderNIC.Text;
            string mobileNumber = this.tbxMobileNum.Text;

            NICValidator.Validate();

      
            ULCustomer custmr1 = new ULCustomer();
            string email = custmr1.getEmailAdrs(Page.User.Identity.Name);

            Revival_Life revLife = new Revival_Life();

            string rev_type = "";
            if (cbxOR.Checked)
            {
                rev_type = "OR";
            }
            else if (cbxSR.Checked)
            {
                rev_type = "SR";
            }

            if (revLife.AlreadyRevivalRequested(polNo))
            {
                lblPayStatus.Text = "This policy has been already requested for revival";
                lblPayStatus.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                bool retVal = revLife.saveRequestData(Page.User.Identity.Name, email, int.Parse(polNo), polHolderName, polHolderNIC, rev_type, mobileNumber, polHolderStatus);
                if (retVal)
                {
                    Print_pdf_Life printPDF = new Print_pdf_Life();

                    int ret = printPDF.GeneratePolicyRevivalPDF(int.Parse(polNo), polHolderName, polHolderNIC, mobileNumber, cbxOR.Checked, cbxSR.Checked, email);
                    if (ret == 1)
                    {

                        bool smsSent = revLife.SendRevSMS(mobileNumber, "Thank you for the request to revive your policy " + polNo + ". We will let you know about the documents required for us to consider the revival.");

                        //if (smsSent)
                        //{
                            Db_Email emailDB = new Db_Email();
                            bool emailSent = emailDB.send_html_email_withLogo(email, "Online Policy Revival", "", "Dear Sir/Madam, <br/><br/>Thank you for the request to revive your policy " + polNo + ". We will let you know about the documents required for us to consider the revival through e mail.");
                            if (emailSent)
                            {
                                lblPayStatus.Text = "Policy revival request was sent to SLIC.";
                                lblPayStatus.ForeColor = System.Drawing.Color.Green;
                                this.txtPolicyHolderName.Enabled = false;
                                this.txtPolicyHolderStatus.Enabled = false;
                                this.txtPolicyHolderNIC.Enabled = false;
                                this.txtPolicyNumber.Enabled = false;
                                this.tbxMobileNum.Enabled = false;
                                this.cbxOR.Enabled = false;
                                this.cbxSR.Enabled = false;
                                this.btnSubmit.Visible = false;
                                this.lblPayStatus.Focus();
                            }
                            else
                            {
                                lblPayStatus.Text = "Request confirmation email was not sent to customer";
                                log logger = new log();
                                logger.write_log("Failed at send_html_email - Policy revival request");
                            }
                        //}
                        //else
                        //{
                        //    lblPayStatus.Text = "Request confirmation SMS was not sent to customer";
                        //    log logger = new log();
                        //    logger.write_log("Failed at SendRevSMS - Policy revival request");
                        //}
                    }
                    else
                    {
                        lblPayStatus.Text = "Request was not sent to SLIC";
                        log logger = new log();
                        logger.write_log("Failed at GeneratePolicyRevivalPDF - Policy revival request");
                    }
                }
                else
                {
                    lblPayStatus.Text = "Request was not sent to SLIC";
                    log logger = new log();
                    logger.write_log("Failed at saveRequestData - Policy revival request");
                }
            }
        //}
    }

    protected void cbxOR_CheckedChanged(object sender, EventArgs e)
    {
        if (cbxOR.Checked)
        {
            cbxSR.Checked = false;
        }
        else
        {
            cbxSR.Checked = true;
        }
    }

    protected void cbxSR_CheckedChanged(object sender, EventArgs e)
    {
        if (cbxSR.Checked)
        {
            cbxOR.Checked = false;
        }
        else
        {
            cbxOR.Checked = true;
        }
    }

    protected void txtPolicyNumber_TextChanged(object sender, EventArgs e)
    {
        this.lblPayStatus.Text = "";
        this.lblError.Text = "";
        if (this.txtPolicyNumber.Text != "")
        {
            Revival_Life polInfo = new Revival_Life(txtPolicyNumber.Text.Trim());            

            if (polInfo.O_Mesg == "success")
            {
                if (polInfo.registeredUnderUserName(Page.User.Identity.Name, txtPolicyNumber.Text.Trim()))
                {
                    this.txtPolicyHolderName.Text = polInfo.O_name;
                    this.txtPolicyHolderName.Enabled = false;
                    this.txtPolicyHolderStatus.Text = polInfo.O_status;
                    this.txtPolicyHolderStatus.Enabled = false;
                    this.txtPolicyHolderNIC.Text = polInfo.O_nicNo;
                    this.txtPolicyHolderNIC.Enabled = false;
                    btnSubmit.Enabled = true;
                }
                else
                {
                    CustomValidator1.IsValid = false;
                    this.txtPolicyHolderName.Text = "";
                    this.txtPolicyHolderStatus.Text = "";
                    this.txtPolicyHolderNIC.Text = "";
                    txtPolicyNumber.Focus();
                    btnSubmit.Enabled = false;
                }
            }
            else
            {
                long n;
                if (long.TryParse(txtPolicyNumber.Text.Trim(), out n))
                {
                    LifePolicy lifePol = new LifePolicy();
                    if ((lifePol.return_status(long.Parse(txtPolicyNumber.Text.Trim()))).Equals("I"))
                    {
                        this.lblError.Text = "policy number is inforce.";
                        this.txtPolicyHolderName.Text = "";
                        this.txtPolicyHolderStatus.Text = "";
                        this.txtPolicyHolderNIC.Text = "";
                        txtPolicyNumber.Focus();
                        btnSubmit.Enabled = false;
                    }
                    else
                    {
                        PolNoValidator.IsValid = false;
                        this.txtPolicyHolderName.Text = "";
                        this.txtPolicyHolderStatus.Text = "";
                        this.txtPolicyHolderNIC.Text = "";
                        txtPolicyNumber.Focus();
                        btnSubmit.Enabled = false;
                    }
                }
                else
                {
                    PolNoValidator.IsValid = false;
                    this.txtPolicyHolderName.Text = "";
                    this.txtPolicyHolderStatus.Text = "";
                    this.txtPolicyHolderNIC.Text = "";
                    txtPolicyNumber.Focus();
                    btnSubmit.Enabled = false;
                }
            }
        }
    }
}