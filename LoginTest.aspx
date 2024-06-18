<%@ Page Title="" Language="C#" MasterPageFile="~/RegisterSite.Master" AutoEventWireup="true" CodeFile="LoginTest.aspx.cs" Inherits="LoginTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form id="loginForm" runat="server">
<div class="main-container">
<div class="row">
       <div class="col-xs-1 col-sm-2 col-md-2 col-lg-3">
       </div>
       <div class="col-xs-10 col-sm-8 col-md-8 col-lg-6" style="margin: 15% 0%">
        <div class="panel panel-info" align="center">
                <div class="panel-heading">
                 <p>
                        If you don't have an account, please <a href="Register.aspx">Register</a></p>
     <asp:Login ID="LoginUser" runat="server" onauthenticate="LoginUser_Authenticate" 
           EnableViewState="False" 
           FailureText="Your login attempt was not successful. Please visit below link if you have forgotten password.">
       <LayoutTemplate>
           
                       <table class="table-responsive">
                       
                           <tr>
                               <td>
                                   <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName"
                                       Font-Bold="False">User 
                                   Name:</asp:Label>
                               </td>
                               <td>
                                   <asp:TextBox ID="UserName" runat="server" MaxLength="15" 
                                       AutoComplete="on" class="form-control"  placeholder="Enter your username"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                       ControlToValidate="UserName" ErrorMessage="*Required" 
                                       ToolTip="User Name is required." ValidationGroup="Login1" ForeColor="Red" 
                                       Font-Bold="True" Font-Names="Calibri" Font-Size="10pt"></asp:RequiredFieldValidator>
                               </td>
                           </tr>
                           <tr>
                               <td>
                                   <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password"
                                       Font-Bold="False">Password:</asp:Label>
                               </td>
                               <td>
                                   <asp:TextBox ID="Password" runat="server" TextMode="Password" MaxLength="11" 
                                       class="form-control" placeholder="Enter your password"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                       ControlToValidate="Password" ErrorMessage="*Required" 
                                       ToolTip="Password is required." ValidationGroup="Login1" ForeColor="Red" 
                                       Font-Bold="True" Font-Names="Calibri" Font-Size="10pt"></asp:RequiredFieldValidator>
                               </td>
                           </tr>
                           <tr>
                               <td colspan="2">
                                   
                               </td>
                           </tr>
                           <tr>
                               <td align="center" colspan="2" style="color:#D9534F;">
                                   <p class="img-rounded btn-danger" style="background-color:#F54947; padding:0px;"><asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></p>
                                   <br />                                   
                               </td>
                           </tr>
                           <tr>
                               <td>
                                   <a href="ForgotPassword.aspx" style="display: inline;">Forgot Password?</a>
                               </td>
                               <td>
                                   <asp:Button ID="LoginButton" runat="server" class="btn btn-primary pull-right" 
                                       CommandName="Login" Height="30px" Text="Log In" ValidationGroup="Login1" 
                                      />
                               </td>
                           </tr>
                       </table>
                   
       </LayoutTemplate>
   </asp:Login>

   </div>
   </div>

       </div>
       <div class="col-xs-1 col-sm-2 col-md-2 col-lg-3">
       </div>
  </div>
  </div>
  </form>
</asp:Content>


