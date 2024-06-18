using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Authorized_Products_TRV_Questionnaire2 : System.Web.UI.Page
{
    EncryptDecrypt dc = new EncryptDecrypt();
    Dictionary<string, string> qs = new Dictionary<string, string>();
    string QID = "";
    public string errorMsg { get; set; }
    public double premiumselected { get; set; }
    public string planselected { get; set; }
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

                if (paraList.ContainsKey("refNo"))
                {
                    QID = paraList["refNo"];
                    premiumselected = double.Parse(paraList["premium"]);
                    planselected = paraList["plan"];

                    DataTable dtMembers = new DataTable();
                    TRV_Proposal prop = new TRV_Proposal();
                    double height = 0;
                    double weight = 0;
                    string gender = "";
                    double premium = 0;
                    string plan = "";
                    double planLimit = 0;
                    string plandesc = "";
                    LitRef.Text = QID;

                    string mesg = prop.getTRVQuotationDetails(QID,planselected, out gender, out premium,   out dtMembers, out plandesc);
                    lbl_plan.Text = plandesc;
                    lbl_premium.Text = premiumselected.ToString("N2");
                    Session["plan"] = planselected;

                    
                    string mesg2 = prop.updateTRVProposal(planselected, QID);


                }
                else
                {
                    errorMsg = "No valid Parameters Passed";
                    Response.Redirect("~/Errors/InternalError.htm");
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        qs.Add("Quot#2N0", QID);
        qs.Add("plan", Session["plan"].ToString());
        qs.Add("repr_State", "false");
        qs.Add("polTy", "TPI");
        qs.Add("poldesc", lbl_plan.Text);
        qs.Add("premium", lbl_premium.Text);
        Response.Redirect(dc.url_encrypt("TRV_Quot_Confirm.aspx", qs), false);
    }
}