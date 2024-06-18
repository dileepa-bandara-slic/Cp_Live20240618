using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Net;
using System.Text;
using System.IO;

public partial class Life_Authorized_Products_PayReceipt_BOC : System.Web.UI.Page
{
    private string orderIdVal = "";
    private string successIndicator_ = "";
    private string sessionID = "";

    private string refNo = "";
    private string retCode = "";
    private string resCode = "";
    protected bool motorDept = false;
    private ArrayList orderDataVal_Array = new ArrayList();

    private string[] serverResponse_Array;   
    private string[] resultValue_Array;
    private bool boc_server_result = false;

    private string[] responseValue_Array;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            orderIdVal = Application["OrderID"].ToString();
            hdf_orderIdVal.Value = orderIdVal;
            
            getOrderDetails();

            hdf_resultIndicator.Value = Request.QueryString["resultIndicator"];

            boc_server_result= GetRequestFromServer();

            if (boc_server_result)
            {


                if (hdf_successIndicator.Value.Equals(hdf_resultIndicator.Value))
                {
                    //retCode = "1";
                    //resCode = "1";

                    lblPayStatus.Text = "Payment was Successful";
                    refNo = hdf_orderIdVal.Value;
                    if (refNo.Contains("/P/"))
                    {
                        LifePayment premPayment = new LifePayment(refNo);

                        if (premPayment.status == "P")
                        {
                            if (retCode == "1" && resCode == "1")
                            {
                                if (premPayment.update_paid_renewal(refNo, "A", retCode, resCode))
                                {
                                    premPayment = new LifePayment(refNo);

                                    if (premPayment.status == "A")
                                    {
                                        litPremRefNo.Text = premPayment.receiptNo;
                                        litAmount.Text = premPayment.amount.ToString("N2");
                                        litPolNo.Text = litPolNum.Text = premPayment.polNum;
                                        litCustName.Text = premPayment.custName;
                                        litTotDueAmt.Text = premPayment.duesAmt.ToString("N2");
                                        litDeposits.Text = premPayment.deposits.ToString("N2");
                                        litPaidDuesAmt.Text = premPayment.paidDuesAmt.ToString("N2");
                                        litAddtAmt.Text = premPayment.addtAmt.ToString("N2");
                                        if (premPayment.dsPaidDues != null)
                                        {
                                            gvDemands.DataSource = premPayment.dsPaidDues.Tables[0];
                                            gvDemands.DataBind();
                                            if (premPayment.dsPaidDues.Tables[0].Rows.Count > 0)
                                            {
                                                gvDemands.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                                                gvDemands.HeaderRow.TableSection = TableRowSection.TableHeader;
                                            }
                                        }
                                        litPayDate.Text = premPayment.entryDate;
                                        Panel1.Visible = true;

                                        bool ret = premPayment.send_pay_receipt_email();
                                        if (ret)
                                        {
                                            lblPayStatus.Text = "Confirmation of payment has been emailed to you.";
                                            lblPayStatus.ForeColor = System.Drawing.Color.Green;
                                        }
                                    }
                                    else
                                    {
                                        lblPayStatus.Text = "Sorry, Payment was not successful";
                                        log logger = new log();
                                        logger.write_log("Failed at PayReceipt_BOC-Pageload: Premium Pay success status not updated");
                                    }
                                }
                                else
                                {
                                    lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                    log logger = new log();
                                    logger.write_log("Failed at PayReceipt_BOC-Pageload: Premium Pay success pay not updated");
                                }
                            }
                            else
                            {
                                if (premPayment.update_paid_renewal(refNo, "F", retCode, resCode))
                                {
                                    lblPayStatus.Text = "Sorry, Payment was not successful";
                                    lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                    log logger = new log();
                                    logger.write_log("Failed at PayReceipt_BOC-Pageload: Premium Pay failure not updated");
                                }
                            }
                        }
                        else
                        {
                            lblPayStatus.Text = "Internal Error. Please contact SLIC";
                            log logger = new log();
                            logger.write_log("Failed at PayReceipt_BOC-Pageload: Premium Pay Status Invalid");
                        }
                    }

                    else if (refNo.Contains("/L/"))
                    {
                        LifePayment loanPaymnt = new LifePayment(refNo);

                        if (loanPaymnt.status == "P")
                        {
                            if (retCode == "1" && resCode == "1")
                            {

                                if (loanPaymnt.update_paid_renewal(refNo, "A", retCode, resCode))
                                {
                                    loanPaymnt = new LifePayment(refNo);

                                    if (loanPaymnt.status == "A")
                                    {
                                        litLoanRefNo.Text = loanPaymnt.receiptNo;
                                        litAmount2.Text = loanPaymnt.amount.ToString("N2");
                                        litLoanNo.Text = loanPaymnt.loanNo;
                                        litPolNo2.Text = litRnPolNum.Text = loanPaymnt.polNum;
                                        litLoanCusNam.Text = loanPaymnt.custName;
                                        litPayDate2.Text = loanPaymnt.entryDate;

                                        Panel2.Visible = true;

                                        bool ret = loanPaymnt.send_pay_receipt_email();
                                        if (ret)
                                        {
                                            lblPayStatus.Text = "Payment receipt details have been emailed to you.";
                                            lblPayStatus.ForeColor = System.Drawing.Color.Green;
                                        }
                                    }
                                    else
                                    {
                                        lblPayStatus.Text = "Sorry, Payment was not successful";
                                        log logger = new log();
                                        logger.write_log("Failed at PayReceipt_BOC-Pageload: Loan pay success status not updated");
                                    }
                                }
                                else
                                {
                                    lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                    log logger = new log();
                                    logger.write_log("Failed at PayReceipt_BOC-Pageload: Loan pay success pay not updated");
                                }
                            }
                            else
                            {
                                if (loanPaymnt.update_paid_renewal(refNo, "F", retCode, resCode))
                                {
                                    lblPayStatus.Text = "Sorry, Payment was not successful";
                                    lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                    log logger = new log();
                                    logger.write_log("Failed at PayReceipt_BOC-Pageload: Loan pay failure not updated");
                                }
                            }

                        }
                        else
                        {
                            lblPayStatus.Text = "Internal Error. Please contact SLIC";
                            log logger = new log();
                            logger.write_log("Failed at PayReceipt_BOC-Pageload: Loan pay Status Invalid");
                        }
                    }
                    /**************Updated***********************/
                    else if (refNo.Contains("/R/"))
                    {
                        LifePayment premPayment = new LifePayment(refNo);

                        if (premPayment.status == "P")
                        {
                            if (retCode == "1" && resCode == "1")
                            {
                                if (premPayment.update_paid_renewal(refNo, "A", retCode, resCode))
                                {
                                    premPayment = new LifePayment(refNo);

                                    if (premPayment.status == "A")
                                    {
                                        //litPremRefNo.Text = premPayment.receiptNo;
                                        //litAmount.Text = premPayment.amount.ToString("N2");
                                        //litPolNo.Text = litPolNum.Text = premPayment.polNum;
                                        //litCustName.Text = premPayment.custName;
                                        ltrRefNo_rev.Text = premPayment.receiptNo;
                                        ltrPayAmount_rev.Text = premPayment.amount.ToString("N2");
                                        ltrPolNo_rev.Text = premPayment.polNum;
                                        ltrCustomerName_rev.Text = premPayment.custName;
                                        ltrPaidAmount_rev.Text = "Rs." + premPayment.amount.ToString("N2");

                                        CustProfile customer = new CustProfile(premPayment.username);
                                        ltrAdrsName_rev.Text = premPayment.custName;
                                        ltrAdress1_rev.Text = customer.O_addrss1;
                                        ltrAdress2_rev.Text = customer.O_addrss2;
                                        ltrAdress3_rev.Text = customer.O_addrss3;
                                        ltrAdress4_rev.Text = customer.O_addrss4;

                                        //litTotDueAmt.Text = premPayment.duesAmt.ToString("N2");
                                        //litDeposits.Text = premPayment.deposits.ToString("N2");
                                        //litPaidDuesAmt.Text = premPayment.paidDuesAmt.ToString("N2");
                                        //litAddtAmt.Text = premPayment.addtAmt.ToString("N2");
                                        //if (premPayment.dsPaidDues != null)
                                        //{
                                        //    gvDemands.DataSource = premPayment.dsPaidDues.Tables[0];
                                        //    gvDemands.DataBind();
                                        //    if (premPayment.dsPaidDues.Tables[0].Rows.Count > 0)
                                        //    {
                                        //        gvDemands.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                                        //        gvDemands.HeaderRow.TableSection = TableRowSection.TableHeader;
                                        //    }
                                        //}
                                        this.ltrPayDate_rev.Text = premPayment.entryDate;
                                        this.ltrPayTime_rev.Text = DateTime.Now.ToString("HH:mm:ss");
                                        Panel3.Visible = true;

                                        bool ret = premPayment.send_pay_receipt_email();
                                        if (ret)
                                        {
                                            bool ret2 = premPayment.send_pay_phs_email();
                                            lblPayStatus.Text = "Confirmation of payment has been emailed to you.";
                                            lblPayStatus.ForeColor = System.Drawing.Color.Green;
                                        }
                                    }
                                    else
                                    {
                                        lblPayStatus.Text = "Sorry, Payment was not successful1";
                                        log logger = new log();
                                        logger.write_log("Failed at PayReceipt-Pageload: Premium Pay success status not updated");
                                    }
                                }
                                else
                                {
                                    lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                    log logger = new log();
                                    logger.write_log("Failed at PayReceipt-Pageload: Premium Pay success pay not updated");
                                }
                            }
                            else
                            {
                                if (premPayment.update_paid_renewal(refNo, "F", retCode, resCode))
                                {
                                    lblPayStatus.Text = "Sorry, Payment was not successful";
                                    lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                    log logger = new log();
                                    logger.write_log("Failed at PayReceipt-Pageload: Premium Pay failure not updated");
                                }
                            }
                        }
                        else
                        {
                            lblPayStatus.Text = "Internal Error. Please contact SLIC";
                            log logger = new log();
                            logger.write_log("Failed at PayReceipt-Pageload: Premium Pay Status Invalid");
                        }
                    }

                }
                else
                {
                    lblPayStatus.Text = "Payment was not Successful";
                    log logger = new log();
                    logger.write_log("Failed at PayReceipt_BOC-Pageload: Not Matching SuccessIndicator and ResultIndicator");
                }
            }
            else {
                lblPayStatus.Text = "Payment was not Successful";
                log logger = new log();
                logger.write_log("Failed at PayReceipt_BOC-Pageload: Payment Failure Message From BOC Server");
            
            }

        }
    }

    private void getOrderDetails()
    {

        BOC_Payment orderData_obj_ = new BOC_Payment();
        orderDataVal_Array = orderData_obj_.get_Order_Details(hdf_orderIdVal.Value);

        sessionID = orderDataVal_Array[0].ToString();
        hdf_SessionID.Value = sessionID;
        successIndicator_ = orderDataVal_Array[1].ToString();
        hdf_successIndicator.Value = successIndicator_;


    }

    public bool GetRequestFromServer()
    {
        bool serverReult =false;

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;


        // Create a request using a URL that can receive a post.   

        ////Test
        //WebRequest request = WebRequest.Create("https://test-bankofceylon.mtf.gateway.mastercard.com/api/nvp/version/40");

        //Live
        WebRequest request = WebRequest.Create("https://bankofceylon.gateway.mastercard.com/api/nvp/version/50");

        
        // Set the Method property of the request to POST.  
        request.Method = "POST";

        // Create POST data and convert it to a byte array.
        ////Test
        ////var postParams = new List<KeyValuePair<string, string>>(){
        ////            new KeyValuePair<string, string>("apiOperation", "RETRIEVE_ORDER"),
        ////            new KeyValuePair<string, string>("order.id",hdf_orderIdVal.Value),
        ////            new KeyValuePair<string, string>("merchant","700163000034"),
        ////            new KeyValuePair<string, string>("apiPassword", "d5eddfc9726780cca272b3cd91563567"),
        ////            new KeyValuePair<string, string>("apiUsername", "Merchant.700163000034")
                                
                
                   
        ////        };

        //Live
        var postParams = new List<KeyValuePair<string, string>>(){
                    new KeyValuePair<string, string>("apiOperation", "RETRIEVE_ORDER"),
                    new KeyValuePair<string, string>("order.id",hdf_orderIdVal.Value),
                    new KeyValuePair<string, string>("merchant","700163000034"),
                    new KeyValuePair<string, string>("apiPassword", "ec1152ff4b59805ec6c441fa9ed8e37f"),
                    new KeyValuePair<string, string>("apiUsername", "Merchant.700163000034")



                };


        //Join KVPs into a x-www-formurlencoded string
        string postData = string.Join("&", postParams.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
        //string postData = "Merchant Object ([proxyServer:Merchant:private] =>[proxyAuth:Merchant:private] =>[proxyCurlOption:Merchant:private] => 0[proxyCurlValue:Merchant:private] => 0[certificatePath:Merchant:private] =>[certificateVerifyPeer:Merchant:private] =>[certificateVerifyHost:Merchant:private] => 0[gatewayUrl:Merchant:private] => https://test-bankofceylon.mtf.gateway.mastercard.com/api/nvp[debug:Merchant:private] => 1[version:Merchant:private] => 40[merchantId:Merchant:private] => 700163000034[password:Merchant:private] => d5eddfc9726780cca272b3cd91563567[apiUsername:Merchant:private] => Merchant.700163000034 ) apiOperation=CREATE_CHECKOUT_SESSION&order.id=DYG9FtrkXT&order.amount=5000&order.currency=LKR&order.reference=YqgSpnKj7E&merchant=700163000034&apiPassword=d5eddfc9726780cca272b3cd91563567&apiUsername=Merchant.700163000034SESSION0002241824372H1047747K38";

        //string postData = "This is a test that posts this string to a Web server.";
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        // Set the ContentType property of the WebRequest.  
        request.ContentType = "application/x-www-form-urlencoded";
        // Set the ContentLength property of the WebRequest.  
        request.ContentLength = byteArray.Length;

        // Get the request stream.  
        Stream dataStream = request.GetRequestStream();
        // Write the data to the request stream.  
        dataStream.Write(byteArray, 0, byteArray.Length);
        // Close the Stream object.  
        dataStream.Close();

        // Get the response.  
        WebResponse response = request.GetResponse();

        // Get the stream containing content returned by the server.  
        // The using block ensures the stream is automatically closed.
        using (dataStream = response.GetResponseStream())
        {
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            serverResponse_Array = responseFromServer.Split('&');
            foreach (string param_Values in serverResponse_Array)
            {
                if (param_Values.Contains("result"))
                {
                    string resultFromServer = param_Values;
                    resultValue_Array = resultFromServer.Split('=');
                    foreach (string serverResultvalue in resultValue_Array)
                    {
                        if (serverResultvalue.Contains("SUCCESS"))
                        {
                            serverReult = true;
                        }
                       
                  
                    }
                    //////break;
                    
                }
                else if (param_Values.Contains("response.gatewayCode"))
                {
                    string responseParamFromServer = param_Values;
                    responseValue_Array = responseParamFromServer.Split('=');
                    foreach (string serverResponsevalue in responseValue_Array)
                    {
                        if (serverResponsevalue.Contains("APPROVED"))
                        {
                            retCode = "1";
                            resCode = "1";
                        }
                        else if (serverResponsevalue.Contains("PARTIALLY_APPROVED"))
                        {
                            retCode = "2";
                            resCode = "2";
                        }
                        else if (serverResponsevalue.Contains("PENDING_RESPONSE"))
                        {
                            retCode = "3";
                            resCode = "3";
                        }

                        else if (serverResponsevalue.Contains("PENDING_SETTLEMENT"))
                        {
                            retCode = "4";
                            resCode = "4";
                        }
                        else if (serverResponsevalue.Contains("DECLINED(NO_ENTRY)"))
                        {
                            retCode = "5";
                            resCode = "5";
                        }
                        else if (serverResponsevalue.Contains("DECLINED(RETRIABLE)"))
                        {
                            retCode = "6";
                            resCode = "6";
                        }
                        else if (serverResponsevalue.Contains("REFER_TO_ISSUER"))
                        {
                            retCode = "7";
                            resCode = "7";
                        }
                        else if (serverResponsevalue.Contains("CANCELLED"))
                        {
                            retCode = "8";
                            resCode = "8";
                        }
                        else if (serverResponsevalue.Contains("BLOCKED"))
                        {
                            retCode = "9";
                            resCode = "9";
                        }
                        else if (serverResponsevalue.Contains("TECHNICAL_ERROR"))
                        {
                            retCode = "10";
                            resCode = "10";
                        }
                        else if (serverResponsevalue.Contains("NOT_SUPPORTED"))
                        {
                            retCode = "11";
                            resCode = "11";
                        }
                        else if (serverResponsevalue.Contains("AUTHENTICATION_IN_PROGRESS"))
                        {
                            retCode = "12";
                            resCode = "12";
                        }
                    }
                    break;

                }
           

            }


        }

        // Close the response.  
        response.Close();
        //return result of the server
        return serverReult;
       
    }

    protected void btnReceipt_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        LifePayment pro = new LifePayment(litPremRefNo.Text.Trim());
        Print_pdf_Life pdf = new Print_pdf_Life();
        pdf.print_receipt(pro, ip, User.Identity.Name, gvDemands);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        LifePayment rnwl = new LifePayment(litLoanRefNo.Text.Trim());
        Print_pdf_Life pdf = new Print_pdf_Life();
        pdf.print_receipt_Loan(rnwl, ip, User.Identity.Name);
    }


    protected void btnPDF_rev_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        LifePayment revival = new LifePayment(this.ltrRefNo_rev.Text.Trim());
        Print_pdf_Life pdf = new Print_pdf_Life();
        pdf.print_receipt_Revival(revival, ip, User.Identity.Name);
    }
}