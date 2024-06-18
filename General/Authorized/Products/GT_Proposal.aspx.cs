using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class General_Authorized_Products_GT_Proposal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CustProfile profile = new CustProfile(Page.User.Identity.Name);
            txtDOB.Text = profile.O_dateOfBirth;
            rblGender.SelectedValue = profile.O_gender;
            FillDropDownList(ddlDestination);
            SetInitialRow();
            FillDDLCategory("F");

            LoadGridView();
        }

        gvMembers.Columns[0].Visible = false;

        LoadGridView();
    }

    private void LoadGridView()
    {
        
        if (gvVisitCntrs.Rows.Count > 0)
        {
            gvVisitCntrs.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            gvVisitCntrs.HeaderRow.Cells[1].Attributes.Add("data-hide", "phone");
            gvVisitCntrs.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone");
            gvVisitCntrs.HeaderRow.Cells[3].Attributes.Add("data-hide", "phone,tablet");

            gvVisitCntrs.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        //if (gvPlanDetails.Rows.Count > 0)
        //{
        //    gvPlanDetails.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
        //    gvPlanDetails.HeaderRow.Cells[5].Attributes.Add("data-hide", "phone");
        //    gvPlanDetails.HeaderRow.Cells[6].Attributes.Add("data-hide", "phone,tablet");
        //    gvPlanDetails.HeaderRow.Cells[7].Attributes.Add("data-hide", "phone,tablet");
        //    gvPlanDetails.HeaderRow.Cells[8].Attributes.Add("data-hide", "phone,tablet");
        //    gvPlanDetails.HeaderRow.Cells[9].Attributes.Add("data-hide", "phone,tablet");
        //    gvPlanDetails.HeaderRow.Cells[10].Attributes.Add("data-hide", "phone,tablet");
        //    gvPlanDetails.HeaderRow.Cells[11].Attributes.Add("data-hide", "phone,tablet");
        //    gvPlanDetails.HeaderRow.Cells[12].Attributes.Add("data-hide", "phone,tablet");

        //    gvPlanDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        //}

        if (gvMembers.Rows.Count > 0)
        {
            //gvMembers.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            gvMembers.HeaderRow.Cells[1].Attributes.Add("data-class", "expand");
            gvMembers.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone");
            gvMembers.HeaderRow.Cells[3].Attributes.Add("data-hide", "phone");
            gvMembers.HeaderRow.Cells[4].Attributes.Add("data-hide", "phone,tablet");
            gvMembers.HeaderRow.Cells[5].Attributes.Add("data-hide", "phone,tablet");

            gvMembers.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void txtDOB_TextChanged(object sender, EventArgs e)
    {
        DOBValidator.Validate();
        //LoadGridView();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        LoadGridView();

        DestValidator.Validate();
        frmDtValidator.Validate();
        toDtValidator.Validate();
        planTypValidator.Validate();
        CategoryValidator.Validate();
        GenderValidator.Validate();
        DOBValidator.Validate();

        if (DestValidator.IsValid && frmDtValidator.IsValid &&
            toDtValidator.IsValid && planTypValidator.IsValid &&
            CategoryValidator.IsValid && GenderValidator.IsValid && DOBValidator.IsValid)
        {
            lblErrMesg.Text = "";
            try
            {
                double age = 0.00;

                InfoValidator validator = new InfoValidator();
                string mesg = "success";
                if (ddlPlanType.SelectedValue == "I" && ddlCategory.SelectedValue != "M")
                {
                    mesg = "Only Main Life can be entered for Individual plan";
                }
                else if (ddlPlanType.SelectedValue == "G" && ddlCategory.SelectedValue != "M" && ddlCategory.SelectedValue != "N")
                {
                    mesg = "Invalid category for Group plan";
                }
                mesg = validator.getAgeForGT(Page.User.Identity.Name.ToUpper(), ddlCategory.SelectedValue.ToString(), txtDOB.Text, txtToDate.Text, out age);

                if (mesg == "success")
                {
                    int rowIndex = 0;

                    if (ViewState["CurrentTable"] != null)
                    {
                        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

                        if (dtCurrentTable.Rows.Count > 0)
                        {
                            DataRow[] foundMainLife = dtCurrentTable.Select("Category = 'Main Life (Self)'");
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

                            //DataRow[] foundChild = dtCurrentTable.Select("Category = 'Child'");
                            //if (ddlCategory.SelectedValue == "C" && foundChild.Length >= 5)
                            //{
                            //    mesg = "Maximum number of children is 5.";
                            //}

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
                                drCurrentRow["BasePremium"] = 0.00;

                                rowIndex++;

                                dtCurrentTable.Rows.Add(drCurrentRow);
                                ViewState["CurrentTable"] = dtCurrentTable;

                                gvMembers.DataSource = dtCurrentTable;
                                gvMembers.DataBind();
                                LoadGridView();
                                // }
                                SetPreviousData();
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
                                dr["BasePremium"] = 0.00;
                                dtCurrentTable.Rows.Add(dr);

                                ViewState["CurrentTable"] = dtCurrentTable;

                                gvMembers.DataSource = dtCurrentTable;
                                gvMembers.DataBind();
                                LoadGridView();

                                SetPreviousData();
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
                            dt.Columns.Add(new DataColumn("BasePremium", typeof(double)));
                            dr = dt.NewRow();
                            dr["RowNumber"] = 1;
                            dr["Category"] = ddlCategory.SelectedItem.Text;
                            dr["Gender"] = rblGender.SelectedItem.Text;
                            dr["Dob"] = txtDOB.Text;
                            dr["Age"] = age;
                            dr["BasePremium"] = 0.00;
                            dt.Rows.Add(dr);

                            ViewState["CurrentTable"] = dt;

                            gvMembers.DataSource = dt;
                            gvMembers.DataBind();
                            LoadGridView();

                            SetPreviousData();
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

                    lblCategory.Text = dt.Rows[i]["Category"].ToString();
                    lblGender.Text = dt.Rows[i]["Gender"].ToString();
                    lblDOB.Text = dt.Rows[i]["Dob"].ToString();
                    lblAge.Text = dt.Rows[i]["Age"].ToString();

                    rowIndex++;
                }

                btnCalc.Visible = true;
            }
            else
            {
                btnCalc.Visible = false;
            }
        }

        //LoadGridView();
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
                LoadGridView();

                //for (int i = 0; i < gvMembers.Rows.Count - 1; i++)
                //{
                //    gvMembers.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                //}

                SetPreviousData();
            }
        }
        //LoadGridView();
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
        //LoadGridView();
    }

    protected void checkPlanType(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validatePlanType(ddlPlanType.SelectedValue.ToString(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            ddlCategory.Focus();
        }
        else
        {
            args.IsValid = false;
            planTypValidator.ErrorMessage = message;
            ddlPlanType.Focus();
        }
        //LoadGridView();
    }

    protected void ddlPlanType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        if (ViewState["CurrentTable"] != null)
        {
            dt = (DataTable)ViewState["CurrentTable"];
        }
        dt.Clear();
        gvMembers.DataSource = dt;
        gvMembers.DataBind();

        SetPreviousData();

        FillDDLCategory(ddlPlanType.SelectedValue);
        //LoadGridView();
    }

    private ArrayList GetCategories(string planType)
    {
        ArrayList arr = new ArrayList();

        if (planType == "I")
        {
            arr.Add(new ListItem("Main Life (Self)", "M"));
        }
        else if (planType == "F")
        {
            arr.Add(new ListItem("Main Life (Self)", "M"));
            arr.Add(new ListItem("Spouse", "S"));
            arr.Add(new ListItem("Child", "C"));
            arr.Add(new ListItem("Other", "N"));
        }
        else if (planType == "G")
        {
            arr.Add(new ListItem("Main Life (Self)", "M"));
            arr.Add(new ListItem("Other", "N"));
        }

        return arr;
    }

    private void FillDDLCategory(string planType)
    {
        ArrayList arr = GetCategories(planType);

        ddlCategory.Items.Clear();
        foreach (ListItem item in arr)
        {
            ddlCategory.Items.Add(item);
        }
        //LoadGridView();
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
        //LoadGridView();
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
        //LoadGridView();
    }

    protected void checkDOB(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateDobForGT(Page.User.Identity.Name.ToUpper(), ddlCategory.SelectedValue.ToString(), txtDOB.Text, out status, out message);

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
        //LoadGridView();
    }

    protected void checkDestination(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateDestination(ddlDestination.SelectedValue.ToString(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtFrmDate.Focus();
        }
        else
        {
            args.IsValid = false;
            DestValidator.ErrorMessage = message;
            ddlDestination.Focus();
        }
        //LoadGridView();
    }

    protected void checkFrmDate(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateFromDate(txtFrmDate.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtToDate.Focus();
        }
        else
        {
            args.IsValid = false;
            frmDtValidator.ErrorMessage = message;
            txtFrmDate.Focus();
        }
        //LoadGridView();
    }

    protected void checkToDate(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateToDate(txtFrmDate.Text, txtToDate.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            btnCalc.Focus();
        }
        else
        {
            args.IsValid = false;
            toDtValidator.ErrorMessage = message;
            txtToDate.Focus();
        }
        //LoadGridView();
    }

    protected void txtFrmDate_TextChanged(object sender, EventArgs e)
    {
        frmDtValidator.Validate();
        //LoadGridView();
    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        toDtValidator.Validate();
        //LoadGridView();
        shenagan_msg();

    }

    protected void btnCalc_Click(object sender, EventArgs e)
    {
        hpl1.Visible = false;
        DestValidator.Validate();
        frmDtValidator.Validate();
        toDtValidator.Validate();
        planTypValidator.Validate();

        //if (lblErrMesg.Text == "" && lblErrMesg1.Text == "")
        //{
        lblErrMesg2.Text = "";
        DataTable dtCurrentTable = new DataTable();
        DataTable dtVisitCntrys = new DataTable();

        if (ViewState["CurrentTable"] != null)
        {
            dtCurrentTable = (DataTable)ViewState["CurrentTable"];

            DataRow[] foundMainLife = dtCurrentTable.Select("Category = 'Main Life (Self)'");
            if (foundMainLife.Length != 0)
            {
                UpdateVisitContrys();
                dtVisitCntrys = (DataTable)ViewState["CurrentTabVisit"];

                Proposal prop = new Proposal();
                string mesg = prop.validateVisitCountrs(ddlDestination.SelectedValue, dtVisitCntrys);

                if (mesg == "success")
                {

                    Country cn = new Country();

                    DateTime arr_date = Convert.ToDateTime(txtToDate.Text.Trim());

                    if (cn.check_schengen(ddlDestination.SelectedValue.Trim()))
                        arr_date = arr_date.AddDays(15);


                    mesg = prop.getAllGTPremiums(Page.User.Identity.Name, dtCurrentTable, ddlDestination.SelectedValue, txtFrmDate.Text, arr_date.ToString("yyyy/MM/dd"), ddlPlanType.SelectedValue, dtVisitCntrys, gvPlanDetails);

                    if (mesg == "success")
                    {
                        Panel1.Visible = false;
                        Panel2.Visible = true;
                        hpl1.Visible = true;
                        //string quotNo = "";
                        //mesg = prop.createAMPQuotation(Page.User.Identity.Name, dtCurrentTable, "A", out quotNo);

                        //if (mesg == "success")
                        //{
                        //    EncryptDecrypt dc = new EncryptDecrypt();
                        //    Dictionary<string, string> qs = new Dictionary<string, string>();
                        //    qs.Add("action", "buy");
                        //    qs.Add("quotNo", quotNo);
                        //    qs.Add("plan", "A");
                        //    hlBuyA.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        //    qs.Remove("plan");
                        //    qs.Add("plan", "B");
                        //    hlBuyB.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        //    qs.Remove("plan");
                        //    qs.Add("plan", "C");
                        //    hlBuyC.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        //    qs.Remove("plan");
                        //    qs.Add("plan", "D");
                        //    hlBuyD.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        //    qs.Remove("plan");
                        //    qs.Add("plan", "E");
                        //    hlBuyE.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);

                        //    qs.Remove("action");
                        //    qs.Add("action", "print");
                        //    qs.Remove("plan");
                        //    qs.Add("plan", "A");
                        //    hlPrintA.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        //    qs.Remove("plan");
                        //    qs.Add("plan", "B");
                        //    hlPrintB.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        //    qs.Remove("plan");
                        //    qs.Add("plan", "C");
                        //    hlPrintC.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        //    qs.Remove("plan");
                        //    qs.Add("plan", "D");
                        //    hlPrintD.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        //    qs.Remove("plan");
                        //    qs.Add("plan", "E");
                        //    hlPrintE.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/AMP_Quot_print.aspx", qs);
                        //}
                        //else
                        //{
                        //    lblErrMesg2.Text = mesg;
                        //}
                    }
                    else
                    {
                        hpl1.Visible = false;
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
        //}
        LoadGridView();
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

    private void FillDropDownList(DropDownList ddl)
    {
        Proposal prop = new Proposal();
        ArrayList arr = prop.getCountryList();

        foreach (ListItem item in arr)
        {
            ddl.Items.Add(item);
        }
        //LoadGridView();
    }

    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("FromCountry", typeof(string)));
        dt.Columns.Add(new DataColumn("ToCountry", typeof(string)));

        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["CurrentTabVisit"] = dt;

        //Bind the Gridview   
        gvVisitCntrs.DataSource = dt;
        gvVisitCntrs.DataBind();

        //After binding the gridview, we can then extract and fill the DropDownList with Data   
        DropDownList ddlFrom = (DropDownList)gvVisitCntrs.Rows[0].Cells[1].FindControl("ddlFrmCntry");
        DropDownList ddlTo = (DropDownList)gvVisitCntrs.Rows[0].Cells[2].FindControl("ddlToCntry");
        FillDropDownList(ddlFrom);
        FillDropDownList(ddlTo);
        ddlFrom.SelectedValue = "LK";
        ddlFrom.Enabled = false;

        LoadGridView();
    }

    private void AddNewRowToGrid()
    {
        lblErrMesg1.Text = "";
        int lastRowIndex = gvVisitCntrs.Rows.Count - 1;
        DropDownList lastFrom = (DropDownList)gvVisitCntrs.Rows[lastRowIndex].Cells[1].FindControl("ddlFrmCntry");
        DropDownList lastTo = (DropDownList)gvVisitCntrs.Rows[lastRowIndex].Cells[2].FindControl("ddlToCntry");

        if ((lastFrom.SelectedValue != "-1") && (lastTo.SelectedValue != "-1"))
        {
            if (lastFrom.SelectedValue != lastTo.SelectedValue)
            {
                if (gvVisitCntrs.Rows.Count != 4)
                {
                    if (ViewState["CurrentTabVisit"] != null)
                    {
                        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTabVisit"];
                        DataRow drCurrentRow = null;

                        if (dtCurrentTable.Rows.Count > 0)
                        {
                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                            //add new row to DataTable   
                            dtCurrentTable.Rows.Add(drCurrentRow);
                            //Store the current data to ViewState for future reference   

                            ViewState["CurrentTabVisit"] = dtCurrentTable;

                            for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                            {

                                //extract the DropDownList Selected Items   

                                DropDownList ddlFrom = (DropDownList)gvVisitCntrs.Rows[i].Cells[1].FindControl("ddlFrmCntry");
                                DropDownList ddlTo = (DropDownList)gvVisitCntrs.Rows[i].Cells[2].FindControl("ddlToCntry");

                                // Update the DataRow with the DDL Selected Items   

                                dtCurrentTable.Rows[i]["FromCountry"] = ddlFrom.SelectedValue;
                                dtCurrentTable.Rows[i]["ToCountry"] = ddlTo.SelectedValue;

                            }

                            dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["FromCountry"] = dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 2]["ToCountry"];
                            if (ddlDestination.SelectedValue != "")
                            {
                                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["ToCountry"] = ddlDestination.SelectedValue;
                            }
                            //Rebind the Grid with the current data to reflect changes   
                            gvVisitCntrs.DataSource = dtCurrentTable;
                            gvVisitCntrs.DataBind();
                            LoadGridView();

                            gvVisitCntrs.Rows[gvVisitCntrs.Rows.Count - 1].Cells[2].FindControl("ddlToCntry").Focus();
                        }
                    }
                    else
                    {
                        lblErrMesg1.Text = "Internal error occured.";
                    }
                }
                else
                {
                    lblErrMesg1.Text = "Maximum number of 4 countries allowed";
                }
            }
            else
            {
                lblErrMesg1.Text = "From Country and To Country cannot be same.";
            }
        }
        else
        {
            lblErrMesg1.Text = "Please select countries before adding new row";
        }
        //Set Previous Data on Postbacks   
        SetPreviousData_2();
        //LoadGridView();
    }

    private void SetPreviousData_2()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTabVisit"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTabVisit"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddl1 = (DropDownList)gvVisitCntrs.Rows[rowIndex].Cells[1].FindControl("ddlFrmCntry");
                    DropDownList ddl2 = (DropDownList)gvVisitCntrs.Rows[rowIndex].Cells[2].FindControl("ddlToCntry");

                    //Fill the DropDownList with Data   
                    FillDropDownList(ddl1);
                    FillDropDownList(ddl2);

                    //if (i < dt.Rows.Count - 1)
                    //{                       
                    //Set the Previous Selected Items on Each DropDownList  on Postbacks  

                    if (dt.Rows[i]["FromCountry"].ToString() != "")
                    {
                        ddl1.ClearSelection();
                        ddl1.Items.FindByValue(dt.Rows[i]["FromCountry"].ToString()).Selected = true;
                    }

                    if (dt.Rows[i]["ToCountry"].ToString() != "")
                    {
                        ddl2.ClearSelection();
                        ddl2.Items.FindByValue(dt.Rows[i]["ToCountry"].ToString()).Selected = true;
                    }

                    if (i != dt.Rows.Count - 1)
                    {
                        ddl2.Enabled = false;
                    }

                    ddl1.Enabled = false;
                    // }

                    rowIndex++;
                }
            }
        }

        LoadGridView();
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
        //LoadGridView();
    }

    protected void Gridview1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)ViewState["CurrentTabVisit"];
            LinkButton lb = (LinkButton)e.Row.FindControl("lbRemove");
            if (lb != null)
            {
                if (dt.Rows.Count > 1)
                {
                    if (e.Row.RowIndex != dt.Rows.Count - 1)
                    {
                        lb.Visible = false;
                    }
                }
                else
                {
                    lb.Text = "Clear";
                    // lb.Visible = false;
                }
            }
        }
        //LoadGridView();
    }

    protected void lbRemove_Click(object sender, EventArgs e)
    {
        lblErrMesg1.Text = "";
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["CurrentTabVisit"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTabVisit"];
            if (dt.Rows.Count > 0)
            {
                if (gvRow.RowIndex == dt.Rows.Count - 1)
                {
                    //Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows[rowID]);
                    ResetRowID_2(dt);
                }
            }


            //Store the current data in ViewState for future reference  
            ViewState["CurrentTabVisit"] = dt;

            //Re bind the GridView for the updated data  
            gvVisitCntrs.DataSource = dt;
            gvVisitCntrs.DataBind();
            LoadGridView();
        }

        //Set Previous Data on Postbacks  
        SetPreviousData_2();
        //LoadGridView();

        int lastRowIndex = gvVisitCntrs.Rows.Count - 1;
        DropDownList lastTo = (DropDownList)gvVisitCntrs.Rows[lastRowIndex].Cells[2].FindControl("ddlToCntry");
        lastTo.SelectedValue = ddlDestination.SelectedValue;
    }

    private void ResetRowID_2(DataTable dt)
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
        else
        {
            DataRow dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["FromCountry"] = "LK";
            dt.Rows.Add(dr);
        }
        //LoadGridView();
    }

    private void UpdateVisitContrys()
    {
        if (ViewState["CurrentTabVisit"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTabVisit"];
            dt.Clear();

            if (gvVisitCntrs.Rows.Count > 0)
            {
                for (int i = 0; i < gvVisitCntrs.Rows.Count; i++)
                {
                    DropDownList ddl1 = (DropDownList)gvVisitCntrs.Rows[i].Cells[1].FindControl("ddlFrmCntry");
                    DropDownList ddl2 = (DropDownList)gvVisitCntrs.Rows[i].Cells[2].FindControl("ddlToCntry");

                    DataRow dr = null;
                    dr = dt.NewRow();
                    dr["RowNumber"] = i + 1;
                    dr["FromCountry"] = ddl1.SelectedValue.ToString();
                    if (ddl2.SelectedValue != "-1")
                    {
                        dr["ToCountry"] = ddl2.SelectedValue.ToString();
                        dt.Rows.Add(dr);
                    }

                }
            }
        }
        //LoadGridView();
    }

    protected void gvPlanDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.Cells[0].Text.IndexOf("i)") == 0) || (e.Row.Cells[0].Text.IndexOf("ii)") == 0))
            {
                e.Row.Cells[0].Style.Add("padding-left", "16px");
            }
        }
        //LoadGridView();
    }
    protected void ddlDestination_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetInitialRow();
        DropDownList ddlTo = (DropDownList)gvVisitCntrs.Rows[0].Cells[2].FindControl("ddlToCntry");
        ddlTo.SelectedValue = ddlDestination.SelectedValue;
        lblErrMesg1.Text = "";
        //LoadGridView();

        shenagan_msg();

    }

    protected void shenagan_msg()
    {
        if (ddlDestination.SelectedValue != "0" && !String.IsNullOrEmpty(txtToDate.Text))
        {
            Country cn = new Country();
            if (cn.check_schengen(ddlDestination.SelectedValue.Trim()))
            {
                ph102.Visible = true;
                lbl_shen.Text = "* Since your final destination is a schengen country, 15 days will be automatically added to the above arrival date.";
            }
            else
            {
                ph102.Visible = false;
                lbl_shen.Text = "";
            }
        }
        else
        {
            ph102.Visible = false;
            lbl_shen.Text = "";
        }
    }

    protected void ddlToCntry_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlTo = (DropDownList)sender;
        GridViewRow gvRow = (GridViewRow)ddlTo.NamingContainer;
        int rowID = gvRow.RowIndex;

        if (rowID == gvVisitCntrs.Rows.Count - 1)
        {
            AddNewRowToGrid();
        }
    }
}