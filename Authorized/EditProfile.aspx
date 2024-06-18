<%@ Page Title="" Language="C#" MasterPageFile="~/Common.master" AutoEventWireup="true"
    CodeFile="EditProfile.aspx.cs" SmartNavigation="true" Inherits="Authorized_EditProfile"
    MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/Common.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


     <style>
     .btn-dg
    { 
        background-color: #CF5D5D!important;
        border-color: #CF5D5D !important;
        color:White !important;
    }

     .btn-dg:focus
    { 
        background-color: #CF5D5D!important;
        border-color: #CF5D5D !important;
        color:White !important;
    }

         label {
             font-weight: normal !important;
         }
    </style>

        <style>

        @media screen and (max-width:1800px)
        {
           /*input[type=text]{width:50%}
           .select {
                width:50%;
           }*/
            input[type="text"], select{
                width:60%;
            }

            .form-control{
                width:60%;
            }

            span{
                width:60%;
            }
              element{
                width:60% !important;
            }

            legend {
                font-size: 100%;
                font-weight:bold;
                /*font-family: Tahoma;*/
            }

            fieldset{
                width:75%;
                margin: auto;
            }
            /*.alert
            {
                 width:75%;
                margin: auto;
            }*/
            /*select {
                 width:50%;
             }*/

        }
    </style>

    <style>

        @media screen and (max-width:600px)
        {
           /*input[type=text]{width:100%}
             .select {
                width:100%;
           }*/
             input[type=text] {
                width:100%;
            }
             select {
                 width:100%;
             }
              .form-control{
                width:100%;
            }

              span{
                width:100%;
            }
          
              element{
                width:100% !important;
            }

              legend {
                font-size: 90%;
                font-weight:bold
                /*font-family: Tahoma;*/
            }
              fieldset{
                width:100%;
            }
               /*.alert
            {
                 width:100%;
                margin: auto;
            }*/
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
           
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(beginRequest);

        function beginRequest() {
            prm._scrollPosition = null;

        }
    </script>
    <asp:UpdatePanel ID="Ggs14451" runat="server">
        <ContentTemplate>
            <div class="main-container" id="main-container">
                <div class="container">
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <center>
                                <h3>
                                    View Profile</h3>
                            </center>
                            <h4>
                                (Click on any field to edit the item)</h4>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <div class="form-group">
                                <table class="table">
                                    <tbody>
                                        <tr>
                                            <asp:Label ID="lblErrMesg" runat="server" ForeColor="Red" 
                                                Style="font-size: 12px"></asp:Label>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                <b>Title</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                &nbsp;<asp:DropDownList ID="ddlTitle" runat="server" AutoPostBack="True" 
                                                    CssClass="textbox" OnTextChanged="ddlTitle_TextChanged" Visible="False">
                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem>Mr.</asp:ListItem>
                                                    <asp:ListItem>Mrs.</asp:ListItem>
                                                    <asp:ListItem>Miss.</asp:ListItem>
                                                    <asp:ListItem>Master</asp:ListItem>
                                                    <asp:ListItem>Dr.</asp:ListItem>
                                                    <asp:ListItem>Doc.</asp:ListItem>
                                                    <asp:ListItem>Rev.</asp:ListItem>
                                                    <asp:ListItem>Dr.(Miss.)</asp:ListItem>
                                                    <asp:ListItem>Maj.</asp:ListItem>
                                                    <asp:ListItem>Prof.</asp:ListItem>
                                                    <asp:ListItem>Ms.</asp:ListItem>
                                                    <asp:ListItem>M/s.</asp:ListItem>
                                                    <asp:ListItem>Dr.(Mrs.)</asp:ListItem>
                                                    <asp:ListItem>Maj.Gen</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Image ID="ImgTitle" runat="server" AlternateText="Saved" 
                                                    BorderColor="#009933" BorderWidth="0px" Height="18px" 
                                                    ImageUrl="~/Authorized/images/ok.png" Visible="False" Width="18px" />
                                                <asp:ImageButton ID="imgBtnTitle" runat="server" Height="1px" 
                                                    ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnTitle_Click" />
                                            </td>
                                            <td>
                                                <asp:CustomValidator ID="TitleValidator" runat="server" 
                                                    ControlToValidate="ddlTitle" ForeColor="Red" OnServerValidate="checkTitle"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                <b>First name</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFirstName" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtFirstName" runat="server" AutoPostBack="True" 
                                                    class="form-control" MaxLength="25" OnTextChanged="txtFirstName_TextChanged" 
                                                    Visible="False"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" 
                                                    ControlToValidate="txtFirstName" ErrorMessage="First Name is required." 
                                                    ForeColor="Red" ToolTip="First name is required.">*</asp:RequiredFieldValidator>
                                                <asp:Image ID="ImgFname" runat="server" AlternateText="Saved" 
                                                    BorderColor="#009933" BorderWidth="0px" Height="18px" 
                                                    ImageUrl="~/Authorized/images/ok.png" Visible="False" Width="18px" />
                                                <asp:ImageButton ID="imgBtnFstN" runat="server" Height="1px" 
                                                    ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnFstN_Click" />
                                            </td>
                                            <td>
                                                <asp:CustomValidator ID="FnameValidator" runat="server" 
                                                    ControlToValidate="txtFirstName" ForeColor="Red" 
                                                    OnServerValidate="checkFirstname"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                <b>Last Name</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLastName" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtLastName" runat="server" AutoPostBack="True" 
                                                    class="form-control" MaxLength="50" OnTextChanged="txtLastName_TextChanged" 
                                                    Visible="False"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" 
                                                    ControlToValidate="txtLastName" ErrorMessage="Last Name is required." 
                                                    ForeColor="Red" ToolTip="Last name is required.">*</asp:RequiredFieldValidator>
                                                <asp:Image ID="ImgLName" runat="server" AlternateText="Saved" 
                                                    BorderColor="#009933" BorderWidth="0px" Height="18px" 
                                                    ImageUrl="~/Authorized/images/ok.png" Visible="False" Width="18px" />
                                                <asp:ImageButton ID="imgBtnLstN" runat="server" Height="1px" 
                                                    ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnLstN_Click" />
                                            </td>
                                            <td>
                                                <asp:CustomValidator ID="LNameValidator" runat="server" 
                                                    ControlToValidate="txtLastName" ForeColor="Red" 
                                                    OnServerValidate="checkLastname"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                <b>Middle Names</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOthNames" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtOtherNames" runat="server" AutoPostBack="True" 
                                                    class="form-control" MaxLength="100" OnTextChanged="txtOtherNames_TextChanged" 
                                                    Visible="False"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Image ID="ImgOName" runat="server" AlternateText="Saved" 
                                                    BorderColor="#009933" BorderWidth="0px" Height="18px" 
                                                    ImageUrl="~/Authorized/images/ok.png" Visible="False" Width="18px" />
                                                <asp:ImageButton ID="imgBtnOthN" runat="server" Height="1px" 
                                                    ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnOthN_Click" />
                                            </td>
                                            <td>
                                                <asp:CustomValidator ID="OthnameValidator" runat="server" 
                                                    ControlToValidate="txtOtherNames" ForeColor="Red" 
                                                    OnServerValidate="checkOthernames"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                <b>Email</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtEmail" runat="server" AutoPostBack="True" 
                                                    class="form-control" MaxLength="50" OnTextChanged="txtEmail_TextChanged" 
                                                    Visible="False"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" 
                                                    ControlToValidate="txtEmail" ErrorMessage="Email is required." ForeColor="Red" 
                                                    ToolTip="Email is required.">*</asp:RequiredFieldValidator>
                                                <asp:Image ID="ImgEmail" runat="server" AlternateText="Saved" BorderWidth="0px" 
                                                    ForeColor="#009933" Height="18px" ImageUrl="~/Authorized/images/ok.png" 
                                                    Visible="False" Width="18px" />
                                                <asp:ImageButton ID="imgBtnEml" runat="server" Height="1px" 
                                                    ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnEml_Click" />
                                            </td>
                                            <td>
                                                <asp:CustomValidator ID="EmailValidator" runat="server" 
                                                    ControlToValidate="txtEmail" ForeColor="Red" OnServerValidate="checkEmail"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                <b>Sri Lankan Citizen?</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCitizen" runat="server"></asp:Label>
                                                <asp:Image ID="ImgCitizen" runat="server" AlternateText="Saved" 
                                                    BorderWidth="0px" ForeColor="#009933" Height="18px" 
                                                    ImageUrl="~/Authorized/images/ok.png" Visible="False" Width="18px" />
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                <b>NIC Number</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNic" runat="server"></asp:Label>
                                                <asp:Image ID="ImgNIC" runat="server" AlternateText="Saved" BorderWidth="0px" 
                                                    ForeColor="#009933" Height="18px" ImageUrl="~/Authorized/images/ok.png" 
                                                    Visible="False" Width="18px" />
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <caption>
                                            <tr>
                                                <td width="20%">
                                                    <b>Passport Number</b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPasport" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txt_passport" runat="server" AutoPostBack="True" 
                                                        class="form-control" MaxLength="20" ontextchanged="txt_passport_TextChanged" 
                                                        Visible="False" CausesValidation="True"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <%--<asp:RequiredFieldValidator ID="reqPassport" runat="server" 
                                                        ControlToValidate="txt_passport" ErrorMessage="Passport Number is Required" 
                                                        ForeColor="Red" ToolTip="Passport Number is Required">*</asp:RequiredFieldValidator>--%>
                                                    <asp:Image ID="ImgPrt" runat="server" AlternateText="Saved" BorderWidth="0px" 
                                                        ForeColor="#009933" Height="18px" ImageUrl="~/Authorized/images/ok.png" 
                                                        Visible="False" Width="18px" />
                                                    <asp:ImageButton ID="imgBtImgPP" runat="server" Height="1px" 
                                                        ImageUrl="~/Authorized/images/edit.png" onclick="imgBtImgPP_Click" />
                                                </td>
                                                <td>
                                                    <asp:CustomValidator ID="PassportValidator" runat="server" ValidateEmptyText="true"
                                                        ControlToValidate="txt_passport" ForeColor="Red" 
                                                        OnServerValidate="checkPassport"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <caption>
                                                <tr>
                                                    <td width="20%">
                                                        <b>Date of Birth</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDatOfBirth" runat="server"></asp:Label>
                                                        <asp:Image ID="ImgDOB" runat="server" AlternateText="Saved" BorderWidth="0px" 
                                                            ForeColor="#009933" Height="18px" ImageUrl="~/Authorized/images/ok.png" 
                                                            Visible="False" Width="18px" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                        <b>Gender</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGender" runat="server"></asp:Label>
                                                        <asp:Image ID="ImgGender" runat="server" AlternateText="Saved" 
                                                            BorderWidth="0px" ForeColor="#009933" Height="18px" 
                                                            ImageUrl="~/Authorized/images/ok.png" Visible="False" Width="18px" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                        <b>Occupation</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblOccupation" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtOccupation" runat="server" AutoPostBack="True" 
                                                            class="form-control" MaxLength="30" OnTextChanged="txtOccupation_TextChanged" 
                                                            Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="ImgOcu2" runat="server" AlternateText="Saved" BorderWidth="0px" 
                                                            ForeColor="#009933" Height="18px" ImageUrl="~/Authorized/images/ok.png" 
                                                            Visible="False" Width="18px" />
                                                        <asp:ImageButton ID="imgBtImgOcu" runat="server" Height="1px" 
                                                            ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnOcu_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:CustomValidator ID="OcupationValidator1" runat="server" 
                                                            ControlToValidate="txtOccupation" ForeColor="Red" 
                                                            OnServerValidate="checkOccupation"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                        <b>Address Line 1</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAddrss1" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtAddress1" runat="server" AutoPostBack="True" 
                                                            class="form-control" MaxLength="30" OnTextChanged="txtAddress1_TextChanged" 
                                                            Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="Address1Required" runat="server" Enabled="false" EnableClientScript="false"
                                                            ControlToValidate="txtAddress1" ErrorMessage="Address 1 is required." 
                                                            ForeColor="Red" ToolTip="Address 1 is required.">*</asp:RequiredFieldValidator>
                                                        <asp:Image ID="ImgAdd1" runat="server" AlternateText="Saved" BorderWidth="0px" 
                                                            ForeColor="#009933" Height="18px" ImageUrl="~/Authorized/images/ok.png" 
                                                            Visible="False" Width="18px" />
                                                        <asp:ImageButton ID="imgBtnAd1" runat="server" Height="1px" 
                                                            ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnAd1_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:CustomValidator ID="Adrs1Validator" runat="server" 
                                                            ControlToValidate="txtAddress1" ForeColor="Red" 
                                                            OnServerValidate="checkAddress1"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                        <b>Address Line 2</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAddrss2" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtAddress2" runat="server" AutoPostBack="True" 
                                                            class="form-control" MaxLength="30" OnTextChanged="txtAddress2_TextChanged" 
                                                            Visible="False" Width="216px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="Address2Required" runat="server" Enabled="false" EnableClientScript="false"
                                                            ControlToValidate="txtAddress2" ErrorMessage="Address 2 is required." 
                                                            ForeColor="Red" ToolTip="Address 2 is required.">*</asp:RequiredFieldValidator>
                                                        <asp:Image ID="ImgAdd2" runat="server" AlternateText="Saved" BorderWidth="0px" 
                                                            ForeColor="#009933" Height="18px" ImageUrl="~/Authorized/images/ok.png" 
                                                            Visible="False" Width="18px" />
                                                        <asp:ImageButton ID="imgBtnAd2" runat="server" Height="1px" 
                                                            ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnAd2_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:CustomValidator ID="Adrs2Validator" runat="server" 
                                                            ControlToValidate="txtAddress2" ForeColor="Red" 
                                                            OnServerValidate="checkAddress2"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                        <b>Address Line 3</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAddrss3" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtAddress3" runat="server" AutoPostBack="True" 
                                                            class="form-control" MaxLength="30" OnTextChanged="txtAddress3_TextChanged" 
                                                            Visible="False" Width="216px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="ImgAdd3" runat="server" AlternateText="Saved" BorderWidth="0px" 
                                                            ForeColor="#009933" Height="18px" ImageUrl="~/Authorized/images/ok.png" 
                                                            Visible="False" Width="18px" />
                                                        <asp:ImageButton ID="imgBtnAd3" runat="server" Height="1px" 
                                                            ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnAd3_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:CustomValidator ID="Adrs3Validator" runat="server" 
                                                            ControlToValidate="txtAddress3" ForeColor="Red" 
                                                            OnServerValidate="checkAddress3"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                        <b>Address Line 4</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAddrss4" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtAddress4" runat="server" AutoPostBack="True" 
                                                            class="form-control" MaxLength="30" OnTextChanged="txtAddress4_TextChanged" 
                                                            Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="ImgAdd4" runat="server" AlternateText="Saved" BorderWidth="0px" 
                                                            ForeColor="#009933" Height="18px" ImageUrl="~/Authorized/images/ok.png" 
                                                            Visible="False" Width="18px" />
                                                        <asp:ImageButton ID="imgBtnAd4" runat="server" Height="1px" 
                                                            ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnAd4_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:CustomValidator ID="Adrs4Validator" runat="server" 
                                                            ControlToValidate="txtAddress4" ForeColor="Red" 
                                                            OnServerValidate="checkAddress4"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                        <b>City/ Town</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCityTown" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtCityTown" runat="server" AutoPostBack="True" 
                                                            class="form-control" MaxLength="30" OnTextChanged="txtCityTown_TextChanged" 
                                                            Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="CityRequired" runat="server" 
                                                            ControlToValidate="txtCityTown" ErrorMessage="City is required." 
                                                            ForeColor="Red" ToolTip="City is required.">*</asp:RequiredFieldValidator>
                                                        <asp:Image ID="ImgCity" runat="server" AlternateText="Saved" BorderWidth="0px" 
                                                            ForeColor="#009933" Height="18px" ImageUrl="~/Authorized/images/ok.png" 
                                                            Visible="False" Width="18px" />
                                                        <asp:ImageButton ID="imgBtnTwn" runat="server" Height="1px" 
                                                            ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnTwn_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:CustomValidator ID="CityTownValidator" runat="server" 
                                                            ControlToValidate="txtCityTown" ForeColor="Red" 
                                                            OnServerValidate="checkCityTown"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                        <b>Postal/ ZIP code</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPostCode" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtPostalCode" runat="server" AutoPostBack="True" 
                                                            class="form-control" MaxLength="10" OnTextChanged="txtPostalCode_TextChanged" 
                                                            Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="ImgPostalCode" runat="server" AlternateText="Saved" 
                                                            BorderWidth="0px" ForeColor="#009933" Height="18px" 
                                                            ImageUrl="~/Authorized/images/ok.png" Visible="False" Width="18px" />
                                                        <asp:ImageButton ID="imgBtnPstC" runat="server" Height="1px" 
                                                            ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnPstC_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:CustomValidator ID="PostalCodeValidator" runat="server" 
                                                            ControlToValidate="txtPostalCode" ForeColor="Red" 
                                                            OnServerValidate="checkPostalCode"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                        <b>Country</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" 
                                                            CssClass="textbox" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" 
                                                            Visible="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="ImgCountry" runat="server" AlternateText="Saved" 
                                                            BorderWidth="0px" ForeColor="#009933" Height="18px" 
                                                            ImageUrl="~/Authorized/images/ok.png" Visible="False" Width="18px" />
                                                        <asp:ImageButton ID="imgBtnCtry" runat="server" Height="1px" 
                                                            ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnCtry_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:CustomValidator ID="CountryValidator" runat="server" 
                                                            ControlToValidate="ddlCountry" ForeColor="Red" OnServerValidate="checkCountry"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                        <b>Mobile Number</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMobNum" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtMobileNo" runat="server" AutoPostBack="True" 
                                                            class="form-control" MaxLength="15" OnTextChanged="txtMobileNo_TextChanged" 
                                                            Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="MobileNumRequired" runat="server" 
                                                            ControlToValidate="txtMobileNo" ErrorMessage="Mobile Number is required" 
                                                            ForeColor="Red" ToolTip="Mobile Number is required">*</asp:RequiredFieldValidator>
                                                        <asp:Image ID="ImgMobile" runat="server" AlternateText="Saved" 
                                                            BorderWidth="0px" ForeColor="#009933" Height="18px" 
                                                            ImageUrl="~/Authorized/images/ok.png" Visible="False" Width="18px" />
                                                        <asp:ImageButton ID="imgBtnMbNo" runat="server" Height="1px" 
                                                            ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnMbNo_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:CustomValidator ID="MobileNoValidator" runat="server" 
                                                            ControlToValidate="txtMobileNo" ForeColor="Red" 
                                                            OnServerValidate="checkMobileNo"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                        <b>Home Number</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblHomNum" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtHomeNo" runat="server" AutoPostBack="True" 
                                                            class="form-control" MaxLength="15" OnTextChanged="txtHomeNo_TextChanged" 
                                                            Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="ImgHome" runat="server" AlternateText="Saved" BorderWidth="0px" 
                                                            ForeColor="#009933" Height="18px" ImageUrl="~/Authorized/images/ok.png" 
                                                            Visible="False" Width="18px" />
                                                        <asp:ImageButton ID="imgBtnHmNo" runat="server" Height="1px" 
                                                            ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnHmNo_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:CustomValidator ID="HomeNoValidator" runat="server" 
                                                            ControlToValidate="txtHomeNo" ForeColor="Red" OnServerValidate="checkHomeNo"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                        <b>Office Number</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblOfcNum" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtOfficeNo" runat="server" AutoPostBack="True" 
                                                            class="form-control" MaxLength="20" OnTextChanged="txtOfficeNo_TextChanged" 
                                                            Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="ImgOfiz" runat="server" AlternateText="Saved" BorderWidth="0px" 
                                                            ForeColor="#009933" Height="18px" ImageUrl="~/Authorized/images/ok.png" 
                                                            Visible="False" Width="18px" />
                                                        <asp:ImageButton ID="imgBtnOfNo" runat="server" Height="1px" 
                                                            ImageUrl="~/Authorized/images/edit.png" OnClick="imgBtnOfNo_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:CustomValidator ID="OfficeNoValidator" runat="server" 
                                                            ControlToValidate="txtOfficeNo" ForeColor="Red" 
                                                            OnServerValidate="checkOfficeNo"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <center>
                                                            <asp:Button ID="btnSubmit" runat="server" class="btn-primary btn-xs" 
                                                                OnClick="btnSubmit_Click" Text="Save Changes" Visible="false" />
                                                        </center>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="color: Red;">
                                                        <asp:Label ID="lblStatusMesg" runat="server" Font-Size="Large" Visible="False"></asp:Label>
                                                        &nbsp;<br />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </caption>
                                        </caption>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    
                </div>
                <div class="clr">
                </div>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                    AssociatedUpdatePanelID="Ggs14451" DisplayAfter="10">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <img src="/Images/load.gif" />
                            <br />
                            <asp:Label ID="lblWait" runat="server" Text=" Please wait... " />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                
                </ContentTemplate>
    </asp:UpdatePanel>

    <link href="/css/jquery-ui.css" rel="stylesheet" />
    <script src="/js/jquery-ui.js"></script>

    <script type="text/javascript" language="javascript">

        $("[id$='lblFirstName']").css('cursor', 'pointer');
        $("[id$='lblTitle']").css('cursor', 'pointer');
        $("[id$='lblLastName']").css('cursor', 'pointer');
        $("[id$='lblOthNames']").css('cursor', 'pointer');
        $("[id$='lblEmail']").css('cursor', 'pointer');
        //$("[id$='lblNic']").css('cursor', 'pointer');
        //$("[id$='lblDatOfBirth']").css('cursor', 'pointer');
        //$("[id$='lblGender']").css('cursor', 'pointer');
        $("[id$='lblOccupation']").css('cursor', 'pointer');
        $("[id$='lblAddrss1']").css('cursor', 'pointer');
        $("[id$='lblAddrss2']").css('cursor', 'pointer');
        $("[id$='lblAddrss3']").css('cursor', 'pointer');
        $("[id$='lblAddrss4']").css('cursor', 'pointer');
        $("[id$='lblCityTown']").css('cursor', 'pointer');
        $("[id$='lblPostCode']").css('cursor', 'pointer');
        $("[id$='lblCountry']").css('cursor', 'pointer');
        $("[id$='lblMobNum']").css('cursor', 'pointer');
        $("[id$='lblHomNum']").css('cursor', 'pointer');
        $("[id$='lblOfcNum']").css('cursor', 'pointer');
        $("[id$='lblPasport']").css('cursor', 'pointer');


        var dely = 100;
        var fade = 1200;

        $("[id$='ImgTitle']").delay(dely).fadeOut(fade);
        $("[id$='ImgFname']").delay(dely).fadeOut(fade);
        $("[id$='ImgLName']").delay(dely).fadeOut(fade);
        $("[id$='ImgOName']").delay(dely).fadeOut(fade);
        $("[id$='ImgEmail']").delay(dely).fadeOut(fade);
        //$("[id$='ImgNIC']").delay(dely).fadeOut(fade);
        //$("[id$='ImgDOB']").delay(dely).fadeOut(fade);
        //$("[id$='ImgGender']").delay(dely).fadeOut(fade);
        $("[id$='ImgOcu2']").delay(dely).fadeOut(fade);
        $("[id$='ImgAdd1']").delay(dely).fadeOut(fade);
        $("[id$='ImgAdd2']").delay(dely).fadeOut(fade);
        $("[id$='ImgAdd3']").delay(dely).fadeOut(fade);
        $("[id$='ImgAdd4']").delay(dely).fadeOut(fade);
        $("[id$='ImgCity']").delay(dely).fadeOut(fade);
        $("[id$='ImgPostalCode']").delay(dely).fadeOut(fade);
        $("[id$='ImgCountry']").delay(dely).fadeOut(fade);
        $("[id$='ImgMobile']").delay(dely).fadeOut(fade);
        $("[id$='ImgHome']").delay(dely).fadeOut(fade);
        $("[id$='ImgOfiz']").delay(dely).fadeOut(fade);
        $("[id$='ImgPrt']").delay(dely).fadeOut(fade);



        $("[id$='lblFirstName']").click(function () {
            $("input[id$='imgBtnFstN']").click();
        });

        $("[id$='lblTitle']").click(function () {
            $("input[id$='imgBtnTitle']").click();
        });

        $("[id$='lblLastName']").click(function () {
            $("input[id$='imgBtnLstN']").click()
        });

        $("[id$='lblOthNames']").click(function () {
            $("input[id$='imgBtnOthN']").click()
        });

        $("[id$='lblEmail']").click(function () {
            $("input[id$='imgBtnEml']").click()
        });

        //    $("[id$='lblNic']").click(function () {
        //        $("input[id$='imgBtnNic']").click()
        //    });

        //    $("[id$='lblDatOfBirth']").click(function () {
        //        $("input[id$='imgBtnDtBrth']").click()
        //    });

        //    $("[id$='lblGender']").click(function () {
        //        $("input[id$='imgBtnGen']").click()
        //    });

        $("[id$='lblOccupation']").click(function () {
            $("input[id$='imgBtImgOcu']").click();
        });

        $("[id$='lblAddrss1']").click(function () {
            $("input[id$='imgBtnAd1']").click()
        });

        $("[id$='lblAddrss2']").click(function () {
            $("input[id$='imgBtnAd2']").click()
        });

        $("[id$='lblAddrss3']").click(function () {
            $("input[id$='imgBtnAd3']").click()
        });

        $("[id$='lblAddrss4']").click(function () {
            $("input[id$='imgBtnAd4']").click();
        });

        $("[id$='lblCityTown']").click(function () {
            $("input[id$='imgBtnTwn']").click()
        });
        ///////////

        $("[id$='lblPostCode']").click(function () {
            $("input[id$='imgBtnPstC']").click()
        });

        $("[id$='lblCountry']").click(function () {
            $("input[id$='imgBtnCtry']").click()
        });

        $("[id$='lblMobNum']").click(function () {
            $("input[id$='imgBtnMbNo']").click()
        });

        $("[id$='lblHomNum']").click(function () {
            $("input[id$='imgBtnHmNo']").click()
        });

        $("[id$='lblOfcNum']").click(function () {
            $("input[id$='imgBtnOfNo']").click()
        });

        $("[id$='lblPasport']").click(function () {
            $("input[id$='imgBtImgPP']").click()
        });

        $(function () {
            today = new Date();
            var month, day, year;
            year = today.getFullYear();
            month = today.getMonth();
            date = today.getDate();
            year = today.getFullYear() - 60;
            var backdate = new Date(year, month, date)
            var backdate1 = new Date(year, month, date)

            $("input[id$='txtDateOfBirth']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, defaultDate: backdate, yearRange: '-70:-18' });


        });

        $(function () {
            $("input[id$='txtOccupation']").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("EditProfile.aspx/GetJobs") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    // $("input[id$='hfCustomerId']").val(i.item.val);
                },
                minLength: 1
            }).keyup(function (e) {
                if (e.which === 13) {
                    $(".ui-menu-item").hide();
                }
            });
        });


        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                var dely = 100;
                var fade = 1200;

                $("[id$='lblFirstName']").css('cursor', 'pointer');
                $("[id$='lblTitle']").css('cursor', 'pointer');
                $("[id$='lblLastName']").css('cursor', 'pointer');
                $("[id$='lblOthNames']").css('cursor', 'pointer');
                $("[id$='lblEmail']").css('cursor', 'pointer');
                //$("[id$='lblNic']").css('cursor', 'pointer');
                //$("[id$='lblDatOfBirth']").css('cursor', 'pointer');
                //$("[id$='lblGender']").css('cursor', 'pointer');
                $("[id$='lblOccupation']").css('cursor', 'pointer');
                $("[id$='lblAddrss1']").css('cursor', 'pointer');
                $("[id$='lblAddrss2']").css('cursor', 'pointer');
                $("[id$='lblAddrss3']").css('cursor', 'pointer');
                $("[id$='lblAddrss4']").css('cursor', 'pointer');
                $("[id$='lblCityTown']").css('cursor', 'pointer');
                $("[id$='lblPostCode']").css('cursor', 'pointer');
                $("[id$='lblCountry']").css('cursor', 'pointer');
                $("[id$='lblMobNum']").css('cursor', 'pointer');
                $("[id$='lblHomNum']").css('cursor', 'pointer');
                $("[id$='lblOfcNum']").css('cursor', 'pointer');
                $("[id$='lblPasport']").css('cursor', 'pointer');


                $("[id$='ImgTitle']").delay(dely).fadeOut(fade);
                $("[id$='ImgFname']").delay(dely).fadeOut(fade);
                $("[id$='ImgLName']").delay(dely).fadeOut(fade);
                $("[id$='ImgOName']").delay(dely).fadeOut(fade);
                $("[id$='ImgEmail']").delay(dely).fadeOut(fade);
                //$("[id$='ImgNIC']").delay(dely).fadeOut(fade);
                //$("[id$='ImgDOB']").delay(dely).fadeOut(fade);
                //$("[id$='ImgGender']").delay(dely).fadeOut(fade);
                $("[id$='ImgOcu2']").delay(dely).fadeOut(fade);
                $("[id$='ImgAdd1']").delay(dely).fadeOut(fade);
                $("[id$='ImgAdd2']").delay(dely).fadeOut(fade);
                $("[id$='ImgAdd3']").delay(dely).fadeOut(fade);
                $("[id$='ImgAdd4']").delay(dely).fadeOut(fade);
                $("[id$='ImgCity']").delay(dely).fadeOut(fade);
                $("[id$='ImgPostalCode']").delay(dely).fadeOut(fade);
                $("[id$='ImgCountry']").delay(dely).fadeOut(fade);
                $("[id$='ImgMobile']").delay(dely).fadeOut(fade);
                $("[id$='ImgHome']").delay(dely).fadeOut(fade);
                $("[id$='ImgOfiz']").delay(dely).fadeOut(fade);
                $("[id$='ImgPrt']").delay(dely).fadeOut(fade);


                $("[id$='lblFirstName']").click(function () {
                    $("input[id$='imgBtnFstN']").click();
                });


                $("[id$='lblTitle']").click(function () {
                    $("input[id$='imgBtnTitle']").click();
                });

                $("[id$='lblLastName']").click(function () {
                    $("input[id$='imgBtnLstN']").click()
                });

                $("[id$='lblOthNames']").click(function () {
                    $("input[id$='imgBtnOthN']").click()
                });

                $("[id$='lblEmail']").click(function () {
                    $("input[id$='imgBtnEml']").click()
                });

                //            $("[id$='lblNic']").click(function () {
                //                $("input[id$='imgBtnNic']").click()
                //            });

                //            $("[id$='lblDatOfBirth']").click(function () {
                //                $("input[id$='imgBtnDtBrth']").click()
                //            });

                //            $("[id$='lblGender']").click(function () {
                //                $("input[id$='imgBtnGen']").click()
                //            });

                $("[id$='lblOccupation']").click(function () {
                    $("input[id$='imgBtImgOcu']").click();
                });

                $("[id$='lblAddrss1']").click(function () {
                    $("input[id$='imgBtnAd1']").click()
                });

                $("[id$='lblAddrss2']").click(function () {
                    $("input[id$='imgBtnAd2']").click()
                });

                $("[id$='lblAddrss3']").click(function () {
                    $("input[id$='imgBtnAd3']").click()
                });

                $("[id$='lblAddrss4']").click(function () {
                    $("input[id$='imgBtnAd4']").click()
                });

                $("[id$='lblCityTown']").click(function () {
                    $("input[id$='imgBtnTwn']").click()
                });
                ///////////

                $("[id$='lblPostCode']").click(function () {
                    $("input[id$='imgBtnPstC']").click()
                });

                $("[id$='lblCountry']").click(function () {
                    $("input[id$='imgBtnCtry']").click()
                });

                $("[id$='lblMobNum']").click(function () {
                    $("input[id$='imgBtnMbNo']").click()
                });

                $("[id$='lblHomNum']").click(function () {
                    $("input[id$='imgBtnHmNo']").click()
                });

                $("[id$='lblOfcNum']").click(function () {
                    $("input[id$='imgBtnOfNo']").click()
                });

                $("[id$='lblPasport']").click(function () {
                    $("input[id$='imgBtImgPP']").click()
                });



                today = new Date();
                var month, day, year;
                year = today.getFullYear();
                month = today.getMonth();
                date = today.getDate();
                year = today.getFullYear() - 60;
                var backdate = new Date(year, month, date)
                var backdate1 = new Date(year, month, date)


                $("input[id$='txtDateOfBirth']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, defaultDate: backdate, yearRange: '-70:-18' });


                $("input[id$='txtOccupation']").autocomplete({

                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("EditProfile.aspx/GetJobs") %>',
                            data: "{ 'prefix': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item.split('-')[0],
                                        val: item.split('-')[1]
                                    }
                                }))
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        // $("input[id$='hfCustomerId']").val(i.item.val);
                    },
                    minLength: 1
                }).keyup(function (e) {
                    if (e.which === 13) {
                        $(".ui-menu-item").hide();
                    }
                }); ;

            }

        }); 

</script>
</asp:Content>
