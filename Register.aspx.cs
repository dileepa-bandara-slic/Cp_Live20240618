using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            try
            {
                DB2 db = new DB2();
                DataSet ds = new DataSet();
                string getCountries = "SELECT C_CODE, C_DESC FROM ULWEB.COUNTRIES ORDER BY C_DESC";
                ds = db.getrows(getCountries, ds);
                ddlCountry.DataSource = ds.Tables[0];
                ddlCountry.DataTextField = "C_DESC";
                ddlCountry.DataValueField = "C_CODE";
                ddlCountry.DataBind();

                ddlCountry.SelectedValue = "LK";
            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at Register-Pageload: " + ex.ToString());
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       // Response.Redirect("http://www.google.lk?q=" + hdn_sub_pwd.Value);

        

        var notValidValidators = Page.Validators.Cast<IValidator>().Where(v => !v.IsValid);

        if (Page.IsValid)
        {
            //Page.Validate();
            lblStatusMesg.Text = "";

            InfoValidator validator = new InfoValidator();
            if (validator.checkNICPPForAML(txtNICNo.Text.Trim(), "", txtMobileNo.Text.Trim()))
            {
                lblStatusMesg.Text = "Dear Customer,\n Required to further review the information entered. We will revert soon.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
            else if (validator.checkNICPPForAML("", txtPPNo.Text, txtMobileNo.Text))
            {
                lblStatusMesg.Text = "Dear Customer,\n Required to further review the information entered. We will revert soon.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }

            if (lblStatusMesg.Text == "")
            {
                try
                {
                    ULCustomer customer = new ULCustomer();

                    string username = txtUserName.Text.Trim();

                    string password = hdn_sub_pwd.Value.Trim();
                    password = Hash.Hash.Base64Decode(password);
                    password = Hash.Hash.Base64Decode(password);
                    password = Hash.Hash.Base64Decode(password);
                    password = Hash.Hash.Base64Decode(password);

                    string title = ddlTitle.Text.Trim();
                    string firstName = txtFirstName.Text.Trim();
                    string lastName = txtLastName.Text.Trim();
                    string otherNames = txtOtherNames.Text.Trim();
                    string nicNum = txtNICNo.Text.Trim();
                    int dateOfBirth = int.Parse(txtDateOfBirth.Text.Trim().Substring(0, 4) + txtDateOfBirth.Text.Trim().Substring(5, 2) + txtDateOfBirth.Text.Trim().Substring(8, 2));
                    string gender = rblGender.SelectedValue.ToString().Trim();
                    string address1 = txtAddress1.Text.Trim();
                    string address2 = txtAddress2.Text.Trim();
                    string address3 = txtAddress3.Text.Trim();
                    string address4 = txtAddress4.Text.Trim();
                    string cityTown = txtCityTown.Text.Trim();
                    string postalCode = txtPostalCode.Text.Trim();
                    string country = ddlCountry.SelectedValue.ToString().Trim();
                    string email = txtEmail.Text.Trim();
                    string mobileNum = txtMobileNo.Text.Trim();
                    string homeNum = txtHomeNo.Text.Trim();
                    string officeNum = txtOfficeNo.Text.Trim();
                    string desg = txtOccupation.Text.Trim();
                    string isSrilankan = rdo_srilankan.SelectedValue;
                    string passport = txtPPNo.Text.Trim();

                    DateTime creationTime = DateTime.Now;
                    string salt = creationTime.ToString("yyyy_MMM_ddd@hhmm");
                    password = password.Trim() + salt;

                    // if (customer.IsRegisteredUser(username) == 0)
                    // {
                    bool registrationSuccess = customer.RegisterCustomer(username, password, title, firstName, lastName, otherNames, nicNum,
                                                                        dateOfBirth, gender, address1, address2, address3, address4, cityTown,
                                                                        postalCode, country, email, mobileNum, homeNum, officeNum, desg, isSrilankan, passport, creationTime);
                    if (registrationSuccess)
                    {
                        //customer.SendRegNotification(username);// this is not required
                        //lblStatusMesg.Text = "Information successfully submitted. Please check your email for the activation link";
                        //lblStatusMesg.ForeColor = System.Drawing.Color.Green;
                        Response.Redirect("RegisterSuccess.aspx", false);

                    }
                    else
                    {
                        lblStatusMesg.Text = "Registration failed";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at Register: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in registration process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }

        }

    }

    protected void checkUsername(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();

        validator.validateUsername(txtUserName.Text.Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtPassword.Focus();
        }
        else
        {
            args.IsValid = false;
            UsernameValidator.ErrorMessage = message;
            txtUserName.Focus();
        }
    }

    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {
        NICValidator.Validate();
        DOBValidator.Validate();
        GenderValidator.Validate();
        EmailValidator.Validate();
        UsernameValidator.Validate();
    }

    protected void checkTitle(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();

        validator.validateTitle(ddlTitle.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtFirstName.Focus();
        }
        else
        {
            args.IsValid = false;
            TitleValidator.ErrorMessage = message;
            ddlTitle.Focus();
        }

        if (rblGender.SelectedValue == "M" || rblGender.SelectedValue == "F")
        {
            GenderValidator.Validate();
        }
    }

    protected void checkFirstname(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateFirstname(txtFirstName.Text.Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtLastName.Focus();
        }
        else
        {
            args.IsValid = false;
            FnameValidator.ErrorMessage = message;
            txtFirstName.Focus();
        }
    }

    protected void checkLastname(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateLastname(txtLastName.Text.Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtOtherNames.Focus();
        }
        else
        {
            args.IsValid = false;
            LNameValidator.ErrorMessage = message;
            txtLastName.Focus();
        }
    }

    protected void checkOthernames(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateOthernames(txtOtherNames.Text.Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtNICNo.Focus();
        }
        else
        {
            args.IsValid = false;
            OthnameValidator.ErrorMessage = message;
            txtOtherNames.Focus();
        }
    }

    protected void checkNIC(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateNIC(txtNICNo.Text.Trim().ToUpper(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            //txtDateOfBirth.Focus();
        }
        else
        {
            args.IsValid = false;
            NICValidator.ErrorMessage = message;
            txtNICNo.Focus();
        }
    }

    protected void txtNICNo_TextChanged(object sender, EventArgs e)
    {
        UsernameValidator.Validate();
        DOBValidator.Validate();
        GenderValidator.Validate();
        EmailValidator.Validate();
        NICValidator.Validate();
        PassportValidator.Validate();

        if (UsernameValidator.IsValid && DOBValidator.IsValid && GenderValidator.IsValid && EmailValidator.IsValid && NICValidator.IsValid && PassportValidator.IsValid)
        {
            txtPPNo.Focus();
        }
        else
        {
            txtNICNo.Focus();
        }
    }

    protected void checkDOB(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateDateofBirth(txtDateOfBirth.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            rblGender.Focus();
        }
        else
        {
            args.IsValid = false;
            DOBValidator.ErrorMessage = message;
            //txtDateOfBirth.Focus();
        }
    }

    protected void txtDateOfBirth_TextChanged(object sender, EventArgs e)
    {
        UsernameValidator.Validate();
        NICValidator.Validate();
        EmailValidator.Validate();
        DOBValidator.Validate();
        GenderValidator.Validate();
        PassportValidator.Validate();
    }

    protected void checkGender(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();

        validator.validateGender(rblGender.SelectedValue.ToString(), txtDateOfBirth.Text.Trim(), txtNICNo.Text.Trim().ToUpper(), ddlTitle.SelectedValue.ToString(), rdo_srilankan.SelectedValue, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtEmail.Focus();
        }
        else
        {
            args.IsValid = false;
            GenderValidator.ErrorMessage = message;
            rblGender.Focus();
        }
    }

    protected void rblGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        UsernameValidator.Validate();
        NICValidator.Validate();
        DOBValidator.Validate();
        EmailValidator.Validate();
        GenderValidator.Validate();
        PassportValidator.Validate();
    }

    protected void checkOcupation(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateOccupation(txtOccupation.Text.Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtAddress1.Focus();
        }
        else
        {
            args.IsValid = false;
            OcupationValidator1.ErrorMessage = message;
            txtOccupation.Focus();
        }
    }

    protected void checkAddress1(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateAddressLine1(txtAddress1.Text.Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtAddress2.Focus();
        }
        else
        {
            args.IsValid = false;
            Adrs1Validator.ErrorMessage = message;
            txtAddress1.Focus();
        }
    }

    protected void checkAddress2(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateAddressLine2(txtAddress2.Text.Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtAddress3.Focus();
        }
        else
        {
            args.IsValid = false;
            Adrs2Validator.ErrorMessage = message;
            txtAddress2.Focus();
        }
    }

    protected void checkAddress3(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateAddressLine3(txtAddress3.Text.Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtAddress4.Focus();
        }
        else
        {
            args.IsValid = false;
            Adrs3Validator.ErrorMessage = message;
            txtAddress3.Focus();
        }
    }

    protected void checkAddress4(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateAddressLine4(txtAddress4.Text.Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtCityTown.Focus();
        }
        else
        {
            args.IsValid = false;
            Adrs4Validator.ErrorMessage = message;
            txtAddress4.Focus();
        }
    }

    protected void checkCityTown(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateCityTown(txtCityTown.Text.Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtPostalCode.Focus();
        }
        else
        {
            args.IsValid = false;
            CityTownValidator.ErrorMessage = message;
            txtCityTown.Focus();
        }
    }

    protected void checkPostalCode(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validatePostaclCode(txtPostalCode.Text.Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            ddlCountry.Focus();
        }
        else
        {
            args.IsValid = false;
            PostalCodeValidator.ErrorMessage = message;
            txtPostalCode.Focus();
        }
    }

    protected void checkCountry(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateCountry(ddlCountry.SelectedValue.ToString(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtEmail.Focus();
        }
        else
        {
            args.IsValid = false;
            CountryValidator.ErrorMessage = message;
            ddlCountry.Focus();
        }
    }

    protected void checkEmail(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateEmail(txtEmail.Text.Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtOccupation.Focus();
        }
        else
        {
            args.IsValid = false;
            EmailValidator.ErrorMessage = message;
            txtEmail.Focus();
        }
    }

    protected void txtEmail_TextChanged(object sender, EventArgs e)
    {
        
        UsernameValidator.Validate();
        //NICValidator.Validate();
        DOBValidator.Validate();
        GenderValidator.Validate();
        EmailValidator.Validate();
        
    }

    protected void checkMobileNo(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateMobileNumber(txtMobileNo.Text.Trim(), ddlCountry.SelectedValue.ToString().Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtHomeNo.Focus();
        }
        else
        {
            args.IsValid = false;
            MobileNoValidator.ErrorMessage = message;
            txtMobileNo.Focus();
        }
    }
    //protected void txtMobileNo_TextChanged(object sender, EventArgs e)
    //{
    //    MobileNoValidator.Validate();
    //}

    protected void checkHomeNo(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateHomeNumber(txtHomeNo.Text.Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtOfficeNo.Focus();
        }
        else
        {
            args.IsValid = false;
            HomeNoValidator.ErrorMessage = message;
            txtHomeNo.Focus();
        }
    }
    //protected void txtHomeNo_TextChanged(object sender, EventArgs e)
    //{
    //    HomeNoValidator.Validate();
    //}

    protected void checkOfficeNo(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateOfficeNumber(txtOfficeNo.Text.Trim(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            btnSubmit.Focus();
        }
        else
        {
            args.IsValid = false;
            OfficeNoValidator.ErrorMessage = message;
            txtOfficeNo.Focus();
        }
    }
    //protected void txtOfficeNo_TextChanged(object sender, EventArgs e)
    //{
    //    OfficeNoValidator.Validate();
    //}

    protected void checkImageCode(object source, ServerValidateEventArgs args)
    {
        if (String.IsNullOrEmpty(txtimgcode.Text))
        {
            ImageValidator.ErrorMessage = "Enter the text found in the image";
            args.IsValid = false;
        }
        else
        {
            if (this.txtimgcode.Text == this.Session["CaptchaImageText"].ToString())
            {
                args.IsValid = true;
            }
            else
            {
                ImageValidator.ErrorMessage = "Entered text doesn't match with the text found in the image";
                args.IsValid = false;
            }
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string s = DateTime.Now.ToLongTimeString();
        Image1.ImageUrl = "~/CImage.aspx?i=" + s;
        //UpdatePanel4.Update();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string[] GetJobs(string prefix)
    {

        List<string> Joblist = new List<string>();

        CustProfile cp = new CustProfile();
        Joblist = cp.getJobList(prefix);

        return Joblist.ToArray();
    }

    protected void Button1_Click(object sender, ImageClickEventArgs e)
    {
        string s = DateTime.Now.ToLongTimeString();
        Image1.ImageUrl = "~/CImage.aspx?i=" + s;
    }
    protected void rdo_srilankan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdo_srilankan.SelectedValue == "Y")
        {
            lit_nic_lbl.Text = "NIC Number *";
            lit_pp_lbl.Text = "Passport Number";
            txtNICNo.Text = "";
            txtNICNo.ReadOnly = false;

            //NICRequired.EnableClientScript = true;
            NICRequired.Enabled = true;

            //NICValidator.EnableClientScript = true;
            NICValidator.Enabled = true;

            ReqPPValidator.Enabled = false;
            ddlCountry.ClearSelection();
            ddlCountry.SelectedValue = "LK";

        }
        else if (rdo_srilankan.SelectedValue == "N")
        {
            lit_nic_lbl.Text = "NIC Number";
            lit_pp_lbl.Text = "Passport Number *";
            txtNICNo.Text = "";
            txtNICNo.ReadOnly = true;

            //NICRequired.EnableClientScript = false;
            NICRequired.Enabled = false;

            //NICValidator.EnableClientScript = false;
            NICValidator.Enabled = false;

            ReqPPValidator.Enabled = true;

            ddlCountry.ClearSelection();
        }
    }

    protected void checkCitizen(object source, ServerValidateEventArgs args)
    {
        //int status = -1;
        //string message = "";

        //InfoValidator validator = new InfoValidator();

        //validator.validateGender(rblGender.SelectedValue.ToString(), txtDateOfBirth.Text, txtNICNo.Text.ToUpper(), ddlTitle.SelectedValue.ToString(), out status, out message);

        //if (status == 0)
        //{
        //    args.IsValid = true;
        //    txtAddress1.Focus();
        //}
        //else
        //{
        //    args.IsValid = false;
        //    GenderValidator.ErrorMessage = message;
        //    rblGender.Focus();
        //}

        if (rdo_srilankan.SelectedValue == "Y" || rdo_srilankan.SelectedValue == "N")
        {
            if (rdo_srilankan.SelectedValue == "Y")
                txtNICNo.Focus();
            else
                txtPPNo.Focus();


            args.IsValid = true;

        }
        else
        {
            args.IsValid = false;
            rdoCitizenValidator.ErrorMessage = "Please select whether you are a sri lankan or not.";
            rdo_srilankan.Focus();
        }

        
    }

    
    protected void txtPPNo_TextChanged(object sender, EventArgs e)
    {
        PassportValidator.Validate();
    }

    protected void checkPassport(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";


        InfoValidator validator = new InfoValidator();
        validator.validatePPnoEntry(txtPPNo.Text.Trim(), rdo_srilankan.SelectedValue, Page.User.Identity.Name, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            //txtDateOfBirth.Focus();
        }
        else
        {
            args.IsValid = false;
            PassportValidator.ErrorMessage = message;
            txtPPNo.Focus();
        }
    }
}