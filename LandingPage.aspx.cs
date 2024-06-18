using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnPlaystore_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("https://play.google.com/store/apps/details?id=com.slic.customer");
    }

    protected void btnAppstore_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("https://apps.apple.com/lk/app/slic-customer/id1476655848");
    }
}