using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Life_Authorized_PolDetailsRequestForm : System.Web.UI.Page
{
    public string errorMsg { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["userName"] != null && Request.QueryString["policyNo"] != null)
        {
            try
            {
                string uname = Request.QueryString["userName"].ToString();
                string polNo = Request.QueryString["policyNo"].ToString();
                string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                CustProfile profile = new CustProfile(uname);
                Print_pdf pdf = new Print_pdf();
                pdf.print_policy_addition_request(profile, polNo, ip);
            }
            catch
            {
                errorMsg = "Internal Error";
                Server.Transfer("ErrorPage.aspx");
            }
        }
        else
        {
            errorMsg = "Missing Parameters";
            Server.Transfer("ErrorPage.aspx");
        }
    }
}