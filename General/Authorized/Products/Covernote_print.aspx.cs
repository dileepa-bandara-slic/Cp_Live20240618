using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Authorized_Covernote_print : System.Web.UI.Page
{
    EncryptDecrypt dc = new EncryptDecrypt();
    Dictionary<string, string> qs = new Dictionary<string, string>();
    string RefID = "", poliID = "";
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

                if (paraList.ContainsKey("CrefNo"))
                {
                    RefID = paraList["CrefNo"];


                    string refId = RefID.Trim();

                    string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                    Covernote c = new Covernote();
                    c.covernote_print(RefID, Page.User.Identity.Name, ip, false);
                }
            }
        }

    }
}