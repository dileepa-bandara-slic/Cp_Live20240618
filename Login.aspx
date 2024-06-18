<%@ Page Title="" Language="C#" MasterPageFile="~/RegisterSite.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
         .table-responsive
         {
             width: 100%;
             display: inline;
         }
    </style>
   

<script type="text/javascript">

    

    function randomNumberFromRange(min, max) {
        return Math.floor(Math.random() * (max - min + 1) + min);
    }

    $(document).ready(function () {
        var minNumber = 999;
        var maxNumber = 100
        var randomNumber = randomNumberFromRange(minNumber, maxNumber);
        $("input[id$='hdn_client']").attr('value', randomNumber);

        $("input[id$='LoginButton']").click(function () {
            var pwd = $("input[id$='Password']").val();
            var pwd_en1 = $.base64.encode(pwd);
            var pwd_hashed_cl = pwd_en1.concat($("input[id$='hdn_client']").val());
            var pwd_hashed_ser = pwd_hashed_cl.concat($("input[id$='hdn_server']").val());
            var int_end = $.base64.encode(pwd_hashed_ser);
            var finalenc = $.base64.encode(int_end);
            //alert(finalenc);

            $("input[id$='hdn_pwd_sbmt']").attr('value', finalenc);

            var len = $("input[id$='Password']").val().length;

            var boru = "";

            var i = 0;
            while (len != i) {
                boru = boru.concat('*');
                i++;
            }

            $("input[id$='Password']").val(boru);
            $("input[id$='hdn_server']").val(boru);

        });
    });

    // This is the suggested way by KPMG 
//    $(document).ready(function () {

//        var minNumber = 999;
//        var maxNumber = 100

//        var randomNumber = randomNumberFromRange(minNumber, maxNumber);


//        $("input[id$='hdn_client']").attr('value', randomNumber);

//        $("input[id$='LoginButton']").click(function () {

//            //var salt = $("input[id$='hdn_client']").val().concat($("input[id$='hdn_server']").val());

//            var pwd = $("input[id$='Password']").val();
//            var pwd_hashed = sha512(pwd);
//            var pwd_hashed_cl = pwd_hashed.concat($("input[id$='hdn_client']").val());
//            var pwd_hashed_ser = pwd_hashed_cl.concat($("input[id$='hdn_server']").val());
//            var all_hashed = sha512(pwd_hashed_ser);

//            $("input[id$='hdn_pwd_sbmt']").attr('value', all_hashed);

//            var len = $("input[id$='Password']").val().length;

//            var boru = "";

//            var i = 0;
//            while (len != i) {
//                boru = boru.concat('*');
//                i++;
//            }

//            $("input[id$='Password']").val(boru);
//            $("input[id$='hdn_server']").val(boru);
//        });

//    });
    
</script>


    <form id="loginForm" runat="server">
<div class="main-container">
<div class="row">
       <div class="col-xs-1 col-sm-2 col-md-2 col-lg-3">
       </div>
       <div class="col-xs-10 col-sm-8 col-md-8 col-lg-6" style="margin: 15% 0%">
        <div class="panel panel-info" align="center">
                <div class="panel-heading">
                 <p>
                        If you don't have an account, please <a  href="Register.aspx"><strong><u>Register.</u></strong></a></p>
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
                                   <asp:TextBox ID="Password" runat="server" TextMode="Password" MaxLength="15" 
                                       class="form-control" placeholder="Enter your password" autocomplete="new-password"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                       ControlToValidate="Password" ErrorMessage="*Required" 
                                       ToolTip="Password is required." ValidationGroup="Login1" ForeColor="Red" 
                                       Font-Bold="True" Font-Names="Calibri" Font-Size="10pt"></asp:RequiredFieldValidator>
                               </td>
                           </tr>
                           <tr>
                               <td colspan="2">
                                   
                                   <asp:HiddenField ID="hdn_server" runat="server" ClientIDMode="Static"  />
                                   <asp:HiddenField ID="hdn_client" runat="server" ClientIDMode="Static" />
                                   <asp:HiddenField ID="hdn_pwd_sbmt"  runat="server" ClientIDMode="Static"/>
                                   
                               </td>
                           </tr>
                           <tr>
                               <td align="center" colspan="2" style="color:#D9534F;">
                                   <p class=" btn-danger" style="background-color:#F54947; padding:0px;"><asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></p>
                                   <br />                                   
                               </td>
                           </tr>
                           <tr>
                               <td>
                                   <a href="ForgotPassword.aspx" style="display: inline;">Forgot Password?</a>
                               </td>
                               <td>

                                   <asp:Button ID="LoginButton" runat="server" CssClass ="btn btn-primary btn-xs pull-right"  Enabled="False"
                                       CommandName="Login" Height="30px" Text="Log In"  ValidationGroup="Login1" onclick="LoginButton_Click" style="left: 0px; top: 5px"  />
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
  <script type="text/javascript" src="js/jquery.base64.min.js"></script>   
 <%-- <script type="text/javascript" src="js/sha512.js"></script>  --%> 
</asp:Content>

