﻿<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="PayReceipt_BOC.aspx.cs" Inherits="General_Authorized_Products_PayReceipt_BOC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<%--    <script src="../assets/js/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>--%>

        <script src="/assets/js/jquery-3.5.1.min.js"></script>
    <script src="/assets/js/bootstrap.min.js"></script>

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
            max-heigh: 100%;
        }
        
        .img-center {margin:0 auto;}
    </style>

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
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="main-container" id="main-container">
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
                <asp:Panel ID="Panel1" runat="server" Visible="False" style="border:thin solid; border-color: #000000; margin-left: 5%; margin-right: 5%;">
                    <%-- <table class="style2" style="border: thin solid #000000">--%>
                    <br />
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
                            <strong>Online Payment Confirmation</strong></div>
                             <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                     <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Literal ID="litAddress" runat="server" Visible="False"></asp:Literal><br />&nbsp;</div>
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
                            Your payment of Rs.
                            <asp:Literal ID="litAmount" runat="server"></asp:Literal>
                            &nbsp;under the reference no:&nbsp;
                            <asp:Literal ID="litPropNo" runat="server"></asp:Literal>
                            &nbsp;regarding premium payment for
                            <asp:Literal ID="litPolType" runat="server"></asp:Literal>
                            &nbsp;policy has been received.</div>
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
                            Policy Number:&nbsp;
                            <asp:Literal ID="litPolNumber" runat="server"></asp:Literal>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Policy Type:&nbsp;
                            <asp:Literal ID="litPolType2" runat="server"></asp:Literal>
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                         <%if (globTrotter)
                            {%>
                            Sum Assured (USD):&nbsp;
                            <%}
                          else
                            { %>
                                Sum Assured (Rs.):&nbsp;
                            <%} %>
                            <asp:Literal ID="litSumAssurd" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Customer Name:&nbsp;
                            <asp:Literal ID="litCustName" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">                       
                         Premium (Rs.):&nbsp;                            
                            <asp:Literal ID="litPremium" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Cover period:&nbsp;
                            <asp:Literal ID="litCovPeriod" runat="server"></asp:Literal></div>
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
                            Date of Payment:&nbsp;<asp:Literal ID="litPayDate" runat="server"></asp:Literal></div>
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
                        <ul>
                            <%if (globTrotter)
                            {%>
                            
                            <li>Your payment receipt will be posted to the registered address in due course.</li>
                            <li>If you are required with a physical policy document, please <a href="/ContactUs.aspx"><span style="font-weight:bold; color:#8C8C8C;">contact us.</span></a></li>
                             <%}                    
                                                       
                            if (amppolicy)
                            {%>
                            <li>Healthplus card will be sent by post to the given address within 15 working days.</li>
                            <%} %>
                            
                            <li>The Policy is valid only if the bank transfer is successful.</li>
                            
                            </ul>
                            </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                         <%if (globTrotter)
                            {%>
                            This policy document will be added to the &#8220;Pending policies&#8221; section in <a href="/General/Authorized/ClientHome.aspx" >General Manage Policies</a> page.<br />

                           <%}
                          else
                            { %>
                            This proposal document will be added to the &#8220;Pending policies&#8221; section in <a href="/General/Authorized/ClientHome.aspx" >General Manage Policies</a> page.<br />
                            

                             <%} %>
                             Once it is underwritten, it will be moved to the &#8220;Non-Motor policies&#8221; section in the same page.
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <br />
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
                            <asp:Button ID="btnReceipt" runat="server" Text="Download PDF Format" 
                                CssClass="btn btn-primary btn-xs" onclick="btnReceipt_Click" />&nbsp;&nbsp;
                                
                                 <asp:Button ID="btnPolDoc" runat="server" Text="Download PDF Format" 
                                CssClass="btn btn-primary btn-xs" onclick="btnPolDoc_Click" />&nbsp;&nbsp;

                                 <asp:HyperLink ID="hyprPolSch" Target="_blank" runat="server" Text="Download" 
                                CssClass="btn btn-primary btn-xs" />&nbsp;&nbsp;
                                
                                </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                   <br />

                </asp:Panel>

                 <br />
                 <br />
                 
                <asp:Panel ID="Panel2" runat="server" Visible="False" style="border:thin solid; border-color: #000000; margin-left: 5%; margin-right: 5%;">
                    <br />
                  
                    
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
                            <strong>Online Payment Confirmation</strong></div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                   
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Literal ID="litAddress2" runat="server" Visible="False"></asp:Literal><br />&nbsp; </div>
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
                            Your payment of Rs.
                            <asp:Literal ID="litAmount2" runat="server"></asp:Literal>
                            &nbsp;under the reference no:&nbsp;
                            <asp:Literal ID="litRefNo" runat="server"></asp:Literal>
                            &nbsp;regarding renewal payment for
                            <asp:Literal ID="litPolTyp2" runat="server"></asp:Literal>
                            &nbsp;policy has been received.</div>
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
                           Policy Number: &nbsp; <asp:Literal ID="litRnPolNum" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>

                     <%if (motorDept)
          { %>
           <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                           Vehicle Number: &nbsp; <asp:Literal ID="litVehiNum" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                    </div>

                      <% } %>

                      <div class="row">
                       <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                           Sum Assured (Rs.): &nbsp; <asp:Literal ID="litRnSumAssurd" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                      </div>

                      <div class="row">
                       <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            Customer Name: &nbsp; <asp:Literal ID="litRnCusNam" runat="server"></asp:Literal></div>
                        <div class="col-xs-1">
                        </div>
                      </div>

                      <div class="row">
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
                            Date of Payment: &nbsp; <asp:Literal ID="litPayDate2" runat="server"></asp:Literal></div>
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
                              <asp:Label ID="lblNoClmMesg" runat="server" style="font-weight: 700" 
                    Text="Please note that if any claim is made prior to the renewal date of the policy, there will be a revision in the premium paid." 
                    Visible="False"></asp:Label></div>
                        <div class="col-xs-1">
                        </div>
                      </div>

                         <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                             <ul>
                             <li>Renewal receipt will be posted to the address in the policy document in due course.</li>
                             <li>Renewal is valid only if the bank transfer is successful</li>
                             
                             </div>

                        <div class="col-xs-1">
                        </div>
                    </div>

                     <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                             Thanking you,<br /> Sri Lanka Insurance.</div>
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
                            <asp:Button ID="Button1" runat="server" Text="Download PDF Format" 
                     CssClass="btn btn-primary" onclick="Button1_Click" />&nbsp;
                            <asp:Button ID="Button2" runat="server" 
                                Text="Download your covernote" CssClass="btn btn-primary" Visible="False" 
                                onclick="Button2_Click"/>
                            &nbsp;<asp:HiddenField ID="HdnCoNum" runat="server" />
                         </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                     <br />
                     <br />
                          <asp:HiddenField ID="hdf_successIndicator" runat="server" />
    <asp:HiddenField ID="hdf_resultIndicator" runat="server" />
    <asp:HiddenField ID="hdf_orderIdVal" runat="server" />
    <asp:HiddenField ID="hdf_SessionID" runat="server" />  
                </asp:Panel>

                 <br />
                 <br />
                   
            </div>
            </br>
        </div>
    </div>
</asp:Content>

