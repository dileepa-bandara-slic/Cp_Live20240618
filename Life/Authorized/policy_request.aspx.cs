using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Life_Authorized_policy_request : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string uname = Page.User.Identity.Name.Trim();
        CustProfile cus = new CustProfile(uname);
        Print_pdf pdf = new Print_pdf();
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        pdf.print_policy_addition_request(cus, "9985553", ip);

    }
}