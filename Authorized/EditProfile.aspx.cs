using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Authorized_EditProfile : System.Web.UI.Page
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
                    ddlTitle.SelectedValue = title;
                    txtFirstName.Text = firstName;
                    txtLastName.Text = lastName;
                    txtOtherNames.Text = othNames;
                    txtEmail.Text = email;
                    //txtNICNo.Text = nicNo;
                    //txtDateOfBirth.Text = dateOfBirth;
                    //rblGender.SelectedValue = gender;
                    txtAddress1.Text = addrss1;
                    txtAddress2.Text = addrss2;
                    txtAddress3.Text = addrss3;
                    txtAddress4.Text = addrss4;
                    txtCityTown.Text = cityTown;
                    txtPostalCode.Text = postCode;
                    ddlCountry.SelectedValue = country;
                    txtMobileNo.Text = mobileNumber;
                    txtHomeNo.Text = homeNumber;
                    txtOfficeNo.Text = officeNumber;
                    txtOccupation.Text = occupation;
                    txt_passport.Text = passport;


                    lblTitle.Text = ddlTitle.SelectedItem.Text;
                    lblFirstName.Text = txtFirstName.Text;
                    lblLastName.Text = txtLastName.Text;
                    lblOthNames.Text = txtOtherNames.Text;
                    lblEmail.Text = txtEmail.Text;
                    lblNic.Text = nicNo;
                    lblDatOfBirth.Text = dateOfBirth;
                    lblGender.Text = (gender == "F" ? "Female" : (gender == "M" ? "Male" : "Unspecified"));
                    lblAddrss1.Text = txtAddress1.Text;
                    lblAddrss2.Text = txtAddress2.Text;
                    lblAddrss3.Text = txtAddress3.Text;
                    lblAddrss4.Text = txtAddress4.Text;
                    lblCityTown.Text = txtCityTown.Text;
                    lblPostCode.Text = txtPostalCode.Text;
                    lblCountry.Text = ddlCountry.SelectedItem.Text;
                    lblMobNum.Text = txtMobileNo.Text;
                    lblHomNum.Text = txtHomeNo.Text;
                    lblOfcNum.Text = txtOfficeNo.Text;
                    lblOccupation.Text = txtOccupation.Text;
                    lblCitizen.Text = (srilankan.Trim() == "Y" ? "Yes" : (srilankan == "N" ? "No" : ""));
                    lblPasport.Text = txt_passport.Text; 

                    if (String.IsNullOrEmpty(lblOthNames.Text))
                    { lblOthNames.Text = "..."; }

                    if (String.IsNullOrEmpty(lblAddrss1.Text))
                    { lblAddrss1.Text = "..."; }

                    if (String.IsNullOrEmpty(lblAddrss2.Text))
                    { lblAddrss2.Text = "..."; }

                    if (String.IsNullOrEmpty(lblAddrss3.Text))
                    { lblAddrss3.Text = "..."; }

                    if (String.IsNullOrEmpty(lblAddrss4.Text))
                    { lblAddrss4.Text = "..."; }

                    if (String.IsNullOrEmpty(lblPostCode.Text))
                    { lblPostCode.Text = "..."; }

                    if (String.IsNullOrEmpty(lblHomNum.Text))
                    { lblHomNum.Text = "..."; }

                    if (String.IsNullOrEmpty(lblOfcNum.Text))
                    { lblOfcNum.Text = "..."; }

                    if (String.IsNullOrEmpty(lblOccupation.Text))
                    { lblOccupation.Text = "..."; }

                    if (String.IsNullOrEmpty(lblCitizen.Text))
                    { lblCitizen.Text = "..."; }

                    if (String.IsNullOrEmpty(lblPasport.Text))
                    { lblPasport.Text = "..."; }

                }
                else
                {
                    lblErrMesg.Text = message;
                }
            }
            catch (Exception ex)
            {
                lblErrMesg.Text = "Error occured while retrieving profile information";
                log logger = new log();
                logger.write_log("Failed at Register-Pageload: " + ex.ToString());
            }
        }
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
    }

    protected void imgBtnTitle_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        lblFirstName.Text = "Changes";
        if (imgBtnTitle.ImageUrl.Contains("edit.png"))
        {
            lblTitle.Visible = false;
            ddlTitle.Visible = true;
            //ddlTitle.Focus();
            imgBtnTitle.Height = 14;
            imgBtnTitle.Width = 14;
            imgBtnTitle.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            TitleValidator.Validate();
            //imgBtnTitle.ImageUrl = "~/Authorized/images/save.png";

            if (TitleValidator.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string title = ddlTitle.Text.Trim();
                    string message = "";
                    if (title != lblTitle.Text)
                    {
                        message = customer.saveProfile_title(Page.User.Identity.Name, title);
                    }
                    else
                    { message = "success"; }

                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblTitle.Text = title;
                        ddlTitle.Visible = false;
                        lblTitle.Visible = true;
                        lblTitle.Focus();
                        imgBtnTitle.ImageUrl = "~/Authorized/images/edit.png";
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }


                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - title: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

    }

    protected void ddlTitle_TextChanged(object sender, EventArgs e)
    {
        TitleValidator.Validate();
        //imgBtnTitle.ImageUrl = "~/Authorized/images/save.png";

        if (TitleValidator.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string title = ddlTitle.Text.Trim();

                string message = customer.saveProfile_title(Page.User.Identity.Name, title);
                if (message == "success")
                {
                    hideControls(ImgTitle);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblTitle.Text = title;
                    ddlTitle.Visible = false;
                    lblTitle.Visible = true;
                    lblTitle.Focus();
                    imgBtnTitle.ImageUrl = "~/Authorized/images/edit.png";
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }


            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - title: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void checkFirstname(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateFirstname(txtFirstName.Text, out status, out message);

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

    protected void imgBtnFstN_Click(object sender, ImageClickEventArgs e)
    {

        hideControls();
        if (imgBtnFstN.ImageUrl.Contains("edit.png"))
        {
            lblFirstName.Visible = false;
            txtFirstName.Visible = true;
            imgBtnFstN.Height = 14;
            imgBtnFstN.Width = 14;
            //txtFirstName.Focus();
            imgBtnFstN.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            FirstNameRequired.Validate();
            FnameValidator.Validate();

            if (FnameValidator.IsValid && FirstNameRequired.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string firstname = txtFirstName.Text.Trim();
                    string message = "";
                    if (firstname != lblFirstName.Text)
                    {
                        message = customer.saveProfile_firstname(Page.User.Identity.Name, firstname);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblFirstName.Text = firstname;
                        txtFirstName.Visible = false;
                        lblFirstName.Visible = true;
                        lblFirstName.Focus();
                        imgBtnFstN.ImageUrl = "~/Authorized/images/edit.png";
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }


                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - firstname: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

    }

    protected void txtFirstName_TextChanged(object sender, EventArgs e)
    {
        FirstNameRequired.Validate();
        FnameValidator.Validate();

        if (FnameValidator.IsValid && FirstNameRequired.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string firstname = txtFirstName.Text.Trim();

                string message = customer.saveProfile_firstname(Page.User.Identity.Name, firstname);
                if (message == "success")
                {
                    hideControls(ImgFname);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblFirstName.Text = firstname;
                    txtFirstName.Visible = false;
                    lblFirstName.Visible = true;
                    lblFirstName.Focus();
                    imgBtnFstN.ImageUrl = "~/Authorized/images/edit.png";
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }


            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - firstname: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void checkLastname(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateLastname(txtLastName.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtLastName.Focus();
        }
        else
        {
            args.IsValid = false;
            LNameValidator.ErrorMessage = message;
            txtLastName.Focus();
        }
    }

    protected void imgBtnLstN_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnLstN.ImageUrl.Contains("edit.png"))
        {
            lblLastName.Visible = false;
            txtLastName.Visible = true;
            //txtLastName.Focus();
            imgBtnLstN.Height = 14;
            imgBtnLstN.Width = 14;

            imgBtnLstN.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            LastNameRequired.Validate();
            LNameValidator.Validate();

            if (LNameValidator.IsValid && LastNameRequired.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string lastname = txtLastName.Text.Trim();

                    string message = "";
                    if (lastname != lblLastName.Text)
                    {
                        message = customer.saveProfile_lastname(Page.User.Identity.Name, lastname);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblLastName.Text = lastname;
                        txtLastName.Visible = false;
                        lblLastName.Visible = true;
                        lblLastName.Focus();
                        imgBtnLstN.ImageUrl = "~/Authorized/images/edit.png";
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }


                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - lastname: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

    }

    protected void txtLastName_TextChanged(object sender, EventArgs e)
    {
        LastNameRequired.Validate();
        LNameValidator.Validate();

        if (LNameValidator.IsValid && LastNameRequired.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string lastname = txtLastName.Text.Trim();

                string message = customer.saveProfile_lastname(Page.User.Identity.Name, lastname);
                if (message == "success")
                {
                    hideControls(ImgLName);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblLastName.Text = lastname;
                    txtLastName.Visible = false;
                    lblLastName.Visible = true;
                    lblLastName.Focus();
                    imgBtnLstN.ImageUrl = "~/Authorized/images/edit.png";
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }


            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - lastname: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void checkOthernames(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateOthernames(txtOtherNames.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            //txtNICNo.Focus();
        }
        else
        {
            args.IsValid = false;
            OthnameValidator.ErrorMessage = message;
            txtOtherNames.Focus();
        }
    }

    protected void imgBtnOthN_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnOthN.ImageUrl.Contains("edit.png"))
        {
            lblOthNames.Visible = false;
            txtOtherNames.Visible = true;
            //txtOtherNames.Focus();
            imgBtnOthN.Height = 14;
            imgBtnOthN.Width = 14;
            imgBtnOthN.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            OthnameValidator.Validate();

            if (OthnameValidator.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string othernames = txtOtherNames.Text.Trim();

                    string message = "";
                    if (othernames != lblOthNames.Text)
                    {
                        message = customer.saveProfile_othernames(Page.User.Identity.Name, othernames);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblOthNames.Text = othernames;
                        txtOtherNames.Visible = false;
                        lblOthNames.Visible = true;
                        lblOthNames.Focus();
                        imgBtnOthN.ImageUrl = "~/Authorized/images/edit.png";
                        if (String.IsNullOrEmpty(lblOthNames.Text))
                        { lblOthNames.Text = "..."; }
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }

                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - othernames: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void txtOtherNames_TextChanged(object sender, EventArgs e)
    {
        OthnameValidator.Validate();

        if (OthnameValidator.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string othernames = txtOtherNames.Text.Trim();

                string message = customer.saveProfile_othernames(Page.User.Identity.Name, othernames);
                if (message == "success")
                {
                    hideControls(ImgOName);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblOthNames.Text = othernames;
                    txtOtherNames.Visible = false;
                    lblOthNames.Visible = true;
                    lblOthNames.Focus();
                    imgBtnOthN.ImageUrl = "~/Authorized/images/edit.png";
                    if (String.IsNullOrEmpty(lblOthNames.Text))
                    { lblOthNames.Text = "..."; }
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - othernames: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    /*
    protected void checkNIC(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateNICUpdate(txtNICNo.Text.ToUpper(), Page.User.Identity.Name, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtDateOfBirth.Focus();
        }
        else
        {
            args.IsValid = false;
            NICValidator.ErrorMessage = message;
            txtNICNo.Focus();
        }
    }*/
    /*
    protected void imgBtnNic_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnNic.ImageUrl.Contains("edit.png"))
        {
            lblNic.Visible = false;
            txtNICNo.Visible = true;
            //txtNICNo.Focus();
            imgBtnNic.Height = 14;
            imgBtnNic.Width = 14;
            imgBtnNic.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            NICRequired.Validate();
            DOBValidator.Validate();
            GenderValidator.Validate();
            EmailValidator.Validate();
            NICValidator.Validate();

            if (DOBValidator.IsValid && GenderValidator.IsValid && NICValidator.IsValid && NICRequired.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string nic = txtNICNo.Text.Trim();

                    string message = "";
                    if (nic != lblNic.Text)
                    {
                        message = customer.saveProfile_nicno(Page.User.Identity.Name, nic);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblNic.Text = nic;
                        txtNICNo.Visible = false;
                        lblNic.Visible = true;
                        lblNic.Focus();
                        imgBtnNic.ImageUrl = "~/Authorized/images/edit.png";
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - nic: " + ex.ToString().Substring(0, 200));
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }      
    
    }
    
    protected void txtNICNo_TextChanged(object sender, EventArgs e)
    {
        NICRequired.Validate();
        DOBValidator.Validate();
        GenderValidator.Validate();
        EmailValidator.Validate();
        NICValidator.Validate();

        if (DOBValidator.IsValid && GenderValidator.IsValid && NICValidator.IsValid && NICRequired.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string nic = txtNICNo.Text.Trim();

                string message = customer.saveProfile_nicno(Page.User.Identity.Name, nic);
                if (message == "success")
                {
                    hideControls(ImgNIC);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblNic.Text = nic;
                    txtNICNo.Visible = false;
                    lblNic.Visible = true;
                    lblNic.Focus();
                    imgBtnNic.ImageUrl = "~/Authorized/images/edit.png";
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }                
            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - nic: " + ex.ToString().Substring(0, 200));
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    */
    /*
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
            txtDateOfBirth.Focus();
        }
    }*/
    /*
    protected void imgBtnDtBrth_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnDtBrth.ImageUrl.Contains("edit.png"))
        {
            lblDatOfBirth.Visible = false;
            txtDateOfBirth.Visible = true;
            //txtDateOfBirth.Focus();
            imgBtnDtBrth.Height = 14;
            imgBtnDtBrth.Width = 14;
            imgBtnDtBrth.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            DateOfBirthRequired.Validate();
            NICValidator.Validate();
            EmailValidator.Validate();
            DOBValidator.Validate();
            GenderValidator.Validate();

            if (DOBValidator.IsValid && GenderValidator.IsValid && NICValidator.IsValid && DateOfBirthRequired.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string dob = txtDateOfBirth.Text.Trim();

                    string message = "";
                    if (dob != lblDatOfBirth.Text)
                    {
                        message = customer.saveProfile_dob(Page.User.Identity.Name, (dob.Substring(0, 4) + dob.Substring(5, 2) + dob.Substring(8, 2)));
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblDatOfBirth.Text = dob;
                        txtDateOfBirth.Visible = false;
                        lblDatOfBirth.Visible = true;
                        lblDatOfBirth.Focus();
                        imgBtnDtBrth.ImageUrl = "~/Authorized/images/edit.png";
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }

                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - dob: " + ex.ToString().Substring(0, 200));
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    
    protected void txtDateOfBirth_TextChanged(object sender, EventArgs e)
    {
        DateOfBirthRequired.Validate();
        NICValidator.Validate();
        EmailValidator.Validate();
        DOBValidator.Validate();
        GenderValidator.Validate();

        if (DOBValidator.IsValid && GenderValidator.IsValid && NICValidator.IsValid && DateOfBirthRequired.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string dob = txtDateOfBirth.Text.Trim();

                string message = customer.saveProfile_dob(Page.User.Identity.Name, (dob.Substring(0, 4) + dob.Substring(5, 2) + dob.Substring(8, 2)));
                if (message == "success")
                {
                    hideControls(ImgDOB);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblDatOfBirth.Text = dob;
                    txtDateOfBirth.Visible = false;
                    lblDatOfBirth.Visible = true;
                    lblDatOfBirth.Focus();
                    imgBtnDtBrth.ImageUrl = "~/Authorized/images/edit.png";        
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
                
            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - dob: " + ex.ToString().Substring(0, 200));
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    */
    /*protected void checkGender(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();

        validator.validateGender(rblGender.SelectedValue.ToString(), txtDateOfBirth.Text, txtNICNo.Text.ToUpper(), out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txtAddress1.Focus();
        }
        else
        {
            args.IsValid = false;
            GenderValidator.ErrorMessage = message;
            rblGender.Focus();
        }
    }*/
    /*
    protected void imgBtnGen_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnGen.ImageUrl.Contains("edit.png"))
        {
            lblGender.Visible = false;
            rblGender.Visible = true;
            //rblGender.Focus();
            imgBtnGen.Height = 14;
            imgBtnGen.Width = 14;
            imgBtnGen.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            GenderRequired.Validate();
            NICValidator.Validate();
            DOBValidator.Validate();
            EmailValidator.Validate();
            GenderValidator.Validate();

            if (DOBValidator.IsValid && GenderValidator.IsValid && NICValidator.IsValid && GenderRequired.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string gender = rblGender.SelectedValue;
                    string genDesc = rblGender.SelectedItem.Text;

                    string message = "";
                    if (genDesc != lblGender.Text)
                    {
                        message = customer.saveProfile_gender(Page.User.Identity.Name, gender);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblGender.Text = genDesc;
                        rblGender.Visible = false;
                        lblGender.Visible = true;
                        lblGender.Focus();
                        imgBtnGen.ImageUrl = "~/Authorized/images/edit.png";
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - gender: " + ex.ToString().Substring(0, 200));
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    
    protected void rblGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        GenderRequired.Validate();
        NICValidator.Validate();
        DOBValidator.Validate();
        EmailValidator.Validate();
        GenderValidator.Validate();

        if (DOBValidator.IsValid && GenderValidator.IsValid && NICValidator.IsValid && GenderRequired.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string gender = rblGender.SelectedValue;
                string genDesc = rblGender.SelectedItem.Text;

                string message = customer.saveProfile_gender(Page.User.Identity.Name, gender);
                if (message == "success")
                {
                    hideControls(ImgGender);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblGender.Text = genDesc;
                    rblGender.Visible = false;
                    lblGender.Visible = true;
                    lblGender.Focus();
                    imgBtnGen.ImageUrl = "~/Authorized/images/edit.png";        
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }                
            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - gender: " + ex.ToString().Substring(0, 200));
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    */

    
    protected void checkOccupation(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateOccupation(txtOccupation.Text, out status, out message);

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

    protected void txtOccupation_TextChanged(object sender, EventArgs e)
    {
        //Address1Required.Validate();
        OcupationValidator1.Validate();

        if (OcupationValidator1.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string occupation = txtOccupation.Text.Trim();

                string message = customer.saveProfile_occupation(Page.User.Identity.Name, occupation);
                if (message == "success")
                {
                    hideControls(ImgOcu2);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblOccupation.Text = occupation;
                    txtOccupation.Visible = false;
                    lblOccupation.Visible = true;
                    lblOccupation.Focus();
                    imgBtImgOcu.ImageUrl = "~/Authorized/images/edit.png";
                    if (String.IsNullOrEmpty(lblOccupation.Text))
                    { lblOccupation.Text = "..."; }
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - ocupation: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void imgBtnOcu_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtImgOcu.ImageUrl.Contains("edit.png"))
        {
            lblOccupation.Visible = false;
            txtOccupation.Visible = true;
            //txtAddress1.Focus();
            imgBtImgOcu.Height = 14;
            imgBtImgOcu.Width = 14;
            imgBtImgOcu.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            //Address1Required.Validate();
            OcupationValidator1.Validate();

            if (OcupationValidator1.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string occupation = txtOccupation.Text.Trim();

                    string message = "";
                    if (occupation != lblOccupation.Text)
                    {
                        message = customer.saveProfile_occupation(Page.User.Identity.Name, occupation);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblOccupation.Text = occupation;
                        txtOccupation.Visible = false;
                        lblOccupation.Visible = true;
                        lblOccupation.Focus();
                        imgBtImgOcu.ImageUrl = "~/Authorized/images/edit.png";
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - Occupation: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }


    protected void checkPassport(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";


        InfoValidator validator = new InfoValidator();
        validator.validatePPnoUpdate(txt_passport.Text, (lblCitizen.Text == "Yes" ? "Y" : (lblCitizen.Text == "No" ? "N" : "")), Page.User.Identity.Name, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            txt_passport.Focus();
        }
        else
        {
            args.IsValid = false;
            PassportValidator.ErrorMessage = message;
            txt_passport.Focus();
        }
    }

    protected void imgBtImgPP_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtImgPP.ImageUrl.Contains("edit.png"))
        {
            lblPasport.Visible = false;
            txt_passport.Visible = true;
            //txtAddress1.Focus();
            imgBtImgPP.Height = 14;
            imgBtImgPP.Width = 14;
            imgBtImgPP.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            PassportValidator.Validate();

            if (PassportValidator.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string ppno = txt_passport.Text.Trim();

                    string message = "";
                    if (ppno != lblPasport.Text)
                    {
                        message = customer.saveProfile_Passport(Page.User.Identity.Name, ppno);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        if (String.IsNullOrEmpty(ppno))
                            lblPasport.Text = "...";
                        else
                            lblPasport.Text = ppno;

                        txt_passport.Visible = false;
                        lblPasport.Visible = true;
                        lblPasport.Focus();
                        imgBtImgPP.ImageUrl = "~/Authorized/images/edit.png";
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - passport:  " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
    }
    protected void txt_passport_TextChanged(object sender, EventArgs e)
    {
        PassportValidator.Validate();

        if (PassportValidator.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string ppno = txt_passport.Text.Trim();

                string message = "";
                if (ppno != lblPasport.Text)
                {
                    message = customer.saveProfile_Passport(Page.User.Identity.Name, ppno);
                }
                else
                { message = "success"; }
                if (message == "success")
                {
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    if (String.IsNullOrEmpty(ppno))
                        lblPasport.Text = "...";
                    else
                        lblPasport.Text = ppno;

                    txt_passport.Visible = false;
                    lblPasport.Visible = true;
                    lblPasport.Focus();
                    imgBtImgPP.ImageUrl = "~/Authorized/images/edit.png";
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - passport: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }


    protected void imgBtnAd1_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnAd1.ImageUrl.Contains("edit.png"))
        {
            lblAddrss1.Visible = false;
            txtAddress1.Visible = true;
            //txtAddress1.Focus();
            imgBtnAd1.Height = 14;
            imgBtnAd1.Width = 14;
            imgBtnAd1.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            Address1Required.Validate();
            Adrs1Validator.Validate();

            if (Adrs1Validator.IsValid && Address1Required.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string addrss1 = txtAddress1.Text.Trim();

                    string message = "";
                    if (addrss1 != lblAddrss1.Text)
                    {
                        message = customer.saveProfile_addrss1(Page.User.Identity.Name, addrss1);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblAddrss1.Text = addrss1;
                        txtAddress1.Visible = false;
                        lblAddrss1.Visible = true;
                        lblAddrss1.Focus();
                        imgBtnAd1.ImageUrl = "~/Authorized/images/edit.png";
                        if (String.IsNullOrEmpty(lblAddrss1.Text))
                        { lblAddrss1.Text = "..."; }
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - address 1: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void checkAddress1(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateAddressLine1(txtAddress1.Text, out status, out message);

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

    protected void txtAddress1_TextChanged(object sender, EventArgs e)
    {
        Address1Required.Validate();
        Adrs1Validator.Validate();

        if (Adrs1Validator.IsValid && Address1Required.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string addrss1 = txtAddress1.Text.Trim();

                string message = customer.saveProfile_addrss1(Page.User.Identity.Name, addrss1);
                if (message == "success")
                {
                    hideControls(ImgAdd1);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblAddrss1.Text = addrss1;
                    txtAddress1.Visible = false;
                    lblAddrss1.Visible = true;
                    lblAddrss1.Focus();
                    imgBtnAd1.ImageUrl = "~/Authorized/images/edit.png";
                    if (String.IsNullOrEmpty(lblAddrss1.Text))
                    { lblAddrss1.Text = "..."; }
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - address 1: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void checkAddress2(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateAddressLine2(txtAddress2.Text, out status, out message);

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

    protected void imgBtnAd2_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnAd2.ImageUrl.Contains("edit.png"))
        {
            lblAddrss2.Visible = false;
            txtAddress2.Visible = true;
            //txtAddress2.Focus();
            imgBtnAd2.Height = 14;
            imgBtnAd2.Width = 14;
            imgBtnAd2.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            Address2Required.Validate();
            Adrs2Validator.Validate();

            if (Adrs2Validator.IsValid && Address2Required.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string addrss2 = txtAddress2.Text.Trim();

                    string message = "";
                    if (addrss2 != lblAddrss2.Text)
                    {
                        message = customer.saveProfile_addrss2(Page.User.Identity.Name, addrss2);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblAddrss2.Text = addrss2;
                        txtAddress2.Visible = false;
                        lblAddrss2.Visible = true;
                        lblAddrss2.Focus();
                        imgBtnAd2.ImageUrl = "~/Authorized/images/edit.png";
                        if (String.IsNullOrEmpty(lblAddrss2.Text))
                        { lblAddrss2.Text = "..."; }
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - address 2: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

    }

    protected void txtAddress2_TextChanged(object sender, EventArgs e)
    {
        Address2Required.Validate();
        Adrs2Validator.Validate();

        if (Adrs2Validator.IsValid && Address2Required.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string addrss2 = txtAddress2.Text.Trim();

                string message = customer.saveProfile_addrss2(Page.User.Identity.Name, addrss2);
                if (message == "success")
                {
                    hideControls(ImgAdd2);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblAddrss2.Text = addrss2;
                    txtAddress2.Visible = false;
                    lblAddrss2.Visible = true;
                    lblAddrss2.Focus();
                    imgBtnAd2.ImageUrl = "~/Authorized/images/edit.png";
                    if (String.IsNullOrEmpty(lblAddrss2.Text))
                    { lblAddrss2.Text = "..."; }
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - address 2: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void checkAddress3(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateAddressLine3(txtAddress3.Text, out status, out message);

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

    protected void imgBtnAd3_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnAd3.ImageUrl.Contains("edit.png"))
        {
            lblAddrss3.Visible = false;
            txtAddress3.Visible = true;
            //txtAddress3.Focus();
            imgBtnAd3.Height = 14;
            imgBtnAd3.Width = 14;
            imgBtnAd3.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            Adrs3Validator.Validate();

            if (Adrs3Validator.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string addrss3 = txtAddress3.Text.Trim();

                    string message = "";
                    if (addrss3 != lblAddrss3.Text)
                    {
                        message = customer.saveProfile_addrss3(Page.User.Identity.Name, addrss3);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblAddrss3.Text = addrss3;
                        txtAddress3.Visible = false;
                        lblAddrss3.Visible = true;
                        lblAddrss3.Focus();
                        imgBtnAd3.ImageUrl = "~/Authorized/images/edit.png";
                        if (String.IsNullOrEmpty(lblAddrss3.Text))
                        { lblAddrss3.Text = "..."; }
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - address 3: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void txtAddress3_TextChanged(object sender, EventArgs e)
    {
        Adrs3Validator.Validate();

        if (Adrs3Validator.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string addrss3 = txtAddress3.Text.Trim();

                string message = customer.saveProfile_addrss3(Page.User.Identity.Name, addrss3);
                if (message == "success")
                {
                    hideControls(ImgAdd3);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblAddrss3.Text = addrss3;
                    txtAddress3.Visible = false;
                    lblAddrss3.Visible = true;
                    lblAddrss3.Focus();
                    imgBtnAd3.ImageUrl = "~/Authorized/images/edit.png";
                    if (String.IsNullOrEmpty(lblAddrss3.Text))
                    { lblAddrss3.Text = "..."; }
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - address 3: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void checkAddress4(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateAddressLine4(txtAddress4.Text, out status, out message);

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

    protected void imgBtnAd4_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnAd4.ImageUrl.Contains("edit.png"))
        {
            lblAddrss4.Visible = false;
            txtAddress4.Visible = true;
            //txtAddress4.Focus();
            imgBtnAd4.Height = 14;
            imgBtnAd4.Width = 14;
            imgBtnAd4.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            Adrs4Validator.Validate();

            if (Adrs4Validator.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string addrss4 = txtAddress4.Text.Trim();

                    string message = "";
                    if (addrss4 != lblAddrss4.Text)
                    {
                        message = customer.saveProfile_addrss4(Page.User.Identity.Name, addrss4);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblAddrss4.Text = addrss4;
                        txtAddress4.Visible = false;
                        lblAddrss4.Visible = true;
                        lblAddrss4.Focus();
                        imgBtnAd4.ImageUrl = "~/Authorized/images/edit.png";
                        if (String.IsNullOrEmpty(lblAddrss4.Text))
                        { lblAddrss4.Text = "..."; }
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }

                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - address 4: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void txtAddress4_TextChanged(object sender, EventArgs e)
    {
        Adrs4Validator.Validate();

        if (Adrs4Validator.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string addrss4 = txtAddress4.Text.Trim();

                string message = customer.saveProfile_addrss4(Page.User.Identity.Name, addrss4);
                if (message == "success")
                {
                    hideControls(ImgAdd4);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblAddrss4.Text = addrss4;
                    txtAddress4.Visible = false;
                    lblAddrss4.Visible = true;
                    lblAddrss4.Focus();
                    imgBtnAd4.ImageUrl = "~/Authorized/images/edit.png";
                    if (String.IsNullOrEmpty(lblAddrss4.Text))
                    { lblAddrss4.Text = "..."; }
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - address 4: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void checkCityTown(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateCityTown(txtCityTown.Text, out status, out message);

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

    protected void imgBtnTwn_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnTwn.ImageUrl.Contains("edit.png"))
        {
            lblCityTown.Visible = false;
            txtCityTown.Visible = true;
            //txtCityTown.Focus();
            imgBtnTwn.Height = 14;
            imgBtnTwn.Width = 14;
            imgBtnTwn.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            CityRequired.Validate();
            CityTownValidator.Validate();

            if (CityTownValidator.IsValid && CityRequired.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string cityTown = txtCityTown.Text.Trim();

                    string message = "";
                    if (cityTown != lblCityTown.Text)
                    {
                        message = customer.saveProfile_cityTown(Page.User.Identity.Name, cityTown);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblCityTown.Text = cityTown;
                        txtCityTown.Visible = false;
                        lblCityTown.Visible = true;
                        lblCityTown.Focus();
                        imgBtnTwn.ImageUrl = "~/Authorized/images/edit.png";
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }

                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - City: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void txtCityTown_TextChanged(object sender, EventArgs e)
    {
        CityRequired.Validate();
        CityTownValidator.Validate();

        if (CityTownValidator.IsValid && CityRequired.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string cityTown = txtCityTown.Text.Trim();

                string message = customer.saveProfile_cityTown(Page.User.Identity.Name, cityTown);
                if (message == "success")
                {
                    hideControls(ImgCity);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblCityTown.Text = cityTown;
                    txtCityTown.Visible = false;
                    lblCityTown.Visible = true;
                    lblCityTown.Focus();
                    imgBtnTwn.ImageUrl = "~/Authorized/images/edit.png";
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - City: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void checkPostalCode(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validatePostaclCode(txtPostalCode.Text, out status, out message);

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

    protected void imgBtnPstC_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnPstC.ImageUrl.Contains("edit.png"))
        {
            lblPostCode.Visible = false;
            txtPostalCode.Visible = true;
            // txtPostalCode.Focus();
            imgBtnPstC.Height = 14;
            imgBtnPstC.Width = 14;
            imgBtnPstC.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            PostalCodeValidator.Validate();

            if (PostalCodeValidator.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string postCode = txtPostalCode.Text.Trim();

                    string message = "";
                    if (postCode != lblPostCode.Text)
                    {
                        message = customer.saveProfile_postCode(Page.User.Identity.Name, postCode);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblPostCode.Text = postCode;
                        txtPostalCode.Visible = false;
                        lblPostCode.Visible = true;
                        lblPostCode.Focus();
                        imgBtnPstC.ImageUrl = "~/Authorized/images/edit.png";
                        if (String.IsNullOrEmpty(lblPostCode.Text))
                        { lblPostCode.Text = "..."; }
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }

                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - Postalcode: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void txtPostalCode_TextChanged(object sender, EventArgs e)
    {
        PostalCodeValidator.Validate();

        if (PostalCodeValidator.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string postCode = txtPostalCode.Text.Trim();

                string message = customer.saveProfile_postCode(Page.User.Identity.Name, postCode);
                if (message == "success")
                {
                    hideControls(ImgPostalCode);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblPostCode.Text = postCode;
                    txtPostalCode.Visible = false;
                    lblPostCode.Visible = true;
                    lblPostCode.Focus();
                    imgBtnPstC.ImageUrl = "~/Authorized/images/edit.png";
                    if (String.IsNullOrEmpty(lblPostCode.Text))
                    { lblPostCode.Text = "..."; }
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - Postalcode: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
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

    protected void imgBtnCtry_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnCtry.ImageUrl.Contains("edit.png"))
        {
            lblCountry.Visible = false;
            ddlCountry.Visible = true;
            //ddlCountry.Focus();
            imgBtnCtry.Height = 14;
            imgBtnCtry.Width = 14;
            imgBtnCtry.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            CountryValidator.Validate();

            if (CountryValidator.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string country = ddlCountry.SelectedValue;
                    string countDesc = ddlCountry.SelectedItem.Text;

                    string message = "";
                    if (countDesc != lblCountry.Text)
                    {
                        message = customer.saveProfile_country(Page.User.Identity.Name, country);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblCountry.Text = countDesc;
                        ddlCountry.Visible = false;
                        lblCountry.Visible = true;
                        lblCountry.Focus();
                        imgBtnCtry.ImageUrl = "~/Authorized/images/edit.png";
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }

                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - Country: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        CountryValidator.Validate();

        if (CountryValidator.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string country = ddlCountry.SelectedValue;
                string countDesc = ddlCountry.SelectedItem.Text;

                string message = customer.saveProfile_country(Page.User.Identity.Name, country);
                if (message == "success")
                {
                    hideControls(ImgCountry);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblCountry.Text = countDesc;
                    ddlCountry.Visible = false;
                    lblCountry.Visible = true;
                    lblCountry.Focus();
                    imgBtnCtry.ImageUrl = "~/Authorized/images/edit.png";
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - Country: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void checkEmail(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateEmailUpdate(txtEmail.Text, Page.User.Identity.Name, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            //txtNICNo.Focus();
        }
        else
        {
            args.IsValid = false;
            EmailValidator.ErrorMessage = message;
            txtEmail.Focus();
        }
    }

    protected void imgBtnEml_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnEml.ImageUrl.Contains("edit.png"))
        {
            lblEmail.Visible = false;
            txtEmail.Visible = true;
            //txtEmail.Focus();
            imgBtnEml.Height = 14;
            imgBtnEml.Width = 14;
            imgBtnEml.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            EmailRequired.Validate();
            //NICValidator.Validate();
            //DOBValidator.Validate();
            //GenderValidator.Validate();
            EmailValidator.Validate();

            if (EmailValidator.IsValid && EmailRequired.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string email = txtEmail.Text.Trim();

                    string message = "";
                    if (email != lblEmail.Text)
                    {
                        message = customer.saveProfile_email(Page.User.Identity.Name, email);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblEmail.Text = email;
                        txtEmail.Visible = false;
                        lblEmail.Visible = true;
                        lblEmail.Focus();
                        imgBtnEml.ImageUrl = "~/Authorized/images/edit.png";
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }

                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - email: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void txtEmail_TextChanged(object sender, EventArgs e)
    {
        EmailRequired.Validate();
        //NICValidator.Validate();
        //DOBValidator.Validate();
        //GenderValidator.Validate();
        EmailValidator.Validate();

        if (EmailValidator.IsValid && EmailRequired.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string email = txtEmail.Text.Trim();

                string message = customer.saveProfile_email(Page.User.Identity.Name, email);
                if (message == "success")
                {
                    hideControls(ImgEmail);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblEmail.Text = email;
                    txtEmail.Visible = false;
                    lblEmail.Visible = true;
                    lblEmail.Focus();
                    imgBtnEml.ImageUrl = "~/Authorized/images/edit.png";
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - email: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void checkMobileNo(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateMobileNumber(txtMobileNo.Text, ddlCountry.SelectedValue.ToString().Trim(), out status, out message);

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

    protected void imgBtnMbNo_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnMbNo.ImageUrl.Contains("edit.png"))
        {
            lblMobNum.Visible = false;
            txtMobileNo.Visible = true;
            // txtMobileNo.Focus();
            imgBtnMbNo.Height = 14;
            imgBtnMbNo.Width = 14;
            imgBtnMbNo.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            MobileNumRequired.Validate();
            MobileNoValidator.Validate();

            if (MobileNoValidator.IsValid && MobileNumRequired.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string mobNo = txtMobileNo.Text.Trim();

                    string message = "";
                    if (mobNo != lblMobNum.Text)
                    {
                        message = customer.saveProfile_mobileNum(Page.User.Identity.Name, mobNo);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblMobNum.Text = mobNo;
                        txtMobileNo.Visible = false;
                        lblMobNum.Visible = true;
                        lblMobNum.Focus();
                        imgBtnMbNo.ImageUrl = "~/Authorized/images/edit.png";
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }

                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - Mobile No: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void txtMobileNo_TextChanged(object sender, EventArgs e)
    {
        MobileNumRequired.Validate();
        MobileNoValidator.Validate();

        if (MobileNoValidator.IsValid && MobileNumRequired.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string mobNo = txtMobileNo.Text.Trim();

                string message = customer.saveProfile_mobileNum(Page.User.Identity.Name, mobNo);
                if (message == "success")
                {
                    hideControls(ImgMobile);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblMobNum.Text = mobNo;
                    txtMobileNo.Visible = false;
                    lblMobNum.Visible = true;
                    lblMobNum.Focus();
                    imgBtnMbNo.ImageUrl = "~/Authorized/images/edit.png";
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - Mobile No: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void checkHomeNo(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateHomeNumber(txtHomeNo.Text, out status, out message);

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

    protected void imgBtnHmNo_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnHmNo.ImageUrl.Contains("edit.png"))
        {
            lblHomNum.Visible = false;
            txtHomeNo.Visible = true;
            //txtHomeNo.Focus();
            imgBtnHmNo.Height = 14;
            imgBtnHmNo.Width = 14;
            imgBtnHmNo.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            HomeNoValidator.Validate();

            if (HomeNoValidator.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string homeNo = txtHomeNo.Text.Trim();

                    string message = "";

                    if (homeNo != lblHomNum.Text)
                    {
                        message = customer.saveProfile_homeNum(Page.User.Identity.Name, homeNo);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblHomNum.Text = homeNo;
                        txtHomeNo.Visible = false;
                        lblHomNum.Visible = true;
                        lblHomNum.Focus();
                        imgBtnHmNo.ImageUrl = "~/Authorized/images/edit.png";
                        if (String.IsNullOrEmpty(lblHomNum.Text))
                        { lblHomNum.Text = "..."; }
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }

                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - Home No: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void txtHomeNo_TextChanged(object sender, EventArgs e)
    {
        HomeNoValidator.Validate();

        if (HomeNoValidator.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string homeNo = txtHomeNo.Text.Trim();

                string message = customer.saveProfile_homeNum(Page.User.Identity.Name, homeNo);
                if (message == "success")
                {
                    hideControls(ImgHome);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblHomNum.Text = homeNo;
                    txtHomeNo.Visible = false;
                    lblHomNum.Visible = true;
                    lblHomNum.Focus();
                    imgBtnHmNo.ImageUrl = "~/Authorized/images/edit.png";
                    if (String.IsNullOrEmpty(lblHomNum.Text))
                    { lblHomNum.Text = "..."; }
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - Home No: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void checkOfficeNo(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validateOfficeNumber(txtOfficeNo.Text, out status, out message);

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

    protected void imgBtnOfNo_Click(object sender, ImageClickEventArgs e)
    {
        hideControls();
        if (imgBtnOfNo.ImageUrl.Contains("edit.png"))
        {
            lblOfcNum.Visible = false;
            txtOfficeNo.Visible = true;
            //txtOfficeNo.Focus();
            imgBtnOfNo.Height = 14;
            imgBtnOfNo.Width = 14;
            imgBtnOfNo.ImageUrl = "~/Authorized/images/save.png";
        }
        else
        {
            OfficeNoValidator.Validate();

            if (OfficeNoValidator.IsValid)
            {
                try
                {
                    CustProfile customer = new CustProfile();

                    string ofcNo = txtOfficeNo.Text.Trim();

                    string message = "";
                    if (ofcNo != lblOfcNum.Text)
                    {
                        message = customer.saveProfile_officeNum(Page.User.Identity.Name, ofcNo);
                    }
                    else
                    { message = "success"; }
                    if (message == "success")
                    {
                        lblStatusMesg.Text = "Profile successfully updated.";
                        lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                        lblOfcNum.Text = ofcNo;
                        txtOfficeNo.Visible = false;
                        lblOfcNum.Visible = true;
                        lblOfcNum.Focus();
                        imgBtnOfNo.ImageUrl = "~/Authorized/images/edit.png";
                        if (String.IsNullOrEmpty(lblOfcNum.Text))
                        { lblOfcNum.Text = "..."; }
                    }
                    else
                    {
                        lblStatusMesg.Text = message;
                        lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                    }

                }
                catch (Exception ex)
                {
                    log logger = new log();
                    logger.write_log("Failed at profile update - Office No: " + ex.ToString());
                    lblStatusMesg.Text = "Error occurred in profile update process.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void txtOfficeNo_TextChanged(object sender, EventArgs e)
    {
        OfficeNoValidator.Validate();

        if (OfficeNoValidator.IsValid)
        {
            try
            {
                CustProfile customer = new CustProfile();

                string ofcNo = txtOfficeNo.Text.Trim();

                string message = customer.saveProfile_officeNum(Page.User.Identity.Name, ofcNo);
                if (message == "success")
                {
                    hideControls(ImgOfiz);
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;

                    lblOfcNum.Text = ofcNo;
                    txtOfficeNo.Visible = false;
                    lblOfcNum.Visible = true;
                    lblOfcNum.Focus();
                    imgBtnOfNo.ImageUrl = "~/Authorized/images/edit.png";
                    if (String.IsNullOrEmpty(lblOfcNum.Text))
                    { lblOfcNum.Text = "..."; }
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update - Office No: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            //Page.Validate();
            lblStatusMesg.Text = "";

            try
            {
                CustProfile customer = new CustProfile();

                string title = ddlTitle.Text.Trim();
                string firstName = txtFirstName.Text.Trim();
                string lastName = txtLastName.Text.Trim();
                string otherNames = txtOtherNames.Text.Trim();
                string nicNum = lblNic.Text.Trim();
                int dateOfBirth = int.Parse(lblDatOfBirth.Text.Substring(0, 4) + lblDatOfBirth.Text.Substring(5, 2) + lblDatOfBirth.Text.Substring(8, 2));
                string gender = lblGender.Text;// rblGender.SelectedValue.ToString().Trim();
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
                string desgnation = txtOccupation.Text.Trim();


                // if (customer.IsRegisteredUser(username) == 0)
                // {
                string message = customer.saveProfileInfo(Page.User.Identity.Name, title, firstName, lastName, otherNames, email, nicNum,
                                                           dateOfBirth, gender, address1, address2, address3, address4, cityTown,
                                                           postalCode, country, mobileNum, homeNum, officeNum, desgnation);
                if (message == "success")
                {
                    lblStatusMesg.Text = "Profile successfully updated.";
                    lblStatusMesg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblStatusMesg.Text = message;
                    lblStatusMesg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                log logger = new log();
                logger.write_log("Failed at profile update: " + ex.ToString());
                lblStatusMesg.Text = "Error occurred in profile update process.";
                lblStatusMesg.ForeColor = System.Drawing.Color.Red;
            }

        }
    }

    private void hideControls(Image img)
    {
        #region
        //ImgTitle.Visible = false;
        //ImgFname.Visible = false;
        //ImgLName.Visible = false;
        //ImgOName.Visible = false;
        //ImgEmail.Visible = false;
        //ImgNIC.Visible = false;
        //ImgDOB.Visible = false;
        //ImgGender.Visible = false;
        //ImgAdd1.Visible = false;
        //ImgAdd2.Visible = false;
        //ImgAdd3.Visible = false;
        //ImgAdd4.Visible = false;
        //ImgCity.Visible = false;
        //ImgPostalCode.Visible = false;
        //ImgCountry.Visible = false;
        //ImgMobile.Visible = false;
        //ImgHome.Visible = false;
        //ImgOfiz.Visible = false;
        #endregion
        hideControls();

        img.Visible = true;
    }

    private void hideControls()
    {
        ImgTitle.Visible = false;
        ImgFname.Visible = false;
        ImgLName.Visible = false;
        ImgOName.Visible = false;
        ImgEmail.Visible = false;
        ImgNIC.Visible = false;
        ImgDOB.Visible = false;
        ImgGender.Visible = false;
        ImgAdd1.Visible = false;
        ImgAdd2.Visible = false;
        ImgAdd3.Visible = false;
        ImgAdd4.Visible = false;
        ImgCity.Visible = false;
        ImgPostalCode.Visible = false;
        ImgCountry.Visible = false;
        ImgMobile.Visible = false;
        ImgHome.Visible = false;
        ImgOfiz.Visible = false;
        ImgOcu2.Visible = false;
        ImgPrt.Visible = false;
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


    
}