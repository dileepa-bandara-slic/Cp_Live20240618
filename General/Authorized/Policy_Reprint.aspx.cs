using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Authorized_Policy_Reprint : System.Web.UI.Page
{
    EncryptDecrypt dc = new EncryptDecrypt();
    Dictionary<string, string> qs = new Dictionary<string, string>();
    string RefID = "";
    string poliID = "";
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

                if (paraList.ContainsKey("refN0"))
                {
                    RefID = paraList["refN0"];


                    string refId = RefID.Trim();

                    poliID = paraList["P0lNo"];

                    string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                    if (refId.Contains("MP"))
                    {
                        AMP_Quotation_print pdfPrint = new AMP_Quotation_print();
                        //pdfPrint.print_quotation(refId, Page.User.Identity.Name, ip, true);
                        string st = paraList["StaDt"];
                        string nd = paraList["EndDt"];
                        string sum = paraList["SM"];

                        pdfPrint.print_policy(refId, poliID, Page.User.Identity.Name, ip, true, st, nd, sum, RefID);
                    }
                    else if (refId.Contains("HIP"))
                    {
                        HP_Quotation_Print pdfPrint = new HP_Quotation_Print();
                       
                        //pdfPrint.print_quotation(refId, Page.User.Identity.Name, ip, true);
                        pdfPrint.print_policy(refId,poliID, Page.User.Identity.Name, ip, true);
                    }
                    else if (refId.Contains("GTI"))
                    {
                        GT_print_pdf pdfPrint = new GT_print_pdf();
                        //pdfPrint.print_quotation(refId, Page.User.Identity.Name, ip, true);

                        pdfPrint.print_policy(refId, poliID, Page.User.Identity.Name, ip, true);
                    }

                }
            }
        }
    }
}