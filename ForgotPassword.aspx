<%@ Page Title="" Language="C#" MasterPageFile="~/RegisterSite.Master" AutoEventWireup="true"
    CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript">


<%--    function yesnoCheck()
    {
        
        //var field = document.getElementById('txtUnameEmail').value;
        document.getElementById("<%= lblErrMesg.ClientID %>").innerHTML = "";

        var field = document.getElementById('<%= txtUnameEmail.ClientID %>').value;
        
        // CHeck if email

        if (document.getElementById('gridRadios2').checked) {
            if (/\@/.test(field)) {
                

                if (/^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(field)) {
                    return (true);

                }
                ////console.log("You have entered an invalid email address!");
                ////return (false)
                else {
                    document.getElementById("<%= lblErrMesg.ClientID %>").innerHTML = "You have entered an invalid email address!";
                    //document.getElementById("lblErrMesg").innerHTML = "You have entered an invalid email address!";
                }
            }
          
        }
    }--%>
    function updateHiddenField(value)
    {
        document.getElementById('<%=RadioButtonValue.ClientID%>').value = value;
    }

</script>

         <script type="text/javascript">
        //function Validate() {
        //    var isValid = false;
        //    isValid = Page_ClientValidate('Group1');
        //    if (isValid) {
        //        isValid = Page_ClientValidate('Group2');
        //    }
        //    if (isValid) {
        //        isValid = Page_ClientValidate('Group3');
        //    }
        //    return isValid;
        //}


             <%--function yesnoCheck() {
                 document.getElementById("<%= lblErrMesg.ClientID %>").innerHTML = "";

                 if (document.getElementById('gridRadios2').checked) {
                     var field = document.getElementById('<%= txtUnameEmail.ClientID %>').value;
                if (!/\S+@\S+\.\S+/.test(field)) {
                         document.getElementById("<%= lblErrMesg.ClientID %>").innerHTML = "You have entered an invalid email address!";
                     }
                 }
             }--%>
         </script>


<form id="forgotPassword" runat="server">

     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
<%--    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">

    </asp:ScriptManager>--%>

<div class="main-container" id="main-container">
        <div class="container">

      <%--           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">       
     <ContentTemplate>--%>


    <div class="row">
        <div class="col-xs-1">
        </div>
        <div class="col-xs-10" style="margin: 10% 0%">
           <%-- <div class="panel panel-default">--%>

                   <div style="align:center">
                                     <fieldset>
  <legend>Password Recovery:</legend>

                           <asp:UpdatePanel ID="UpdatePanel1" runat="server">       
     <ContentTemplate>

          <%--      <div class="panel-heading">
                <center>
                   <h3> Password Recovery</h3>
                   </center>
                </div>
                <div class="panel-body">
                    <p>
                        Enter either your username <u><b>OR </b></u>email address below. We'll send you
                        a password reset email.
                    </p>


    
    </div>--%>

                <div class="row">
                       <div class="col-xs-12">
                            <p>
                        Enter either your username <u><b>OR </b></u>email address below. We'll send you
                        a password reset email.
                    </p>
                    </div>
                    </div>

                 <div class="row">
                        <label for="inputEmail3" class="col-sm-3 control-label"> Username OR Email</label>
           
                        <div class="col-sm-9">

                             <asp:TextBox ID="txtUnameEmail" runat="server" class="form-control" MaxLength="50" placeholder="Enter your username or email address"></asp:TextBox>

                        </div>
                    </div>

         <div class="row">
             <label for="inputEmail3" class="col-sm-3 control-label"></label>

             <asp:HiddenField ID="RadioButtonValue" runat="server" />
                <div class="form-check">
                    <%--<input id="gridRadios1" onclick="yesnoCheck();" class="form-check-input" type="radio" name="gridRadios"  value="option1" checked>
                    <label class="form-check-label" for="gridRadios1"> &nbsp;&nbsp; Username--%>
                        <asp:RadioButton ID="RadioButton1" runat="server" Text="Username" GroupName="radioGroup" />
                    </label>
                </div>
                <div class="form-check">
                    <%--<input id="gridRadios2" onclick="yesnoCheck();" class="form-check-input" type="radio" name="gridRadios" value="option2">
                    <label class="form-check-label" for="gridRadios2"> &nbsp;&nbsp; Email address--%>
                        <asp:RadioButton ID="RadioButton2" runat="server" text="Email address" GroupName="radioGroup" />
                    </label>
                </div>
         </div>
         <br />
         <div class="row">
                        <label for="inputEmail3" class="col-sm-3 control-label"> Phone Number</label>
           
                        <div class="col-sm-9">

                             <asp:TextBox ID="txtPhoneNo" runat="server" class="form-control" MaxLength="50" placeholder="Enter your phone number"></asp:TextBox>

                        </div>
                    </div>          



                <br />
        
                <div class="row">
                    <div class="col-xs-3">
                    </div>
                    <div class="col-xs-6">
                        <img class="img-responsive" src="CImage.aspx" alt="Flower" width="100%" height="100%" />
                    </div>
                    <div class="col-xs-3">
                    </div>
                </div>

                </ContentTemplate>
                               </asp:UpdatePanel>
          <br />
        
                <div class="row">
                    <div class="col-xs-3">
                    </div>
                    <div class="col-xs-6">
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="btn btn-primary btn-xs pull-left"  Height="20" OnClick="Button1_Click" Text="Change Image"/>
                    

                     
                    </div>
                    <div class="col-xs-3">
                    </div>
                </div>
                <br />
        
                <div class="row">
                    <label class="col-sm-3 control-label" for="inputText">
                    Enter text above</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="txtimgcode" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="MobileNumRequired0" runat="server" ControlToValidate="txtimgcode" ErrorMessage="Text is required" ForeColor="Red" ToolTip="Text is required">*</asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="ImageValidator" runat="server"  Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtimgcode" ForeColor="Red" OnServerValidate="checkImageCode"></asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-3">
                    </div>
                    <div class="col-xs-6">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-xs pull-left" onclick="btnSubmit_Click" Text="Reset Password" OnAuthenticate="ForgotPassword_Authentication"/>
                    </div>
                    <div class="col-xs-3">
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-3">
                    </div>
                    <div class="col-xs-6">
                        <asp:Label ID="lblErrMesg" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <div class="col-xs-3">
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-3">
                    </div>
                    <div class="col-xs-6">
   

                         
                      <asp:CustomValidator ID="UsernameValidator" runat="server" ForeColor="Red" 
                        ControlToValidate="txtUnameEmail" onservervalidate="checkUsername">
                      </asp:CustomValidator>

                    </div>
                    <div class="col-xs-3">
                    </div>
                </div>
             
                <div class="row">
                    <div class="col-xs-3">
                    </div>
                    <div class="col-xs-9">
                        <asp:Label ID="lblSuccessMesg" runat="server" Font-Size="Medium" ForeColor="#008040"></asp:Label>
                      <%--  </td>--%>
                    </div>
                </div>
             
</fieldset>
                       </div>
                <%--</div>--%>
            </div>
     
        <div class="col-xs-1">
        </div>
    </div>



<br />


    </div>
    </div>
    </form>
</asp:Content>
