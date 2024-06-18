<%@ Page Title="" Language="C#" MasterPageFile="~/Life.master" AutoEventWireup="true" CodeFile="PolicyRevivalRequest.aspx.cs" Inherits="Life_Authorized_PolicyRevivalRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

     <link href="/css/fieldset.css" rel="stylesheet" />
<%--    <style>
        @media (max-width:479px)
        {
            .navbar-fixed-top + .main-container
            {
                padding-top: 40px;
            }
        }
        .test11
        {
            font-size: 100%;
        }
    </style>--%>


    <style>
        
        
        .divWaiting
        {
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            position: absolute;
            text-align: center;
            top: 0;
            left: 0;
            height: 1024px;
            width: 100%;
        }
        
        
         .button101{
   width: 230px;
   height: 26px;
   background: #428bca;
   border: 0px solid #428bca;
   position: relative;
   color:White;
   padding-left:10px;
   vertical-align:bottom;
}

.button101::before{
   width: 0;
   height: 0;
   border: 13px solid transparent;
   border-left: 8px solid #428bca;
   content: '';
   position: absolute;
   top: 0px;
   left: 230px;
   
  
}
.button101::after{
 
 width: 0;
   height: 0;
   border: 13px solid transparent;
   border-left: 8px solid #428bca;
   content: '';
   position: absolute;
   top: 0px;
   left: 230px;


}
.arrow-right {
  width: 0; 
  height: 0; 
  border-top: 5px solid transparent;
  border-bottom: 5px solid transparent;
  border-left: 5px solid #393939;
}
   
    </style>

    <style type="text/css">
            
             
        
             .form-control2 {
        border-color: #428bca !important;


    }
    
            .panel
            { /*border: 1px solid #428bca !important;*/ 
             }
    
    </style>

            <style>

        @media screen and (max-width:1800px)
        {
           /*input[type=text]{width:50%}
           .select {
                width:50%;
           }*/
            input[type='text'], select{
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
        
        }

                @media screen and (max-width:600px)
        {
           /*input[type=text]{width:100%}
             .select {
                width:100%;
           }*/
             input[type='text'] {
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
         
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-container" id="main-container">
   <link href="/css/footable.min.css" rel="stylesheet" type="text/css" />
        <script src="/js/footable.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=gvDemands]').footable();
                $('[id*=gvPolicies').footable();
            });

          

        </script>




        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        

        <div class="container">
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/Life/Authorized/">Life</a></li>
                        <li class="breadcrumb-item active">Policy Revivals</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                <center>
                    <h3>
                        Policy Revival Request</h3>
                        </center>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <br/>
       
            <div class="row">
                <div class="col-xs-1">
                </div>
                 <div class="col-xs-10">

                          <asp:UpdatePanel ID="Ggs14451" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" Visible="true" >
            
               <div style="align:center">
                                     <fieldset>
  <legend>Policy Details :</legend>
          

          <%-- <div class="row">
                            <div class="col-sm-3">
                                <strong>
                                     Policy Details : 
                                </strong>
                            </div>
                            <div class="col-sm-9">
                                
                            </div>
                        </div>--%>

                 <div class="row">
                            <div class="col-sm-3">
                                 Policy Number
                            </div>
                            <div class="col-sm-9">
                                  <asp:TextBox ID="txtPolicyNumber" runat="server"  class="form-control" 
                                                             MaxLength="12"  AutoPostBack="True" 
                                                             ontextchanged="txtPolicyNumber_TextChanged"></asp:TextBox>
                                                     <asp:RequiredFieldValidator ID="PolNoRequired" runat="server" 
                        ControlToValidate="txtPolicyNumber" 
                        ErrorMessage="Policy Number is required" 
                        ToolTip="Policy Number is required." ForeColor="Red" Font-Size="Small">Policy Number is required.</asp:RequiredFieldValidator>
                                                       <asp:CustomValidator ID="PolNoValidator" runat="server" 
                                ControlToValidate="txtPolicyNumber" 
                                      ErrorMessage="No record found for Policy Number" style="color: #FF0000"></asp:CustomValidator>
                                      <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                ControlToValidate="txtPolicyNumber" 
                                      ErrorMessage="Policy is not registered under this username" style="color: #FF0000"></asp:CustomValidator>
                                      
                                  <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                      
                            </div>
                        </div>
                                         <br />

                   <div class="row">
                            <div class="col-sm-3">
                                  Name of the Policy Holder
                            </div>
                            <div class="col-sm-2">
                            <asp:TextBox ID="txtPolicyHolderStatus" runat="server"  class="form-control" 
                                                            MaxLength="12"></asp:TextBox>
                                     
                            </div>
                            <div class="col-sm-7">
                            <asp:TextBox ID="txtPolicyHolderName" runat="server"  class="form-control" 
                                                            ></asp:TextBox>
                            </div>
                        </div>
                                         <br />

                 

                   <div class="row">
                            <div class="col-sm-3">
                              NIC No of the Policy Holder
                            </div>
                            <div class="col-sm-9">
                                               <asp:TextBox ID="txtPolicyHolderNIC" runat="server"  class="form-control" 
                                                                    MaxLength="12" ontextchanged="txtPolicyHolderNIC_TextChanged"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="NICRequired" runat="server" 
                        ControlToValidate="txtPolicyHolderNIC" 
                        ErrorMessage="NIC Number is required" 
                        ToolTip="NIC Number is required." ForeColor="Red" Font-Size="Small">NIC Number is required.</asp:RequiredFieldValidator>
                       
                        <asp:CustomValidator ID="NICValidator" runat="server" ForeColor="Red" ValidateEmptyText="true"
                        ControlToValidate="txtPolicyHolderNIC" onservervalidate="checkNIC"></asp:CustomValidator>
                            </div>
                        </div>

                <br />
                   <div class="row">
                            <div class="col-sm-3">
                              Activate Policy by paying Arrears of Premiums (OR)
                            </div>
                            <div class="col-sm-9">
                                                <asp:CheckBox ID="cbxOR" runat="server" AutoPostBack="True" oncheckedchanged="cbxOR_CheckedChanged" />
                            </div>
                        </div>
                                         <br />

                 <div class="row">
                            <div class="col-sm-3">
                             Activate under Special Revival by Extending Commencement Date (SR)
                            </div>
                            <div class="col-sm-9">
                            <asp:CheckBox ID="cbxSR" runat="server" AutoPostBack="True" oncheckedchanged="cbxSR_CheckedChanged" />

                            </div>
                </div>
                                         <br />

                <div class="row">
                            <div class="col-sm-3">
                             Contact Details
                            </div>
                            <div class="col-sm-9">
                            <asp:TextBox ID="tbxMobileNum" runat="server"  class="form-control" MaxLength="12"></asp:TextBox>
                            </div>
                </div>
                    <br />

                     <div class="row">
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-9">
                           <%-- <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary btn-xs pull-left"  Text="Submit" 
                                                                    ValidationGroup="GP102" onclick="btnSubmit_Click"  />--%>

                                 <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary"  Text="Submit"
                                                                    onclick="btnSubmit_Click" />

                            </div>
                </div>
                     <br />

                          <div class="row">
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-9">
                           <asp:HiddenField ID="hdfSeqNo" runat="server" />

                            </div>
                </div>
                     <br />

                          <div class="row">
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-9">
                             <asp:Label ID="lblPayStatus" runat="server" Style="font-weight: 700; font-size: medium;"></asp:Label>

                            </div>
                </div>
                                         <br />

                                          <div class="row">
                            <div class="col-sm-3">
                              
                            </div>
                            <div class="col-sm-9">
                                      <asp:CustomValidator ID="PayPremValidator" runat="server" CssClass="errorMsg_1" 
                                                                ForeColor="Red"></asp:CustomValidator>
                            </div>
                        </div>
                                         
                
</fieldset>
                   </div>
                <br/>

            </asp:Panel>

                          <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="Ggs14451"
                        DisplayAfter="10">
                        <ProgressTemplate>
                            <div class="divWaiting" valign="middle">
                                <img src="/images/load.gif" style="position: absolute;left: 50%;top: 35%"/>
                                <br />
                                <asp:Label ID="lblWait1" runat="server" Text=" Please wait... " style="position: absolute;left: 50%;top: 40%" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                </ContentTemplate>
                <Triggers>
                <asp:PostBackTrigger ControlID = "btnSubmit" />
                </Triggers>
                </asp:UpdatePanel>
               </div>
                 <div class="col-xs-1">
                </div>
                </div>

               
           
        </div>
    <%--    <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <div class="form-group">
                        <table class="table">
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPayStatus" runat="server" Style="font-weight: 700; font-size: medium;"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="col-xs-1">
                </div>
            </div>--%>

    </div>


</asp:Content>

