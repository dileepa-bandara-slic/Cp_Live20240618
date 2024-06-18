using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Authorized_Products_TRV_Brochure : System.Web.UI.Page
{
    public DataTable dtconts = new DataTable();
    public DataTable dtPurposes = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        TRV_Contacts objconts = new TRV_Contacts();
        dtconts = objconts.dtContct;

        TRV_Contacts objconts2 = new TRV_Contacts("Purp");
        dtPurposes = objconts2.dtContct;

    }
}