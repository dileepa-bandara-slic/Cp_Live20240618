using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;

public partial class Life_Authorized_PolicyRevival : System.Web.UI.Page
{
    public bool uploaded_15E = false;
    public bool uploaded_9E = false;
    public bool uploaded_JMER = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        LifeCustomer customer = new LifeCustomer();
        DataTable dtPols = new DataTable();
        string mesg = "";

        if (!IsPostBack)
        {
            LoadGridview();

        }

    
        dtPols = customer.getRevivalPolicies(Page.User.Identity.Name, out mesg);
        gvRevPolicies.DataSource = dtPols;
        gvRevPolicies.DataBind();        

        if (gvRevPolicies.Rows.Count > 0)
        {
            Panel3.Visible = true;

            //gvRevPolicies.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            //gvRevPolicies.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone");

            //gvRevPolicies.HeaderRow.TableSection = TableRowSection.TableHeader;

            gvRevPolicies.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            gvRevPolicies.HeaderRow.Cells[1].Attributes.Add("data-hide", "phone");
            gvRevPolicies.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone,tablet");

            gvRevPolicies.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        else
        {
            Panel3.Visible = false;
        }
    }

    protected void checkAddtAmt(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";


        InfoValidator validator = new InfoValidator();

        validator.checkAmountField(txtPayAmt.Text, out status, out message);
        if (status == 0)
        {
            args.IsValid = true;
            btnPay.Focus();
        }
        else
        {
            args.IsValid = false;
            addtAmtValidator.ErrorMessage = message;
            txtPayAmt.Focus();
        }
    }

    protected void checkPolLoanNum(object source, ServerValidateEventArgs args)
    {

    }

    protected void gvRevPolicies_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGridview();

        Panel1.Visible = false;
        string polNum = gvRevPolicies.SelectedRow.Cells[0].Text;
        log logger = new log();
        logger.write_log("went in 1: " + gvRevPolicies.SelectedRow.Cells[0].Text);

        txtPayAmt.Text = "";
        btnPay.Enabled = true;

        LifeCustomer customer = new LifeCustomer();
        string message = "";
        
        string name = "";
        string status = "";
        double amount_to_pay = 0;
        string expiry_date = "";
        int rec_seq_num = 0;

        message = customer.getRevivalPolicyDetails(polNum, Page.User.Identity.Name, gvRevPolicies, out name, out amount_to_pay, out expiry_date, out rec_seq_num, out status);

        if (message != "success")
        {
            PolNumValidator.IsValid = false;
            PolNumValidator.ErrorMessage = message;
        }
        else
        {
            litPolNumber.Text = polNum;
            litName.Text = name;
            if (!name.ToUpper().StartsWith(status.ToUpper()))
            {
                litStatus.Text = status;
            }
            //litStatus.Text = status;
            litTotDueAmt.Text = amount_to_pay.ToString("N2");
            litExpiryDate.Text = expiry_date;
            hdfSeqNo.Value = rec_seq_num.ToString();

            Panel1.Visible = true;           

            Revival_Life revvl_life = new Revival_Life();
            string mesg = "success";

            if (revvl_life.get15E_reqstd_list(int.Parse(litPolNumber.Text.Trim()), out mesg).Rows.Count > 0 ||
                    revvl_life.get9E_reqstd_list(int.Parse(litPolNumber.Text.Trim()), out mesg).Rows.Count > 0 ||
                        revvl_life.getJMER_reqstd_list(int.Parse(litPolNumber.Text.Trim()), out mesg).Rows.Count > 0 ||
                revvl_life.getCovid19_reqstd_list(int.Parse(litPolNumber.Text.Trim()), out mesg).Rows.Count > 0)
            {
                btnPay.Text = "Upload Documents";
            }

            btnPay.Enabled = true;
        }

        LoadGridview();
    }

    private void LoadGridview()
    {
        if (gvRevPolicies.Rows.Count > 0)
        {
            gvRevPolicies.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            gvRevPolicies.HeaderRow.Cells[1].Attributes.Add("data-hide", "phone");
            gvRevPolicies.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone,tablet");

            gvRevPolicies.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void btnPay_Click(object sender, EventArgs e)
    {
        LoadGridview();

        double payAmt = 0;        
        double addtAmt = 0;
        double duesTotal = 0;

        if (txtPayAmt.Text.Trim() == "")
        {
            txtPayAmt.Text = "0.00";
        }        

        try
        {            
            duesTotal = double.Parse(litTotDueAmt.Text.Trim());            
            payAmt = double.Parse(txtPayAmt.Text.Trim());            

            if (payAmt > duesTotal)
            {
                addtAmt = payAmt - duesTotal;
            }
        }
        catch
        {
            addtAmtValidator.IsValid = false;
            addtAmtValidator.ErrorMessage = "Invalid amount.";
        }


        //if (Math.Round(payAmt, 2) <= 0)
        if (btnPay.Text.Equals("Pay") && Math.Round(payAmt, 2) <= 0)
        {
            addtAmtValidator.IsValid = false;
            addtAmtValidator.ErrorMessage = "Total paid amount should be greater than 0.";
        }


        if (Page.IsValid)
        {
            string mesg = "success";
            Revival_Life revvl_life = new Revival_Life();

            string mesg2 = "";

            if (revvl_life.get15E_reqstd_list(int.Parse(litPolNumber.Text.Trim()), out mesg2).Rows.Count > 0 ||
                    revvl_life.get9E_reqstd_list(int.Parse(litPolNumber.Text.Trim()), out mesg2).Rows.Count > 0 ||
                        revvl_life.getJMER_reqstd_list(int.Parse(litPolNumber.Text.Trim()), out mesg2).Rows.Count > 0 ||
                revvl_life.getCovid19_reqstd_list(int.Parse(litPolNumber.Text.Trim()), out mesg2).Rows.Count > 0)
            {

                EncryptDecrypt dc = new EncryptDecrypt();
                Dictionary<string, string> qs = new Dictionary<string, string>();
                qs.Add("Pol_No", litPolNumber.Text.Trim());
                qs.Add("Pay_Amount", payAmt.ToString());
                qs.Add("Total_Due", duesTotal.ToString());
                qs.Add("PH_Name", litName.Text.Trim());
                qs.Add("PH_Status", litStatus.Text.Trim());
                qs.Add("Seq_No", this.hdfSeqNo.Value);
                Response.Redirect(dc.url_encrypt("PolicyRevival_DocsUpload.aspx", qs));
            }
            else
            {
                

                LifePayment directPayment = new LifePayment();

                if (duesTotal > 0)
                {
                    mesg = directPayment.depositAdjPending(litPolNumber.Text.Trim());
                }

                Revival_Life revl = new Revival_Life();                

                if (mesg == "success")
                {
                    string recptNo = directPayment.generate_renwReceiptNo(Convert.ToInt32(DateTime.Today.ToString("yyyy")), "R");

                    if (!String.IsNullOrEmpty(recptNo))
                    {
                        CustProfile profile = new CustProfile(Page.User.Identity.Name);

                        if (directPayment.insert_revival(litPolNumber.Text.Trim(), payAmt, Page.User.Identity.Name, "P",
                                            recptNo, litName.Text.Trim(), "R", duesTotal, addtAmt, profile.O_email, profile.O_mobileNumber, int.Parse(this.hdfSeqNo.Value)))
                        {
                            EncryptDecrypt dc = new EncryptDecrypt();
                            Dictionary<string, string> qs = new Dictionary<string, string>();
                            qs.Add("Ref_No", recptNo.Trim());
                            qs.Add("Type", "RV"); // N-new businees, R-renewals
                            Response.Redirect(dc.url_encrypt("Products/Payment.aspx", qs));
                        }
                        else
                        {
                            PayPremValidator.IsValid = false;
                            PayPremValidator.ErrorMessage = "An error occured, please resubmit with correct details or contact SLIC.";
                            //txtPayPremAmt.Focus();
                        }
                    }
                    else
                    {
                        PayPremValidator.IsValid = false;
                        PayPremValidator.ErrorMessage = "An error occured, please resubmit with correct details or contact SLIC.";
                        //txtPayPremAmt.Focus();
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

    
}


//}