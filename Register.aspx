<%@ Page Title="" Language="C#" MasterPageFile="~/RegisterSite.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

      <link href="assets/css/fieldset.css" rel="stylesheet" />

<style>
 .panel {
    border: none;
    box-shadow: none;
    padding-bottom:0;
    padding-top: 0; 
}

.refreshImage
{
  width:80px; 
  height:30px; 
}

     .divWaiting{
   
position: fixed;
background-color: #FAFAFA;
z-index: 2147483647 !important;
opacity: 0.8;
overflow: hidden;
text-align: center; top: 0; left: 0;
height: 100%;
width: 100%;
padding-top:20%;
} 

     fieldset {
    font-family: sans-serif !important;
    border: 1px solid #e5e5e5 !important;
    border-radius: 5px !important;
    padding: 15px !important;
}

    legend {
        width: 25%;
        border-bottom: none;
    }

    select.form-control:not([size]):not([multiple]) {

    height: auto;
    }

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <link href="/css/jquery-ui.css" rel="stylesheet" />
     <script src="/js/jquery-ui.js"></script>

<script type = "text/javascript">
    function VerifyAndSave() {
        if (typeof (Page_ClientValidate) == 'function') {
            Page_ClientValidate();
        }
        if (Page_IsValid) {
            alert("Validations successful");
            
        }
        else {
            alert("Validations not successful");
        }
    }
</script>

<form id="RegisterForm" runat="server">
<div id="mine" name="mine">
<div class="main-container" id="main-container">


        <div class="container">

                    <br /><br />
                    <div style="align:center">
                     <fieldset>
  <legend>   Sign Up for Your New Account
                           </legend>

 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

          <asp:UpdatePanel ID="UpdatePanel1" runat="server">       
     <ContentTemplate>

    <div class="row">



        <div class="col-xs-1">
                </div>
        <div class="col-xs-10">

       

             <%--   <div class="panel-heading">--%>
                 <%--   Sign Up for Your New Account--%>

                   
             <%--   </div>
            --%>

                    <div class="row">
                        <label for="inputEmail3" class="col-sm-3 control-label">
                            Username *
                        </label>
                        <div class="col-sm-6">
                        <asp:TextBox ID="txtUserName" runat="server" MaxLength="15"
                         AutoPostBack="True" 
                        ontextchanged="txtUserName_TextChanged" class="form-control" placeholder="Enter your username" autocomplete="new-password"></asp:TextBox>

                         <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                        ControlToValidate="txtUserName" ErrorMessage="User Name is required." 
                        ToolTip="User Name is required." ForeColor="Red" Font-Size="Small">User Name is required.</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-3">
                        <asp:CustomValidator ID="UsernameValidator" runat="server" ForeColor="Red" 
                        ControlToValidate="txtUserName" onservervalidate="checkUsername"></asp:CustomValidator>
                        </div>
                    </div>


 

 
         </div>
          <div class="col-xs-1">
          </div>



          </div>

           
                        <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" 
runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
  <div class="divWaiting">  
  <img src="/Images/load.gif" style="position: fixed;left: 50%;top: 40%"/><br />       
	<asp:Label ID="lblWait" runat="server" style="position: fixed;left: 50%;top: 47%" Text=" Please wait... " />
     


  </div>
</ProgressTemplate>
</asp:UpdateProgress>
<br />

 </ContentTemplate>
         </asp:UpdatePanel>


          <div class="row">
        <div class="col-xs-1">
                </div>
        <div class="col-xs-10">
           
                    <div class="row">
                        <label for="inputPassword" class="col-sm-3 control-label">
                            Password *
                        </label>
                        <div class="col-sm-6">

                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="15" TextMode="Password" class="form-control" placeholder="Enter a password" data-toggle="collapse" data-parent="#effect" data-placement="bottom"  autocomplete="new-password">
                        </asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                        ControlToValidate="txtPassword" ErrorMessage="Password is required." 
                        ToolTip="Password is required." ForeColor="Red" Font-Size="Small">Password is required.</asp:RequiredFieldValidator>
                          
                        </div>

                        <div class="col-sm-3"> 
                        <%--<asp:RegularExpressionValidator ID="PasswordValidator" runat="server" 
                        ControlToValidate="txtPassword" 
                        ErrorMessage="Password should conform to password rules" 
                        ValidationExpression="(?=^.{8,15}$)(?=.*\d)(?=.*[!@#$%^&*_]+)(?![.\n])(?=.*[A-Za-z]).*$" 
                        ForeColor="Red"></asp:RegularExpressionValidator>--%>
                         <asp:RegularExpressionValidator ID="PasswordValidator" runat="server" 
                        ControlToValidate="txtPassword" 
                        ErrorMessage="Password should conform to password rules" 
                        ValidationExpression="(?=^.{8,15}$)(?=.*\d)(?=.*[A-Za-z]).*$" 
                        ForeColor="Red"></asp:RegularExpressionValidator>

                        </div>
                    </div>
        
                <div class="row">
                    <div class="col-sm-3">
                        </div>

                    <div class="col-sm-9">
                        
                     <div id="effect" class="collapse in">
                      
                        <%--<div class="col-sm-6">--%>
                            <ul class="list-group" style="font-size:0.9em; border: 1px solid #CCC;">
                                <li class="list-group-item" style="border-width:0; font-size:0.9em;">Password should be 8 - 15 characters long</li>
                                <li class="list-group-item" style="border-width:0; font-size:0.9em;">Contain one or more letters</li> 
                                <li class="list-group-item" style="border-width:0; font-size:0.9em;">Contain one or more numeric values</li> 
                                <li class="list-group-item" style="border-width:0; font-size:0.9em; background-color:#E5E4E2;"><span> <a class="btn-link"  target="_blank" href="/PasswordHints.aspx"><strong>Hints for a strong password</strong> </a> </span> </li> 
                            </ul>
                       <%-- </div>--%>
                       <%-- <div class="col-sm-3">
                        </div>--%>
                    </div>
                        </div>

                    </div>
           
             
                    
                    <div class="row">
                        <label for="inputPasswordConfirm" class="col-sm-3 control-label">
                            Confirm *</span>
                        </label>
                        <div class="col-sm-6">
                        <asp:TextBox ID="txtConfirmPassword" runat="server" 
                        MaxLength="15" TextMode="Password" class="form-control" placeholder="Re-type your password" autocomplete="new-password"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" 
                        ControlToValidate="txtConfirmPassword" 
                        ErrorMessage="Confirm Password is required." 
                        ToolTip="Confirm Password is required." ForeColor="Red" Font-Size="Small">Confirm Password is required.</asp:RequiredFieldValidator>   
                        </div>
                        <div class="col-sm-3">
                         <asp:CompareValidator ID="PasswordCompare" runat="server" 
                        ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" 
                        Display="Dynamic" 
                        ErrorMessage="The Password and Confirmation Password must match." ForeColor="Red" 
                        ></asp:CompareValidator>
                        </div>
                    </div>
                    </div>
                     <div class="col-xs-1">
                </div>
                </div>

                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"> 
            <ContentTemplate>

              <div class="row">
        <div class="col-xs-1">
                </div>
        <div class="col-xs-10">

                   <%-- <div class="panel">--%>
        
                    <div class="row">
                        <label for="inputTitle" class="col-sm-3 control-label">
                            Title *
                        </label>
                        <div class="col-sm-6">
                  
                            <asp:DropDownList ID="ddlTitle" runat="server" class="form-control" 
                                AutoPostBack="True"  >
                        <asp:ListItem Value="0">Select</asp:ListItem>
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
                            <asp:RequiredFieldValidator ID="Req_Title" ForeColor="Red" Font-Size="Small" ControlToValidate="ddlTitle" InitialValue="0" runat="server" ErrorMessage="RequiredFieldValidator">Title is required.</asp:RequiredFieldValidator>
                        </div>
                  <%--      <div class="col-sm-2">
                        </div>--%>
                        <div class="col-sm-3">
                        <asp:CustomValidator ID="TitleValidator" runat="server" ForeColor="Red" 
                        ControlToValidate="ddlTitle" onservervalidate="checkTitle"></asp:CustomValidator>
                        </div>
                    </div>
                    <%--<br/>--%>
          
                    <div class="row">
                        <label for="inputEnterFirstName" class="col-sm-3 control-label">
                            First Name *
                        </label>
                        <div class="col-sm-6">
                        <asp:TextBox ID="txtFirstName" runat="server" 
                        MaxLength="25" class="form-control" placeholder="Enter your first name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" 
                        ControlToValidate="txtFirstName" 
                        ErrorMessage="First Name is required." 
                        ToolTip="First name is required." ForeColor="Red" Font-Size="Small">First Name is required.</asp:RequiredFieldValidator>
                        </div>
                          <div class="col-sm-3">
                            <asp:CustomValidator ID="FnameValidator" runat="server" 
                        ControlToValidate="txtFirstName" ForeColor="Red" 
                        onservervalidate="checkFirstname"></asp:CustomValidator>
                        </div>
                    </div>
   
                    <div class="row">
                        <label for="inputEnterLastName" class="col-sm-3 control-label">
                            Last Name *
                        </label>
                        <div class="col-sm-6">
                        <asp:TextBox ID="txtLastName" runat="server" 
                        MaxLength="50" class="form-control" placeholder="Enter your last name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" 
                        ControlToValidate="txtLastName" 
                        ErrorMessage="Last Name is required." 
                        ToolTip="Last name is required." ForeColor="Red" Font-Size="Small">Last Name is required.</asp:RequiredFieldValidator>
                           
                        </div>
                         <div class="col-sm-3">
                            <asp:CustomValidator ID="LNameValidator" runat="server" 
                        ControlToValidate="txtLastName" ForeColor="Red" 
                        onservervalidate="checkLastname"></asp:CustomValidator>
                        </div>
                    </div>
    
                    <div class="row">
                        <label for="inputEnterLastName" class="col-sm-3 control-label">
                            Middle Names
                        </label>
                        <div class="col-sm-6">
                        <asp:TextBox ID="txtOtherNames" runat="server" 
                        MaxLength="100" class="form-control" placeholder="Enter other names"></asp:TextBox>
                           
                        </div>
                        <div class="col-sm-3">   
                        <asp:CustomValidator ID="OthnameValidator" runat="server" 
                        ControlToValidate="txtOtherNames" ForeColor="Red" 
                        onservervalidate="checkOthernames"></asp:CustomValidator></div>
                    </div>
                    <br />
      
                    
      

      <div class="row">
                        <label for="rdo_srilankan" class="col-sm-3 control-label">
                            Are you a Sri Lankan Citizen?
                        </label>
                        <div class="col-sm-6">

                         <asp:RadioButtonList ID="rdo_srilankan" runat="server" AutoPostBack="True" 
                                    RepeatDirection="Horizontal" style="margin-left: 0px" 
                                onselectedindexchanged="rdo_srilankan_SelectedIndexChanged" >
                                    <asp:ListItem Value="Y" Selected="True">Yes &nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                     
                     
                        </div>
                         <div class="col-sm-3">
                        <asp:CustomValidator ID="rdoCitizenValidator" runat="server" ValidateEmptyText="true"
                        ControlToValidate="rdo_srilankan" ForeColor="Red" onservervalidate="checkCitizen"></asp:CustomValidator>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <label for="inputEnterEmail" class="col-sm-3 control-label">
                            Country *
                        </label>
                        <div class="col-sm-6">
                        <asp:DropDownList ID="ddlCountry" runat="server" class="form-control" 
                                AppendDataBoundItems="True">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="Req_country" ForeColor="Red" Font-Size="Small" ControlToValidate="ddlCountry" InitialValue="0" runat="server" ErrorMessage="Country is required.">Country is required.</asp:RequiredFieldValidator>
                    </div>
                    
                       
                        <div class="col-sm-3">
                        <asp:CustomValidator ID="CountryValidator" runat="server" 
                        ControlToValidate="ddlCountry" ForeColor="Red" onservervalidate="checkCountry"></asp:CustomValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label for="lit_nic_lbl" class="col-sm-3 control-label">
                            <asp:Literal ID="lit_nic_lbl" Text="NIC Number *" runat="server" />
                        </label>
                        <div class="col-sm-6">

                        <asp:TextBox ID="txtNICNo" runat="server" 
                        MaxLength="12" AutoPostBack="True" 
                        ontextchanged="txtNICNo_TextChanged" class="form-control"
                        placeholder="Enter NIC number" ></asp:TextBox>

                         <asp:RequiredFieldValidator ID="NICRequired" runat="server" 
                        ControlToValidate="txtNICNo" 
                        ErrorMessage="NIC Number is required" 
                        ToolTip="NIC Number is required." ForeColor="Red" Font-Size="Small">NIC Number is required.</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-3">
                        <asp:CustomValidator ID="NICValidator" runat="server" ForeColor="Red" ValidateEmptyText="true"
                        ControlToValidate="txtNICNo" onservervalidate="checkNIC"></asp:CustomValidator>
                        </div>
                    </div>


                     <div class="row">
                        <label for="lit_pp_lbl" class="col-sm-3 control-label">
                             <asp:Literal ID="lit_pp_lbl" Text="Passport Number" runat="server" />
                        </label>
                        <div class="col-sm-6">

                        <asp:TextBox ID="txtPPNo" runat="server" 
                        MaxLength="20" AutoPostBack="True" class="form-control"
                        placeholder="Enter Passport number" ontextchanged="txtPPNo_TextChanged" ></asp:TextBox>

                         <asp:RequiredFieldValidator ID="ReqPPValidator" runat="server" 
                        ControlToValidate="txtPPNo"  Enabled="false"
                        ErrorMessage="Passport Number is requiredd" 
                        ToolTip="Passport Number is required." ForeColor="Red" Font-Size="Small">Passport Number is required.</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-3">
                        <asp:CustomValidator ID="PassportValidator" runat="server" ForeColor="Red" ValidateEmptyText="true"
                        ControlToValidate="txtPPNo" onservervalidate="checkPassport" Enabled="true"></asp:CustomValidator>
                        </div>
                    </div>
           
                    <div class="row">
                        <label for="inputEnterEmail" class="col-sm-3 control-label">
                            Date of Birth *</label><div class="col-sm-6">
                        <asp:TextBox ID="txtDateOfBirth" runat="server" 
                        MaxLength="10" AutoPostBack="True" 
                        ontextchanged="txtDateOfBirth_TextChanged" class="form-control" placeholder="Date of birth" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="DateOfBirthRequired" runat="server" 
                        ControlToValidate="txtDateOfBirth" 
                        ErrorMessage="Date of Birth is required." 
                        ToolTip="Date of Birth is required." ForeColor="Red" Font-Size="Small">Date of Birth is required.</asp:RequiredFieldValidator>
                           
                        </div>
                        <div class="col-sm-3">
                        <asp:CustomValidator ID="DOBValidator" runat="server" 
                        ControlToValidate="txtDateOfBirth" ForeColor="Red" onservervalidate="checkDOB"></asp:CustomValidator>
                        </div>
                    </div>
      
                    <div class="row">
                        <label for="inputEnterEmail" class="col-sm-3 control-label">
                            Gender *
                        </label>
                        <div class="col-sm-6">

                         <asp:RadioButtonList ID="rblGender" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="rblGender_SelectedIndexChanged" 
                                    RepeatDirection="Horizontal" style="margin-left: 0px" >
                                    <asp:ListItem Value="M">Male &nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                </asp:RadioButtonList>
                     <asp:RequiredFieldValidator Font-Size="Small" ForeColor="Red"  ID="Gender_Req" ControlToValidate="rblGender" runat="server" ErrorMessage="*">Gender is required.
                     </asp:RequiredFieldValidator>
                        </div>

                        <div class="col-sm-3">
                            
                        <asp:CustomValidator ID="GenderValidator" runat="server" 
                        ControlToValidate="rblGender" ForeColor="Red" onservervalidate="checkGender"></asp:CustomValidator>
                        </div>
                    </div>
          <div class="row">
                        <label for="inputEnterEmail" class="col-sm-3 control-label">
                            Email *
                        </label>
                        <div class="col-sm-6">
                        <asp:TextBox ID="txtEmail" runat="server" 
                        MaxLength="50" AutoPostBack="True" 
                        ontextchanged="txtEmail_TextChanged" class="form-control" placeholder="Enter your email address" ></asp:TextBox>
                              <asp:RequiredFieldValidator ID="EmailRequired0" runat="server" 
                        ControlToValidate="txtEmail" 
                        ErrorMessage="Email is required." 
                        ToolTip="Email is required." ForeColor="Red" Font-Size="Small">Email is required.</asp:RequiredFieldValidator>
                        </div>
                         <div class="col-sm-3">
                            <asp:CustomValidator ID="EmailValidator" runat="server" 
                        ControlToValidate="txtEmail" ForeColor="Red" onservervalidate="checkEmail"></asp:CustomValidator>
                        </div>
                    </div>

                    <div class="row">
                        <label for="inputEnterLastName" class="col-sm-3 control-label">
                            Occupation
                        </label>
                        <div class="col-sm-6">
                        <asp:TextBox ID="txtOccupation" runat="server" 
                        MaxLength="30" class="form-control" placeholder="Enter your occupation"></asp:TextBox>
                            
                        </div>
                        <div class="col-sm-3">
                         <asp:CustomValidator ID="OcupationValidator1" runat="server" 
                        ControlToValidate="txtOccupation" ForeColor="Red" 
                        onservervalidate="checkOcupation"></asp:CustomValidator>
                        </div>
                    </div>
                    <br />
         
                     <div class="row">
                        <label for="inputEnterEmail" class="col-sm-3 control-label">
                            Address Line 1 *
                        </label>
                        <div class="col-sm-6">
                        <asp:TextBox ID="txtAddress1" runat="server" 
                        MaxLength="30" class="form-control" placeholder="Enter your address line 1" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Address1Required" runat="server" 
                        ControlToValidate="txtAddress1" 
                        ErrorMessage="Address 1 is required." 
                        ToolTip="Address 1 is required." ForeColor="Red" Font-Size="Small">Address line 1 is required.</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-3">
                        <asp:CustomValidator ID="Adrs1Validator" runat="server" 
                        ControlToValidate="txtAddress1" ForeColor="Red" 
                        onservervalidate="checkAddress1"></asp:CustomValidator>
                        </div>
                    </div>
   

                    <div class="row">
                        <label for="inputEnterEmail" class="col-sm-3 control-label">
                            Address Line 2 *
                        </label>
                        <div class="col-sm-6">
                        <asp:TextBox ID="txtAddress2" runat="server" 
                        MaxLength="30" class="form-control" placeholder="Enter your address line 2" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Address2Required" runat="server" 
                        ControlToValidate="txtAddress2" 
                        ErrorMessage="Address 2 is required." 
                        ToolTip="Address 2 is required." ForeColor="Red" Font-Size="Small">Address line 2 is required.</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-3">
                        <asp:CustomValidator ID="Adrs2Validator" runat="server" 
                        ControlToValidate="txtAddress2" ForeColor="Red" 
                        onservervalidate="checkAddress2"></asp:CustomValidator>
                        </div>
                    </div>
          

                    <div class="row">
                        <label for="inputEnterEmail" class="col-sm-3 control-label">
                            Address Line 3
                        </label>
                        <div class="col-sm-6">
                        <asp:TextBox ID="txtAddress3" runat="server" 
                        MaxLength="30" class="form-control" placeholder="Enter your address line 3"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                        <asp:CustomValidator ID="Adrs3Validator" runat="server" 
                        ControlToValidate="txtAddress3" ForeColor="Red" 
                        onservervalidate="checkAddress3"></asp:CustomValidator>
                        </div>
                    </div>
                    <br />
       

                     <div class="row">
                        <label for="inputEnterEmail" class="col-sm-3 control-label">
                            Address Line 4
                        </label>
                        <div class="col-sm-6">
                        <asp:TextBox ID="txtAddress4" runat="server" 
                        MaxLength="30" class="form-control" placeholder="Enter your address line 4" ></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                        <asp:CustomValidator ID="Adrs4Validator" runat="server" 
                        ControlToValidate="txtAddress4" ForeColor="Red" 
                        onservervalidate="checkAddress4"></asp:CustomValidator>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <label for="inputEnterEmail" class="col-sm-3 control-label">
                            City / Town *
                        </label>
                        <div class="col-sm-6">
                          <asp:TextBox ID="txtCityTown" runat="server" 
                        MaxLength="30" class="form-control" placeholder="Enter your city or town"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="CityRequired" runat="server" 
                        ControlToValidate="txtCityTown" 
                        ErrorMessage="City is required." 
                        ToolTip="City is required." ForeColor="Red" Font-Size="Small">City is required.</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-3">
                       <asp:CustomValidator ID="CityTownValidator" runat="server" 
                        ControlToValidate="txtCityTown" ForeColor="Red" 
                        onservervalidate="checkCityTown"></asp:CustomValidator>
                        </div>
                    </div>


                    <div class="row">
                        <label for="inputEnterEmail" class="col-sm-3 control-label">
                            Postal / Zip Code
                        </label>
                        <div class="col-sm-6">
                      <asp:TextBox ID="txtPostalCode" runat="server" 
                        MaxLength="10" class="form-control" placeholder="Enter postal/zip code"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                         <asp:CustomValidator ID="PostalCodeValidator" runat="server" 
                        ControlToValidate="txtPostalCode" ForeColor="Red" 
                        onservervalidate="checkPostalCode"></asp:CustomValidator>
                        </div>
                    </div>
                    <br />
       

                     
     

                    <div class="row">
                        <label for="inputEnterEmail" class="col-sm-3 control-label">
                            Mobile Number *
                        </label>
                        <div class="col-sm-6">
                        <asp:TextBox ID="txtMobileNo" runat="server" 
                        MaxLength="15" class="form-control" placeholder="Enter your mobile no."></asp:TextBox>
                     <asp:RequiredFieldValidator ID="MobileNumRequired" runat="server" 
                        ControlToValidate="txtMobileNo" 
                        ErrorMessage="Mobile Number is required" 
                        ToolTip="Mobile Number is required" ForeColor="Red" Font-Size="Small">Mobile Number is required.</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-3">
                        <asp:CustomValidator ID="MobileNoValidator" runat="server" 
                        ControlToValidate="txtMobileNo" ForeColor="Red" 
                        onservervalidate="checkMobileNo"></asp:CustomValidator>
                        </div>
                    </div>
       

                    <div class="row">
                        <label for="inputEnterEmail" class="col-sm-3 control-label">
                            Home Number
                        </label>
                        <div class="col-sm-6">
                        <asp:TextBox ID="txtHomeNo" runat="server" 
                        MaxLength="15" class="form-control" placeholder="Residential tel. no."></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                        <asp:CustomValidator ID="HomeNoValidator" runat="server" 
                        ControlToValidate="txtHomeNo" ForeColor="Red" onservervalidate="checkHomeNo"></asp:CustomValidator>
                        </div>
                    </div>
                    <br />
    

                     <div class="row">
                        <label for="inputEnterEmail" class="col-sm-3 control-label">
                            Office Number
                        </label>
                        <div class="col-sm-6">
                        <asp:TextBox ID="txtOfficeNo" runat="server" 
                        MaxLength="20" class="form-control" placeholder="Office tel. no."></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                        <asp:CustomValidator ID="OfficeNoValidator" runat="server" 
                        ControlToValidate="txtOfficeNo" ForeColor="Red" 
                        onservervalidate="checkOfficeNo"></asp:CustomValidator>
                        </div>
                    </div>
                    <br />
      

                    <div class="row">
                        <div class="col-sm-3">
                        </div>
                        <div class="col-sm-6">
                           
                            <!--<img class="img-responsive" src="CImage.aspx" alt="Flower" width="100%" height="100%" />-->
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/CImage.aspx" class="img-responsive" width="100%" height="100%"/>
                        </div>
                        <div class="col-sm-3">
                        </div>
                    </div>
                    <br/>
         
                    <div class="row">
                        <div class="col-sm-3">
                        </div>
                        <div class="col-sm-6">
                          
                              <%--<asp:Button   runat="server" Text="Change Image" onclick="Button1_Click"  CssClass="btn btn-primary btn-xs pull-left"
                                CausesValidation="False" />--%>
                             
                              <%--  <input type="image" ID="Button1" class="refreshImage" src="images/Refresh1_sm3.png" runat="server" onclick="Button1_Click" />--%>

                               <%-- <input ID="Button1" type="image" runat="server" onclick="Button1_Click"  src="images/Refresh1_sm3.png" />--%>
                                 <asp:ImageButton ID="Button1" runat="server" ImageUrl="images/Refresh1_sm3.png" OnClick="Button1_Click" CausesValidation="False"/>
                       
                        </div>
                        <div class="col-sm-3">
                        </div>
                    </div>
                    <br />
       
                    <div class="row">
                        <label for="inputText" class="col-sm-3 control-label">
                            Enter text above *</label>
                        <div class="col-sm-6">
                          <asp:TextBox ID="txtimgcode" runat="server" MaxLength="50" class="form-control" placeholder="Enter the text above"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="MobileNumRequired0" runat="server" 
                                ControlToValidate="txtimgcode" ErrorMessage="Text is required" ForeColor="Red" 
                                ToolTip="Text is required" Font-Size="Small"> Please enter the text above</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-3">
                     <%--   <asp:CustomValidator ID="ImageValidator" runat="server" 
                                ControlToValidate="txtimgcode" ForeColor="Red" onservervalidate="checkImageCode"></asp:CustomValidator>--%>
                        <asp:CustomValidator ID="ImageValidator" runat="server" 
                                ControlToValidate="txtimgcode" ForeColor="Red" onservervalidate="checkImageCode" ValidateEmptyText="true"></asp:CustomValidator>
                        
                        </div>
                    </div>
          
                    <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-10">
                    <asp:Label ID="lblStatusMesg" runat="server" Font-Size="Large"></asp:Label>
                        &nbsp;<asp:HyperLink ID="hlLogin" runat="server"></asp:HyperLink>
                        <asp:HiddenField ID="hdn_sub_pwd" runat="server" />
                        </div>
                    <div class="col-sm-1"></div>
                    </div>
       
                    <div class="row">
                        <div class="col-sm-3">
                        </div>
                        <div class="col-sm-6">
                         <div class="alert alert-info">
                           <span style="font-size:12px; color:#444444;">
                            By clicking the button &quot;Submit&quot; below,  you  confirm  that  you  have  read  the  <a href="/Documents/User_Agreement.pdf" target="_blank" >terms and conditions</a>,  that  you understand them and that you agree to be bound by them.
                           </span>
                           </div>
                           <br />   
                                <asp:Button ID="btnSubmit" Height="30px" runat="server" onclick="btnSubmit_Click" Enabled="false"  Text="Submit" CssClass="btn btn-primary btn-xs pull-left" OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true; this.value = 'Saving...';"  />
                        </div>
                        <div class="col-sm-3"> <%--<input id="btnSave" type="button" value="Save" onclick = "VerifyAndSave()" />--%>
                        </div>
                    </div>
    <%--
                </div>--%>
                <br />


     </div>
    <div class="col-xs-1">
    </div>
    </div>

 <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10" 
runat="server" AssociatedUpdatePanelID="UpdatePanel2">
<ProgressTemplate>
  <div class="divWaiting">     
  <img src="/Images/load.gif" style="position: fixed;left: 50%;top: 40%"/><br />       
	<asp:Label ID="lblWait2" runat="server" style="position: fixed;left: 50%;top: 47%" Text=" Please wait... " />
  </div>
</ProgressTemplate>
</asp:UpdateProgress>
<br />


 


                </ContentTemplate>
                 <Triggers >
                    <asp:AsyncPostBackTrigger ControlID="Button1" />
                    
                    </Triggers>
                </asp:UpdatePanel>


     


        </fieldset>
                        </div>
        </div>


        </div></div>
    </form>

<script>

    $(function () {
        today = new Date();
        var month, day, year;
        year = today.getFullYear();
        month = today.getMonth();
        date = today.getDate();
        year = today.getFullYear() - 50;
        var backdate = new Date(year, month, date)
        var backdate1 = new Date(year, month, date)

        
        $("input[id$='txtDateOfBirth']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, defaultDate: backdate, yearRange: '-100:-16' });
       


    });


    $(document).ready(function () {

        $("input").keypress(function (event) {
            if (event.which == 13) {
                event.preventDefault();
                //$("form").submit();
                $("#effect").hide();
            }
        });

        $(window).scroll(function () {
            sessionStorage.scrollTop = $(this).scrollTop();
        });

        if (sessionStorage.scrollTop != "undefined") {
            $(window).scrollTop(sessionStorage.scrollTop);
            //alert("In" + sessionStorage.scrollTop);

            $('html, body').animate({
                scrollTop: sessionStorage.scrollTop,
                scrollLeft: 0
            }, 1);

        }


        $("input[id$='btnSubmit']").click(function () {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
            }
            if (Page_IsValid) {
                var dateObj = new Date();
                var year = dateObj.getUTCFullYear();

                var pwd = $("input[id$='txtPassword']").val();
                var hashed_1 = $.base64.encode(pwd);
                var hashed_2 = $.base64.encode(hashed_1);
                var hashed_21 = $.base64.encode(hashed_2);
                var hashed_3 = $.base64.encode(hashed_21);


                $("input[id$='hdn_sub_pwd']").attr('value', hashed_3);


                var boru = "";

                var i = 0;
                while (len != i) {
                    boru = boru.concat('*');
                    i++;
                }

                $("input[id$='txtPassword']").val(boru);
                $("input[id$='txtConfirmPassword']").val(boru);



            }
            else {

                //alert("Validations not successful");
            }
        });


        //    $(function() {

        //        if ($('#ContentPlaceHolder1_EmailRequired0').is(':visible')) {
        //            $("#ContentPlaceHolder1_txtEmail").css("background-color", "pink");
        //        }
        //    });



        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        $("#effect").hide();

        $("input[id$='txtPassword']").focusin(function () {
            $("#effect").show();

        }).focusout(function () {

                $("#effect").hide(600);
        });






        today = new Date();
        var month, day, year;
        year = today.getFullYear();
        month = today.getMonth();
        date = today.getDate();
        year = today.getFullYear() - 50;
        var backdate = new Date(year, month, date)
        var backdate1 = new Date(year, month, date)


        function EndRequestHandler(sender, args) {
            noSSL();

            $("input").keypress(function (event) {
                if (event.which == 13) {
                    event.preventDefault();
                    // $("form").submit();

                }
            });

            $(window).scroll(function () {
                sessionStorage.scrollTop = $(this).scrollTop();
            });

            if (sessionStorage.scrollTop != "undefined") {
                $(window).scrollTop(sessionStorage.scrollTop);

                $('html, body').animate({
                    scrollTop: sessionStorage.scrollTop,
                    scrollLeft: 0
                }, 1);

            }

            $("input[id$='btnSubmit']").click(function () {
                if (typeof (Page_ClientValidate) == 'function') {
                    Page_ClientValidate();
                }
                if (Page_IsValid) {
                    var dateObj = new Date();
                    var year = dateObj.getUTCFullYear();

                    var pwd = $("input[id$='txtPassword']").val();
                    var hashed_1 = $.base64.encode(pwd);
                    var hashed_2 = $.base64.encode(hashed_1);
                    var hashed_21 = $.base64.encode(hashed_2);
                    var hashed_3 = $.base64.encode(hashed_21);


                    $("input[id$='hdn_sub_pwd']").attr('value', hashed_3);


                    var boru = "";

                    var i = 0;
                    while (len != i) {
                        boru = boru.concat('*');
                        i++;
                    }

                    $("input[id$='txtPassword']").val(boru);
                    $("input[id$='txtConfirmPassword']").val(boru);





                }
                else {
                    //alert("Validations not successful");
                }
            });


            $("input[id$='txtDateOfBirth']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, defaultDate: backdate, yearRange: '-100:-16' });
        }

    });

    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });

         </script>
    <%--<script src="js/jquery-3.5.1.min.js"></script>
    <script src="js/jquery-3.5.1.js"></script>
    <script type="text/javascript" src="js/script.js"></script>--%>

    <script type="text/javascript" src="js/jquery.base64.min.js"></script>   
</asp:Content>

