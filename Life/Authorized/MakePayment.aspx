<%@ Page Title="" Language="C#" MasterPageFile="~/Life.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="MakePayment.aspx.cs" Inherits="Life_Authorized_MakePayment" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
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
    </style>


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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container" id="main-container" style="min-height:600px">
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
     <%--     <br />--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/Life/Authorized/">Life</a></li>
                        <li class="breadcrumb-item active">Pay Third Party Premiums</li>
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
                        Pay Third Party Premiums</h3>
                        </center>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <br/>
     
                  <asp:UpdatePanel ID="Ggs14451" runat="server">
                <ContentTemplate>


               <div class="row">
                <div class="col-xs-1">
                </div>
                 <div class="col-xs-10">
           <%-- <div class="panel panel-default">
                <div class="panel-body">--%>
                    <div class="col-sm-9">
                        <span>
                            <asp:RadioButtonList ID="rblPayType" runat="server" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="rblPayType_SelectedIndexChanged" AutoPostBack="True"
                                ValidationGroup="GP101" Visible="False">
                                <asp:ListItem Value="P" Selected="True" CssClass="test11">Pay Premium &nbsp; &nbsp;</asp:ListItem>
                                <asp:ListItem Value="L" CssClass="test11">Pay Loan</asp:ListItem>
                            </asp:RadioButtonList>
                        </span>
                    </div>
                    <asp:PlaceHolder  ID="Panel3" runat="server" Visible="false">
                    
                        <asp:GridView ID="gvPolicies" runat="server" AutoGenerateColumns="false" CellPadding="4" CssClass="footable"                        
                                OnSelectedIndexChanged ="gvPolicies_SelectedIndexChanged">  
                            <Columns>  
                                <asp:BoundField DataField="POLICY_NUMBER" HeaderText="Policy No."/>  
                                <asp:BoundField DataField="NAME" HeaderText="Name"/>  
                                <asp:BoundField DataField="RELATIONSHIP" HeaderText="Relationship"/> 
                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="30" ControlStyle-CssClass="btn btn-xs btn-primary"  /> 
                            </Columns>                              
                        </asp:GridView>             
                    </asp:PlaceHolder>
                     <div class="form-group">
                        <div class="col-sm-3">
                        </div>
                        <div class="col-sm-9">
                            <asp:CustomValidator ID="PolNumValidator" runat="server" CssClass="errorMsg_1"
                                ForeColor="Red" OnServerValidate="checkPolLoanNum" ValidationGroup="GP101"></asp:CustomValidator>
                        </div>
                    </div>
           <%-- </div>
                </div>--%>
                 
                </div>
                <div class="col-xs-1">
                </div>
                </div>

                <div class="row">
                <div class="col-xs-1">
                </div>
                 <div class="col-xs-10">
                 <div class="button101">&nbsp;<strong>Registering a third party policy</strong></div>
                     <%--To make a payment to a third party policy (not owned by you), first you 
                     have to register it under your user account. To make a request to register a 
                     policy to your account, please complete the form, which can be <a href="/Documents/Third_party_policy.pdf" target="_blank" >downloaded</a> and email a scanned copy to <a href="mailto:slic_phsweb@srilankainsurance.com" target="_top">slic_phsweb@srilankainsurance.com</a> or hand over to the nearest branch.
--%>                <br />
                   <%--  To make payment to a third party policy (not owned by you), it has to be first registered under your user account.
                     <br />
                     In order to register a policy to your account, please complete the 
                     form, which can be <a href="/Documents/Third_party_policy.pdf" target="_blank" >downloaded</a> 
                     and email a scanned copy to <strong>slic_phsweb@srilankainsurance.com</strong> 
                     or hand over to the nearest branch/ your advisor. --%>
                     To make a payment to a third party policy (not owned by you), first you 
                     have to register it under your user account. To make a request to register a 
                     policy to your account, please complete the form, which can be <a href="/Documents/Third_party_policy.pdf" target="_blank" >downloaded</a> and email a scanned copy to <strong>slic_phsweb@srilankainsurance.com</strong> or hand over to the nearest branch or give this to your advisor.

                 </div>
                  <div class="col-xs-1">
                </div>
                </div>

               <%--  <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                        DisplayAfter="10">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <img src="/images/load.gif" />
                                <br />
                                <asp:Label ID="lblWait1" runat="server" Text=" Please wait... " />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                </ContentTemplate>
                <Triggers>
                <asp:PostBackTrigger ControlID = "btnSubmit" />
                </Triggers>
                </asp:UpdatePanel>--%>

            <%-- </div>--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                 <div class="col-xs-10">
            <asp:Panel ID="Panel1" runat="server" Visible="False">
            
               

           <div class="row">
                            <div class="col-sm-3">
                                <strong></strong>
                            </div>
                            <div class="col-sm-9">
                                
                            </div>
                        </div>
                    <%--    <asp:UpdatePanel ID="Ggs14451" runat="server">
                <ContentTemplate>--%>
                 <div class="table-responsive">
                                        <table class="table">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2">
                                                        Policy Details : 
                                                    </td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td width="35%">
                                                        Policy Number
                                                    </td>
                                                    <td width="65%">
                                                         <asp:Literal ID="litPolNumber" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="35%">
                                                        Customer Name
                                                    </td>
                                                    <td width="65%">
                                                        <asp:Literal ID="litName" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="35%">
                                                        Deposits Total
                                                    </td>
                                                    <td width="65%">
                                                        <asp:Literal ID="litDeposits" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <caption>
                                                    <br />
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:GridView ID="gvDemands" runat="server" AutoGenerateColumns="False" 
                                                                CssClass="footable">
                                                                <Columns>
                                                                    <asp:BoundField DataField="DEMAND" HeaderStyle-Font-Bold="true" 
                                                                        HeaderStyle-ForeColor="#000000" HeaderText="Due Date" 
                                                                        ItemStyle-Font-Bold="false">
                                                                    <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                                                    <ItemStyle Font-Bold="False" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="PREMIUM" HeaderStyle-Font-Bold="true" 
                                                                        HeaderStyle-ForeColor="#000000" HeaderText="Premium (Rs.)" 
                                                                        ItemStyle-Font-Bold="false">
                                                                    <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                                                    <ItemStyle Font-Bold="False" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="LATEFEE" HeaderStyle-Font-Bold="true" 
                                                                        HeaderStyle-ForeColor="#000000" HeaderText="Late fee (Rs.)" 
                                                                        ItemStyle-Font-Bold="false">
                                                                    <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                                                    <ItemStyle Font-Bold="False" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="cbPayDue" runat="server" AutoPostBack="true" 
                                                                                OnCheckedChanged="cbPayDue_CheckedChanged" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:CustomValidator ID="PayPremValidator" runat="server" CssClass="errorMsg_1" 
                                                                ForeColor="Red"></asp:CustomValidator>
                                                        </td>
                                                    </tr>
                                                    <caption>
                                                        <br />
                                                        <tr>
                                                            <td width="35%">
                                                                Total Dues Amount (Rs.)</td>
                                                            <td width="65%">
                                                                <asp:Literal ID="litTotDueAmt" runat="server"></asp:Literal>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="35%">
                                                                Payable Dues Amount (Rs.)</td>
                                                            <td width="65%">
                                                                <asp:Literal ID="litPayableDue" runat="server"></asp:Literal>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="35%">
                                                                Future Payment (Rs.)</td>
                                                            <td width="65%">
                                                                <asp:TextBox ID="txtAddtAmt" runat="server"  placeholder="Optional" class="form-control" MaxLength="12"></asp:TextBox>
                                                                <asp:CustomValidator ID="addtAmtValidator" runat="server" 
                                                                    ControlToValidate="txtAddtAmt" CssClass="errorMsg_1" ForeColor="Red" 
                                                                    OnServerValidate="checkAddtAmt"></asp:CustomValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="35%">
                                                            </td>
                                                            <td width="65%">
                                                                <asp:Button ID="btnPayPrem" runat="server" 
                                                                    class="btn btn-primary btn-xs pull-left" OnClick="btnPayPrem_Click" Text="Pay" 
                                                                    ValidationGroup="GP102" />
                                                            </td>
                                                        </tr>
                                                    </caption>
                                                </caption>
                                               
                                            </tbody>
                                        </table>
                                    </div>
<%--
                                     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Ggs14451"
                        DisplayAfter="10">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <img src="/images/load.gif" />
                                <br />
                                <asp:Label ID="lblWait" runat="server" Text=" Please wait... " />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                                    </ContentTemplate>
                                    <Triggers>
                                     <asp:AsyncPostBackTrigger ControlID="btnPayPrem" />
                                    </Triggers>
            </asp:UpdatePanel>--%>


            </asp:Panel>
               </div>
                 <div class="col-xs-1">
                </div>
                </div>

                 <div class="row">
                <div class="col-xs-1">
                </div>
                 <div class="col-xs-10">

            <asp:Panel ID="Panel2" runat="server" Visible="False">
                <%--        <div class="container">--%>
                <div class="panel panel-default">
                    <div class="panel-body">
                        <h4>
                            Loan Details:</h4>
                        <div class="form-group">
                            <div class="col-sm-3">
                               <strong>
                                    Loan Number</strong>
                            </div>
                            <div class="col-sm-9">
                                <asp:Literal ID="litLoanNumber" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <strong>
                                    Policy Number</strong>
                            </div>
                            <div class="col-sm-9">
                                <asp:Literal ID="litPolNum" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <strong>
                                    Customer Name</strong>
                            </div>
                            <div class="col-sm-9">
                                <asp:Literal ID="litName2" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <strong>
                                    Granted Date</strong>
                            </div>
                            <div class="col-sm-9">
                                <asp:Literal ID="litGrantDate" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <strong>
                                    Granted Amount (Rs.)</strong>
                            </div>
                            <div class="col-sm-9">
                                <asp:Literal ID="litGrantAmt" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="table-responsive">
                                <table class="table">
                                    <thead class="thead-default">
                                        <tr>
                                            <th>
                                            </th>
                                            <th>
                                                Date
                                            </th>
                                            <th>
                                                Capital (Rs.)
                                            </th>
                                            <th>
                                                Interest (Rs.)
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <th scope="row">
                                                Next Due
                                            </th>
                                            <td>
                                                <asp:Literal ID="litNextDueDt" runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="litNextDueCap" runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="litNextDueInt" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">
                                                Last Repayment
                                            </th>
                                            <td>
                                                <asp:Literal ID="litLastRepdDt" runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="litLastRepdCap" runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="litLastRepdInt" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                Amount to be paid (Rs.)
                            </div>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtPayLoanAmt" runat="server" class="form-control" MaxLength="12"
                                    ValidationGroup="GP103"></asp:TextBox>
                                <asp:CustomValidator ID="PayLoanValidator" runat="server" ControlToValidate="txtPayLoanAmt"
                                    CssClass="errorMsg_1" ForeColor="Red" OnServerValidate="checkPayLoanAmt" ValidationGroup="GP103"></asp:CustomValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-3">
                            </div>
                            <div class="col-xs-6">
                                <asp:Button ID="btnPayLoan" runat="server" Text="Pay" class="btn btn-primary btn-xs pull-left"
                                    OnClick="btnPayLoan_Click" ValidationGroup="GP103" CausesValidation="false"/>
                            </div>
                            <div class="col-xs-3">
                            </div>
                        </div>
                    </div>
                </div>
                <%--</div>--%>
            </asp:Panel>
             </div>
                 <div class="col-xs-1">
                </div>
                </div>

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
               <%-- <Triggers>
                <asp:PostBackTrigger ControlID = "btnSubmit" />
                </Triggers>--%>
                </asp:UpdatePanel>
            <br />
        </div>
    </div>
</asp:Content>
