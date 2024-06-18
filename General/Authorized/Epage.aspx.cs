using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Authorized_Epage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["errMesg"] != null)
        {
            lblMesg.Text = Session["errMesg"].ToString();
        }
        Session.Clear();
    }
}