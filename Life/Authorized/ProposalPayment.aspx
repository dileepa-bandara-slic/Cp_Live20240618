<%@ Page Title="" Language="C#" MasterPageFile="~/Life.master" AutoEventWireup="true" CodeFile="ProposalPayment.aspx.cs" Inherits="ProposalPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-container" id="main-container" style="min-height:600px">

<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div class="container">

        <div class="row">
        <div class = "col-xs-1">        
        </div>
        <div class="col-xs-10">
        <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/Life/Authorized/">Life</a></li>
                        <li class="breadcrumb-item active">Proposal Payments</li>
                    </ol>
        </div>
        <div class="col-xs-1">
        </div>
        </div>

        <div class="row">
        <div class = "col-xs-1">        
        </div>
        <div class="col-xs-10">
        <center>
        <h3>Proposal Payments</h3>
        </center>
        </div>
        <div class="col-xs-1">
        </div>
        </div>

        <div class="row">
        <div class = "col-xs-1">        
        </div>
        <div class="col-xs-10">
        <asp:UpdatePanel ID="ppupdpnl1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <strong>Proposal Details :</strong>
        <fieldset>
                        <legend></legend>
        <asp:Panel ID="ppPanel1" runat="server" visible="true">
            <div>
                <center style="text-align: left">
                    
                    
                </center>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    Proposal Number
                </div>
                <div class="col-sm-9">
                    <asp:TextBox ID="txt_propNum" runat="server" class="form-control" MaxLength="12" AutopostBack="true" 
                    ontextchanged="txtProposalNumber_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="propNoRequired" runat="server" ControlToValidate="txt_propNum" 
                    ErrorMessage="Proposal Number is required" ToolTip="Proposal Number is required"
                    ForeColor="Red" Font-Size="small" >Proposal Number is required</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="propCustomVal" runat="server" ControlToValidate="txt_propNum" 
                    ErrorMessage="No record found for this proposal number." ForeColor="Red" ></asp:CustomValidator>
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-3">
                    Name
                </div>
                <div class="col-sm-9">
                    <asp:Literal ID="lit_name" runat="server"></asp:Literal>
                </div>            
            </div>
            <br />
            <div class="row">
                <div class="col-sm-3">
                    NIC No
                </div>
                <div class="col-sm-9">
                    <asp:Literal ID="lit_nic" runat="server"></asp:Literal>
                </div>            
            </div> 
            <br />
            <div class="row">
                <div class="col-sm-3">
                    Policy Type
                </div>
                <div class="col-sm-9">
                    <asp:Literal ID="lit_poltype" runat="server"></asp:Literal>
                </div>            
            </div>
            <br />
            <div class="row">
                <div class="col-sm-3">
                    Sum Assured
                </div>
                <div class="col-sm-9">
                    <asp:Literal ID="lit_SumAssured" runat="server"></asp:Literal>
                </div>            
            </div> 
            <br /> 
            <div class="row">
            <div class="col-sm-3">
                Payment Amount (Rs.)
            </div>
            <div class="col-sm-9">
                <asp:TextBox ID="txt_amount" runat="server" class="form-control" MaxLength="12" ></asp:TextBox>                
                    <asp:CustomValidator ID="customValAmount" runat="server" ControlToValidate="txt_amount" 
                    ForeColor="Red" CssClass="errorMsg_1" OnServerValidate="checkAddtAmt" ></asp:CustomValidator>
            </div>
            </div>
           <%--<% if (!lit_poltype.Text.Equals("Online Early Cash"))
               {
                   %>--%>
            <br /> 
            <div class="row">
            <div class="col-sm-3">
                Policy Fee (Rs.)
            </div>
            <div class="col-sm-8">
                <asp:TextBox ID="txt_policy_fee" runat="server" class="form-control" 
                    MaxLength="12" ReadOnly="True" ></asp:TextBox>
                    
            </div>
            <div class="col-sm-1">
                <asp:CheckBox ID="cbx_policy_fee" runat="server" class="form-control" />
            </div>
                
            </div>
           <%-- <%
                }
                %>
            --%>
            <br />       
            <br /> 
            <div class="row">
                <div class="col-sm-3"></div>
                <div class="col-sm-9">
                    <asp:Button ID="btn_submit" runat="server" class="btn btn-primary" Text="Pay" onclick="btnSubmit_Click" />
                </div>
            </div> 
            <div class="row">
            <div class="col-sm-3"></div>
            <div class="col-sm-9">
            <asp:CustomValidator ID="CustomValAmount2" runat="server" CssClass="errorMsg_1" ForeColor="Red"></asp:CustomValidator>
            </div>
            </div>     
        </asp:Panel>  
        </fieldset>      
        </ContentTemplate>        
        </asp:UpdatePanel>
        </div>
        <div class="col-xs-1">
        </div>
        </div>

        </div>


</div>
</asp:Content>

