using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Authorized_Products_TRV_Proposal_New : System.Web.UI.Page
{
    private DataTable dtCurrentTable;
    private string schenagan;
    private string arr_date;
    DataTable dtVisitCntrys;
    private string agentcode;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        gvMembers.Columns[0].Visible = false;
        LoadGridView();
        if (!IsPostBack)
        {
            CustProfile profile = new CustProfile(Page.User.Identity.Name);
            txtDOB.Text = profile.O_dateOfBirth;
            rblGender.SelectedValue = profile.O_gender;
            //FillDropDownList(ddlDestination); // Load Destination Country List -No need to add this field           
            this.SetInitialRow();
            FillDDLCategory("F");
            LoadGridView();

            if (Session["dtCurrentTable"] != null)
            {
                dtCurrentTable =(DataTable) Session["dtCurrentTable"] ;
                ViewState["CurrentTable"] = dtCurrentTable;
                schenagan = Session["schenagan"].ToString() ;
                txtFrmDate.Text= Session["fromDate"].ToString();
                arr_date=Session["arr_date"] .ToString();
                txtToDate.Text = arr_date;
                dtVisitCntrys =(DataTable) Session["dtVisitCntrys"];
                ViewState["CurrentTabVisit"] = dtVisitCntrys;
                ddlPlanType.SelectedValue = Session["TRV_Type"].ToString();

                lblAgntNAme.Text = Session["agentname"].ToString();
                lblnoofDays.Text=Session["duration"].ToString();

                FillDDLCategory(ddlPlanType.SelectedValue);
                gvVisitCntrs.DataSource = dtVisitCntrys;
                gvVisitCntrs.DataBind();

                

                //SetPreviousData();

                SetPreviousData_2();
                LoadGridView();
                AddNewRowToGrid();

                gvMembers.DataSource = dtCurrentTable;
                gvMembers.DataBind();
                LoadGridView();
                SetPreviousData();
                agentcode = Session["agentcode"] .ToString();
                txtxAgtcd.Text = agentcode;

            }
        }
        else
        {

        }
        
    }

     

    #region Load Destination
    private void FillDropDownList(DropDownList ddl)
    {
        TRV_Proposal prop = new TRV_Proposal();
        ArrayList arr = prop.getCountryList();

        foreach (ListItem item in arr)
        {
            ddl.Items.Add(item);
        }
        //LoadGridView();
    }
    #endregion



    //protected void checkDestination(object source, ServerValidateEventArgs args)
    //{
    //    int status = -1;
    //    string message = "";

    //    InfoValidator validator = new InfoValidator();
    //    validator.validateDestination(ddlDestination.SelectedValue.ToString(), out status, out message);

    //    if (status == 0)
    //    {
    //        args.IsValid = true;
    //        txtFrmDate.Focus();
    //    }
    //    else
    //    {
    //        args.IsValid = false;
    //        DestValidator.ErrorMessage = message;
    //        ddlDestination.Focus();
    //    }
    //}

    protected void txtFrmDate_TextChanged(object sender, EventArgs e)
    {
        frmDtValidator.Validate();
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
    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        DateTime fromDt = DateTime.ParseExact(txtFrmDate.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture);
        DateTime toDt = DateTime.ParseExact(txtToDate.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture);
        if (toDt > fromDt)
        {
            lblnoofDays0.Text = "No of Days";
            lblnoofDays.Text = (((toDt - fromDt).Days)+1).ToString() + " Days";
        }

        toDtValidator.Validate();
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
           // btnCalc.Focus();
        }
        else
        {
            args.IsValid = false;
            toDtValidator.ErrorMessage = message;
            txtToDate.Focus();
        }
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

        //************* No need this part since Destination Removed******************
        //int lastRowIndex = gvVisitCntrs.Rows.Count - 1;
        //DropDownList lastTo = (DropDownList)gvVisitCntrs.Rows[lastRowIndex].Cells[2].FindControl("ddlToCntry");
        //lastTo.SelectedValue = ddlDestination.SelectedValue;
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
                  
                    //Fill the DropDownList with Data   
                    FillDropDownList(ddl1);
                   // FillDropDownList(ddl2);

                    //if (i < dt.Rows.Count - 1)
                    //{                       
                    //Set the Previous Selected Items on Each DropDownList  on Postbacks  

                    if (dt.Rows[i]["FromCountry"].ToString() != "")
                    {
                        ddl1.ClearSelection();
                        ddl1.Items.FindByValue(dt.Rows[i]["FromCountry"].ToString()).Selected = true;
                    }                

                   

                    ddl1.Enabled = true;
                    // }

                    rowIndex++;
                     
                }
            }
        }

        LoadGridView();
    }

    private void LoadGridView()
    {

        if (gvVisitCntrs.Rows.Count > 0)
        {
            gvVisitCntrs.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            //gvVisitCntrs.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone");
            gvVisitCntrs.HeaderRow.Cells[2].Attributes.Add("data-class", "expand");
            //gvVisitCntrs.HeaderRow.Cells[3].Attributes.Add("data-hide", "phone,tablet");

            gvVisitCntrs.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        //gridView.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
        //gridView.HeaderRow.Cells[2].Attributes.Add("data-class", "expand");


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

    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("FromCountry", typeof(string)));
        //dt.Columns.Add(new DataColumn("ToCountry", typeof(string)));

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
       // DropDownList ddlTo = (DropDownList)gvVisitCntrs.Rows[0].Cells[2].FindControl("ddlToCntry");
        FillDropDownList(ddlFrom);
        //FillDropDownList(ddlTo);
        ddlFrom.SelectedValue = "LK";
        ddlFrom.Enabled = true;

        LoadGridView();
    }


    protected void ddlFrmCntry_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlfrom = (DropDownList)sender;
        GridViewRow gvRow = (GridViewRow)ddlfrom.NamingContainer;
        int rowID = gvRow.RowIndex;

        if (rowID == gvVisitCntrs.Rows.Count - 1)
        {
            AddNewRowToGrid();
        }

        if (ViewState["CurrentTabVisit"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTabVisit"];
            for (int x = 0; x < dtCurrentTable.Rows.Count-1; x++)
            {
                
                if (x > 0)
                {
                    string selcode = dtCurrentTable.Rows[x-1].ItemArray[1].ToString();
                    if (selcode == ddlfrom.SelectedValue)
                    {
                        lblErrMesg1.Text = "Same Country Added.. Please Remove the Dupplicate Country";
                        btnCalc.Enabled = false;
                        break;
                    }
                    else
                    {
                        lblErrMesg1.Text = " ";
                        btnCalc.Enabled = true;
                    }
                }
            }
        }
         
    }

    private void AddNewRowToGrid()
    {
        lblErrMesg1.Text = "";
        int lastRowIndex = gvVisitCntrs.Rows.Count - 1;
        DropDownList lastFrom = (DropDownList)gvVisitCntrs.Rows[lastRowIndex].Cells[1].FindControl("ddlFrmCntry");


        if ((lastFrom.SelectedValue != "-1")) //&& (lastTo.SelectedValue != "-1")
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
                    try
                    {
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }
                    catch { }
                    //Store the current data to ViewState for future reference   

                    ViewState["CurrentTabVisit"] = dtCurrentTable;

                    for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                    {

                        //extract the DropDownList Selected Items   

                        DropDownList ddlFrom = (DropDownList)gvVisitCntrs.Rows[i].Cells[1].FindControl("ddlFrmCntry");


                        // Update the DataRow with the DDL Selected Items   

                        dtCurrentTable.Rows[i]["FromCountry"] = ddlFrom.SelectedValue;


                    }

                    //dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["FromCountry"] = dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 2]["ToCountry"];
                    //if (ddlDestination.SelectedValue != "")
                    //{
                    //    dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["ToCountry"] = ddlDestination.SelectedValue;
                    //}
                    //Rebind the Grid with the current data to reflect changes   
                    gvVisitCntrs.DataSource = dtCurrentTable;
                    gvVisitCntrs.DataBind();
                    LoadGridView();

                    gvVisitCntrs.Rows[gvVisitCntrs.Rows.Count - 1].Cells[1].FindControl("ddlFrmCntry").Focus();
                }
            }
            else
            {
                lblErrMesg1.Text = "Internal error occured.";
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

    private ArrayList GetCategories(string planType)
    {
        ArrayList arr = new ArrayList();

        if (planType == "I")
        {
            arr.Add(new ListItem("Main Life", "M"));
        }
        else if (planType == "F")
        {
            arr.Add(new ListItem("Main Life", "M"));
            arr.Add(new ListItem("Spouse", "S"));
            arr.Add(new ListItem("Child", "C"));
            arr.Add(new ListItem("Other", "N"));
        }
        else if (planType == "G")
        {
            arr.Add(new ListItem("Main Life", "M"));
            arr.Add(new ListItem("Other", "N"));
        }

        return arr;
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

        TRV_InfoValidator validator = new TRV_InfoValidator();
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

        TRV_InfoValidator validator = new TRV_InfoValidator();
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
    }

    protected void txtDOB_TextChanged(object sender, EventArgs e)
    {
        DOBValidator.Validate();
        lblnothing.Focus();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        LoadGridView();
         

        frmDtValidator.Validate();
        toDtValidator.Validate();
        planTypValidator.Validate();
        CategoryValidator.Validate();
        GenderValidator.Validate();
        DOBValidator.Validate();

        if (  frmDtValidator.IsValid &&
            toDtValidator.IsValid && planTypValidator.IsValid &&
            CategoryValidator.IsValid && GenderValidator.IsValid && DOBValidator.IsValid)
        {
            lblErrMesg.Text = "";
            try
            {
                double age = 0.00;

                TRV_InfoValidator validator = new TRV_InfoValidator();
                string mesg = "success";

                //TRV_Policy_mast quot = new TRV_Policy_mast();
                //quot.getLimits(ddlCategory.SelectedValue.ToString(), "TPI");
                //string minage = quot.minAge;
                //string maxage = quot.maxAge;


                mesg = validator.validateMinMaxAgeForTRV(Page.User.Identity.Name.ToUpper(), ddlCategory.SelectedValue.ToString(), "6", "80", txtDOB.Text, txtFrmDate.Text.Trim(), txtToDate.Text.Trim(), out age);
              //  mesg = validator.getAgeForTRV(Page.User.Identity.Name.ToUpper(), ddlCategory.SelectedValue.ToString(), txtDOB.Text, txtFrmDate.Text, out age);

                if (ddlPlanType.SelectedValue == "I" && ddlCategory.SelectedValue != "M")
                {
                    mesg = "Only Main Life can be entered for Individual plan";
                }
                else if (ddlPlanType.SelectedValue == "G" && ddlCategory.SelectedValue != "M" && ddlCategory.SelectedValue != "N")
                {
                    mesg = "Invalid category for Group plan";
                }
                else if (ddlPlanType.SelectedValue == "F" && ddlCategory.SelectedValue == "M" && age < 18)
                {
                    mesg = "Age of Main Life Should be greater than 18 years for a Family plan.";
                }
                
               
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
                                mesg = "Main Life is already added.";
                            }
                            else if (ddlCategory.SelectedValue == "M")
                            {
                                mesg = "Main Life should be entered first.";
                            }
                            DataRow[] foundSpouse = dtCurrentTable.Select("Category = 'Spouse'");
                            if (ddlCategory.SelectedValue == "S" && foundSpouse.Length != 0)
                            {
                                mesg = "Spouse is already entered.";
                            }
                            if (ddlCategory.SelectedValue == "S" && rblGender.SelectedItem.Text == foundMainLife[0].ItemArray[2].ToString())
                            {
                                mesg = "Gender cannot be same for both Main Life and Spouse.";
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
                                if (ddlCategory.SelectedValue == "M")
                                {
                                    if (ddlPlanType.SelectedValue == "F")
                                    {
                                        ddlCategory.SelectedIndex = 1;
                                    }
                                    else if (ddlPlanType.SelectedValue == "G")
                                    {
                                        ddlCategory.SelectedIndex = 1;
                                    }

                                }
                                else if (ddlCategory.SelectedValue == "S")
                                {
                                    if (ddlPlanType.SelectedValue == "F")
                                    {
                                        ddlCategory.SelectedIndex = 2;
                                    }
                                }

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
                                if (ddlCategory.SelectedValue == "M")
                                {
                                    if (ddlPlanType.SelectedValue == "F")
                                    {
                                        ddlCategory.SelectedIndex = 1;
                                    }
                                    else if (ddlPlanType.SelectedValue == "G")
                                    {
                                        ddlCategory.SelectedIndex = 1;
                                    }

                                }
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
                            if (ddlCategory.SelectedValue == "M")
                            {
                                if (ddlPlanType.SelectedValue == "F")
                                {
                                    ddlCategory.SelectedIndex = 1;
                                }
                                else if (ddlPlanType.SelectedValue == "G")
                                {
                                    ddlCategory.SelectedIndex = 1;
                                }

                            }
                            SetPreviousData();
                        }
                        else
                        {
                            lblErrMesg.Text = "Main Life should be entered first.";
                        }
                    }
                    btnSubmit.Focus();
                }
                else
                {
                    lblErrMesg.Text = mesg;
                }
            }
            catch(Exception exc)
            {
                lblErrMesg.Text = "Error while adding member.";
                log logger = new log();
                logger.write_log("Failed at TRV_PROPOSAL.aspx: " + e.ToString());
            }

        }
        lblnothing.Focus();
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

    protected void btnCalc_Click(object sender, EventArgs e)
    {
        hpl1.Visible = false;
         
        frmDtValidator.Validate();
        toDtValidator.Validate();
        planTypValidator.Validate();

        //if (lblErrMesg.Text == "" && lblErrMesg1.Text == "")
        //{
        lblErrMesg2.Text = "";
        DataTable dtCurrentTable = new DataTable();
        DataTable dtVisitCntrys = new DataTable();
        DataTable dtnew = (DataTable)ViewState["CurrentTabVisit"];
        if (ViewState["CurrentTable"] != null )
        {
            dtCurrentTable = (DataTable)ViewState["CurrentTable"];

            DataRow[] foundMainLife = dtCurrentTable.Select("Category = 'Main Life'");
            if (foundMainLife.Length != 0)
            {
                UpdateVisitContrys();
                dtVisitCntrys = (DataTable)ViewState["CurrentTabVisit"];
                string schenagan = "";
                for (int a = 0; a < dtVisitCntrys.Rows.Count; a++)
                {
                    if (a == 0)
                    {
                        schenagan = "'" + dtVisitCntrys.Rows[a].ItemArray[1].ToString() + "'";
                    }
                    else
                    {
                        schenagan = schenagan + ",'" + dtVisitCntrys.Rows[a].ItemArray[1].ToString() + "'";
                    }
                }
                TRV_Proposal prop = new TRV_Proposal();

                DateTime arr_date = Convert.ToDateTime(txtToDate.Text.Trim());

                if (prop.check_schengen(schenagan))
                {
                    // arr_date = arr_date.AddDays(15);  *********Commented on 2019.07.22 by Kumuduni
                }
                int agentcode = 0;
                if (!String.IsNullOrEmpty(txtxAgtcd.Text))
                {
                    agentcode = int.Parse(txtxAgtcd.Text);
                }

                Session["dtCurrentTable"] = dtCurrentTable;
                Session["schenagan"] = schenagan;
                Session["fromDate"] = txtFrmDate.Text;
                Session["arr_date"] = arr_date.ToString("yyyy/MM/dd");
                Session["dtVisitCntrys"] = dtVisitCntrys;
                Session["agentcode"] = agentcode;
                Session["TRV_Type"] = ddlPlanType.SelectedValue;
                Session["agentname"] = lblAgntNAme.Text;
                Session["duration"] = lblnoofDays.Text;
                if (dtnew.Rows.Count > 0)
                {
                    string mesg = prop.getAllTRVPremiums(Page.User.Identity.Name, dtCurrentTable, schenagan, txtFrmDate.Text, arr_date.ToString("yyyy/MM/dd"), ddlPlanType.SelectedValue, dtVisitCntrys, gvPlanDetails, agentcode);

                    if (mesg == "success")
                    {
                        Panel1.Visible = false;
                        Panel2.Visible = true;
                        hpl1.Visible = true;

                    }
                    else
                    {
                        hpl1.Visible = false;
                        lblErrMesg2.Text = mesg;
                    }
                }
                else
                {
                    lblErrMesg2.Text = "Please select visiting Countries.";
                }

               

            }
            else
            {
                lblErrMesg2.Text = "Main Life should be entered.";
            }
        }
        else
        {
            lblErrMesg2.Text = "Please select visiting Countries.";
        }

        //}
        LoadGridView();

       // Session["Complete"] = true;
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
                   
                    DataRow dr = null;
                    if (ddl1.SelectedValue != "-1")
                    {
                        dr = dt.NewRow();
                        dr["RowNumber"] = i + 1;
                        dr["FromCountry"] = ddl1.SelectedValue.ToString();


                        dt.Rows.Add(dr);
                    }
                  

                }
            }
        }
       
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
    }

    protected void checkagent(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";
        string Name = "";
        string AgtStatus = "";
        if (txtxAgtcd.Text != "0")
        {
            InfoValidator validator = new InfoValidator();
            validator.validateagtcd(txtxAgtcd.Text, out status, out message);

            if (status == 0)
            {
                args.IsValid = true;
                TRV_Proposal objquot = new TRV_Proposal();
                objquot.getAgtName(int.Parse(txtxAgtcd.Text), out Name);
                if (!String.IsNullOrEmpty(Name))
                {
                    lblAgntNAme.Text = Name;
                    args.IsValid = true;

                }
                else
                {
                    args.IsValid = false;
                    message = "Invalid Agency Code..";
                    agtValidator.ErrorMessage = message;
                }
                 
                ////btnCalc.Focus();
            }
            else
            {
                args.IsValid = false;
                agtValidator.ErrorMessage = message;
                txtxAgtcd.Focus();
            }
        }

    }

    protected void txtxAgtcd_TextChanged(object sender, EventArgs e)
    {
        lblAgntNAme.Text = "";
        agtValidator.Validate();
    }



    protected void gvVisitCntrs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //for (int currentRow = 0; currentRow < grv.Rows.Count - 1; currentRow++)
        //{
        //    GridViewRow rowToCompare = grv.Rows[currentRow];
        //    //specify otherRow as currentRow + 1
        //    for (int otherRow = currentRow + 1; otherRow < grv.Rows.Count; otherRow++)
        //    {
        //        GridViewRow row = grv.Rows[otherRow];

        //        bool duplicateRow = true;
        //        //compare cell ENVA_APP_ID between the two rows
        //        if (rowToCompare.Cells[0].Text != row.Cells[0].Text)
        //        {
        //            duplicateRow = false;
        //            break;
        //        }
        //        //highlight both the currentRow and otherRow if ENVA_APP_ID matches
        //        if (duplicateRow)
        //        {
        //            rowToCompare.BackColor = Color.Red;
        //            rowToCompare.ForeColor = Color.Black;
        //            row.BackColor = Color.Red;
        //            row.ForeColor = Color.Black;
        //        }
        //    }
        //}
    }
}