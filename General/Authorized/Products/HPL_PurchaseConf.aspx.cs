using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class General_Authorized_Products_HPL_PurchaseConf : System.Web.UI.Page
{
    Proposal pro = new Proposal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (PreviousPage != null)
            {
                Lit_cus_title.Text = PreviousPage.Cs_title;
                Lit_full_name.Text = PreviousPage.Cs_Name;
                Lit_cus_nic.Text = PreviousPage.Cs_NIC;
                Lit_address1.Text = PreviousPage.Cs_permenet_add1;
                Lit_address2.Text = PreviousPage.Cs_permenet_add2;
                Lit_address3.Text = PreviousPage.Cs_permenet_add3;
                Lit_address4.Text = PreviousPage.Cs_permenet_add4;
                Lit_homePhone.Text = PreviousPage.Cs_home_phone;
                Lit_mobilePhone.Text = PreviousPage.Cs_mobile_phone;
                Lit_officePhone.Text = PreviousPage.Cs_office_phone;

                Lit_profession.Text = PreviousPage.Cs_profession;

                Lit_email.Text = PreviousPage.Cs_email;
                Lit_riskLocAd1.Text = PreviousPage.Cs_riskLoc_Add1;
                Lit_riskLocAd2.Text = PreviousPage.Cs_riskLoc_Add2;
                Lit_riskLocAd3.Text = PreviousPage.Cs_riskLoc_Add3;
                Lit_riskLocAd4.Text = PreviousPage.Cs_riskLoc_Add4;
                Lit_asgnee.Text = PreviousPage.Cs_Assignee;
                Lit_losses.Text = PreviousPage.Cs_prev_Losses;
                Lit_rejReasonLos.Text = PreviousPage.Cs_prev_Losses_rsn;
                Lit_rejects.Text = PreviousPage.Cs_prev_Rej;
                Lit_rejReason.Text = PreviousPage.Cs_prev_Rej_rsn;
                Lit_plan.Text = PreviousPage.Cs_plan;
                Lit_comDate.Text = PreviousPage.Cs_comDate;
                if (!String.IsNullOrEmpty(Lit_rejReason.Text.Trim()))
                    Lit_remarks.Visible = true;

                if (!String.IsNullOrEmpty(Lit_rejReasonLos.Text.Trim()))
                    Lit_remarksLos.Visible = true;

                double baseAnuPrem = 0;
                double admnFee = 0;
                double polFee = 0;
                double nbt = 0;
                double vat = 0;
                double totAnuPrem = 0;
                double sumAssured = 0;

                if (pro.getHPParameters(PreviousPage.Cs_plan.Trim(), Lit_comDate.Text.Trim(), 0, out sumAssured, out baseAnuPrem, out admnFee, out polFee, out nbt, out vat, out  totAnuPrem))
                {
                    Lit_adminFee.Text = admnFee.ToString("N2");
                    Lit_vat.Text = vat.ToString("N2");
                    Lit_nbt.Text = nbt.ToString("N2");
                    Lit_polFee.Text = polFee.ToString("N2");
                    Lit_SA.Text = sumAssured.ToString("N2");
                    Lit_BasicPrem.Text = baseAnuPrem.ToString("N2");
                    Lit_TotalPrem.Text = totAnuPrem.ToString("N2");
                }

            }
            else
            {
                Response.Redirect("HPL_Purchase.aspx");
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string proid = pro.generate_proposalID("G", Convert.ToInt32(DateTime.Today.ToString("yyyy")), "HIP");
        if (!String.IsNullOrEmpty(proid))
        {
            var newDate = DateTime.ParseExact(Lit_comDate.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime end = newDate.AddYears(1).AddDays(-1);
            string end_date = end.ToString("yyyy/MM/dd");


            if (pro.insert_proposal("G", proid, Lit_full_name.Text, Lit_address1.Text, Lit_address2.Text, Lit_address3.Text, Lit_address4.Text, Lit_mobilePhone.Text, Lit_homePhone.Text, Lit_officePhone.Text, Lit_email.Text, Lit_cus_nic.Text, Lit_riskLocAd1.Text, Lit_riskLocAd2.Text, Lit_riskLocAd3.Text, Lit_riskLocAd4.Text, Lit_asgnee.Text, (Lit_losses.Text == "Yes" ? "Y" : (Lit_losses.Text == "No" ? "N" : "")), (Lit_rejects.Text == "Yes" ? "Y" : (Lit_rejects.Text == "No" ? "N" : "")), Lit_rejReason.Text, Lit_plan.Text, Convert.ToDouble(Lit_SA.Text), Convert.ToDouble(Lit_TotalPrem.Text), Convert.ToDouble(Lit_adminFee.Text), Convert.ToDouble(Lit_polFee.Text), Convert.ToDouble(Lit_nbt.Text), Convert.ToDouble(Lit_vat.Text), Lit_comDate.Text, "P", Page.User.Identity.Name, Lit_cus_title.Text.Trim(), "HIP", end_date, Lit_rejReasonLos.Text.Trim(), Lit_profession.Text.Trim()))
            {
                //Lit_msg.Text = "Should be navigated to the next page. Proposal id : " + proid;

                EncryptDecrypt dc = new EncryptDecrypt();
                Dictionary<string, string> qs = new Dictionary<string, string>();
                qs.Add("Ref_No", proid.Trim());
                qs.Add("Type", "N"); // N-new businees, R-renewals
                Response.Redirect(dc.url_encrypt("Payment.aspx", qs));
            }
            else
            {
                Lit_msg.Text = "An error occured, please resubmit with correct details";
            }
        }
        else
        {
            Lit_msg.Text = "An error occured, please resubmit with correct details";
        }
    }
}