using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Authorized_Products_HPL_Purchase : System.Web.UI.Page
{
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
        CustProfile customer = new CustProfile();
        string title = "";
        string firstName = "";
        string lastName = "";
        string othNames = "";
        string email = "";
        string nicNo = "";
        string dateOfBirth = "";
        string gender = "";
        string addrss1 = "";
        string addrss2 = "";
        string addrss3 = "";
        string addrss4 = "";
        string cityTown = "";
        string postCode = "";
        string country = "";
        string mobileNumber = "";
        string homeNumber = "";
        string officeNumber = "";
        string occupation = "";
        string srilankan = "";
        string passport = "";


        string message = customer.getProfileInfo(Page.User.Identity.Name, out title, out firstName, out lastName, out othNames,
                                                out email, out nicNo, out dateOfBirth, out gender, out addrss1, out addrss2,
                                                out addrss3, out addrss4, out cityTown, out postCode, out country, out mobileNumber,
                                                out homeNumber, out officeNumber, out occupation, out srilankan, out passport);
        if (message == "success")
        {
            Lit_cus_title.Text = title;
            Lit_full_name.Text = firstName + " " + (String.IsNullOrEmpty(othNames) ? " " : othNames + " ") + lastName;
            Lit_cus_nic.Text = nicNo;
            Lit_address1.Text = addrss1;
            Lit_address2.Text = addrss2;
            Lit_address3.Text = addrss3;
            Lit_address4.Text = addrss4;
            Lit_homePhone.Text = homeNumber;
            Lit_mobilePhone.Text = mobileNumber;
            Lit_officePhone.Text = officeNumber;
            Lit_email.Text = email;
            //txt_riskLoc.Text = addrss1 + " " + addrss2 + ((String.IsNullOrEmpty(addrss3) ? "" : " " + addrss3)) + ((String.IsNullOrEmpty(addrss4) ? "" : " " + addrss4));
            txt_rskLoc_address1.Text = addrss1;
            txt_rskLoc_address2.Text = addrss2;
            txt_rskLoc_address3.Text = addrss3;
            txt_rskLoc_address4.Text = addrss4;
        }
    }

    protected void rdo_Reject_Y_CheckedChanged(object sender, EventArgs e)
    {
        if (rdo_Reject_Y.Checked)
        {
            lblErr2.Text = "Please contact our call center for assistance.";
            Button1.Enabled = false;
        }
    }
    protected void rdo_Reject_N_CheckedChanged(object sender, EventArgs e)
    {
        if (rdo_Reject_N.Checked)
        {
            lblErr2.Text = "";

            if (rdo_Loss_N.Checked)
            {
                Button1.Enabled = true;
            }
            else
            {
                Button1.Enabled = false;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CVlblErr2.Validate();
        if (Page.IsValid)
        {

            Cs_title = Lit_cus_title.Text;
            Cs_Name = Lit_full_name.Text;
            Cs_NIC = Lit_cus_nic.Text;
            Cs_permenet_add1 = Lit_address1.Text;
            Cs_permenet_add2 = Lit_address2.Text;
            Cs_permenet_add3 = Lit_address3.Text;
            Cs_permenet_add4 = Lit_address4.Text;
            Cs_home_phone = Lit_homePhone.Text;
            Cs_mobile_phone = Lit_mobilePhone.Text;
            Cs_office_phone = Lit_officePhone.Text;

            Cs_profession = txt_profession.Text.Trim();


            Cs_email = Lit_email.Text;
            Cs_riskLoc_Add1 = txt_rskLoc_address1.Text.Trim();
            Cs_riskLoc_Add2 = txt_rskLoc_address2.Text.Trim();
            Cs_riskLoc_Add3 = txt_rskLoc_address3.Text.Trim();
            Cs_riskLoc_Add4 = txt_rskLoc_address4.Text.Trim();
            Cs_Assignee = txt_asgnee.Text.Trim();
            Cs_prev_Losses = (rdo_Loss_Y.Checked ? "Yes" : "No");
            Cs_prev_Rej = (rdo_Reject_Y.Checked ? "Yes" : "No");
            Cs_prev_Rej_rsn = "";// txt_remarks.Text.Trim();
            Cs_plan = (rdo_Plan_1.Checked ? "1" : "2");
            Cs_comDate = Txt_comDate.Text.Trim();
            Cs_prev_Losses_rsn = "";// txt_remarksdmg.Text.Trim();
        }

    }

    protected void RangeValidator6_Init(object sender, EventArgs e)
    {
        ((RangeValidator)sender).MaximumValue = DateTime.Today.AddYears(200).ToString("yyyy/MM/dd");
        ((RangeValidator)sender).MinimumValue = DateTime.Today.ToString("yyyy/MM/dd");
    }
    protected void rdo_Loss_Y_CheckedChanged(object sender, EventArgs e)
    {
        if (rdo_Loss_Y.Checked)
        {
            lblErr1.Text = "Please contact our call center for assistance.";
            Button1.Enabled = false;
        }

    }

    protected void rdo_Loss_N_CheckedChanged(object sender, EventArgs e)
    {
        if (rdo_Loss_N.Checked)
        {
            lblErr1.Text = "";
            if (rdo_Reject_N.Checked)
            {
                Button1.Enabled = true;
            }
            else
            {
                Button1.Enabled = false;
            }
        }
    }


    protected void Check_select1(object source, ServerValidateEventArgs args)
    {
        if (rdo_Reject_Y.Checked || rdo_Reject_N.Checked)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }

    }
}