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
using System.Globalization;

public partial class General_Authorized_Products_PayReceipt_BOC : System.Web.UI.Page
{
    private string orderIdVal = "";
    private string successIndicator_ = "";
    private string sessionID = "";

    private string retCode = "";
    private string resCode = "";
    private string refNo = "";

    protected bool motorDept = false;
    protected bool globTrotter = false;
    protected bool amppolicy = false;
    protected bool travelprot = false;

    private ArrayList orderDataVal_Array = new ArrayList();

    private string[] serverResponse_Array;
    private string[] resultValue_Array;
    private string[] responseValue_Array;
    private bool boc_server_result = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            orderIdVal = Application["OrderID"].ToString();
            hdf_orderIdVal.Value = orderIdVal;
            getOrderDetails();

            hdf_resultIndicator.Value = Request.QueryString["resultIndicator"];

            boc_server_result = GetRequestFromServer();

            if (boc_server_result)
            {
                if (hdf_successIndicator.Value.Equals(hdf_resultIndicator.Value))
                {
                    //retCode = "1";
                    //resCode = "1";
                    refNo = hdf_orderIdVal.Value;

                    Properties properties = new Properties();


                    if (refNo.Contains("/999/"))
                    {
                        if (refNo.Contains("TPI") || (refNo.Contains("TPM")))
                        {
                            TRV_PolMast trvpolMast = new TRV_PolMast(refNo);
                            if (trvpolMast.status == "P")
                            {
                                if (retCode == "1" && resCode == "1")
                                {
                                    if (trvpolMast.update_paid_proposal(refNo, "A", retCode, resCode, trvpolMast.policy_no, User.Identity.Name))
                                    {
                                        trvpolMast = new TRV_PolMast(refNo);
                                        if (trvpolMast.status == "A")
                                        {
                                            if (trvpolMast.policy_no.Contains("GTP"))
                                            {
                                                travelprot = true;
                                                hyprPolSch.NavigateUrl = "/General/Authorized/Products/Documents/TRV_PB.pdf";
                                                hyprPolSch.Text = "Download Policy Booklet";
                                                btnPolDoc.Text = "Download Policy Schedule";

                                            }
                                            TRV_PolMast trv_polmast2 = new TRV_PolMast(trvpolMast.policy_no, refNo);
                                            litPolNumber.Text = trvpolMast.policy_no;
                                            litPropNo.Text = trvpolMast.refNo;
                                            litAmount.Text = litPremium.Text = (trv_polmast2.netprmrs + trv_polmast2.adminFee + trv_polmast2.polFee + trv_polmast2.VAT + trv_polmast2.NBT).ToString("N2");
                                            litPolType.Text = litPolType2.Text = trvpolMast.product_Name + " (Plan - " + trvpolMast.plan + ")";
                                            litSumAssurd.Text = trvpolMast.sumAssured.ToString("N2");
                                            litCustName.Text = trvpolMast.fullName;
                                            litAddress.Text = trvpolMast.getFullAddress();
                                            litCovPeriod.Text = trvpolMast.getCoverPeriod();
                                            litPayDate.Text = trvpolMast.entryDate;
                                            Panel1.Visible = true;


                                            #region Generate Cleint ID for Travel Protect custoemrs who had completed the payment for the policy
                                            //CustProfile cp = new CustProfile(User.Identity.Name);
                                            //TRV_Client Client=new TRV_Client();
                                            //Client.full_name = cp.O_firstName + " " + cp.O_lastName;
                                            //Client.last_name = cp.O_lastName;
                                            //Client.initials = cp.O_firstName;
                                            //Client.status = cp.O_title;
                                            //Client.passport_no = cp.O_passportNo;
                                            //Client.nic_no = cp.O_nicNo;
                                            //Client.dob = cp.O_dateOfBirth;
                                            //Client.profession = cp.O_ocupation;
                                            //Client.mobileno = cp.O_mobileNumber;
                                            //Client.home_add1 = cp.O_addrss1;
                                            //Client.home_add2 = cp.O_addrss2;
                                            //Client.home_add3 = cp.O_addrss3;
                                            //Client.home_add4 = cp.O_addrss4;
                                            //Client.UserID = cp.O_username;                                                 

                                            //bool clientgenerated = Client.GenerateClient(Client);
                                            //lblPayStatus.Text = Client.error;


                                            #endregion

                                            //if (clientgenerated)
                                            //{

                                            bool tpi_ret = trvpolMast.send_tpi_pay_receipt_email();
                                            if (tpi_ret)
                                            {
                                                lblPayStatus.Text = "Travel Protect Payment receipt details have been emailed to you.";
                                                lblPayStatus.ForeColor = System.Drawing.Color.Green;
                                            }
                                            //}
                                            //else
                                            //{
                                            //    //lblPayStatus.Text = "Travel Protect client error.";
                                            //    lblPayStatus.ForeColor = System.Drawing.Color.Green;
                                            //}
                                        }
                                        else
                                        {
                                            lblPayStatus.Text = "Sorry, Travel Protect Payment was not successful";
                                            log logger = new log();
                                            logger.write_log("Failed at PayReceipt_BOC-Pageload: Proposal success status not updated");
                                        }
                                    }
                                    else
                                    {
                                        lblPayStatus.Text = "Travel Protect Internal Error. Please contact SLIC " + trvpolMast.emesg;
                                        log logger = new log();
                                        logger.write_log("Failed at PayReceipt_BOC-Pageload: Proposal success pay not updated");
                                    }
                                }
                                else
                                {
                                    lblPayStatus.Text = "Sorry, Payment was not successful";
                                    log logger = new log();
                                    logger.write_log("Failed at PayReceipt_BOC-Pageload: Proposal success status not updated");
                                }
                            }
                            else
                            {
                                lblPayStatus.Text = "Internal Error in Travel Protect. Please contact SLIC";
                                log logger = new log();
                                logger.write_log("Failed at TPI PayReceipt_BOC-Pageload: Proposal Status Invalid");
                            }

                        }
                        else
                        {
                            Proposal prop = new Proposal(refNo);

                            if (prop.status == "P")
                            {
                                if (retCode == "1" && resCode == "1")
                                {

                                    if (prop.update_paid_proposal(refNo, "A", retCode, resCode))
                                    {
                                        prop = new Proposal(refNo);

                                        if (prop.status == "A")
                                        {
                                            if (refNo.Contains("G/999/GTI/"))
                                            {
                                                globTrotter = true;
                                                hyprPolSch.NavigateUrl = "/General/Authorized/Products/Documents/GTI.pdf";
                                                hyprPolSch.Text = "Download Policy Booklet";
                                                btnPolDoc.Text = "Download Policy Schedule";

                                            }
                                            else if (refNo.Contains("G/999/AMP/"))
                                            {
                                                amppolicy = true;
                                                hyprPolSch.NavigateUrl = "/General/Authorized/Products/Documents/AMP_Policy_book.pdf";
                                                hyprPolSch.Text = "Download Policy Booklet";
                                                btnPolDoc.Text = "Download Policy Schedule";
                                            }

                                            litPolNumber.Text = prop.policy_no;
                                            litPropNo.Text = prop.refNo;
                                            litAmount.Text = litPremium.Text = prop.annualPremium.ToString("N2");
                                            litPolType.Text = litPolType2.Text = prop.product_Name + " (Plan - " + prop.plan + ")";
                                            litSumAssurd.Text = prop.sumAssured.ToString("N2");
                                            litCustName.Text = prop.fullName;
                                            litAddress.Text = prop.getFullAddress();
                                            litCovPeriod.Text = prop.getCoverPeriod();
                                            litPayDate.Text = prop.entryDate;
                                            Panel1.Visible = true;

                                            bool ret = prop.send_pay_receipt_email();
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
                                            logger.write_log("Failed at PayReceipt_BOC-Pageload: Proposal success status not updated");
                                        }
                                    }
                                    else
                                    {
                                        lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                        log logger = new log();
                                        logger.write_log("Failed at PayReceipt_BOC-Pageload: Proposal success pay not updated");
                                    }
                                }
                                else
                                {
                                    if (prop.update_paid_proposal(refNo, "F", retCode, resCode))
                                    {
                                        lblPayStatus.Text = "Sorry, Payment was not successful";
                                        lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                        log logger = new log();
                                        logger.write_log("Failed at PayReceipt_BOC-Pageload: Proposal pay failure not updated");
                                    }
                                }
                            }
                            else
                            {
                                lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                log logger = new log();
                                logger.write_log("Failed at PayReceipt_BOC-Pageload: Proposal Status Invalid");
                            }
                        }
                    }
                    else if (refNo.Contains("/R/") || refNo.Contains("/D/"))
                    {
                        Renewal renw = new Renewal(refNo);

                        if (renw.status == "P")
                        {
                            if (retCode == "1" && resCode == "1")
                            {
                                if (renw.update_paid_renewal(refNo, "A", retCode, resCode))
                                {
                                    renw = new Renewal(refNo);

                                    if (renw.status == "A")
                                    {
                                        if (renw.dept == "M")
                                        {
                                            bool retVal = renw.updateRevLicenDetails();
                                            if (retVal == false)
                                            {
                                                log logger = new log();
                                                logger.write_log("Failed at PayReceipt_BOC-Pageload: Revenue License details not updated");
                                            }

                                            Button2.Visible = true;

                                            DateTime today = DateTime.Now;

                                            CustProfile cp = new CustProfile(Page.User.Identity.Name);
                                            string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];


                                            Covernote_Info ci = new Covernote_Info(renw.polNum.Trim());

                                            DateTime pol_end_date = Convert.ToDateTime(ci.policy_end_date);
                                            DateTime effective_date = new DateTime();
                                            string com_time = "";

                                            if (pol_end_date > today)
                                            {
                                                effective_date = pol_end_date;
                                                com_time = "00:00";
                                            }
                                            else
                                            {
                                                effective_date = today;
                                                com_time = DateTime.Now.ToString("HH:mm");
                                            }
                                            DateTime expire_date = effective_date.AddDays(15);

                                            string pendingReason = "Pending Certificate#Luxury/Semi Luxury Tax if Applicable.";

                                            Covernote cn = new Covernote();
                                            HdnCoNum.Value = cn.insertCN(999, effective_date.ToString("yyyy-MM-dd"), 15, expire_date.ToString("yyyy-MM-dd"), ci.Cus_name, cp.O_title, "", renw.receiptNo, Page.User.Identity.Name, ip, "", "", "M", "", "", 0, "N", ci.address1, ci.address2, ci.address3, ci.address4, "R", " ", " ", renw.polNum, "", com_time, renw.vehiNum, "", ci.cylinder_capacity, "NA", "Comprehensive Policy", "NA", "", ci.SA, ci.rcc_tag, ci.tc_tag, "Comprehensive", ci.province, pendingReason, renw.amount, DateTime.Today.ToString("yyyy-MM-dd"), "N", cp.O_mobileNumber);

                                            if (HdnCoNum.Value.Length >= 11)
                                            {
                                                Button2.Visible = true;
                                                cn.cover_note_email(HdnCoNum.Value, Page.User.Identity.Name, ip, false, cp.O_email);
                                            }

                                        }
                                        litRefNo.Text = renw.receiptNo;
                                        litAmount2.Text = litRnPremium.Text = renw.amount.ToString("N2");
                                        litPolTyp2.Text = renw.polTypName;
                                        litRnPolNum.Text = renw.polNum;
                                        litRnSumAssurd.Text = renw.sumAssurd.ToString("N2");
                                        litRnCusNam.Text = renw.custName;
                                        litAddress2.Text = renw.address;
                                        litRnCovPeriod.Text = "From: " + renw.startDate + " to: " + renw.endDate;
                                        litPayDate2.Text = renw.entryDate;

                                        if (renw.dept == "M")
                                        {
                                            litVehiNum.Text = renw.vehiNum;
                                            motorDept = true;
                                        }
                                        if (DateTime.Now <= DateTime.ParseExact(renw.startDate, "yyyy/MM/dd", CultureInfo.InvariantCulture))
                                        {
                                            lblNoClmMesg.Visible = true;
                                        }
                                        Panel2.Visible = true;

                                        bool ret = renw.send_pay_receipt_email();
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
                                        logger.write_log("Failed at PayReceipt_BOC-Pageload: Renewal success status not updated");
                                    }
                                }
                                else
                                {
                                    lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                    log logger = new log();
                                    logger.write_log("Failed at PayReceipt_BOC-Pageload: Renewal success pay not updated");
                                }
                            }
                            else
                            {
                                if (renw.update_paid_renewal(refNo, "F", retCode, resCode))
                                {
                                    lblPayStatus.Text = "Sorry, Payment was not successful";
                                    lblPayStatus.ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    lblPayStatus.Text = "Internal Error. Please contact SLIC";
                                    log logger = new log();
                                    logger.write_log("Failed at PayReceipt_BOC-Pageload: Renewal pay failure not updated");
                                }
                            }
                        }
                        else
                        {
                            lblPayStatus.Text = "Internal Error. Please contact SLIC";
                            log logger = new log();
                            logger.write_log("Failed at PayReceipt_BOC-Pageload: Renewal Status Invalid");
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

    private bool GetRequestFromServer()
    {
        bool serverReult = false;

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

        //Test
        ////var postParams = new List<KeyValuePair<string, string>>(){
        ////            new KeyValuePair<string, string>("apiOperation", "RETRIEVE_ORDER"),
        ////            new KeyValuePair<string, string>("order.id",hdf_orderIdVal.Value),
        ////            new KeyValuePair<string, string>("merchant","700163000047"),
        ////            new KeyValuePair<string, string>("apiPassword", "e2b54c964c3603bef13f043cd202def6"),
        ////            new KeyValuePair<string, string>("apiUsername", "Merchant.700163000047")

                
                   
        ////        };



        //Live
        var postParams = new List<KeyValuePair<string, string>>(){
                    new KeyValuePair<string, string>("apiOperation", "RETRIEVE_ORDER"),
                    new KeyValuePair<string, string>("order.id",hdf_orderIdVal.Value),
                    new KeyValuePair<string, string>("merchant","700163000047"),
                    new KeyValuePair<string, string>("apiPassword", "85dced7bcb339e506e885ad14a1ad3b4"),
                    new KeyValuePair<string, string>("apiUsername", "Merchant.700163000047")



                };

        ////Join KVPs into a x-www-formurlencoded string
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
                    //break;
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
        //Return value
        return serverReult;
    }

    private void getOrderDetails()
    {

        BOC_Payment orderData_obj_ = new BOC_Payment();
        orderDataVal_Array = orderData_obj_.get_General_Order_Details(hdf_orderIdVal.Value);

        sessionID = orderDataVal_Array[0].ToString();
        hdf_SessionID.Value = sessionID;
        successIndicator_ = orderDataVal_Array[1].ToString();
        hdf_successIndicator.Value = successIndicator_;


    }

    protected void btnReceipt_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        Proposal pro = new Proposal(litPropNo.Text.Trim());
        Print_pdf pdf = new Print_pdf();
        pdf.print_receipt(pro, ip, User.Identity.Name);
    }

    protected void btnPolDoc_Click(object sender, EventArgs e)
    {
        EncryptDecrypt en = new EncryptDecrypt();

        if (litPropNo.Text.Contains("G/999/GTI/"))
        {
            btnPolDoc.Text = "Download Policy Document";

            Dictionary<string, string> dc = new Dictionary<string, string>();
            dc.Add("refN0", litPropNo.Text.Trim());
            dc.Add("P0lNo", litPropNo.Text.Trim());
            string link2 = en.url_encrypt("/General/Authorized/Quotation_Reprint.aspx", dc);

            Response.Redirect(link2);


        }
        else if (litPropNo.Text.Contains("G/999/AMP/"))
        {


            btnPolDoc.Text = "Download Proposal Document";

            Dictionary<string, string> dc = new Dictionary<string, string>();
            dc.Add("refN0", litPropNo.Text.Trim());
            dc.Add("SA", litSumAssurd.Text.Trim());
            string link2 = en.url_encrypt("/General/Authorized/Quotation_Reprint.aspx", dc);

            Response.Redirect(link2);

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        Renewal rnwl = new Renewal(litRefNo.Text.Trim());
        Print_pdf pdf = new Print_pdf();
        pdf.print_receipt(rnwl, ip, User.Identity.Name);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (HdnCoNum.Value.Length >= 11)
        {
            EncryptDecrypt en = new EncryptDecrypt();
            Dictionary<string, string> dc = new Dictionary<string, string>();

            dc.Add("CrefNo", HdnCoNum.Value);
            string link = en.url_encrypt("/General/Authorized/Covernote_print.aspx", dc);
            Response.Redirect(link);
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        if (litPropNo.Text.Contains("G/999/GTI/"))
        {
            hyprPolSch.Text = "Download Policy Schedule";
            hyprPolSch.NavigateUrl = " ";
        }
        else if (litPropNo.Text.Contains("G/999/AMP/"))
        {
            hyprPolSch.Text = "Download Policy Booklet";
            hyprPolSch.NavigateUrl = "/General/Authorized/Products/Documents/AMP_Policy_book.pdf";
        }
        else if (litPropNo.Text.Contains("G/999/TPI/"))
        {
            hyprPolSch.Text = "Download Policy Booklet1";
            hyprPolSch.NavigateUrl = "/General/Authorized/Products/Documents/Travel_Policy.pdf";
        }
    }
}