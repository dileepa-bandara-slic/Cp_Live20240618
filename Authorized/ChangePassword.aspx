<%@ Page Title="" Language="C#" MasterPageFile="~/Common.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="Authorized_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/fieldset.css" rel="stylesheet" />


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

<script type="text/javascript">
    document.body.onload = function () { noSSL(); }
</script>

<script>
    function noSSL() {
        var str = window.location.href;
        var res = str.substring(0, 5);

        if (res == "https") {
            var btn = document.getElementById("ContentPlaceHolder1_ContentPlaceHolder1_btnSubmit");

            btn.disabled = false;
        }
        else {

            var inputs = document.getElementsByTagName("INPUT");
            for (var i = 0; i < inputs.length; i++) {
               
                inputs[i].disabled = false;
             
            }
        }
    }
</script>

    <%--    <form id="forgotPassword" runat="server">--%>
<%--    <div class="container">--%>
 <%--<div class="container">--%>
 </br>
  <div class="main-container" id="main-container"  style="min-height:600px">
<%--  </br>
  </br>--%>
    <div class="container">
<%--   <br />--%>
    <div class="row">
        <div class="col-xs-1">
        </div>
        <div class="col-xs-10">

            
                    <div style="align:center">
                     <fieldset>
  <legend>Change Password:</legend>

           <%-- <div class="panel panel-default">--%>
               <%-- <div class="panel-heading">
                   
                     <center>
                                <h3>
                                    Change Password</h3>
                            </center>
                </div>--%>
               <%-- <div class="panel-body">  --%>
                    <div class="row">
                        <label for="inputEmail3" class="col-sm-3 control-label">
                            Old Password</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtOldPasswd" runat="server" TextMode="Password" MaxLength="15"
                                class="form-control" placeholder="Enter existing password" autocomplete="new-password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="OldPasswordRequired" runat="server" ControlToValidate="txtOldPasswd"
                                ErrorMessage="Password is required." ToolTip="Password is required." 
                                ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
             
                    <div class="row">
                        <label for="inputEmail3" class="col-sm-3">
                            New Password</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="15" class="form-control"
                                placeholder="Enter a new password" data-toggle="collapse" data-target="#effect"
                                data-placement="bottom" autocomplete="new-password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="Password is required." ToolTip="Password is required." 
                                ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row">
                        <div id="effect" class="collapse">
                            <label for="inputPassword" class="col-sm-3">
                            </label>
                            <div class="col-sm-9">
                               <ul class="list-group" style="font-size:0.9em; border: 1px solid #CCC;">
                                <li class="list-group-item" style="border-width:0; font-size:0.9em;">Password should be 8 - 15 characters long</li>
                                <li class="list-group-item" style="border-width:0; font-size:0.9em;">Contain one or more letters</li> 
                                <li class="list-group-item" style="border-width:0; font-size:0.9em;">Contain one or more numeric values</li> 
                               <li class="list-group-item" style="border-width:0; font-size:0.9em; background-color:#E5E4E2;"><span> <a class="btn-link"  target="_blank" href="/PasswordHints.aspx"><strong>Hints for a strong password</strong> </a> </span> </li> 
                            </ul>
                            </div>
                      <%--      <div class="col-sm-3">
                            </div>--%>
                        </div>
                    </div>
             
                    <div class="row">
                        <label for="inputEmail3" class="col-sm-3">
                            Confirm Password</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="15"
                                class="form-control" placeholder="Re-type your new password" autocomplete="new-password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtConfirmPassword"
                                ErrorMessage="Confirm Password is required." 
                                ToolTip="Confirm Password is required." ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="txtPassword"
                                ControlToValidate="txtConfirmPassword" Display="Dynamic" 
                                ErrorMessage="The New Password and Confirmation Password must match." 
                                ForeColor="Red"></asp:CompareValidator>
                        </div>
                    </div>

                   <%-- <div class="row">
                        <div class="col-xs-12">
                            <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="txtPassword"
                                ControlToValidate="txtConfirmPassword" Display="Dynamic" ErrorMessage="The New Password and Confirmation Password must match."></asp:CompareValidator>
                        </div>
                    </div>--%>
             
                    <div class="row">
                        <div class="col-sm-3">
                        </div>
                        <div class="col-sm-6">
                         <%-- <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" Enabled="false" CssClass="btn btn-primary btn-xs"
                    Text="Submit" />--%>

                             <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" CssClass="btn btn-primary btn-xs"
                    Text="Submit" />
                        </div>
                        <div class="col-sm-3">
                        </div>
                    </div>

                    <div class="row">
                      <div class="col-sm-3">
                        </div>
                        <div class="col-sm-9">
                            <asp:Label ID="lblStatusMesg" runat="server" Font-Size="Medium" style="color:Red;"></asp:Label>
                            <br />
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPassword" style="color:Red;"
                                ErrorMessage="Password should conform to password rules" ValidationExpression="(?=^.{8,15}$)(?=.*\d)(?=.*[!@#$%^&*_]+)(?![.\n])(?=.*[A-Za-z]).*$"></asp:RegularExpressionValidator>--%>

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPassword" style="color:Red;"
                                ErrorMessage="Password should conform to password rules" ValidationExpression="(?=^.{8,15}$)(?=.*\d)(?=.*[A-Za-z]).*$"></asp:RegularExpressionValidator>

                        </div>
                    </div>

                  </fieldset>
                        </div>
                    
                    
                  
                    
              <%--  </div>--%>
            <%--</div>--%>
        </div>
        <div class="col-xs-1">
        </div>
    </div>
        </br>
    </div>
    </div>
    <%--</div>--%>
   <%-- </div>--%>

   <%-- </form>--%>

   <script>
    $(document).ready(function () {
      
        $("#effect").hide();
$("input[id$='txtPassword']").focus(function() {
$("#effect").show();

//            $("input[id$='txtPassword']").focusin(function () {
//                            $("#effect").show();

//                        }).focusout(function () {

//                            $("#effect").hide(600);
//                        });
        });

         </script>
    <script src="../js/jquery-3.5.1.min.js"></script>
    <script src="../js/jquery-3.5.1.js"></script>


</asp:Content>
