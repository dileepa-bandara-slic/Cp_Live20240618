using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class General_Authorized_Renewal : System.Web.UI.Page
{
    EncryptDecrypt enc = new EncryptDecrypt();
    public string errorMsg { get; set; }
    protected string dept = "";
    protected string polType = "";
    protected double debitBalAmt = 0;
    protected double partialAmt = 0;
    Renewal renwl = new Renewal();

    protected void Page_Load(object sender, EventArgs e)
    {
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

                    if (paraList.ContainsKey("PolicyNo") && paraList.ContainsKey("EndDate") && paraList.ContainsKey("PolicyTyp"))
                    {
                        string polNo = paraList["PolicyNo"];
                        string endDate = paraList["EndDate"];
                        string pmtyp = paraList["pmtyp"];
                        dept = paraList["Dept"];
                        polType = paraList["PolicyTyp"];
                        ViewState["Dept"] = dept;
                        ViewState["PolType"] = polType;

                        ULCustomer customer = new ULCustomer();
                        string insurdName = "";
                        string addrss = "";
                        string vehiNum = "";
                        string startDt = "";
                        string endDt = "";
                        double sumInsrd = 0;
                        string basicPrem = "";
                        string rccPrem = "";
                        string tcPrem = "";
                        string vatAmt = "";
                        string admnAmt = "";
                        string stampFee = "";
                        string polFee = "";
                        string nbtVal = "";
                        double totPremium = 0;
                        bool payDisable = true;
                        string roadTax = "";
                        string warnMsg = "";
                        DataSet dsCovers = null;
                        Int64 refNo = 0;

                        string message = customer.getRenewalInfo(polNo, endDate, dept, polType, pmtyp, out insurdName, out addrss, out vehiNum, out startDt,
                                         out endDt, out sumInsrd, out basicPrem, out rccPrem, out tcPrem, out vatAmt, out admnAmt, out stampFee,
                                         out polFee, out nbtVal, out roadTax, out totPremium, out debitBalAmt, out partialAmt, out payDisable, out warnMsg, out dsCovers, out refNo);


                        if (message != "success" && !payDisable)
                        {
                            lblErrMesg.Text = message;
                            Session["errMesg"] = message;
                            Server.Transfer("Epage.aspx");
                        }
                        else
                        {
                            litPayRefNo.Text = (refNo == 0 ? "NA":refNo.ToString());

                            litPolNum.Text = polNo;
                            litInsured.Text = insurdName;
                            litAddress.Text = addrss;
                            litVehiNum.Text = vehiNum;
                            litStartDate.Text = startDt;
                            if (dept == "M")
                            {
                                litEndDate.Text = endDt;
                            }
                            ViewState["EndDate"] = endDt;
                            litSumInsurd.Text = sumInsrd.ToString("N2");
                            ViewState["SumInsurrd"] = sumInsrd;
                            litBasicPrem.Text = basicPrem;
                            litRccPrem.Text = rccPrem;
                            litTcPrem.Text = tcPrem;
                            litVatAmt.Text = vatAmt;
                            litAdmnAmt.Text = admnAmt;
                            litStampFee.Text = stampFee;
                            litPolFee.Text = polFee;
                            litNbtVal.Text = nbtVal;
                            litRdTax.Text = roadTax;
                            litTotPrem.Text = totPremium.ToString("N2");
                            if (partialAmt > 0)
                            {
                                txtAmount.Text = partialAmt.ToString("N2");
                            }
                            if (debitBalAmt > 0)
                            {
                                txtAmount.Text = debitBalAmt.ToString("N2");
                                //txtAmount.Enabled = false;
                            }
                            ViewState["TotPremium"] = totPremium;
                            ViewState["debitBalAmt"] = debitBalAmt;
                            ViewState["partialAmt"] = partialAmt;

                            if (dept == "M")
                            {
                                if (dsCovers.Tables.Count > 0)
                                {
                                    for (int i = 0; i < dsCovers.Tables[0].Rows.Count; i++)
                                    {
                                        litCovers.Text = litCovers.Text + dsCovers.Tables[0].Rows[i]["COVER"].ToString() + "<br/>";
                                    }
                                }

                            }

                            if (payDisable)
                            {
                                btnPay.Enabled = false;
                                lblErrMesg.Text = message;
                                if (dept != "M" && polType != "HIP")
                                {
                                    txtAmount.Enabled = false;
                                }
                            }
                            if (warnMsg != "")
                            {
                                lblWrnMsg.Text = warnMsg;
                            }
                        }
                    }
                    else
                    {
                        errorMsg = "No valid Parameters Passed";
                        Server.Transfer("ErrorPage.aspx");
                    }
                }
            }
        }
        else
        {
            dept = ViewState["Dept"].ToString();
            polType = ViewState["PolType"].ToString();
        }
    }

    protected void btnPay_Click(object sender, EventArgs e)
    {
        lblErrMesg.Text = "";
        dept = ViewState["Dept"].ToString();
        polType = ViewState["PolType"].ToString();

        double debitBalAmt = Convert.ToDouble(ViewState["debitBalAmt"]);
        double partialAmt = Convert.ToDouble(ViewState["partialAmt"]);

        string recptNo = "";
        if (debitBalAmt > 0)
        {
            recptNo = renwl.generate_renwReceiptNo(dept, Convert.ToInt32(DateTime.Today.ToString("yyyy")), polType, true);
        }
        else
        {
            recptNo = renwl.generate_renwReceiptNo(dept, Convert.ToInt32(DateTime.Today.ToString("yyyy")), polType);
        }
        
        if (!String.IsNullOrEmpty(recptNo))
        {
            string endDate = ViewState["EndDate"].ToString();
            double sumInsurrd = Convert.ToDouble(ViewState["SumInsurrd"]);
            double premium = Convert.ToDouble(ViewState["TotPremium"]);


            if ((dept != "M" || (dept == "M" && (debitBalAmt > 0 || partialAmt > 0))) && polType != "HIP" && polType != "AMP")
            {
                if (txtAmount.Visible == true)
                {
                    try
                    {
                        premium = double.Parse(txtAmount.Text.Trim());
                    }
                    catch
                    {
                        lblErrMesg.Text = "Invalid amount";
                    }
                }
                else
                {
                    lblErrMesg.Text = "An error occured, please resubmit with correct details";
                }
            }
            if (lblErrMesg.Text == "")
            {
                string vehiNum = "";
                if (dept == "M")
                {
                    vehiNum = litVehiNum.Text.Trim();
                }
                if (renwl.insert_renewal(litPolNum.Text, dept, polType, premium, Page.User.Identity.Name, "P", "",
                                    litStartDate.Text.Trim(), endDate, recptNo, litInsured.Text.Trim(), sumInsurrd, litAddress.Text.Trim(), vehiNum))
                {
                    //Lit_msg.Text = "Should be navigated to the next page. Proposal id : " + proid;

                    EncryptDecrypt dc = new EncryptDecrypt();
                    Dictionary<string, string> qs = new Dictionary<string, string>();
                    qs.Add("Pol_No", litPolNum.Text.Trim());
                    qs.Add("Ref_No", recptNo.Trim());
                    qs.Add("Type", "R"); // N-new businees, R-renewals
                    Response.Redirect(dc.url_encrypt("Products/Payment.aspx", qs));
                }
                else
                {
                    lblErrMesg.Text = "An error occured, please resubmit with correct details";
                }
            }

        }
        else
        {
            lblErrMesg.Text = "An error occured, please resubmit with correct details";
        }
    }


}