<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="HPL_Payment.aspx.cs" Inherits="General_Authorized_Products_HPL_Payment" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">
.checkboxList {
            margin-top: 3px;
            margin-left: 0px;
        }
              .checkboxList label {
                  margin-left: 5px;
                  font-weight: 200;
              }

        .Table_desc {
            border: 1px solid black;
            border-collapse: collapse;
            text-align: left;
            padding: 7px 5px 7px 5px;
            text-align: justify; 
            text-justify: inter-word;
        }
        
        .Table_tdplan{
            text-align: center;
        }
        
</style>
  <script type="text/javascript">

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
          getScreenSize()
          Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
          function EndRequestHandler(sender, args) {
          }
      });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    <asp:HiddenField ID="hdf_busi_type" runat="server" />
    <asp:HiddenField ID="hdf_polNo" runat="server" />
    <%--Visa Master Upgrade 2024--%>
    <asp:HiddenField ID="hid_height" runat="server" />
    <asp:HiddenField ID="hid_width" runat="server" />

 <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" >
    </asp:ScriptManager>
   
       <asp:HiddenField ID="HID_ChPlRefNo" runat="server" />
    <div class="row">
        <div class="col-xs-10">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="/">Home</a></li>
                <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                <li class="breadcrumb-item"><a href="/General/Authorized/General_Products.aspx">Purchase
                    online</a></li>
                <li class="breadcrumb-item"><a href="/General/Authorized/Products/HPL_2023_Purchase.aspx">
                    Home protect lite - Purchase</a></li>
                <li class="breadcrumb-item active">Home protect lite Payment</li>
            </ol>
        </div>
    </div>

    <div class="row" id="payment_container">

        <div class="col-lg-6 offset-lg-3">

            <div class="row">
                <div class="form-group" style=" text-align:center">
                    <h3>
                        <p>
                            <b>Purchased Policy </b>
                        </p>
                        <h3>
                        </h3>
                    </h3>
                </div>
            </div>


            <div class="form-group row" style="padding-left:20px">
                <label for="customerName" class="col-sm-3 col-form-label">Customer Name</label>
                <div class="col-sm-9">
                 <asp:Literal ID="LtxtCustomer" runat="server"></asp:Literal>
                </div>
       
            </div>

            <div class="form-group row" style="padding-left:20px">
                <label for="customerRefNo" class="col-sm-3 col-form-label">Reference Number</label>
                <div class="col-sm-9">
                    <asp:Literal ID="Ltxt_payRefNo" runat="server"></asp:Literal>
                </div>
            </div>

            <div class="form-group row" style="padding-left:20px">
                <label for="customerCommenceDate" class="col-sm-3 col-form-label">Commencement Date</label>
                <div class="col-sm-9">
                    <asp:Literal ID="Ltxt_pay_CommencementDate" runat="server"></asp:Literal>
                </div>
            </div>

             <div class="form-group row" style="padding-left:20px">
                <label for="sumAssured" class="col-sm-3 col-form-label">Sum Assured (Rs.) </label>
                <div class="col-sm-9">
                <asp:Literal ID="Ltxt_sumAssured" runat="server"></asp:Literal>
                </div>
            </div>

            <div class="form-group row" style="padding-left:20px">
                <label for="Premium" class="col-sm-3 col-form-label">Premium (Rs.)</label>
                <div class="col-sm-9">
                    <asp:Literal ID="Ltxt_payPremium" runat="server"></asp:Literal>
                </div>
            </div>

            <div class="form-group row" style="padding-left:27px">
                <asp:RadioButtonList ID="rdbPayMethod" runat="server" RepeatDirection="Horizontal"
                    CssClass="checkboxList Table_tdplan" class="form-control Table_tdplan" RepeatLayout="Table"
                    Width="70%">
                    <asp:ListItem Value="0">Visa <img src="/images/Visa_70599.png" class="img-responsive img-thumbnail"/></asp:ListItem>
                    <asp:ListItem Value="1">Master <img src="/images/Master_70593.png"  class="img-responsive img-thumbnail"/></asp:ListItem>
                    <asp:ListItem Value="2">Amex <img src="/images/Amex_70583.png" class="img-responsive img-thumbnail"/></asp:ListItem>
                </asp:RadioButtonList>
            </div>

             <div style="text-align:center; margin-top: 35px;">
                <asp:Button ID="btnPay" runat="server" Text="Pay" class="btn btn-success btn-sm" 
                     OnClientClick="Message()" onclick="btnPay_Click" />
                <asp:Button ID="btnCancel_Payment" runat="server" class="btn btn-danger btn-sm" Text="Cancel" />
            </div>


            <div class="row " style="padding:7px" id="_message" runat="server" >               
            </div>
        </div>
    </div>

</asp:Content>

