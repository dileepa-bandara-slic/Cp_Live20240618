using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ErrorPages_Error_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        e_hedding.Text = Request.QueryString["mh_"];
        _error.Text = Request.QueryString["msg_"];
        NoAutority_Panel.Visible = true;
    }
}