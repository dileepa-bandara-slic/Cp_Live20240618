<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="BOC_PaymentForm.aspx.cs" Inherits="General_Authorized_Products_BOC_PaymentForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



<%--Test--%>
<%--<script src="https://test-bankofceylon.mtf.gateway.mastercard.com/checkout/version/40/checkout.js"--%>
<%--Live--%>
<script src="https://bankofceylon.gateway.mastercard.com/checkout/version/50/checkout.js"
               	 data-error="errorCallback"
               	 data-cancel="cancelCallback"
                 data-complete="PayReceipt_BOC.aspx">
                
       </script>


	
 <script type="text/javascript">
     function errorCallback(error) {
         alert(JSON.stringify(error));
     }

     function completeCallback(resultIndicator, sessionVersion) {
         alert("Result Indicator");
         alert(JSON.stringify(resultIndicator));

         alert("Session Version:");
         alert(JSON.stringify(sessionVersion));
         alert("Successful Payment");

     }

     function cancelCallback() {
         alert('Payment cancelled');

     }


     Checkout.configure({
         merchant: "<%=hdf_merchantId.Value%>",
         order: {
             amount: "<%=hdf_order_amount.Value%>",
             currency: "<%=hdf_order_currency.Value%>",
             description: 'Hosted Checkout Test Order - Return to Merchant - PHP/JavaScript/NVP',
             id: "<%=hdf_order_id.Value%>",
             reference: "<%=hdf_orderRef.Value%>",
             item: {
                 brand: 'Mastercard',
                 description: 'Hosted Checkout Test Item - Return to Merchant - PHP/JavaScript/NVP',
                 name: 'HostedCheckoutItem',
                 quantity: '1',
                 unitPrice: '1.00',
                 unitTaxAmount: '1.00'
             }
         },

         customer: {
             email: "<%=hdf_customerEmail.Value%>"
         },
         interaction: {
             merchant: {
                 name: 'Sri Lanka Insurance - Customer Portal',
                 address: {
                     line1: '300 Adelaide Street',
                     line2: 'Brisbane Queensland 4000'
                 },
                 //logo: 'https://www.albemarle-london.com/OnlineBooking/Albemarle/ShowPics/ALBAniv.png'
                       logo: 'https://www.srilankainsurance.net/images/slic_logo.png'
             },
             displayControl: {

                 billingAddress: 'HIDE',
                 orderSummary: 'HIDE'

             }
         },
         session: {
             id: "<%=hdf_sessionID.Value%>"

         }
     });
						
 </script>

    <div class="main-container" id="main-container" style="min-height:600px">

<%--Test--%>
<form id ="boc_IPG" action="https://test-bankofceylon.mtf.gateway.mastercard.com/api/nvp/40" method="post">

    <%--Live--%>
<form id ="Form1" action="https://bankofceylon.gateway.mastercard.com/api/nvp/50" method="post" style="min-height:600px">

<p style="text-align:center;"><a href="../index.html"><img src="https://c.ap1.content.force.com/servlet/servlet.ImageServer?id=01590000008h62j&oid=00D90000000sUDO" alt="Main Order Home Page" /></a></p>


<%--<input id='data' type='hidden' name='' value='apiOperation=CREATE_CHECKOUT_SESSION&order.id=OXsOkYJkY8&order.amount=2222&order.currency=LKR&order.reference=8HGPyoibXF&merchant=700163000034&apiPassword=d5eddfc9726780cca272b3cd91563567&apiUsername=Merchant.700163000034SESSION0002458553363M85222327K5' />
--%>
<%--<input id='Hidden1' type='text' name='merchantId' value="apiOperation=CREATE_CHECKOUT_SESSION&order.id=OXsOkYJkY8&order.amount=2222&order.currency=LKR&order.reference=8HGPyoibXF&merchant=700163000034&apiPassword=d5eddfc9726780cca272b3cd91563567&apiUsername=Merchant.700163000034SESSION0002458553363M85222327K5"/>--%>
<%--<input id='Hidden2' type='text' name='apiUsername' value='Merchant.700163000034'/>
<input id='Hidden3' type='text' name='password' value='d5eddfc9726780cca272b3cd91563567'/>
<input id='Hidden4' type='text' name='gatewayUrl' value='https://test-bankofceylon.mtf.gateway.mastercard.com/api/nvp'/>
<input id='Hidden5' type='text' name='order_id' value='000300'/>
<input id='Hidden6' type='text' name='order_amount' value='4600'/>
<input id='Hidden7' type='text' name='order_currency' value='LKR'/>
<input id='Hidden8' type='text' name='order_reference' value='00000'/>--%>

        <br><br><br><br>
        <%--<h1 align="center"> Hosted Checkout - Return To Merchant - PHP/JavaScript/NVP</h1>--%>
        <h2 align="center"> <u>Order Summary</u></h2>
        <p style="text-align:center;"> <strong> Order Amount :<%=hdf_order_amount.Value%></p>
        <p style="text-align:center;"> Currency&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:<%=hdf_order_currency.Value%></strong> </p>
        <%--<p style="text-align:center;"> <strong> Session ID :"<%=hdf_sessionID.Value%>" </strong></p>   --%>
        <%-- <p style="text-align:center;"> <strong> Merchant ID :"<%=hdf_merchantId.Value%>"--%>
    </strong></p>
        <br>

        <!-- Note in reality only one of the following functions will be called -->
       <%-- <p style="text-align:center;"><input type="button" value="Pay with Lightbox" onclick="Checkout.showLightbox();" /></p>--%>
        <p style="text-align:center;"><input type="button" value="Pay" 
                onclick="Checkout.showPaymentPage();" <%--style="width: 58px; height: 33px"--%> class="btn btn-primary" /></p>
				
       <%-- <p style="text-align:center;"><a href= "Payment.aspx"><br><br><input type="button" value="Cancel Payment and Return to Previous Page" /></a></p>--%>

        <asp:Label ID="lbl_result" runat="server" ></asp:Label>

<%--        <asp:Label ID="Label1" runat="server" ></asp:Label>--%>

         <%--<asp:Label ID="Label2" runat="server" ></asp:Label>--%>


    <asp:HiddenField ID="hdf_sessionID" runat="server" />
    <asp:HiddenField ID="hdf_orderID" runat="server" />
     <asp:HiddenField ID="hdf_orderRef" runat="server" />
     <asp:HiddenField ID="hdf_apiUserName" runat="server" />
      <asp:HiddenField ID="hdf_apiPassword" runat="server" />
      <asp:HiddenField ID="hdf_merchantId" runat="server" />
      <asp:HiddenField ID="hdf_order_id" runat="server" />
      <asp:HiddenField ID="hdf_order_amount" runat="server" />
      <asp:HiddenField ID="hdf_order_currency" runat="server" />
      <asp:HiddenField ID="hdf_customerEmail" runat="server" />
      <asp:HiddenField ID="hdf_SuccessIndicator" runat="server" />
      
      </form>
        </div>

</asp:Content>
