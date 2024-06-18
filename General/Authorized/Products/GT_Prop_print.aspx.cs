using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Authorized_Products_GT_Prop_print : System.Web.UI.Page
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

                if (paraList.ContainsKey("plan") && paraList.ContainsKey("refNo") && paraList.ContainsKey("action"))
                {
                    string plan = paraList["plan"];
                    string refid = paraList["refNo"];
                    string action = paraList["action"];

                    Proposal prop = new Proposal();
                    string mesg = prop.updateGTProposal(plan, refid);
                    string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                    if (mesg == "success")
                    {
                        if (action == "buy")
                        {
                            EncryptDecrypt dc = new EncryptDecrypt();
                            Dictionary<string, string> qs = new Dictionary<string, string>();
                            qs.Add("Quot#2N0", refid);
                            //Response.Redirect(dc.url_encrypt("GT_Questions.aspx", qs), false);
                            Response.Redirect(dc.url_encrypt("GT_Questions.aspx", qs), false);
                        }
                        else if (action == "print")
                        {
                            GT_print_pdf pdfPrint = new GT_print_pdf();
                            pdfPrint.print_quotation(refid, Page.User.Identity.Name, ip, false);
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