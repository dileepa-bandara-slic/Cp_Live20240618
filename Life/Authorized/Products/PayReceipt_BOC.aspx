<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Life.master" CodeFile="PayReceipt_BOC.aspx.cs" Inherits="Life_Authorized_Products_PayReceipt_BOC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/js/jquery.min.js" type="text/javascript"></script>
    <script src="/js/bootstrap.min.js" type="text/javascript"></script>

    <script type = "text/ecmascript" language="javascript">
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

    <style>
        @media (max-width:479px)
        {
            .navbar-fixed-top + .main-container
            {
                padding-top: 50px;
            }
        }
        
         .ImageLoad
        {
            height:100%; width:100%;
        }
        .img-center {margin:0 auto;}
        
        .style1
        {
            font-size: xx-small;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-container" id="main-container">
        <div class="container">

         <!-- Modal -->
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
        <div class="row"><br /><br /></div>
        <br />
            <div class="row">
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
            </div>
            <div class="row">
                <asp:Panel ID="Panel1" runat="server" Visible="False" Style="border:thin solid; border-color: #000000;
                    margin-left: 5%; margin-right: 5%;">
                    <br />
                    <%-- <table class="style2" style="border: thin solid #000000">--%>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10" style="text-align: center">
                        <div class="row">
                         <div class="col-xs-4"></div>
                         <div class="col-xs-4" style="text-align: center">
                            <asp:Image ID="Image1" class="img-responsive img-center" runat="server" ImageUrl="~/images/logo.png"  style="text-align: center" />
                            </div>
                           <div class="col-xs-4"></div>
                        </div>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10" style="text-align: center">
                            Sri Lanka Insurance Corporation
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10" style="text-align: center">
                            <strong>Online Payment Receipt</strong></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Literal ID="litAddress" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Dear Customer,</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <%--    We have received your payment of Rs.
                            <asp:Literal ID="litAmount" runat="server"></asp:Literal>
                            &nbsp;under the reference no:&nbsp;
                            <asp:Literal ID="litPropNo" runat="server"></asp:Literal>
                            &nbsp;regarding premium payment for
                            <asp:Literal ID="litPolType" runat="server"></asp:Literal>
                            &nbsp;policy.--%>
                            We have received your payment of Rs.
                            <asp:Literal ID="litAmount" runat="server"></asp:Literal>
                            &nbsp;under the reference no:&nbsp;
                            <asp:Literal ID="litPremRefNo" runat="server"></asp:Literal>
                            &nbsp;regarding premium payment for&nbsp;Life policy number
                            <asp:Literal ID="litPolNo" runat="server"></asp:Literal>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Payment Details:</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Policy Number:&nbsp;
                            <asp:Literal ID="litPolNum" runat="server"></asp:Literal>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Customer Name:&nbsp;
                            <asp:Literal ID="litCustName" runat="server"></asp:Literal>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
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
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Total Due premium:&nbsp;
                            <asp:Literal ID="litTotDueAmt" runat="server"></asp:Literal>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Deposits:&nbsp;
                            <asp:Literal ID="litDeposits" runat="server"></asp:Literal>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Paid premium:&nbsp;
                            <asp:Literal ID="litPaidDuesAmt" runat="server"></asp:Literal>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Additional amount:&nbsp;
                            <asp:Literal ID="litAddtAmt" runat="server"></asp:Literal>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Date of Payment:&nbsp;
                            <asp:Literal ID="litPayDate" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Premium receipt will be posted to the above address in due course.</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Thanking you,<br />
                            Sri Lanka Insurance.</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                   
                <br />
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Button ID="btnReceipt" runat="server" Text="Download PDF Format" OnClick="btnReceipt_Click"
                                CssClass="btn btn-primary " /></div>
                        <div class="col-xs-1">
                        </div>
                    </div>

                <br />
                </asp:Panel>
                <br />
                <br/>
             
                <asp:Panel ID="Panel2" runat="server" Visible="False" Style="border:thin solid; border-color: #000000;
                    margin-left: 5%; margin-right: 5%;">
                    <br />
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10" style="text-align: center">
                        <div class="row">
                       <div class="col-xs-4"></div>
                        <div class="col-xs-4" style="text-align: center">
                            <asp:Image ID="Image2" class="img-responsive img-center" runat="server" ImageUrl="~/images/logo.png"  style="text-align: center"/>
                        </div>
                       <div class="col-xs-4"></div>
                        </div>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10" style="text-align: center">
                            Sri Lanka Insurance Corporation
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10" style="text-align: center">
                            <strong>Online Payment Receipt</strong></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Literal ID="litAddress2" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Dear Customer,</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        
                        <div class="col-xs-10">
                            We have received your payment of Rs.
                            <asp:Literal ID="litAmount2" runat="server"></asp:Literal>
                            &nbsp;under the reference no:&nbsp;
                            <asp:Literal ID="litLoanRefNo" runat="server"></asp:Literal>
                            &nbsp;regarding Loan payment for loan number
                            <asp:Literal ID="litLoanNo" runat="server"></asp:Literal>
                            &nbsp;under the policy number
                            <asp:Literal ID="litPolNo2" runat="server"></asp:Literal>
                        </div>                       
                                 
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Policy Details:</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Policy Number: &nbsp;
                            <asp:Literal ID="litRnPolNum" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <%if (motorDept)
                      { %>
                    <% } %>
                    <%--    <div class="row">
                       <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                           Sum Assured (Rs.): &nbsp; <asp:Literal ID="litRnSumAssurd" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                      </div>--%>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Customer Name: &nbsp;
                            <asp:Literal ID="litLoanCusNam" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <%-- <div class="row">
                       <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Premium (Rs.): &nbsp; <asp:Literal ID="litRnPremium" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                      </div>

                     <div class="row">
                       <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Cover period: &nbsp; <asp:Literal ID="litRnCovPeriod" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                      </div>--%>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Date of Payment: &nbsp;
                            <asp:Literal ID="litPayDate2" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <%--
                       <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>

                         <div class="row">
                       <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                              <asp:Label ID="lblNoClmMesg" runat="server" style="font-weight: 700" 
                    Text="Subject to there being no claim from the date of Renewal to the date of online payment." 
                    Visible="False"></asp:Label></div>
                        <div class="col-xs-1">
                        </div>
                      </div>--%>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Loan Payment Receipt will be posted to the above address in due course.</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Thanking you,<br />
                            Sri Lanka Insurance.</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                 
                <br />

                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Button ID="Button1" runat="server" Text="Download PDF Format" OnClick="Button1_Click"
                                CssClass="btn btn-primary " />
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>

                <br />
                </asp:Panel>
                <br />
                <br />

                <asp:Panel ID="Panel3" runat="server" Visible="False" Style="border:thin solid; border-color: #000000;
                    margin-left: 5%; margin-right: 5%;">
                    <br />
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10" style="text-align: center">
                        <div class="row">
                       <div class="col-xs-4"></div>
                        <div class="col-xs-4" style="text-align: center">
                            <asp:Image ID="Image3" class="img-responsive img-center" runat="server" ImageUrl="~/images/logo.png"  style="text-align: center"/>
                        </div>
                       <div class="col-xs-4"></div>
                        </div>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10" style="text-align: center">
                            Sri Lanka Insurance Corporation
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10" style="text-align: center">
                            <strong>Online Payment Receipt</strong></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Literal ID="ltrAdrsName_rev" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Literal ID="ltrAdress1_rev" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <%
                        if (!ltrAdress2_rev.Text.Equals(""))
                        {
                         %>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Literal ID="ltrAdress2_rev" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <%
                        }
                        if (!ltrAdress3_rev.Text.Equals(""))
                        {
                         %>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Literal ID="ltrAdress3_rev" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <%
                        }
                        if (!ltrAdress4_rev.Text.Equals(""))
                        { 
                             %>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Literal ID="ltrAdress4_rev" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <%
                        }
                         %>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Dear Valued Customer,</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        
                        <div class="col-xs-10">
                            <br />
                            We have received your payment of Rs.
                            <asp:Literal ID="ltrPayAmount_rev" runat="server"></asp:Literal>
                            &nbsp;under the reference no:&nbsp;
                            <asp:Literal ID="ltrRefNo_rev" runat="server"></asp:Literal>
                            &nbsp;for the policy revival as follows.                            
                        </div>                       
                                 
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Policy Details:<br /> <br /> </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Policy Number: &nbsp;
                            <asp:Literal ID="ltrPolNo_rev" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <%if (motorDept)
                      { %>
                    <% } %>
                    <%--    <div class="row">
                       <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                           Sum Assured (Rs.): &nbsp; <asp:Literal ID="litRnSumAssurd" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                      </div>--%>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Customer Name: &nbsp;
                            <asp:Literal ID="ltrCustomerName_rev" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Paid Amount: &nbsp;
                            <asp:Literal ID="ltrPaidAmount_rev" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <%-- <div class="row">
                       <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Premium (Rs.): &nbsp; <asp:Literal ID="litRnPremium" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                      </div>

                     <div class="row">
                       <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Cover period: &nbsp; <asp:Literal ID="litRnCovPeriod" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                      </div>--%>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Date of Payment: &nbsp;
                            <asp:Literal ID="ltrPayDate_rev" runat="server"></asp:Literal>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Literal ID="ltrPayTime_rev" runat="server"></asp:Literal>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <%--
                       <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>

                         <div class="row">
                       <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                              <asp:Label ID="lblNoClmMesg" runat="server" style="font-weight: 700" 
                    Text="Subject to there being no claim from the date of Renewal to the date of online payment." 
                    Visible="False"></asp:Label></div>
                        <div class="col-xs-1">
                        </div>
                      </div>--%>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                        <br />
                            Deposit receipt will be posted to the above address in due course.</div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <br />
                            <br />
                            Thanking you,<br />
                            <br />
                            Sri Lanka Insurance Corporation Ltd.</div>
                        <div class="col-xs-1">
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <br />
                            <br />
                            <span class="style1">This is a computer generated letter and no signature 
                            required.</span></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <%--<div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            &nbsp;</div>
                        <div class="col-xs-1">
                        </div>
                    </div>--%>
                 
                <br />

                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Button ID="btnPDF_rev" runat="server" Text="Download PDF Format"
                                CssClass="btn btn-primary " onclick="btnPDF_rev_Click" />
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>

                <br />

                     <asp:HiddenField ID="hdf_successIndicator" runat="server" />
    <asp:HiddenField ID="hdf_resultIndicator" runat="server" />
    <asp:HiddenField ID="hdf_orderIdVal" runat="server" />
    <asp:HiddenField ID="hdf_SessionID" runat="server" />  

                </asp:Panel>
            </div>
            </br>
        </div>
    </div>
</asp:Content>
