<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true"
    CodeFile="Renewal.aspx.cs" Inherits="General_Authorized_Renewal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        @media (max-width:479px)
        {
            .navbar-fixed-top + .main-container
            {
                padding-top: 40px;
            }
        }
        
        .style12
        {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container" id="main-container" style="min-height:600px">
        <div class="container">
       <%--  <br />--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/ClientHome.aspx">Manage Policies</a></li>
                        <li class="breadcrumb-item active">Renew Policies</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
           
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10" style="text-align:center">
                    <h3>
                        Renewal</h3>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
           <%-- <br/>--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <div class="form-group">
                        <table class="table">
                            <tbody>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                         
                <asp:Label ID="lblWrnMsg" runat="server" ForeColor="Orange" Font-Size="13pt"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Policy Number
                                    </td>
                                    <td>
                                        <asp:Literal ID="litPolNum" runat="server"></asp:Literal>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        Reference No. for CDM Payments
                                    </td>
                                    <td>
                                        <asp:Literal ID="litPayRefNo" runat="server"></asp:Literal>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        Insured Name
                                    </td>
                                    <td>
                                        <asp:Literal ID="litInsured" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Address
                                    </td>
                                    <td>
                                        <asp:Literal ID="litAddress" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                 <% if (dept == "M")
                                {%>
                                <tr>
                                    <td>
                                        Vehicle Number
                                    </td>
                                    <td>
                                        <asp:Literal ID="litVehiNum" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                 <%} %>
                                   <% if (dept == "M" || (dept == "G" && (polType == "HIP" || polType == "AMP")))
                                {%>
                                <tr>
                                    <td>
                                        Start Date
                                    </td>
                                    <td>
                                        <asp:Literal ID="litStartDate" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td>
                                        End Date
                                    </td>
                                    <td>
                                        <asp:Literal ID="litEndDate" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                 <%} %>
                                <tr>
                                    <td>
                                        Sum Insured
                                    </td>
                                    <td>
                                   <asp:Literal ID="litSumInsurd" runat="server"></asp:Literal>
                                    </td>
                                 </tr>
                                 <tr>
                                    <td>
                                        Additional Covers
                                    </td>
                                    <td>
                                   <asp:Literal ID="litCovers" runat="server"></asp:Literal>
                                    </td>
                                 </tr>

                                 <%if (dept == "M" || (dept == "G" && (polType == "HIP" || polType == "AMP")))
          { %>
                                <tr>
                                    <td>
                                    <% if (dept == "M")
                                       {%>
                                        Net Premium
                                    <%}
                                       else
                                       { %>
                                       Basic Premium
                                    <%} %>
                                    </td>
                                    <td>
                                   <asp:Literal ID="litBasicPrem" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                    <tr>
                                    <td>
                                        RCC Premium
                                    </td>
                                    <td>
                                        <asp:Literal ID="litRccPrem" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        TC Premium
                                    </td>
                                    <td>
                                   <asp:Literal ID="litTcPrem" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        VAT amount
                                    </td>
                                    <td>
                                   <asp:Literal ID="litVatAmt" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Admin Fee Amount
                                    </td>
                                    <td>
                                   <asp:Literal ID="litAdmnAmt" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Stamp Fee
                                    </td>
                                    <td>
                                   <asp:Literal ID="litStampFee" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Policy Fee
                                    </td>
                                    <td>
                                   <asp:Literal ID="litPolFee" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        NBT Value
                                    </td>
                                    <td>
                                   <asp:Literal ID="litNbtVal" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                         Road Tax
                                    </td>
                                    <td>
                                   <asp:Literal ID="litRdTax" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                             

                                   <tr>
                                    <td>
                                      <b> Total Premium </b>
                                    </td>
                                    <td>
                                    <span class="style12"><asp:Literal ID="litTotPrem" runat="server"></asp:Literal></span>
                                    </td>
                                </tr>
                               
                                
                                  <%}
                                      if ((dept != "M" || (dept == "M" && (debitBalAmt > 0 || partialAmt > 0))) && polType != "HIP" && polType != "AMP")
                                      {%>
                                   <tr> <td>
                                    <% if (dept == "M" && debitBalAmt > 0)
                                       { %>
                                       Debit Outstanding Amount (Rs.)
                                    <%}
                                       else if (dept == "M" && partialAmt > 0)
                                       { %>
                                       Outstanding Amount (Rs.)
                                    <%}
                                       else
                                       { %>
                                      Premium amount (Rs.)
                                      <%} %>
                                   </td>

                                   <td>
                                     <asp:TextBox ID="txtAmount" runat="server"
                    style="text-align: left" CssClass="form-control"></asp:TextBox>
                    
                                    </td>
</tr>
                                
                                <%} %>

                                <tr>
                                <td>&nbsp;</td>
                                <td><asp:Label ID="lblErrMesg" runat="server" ForeColor="Red"></asp:Label></td>
                                </tr>
                                 
                                
                                   <tr>
                                    <td>
                                      
                                    </td>
                                    <td>
                                     <asp:Button ID="btnPay" runat="server" Text="Pay Renewal" class="btn btn-primary btn-xs"
                    onclick="btnPay_Click" />
                                    </td>
                                    
                                </tr>

                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            </br>
            </br>
        </div>
    </div>
</asp:Content>
