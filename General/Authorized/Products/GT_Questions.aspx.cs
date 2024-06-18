using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Authorized_Products_GT_Questions : System.Web.UI.Page
{
    EncryptDecrypt dc = new EncryptDecrypt();
    Dictionary<string, string> qs = new Dictionary<string, string>();
    string QID = "";
    public string errorMsg { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        string strReq = "";
        strReq = Request.RawUrl;
        string h = strReq.Substring(strReq.Length - 1);
        if (h == "#")
        {
            errorMsg = "No url";
            // Response.Redirect("~/Errors/InternalError.htm");
        }
        else
        {
            strReq = strReq.Substring(strReq.IndexOf('?') + 1);

            EncryptDecrypt enc = new EncryptDecrypt();
            strReq = enc.Decrypt(strReq);
            if (strReq == "#")
            {
                errorMsg = "No Parameters Passed";
                //  Response.Redirect("~/Errors/InternalError.htm");
            }
            else
            {
                Dictionary<string, string> paraList = new Dictionary<string, string>();
                paraList = enc.getParameters(strReq);

                if (paraList.ContainsKey("Quot#2N0"))
                {
                    QID = paraList["Quot#2N0"];
                    //lbl.Text = QID;
                    // VALIDATE THIS
                    GT_Proposal_mast gtp = new GT_Proposal_mast(QID);

                    //DataTable dtMembers = new DataTable();
                    //Proposal prop = new Proposal();
                    //double height = 0;
                    //double weight = 0;
                    //string gender = "";
                    //double premium = 0;
                    //string plan = "";
                    //double planLimit = 0;

                    //string mesg = prop.getAMPQuotationDetails(QID, out height, out weight, out gender, out premium, out plan, out planLimit, out dtMembers);
                    lbl_plan.Text = gtp.plan;
                    lbl_premium.Text = gtp.finalPremium_rs.ToString("N2");
                    //lbl_planLim.Text = planLimit.ToString("N2");

                }
                else
                {
                    errorMsg = "No valid Parameters Passed";
                    // Response.Redirect("~/Errors/InternalError.htm");
                }
            }
        }
    }

    protected void but1_Click(object sender, EventArgs e)
    {
        qs.Add("Quot#5N0", QID);
        Response.Redirect(dc.url_encrypt("GT_Prop_Confirm.aspx", qs), false);
    }

}