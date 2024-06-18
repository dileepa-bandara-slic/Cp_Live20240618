﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class General_Authorized_Products_AMP_Quotation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CustProfile profile = new CustProfile(Page.User.Identity.Name);
            txtDOB.Text = profile.O_dateOfBirth;
            rblGender.SelectedValue = profile.O_gender;
            LoadGridview();
        }
        LoadGridview();
        gvMembers.Columns[0].Visible = false;
    }

    protected void txtDOB_TextChanged(object sender, EventArgs e)
    {
        DOBValidator.Validate();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        LoadGridview();
        lblDOBMsg.Text = "";
        //lblHeightMsg.Text = "";
        //lblWeightMsg.Text = "";
        CategoryValidator.Validate();
        GenderValidator.Validate();
        checkDOBRequired();
        //checkHeightRequired();
        //checkWeightRequired();
        DOBValidator.Validate();
        //heightValidator.Validate();
        //weightValidator.Validate();

        if (lblDOBMsg.Text == "" /*&& lblHeightMsg.Text == "" && lblWeightMsg.Text == "" */ && GenderValidator.IsValid && DOBValidator.IsValid /*&& heightValidator.IsValid && weightValidator.IsValid*/)
        {
            lblErrMesg.Text = "";
            try
            {
                //double bmiValue = 0.00;
                //double bmiRate = 0.00;
                double age = 0.00;

                InfoValidator validator = new InfoValidator();
                string mesg = "success";

                /*if (ddlCategory.SelectedValue != "C")
                {
                    mesg = validator.IsBmiWithinRange(double.Parse(txtHeight.Text), double.Parse(txtWeight.Text), out bmiValue, out bmiRate);
                }*/
                if (mesg == "success")
                {
                    mesg = validator.getAge(ddlCategory.SelectedValue.ToString(), txtDOB.Text, out age);
                }
                /*if (ddlCategory.SelectedValue == "C" && mesg == "success")
                {
                    if (age >= 16)
                    {
                        mesg = validator.IsBmiWithinRange(double.Parse(txtHeight.Text), double.Parse(txtWeight.Text), out bmiValue, out bmiRate);
                    }
                    else
                    {
                        mesg = validator.IsChildBmiWithinRange(age, double.Parse(txtHeight.Text), double.Parse(txtWeight.Text));
                    }
                }*/

                if (mesg == "success")
                {
                    int rowIndex = 0;

                    if (ViewState["CurrentTable"] != null)
                    {
                        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

                        if (dtCurrentTable.Rows.Count > 0)
                        {
                            DataRow[] foundMainLife = dtCurrentTable.Select("Category = 'Main Life'");
                            if (ddlCategory.SelectedValue == "M" && foundMainLife.Length != 0)
                            {
                                mesg = "Main Life cannot be entered twice.";
                            }
                            else if (ddlCategory.SelectedValue == "M")
                            {
                                mesg = "Main Life should be entered first.";
                            }
                            DataRow[] foundSpouse = dtCurrentTable.Select("Category = 'Spouse'");
                            if (ddlCategory.SelectedValue == "S" && foundSpouse.Length != 0)
                            {
                                mesg = "Spouse cannot be entered twice.";
                            }

                            DataRow[] foundChild = dtCurrentTable.Select("Category = 'Child'");
                            if (ddlCategory.SelectedValue == "C" && foundChild.Length >= 5)
                            {
                                mesg = "Maximum number of children is 5.";
                            }

                            if (mesg == "success")
                            {
                                DataRow drCurrentRow = null;
                                // if (dtCurrentTable.Rows.Count > 0)
                                // {
                                drCurrentRow = dtCurrentTable.NewRow();
                                drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                                drCurrentRow["Category"] = ddlCategory.SelectedItem.Text;
                                drCurrentRow["Gender"] = rblGender.SelectedItem.Text;
                                drCurrentRow["Dob"] = txtDOB.Text;
                                drCurrentRow["Age"] = age;
                                /*drCurrentRow["Height"] = double.Parse(txtHeight.Text);
                                drCurrentRow["Weight"] = double.Parse(txtWeight.Text);*/
                                /*drCurrentRow["BmiVal"] = bmiValue;
                                drCurrentRow["MobRate"] = 0.00;
                                drCurrentRow["BaseRate"] = 0.00;
                                drCurrentRow["MaternSlic"] = 0.00;
                                drCurrentRow["FlDiscountedAmt"] = 0.00;*/
                                drCurrentRow["FinalPremium"] = 0.00;
                                /*drCurrentRow["BmiRate"] = bmiRate;
                                drCurrentRow["BmiLoading"] = 0.00;
                                drCurrentRow["MobLoading"] = 0.00;*/

                                rowIndex++;

                                dtCurrentTable.Rows.Add(drCurrentRow);
                                ViewState["CurrentTable"] = dtCurrentTable;

                                gvMembers.DataSource = dtCurrentTable;
                                gvMembers.DataBind();
                                // }
                                SetPreviousData();


                                ddlCategory.Enabled = true;
                                ddlCategory.SelectedValue = "C";
                                rblGender.Enabled = true;
                                rblGender.SelectedIndex = 0;
                                txtDOB.Enabled = true;
                                txtDOB.Text = "";
                                /*txtHeight.Text = "";
                                txtWeight.Text = "";*/
                            }
                            else
                            {
                                lblErrMesg.Text = mesg;
                            }
                        }
                        else
                        {
                            if (ddlCategory.SelectedValue == "M")
                            {
                                DataRow dr = null;

                                dr = dtCurrentTable.NewRow();
                                dr["RowNumber"] = 1;
                                dr["Category"] = ddlCategory.SelectedItem.Text;
                                dr["Gender"] = rblGender.SelectedItem.Text;
                                dr["Dob"] = txtDOB.Text;
                                dr["Age"] = age;
                                /*dr["Height"] = double.Parse(txtHeight.Text);
                                dr["Weight"] = double.Parse(txtWeight.Text);
                                dr["BmiVal"] = bmiValue;
                                dr["MobRate"] = 0.00;
                                dr["BaseRate"] = 0.00;
                                dr["MaternSlic"] = 0.00;
                                dr["FlDiscountedAmt"] = 0.00;*/
                                dr["FinalPremium"] = 0.00;
                                /*dr["BmiRate"] = bmiRate;
                                dr["BmiLoading"] = 0.00;
                                dr["MobLoading"] = 0.00;*/
                                dtCurrentTable.Rows.Add(dr);

                                ViewState["CurrentTable"] = dtCurrentTable;

                                gvMembers.DataSource = dtCurrentTable;
                                gvMembers.DataBind();

                                SetPreviousData();

                                ddlCategory.Enabled = true;
                                ddlCategory.SelectedValue = "S";
                                rblGender.Enabled = true;
                                rblGender.SelectedIndex = 0;
                                txtDOB.Enabled = true;
                                txtDOB.Text = "";
                                /*txtHeight.Text = "";
                                txtWeight.Text = "";*/
                                btnSubmit.Text = "Add members";

                            }
                            else
                            {
                                lblErrMesg.Text = "Main Life should be entered first.";
                            }
                        }
                    }
                    else
                    {
                        if (ddlCategory.SelectedValue == "M")
                        {
                            DataTable dt = new DataTable();
                            DataRow dr = null;
                            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                            dt.Columns.Add(new DataColumn("Category", typeof(string)));
                            dt.Columns.Add(new DataColumn("Gender", typeof(string)));
                            dt.Columns.Add(new DataColumn("Dob", typeof(string)));
                            dt.Columns.Add(new DataColumn("Age", typeof(double)));
                            /*dt.Columns.Add(new DataColumn("Height", typeof(double)));
                            dt.Columns.Add(new DataColumn("Weight", typeof(double)));
                            dt.Columns.Add(new DataColumn("BmiVal", typeof(double)));
                            dt.Columns.Add(new DataColumn("MobRate", typeof(double)));
                            dt.Columns.Add(new DataColumn("BaseRate", typeof(double)));
                            dt.Columns.Add(new DataColumn("MaternSlic", typeof(double)));
                            dt.Columns.Add(new DataColumn("FlDiscountedAmt", typeof(double)));*/
                            dt.Columns.Add(new DataColumn("FinalPremium", typeof(double)));
                            /*dt.Columns.Add(new DataColumn("BmiRate", typeof(double)));
                            dt.Columns.Add(new DataColumn("BmiLoading", typeof(double)));
                            dt.Columns.Add(new DataColumn("MobLoading", typeof(double)));*/
                            dr = dt.NewRow();
                            dr["RowNumber"] = 1;
                            dr["Category"] = ddlCategory.SelectedItem.Text;
                            dr["Gender"] = rblGender.SelectedItem.Text;
                            dr["Dob"] = txtDOB.Text;
                            dr["Age"] = age;
                            /*dr["Height"] = double.Parse(txtHeight.Text);
                            dr["Weight"] = double.Parse(txtWeight.Text);
                            dr["BmiVal"] = bmiValue;
                            dr["MobRate"] = 0.00;
                            dr["BaseRate"] = 0.00;
                            dr["MaternSlic"] = 0.00;
                            dr["FlDiscountedAmt"] = 0.00;*/
                            dr["FinalPremium"] = 0.00;
                            /*dr["BmiRate"] = bmiRate;
                            dr["BmiLoading"] = 0.00;
                            dr["MobLoading"] = 0.00;*/
                            dt.Rows.Add(dr);

                            ViewState["CurrentTable"] = dt;

                            gvMembers.DataSource = dt;
                            gvMembers.DataBind();

                            SetPreviousData();

                            ddlCategory.Enabled = true;
                            ddlCategory.SelectedValue = "S";
                            rblGender.Enabled = true;
                            rblGender.SelectedIndex = 0;
                            txtDOB.Enabled = true;
                            txtDOB.Text = "";
                            /*txtHeight.Text = "";
                            txtWeight.Text = "";*/
                            btnSubmit.Text = "Add members";

                        }
                        else
                        {
                            lblErrMesg.Text = "Main Life should be entered first.";
                        }
                    }

                }
                else
                {
                    lblErrMesg.Text = mesg;
                }
            }
            catch
            {
                lblErrMesg.Text = "Error while adding member.";
                log logger = new log();
                logger.write_log("Failed at AMP_Quotation.aspx: " + e.ToString());
            }
            LoadGridview();
        }


    }

    private void LoadGridview()
    {
        if (gvMembers.Rows.Count > 0)
        {
            gvMembers.HeaderRow.Cells[1].Attributes.Add("data-class", "expand");
            //gvMembers.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone");
            //gvMembers.HeaderRow.Cells[3].Attributes.Add("data-hide", "phone");
            //gvMembers.HeaderRow.Cells[4].Attributes.Add("data-hide", "phone");
            //gvMembers.HeaderRow.Cells[5].Attributes.Add("data-hide", "phone");
            /*gvMembers.HeaderRow.Cells[6].Attributes.Add("data-hide", "phone,tablet");
            gvMembers.HeaderRow.Cells[7].Attributes.Add("data-hide", "phone,tablet");*/

            gvMembers.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Label lblCategory = (Label)gvMembers.Rows[rowIndex].Cells[1].FindControl("lblCategory");
                    Label lblGender = (Label)gvMembers.Rows[rowIndex].Cells[2].FindControl("lblGender");
                    Label lblDOB = (Label)gvMembers.Rows[rowIndex].Cells[3].FindControl("lblDOB");
                    Label lblAge = (Label)gvMembers.Rows[rowIndex].Cells[4].FindControl("lblAge");
                    /* Label lblHeight = (Label)gvMembers.Rows[rowIndex].Cells[5].FindControl("lblHeight");
                     Label lblWeight = (Label)gvMembers.Rows[rowIndex].Cells[6].FindControl("lblWeight");*/

                    lblCategory.Text = dt.Rows[i]["Category"].ToString();
                    lblGender.Text = dt.Rows[i]["Gender"].ToString();
                    lblDOB.Text = dt.Rows[i]["Dob"].ToString();
                    lblAge.Text = dt.Rows[i]["Age"].ToString();
                    /*lblHeight.Text = dt.Rows[i]["Height"].ToString();
                    lblWeight.Text = dt.Rows[i]["Weight"].ToString();*/
                    rowIndex++;
                }

                btnCalc.Visible = true;
            }
            else
            {
                btnCalc.Visible = false;
            }
        }
        LoadGridview();
    }

    protected void gvMembers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count >= 1)
            {
                if (rowIndex <= dt.Rows.Count - 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    ResetRowID(dt);
                }

                ViewState["CurrentTable"] = dt;

                gvMembers.DataSource = dt;
                gvMembers.DataBind();

                //for (int i = 0; i < gvMembers.Rows.Count - 1; i++)
                //{
                //    gvMembers.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                //}

                SetPreviousData();
            }

            if (dt.Rows.Count == 0)
            {
                ddlCategory.Enabled = false;
                ddlCategory.SelectedValue = "M";
                rblGender.Enabled = false;
                txtDOB.Enabled = false;

                CustProfile profile = new CustProfile(Page.User.Identity.Name);
                txtDOB.Text = profile.O_dateOfBirth;
                rblGender.SelectedValue = profile.O_gender;
                /*txtHeight.Text = "";
                txtWeight.Text = "";*/
                btnSubmit.Text = "Submit";
            }
        }
        LoadGridview();
    }

    private void ResetRowID(DataTable dt)
    {
        int rowNumber = 1;
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                row[0] = rowNumber;
                rowNumber++;
            }
        }
        LoadGridview();
    }

    protected void checkCategory(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateCategory(ddlCategory.SelectedValue.ToString(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            rblGender.Focus();
        }
        else
        {
            args.IsValid = false;
            CategoryValidator.ErrorMessage = message;
            ddlCategory.Focus();
        }
    }

    protected void checkGender(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateGenderBasic(Page.User.Identity.Name.ToUpper(), ddlCategory.SelectedValue.ToString(), rblGender.SelectedValue.ToString().ToUpper(), out status, out message);
        if (status == 0)
        {
            args.IsValid = true;
            txtDOB.Focus();
        }
        else
        {
            args.IsValid = false;
            GenderValidator.ErrorMessage = message;
            rblGender.Focus();
        }
    }

    protected void checkDOB(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateDobForAMP(Page.User.Identity.Name.ToUpper(), ddlCategory.SelectedValue.ToString(), txtDOB.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            btnSubmit.Focus();
        }
        else
        {
            args.IsValid = false;
            DOBValidator.ErrorMessage = message;
            txtDOB.Focus();
        }
    }

    protected void checkDOBRequired()
    {
        if (txtDOB.Text == "")
        {
            lblDOBMsg.Text = "Date of Birth should be entered";
        }
    }

    /*
    protected void checkHeight(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateHeight(txtHeight.Text.Trim(), out status, out message);
        if (status == 0)
        {
            args.IsValid = true;
            txtWeight.Focus();
        }
        else
        {
            args.IsValid = false;
            heightValidator.ErrorMessage = message;
            txtHeight.Focus();
        }
    }

    protected void checkHeightRequired()
    {
        if (txtHeight.Text == "")
        {
            lblHeightMsg.Text = "Height should be entered";
        }
    }

    protected void checkWeight(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateWeight(txtWeight.Text.Trim(), out status, out message);
        if (status == 0)
        {
            args.IsValid = true;
            btnSubmit.Focus();
        }
        else
        {
            args.IsValid = false;
            weightValidator.ErrorMessage = message;
            txtWeight.Focus();
        }
    }

    protected void checkWeightRequired()
    {
        if (txtWeight.Text == "")
        {
            lblWeightMsg.Text = "Weight should be entered";
        }
    }
    */

    protected void btnCalc_Click(object sender, EventArgs e)
    {
        double premiumA = 0;
        double premiumB = 0;
        double premiumC = 0;
        double premiumD = 0;
        double premiumE = 0;

        lblErrMesg2.Text = "";
        DataTable dtCurrentTable = new DataTable();
        if (ViewState["CurrentTable"] != null)
        {
            dtCurrentTable = (DataTable)ViewState["CurrentTable"];

            DataRow[] foundMainLife = dtCurrentTable.Select("Category = 'Main Life'");
            if (foundMainLife.Length != 0)
            {
                Proposal prop = new Proposal();
                string mesg = prop.getAllAMPPremiums(dtCurrentTable, out premiumA, out premiumB, out premiumC, out premiumD, out premiumE);

                if (mesg == "success")
                {
                    //Response.Redirect("http://www.google.lk?q=test"+mesg);
                    lblPremiumA.Text = premiumA.ToString("N2");
                    lblPremiumB.Text = premiumB.ToString("N2");
                    lblPremiumC.Text = premiumC.ToString("N2");
                    lblPremiumD.Text = premiumD.ToString("N2");
                    //lblPremiumE.Text = premiumE.ToString("N2");

                    Panel1.Visible = false;
                    Panel2.Visible = true;

                    string quotNo = "";
                    mesg = prop.createAMPQuotation(Page.User.Identity.Name, dtCurrentTable, "A", out quotNo);
                    
                    if (mesg == "success")
                    {
                        //Response.Redirect("http://www.google.lk?q=" + mesg);
                        EncryptDecrypt dc = new EncryptDecrypt();
                        Dictionary<string, string> qs = new Dictionary<string, string>();
                        qs.Add("action", "buy");
                        qs.Add("quotNo", quotNo);
                        qs.Add("plan", "A");
                        hlBuyA.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        qs.Remove("plan");
                        qs.Add("plan", "B");
                        hlBuyB.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        qs.Remove("plan");
                        qs.Add("plan", "C");
                        hlBuyC.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        qs.Remove("plan");
                        qs.Add("plan", "D");
                        hlBuyD.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        //qs.Remove("plan");
                        //qs.Add("plan", "E");
                        //hlBuyE.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);

                        qs.Remove("action");
                        qs.Add("action", "print");
                        qs.Remove("plan");
                        qs.Add("plan", "A");
                        hlPrintA.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        qs.Remove("plan");
                        qs.Add("plan", "B");
                        hlPrintB.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        qs.Remove("plan");
                        qs.Add("plan", "C");
                        hlPrintC.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        qs.Remove("plan");
                        qs.Add("plan", "D");
                        hlPrintD.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        //qs.Remove("plan");
                        //qs.Add("plan", "E");
                        //hlPrintE.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                    }
                    else
                    {
                        lblErrMesg2.Text = mesg;
                    }
                }
                else
                {
                    lblErrMesg2.Text = mesg;
                }
            }
            else
            {
                lblErrMesg2.Text = "Main Life should be entered.";
            }
        }
    }

    protected void hlBuyA_PreRender(object sender, EventArgs e)
    {
        //if (Panel2.Visible == true)
        //{
        //    if (ViewState["CurrentTable"] != null)
        //    {
        //        DataTable dtCurrentTable = new DataTable();
        //        dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        //        string quotNo = "";

        //        Proposal prop = new Proposal();
        //        prop.createQuotation(Page.User.Identity.Name, dtCurrentTable, "A", out quotNo);

        //        hlBuyA.NavigateUrl = "~/General/Authorized/Products/AMP_Quot_Confirm.aspx?quotNo=" + quotNo;
        //    }
        //}
    }

    
}