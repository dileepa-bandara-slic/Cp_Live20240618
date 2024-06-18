using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class General_Authorized_Products_HPL_Questions : System.Web.UI.Page
{
    EncryptDecrypt dc = new EncryptDecrypt();
    Dictionary<string, string> qs = new Dictionary<string, string>();
    string QID = "";
    public string errorMsg { get; set; }


    public string Cs_title { get; set; }
    public string Cs_Name { get; set; }
    public string Cs_NIC { get; set; }
    public string Cs_permenet_add1 { get; set; }
    public string Cs_permenet_add2 { get; set; }
    public string Cs_permenet_add3 { get; set; }
    public string Cs_permenet_add4 { get; set; }
    public string Cs_home_phone { get; set; }
    public string Cs_mobile_phone { get; set; }
    public string Cs_office_phone { get; set; }

    public string Cs_profession { get; set; }

    public string Cs_email { get; set; }
    public string Cs_riskLoc_Add1 { get; set; }
    public string Cs_riskLoc_Add2 { get; set; }
    public string Cs_riskLoc_Add3 { get; set; }
    public string Cs_riskLoc_Add4 { get; set; }
    public string Cs_Assignee { get; set; }
    public string Cs_prev_Losses { get; set; }
    public string Cs_prev_Losses_rsn { get; set; }
    public string Cs_prev_Rej { get; set; }
    public string Cs_prev_Rej_rsn { get; set; }
    public string Cs_plan { get; set; }
    public string Cs_comDate { get; set; }


    protected void Page_Load(object sender, EventArgs e)
    {
        
                    this.Cs_title = PreviousPage.Cs_title;
                    this.Cs_Name = PreviousPage.Cs_Name;
                    this.Cs_NIC  = PreviousPage.Cs_NIC;
                    this.Cs_permenet_add1 = PreviousPage.Cs_permenet_add1;
                    this.Cs_permenet_add2 = PreviousPage.Cs_permenet_add2;
                    this.Cs_permenet_add3 = PreviousPage.Cs_permenet_add3;
                    this.Cs_permenet_add4 = PreviousPage.Cs_permenet_add4;
                    this.Cs_home_phone = PreviousPage.Cs_home_phone;
                    this.Cs_mobile_phone = PreviousPage.Cs_mobile_phone;
                    this.Cs_office_phone = PreviousPage.Cs_office_phone;

                    this.Cs_profession = PreviousPage.Cs_profession;

                    this.Cs_email = PreviousPage.Cs_email;
                    this.Cs_riskLoc_Add1 = PreviousPage.Cs_riskLoc_Add1;
                    this.Cs_riskLoc_Add2 = PreviousPage.Cs_riskLoc_Add2;
                    this.Cs_riskLoc_Add3 = PreviousPage.Cs_riskLoc_Add3;
                    this.Cs_riskLoc_Add4 = PreviousPage.Cs_riskLoc_Add4;
                    this.Cs_Assignee = PreviousPage.Cs_Assignee;
                    this.Cs_prev_Losses = PreviousPage.Cs_prev_Losses;
                    this.Cs_prev_Losses_rsn = PreviousPage.Cs_prev_Losses_rsn;
                    this.Cs_prev_Rej = PreviousPage.Cs_prev_Rej;
                    this.Cs_prev_Rej_rsn = PreviousPage.Cs_prev_Rej_rsn;
                    this.Cs_plan = PreviousPage.Cs_plan;
                    this.Cs_comDate = PreviousPage.Cs_comDate;
                    

                
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/General/Authorized/Products/HPL_PurchaseConf.aspx");
    }

}