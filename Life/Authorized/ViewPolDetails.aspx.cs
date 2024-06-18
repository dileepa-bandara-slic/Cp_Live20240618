using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using iTextSharp.text.html;



public partial class Life_Authorized_ViewPolDetails : System.Web.UI.Page
{
    EncryptDecrypt enc = new EncryptDecrypt();
    string mat;
    string mat1;
    string mat2;
    string claim;
    string pol;
    string paoad1;
    string paoad2;
    string padsja;
    int ppayamt;
    int pvbonus;
    int pibonus;
    int ploncap;
    int plonint;
    int dptam;
    int pdprm;
    int pdefprm;
    int pdefint;
    int lmlon;
    string lmcdt;
    int pdedam1;
    int pdedam2;
    int stamp_fee;
    string errorMsg;
    public int tot;
    int ded;
    int otded;
    int net;
    private ReadAmountInWords readAmt = new ReadAmountInWords();
    private string amnt;
    DataManager dm;
    private string seqn;
    private string docname;
    private string Sqno;
    private int maxdoc;
    private int docid;
    OracleConnection con;
    OracleCommand cmd;
    private string dis;
    private string im_nt;
    private string res_fm;
    private string nic;
    private string pass_bk;
    private string pol_doc;
    private string aff;
    private int pol1;
    private int loan2;
    private int date2;
    private string cc;
    private string to_add;
    private string to_ad;
    private string from;
    private string sub;
    private string body;
    private string email;
    private int lamt;



    public string setDate()
    {
        string[] datetime = new string[2];
        string year = System.DateTime.Now.Year.ToString();
        string month = System.DateTime.Now.Month.ToString();
        string day = System.DateTime.Now.Day.ToString();
        string ourDate;
        if (month.Length < 2)
        {
            month = "0" + month;
        }
        if (day.Length < 2)
        {
            day = "0" + day;
        }

        ourDate = year + month + day;
        datetime[0] = ourDate;

        return ourDate;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack)
        {
            PaneName.Value = Request.Form[PaneName.UniqueID];
        }

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
                        string polStatus = paraList["PolStatus"];
                        hfPolNum.Value = polNo;
                        hlRequestForm.NavigateUrl = "PolDetailsRequestForm.aspx?userName=" + Page.User.Identity.Name + "&policyNo=" + polNo;
                        LifeCustomer customer = new LifeCustomer();
                        string viewStatus = customer.viewPolicyDetails(polNo, polStatus, gvDemands, gvDeposits);

                        if (viewStatus == "N")
                        {
                            Panel1.Visible = true;
                            Panel2.Visible = false;
                            Panel3.Visible = false;
                        }
                        else if (viewStatus == "P")
                        {
                            Panel1.Visible = false;
                            Panel2.Visible = true;
                            Panel3.Visible = false;
                        }
                        else if (viewStatus == "Y")
                        {
                            litPolNumber.Text = polNo;
                            litName.Text = customer.name;
                            litAddr1.Text = customer.address1;
                            litAddr2.Text = customer.address2;
                            litAddr3.Text = customer.address3;
                            litAddr4.Text = customer.address4;
                            litSumIns.Text = customer.sumInsured.ToString("N2");
                            litPolType.Text = customer.tableDesc;
                            litPolTerm.Text = customer.term;
                            litPolMode.Text = customer.mode;
                            litComDate.Text = customer.commDate;
                            litPolStatus.Text = customer.polStatus;
                            HiddenField26.Value = customer.sta;

                            customer.getmaturity(polNo);
                            string view = customer.viewStatus;

                            if (view == "Y")
                            {

                                //    this.Panel5.Visible = true;
                                //    this.Panel6.Visible = true;
                                //    this.Panel7.Visible = true;

                                //    mat = customer.maturity.ToString();
                                //    mat1 = customer.maturity1.ToString();
                                //    mat2 = customer.maturity2.ToString();
                                //    claim = customer.claim.ToString();
                                //    pol = customer.pol.ToString();
                                //    paoad1 = customer.paoad1.ToString();
                                //    paoad2 = customer.paoad2.ToString();
                                //    padsja = customer.padsja.ToString();
                                //    ppayamt = customer.ppayamt;
                                //    pvbonus = customer.pvbonus;
                                //    pibonus = customer.pibonus;
                                //    ploncap = customer.ploncap;
                                //    plonint = customer.plonint;
                                //    pdefprm = customer.pdefprm;
                                //    pdefint = customer.pdefint;
                                //    pdedam1 = customer.pdedam1;
                                //    pdedam2 = customer.pdedam2;
                                //    stamp_fee = customer.stamp_fee;
                                //    lmlon = customer.lmlon;
                                //    lmcdt = customer.lmcdt.ToString();
                                //    dptam = customer.dptam;
                                //    pdprm = customer.pdprm;

                                //    tot = ppayamt + pibonus + pvbonus;
                                //    otded = pdedam1 + pdedam2;
                                //    ded = otded + stamp_fee + pdefprm + pdefint + lmlon;
                                //    net = tot - ded;

                                this.HiddenField1.Value = mat;
                            this.HiddenField2.Value = mat1;
                            this.HiddenField27.Value = mat2;
                            this.HiddenField3.Value = claim;
                            this.HiddenField4.Value = pol;
                            this.HiddenField5.Value = paoad1;
                            this.HiddenField6.Value = paoad2;
                            this.HiddenField7.Value = padsja;
                            this.HiddenField8.Value = ppayamt.ToString();
                            this.HiddenField9.Value = pvbonus.ToString();
                            this.HiddenField10.Value = pibonus.ToString();
                            this.HiddenField11.Value = ploncap.ToString();
                            this.HiddenField12.Value = plonint.ToString();
                            this.HiddenField13.Value = pdefprm.ToString();
                            this.HiddenField14.Value = pdefint.ToString();
                            this.HiddenField15.Value = pdedam1.ToString();
                            this.HiddenField16.Value = pdedam2.ToString();
                            this.HiddenField17.Value = stamp_fee.ToString();
                            this.HiddenField18.Value = lmlon.ToString();
                            this.HiddenField19.Value = lmcdt;
                            this.HiddenField20.Value = dptam.ToString();
                            this.HiddenField21.Value = pdprm.ToString();
                            this.HiddenField22.Value = tot.ToString();
                            this.HiddenField23.Value = otded.ToString();
                            this.HiddenField24.Value = ded.ToString();
                            this.HiddenField25.Value = net.ToString();

                            }

                            else
                            {
                                //    //this.Panel5.Visible = false;
                                //    //this.Panel6.Visible = false;
                                //    //this.Panel7.Visible = false;
                            }


                            customer.getloan_det(polNo);
                            string view1 = customer.viewStatus1;

                            if (view1 == "Y")
                            {
                                string dt = DateTime.Now.ToString("yyyyMMdd");
                                Label6.Text = DateTime.Now.ToString("yyyy/MM/dd");


                               
                                Label8.Text = customer.loanNum.ToString();
                                Label4.Text = customer.grantDate.ToString();
                                Label3.Text = Convert.ToDecimal(customer.grantAmt.ToString().Trim()).ToString("#,##0.00");
                                Label5.Text = customer.interest.ToString();
                                //Label5.Text = Convert.ToDecimal(customer.interest.ToString().Trim()).ToString("#,##0.00");
                               // Label13.Text = customer.cap.ToString();
                                Label13.Text= Convert.ToDecimal(customer.cap.ToString().Trim()).ToString("#,##0.00");
                                Label14.Text = Convert.ToDecimal(customer.ints.ToString().Trim()).ToString("#,##0.00");
                               // Label14.Text = customer.ints.ToString();
                                Label12.Text = customer.repay.ToString();

                                pol1 = Convert.ToInt32(polNo);
                                loan2 = Convert.ToInt32(Label8.Text);
                                date2 = Convert.ToInt32(dt);
                                //lamt= Convert.ToInt32(Label3.Text);

                                customer.outstanding(pol1, loan2, date2);

                                // Label9.Text = Convert.ToString(customer.grantAmt - customer.cap);
                                //Label9.Text = customer.p_CapitalAsAtDeath.ToString("N2");
                                //Label10.Text = customer.p_InterestAsAtDeath.ToString("N2");
                                //Label9.Text = customer.capital_to_pay.ToString();
                                Label9.Text = Convert.ToDecimal(customer.capital_to_pay.ToString().Trim()).ToString("#,##0.00");
                                Label10.Text = Convert.ToDecimal(customer.interst_to_pay.ToString().Trim()).ToString("#,##0.00");
                               // Label10.Text = customer.interst_to_pay.ToString();
                                

                                double total = customer.capital_to_pay + customer.interst_to_pay;
                                //Label11.Text = total.ToString();
                                Label11.Text = Convert.ToDecimal(total.ToString().Trim()).ToString("#,##0.00");

                                double tot = customer.cap + customer.ints;
                                //Label15.Text = tot.ToString();
                                Label15.Text = Convert.ToDecimal(tot.ToString().Trim()).ToString("#,##0.00");

                            }

                            else
                            {
                                this.Panel8.Visible = false;
                            }

                            if (gvDemands.Rows.Count > 0)
                            {
                                litLastPdDue.Text = gvDemands.Rows[0].Cells[0].Text;
                                litLastPdAmt.Text = gvDemands.Rows[0].Cells[1].Text;
                                litLastPdDate.Text = gvDemands.Rows[0].Cells[2].Text;
                            }
                            else
                            {
                                litLastPdDue.Text = "-";
                                litLastPdAmt.Text = "-";
                                litLastPdDate.Text = "-";
                            }

                            Panel1.Visible = false;
                            Panel2.Visible = false;
                            Panel3.Visible = true;
                        }
                        else
                        {
                            lblErrMesg.Text = "Error while retrieving Policy details";
                            Panel1.Visible = false;
                            Panel2.Visible = false;
                            Panel3.Visible = false;
                        }

                        string message = customer.getOnlinePayments(polNo, gvOnlPaymnts);
                    }
                    else
                    {
                        errorMsg = "No valid Parameters Passed";
                        Server.Transfer("ErrorPage.aspx");
                    }
                }
            }
        }
        Load();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!chkAgree.Checked)
        {
            lblErrMesg.Text = "Please tick I agree and submit.";
        }
        else
        {
            LifeCustomer customer = new LifeCustomer();
            string mesg = customer.setPolicyViewStatus(Page.User.Identity.Name, hfPolNum.Value, "P");

            if (mesg == "success")
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
                Panel3.Visible = false;
            }
            else
            {
                lblErrMesg.Text = mesg;
            }
        }
        Load();
    }

    private void Load()
    {
        if (gvOnlPaymnts.Rows.Count > 0)
        {
            gvOnlPaymnts.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            gvOnlPaymnts.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone");

            gvOnlPaymnts.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        if (gvDemands.Rows.Count > 0)
        {
            gvDemands.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            gvDemands.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone");

            gvDemands.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        if (gvDeposits.Rows.Count > 0)
        {
            gvDeposits.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            gvDeposits.HeaderRow.Cells[1].Attributes.Add("data-class", "expand");

            gvDeposits.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        //litPolNumber.Text = "2927556";
        hfAccordionIndex.Value = "Y";

        bool laps = false;
        bool flag1 = true;
        int InsTobePaid = 0;
        double Tot_Amt_TobePaid = 0;
        bool proceedTO2 = false;
        bool premast_found = false;



        LifePolicy lp = new LifePolicy(Convert.ToInt64(litPolNumber.Text.Trim()), Convert.ToInt32(txt_year.Text.Trim()));

        if (lp.master_exist)
        {
            int CommnceDate = lp.ComDate;
            int CommenceYear = lp.ComYear;
            int CurrentYear = int.Parse(txt_year.Text);
            string NextYear = (CurrentYear + 1).ToString() + "03" + "31";
            int Next_Year = int.Parse(NextYear);
            int Nxt_Year = CurrentYear + 1;

            string Curr_year = CurrentYear.ToString() + "04" + "01";
            int Curr_yr = int.Parse(Curr_year);
            // Get the Maturity date of the Policy
            string GetTerms = "select pmtrm from lphs.policymaster   where pmpol='" + Convert.ToInt32(litPolNumber.Text) + "'";
            int Terms = lp.Term;
            int matur_Year = CommenceYear + Terms;
            string MachYear = matur_Year.ToString() + CommnceDate.ToString().Substring(4, 4);
            int MaturityYear = int.Parse(MachYear);

            if (1962 > CommnceDate || CommnceDate > Next_Year)
            {
                this.msg.Text = " You are trying to get a Certificate before commenced year for Policy No : " + litPolNumber.Text;
            }
            else if (Curr_yr >= MaturityYear)
            {
                this.msg.Text = " You are trying to get a certificate After the Maturity year for the Policy No :" + litPolNumber.Text;
            }
            else if (CurrentYear < 1962)
            {
                this.msg.Text = "Invalid Tax Year.Please enter Valid Tax Year";
            }
            else
            {

                if (CurrentYear > (System.DateTime.Now.Year))
                {
                    msg.Text = " Tax year cannot exceed the current year ";
                }
                else
                {
                    if (lp.dis_hon)
                    {
                        msg.Text = "Cannot Issue Tax Certificate..Dishonor Cheques Found...";
                    }
                    else
                    {
                        if (lp.premast_found)
                        {
                            proceedTO2 = true;
                        }
                        else
                        {
                            if (lp.lapse_found)
                            {
                                if (lp.DueDate > Convert.ToInt32(txt_year.Text.Trim() + "04"))
                                {
                                    proceedTO2 = true;
                                }
                                else
                                {
                                    msg.Text = " This Policy Number has been lapsed before this Tax Year." +
                                                                                                      "So Cannot Issue the Tax Certificate.";
                                }
                            }
                            else
                            {

                            }

                        }

                        if (proceedTO2)
                        {
                            #region next page
                            int endYear = Convert.ToInt32(txt_year.Text.Trim()) + 1;
                            //txbTaxYear.Text = taxYear + " / 04/ 01 To " + endYear.ToString() + "  / 03/ 31";

                            string StDate = txt_year.Text.Trim();
                            string EndDate = Convert.ToString(endYear);
                            int stdat = Convert.ToInt32(StDate + "0401");
                            int enddat = Convert.ToInt32(EndDate + "0331");


                            #region Get Current Date
                            string CurrMonth;
                            string CurrDay;
                            if (DateTime.Now.Month.ToString().Length != 2)
                            {
                                CurrMonth = "0" + DateTime.Now.Month.ToString();
                            }
                            else
                            {
                                CurrMonth = DateTime.Now.Month.ToString();
                            }
                            if (DateTime.Now.Day.ToString().Length != 2)
                            {
                                CurrDay = "0" + DateTime.Now.Day.ToString();
                            }
                            else
                            {
                                CurrDay = DateTime.Now.Day.ToString();
                            }

                            string CurrentDate = DateTime.Now.Year.ToString() + CurrMonth + CurrDay;//Get The Current Date
                            int CurrentDate_Int = int.Parse(CurrentDate);
                            #endregion

                            #region Check whether the given tax year is Current Tax year

                            //lg.write_log("1. " + stdat.ToString() + " | " + CurrentDate_Int.ToString() + " | " + enddat);

                            if (stdat < CurrentDate_Int && CurrentDate_Int < enddat)
                            {
                                //lg.write_log("A");
                                //lg.write_log("2. " + lp.ds_amount_paid.Tables[0].Rows.Count.ToString() + " || " + lp.premast);
                                if (lp.ds_amount_paid.Tables[0].Rows.Count != 0) //premium.Count != 0)
                                {
                                    //lg.write_log("A1");
                                    flag1 = true;
                                    //this.txbAmtPaid.Text = Tot_AmountPaid.ToString("N2");
                                    //Tot_Inst_Paid = int.Parse(premium[1].ToString());

                                    //===========Get to be paid installments
                                    DateTime Enddate = Convert.ToDateTime("03/31/" + Nxt_Year.ToString());
                                    DateTime Duedate = Convert.ToDateTime(lp.lastDue_All.ToString().Substring(4, 2) + "/" + "01" + "/" + lp.lastDue_All.ToString().Substring(0, 4));
                                    TimeSpan diff = Enddate.Subtract(Duedate);
                                    int totalMonths = (int)diff.TotalDays / 30;

                                    if (lp.mode == 1)
                                    {
                                        InsTobePaid = 1 - lp.maxDueCount;
                                    }
                                    if (lp.mode == 2)
                                    {
                                        InsTobePaid = 2 - lp.maxDueCount;
                                    }
                                    if (lp.mode == 3)
                                    {
                                        InsTobePaid = 4 - lp.maxDueCount;
                                    }
                                    if (lp.mode == 4)
                                    {
                                        InsTobePaid = 12 - lp.maxDueCount;
                                    }
                                    if (InsTobePaid > 0)
                                    {
                                        Tot_Amt_TobePaid = InsTobePaid * lp.Premiumamt;
                                        //this.txbAmtPayable.Text = Tot_Amt_TobePaid.ToString("N2");
                                    }
                                    else
                                    {
                                        InsTobePaid = 0;
                                        Tot_Amt_TobePaid = InsTobePaid * lp.Premiumamt;
                                        //this.txbAmtPayable.Text = Tot_Amt_TobePaid.ToString("N2");
                                    }
                                    if (totalMonths >= 6 && CurrentDate_Int > enddat)
                                    {
                                        //this.txbAmtPayable.Text = "0.00";
                                        //Session["Laps"] = "LAPS";
                                        laps = true;
                                    }
                                }
                                else if (lp.ds_amount_paid.Tables[0].Rows.Count == 0 && lp.premast != 0)
                                {
                                    // lg.write_log("A2");
                                    flag1 = true;
                                    //this.txbAmtPaid.Text = "0.00";
                                    //Tot_Inst_Paid = 0;
                                    lp.total_paid_inst = 0;
                                    //===========Get to be paid installments

                                    DateTime Enddate = Convert.ToDateTime("03/31/" + Nxt_Year.ToString());
                                    DateTime Duedate = Convert.ToDateTime(lp.lastDue_All.ToString().Substring(4, 2) + "/" + "01" + "/" + lp.lastDue_All.ToString().Substring(0, 4));
                                    TimeSpan diff = Enddate.Subtract(Duedate);
                                    int totalMonths = (int)diff.TotalDays / 30;

                                    if (lp.mode == 1)
                                    {
                                        InsTobePaid = 1;
                                    }
                                    if (lp.mode == 2)
                                    {
                                        InsTobePaid = 2;
                                    }
                                    if (lp.mode == 3)
                                    {
                                        InsTobePaid = 4;
                                    }
                                    if (lp.mode == 4)
                                    {
                                        InsTobePaid = 12;
                                    }

                                    Tot_Amt_TobePaid = InsTobePaid * lp.Premiumamt;
                                    //this.txbAmtPayable.Text = Tot_Amt_TobePaid.ToString("N2");

                                    if (totalMonths >= 6 && CurrentDate_Int > enddat)
                                    {
                                        //this.txbAmtPayable.Text = "0.00";
                                        //Session["Laps"] = "LAPS";
                                        laps = true;
                                    }

                                }
                                else
                                {

                                    //lg.write_log("A3");

                                    flag1 = false;
                                    //string SelectLps = "Select LPPOL from lphs.liflaps   where LPPOL=" + PLNum + " ";
                                    //int polno = ledman.GetPolNo(SelectLps);
                                    if (lp.lapse_found)
                                    {
                                        this.msg.Text = " This Policy Number has been lapsed ";
                                        //this.btnview.Enabled = false;
                                    }
                                    else
                                    {
                                        Session["polno"] = "Policy no can not be found in the Ledger Table";
                                        this.msg.Text = "No Premium payments were done during the given period of time";
                                        //this.btnview.Enabled = false;
                                    }
                                }
                            }
                            #endregion
                            else if (CurrentDate_Int > enddat && stdat < CurrentDate_Int)
                            {
                                //lg.write_log("A4");
                                if (lp.ds_amount_paid.Tables[0].Rows.Count != 0)
                                {
                                    //this.txbAmtPaid.Text = Tot_AmountPaid.ToString("N2");
                                    //Tot_Inst_Paid = int.Parse(premium[1].ToString());
                                    //this.txbAmtPayable.Text = "0.00";
                                }
                                else
                                {
                                    //this.txbAmtPaid.Text = Tot_AmountPaid.ToString("N2");
                                    //Tot_Inst_Paid = 0;
                                    //this.txbAmtPayable.Text = "0.00";
                                    lp.total_paid_inst = 0;
                                }
                            }


                            #region Check the Commenced Date within the Tax year

                            if (endYear == lp.ComYear || int.Parse(stdat.ToString().Substring(0, 4)) == lp.ComYear)
                            {
                                //lg.write_log("B2");
                                if (stdat < CurrentDate_Int && CurrentDate_Int < enddat)
                                {
                                    //lg.write_log("B3");
                                    if (lp.ds_amount_paid.Tables[0].Rows.Count != 0)
                                    {
                                        //lg.write_log("B4");
                                        //flag1 = true;
                                        //this.txbAmtPaid.Text = Tot_AmountPaid.ToString("N2");
                                        //Tot_Inst_Paid = int.Parse(premium[1].ToString());

                                        //===========Get to be paid installments
                                        DateTime Enddate = Convert.ToDateTime("03/31/" + Nxt_Year.ToString());
                                        DateTime Duedate = Convert.ToDateTime(lp.lastDue_All.ToString().Substring(4, 2) + "/" + "01" + "/" + lp.lastDue_All.ToString().Substring(0, 4));
                                        TimeSpan diff = Enddate.Subtract(Duedate);
                                        int totalMonths = (int)diff.TotalDays / 30;

                                        if (lp.mode == 1) { InsTobePaid = ((totalMonths / 12) - lp.maxDueCount); }
                                        if (lp.mode == 2)
                                        {
                                            if (totalMonths < 6) { InsTobePaid = 1; }
                                            else { InsTobePaid = ((totalMonths / 6) - lp.maxDueCount); }
                                        }
                                        if (lp.mode == 3) { InsTobePaid = ((totalMonths / 3) - lp.maxDueCount); }
                                        if (lp.mode == 4) { InsTobePaid = (totalMonths - lp.maxDueCount); }

                                        Tot_Amt_TobePaid = InsTobePaid * lp.Premiumamt;
                                        //this.txbAmtPayable.Text = Tot_Amt_TobePaid.ToString("N2");



                                    }
                                    else
                                    {
                                        //lg.write_log("B5");
                                        flag1 = false;
                                        //string SelectLps = "Select LPPOL from lphs.liflaps   where LPPOL=" + PLNum + " ";
                                        //int polno = ledman.GetPolNo(SelectLps);
                                        if (lp.lapse_found)
                                        {
                                            this.msg.Text = " This Policy Number has been lapsed ";
                                            //this.btnview.Enabled = false;
                                        }
                                        else
                                        {
                                            Session["polno"] = "Policy no can not be found in the Ledger Table";
                                            this.msg.Text = "No Premium payments were done during the given period of time";
                                            //this.btnview.Enabled = false;
                                        }
                                    }
                                }
                                else
                                {
                                    //lg.write_log("B6");
                                    //this.txbAmtPaid.Text = Tot_AmountPaid.ToString("N2");
                                    //Tot_Inst_Paid = int.Parse(premium[1].ToString());
                                    //this.txbAmtPayable.Text = "0.00";
                                }
                            }
                            #endregion

                            #region Check whether the Maturity date within the Tax Year

                            if (lp.MaturityDate > stdat && lp.MaturityDate < enddat)
                            {
                                //Get The Month Difference
                                DateTime MaturityDate = Convert.ToDateTime(lp.MaturityDate.ToString().Substring(4, 2) + "/01/" + lp.MaturityDate.ToString().Substring(0, 4));
                                DateTime Duedate = Convert.ToDateTime(lp.lastDue_All.ToString().Substring(4, 2) + "/" + "01" + "/" + lp.lastDue_All.ToString().Substring(0, 4));
                                TimeSpan diffToPay = MaturityDate.Subtract(Duedate);
                                int ttodaysToPay = (int)diffToPay.Days;
                                int totalMonthsToPay = ttodaysToPay / 30; //I have conside 30 days a monthsResponse.Write(totalMonths.ToString());

                                if (lp.mode == 1) { InsTobePaid = ((totalMonthsToPay / 12)); }
                                if (lp.mode == 2)
                                {
                                    InsTobePaid = (totalMonthsToPay / 6);
                                }
                                if (lp.mode == 3) { InsTobePaid = (totalMonthsToPay / 3); }
                                if (lp.mode == 4) { InsTobePaid = totalMonthsToPay; }


                                // Deduct last installment from matured policies. Should be tested
                                if (InsTobePaid > 0)
                                    InsTobePaid = InsTobePaid - 1;

                                if (int.Parse(stdat.ToString().Substring(0, 4)) == int.Parse(CurrentDate_Int.ToString().Substring(0, 4)) || int.Parse(CurrentDate_Int.ToString().Substring(0, 4)) == (Convert.ToInt32(txt_year.Text.Trim()) + 1))
                                {
                                    Tot_Amt_TobePaid = InsTobePaid * lp.Premiumamt;
                                    //this.txbAmtPayable.Text = Tot_Amt_TobePaid.ToString("N2");
                                }
                                else
                                {
                                    //this.txbAmtPayable.Text = "0.00";
                                }

                            }

                            #endregion

                            if (flag1)
                            {
                                DataTable table = new DataTable();

                                table.Columns.Add("Policy No.", typeof(string));
                                table.Columns.Add("Sum Assured Rs.", typeof(string));
                                table.Columns.Add("Mode", typeof(string));
                                table.Columns.Add("Premium Installment Rs.", typeof(string));
                                table.Columns.Add("No.of Installments", typeof(string));
                                table.Columns.Add("H2", typeof(string));

                                table.Rows.Add(litPolNumber.Text, lp.sumAssured.ToString("N2"), lp.Str_mode, lp.Premiumamt.ToString("N2"), lp.total_paid_inst.ToString(), "**");

                                if (!laps && InsTobePaid != 0)
                                    table.Rows.Add(litPolNumber.Text, lp.sumAssured.ToString("N2"), lp.Str_mode, lp.Premiumamt.ToString("N2"), InsTobePaid.ToString(), "*");

                                string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                                //Response.Write("<script> alert('The document will be downloaded'); </script>");

                                string refno = "";
                                bool result = false;
                                TaxCert tc = new TaxCert();
                                tc.enter_tax_certicates(Convert.ToInt32(txt_year.Text.Trim()), Page.User.Identity.Name.ToString(), ip, Convert.ToInt32(litPolNumber.Text), out result, out refno);
                                if (result)
                                    lp.print_tax(Page.User.Identity.Name.ToString(), ip, refno, Convert.ToInt32(txt_year.Text.Trim()), Tot_Amt_TobePaid, table);




                            }
                            // else
                            //this.msg.Text = "Lapsed";
                            #endregion
                        }
                    }
                }
            }

        }
        else
        {
            msg.Text = "Given Policy Number Does not exsist";
        }
    }

    protected void Button8_Click(object sender, EventArgs e)
    {

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
      //  hfAccordionIndex1.Value = "Y";

        if (litPolType.Text == "DIVI THILINA - Endowment Assurance")
        {
          //  print2();
        }

        else if (litPolType.Text == "Early Cash")
        {
           // print4();
        }
    }

    private void print2()
    {
        Document document = new Document(PageSize.A4, 50, 50, 25, 25);
        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        document.Open();
        Font titleFont1 = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font boldTableFont = FontFactory.GetFont("Times New Roman", 11, Font.BOLD);
        Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 11, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont4_bold = FontFactory.GetFont("Times New Roman", 7, Font.BOLD);
        Font bodyFont5 = FontFactory.GetFont("Times New Roman", 7, Font.NORMAL);
        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);
        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font bodyFont2_bold_und = FontFactory.GetFont("Times New Roman", 10, Font.BOLD | Font.UNDERLINE);

        string fontpath = System.Web.HttpContext.Current.Server.MapPath("~/Font/sa_____.TTF");
        var bf = BaseFont.CreateFont(fontpath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        var sinhalaFont = new Font(bf, 11, Font.NORMAL);

        iTextSharp.text.Image signature = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Capture.jpg"));
        signature.ScalePercent(55f);
        signature.SetAbsolutePosition(70, 75);

        document.Add(new Paragraph("\n\n"));
        int[] clmwidths111 = { 60, 40 };
        PdfPTable tbl14 = new PdfPTable(2);
        tbl14.SetWidths(clmwidths111);

        tbl14.WidthPercentage = 100;
        tbl14.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl14.SpacingBefore = 10;
        tbl14.SpacingAfter = 0;
        tbl14.DefaultCell.Border = 0;

        PdfPCell cell = new PdfPCell(new Phrase(this.HiddenField26.Value + litName.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        //  tbl14.AddCell(new Phrase("wfma fhduqj PS$ysusluz$ "+ this.HiddenField3.Value, sinhalaFont));

        Phrase phrase = new Phrase();
        phrase.Add(
            new Chunk("wfma fhduqj PS$ysusluz$", new Font(bf, 11, Font.NORMAL))
        );
        phrase.Add(new Chunk("1" + this.HiddenField3.Value, new Font()));
        tbl14.AddCell(phrase);

        cell = new PdfPCell(new Phrase(litAddr1.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("oskh " + DateTime.Today.ToString("yyyy$MM$dd"), sinhalaFont));

        cell = new PdfPCell(new Phrase(litAddr2.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr3.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr4.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("ms%h uy;auhdfKks$uy;aushks", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        PdfPCell cell12321 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh35 = new Chunk("PSjs; rCIK Tmamq wxlh ", sinhalaFont);
            ph.Add(chh35);
            Chunk chh355 = new Chunk(litPolNumber.Text, bodyFont);
            ph.Add(chh355);
            cell12321 = new PdfPCell(ph);
            cell12321.HorizontalAlignment = 0;
            cell12321.Border = 0;
            cell12321.Colspan = 3;
            tbl14.AddCell(cell12321);
        }

        PdfPCell cell12325 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh315 = new Chunk("PSjs; rCIs;  ", sinhalaFont);
            ph.Add(chh315);
            Chunk chh3551 = new Chunk(this.HiddenField26.Value + litName.Text, bodyFont);
            ph.Add(chh3551);
            cell12325 = new PdfPCell(ph);
            cell12325.HorizontalAlignment = 0;
            cell12325.Border = 0;
            cell12325.Colspan = 3;
            tbl14.AddCell(cell12325);
        }

        PdfPCell cell1232518 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            cell1232518 = new PdfPCell(ph);
            cell1232518.HorizontalAlignment = 0;
            cell1232518.Border = 0;
            cell1232518.Colspan = 3;
            tbl14.AddCell(cell1232518);
        }

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        PdfPCell cell123 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("by; ioZyka Tmamqj " + this.HiddenField1.Value + "$" + this.HiddenField2.Value + "$" + this.HiddenField27.Value + " osk mQrAKFjhg m;ajSug kshus; nj i;=gska okajd isgsuq' fuu Tmamqj", sinhalaFont);
            ph.Add(chh3);
            cell123 = new PdfPCell(ph);
            cell123.HorizontalAlignment = 0;
            cell123.Border = 0;
            cell123.Colspan = 3;
            tbl14.AddCell(cell123);
        }

        PdfPCell cell124 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("hgf;a f.jSug kshus; uqo,a ms<snoZj jsia;rhla fuz iu. tjd we;s Tmamqj f.jd ksulsrSfuz ksoyia", sinhalaFont);
            ph.Add(chh3);
            cell124 = new PdfPCell(ph);
            cell124.HorizontalAlignment = 0;
            cell124.Border = 0;
            cell124.Colspan = 3;
            tbl14.AddCell(cell124);
        }

        PdfPCell cell125 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("lsrSfuz l=js;dkaisfhys ioZyka fjz'", sinhalaFont);
            ph.Add(chh3);
            cell125 = new PdfPCell(ph);
            cell125.HorizontalAlignment = 0;
            cell125.Border = 0;
            cell125.Colspan = 3;
            tbl14.AddCell(cell125);
        }

        PdfPCell cell126 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("fuu uqo, Tn fj; f.jSug yelsjkq msKsi my; olajd we;s wjYH;djhka muKla fkdmudj imqrd,kq uekjs'", sinhalaFont);
            ph.Add(chh3);
            cell126 = new PdfPCell(ph);
            cell126.HorizontalAlignment = 0;
            cell126.Border = 0;
            cell126.Colspan = 3;
            tbl14.AddCell(cell126);
        }

        PdfPCell cell127 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("wjYH;djhka", sinhalaFont);
            ph.Add(chh3);
            cell127 = new PdfPCell(ph);
            cell127.HorizontalAlignment = 0;
            cell127.Border = 0;
            cell127.Colspan = 3;
            tbl14.AddCell(cell127);
        }

        PdfPCell cell128 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" 1' fuz iu.Z tjd we;s Tmamqj f.jd ksu lsrSu ksoyia lsrSfuz l=js;dkaish ksis f,i iuzmQrAK lr", sinhalaFont);
            ph.Add(chh3);
            cell128 = new PdfPCell(ph);
            cell128.HorizontalAlignment = 0;
            cell128.Border = 0;
            cell128.Colspan = 3;
            tbl14.AddCell(cell128);
        }

        PdfPCell cell129 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("    tjkak' ^w;aika lr osk oud idlaIs fhdod& taldnoaO rCIKhla kuz fofokdf.au w;aik ;ensh hq;=h'", sinhalaFont);
            ph.Add(chh3);
            cell129 = new PdfPCell(ph);
            cell129.HorizontalAlignment = 0;
            cell129.Border = 0;
            cell129.Colspan = 3;
            tbl14.AddCell(cell129);
        }

        PdfPCell cell130 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" 2' fuz iu.Z we;s jeo.;a ksfjzokh lshjd tys ioZyka ,sms wxl 1 iuzmQrAK lr tjSu'", sinhalaFont);
            ph.Add(chh3);
            cell130 = new PdfPCell(ph);
            cell130.HorizontalAlignment = 0;
            cell130.Border = 0;
            cell130.Colspan = 3;
            tbl14.AddCell(cell130);
        }

        PdfPCell cell131 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" 3' cd;sl yeªkquzmf;a iy;sl lrk ,o cdhd msgm;la wod, wjYH;d iu. tjSug lghq;+ lrkak'", sinhalaFont);
            ph.Add(chh3);
            cell131 = new PdfPCell(ph);
            cell131.HorizontalAlignment = 0;
            cell131.Border = 0;
            cell131.Colspan = 3;
            tbl14.AddCell(cell131);
        }

        PdfPCell cell132 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" 4' Tnf.a nexl= fmdf;ys .sKquz wxlh yd ku i|yka msgqfjz iy;sl l, Pdhd msgm;la", sinhalaFont);
            ph.Add(chh3);
            cell132 = new PdfPCell(ph);
            cell132.HorizontalAlignment = 0;
            cell132.Border = 0;
            cell132.Colspan = 3;
            tbl14.AddCell(cell132);
        }
        LifeCustomer customer1 = new LifeCustomer();
        customer1.getloan_det(this.litPolNumber.Text);
        string view11 = customer1.viewStatus1;

        if (view11 == "N")
        {
            PdfPCell cell13288 = new PdfPCell();
            {
                Phrase ph = new Phrase();
                Chunk chh3 = new Chunk(" 5' uq,a rlAIK Tmamqj", sinhalaFont);
                ph.Add(chh3);
                cell13288 = new PdfPCell(ph);
                cell13288.HorizontalAlignment = 0;
                cell13288.Border = 0;
                cell13288.Colspan = 3;
                tbl14.AddCell(cell13288);
            }
        }

        else { }


        PdfPCell cell13289 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell13289 = new PdfPCell(ph);
            cell13289.HorizontalAlignment = 0;
            cell13289.Border = 0;
            cell13289.Colspan = 3;
            tbl14.AddCell(cell13289);
        }

        PdfPCell cell134 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("iehq  fuu rCIKfha jdrsl kshus; fjz,djg f.jSu ioyd Tnf.a nexl=lrejka fj; ia:djr", sinhalaFont);
            ph.Add(chh3);
            cell134 = new PdfPCell(ph);
            cell134.HorizontalAlignment = 0;
            cell134.Border = 0;
            cell134.Colspan = 3;
            tbl14.AddCell(cell134);
        }
        PdfPCell cell135 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("ksfhda.hla ,nd oS we;akuz rCIK Tmamqfjz ioyka wjidk jdrslh f.jsh hq;= osfkka miqj jdrsl", sinhalaFont);
            ph.Add(chh3);
            cell135 = new PdfPCell(ph);
            cell135.HorizontalAlignment = 0;
            cell135.Border = 0;
            cell135.Colspan = 3;
            tbl14.AddCell(cell135);
        }

        PdfPCell cell136 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("f.jSu  kj;ajk f,ig Tjqkag Wmfoia oSug lgqh;= lrk f,i b,a,uq'", sinhalaFont);
            ph.Add(chh3);
            cell136 = new PdfPCell(ph);
            cell136.HorizontalAlignment = 0;
            cell136.Border = 0;
            cell136.Colspan = 3;
            tbl14.AddCell(cell136);
        }
        PdfPCell cell137 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell137 = new PdfPCell(ph);
            cell137.HorizontalAlignment = 0;
            cell137.Border = 0;
            cell137.Colspan = 3;
            tbl14.AddCell(cell137);
        }


        PdfPCell cell138 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("wmf.a jgskd mdrsfNda.slfhl= f,i cSjs; rCIK Tmamqjla ,nd .ksuska wm wdh;kh flfrys", sinhalaFont);
            ph.Add(chh3);
            cell138 = new PdfPCell(ph);
            cell138.HorizontalAlignment = 0;
            cell138.Border = 0;
            cell138.Colspan = 3;
            tbl14.AddCell(cell138);
        }

        PdfPCell cell139 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Tn ;enq jsYajdih ms<snoj Tng ia;+;s jka; fjuq'", sinhalaFont);
            ph.Add(chh3);
            cell139 = new PdfPCell(ph);
            cell139.HorizontalAlignment = 0;
            cell139.Border = 0;
            cell139.Colspan = 3;
            tbl14.AddCell(cell139);
        }

        PdfPCell cell140 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell140 = new PdfPCell(ph);
            cell140.HorizontalAlignment = 0;
            cell140.Border = 0;
            cell140.Colspan = 3;
            tbl14.AddCell(cell140);
        }

        PdfPCell cell141 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("ia;=;shs", sinhalaFont);
            ph.Add(chh3);
            cell141 = new PdfPCell(ph);
            cell141.HorizontalAlignment = 0;
            cell141.Border = 0;
            cell141.Colspan = 3;
            tbl14.AddCell(cell141);
        }

        PdfPCell cell142 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("fuhg  jsYajdis jQ", sinhalaFont);
            ph.Add(chh3);
            cell142 = new PdfPCell(ph);
            cell142.HorizontalAlignment = 0;
            cell142.Border = 0;
            cell142.Colspan = 3;
            tbl14.AddCell(cell142);
        }

        PdfPCell cell143 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell143 = new PdfPCell(ph);
            cell143.HorizontalAlignment = 0;
            cell143.Border = 0;
            cell143.Colspan = 3;
            tbl14.AddCell(cell143);
        }

        PdfPCell cell144 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell144 = new PdfPCell(ph);
            cell144.HorizontalAlignment = 0;
            cell144.Border = 0;
            cell144.Colspan = 3;
            tbl14.AddCell(cell144);
        }
        PdfPCell cell145 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("cSjs; l<uKdlre ^fjkqjg&", sinhalaFont);
            ph.Add(chh3);
            cell145 = new PdfPCell(ph);
            cell145.HorizontalAlignment = 0;
            cell145.Border = 0;
            cell145.Colspan = 3;
            tbl14.AddCell(cell145);
        }

        PdfPCell cell146 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Y%S ,xld rÌK ixia:dj", sinhalaFont);
            ph.Add(chh3);
            cell146 = new PdfPCell(ph);
            cell146.HorizontalAlignment = 0;
            cell146.Border = 0;
            cell146.Colspan = 3;
            tbl14.AddCell(cell146);
        }


        PdfPCell cell147 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("fkd21 fjdlafIda,a jSoSh fld<U 02 oqrl:k wxl ( 0112357276 0112357278 0112357200", sinhalaFont);
            ph.Add(chh3);
            cell147 = new PdfPCell(ph);
            cell147.HorizontalAlignment = 0;
            cell147.Border = 0;
            cell147.Colspan = 3;
            tbl14.AddCell(cell147);
        }

        PdfPCell cell148 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", sinhalaFont);
            ph.Add(chh3);
            cell148 = new PdfPCell(ph);
            cell148.HorizontalAlignment = 0;
            cell148.Border = 0;
            cell148.Colspan = 3;
            tbl14.AddCell(cell148);
        }

        PdfPCell cell149 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("oskh ", sinhalaFont);
            Chunk c1 = new Chunk(DateTime.Today.ToString("yyyy / MM / dd"), bodyFont);
            ph.Add(chh3);
            ph.Add(c1);
            cell149 = new PdfPCell(ph);
            cell149.HorizontalAlignment = 0;
            cell149.Border = 0;
            cell149.Colspan = 3;
            tbl14.AddCell(cell149);
        }

        document.Add(tbl14);
        //writer.CloseStream = false;
        document.NewPage();


        document.Add(new Paragraph("\n\n"));
        int[] clmwidths1115 = { 90, 30 };
        PdfPTable tbl141 = new PdfPTable(2);
        tbl141.SetWidths(clmwidths111);

        tbl141.WidthPercentage = 100;
        tbl141.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl141.SpacingBefore = 10;
        tbl141.SpacingAfter = 0;
        tbl141.DefaultCell.Border = 0;

        PdfPCell cell133 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("wfma fhduqj PS$ysusluz$", sinhalaFont);
            ph.Add(chh3);
            Chunk chh38 = new Chunk("1" + this.HiddenField3.Value, bodyFont);
            ph.Add(chh38);
            cell133 = new PdfPCell(ph);
            cell133.HorizontalAlignment = 1;
            cell133.Border = 0;
            cell133.Colspan = 3;
            tbl141.AddCell(cell133);
        }

        PdfPCell cell1231 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Y%S ,xld rCIK ixia:dj", sinhalaFont);
            ph.Add(chh3);
            cell1231 = new PdfPCell(ph);
            cell1231.HorizontalAlignment = 1;
            cell1231.Border = 0;
            cell1231.Colspan = 3;
            tbl141.AddCell(cell1231);
        }

        PdfPCell cell150 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("1961 wxl 2 orK ixia:d mK; hgf;a ixia:dms;hs", sinhalaFont);
            ph.Add(chh3);
            cell150 = new PdfPCell(ph);
            cell150.HorizontalAlignment = 1;
            cell150.Border = 0;
            cell150.Colspan = 3;
            tbl141.AddCell(cell150);
        }

        PdfPCell cell151 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell151 = new PdfPCell(ph);
            cell151.HorizontalAlignment = 1;
            cell151.Border = 0;
            cell151.Colspan = 3;
            tbl141.AddCell(cell151);
        }

        string tobePaidText = this.get_netAmtPayInWords(Convert.ToDouble(this.HiddenField22.Value));

        PdfPCell cell152 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh31 = new Chunk(this.HiddenField26.Value + litName.Text, bodyFont);
            Chunk chh3 = new Chunk(" f.a cSjs;h$ cSjs;$ fjkqfjka jQ wxl " + litPolNumber.Text + " orK rlaIs; uqo, re'" + litSumIns.Text, sinhalaFont);
            Chunk chh4 = new Chunk("jk l,a msreKq Tmamqj f.jd ksu lsrSu'", sinhalaFont);
            Chunk chh41 = new Chunk("                                                                                       ", sinhalaFont);
            Chunk chh5 = new Chunk("cSjs; rCIs;hd$ rCIs;hka $mejreuz,dNShd jk  ", sinhalaFont);
            Chunk chh51 = new Chunk(this.HiddenField26.Value + litName.Text, bodyFont);
            Chunk chh511 = new Chunk("  keu;s uu$wms mQrAfjdla; cSjs;h $", sinhalaFont);
            Chunk chh6 = new Chunk("cSjs; fjkqfjka jQo " + this.HiddenField1.Value + "$" + this.HiddenField2.Value + "$" + this.HiddenField27.Value + " osk l,amsreKq njg m;a jQo Tmamqj hgf;a we;s uf.a$wfma ishMZ ysuslu iuzmQrAKfhk ", sinhalaFont);
            Chunk chh7 = new Chunk("fnzreuz lrjd .ekSula jYfhka iy ksoyia lrjd .ekSula jYfhka by; ioZyka ixia:dfjka my; jsia;r jk mrsos ", sinhalaFont);
            Chunk chh8 = new Chunk(tobePaidText + " " + "(" + this.HiddenField22.Value + ")", boldTableFont);
            Chunk chh81 = new Chunk("jq uqo,la ", sinhalaFont);
            Chunk chh9 = new Chunk("Ndr.ekSug tl. njo tu Tmamqj wj,x.= lsrSu msKsi mQrAfjdala; ixia:djg  ndrfok nj ", sinhalaFont);
            Chunk chh10 = new Chunk("fuhska m%ldY lrus$lruq' fuys m%;s,dN uqo, .Kkhlr we;af;a u jsiska f.jsh= hq; ish,q jdrsl", sinhalaFont);
            Chunk chh11 = new Chunk(" f.jd we;s nj mokuz lrf.knj oksu's ;jo Tmamqfjys uq,q ld, mrspzfPaoh ;=, lsishuz wjia:djl jdrslhla fyda", sinhalaFont);
            Chunk chh12 = new Chunk("jdrsl f.jd fkdue;s kuz tfiaf.jd fkdue; s jdrslh fyda jdrsl fmd,sh; a iu. wjidk ", sinhalaFont);
            Chunk chh13 = new Chunk("ysuslu uqo,ska wvqlrk njo uu oksu", sinhalaFont);
            Chunk chh14 = new Chunk("", sinhalaFont);
            ph.Add(chh31);
            ph.Add(chh3);
            ph.Add(chh4);
            ph.Add(chh41);
            ph.Add(chh5);
            ph.Add(chh51);
            ph.Add(chh511);
            ph.Add(chh6);
            ph.Add(chh7);
            ph.Add(chh8);
            ph.Add(chh81);
            ph.Add(chh9);
            ph.Add(chh10);
            ph.Add(chh11);
            ph.Add(chh12);
            ph.Add(chh13);
            ph.Add(chh14);

            cell152 = new PdfPCell(ph);
            cell152.HorizontalAlignment = 0;
            cell152.Border = 0;
            cell152.Colspan = 3;
            tbl141.AddCell(cell152);
        }

        document.Add(tbl141);

        int[] clmwidths1112 = { 50, 25, 25 };
        PdfPTable tbl142 = new PdfPTable(3);
        tbl142.SetWidths(clmwidths1112);

        tbl142.WidthPercentage = 100;
        tbl142.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl142.SpacingBefore = 12;
        tbl142.SpacingAfter = 0;
        tbl142.DefaultCell.Border = 0;

        cell = new PdfPCell(new Phrase("rCIs; uqo, $f.jd ksus w.h", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", sinhalaFont));
        tbl142.AddCell(new Phrase("re'" + this.HiddenField8.Value, sinhalaFont));


        PdfPCell cell123215 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh35 = new Chunk(this.litComDate.Text, bodyFont);
            ph.Add(chh35);
            Chunk chh355 = new Chunk(" isg " + this.HiddenField1.Value + this.HiddenField2.Value + "olajd ld,h fjkqfjka ysus m%ido uqo, ", sinhalaFont);
            ph.Add(chh355);
            cell123215 = new PdfPCell(ph);
            cell123215.HorizontalAlignment = 0;
            cell123215.Border = 0;
            cell123215.Colspan = 3;
            tbl14.AddCell(cell123215);
        }

        cell = new PdfPCell(new Phrase("w;=re m%ido uqo,", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", sinhalaFont));
        tbl142.AddCell(new Phrase("re'" + this.HiddenField10.Value, sinhalaFont));

        cell = new PdfPCell(new Phrase("", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", sinhalaFont));
        tbl142.AddCell(new Phrase("re'" + this.HiddenField22.Value, sinhalaFont));

        cell = new PdfPCell(new Phrase("wvq l,d", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("ys.Z jdr uqo,a", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("re'" + this.HiddenField13.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("ta ioZyd fmd,sh", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("re'" + this.HiddenField14.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("Kh uqo,a", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("re'" + this.HiddenField11.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("wxl", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(this.HiddenField18.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("oskh ", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(this.HiddenField19.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("Kh uqo, ioZyd fmd,sh", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("re'" + this.HiddenField12.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("fjk;a wvq lsrSu", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("re'" + this.HiddenField23.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("uqoaor .dia;", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("re'" + this.HiddenField17.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("wvq lsrSu tl;=j", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("re'" + this.HiddenField24.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("tl;= Y=oaO uqo,", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", sinhalaFont));
        tbl142.AddCell(new Phrase("re'" + this.HiddenField25.Value, sinhalaFont));


        PdfPCell cell20029 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell20029 = new PdfPCell(ph);
            cell20029.HorizontalAlignment = 0;
            cell20029.Border = 0;
            cell20029.Colspan = 3;
            tbl142.AddCell(cell20029);
        }


        PdfPCell cell2002 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("20 '''''''''''''''''''''' jq'''''''''''''''''''''''''''''''''''''''''ui''''''''''''''' jeks osk ''''''''''''''''''''''''''''''''''''oSh'", sinhalaFont);
            ph.Add(chh3);
            cell2002 = new PdfPCell(ph);
            cell2002.HorizontalAlignment = 0;
            cell2002.Border = 0;
            cell2002.Colspan = 3;
            tbl142.AddCell(cell2002);
        }

        PdfPCell cell2003 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("my; ioZyka mdrAYjh $ mdrAYj jsiska ud bosrsfha ish w;aik$ w; aika udmge.s,", sinhalaFont);
            ph.Add(chh3);
            cell2003 = new PdfPCell(ph);
            cell2003.HorizontalAlignment = 0;
            cell2003.Border = 0;
            cell2003.Colspan = 3;
            tbl142.AddCell(cell2003);
        }

        PdfPCell cell2004 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("i,l=K$i,l=Kq ;nk ,o nj;a fuys wvx.= lreKq ujsiska tu mdrAYjhg $", sinhalaFont);
            ph.Add(chh3);
            cell2004 = new PdfPCell(ph);
            cell2004.HorizontalAlignment = 0;
            cell2004.Border = 0;
            cell2004.Colspan = 3;
            tbl142.AddCell(cell2004);
        }

        PdfPCell cell2005 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("$mdrAYjhkag jsjsO jQ mrsos mrsjrA;kh fldg f;dard fok ,o nj;a tu", sinhalaFont);
            ph.Add(chh3);
            cell2005 = new PdfPCell(ph);
            cell2005.HorizontalAlignment = 0;
            cell2005.Border = 0;
            cell2005.Colspan = 3;
            tbl142.AddCell(cell2005);
        }

        PdfPCell cell2006 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("mdrAYjh$mdrAYj ujsiska yoZqkd.kakd, o nj; a iy; sl lrus'", sinhalaFont);
            ph.Add(chh3);
            cell2006 = new PdfPCell(ph);
            cell2006.HorizontalAlignment = 0;
            cell2006.Border = 0;
            cell2006.Colspan = 3;
            tbl142.AddCell(cell2006);
        }

        PdfPCell cell2007 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("                                                                                    ............................................................ ", bodyFont2);
            ph.Add(chh3);
            cell2007 = new PdfPCell(ph);
            cell2007.HorizontalAlignment = 0;
            cell2007.Border = 0;
            cell2007.Colspan = 3;
            tbl142.AddCell(cell2007);
        }

        PdfPCell cell2008 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("                                                                                  " + "rCIs;hdf.a$rCIs;hkaf.a$mejreuz,dNshdf. mqreoq w;aik", sinhalaFont);
            ph.Add(chh3);
            cell2008 = new PdfPCell(ph);
            cell2008.HorizontalAlignment = 0;
            cell2008.Border = 0;
            cell2008.Colspan = 3;
            tbl142.AddCell(cell2008);
        }

        PdfPCell cell2009 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2009 = new PdfPCell(ph);
            cell2009.HorizontalAlignment = 0;
            cell2009.Border = 0;
            cell2009.Colspan = 3;
            tbl142.AddCell(cell2009);
        }

        cell = new PdfPCell(new Phrase("idCIslref.a fyda iy;sl lrk ks,Odrshdf.a w;aik", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":......................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("idCIslref.a ku", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":......................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("mojs kduh fyda /lshdj", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":......................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(",smskh", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":......................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":......................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":......................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        PdfPCell cell2016 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("ie hq  rCIs;hdf.a$rCIs;hkaf.a mejreuz,dNshdf.a fuu w;aik isxy, okakd = mqoa.,fhl= jsiska iy;sl l, hq;=h", sinhalaFont);
            ph.Add(chh3);
            cell2016 = new PdfPCell(ph);
            cell2016.HorizontalAlignment = 0;
            cell2016.Border = 0;
            cell2016.Colspan = 3;
            tbl142.AddCell(cell2016);
        }
        PdfPCell cell2017 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("rCIs;hd $rCIs;hka mejreuz,dNshd wl=re fkdokfka kuz Tyqf.a fydawehf.a udmge.s,s i<l=K$i<l=Kq ", sinhalaFont);
            ph.Add(chh3);
            cell2017 = new PdfPCell(ph);
            cell2017.HorizontalAlignment = 0;
            cell2017.Border = 0;
            cell2017.Colspan = 3;
            tbl142.AddCell(cell2017);
        }
        PdfPCell cell2018 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("iudodk jsksYaphldrhl= fydakS;S{flfkl= fyda osjqreuz flduidrsia flfkl= jsiska iy;sl l< hq;=h", sinhalaFont);
            ph.Add(chh3);
            cell2018 = new PdfPCell(ph);
            cell2018.HorizontalAlignment = 0;
            cell2018.Border = 0;
            cell2018.Colspan = 3;
            tbl142.AddCell(cell2018);
        }

        PdfPCell cell2019 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("uqoaor .dia;= by; ysuslus uqo,ska wvqlr we;", sinhalaFont);
            ph.Add(chh3);
            cell2019 = new PdfPCell(ph);
            cell2019.HorizontalAlignment = 0;
            cell2019.Border = 0;
            cell2019.Colspan = 3;
            tbl142.AddCell(cell2019);
        }

        PdfPCell cell2020 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell2020 = new PdfPCell(ph);
            cell2020.HorizontalAlignment = 0;
            cell2020.Border = 0;
            cell2020.Colspan = 3;
            tbl142.AddCell(cell2020);
        }

        document.Add(tbl142);

        document.NewPage();
        document.Add(new Paragraph("\n\n"));

        int[] clmwidths1122 = { 100 };
        PdfPTable tbl143 = new PdfPTable(1);
        tbl143.WidthPercentage = 100;
        tbl143.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl143.SpacingBefore = 12;
        tbl143.SpacingAfter = 0;
        tbl143.DefaultCell.Border = 0;

        PdfPCell cell2080 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("jeo.;a ksfjzokhh", sinhalaFont);
            ph.Add(chh3);
            cell2080 = new PdfPCell(ph);
            cell2080.HorizontalAlignment = 1;
            cell2080.Border = 0;
            cell2080.Colspan = 3;
            tbl143.AddCell(cell2080);
        }

        PdfPCell cell2081 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2081 = new PdfPCell(ph);
            cell2081.HorizontalAlignment = 0;
            cell2081.Border = 0;
            cell2081.Colspan = 3;
            tbl143.AddCell(cell2081);
        }

        PdfPCell cell2082 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("mQrAKFj ysuslug wod, fplam; Tn fj; tjSu $ nexl=jg ner lsrSu i|yd f.jd ksulsrSfuz", sinhalaFont);
            ph.Add(chh3);
            cell2082 = new PdfPCell(ph);
            cell2082.HorizontalAlignment = 0;
            cell2082.Border = 0;
            cell2082.Colspan = 3;
            tbl143.AddCell(cell2082);
        }

        PdfPCell cell2083 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("l=js;dkaish iuzmqrAK lr bosrsm;a l< hq;=h' fjk;a mdrAYjhlg uqo,a m;a jSu je<elajSu i|yd", sinhalaFont);
            ph.Add(chh3);
            cell2083 = new PdfPCell(ph);
            cell2083.HorizontalAlignment = 0;
            cell2083.Border = 0;
            cell2083.Colspan = 3;
            tbl143.AddCell(cell2083);
        }

        PdfPCell cell2084 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Tnf.a nexl= .sKqug uqo,a ner lsrSug my; i|yka wxl 01 ,smsfhys Tnf.a kuska we;s cx.u", sinhalaFont);
            ph.Add(chh3);
            cell2084 = new PdfPCell(ph);
            cell2084.HorizontalAlignment = 0;
            cell2084.Border = 0;
            cell2084.Colspan = 3;
            tbl143.AddCell(cell2084);
        }

        PdfPCell cell2085 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("fyda b;srslsrSfuz .sKqul jsia;r i|yka lr wm fj; okajkak' wvq jYfhka b;srs lsrSfuz .sKqula", sinhalaFont);
            ph.Add(chh3);
            cell2085 = new PdfPCell(ph);
            cell2085.HorizontalAlignment = 0;
            cell2085.Border = 0;
            cell2085.Colspan = 3;
            tbl143.AddCell(cell2085);
        }

        PdfPCell cell1189 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chhh5 = new Chunk("fyda fkdue;s kuz b;srs lsrSfuz .sKqula jsjD; lr  wod, jsia;r wm fj; oekajSu Tnf.a", sinhalaFont);
            ph.Add(chhh5);
            cell1189 = new PdfPCell(ph);
            cell1189.HorizontalAlignment = 0;
            cell1189.Border = 0;
            cell1189.Colspan = 3;
            tbl143.AddCell(cell1189);
        }

        PdfPCell cell11891 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chhh5 = new Chunk("uqo, iqrCIs; j ,nd.ekSug bjy,a jkjd we;'", sinhalaFont);
            ph.Add(chhh5);
            cell11891 = new PdfPCell(ph);
            cell11891.HorizontalAlignment = 0;
            cell11891.Border = 0;
            cell11891.Colspan = 3;
            tbl143.AddCell(cell11891);
        }

        int[] clmwidths1124 = { 60, 40 };
        PdfPTable tbl1413 = new PdfPTable(2);
        tbl1413.SetWidths(clmwidths1124);

        tbl1413.WidthPercentage = 100;
        tbl1413.SpacingBefore = 0;
        tbl1413.SpacingAfter = 0;
        tbl1413.DefaultCell.Border = 0;

        PdfPCell cell2088 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2088 = new PdfPCell(ph);
            cell2088.HorizontalAlignment = 0;
            cell2088.Border = 1;
            cell2088.Colspan = 3;
            tbl1413.AddCell(cell2088);
        }
        cell = new PdfPCell(new Phrase(" ", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("", sinhalaFont));
        cell.BorderWidthTop = 1;
        cell.BorderWidthLeft = 1;

        Phrase phrase1 = new Phrase();
        phrase1.Add(
            new Chunk("", new Font(bf, 11, Font.NORMAL))
        );
        phrase1.Add(new Chunk("", new Font()));
        tbl1413.AddCell(phrase1);

        Phrase phrase3 = new Phrase();
        phrase3.Add(
            new Chunk("wmf.a fhduqj", new Font(bf, 11, Font.NORMAL))
        );
        phrase3.Add(new Chunk("1" + this.HiddenField3.Value, new Font()));
        tbl1413.AddCell(phrase3);

        cell = new PdfPCell(new Phrase("wxl 1 ,smsh  ", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("ysusluz uqo,a Tnf.a .sKqug iDcqj ner ", sinhalaFont));
        cell.BorderWidthTop = 1;
        cell.BorderWidthLeft = 1;

        cell = new PdfPCell(new Phrase("Y%S ,xld bkaIqjrkaia fldamfraIka ,sñgÙ", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("l< yelafla kuz th nerl< miq flg", sinhalaFont));

        cell = new PdfPCell(new Phrase("", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("mksjsvhla  uÛska Tn fj; oekajSu", sinhalaFont));

        cell = new PdfPCell(new Phrase("", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("i|yd Tnf.a cx.u oqrl:k wxlhla", sinhalaFont));

        cell = new PdfPCell(new Phrase("", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("i|yka lrkak", sinhalaFont));

        cell = new PdfPCell(new Phrase("rCIK Tmamq wxlh " + litPolNumber.Text, sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("", bodyFont2));


        PdfPCell cell123251 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh315 = new Chunk("ku ", sinhalaFont);
            ph.Add(chh315);
            Chunk chh3551 = new Chunk(litName.Text, bodyFont);
            ph.Add(chh3551);
            cell123251 = new PdfPCell(ph);
            cell123251.HorizontalAlignment = 0;
            cell123251.Border = 0;
            cell123251.Colspan = 3;
            tbl1413.AddCell(cell123251);
        }

        PdfPCell cell20914 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("                                                                        ", bodyFont2);
            ph.Add(chh3);
            cell20914 = new PdfPCell(ph);
            cell20914.HorizontalAlignment = 0;
            cell20914.Border = 1;
            cell20914.Colspan = 3;
            tbl1413.AddCell(cell20914);
        }

        PdfPCell cell2096 = new PdfPCell(signature);
        {
            cell2096.Colspan = 2;
            cell2096.HorizontalAlignment = 0;
            cell2096.Border = 0;
            cell2096.BorderWidthBottom = 0;
            cell2096.BorderWidthTop = 0;
            cell2096.BorderWidthLeft = 0;
            cell2096.BorderWidthRight = 0;
            tbl1413.AddCell(cell2096);
        }


        document.Add(tbl143);
        document.Add(tbl1413);


        writer.CloseStream = false;
        document.NewPage();
        document.Close();
        Response.Buffer = false;
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();

        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Covering_Sinhala.pdf"));
        Response.BinaryWrite(output.ToArray());

    }

    private void print4()
    {
         Document document = new Document(PageSize.A4, 50, 50, 25, 25);
        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        document.Open();
        Font titleFont1 = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font boldTableFont = FontFactory.GetFont("Times New Roman", 11, Font.BOLD);
        Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 11, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont4_bold = FontFactory.GetFont("Times New Roman", 7, Font.BOLD);
        Font bodyFont5 = FontFactory.GetFont("Times New Roman", 7, Font.NORMAL);
        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);
        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font bodyFont2_bold_und = FontFactory.GetFont("Times New Roman", 10, Font.BOLD | Font.UNDERLINE);

        string fontpath = System.Web.HttpContext.Current.Server.MapPath("~/Font/sa_____.TTF");
        var bf = BaseFont.CreateFont(fontpath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        var sinhalaFont = new Font(bf, 11, Font.NORMAL);

        iTextSharp.text.Image signature = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Capture.jpg"));
        signature.ScalePercent(55f);
        signature.SetAbsolutePosition(70, 75);

        document.Add(new Paragraph("\n\n"));
        int[] clmwidths111 = { 60, 40 };
        PdfPTable tbl14 = new PdfPTable(2);
        tbl14.SetWidths(clmwidths111);

        tbl14.WidthPercentage = 100;
        tbl14.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl14.SpacingBefore = 10;
        tbl14.SpacingAfter = 0;
        tbl14.DefaultCell.Border = 0;

        PdfPCell cell = new PdfPCell(new Phrase(this.HiddenField26.Value + litName.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        //  tbl14.AddCell(new Phrase("wfma fhduqj PS$ysusluz$ "+ this.HiddenField3.Value, sinhalaFont));

        Phrase phrase = new Phrase();
        phrase.Add(
            new Chunk("wfma fhduqj PS$ysusluz$", new Font(bf, 11, Font.NORMAL))
        );
        phrase.Add(new Chunk("1" + this.HiddenField3.Value, new Font()));
        tbl14.AddCell(phrase);

        cell = new PdfPCell(new Phrase(litAddr1.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("oskh " + DateTime.Today.ToString("yyyy$MM$dd"), sinhalaFont));

        cell = new PdfPCell(new Phrase(litAddr2.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr3.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr4.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("ms%h uy;auhdfKks$uy;aushks", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        PdfPCell cell12321 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh35 = new Chunk("PSjs; rCIK Tmamq wxlh ", sinhalaFont);
            ph.Add(chh35);
            Chunk chh355 = new Chunk(litPolNumber.Text, bodyFont);
            ph.Add(chh355);
            cell12321 = new PdfPCell(ph);
            cell12321.HorizontalAlignment = 0;
            cell12321.Border = 0;
            cell12321.Colspan = 3;
            tbl14.AddCell(cell12321);
        }

        PdfPCell cell12325 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh315 = new Chunk("PSjs; rCIs;  ", sinhalaFont);
            ph.Add(chh315);
            Chunk chh3551 = new Chunk(this.HiddenField26.Value + litName.Text, bodyFont);
            ph.Add(chh3551);
            cell12325 = new PdfPCell(ph);
            cell12325.HorizontalAlignment = 0;
            cell12325.Border = 0;
            cell12325.Colspan = 3;
            tbl14.AddCell(cell12325);
        }

        PdfPCell cell1232518 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            cell1232518 = new PdfPCell(ph);
            cell1232518.HorizontalAlignment = 0;
            cell1232518.Border = 0;
            cell1232518.Colspan = 3;
            tbl14.AddCell(cell1232518);
        }

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        PdfPCell cell123 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("by; ioZyka Tmamqj " + this.HiddenField1.Value + "$" + this.HiddenField2.Value + "$" + this.HiddenField27.Value + " osk mQrAKFjhg m;ajSug kshus; nj i;=gska okajd isgsuq' fuu Tmamqj", sinhalaFont);
            ph.Add(chh3);
            cell123 = new PdfPCell(ph);
            cell123.HorizontalAlignment = 0;
            cell123.Border = 0;
            cell123.Colspan = 3;
            tbl14.AddCell(cell123);
        }

        PdfPCell cell124 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("hgf;a f.jSug kshus; uqo,a ms<snoZj jsia;rhla fuz iu. tjd we;s Tmamqj f.jd ksulsrSfuz ksoyia", sinhalaFont);
            ph.Add(chh3);
            cell124 = new PdfPCell(ph);
            cell124.HorizontalAlignment = 0;
            cell124.Border = 0;
            cell124.Colspan = 3;
            tbl14.AddCell(cell124);
        }

        PdfPCell cell125 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("lsrSfuz l=js;dkaisfhys ioZyka fjz'", sinhalaFont);
            ph.Add(chh3);
            cell125 = new PdfPCell(ph);
            cell125.HorizontalAlignment = 0;
            cell125.Border = 0;
            cell125.Colspan = 3;
            tbl14.AddCell(cell125);
        }

        PdfPCell cell126 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("fuu uqo, Tn fj; f.jSug yelsjkq msKsi my; olajd we;s wjYH;djhka muKla fkdmudj imqrd,kq uekjs'", sinhalaFont);
            ph.Add(chh3);
            cell126 = new PdfPCell(ph);
            cell126.HorizontalAlignment = 0;
            cell126.Border = 0;
            cell126.Colspan = 3;
            tbl14.AddCell(cell126);
        }

        PdfPCell cell127 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("wjYH;djhka", sinhalaFont);
            ph.Add(chh3);
            cell127 = new PdfPCell(ph);
            cell127.HorizontalAlignment = 0;
            cell127.Border = 0;
            cell127.Colspan = 3;
            tbl14.AddCell(cell127);
        }

        PdfPCell cell128 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("1'fuz iu.Z tjd we;s Tmamqj f.jd ksu lsrSu ksoyia lsrSfuz l=js;dkaish ksis f,i iuzmQrAK lr", sinhalaFont);
            ph.Add(chh3);
            cell128 = new PdfPCell(ph);
            cell128.HorizontalAlignment = 0;
            cell128.Border = 0;
            cell128.Colspan = 3;
            tbl14.AddCell(cell128);
        }

        PdfPCell cell129 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("  tjkak' ^w;aika lr osk oud idlaIs fhdod& taldnoaO rCIKhla kuz fofokdf.au w;aik ;ensh hq;=h'", sinhalaFont);
            ph.Add(chh3);
            cell129 = new PdfPCell(ph);
            cell129.HorizontalAlignment = 0;
            cell129.Border = 0;
            cell129.Colspan = 3;
            tbl14.AddCell(cell129);
        }

        PdfPCell cell130 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("2'fuz iu.Z we;s jeo.;a ksfjzokh lshjd tys ioZyka ,sms wxl 1 iuzmQrAK lr tjSu'", sinhalaFont);
            ph.Add(chh3);
            cell130 = new PdfPCell(ph);
            cell130.HorizontalAlignment = 0;
            cell130.Border = 0;
            cell130.Colspan = 3;
            tbl14.AddCell(cell130);
        }

        PdfPCell cell131 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("3'cd;sl yeªkquzmf;a iy;sl lrk ,o cdhd msgm;la wod, wjYH;d iu. tjSug lghq;+ lrkak'", sinhalaFont);
            ph.Add(chh3);
            cell131 = new PdfPCell(ph);
            cell131.HorizontalAlignment = 0;
            cell131.Border = 0;
            cell131.Colspan = 3;
            tbl14.AddCell(cell131);
        }

        PdfPCell cell132 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("4'Tnf.a nexl= fmdf;ys .sKquz wxlh yd ku i|yka msgqfjz iy;sl l, Pdhd msgm;la", sinhalaFont);
            ph.Add(chh3);
            cell132 = new PdfPCell(ph);
            cell132.HorizontalAlignment = 0;
            cell132.Border = 0;
            cell132.Colspan = 3;
            tbl14.AddCell(cell132);
        }
        LifeCustomer customer1 = new LifeCustomer();
        customer1.getloan_det(this.litPolNumber.Text);
        string view11 = customer1.viewStatus1;

        if (view11 == "N")
        {
            PdfPCell cell13288 = new PdfPCell();
            {
                Phrase ph = new Phrase();
                Chunk chh3 = new Chunk("5'uq,a rlAIK Tmamqj", sinhalaFont);
                ph.Add(chh3);
                cell13288 = new PdfPCell(ph);
                cell13288.HorizontalAlignment = 0;
                cell13288.Border = 0;
                cell13288.Colspan = 3;
                tbl14.AddCell(cell13288);
            }
        }

        else { }


        PdfPCell cell13289 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell13289 = new PdfPCell(ph);
            cell13289.HorizontalAlignment = 0;
            cell13289.Border = 0;
            cell13289.Colspan = 3;
            tbl14.AddCell(cell13289);
        }

        PdfPCell cell134 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("iehq  fuu rCIKfha jdrsl kshus; fjz,djg f.jSu ioyd Tnf.a nexl=lrejka fj; ia:djr", sinhalaFont);
            ph.Add(chh3);
            cell134 = new PdfPCell(ph);
            cell134.HorizontalAlignment = 0;
            cell134.Border = 0;
            cell134.Colspan = 3;
            tbl14.AddCell(cell134);
        }
        PdfPCell cell135 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("ksfhda.hla ,nd oS we;akuz rCIK Tmamqfjz ioyka wjidk jdrslh f.jsh hq;= osfkka miqj jdrsl", sinhalaFont);
            ph.Add(chh3);
            cell135 = new PdfPCell(ph);
            cell135.HorizontalAlignment = 0;
            cell135.Border = 0;
            cell135.Colspan = 3;
            tbl14.AddCell(cell135);
        }

        PdfPCell cell136 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("f.jSu  kj;ajk f,ig Tjqkag Wmfoia oSug lgqh;= lrk f,i b,a,uq'", sinhalaFont);
            ph.Add(chh3);
            cell136 = new PdfPCell(ph);
            cell136.HorizontalAlignment = 0;
            cell136.Border = 0;
            cell136.Colspan = 3;
            tbl14.AddCell(cell136);
        }
        PdfPCell cell137 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell137 = new PdfPCell(ph);
            cell137.HorizontalAlignment = 0;
            cell137.Border = 0;
            cell137.Colspan = 3;
            tbl14.AddCell(cell137);
        }


        PdfPCell cell138 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("wmf.a jgskd mdrsfNda.slfhl= f,i cSjs; rCIK Tmamqjla ,nd .ksuska wm wdh;kh flfrys", sinhalaFont);
            ph.Add(chh3);
            cell138 = new PdfPCell(ph);
            cell138.HorizontalAlignment = 0;
            cell138.Border = 0;
            cell138.Colspan = 3;
            tbl14.AddCell(cell138);
        }

        PdfPCell cell139 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Tn ;enq jsYajdih ms<snoj Tng ia;+;s jka; fjuq'", sinhalaFont);
            ph.Add(chh3);
            cell139 = new PdfPCell(ph);
            cell139.HorizontalAlignment = 0;
            cell139.Border = 0;
            cell139.Colspan = 3;
            tbl14.AddCell(cell139);
        }

        PdfPCell cell140 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell140 = new PdfPCell(ph);
            cell140.HorizontalAlignment = 0;
            cell140.Border = 0;
            cell140.Colspan = 3;
            tbl14.AddCell(cell140);
        }

        PdfPCell cell141 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("ia;=;shs", sinhalaFont);
            ph.Add(chh3);
            cell141 = new PdfPCell(ph);
            cell141.HorizontalAlignment = 0;
            cell141.Border = 0;
            cell141.Colspan = 3;
            tbl14.AddCell(cell141);
        }

        PdfPCell cell142 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("fuhg  jsYajdis jQ", sinhalaFont);
            ph.Add(chh3);
            cell142 = new PdfPCell(ph);
            cell142.HorizontalAlignment = 0;
            cell142.Border = 0;
            cell142.Colspan = 3;
            tbl14.AddCell(cell142);
        }

        PdfPCell cell143 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell143 = new PdfPCell(ph);
            cell143.HorizontalAlignment = 0;
            cell143.Border = 0;
            cell143.Colspan = 3;
            tbl14.AddCell(cell143);
        }

        PdfPCell cell144 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell144 = new PdfPCell(ph);
            cell144.HorizontalAlignment = 0;
            cell144.Border = 0;
            cell144.Colspan = 3;
            tbl14.AddCell(cell144);
        }
        PdfPCell cell145 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("cSjs; l<uKdlre ^fjkqjg&", sinhalaFont);
            ph.Add(chh3);
            cell145 = new PdfPCell(ph);
            cell145.HorizontalAlignment = 0;
            cell145.Border = 0;
            cell145.Colspan = 3;
            tbl14.AddCell(cell145);
        }

        PdfPCell cell146 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Y%S ,xld rÌK ixia:dj", sinhalaFont);
            ph.Add(chh3);
            cell146 = new PdfPCell(ph);
            cell146.HorizontalAlignment = 0;
            cell146.Border = 0;
            cell146.Colspan = 3;
            tbl14.AddCell(cell146);
        }


        PdfPCell cell147 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("fkd21 fjdlafIda,a jSoSh fld<U 02 oqrl:k wxl ( 0112357276 0112357278 0112357200", sinhalaFont);
            ph.Add(chh3);
            cell147 = new PdfPCell(ph);
            cell147.HorizontalAlignment = 0;
            cell147.Border = 0;
            cell147.Colspan = 3;
            tbl14.AddCell(cell147);
        }

        PdfPCell cell148 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", sinhalaFont);
            ph.Add(chh3);
            cell148 = new PdfPCell(ph);
            cell148.HorizontalAlignment = 0;
            cell148.Border = 0;
            cell148.Colspan = 3;
            tbl14.AddCell(cell148);
        }

        PdfPCell cell149 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("oskh ", sinhalaFont);
            Chunk c1 = new Chunk(DateTime.Today.ToString("yyyy / MM / dd"), bodyFont);
            ph.Add(chh3);
            ph.Add(c1);
            cell149 = new PdfPCell(ph);
            cell149.HorizontalAlignment = 0;
            cell149.Border = 0;
            cell149.Colspan = 3;
            tbl14.AddCell(cell149);
        }

        document.Add(tbl14);
        //writer.CloseStream = false;
        document.NewPage();


        document.Add(new Paragraph("\n\n"));
        int[] clmwidths1115 = { 90, 30 };
        PdfPTable tbl141 = new PdfPTable(2);
        tbl141.SetWidths(clmwidths111);

        tbl141.WidthPercentage = 100;
        tbl141.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl141.SpacingBefore = 10;
        tbl141.SpacingAfter = 0;
        tbl141.DefaultCell.Border = 0;

        PdfPCell cell133 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("wfma fhduqj PS$ysusluz$", sinhalaFont);
            ph.Add(chh3);
            Chunk chh38 = new Chunk(this.HiddenField3.Value, bodyFont);
            ph.Add(chh38);
            cell133 = new PdfPCell(ph);
            cell133.HorizontalAlignment = 1;
            cell133.Border = 0;
            cell133.Colspan = 3;
            tbl141.AddCell(cell133);
        }

        PdfPCell cell1231 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Y%S ,xld rCIK ixia:dj", sinhalaFont);
            ph.Add(chh3);
            cell1231 = new PdfPCell(ph);
            cell1231.HorizontalAlignment = 1;
            cell1231.Border = 0;
            cell1231.Colspan = 3;
            tbl141.AddCell(cell1231);
        }

        PdfPCell cell150 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("1961 wxl 2 orK ixia:d mK; hgf;a ixia:dms;hs", sinhalaFont);
            ph.Add(chh3);
            cell150 = new PdfPCell(ph);
            cell150.HorizontalAlignment = 1;
            cell150.Border = 0;
            cell150.Colspan = 3;
            tbl141.AddCell(cell150);
        }

        PdfPCell cell151 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell151 = new PdfPCell(ph);
            cell151.HorizontalAlignment = 1;
            cell151.Border = 0;
            cell151.Colspan = 3;
            tbl141.AddCell(cell151);
        }

        string tobePaidText = this.get_netAmtPayInWords(Convert.ToDouble(this.HiddenField22.Value));

        PdfPCell cell152 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh31 = new Chunk(this.HiddenField26.Value + litName.Text, bodyFont);
            Chunk chh3 = new Chunk(" f.a cSjs;h$ cSjs;$ fjkqfjka jQ wxl " + litPolNumber.Text + " orK rlaIs; uqo, re'" + litSumIns.Text, sinhalaFont);
            Chunk chh4 = new Chunk("jk l,a msreKq Tmamqj f.jd ksu lsrSu'", sinhalaFont);
            Chunk chh41 = new Chunk("                                                                                       ", sinhalaFont);
            Chunk chh5 = new Chunk("cSjs; rCIs;hd$ rCIs;hka $mejreuz,dNShd jk  ", sinhalaFont);
            Chunk chh51 = new Chunk(this.HiddenField26.Value + litName.Text, bodyFont);
            Chunk chh511 = new Chunk("  keu;s uu$wms mQrAfjdla; cSjs;h $", sinhalaFont);
            Chunk chh6 = new Chunk("cSjs; fjkqfjka jQo " + this.HiddenField1.Value + "$" + this.HiddenField2.Value + "$" + this.HiddenField27.Value + " osk l,amsreKq njg m;a jQo Tmamqj hgf;a we;s uf.a$wfma ishMZ ysuslu iuzmQrAKfhk ", sinhalaFont);
            Chunk chh7 = new Chunk("fnzreuz lrjd .ekSula jYfhka iy ksoyia lrjd .ekSula jYfhka by; ioZyka ixia:dfjka my; jsia;r jk mrsos ", sinhalaFont);
            Chunk chh8 = new Chunk(tobePaidText + " " + "(" + this.HiddenField22.Value + ")", boldTableFont);
            Chunk chh81 = new Chunk("jq uqo,la ", sinhalaFont);
            Chunk chh9 = new Chunk("Ndr.ekSug tl. njo tu Tmamqj wj,x.= lsrSu msKsi mQrAfjdala; ixia:djg  ndrfok nj ", sinhalaFont);
            Chunk chh10 = new Chunk("fuhska m%ldY lrus$lruq' fuys m%;s,dN uqo, .Kkhlr we;af;a u jsiska f.jsh= hq; ish,q jdrsl", sinhalaFont);
            Chunk chh11 = new Chunk(" f.jd we;s nj mokuz lrf.knj oksu's ;jo Tmamqfjys uq,q ld, mrspzfPaoh ;=, lsishuz wjia:djl jdrslhla fyda", sinhalaFont);
            Chunk chh12 = new Chunk("jdrsl f.jd fkdue;s kuz tfiaf.jd fkdue; s jdrslh fyda jdrsl fmd,sh; a iu. wjidk ", sinhalaFont);
            Chunk chh13 = new Chunk("ysuslu uqo,ska wvqlrk njo uu oksu", sinhalaFont);
            Chunk chh14 = new Chunk("", sinhalaFont);
            ph.Add(chh31);
            ph.Add(chh3);
            ph.Add(chh4);
            ph.Add(chh41);
            ph.Add(chh5);
            ph.Add(chh51);
            ph.Add(chh511);
            ph.Add(chh6);
            ph.Add(chh7);
            ph.Add(chh8);
            ph.Add(chh81);
            ph.Add(chh9);
            ph.Add(chh10);
            ph.Add(chh11);
            ph.Add(chh12);
            ph.Add(chh13);
            ph.Add(chh14);

            cell152 = new PdfPCell(ph);
            cell152.HorizontalAlignment = 0;
            cell152.Border = 0;
            cell152.Colspan = 3;
            tbl141.AddCell(cell152);
        }

        document.Add(tbl141);

        int[] clmwidths1112 = { 50, 25, 25 };
        PdfPTable tbl142 = new PdfPTable(3);
        tbl142.SetWidths(clmwidths1112);

        tbl142.WidthPercentage = 100;
        tbl142.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl142.SpacingBefore = 12;
        tbl142.SpacingAfter = 0;
        tbl142.DefaultCell.Border = 0;

        cell = new PdfPCell(new Phrase("rCIs; uqo, $f.jd ksus w.h", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", sinhalaFont));
        tbl142.AddCell(new Phrase("re'" + this.HiddenField8.Value, sinhalaFont));


        PdfPCell cell123215 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh35 = new Chunk(this.litComDate.Text, bodyFont);
            ph.Add(chh35);
            Chunk chh355 = new Chunk(" isg " + this.HiddenField1.Value + this.HiddenField2.Value + "olajd ld,h fjkqfjka ysus m%ido uqo, ", sinhalaFont);
            ph.Add(chh355);
            cell123215 = new PdfPCell(ph);
            cell123215.HorizontalAlignment = 0;
            cell123215.Border = 0;
            cell123215.Colspan = 3;
            tbl14.AddCell(cell123215);
        }

        cell = new PdfPCell(new Phrase("w;=re m%ido uqo,", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", sinhalaFont));
        tbl142.AddCell(new Phrase("re'" + this.HiddenField10.Value, sinhalaFont));

        cell = new PdfPCell(new Phrase("", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", sinhalaFont));
        tbl142.AddCell(new Phrase("re'" + this.HiddenField22.Value, sinhalaFont));

        cell = new PdfPCell(new Phrase("wvq l,d", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("ys.Z jdr uqo,a", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("re'" + this.HiddenField13.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("ta ioZyd fmd,sh", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("re'" + this.HiddenField14.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("Kh uqo,a", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("re'" + this.HiddenField11.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("wxl", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(this.HiddenField18.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("oskh ", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(this.HiddenField19.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("Kh uqo, ioZyd fmd,sh", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("re'" + this.HiddenField12.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("fjk;a wvq lsrSu", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("re'" + this.HiddenField23.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("uqoaor .dia;", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("re'" + this.HiddenField17.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("wvq lsrSu tl;=j", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("re'" + this.HiddenField24.Value, sinhalaFont));
        tbl142.AddCell(new Phrase("", sinhalaFont));

        cell = new PdfPCell(new Phrase("tl;= Y=oaO uqo,", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", sinhalaFont));
        tbl142.AddCell(new Phrase("re'" + this.HiddenField25.Value, sinhalaFont));


        PdfPCell cell20029 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell20029 = new PdfPCell(ph);
            cell20029.HorizontalAlignment = 0;
            cell20029.Border = 0;
            cell20029.Colspan = 3;
            tbl142.AddCell(cell20029);
        }


        PdfPCell cell2002 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("20 '''''''''''''''''''''' jq'''''''''''''''''''''''''''''''''''''''''ui''''''''''''''' jeks osk ''''''''''''''''''''''''''''''''''''oSh'", sinhalaFont);
            ph.Add(chh3);
            cell2002 = new PdfPCell(ph);
            cell2002.HorizontalAlignment = 0;
            cell2002.Border = 0;
            cell2002.Colspan = 3;
            tbl142.AddCell(cell2002);
        }

        PdfPCell cell2003 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("my; ioZyka mdrAYjh $ mdrAYj jsiska ud bosrsfha ish w;aik$ w; aika udmge.s,", sinhalaFont);
            ph.Add(chh3);
            cell2003 = new PdfPCell(ph);
            cell2003.HorizontalAlignment = 0;
            cell2003.Border = 0;
            cell2003.Colspan = 3;
            tbl142.AddCell(cell2003);
        }

        PdfPCell cell2004 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("i,l=K$i,l=Kq ;nk ,o nj;a fuys wvx.= lreKq ujsiska tu mdrAYjhg $", sinhalaFont);
            ph.Add(chh3);
            cell2004 = new PdfPCell(ph);
            cell2004.HorizontalAlignment = 0;
            cell2004.Border = 0;
            cell2004.Colspan = 3;
            tbl142.AddCell(cell2004);
        }

        PdfPCell cell2005 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("$mdrAYjhkag jsjsO jQ mrsos mrsjrA;kh fldg f;dard fok ,o nj;a tu", sinhalaFont);
            ph.Add(chh3);
            cell2005 = new PdfPCell(ph);
            cell2005.HorizontalAlignment = 0;
            cell2005.Border = 0;
            cell2005.Colspan = 3;
            tbl142.AddCell(cell2005);
        }

        PdfPCell cell2006 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("mdrAYjh$mdrAYj ujsiska yoZqkd.kakd, o nj; a iy; sl lrus'", sinhalaFont);
            ph.Add(chh3);
            cell2006 = new PdfPCell(ph);
            cell2006.HorizontalAlignment = 0;
            cell2006.Border = 0;
            cell2006.Colspan = 3;
            tbl142.AddCell(cell2006);
        }

        PdfPCell cell2007 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("                                                                                    ............................................................ ", bodyFont2);
            ph.Add(chh3);
            cell2007 = new PdfPCell(ph);
            cell2007.HorizontalAlignment = 0;
            cell2007.Border = 0;
            cell2007.Colspan = 3;
            tbl142.AddCell(cell2007);
        }

        PdfPCell cell2008 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("                                                                                  " + "rCIs;hdf.a$rCIs;hkaf.a$mejreuz,dNshdf. mqreoq w;aik", sinhalaFont);
            ph.Add(chh3);
            cell2008 = new PdfPCell(ph);
            cell2008.HorizontalAlignment = 0;
            cell2008.Border = 0;
            cell2008.Colspan = 3;
            tbl142.AddCell(cell2008);
        }

        PdfPCell cell2009 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2009 = new PdfPCell(ph);
            cell2009.HorizontalAlignment = 0;
            cell2009.Border = 0;
            cell2009.Colspan = 3;
            tbl142.AddCell(cell2009);
        }

        cell = new PdfPCell(new Phrase("idCIslref.a fyda iy;sl lrk ks,Odrshdf.a w;aik", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":......................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("idCIslref.a ku", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":......................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("mojs kduh fyda /lshdj", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":......................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(",smskh", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":......................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":......................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":......................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        PdfPCell cell2016 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("ie hq  rCIs;hdf.a$rCIs;hkaf.a mejreuz,dNshdf.a fuu w;aik isxy, okakd = mqoa.,fhl= jsiska iy;sl l, hq;=h", sinhalaFont);
            ph.Add(chh3);
            cell2016 = new PdfPCell(ph);
            cell2016.HorizontalAlignment = 0;
            cell2016.Border = 0;
            cell2016.Colspan = 3;
            tbl142.AddCell(cell2016);
        }
        PdfPCell cell2017 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("rCIs;hd $rCIs;hka mejreuz,dNshd wl=re fkdokfka kuz Tyqf.a fydawehf.a udmge.s,s i<l=K$i<l=Kq ", sinhalaFont);
            ph.Add(chh3);
            cell2017 = new PdfPCell(ph);
            cell2017.HorizontalAlignment = 0;
            cell2017.Border = 0;
            cell2017.Colspan = 3;
            tbl142.AddCell(cell2017);
        }
        PdfPCell cell2018 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("iudodk jsksYaphldrhl= fydakS;S{flfkl= fyda osjqreuz flduidrsia flfkl= jsiska iy;sl l< hq;=h", sinhalaFont);
            ph.Add(chh3);
            cell2018 = new PdfPCell(ph);
            cell2018.HorizontalAlignment = 0;
            cell2018.Border = 0;
            cell2018.Colspan = 3;
            tbl142.AddCell(cell2018);
        }

        PdfPCell cell2019 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("uqoaor .dia;= by; ysuslus uqo,ska wvqlr we;", sinhalaFont);
            ph.Add(chh3);
            cell2019 = new PdfPCell(ph);
            cell2019.HorizontalAlignment = 0;
            cell2019.Border = 0;
            cell2019.Colspan = 3;
            tbl142.AddCell(cell2019);
        }

        PdfPCell cell2020 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", sinhalaFont);
            ph.Add(chh3);
            cell2020 = new PdfPCell(ph);
            cell2020.HorizontalAlignment = 0;
            cell2020.Border = 0;
            cell2020.Colspan = 3;
            tbl142.AddCell(cell2020);
        }

        document.Add(tbl142);

        document.NewPage();
        document.Add(new Paragraph("\n\n"));

        int[] clmwidths1122 = { 100 };
        PdfPTable tbl143 = new PdfPTable(1);
        tbl143.WidthPercentage = 100;
        tbl143.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl143.SpacingBefore = 12;
        tbl143.SpacingAfter = 0;
        tbl143.DefaultCell.Border = 0;

        PdfPCell cell2080 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("jeo.;a ksfjzokhh", sinhalaFont);
            ph.Add(chh3);
            cell2080 = new PdfPCell(ph);
            cell2080.HorizontalAlignment = 1;
            cell2080.Border = 0;
            cell2080.Colspan = 3;
            tbl143.AddCell(cell2080);
        }

        PdfPCell cell2081 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2081 = new PdfPCell(ph);
            cell2081.HorizontalAlignment = 0;
            cell2081.Border = 0;
            cell2081.Colspan = 3;
            tbl143.AddCell(cell2081);
        }

        PdfPCell cell2082 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("mQrAKFj ysuslug wod, fplam; Tn fj; tjSu $ nexl=jg ner lsrSu i|yd f.jd ksulsrSfuz", sinhalaFont);
            ph.Add(chh3);
            cell2082 = new PdfPCell(ph);
            cell2082.HorizontalAlignment = 0;
            cell2082.Border = 0;
            cell2082.Colspan = 3;
            tbl143.AddCell(cell2082);
        }

        PdfPCell cell2083 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("l=js;dkaish iuzmqrAK lr bosrsm;a l< hq;=h' fjk;a mdrAYjhlg uqo,a m;a jSu je<elajSu i|yd", sinhalaFont);
            ph.Add(chh3);
            cell2083 = new PdfPCell(ph);
            cell2083.HorizontalAlignment = 0;
            cell2083.Border = 0;
            cell2083.Colspan = 3;
            tbl143.AddCell(cell2083);
        }

        PdfPCell cell2084 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Tnf.a nexl= .sKqug uqo,a ner lsrSug my; i|yka wxl 01 ,smsfhys Tnf.a kuska we;s cx.u", sinhalaFont);
            ph.Add(chh3);
            cell2084 = new PdfPCell(ph);
            cell2084.HorizontalAlignment = 0;
            cell2084.Border = 0;
            cell2084.Colspan = 3;
            tbl143.AddCell(cell2084);
        }

        PdfPCell cell2085 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("fyda b;srslsrSfuz .sKqul jsia;r i|yka lr wm fj; okajkak' wvq jYfhka b;srs lsrSfuz .sKqula", sinhalaFont);
            ph.Add(chh3);
            cell2085 = new PdfPCell(ph);
            cell2085.HorizontalAlignment = 0;
            cell2085.Border = 0;
            cell2085.Colspan = 3;
            tbl143.AddCell(cell2085);
        }

        PdfPCell cell1189 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chhh5 = new Chunk("fyda fkdue;s kuz b;srs lsrSfuz .sKqula jsjD; lr  wod, jsia;r wm fj; oekajSu Tnf.a", sinhalaFont);
            ph.Add(chhh5);
            cell1189 = new PdfPCell(ph);
            cell1189.HorizontalAlignment = 0;
            cell1189.Border = 0;
            cell1189.Colspan = 3;
            tbl143.AddCell(cell1189);
        }

        PdfPCell cell11891 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chhh5 = new Chunk("uqo, iqrCIs; j ,nd.ekSug bjy,a jkjd we;'", sinhalaFont);
            ph.Add(chhh5);
            cell11891 = new PdfPCell(ph);
            cell11891.HorizontalAlignment = 0;
            cell11891.Border = 0;
            cell11891.Colspan = 3;
            tbl143.AddCell(cell11891);
        }

        int[] clmwidths1124 = { 60, 40 };
        PdfPTable tbl1413 = new PdfPTable(2);
        tbl1413.SetWidths(clmwidths1124);

        tbl1413.WidthPercentage = 100;
        tbl1413.SpacingBefore = 0;
        tbl1413.SpacingAfter = 0;
        tbl1413.DefaultCell.Border = 0;

        PdfPCell cell2088 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2088 = new PdfPCell(ph);
            cell2088.HorizontalAlignment = 0;
            cell2088.Border = 1;
            cell2088.Colspan = 3;
            tbl1413.AddCell(cell2088);
        }
        cell = new PdfPCell(new Phrase(" ", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("", sinhalaFont));
        cell.BorderWidthTop = 1;
        cell.BorderWidthLeft = 1;

        Phrase phrase1 = new Phrase();
        phrase1.Add(
            new Chunk("", new Font(bf, 11, Font.NORMAL))
        );
        phrase1.Add(new Chunk("", new Font()));
        tbl1413.AddCell(phrase1);

        Phrase phrase3 = new Phrase();
        phrase3.Add(
            new Chunk("wmf.a fhduqj", new Font(bf, 11, Font.NORMAL))
        );
        phrase3.Add(new Chunk("1" + this.HiddenField3.Value, new Font()));
        tbl1413.AddCell(phrase3);

        cell = new PdfPCell(new Phrase("wxl 1 ,smsh  ", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("ysusluz uqo,a Tnf.a .sKqug iDcqj ner ", sinhalaFont));
        cell.BorderWidthTop = 1;
        cell.BorderWidthLeft = 1;

        cell = new PdfPCell(new Phrase("Y%S ,xld bkaIqjrkaia fldamfraIka ,sñgÙ", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("l< yelafla kuz th nerl< miq flg", sinhalaFont));

        cell = new PdfPCell(new Phrase("", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("mksjsvhla  uÛska Tn fj; oekajSu", sinhalaFont));

        cell = new PdfPCell(new Phrase("", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("i|yd Tnf.a cx.u oqrl:k wxlhla", sinhalaFont));

        cell = new PdfPCell(new Phrase("", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("i|yka lrkak", sinhalaFont));

        cell = new PdfPCell(new Phrase("rCIK Tmamq wxlh " + litPolNumber.Text, sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("", bodyFont2));


        PdfPCell cell123251 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh315 = new Chunk("ku ", sinhalaFont);
            ph.Add(chh315);
            Chunk chh3551 = new Chunk(litName.Text, bodyFont);
            ph.Add(chh3551);
            cell123251 = new PdfPCell(ph);
            cell123251.HorizontalAlignment = 0;
            cell123251.Border = 0;
            cell123251.Colspan = 3;
            tbl1413.AddCell(cell123251);
        }

        PdfPCell cell20914 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("                                                                        ", bodyFont2);
            ph.Add(chh3);
            cell20914 = new PdfPCell(ph);
            cell20914.HorizontalAlignment = 0;
            cell20914.Border = 1;
            cell20914.Colspan = 3;
            tbl1413.AddCell(cell20914);
        }

        PdfPCell cell2096 = new PdfPCell(signature);
        {
            cell2096.Colspan = 2;
            cell2096.HorizontalAlignment = 0;
            cell2096.Border = 0;
            cell2096.BorderWidthBottom = 0;
            cell2096.BorderWidthTop = 0;
            cell2096.BorderWidthLeft = 0;
            cell2096.BorderWidthRight = 0;
            tbl1413.AddCell(cell2096);
        }


        document.Add(tbl143);
        document.Add(tbl1413);

        writer.CloseStream = false;
        document.NewPage();
        document.Close();
        Response.Buffer = false;
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();

        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Covering_Sinhala.pdf"));
        Response.BinaryWrite(output.ToArray());

    }


    protected void Button3_Click(object sender, EventArgs e)
    {
        //hfAccordionIndex1.Value = "Y";

        if (litPolType.Text == "DIVI THILINA - Endowment Assurance")
        {
          //  print1();
        }

        else if (litPolType.Text == "Early Cash")
        {
          //  print3();
        }


    }

    private void print1()
    {
        Document document = new Document(PageSize.A4, 50, 50, 25, 25);
        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        document.Open();
        Font titleFont1 = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font boldTableFont = FontFactory.GetFont("Times New Roman", 11, Font.BOLD);
        Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 11, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont4_bold = FontFactory.GetFont("Times New Roman", 7, Font.BOLD);
        Font bodyFont5 = FontFactory.GetFont("Times New Roman", 7, Font.NORMAL);
        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);
        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font bodyFont2_bold_und = FontFactory.GetFont("Times New Roman", 10, Font.BOLD | Font.UNDERLINE);

        iTextSharp.text.Image signature = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/sq1.jpg"));
        signature.ScalePercent(60f);
        signature.SetAbsolutePosition(70, 170);

        document.Add(new Paragraph("\n\n"));
        int[] clmwidths111 = { 60, 40 };
        PdfPTable tbl14 = new PdfPTable(2);
        tbl14.SetWidths(clmwidths111);

        tbl14.WidthPercentage = 100;
        tbl14.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl14.SpacingBefore = 12;
        tbl14.SpacingAfter = 0;
        tbl14.DefaultCell.Border = 0;

        PdfPCell cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase(" ", bodyFont2));

        cell = new PdfPCell(new Phrase(HiddenField26.Value + litName.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("Our Ref:L/Claim/" + "1" + this.HiddenField3.Value, bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr1.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("Date: " + DateTime.Today.ToString("yyyy/MM/dd"), bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr2.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr3.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr4.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(" ", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Sir/Madam,", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Policy No :" + litPolNumber.Text, bodyFont2));
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Life Assured :" + HiddenField26.Value + litName.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        PdfPCell cell2001 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2001 = new PdfPCell(ph);
            cell2001.HorizontalAlignment = 0;
            cell2001.Border = 0;
            cell2001.Colspan = 3;
            tbl14.AddCell(cell2001);
        }

        PdfPCell cell200 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("We have pleasure in advising you that above mentioned policy is due to mature on " + this.HiddenField1.Value + "/" + this.HiddenField2.Value + "/" + this.HiddenField27.Value + ".", bodyFont2);
            ph.Add(chh3);
            cell200 = new PdfPCell(ph);
            cell200.HorizontalAlignment = 0;
            cell200.Border = 0;
            cell200.Colspan = 3;
            tbl14.AddCell(cell200);
        }
        PdfPCell cell201 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Particulars of the amount due under the Policy are set out in the enclosed discharge receipt.", bodyFont2);
            ph.Add(chh3);
            cell201 = new PdfPCell(ph);
            cell201.HorizontalAlignment = 0;
            cell201.Border = 0;
            cell201.Colspan = 3;
            tbl14.AddCell(cell201);
        }

        PdfPCell cell202 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell202 = new PdfPCell(ph);
            cell202.HorizontalAlignment = 0;
            cell202.Border = 0;
            cell202.Colspan = 3;
            tbl14.AddCell(cell202);
        }

        PdfPCell cell203 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("In order to enable us to settle this claim, kindly comply with under mentioned requirements:", bodyFont2);
            ph.Add(chh3);
            cell203 = new PdfPCell(ph);
            cell203.HorizontalAlignment = 0;
            cell203.Border = 0;
            cell203.Colspan = 3;
            tbl14.AddCell(cell203);
        }


        PdfPCell cell2041 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2041 = new PdfPCell(ph);
            cell2041.HorizontalAlignment = 0;
            cell2041.Border = 0;
            cell2041.Colspan = 3;
            tbl14.AddCell(cell2041);
        }

        PdfPCell cell205 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell205 = new PdfPCell(ph);
            cell205.HorizontalAlignment = 0;
            cell205.Border = 0;
            cell205.Colspan = 3;
            tbl14.AddCell(cell205);
        }

        PdfPCell cell206 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("1. Return the attached form of Discharge receipt duly completed (signed, dated and witnessed).", bodyFont2);
            ph.Add(chh3);
            cell206 = new PdfPCell(ph);
            cell206.HorizontalAlignment = 0;
            cell206.Border = 0;
            cell206.Colspan = 3;
            tbl14.AddCell(cell206);
        }

     

        PdfPCell cell208 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("2. Read the attached Important Notice and perfect letter No. 01.", bodyFont2);
            ph.Add(chh3);
            cell208 = new PdfPCell(ph);
            cell208.HorizontalAlignment = 0;
            cell208.Border = 0;
            cell208.Colspan = 3;
            tbl14.AddCell(cell208);
        }

        PdfPCell cell209 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("3. A certified photo copy of the National Identity Card.", bodyFont2);
            ph.Add(chh3);
            cell209 = new PdfPCell(ph);
            cell209.HorizontalAlignment = 0;
            cell209.Border = 0;
            cell209.Colspan = 3;
            tbl14.AddCell(cell209);
        }

        PdfPCell cell210 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("4. A certified photo copy of the 1st page of Bank Pass book where the account number & name are", bodyFont2);
            ph.Add(chh3);
            cell210 = new PdfPCell(ph);
            cell210.HorizontalAlignment = 0;
            cell210.Border = 0;
            cell210.Colspan = 3;
            tbl14.AddCell(cell210);
        }

        PdfPCell cell211 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("    indicated.", bodyFont2);
            ph.Add(chh3);
            cell211 = new PdfPCell(ph);
            cell211.HorizontalAlignment = 0;
            cell211.Border = 0;
            cell211.Colspan = 3;
            tbl14.AddCell(cell211);
        }

        LifeCustomer customer1 = new LifeCustomer();
        customer1.getloan_det(this.litPolNumber.Text);
        string view11 = customer1.viewStatus1;

        if (view11 == "N")
        {
            PdfPCell cell2118 = new PdfPCell();
            {
                Phrase ph = new Phrase();
                Chunk chh3 = new Chunk("5. Original Policy Document", bodyFont2);
                ph.Add(chh3);
                cell2118 = new PdfPCell(ph);
                cell2118.HorizontalAlignment = 0;
                cell2118.Border = 0;
                cell2118.Colspan = 3;
                tbl14.AddCell(cell2118);
            }
        }

        else { }

        PdfPCell cell212 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell212 = new PdfPCell(ph);
            cell212.HorizontalAlignment = 0;
            cell212.Border = 0;
            cell212.Colspan = 3;
            tbl14.AddCell(cell212);
        }

        PdfPCell cell213 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("N.B. If you have arranged any standing order advice with your banker to remit the premium on ", bodyFont2);
            ph.Add(chh3);
            cell213 = new PdfPCell(ph);
            cell213.HorizontalAlignment = 0;
            cell213.Border = 0;
            cell213.Colspan = 3;
            tbl14.AddCell(cell213);
        }

        PdfPCell cell214 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("time, please advice them to terminate the same with effect from last due as indicated in your ", bodyFont2);
            ph.Add(chh3);
            cell214 = new PdfPCell(ph);
            cell214.HorizontalAlignment = 0;
            cell214.Border = 0;
            cell214.Colspan = 3;
            tbl14.AddCell(cell214);
        }

        PdfPCell cell215 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("policy document.", bodyFont2);
            ph.Add(chh3);
            cell215 = new PdfPCell(ph);
            cell215.HorizontalAlignment = 0;
            cell215.Border = 0;
            cell215.Colspan = 3;
            tbl14.AddCell(cell215);
        }

        PdfPCell cell216 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell216 = new PdfPCell(ph);
            cell216.HorizontalAlignment = 0;
            cell216.Border = 0;
            cell216.Colspan = 3;
            tbl14.AddCell(cell216);
        }

        PdfPCell cell217 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("We take this opportunity to thank you for obtaining a Life Policy from us.", bodyFont2);
            ph.Add(chh3);
            cell217 = new PdfPCell(ph);
            cell217.HorizontalAlignment = 0;
            cell217.Border = 0;
            cell217.Colspan = 3;
            tbl14.AddCell(cell217);
        }

        PdfPCell cell218 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell218 = new PdfPCell(ph);
            cell218.HorizontalAlignment = 0;
            cell218.Border = 0;
            cell218.Colspan = 3;
            tbl14.AddCell(cell218);
        }

        PdfPCell cell219 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Thanking You,", bodyFont2);
            ph.Add(chh3);
            cell219 = new PdfPCell(ph);
            cell219.HorizontalAlignment = 0;
            cell219.Border = 0;
            cell219.Colspan = 3;
            tbl14.AddCell(cell219);
        }
        PdfPCell cell220 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Yours Faithfully,", bodyFont2);
            ph.Add(chh3);
            cell220 = new PdfPCell(ph);
            cell220.HorizontalAlignment = 0;
            cell220.Border = 0;
            cell220.Colspan = 3;
            tbl14.AddCell(cell220);
        }

        PdfPCell cell221 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell221 = new PdfPCell(ph);
            cell221.HorizontalAlignment = 0;
            cell221.Border = 0;
            cell221.Colspan = 3;
            tbl14.AddCell(cell221);
        }

        PdfPCell cell222 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("For Manager Life,", bodyFont2);
            ph.Add(chh3);
            cell222 = new PdfPCell(ph);
            cell222.HorizontalAlignment = 0;
            cell222.Border = 0;
            cell222.Colspan = 3;
            tbl14.AddCell(cell222);
        }

        PdfPCell cell223 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("SRI LANKA INSURANCE CORPORATION LTD.", boldTableFont);
            ph.Add(chh3);
            cell223 = new PdfPCell(ph);
            cell223.HorizontalAlignment = 0;
            cell223.Border = 0;
            cell223.Colspan = 3;
            tbl14.AddCell(cell223);
        }

        PdfPCell cell224 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("No. 21, Vauxhall Street, Colombo 2. Tel : 0112357276, 0112357278, 0112357200", bodyFont2);
            ph.Add(chh3);
            cell224 = new PdfPCell(ph);
            cell224.HorizontalAlignment = 0;
            cell224.Border = 0;
            cell224.Colspan = 3;
            tbl14.AddCell(cell224);
        }

        PdfPCell cell225 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell225 = new PdfPCell(ph);
            cell225.HorizontalAlignment = 0;
            cell225.Border = 0;
            cell225.Colspan = 3;
            tbl14.AddCell(cell225);
        }

        PdfPCell cell226 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Date : " + DateTime.Today.ToString(), bodyFont2);
            ph.Add(chh3);
            cell226 = new PdfPCell(ph);
            cell226.HorizontalAlignment = 0;
            cell226.Border = 0;
            cell226.Colspan = 3;
            tbl14.AddCell(cell226);
        }

        document.Add(tbl14);
        //writer.CloseStream = false;
        document.NewPage();

        document.Add(new Paragraph("\n\n"));
        int[] clmwidths1115 = { 90, 30 };
        PdfPTable tbl141 = new PdfPTable(2);
        tbl141.SetWidths(clmwidths111);

        tbl141.WidthPercentage = 100;
        tbl141.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl141.SpacingBefore = 10;
        tbl141.SpacingAfter = 0;
        tbl141.DefaultCell.Border = 0;

        PdfPCell cell227 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Discharge receipt for Life Assurance Maturity Payment-Claim No :"+"1" + this.HiddenField3.Value, bodyFont2);
            ph.Add(chh3);
            cell227 = new PdfPCell(ph);
            cell227.HorizontalAlignment = 0;
            cell227.Border = 0;
            cell227.Colspan = 3;
            tbl141.AddCell(cell227);
        }

        PdfPCell cell2271 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2271 = new PdfPCell(ph);
            cell2271.HorizontalAlignment = 0;
            cell2271.Border = 0;
            cell2271.Colspan = 3;
            tbl141.AddCell(cell2271);
        }
        PdfPCell cell228 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Discharge of Matured Policy No " + this.litPolNumber.Text + ", Sum Assured " + litSumIns.Text + ", On the Life of", bodyFont2);
            ph.Add(chh3);
            cell228 = new PdfPCell(ph);
            cell228.HorizontalAlignment = 0;
            cell228.Border = 0;
            cell228.Colspan = 3;
            tbl141.AddCell(cell228);
        }

        PdfPCell cell229 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(HiddenField26.Value + litName.Text, bodyFont2);
            ph.Add(chh3);
            cell229 = new PdfPCell(ph);
            cell229.HorizontalAlignment = 0;
            cell229.Border = 0;
            cell229.Colspan = 3;
            tbl141.AddCell(cell229);
        }

        PdfPCell cell230 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell230 = new PdfPCell(ph);
            cell230.HorizontalAlignment = 0;
            cell230.Border = 0;
            cell230.Colspan = 3;
            tbl141.AddCell(cell230);
        }

        PdfPCell cell231 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("I " + HiddenField26.Value + litName.Text, bodyFont2);
            ph.Add(chh3);
            cell231 = new PdfPCell(ph);
            cell231.HorizontalAlignment = 0;
            cell231.Border = 0;
            cell231.Colspan = 3;
            tbl141.AddCell(cell231);
        }

        PdfPCell cell232 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("The Life Assured/s/ Assignee do here by agreed to receive the sum of :", bodyFont2);
            ph.Add(chh3);
            cell232 = new PdfPCell(ph);
            cell232.HorizontalAlignment = 0;
            cell232.Border = 0;
            cell232.Colspan = 3;
            tbl141.AddCell(cell232);
        }

        string tobePaidText = this.get_netAmtPayInWords(Convert.ToDouble(this.HiddenField22.Value));

        PdfPCell cell233 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(tobePaidText + "(" + this.HiddenField22.Value + ")", bodyFont2);
            ph.Add(chh3);
            cell233 = new PdfPCell(ph);
            cell233.HorizontalAlignment = 0;
            cell233.Border = 0;
            cell233.Colspan = 3;
            tbl141.AddCell(cell233);
        }


        PdfPCell cell234 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("from the Sri Lanka Insurance Corporation as per detail shown below in full satisfaction", bodyFont2);
            ph.Add(chh3);
            cell234 = new PdfPCell(ph);
            cell234.HorizontalAlignment = 0;
            cell234.Border = 0;
            cell234.Colspan = 3;
            tbl141.AddCell(cell234);
        }

        PdfPCell cell235 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("and discharge of all my claims and demands under the above policy on the said Life which matured ", bodyFont2);
            ph.Add(chh3);
            cell235 = new PdfPCell(ph);
            cell235.HorizontalAlignment = 0;
            cell235.Border = 0;
            cell235.Colspan = 3;
            tbl141.AddCell(cell235);
        }

        PdfPCell cell236 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("on " + this.HiddenField1.Value + "/" + this.HiddenField2.Value + "/" + this.HiddenField27.Value + " and this policy is hereby delivered to the said Corporation to be cancelled.", bodyFont2);
            ph.Add(chh3);
            cell236 = new PdfPCell(ph);
            cell236.HorizontalAlignment = 0;
            cell236.Border = 0;
            cell236.Colspan = 3;
            tbl141.AddCell(cell236);
        }

        PdfPCell cell237 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell237 = new PdfPCell(ph);
            cell237.HorizontalAlignment = 0;
            cell237.Border = 0;
            cell237.Colspan = 3;
            tbl141.AddCell(cell237);
        }


        document.Add(tbl141);

        int[] clmwidths1112 = { 50, 25, 25 };
        PdfPTable tbl142 = new PdfPTable(3);
        tbl142.SetWidths(clmwidths1112);

        tbl142.WidthPercentage = 100;
        tbl142.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl142.SpacingBefore = 12;
        tbl142.SpacingAfter = 0;
        tbl142.DefaultCell.Border = 0;

        cell = new PdfPCell(new Phrase("Sum Assured/Paid up value", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("Rs."+this.HiddenField8.Value, bodyFont2));


        cell = new PdfPCell(new Phrase("Vested Bonus for the period from " + this.litComDate.Text + " to " + this.HiddenField1.Value + "/" + this.HiddenField2.Value + "/" + this.HiddenField27.Value, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("Rs."+this.HiddenField9.Value, bodyFont2));

        cell = new PdfPCell(new Phrase("Interim Bonus", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("Rs."+this.HiddenField10.Value, bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase(this.HiddenField22.Value, bodyFont2));

        cell = new PdfPCell(new Phrase("Less", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Default Premiums", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField13.Value, bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Interest thereon", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField14.Value, bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Loan", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField11.Value, bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("No:" + this.HiddenField18.Value, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Date:" + this.HiddenField19.Value, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Interest on loan:", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField12.Value, bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Other deductions:", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField23.Value, bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Stamp Fee", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField17.Value, bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Total deductions", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField24.Value, bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Net amount payble", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField25.Value, bodyFont2));

        PdfPCell cell20029 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell20029 = new PdfPCell(ph);
            cell20029.HorizontalAlignment = 0;
            cell20029.Border = 0;
            cell20029.Colspan = 3;
            tbl142.AddCell(cell20029);
        }


        PdfPCell cell2002 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Dated at ...............................this.........................Day.........................20....", bodyFont2);
            ph.Add(chh3);
            cell2002 = new PdfPCell(ph);
            cell2002.HorizontalAlignment = 0;
            cell2002.Border = 0;
            cell2002.Colspan = 3;
            tbl142.AddCell(cell2002);
        }

        PdfPCell cell2003 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("signed/thumb impression affixed by the above mentioned party in my", bodyFont2);
            ph.Add(chh3);
            cell2003 = new PdfPCell(ph);
            cell2003.HorizontalAlignment = 0;
            cell2003.Border = 0;
            cell2003.Colspan = 3;
            tbl142.AddCell(cell2003);
        }

        PdfPCell cell2004 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("presence and certify that the contents were duly translated and explained by ", bodyFont2);
            ph.Add(chh3);
            cell2004 = new PdfPCell(ph);
            cell2004.HorizontalAlignment = 0;
            cell2004.Border = 0;
            cell2004.Colspan = 3;
            tbl142.AddCell(cell2004);
        }

        PdfPCell cell2005 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("me to the party who has been identified by me.", bodyFont2);
            ph.Add(chh3);
            cell2005 = new PdfPCell(ph);
            cell2005.HorizontalAlignment = 0;
            cell2005.Border = 0;
            cell2005.Colspan = 3;
            tbl142.AddCell(cell2005);
        }

        PdfPCell cell2006 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2006 = new PdfPCell(ph);
            cell2006.HorizontalAlignment = 0;
            cell2006.Border = 0;
            cell2006.Colspan = 3;
            tbl142.AddCell(cell2006);
        }

        PdfPCell cell2007 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("                                                                    ............................................................ ", bodyFont2);
            ph.Add(chh3);
            cell2007 = new PdfPCell(ph);
            cell2007.HorizontalAlignment = 0;
            cell2007.Border = 0;
            cell2007.Colspan = 3;
            tbl142.AddCell(cell2007);
        }

        PdfPCell cell2008 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("                                                                   Usual signature of Life Assureds/Assignee ", bodyFont2);
            ph.Add(chh3);
            cell2008 = new PdfPCell(ph);
            cell2008.HorizontalAlignment = 0;
            cell2008.Border = 0;
            cell2008.Colspan = 3;
            tbl142.AddCell(cell2008);
        }

        PdfPCell cell2009 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2009 = new PdfPCell(ph);
            cell2009.HorizontalAlignment = 0;
            cell2009.Border = 0;
            cell2009.Colspan = 3;
            tbl142.AddCell(cell2009);
        }

        cell = new PdfPCell(new Phrase("Signature of witness", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":.....................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Name of witness", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":.....................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Designation/Occupation", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":.....................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Address", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":..................................... ", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":.....................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":.....................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        PdfPCell cell2016 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Notes :- (1) This Signature of assured/s assignee should be witnessed by a person who could read", bodyFont2);
            ph.Add(chh3);
            cell2016 = new PdfPCell(ph);
            cell2016.HorizontalAlignment = 0;
            cell2016.Border = 0;
            cell2016.Colspan = 3;
            tbl142.AddCell(cell2016);
        }

        PdfPCell cell2017 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("               and write in English.", bodyFont2);
            ph.Add(chh3);
            cell2017 = new PdfPCell(ph);
            cell2017.HorizontalAlignment = 0;
            cell2017.Border = 0;
            cell2017.Colspan = 3;
            tbl142.AddCell(cell2017);
        }

        PdfPCell cell2018 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Notes :- (2) If the assured/s assignee is illiterate, his or her thumb impression should be certified", bodyFont2);
            ph.Add(chh3);
            cell2018 = new PdfPCell(ph);
            cell2018.HorizontalAlignment = 0;
            cell2018.Border = 0;
            cell2018.Colspan = 3;
            tbl142.AddCell(cell2018);
        }
        PdfPCell cell2019 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("              by a Justice  of Peace or an Attorney at law or a Commissioner of the Oaths.", bodyFont2);
            ph.Add(chh3);
            cell2019 = new PdfPCell(ph);
            cell2019.HorizontalAlignment = 0;
            cell2019.Border = 0;
            cell2019.Colspan = 3;
            tbl142.AddCell(cell2019);
        }

        PdfPCell cell2020 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Notes :- (3) Stamp Fee has been deducted from above Claim.", bodyFont2);
            ph.Add(chh3);
            cell2020 = new PdfPCell(ph);
            cell2020.HorizontalAlignment = 0;
            cell2020.Border = 0;
            cell2020.Colspan = 3;
            tbl142.AddCell(cell2020);
        }

        document.Add(tbl142);

        document.NewPage();
        document.Add(new Paragraph("\n\n"));
        int[] clmwidths1122 = { 100 };
        PdfPTable tbl143 = new PdfPTable(1);


        tbl143.WidthPercentage = 100;
        tbl143.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl143.SpacingBefore = 12;
        tbl143.SpacingAfter = 0;
        tbl143.DefaultCell.Border = 0;

        PdfPCell cell2080 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("IMPORTANT NOTICE", bodyFont2);
            ph.Add(chh3);
            cell2080 = new PdfPCell(ph);
            cell2080.HorizontalAlignment = 1;
            cell2080.Border = 0;
            cell2080.Colspan = 3;
            tbl143.AddCell(cell2080);
        }

        PdfPCell cell2081 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2081 = new PdfPCell(ph);
            cell2081.HorizontalAlignment = 0;
            cell2081.Border = 0;
            cell2081.Colspan = 3;
            tbl143.AddCell(cell2081);
        }

        PdfPCell cell2082 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("The Maturity payment will be sent / credited to you or your account on receipt of the completed", bodyFont2);
            ph.Add(chh3);
            cell2082 = new PdfPCell(ph);
            cell2082.HorizontalAlignment = 0;
            cell2082.Border = 0;
            cell2082.Colspan = 3;
            tbl143.AddCell(cell2082);
        }

        PdfPCell cell2083 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Discharge Receipt. If you have a Bank Account, please let us know, by completing letter No.1", bodyFont2);
            ph.Add(chh3);
            cell2083 = new PdfPCell(ph);
            cell2083.HorizontalAlignment = 0;
            cell2083.Border = 0;
            cell2083.Colspan = 3;
            tbl143.AddCell(cell2083);
        }

        PdfPCell cell2084 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("given below, in order to forward the payment directly to your Bank to avoid any third party", bodyFont2);
            ph.Add(chh3);
            cell2084 = new PdfPCell(ph);
            cell2084.HorizontalAlignment = 0;
            cell2084.Border = 0;
            cell2084.Colspan = 3;
            tbl143.AddCell(cell2084);
        }

        PdfPCell cell2085 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("encashing the cheque. If you do not have a bank account, we would advise you to open a Savings", bodyFont2);
            ph.Add(chh3);
            cell2085 = new PdfPCell(ph);
            cell2085.HorizontalAlignment = 0;
            cell2085.Border = 0;
            cell2085.Colspan = 3;
            tbl143.AddCell(cell2085);
        }

        PdfPCell cell2086 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Account and provide the said details to us for the safe receipt of your money.", bodyFont2);
            ph.Add(chh3);
            cell2086 = new PdfPCell(ph);
            cell2086.HorizontalAlignment = 0;
            cell2086.Border = 0;
            cell2086.Colspan = 3;
            tbl143.AddCell(cell2086);
        }

        PdfPCell cell2087 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2087 = new PdfPCell(ph);
            cell2087.HorizontalAlignment = 0;
            cell2087.Border = 0;
            cell2087.Colspan = 3;
            tbl143.AddCell(cell2087);
        }
        PdfPCell cell1189 = new PdfPCell();

        {
            cell1189.Colspan = 3;
            cell1189.HorizontalAlignment = 0;
            cell1189.Border = 0;
            cell1189.BorderWidthBottom = 0;
            cell1189.BorderWidthTop = 0;
            cell1189.BorderWidthLeft = 0;
            cell1189.BorderWidthRight = 0;
            tbl143.AddCell(cell1189);
        }

        int[] clmwidths1124 = { 60, 40 };
        PdfPTable tbl1413 = new PdfPTable(2);
        tbl1413.SetWidths(clmwidths1124);


        tbl1413.WidthPercentage = 100;
        // tbl1413.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl1413.SpacingBefore = 0;
        tbl1413.SpacingAfter = 0;
        tbl1413.DefaultCell.Border = 0;

        PdfPCell cell2088 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2088 = new PdfPCell(ph);
            cell2088.HorizontalAlignment = 0;
            cell2088.Border = 1;
            cell2088.Colspan = 3;
            tbl1413.AddCell(cell2088);
        }

        cell = new PdfPCell(new Phrase("Letter No. 1", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("Ref No. : 1" + this.HiddenField3.Value, bodyFont2));


        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("Please indicate your mobile phone", bodyFont2));

        cell = new PdfPCell(new Phrase("Life/Regional Manager", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("number in order to inform you via", bodyFont2));


        cell = new PdfPCell(new Phrase("Sri Lanka Insurance Corporation Ltd.", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("SMS once the payment is send to ", bodyFont2));

        cell = new PdfPCell(new Phrase("Colombo 2", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("your account if payment could be", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("directly credited to your bank account", bodyFont2));

        cell = new PdfPCell(new Phrase("Policy No : " + this.litPolNumber.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Name : " + this.HiddenField26.Value + this.litName.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("", bodyFont2));

        PdfPCell cell20914 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("                                                                        ", bodyFont2);
            ph.Add(chh3);
            cell20914 = new PdfPCell(ph);
            cell20914.HorizontalAlignment = 0;
            cell20914.Border = 1;
            cell20914.Colspan = 3;
            tbl1413.AddCell(cell20914);
        }

        PdfPCell cell2094 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Please credit my payment to the following bank account or post the cheque to the address given bellow.", bodyFont2);
            ph.Add(chh3);
            cell2094 = new PdfPCell(ph);
            cell2094.HorizontalAlignment = 0;
            cell2094.Border = 0;
            cell2094.Colspan = 3;
            tbl1413.AddCell(cell2094);
        }

        PdfPCell cell2095 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Particulars of the Bank Account", bodyFont2);
            ph.Add(chh3);
            cell2095 = new PdfPCell(ph);
            cell2095.HorizontalAlignment = 0;
            cell2095.Border = 0;
            cell2095.Colspan = 3;
            tbl1413.AddCell(cell2095);
        }

        PdfPCell cell2096 = new PdfPCell(signature);
        {
            cell2096.Colspan = 2;
            cell2096.HorizontalAlignment = 0;
            cell2096.Border = 0;
            cell2096.BorderWidthBottom = 0;
            cell2096.BorderWidthTop = 0;
            cell2096.BorderWidthLeft = 0;
            cell2096.BorderWidthRight = 0;
            tbl1413.AddCell(cell2096);
        }


        document.Add(tbl143);
        document.Add(tbl1413);

        document.Close();
        Response.Buffer = false;
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Covering_English.pdf"));
        Response.BinaryWrite(output.ToArray());
    }

    private void print3()
    {
        Document document = new Document(PageSize.A4, 50, 50, 25, 25);
        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        document.Open();
        Font titleFont1 = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font boldTableFont = FontFactory.GetFont("Times New Roman", 11, Font.BOLD);
        Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 11, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont4_bold = FontFactory.GetFont("Times New Roman", 7, Font.BOLD);
        Font bodyFont5 = FontFactory.GetFont("Times New Roman", 7, Font.NORMAL);
        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);
        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font bodyFont2_bold_und = FontFactory.GetFont("Times New Roman", 10, Font.BOLD | Font.UNDERLINE);

        iTextSharp.text.Image signature = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/sq1.jpg"));
        signature.ScalePercent(60f);
        signature.SetAbsolutePosition(70, 170);

        document.Add(new Paragraph("\n\n"));
        int[] clmwidths111 = { 60, 40 };
        PdfPTable tbl14 = new PdfPTable(2);
        tbl14.SetWidths(clmwidths111);

        tbl14.WidthPercentage = 100;
        tbl14.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl14.SpacingBefore = 12;
        tbl14.SpacingAfter = 0;
        tbl14.DefaultCell.Border = 0;

        PdfPCell cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase(" ", bodyFont2));

        cell = new PdfPCell(new Phrase(HiddenField26.Value + litName.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("Our Ref:L/Claim/" + this.HiddenField3.Value, bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr1.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("Date: " + DateTime.Today.ToString("yyyy/MM/dd"), bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr2.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr3.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr4.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

      
        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Sir/Madam,", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Policy No :" + litPolNumber.Text, bodyFont2));
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Life Assured :" + HiddenField26.Value + litName.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        PdfPCell cell2001 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2001 = new PdfPCell(ph);
            cell2001.HorizontalAlignment = 0;
            cell2001.Border = 0;
            cell2001.Colspan = 3;
            tbl14.AddCell(cell2001);
        }

        PdfPCell cell200 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("We have pleasure in advising you that above mentioned policy is due to mature on" + this.HiddenField1.Value + "/" + this.HiddenField2.Value + "/" + this.HiddenField27.Value + ".", bodyFont2);
            ph.Add(chh3);
            cell200 = new PdfPCell(ph);
            cell200.HorizontalAlignment = 0;
            cell200.Border = 0;
            cell200.Colspan = 3;
            tbl14.AddCell(cell200);
        }
        PdfPCell cell201 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Particulars of the amount due under the Policy are set out in the enclosed discharge receipt.", bodyFont2);
            ph.Add(chh3);
            cell201 = new PdfPCell(ph);
            cell201.HorizontalAlignment = 0;
            cell201.Border = 0;
            cell201.Colspan = 3;
            tbl14.AddCell(cell201);
        }

        PdfPCell cell202 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell202 = new PdfPCell(ph);
            cell202.HorizontalAlignment = 0;
            cell202.Border = 0;
            cell202.Colspan = 3;
            tbl14.AddCell(cell202);
        }

        PdfPCell cell203 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("In order to enable us to settle this claim, kindly comply with under mentioned requirements:", bodyFont2);
            ph.Add(chh3);
            cell203 = new PdfPCell(ph);
            cell203.HorizontalAlignment = 0;
            cell203.Border = 0;
            cell203.Colspan = 3;
            tbl14.AddCell(cell203);
        }


        PdfPCell cell2041 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2041 = new PdfPCell(ph);
            cell2041.HorizontalAlignment = 0;
            cell2041.Border = 0;
            cell2041.Colspan = 3;
            tbl14.AddCell(cell2041);
        }

        PdfPCell cell205 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell205 = new PdfPCell(ph);
            cell205.HorizontalAlignment = 0;
            cell205.Border = 0;
            cell205.Colspan = 3;
            tbl14.AddCell(cell205);
        }

        PdfPCell cell206 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("1. Return the attached form of Discharge receipt duly completed (signed, dated and witnessed).", bodyFont2);
            ph.Add(chh3);
            cell206 = new PdfPCell(ph);
            cell206.HorizontalAlignment = 0;
            cell206.Border = 0;
            cell206.Colspan = 3;
            tbl14.AddCell(cell206);
        }


        PdfPCell cell208 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("2. Read the attached Important Notice and perfect letter No. 01.", bodyFont2);
            ph.Add(chh3);
            cell208 = new PdfPCell(ph);
            cell208.HorizontalAlignment = 0;
            cell208.Border = 0;
            cell208.Colspan = 3;
            tbl14.AddCell(cell208);
        }

        PdfPCell cell209 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("3. A certified photo copy of the National Identity Card.", bodyFont2);
            ph.Add(chh3);
            cell209 = new PdfPCell(ph);
            cell209.HorizontalAlignment = 0;
            cell209.Border = 0;
            cell209.Colspan = 3;
            tbl14.AddCell(cell209);
        }

        PdfPCell cell210 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("4. A certified photo copy of the 1st page of Bank Pass book where the account number & name are", bodyFont2);
            ph.Add(chh3);
            cell210 = new PdfPCell(ph);
            cell210.HorizontalAlignment = 0;
            cell210.Border = 0;
            cell210.Colspan = 3;
            tbl14.AddCell(cell210);
        }

        PdfPCell cell211 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("    indicated.", bodyFont2);
            ph.Add(chh3);
            cell211 = new PdfPCell(ph);
            cell211.HorizontalAlignment = 0;
            cell211.Border = 0;
            cell211.Colspan = 3;
            tbl14.AddCell(cell211);
        }

        PdfPCell cell212 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell212 = new PdfPCell(ph);
            cell212.HorizontalAlignment = 0;
            cell212.Border = 0;
            cell212.Colspan = 3;
            tbl14.AddCell(cell212);
        }

        PdfPCell cell213 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("N.B. If you have arranged any standing order advice with your banker to remit the premium on", bodyFont2);
            ph.Add(chh3);
            cell213 = new PdfPCell(ph);
            cell213.HorizontalAlignment = 0;
            cell213.Border = 0;
            cell213.Colspan = 3;
            tbl14.AddCell(cell213);
        }

        PdfPCell cell214 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("time, please advice them to terminate the same with effect from last due as indicated in your", bodyFont2);
            ph.Add(chh3);
            cell214 = new PdfPCell(ph);
            cell214.HorizontalAlignment = 0;
            cell214.Border = 0;
            cell214.Colspan = 3;
            tbl14.AddCell(cell214);
        }

        PdfPCell cell215 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("policy document.", bodyFont2);
            ph.Add(chh3);
            cell215 = new PdfPCell(ph);
            cell215.HorizontalAlignment = 0;
            cell215.Border = 0;
            cell215.Colspan = 3;
            tbl14.AddCell(cell215);
        }

        PdfPCell cell216 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell216 = new PdfPCell(ph);
            cell216.HorizontalAlignment = 0;
            cell216.Border = 0;
            cell216.Colspan = 3;
            tbl14.AddCell(cell216);
        }

        PdfPCell cell217 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("We take this opportunity to thank you for obtaining a Life Policy from us.", bodyFont2);
            ph.Add(chh3);
            cell217 = new PdfPCell(ph);
            cell217.HorizontalAlignment = 0;
            cell217.Border = 0;
            cell217.Colspan = 3;
            tbl14.AddCell(cell217);
        }

        PdfPCell cell218 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell218 = new PdfPCell(ph);
            cell218.HorizontalAlignment = 0;
            cell218.Border = 0;
            cell218.Colspan = 3;
            tbl14.AddCell(cell218);
        }

        PdfPCell cell219 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Thanking You,", bodyFont2);
            ph.Add(chh3);
            cell219 = new PdfPCell(ph);
            cell219.HorizontalAlignment = 0;
            cell219.Border = 0;
            cell219.Colspan = 3;
            tbl14.AddCell(cell219);
        }
        PdfPCell cell220 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Yours Faithfully,", bodyFont2);
            ph.Add(chh3);
            cell220 = new PdfPCell(ph);
            cell220.HorizontalAlignment = 0;
            cell220.Border = 0;
            cell220.Colspan = 3;
            tbl14.AddCell(cell220);
        }

        PdfPCell cell221 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell221 = new PdfPCell(ph);
            cell221.HorizontalAlignment = 0;
            cell221.Border = 0;
            cell221.Colspan = 3;
            tbl14.AddCell(cell221);
        }

        PdfPCell cell222 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("For Manager Life,", bodyFont2);
            ph.Add(chh3);
            cell222 = new PdfPCell(ph);
            cell222.HorizontalAlignment = 0;
            cell222.Border = 0;
            cell222.Colspan = 3;
            tbl14.AddCell(cell222);
        }

        PdfPCell cell223 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("SRI LANKA INSURANCE CORPORATION LTD.", boldTableFont);
            ph.Add(chh3);
            cell223 = new PdfPCell(ph);
            cell223.HorizontalAlignment = 0;
            cell223.Border = 0;
            cell223.Colspan = 3;
            tbl14.AddCell(cell223);
        }

        PdfPCell cell224 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("No. 21, Vauxhall Street, Colombo 2. Tel : 0112357276, 0112357278, 0112357200", bodyFont2);
            ph.Add(chh3);
            cell224 = new PdfPCell(ph);
            cell224.HorizontalAlignment = 0;
            cell224.Border = 0;
            cell224.Colspan = 3;
            tbl14.AddCell(cell224);
        }

        PdfPCell cell225 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell225 = new PdfPCell(ph);
            cell225.HorizontalAlignment = 0;
            cell225.Border = 0;
            cell225.Colspan = 3;
            tbl14.AddCell(cell225);
        }

        PdfPCell cell226 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Date : " + DateTime.Today.ToString(), bodyFont2);
            ph.Add(chh3);
            cell226 = new PdfPCell(ph);
            cell226.HorizontalAlignment = 0;
            cell226.Border = 0;
            cell226.Colspan = 3;
            tbl14.AddCell(cell226);
        }

        document.Add(tbl14);
        //writer.CloseStream = false;
        document.NewPage();


        document.Add(new Paragraph("\n\n"));
        int[] clmwidths1115 = { 90, 30 };
        PdfPTable tbl141 = new PdfPTable(2);
        tbl141.SetWidths(clmwidths111);

        tbl141.WidthPercentage = 100;
        tbl141.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl141.SpacingBefore = 10;
        tbl141.SpacingAfter = 0;
        tbl141.DefaultCell.Border = 0;

        PdfPCell cell227 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Discharge receipt for Life Assurance Maturity Payment - Claim No : " +"1"+ this.HiddenField3.Value, bodyFont2);
            ph.Add(chh3);
            cell227 = new PdfPCell(ph);
            cell227.HorizontalAlignment = 0;
            cell227.Border = 0;
            cell227.Colspan = 3;
            tbl141.AddCell(cell227);
        }
        PdfPCell cell2271 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2271 = new PdfPCell(ph);
            cell2271.HorizontalAlignment = 0;
            cell2271.Border = 0;
            cell2271.Colspan = 3;
            tbl141.AddCell(cell2271);
        }
        PdfPCell cell228 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Discharge of Matured Policy No " + litPolNumber.Text + ", Sum Assured " + litSumIns.Text + ", On the Life of", bodyFont2);
            ph.Add(chh3);
            cell228 = new PdfPCell(ph);
            cell228.HorizontalAlignment = 0;
            cell228.Border = 0;
            cell228.Colspan = 3;
            tbl141.AddCell(cell228);
        }

        PdfPCell cell229 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(HiddenField26.Value + this.litName.Text, bodyFont2);
            ph.Add(chh3);
            cell229 = new PdfPCell(ph);
            cell229.HorizontalAlignment = 0;
            cell229.Border = 0;
            cell229.Colspan = 3;
            tbl141.AddCell(cell229);
        }

        PdfPCell cell230 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell230 = new PdfPCell(ph);
            cell230.HorizontalAlignment = 0;
            cell230.Border = 0;
            cell230.Colspan = 3;
            tbl141.AddCell(cell230);
        }

        PdfPCell cell231 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("I " + HiddenField26.Value + this.litName.Text, bodyFont2);
            ph.Add(chh3);
            cell231 = new PdfPCell(ph);
            cell231.HorizontalAlignment = 0;
            cell231.Border = 0;
            cell231.Colspan = 3;
            tbl141.AddCell(cell231);
        }

        PdfPCell cell232 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("The Life Assured/s/ Assignee do here by agreed to receive the sum of :", bodyFont2);
            ph.Add(chh3);
            cell232 = new PdfPCell(ph);
            cell232.HorizontalAlignment = 0;
            cell232.Border = 0;
            cell232.Colspan = 3;
            tbl141.AddCell(cell232);
        }

        //string amttobepaid1 = tot.ToString().Substring(0, (tot.ToString().Length - 2)) + "." + tot.ToString().Substring((tot.ToString().Length - 2), 2);
        //string amttobepaid1 = this.HiddenField22.Value.Substring(0, (this.HiddenField22.Value.Length - 2)) + "." + this.HiddenField22.Value.Substring((this.HiddenField22.Value.Length - 2), 2);
        //ReadAmount txtreadtoPay = new ReadAmount();
        string tobePaidText = this.get_netAmtPayInWords(Convert.ToDouble(this.HiddenField22.Value));

        PdfPCell cell233 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(tobePaidText + "(" + this.HiddenField22.Value + ")", bodyFont2);
            ph.Add(chh3);
            cell233 = new PdfPCell(ph);
            cell233.HorizontalAlignment = 0;
            cell233.Border = 0;
            cell233.Colspan = 3;
            tbl141.AddCell(cell233);
        }


        PdfPCell cell234 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("from the Sri Lanka Insurance Corporation as per detail shown below in full satisfaction", bodyFont2);
            ph.Add(chh3);
            cell234 = new PdfPCell(ph);
            cell234.HorizontalAlignment = 0;
            cell234.Border = 0;
            cell234.Colspan = 3;
            tbl141.AddCell(cell234);
        }

        PdfPCell cell235 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("and discharge of all my claims and demands under the above policy on the said Life which matured ", bodyFont2);
            ph.Add(chh3);
            cell235 = new PdfPCell(ph);
            cell235.HorizontalAlignment = 0;
            cell235.Border = 0;
            cell235.Colspan = 3;
            tbl141.AddCell(cell235);
        }

        PdfPCell cell236 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("on " + this.HiddenField1.Value + this.HiddenField2.Value + " and this policy is hereby delivered to the said Corporation to be cancelled.", bodyFont2);
            ph.Add(chh3);
            cell236 = new PdfPCell(ph);
            cell236.HorizontalAlignment = 0;
            cell236.Border = 0;
            cell236.Colspan = 3;
            tbl141.AddCell(cell236);
        }

        PdfPCell cell237 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell237 = new PdfPCell(ph);
            cell237.HorizontalAlignment = 0;
            cell237.Border = 0;
            cell237.Colspan = 3;
            tbl141.AddCell(cell237);
        }


        document.Add(tbl141);
        int[] clmwidths1112 = { 50, 25, 25 };
        PdfPTable tbl142 = new PdfPTable(3);
        tbl142.SetWidths(clmwidths1112);

        tbl142.WidthPercentage = 100;
        tbl142.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl142.SpacingBefore = 12;
        tbl142.SpacingAfter = 0;
        tbl142.DefaultCell.Border = 0;

        cell = new PdfPCell(new Phrase("Sum Assured ", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("Rs." + litSumIns.Text, bodyFont2));

        cell = new PdfPCell(new Phrase("Vested Bonus for the period from " + this.litComDate.Text + " to " + this.HiddenField1.Value + this.HiddenField2.Value, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField9.Value, bodyFont2));

        cell = new PdfPCell(new Phrase("Interim Bonus", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField10.Value, bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField22.Value, bodyFont2));

        cell = new PdfPCell(new Phrase("Less", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Default Premiums", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField13.Value, bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Interest thereon", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField14.Value, bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Loan", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField11.Value, bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("No:" + this.HiddenField18.Value, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Date:" + this.HiddenField19.Value, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Interest on loan:", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField14.Value, bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Other deductions:", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField23.Value, bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Stamp Fee", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField17.Value, bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Total deductions", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField24.Value, bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Net amount payble", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("Rs." + this.HiddenField25.Value, bodyFont2));

        PdfPCell cell20029 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell20029 = new PdfPCell(ph);
            cell20029.HorizontalAlignment = 0;
            cell20029.Border = 0;
            cell20029.Colspan = 3;
            tbl142.AddCell(cell20029);
        }


        PdfPCell cell2002 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Dated at ...............................this.........................Day.........................20....", bodyFont2);
            ph.Add(chh3);
            cell2002 = new PdfPCell(ph);
            cell2002.HorizontalAlignment = 0;
            cell2002.Border = 0;
            cell2002.Colspan = 3;
            tbl142.AddCell(cell2002);
        }

        PdfPCell cell2003 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("signed/thumb impression affixed by the above mentioned party in my", bodyFont2);
            ph.Add(chh3);
            cell2003 = new PdfPCell(ph);
            cell2003.HorizontalAlignment = 0;
            cell2003.Border = 0;
            cell2003.Colspan = 3;
            tbl142.AddCell(cell2003);
        }

        PdfPCell cell2004 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("presence and certify that the contents were duly translated and explained by ", bodyFont2);
            ph.Add(chh3);
            cell2004 = new PdfPCell(ph);
            cell2004.HorizontalAlignment = 0;
            cell2004.Border = 0;
            cell2004.Colspan = 3;
            tbl142.AddCell(cell2004);
        }

        PdfPCell cell2005 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("me to the party who has been identified by me.", bodyFont2);
            ph.Add(chh3);
            cell2005 = new PdfPCell(ph);
            cell2005.HorizontalAlignment = 0;
            cell2005.Border = 0;
            cell2005.Colspan = 3;
            tbl142.AddCell(cell2005);
        }

        PdfPCell cell2006 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2006 = new PdfPCell(ph);
            cell2006.HorizontalAlignment = 0;
            cell2006.Border = 0;
            cell2006.Colspan = 3;
            tbl142.AddCell(cell2006);
        }

        PdfPCell cell2007 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("                                                                    ............................................................ ", bodyFont2);
            ph.Add(chh3);
            cell2007 = new PdfPCell(ph);
            cell2007.HorizontalAlignment = 0;
            cell2007.Border = 0;
            cell2007.Colspan = 3;
            tbl142.AddCell(cell2007);
        }

        PdfPCell cell2008 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("                                                                   Usual signature of Life Assureds/Assignee ", bodyFont2);
            ph.Add(chh3);
            cell2008 = new PdfPCell(ph);
            cell2008.HorizontalAlignment = 0;
            cell2008.Border = 0;
            cell2008.Colspan = 3;
            tbl142.AddCell(cell2008);
        }

        PdfPCell cell2009 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2009 = new PdfPCell(ph);
            cell2009.HorizontalAlignment = 0;
            cell2009.Border = 0;
            cell2009.Colspan = 3;
            tbl142.AddCell(cell2009);
        }

        cell = new PdfPCell(new Phrase("Signature of witness", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":.................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Name of witness", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":.................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Designation/Occupation", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":.................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Address ", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":.................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":.................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase(":.................................", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl142.AddCell(cell);
        tbl142.AddCell(new Phrase("", bodyFont2));
        tbl142.AddCell(new Phrase("", bodyFont2));

        PdfPCell cell2016 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Notes :- (1) This Signature of assured/s assignee should be witnessed by a person who could read", bodyFont2);
            ph.Add(chh3);
            cell2016 = new PdfPCell(ph);
            cell2016.HorizontalAlignment = 0;
            cell2016.Border = 0;
            cell2016.Colspan = 3;
            tbl142.AddCell(cell2016);
        }

        PdfPCell cell2017 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("               and write in English.", bodyFont2);
            ph.Add(chh3);
            cell2017 = new PdfPCell(ph);
            cell2017.HorizontalAlignment = 0;
            cell2017.Border = 0;
            cell2017.Colspan = 3;
            tbl142.AddCell(cell2017);
        }

        PdfPCell cell2018 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Notes :- (2) If the assured/s assignee is illiterate, his or her thumb impression should be certified", bodyFont2);
            ph.Add(chh3);
            cell2018 = new PdfPCell(ph);
            cell2018.HorizontalAlignment = 0;
            cell2018.Border = 0;
            cell2018.Colspan = 3;
            tbl142.AddCell(cell2018);
        }
        PdfPCell cell2019 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("              by a Justice  of Peace or an Attorney at law or a Commissioner of the Oaths.", bodyFont2);
            ph.Add(chh3);
            cell2019 = new PdfPCell(ph);
            cell2019.HorizontalAlignment = 0;
            cell2019.Border = 0;
            cell2019.Colspan = 3;
            tbl142.AddCell(cell2019);
        }

        PdfPCell cell2020 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Notes :- (3) Stamp Fee has been deducted from above Claim.", bodyFont2);
            ph.Add(chh3);
            cell2020 = new PdfPCell(ph);
            cell2020.HorizontalAlignment = 0;
            cell2020.Border = 0;
            cell2020.Colspan = 3;
            tbl142.AddCell(cell2020);
        }



        document.Add(tbl142);
        document.NewPage();
        document.Add(new Paragraph("\n\n"));
        int[] clmwidths1122 = { 100 };
        PdfPTable tbl143 = new PdfPTable(1);

        tbl143.WidthPercentage = 100;
        tbl143.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl143.SpacingBefore = 12;
        tbl143.SpacingAfter = 0;
        tbl143.DefaultCell.Border = 0;

        PdfPCell cell2080 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("IMPORTANT NOTICE", bodyFont2);
            ph.Add(chh3);
            cell2080 = new PdfPCell(ph);
            cell2080.HorizontalAlignment = 1;
            cell2080.Border = 0;
            cell2080.Colspan = 3;
            tbl143.AddCell(cell2080);
        }

        PdfPCell cell2081 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2081 = new PdfPCell(ph);
            cell2081.HorizontalAlignment = 0;
            cell2081.Border = 0;
            cell2081.Colspan = 3;
            tbl143.AddCell(cell2081);
        }

        PdfPCell cell2082 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("The Maturity payment will be sent / credited to you or your account on receipt of the completed", bodyFont2);
            ph.Add(chh3);
            cell2082 = new PdfPCell(ph);
            cell2082.HorizontalAlignment = 0;
            cell2082.Border = 0;
            cell2082.Colspan = 3;
            tbl143.AddCell(cell2082);
        }

        PdfPCell cell2083 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Discharge Receipt. If you have a Bank Account, please let us know, by completing letter No.1", bodyFont2);
            ph.Add(chh3);
            cell2083 = new PdfPCell(ph);
            cell2083.HorizontalAlignment = 0;
            cell2083.Border = 0;
            cell2083.Colspan = 3;
            tbl143.AddCell(cell2083);
        }

        PdfPCell cell2084 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("given below, in order to forward the payment directly to your Bank to avoid any third party", bodyFont2);
            ph.Add(chh3);
            cell2084 = new PdfPCell(ph);
            cell2084.HorizontalAlignment = 0;
            cell2084.Border = 0;
            cell2084.Colspan = 3;
            tbl143.AddCell(cell2084);
        }

        PdfPCell cell2085 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("encashing the cheque. If you do not have a bank account, we would advise you to open a Savings", bodyFont2);
            ph.Add(chh3);
            cell2085 = new PdfPCell(ph);
            cell2085.HorizontalAlignment = 0;
            cell2085.Border = 0;
            cell2085.Colspan = 3;
            tbl143.AddCell(cell2085);
        }

        PdfPCell cell2086 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Account and provide the said details to us for the safe receipt of your money.", bodyFont2);
            ph.Add(chh3);
            cell2086 = new PdfPCell(ph);
            cell2086.HorizontalAlignment = 0;
            cell2086.Border = 0;
            cell2086.Colspan = 3;
            tbl143.AddCell(cell2086);
        }

        PdfPCell cell2087 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2087 = new PdfPCell(ph);
            cell2087.HorizontalAlignment = 0;
            cell2087.Border = 0;
            cell2087.Colspan = 3;
            tbl143.AddCell(cell2087);
        }
        PdfPCell cell1189 = new PdfPCell();

        {
            cell1189.Colspan = 3;
            cell1189.HorizontalAlignment = 0;
            cell1189.Border = 0;
            cell1189.BorderWidthBottom = 0;
            cell1189.BorderWidthTop = 0;
            cell1189.BorderWidthLeft = 0;
            cell1189.BorderWidthRight = 0;
            tbl143.AddCell(cell1189);
        }

        int[] clmwidths1124 = { 60, 40 };
        PdfPTable tbl1413 = new PdfPTable(2);
        tbl1413.SetWidths(clmwidths1124);


        tbl1413.WidthPercentage = 100;
        // tbl1413.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl1413.SpacingBefore = 0;
        tbl1413.SpacingAfter = 0;
        tbl1413.DefaultCell.Border = 0;

        PdfPCell cell2088 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2088 = new PdfPCell(ph);
            cell2088.HorizontalAlignment = 0;
            cell2088.Border = 1;
            cell2088.Colspan = 3;
            tbl1413.AddCell(cell2088);
        }

        cell = new PdfPCell(new Phrase("Letter No. 1 Ref No. :" + this.HiddenField3.Value, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("Please indicate your mobile phone", bodyFont2));

        cell = new PdfPCell(new Phrase("Life/Regional Manager", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("number in order to inform you via", bodyFont2));


        cell = new PdfPCell(new Phrase("Sri Lanka Insurance Corporation Ltd.", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("SMS once the payment is send to ", bodyFont2));

        cell = new PdfPCell(new Phrase("Colombo 2", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("your account if payment could be", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("directly credited to your bank account", bodyFont2));

        cell = new PdfPCell(new Phrase("Policy No :" + this.litPolNumber.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Name : " + HiddenField26.Value + this.litName.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl1413.AddCell(cell);
        tbl1413.AddCell(new Phrase("", bodyFont2));

        PdfPCell cell20914 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("                                                                        ", bodyFont2);
            ph.Add(chh3);
            cell20914 = new PdfPCell(ph);
            cell20914.HorizontalAlignment = 0;
            cell20914.Border = 1;
            cell20914.Colspan = 3;
            tbl1413.AddCell(cell20914);
        }

        PdfPCell cell2094 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Please credit my payment to the following bank account or post the cheque to the address given bellow.", bodyFont2);
            ph.Add(chh3);
            cell2094 = new PdfPCell(ph);
            cell2094.HorizontalAlignment = 0;
            cell2094.Border = 0;
            cell2094.Colspan = 3;
            tbl1413.AddCell(cell2094);
        }

        PdfPCell cell2095 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Particulars of the Bank Account", bodyFont2);
            ph.Add(chh3);
            cell2095 = new PdfPCell(ph);
            cell2095.HorizontalAlignment = 0;
            cell2095.Border = 0;
            cell2095.Colspan = 3;
            tbl1413.AddCell(cell2095);
        }

        PdfPCell cell2096 = new PdfPCell(signature);
        {
            cell2096.Colspan = 2;
            cell2096.HorizontalAlignment = 0;
            cell2096.Border = 0;
            cell2096.BorderWidthBottom = 0;
            cell2096.BorderWidthTop = 0;
            cell2096.BorderWidthLeft = 0;
            cell2096.BorderWidthRight = 0;
            tbl1413.AddCell(cell2096);
        }


        document.Add(tbl143);
        document.Add(tbl1413);
        document.Close();
        Response.Buffer = false;
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Covering_English.pdf"));
        Response.BinaryWrite(output.ToArray());

    }



    protected void Button12_Click(object sender, EventArgs e)
    {
        Document document = new Document(PageSize.A4, 50, 50, 25, 25);
        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        document.Open();
        Font titleFont1 = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font boldTableFont = FontFactory.GetFont("Times New Roman", 11, Font.BOLD);
        Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 11, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont4_bold = FontFactory.GetFont("Times New Roman", 7, Font.BOLD);
        Font bodyFont5 = FontFactory.GetFont("Times New Roman", 7, Font.NORMAL);
        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);
        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font bodyFont2_bold_und = FontFactory.GetFont("Times New Roman", 10, Font.BOLD | Font.UNDERLINE);

        iTextSharp.text.Image Capturess = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Capturess.jpg"));
        Capturess.ScalePercent(60f);
        Capturess.SetAbsolutePosition(70, 180);

        iTextSharp.text.Image Captures4 = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/Capture44.jpg"));
        Captures4.ScalePercent(60f);
        Captures4.SetAbsolutePosition(70, 180);

        string fontpath = System.Web.HttpContext.Current.Server.MapPath("~/Font/sa_____.TTF");
        var bf = BaseFont.CreateFont(fontpath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        var sinhalaFont = new Font(bf, 11, Font.NORMAL);

        document.Add(new Paragraph("\n\n"));
        int[] clmwidths111 = { 55, 45 };
        PdfPTable tbl14 = new PdfPTable(2);
        tbl14.SetWidths(clmwidths111);

        tbl14.WidthPercentage = 100;
        tbl14.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl14.SpacingBefore = 12;
        tbl14.SpacingAfter = 0;
        tbl14.DefaultCell.Border = 0;

        PdfPCell cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase(" ", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("LI/MA/FO/SE/07", bodyFont2));

        cell = new PdfPCell(new Phrase(HiddenField26.Value + litName.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase(" ", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr1.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase(" ", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr2.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr3.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr4.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));


        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("wfma fhduqj( cS$mQrAK;aj T w " + this.litPolNumber.Text, sinhalaFont));


        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("oq wxl  0112357200", sinhalaFont));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("oskh " + DateTime.Today.ToString("yyyy$MM$dd"), sinhalaFont));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("uy;auhdKks$uy;aushks", sinhalaFont));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("csjs; rCIK Tmamq wxl " + litPolNumber.Text, sinhalaFont));
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        PdfPCell cell2001 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2001 = new PdfPCell(ph);
            cell2001.HorizontalAlignment = 0;
            cell2001.Border = 0;
            cell2001.Colspan = 3;
            tbl14.AddCell(cell2001);
        }

        PdfPCell cell200 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("by; wxl orK " + this.HiddenField1.Value + this.HiddenField2.Value + " osk mqrAK;ajhg m;aj we;s Tnf.a csjs; rCIK Tmamqj wia:dk .;j ", sinhalaFont);
            ph.Add(chh3);
            cell200 = new PdfPCell(ph);
            cell200.HorizontalAlignment = 0;
            cell200.Border = 0;
            cell200.Colspan = 3;
            tbl14.AddCell(cell200);
        }
        PdfPCell cell201 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("we;s nj oekquz oS we;s nejska fuu rCIK Tmamqj hgf;a mqrAK;aj ysusluz ksrdlrKh lsrSu i|yd", sinhalaFont);
            ph.Add(chh3);
            cell201 = new PdfPCell(ph);
            cell201.HorizontalAlignment = 0;
            cell201.Border = 0;
            cell201.Colspan = 3;
            tbl14.AddCell(cell201);
        }

        PdfPCell cell229 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell229 = new PdfPCell(ph);
            cell229.HorizontalAlignment = 0;
            cell229.Border = 0;
            cell229.Colspan = 3;
            tbl14.AddCell(cell229);
        }

        PdfPCell cell230 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("my; wjYH;djhka fkdmudj iemsrSug lghq;= lrk fuka b,a,uq", sinhalaFont);
            ph.Add(chh3);
            cell230 = new PdfPCell(ph);
            cell230.HorizontalAlignment = 0;
            cell230.Border = 0;
            cell230.Colspan = 3;
            tbl14.AddCell(cell230);
        }

        PdfPCell cell231 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell231 = new PdfPCell(ph);
            cell231.HorizontalAlignment = 0;
            cell231.Border = 0;
            cell231.Colspan = 3;
            tbl14.AddCell(cell231);
        }

        PdfPCell cell232 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("01 osjqreuz m%ldYh ", sinhalaFont);
            ph.Add(chh3);
            cell232 = new PdfPCell(ph);
            cell232.HorizontalAlignment = 0;
            cell232.Border = 0;
            cell232.Colspan = 3;
            tbl14.AddCell(cell232);
        }

        PdfPCell cell233 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("fuz iuÛ wuqKd we;s Tmamqj ke;sjSu iuznkaO osjqreuz m%ldYh ksis mrsos iuzmQrAK lr", sinhalaFont);
            ph.Add(chh3);
            cell233 = new PdfPCell(ph);
            cell233.HorizontalAlignment = 0;
            cell233.Border = 0;
            cell233.Colspan = 3;
            tbl14.AddCell(cell233);
        }

        PdfPCell cell234 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("re 50 l uqoaor u; idudodk jsksYaphldrjrhl= bosrsmsg w;aika lr iy;sl lr", sinhalaFont);
            ph.Add(chh3);
            cell234 = new PdfPCell(ph);
            cell234.HorizontalAlignment = 0;
            cell234.Border = 0;
            cell234.Colspan = 3;
            tbl14.AddCell(cell234);
        }

        PdfPCell cell235 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("bosrsm;a lrkak", sinhalaFont);
            ph.Add(chh3);
            cell235 = new PdfPCell(ph);
            cell235.HorizontalAlignment = 0;
            cell235.Border = 0;
            cell235.Colspan = 3;
            tbl14.AddCell(cell235);
        }


        PdfPCell cell236 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell236 = new PdfPCell(ph);
            cell236.HorizontalAlignment = 0;
            cell236.Border = 0;
            cell236.Colspan = 3;
            tbl14.AddCell(cell236);
        }

        PdfPCell cell237 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("02 Wmamekak iy;slh", sinhalaFont);
            ph.Add(chh3);
            cell237 = new PdfPCell(ph);
            cell237.HorizontalAlignment = 0;
            cell237.Border = 0;
            cell237.Colspan = 3;
            tbl14.AddCell(cell237);
        }

        PdfPCell cell238 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("i;H msgm;la njg iy;sl lrk ,o Tnf.a Wmamekak iy;slfha Pdhd ", sinhalaFont);
            ph.Add(chh3);
            cell238 = new PdfPCell(ph);
            cell238.HorizontalAlignment = 0;
            cell238.Border = 0;
            cell238.Colspan = 3;
            tbl14.AddCell(cell238);
        }

        PdfPCell cell239 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("msgm;la ,ndoSug lghq;= lrkak", sinhalaFont);
            ph.Add(chh3);
            cell239 = new PdfPCell(ph);
            cell239.HorizontalAlignment = 0;
            cell239.Border = 0;
            cell239.Colspan = 3;
            tbl14.AddCell(cell239);
        }
        PdfPCell cell240 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell240 = new PdfPCell(ph);
            cell240.HorizontalAlignment = 0;
            cell240.Border = 0;
            cell240.Colspan = 3;
            tbl14.AddCell(cell240);
        }
        PdfPCell cell241 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("ie h", sinhalaFont);
            ph.Add(chh3);
            cell241 = new PdfPCell(ph);
            cell241.HorizontalAlignment = 0;
            cell241.Border = 0;
            cell241.Colspan = 3;
            tbl14.AddCell(cell241);
        }
        PdfPCell cell243 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("1 taldnoaO cSjs; rCIKhla kuz rCIs;hska fofokdu wod, ia:dkhkays iuzmqrAK ku i|yka lr", sinhalaFont);
            ph.Add(chh3);
            cell243 = new PdfPCell(ph);
            cell243.HorizontalAlignment = 0;
            cell243.Border = 0;
            cell243.Colspan = 3;
            tbl14.AddCell(cell243);
        }
        PdfPCell cell244 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("w;aika l< hq;= fj", sinhalaFont);
            ph.Add(chh3);
            cell244 = new PdfPCell(ph);
            cell244.HorizontalAlignment = 0;
            cell244.Border = 0;
            cell244.Colspan = 3;
            tbl14.AddCell(cell244);
        }
        PdfPCell cell245 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell245 = new PdfPCell(ph);
            cell245.HorizontalAlignment = 0;
            cell245.Border = 0;
            cell245.Colspan = 3;
            tbl14.AddCell(cell245);
        }

        PdfPCell cell246 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("2 <ud rCIKhlo  orejd jhi wjqreoq 18 iuzmqrAK jqfjla kuz Tyq  fyda weh jsiska fuu osjzreuz", sinhalaFont);
            ph.Add(chh3);
            cell246 = new PdfPCell(ph);
            cell246.HorizontalAlignment = 0;
            cell246.Border = 0;
            cell246.Colspan = 3;
            tbl14.AddCell(cell246);
        }

        PdfPCell cell247 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("m%ldYh ,ndoSh hq;=h ", sinhalaFont);
            ph.Add(chh3);
            cell247 = new PdfPCell(ph);
            cell247.HorizontalAlignment = 0;
            cell247.Border = 0;
            cell247.Colspan = 3;
            tbl14.AddCell(cell247);
        }

        PdfPCell cell248 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell248 = new PdfPCell(ph);
            cell248.HorizontalAlignment = 0;
            cell248.Border = 0;
            cell248.Colspan = 3;
            tbl14.AddCell(cell248);
        }

        PdfPCell cell249 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("iuzmqrAK lrk ,o by; wjYH;djhka wmf.a m%Odk ldrAhd,hg fyda Tng ,Ûu msysgs YdLd", sinhalaFont);
            ph.Add(chh3);
            cell249 = new PdfPCell(ph);
            cell249.HorizontalAlignment = 0;
            cell249.Border = 0;
            cell249.Colspan = 3;
            tbl14.AddCell(cell249);
        }

        PdfPCell cell250 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("ldrAhd,hg ,ndoSug lghq;= lrk fuka okajuq", sinhalaFont);
            ph.Add(chh3);
            cell250 = new PdfPCell(ph);
            cell250.HorizontalAlignment = 0;
            cell250.Border = 0;
            cell250.Colspan = 3;
            tbl14.AddCell(cell250);
        }

        PdfPCell cell251 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell251 = new PdfPCell(ph);
            cell251.HorizontalAlignment = 0;
            cell251.Border = 0;
            cell251.Colspan = 3;
            tbl14.AddCell(cell251);
        }

        PdfPCell cell252 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("fuz iuznkaOj Tn ,ndfok iyfhda.h wm b;d w.h fldg i,luq", sinhalaFont);
            ph.Add(chh3);
            cell252 = new PdfPCell(ph);
            cell252.HorizontalAlignment = 0;
            cell252.Border = 0;
            cell252.Colspan = 3;
            tbl14.AddCell(cell252);
        }

        PdfPCell cell253 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell253 = new PdfPCell(ph);
            cell253.HorizontalAlignment = 0;
            cell253.Border = 0;
            cell253.Colspan = 3;
            tbl14.AddCell(cell253);
        }

        PdfPCell cell254 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("ia;+;shs ", sinhalaFont);
            ph.Add(chh3);
            cell254 = new PdfPCell(ph);
            cell254.HorizontalAlignment = 0;
            cell254.Border = 0;
            cell254.Colspan = 3;
            tbl14.AddCell(cell254);
        }

        PdfPCell cell255 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("fuhg jsYajdi", sinhalaFont);
            ph.Add(chh3);
            cell255 = new PdfPCell(ph);
            cell255.HorizontalAlignment = 0;
            cell255.Border = 0;
            cell255.Colspan = 3;
            tbl14.AddCell(cell255);
        }

        PdfPCell cell256 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell256 = new PdfPCell(ph);
            cell256.HorizontalAlignment = 0;
            cell256.Border = 0;
            cell256.Colspan = 3;
            tbl14.AddCell(cell256);
        }


        PdfPCell cell257 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("YS% ,xld rlaIK ixia:dj", sinhalaFont);
            ph.Add(chh3);
            cell257 = new PdfPCell(ph);
            cell257.HorizontalAlignment = 0;
            cell257.Border = 0;
            cell257.Colspan = 3;
            tbl14.AddCell(cell257);
        }

        PdfPCell cell258 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("oskh " + DateTime.Today.ToString("yyyy$MM$dd"), sinhalaFont);
            ph.Add(chh3);
            cell258 = new PdfPCell(ph);
            cell258.HorizontalAlignment = 0;
            cell258.Border = 0;
            cell258.Colspan = 3;
            tbl14.AddCell(cell258);
        }

        document.Add(tbl14);
        document.NewPage();
        document.Add(new Paragraph("\n\n"));


        int[] clmwidths1115 = { 90, 30 };
        PdfPTable tbl141 = new PdfPTable(2);
        tbl141.SetWidths(clmwidths111);

        tbl141.WidthPercentage = 100;
        tbl141.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl141.SpacingBefore = 10;
        tbl141.SpacingAfter = 0;
        tbl141.DefaultCell.Border = 0;

        PdfPCell cell227 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("osjqreuz m%ldYh", sinhalaFont);
            ph.Add(chh3);
            cell227 = new PdfPCell(ph);
            cell227.HorizontalAlignment = 1;
            cell227.Border = 0;
            cell227.Colspan = 3;
            tbl141.AddCell(cell227);
        }
        PdfPCell cell2271 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("^ Tmamq ke;sjSu &", sinhalaFont);
            ph.Add(chh3);
            cell2271 = new PdfPCell(ph);
            cell2271.HorizontalAlignment = 1;
            cell2271.Border = 0;
            cell2271.Colspan = 3;
            tbl141.AddCell(cell2271);
        }

        PdfPCell cell2272 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell2272 = new PdfPCell(ph);
            cell2272.HorizontalAlignment = 1;
            cell2272.Border = 0;
            cell2272.Colspan = 3;
            tbl141.AddCell(cell2272);
        }
    
        PdfPCell cell2096 = new PdfPCell(Capturess);
        {
            cell2096.Colspan = 1;
            cell2096.HorizontalAlignment = 0;
            cell2096.Border = 0;
            cell2096.BorderWidthBottom = 0;
            cell2096.BorderWidthTop = 0;
            cell2096.BorderWidthLeft = 0;
            cell2096.BorderWidthRight = 0;
            tbl141.AddCell(cell2096);
        }


        PdfPCell cell22941 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell22941 = new PdfPCell(ph);
            cell22941.HorizontalAlignment = 0;
            cell22941.Border = 0;
            cell22941.Colspan = 3;
            tbl141.AddCell(cell22941);
        }

        PdfPCell cell2295 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("1 YS% ,xld rlaIK ixia:dj jsiska ksl+;a lr we;s wxl  " + this.litPolNumber.Text + " orK Tmamqfj ysuslre", sinhalaFont);
            ph.Add(chh3);
            cell2295 = new PdfPCell(ph);
            cell2295.HorizontalAlignment = 0;
            cell2295.Border = 0;
            cell2295.Colspan = 3;
            tbl141.AddCell(cell2295);
        }

        PdfPCell cell2296 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("   jk uu by; isoaOs m%ldYl fju", sinhalaFont);
            ph.Add(chh3);
            cell2296 = new PdfPCell(ph);
            cell2296.HorizontalAlignment = 0;
            cell2296.Border = 0;
            cell2296.Colspan = 3;
            tbl141.AddCell(cell2296);
        }

        PdfPCell cell22961 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("  ", bodyFont2);
            ph.Add(chh3);
            cell22961 = new PdfPCell(ph);
            cell22961.HorizontalAlignment = 0;
            cell22961.Border = 0;
            cell22961.Colspan = 3;
            tbl141.AddCell(cell22961);
        }

        PdfPCell cell2297 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("2 fuu cSjs; rCIK Tmamq wxl " + this.litPolNumber.Text + " orK Tmamqj ke;sjS we;", sinhalaFont);
            ph.Add(chh3);
            cell2297 = new PdfPCell(ph);
            cell2297.HorizontalAlignment = 0;
            cell2297.Border = 0;
            cell2297.Colspan = 3;
            tbl141.AddCell(cell2297);
        }

        PdfPCell cell22971 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell22971 = new PdfPCell(ph);
            cell22971.HorizontalAlignment = 0;
            cell22971.Border = 0;
            cell22971.Colspan = 3;
            tbl141.AddCell(cell22971);
        }

        PdfPCell cell2298 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("3 th fidhd .ekSu i|yd uu jsiska Wmrsu W;aidy .;a;o lsisu whqrlska fuu Tmamqj", sinhalaFont);
            ph.Add(chh3);
            cell2298 = new PdfPCell(ph);
            cell2298.HorizontalAlignment = 0;
            cell2298.Border = 0;
            cell2298.Colspan = 3;
            tbl141.AddCell(cell2298);
        }

        PdfPCell cell2299 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("   iuznkaOfhka lsisjla fidhd.; fkdyels jS we;", sinhalaFont);
            ph.Add(chh3);
            cell2299 = new PdfPCell(ph);
            cell2299.HorizontalAlignment = 0;
            cell2299.Border = 0;
            cell2299.Colspan = 3;
            tbl141.AddCell(cell2299);
        }

        PdfPCell cell22991 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell22991 = new PdfPCell(ph);
            cell22991.HorizontalAlignment = 0;
            cell22991.Border = 0;
            cell22991.Colspan = 3;
            tbl141.AddCell(cell22991);
        }

        PdfPCell cell2300 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("4  fuu Tmamqj ke;sjSug fyda jskdYjSug mQrAjfhka fuu Tmamqj mejrSula, WlialsrSula fyda", sinhalaFont);
            ph.Add(chh3);
            cell2300 = new PdfPCell(ph);
            cell2300.HorizontalAlignment = 0;
            cell2300.Border = 0;
            cell2300.Colspan = 3;
            tbl141.AddCell(cell2300);
        }

        PdfPCell cell2301 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("   Tmamqj fjkqfjka fjk;a .Kqfokqjla fyda tjeks jrA.fha fjk;a YS% ,xld rCIK", sinhalaFont);
            ph.Add(chh3);
            cell2301 = new PdfPCell(ph);
            cell2301.HorizontalAlignment = 0;
            cell2301.Border = 0;
            cell2301.Colspan = 3;
            tbl141.AddCell(cell2301);
        }

        PdfPCell cell2302 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("   ixia:djg wkrA:ldrs jk wdldrfha  lsisª ls%hdjlg fhdojd fkdue;", sinhalaFont);
            ph.Add(chh3);
            cell2302 = new PdfPCell(ph);
            cell2302.HorizontalAlignment = 0;
            cell2302.Border = 0;
            cell2302.Colspan = 3;
            tbl141.AddCell(cell2302);
        }

        PdfPCell cell2303 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell2303 = new PdfPCell(ph);
            cell2303.HorizontalAlignment = 0;
            cell2303.Border = 0;
            cell2303.Colspan = 3;
            tbl141.AddCell(cell2303);
        }

        PdfPCell cell2304 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell2304 = new PdfPCell(ph);
            cell2304.HorizontalAlignment = 0;
            cell2304.Border = 0;
            cell2304.Colspan = 3;
            tbl141.AddCell(cell2304);
        }

        PdfPCell cell2305 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell2305 = new PdfPCell(ph);
            cell2305.HorizontalAlignment = 0;
            cell2305.Border = 0;
            cell2305.Colspan = 3;
            tbl141.AddCell(cell2305);
        }

        PdfPCell cell23051 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell23051 = new PdfPCell(ph);
            cell23051.HorizontalAlignment = 0;
            cell23051.Border = 0;
            cell23051.Colspan = 3;
            tbl141.AddCell(cell23051);
        }

        document.Add(tbl141);

        document.Add(new Paragraph("\n", bodyFont));
        //Rectangle rect = new Rectangle(480, 500, 360, 440);
        Rectangle rect = new Rectangle(480, 400, 350, 440);
        rect.Border = Rectangle.BOX;
        rect.BorderWidth = 0.5f;
        //rect.BorderColor = new BaseColor(0x00, 0x00, 0x00);
        document.Add(rect);
        document.Add(new Paragraph("                                                                     uQoaorh re 50", sinhalaFont));
        document.Add(new Paragraph("                                                                     osjzreuz m%ldYl w;aik", sinhalaFont));
        document.Add(new Paragraph("                                                                     idu jsksiqre bosrsmsgoS", sinhalaFont));
        document.Add(new Paragraph("\n", bodyFont));

        //document.Add(new Paragraph("fmr i|yka l< fuys ku i|yka m%ldYlg fuys wka;rA.;h ud jsiska  *isxy,$*fou<$ fyda", sinhalaFont));
        //document.Add(new Paragraph("m%ldYlrkakdf.a NdIdfjka lshjd i; H f, i f; areuzlr oqka w; r weh$Tyq jsiska fuu lreK", sinhalaFont));
        //document.Add(new Paragraph("f;areuz.ekSfuka wk;+rej jrAI..............jq.......... ui......osk............  os ud bosrsmsgoS", sinhalaFont));
        //document.Add(new Paragraph("*w;aik/ *weÛs,s i,l=K ;nk ,o", sinhalaFont));

        int[] clmwidths1121 = { 95, 5 };
        PdfPTable tbl145 = new PdfPTable(2);
        tbl145.SetWidths(clmwidths1121);

        tbl145.WidthPercentage = 100;
        tbl145.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl145.SpacingBefore = 12;
        tbl145.SpacingAfter = 0;
        tbl145.DefaultCell.Border = 0;

        cell = new PdfPCell(Captures4);
        cell.HorizontalAlignment = 0;
        cell.Colspan = 1;
        cell.Border = 0;
        tbl145.AddCell(cell);
        tbl145.AddCell(new Phrase("", bodyFont2));

        //PdfPCell cell20961 = new PdfPCell(Captures4);
        //{
        //    cell20961.Colspan = 1;
        //    cell20961.HorizontalAlignment = 0;
        //    cell20961.Border = 0;
        //    cell20961.BorderWidthBottom = 0;
        //    cell20961.BorderWidthTop = 0;
        //    cell20961.BorderWidthLeft = 0;
        //    cell20961.BorderWidthRight = 0;
        //    tbl145.AddCell(cell20961);
        //}

        document.Add(tbl145);

        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("                                                                                           .........................................", bodyFont));
        document.Add(new Paragraph("                                                        iduodk jsksYaphlref.a w;aik iy ks, uq%dj", sinhalaFont));
        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("ie hq", sinhalaFont));
        document.Add(new Paragraph("1  fuu m%ldYKh m%ldY lrkakd jsiska iajNdYdfjka f;areuzf.k w;aika lr iy;sl l< hq;=h", sinhalaFont));
        document.Add(new Paragraph("2  fuys m%ldYl re  50  l uqoaorhla u; w;aika ;ensh hq;+h", sinhalaFont));
        document.Add(new Paragraph("3  wod, fkdjk jpk lmd yrskak", sinhalaFont));
        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("oskh " + DateTime.Today.ToString("yyyy$MM$dd"), sinhalaFont));
        document.Close();
        Response.Buffer = false;
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Affidavit_Sinhala.pdf"));
        Response.BinaryWrite(output.ToArray());
    }

    protected void Button13_Click(object sender, EventArgs e)
    {
        Document document = new Document(PageSize.A4, 50, 50, 25, 25);
        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        document.Open();
        Font titleFont1 = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font boldTableFont = FontFactory.GetFont("Times New Roman", 11, Font.BOLD);
        Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 11, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont4_bold = FontFactory.GetFont("Times New Roman", 7, Font.BOLD);
        Font bodyFont5 = FontFactory.GetFont("Times New Roman", 7, Font.NORMAL);
        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);
        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font bodyFont2_bold_und = FontFactory.GetFont("Times New Roman", 10, Font.BOLD | Font.UNDERLINE);

        iTextSharp.text.Image signature = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/sq1.jpg"));
        signature.ScalePercent(60f);
        signature.SetAbsolutePosition(70, 170);

        document.Add(new Paragraph("\n\n"));
        int[] clmwidths111 = { 60, 40 };
        PdfPTable tbl14 = new PdfPTable(2);
        tbl14.SetWidths(clmwidths111);

        tbl14.WidthPercentage = 100;
        tbl14.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl14.SpacingBefore = 12;
        tbl14.SpacingAfter = 0;
        tbl14.DefaultCell.Border = 0;

        PdfPCell cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase(" ", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("LI/MA/FO/SE/07", bodyFont2));

        cell = new PdfPCell(new Phrase(HiddenField26.Value + litName.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase(" ", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr1.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase(" ", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr2.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr3.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase(litAddr4.Text, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));


        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("Our Ref: L/Matu./P.No." + this.litPolNumber.Text, bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("Life Maturity Section ", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("Tel.No.: 011/2357200", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("Date : " + DateTime.Today.ToString("yyyy/MM/dd"), bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Dear Sir/Madam,", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        cell = new PdfPCell(new Phrase("Life Insurance Policy :" + litPolNumber.Text, bodyFont2));
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.Border = 0;
        tbl14.AddCell(cell);
        tbl14.AddCell(new Phrase("", bodyFont2));

        PdfPCell cell2001 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell2001 = new PdfPCell(ph);
            cell2001.HorizontalAlignment = 0;
            cell2001.Border = 0;
            cell2001.Colspan = 3;
            tbl14.AddCell(cell2001);
        }

        PdfPCell cell200 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("We write with reference to the maturity claim of above life insurance policy of you which was ", bodyFont2);
            ph.Add(chh3);
            cell200 = new PdfPCell(ph);
            cell200.HorizontalAlignment = 0;
            cell200.Border = 0;
            cell200.Colspan = 3;
            tbl14.AddCell(cell200);
        }
        PdfPCell cell201 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("matured on " + this.HiddenField1.Value + this.HiddenField2.Value + ". ", bodyFont2);
            ph.Add(chh3);
            cell201 = new PdfPCell(ph);
            cell201.HorizontalAlignment = 0;
            cell201.Border = 0;
            cell201.Colspan = 3;
            tbl14.AddCell(cell201);
        }

        PdfPCell cell229 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell229 = new PdfPCell(ph);
            cell229.HorizontalAlignment = 0;
            cell229.Border = 0;
            cell229.Colspan = 3;
            tbl14.AddCell(cell229);
        }

        PdfPCell cell230 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("In order to settle the maturity claim of the policy, please be good enough to submit the under ", bodyFont2);
            ph.Add(chh3);
            cell230 = new PdfPCell(ph);
            cell230.HorizontalAlignment = 0;
            cell230.Border = 0;
            cell230.Colspan = 3;
            tbl14.AddCell(cell230);
        }

        PdfPCell cell231 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("mentioned particular requirements, since you have informed us that your original life ", bodyFont2);
            ph.Add(chh3);
            cell231 = new PdfPCell(ph);
            cell231.HorizontalAlignment = 0;
            cell231.Border = 0;
            cell231.Colspan = 3;
            tbl14.AddCell(cell231);
        }

        PdfPCell cell232 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("insurance policy had been misplaced.", bodyFont2);
            ph.Add(chh3);
            cell232 = new PdfPCell(ph);
            cell232.HorizontalAlignment = 0;
            cell232.Border = 0;
            cell232.Colspan = 3;
            tbl14.AddCell(cell232);
        }

        PdfPCell cell233 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell233 = new PdfPCell(ph);
            cell233.HorizontalAlignment = 0;
            cell233.Border = 0;
            cell233.Colspan = 3;
            tbl14.AddCell(cell233);
        }

        PdfPCell cell234 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("01 Affidavit", bodyFont2);
            ph.Add(chh3);
            cell234 = new PdfPCell(ph);
            cell234.HorizontalAlignment = 0;
            cell234.Border = 0;
            cell234.Colspan = 3;
            tbl14.AddCell(cell234);
        }

        PdfPCell cell235 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Please return the attached affidavit for lost policy with the certification of Justice of ", bodyFont2);
            ph.Add(chh3);
            cell235 = new PdfPCell(ph);
            cell235.HorizontalAlignment = 0;
            cell235.Border = 0;
            cell235.Colspan = 3;
            tbl14.AddCell(cell235);
        }


        PdfPCell cell236 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("the Peace along with your signature on Rs.50 /-stamp.", bodyFont2);
            ph.Add(chh3);
            cell236 = new PdfPCell(ph);
            cell236.HorizontalAlignment = 0;
            cell236.Border = 0;
            cell236.Colspan = 3;
            tbl14.AddCell(cell236);
        }

        PdfPCell cell237 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell237 = new PdfPCell(ph);
            cell237.HorizontalAlignment = 0;
            cell237.Border = 0;
            cell237.Colspan = 3;
            tbl14.AddCell(cell237);
        }

        PdfPCell cell238 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("02 Birth Certificate", bodyFont2);
            ph.Add(chh3);
            cell238 = new PdfPCell(ph);
            cell238.HorizontalAlignment = 0;
            cell238.Border = 0;
            cell238.Colspan = 3;
            tbl14.AddCell(cell238);
        }

        PdfPCell cell239 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Please submit a certified copy of your Certificate of Birth.", bodyFont2);
            ph.Add(chh3);
            cell239 = new PdfPCell(ph);
            cell239.HorizontalAlignment = 0;
            cell239.Border = 0;
            cell239.Colspan = 3;
            tbl14.AddCell(cell239);
        }
        PdfPCell cell240 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell240 = new PdfPCell(ph);
            cell240.HorizontalAlignment = 0;
            cell240.Border = 0;
            cell240.Colspan = 3;
            tbl14.AddCell(cell240);
        }
        PdfPCell cell241 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Note –", bodyFont2);
            ph.Add(chh3);
            cell241 = new PdfPCell(ph);
            cell241.HorizontalAlignment = 0;
            cell241.Border = 0;
            cell241.Colspan = 3;
            tbl14.AddCell(cell241);
        }
        PdfPCell cell243 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("1. If the policy is a joint life policy, both life assured should placed their signatures and state their", bodyFont2);
            ph.Add(chh3);
            cell243 = new PdfPCell(ph);
            cell243.HorizontalAlignment = 0;
            cell243.Border = 0;
            cell243.Colspan = 3;
            tbl14.AddCell(cell243);
        }
        PdfPCell cell244 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("full names on the respective places of the affidavit.", bodyFont2);
            ph.Add(chh3);
            cell244 = new PdfPCell(ph);
            cell244.HorizontalAlignment = 0;
            cell244.Border = 0;
            cell244.Colspan = 3;
            tbl14.AddCell(cell244);
        }
        PdfPCell cell245 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell245 = new PdfPCell(ph);
            cell245.HorizontalAlignment = 0;
            cell245.Border = 0;
            cell245.Colspan = 3;
            tbl14.AddCell(cell245);
        }

        PdfPCell cell246 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("2. If the policy is a child policy and if the child has attained 18 years of age, the affidavit should ", bodyFont2);
            ph.Add(chh3);
            cell246 = new PdfPCell(ph);
            cell246.HorizontalAlignment = 0;
            cell246.Border = 0;
            cell246.Colspan = 3;
            tbl14.AddCell(cell246);
        }

        PdfPCell cell247 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("be completed by the child.", bodyFont2);
            ph.Add(chh3);
            cell247 = new PdfPCell(ph);
            cell247.HorizontalAlignment = 0;
            cell247.Border = 0;
            cell247.Colspan = 3;
            tbl14.AddCell(cell247);
        }

        PdfPCell cell248 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell248 = new PdfPCell(ph);
            cell248.HorizontalAlignment = 0;
            cell248.Border = 0;
            cell248.Colspan = 3;
            tbl14.AddCell(cell248);
        }

        PdfPCell cell249 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Please handover or post the completed requirements to our Head office or handover it to our ", bodyFont2);
            ph.Add(chh3);
            cell249 = new PdfPCell(ph);
            cell249.HorizontalAlignment = 0;
            cell249.Border = 0;
            cell249.Colspan = 3;
            tbl14.AddCell(cell249);
        }

        PdfPCell cell250 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("nearest branch office.", bodyFont2);
            ph.Add(chh3);
            cell250 = new PdfPCell(ph);
            cell250.HorizontalAlignment = 0;
            cell250.Border = 0;
            cell250.Colspan = 3;
            tbl14.AddCell(cell250);
        }

        PdfPCell cell251 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell251 = new PdfPCell(ph);
            cell251.HorizontalAlignment = 0;
            cell251.Border = 0;
            cell251.Colspan = 3;
            tbl14.AddCell(cell251);
        }

        PdfPCell cell252 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Your kind co-operation in this regard is highly appreciated.", bodyFont2);
            ph.Add(chh3);
            cell252 = new PdfPCell(ph);
            cell252.HorizontalAlignment = 0;
            cell252.Border = 0;
            cell252.Colspan = 3;
            tbl14.AddCell(cell252);
        }

        PdfPCell cell253 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell253 = new PdfPCell(ph);
            cell253.HorizontalAlignment = 0;
            cell253.Border = 0;
            cell253.Colspan = 3;
            tbl14.AddCell(cell253);
        }

        PdfPCell cell254 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Thanking you", bodyFont2);
            ph.Add(chh3);
            cell254 = new PdfPCell(ph);
            cell254.HorizontalAlignment = 0;
            cell254.Border = 0;
            cell254.Colspan = 3;
            tbl14.AddCell(cell254);
        }

        PdfPCell cell255 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Yours faithfully", bodyFont2);
            ph.Add(chh3);
            cell255 = new PdfPCell(ph);
            cell255.HorizontalAlignment = 0;
            cell255.Border = 0;
            cell255.Colspan = 3;
            tbl14.AddCell(cell255);
        }

        PdfPCell cell256 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("", bodyFont2);
            ph.Add(chh3);
            cell256 = new PdfPCell(ph);
            cell256.HorizontalAlignment = 0;
            cell256.Border = 0;
            cell256.Colspan = 3;
            tbl14.AddCell(cell256);
        }

        PdfPCell cell257 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Sri Lanka Insurance Corporation", bodyFont2);
            ph.Add(chh3);
            cell257 = new PdfPCell(ph);
            cell257.HorizontalAlignment = 0;
            cell257.Border = 0;
            cell257.Colspan = 3;
            tbl14.AddCell(cell257);
        }

        PdfPCell cell258 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("Date : " + DateTime.Today.ToString("yyyy/MM/dd"), bodyFont2);
            ph.Add(chh3);
            cell258 = new PdfPCell(ph);
            cell258.HorizontalAlignment = 0;
            cell258.Border = 0;
            cell258.Colspan = 3;
            tbl14.AddCell(cell258);
        }

        document.Add(tbl14);
        document.NewPage();
        document.Add(new Paragraph("\n\n"));


        int[] clmwidths1115 = { 90, 30 };
        PdfPTable tbl141 = new PdfPTable(2);
        tbl141.SetWidths(clmwidths111);

        tbl141.WidthPercentage = 100;
        tbl141.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl141.SpacingBefore = 10;
        tbl141.SpacingAfter = 0;
        tbl141.DefaultCell.Border = 0;

        PdfPCell cell227 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("AFFIDAVIT", bodyFont2);
            ph.Add(chh3);
            cell227 = new PdfPCell(ph);
            cell227.HorizontalAlignment = 1;
            cell227.Border = 0;
            cell227.Colspan = 3;
            tbl141.AddCell(cell227);
        }
        PdfPCell cell2271 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("(Loss of Policy by unknown nature)", bodyFont2);
            ph.Add(chh3);
            cell2271 = new PdfPCell(ph);
            cell2271.HorizontalAlignment = 1;
            cell2271.Border = 0;
            cell2271.Colspan = 3;
            tbl141.AddCell(cell2271);
        }

        PdfPCell cell2272 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell2272 = new PdfPCell(ph);
            cell2272.HorizontalAlignment = 1;
            cell2272.Border = 0;
            cell2272.Colspan = 3;
            tbl141.AddCell(cell2272);
        }
        PdfPCell cell228 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("I (Full name of Policyholder) .................................................................................................................", bodyFont2);
            ph.Add(chh3);
            cell228 = new PdfPCell(ph);
            cell228.HorizontalAlignment = 0;
            cell228.Border = 0;
            cell228.Colspan = 3;
            tbl141.AddCell(cell228);
        }

        PdfPCell cell2291 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("of (Permanent address of Policyholder) ...............................................................................................", bodyFont2);
            ph.Add(chh3);
            cell2291 = new PdfPCell(ph);
            cell2291.HorizontalAlignment = 0;
            cell2291.Border = 0;
            cell2291.Colspan = 3;
            tbl141.AddCell(cell2291);
        }

        PdfPCell cell2292 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("......................................................................................................................................................", bodyFont2);
            ph.Add(chh3);
            cell2292 = new PdfPCell(ph);
            cell2292.HorizontalAlignment = 0;
            cell2292.Border = 0;
            cell2292.Colspan = 3;
            tbl141.AddCell(cell2292);
        }

        PdfPCell cell2293 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("being a *Buddhist/*Roman Catholic/ *Christian/ *Hinduist/ *Islamic do hereby solemnly, sincerely and ", bodyFont2);
            ph.Add(chh3);
            cell2293 = new PdfPCell(ph);
            cell2293.HorizontalAlignment = 0;
            cell2293.Border = 0;
            cell2293.Colspan = 3;
            tbl141.AddCell(cell2293);
        }

        PdfPCell cell2294 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("truly declare and affirm/ take oath and swear as follows:-", bodyFont2);
            ph.Add(chh3);
            cell2294 = new PdfPCell(ph);
            cell2294.HorizontalAlignment = 0;
            cell2294.Border = 0;
            cell2294.Colspan = 3;
            tbl141.AddCell(cell2294);
        }

        PdfPCell cell22941 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell22941 = new PdfPCell(ph);
            cell22941.HorizontalAlignment = 0;
            cell22941.Border = 0;
            cell22941.Colspan = 3;
            tbl141.AddCell(cell22941);
        }

        PdfPCell cell2295 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("1. I am the person in whose name Policy No " + this.litPolNumber.Text + " is issued by the", bodyFont2);
            ph.Add(chh3);
            cell2295 = new PdfPCell(ph);
            cell2295.HorizontalAlignment = 0;
            cell2295.Border = 0;
            cell2295.Colspan = 3;
            tbl141.AddCell(cell2295);
        }

        PdfPCell cell2296 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("   SRI LANKA INSURANCE CORPORATION LTD.", bodyFont2);
            ph.Add(chh3);
            cell2296 = new PdfPCell(ph);
            cell2296.HorizontalAlignment = 0;
            cell2296.Border = 0;
            cell2296.Colspan = 3;
            tbl141.AddCell(cell2296);
        }

        PdfPCell cell22961 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("   .", bodyFont2);
            ph.Add(chh3);
            cell22961 = new PdfPCell(ph);
            cell22961.HorizontalAlignment = 0;
            cell22961.Border = 0;
            cell22961.Colspan = 3;
            tbl141.AddCell(cell22961);
        }

        PdfPCell cell2297 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("2. The said Policy No " + this.litPolNumber.Text + " has been lost.", bodyFont2);
            ph.Add(chh3);
            cell2297 = new PdfPCell(ph);
            cell2297.HorizontalAlignment = 0;
            cell2297.Border = 0;
            cell2297.Colspan = 3;
            tbl141.AddCell(cell2297);
        }

        PdfPCell cell22971 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell22971 = new PdfPCell(ph);
            cell22971.HorizontalAlignment = 0;
            cell22971.Border = 0;
            cell22971.Colspan = 3;
            tbl141.AddCell(cell22971);
        }

        PdfPCell cell2298 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("3. I have made a diligent search for same, all of which have not been of any avail in ", bodyFont2);
            ph.Add(chh3);
            cell2298 = new PdfPCell(ph);
            cell2298.HorizontalAlignment = 0;
            cell2298.Border = 0;
            cell2298.Colspan = 3;
            tbl141.AddCell(cell2298);
        }

        PdfPCell cell2299 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("   tracing the whereabouts of this policy.", bodyFont2);
            ph.Add(chh3);
            cell2299 = new PdfPCell(ph);
            cell2299.HorizontalAlignment = 0;
            cell2299.Border = 0;
            cell2299.Colspan = 3;
            tbl141.AddCell(cell2299);
        }

        PdfPCell cell22991 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell22991 = new PdfPCell(ph);
            cell22991.HorizontalAlignment = 0;
            cell22991.Border = 0;
            cell22991.Colspan = 3;
            tbl141.AddCell(cell22991);
        }

        PdfPCell cell2300 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("4. I have not at any time prior to the loss, sold, assigned, mortgaged, or dealt with this ", bodyFont2);
            ph.Add(chh3);
            cell2300 = new PdfPCell(ph);
            cell2300.HorizontalAlignment = 0;
            cell2300.Border = 0;
            cell2300.Colspan = 3;
            tbl141.AddCell(cell2300);
        }

        PdfPCell cell2301 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("   policy in any manner whatsoever detrimental to the SRI LANKA INSURANCE CORPORATION LTD.", bodyFont2);
            ph.Add(chh3);
            cell2301 = new PdfPCell(ph);
            cell2301.HorizontalAlignment = 0;
            cell2301.Border = 0;
            cell2301.Colspan = 3;
            tbl141.AddCell(cell2301);
        }

        PdfPCell cell2302 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("   ", bodyFont2);
            ph.Add(chh3);
            cell2302 = new PdfPCell(ph);
            cell2302.HorizontalAlignment = 0;
            cell2302.Border = 0;
            cell2302.Colspan = 3;
            tbl141.AddCell(cell2302);
        }

        PdfPCell cell2303 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("* sworn/affirmed to at (Place) ..............................on this (Day) ....................day", bodyFont2);
            ph.Add(chh3);
            cell2303 = new PdfPCell(ph);
            cell2303.HorizontalAlignment = 0;
            cell2303.Border = 0;
            cell2303.Colspan = 3;
            tbl141.AddCell(cell2303);
        }

        PdfPCell cell2304 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk("of (Month and Year) ....................,..........................", bodyFont2);
            ph.Add(chh3);
            cell2304 = new PdfPCell(ph);
            cell2304.HorizontalAlignment = 0;
            cell2304.Border = 0;
            cell2304.Colspan = 3;
            tbl141.AddCell(cell2304);
        }

        PdfPCell cell2305 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell2305 = new PdfPCell(ph);
            cell2305.HorizontalAlignment = 0;
            cell2305.Border = 0;
            cell2305.Colspan = 3;
            tbl141.AddCell(cell2305);
        }

        PdfPCell cell23051 = new PdfPCell();
        {
            Phrase ph = new Phrase();
            Chunk chh3 = new Chunk(" ", bodyFont2);
            ph.Add(chh3);
            cell23051 = new PdfPCell(ph);
            cell23051.HorizontalAlignment = 0;
            cell23051.Border = 0;
            cell23051.Colspan = 3;
            tbl141.AddCell(cell23051);
        }

        document.Add(tbl141);

        document.Add(new Paragraph("\n", bodyFont));
        //Rectangle rect = new Rectangle(480, 500, 360, 440);
        Rectangle rect = new Rectangle(480, 400, 350, 440);
        rect.Border = Rectangle.BOX;
        rect.BorderWidth = 0.5f;
        //rect.BorderColor = new BaseColor(0x00, 0x00, 0x00);
        document.Add(rect);
        document.Add(new Paragraph("                                                                                                       Stamp(Rs.50/=)", bodyFont));
        document.Add(new Paragraph("                                                                                                       SIGNATURE OF THE ASSURED", bodyFont));
        document.Add(new Paragraph("                                                                                                      (In the presence of the Justice of the Peace.)", bodyFont));
        document.Add(new Paragraph("\n", bodyFont));
        document.Add(new Paragraph("The foregoing affidavit was duly read over and truly interpreted by me in Sinhala/Tamil, the", bodyFont));
        document.Add(new Paragraph("declarant's’ own language and he/she having appeared to understand the contents of same", bodyFont));
        document.Add(new Paragraph("appended his/her *signature/ *thumb print in my presence.", bodyFont));
        document.Add(new Paragraph("\n", bodyFont));
        document.Add(new Paragraph("                                                                                                 .........................................", bodyFont));
        document.Add(new Paragraph("                                                                                        THE JUSTICE OF THE PEACE", bodyFont));
        document.Add(new Paragraph("                                                                         (SIGNATURE AND THE OFFICIAL STAMP).", bodyFont));
        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("NOTE:", bodyFont));
        document.Add(new Paragraph("1. The above further declaration should be certified if the declarant understands in the vernacular. ", bodyFont));
        document.Add(new Paragraph("2. The signature should be placed on Rs. 50/- worth stamp.", bodyFont));
        document.Add(new Paragraph("3. Strike out whichever is inapplicable and denoted with an asterisk.", bodyFont));
        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("Date:" + DateTime.Today.ToString("yyyy/MM/dd"), bodyFont));
        document.Close();
        Response.Buffer = false;
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Affidavit_English.pdf"));
        Response.BinaryWrite(output.ToArray());

    }

    public string get_netAmtPayInWords(double amount)
    {
        string amountInWords = ""; string cents = ""; string rupees = "";
        amnt = this.HiddenField22.Value;
        int dotIndex = (amnt).IndexOf('.');
        if (dotIndex > 0)
        {
            rupees = (amnt).Substring(0, dotIndex);
            cents = (amnt).Substring(dotIndex + 1);
            int centsLength = cents.Length;

            if (centsLength == 1) { cents = cents + "0"; }
            if (centsLength > 2) { cents = (Math.Round(double.Parse(cents))).ToString(); }
        }
        else
        {
            rupees = amnt;
            cents = "00";
        }

        string amount2 = rupees + "." + cents;
        amountInWords = readAmt.readAmount((amount2.ToString()), "RUPEES", "CENTS ");
        return amountInWords;
    }


    //protected void Button6_Click(object sender, EventArgs e)
    //{
    //        hfAccordionIndex1.Value = "Y";
    //        this.Panel5.Visible = true;
    //        this.Panel6.Visible = true;
    //        this.Panel7.Visible = true;

    //        dm = new DataManager();

    //        if (!FileUpload1.HasFile)
    //        {
    //            Literal1.Text = "Attach a file";
    //        }
    //        else
    //        {
    //            HttpPostedFile file1 = FileUpload1.PostedFile;


    //            if (file1.ContentType != "application/pdf")//check extension  
    //            {
    //                Literal1.Text = "Attachment should be in pdf format";
    //            }

    //            //if (CheckBox4.Checked == false || CheckBox5.Checked == false || CheckBox6.Checked == false || CheckBox7.Checked == false || CheckBox8.Checked == false || CheckBox9.Checked == false || CheckBox10.Checked == false)
    //            //{
    //            //    Literal1.Text = "Please select documents upload";
    //            //}
    //            else
    //            {

    //                if (file1.ContentLength < 3000000)//check size  
    //                {
    //                    string udate = setDate(); ;
    //                    string prp = this.litPolNumber.Text;
    //                    string time = DateTime.Now.ToString("h:mm:ss tt");
    //                    string filename1 = this.litPolNumber.Text;
    //                    string contentType = FileUpload1.PostedFile.ContentType;
    //                    string content_size = file1.ContentLength.ToString();
    //                    string seq_no = this.litPolNumber.Text + "/" + this.HiddenField3.Value;
    //                    string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

    //                    //string clm_fm;
    //                    if (CheckBox4.Checked == true)
    //                    {
    //                        dis = "Y";
    //                    }
    //                    else if (CheckBox4.Checked == false)
    //                    {
    //                        dis = "N";
    //                    }

    //                    //string nic;
    //                    if (CheckBox5.Checked == true)
    //                    {
    //                        nic = "Y";
    //                    }
    //                    else if (CheckBox5.Checked == false)
    //                    {
    //                        nic = "N";
    //                    }

    //                    //string pass_bk;
    //                    if (CheckBox6.Checked == true)
    //                    {
    //                        pass_bk = "Y";
    //                    }
    //                    else if (CheckBox6.Checked == false)
    //                    {
    //                        pass_bk = "N";
    //                    }

    //                    //string pol_doc;
    //                    if (CheckBox7.Checked == true)
    //                    {
    //                        pol_doc = "Y";
    //                    }
    //                    else if (CheckBox7.Checked == false)
    //                    {
    //                        pol_doc = "N";
    //                    }

    //                    //string aff;
    //                    if (CheckBox8.Checked == true)
    //                    {
    //                        aff = "Y";
    //                    }
    //                    else if (CheckBox8.Checked == false)
    //                    {
    //                        aff = "N";
    //                    }

    //                    //string aff;
    //                    if (CheckBox8.Checked == true)
    //                    {
    //                        aff = "Y";
    //                    }
    //                    else if (CheckBox8.Checked == false)
    //                    {
    //                        aff = "N";
    //                    }

    //                    //string im;
    //                    if (CheckBox9.Checked == true)
    //                    {
    //                        im_nt = "Y";
    //                    }
    //                    else if (CheckBox9.Checked == false)
    //                    {
    //                        im_nt = "N";
    //                    }
    //                    //string resi;
    //                    if (CheckBox10.Checked == true)
    //                    {
    //                        res_fm = "Y";
    //                    }
    //                    else if (CheckBox10.Checked == false)
    //                    {
    //                        res_fm = "N";
    //                    }
    //                    using (Stream fs = FileUpload1.PostedFile.InputStream)
    //                    {
    //                        using (BinaryReader br = new BinaryReader(fs))
    //                        {
    //                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
    //                            //string constr = ConfigurationManager.ConnectionStrings["DBConString"].ConnectionString;
    //                            //string constr = "User ID=ais;Password=ais;Data Source=BELIFE_TEST";
    //                            string constr = "User ID=ais;Password=ais;Data Source=BELIFE";

    //                            using (OracleConnection con = new OracleConnection(constr))
    //                            {
    //                                string query = "insert into LCLM.ONLINE_DOCOUMENT_UPLOAD(POLNO,REF_NO,DOCUMENT_NAME,DOCUMENT1,UP_DATE,UP_TIME,UPLOADED_BY,CLAIM_NO,SIZE1,DIS_FORMS,NIC_COPY,PASS_BK_COPY,POLY_DOC,AFFIDAVIT,DOC_TYPE,IM_NOTICE,RES_FORM) values (:POLNO,:REF_NO,:DOCUMENT_NAME,:DOCUMENT1,:UP_DATE,:UP_TIME,:UPLOADED_BY,:CLAIM_NO,:SIZE1,:DIS_FORMS,:NIC_COPY,:PASS_BK_COPY,:POLY_DOC,:AFFIDAVIT,:DOC_TYPE,:IM_NOTICE,:RES_FORM)";
    //                                using (OracleCommand cmd = new OracleCommand(query))
    //                                {
    //                                    cmd.Connection = con;
    //                                    cmd.Parameters.AddWithValue(":POLNO", filename1);
    //                                    cmd.Parameters.AddWithValue(":REF_NO", seq_no);
    //                                    cmd.Parameters.AddWithValue(":DOCUMENT_NAME", filename1);
    //                                    cmd.Parameters.AddWithValue(":DOCUMENT1", bytes);
    //                                    cmd.Parameters.AddWithValue(":UP_DATE", udate);
    //                                    cmd.Parameters.AddWithValue(":UP_TIME", time);
    //                                    cmd.Parameters.AddWithValue(":UPLOADED_BY", ip);
    //                                    cmd.Parameters.AddWithValue(":CLAIM_NO", this.HiddenField3.Value);
    //                                    cmd.Parameters.AddWithValue(":SIZE1", content_size);
    //                                    cmd.Parameters.AddWithValue(":DIS_FORMS", dis);
    //                                    cmd.Parameters.AddWithValue(":NIC_COPY", nic);
    //                                    cmd.Parameters.AddWithValue(":PASS_BK_COPY", pass_bk);
    //                                    cmd.Parameters.AddWithValue(":POLY_DOC", pol_doc);
    //                                    cmd.Parameters.AddWithValue(":AFFIDAVIT", aff);
    //                                    cmd.Parameters.AddWithValue(":DOC_TYPE", contentType);
    //                                    cmd.Parameters.AddWithValue(":IM_NOTICE", im_nt);
    //                                    cmd.Parameters.AddWithValue(":RES_FORM", res_fm);
    //                                    con.Open();
    //                                    cmd.ExecuteNonQuery();
    //                                    con.Close();
    //                                }
    //                            }
    //                        }
    //                    }

    //                    this.Literal1.Text = "Uploaded Sucessfully!";
    //                    //****************************************
    //                    from = "lifematurity@srilankainsurance.com";
    //                    sub = "Maturity Claim forms uploding Policy No. " + prp;
    //                    // to_ad = "sujeewan@srilankainsurance.com"+","+ "piumin@srilankainsurance.com"+","+ "saliyaw@srilankainsurance.com"+","+ "shanakap@srilankainsurance.com"+","+ "lifematurity@srilankainsurance.com"+","+"umeshk@srilankainsurance.com"+","+"maduris@srilankainsurance.com"+","+"achinip@srilankainsurance.com";
    //                    to_ad = "sitharir@srilankainsurance.com";

    //                    cc = "";


    //                    string htmlString = @"<html>
    //                      <body>
    //                           <table> <tbody> <tr> <td></td> </tr> 
    //                           <tr></tr><tr></tr>"
    //                                  + " <tr><td>Claim forms uploaded through Customer portal on:" + DateTime.Today.ToString("yyyy/MM/dd") + "</td></tr>"
    //                                  + "<tr><td>Claim Number: " + this.HiddenField3.Value + " </td></tr>"
    //                                  + " <tr><td>Due Date of the Claim:" + this.HiddenField1.Value + "/" + this.HiddenField2.Value + "/" + this.HiddenField27.Value + "</td></tr>"
    //                                  + " <tr><td></td></tr>"
    //                                         + "<tr><td style='font-size:9pt'>(This is a system generated email and therefore do not reply to this email address.)"
    //                                         + "</td></tr>"
    //                                         + "</tbody>"
    //                                         + "</table>"
    //                                         + "</body>"
    //                                         + " </html>";
    //                    body = htmlString;

    //                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
    //                    string mailgateway_ip;
    //                    string mailgateway_port;
    //                    mailgateway_ip = ConfigurationManager.AppSettings["smtpServer"].Trim();
    //                    mailgateway_port = ConfigurationManager.AppSettings["smtpPort"].Trim();
    //                    to_add = to_ad;
    //                    List<string> ccList = new List<string>();
    //                    ccList.Add(cc);
    //                    sub = sub;
    //                    body = body;
    //                    body = body.Replace("\"", "'");
    //                    body = System.Net.WebUtility.HtmlDecode(body);
    //                    message.Subject = sub;
    //                    message.SubjectEncoding = System.Text.Encoding.UTF8;
    //                    message.From = new System.Net.Mail.MailAddress(from, "Maturity-Claim Forms Upload");
    //                    message.To.Add(to_add);
    //                    // message.CC.Add(cc);
    //                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
    //                    //add the views
    //                    message.AlternateViews.Add(htmlView);

    //                    object userState = message;
    //                    SmtpClient smtp = new SmtpClient(mailgateway_ip, Convert.ToInt32(mailgateway_port));
    //                    smtp.Send(message);

    //                    //**************************************



    //                }

    //            }
    //        }

    //    }
}
