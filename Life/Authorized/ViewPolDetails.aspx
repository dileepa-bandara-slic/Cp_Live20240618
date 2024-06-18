<%@ Page Title="" Language="C#" MasterPageFile="~/Life.master" AutoEventWireup="true" CodeFile="ViewPolDetails.aspx.cs" Inherits="Life_Authorized_ViewPolDetails" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
@media (max-width:479px)
{.navbar-fixed-top+.main-container{padding-top:40px};
</style>


<style>
    button.accordion {
        border-style: none;
        border-color: inherit;
        border-width: medium;
        background-color: #eee;
        color: #444;
        cursor: pointer;
        padding: 10px;
        width: 99%;
        text-align: left;
        outline: none;
        font-size: 15px;
        transition: 0.4s;
}

button.accordion.active, button.accordion:hover {
    background-color: #ddd;
}

button.accordion:after {
    content: '\002B';
    color: #777;
    font-weight: bold;
    float: right;
    margin-left: 5px;
}

button.accordion.active:after {
    content: "\2212";
}

div.panel {
    padding: 0 18px;
    background-color: white;
    max-height: 0;
    overflow: hidden;
    transition: max-height 0.2s ease-out;
     
}
    .auto-style5 {
        width: 61%;
        margin-left: 222px;
    }
    .auto-style10 {
        width: 363px;
    }
    .auto-style13 {
        height: 22px;
    }
    .auto-style16 {
        width: 363px;
        height: 33px;
    }
    .auto-style17 {
        height: 33px;
    }
    .auto-style18 {
        width: 233px;
    }
    .auto-style20 {
        width: 281px;
    }
    .auto-style24 {
        height: 22px;
        width: 198px;
    }
    .auto-style25 {
        width: 198px;
    }
    .auto-style26 {
        width: 233px;
        height: 39px;
    }
    .auto-style27 {
        width: 281px;
        height: 39px;
    }
    .auto-style28 {
        width: 246px;
        height: 39px;
    }
    .auto-style29 {
        width: 198px;
        height: 39px;
    }
    .auto-style30 {
        height: 39px;
    }
    .auto-style32 {
        width: 233px;
        height: 22px;
    }
    .auto-style33 {
        height: 22px;
        width: 281px;
    }
    .auto-style34 {
        height: 22px;
        width: 246px;
    }
    .auto-style35 {
        width: 246px;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"> </asp:ToolkitScriptManager>--%>
    <div class="main-container" id="main-container" style="min-height:600px">
        <link href="/css/footable.min.css"
            rel="stylesheet" type="text/css" />
       <script src="/js/footable.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=gvOnlPaymnts]').footable();
                $('[id*=gvDemands]').footable();
                $('[id*=gvDeposits]').footable();
                
            });

            
        </script>
        <div class="container">
       <%-- <br />--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/Life/Authorized/">Life</a></li>
                        <li class="breadcrumb-item"><a href="/Life/Authorized/ClientHome.aspx">Manage My Policies</a></li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item active">View Policy Details</li>
                    </ol>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                <center>
                    <h3>
                        View Policy Details</h3>
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
                    <asp:Label ID="lblErrMesg" runat="server" ForeColor="Red"></asp:Label></div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1"></div>
                <div class="col-xs-10">
                    <asp:GridView ID="gvOnlPaymnts" runat="server" AutoGenerateColumns="False" CssClass="footable"
                        Caption="<b>Recent Online payments</b>">
                        <Columns>
                            <asp:BoundField DataField="POL_NUM" HeaderText="Policy/ Loan No." HeaderStyle-Font-Bold="true"
                                HeaderStyle-ForeColor="#000000" ItemStyle-Font-Bold="false" />
                            <asp:BoundField DataField="PAY_AMT" HeaderText="Payment Amount (Rs.)" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="#000000" ItemStyle-Font-Bold="false" />
                            <asp:BoundField DataField="ENTRY_DATE" HeaderText="Date of Transaction" HeaderStyle-Font-Bold="true"
                                HeaderStyle-ForeColor="#000000" ItemStyle-Font-Bold="false" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <asp:Panel ID="Panel1" runat="server">
                <div class="row">
                    <div class="col-xs-1">
                    </div>
                    <div class="col-xs-10">
                        <div class="well">
                           <%-- <div class="panel-body">--%>
                                <p>
                                    A form will be available for download which should be duly filled and submitted
                                    to SLIC.
                                    <br />                                    
                                    You will be able to view your policy information after the submitted form has been
                                    processed by SLIC.<br />
                                    <br />
                                    <asp:CheckBox ID="chkAgree" runat="server" CssClass="mycheckbox" Text="&nbsp;&nbsp; I agree" />
                                    <asp:HiddenField ID="hfPolNum" runat="server" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit"
                                        OnClick="btnSubmit_Click" />
                                </p>
                           <%-- </div>--%>
                        </div>
                    </div>
                    <div class="col-xs-1">
                    </div>
                </div>
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel2" runat="server">
                <div class="row">
                    <div class="col-xs-1">
                    </div>
                    <div class="col-xs-10">
                        <div class="well">
                           <%-- <div class="panel-body">--%>
                                <p>
                                    Authority for viewing additional policy details is pending.<br />
                                    Please download the following form and submit the duly filled form to Sri Lanka
                                    Insurance.                                    
                                    <br /><br />
                                    <asp:HyperLink ID="hlRequestForm" runat="server">Request Form </asp:HyperLink>
                                    to view additional policy details
                                    <br /><br />
                                    Please submit a copy of the completed form to any SLIC branch OR email a scanned copy to: slic_phsweb@srilankainsurance.com .
                                    <br />
                                    Your request will be processed within 2 working days.
                                </p>
                           <%-- </div>--%>
                        </div>
                    </div>
                    <div class="col-xs-1">
                    </div>
                </div>
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel3" runat="server">

            <%--<button class="accordion" type="button">Basic Information</button>
<div class="panel">
--%>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4">
                        Policy Number</div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litPolNumber" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4">
                        Name</div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litName" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4">
                        Address</div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litAddr1" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4"></div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litAddr2" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4"></div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litAddr3" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4"></div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litAddr4" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4">
                        Sum Insured</div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litSumIns" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4">
                        Policy Type</div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litPolType" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4">
                        Term</div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litPolTerm" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4">
                        Mode</div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litPolMode" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4">
                        Date of Commencement</div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litComDate" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4">
                        Policy Staus</div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litPolStatus" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4">
                        Last Paid Due</div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litLastPdDue" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4">
                        Last Paid Premium (Rs.)</div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litLastPdAmt" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-4">
                        Last Paid Date</div>
                    <div class="col-xs-6">
                        <asp:Literal ID="litLastPdDate" runat="server"></asp:Literal></div>
                    <div class="col-xs-1"></div>
                </div>
                <br />
<button class="accordion" type="button"><span style="font-size:0.8em;"><strong> Policy Loan Inquiry</span></strong></button>
                <div class="panel">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <%--<table style="width:100%;">
                            <tr>
                                <td class="auto-style22">Policy Holder Name</td>
                                <td class="auto-style24">:<asp:Label ID="Label5" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style25">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style22">Loan Capital</td>
                                <td class="auto-style24">:<asp:Label ID="Label6" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style25">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style22">
                                    <asp:Label ID="Label7" runat="server" Text="Loan Granted Date"></asp:Label>
                                </td>
                                <td class="auto-style24">:<asp:Label ID="Label8" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style25">
                                    <asp:Label ID="Label9" runat="server" Text="Loan Interest Rate"></asp:Label>
                                </td>
                                <td>:<asp:Label ID="Label10" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style22">&nbsp;</td>
                                <td class="auto-style24">&nbsp;</td>
                                <td class="auto-style25">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style27">Loan outstandind as at
                                    <asp:Label ID="Label11" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style28">:<asp:Label ID="Label12" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style29"></td>
                                <td class="auto-style13"></td>
                            </tr>
                            <tr>
                                <td class="auto-style27"></td>
                                <td class="auto-style28"></td>
                                <td class="auto-style29"></td>
                                <td class="auto-style13"></td>
                            </tr>
                            <tr>
                                <td class="auto-style22">
                                    <asp:Label ID="Label13" runat="server" Text="Capital"></asp:Label>
                                </td>
                                <td class="auto-style24">:Rs.<asp:Label ID="Label14" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style25">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style22">Interest</td>
                                <td class="auto-style24">:Rs.<asp:Label ID="Label15" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style25">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style27">Total</td>
                                <td class="auto-style28">:Rs.<asp:Label ID="Label16" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style29"></td>
                                <td class="auto-style13"></td>
                            </tr>
                        </table>--%>


                       
                        
                        <div class="row">
                    <div class="col-xs-1"></div>
                        
                            <asp:Panel ID="Panel8" runat="server" Height="235px">
                                <table class="w-100">
                                    <tr>
                                        <td class="auto-style32">Loan Number</td>
                                        <td class="auto-style33">:<asp:Label ID="Label8" runat="server"></asp:Label>
                                        </td>
                                        <td class="auto-style34">&nbsp;</td>
                                        <td class="auto-style24">&nbsp;</td>
                                        <td class="auto-style13">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style32">Loan Capital </td>
                                        <td class="auto-style33">:Rs.<asp:Label ID="Label3" runat="server"></asp:Label>
                                        </td>
                                        <td class="auto-style34"></td>
                                        <td class="auto-style24"></td>
                                        <td class="auto-style13">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style18">Loan Granted Date</td>
                                        <td class="auto-style20">:<asp:Label ID="Label4" runat="server"></asp:Label>
                                        </td>
                                        <td class="auto-style35">Loan Interest Rate</td>
                                        <td class="auto-style25">:<asp:Label ID="Label5" runat="server"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style26"></td>
                                        <td class="auto-style27"></td>
                                        <td class="auto-style28"></td>
                                        <td class="auto-style29"></td>
                                        <td class="auto-style30"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style32">
                                        </td>
                                        <td class="auto-style33"><strong>Date</strong></td>
                                        <td class="auto-style34"><strong>Capital(Rs.)</strong></td>
                                        <td class="auto-style24"><strong>Interest(Rs.)</strong></td>
                                        <td class="auto-style13"><strong>Total(Rs.)</strong></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style32">Last Repayment </td>
                                        <td class="auto-style33">
                                            <asp:Label ID="Label12" runat="server"></asp:Label>
                                        </td>
                                        <td class="auto-style34">
                                            <asp:Label ID="Label13" runat="server"></asp:Label>
                                        </td>
                                        <td class="auto-style24">
                                            <asp:Label ID="Label14" runat="server"></asp:Label>
                                        </td>
                                        <td class="auto-style13">
                                            <asp:Label ID="Label15" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style32">Loan Outstanding </td>
                                        <td class="auto-style33">
                                            <asp:Label ID="Label6" runat="server"></asp:Label>
                                        </td>
                                        <td class="auto-style34">
                                            <asp:Label ID="Label9" runat="server"></asp:Label>
                                        </td>
                                        <td class="auto-style24">
                                            <asp:Label ID="Label10" runat="server"></asp:Label>
                                            &nbsp;</td>
                                        <td class="auto-style13">
                                            <asp:Label ID="Label11" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        
                    </div>
                    
                </div></div><%--</div>--%>

<button class="accordion" type="button"><span style="font-size:0.8em;"><strong> Premiums not credited to policy</span></strong></button>
<div class="panel">
<br />
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-10">
                        <asp:GridView ID="gvDeposits" runat="server" AutoGenerateColumns="False"
                            CssClass="footable"  >
                            <Columns>
                                <asp:BoundField DataField="PAY_DATE" HeaderStyle-Font-Bold="true"
                                    HeaderStyle-ForeColor="#000000" HeaderText="Paid Date" ItemStyle-Font-Bold="false" />
                                <asp:BoundField DataField="PAY_AMT" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="#000000" HeaderText="Paid amount (Rs.)"
                                    ItemStyle-Font-Bold="false" />                                
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-xs-1"></div>
                </div>

</div>

<button class="accordion" type="button"><span style="font-size:0.8em;"><strong>Premium Payment History</strong></span> </button>
<div class="panel">
                <br />
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-10">
                        <asp:GridView ID="gvDemands" runat="server" AutoGenerateColumns="False"
                            CssClass="footable"
                            Caption="<b>Premium Payment History (Upto 1 year) </b><br/> Online Transactions will take sometime to update.">
                            <Columns>
                                <asp:BoundField DataField="DUE_MN" HeaderStyle-Font-Bold="true"
                                    HeaderStyle-ForeColor="#000000" HeaderText="Due Month" ItemStyle-Font-Bold="false" />
                                <asp:BoundField DataField="PREMIUM" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="#000000" HeaderText="Premium (Rs.)"
                                    ItemStyle-Font-Bold="false" />
                                <asp:BoundField DataField="PAY_DATE" HeaderStyle-Font-Bold="true"
                                    HeaderStyle-ForeColor="#000000" HeaderText="Payment Date" ItemStyle-Font-Bold="false" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-xs-1"></div>
                </div>
</div>
<button class="accordion" type="button"><span style="font-size:0.8em;"><strong>Tax Certificate </strong></span></button>
<div class="panel">
<asp:HiddenField ID="hfAccordionIndex" runat="server" Value="N"/>
<asp:HiddenField ID="PaneName" runat="server" />
                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-10">&nbsp;</div>
                    <div class="col-xs-1"></div>
                    </div>

                <div class="row">
                    <div class="col-xs-1"></div>
                    <div class="col-xs-10">

                    <%--<div class="panel panel-default ">--%>
                    <%--<div class="panel-heading"><p style="font-weight:bold;">Get your tax certificate</p></div>--%>
                    <%--<div class="panel-body">--%>
                        <div class="row">
                        <div class="col-xs-4">Tax Year*</div>
                        <div class="col-xs-6"><asp:TextBox ID="txt_year" runat="server" class="form-control" MaxLength="4"></asp:TextBox></div>
                        <div class="col-xs-2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ErrorMessage="*" ControlToValidate="txt_year" Font-Bold="True" 
                                Font-Names="Calibri" Font-Size="10pt" ForeColor="Red"></asp:RequiredFieldValidator>
                            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                runat="server" ControlToValidate="txt_year" ErrorMessage="Invalid" 
                                Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" ForeColor="Red" 
                                ValidationExpression="[0-9]{4}"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <br />
                        <!--
                        <div class="row">
                        <div class="col-xs-4">Your Reference No*</div>
                        <div class="col-xs-6"><asp:TextBox ID="txt_refNo" runat="server" class="form-control" MaxLength="20"></asp:TextBox></div>
                        <div class="col-xs-2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txt_refNo" ErrorMessage="*" Font-Bold="True" 
                                Font-Names="Calibri" Font-Size="10pt" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div></div>
                        <br />
                        -->
                        <div class="row">
                        <div class="col-xs-4"></div>
                        <div class="col-xs-6">
                            <asp:Button ID="Button1" runat="server"  class="btn btn-primary btn-xs pull-left" Text="Submit" onclick="Button1_Click1" />
                        </div>
                        </div>
                        <br/>
                        <div class="row">
                            <div class="col-xs-12">
                                <asp:Label ID="msg" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                   <%-- </div>
                    </div>--%>

                    </div>
                    <div class="col-xs-1"></div>
                 </div>

                 
</div>

                <%--<button class="accordion" type="button"><span style="font-size:0.8em;"><strong>Maturity Claim Forms </strong></span></button>--%>
        <%--<button class="accordion" type="button"><span style="font-size:0.8em;"><strong>Maturity Claim Forms </strong></span></button>--%>
<div class="panel" >
    <asp:HiddenField ID="hfAccordionIndex1" runat="server" Value="N" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
<asp:HiddenField ID="HiddenField2" runat="server" />
    <asp:HiddenField ID="HiddenField3" runat="server" />
    <br />
    <%--<asp:Panel ID="Panel7" runat="server">
        <div class="auto-style8">
            <strong><span class="auto-style7">Instructions</span></strong>
            <br />
            1. Please print these forms after downloading them. You can also visit any of our branches with the documents listed below. (The branch can provide you with certify photocopies.)
            <br />
            2. Fill in the claimant&#39;s relevant data and return the forms, duly completed, along with the documents requested in the covering letter. (Signed, dated, and witnessed)
            <br />
            &nbsp;a. A certified photo copy of the National Identity Card.
            <br />
            &nbsp;b. A certified photocopy of the first page of the Bank Pass book, indicating the account number and name.
            <br />
            &nbsp;c. Original Policy Document.
            <br />
            3. You can send them directly to the address mentioned below by post or courier. Otherwise, you can give them to your insurance agent or any of our branches near your place.
            <br />
            “Senior Manager- Life,
            <br />
            Maturity and Stage Claims,
            <br />
            Sri Lanka Insurance Corporation,
            <br />
            No 21, Vauxhall Street,
            <br />
            Colombo 2,
            <br />
            Sri Lanka.&quot;
            <br />
            4. If you live overseas, please send them to the above-mentioned address via postal service.
            <br />
            5. Upload a scanned copy of the duly completed claim forms and documents required (as a one file in PDF format) for verification and the initial procedure.<br /> &nbsp; * Please note that we will not be able to release your maturity payment until we receive the original claim forms and documents. Hence, after you upload scanned documents, send us the originals once we notify you to do so.
            <br />
            &nbsp; *&nbsp; If you do not have the original policy book and schedule, please download the &quot;Loss Policy Affidavit&quot; or visit any of our branches or call us at the numbers listed on claim forms.<br /> </div>
    </asp:Panel>
    <%--<asp:Panel ID="Panel4" runat="server">
        <button class="accordion" type="button"><span style="font-size:0.8em;"><strong>Download Claim Forms </strong></span></button>
        <asp:Panel ID="Panel5" runat="server" Width="1317px">
            <table class="auto-style5">
                <tr>
                    <td class="auto-style16">
                        <asp:Label ID="Label1" runat="server" style="color: #000000" Text="Claim Forms"></asp:Label>
                    </td>
                    <td class="auto-style17">
                        <asp:Button ID="Button2" runat="server" Height="33px" Text="Sinhala" Width="77px" OnClick="Button2_Click" />
                    </td>
                    <td class="auto-style17">
                        <asp:Button ID="Button3" runat="server" Height="33px" Text="English" Width="77px" OnClick="Button3_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                   <td class="auto-style9">Loss policy Affidavit(Only if policy is lost)&nbsp;&nbsp;&nbsp; </td>
                    <td>
                     <asp:Button ID="Button12" runat="server" Height="33px" Text="Sinhala" Width="77px" OnClick="Button12_Click" />
                    </td>
                    <td>
                      <asp:Button ID="Button13" runat="server" Height="33px" Text="English" Width="77px" OnClick="Button13_Click" />
                    </td>
                </tr>
            </table>
        
        </asp:Panel>
        <button class="accordion" type="button"><span style="font-size:0.8em;"><strong>Upload Claim Forms </strong></span></button>
        <%--<asp:Panel ID="Panel6" runat="server" Height="286px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <br />
            <%--<table style="margin-left: 277px;" class="auto-style11">
                <tr>
                    <td class="auto-style3">Discharge Forms</td>
                    <td class="auto-style14">
                        <asp:CheckBox ID="CheckBox4" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td class="auto-style3">Important Notice</td>
                    <td class="auto-style14">
                        <asp:CheckBox ID="CheckBox9" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">7010 (Residence Form)</td>
                    <td class="auto-style14">
                        <asp:CheckBox ID="CheckBox10" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td class="auto-style3"><span>Copy of NIC</span></td>
                    <td class="auto-style14">
                        <asp:CheckBox ID="CheckBox5" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"><span>Copy of Bank Pass Book</span></td>
                    <td class="auto-style14">
                        <asp:CheckBox ID="CheckBox6" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"><span>Original Policy Document </span></td>
                    <td class="auto-style14">
                        <asp:CheckBox ID="CheckBox7" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"><span>Affidavit for Loss of policy</span></td>
                    <td class="auto-style14">
                        <asp:CheckBox ID="CheckBox8" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style13"></td>
                    <td class="auto-style15"></td>
                </tr>
                <tr>
                    <td class="text-sm-center" colspan="2" >
                        <br />
                       
                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server"> <ContentTemplate>--%>
                        
                        <%--<asp:FileUpload ID="FileUpload1" runat="server" CssClass="auto-style12" />--%>
                      
                       <%--<asp:Button ID="Button6" runat="server" Text="Upload" Width="112px" OnClick="Button6_Click" CausesValidation="False"  CssClass="accordion" />--%>
<%--</ContentTemplate>--%>
                          <%--  <Triggers>     
                           
                             <%--<asp:AsyncPostbackTrigger ControlID="lnkDownload" EventName="Click" />--%>
                          
                           <%-- <asp:PostBackTrigger ControlID="Button6" />
                                </Triggers>    
                     </asp:UpdatePanel>--%>
                        <br />
                    
                        <%--<asp:Literal ID="Literal1" runat="server"></asp:Literal>--%>
                        <br />

                     
                    </td>
                </tr>

                <tr>
                    <td class="text-sm-center" colspan="2">&nbsp;</td>
                </tr>

            </table>
           
            <asp:HiddenField ID="HiddenField4" runat="server" />
            <asp:HiddenField ID="HiddenField6" runat="server" />
            <asp:HiddenField ID="HiddenField5" runat="server" />
            <asp:HiddenField ID="HiddenField17" runat="server" />
            <asp:HiddenField ID="HiddenField15" runat="server" />
            <asp:HiddenField ID="HiddenField13" runat="server" />
            <asp:HiddenField ID="HiddenField11" runat="server" />
            <asp:HiddenField ID="HiddenField9" runat="server" />
            <asp:HiddenField ID="HiddenField20" runat="server" />
            <asp:HiddenField ID="HiddenField18" runat="server" />
            <asp:HiddenField ID="HiddenField16" runat="server" />
            <asp:HiddenField ID="HiddenField14" runat="server" />
            <asp:HiddenField ID="HiddenField12" runat="server" />
            <asp:HiddenField ID="HiddenField7" runat="server" />
            <asp:HiddenField ID="HiddenField21" runat="server" />
            <asp:HiddenField ID="HiddenField19" runat="server" />
            <asp:HiddenField ID="HiddenField8" runat="server" />
            <asp:HiddenField ID="HiddenField22" runat="server" />
            <asp:HiddenField ID="HiddenField25" runat="server" />
            <asp:HiddenField ID="HiddenField23" runat="server" />
            <asp:HiddenField ID="HiddenField24" runat="server" />
            <asp:HiddenField ID="HiddenField10" runat="server" />
            
            <asp:HiddenField ID="HiddenField26" runat="server" />
            <asp:HiddenField ID="HiddenField27" runat="server" />
            
     <%--   </asp:Panel
        </asp:Panel>--%>
    </div>
            </asp:Panel>

            

            <br />
            <br />
        </div>
        <br/>
    </div>

    <script>
        var acc = document.getElementsByClassName("accordion");
        var i;

        for (i = 0; i < acc.length; i++) {

            acc[i].onclick = function () {

                $(this).toggleClass("active").next().toggleClass("show");
//                this.classList.toggle("active");
//                alert(classList);
                var panel = this.nextElementSibling;
                

                if (panel.style.maxHeight) {
                    panel.style.maxHeight = null;
                }
                else {
                    panel.style.maxHeight = (panel.scrollHeight*2.25) + "px";

                }
            }
        }
        $(document).ready(function () {
            var hv = $('input[id$=hfAccordionIndex]').val();

            if (hv == "Y") {
                //acc[3].onclick();
            }

           var hv1 = $('input[id$=hfAccordionIndex1]').val();

            if (hv1 == "Y") {
                acc[2].onclick();
            }
           

        });

</script>

</asp:Content>

