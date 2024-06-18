using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.IO;

public partial class Life_Authorized_Products_BOC_PaymentForm : System.Web.UI.Page
{

    //Merchant Related Variables
    private string version = "";    

    //Order Related Variables
    public string customer_receipt_email = ""; 


    //Response related variales
    private string responseParamterString = "";
    private string[] splittedParamsList;
    private string sessionIDValues = "";
    private string[] splittedSessionIDValue_List;
    private string[] splittedParamsList_successIndicator;
    private string[] splittedParamsList_result;
    private bool result = false;   
   

    private int count_PageLoad = 0;

    //Encrypt Data
    EncryptDecrypt dc = new EncryptDecrypt();
    public string errorMsg { get; set; }
    EncryptDecrypt enc = new EncryptDecrypt();
    string strReq = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Get the Count of Page loading to prevent loading in postback
            count_PageLoad = Convert.ToInt32(ViewState["session_key"]);
            if (count_PageLoad == 0)
            {
                //////Merchant Related Variables
                ////hdf_merchantId.Value = "700163000034";
                ////hdf_apiUserName.Value = "Merchant.700163000034";
                ////hdf_apiPassword.Value = "d5eddfc9726780cca272b3cd91563567";
                ////version = "40";
                //////gatewayUrl = "https://test-bankofceylon.mtf.gateway.mastercard.com/api/nvp";


                //Live
                //Merchant Related Variables
                hdf_merchantId.Value = "700163000034";
                hdf_apiUserName.Value = "Merchant.700163000034";
                hdf_apiPassword.Value = "ec1152ff4b59805ec6c441fa9ed8e37f";
                version = "50";
                //gatewayUrl = "https://bankofceylon.gateway.mastercard.com/api/nvp";


                //Order Related Variables
                hdf_order_currency.Value = "LKR";

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


                        if (paraList.ContainsKey("Pol_No"))
                        {
                            string polNo = paraList["Pol_No"];
                            hdf_orderRef.Value = polNo;

                        }
                        if (paraList.ContainsKey("Ref_No"))
                        {
                            string refNo = paraList["Ref_No"];
                            hdf_order_id.Value = refNo;
                        }
                        if (paraList.ContainsKey("Amount"))
                        {
                            string amount = paraList["Amount"];
                            hdf_order_amount.Value = amount;

                        }
                        if (paraList.ContainsKey("Cust_Email"))
                        {
                            string cust_Email = paraList["Cust_Email"];
                            hdf_customerEmail.Value = cust_Email;
                        }
                        else
                        {
                            errorMsg = "No valid Parameters Passed";
                            Server.Transfer("ErrorPage.aspx");
                        }
                    }
                }

                SendRequestToServer();


                count_PageLoad = count_PageLoad + 1;
                ViewState["session_key"] = count_PageLoad;
            }

        }
    }

    private void SendRequestToServer()
    {

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;


        // Create a request using a URL that can receive a post.   
        //Test
        //WebRequest request = WebRequest.Create("https://test-bankofceylon.mtf.gateway.mastercard.com/api/nvp/version/40");

        //Live
        WebRequest request = WebRequest.Create("https://bankofceylon.gateway.mastercard.com/api/nvp/version/50");
        
        // Set the Method property of the request to POST.  
        request.Method = "POST";

        // Create POST data and convert it to a byte array.
        var postParams = new List<KeyValuePair<string, string>>(){
                    new KeyValuePair<string, string>("apiOperation", "CREATE_CHECKOUT_SESSION"),
                    new KeyValuePair<string, string>("order.id",hdf_order_id.Value ) ,
                    new KeyValuePair<string, string>("order.amount", hdf_order_amount.Value),
                    new KeyValuePair<string, string>("order.currency", hdf_order_currency.Value),
                    new KeyValuePair<string, string>("order.reference", hdf_orderRef.Value),
                    new KeyValuePair<string, string>("merchant", hdf_merchantId.Value),
                    new KeyValuePair<string, string>("apiPassword", hdf_apiPassword.Value),
                    new KeyValuePair<string, string>("apiUsername", hdf_apiUserName.Value)              
                
                   
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
            // Display the content.           
            //Label1.Text = responseFromServer;
            responseParamterString = responseFromServer;


            splittedParamsList = responseParamterString.Split('&');
            foreach (string param_Values in splittedParamsList)
            {
                if (param_Values.Contains("session.id"))
                {
                    sessionIDValues = param_Values;
                    splittedSessionIDValue_List = sessionIDValues.Split('=');
                    foreach (string sessionIDvalue in splittedSessionIDValue_List)
                    {
                        if (sessionIDvalue.Contains("SESSION"))
                        {
                            hdf_sessionID.Value = sessionIDvalue;                            
                            Session["session_id_"] = hdf_sessionID.Value;
                        }
                    }

                   
                }

                if (param_Values.Contains("successIndicator"))
                {
                    splittedParamsList_successIndicator = param_Values.Split('=');
                    foreach (string successIndicator_Value in splittedParamsList_successIndicator)
                    {
                        if (!successIndicator_Value.Equals("successIndicator"))
                        {
                             hdf_SuccessIndicator.Value = successIndicator_Value;                            
                            Session["success_Indicator"] = hdf_SuccessIndicator.Value;

                        }
                    }
                    
                }

                if (param_Values.Contains("result"))
                {
                    splittedParamsList_result = param_Values.Split('=');
                    foreach (string result_Value in splittedParamsList_result)
                    {
                        if (result_Value.Equals("SUCCESS"))
                        {
                            result = true;

                        }
                    }

                }
            }
            
        }

        // Close the response.  
        response.Close();
        //Insert Life Payment Details
        if (result)
        {
            insertBOCPayment();
        }
        else {
            lbl_result.Text = "Sorry, Unsuccessful Web Request !.";
            log logger = new log();
            logger.write_log("Failed at BOC_PaymentForm-Pageload:Unsuccessful Web Request.Record Was Not Inserted To DB. ");
        
        }

    }

    private void insertBOCPayment()   {


        /*Insert Initial Order Details To BOC_IPG_PAYMENTS*/
        BOC_Payment insertInitialOrderDetail_obj = new BOC_Payment();
        bool isInitialInsertSuccess = insertInitialOrderDetail_obj.Insert_BOCPaymentDetails_Life((hdf_order_id.Value), hdf_order_amount.Value, hdf_order_currency.Value, hdf_orderRef.Value, hdf_sessionID.Value, hdf_SuccessIndicator.Value);
        if (isInitialInsertSuccess)
        {

            Application["OrderID"] = hdf_order_id.Value;


        }
        //else {
        //    lbl_result.Text = "Sorry, Record Was Not Saved !.";
        //    log logger = new log();
        //    logger.write_log("Failed at BOC_PaymentForm-Pageload:Unsuccessful Insertion To SLIC_NET_LIFE.BOC_IPG_PAYMENTS DB. ");
        
        //}



    }

}