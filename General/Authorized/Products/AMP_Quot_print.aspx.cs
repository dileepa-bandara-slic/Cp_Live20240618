using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Authorized_Products_AMP_Quot_print : System.Web.UI.Page
{
    public string errorMsg { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
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

                if (paraList.ContainsKey("plan") && paraList.ContainsKey("quotNo") && paraList.ContainsKey("action"))
                {
                    string plan = paraList["plan"];
                    string qid = paraList["quotNo"];
                    string action = paraList["action"];

                    Proposal prop = new Proposal();
                    string mesg = prop.updateAMPQuotation(plan, qid);
                    string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                    if (mesg == "success")
                    {
                        if (action == "buy")
                        {
                            EncryptDecrypt dc = new EncryptDecrypt();
                            Dictionary<string, string> qs = new Dictionary<string, string>();
                            qs.Add("Quot#1N0", qid);
                            //Response.Redirect(dc.url_encrypt("AMP_Questions.aspx", qs), false);
                            Response.Redirect(dc.url_encrypt("AMP_Questions.aspx", qs), false);
                            
                        }
                        else if (action == "print")
                        {
                            AMP_Quotation_print pdfPrint = new AMP_Quotation_print();
                            pdfPrint.print_quotation(qid, Page.User.Identity.Name, ip, false, "Q");
                        }
                        else
                        {
                            Response.Redirect("~/Errors/InternalError.htm");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Errors/InternalError.htm");
                    }
                }
                else
                {
                    errorMsg = "No valid Parameters Passed";
                    Response.Redirect("~/Errors/InternalError.htm");
                }
            }
        }
    }
}