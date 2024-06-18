using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;

public partial class Life_Authorized_PolicyRevival_DocsUpload : System.Web.UI.Page
{
    public string errorMsg { get; set; }
    EncryptDecrypt dc = new EncryptDecrypt();

    public bool view_15E_1 = false, view_15E_2 = false, view_15E_4 = false, view_15E_5 = false, view_15E_6 = false, view_15E_7 = false, view_15E_8 = false;
    public bool view_9E_4 = false, view_9E_5 = false, view_9E_6 = false, view_9E_7 = false, view_9E_8 = false;
    public bool view_JMER_4 = false, view_JMER_5 = false, view_JMER_6 = false, view_JMER_7 = false, view_JMER_8 = false;
    public bool view_covid19 = false;
    public bool view_12E_1 = false, view_12E_2 = false, view_12E_3 = false, view_12E_4 = false, view_12E_5 = false, view_12E_6 = false, view_12E_7 = false;
    public bool view_RESQ_1 = false, view_RESQ_2 = false, view_RESQ_3 = false, view_RESQ_4 = false, view_RESQ_5 = false, view_RESQ_6 = false, view_RESQ_7 = false;

    private static int count_to_upload = 0;
    private static int count_uploaded = 0;

    private static bool docRecvEmailSent = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string strReq = "";
            errorMsg = "";
            strReq = Request.RawUrl;
            string h = strReq.Substring(strReq.Length - 1);
            if (strReq.Contains("?"))
            {

                if (h == "#")
                {
                    errorMsg = "No url";
                    Server.Transfer("ErrorPage.aspx");
                }
                else
                {
                    strReq = strReq.Substring(strReq.IndexOf('?') + 1);
                    strReq = dc.Decrypt(strReq);
                    if (strReq == "#")
                    {
                        errorMsg = "No Parameters Passed";
                        Server.Transfer("ErrorPage.aspx");
                    }
                    else
                    {

                        Dictionary<string, string> paraList = new Dictionary<string, string>();
                        paraList = dc.getParameters(strReq);

                        if (paraList.ContainsKey("Pol_No") && paraList.ContainsKey("Pay_Amount") && paraList.ContainsKey("Total_Due") && paraList.ContainsKey("PH_Name") && paraList.ContainsKey("Seq_No"))
                        {
                            count_to_upload = 0;
                            count_uploaded = 0;
                            docRecvEmailSent = false;

                            this.ltrPolNo.Text = paraList["Pol_No"];
                            this.ltrPHName.Text = paraList["PH_Name"];
                            this.ltrPHStatus.Text = paraList["PH_Status"];
                            this.hdfTotlDue.Value = paraList["Total_Due"];
                            this.hdfPayAmount.Value = paraList["Pay_Amount"];
                            this.ltrTotalDue.Text = double.Parse(paraList["Total_Due"]).ToString("N2");
                            this.ltrPayAmount.Text = double.Parse(paraList["Pay_Amount"]).ToString("N2");
                            this.hdfSeqNo.Value = paraList["Seq_No"];

                            if (ltrPayAmount.Text.Equals("0.00"))
                            {
                                this.btnPay.Visible = false;
                            }

                            Revival_Life revvl = new Revival_Life();

                            # region Load upload documents region                            

                            # region 15E Form

                            DataSet ds_15E = new DataSet();
                            DataTable dt1 = new DataTable();
                            dt1.Columns.Add("Name", typeof(string));
                            dt1.Columns.Add("prpertype", typeof(int));
                            ds_15E.Tables.Add(dt1);

                            DataTable dt15E = new DataTable();
                            string mesg1 = "";
                            dt15E = revvl.get15E_reqstd_list_prpertype(int.Parse(this.ltrPolNo.Text), out mesg1);

                            if (dt15E.Rows.Count > 0)
                            {
                                this.lt_15E_doc_for_1.Text = "15E form for ";
                                this.lt_15E_name_1.Text = dt15E.Rows[0]["Name"].ToString();
                                view_15E_1 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt15E.Rows.Count > 1)
                            {
                                this.lt_15E_doc_for_2.Text = "15E form for ";
                                this.lt_15E_name_2.Text = dt15E.Rows[1]["Name"].ToString();
                                view_15E_2 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt15E.Rows.Count > 2)
                            {
                                this.lt_15E_doc_for_4.Text = "15E form for ";
                                this.lt_15E_name_4.Text = dt15E.Rows[2]["Name"].ToString();
                                view_15E_4 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt15E.Rows.Count > 3)
                            {
                                this.lt_15E_doc_for_5.Text = "15E form for ";
                                this.lt_15E_name_5.Text = dt15E.Rows[3]["Name"].ToString();
                                view_15E_5 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt15E.Rows.Count > 4)
                            {
                                this.lt_15E_doc_for_6.Text = "15E form for ";
                                this.lt_15E_name_6.Text = dt15E.Rows[4]["Name"].ToString();
                                view_15E_6 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt15E.Rows.Count > 5)
                            {
                                this.lt_15E_doc_for_7.Text = "15E form for ";
                                this.lt_15E_name_7.Text = dt15E.Rows[5]["Name"].ToString();
                                view_15E_7 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt15E.Rows.Count > 6)
                            {
                                this.lt_15E_doc_for_8.Text = "15E form for ";
                                this.lt_15E_name_8.Text = dt15E.Rows[6]["Name"].ToString();
                                view_15E_8 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            ViewState["view_15E_1"] = view_15E_1;
                            ViewState["view_15E_2"] = view_15E_2;
                            ViewState["view_15E_4"] = view_15E_4;
                            ViewState["view_15E_5"] = view_15E_5;
                            ViewState["view_15E_6"] = view_15E_6;
                            ViewState["view_15E_7"] = view_15E_7;
                            ViewState["view_15E_8"] = view_15E_8;

                            # endregion

                            # region 12E Form

                            DataSet ds_12E = new DataSet();
                            DataTable dt5 = new DataTable();
                            dt5.Columns.Add("Name", typeof(string));
                            dt5.Columns.Add("prpertype", typeof(int));
                            ds_12E.Tables.Add(dt5);

                            DataTable dt12E = new DataTable();
                            string mesg5 = "";
                            dt12E = revvl.get12E_reqstd_list_prpertype(int.Parse(this.ltrPolNo.Text), out mesg5);

                            if (dt12E.Rows.Count > 0)
                            {
                                this.lt_12E_doc_for_1.Text = "12E form for ";
                                this.lt_12E_name_1.Text = dt12E.Rows[0]["Name"].ToString();
                                view_12E_1 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt12E.Rows.Count > 1)
                            {
                                this.lt_12E_doc_for_2.Text = "12E form for ";
                                this.lt_12E_name_2.Text = dt12E.Rows[1]["Name"].ToString();
                                view_12E_2 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt12E.Rows.Count > 2)
                            {
                                this.lt_12E_doc_for_3.Text = "12E form for ";
                                this.lt_12E_name_3.Text = dt12E.Rows[6]["Name"].ToString();
                                view_12E_3 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt12E.Rows.Count > 3)
                            {
                                this.lt_12E_doc_for_4.Text = "12E form for ";
                                this.lt_12E_name_4.Text = dt12E.Rows[2]["Name"].ToString();
                                view_12E_4 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt12E.Rows.Count > 4)
                            {
                                this.lt_12E_doc_for_5.Text = "12E form for ";
                                this.lt_12E_name_5.Text = dt12E.Rows[3]["Name"].ToString();
                                view_12E_5 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt12E.Rows.Count > 5)
                            {
                                this.lt_12E_doc_for_6.Text = "12E form for ";
                                this.lt_12E_name_6.Text = dt12E.Rows[4]["Name"].ToString();
                                view_12E_6 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt12E.Rows.Count > 6)
                            {
                                this.lt_12E_doc_for_7.Text = "12E form for ";
                                this.lt_12E_name_7.Text = dt12E.Rows[5]["Name"].ToString();
                                view_12E_7 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            ViewState["view_12E_1"] = view_12E_1;
                            ViewState["view_12E_2"] = view_12E_2;
                            ViewState["view_12E_3"] = view_12E_3;
                            ViewState["view_12E_4"] = view_12E_4;
                            ViewState["view_12E_5"] = view_12E_5;
                            ViewState["view_12E_6"] = view_12E_6;
                            ViewState["view_12E_7"] = view_12E_7;

                            # endregion

                            # region Travel and Residential Questionnaire Form

                            DataSet ds_RESQ = new DataSet();
                            DataTable dt6 = new DataTable();
                            dt6.Columns.Add("Name", typeof(string));
                            dt6.Columns.Add("prpertype", typeof(int));
                            ds_RESQ.Tables.Add(dt6);

                            DataTable dtRESQ = new DataTable();
                            string mesg6 = "";
                            dtRESQ = revvl.getRESQ_reqstd_list_prpertype(int.Parse(this.ltrPolNo.Text), out mesg6);

                            if (dtRESQ.Rows.Count > 0)
                            {
                                this.lt_RESQ_doc_for_1.Text = "Travel & Resi. form for ";
                                this.lt_RESQ_name_1.Text = dtRESQ.Rows[0]["Name"].ToString();
                                view_RESQ_1 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dtRESQ.Rows.Count > 1)
                            {
                                this.lt_RESQ_doc_for_2.Text = "Travel & Resi. form for ";
                                this.lt_RESQ_name_2.Text = dtRESQ.Rows[1]["Name"].ToString();
                                view_RESQ_2 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dtRESQ.Rows.Count > 2)
                            {
                                this.lt_RESQ_doc_for_3.Text = "Travel & Resi. form for ";
                                this.lt_RESQ_name_3.Text = dtRESQ.Rows[6]["Name"].ToString();
                                view_RESQ_3 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dtRESQ.Rows.Count > 3)
                            {
                                this.lt_RESQ_doc_for_4.Text = "Travel & Resi. form for ";
                                this.lt_RESQ_name_4.Text = dtRESQ.Rows[2]["Name"].ToString();
                                view_RESQ_4 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dtRESQ.Rows.Count > 4)
                            {
                                this.lt_RESQ_doc_for_5.Text = "Travel & Resi. form for ";
                                this.lt_RESQ_name_5.Text = dtRESQ.Rows[3]["Name"].ToString();
                                view_RESQ_5 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dtRESQ.Rows.Count > 5)
                            {
                                this.lt_RESQ_doc_for_6.Text = "Travel & Resi. form for ";
                                this.lt_RESQ_name_6.Text = dtRESQ.Rows[4]["Name"].ToString();
                                view_RESQ_6 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dtRESQ.Rows.Count > 6)
                            {
                                this.lt_RESQ_doc_for_7.Text = "Travel & Resi. form for ";
                                this.lt_RESQ_name_7.Text = dtRESQ.Rows[5]["Name"].ToString();
                                view_RESQ_7 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            ViewState["view_RESQ_1"] = view_RESQ_1;
                            ViewState["view_RESQ_2"] = view_RESQ_2;
                            ViewState["view_RESQ_3"] = view_RESQ_3;
                            ViewState["view_RESQ_4"] = view_RESQ_4;
                            ViewState["view_RESQ_5"] = view_RESQ_5;
                            ViewState["view_RESQ_6"] = view_RESQ_6;
                            ViewState["view_RESQ_7"] = view_RESQ_7;

                            # endregion

                            # region 9E Form

                            DataSet ds_9E = new DataSet();
                            DataTable dt2 = new DataTable();
                            dt2.Columns.Add("Name", typeof(string));
                            dt2.Columns.Add("prpertype", typeof(int));
                            ds_9E.Tables.Add(dt2);

                            DataTable dt9E = new DataTable();
                            string mesg2 = "";
                            dt9E = revvl.get9E_reqstd_list_prpertype(int.Parse(this.ltrPolNo.Text), out mesg2);

                            if (dt9E.Rows.Count > 0)
                            {
                                this.lt_9E_doc_for_4.Text = "9E form for ";
                                this.lt_9E_name_4.Text = dt9E.Rows[0]["Name"].ToString();
                                view_9E_4 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt9E.Rows.Count > 1)
                            {
                                this.lt_9E_doc_for_5.Text = "9E form for ";
                                this.lt_9E_name_5.Text = dt9E.Rows[1]["Name"].ToString();
                                view_9E_5 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt9E.Rows.Count > 2)
                            {
                                this.lt_9E_doc_for_6.Text = "9E form for ";
                                this.lt_9E_name_6.Text = dt9E.Rows[2]["Name"].ToString();
                                view_9E_6 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt9E.Rows.Count > 3)
                            {
                                this.lt_9E_doc_for_7.Text = "9E form for ";
                                this.lt_9E_name_7.Text = dt9E.Rows[3]["Name"].ToString();
                                view_9E_7 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dt9E.Rows.Count > 4)
                            {
                                this.lt_9E_doc_for_8.Text = "9E form for ";
                                this.lt_9E_name_8.Text = dt9E.Rows[4]["Name"].ToString();
                                view_9E_8 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            ViewState["view_9E_4"] = view_9E_4;
                            ViewState["view_9E_5"] = view_9E_5;
                            ViewState["view_9E_6"] = view_9E_6;
                            ViewState["view_9E_7"] = view_9E_7;
                            ViewState["view_9E_8"] = view_9E_8;

                            # endregion

                            # region JMER Form

                            DataSet ds_JMER = new DataSet();
                            DataTable dt3 = new DataTable();
                            dt3.Columns.Add("Name", typeof(string));
                            dt3.Columns.Add("prpertype", typeof(int));
                            ds_JMER.Tables.Add(dt3);

                            DataTable dtJMER = new DataTable();
                            string mesg3 = "";
                            dtJMER = revvl.getJMER_reqstd_list_prpertype(int.Parse(this.ltrPolNo.Text), out mesg3);

                            if (dtJMER.Rows.Count > 0)
                            {
                                this.lt_JMER_doc_for_4.Text = "JMER form for ";
                                this.lt_JMER_name_4.Text = dtJMER.Rows[0]["Name"].ToString();
                                view_JMER_4 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dtJMER.Rows.Count > 1)
                            {
                                this.lt_JMER_doc_for_5.Text = "JMER form for ";
                                this.lt_JMER_name_5.Text = dtJMER.Rows[1]["Name"].ToString();
                                view_JMER_5 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dtJMER.Rows.Count > 2)
                            {
                                this.lt_JMER_doc_for_6.Text = "JMER form for ";
                                this.lt_JMER_name_6.Text = dtJMER.Rows[2]["Name"].ToString();
                                view_JMER_6 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dtJMER.Rows.Count > 3)
                            {
                                this.lt_JMER_doc_for_7.Text = "JMER form for ";
                                this.lt_JMER_name_7.Text = dtJMER.Rows[3]["Name"].ToString();
                                view_JMER_7 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            if (dtJMER.Rows.Count > 4)
                            {
                                this.lt_JMER_doc_for_8.Text = "JMER form for ";
                                this.lt_JMER_name_8.Text = dtJMER.Rows[4]["Name"].ToString();
                                view_JMER_8 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            ViewState["view_JMER_4"] = view_JMER_4;
                            ViewState["view_JMER_5"] = view_JMER_5;
                            ViewState["view_JMER_6"] = view_JMER_6;
                            ViewState["view_JMER_7"] = view_JMER_7;
                            ViewState["view_JMER_8"] = view_JMER_8;

                            # endregion

                            # region Covid19 Questionnaire Form

                            DataSet ds_covid = new DataSet();
                            DataTable dt4 = new DataTable();
                            dt4.Columns.Add("Name", typeof(string));
                            dt4.Columns.Add("prpertype", typeof(int));
                            ds_covid.Tables.Add(dt4);

                            DataTable dtCovid = new DataTable();
                            string mesg4 = "";
                            dtCovid = revvl.getCovid_reqstd_list_prpertype(int.Parse(this.ltrPolNo.Text), out mesg4);

                            if (dtCovid.Rows.Count > 0)
                            {
                                this.lt_covid_doc_for.Text = "Covid 19 Questionnaire form";
                                this.lt_covid_name.Text = "";
                                view_covid19 = true;
                                count_to_upload = count_to_upload + 1;
                            }

                            ViewState["view_covid19"] = view_covid19;

                            # endregion                            

                            # endregion

                            ViewState["count_to_upload"] = count_to_upload;
                            ViewState["docRecvEmailSent"] = docRecvEmailSent;

                        }
                        else
                        {
                            errorMsg = "No url";
                            Server.Transfer("ErrorPage.aspx");
                        }

                    }
                }
            }
            else
            {

            }
        }
        else
        {
            getViewStates();
        }
    }

    private void getViewStates()
    {
        if (ViewState["view_15E_1"] != null)
            view_15E_1 = (bool)ViewState["view_15E_1"];
        if (ViewState["view_15E_2"] != null)
            view_15E_2 = (bool)ViewState["view_15E_2"];
        if (ViewState["view_15E_4"] != null)
            view_15E_4 = (bool)ViewState["view_15E_4"];
        if (ViewState["view_15E_5"] != null)
            view_15E_5 = (bool)ViewState["view_15E_5"];
        if (ViewState["view_15E_6"] != null)
            view_15E_6 = (bool)ViewState["view_15E_6"];
        if (ViewState["view_15E_7"] != null)
            view_15E_7 = (bool)ViewState["view_15E_7"];
        if (ViewState["view_15E_8"] != null)
            view_15E_8 = (bool)ViewState["view_15E_8"];

        if (ViewState["view_12E_1"] != null)
            view_12E_1 = (bool)ViewState["view_12E_1"];
        if (ViewState["view_12E_2"] != null)
            view_12E_2 = (bool)ViewState["view_12E_2"];
        if (ViewState["view_12E_3"] != null)
            view_12E_3 = (bool)ViewState["view_12E_3"];
        if (ViewState["view_12E_4"] != null)
            view_12E_4 = (bool)ViewState["view_12E_4"];
        if (ViewState["view_12E_5"] != null)
            view_12E_5 = (bool)ViewState["view_12E_5"];
        if (ViewState["view_12E_6"] != null)
            view_12E_6 = (bool)ViewState["view_12E_6"];
        if (ViewState["view_12E_7"] != null)
            view_12E_7 = (bool)ViewState["view_12E_7"];       

        if (ViewState["view_RESQ_1"] != null)
            view_RESQ_1 = (bool)ViewState["view_RESQ_1"];
        if (ViewState["view_RESQ_2"] != null)
            view_RESQ_2 = (bool)ViewState["view_RESQ_2"];
        if (ViewState["view_RESQ_3"] != null)
            view_RESQ_3 = (bool)ViewState["view_RESQ_3"];
        if (ViewState["view_RESQ_4"] != null)
            view_RESQ_4 = (bool)ViewState["view_RESQ_4"];
        if (ViewState["view_RESQ_5"] != null)
            view_RESQ_5 = (bool)ViewState["view_RESQ_5"];
        if (ViewState["view_RESQ_6"] != null)
            view_RESQ_6 = (bool)ViewState["view_RESQ_6"];
        if (ViewState["view_RESQ_7"] != null)
            view_RESQ_7 = (bool)ViewState["view_RESQ_7"];        

        if (ViewState["view_9E_4"] != null)
            view_9E_4 = (bool)ViewState["view_9E_4"];
        if (ViewState["view_9E_5"] != null)
            view_9E_5 = (bool)ViewState["view_9E_5"];
        if (ViewState["view_9E_6"] != null)
            view_9E_6 = (bool)ViewState["view_9E_6"];
        if (ViewState["view_9E_7"] != null)
            view_9E_7 = (bool)ViewState["view_9E_7"];
        if (ViewState["view_9E_8"] != null)
            view_9E_8 = (bool)ViewState["view_9E_8"];

        if (ViewState["view_JMER_4"] != null)
            view_JMER_4 = (bool)ViewState["view_JMER_4"];
        if (ViewState["view_JMER_5"] != null)
            view_JMER_5 = (bool)ViewState["view_JMER_5"];
        if (ViewState["view_JMER_6"] != null)
            view_JMER_6 = (bool)ViewState["view_JMER_6"];
        if (ViewState["view_JMER_7"] != null)
            view_JMER_7 = (bool)ViewState["view_JMER_7"];
        if (ViewState["view_JMER_8"] != null)
            view_JMER_8 = (bool)ViewState["view_JMER_8"];

        if (ViewState["view_covid19"] != null)
            view_covid19 = (bool)ViewState["view_covid19"];
    }

    protected void btnPay_Click(object sender, EventArgs e)
    {        
        double payAmt = 0;        
        double addtAmt = 0;
        double duesTotal = 0;        

        try
        {            
            duesTotal = double.Parse(this.hdfTotlDue.Value.Trim());            
            payAmt = double.Parse(this.hdfPayAmount.Value.Trim());            

            if (payAmt > duesTotal)
            {
                addtAmt = payAmt - duesTotal;
            }
        }
        catch
        {
            //addtAmtValidator.IsValid = false;
            //addtAmtValidator.ErrorMessage = "Invalid amount.";
        }

        if (Math.Round(payAmt, 2) <= 0)
        {
            //addtAmtValidator.IsValid = false;
            //addtAmtValidator.ErrorMessage = "Total paid amount should be greater than 0.";
        }

        if (Page.IsValid)
        {
            LifePayment directPayment = new LifePayment();

            string mesg = "success";
            if (duesTotal > 0)
            {
                mesg = directPayment.depositAdjPending(ltrPolNo.Text.Trim());
            }

            Revival_Life revl = new Revival_Life();          

            if (mesg == "success")
            {
                string recptNo = directPayment.generate_renwReceiptNo(Convert.ToInt32(DateTime.Today.ToString("yyyy")), "R");

                if (!String.IsNullOrEmpty(recptNo))
                {
                    CustProfile profile = new CustProfile(Page.User.Identity.Name);

                    if (directPayment.insert_revival(ltrPolNo.Text.Trim(), payAmt, Page.User.Identity.Name, "P",
                                        recptNo, this.ltrPHName.Text.Trim(), "R", duesTotal, addtAmt, profile.O_email, profile.O_mobileNumber, int.Parse(this.hdfSeqNo.Value)))
                    {
                        EncryptDecrypt dc = new EncryptDecrypt();
                        Dictionary<string, string> qs = new Dictionary<string, string>();
                        qs.Add("Ref_No", recptNo.Trim());
                        qs.Add("Type", "RV"); // N-new businees, R-renewals
                        Response.Redirect(dc.url_encrypt("Products/Payment.aspx", qs));
                    }
                    else
                    {
                        //PayPremValidator.IsValid = false;
                        //PayPremValidator.ErrorMessage = "An error occured, please resubmit with correct details or contact SLIC.";
                        //txtPayPremAmt.Focus();
                    }
                }
                else
                {
                    //PayPremValidator.IsValid = false;
                    //PayPremValidator.ErrorMessage = "An error occured, please resubmit with correct details or contact SLIC.";
                    //txtPayPremAmt.Focus();
                }
            }
            else
            {
                //PayPremValidator.IsValid = false;
                //PayPremValidator.ErrorMessage = mesg;
            }
        }
       
    }

    # region 15 E button clicks

    protected void btn_15E_upload_1_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_15E_1.HasFile)
        {
            if (this.fu_15E_1.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_15E_1.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("15E", this.lt_15E_name_1.Text, Path.GetFileName(this.fu_15E_1.PostedFile.FileName), data,
                    this.fu_15E_1.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_15E_1.Visible = false;
                    this.btn_15E_upload_1.Visible = false;
                    this.ltr_15E_uploaded_1.Text = "Uploaded";
                    this.ltr_15E_uploaded_1.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_15E_error_1.Text = "Too larger file";
                lbl_15E_error_1.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_15E_error_1.Text = "Select file";
            lbl_15E_error_1.ForeColor = System.Drawing.Color.Red;
        }        
        getViewStates();        
    }

    protected void btn_15E_upload_2_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_15E_2.HasFile)
        {
            if (this.fu_15E_2.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_15E_2.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("15E", this.lt_15E_name_2.Text, Path.GetFileName(this.fu_15E_2.PostedFile.FileName), data,
                    this.fu_15E_2.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_15E_2.Visible = false;
                    this.btn_15E_upload_2.Visible = false;
                    this.ltr_15E_uploaded_2.Text = "Uploaded";
                    this.ltr_15E_uploaded_2.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }

            else
            {
                lbl_15E_error_2.Text = "Too larger file";
                lbl_15E_error_2.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_15E_error_2.Text = "Select file";
            lbl_15E_error_2.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates(); 
    }

    protected void btn_15E_upload_4_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_15E_4.HasFile)
        {
            if (this.fu_15E_4.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_15E_4.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("15E", this.lt_15E_name_4.Text, Path.GetFileName(this.fu_15E_4.PostedFile.FileName), data,
                    this.fu_15E_4.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_15E_4.Visible = false;
                    this.btn_15E_upload_4.Visible = false;
                    this.ltr_15E_uploaded_4.Text = "Uploaded";
                    this.ltr_15E_uploaded_4.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_15E_error_4.Text = "Too larger file";
                lbl_15E_error_4.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_15E_error_4.Text = "Select file";
            lbl_15E_error_4.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();  
    }

    protected void btn_15E_upload_5_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_15E_5.HasFile)
        {
            if (this.fu_15E_5.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_15E_5.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("15E", this.lt_15E_name_5.Text, Path.GetFileName(this.fu_15E_5.PostedFile.FileName), data,
                    this.fu_15E_5.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_15E_5.Visible = false;
                    this.btn_15E_upload_5.Visible = false;
                    this.ltr_15E_uploaded_5.Text = "Uploaded";
                    this.ltr_15E_uploaded_5.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_15E_error_5.Text = "Too larger file";
                lbl_15E_error_5.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_15E_error_5.Text = "Select file";
            lbl_15E_error_5.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();  
    }

    protected void btn_15E_upload_6_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_15E_6.HasFile)
        {
            if (this.fu_15E_6.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_15E_6.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("15E", this.lt_15E_name_6.Text, Path.GetFileName(this.fu_15E_6.PostedFile.FileName), data,
                    this.fu_15E_6.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_15E_6.Visible = false;
                    this.btn_15E_upload_6.Visible = false;
                    this.ltr_15E_uploaded_6.Text = "Uploaded";
                    this.ltr_15E_uploaded_6.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_15E_error_6.Text = "Too larger file";
                lbl_15E_error_6.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_15E_error_6.Text = "Select file";
            lbl_15E_error_6.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();  
    }

    protected void btn_15E_upload_7_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_15E_7.HasFile)
        {
            if (this.fu_15E_7.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_15E_7.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("15E", this.lt_15E_name_7.Text, Path.GetFileName(this.fu_15E_7.PostedFile.FileName), data,
                    this.fu_15E_7.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_15E_7.Visible = false;
                    this.btn_15E_upload_7.Visible = false;
                    this.ltr_15E_uploaded_7.Text = "Uploaded";
                    this.ltr_15E_uploaded_7.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_15E_error_7.Text = "Too larger file";
                lbl_15E_error_7.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_15E_error_7.Text = "Select file";
            lbl_15E_error_7.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();  
    }

    protected void btn_15E_upload_8_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_15E_8.HasFile)
        {
            if (this.fu_15E_8.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_15E_8.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("15E", this.lt_15E_name_8.Text, Path.GetFileName(this.fu_15E_8.PostedFile.FileName), data,
                    this.fu_15E_8.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_15E_8.Visible = false;
                    this.btn_15E_upload_8.Visible = false;
                    this.ltr_15E_uploaded_8.Text = "Uploaded";
                    this.ltr_15E_uploaded_8.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_15E_error_8.Text = "Too larger file";
                lbl_15E_error_8.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_15E_error_8.Text = "Select file";
            lbl_15E_error_8.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();  
    }

    # endregion

    # region 12 E button clicks

    protected void btn_12E_upload_1_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_12E_1.HasFile)
        {
            if (this.fu_12E_1.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_12E_1.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("12E", this.lt_12E_name_1.Text, Path.GetFileName(this.fu_12E_1.PostedFile.FileName), data,
                    this.fu_12E_1.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_12E_1.Visible = false;
                    this.btn_12E_upload_1.Visible = false;
                    this.ltr_12E_uploaded_1.Text = "Uploaded";
                    this.ltr_12E_uploaded_1.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_12E_error_1.Text = "Too larger file";
                lbl_12E_error_1.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_12E_error_1.Text = "Select file";
            lbl_12E_error_1.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

    protected void btn_12E_upload_2_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_12E_2.HasFile)
        {
            if (this.fu_12E_2.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_12E_2.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("12E", this.lt_12E_name_2.Text, Path.GetFileName(this.fu_12E_2.PostedFile.FileName), data,
                    this.fu_12E_2.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_12E_2.Visible = false;
                    this.btn_12E_upload_2.Visible = false;
                    this.ltr_12E_uploaded_2.Text = "Uploaded";
                    this.ltr_12E_uploaded_2.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }

            else
            {
                lbl_12E_error_2.Text = "Too larger file";
                lbl_12E_error_2.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_12E_error_2.Text = "Select file";
            lbl_12E_error_2.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

    protected void btn_12E_upload_3_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_12E_3.HasFile)
        {
            if (this.fu_12E_3.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_12E_3.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("12E", this.lt_12E_name_3.Text, Path.GetFileName(this.fu_12E_3.PostedFile.FileName), data,
                    this.fu_12E_3.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_12E_3.Visible = false;
                    this.btn_12E_upload_3.Visible = false;
                    this.ltr_12E_uploaded_3.Text = "Uploaded";
                    this.ltr_12E_uploaded_3.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_12E_error_3.Text = "Too larger file";
                lbl_12E_error_3.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_12E_error_3.Text = "Select file";
            lbl_12E_error_3.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

    protected void btn_12E_upload_4_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_12E_4.HasFile)
        {
            if (this.fu_12E_4.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_12E_4.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("12E", this.lt_12E_name_4.Text, Path.GetFileName(this.fu_12E_4.PostedFile.FileName), data,
                    this.fu_12E_4.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_12E_4.Visible = false;
                    this.btn_12E_upload_4.Visible = false;
                    this.ltr_12E_uploaded_4.Text = "Uploaded";
                    this.ltr_12E_uploaded_4.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_12E_error_4.Text = "Too larger file";
                lbl_12E_error_4.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_12E_error_4.Text = "Select file";
            lbl_12E_error_4.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

    protected void btn_12E_upload_5_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_12E_5.HasFile)
        {
            if (this.fu_12E_5.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_12E_5.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("12E", this.lt_12E_name_5.Text, Path.GetFileName(this.fu_12E_5.PostedFile.FileName), data,
                    this.fu_12E_5.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_12E_5.Visible = false;
                    this.btn_12E_upload_5.Visible = false;
                    this.ltr_12E_uploaded_5.Text = "Uploaded";
                    this.ltr_12E_uploaded_5.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_12E_error_5.Text = "Too larger file";
                lbl_12E_error_5.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_12E_error_5.Text = "Select file";
            lbl_12E_error_5.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

    protected void btn_12E_upload_6_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_12E_6.HasFile)
        {
            if (this.fu_12E_6.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_12E_6.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("12E", this.lt_12E_name_6.Text, Path.GetFileName(this.fu_12E_6.PostedFile.FileName), data,
                    this.fu_12E_6.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_12E_6.Visible = false;
                    this.btn_12E_upload_6.Visible = false;
                    this.ltr_12E_uploaded_6.Text = "Uploaded";
                    this.ltr_12E_uploaded_6.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_12E_error_6.Text = "Too larger file";
                lbl_12E_error_6.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_12E_error_6.Text = "Select file";
            lbl_12E_error_6.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

    protected void btn_12E_upload_7_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_12E_7.HasFile)
        {
            if (this.fu_12E_7.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_12E_7.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("12E", this.lt_12E_name_7.Text, Path.GetFileName(this.fu_12E_7.PostedFile.FileName), data,
                    this.fu_12E_7.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_12E_7.Visible = false;
                    this.btn_12E_upload_7.Visible = false;
                    this.ltr_12E_uploaded_7.Text = "Uploaded";
                    this.ltr_12E_uploaded_7.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_12E_error_7.Text = "Too larger file";
                lbl_12E_error_7.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_12E_error_7.Text = "Select file";
            lbl_12E_error_7.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

    # endregion

    # region RESQ button clicks

    protected void btn_RESQ_upload_1_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_RESQ_1.HasFile)
        {
            if (this.fu_RESQ_1.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_RESQ_1.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("RESQ", this.lt_RESQ_name_1.Text, Path.GetFileName(this.fu_RESQ_1.PostedFile.FileName), data,
                    this.fu_RESQ_1.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_RESQ_1.Visible = false;
                    this.btn_RESQ_upload_1.Visible = false;
                    this.ltr_RESQ_uploaded_1.Text = "Uploaded";
                    this.ltr_RESQ_uploaded_1.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_RESQ_error_1.Text = "Too larger file";
                lbl_RESQ_error_1.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_RESQ_error_1.Text = "Select file";
            lbl_RESQ_error_1.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

    protected void btn_RESQ_upload_2_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_RESQ_2.HasFile)
        {
            if (this.fu_RESQ_2.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_RESQ_2.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("RESQ", this.lt_RESQ_name_2.Text, Path.GetFileName(this.fu_RESQ_2.PostedFile.FileName), data,
                    this.fu_RESQ_2.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_RESQ_2.Visible = false;
                    this.btn_RESQ_upload_2.Visible = false;
                    this.ltr_RESQ_uploaded_2.Text = "Uploaded";
                    this.ltr_RESQ_uploaded_2.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }

            else
            {
                lbl_RESQ_error_2.Text = "Too larger file";
                lbl_RESQ_error_2.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_RESQ_error_2.Text = "Select file";
            lbl_RESQ_error_2.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

    protected void btn_RESQ_upload_3_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_RESQ_3.HasFile)
        {
            if (this.fu_RESQ_3.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_RESQ_3.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("RESQ", this.lt_RESQ_name_3.Text, Path.GetFileName(this.fu_RESQ_3.PostedFile.FileName), data,
                    this.fu_RESQ_3.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_RESQ_3.Visible = false;
                    this.btn_RESQ_upload_3.Visible = false;
                    this.ltr_RESQ_uploaded_3.Text = "Uploaded";
                    this.ltr_RESQ_uploaded_3.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_RESQ_error_3.Text = "Too larger file";
                lbl_RESQ_error_3.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_RESQ_error_3.Text = "Select file";
            lbl_RESQ_error_3.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

    protected void btn_RESQ_upload_4_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_RESQ_4.HasFile)
        {
            if (this.fu_RESQ_4.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_RESQ_4.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("RESQ", this.lt_RESQ_name_4.Text, Path.GetFileName(this.fu_RESQ_4.PostedFile.FileName), data,
                    this.fu_RESQ_4.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_RESQ_4.Visible = false;
                    this.btn_RESQ_upload_4.Visible = false;
                    this.ltr_RESQ_uploaded_4.Text = "Uploaded";
                    this.ltr_RESQ_uploaded_4.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_RESQ_error_4.Text = "Too larger file";
                lbl_RESQ_error_4.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_RESQ_error_4.Text = "Select file";
            lbl_RESQ_error_4.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

    protected void btn_RESQ_upload_5_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_RESQ_5.HasFile)
        {
            if (this.fu_RESQ_5.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_RESQ_5.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("RESQ", this.lt_RESQ_name_5.Text, Path.GetFileName(this.fu_RESQ_5.PostedFile.FileName), data,
                    this.fu_RESQ_5.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_RESQ_5.Visible = false;
                    this.btn_RESQ_upload_5.Visible = false;
                    this.ltr_RESQ_uploaded_5.Text = "Uploaded";
                    this.ltr_RESQ_uploaded_5.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_RESQ_error_5.Text = "Too larger file";
                lbl_RESQ_error_5.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_RESQ_error_5.Text = "Select file";
            lbl_RESQ_error_5.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

    protected void btn_RESQ_upload_6_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_RESQ_6.HasFile)
        {
            if (this.fu_RESQ_6.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_RESQ_6.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("RESQ", this.lt_RESQ_name_6.Text, Path.GetFileName(this.fu_RESQ_6.PostedFile.FileName), data,
                    this.fu_RESQ_6.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_RESQ_6.Visible = false;
                    this.btn_RESQ_upload_6.Visible = false;
                    this.ltr_RESQ_uploaded_6.Text = "Uploaded";
                    this.ltr_RESQ_uploaded_6.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_RESQ_error_6.Text = "Too larger file";
                lbl_RESQ_error_6.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_RESQ_error_6.Text = "Select file";
            lbl_RESQ_error_6.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

    protected void btn_RESQ_upload_7_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_RESQ_7.HasFile)
        {
            if (this.fu_RESQ_7.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_RESQ_7.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("RESQ", this.lt_RESQ_name_7.Text, Path.GetFileName(this.fu_RESQ_7.PostedFile.FileName), data,
                    this.fu_RESQ_7.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_RESQ_7.Visible = false;
                    this.btn_RESQ_upload_7.Visible = false;
                    this.ltr_RESQ_uploaded_7.Text = "Uploaded";
                    this.ltr_RESQ_uploaded_7.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_RESQ_error_7.Text = "Too larger file";
                lbl_RESQ_error_7.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_RESQ_error_7.Text = "Select file";
            lbl_RESQ_error_7.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

    # endregion

    # region 9E button clicks

    protected void btn_9E_upload_4_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_9E_4.HasFile)
        {
            if (this.fu_9E_4.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_9E_4.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("9E", this.lt_9E_name_4.Text, Path.GetFileName(this.fu_9E_4.PostedFile.FileName), data,
                    this.fu_9E_4.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_9E_4.Visible = false;
                    this.btn_9E_upload_4.Visible = false;
                    this.ltr_9E_uploaded_4.Text = "Uploaded";
                    this.ltr_9E_uploaded_4.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_9E_error_4.Text = "Too larger file";
                lbl_9E_error_4.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_9E_error_4.Text = "Select file";
            lbl_9E_error_4.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();   
    }

    protected void btn_9E_upload_5_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_9E_5.HasFile)
        {
            if (this.fu_9E_5.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_9E_5.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("9E", this.lt_9E_name_5.Text, Path.GetFileName(this.fu_9E_5.PostedFile.FileName), data,
                    this.fu_9E_5.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_9E_5.Visible = false;
                    this.btn_9E_upload_5.Visible = false;
                    this.ltr_9E_uploaded_5.Text = "Uploaded";
                    this.ltr_9E_uploaded_5.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_9E_error_5.Text = "Too larger file";
                lbl_9E_error_5.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_9E_error_5.Text = "Select file";
            lbl_9E_error_5.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates(); 
    }

    protected void btn_9E_upload_6_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_9E_6.HasFile)
        {
            if (this.fu_9E_6.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_9E_6.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("9E", this.lt_9E_name_6.Text, Path.GetFileName(this.fu_9E_6.PostedFile.FileName), data,
                    this.fu_9E_6.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_9E_6.Visible = false;
                    this.btn_9E_upload_6.Visible = false;
                    this.ltr_9E_uploaded_6.Text = "Uploaded";
                    this.ltr_9E_uploaded_6.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_9E_error_6.Text = "Too larger file";
                lbl_9E_error_6.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_9E_error_6.Text = "Select file";
            lbl_9E_error_6.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates(); 
    }

    protected void btn_9E_upload_7_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_9E_7.HasFile)
        {
            if (this.fu_9E_7.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_9E_7.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("9E", this.lt_9E_name_7.Text, Path.GetFileName(this.fu_9E_7.PostedFile.FileName), data,
                    this.fu_9E_7.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_9E_7.Visible = false;
                    this.btn_9E_upload_7.Visible = false;
                    this.ltr_9E_uploaded_7.Text = "Uploaded";
                    this.ltr_9E_uploaded_7.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_9E_error_7.Text = "Too larger file";
                lbl_9E_error_7.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_9E_error_7.Text = "Select file";
            lbl_9E_error_7.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates(); 
    }

    protected void btn_9E_upload_8_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_9E_8.HasFile)
        {
            if (this.fu_9E_8.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_9E_8.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("9E", this.lt_9E_name_8.Text, Path.GetFileName(this.fu_9E_8.PostedFile.FileName), data,
                    this.fu_9E_8.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_9E_8.Visible = false;
                    this.btn_9E_upload_8.Visible = false;
                    this.ltr_9E_uploaded_8.Text = "Uploaded";
                    this.ltr_9E_uploaded_8.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_9E_error_8.Text = "Too larger file";
                lbl_9E_error_8.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_9E_error_8.Text = "Select file";
            lbl_9E_error_8.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates(); 
    }

    # endregion

    # region JMER button clicks

    protected void btn_JMER_upload_4_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_JMER_4.HasFile)
        {
            if (this.fu_JMER_4.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_JMER_4.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("JMER", this.lt_JMER_name_4.Text, Path.GetFileName(this.fu_JMER_4.PostedFile.FileName), data,
                    this.fu_JMER_4.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_JMER_4.Visible = false;
                    this.btn_JMER_upload_4.Visible = false;
                    this.ltr_JMER_uploaded_4.Text = "Uploaded";
                    this.ltr_JMER_uploaded_4.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_JMER_error_4.Text = "Too larger file";
                lbl_JMER_error_4.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_JMER_error_4.Text = "Select file";
            lbl_JMER_error_4.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates(); 
    }

    protected void btn_JMER_upload_5_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_JMER_5.HasFile)
        {
            if (this.fu_JMER_5.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_JMER_5.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("JMER", this.lt_JMER_name_5.Text, Path.GetFileName(this.fu_JMER_5.PostedFile.FileName), data,
                    this.fu_JMER_5.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_JMER_5.Visible = false;
                    this.btn_JMER_upload_5.Visible = false;
                    this.ltr_JMER_uploaded_5.Text = "Uploaded";
                    this.ltr_JMER_uploaded_5.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_JMER_error_5.Text = "Too larger file";
                lbl_JMER_error_5.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_JMER_error_5.Text = "Select file";
            lbl_JMER_error_5.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates(); 
    }

    protected void btn_JMER_upload_6_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_JMER_6.HasFile)
        {
            if (this.fu_JMER_6.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_JMER_6.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("JMER", this.lt_JMER_name_6.Text, Path.GetFileName(this.fu_JMER_6.PostedFile.FileName), data,
                    this.fu_JMER_6.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_JMER_6.Visible = false;
                    this.btn_JMER_upload_6.Visible = false;
                    this.ltr_JMER_uploaded_6.Text = "Uploaded";
                    this.ltr_JMER_uploaded_6.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_JMER_error_6.Text = "Too larger file";
                lbl_JMER_error_6.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_JMER_error_6.Text = "Select file";
            lbl_JMER_error_6.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates(); 
    }

    protected void btn_JMER_upload_7_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_JMER_7.HasFile)
        {
            if (this.fu_JMER_7.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_JMER_7.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("JMER", this.lt_JMER_name_7.Text, Path.GetFileName(this.fu_JMER_7.PostedFile.FileName), data,
                    this.fu_JMER_7.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_JMER_7.Visible = false;
                    this.btn_JMER_upload_7.Visible = false;
                    this.ltr_JMER_uploaded_7.Text = "Uploaded";
                    this.ltr_JMER_uploaded_7.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_JMER_error_7.Text = "Too larger file";
                lbl_JMER_error_7.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_JMER_error_7.Text = "Select file";
            lbl_JMER_error_7.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates(); 
    }

    protected void btn_JMER_upload_8_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_JMER_8.HasFile)
        {
            if (this.fu_JMER_8.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_JMER_8.PostedFile.InputStream;
                ds_recvd_docs.Tables[0].Rows.Add("JMER", this.lt_JMER_name_8.Text, Path.GetFileName(this.fu_JMER_8.PostedFile.FileName), data,
                    this.fu_JMER_8.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_JMER_8.Visible = false;
                    this.btn_JMER_upload_8.Visible = false;
                    this.ltr_JMER_uploaded_8.Text = "Uploaded";
                    this.ltr_JMER_uploaded_8.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_JMER_error_8.Text = "Too larger file";
                lbl_JMER_error_8.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_JMER_error_8.Text = "Select file";
            lbl_JMER_error_8.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates(); 
    }

    # endregion

    private void clearErrorMsgs()
    {
        lbl_15E_error_1.Text = "";
        lbl_15E_error_2.Text = "";
        lbl_15E_error_4.Text = "";
        lbl_15E_error_5.Text = "";
        lbl_15E_error_6.Text = "";
        lbl_15E_error_7.Text = "";
        lbl_15E_error_8.Text = "";

        lbl_12E_error_1.Text = "";
        lbl_12E_error_2.Text = "";
        lbl_12E_error_3.Text = "";
        lbl_12E_error_4.Text = "";
        lbl_12E_error_5.Text = "";
        lbl_12E_error_6.Text = "";
        lbl_12E_error_7.Text = "";

        lbl_RESQ_error_1.Text = "";
        lbl_RESQ_error_2.Text = "";
        lbl_RESQ_error_3.Text = "";
        lbl_RESQ_error_4.Text = "";
        lbl_RESQ_error_5.Text = "";
        lbl_RESQ_error_6.Text = "";
        lbl_RESQ_error_7.Text = "";

        lbl_9E_error_4.Text = "";
        lbl_9E_error_5.Text = "";
        lbl_9E_error_6.Text = "";
        lbl_9E_error_7.Text = "";
        lbl_9E_error_8.Text = "";

        lbl_JMER_error_4.Text = "";
        lbl_JMER_error_5.Text = "";
        lbl_JMER_error_6.Text = "";
        lbl_JMER_error_7.Text = "";
        lbl_JMER_error_8.Text = "";

        lbl_covid_error.Text = "";
    }

    // Updated 0n 21/08/2020

    protected void btn_covid_upload_Click(object sender, EventArgs e)
    {
        clearErrorMsgs();
        if (this.fu_covid.HasFile)
        {
            if (this.fu_covid.PostedFile.ContentLength < 1048576)
            {
                DataSet ds_recvd_docs = new DataSet();
                DataTable dt_recvd_docs = new DataTable();
                dt_recvd_docs.Columns.Add("doc_type", typeof(string));
                dt_recvd_docs.Columns.Add("person_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_name", typeof(string));
                dt_recvd_docs.Columns.Add("doc_data", typeof(Stream));
                dt_recvd_docs.Columns.Add("doc_app_type", typeof(string));
                ds_recvd_docs.Tables.Add(dt_recvd_docs);

                Stream data = this.fu_covid.PostedFile.InputStream;
                //ds_recvd_docs.Tables[0].Rows.Add("COVID19", this.lt_covid_name.Text, Path.GetFileName(this.fu_covid.PostedFile.FileName), data,this.fu_covid.PostedFile.ContentType);
                ds_recvd_docs.Tables[0].Rows.Add("COVID19", "ALL", Path.GetFileName(this.fu_covid.PostedFile.FileName), data, this.fu_covid.PostedFile.ContentType);

                Revival_Life revl = new Revival_Life();

                bool ok = revl.insert_uploaded_sigle_doc(ds_recvd_docs.Tables[0], Page.User.Identity.Name, this.ltrPolNo.Text.Trim());

                if (ok)
                {
                    this.fu_covid.Visible = false;
                    this.btn_covid_upload.Visible = false;
                    this.ltr_covid_uploaded.Text = "Uploaded";
                    this.ltr_covid_uploaded.Visible = true;
                    count_uploaded = count_uploaded + 1;

                    count_to_upload = (int)ViewState["count_to_upload"];
                    docRecvEmailSent = (bool)ViewState["docRecvEmailSent"];
                    //if (count_to_upload == count_uploaded)
                    //{
                    //    this.btnPay.Visible = true;
                    //}

                    if (!docRecvEmailSent && !this.btnPay.Visible)
                    {
                        Db_Email emailDB = new Db_Email();
                        bool emailSent = emailDB.send_html_email("phs@srilankainsurance.com", "Online Policy Revival", "", "Dear Life PHS, <br/><br/>" + ltrPHStatus.Text + this.ltrPHName.Text + " (Policy No:" + this.ltrPolNo.Text + ") has submitted some documents for the revival on " + DateTime.Today.ToString("yyyy/MM/dd") + ". <br/> Please check.  <br/> <br/> Thanking you,  <br/> Sri Lanka Insurance - Customer portal.");
                        docRecvEmailSent = true;
                        ViewState["docRecvEmailSent"] = docRecvEmailSent;
                    }
                }
            }
            else
            {
                lbl_covid_error.Text = "Too larger file";
                lbl_covid_error.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lbl_covid_error.Text = "Select file";
            lbl_covid_error.ForeColor = System.Drawing.Color.Red;
        }
        getViewStates();
    }

}