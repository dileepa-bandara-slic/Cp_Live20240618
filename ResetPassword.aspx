<%@ Page Title="" Language="C#" MasterPageFile="~/RegisterSite.Master" AutoEventWireup="true"
    CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="ResetPasswordForm" runat="server">
    
<div class="main-container" id="main-container">
        <div class="container">

    <div class="row">
        <div class="col-xs-1">
        </div>
        <div class="col-xs-10" style="margin: 10% 0%">
            <div class="panel panel-default">
                <!-- <div class="panel-heading">
                    Password Recovery
                </div>-->
                <div class="panel-body" align="center">
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Label ID="lblErrMesg" runat="server" ForeColor="Red" Style="font-size: 100%"></asp:Label>&nbsp;<asp:HyperLink
                                ID="hlForgotPass" runat="server"></asp:HyperLink>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    </br>
                    <div class="row">
                        <div class="col-xs-3">
                        </div>
                        <div class="col-xs-9" align="left" style="font: bold">
                            Enter your new password</div>
                    </div>
                    <br />
                    <div class="row">
                        <label for="inputEmail3" class="col-sm-3 control-label">
                            New Password</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="15" placeholder="Enter your new password"
                                class="form-control" ReadOnly="false" autocomplete="new-password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="Password is required." ToolTip="Password is required." ForeColor="Red">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            </div>
                             <div class="col-sm-9">
                    <div id="effect" class="collapse in">
                   
                            <ul class="list-group" style="font-size:0.9em; border: 1px solid #CCC; text-align:left">
                                <li class="list-group-item" style="border-width:0; font-size:0.9em;">Password should be 8 - 15 characters long</li>
                                <li class="list-group-item" style="border-width:0; font-size:0.9em;">Contain one or more letters</li> 
                                <li class="list-group-item" style="border-width:0; font-size:0.9em;">Contain one or more numeric values</li> 
                                <li class="list-group-item" style="border-width:0; font-size:0.9em; background-color:#E5E4E2;"><span> <a class="btn-link"  target="_blank" href="/PasswordHints.aspx"><strong>Hints for a strong password</strong> </a> </span> </li> 
                            </ul>
                        </div>
                            </div>
                    </div>
                    <br />
                    <div class="row">
                        <label for="inputText" class="col-sm-3 control-label">
                            Confirm Password</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" class="form-control"
                                placeholder="Re-enter password" ReadOnly="false" MaxLength="15" autocomplete="new-password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtConfirmPassword"
                                ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                                ForeColor="Red">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-sm-3 control-label">
                        </div>
                        <div class="col-sm-9">
                            <asp:Button ID="btnSubmit" Enabled="false" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-primary btn-sm pull-left"
                                Text="Submit" />
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-xs-12">
                            <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="txtPassword"
                                ControlToValidate="txtConfirmPassword" Display="Dynamic" 
                                ErrorMessage="The Password and Confirmation Password must match." 
                                ForeColor="Red"></asp:CompareValidator>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-4">
                            <asp:Label ID="lblSuccessMesg" runat="server" Font-Size="Large" ForeColor="Green"></asp:Label>
                        </div>
                        <div class="col-xs-2">
                            <asp:HyperLink ID="hlLogin" runat="server"></asp:HyperLink>
                        </div>
                        <div class="col-xs-4">
                            <asp:Label ID="lblPostErrMesg" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-2">
                        </div>
                        <div class="col-xs-8">
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="Password should conform to password rules" ValidationExpression="(?=^.{8,15}$)(?=.*\d)(?=.*[!@#$%^&*_]+)(?![.\n])(?=.*[A-Za-z]).*$"></asp:RegularExpressionValidator>--%>

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="Password should conform to password rules" 
                                ValidationExpression="(?=^.{8,15}$)(?=.*\d)(?=.*[A-Za-z]).*$" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-xs-2">
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
        <div class="col-xs-1">
        </div>
    </div>

    </div>
    </div>

    </form>

    <script>


        $(document).ready(function () {
           // $("#effect").hide();



            $("input[id$='txtPassword']").focusin(function () {
                            $("#effect").show();

                        }).focusout(function () {

                            $("#effect").hide(600);
                        });
        });

         </script>
    <script src="js/jquery-3.5.1.min.js"></script>
    <script src="js/jquery-3.5.1.js"></script>
    <script type="text/javascript" src="/js/script.js"></script>
    <script type="text/javascript" src="/js/jquery.base64.min.js"></script>   

</asp:Content>
