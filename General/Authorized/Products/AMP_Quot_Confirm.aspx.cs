using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;

public partial class General_Authorized_Products_AMP_Quot_Confirm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                LoadGridview();
                string strReq = "";
                strReq = Request.RawUrl;
                string h = strReq.Substring(strReq.Length - 1);
                if (h == "#")
                {
                    //errorMsg = "No url";
                    Response.Redirect("~/Errors/InternalError.htm");
                }
                else
                {
                    strReq = strReq.Substring(strReq.IndexOf('?') + 1);

                    EncryptDecrypt enc = new EncryptDecrypt();
                    strReq = enc.Decrypt(strReq);
                    if (strReq == "#")
                    {
                        //errorMsg = "No Parameters Passed";
                        Response.Redirect("~/Errors/InternalError.htm");
                    }
                    else
                    {
                        Dictionary<string, string> paraList = new Dictionary<string, string>();
                        paraList = enc.getParameters(strReq);

                        if (paraList.ContainsKey("quotNo"))
                        {
                            string quotNo = paraList["quotNo"];

                            litQuotNo.Text = quotNo;
                            CustProfile profile = new CustProfile(Page.User.Identity.Name);
                            litTitle.Text = profile.O_title;
                            litName.Text = profile.O_firstName + " " + profile.O_lastName;
                            litAddress.Text = profile.getFullAddress();
                            litMobileNo.Text = profile.O_mobileNumber;
                            litHomeNo.Text = profile.O_homeNumber;
                            litOfficeNo.Text = profile.O_officeNumber;
                            litEmail.Text = profile.O_email;
                            litNIC.Text = profile.O_nicNo;
                            litPPNo.Text = profile.O_passportNo;
                            litBirthDate.Text = profile.O_dateOfBirth;
                            txtOccupation.Text = profile.O_ocupation;
                            txtStrtDt.Text = DateTime.Now.ToString("yyyy/MM/dd");

                            //txtStrtDt.Text = DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd");
                            //txtStrtDt.Text = "2016/11/04";


                            Proposal prop = new Proposal();
                            DataTable dtMembers = new DataTable();
                            double height = 0;
                            double weight = 0;
                            string gender = "";
                            double premium = 0;
                            string plan = "";
                            double planLimit = 0;

                            string mesg = prop.getAMPQuotationDetails(quotNo, out height, out weight, out gender, out premium, out plan, out planLimit, out dtMembers);

                            Dictionary<string, string> qs = new Dictionary<string, string>();
                            EncryptDecrypt dc = new EncryptDecrypt();


                            if (mesg == "success")
                            {

                                qs.Add("action", "print");
                                qs.Add("quotNo", quotNo);
                                qs.Add("plan", plan);
                                hlPrintA.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);

                                litHeight.Text = height.ToString();
                                litWeight.Text = weight.ToString();
                                litGender.Text = gender;
                                litPremium.Text = premium.ToString("N2");
                                litPlan.Text = plan;
                                litPlanLimit.Text = planLimit.ToString("N2");
                                gvDependents.DataSource = dtMembers;
                                gvDependents.DataBind();

                                gvDependents.Columns[0].Visible = false;
                                //if (profile.O_srilankan != "Y")
                                //{
                                //    gvDependents.Columns[8].Visible = false;
                                //}

                                LoadGridview();                              

                            }
                            else
                            {
                                Response.Redirect("~/Errors/InternalError.htm");
                            }
                        }
                        else
                        {
                            Response.Redirect("~/Errors/InternalError.htm");
                        }
                    }
                }
            }
            catch
            {
                Response.Redirect("~/Errors/InternalError.htm");
            }
        }
    }

    private void LoadGridview()
    {
        if (gvDependents.Rows.Count > 0)
        {
            gvDependents.HeaderRow.Cells[1].Attributes.Add("data-class", "expand");
            gvDependents.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone");
            gvDependents.HeaderRow.Cells[3].Attributes.Add("data-hide", "phone");
            gvDependents.HeaderRow.Cells[4].Attributes.Add("data-hide", "phone");
            gvDependents.HeaderRow.Cells[5].Attributes.Add("data-hide", "phone");
            gvDependents.HeaderRow.Cells[6].Attributes.Add("data-hide", "phone,tablet");
            gvDependents.HeaderRow.Cells[7].Attributes.Add("data-hide", "phone,tablet");
            gvDependents.HeaderRow.Cells[8].Attributes.Add("data-hide", "phone,tablet");
            gvDependents.HeaderRow.Cells[9].Attributes.Add("data-hide", "phone,tablet");

            gvDependents.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }


    protected void RangeValidator1_Init(object sender, EventArgs e)
    {
        ((RangeValidator)sender).MaximumValue = DateTime.Today.AddDays(15).ToString("yyyy/MM/dd");
        ((RangeValidator)sender).MinimumValue = DateTime.Today.ToString("yyyy/MM/dd");
    }

    protected void txtNaturOccup_TextChanged(object sender, EventArgs e)
    {
        
        NaturOccupValidator.Validate();
        //LoadGridview();
    }

    protected void checkStrtDt(object source, ServerValidateEventArgs args)
    {
        LoadGridview();
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateStartDate(txtStrtDt.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            StrtDtValidator.ErrorMessage = "";

        }
        else
        {
            args.IsValid = false;
            StrtDtValidator.ErrorMessage = message;
        }
    }

    protected void checkNaturOccup(object source, ServerValidateEventArgs args)
    {
        LoadGridview();
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateNatureOccupation(txtNaturOccup.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtEmplName.Focus();
        }
        else
        {
            args.IsValid = false;
            NaturOccupValidator.ErrorMessage = message;
            txtNaturOccup.Focus();
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        //LoadGridview();

        StrtDtValidator.Validate();
        OccupValidator.Validate();
        NaturOccupValidator.Validate();
        EmplNameValidator.Validate();

        if (StrtDtValidator.IsValid && NaturOccupValidator.IsValid && EmplNameValidator.IsValid && OccupValidator.IsValid)
        {
            formatMemDetails(gvDependents);
            bool memValid = checkMemberDetails(gvDependents);

            if (memValid)
            {
                try
                {
                    double premium = double.Parse(litPremium.Text);
                    double sumIns = double.Parse(litPlanLimit.Text);
                    double height = double.Parse(litHeight.Text);
                    double weight = double.Parse(litWeight.Text);
                    string endDate = (DateTime.ParseExact(txtStrtDt.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture).AddYears(1).AddDays(-1)).ToString("yyyy/MM/dd");

                    //string endDate = Convert.ToDateTime(txtStrtDt.Text).AddYears(1).AddDays(-1).ToString("yyyy/MM/dd");

                    string gender = "";
                    if (litGender.Text == "Female")
                    {
                        gender = "F";
                    }
                    else if (litGender.Text == "Male")
                    {
                        gender = "M";
                    }

                    Proposal prop = new Proposal();
                    bool retValue = prop.insert_AMP_proposal(Page.User.Identity.Name, litQuotNo.Text, litPlan.Text, premium, txtStrtDt.Text, txtStrtDt.Text,
                                                            endDate, sumIns, litTitle.Text, litName.Text, litAddress.Text, gender, height, weight, litMobileNo.Text, litHomeNo.Text, litOfficeNo.Text,
                                                            litEmail.Text, litNIC.Text, litPPNo.Text, txtOccupation.Text, txtNaturOccup.Text, txtEmplName.Text, gvDependents);

                    if (retValue)
                    {
                        EncryptDecrypt dc = new EncryptDecrypt();
                        Dictionary<string, string> qs = new Dictionary<string, string>();
                        qs.Add("Ref_No", litQuotNo.Text);
                        qs.Add("Type", "N"); // N-new businees, R-renewals
                        qs.Add("Pol_No", litQuotNo.Text);
                        Response.Redirect(dc.url_encrypt("Payment.aspx", qs), false);
                    }
                    else
                    {
                        lblErrMesg.Text = "An error occured, please resubmit with correct details";
                        lblErrMesg.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Errors/InternalError.htm");
                }
            }
        }
    }

    protected void checkEmplName(object source, ServerValidateEventArgs args)
    {
        LoadGridview();
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateEmplName(txtEmplName.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            btnConfirm.Focus();
        }
        else
        {
            args.IsValid = false;
            EmplNameValidator.ErrorMessage = message;
            txtEmplName.Focus();
        }
    }

    protected void txtEmplName_TextChanged(object sender, EventArgs e)
    {
        LoadGridview();
        EmplNameValidator.Validate();
    }

    protected bool checkMemberDetails(GridView gvMembers)
    {
        bool retVal = false;

        lblErrMesg.Text = "";
        int status = -1;
        string message = "";


        foreach (GridViewRow row in gvMembers.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string memId = row.Cells[0].Text.Trim();
                string memName = ((TextBox)row.FindControl("txtName")).Text.Trim();
                string memNic = ((TextBox)row.FindControl("txtNIC")).Text.Trim();
                string memPP = ((TextBox)row.FindControl("txtPP")).Text.Trim();
                string gender = row.Cells[2].Text.Trim().Substring(0,1);
                string dob = row.Cells[3].Text.Trim();
               
                double age = double.Parse(row.Cells[4].Text.Trim());

                /*TextBox tbName = (TextBox)row.FindControl("txtName");
                string txtname = tbName.Text;
                int index1 = txtname.LastIndexOf(',');

                string memName = "";

                if (index1 != -1)
                {
                    index1++;
                    memName = tbName.Text.Substring(index1);
                }

                tbName.Text = memName;

                TextBox tbNIC = (TextBox)row.FindControl("txtNIC");
                string txtnic = tbNIC.Text;
                int index2 = txtnic.LastIndexOf(',');


                string memNic = "";

                if (index2 != -1)
                {
                    index2++;
                    memNic = tbNIC.Text.Substring(index2);
                }

                tbNIC.Text = memNic;
                */

                //tbNIC.Text = tbNIC.Text.Replace(",", "");
                //string memNic = tbNIC.Text;//((TextBox)row.FindControl("txtNIC")).Text.Trim();                

                //string memName = row.Cells[7].Text.Trim();
                //string memNic = row.Cells[8].Text.Trim();
                //string memNic = (gvMembers.FooterRow.FindControl("txtNIC") as TextBox).Text;

                if (memName != "")
                {
                    status = -1;
                    message = "";
                    InfoValidator validator = new InfoValidator();
                    validator.validateMemberName(memName, out status, out message);

                    if (status != 0)
                    {
                        lblErrMesg.Text = message;
                        lblErrMesg.Focus();
                    }
                }
                else
                {
                    lblErrMesg.Text = "Name(s) of all dependents should be entered (Please click on <b>+</b> sign in the above grid to expand member details).";
                    lblErrMesg.Focus();
                }
                if (lblErrMesg.Text == "")
                {
                    if (memNic != "")
                    {
                        status = -1;
                        message = "";
                        InfoValidator validator = new InfoValidator();
                        validator.validateMemberNIC(memNic, out status, out message);

                        if (status != 0)
                        {
                            lblErrMesg.Text = message;
                            lblErrMesg.Focus();
                        }
                        else
                        {
                            if (validator.checkNICWithDob(dob, gender, memNic))
                            {
                                status = 0;
                            }
                            else
                            {
                                status = -1;
                                lblErrMesg.Text = "NIC, Date of birth and gender does not match. Please check on member details entered.";
                                lblErrMesg.Focus();
                            }
                        }
                       
                    }                    
                }
                if (lblErrMesg.Text == "")
                {
                    if (memPP != "")
                    {
                        status = -1;
                        message = "";
                        InfoValidator validator = new InfoValidator();
                        validator.validateMemberPP(memPP, out status, out message);

                        if (status != 0)
                        {
                            lblErrMesg.Text = message;
                        }
                    }
                }

                if (lblErrMesg.Text == "")
                {
                    if (age > 16 && memNic == "" && memPP == "")
                    {
                        lblErrMesg.Text = "Either NIC No. or Passport No. should be entered for members above 16 years.";
                        lblErrMesg.Focus();
                    }
                }
            }

            if (lblErrMesg.Text != "")
            {
                break;
            }
        }

        if (lblErrMesg.Text == "")
        {
            retVal = true;
        }
        return retVal;
    }

    protected void txtStrtDt_TextChanged(object sender, EventArgs e)
    {
        LoadGridview();
        formatMemDetails(gvDependents);
        StrtDtValidator.Validate();
    }

    protected void txtOccup_TextChanged(object sender, EventArgs e)
    {
        //LoadGridview();
        OccupValidator.Validate();
    }

    protected void checkOccup(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateOccupation(txtOccupation.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtNaturOccup.Focus();
        }
        else
        {
            args.IsValid = false;
            OccupValidator.ErrorMessage = message;
            txtOccupation.Focus();
        }
    }

    protected void formatMemDetails(GridView gvMembers)
    {
        foreach (GridViewRow row in gvMembers.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                TextBox tbName = (TextBox)row.FindControl("txtName");
                string txtname = tbName.Text;
                int index1 = txtname.LastIndexOf(',');

                string memName = "";

                if (index1 != -1)
                {
                    index1++;
                    memName = tbName.Text.Substring(index1);
                    tbName.Text = memName;
                }                

                TextBox tbNIC = (TextBox)row.FindControl("txtNIC");
                string txtnic = tbNIC.Text;
                int index2 = txtnic.LastIndexOf(',');


                string memNic = "";

                if (index2 != -1)
                {
                    index2++;
                    memNic = tbNIC.Text.Substring(index2);
                    tbNIC.Text = memNic;
                }

                TextBox tbPP = (TextBox)row.FindControl("txtPP");
                string txtPP = tbPP.Text;
                int index3 = txtPP.LastIndexOf(',');


                string memPP = "";

                if (index3 != -1)
                {
                    index3++;
                    memPP = tbPP.Text.Substring(index3);
                    tbPP.Text = memPP;
                }                
            }
        }
    }

}