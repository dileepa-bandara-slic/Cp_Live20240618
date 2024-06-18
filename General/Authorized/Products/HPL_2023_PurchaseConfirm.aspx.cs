using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Authorized_Products_HPL_2023_PurchaseConfirm : System.Web.UI.Page
{
    public string Ref_No { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (PreviousPage != null)
            {
                hpl_message.InnerHtml = "";

                HID_ChPlpolTyp.Value = PreviousPage.ChPlpolTyp;
                HID_ChPltitle.Value = PreviousPage.ChPltitle;
                HID_ChPlfullNam.Value = PreviousPage.ChPlfullNam;
                HID_ChPladrs1.Value = PreviousPage.ChPladrs1;
                HID_ChPladrs2.Value = PreviousPage.ChPladrs2; 
                HID_ChPladrs3.Value = PreviousPage.ChPladrs3;
                HID_ChPladrs4.Value = PreviousPage.ChPladrs4;
                HID_ChPlmobNo.Value = PreviousPage.ChPlmobNo; 
                HID_ChPlhomeNo.Value = PreviousPage.ChPlhomeNo; 
                HID_ChPlofcNo.Value = PreviousPage.ChPlofcNo;
                HID_ChPlemail.Value = PreviousPage.ChPlemail; 
                HID_ChPlnic.Value = PreviousPage.ChPlnic;         
                HID_ChPlassignee.Value = PreviousPage.ChPlassignee; 
                HID_ChPlsustained.Value = PreviousPage.ChPlsustained; 
                HID_ChPldeclinned.Value = PreviousPage.ChPldeclinned; 
                HID_ChPldmgBefore.Value = PreviousPage.ChPldmgBefore; 
                HID_ChPlrejBefore.Value = PreviousPage.ChPlrejBefore; 
                HID_ChPlrejResn.Value = PreviousPage.ChPlrejResn; 
                HID_ChPlplan.Value = PreviousPage.ChPlplan; 
                HID_ChPlstatus.Value = PreviousPage.ChPlstatus; 
                HID_ChPlusername.Value = PreviousPage.ChPlusername; 
                HID_ChPlprodId.Value = PreviousPage.ChPlprodId; 
                HID_ChPlprof.Value = PreviousPage.ChPlprof;
                HID_ChPlpayMethod.Value = PreviousPage.ChPlpayMethod;
                HID_Comm_Date.Value = PreviousPage.ChPlcomm_Date;
                HID_PolExp_Date.Value = PreviousPage.ChPlpol_exp_date;
                HID_Agt_Code.Value = PreviousPage.ChP_agtCode;

                this.Initalization((HID_ChPltitle.Value + " " + HID_ChPlfullNam.Value), PreviousPage.ChPllcAdrs1, PreviousPage.ChPllcAdrs2, PreviousPage.ChPllcAdrs3, PreviousPage.ChPllcAdrs4, HID_ChPlplan.Value, PreviousPage.ChPlcomm_Date, PreviousPage.ChPlpol_exp_date);
            }
            else
            {
                Response.Redirect("~/General/Authorized/Products/HPL_2023_PurchaseConfirm.aspx");
            }
        }
    }

    protected void Initalization(string name, string risk_loc1, string risk_loc2, string risk_loc3, string risk_loc4, string planid, string Comm_Date, string PolExp_Date)
    {
        txt_custonName.Value = name;
        cr_address_1.Value = risk_loc1;
        cr_address_2.Value = risk_loc2;
        cr_address_3.Value = risk_loc3;
        cr_address_4.Value = risk_loc4;
        cr_txtPeriod.Value= Comm_Date;
        cr_txtPeriod_to.Value = PolExp_Date;

        if (planid == "1")
        {
            PLANDESC.InnerHtml = "Plan 01 (Rs.)";
            P1C1R1.InnerHtml = "0.75Mn";
            P1C1R2.InnerHtml = "0.25Mn";
            P1C1R3.InnerHtml = "25,000";
            P1C1R4.InnerHtml = "25,000";
            P1C1R5.InnerHtml = "25,000";
            P1C1R6.InnerHtml = "25,000";
        }

        else if (planid == "2")
        {
            PLANDESC.InnerHtml = "Plan 02 (Rs.)";
            P1C1R1.InnerHtml = "1.5Mn";
            P1C1R2.InnerHtml = "0.5Mn";
            P1C1R3.InnerHtml = "25,000";
            P1C1R4.InnerHtml = "25,000";
            P1C1R5.InnerHtml = "25,000";
            P1C1R6.InnerHtml = "25,000";
        }

        else if (planid == "3")
        {
            PLANDESC.InnerHtml = "Plan 03 (Rs.)";
            P1C1R1.InnerHtml = "2.25Mn";
            P1C1R2.InnerHtml = "0.75Mn";
            P1C1R3.InnerHtml = "25,000";
            P1C1R4.InnerHtml = "25,000";
            P1C1R5.InnerHtml = "50,000";
            P1C1R6.InnerHtml = "50,000";
        }

        else if (planid == "4")
        {
            PLANDESC.InnerHtml = "Plan 04 (Rs.)";
            P1C1R1.InnerHtml = "3Mn";
            P1C1R2.InnerHtml = "1Mn";
            P1C1R3.InnerHtml = "25,000";
            P1C1R4.InnerHtml = "25,000";
            P1C1R5.InnerHtml = "75,000";
            P1C1R6.InnerHtml = "75,000";
        }

        else if (planid == "5")
        {
            PLANDESC.InnerHtml = "Plan 05 (Rs.)";
            P1C1R1.InnerHtml = "3.75Mn";
            P1C1R2.InnerHtml = "1.25Mn";
            P1C1R3.InnerHtml = "25,000";
            P1C1R4.InnerHtml = "25,000";
            P1C1R5.InnerHtml = "100,000";
            P1C1R6.InnerHtml = "100,000";
        }
    }


    protected void btnConfirmPurchase_Click(object sender, EventArgs e)
    {
        HPL_Proposal hPL_Proposal = new HPL_Proposal();
        HPL_Transactions hPL_Transactions = new HPL_Transactions();

        try
        {
            hPL_Proposal.polTyp = HID_ChPlpolTyp.Value.Trim();
            hPL_Proposal.title = HID_ChPltitle.Value.Trim();
            hPL_Proposal.fullNam = HID_ChPlfullNam.Value.Trim();

            hPL_Proposal.adrs1 = HID_ChPladrs1.Value.Trim();
            hPL_Proposal.adrs2 = HID_ChPladrs2.Value.Trim();
            hPL_Proposal.adrs3 = HID_ChPladrs3.Value.Trim();
            hPL_Proposal.adrs4 = HID_ChPladrs4.Value.Trim();

            hPL_Proposal.mobNo = HID_ChPlmobNo.Value.Trim();
            hPL_Proposal.homeNo = HID_ChPlhomeNo.Value.Trim();
            hPL_Proposal.ofcNo = HID_ChPlofcNo.Value.Trim();
            hPL_Proposal.email = HID_ChPlemail.Value.Trim();
            hPL_Proposal.nic = HID_ChPlnic.Value.Trim();
            hPL_Proposal.lcAdrs1 = cr_address_1.Value.Trim();
            hPL_Proposal.lcAdrs2 = cr_address_2.Value.Trim();
            hPL_Proposal.lcAdrs3 = cr_address_3.Value.Trim();
            hPL_Proposal.lcAdrs4 = cr_address_4.Value.Trim();
            hPL_Proposal.assignee = HID_ChPlassignee.Value.Trim();
            hPL_Proposal.sustained = HID_ChPlsustained.Value.Trim();
            hPL_Proposal.declinned = HID_ChPldeclinned.Value.Trim();
            hPL_Proposal.dmgBefore = HID_ChPldmgBefore.Value.Trim();
            hPL_Proposal.rejBefore = HID_ChPlrejBefore.Value.Trim();
            hPL_Proposal.rejResn = HID_ChPlrejResn.Value.Trim();
            hPL_Proposal.plan = HID_ChPlplan.Value.Trim();
            hPL_Proposal.status = HID_ChPlstatus.Value.Trim();
            hPL_Proposal.username = HID_ChPlusername.Value.Trim();
            hPL_Proposal.prodId = HID_ChPlprodId.Value.Trim();
            hPL_Proposal.prof = HID_ChPlprof.Value.Trim();
            hPL_Proposal.payMethod = HID_ChPlpayMethod.Value.Trim();
            hPL_Proposal.agtcode = HID_Agt_Code.Value.Trim();

            string sucess_ref = hPL_Transactions.BuyProduct(hPL_Proposal);

            if (sucess_ref != "#" && !String.IsNullOrEmpty(sucess_ref))
            {
                this.Reset();
                //hpl_message.InnerHtml = sucess;
                //Ref_No = sucess_ref;

                EncryptDecrypt dc = new EncryptDecrypt();
                Dictionary<string, string> qs = new Dictionary<string, string>();
                qs.Add("Ref_No", sucess_ref);
                qs.Add("Type", "N"); // N-new businees, R-renewals
                Response.Redirect(dc.url_encrypt("HPL_Payment.aspx", qs));
            }
            else
            {
                hpl_message.InnerHtml = hPL_Transactions.Error_Message;
            }
        }
        catch (Exception ex)
        {

            hpl_message.InnerHtml = ex.Message;
            //string message = string.Format("Exception: {0}", ex.Message + ". ");
            //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "message('Error Message', '" + message + "', 'swal-modal-error','swal-button-error', 0,1);", true);

            //Application_Log application_Log = new Application_Log();
            //string Error = "Exception : Request Data - (EVT-btn_ms_add_Click()) -> [Error Type : Application] - [BR : " + hubrc.Value + "] - [USER : " + hepf.Value + "] :: > ";
            //application_Log.WriteLog(Error + ex.Message + Environment.NewLine + HttpContext.Current.Request.Url.AbsolutePath);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/General/Authorized/Products/HPL_2023_Purchase.aspx");
    }

    protected void Reset()
    {
        HID_ChPlpolTyp.Value= string.Empty;
        HID_ChPltitle.Value= string.Empty;
        HID_ChPlfullNam.Value= string.Empty;
        HID_ChPladrs1.Value= string.Empty;
        HID_ChPladrs2.Value= string.Empty;
        HID_ChPladrs3.Value= string.Empty;
        HID_ChPladrs4.Value= string.Empty;
        HID_ChPlmobNo.Value= string.Empty;
        HID_ChPlhomeNo.Value= string.Empty;
        HID_ChPlofcNo.Value= string.Empty;
        HID_ChPlemail.Value= string.Empty;
        HID_ChPlnic.Value= string.Empty;
        cr_address_1.Value= string.Empty;
        cr_address_2.Value= string.Empty;
        cr_address_3.Value= string.Empty;
        cr_address_4.Value= string.Empty;
        HID_ChPlassignee.Value= string.Empty;
        HID_ChPlsustained.Value= string.Empty;
        HID_ChPldeclinned.Value= string.Empty;
        HID_ChPldmgBefore.Value= string.Empty;
        HID_ChPlrejBefore.Value= string.Empty;
        HID_ChPlrejResn.Value= string.Empty;
        HID_ChPlplan.Value= string.Empty;
        HID_ChPlstatus.Value= string.Empty;
        HID_ChPlusername.Value= string.Empty;
        HID_ChPlprodId.Value= string.Empty;
        HID_ChPlprof.Value= string.Empty;
        HID_ChPlpayMethod.Value = string.Empty;
        hpl_message.InnerHtml = "";
    }
   
}