using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Configuration;

public partial class General_Authorized_Products_HPL_2023_Purchase : System.Web.UI.Page
{
    public string ChPlpolTyp { get; set; }
    public string ChPltitle { get; set; }
    public string ChPlfullNam { get; set; }
    public string ChPladrs1 { get; set; }
    public string ChPladrs2 { get; set; }
    public string ChPladrs3 { get; set; }
    public string ChPladrs4 { get; set; }
    public string ChPlmobNo { get; set; }
    public string ChPlhomeNo { get; set; }
    public string ChPlofcNo { get; set; }
    public string ChPlemail { get; set; }
    public string ChPlnic { get; set; }
    public string ChPllcAdrs1 { get; set; }
    public string ChPllcAdrs2 { get; set; }
    public string ChPllcAdrs3 { get; set; }
    public string ChPllcAdrs4 { get; set; }
    public string ChPlassignee { get; set; }
    public string ChPlsustained { get; set; }
    public string ChPldeclinned { get; set; }
    public string ChPldmgBefore { get; set; }
    public string ChPlrejBefore { get; set; }
    public string ChPlrejResn { get; set; }
    public string ChPlplan { get; set; }
    public string ChPlstatus { get; set; }
    public string ChPlusername  { get; set; }
    public string ChPlprodId  { get; set; }
    public string ChPlprof  { get; set; }
    public string ChPlpayMethod  { get; set; }
    public string ChPlcomm_Date  { get; set; }
    public string ChPlpol_exp_date  { get; set; }
    public string ChP_agtCode { get; set; }

    static int indexOfL = 0;// the index of initial selected item

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.ApplicationIni();
            this.AddSingleSelection();
        }     
    }

    protected void ApplicationIni()
    {
        hpl_message.InnerHtml = "";

        txt_PerilsReson.Attributes["disabled"] = "disabled";
        txtPeriod.Attributes["disabled"] = "disabled";
        txtPeriod_to.Attributes["disabled"] = "disabled";
        btnSubmit.Attributes["disabled"] = "disabled";
        btn_agtValidate.Attributes["disabled"] = "disabled";
       
        txtPeriod.Value = System.DateTime.Now.ToString("dd/MM/yyyy");
        DateTime nextMonth = System.DateTime.Now.AddMonths(12);
        txtPeriod_to.Value = nextMonth.ToString("dd/MM/yyyy");

        chk_signature.Attributes.Add("onclick", " ValidateChkBoxSignature_ActFunction(this);");
    }

    protected void AddSingleSelection()
    {
        SingleSelection_chk_perils(chk_perils);
        SingleSelection_chk_declinned(chk_declinned);
        SingleSelection_chk_Plan(chkListPlan);
        SingleSelection_chk_userCinfirm(chk_UserConfirm);
    }

    protected void SingleSelection_chk_userCinfirm(CheckBoxList chkBoxList)
    {
        for (int i = 0; i < chkBoxList.Items.Count; i++)
        {
            chkBoxList.Items[i].Attributes.Add("onclick", "SingleSelection(this);");
        }
    }

    protected void SingleSelection_chk_perils(CheckBoxList chkBoxList)
    {
        for (int i = 0; i < chkBoxList.Items.Count; i++)
        {
            chkBoxList.Items[i].Attributes.Add("onclick", "SingleSelection(this);GetSelectedPerils();");
        }
    }

    protected void SingleSelection_chk_declinned(CheckBoxList chkBoxList)
    {
        for (int i = 0; i < chkBoxList.Items.Count; i++)
        {
            chkBoxList.Items[i].Attributes.Add("onclick", "SingleSelection(this);");
        }
    }

    protected void SingleSelection_chk_Plan(CheckBoxList chkBoxList)
    {
        for (int i = 0; i < chkBoxList.Items.Count; i++)
        {
            chkListPlan.Items[i].Attributes.Add("onclick", "SingleSelection(this);");
        }
    }


    protected void OnCheckBox_Changed(object sender, EventArgs e)
    {
        int i = 0;
        foreach (ListItem li in chk_UserConfirm.Items)
        {
            {
                if (i != indexOfL && li.Selected)
                {
                    indexOfL = i;
                    chk_UserConfirm.ClearSelection();
                    li.Selected = true;
                }

                i++;
            }
        }

            if (chk_UserConfirm.SelectedIndex == 0)
            {
                this.AddSingleSelection();
                this.CallREgistedCustomer();  
            }

            else if (chk_UserConfirm.SelectedIndex == 1)
            {
                this.AddSingleSelection();
                this.CleareForm();
            }
    }


    protected void CallREgistedCustomer()
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
            ch_Title.Value = title;
            txtcusName.Value = firstName + " " + (String.IsNullOrEmpty(othNames) ? " " : othNames + " ") + lastName;
            txtaddress_1.Value = addrss1;
            txtaddress_2.Value = addrss2;
            txtaddress_3.Value = addrss3;
            txtaddress_4.Value = addrss4;
            txt_contactno.Value = mobileNumber;
            txt_email.Value = email;
            txt_occupation.Value = occupation;
            txt_nic.Value = nicNo;
        }
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        this.AddSingleSelection();

            if (AgentConfirmation() > 0)
            {
                hpl_message.InnerHtml = "";
                chk_signature.Attributes.Remove("disabled");
                chk_signature.Enabled = true;
            }
            else
            {
                chk_signature.Enabled = false;
                chk_signature.Attributes.Add("class", "disabled");
                hpl_message.InnerHtml = "Invalid agemt code. Please check the agent code and try again";
            }
    }


    protected int AgentConfirmation()
    {
        HPL_Transactions hpl_Transaction = new HPL_Transactions();
        int get_confirmation = 0;
        int ConfirmConfirmation = 0;

        try
        {
            get_confirmation = hpl_Transaction.Get_AgencyConfirmation(int.Parse(txt_agent_Code.Value.Trim()));
            if (hpl_Transaction.Trans_Sucess_State != false)
            {
                if (get_confirmation > 0)
                {
                    ConfirmConfirmation = get_confirmation;
                }
                else
                {
                    ConfirmConfirmation = 0;
                }
            }
            else
            {
                HPL_OrclLog log_tr = new HPL_OrclLog();
                string Error = "Error : Request Data - (M- GetRecord()) -> [Error Type : Database] - [User Name : " + Page.User.Identity.Name + "]:: > ";
                log_tr.WriteLog(Error + hpl_Transaction.Error_Message + Environment.NewLine + HttpContext.Current.Request.Url.AbsolutePath);
            }
        }
        catch (Exception ex)
        {
            hpl_message.InnerHtml = ex.Message;
        }
        return ConfirmConfirmation;
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ChPlpolTyp = "G";
        ChPltitle = ch_Title.Value.Trim();
        ChPlfullNam = txtcusName.Value.Trim();
        ChPladrs1 = txtaddress_1.Value.Trim();
        ChPladrs2 = txtaddress_2.Value.Trim();
        ChPladrs3 = txtaddress_3.Value.Trim();
        ChPladrs4 = txtaddress_4.Value.Trim();
        ChPlmobNo = txt_contactno.Value.Trim();
        ChPlhomeNo = string.Empty;
        ChPlofcNo = string.Empty;
        ChPlemail = txt_email.Value.Trim();
        ChPlnic = txt_nic.Value.Trim();
        ChPllcAdrs1 = r_address_1.Value.Trim();
        ChPllcAdrs2 = r_address_2.Value.Trim();
        ChPllcAdrs3 = r_address_3.Value.Trim();
        ChPllcAdrs4 = r_address_4.Value.Trim();
        ChPlassignee = txt_assignees.Value.Trim();
        ChPlsustained = chk_perils.SelectedValue.Trim();
        ChPldeclinned = chk_declinned.SelectedValue.Trim();
        ChPldmgBefore = "N";
        ChPlrejBefore = "N";
        ChPlrejResn = txt_PerilsReson.Text.Trim();
        ChPlplan = chkListPlan.SelectedValue.Trim();
        ChPlstatus = "P";
        ChPlusername = Page.User.Identity.Name;
        ChPlprodId = "HIP";
        ChPlprof = txt_occupation.Value.Trim();
        ChPlpayMethod = "HNBGTW";

        ChPlcomm_Date = txtPeriod.Value.Trim();
        ChPlpol_exp_date = txtPeriod_to.Value.Trim();

        ChP_agtCode = txt_agent_Code.Value.Trim();
    }


    protected void btnSend_Click(object sender, EventArgs e)
    {   
        this.AddSingleSelection();
        this.SendMail("sanjeewar@srilankainsurance.com");
    }


    protected void CleareForm()
    {
        ch_Title.Value = "0";
        txtcusName.Value = string.Empty;
        txtaddress_1.Value = string.Empty;
        txtaddress_2.Value = string.Empty;
        txtaddress_3.Value = string.Empty;
        txtaddress_4.Value = string.Empty;
        txt_contactno.Value = string.Empty;
        txt_email.Value = string.Empty;
        txt_nic.Value = string.Empty;
        r_address_1.Value = string.Empty;
        r_address_2.Value = string.Empty;
        r_address_3.Value = string.Empty;
        r_address_4.Value = string.Empty;
        txt_assignees.Value = string.Empty;
        txt_PerilsReson.Text = string.Empty;
        chkListPlan.ClearSelection();
        chk_perils.ClearSelection();
        chk_declinned.ClearSelection();
        txt_occupation.Value = string.Empty;
        chk_signature.Checked = false;
        txt_agent_Code.Value = string.Empty;
        //chk_UserConfirm.ClearSelection();
        //-----------------------------------------------
    }

    protected void SendMail(string e_mail)
    {
        string body = string.Empty;
        try
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(Server.MapPath("~/HPL_Email/hpl_reject.htm")))
            {
                body = reader.ReadToEnd();
            }
                body = body.Replace("{SYSTEM_DATE}", DateTime.Now.ToString("dd:MM:yyyy HH:mm:ss tt"));
                body = body.Replace("{CUSNAME}", ch_Title.Value.Trim() + txtcusName.Value.Trim());
                body = body.Replace("{NIC}", txt_nic.Value.Trim());
                body = body.Replace("{CONNUMBER}", txt_contactno.Value.Trim());
                body = body.Replace("{EMAIL}", txt_email.Value.Trim());
                body = body.Replace("{RLOC}", r_address_1.Value.Trim() + ", " + r_address_2.Value.Trim()+ ", " + r_address_3.Value.Trim()+ ", " + r_address_4.Value.Trim());
                body = body.Replace("{CNOTE}", txt_PerilsReson.Text.Trim());

            this.SendHtmlFormattedEmail(body, e_mail);
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "message('Error Message', 'Message : Error In Editing Template. " + ex.Message + "', 'swal-modal-error','swal-button-error', 1, 1);", true);
        }
    }


    protected void SendHtmlFormattedEmail(string body, string cus_email)
    {
        Regex pattern = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        if (pattern.IsMatch(cus_email))
        {
            try
            {
                MailMessage message = new MailMessage();

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                message.AlternateViews.Add(htmlView);

                MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["smtpUser"]);
                message.From = fromAddress;
                message.Subject = "Home Protect Policy Rejection By System";
                message.To.Add(cus_email);
                //message.CC.Add("");
                message.Body = body;


                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;

                SmtpClient sendClient = new SmtpClient();
                sendClient.Host = ConfigurationManager.AppSettings["smtpServer"];
                sendClient.Port = int.Parse(ConfigurationManager.AppSettings["smtpPort"]);

                sendClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["smtpUser"];
                //NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                sendClient.UseDefaultCredentials = true;
                sendClient.Credentials = NetworkCred;
                message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                sendClient.Send(message);

                mp1.Hide();
                mp2.Show();
                CleareForm();
               
               
            }
            catch (Exception ex)
            {
                // ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "message('Error Message', 'Message : Error Sending E-Mail. " + ex.Message + "', 'swal-modal-error','swal-button-error', 1, 1);", true);
            }
        }
    }

}