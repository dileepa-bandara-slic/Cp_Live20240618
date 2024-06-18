using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Authorized_Products_TRV_Quot_Confirm : System.Web.UI.Page
{
    public string errorMsg { get; set; }
    EncryptDecrypt dc = new EncryptDecrypt();
    string passportNo = "";
    string QID = "";
    private string destination;
    DateTime deptdate;
    DateTime returndate;
    DateTime arrivedate;
    DataTable DtMembers = new DataTable();
    public string visitCntr1 { get; set; }
    public string visitCntr2 { get; set; }
    public string visitCntr3 { get; set; }
    public string visitCntr4 { get; set; }
    public string Quotno { get; set; }
    public double PremiumSelected { get; set; }
    public string  planSelected { get; set; }
    public string  poltype { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["dtCurrentTable"] = null;
        Session["schenagan"] = null;
        Session["fromDate"] = null;
        Session["arr_date"] = null;
        Session["dtVisitCntrys"] = null;
        Session["agentcode"] = null;
        Session["TRV_Type"] = null;
        Session["agentname"] = null;
        Session["duration"] = null;
        // Session["Complete"] = null;
        if (!IsPostBack)
        {
            LoadGridview();
            string strReq = "";
            strReq = Request.RawUrl;
            string h = strReq.Substring(strReq.Length - 1);
            if (h == "#")
            {
                errorMsg = "No url";
                Response.Redirect("~/Errors/InternalError.htm");
            }
            else
            {
                strReq = strReq.Substring(strReq.IndexOf('?') + 1);

                EncryptDecrypt enc = new EncryptDecrypt();
                strReq = enc.Decrypt(strReq);
                if (strReq == "#")
                {
                    errorMsg = "No Parameters Passed";
                    Response.Redirect("~/Errors/InternalError.htm");
                }
                else
                {
                    Dictionary<string, string> paraList = new Dictionary<string, string>();
                    paraList = enc.getParameters(strReq);

                    if (paraList.ContainsKey("Quot#2N0"))
                    {
                        //DataTable dtMembers = new DataTable();
                        List<TRV_Proposal_mem> dtMembers = new List<TRV_Proposal_mem>();
                        CustProfile cp = new CustProfile(User.Identity.Name);
                        passportNo = cp.O_passportNo;
                        QID = paraList["Quot#2N0"];
                        litQuotNo.Text = QID;
                        PremiumSelected = double.Parse(paraList["premium"]);
                        planSelected = paraList["plan"];
                        poltype= paraList["polTy"];
                        
                        hdn_plan.Value = planSelected;

                        litPremium.Text = PremiumSelected.ToString("N2");


                        TRV_Prop_mast trvsumins = new TRV_Prop_mast();
                        litSumIns.Text = trvsumins.get_SumIns(planSelected, poltype).ToString("N2");
                        litPlan.Text = paraList["poldesc"]; // trvsumins.get_scheme_name(planSelected, poltype);

                       EncryptDecrypt dc = new EncryptDecrypt();
                        Dictionary<string, string> qs = new Dictionary<string, string>();

                        TRV_Prop_mast trv = new TRV_Prop_mast(QID);

                        hdftrv.Value = trv.TRV_TYPE;
                        TRV_Proposal_mem mem = new TRV_Proposal_mem(QID, DtMembers);
                        TRV_Country cn = new TRV_Country();
                        TRV_Country trvDest;
                        hdn_plan0.Value = trv.DESTINATION;
                        if (trv.DESTINATION.Contains(','))
                        {
                            string[] arrDest = trv.DESTINATION.Split(',');
                            string fDest = "";
                            for (int x = 0; x < arrDest.Length; x++)
                            {
                                if (x == 0)
                                {
                                    fDest =  "'"+arrDest[x]+"'";
                                }
                                else
                                {
                                    fDest += ",'" + arrDest[x] + "'";
                                }
                            }
                            trvDest = new TRV_Country(fDest);
                        }
                        else
                        {
                            string Destination = "'" + trv.DESTINATION + "'";
                            trvDest = new TRV_Country(Destination);
                        }

                        if (cn.check_schengen(trv.DESTINATION))
                        {
                           
                            // lbl_shen.Text = "* 15 days are added, since your final destination is a schengen country."; **** Commeted on 2019.07.22 by Kumuduni
                        }
                        else
                            lbl_shen.Text = "";

                        qs.Add("action", "print");
                        qs.Add("refNo", QID);
                        qs.Add("plan", planSelected);
                        hlPrint.NavigateUrl = dc.url_encrypt("~/General/Authorized/Products/TRV_Prop_print.aspx", qs);
                        string polType = "";
                        if (QID.Contains("TPI"))
                        {
                            polType = "TPI";
                        }
                        else if (QID.Contains("TPM"))
                        {
                            polType = "TPM";
                        }


                        
                      //  DtMembers = mem.;

                        //litStrtDt.Text = DateTime.Now.ToString("yyyy/MM/dd");
                        if (RadioButtonList1.SelectedValue == "No")
                        {

                            for (int a = 0; a < DtMembers.Rows.Count; a++)
                            {
                                if (DtMembers.Rows[a].ItemArray[2].ToString() == "M")
                                {
                                    DtMembers.Rows[a].ItemArray[4] = txtdob.Text;
                                    DtMembers.Rows[a].ItemArray[6] = txtname.Text.Trim();
                                    DtMembers.AcceptChanges();
                                }
                            }

                            //string address= trv.ADDRESS1 + (!String.IsNullOrEmpty(trv.ADDRESS2.Trim()) ? "<br/>&nbsp;&nbsp" + trv.ADDRESS2 : "") + (!String.IsNullOrEmpty(trv.ADDRESS3.Trim()) ? "<br/>&nbsp;&nbsp" + trv.ADDRESS3 : "") + (!String.IsNullOrEmpty(trv.ADDRESS4.Trim()) ? "<br/>&nbsp;&nbsp" + trv.ADDRESS4 : "");
                            //hdfval.Value= trv.TITLE+"*"+ trv.FULL_NAME+"*"+ address+"*" + trv.MOBILE_NUMBER+"*"+ trv.HOME_NUMBER+"*"+ trv.OFFICE_NUMBER+"*"+ cp.O_nicNo+"*"+ cp.O_dateOfBirth+"*"+ cp.O_email; 
                        }
                        else
                        {

                            
                            litTitle.Text = trv.TITLE;
                            litName.Text = trv.FULL_NAME;
                            litAddress.Text = trv.ADDRESS1 + (!String.IsNullOrEmpty(trv.ADDRESS2.Trim()) ? "<br/>&nbsp;&nbsp" + trv.ADDRESS2 : "") + (!String.IsNullOrEmpty(trv.ADDRESS3.Trim()) ? "<br/>&nbsp;&nbsp" + trv.ADDRESS3 : "") + (!String.IsNullOrEmpty(trv.ADDRESS4.Trim()) ? "<br/>&nbsp;&nbsp" + trv.ADDRESS4 : "");
                            litMobileNo.Text = trv.MOBILE_NUMBER;
                            litHomeNo.Text = trv.HOME_NUMBER;
                            litOfficeNo.Text = trv.OFFICE_NUMBER;
                            litNIC.Text = cp.O_nicNo;
                            litBirthDate.Text = cp.O_dateOfBirth;
                            litEmail.Text = cp.O_email;


                            txtname.Text = trv.FULL_NAME;
                            txtadd.Text = trv.ADDRESS1;
                            txtadd0.Text = trv.ADDRESS2;
                            txtadd1.Text = trv.ADDRESS3;
                            txtadd2.Text = trv.ADDRESS4;

                            txtmob.Text = trv.MOBILE_NUMBER;
                            txthmno.Text = trv.HOME_NUMBER;
                            txtofno.Text = trv.OFFICE_NUMBER;
                            txtnic.Text = cp.O_nicNo;


                        }
                        gvMembers.DataSource = DtMembers;
                        gvMembers.DataBind();
                        Session["DtMembers"] = DtMembers;
                        ViewState["DtMembers"] = DtMembers;
                        gvMembers.Columns[0].Visible = false;

                       
                        lit_leaving.Text = trv.DEPART_DATE;
                        lit_returning.Text = trv.RETURN_DATE;
                        

                        TRV_Proposal prop = new TRV_Proposal();
                        prop.fillTravelPurposeDdl(ddr_purpose);
                        prop.fillDdlTitles(gvMembers);
                        prop.fillproplTitles(ddltitle);

                        DropDownList ddlTitles = ((DropDownList)gvMembers.Rows[0].Cells[1].FindControl("ddTitle"));
                        TextBox txtName = ((TextBox)gvMembers.Rows[0].Cells[2].FindControl("txtName"));
                        TextBox txtPPno = ((TextBox)gvMembers.Rows[0].Cells[2].FindControl("txtPP"));

                        string memType = gvMembers.Rows[0].Cells[7].Text.ToString();
                        if (memType == "Main Life" ||memType=="M")
                        {
                            if (RadioButtonList1.SelectedValue == "No")
                            {
                                ddlTitles.SelectedValue = ddltitle.SelectedValue;
                                txtName.Text = txtname.Text;
                                 txtPPno.Text = "";
                                txtPPno.Enabled = true;
                                ddlTitles.Enabled = true;//false;
                                txtName.Enabled = true;
                            }
                            else
                            {
                                ddlTitles.SelectedValue = litTitle.Text;
                                txtName.Text = litName.Text;
                                
                                ddlTitles.SelectedValue = trv.TITLE;
                                ddlTitles.Enabled = true;
                                if (!String.IsNullOrEmpty(cp.O_passportNo))
                                {
                                    txtPPno.Enabled = false;
                                    txtPPno.Text = cp.O_passportNo;
                                }
                                else
                                {
                                    txtPPno.Enabled = true;
                                }
                                
                                txtName.Enabled = false;
                            }
                            // txtName.Enabled = false;

                            //if (!String.IsNullOrEmpty(cp.O_passportNo))
                            //    txtPPno.Enabled = false;
                        }

                        DataTable dttbl = new DataTable();
                        dttbl = trvDest.dtCountry;                          

                        GridView1.DataSource = dttbl;
                        GridView1.DataBind();
                        //LoadGridview();
                          
                    }
                    else
                    {
                        errorMsg = "No valid Parameters Passed";
                        Response.Redirect("~/Errors/InternalError.htm");
                    }
                }
            }
        }
        else
        {
            if (RadioButtonList1.SelectedValue == "Yes")
            {
                CustProfile cp = new CustProfile(User.Identity.Name);
                TRV_Prop_mast trv = new TRV_Prop_mast(litQuotNo.Text);

                litTitle.Text = trv.TITLE;
                litName.Text = trv.FULL_NAME;
                litAddress.Text = trv.ADDRESS1 + (!String.IsNullOrEmpty(trv.ADDRESS2.Trim()) ? "<br/>&nbsp;&nbsp" + trv.ADDRESS2 : "") + (!String.IsNullOrEmpty(trv.ADDRESS3.Trim()) ? "<br/>&nbsp;&nbsp" + trv.ADDRESS3 : "") + (!String.IsNullOrEmpty(trv.ADDRESS4.Trim()) ? "<br/>&nbsp;&nbsp" + trv.ADDRESS4 : "");
                litMobileNo.Text = trv.MOBILE_NUMBER;
                litHomeNo.Text = trv.HOME_NUMBER;
                litOfficeNo.Text = trv.OFFICE_NUMBER;
                litNIC.Text = cp.O_nicNo;
                litBirthDate.Text = cp.O_dateOfBirth;
                litEmail.Text = cp.O_email;


                DropDownList ddlTitles = ((DropDownList)gvMembers.Rows[0].Cells[1].FindControl("ddTitle"));
                TextBox txtName = ((TextBox)gvMembers.Rows[0].Cells[2].FindControl("txtName"));
                TextBox txtPPno = ((TextBox)gvMembers.Rows[0].Cells[2].FindControl("txtPP"));

                string memType = gvMembers.Rows[0].Cells[7].Text.ToString();
                if (memType == "Main Life" || memType == "M")
                {
                    if (RadioButtonList1.SelectedValue == "No")
                    {
                        ddlTitles.SelectedValue = ddltitle.SelectedValue;
                        txtName.Text = txtname.Text;
                        //txtPPno.Text = ""; cp.O_passportNo;
                        txtPPno.Enabled = true;
                        txtName.Enabled = true;
                        ddlTitles.Enabled = false;


                    }
                    else
                    {
                        ddlTitles.SelectedValue = litTitle.Text;
                        txtName.Text = litName.Text;
                        if (!string.IsNullOrEmpty(cp.O_passportNo))
                        {
                            txtPPno.Text = cp.O_passportNo;
                        }
                        if (!String.IsNullOrEmpty(cp.O_passportNo))
                        {
                            txtPPno.Enabled = false;
                        }
                        else
                        {
                            txtPPno.Enabled = true;
                        }
                        txtName.Enabled = false;
                        ddlTitles.SelectedValue = trv.TITLE;
                        ddlTitles.Enabled = true;
                    }
                    // txtName.Enabled = false;

                    //if (!String.IsNullOrEmpty(cp.O_passportNo))
                    //    txtPPno.Enabled = false;
                } 
                ddltitle.Visible = false;
                txtname.Visible = false;
                txtadd.Visible = false;
                txtadd0.Visible = false;
                txtadd1.Visible = false;
                txtadd2.Visible = false;

                txtmob.Visible = false;
                txthmno.Visible = false;
                txtofno.Visible = false;
                txtnic.Visible = false;
                txtdob.Visible = false;
               

                litTitle.Visible = true;
                litName.Visible = true;
                litAddress.Visible = true;
                litMobileNo.Visible = true;
                litHomeNo.Visible = true;
                litOfficeNo.Visible = true;
                litNIC.Visible = true;
                litBirthDate.Visible = true;
                litEmail.Visible = true;
                 

            }
            else
            {
                TextBox txtName = ((TextBox)gvMembers.Rows[0].Cells[2].FindControl("txtName"));
                TextBox txtPPno = ((TextBox)gvMembers.Rows[0].Cells[2].FindControl("txtPP"));
                txtName.Text=txtname.Text;
                //txtPPno.Text = "";
                ddltitle.Visible = true;
                txtname.Visible = true;
                txtadd.Visible = true;
                txtmob.Visible = true;
                txthmno.Visible = true;
                txtofno.Visible = true;
                txtnic.Visible = true;
                txtdob.Visible = true;
               
                txtadd0.Visible = true;
                txtadd1.Visible = true;
                txtadd2.Visible = true;


                litTitle.Visible = false;
                litName.Visible = false;
                litAddress.Visible = false;
                litMobileNo.Visible = false;
                litHomeNo.Visible = false;
                litOfficeNo.Visible = false;
                litNIC.Visible = false;
                litBirthDate.Visible = false;
                litEmail.Visible = false;
                txtPPno.Enabled = true;
                txtName.Enabled = true;

            }
        }
        if (!String.IsNullOrEmpty(txtnic.Text))
        {
            NICValidator2.Validate();
            NICDOBValidator.Validate();
        }
    }

    protected void checkDdrPurpose(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateTravelPurpose(ddr_purpose.SelectedValue, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txt_ConName.Focus();
        }
        else
        {
            args.IsValid = false;
            ddrPurposeValidator.ErrorMessage = message;
            ddr_purpose.Focus();
        }
    }

    protected void txt_ConName_TextChanged(object sender, EventArgs e)
    {
        cntNameValidator.Validate();
    }

    protected void checkCntName(object source, ServerValidateEventArgs args)
    {

        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateContctName(txt_ConName.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txt_ConAdd1.Focus();
        }
        else
        {
            args.IsValid = false;
            cntNameValidator.ErrorMessage = message;
            txt_ConName.Focus();
        }
    }

    protected void checkCntAdrs1(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateContAddressLine(txt_ConAdd1.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txt_ConAdd2.Focus();
        }
        else
        {
            args.IsValid = false;
            adrs1Validator.ErrorMessage = message;
            txt_ConAdd1.Focus();
        }
    }

    protected void checkCntAdrs2(object source, ServerValidateEventArgs args)
    {

        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateContAddressLine(txt_ConAdd2.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txt_ConAdd3.Focus();
        }
        else
        {
            args.IsValid = false;
            adrs2Validator.ErrorMessage = message;
            txt_ConAdd2.Focus();
        }
    }

    protected void checkCntTel1(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateContactNumber(txt_ConTel1.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txt_ConTel2.Focus();
        }
        else
        {
            args.IsValid = false;
            tel1Validator.ErrorMessage = message;
            txt_ConTel1.Focus();
        }
    }

    protected void checkCntTel2(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateHomeNumber(txt_ConTel2.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            Btn_Submit.Focus();
        }
        else
        {
            args.IsValid = false;
            tel2Validator.ErrorMessage = message;
            txt_ConTel2.Focus();
        }
    }

    protected void txt_ConAdd1_TextChanged(object sender, EventArgs e)
    {
        adrs1Validator.Validate();
    }

    protected void txt_ConAdd2_TextChanged(object sender, EventArgs e)
    {
        adrs2Validator.Validate();
    }

    protected void txt_ConAdd3_TextChanged(object sender, EventArgs e)
    {
        adrs3Validator.Validate();
    }

    protected void txt_ConTel1_TextChanged(object sender, EventArgs e)
    {
        tel1Validator.Validate();
    }

    protected void txt_ConTel2_TextChanged(object sender, EventArgs e)
    {
        tel2Validator.Validate();
    }

    protected void checkCntAdrs3(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateContAddressLine(txt_ConAdd3.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txt_ConAdd4.Focus();
        }
        else
        {
            args.IsValid = false;
            adrs3Validator.ErrorMessage = message;
            txt_ConAdd3.Focus();
        }
    }

    protected void checkCntAdrs4(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateContAddressLine(txt_ConAdd4.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txt_ConTel1.Focus();
        }
        else
        {
            args.IsValid = false;
            adrs4Validator.ErrorMessage = message;
            txt_ConAdd4.Focus();
        }
    }
    protected bool checkMemberDetails(GridView gvMembers)
    {
        //LoadGridview();

        bool retVal = false;
        
        lblErrMesg.Text = "";
        int status = -1;
        string message = "";
        string PPno = "";
        string MemNam = "";
        int i = 0;

       bool dupl= HighlightDuplicate(gvMembers);

        if (!dupl)
        {
            retVal = false;
            string oldppno = "";
            foreach (GridViewRow row2 in gvMembers.Rows)
            {
                i++;
                if (row2.RowType == DataControlRowType.DataRow)
                {
                    string memId = row2.Cells[1].Text;
                    string memTitle = ((DropDownList)row2.FindControl("ddTitle")).SelectedValue.ToString().Trim();
                    string memGender = row2.Cells[4].Text.Trim().Substring(0, 1);
                    string memName = ((TextBox)row2.FindControl("txtName")).Text.Trim();
                    string memPP = ((TextBox)row2.FindControl("txtPP")).Text.Trim();
                    PPno = memPP;
                    MemNam = memName;
                    if (String.IsNullOrEmpty(MemNam) && String.IsNullOrEmpty(PPno))
                    {
                        lblErrMesg.Text = "Member details should be filled completly";
                        break;
                    }
                    else if (String.IsNullOrEmpty(MemNam))
                    {
                        lblErrMesg.Text = "Name(s) of all members should be entered";
                        break;
                    }
                    else if (String.IsNullOrEmpty(PPno))
                    {
                        lblErrMesg.Text = "Passport Number(s) of all members should be entered";
                        break;
                    }
                    //if (memPP == "")
                    //{
                    //    lblErrMesg.Text = "Passport Number of all members should be entered";
                    //    break;
                    //}
                    else if (memPP == oldppno)
                    {
                        lblErrMesg.Text = "Duplicate Passport Number entered..";
                        break;
                    }
                    else if (memTitle != "" && memTitle != "SELECT")
                    {
                        TRV_InfoValidator validator = new TRV_InfoValidator();
                        if (!validator.validateGenderWithTitle(memGender, memTitle))
                        {
                            lblErrMesg.Text = "Gender and Title does not match.";
                            lblErrMesg.Focus();
                            break;
                        }
                    }
                    else if (memTitle == "" || memTitle == "SELECT")
                    {
                        lblErrMesg.Text = "Titles of all dependents should be entered";
                        lblErrMesg.Focus();
                        break;
                    }
                    else if (memName != "")
                    {
                        status = -1;
                        message = "";
                        InfoValidator validator = new InfoValidator();
                        validator.validateGTMemberName(memName, out status, out message);

                        if (status != 0)
                        {
                            lblErrMesg.Text = message;
                            lblErrMesg.Focus();
                            break;
                        }
                    }
                    else if (memName == "")
                    {
                        lblErrMesg.Text = "Name(s) of all dependents should be entered";
                        lblErrMesg.Focus();
                        break;
                    }

                    oldppno = memPP;
                }

            }
        }
        else
        {
            lblErrMesg.Text = "Duplicate Passport Numbers entered..";
             
        }
             
        /*
        foreach (GridViewRow row in gvMembers.Rows)
        {
            i++;
            if (row.RowType == DataControlRowType.DataRow)
            {
                string memId = row.Cells[1].Text;
                string memTitle = ((DropDownList)row.FindControl("ddTitle")).SelectedValue.ToString().Trim();
                string memGender = row.Cells[4].Text.Trim().Substring(0, 1);
                string memName = ((TextBox)row.FindControl("txtName")).Text.Trim();
                string memPP = ((TextBox)row.FindControl("txtPP")).Text.Trim();
                PPno = memPP;
                MemNam = memName;
                if (memTitle != "" && memTitle != "SELECT")
                {
                    InfoValidator validator = new InfoValidator();
                    if (!validator.validateGenderWithTitle(memGender, memTitle))
                    {
                        lblErrMesg.Text = "Gender and Title does not match.";
                        lblErrMesg.Focus();
                    }
                }
                else
                {
                    lblErrMesg.Text = "Titles of all dependents should be entered";
                    lblErrMesg.Focus();
                }
                if (lblErrMesg.Text == "")
                {
                    if (memName != "")
                    {
                        status = -1;
                        message = "";
                        InfoValidator validator = new InfoValidator();
                        validator.validateGTMemberName(memName, out status, out message);

                        if (status != 0)
                        {
                            lblErrMesg.Text = message;
                            lblErrMesg.Focus();
                        }
                    }
                    else
                    {
                        lblErrMesg.Text = "Name(s) of all dependents should be entered";
                        lblErrMesg.Focus();
                    }
                }
                if (lblErrMesg.Text == "")
                {
                    if (memPP != "")
                    {
                        status = -1;
                        message = "";
                        InfoValidator validator = new InfoValidator();
                        validator.validateGTMemberPPNo(memPP, Page.User.Identity.Name, i, out status, out message);

                        if (status != 0)
                        {
                            lblErrMesg.Text = message;
                            lblErrMesg.Focus();
                        }
                    }
                    else
                    {
                        lblErrMesg.Text = "Passport Number of all members should be entered";
                        lblErrMesg.Focus();
                    }
                }
            }

            if (lblErrMesg.Text != "")
            {
                break;
            }

            //if (retVal == false && !String.IsNullOrEmpty(PPno))
            //{

            //    if (HighlightDuplicate(gvMembers))
            //    {
            //        retVal = false;
            //        string oldppno = "";
            //        foreach (GridViewRow row2 in gvMembers.Rows)
            //        {
            //            i++;
            //            if (row2.RowType == DataControlRowType.DataRow)
            //            {
            //                string memId = row2.Cells[1].Text;
            //                string memTitle = ((DropDownList)row2.FindControl("ddTitle")).SelectedValue.ToString().Trim();
            //                string memGender = row2.Cells[4].Text.Trim().Substring(0, 1);
            //                string memName = ((TextBox)row2.FindControl("txtName")).Text.Trim();
            //                string memPP = ((TextBox)row2.FindControl("txtPP")).Text.Trim();

            //                if (memPP == "")
            //                {
            //                    lblErrMesg.Text = "Passport Number of all members should be entered";
            //                    break;
            //                }
            //                if (memPP == oldppno)
            //                {
            //                    lblErrMesg.Text = "Duplicate Passport Number entered..";
            //                }
            //                oldppno = memPP;
            //            }

            //        }
            //        //if (lblErrMesg.Text == "")
            //        //{
            //        //    lblErrMesg.Text = "Dupplicate Passport Number";
            //        //}
            //    }
            //    //else
            //    //{
            //    //    retVal = true;
            //    //}
            //}
            //else
            //{
            //    if (String.IsNullOrEmpty(MemNam) && String.IsNullOrEmpty(PPno))
            //    {
            //        lblErrMesg.Text = "Member details should be filled completly";
            //    }
            //    else if (String.IsNullOrEmpty(MemNam))
            //    {
            //        lblErrMesg.Text = "Name(s) of all members should be entered";
            //    }
            //    else if (String.IsNullOrEmpty(PPno))
            //    {
            //        lblErrMesg.Text = "Passport Number(s) of all members should be entered";
            //    }

            //}
        }*/
        if (lblErrMesg.Text == "")
        {
            retVal = true;
        }
        return retVal;
    }
    public bool HighlightDuplicate(GridView grv)
    {
        bool oneTimetrue = false;
        bool duplicateRow = false;
        //use the currentRow to compare against
        for (int currentRow = 0; currentRow < grv.Rows.Count - 1; currentRow++)
        {
            GridViewRow rowToCompare = grv.Rows[currentRow];

             

            //specify otherRow as currentRow + 1
            for (int otherRow = currentRow + 1; otherRow < grv.Rows.Count; otherRow++)
            {
                GridViewRow row = grv.Rows[otherRow];

                
                string memPPOld = ((TextBox)rowToCompare.FindControl("txtPP")).Text.Trim();
                string memPPNew = ((TextBox)row.FindControl("txtPP")).Text.Trim();

                //compare cell pp_no between the two rows
                if (!String.IsNullOrEmpty(memPPOld) && !String.IsNullOrEmpty(memPPNew))
                {
                    if ((memPPOld != memPPNew) && (string.IsNullOrEmpty(memPPNew)))
                    {
                        duplicateRow = false;
                        break;
                    }
                    else if (memPPOld == memPPNew)
                    {
                        duplicateRow = true;
                        oneTimetrue = true;
                    }
                    else
                    {
                        duplicateRow = false;
                     //   oneTimetrue = false;
                    }
                }
                else if (String.IsNullOrEmpty(memPPOld) ||  String.IsNullOrEmpty(memPPNew))
                {
                    duplicateRow = false;
                   // oneTimetrue = false;
                    break;
                }
                else
                {
                    duplicateRow = true;
                   // oneTimetrue = true;
                }
                if (duplicateRow)
                {
                    /*
                    rowToCompare.BackColor = Color.Red;
                    rowToCompare.ForeColor = Color.Black;
                    row.BackColor = Color.Red;
                    row.ForeColor = Color.Black;*/

                    ((TextBox)rowToCompare.FindControl("txtPP")).BackColor = Color.MistyRose;
                    ((TextBox)row.FindControl("txtPP")).ForeColor = Color.Black;
                    ((TextBox)row.FindControl("txtPP")).BackColor = Color.MistyRose;
                    ((TextBox)row.FindControl("txtPP")).ForeColor = Color.Black;

                }
                else
                {
                   
                }
                if (duplicateRow)
                {
                    break;
                }
            }
            
        }
        return oneTimetrue;
    }
    private void LoadGridview()
    {
        if (GridView1.Rows.Count > 0)
        {
            GridView1.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            //GridView1.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone");

            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        if (gvMembers.Rows.Count > 0)
        {
            gvMembers.HeaderRow.Cells[1].Attributes.Add("data-class", "expand");
            gvMembers.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone");
            gvMembers.HeaderRow.Cells[3].Attributes.Add("data-hide", "phone");
            gvMembers.HeaderRow.Cells[4].Attributes.Add("data-hide", "phone,tablet");
            gvMembers.HeaderRow.Cells[5].Attributes.Add("data-hide", "phone,tablet");
            gvMembers.HeaderRow.Cells[6].Attributes.Add("data-hide", "phone,tablet");
            gvMembers.HeaderRow.Cells[7].Attributes.Add("data-hide", "phone,tablet");

            gvMembers.HeaderRow.TableSection = TableRowSection.TableHeader;
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

                TextBox tbPP = (TextBox)row.FindControl("txtPP");
                string txtPP = tbPP.Text;
                int index2 = txtPP.LastIndexOf(',');

                string memPP = "";

                if (index2 != -1)
                {
                    index2++;
                    memPP = tbPP.Text.Substring(index2);
                    tbPP.Text = memPP;
                }

            }
        }
    }


    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        //LoadGridview();
        //foreach (GridViewRow row in gvMembers.Rows)
        //{

        //    if (row.RowType == DataControlRowType.DataRow)
        //    {
        //        string memId = row.Cells[1].Text;
        //        string memTitle = ((DropDownList)row.FindControl("ddTitle")).SelectedValue.ToString().Trim();
        //        string memGender = row.Cells[4].Text.Trim().Substring(0, 1);
        //        string memName = ((TextBox)row.FindControl("txtName")).Text.Trim();
        //        string memPP = ((TextBox)row.FindControl("txtPP")).Text.Trim();
        //    }
        //}
        DtMembers = (DataTable)ViewState["DtMembers"];
        
        ddrPurposeValidator.Validate();
        cntNameValidator.Validate();
        adrs1Validator.Validate();
        adrs2Validator.Validate();
        adrs3Validator.Validate();
        adrs4Validator.Validate();
        tel1Validator.Validate();
        tel2Validator.Validate();
        NICDOBValidator.Validate();

        if (ddrPurposeValidator.IsValid && cntNameValidator.IsValid &&
            adrs1Validator.IsValid && adrs2Validator.IsValid &&
            adrs3Validator.IsValid && adrs4Validator.IsValid &&
            tel1Validator.IsValid && tel2Validator.IsValid && NICDOBValidator.IsValid && NICValidator2.IsValid)
        {
            formatMemDetails(gvMembers);
            bool memValid = checkMemberDetails(gvMembers);

            if (memValid)
            {
                try
                {
                    CustProfile cp = new CustProfile();
                    TextBox tb = (TextBox)gvMembers.Rows[0].FindControl("txtPP");

                    string message = cp.saveProfile_Passport(Page.User.Identity.Name, tb.Text);

                    cp = new CustProfile(User.Identity.Name);
                    passportNo = cp.O_passportNo;
                    TRV_Prop_mast trv = new TRV_Prop_mast(litQuotNo.Text);

                    TRV_PropConfirm propcnfm = new TRV_PropConfirm();
                    propcnfm.quotNo = litQuotNo.Text;
                    propcnfm.destination = hdn_plan0.Value;
                    propcnfm.departdate = lit_leaving.Text;
                    propcnfm.returndate = lit_returning.Text;
                    propcnfm.visitcntr1 = "";
                    propcnfm.visitcntr2 = "";
                    propcnfm.visitcntr3 = "";
                    propcnfm.visitcntr4 = "";
                    propcnfm.travelPurpose = ddr_purpose.SelectedItem.Text;
                    propcnfm.cntcName = txt_ConName.Text.Trim();
                    propcnfm.cntcadd1 = txt_ConAdd1.Text.Trim();
                    propcnfm.cntcadd2 = txt_ConAdd2.Text.Trim();
                    propcnfm.cntcadd3 = txt_ConAdd3.Text.Trim();
                    propcnfm.cntcadd4 = txt_ConAdd4.Text.Trim();
                    propcnfm.cntcno1 = txt_ConTel1.Text.Trim();
                    propcnfm.cntcno2 = txt_ConTel2.Text.Trim();
                    propcnfm.trvtype = trv.TRV_TYPE;
                    if (RadioButtonList1.Text == "Yes")
                    {
                        propcnfm.fullName = litName.Text;
                        propcnfm.add1 = litAddress.Text;
                        propcnfm.mobileNo = litMobileNo.Text;
                        propcnfm.title = litTitle.Text;
                        propcnfm.email = cp.O_email;
                        propcnfm.nic = cp.O_nicNo;

                    }
                    else
                    {
                        propcnfm.fullName = txtname.Text;
                        propcnfm.add1 = txtadd.Text;
                        propcnfm.add2 = txtadd0.Text;
                        propcnfm.add3 = txtadd1.Text;
                        propcnfm.add4 = txtadd2.Text;
                        propcnfm.mobileNo = txtmob.Text;
                        propcnfm.title = ddltitle.Text;
                        propcnfm.email = cp.O_email;
                        propcnfm.nic = txtnic.Text.Trim();
                    }

                    propcnfm.plan = hdn_plan.Value;
                    propcnfm.noofPerson = gvMembers.Rows.Count;
                    propcnfm.netPrmUSD = trv.NET_PREMIUM_USD;
                    propcnfm.adminFee = trv.ADMIN_FEE_RS;
                    propcnfm.polFee = trv.POLICY_FEE_RS;
                    propcnfm.nbtAmt = trv.NBT_RS;
                    propcnfm.vatAmt = trv.VAT_RS;
                    propcnfm.finalPrmRS = trv.FINAL_PREMIUM_RS;
                    propcnfm.netPrmRs = trv.NET_PREMIUM_RS;
                    propcnfm.noofDays = trv.NO_OF_DAYS;
                    propcnfm.suminsUSD = trv.SUM_INS_USD;
                    propcnfm.usdRate = trv.USD_RATE;
                    propcnfm.agtcode = trv.AGENT_CODE;
                    propcnfm.currType = "LKR";
                    propcnfm.polType = "TPI";
                    propcnfm.polsDate = trv.DEPART_DATE;
                    propcnfm.returndate = trv.RETURN_DATE;
                    propcnfm.finalPrmUSD = trv.FINAL_PREMIUM_USD;
                    propcnfm.entryUser = trv.ENTERED_BY;
                    propcnfm.discRate = trv.DiscountRate;
                    propcnfm.discAmt = trv.DiscountAmt;

                    propcnfm.taxesexp = trv.TAXES_EXPENSES_RS;
                    string IP_ADDRESS = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    propcnfm.ipAddress = IP_ADDRESS;

                    propcnfm.username = cp.O_username;
                    propcnfm.passportno = cp.O_passportNo;
                    if (propcnfm.confirm_TRV_proposal(propcnfm, gvMembers, DtMembers))
                    {
try
{
                        EncryptDecrypt dc = new EncryptDecrypt();
                        Dictionary<string, string> qs = new Dictionary<string, string>();
                        qs.Add("Ref_No", litQuotNo.Text);
                        qs.Add("Pol_No", litQuotNo.Text);
                        qs.Add("Type", "N"); // N-new businees, R-renewals
                        Response.Redirect(dc.url_encrypt("Payment.aspx", qs), false);
			log lg = new log();

                	lg.write_log("Proposal Confirm");
}
catch (Exception ex)
                {
                    log lg = new log();

                	lg.write_log(ex.Message);
                }
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
        else
        {
            lblErrMesg.Text = "Some issues in the input fields..";
            lblErrMesg.Focus();
        }
    }



    protected void checkNIC(object source, ServerValidateEventArgs args)
    {

        int status = -1;
        string message = "";
        string NIC = "";
        if (RadioButtonList1.Text == "Yes")
        {
            NIC = litNIC.Text;
        }
        else
        {
            NIC = txtnic.Text;
        }
        TRV_InfoValidator validator = new TRV_InfoValidator();
        validator.validateNIC(NIC.Trim().ToUpper(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            //txtDateOfBirth.Focus();
        }
        else
        {
            args.IsValid = false;
            NICValidator.ErrorMessage = message;
            txtnic.Focus();
        }
    }

    protected void checkDOB(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";
        string DOB = "";
        if (RadioButtonList1.Text == "Yes")
        {
            DOB = litBirthDate.Text;
        }
        else
        {
            DOB = txtdob.Text;
        }

        TRV_InfoValidator validator = new TRV_InfoValidator();
        validator.validateDateofBirth(DOB, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtadd.Focus();
        }
        else
        {
            args.IsValid = false;
            DOBValidator.ErrorMessage = message;
            //txtDateOfBirth.Focus();
        }
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TRV_Proposal prop = new TRV_Proposal();
        prop.fillDdlTitles(gvMembers);
    }

    protected void checkMobileNo(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        TRV_InfoValidator validator = new TRV_InfoValidator();
        validator.validateMobileNumber(txtmob.Text.Trim(), "LK", out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
          
        }
        else
        {
            args.IsValid = false;
            MobileNoValidator.ErrorMessage = message;
           
        }
    }

    //protected void checkEmail(object source, ServerValidateEventArgs args)
    //{
    //    int status = -1;
    //    string message = "";

    //    TRV_InfoValidator validator = new TRV_InfoValidator();
    //    validator.validateEmail(txtemail.Text.Trim(), out status, out message);

    //    if (status == 0)
    //    {
    //        args.IsValid = true;
    //        txt_ConName.Focus();
    //    }
    //    else
    //    {
    //        args.IsValid = false;
    //        EmailValidator.ErrorMessage = message;
    //        txtemail.Focus();
    //    }
    //}

    protected void txtdob_TextChanged(object sender, EventArgs e)
    {
        NICDOBValidator.Validate();
        NICValidator2.Validate();
        double ageOnArrival = 0.0;
        if (!String.IsNullOrEmpty(txtdob.Text))
        {
            DateTime dob = DateTime.ParseExact(txtdob.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime toDt = DateTime.ParseExact(lit_leaving.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            //DateTime toDt  = DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture);
           // = DateTime.ParseExact(today, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            //age = Math.Round((DateTime.Now - dob).Days / 365.25);
            ageOnArrival = Math.Round(((toDt - dob).Days / 365.25),2);

            if (ViewState["DtMembers"] == null)
            {
                TRV_Proposal_mem mem = new TRV_Proposal_mem(litQuotNo.Text, DtMembers);
            }
            else
            {

                DtMembers = (DataTable)ViewState["DtMembers"];
            }

            for (int a = 0; a < DtMembers.Rows.Count; a++)
            {
                if (DtMembers.Rows[a].ItemArray[2].ToString() == "M")
                {
                    if (hdftrv.Value == "F" && ageOnArrival < 18)
                    {
                        lblErrMesg.Text = "Proposer should be greater than 18 years for a Family Plan.";
                        Btn_Submit.Enabled = false;
                        break;
                    }
                    else if (hdftrv.Value == "F" && ageOnArrival >= 18)
                    {
                        lblErrMesg.Text = "";
                        DtMembers.Rows[a].SetField("dob", txtdob.Text);
                        DtMembers.Rows[a].SetField("age", Math.Round(ageOnArrival, 0).ToString());
                        DtMembers.AcceptChanges();
                        Btn_Submit.Enabled = true;
                    }
                    else if (hdftrv.Value != "F" && ageOnArrival < 1)
                    {
                        lblErrMesg.Text = "Proposer should be greater than 6 Months .";
                        Btn_Submit.Enabled = false;
                        break;
                    }
                    else
                    {
                        lblErrMesg.Text = "";
                        DtMembers.Rows[a].SetField("dob", txtdob.Text);
                        DtMembers.Rows[a].SetField("age", Math.Round(ageOnArrival,0).ToString());
                        DtMembers.AcceptChanges();
                        Btn_Submit.Enabled = true;
                    }
                }
            }
            
             
            gvMembers.DataSource = DtMembers;
            gvMembers.DataBind();
        }
        TRV_Proposal prop = new TRV_Proposal();
        prop.fillDdlTitles(gvMembers);

    }

    protected void gvMembers_RowDataBound(object sender, GridViewRowEventArgs e)
    {   
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex==0)
        {
            TRV_Proposal prop = new TRV_Proposal();
            prop.fillDdlTitles(gvMembers);
            DtMembers = (DataTable)gvMembers.DataSource;
            for (int i = 0; i < DtMembers.Rows.Count; i++)
            {
                if (DtMembers.Rows[i].ItemArray[2].ToString() == "M")
                {
                    TextBox t = (TextBox)e.Row.FindControl("txtName");
                    t.Text = txtname.Text;
                    if (ddltitle.SelectedValue != "")
                    {
                        DropDownList ddlTitle = (DropDownList)e.Row.FindControl("ddTitle");
                        ddlTitle.SelectedValue = ddltitle.SelectedValue;
                    }
                    break;
                }
            }
        }
    }

    protected void ddltitle_SelectedIndexChanged(object sender, EventArgs e)
    {
     
        string updt = "";
        if (ddltitle.SelectedIndex > 0)
        {
            DropDownList ddlTitles = ((DropDownList)gvMembers.Rows[0].Cells[1].FindControl("ddTitle"));
            ddlTitles.Enabled= false;
            
            if (gvMembers.DataSource == null)
            {
                DtMembers.Rows.Clear();
                TRV_Proposal_mem mem = new TRV_Proposal_mem(litQuotNo.Text, DtMembers);
            }
            else
            {
                DtMembers = (DataTable)gvMembers.DataSource;
            }

            for (int a = 0; a < DtMembers.Rows.Count; a++)
            {
                if (DtMembers.Rows[a].ItemArray[2].ToString() == "M")
                {
                    if ((ddltitle.SelectedValue == "Mr.") || (ddltitle.SelectedValue == "Master."))
                    {
                        DtMembers.Rows[a].SetField("genderdesc", "Male");
                        DtMembers.Rows[a].SetField("gender", "M");
                        DtMembers.AcceptChanges();
                        updt = "Male";
                    }
                    else if ((ddltitle.SelectedValue == "Mrs.") || (ddltitle.SelectedValue == "Miss.") || (ddltitle.SelectedValue == "Dr.(Miss.)") || (ddltitle.SelectedValue == "Dr.(Mrs.)"))
                    {
                        DtMembers.Rows[a].SetField("genderdesc", "Female");
                        DtMembers.Rows[a].SetField("gender", "F");
                        DtMembers.AcceptChanges();

                        updt = "Female";
                    }
                }
                if (DtMembers.Rows[a].ItemArray[2].ToString() == "S" && updt == "Male")
                {
                    DtMembers.Rows[a].SetField("genderdesc", "Female");
                    DtMembers.Rows[a].SetField("gender", "F");
                    DtMembers.AcceptChanges();
                }
                else if (DtMembers.Rows[a].ItemArray[2].ToString() == "S" && updt == "Female")
                {
                    DtMembers.Rows[a].SetField("genderdesc", "Male");
                    DtMembers.Rows[a].SetField("gender", "M");
                    DtMembers.AcceptChanges();
                }

            }
            gvMembers.DataSource = DtMembers;
            gvMembers.DataBind();
            ViewState["DtMembers"] = DtMembers;

            TRV_Proposal prop = new TRV_Proposal();
            prop.fillDdlTitles(gvMembers);

            Btn_Submit.Focus();
        }
        else
        {

        }
    }

    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        if (ViewState["DtMembers"] == null)
        {
            TRV_Proposal_mem mem = new TRV_Proposal_mem(litQuotNo.Text, DtMembers);
        }
        else
        {

            DtMembers = (DataTable)ViewState["DtMembers"];
        }
        for (int i = 0; i < DtMembers.Rows.Count; i++)
        {
            if (DtMembers.Rows[i].ItemArray[2].ToString() == "M")
            {
                TextBox t = (TextBox)gvMembers.Rows[i].FindControl("txtName");
                t.Text = txtname.Text;
                if (ddltitle.SelectedValue != "")
                {
                    DropDownList ddlTitle = (DropDownList)gvMembers.Rows[i].FindControl("ddTitle");
                    ddlTitle.SelectedValue = ddltitle.SelectedValue;
                }
                break;
            }
        }
    }

    protected void ddTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        string updt = "";
        TRV_Prop_mast trv = new TRV_Prop_mast(litQuotNo.Text);
        DropDownList ddlTitles = ((DropDownList)gvMembers.Rows[0].Cells[1].FindControl("ddTitle"));
        

        if(trv.TRV_TYPE == "F" && (ddlTitles.SelectedValue !=ddltitle.SelectedValue))
        {
            ddltitle.SelectedValue = ddlTitles.SelectedValue;
            if (ViewState["DtMembers"] != null)
            {
                DtMembers = (DataTable)ViewState["DtMembers"];

                for (int a = 0; a < DtMembers.Rows.Count; a++)
                {
                    if (DtMembers.Rows[a].ItemArray[2].ToString() == "M")
                    {
                        if ((ddlTitles.SelectedValue == "Mr.") || (ddlTitles.SelectedValue == "Master."))
                        {
                            DtMembers.Rows[a].SetField("genderdesc", "Male");
                            DtMembers.Rows[a].SetField("gender", "M");
                            DtMembers.AcceptChanges();
                            updt = "Male";

                        }
                        else if ((ddlTitles.SelectedValue == "Mrs.") || (ddlTitles.SelectedValue == "Miss.") || (ddlTitles.SelectedValue == "Dr.(Miss.)") || (ddlTitles.SelectedValue == "Dr.(Mrs.)"))
                        {
                            DtMembers.Rows[a].SetField("genderdesc", "Female");
                            DtMembers.Rows[a].SetField("gender", "F");
                            DtMembers.AcceptChanges();

                            updt = "Female";
                        }
                    }
                    if (DtMembers.Rows[a].ItemArray[2].ToString() == "S" && updt == "Male")//
                    {
                        DtMembers.Rows[a].SetField("genderdesc", "Female");
                        DtMembers.Rows[a].SetField("gender", "F");
                        DtMembers.AcceptChanges();
                    }
                    else if (DtMembers.Rows[a].ItemArray[2].ToString() == "S" && updt == "Female")// 
                    {
                        DtMembers.Rows[a].SetField("genderdesc", "Male");
                        DtMembers.Rows[a].SetField("gender", "M");
                        DtMembers.AcceptChanges();
                    }

                }

            }
        }
        if (ViewState["DtMembers"] != null)
        {
            DtMembers = (DataTable)ViewState["DtMembers"];
            
            for (int b = 1; b < DtMembers.Rows.Count;b++)
            {
                DropDownList ddlTitles2 = ((DropDownList)gvMembers.Rows[b].Cells[1].FindControl("ddTitle"));
                if (trv.TRV_TYPE == "F" && (ddlTitles.SelectedValue == ddlTitles2.SelectedValue) && (DtMembers.Rows[b].ItemArray[2].ToString() == "S"))
                {
                    lblErrMesg.Text = "Spouse and Main Life gender should be different..";
                }
                else
                {
                    lblErrMesg.Text = "";
                }
            }
            
        }
        Btn_Submit.Focus();
    }

    protected void txtnic_TextChanged(object sender, EventArgs e)
    {
        NICValidator2.Validate();
        NICDOBValidator.Validate();
    }

    protected void checkNICDOB(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";
        string Gender = "";
        string NIC = "";
        string DOB = "";
        if (RadioButtonList1.Text == "Yes")
        {
            NIC = litNIC.Text;
            DOB = litBirthDate.Text;
        }
        else
        {
            NIC = txtnic.Text;
            DOB = txtdob.Text;
        }

        if (ViewState["DtMembers"] != null)
        {
            DtMembers = (DataTable)ViewState["DtMembers"];
            for (int a = 0; a < DtMembers.Rows.Count; a++)
            {
                if (DtMembers.Rows[a].ItemArray[2].ToString() == "M")
                {
                    Gender = DtMembers.Rows[a].ItemArray[3].ToString();
                }
            }
        }
                TRV_InfoValidator validator = new TRV_InfoValidator();
        validator.checkNICWithDob(DOB, Gender,   NIC.ToUpper() , out status);

        if (status == 0)
        {
            args.IsValid = true;
            //txtDateOfBirth.Focus();
        }
        else
        {
            args.IsValid = false;
            NICValidator.ErrorMessage = "NIC and Date Of Birth doesn't Matched";
            txtnic.Focus();
        }
    }





    protected void checkNICExist(object source, ServerValidateEventArgs args)
    {
        int status = 0;
        if (RadioButtonList1.SelectedValue == "No")
        {
            CustProfile cp = new CustProfile(User.Identity.Name);
            if (!String.IsNullOrEmpty(cp.O_nicNo))
            {
                if (txtnic.Text.ToUpper() == cp.O_nicNo.ToUpper())
                {
                    status = 1;
                }
                else
                {
                    status = 0;
                }
            }
        }
        if (status == 0)
        {
            args.IsValid = true;
            //txtDateOfBirth.Focus();
        }
        else
        {
            args.IsValid = false;
            NICValidator2.ErrorMessage = "Proposer NIC is same as your registered NIC.";
            txtnic.Focus();
        }
    }



    protected void gvMembers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
    }

    protected void txtadd_TextChanged(object sender, EventArgs e)
    {
        NICValidator2.Validate();
        NICDOBValidator.Validate();
    }



    //protected void chkYes_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkYes.Checked)
    //    {
    //        chkNo.Checked = false;
    //        txt_no.Enabled = false;
    //    }
    //}

    //protected void chkNo_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkNo.Checked)
    //    {
    //        chkYes.Checked = false;
    //        txt_no.Enabled = true;
    //    }
    //}

    protected void txtadd0_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtmob_TextChanged(object sender, EventArgs e)
    {

    }

    protected void rdo_address_SelectedIndexChanged(object sender, EventArgs e)
    {
        TRV_Prop_mast trv = new TRV_Prop_mast(litQuotNo.Text);

        if (RadioButtonList1.SelectedValue == "Yes")
        {
            if (rdo_address.SelectedValue == "No")
            {
                txt_ConAdd1.Text = "";
                txt_ConAdd2.Text = "";
                txt_ConAdd3.Text = "";
                txt_ConAdd4.Text = "";
            }
            else
            {
                txt_ConAdd1.Text = trv.ADDRESS1;
                txt_ConAdd2.Text = trv.ADDRESS2;
                txt_ConAdd3.Text = trv.ADDRESS3;
                txt_ConAdd4.Text = trv.ADDRESS4;
            }
        }
        else if (RadioButtonList1.SelectedValue == "No")
        {
            if (rdo_address.SelectedValue == "Yes")
            {
                txt_ConAdd1.Text = txtadd.Text;
                txt_ConAdd2.Text = txtadd0.Text;
                txt_ConAdd3.Text = txtadd1.Text;
                txt_ConAdd4.Text = txtadd2.Text;
            }
            else
            {
                txt_ConAdd1.Text = "";
                txt_ConAdd2.Text = "";
                txt_ConAdd3.Text = "";
                txt_ConAdd4.Text = "";
            }
        }
    }
}