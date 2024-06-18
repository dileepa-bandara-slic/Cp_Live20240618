using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Authorized_General_Products : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["dtCurrentTable"] = null;
        Session["schenagan"] = null;
        Session["fromDate"] = null;
        Session["arr_date"] = null;
        Session["dtVisitCntrys"] = null;
        Session["agentcode"] = null;
        // relavant only for disabling HIP for foriegners
        //CustProfile cus = new CustProfile(Page.User.Identity.Name);

        //if (cus != null)
        //{
        //    if (cus.O_srilankan.ToUpper() == "Y")
        //    {

        //    }
        //    else
        //    {
        //        lnk_amp.NavigateUrl = lnk_hpl.NavigateUrl = "";
        //        lnk_amp.Attributes.Add("class", "disabled btn btn-amp");
        //        lnk_hpl.Attributes.Add("class", "disabled btn btn-hpl");
        //    }
        //}
    }
}