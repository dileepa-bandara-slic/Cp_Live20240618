<%@ Page Title="" Language="C#" MasterPageFile="~/Life.master" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="Life_Authorized_Products_Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="/css/fieldset.css" rel="stylesheet" />
    <script type = "text/ecmascript" language="javascript">
          //disabling back, needs more testing
          history.pushState(null, null, document.URL);
          window.addEventListener('popstate', function () {
             $('#myModal').modal('show');
              history.pushState(null, null, document.URL);
          });

         function pageLoad() {
            getScreenSize();
        }

        function getScreenSize() {
            var scr_height = screen.height;
            var scr_width = screen.width;
            document.getElementById('<%= hid_height.ClientID %>').value = scr_height;
            document.getElementById('<%= hid_width.ClientID %>').value = scr_width;
        }


          $(document).ready(function () {
              $('#myModal').modal('hide');
                    });

                    </script>

<style>

        .spaced input[type="radio"]
        {
            margin-right: 50px; 
        }

        @media (max-width:479px) {
            .navbar-fixed-top + .main-container {
                padding-top: 50px;
            }
        }
        .auto-style1 {
            left: 0px;
            top: 5px;
        }

        
        .modal-backdrop {
            
            z-index: 0;
        }

        
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

    select.form-control:not([size]):not([multiple]) {

    height: auto;
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
        
 .rbl
{
   margin-left: 10px;
   margin-right: 10px;
}
      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link href="/css/modal.css" rel="stylesheet" />
    <asp:HiddenField ID="hid_height" runat="server" />
    <asp:HiddenField ID="hid_width" runat="server" />

  <div class="main-container" id="main-container" style="min-height:600px">
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


                    <br /><br />
    


                <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                             <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
                         <asp:UpdatePanel ID="pnlTasks" runat="server" UpdateMode="Conditional" RenderMode="Inline">
<ContentTemplate>
                             <div style="align:center">
                     <fieldset>
                          <legend>   Premium / Loan Payment
                           </legend>

                     <div class="row">
                        <label for="inputEmail3" class="col-sm-3 control-label">
                           Policy Number
                        </label>
                        <div class="col-sm-9">
                        <asp:Literal ID="Lit_PolNo" runat="server"></asp:Literal>
                        </div>
                        
                    </div>
                         <br/>
                    <div class="row">
                        <label for="inputPayType" class="col-sm-3 control-label">
                           Payment Type
                        </label>
                        <div class="col-sm-9">
                       <asp:Literal ID="Lit_PayType" runat="server"></asp:Literal>
                        </div>
                        
                    </div>
                        <br/>
                    <div class="row">
                        <label for="inputCusName" class="col-sm-3 control-label">
                           Customer Name
                        </label>
                        <div class="col-sm-9">
                       <asp:Literal ID="Lit_CusName" runat="server"></asp:Literal>
                        </div>
                        
                    </div>
                           <br/>
                             <div class="row">
                        <label for="inputRefNo" class="col-sm-3 control-label">
                           Reference Number
                        </label>
                        <div class="col-sm-9">
                        <asp:Literal ID="Lit_RefNo" runat="server"></asp:Literal>
                        </div>
                        
                    </div>
                            <br/>
                                  <div class="row">
                        <label for="inputAmnt" class="col-sm-3 control-label">
                           Amount (Rs.)
                        </label>
                        <div class="col-sm-9">
                       <asp:Literal ID="Lit_Premium" runat="server"></asp:Literal>
                        </div>
                        
                    </div>
                            <br/>
       
                         <div class="row">
                        <label for="inputPayMethod" class="col-sm-3 control-label">
                           Payment Method
                        </label>
                        <div class="col-sm-6">
                         <div class="wrapper">
                        <asp:RadioButtonList ID="rdbPayMethod" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">Visa <img src="/images/Visa_70599.png" class="img-responsive img-thumbnail"/></asp:ListItem>
                                                    <asp:ListItem Value="1">Master <img src="/images/Master_70593.png" class="img-responsive img-thumbnail"/></asp:ListItem>
                                                    <asp:ListItem Value="2">Amex <img src="/images/Amex_70583.png" class="img-responsive img-thumbnail"/></asp:ListItem>
                                                </asp:RadioButtonList>
                                                </div>
                        </div>
                                           <div class="col-sm-3">
                                               </div>
                        
                    </div>
                          

                            <br/>
                         <% if (rdbPayMethod.SelectedValue.Equals("0") || rdbPayMethod.SelectedValue.Equals("1"))
                             {%>
                         <div class="row">
                                  <div class="col-sm-3">
                                      </div>
                                <div class="col-sm-9">
                                                <asp:Button ID="Button1"  runat="server" Text="Pay" class="btn btn-primary" 
                    onclick="Button1_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...';"/>
                                             <asp:Button ID="btn_PayWithBOC" runat="server" Text="Pay" Visible="false"
                                                    class="btn btn-primary" onclick="btn_PayWithBOC_Click" UseSubmitBehavior="false"
            OnClientClick="this.disabled='true'; this.value='Please wait...';" /> 
                    
                                    </div>
                                 </div>
                          <% } %>
                          <% if (rdbPayMethod.SelectedValue.Equals("2"))
                              {%>
                                 <div class="row">
                                 
                                  <div class="col-sm-3">
                                      </div>
                                <div class="col-sm-9">
                                    <asp:Button ID="btn_Amex" runat="server" Text="Pay" 
                                                    CssClass="btn btn-primary" onclick="btn_Amex_Click" UseSubmitBehavior="false"
            OnClientClick="this.disabled='true'; this.value='Please wait...';" style="left: 2px; top: 5px"/>
                                </div>
                                    
                                </div>
                         <% } %>
                 </fieldset>
                                 </div>


                            </ContentTemplate>
<Triggers>
<asp:PostBackTrigger ControlID="btn_Amex" />
    <asp:PostBackTrigger ControlID="Button1" />
    
    <asp:PostBackTrigger ControlID="btn_PayWithBOC" />

</Triggers>
</asp:UpdatePanel>
                     

                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <br />

    
    <asp:HiddenField ID="hidVersion" runat="server" />
    <asp:HiddenField ID="hidMerchant" runat="server"/>
    <asp:HiddenField ID="hidAcquiredId" runat="server"/>
    <asp:HiddenField ID="hidMerRespUrl" runat="server"/>
    <asp:HiddenField ID="hidPurcAmount" runat="server"/>
    <asp:HiddenField ID="hidPurchaseCurrency" runat="server"/>
    <asp:HiddenField ID="hidPurchaseCurrencyExponent" runat="server"/>
    <asp:HiddenField ID="hidGOrderId" runat="server"/>
    <asp:HiddenField ID="hidSignatureMethod" runat="server"/>
    <asp:HiddenField ID="hidSignature" runat="server"/>
    <asp:HiddenField ID="hidCaptureFlag" runat="server"/>
    <asp:HiddenField ID="hidHostUrl" runat="server"/>

    <%-- BOC Payment HiddenFields--%>
    <asp:HiddenField ID="hdf_Cust_Email_Address" runat="server"/>


        </div>
    </div>
</asp:Content>

