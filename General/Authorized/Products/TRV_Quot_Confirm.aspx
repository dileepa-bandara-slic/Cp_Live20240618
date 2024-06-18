<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true"
    CodeFile="TRV_Quot_Confirm.aspx.cs" Inherits="General_Authorized_Products_TRV_Quot_Confirm" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


<%--    <style type="text/css">
        .auto-style2 {
            width: 371px;
            color: #FF6600;
        }

        .auto-style3 {
            color: #FF3300;
        }

        .auto-style4 {
            right: 119px;
        }

        .auto-style7 {
            width: 371px;
            height: 102px;
        }

        .auto-style8 {
            height: 102px;
        }
    </style>--%>

    
    <style type="text/css">

        td, th {
    padding: 5px;
}

        .modal-backdrop {
            
            z-index: 0;
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
    

<%--    <script src="../assets/js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <link href="../css/smoothness/jquery-ui-1.10.4.custom.css" rel="stylesheet"   type="text/css" />--%>


  <%--  <script type="text/javascript" language="javascript">
        //disabling back, needs more testing
        history.pushState(null, null, document.URL);
        window.addEventListener('popstate', function () {
            $('#myModal').modal('show');
            history.pushState(null, null, document.URL);
        });

        $(document).ready(function () {
            $('#myModal').modal('hide');
        });

    </script>--%>


   <%--          <link href="/css/footable.min.css" rel="stylesheet" type="text/css" />

 <script type="text/javascript" src="/js/footable.min.js"></script>--%>
   <%--     <script type="text/javascript">
            $(function () {
                $('[id*=GridView1]').footable();
                $('[id*=gvMembers]').footable();
            });
        </script>--%>

<%--    <style>
        @media (max-width:479px) {
            .navbar-fixed-top + .main-container {
                padding-top: 50px;
            }
        }


        .auto-style9 {
            color: #6699FF;
        }


        .auto-style10 {
            width: 371px;
        }
        .auto-style11 {
            width: 339px;
        }
        </style>
 --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <link href="/css/modal.css" rel="stylesheet" />

       <script type="text/javascript" language="javascript">
        //disabling back, needs more testing
        history.pushState(null, null, document.URL);
        window.addEventListener('popstate', function () {
            $('#myModal').modal('show');
            history.pushState(null, null, document.URL);
        });

        $(document).ready(function () {
            $('#myModal').modal('hide');
        });

    </script>
    <div class="main-container" id="main-container">

             <link href="/css/footable.min.css" rel="stylesheet" type="text/css" />
            <script src="/js/footable.min.js" type="text/javascript"></script>

<script type="text/javascript">
    $(function () {
        $('[id*=GridView1]').footable();
        $('[id*=gvMembers]').footable();
    });
</script>

         

            <%--     <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <center>
    <div class="modal-content  modal-sm">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h5 class="modal-title" id="myModalLabel"><strong>Warning !</strong> </h5>
      </div>
      <div class="modal-body">
       Back button is disabled for this page. Please use the navigation links in the menu  to start a new transaction.
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
      </div>
    </div></center>
                </div>
            </div>--%>

        <div class="container">
         <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
  <div class="modal-dialog" role="document">
 <center>
    <div class="modal-content  modal-sm">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h5 class="modal-title" id="myModalLabel"><strong>Warning !</strong> </h5>
      </div>
      <div class="modal-body">
       Back button is disabled for this page. Please use the navigation links in the menu  to start a new transaction.
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
      </div>
    </div></center>
  </div>
</div>


            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
      <%--      <br />--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/General_Products.aspx">Purchase
                            online</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/Products/TRV_Quot_Confirm.aspx">Travel Protect Quotation</a></li>
                        <li class="breadcrumb-item active">Travel Protect Quotation - Confirm</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
 <%--           </br>--%>
            <asp:UpdatePanel ID="Ggs14451" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <center>
                    <h3>
                        Confirm Quotation: Travel Protect Plan</h3>
                        </center>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                  <%--  <br />--%>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <%--<div class="form-group">--%>

                                <table>
                                    <tbody>
                                        <tr>
                                            <td width="35%">Quotation Number
                                            </td>
                                            <td width="65%">
                                                <asp:Literal ID="litQuotNo" runat="server"></asp:Literal>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">Plan
                                            </td>
                                            <td width="65%">
                                                <asp:Literal ID="litPlan" runat="server"></asp:Literal>
                                                <asp:HiddenField ID="hdn_plan" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">Sum Insured (USD)
                                            </td>
                                            <td width="65%">
                                                <asp:Literal ID="litSumIns" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">Premium (Rs.)
                                            </td>
                                            <td width="65%">
                                                <asp:Literal ID="litPremium" runat="server"></asp:Literal>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:HyperLink ID="hlPrint" runat="server" class="btn btn-primary btn-xs" Font-Bold="True"
                                                    Target="_blank" CssClass="auto-style4">Print Quotation</asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">Purpose of visit
                                            </td>
                                            <td width="65%">
                                                <asp:DropDownList ID="ddr_purpose" runat="server">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="ddrPurposeValidator" runat="server" ControlToValidate="ddr_purpose" CssClass="errorMsg_1" ForeColor="Red" OnServerValidate="checkDdrPurpose"></asp:CustomValidator>
                                            </td>
                                        </tr>

                                      
                                    

                                        <tr>
                                            <td width="50%" colspan="2"><strong><%--<span class="auto-style3">--%><span>*</span>Any sickness/injury/illness related to sports activities of professional and semi professional sportsmen, are excluded.</strong></td>
                                        </tr>

                                      
                                    

                                     <%--   <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>--%>
                                        <tr>
                                            <td width="35%"><strong>Is the Logged User and Proposer same?</strong></td>
                                            <td width="65%">
                                                <strong>
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" CssClass="auto-style3" Font-Bold="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </strong>
                                            </td>
                                        </tr>

                                        <%
                                            if (RadioButtonList1.Text == "No")
                                            {
                                        %>
                                        <tr>
                                            <td width="35%">Proposer&#39;s Name </td>
                                            <td width="65%">
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                         <asp:DropDownList ID="ddltitle" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltitle_SelectedIndexChanged">
                                                         </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtname" runat="server" AutoPostBack="True" OnTextChanged="txtname_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="NameRequired" runat="server" ControlToValidate="txtname" ErrorMessage="Proposer Name is required" Font-Size="Small" ForeColor="Red" ToolTip="Proposer Name is required">Proposer Name is required</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                               
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">NIC Number </td>
                                            <td width="65%">

                                                <div class="row">
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txtnic" runat="server" OnTextChanged="txtnic_TextChanged" MaxLength="12"></asp:TextBox>
                                              
                                                        </div>
                                                     <div class="col-sm-7">
                                                  <asp:RequiredFieldValidator ID="NICRequired" runat="server" ControlToValidate="txtnic" ErrorMessage="NIC Number is required" Font-Size="Small" ForeColor="Red" ToolTip="NIC Number is required.">NIC Number is required.</asp:RequiredFieldValidator>
                                                      <asp:CustomValidator ID="NICValidator" runat="server" ControlToValidate="txtnic" ForeColor="Red" OnServerValidate="checkNIC" ValidateEmptyText="true"></asp:CustomValidator>
                                                   <asp:CustomValidator ID="NICValidator2" runat="server" ControlToValidate="txtnic" ForeColor="Red" OnServerValidate="checkNICExist" ValidateEmptyText="true"></asp:CustomValidator>
                                                
                                                     </div>
                                                 
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">Date of Birth </td>
                                            <td width="65%">
                                                     <div class="row">
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txtdob" runat="server" AutoPostBack="True" class="form-control" MaxLength="10" placeholder="Date of birth" OnTextChanged="txtdob_TextChanged" ToolTip="yyyy/mm/dd"></asp:TextBox>
                                               
                                                        </div>
                                                         <div class="col-sm-7">
                                                               <asp:CustomValidator ID="DOBValidator" runat="server" ControlToValidate="txtdob" ForeColor="Red" OnServerValidate="checkDOB"></asp:CustomValidator>
                                                 <asp:RequiredFieldValidator ID="DateOfBirthRequired" runat="server" ControlToValidate="txtdob" ErrorMessage="Date of Birth is required." Font-Size="Small" ForeColor="Red" ToolTip="Date of Birth is required.">Date of Birth is required.</asp:RequiredFieldValidator>
                                                 <asp:CustomValidator ID="NICDOBValidator" runat="server" ControlToValidate="txtdob" ErrorMessage="NIC, Date of Birth / NIC, Gender doesn't matched" ForeColor="Red" OnServerValidate="checkNICDOB" ValidateEmptyText="true"></asp:CustomValidator>
                                           
                                                             </div>
                                                         
                                                         </div>
                                                 </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">Address </td>
                                            <td width="65%">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                          <asp:TextBox ID="txtadd" runat="server" Wrap="False" AutoPostBack="True" OnTextChanged="txtadd_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="Add1Required1" runat="server" ControlToValidate="txtadd" ErrorMessage="Address Line 1 is required" Font-Size="Small" ForeColor="Red" ToolTip="Address Line 1 is required">Address Line 1 is required</asp:RequiredFieldValidator>
                                               
                                                        </div>

                                                    <div class="col-sm-3">
                                                          <asp:TextBox ID="txtadd0" runat="server" OnTextChanged="txtadd0_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="Add1Required2" runat="server" ControlToValidate="txtadd0" ErrorMessage="Address Line 2 is required" Font-Size="Small" ForeColor="Red" ToolTip="Address Line 2 is required">Address Line 2 is required</asp:RequiredFieldValidator>
                                                
                                                        </div>
                                                     <div class="col-sm-3">
                                                         <asp:TextBox ID="txtadd1" runat="server"></asp:TextBox>
                                                         </div>
                                                <div class="col-sm-3">
                                                <asp:TextBox ID="txtadd2" runat="server"></asp:TextBox>
                                                    </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">Mobile Number </td>
                                            <td width="65%">
                                                <asp:TextBox ID="txtmob" runat="server" MaxLength="10" OnTextChanged="txtmob_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="contctnoRequired" runat="server" ControlToValidate="txtmob" ErrorMessage="Contact No is required" Font-Size="Small" ForeColor="Red" ToolTip="Contact No is required">Contact No is required</asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="MobileNoValidator" runat="server" ControlToValidate="txtmob" ForeColor="Red" OnServerValidate="checkMobileNo"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">Home Number </td>
                                            <td width="65%">
                                                <asp:TextBox ID="txthmno" runat="server" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">Office Number </td>
                                            <td width="65%">
                                                <asp:TextBox ID="txtofno" runat="server" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <%
                                            }
                                            else
                                            {
                                        %>
                                        <tr>
                                            <td width="35%">Proposer&#39;s Name </td>
                                            <td width="65%">
                                                <asp:Literal ID="litTitle" runat="server"></asp:Literal>
                                                <asp:Literal ID="litName" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">NIC Number
                                            </td>

                                            <td width="65%">

                                                <asp:Literal ID="litNIC" runat="server"></asp:Literal>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td width="35%">Date of Birth
                                            </td>
                                            <td width="65%">

                                                <asp:Literal ID="litBirthDate" runat="server"></asp:Literal>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td width="35%">Address
                                            </td>
                                            <td width="65%">
                                                <asp:Literal ID="litAddress" runat="server"></asp:Literal>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td width="35%">Mobile Number
                                            </td>
                                            <td width="65%">
                                                <asp:Literal ID="litMobileNo" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">Home Number
                                            </td>
                                            <td width="65%">
                                                <asp:Literal ID="litHomeNo" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">Office Number
                                            </td>
                                            <td width="65%">
                                                <asp:Literal ID="litOfficeNo" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">Email </td>
                                            <td width="65%">
                                                <asp:Literal ID="litEmail" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <%
                                            }

                                        %>
                                        <tr>
                                            <td width="35%">Leaving Sri Lanka on
                                            </td>
                                            <td width="65%">
                                                <asp:Literal ID="lit_leaving" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="35%">Returning to Sri Lanka on
                                            </td>
                                            <td width="65%">
                                                <asp:Literal ID="lit_returning" runat="server"></asp:Literal>
                                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_shen" runat="server" Font-Bold="True"
                                                    Font-Names="Calibri" Font-Size="10pt" ForeColor="#993300" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                        <%--    </div>--%>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Details of Journey (Visiting Countries)
                            <asp:HiddenField ID="hdn_plan0" runat="server" />
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:GridView ID="GridView1" CssClass="footable" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Row Number">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="country_name" HeaderText="Country Name" SortExpression="country_name" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-xs-1">
                        </div>

                    </div>
                    <br />
                      <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                              <strong>Contact person / beneficiary in case of an emergency </strong>
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
                                      <%--  <caption>
                                            <strong>Contact person / beneficiary in case of an emergency </strong>
                                        
                                        </caption>--%>
                                <tr>
                                    <td>Name
                                    </td>
                                    <td>
                                        <%--<div class="form-inline">--%>
                                        <asp:TextBox ID="txt_ConName" runat="server" class="form-control"
                                            MaxLength="100" OnTextChanged="txt_ConName_TextChanged" placeholder="Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="cntNamRequired" runat="server"
                                            ControlToValidate="txt_ConName" CssClass="errorMsg_1"
                                            ErrorMessage="Contact Name is required" ForeColor="Red"
                                            ToolTip="Contact Name is required"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="cntNameValidator" runat="server"
                                            ControlToValidate="txt_ConName" CssClass="errorMsg_1" ForeColor="Red"
                                            OnServerValidate="checkCntName"></asp:CustomValidator>
                                    </td>
                                </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table>
                                                    <tr>
                                                        <td style="color: #FF6600; font-weight: bold;">Is the Contact Address is same as the Proposer ?&nbsp;&nbsp; </td>
                                                        <td><strong>
                                                            <asp:RadioButtonList ID="rdo_address" runat="server" AutoPostBack="True" CssClass="auto-style3" Font-Bold="True" OnSelectedIndexChanged="rdo_address_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                                <asp:ListItem>Yes</asp:ListItem>
                                                                <asp:ListItem Selected="True">No</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            </strong></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Address
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_ConAdd1" runat="server" class="form-control"
                                                    MaxLength="30" OnTextChanged="txt_ConAdd1_TextChanged"
                                                    placeholder="Address Line 1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="contAdrs1Required" runat="server"
                                                    ControlToValidate="txt_ConAdd1" CssClass="errorMsg_1"
                                                    ErrorMessage="Address Line 1 is required" ForeColor="Red"
                                                    ToolTip="Address Line 1 is required"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="adrs1Validator" runat="server"
                                                    ControlToValidate="txt_ConAdd1" CssClass="errorMsg_1" ForeColor="Red"
                                                    OnServerValidate="checkCntAdrs1"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txt_ConAdd2" runat="server" class="form-control"
                                                    MaxLength="30" OnTextChanged="txt_ConAdd2_TextChanged"
                                                    placeholder="Address Line 2"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="contAdrs2Required" runat="server"
                                                    ControlToValidate="txt_ConAdd2" CssClass="errorMsg_1"
                                                    ErrorMessage="Address Line 2 is required" ForeColor="Red"
                                                    ToolTip="Address Line 2 is required"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="adrs2Validator" runat="server"
                                                    ControlToValidate="txt_ConAdd2" CssClass="errorMsg_1" ForeColor="Red"
                                                    OnServerValidate="checkCntAdrs2"></asp:CustomValidator>
                                            </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:TextBox ID="txt_ConAdd3" runat="server" class="form-control"
                                                        MaxLength="30" OnTextChanged="txt_ConAdd3_TextChanged"
                                                        placeholder="Address Line 3"></asp:TextBox>
                                                    <asp:CustomValidator ID="adrs3Validator" runat="server"
                                                        ControlToValidate="txt_ConAdd3" CssClass="errorMsg_1" ForeColor="Red"
                                                        OnServerValidate="checkCntAdrs3"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txt_ConAdd4" runat="server" class="form-control"
                                                    MaxLength="30" placeholder="Address Line 4"></asp:TextBox>
                                                <asp:CustomValidator ID="adrs4Validator" runat="server"
                                                    ControlToValidate="txt_ConAdd4" CssClass="errorMsg_1" ForeColor="Red"
                                                    OnServerValidate="checkCntAdrs4"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Mobile No.
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_ConTel1" runat="server" class="form-control"
                                                    MaxLength="10" OnTextChanged="txt_ConTel1_TextChanged"
                                                    placeholder="Mobile No. "></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="tel1Required" runat="server"
                                                    ControlToValidate="txt_ConTel1" CssClass="errorMsg_1"
                                                    ErrorMessage="Contact number is required" ForeColor="Red"
                                                    ToolTip="Contact number is required"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="tel1Validator" runat="server"
                                                    ControlToValidate="txt_ConTel1" CssClass="errorMsg_1" ForeColor="Red"
                                                    OnServerValidate="checkCntTel1"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txt_ConTel2" runat="server" class="form-control"
                                                    MaxLength="10" OnTextChanged="txt_ConTel2_TextChanged"
                                                    placeholder="Telephone No."></asp:TextBox>
                                                <asp:CustomValidator ID="tel2Validator" runat="server"
                                                    ControlToValidate="txt_ConTel2" CssClass="errorMsg_1" ForeColor="Red"
                                                    OnServerValidate="checkCntTel2"></asp:CustomValidator>
                                            </td>
                                        </tr>

                                    </tbody>
                                    <%--</tr>
                            </caption>--%>
                                </table>
                            </div>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>

                    <%--<div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <div class="form-group">
                                <table class="table">
                                    <tbody>
                                        <caption>
                                            <strong>Medical Details </strong>
                                            <br>
                                        </tr>
                                <tr>
                                    <td class="auto-style10">Pre existing Medical Conditions including birth defects</td>
                                    <td>
                                         
                                        <asp:TextBox ID="txtPremed" runat="server" class="form-control"
                                            MaxLength="100" OnTextChanged="txt_ConName_TextChanged" placeholder="Pre existing Medical conditions"></asp:TextBox>
                                    </td>
                                </tr>
                                        <tr>
                                            <td class="auto-style10">Are you taking medicine for
                                            </td>
                                            <td>
                                                <strong>
                                                <asp:CheckBox ID="ChkDiab" runat="server" Font-Bold="True" ForeColor="Black" Text="Diabetes" />
                                                </strong>&nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="Chkhyper" runat="server" Font-Bold="True" ForeColor="Black" Text="Hypertension" />
                                                &nbsp;
                                                <asp:CheckBox ID="ChkCholes" runat="server" Font-Bold="True" ForeColor="Black" Text="Cholesterol" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style10">Are you presently in good health :</td>
                                            <td>
                                                <asp:CheckBox ID="chkYes" runat="server" AutoPostBack="True" OnCheckedChanged="chkYes_CheckedChanged" Text="Yes" />
                                                &nbsp;&nbsp;
                                                <asp:CheckBox ID="chkNo" runat="server" AutoPostBack="True" OnCheckedChanged="chkNo_CheckedChanged" Text="No" />
                                            </td>
                                            <tr>
                                                <td class="auto-style10">If <strong>&quot; No &quot; </strong>&nbsp;Please specify</td>
                                                <td>
                                                    <asp:TextBox ID="txt_no" runat="server" class="form-control"
                                                        MaxLength="30"
                                                        placeholder="Reason for No "></asp:TextBox>
                                                </td>
                                            </tr>
                                                                                     
                                    </tbody>
                                    </tr>
                            </caption>
                                </table>
                            </div>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>--%>

                    
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="Btn_Submit" />
                    <asp:PostBackTrigger ControlID="RadioButtonList1" />
                </Triggers>
            </asp:UpdatePanel>


                    <div class="row">
                        <div class="col-xs-1"></div>
                        <div class="col-xs-10">
                            <strong>All members</strong>
                        </div>
                        <div class="col-xs-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1"></div>
                        <div class="col-xs-10">
                            <asp:GridView ID="gvMembers" CssClass="footable" runat="server" AutoGenerateColumns="false"
                                ForeColor="Black" OnRowDataBound="gvMembers_RowDataBound" OnRowEditing="gvMembers_RowEditing">
                                <AlternatingRowStyle BackColor="#F9F9F9" />
                                <Columns>
                                    <asp:BoundField DataField="member_id" HeaderText="Number" SortExpression="member_id" />
                                    <asp:TemplateField HeaderText="Title">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddTitle" runat="server" placeholder="Title" DataMember="TITLE" DataValueField="TITLE" AutoPostBack="True" OnSelectedIndexChanged="ddTitle_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name as in Passport">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtName" runat="server" MaxLength="200" CssClass="form-control"
                                                placeholder="Name as in Passport"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Passport No.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPP" runat="server" MaxLength="20" CssClass="form-control" placeholder="Passport No."></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="genderDesc" HeaderText="Gender" SortExpression="genderDesc" />
                                    <asp:BoundField DataField="dob" HeaderText="Date of Birth" SortExpression="dob" />
                                    <asp:BoundField DataField="age" HeaderText="Age" SortExpression="age" />
                                    <asp:BoundField DataField="memType_Desc" HeaderText="Member Type" SortExpression="membType_Desc" />
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                        </div>
                        <div class="col-xs-1"></div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10" style="text-align: center">
                            <asp:Button ID="Btn_Submit" runat="server" CssClass="btn btn-primary btn-xs" Text="Submit" OnClick="Btn_Submit_Click" style="left: 0px; top: 5px" />
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Label ID="lblErrMesg" runat="server" CssClass="errorMsg_1" ForeColor="Red"></asp:Label>
                            &nbsp;<asp:HiddenField ID="hdfval" runat="server" />
                            <asp:HiddenField ID="hdftrv" runat="server" />
                        </div>
                         <div class="col-xs-1">
                        </div>
                    </div>


<%--                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="Btn_Submit" />
                    <asp:PostBackTrigger ControlID="RadioButtonList1" />
                </Triggers>
            </asp:UpdatePanel>--%>
        </div>
        <br />
        <br />
        <br />

 
        <link href="/css/jquery-ui.css" rel="stylesheet" />
                <script src="/js/jquery-ui.js"></script>
        <script type="text/javascript" language="javascript">

            

            $(function () {
                today = new Date();
                var month, day, year;
                year = today.getFullYear();
                month = today.getMonth();
                date = today.getDate();
                year = today.getFullYear() - 0;
                var backdate = new Date(year, month, date)
                var backdate1 = new Date(year, month, date)

      
                $("input[id$='txtdob']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, maxDate: 0, yearRange: "-80:+0", numberOfMonths: 1 });
           
            });



            $(document).ready(function () {

                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);


                today = new Date();
                var month, day, year;
                year = today.getFullYear();
                month = today.getMonth();
                date = today.getDate();
                year = today.getFullYear() - 0;
                var backdate = new Date(year, month, date)
                var backdate1 = new Date(year, month, date)


                function EndRequestHandler(sender, args) {

                   
                    $("input[id$='txtdob']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, maxDate: 0, yearRange: "-80:+0", numberOfMonths: 1 });
                }

            });


        </script>

    </div>
</asp:Content>

