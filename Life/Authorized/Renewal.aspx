<%@ Page Title="" Language="C#" MasterPageFile="~/Life.master" AutoEventWireup="true"
    CodeFile="Renewal.aspx.cs" Inherits="Life_Authorized_Renewal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        @media (max-width:479px)
        {
            .navbar-fixed-top + .main-container
            {
                padding-top: 40px;
            }
        }
        
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
            padding-top: 20%;
        }
        
        
        .border-bottom
        {
            	border-bottom: 1px solid #DCDCDC;
        }
    </style>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container" id="main-container" style="min-height:600px">

       <%-- <div class="main-container" id="main-container">--%>
        <link href="/css/footable.min.css"
            rel="stylesheet" type="text/css" />
       <script src="/js/footable.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=gvOnlPaymnts]').footable();
                $('[id*=gvDemands]').footable();
            });

            $(document).ready(function () {
                function EndRequestHandler(sender, args) {

                    $('[id*=gvOnlPaymnts]').footable();
                    $('[id*=gvDemands]').footable();
                }

            });
        </script>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="container">
        <%--<br />--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/Life/Authorized/">Life</a></li>
                        <li class="breadcrumb-item"><a href="/Life/Authorized/ClientHome.aspx">Manage My Policies</a></li>
                        <li class="breadcrumb-item active">Premium Payments</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <asp:UpdatePanel ID="Ggs14451" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                        <center>
                            <h3>
                                Premium Payments</h3>
                                </center>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Label ID="lblErrMesg" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:GridView ID="gvOnlPaymnts" runat="server" AutoGenerateColumns="False" CssClass="footable">
                                <Columns>
                                    <asp:BoundField DataField="POL_NUM" HeaderText="Policy/ Loan No." HeaderStyle-Font-Bold="true"
                                        HeaderStyle-ForeColor="#000000" ItemStyle-Font-Bold="false" />
                                    <asp:BoundField DataField="PAY_AMT" HeaderText="Payment Amount (Rs.)" HeaderStyle-Font-Bold="true"
                                        HeaderStyle-ForeColor="#000000" ItemStyle-Font-Bold="false" />
                                    <asp:BoundField DataField="ENTRY_DATE" HeaderText="Date of Transaction" HeaderStyle-Font-Bold="true"
                                        HeaderStyle-ForeColor="#000000" ItemStyle-Font-Bold="false" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:RadioButtonList ID="rblPayType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblPayType_SelectedIndexChanged"
                                RepeatDirection="Horizontal" Width="250px">
                                <asp:ListItem Selected="True" Value="P">Pay Premium</asp:ListItem>
                                <asp:ListItem Value="L">Pay Loan</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="row">
                            <div class="col-xs-1">
                            </div>
                            <div class="col-xs-10">

                                <div class="form-group">

                                <div class="row border-bottom">
                                           
                                <div class="col-xs-4">
                                Policy Number
                                </div>
                                <div class="col-xs-8">
                                <asp:Literal ID="litPolNumber" runat="server"></asp:Literal>
                                </div>
                                </div>
                                    <br>
                                </br>

                                 <div class="row border-bottom">
                                <div class="col-xs-4">
                                Customer Name
                                </div>
                                <div class="col-xs-8">
                                <asp:Literal ID="litName" runat="server"></asp:Literal>
                                </div>
                                </div>
                                    <br>
                                 </br>

                                   <div class="row border-bottom">
                                <div class="col-xs-4">
                                Deposits Total
                                </div>
                                <div class="col-xs-8">
                                <asp:Literal ID="litDeposits" runat="server"></asp:Literal>
                                </div>
                                </div>
                                    <br>
                                 </br>

                                <div class="row">
                                <div class="col-xs-12">
                                </div>
                                </div>

                                    <div class="row">    
                                <div class="col-xs-12">

                                     <asp:GridView ID="gvDemands" runat="server" AutoGenerateColumns="False" CssClass="footable">
                                                <Columns>
                                                    <asp:BoundField DataField="DEMAND" HeaderText="Due Date" HeaderStyle-Font-Bold="true"
                                                        HeaderStyle-ForeColor="#000000" ItemStyle-Font-Bold="false" >
                                                    <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                                    <ItemStyle Font-Bold="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PREMIUM" HeaderText="Premium (Rs.)" HeaderStyle-Font-Bold="true"
                                                        HeaderStyle-ForeColor="#000000" ItemStyle-Font-Bold="false" >
                                                    <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                                    <ItemStyle Font-Bold="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LATEFEE" HeaderText="Late fee (Rs.)" HeaderStyle-Font-Bold="true"
                                                        HeaderStyle-ForeColor="#000000" ItemStyle-Font-Bold="false" >
                                                    <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                                    <ItemStyle Font-Bold="False" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>                                                        
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbPayDue" runat="server" AutoPostBack="true" OnCheckedChanged="cbPayDue_CheckedChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:CustomValidator ID="PayPremValidator" runat="server"
                                                        CssClass="errorMsg_1" ForeColor="Red">
                                            </asp:CustomValidator>
                                </div>
                            
                                </div>
                                    <br>
                                 </br>

                                <div class="row">
                                <div class="col-xs-12">
                                </div>
                                </div>

                                <div class="row border-bottom">
                                <div class="col-xs-4">
                                Total Dues Amount (Rs.)
                                </div>
                                <div class="col-xs-8">
                                <asp:Literal ID="litTotDueAmt" runat="server"></asp:Literal>
                                </div>
                                </div>
                                    <br>
                                 </br>

                                 <div class="row border-bottom">
                                <div class="col-xs-4">
                                Payable Dues Amount (Rs.)
                                </div>
                                <div class="col-xs-8">
                                <asp:Literal ID="litPayableDue" runat="server"></asp:Literal>
                                </div>
                                </div>
                                    <br>
                                 </br>

                           <%--           <div class="row border-bottom">
                           
                                     <div class="col-xs-12" style="color:green">
                                             Please note that a penalty will not be charged for late premium payments (due from 20th March to 30th June). Kindly proceed through the “Future Payments” option to pay life insurance premiums without a late fee.
                                    </div>
                                       <div class="col-xs-12">
                                        </div>
                                        <div class="col-xs-12" style="color:green">
                                            පාරිභෝගිකයින් ගේ පහසුව තකා, මාර්තු මස සිට ජුනි 30 දක්වා ගෙවීමට නියමිත ජීවිත රක්ෂණ වාරික සඳහා ප්‍රමාද ගාස්තුවක් අය නොකරයි. ඔබට නියමිත වාරික මුදල ගෙවීමට කරුණාකර &quot;Future Payments&quot; විකල්පය භාවිතා කරන්න.
                                        </div>
                               
                                </div> --%>

                                 <button type="button" class="btn btn-link btn-xs" data-toggle="collapse" data-target="#demo" style="padding:0; margin:0;">Future Payments <span class="glyphicon">&#xe114;</span></button>
                                 <div id="demo" class="collapse">
                                    <div class="row border-bottom">
                                <div class="col-xs-4">
                                Payments in Advance (Rs.)
                                </div>
                                <div class="col-xs-8">
                                   <asp:TextBox ID="txtAddtAmt" runat="server" class="form-control" placeholder="Optional" MaxLength="12"></asp:TextBox>
                                                    <asp:CustomValidator ID="addtAmtValidator" runat="server" ControlToValidate="txtAddtAmt"
                                                        CssClass="errorMsg_1" ForeColor="Red" OnServerValidate="checkAddtAmt"></asp:CustomValidator>
                                               
                                </div>
                                </div>
                                </div>
                                    <br>
                                 </br>

                                    <div class="row">
                                <div class="col-xs-4">
                                </div>
                                <div class="col-xs-8">
                                 <asp:Button ID="btnPayPrem" runat="server" CssClass="btn btn-primary btn-xs" 
                                                        OnClick="btnPayPrem_Click" Text="Submit" />        
                                </div>
                                </div>
                                    <br>
                                 </br>


                                    <%--<table class="table">
                                        <tbody>--%>
                                            <%--<tr>
                                                <td>
                                                    Policy Number
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litPolNumber" runat="server"></asp:Literal>
                                                </td>
                                            </tr>--%>
                                    <%--        <tr>
                                                <td>
                                                    Customer Name
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litName" runat="server"></asp:Literal>
                                                </td>
                                            </tr>  --%>
                                          <%--  <tr>
                                                <td>
                                                    Deposits Total
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litDeposits" runat="server"></asp:Literal>
                                                </td>
                                            </tr> --%>                                                                                  
                                    <%--        <tr>
                                            <td colspan="2">


                                            <asp:GridView ID="gvDemands" runat="server" AutoGenerateColumns="False" CssClass="footable">
                                                <Columns>
                                                    <asp:BoundField DataField="DEMAND" HeaderText="Due Date" HeaderStyle-Font-Bold="true"
                                                        HeaderStyle-ForeColor="#000000" ItemStyle-Font-Bold="false" >
                                                    <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                                    <ItemStyle Font-Bold="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PREMIUM" HeaderText="Premium (Rs.)" HeaderStyle-Font-Bold="true"
                                                        HeaderStyle-ForeColor="#000000" ItemStyle-Font-Bold="false" >
                                                    <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                                    <ItemStyle Font-Bold="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LATEFEE" HeaderText="Late fee (Rs.)" HeaderStyle-Font-Bold="true"
                                                        HeaderStyle-ForeColor="#000000" ItemStyle-Font-Bold="false" >
                                                    <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                                    <ItemStyle Font-Bold="False" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>                                                        
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbPayDue" runat="server" AutoPostBack="true" OnCheckedChanged="cbPayDue_CheckedChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:CustomValidator ID="PayPremValidator" runat="server"
                                                        CssClass="errorMsg_1" ForeColor="Red">
                                            </asp:CustomValidator>



                                            </td>
                                            </tr>--%>
                                         
                                        <%--  <tr>
                                                <td>
                                                    Total Dues Amount (Rs.)
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litTotDueAmt" runat="server"></asp:Literal>
                                                    
                                                </td>
                                            </tr>  --%>
                                         <%--   <tr>
                                                <td>
                                                    Payable Dues Amount (Rs.)
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litPayableDue" runat="server"></asp:Literal>
                                                    
                                                </td>
                                            </tr>  --%>                                      
                                        <%--    <tr>
                                                <td>
                                                    Future Payments (Rs.)
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAddtAmt" runat="server" class="form-control" placeholder="Optional" MaxLength="12"></asp:TextBox>
                                                    <asp:CustomValidator ID="addtAmtValidator" runat="server" ControlToValidate="txtAddtAmt"
                                                        CssClass="errorMsg_1" ForeColor="Red" OnServerValidate="checkAddtAmt"></asp:CustomValidator>
                                                </td>
                                            </tr>--%>
                                       <%--     <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnPayPrem" runat="server" CssClass="btn btn-primary btn-xs" 
                                                        OnClick="btnPayPrem_Click" Text="Submit" />
                                                </td>
                                            </tr>--%>
                                  <%--      </tbody>
                                    </table>--%>



                                </div>
                            </div>
                            <div class="col-xs-1">
                            </div>
                        </div>


                          <div class="row">
                            <div class="col-xs-1">
                            </div>
                            <div class="col-xs-10">

                                    <table class="table">
                                        <tbody>
                                           <%-- <tr>
                                                <td>
                                                    Total Dues Amount (Rs.)
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litTotDueAmt" runat="server"></asp:Literal>
                                                    
                                                </td>
                                            </tr>  
                                            <tr>
                                                <td>
                                                    Payable Dues Amount (Rs.)
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litPayableDue" runat="server"></asp:Literal>
                                                    
                                                </td>
                                            </tr>                                        
                                            <tr>
                                                <td>
                                                    Additional Payment (Rs.)
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAddtAmt" runat="server" class="form-control" MaxLength="12"></asp:TextBox>
                                                    <asp:CustomValidator ID="addtAmtValidator" runat="server" ControlToValidate="txtAddtAmt"
                                                        CssClass="errorMsg_1" ForeColor="Red" OnServerValidate="checkAddtAmt"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnPayPrem" runat="server" CssClass="btn btn-primary btn-xs" 
                                                        OnClick="btnPayPrem_Click" Text="Submit" />
                                                </td>
                                            </tr>--%>                                                                                   
                                       
                                          
                                        </tbody>
                                    </table>
                               

                            </div>
                            <div class="col-xs-1">
                            </div>
                        </div>

                    </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server">
                        <div class="row">
                            <div class="col-xs-1">
                            </div>
                            <div class="col-xs-10">
                                <div class="form-group">



                               <div class="table-responsive">
                                  <table class="table">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        Loan Number
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litLoanNumber" runat="server"></asp:Literal>
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
                                                        Customer Name
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litName2" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Granted Date
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litGrantDate" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Granted Amount (Rs.)
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litGrantAmt" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>

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
                                                        Capital
                                                    </th>
                                                    <th>
                                                        Interest
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        Next Due
                                                    </td>
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
                                                    <td>
                                                        Last Repayment
                                                    </td>
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
                                            <tr>
                                                <td>
                                                    Amount to be paid (Rs.)
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPayLoanAmt" runat="server" class="form-control" MaxLength="12"></asp:TextBox>
                                                    <asp:CustomValidator ID="PayLoanValidator" runat="server" ControlToValidate="txtPayLoanAmt"
                                                        CssClass="errorMsg_1" ForeColor="Red" OnServerValidate="checkPayLoanAmt"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                      <asp:Button ID="btnPayLoan" runat="server" class="btn btn-primary btn-sm" OnClick="btnPayLoan_Click"
                                                        Text="Submit" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="col-xs-1">
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="Ggs14451"
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
            </asp:UpdatePanel>
            <br />
            <br />
         
            <br />
        </div>
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
                    panel.style.maxHeight = (panel.scrollHeight * 2.25) + "px";

                }
            }
        }
        $(document).ready(function () {
            var hv = $('input[id$=hfAccordionIndex]').val();

            if (hv == "Y") {
                acc[2].onclick();
            }

        });

</script>
</asp:Content>
