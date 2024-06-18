<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="HPL_PurchaseConf.aspx.cs" Inherits="General_Authorized_Products_HPL_PurchaseConf" %>
<%@ PreviousPageType VirtualPath="~/General/Authorized/Products/HPL_Purchase.aspx" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/modal.css" rel="stylesheet" />
<%--    <script src="/js/jquery-3.5.1.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>--%>
    <style>
        @media (max-width:479px) {
            .navbar-fixed-top + .main-container {
                padding-top: 50px;
            }
        }
    </style>

         <style>
     .modal-backdrop {
                 z-index: 0;
             }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <div class="main-container" id="main-container"  style="min-height:600px">
        <div class="container">
      <%--   <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>--%>
            <%-- <br />--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/General_Products.aspx">Purchase
                            online</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/Products/HPL_Purchase.aspx">
                            Home protect lite - Purchase</a></li>
                        <li class="breadcrumb-item active">Home protect lite purchase - Confirm</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
         <%--   <asp:UpdatePanel ID="Ggs14451" runat="server">
            <ContentTemplate>--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                <center>
                    <h3>
                        Home Protect Lite Purchase Confirm</h3>
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
                    <div class="form-group">
                        <table class="table">
                            <tbody>
                                <tr>
                                   <%-- <td>
                                        <b>Name in Full</b>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Lit_cus_title" runat="server"></asp:Literal>
                                        &nbsp;<asp:Literal ID="Lit_full_name" runat="server"></asp:Literal>
                                    </td>--%>

                                            <div class="row">
                                                <div class="col-sm-4">
                                                 <b>Name in Full</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                           <asp:Literal ID="Lit_cus_title" runat="server"></asp:Literal>
                                        &nbsp;<asp:Literal ID="Lit_full_name" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                </tr>
                                 <br />  
                                <tr>
                                <%--    <td>
                                        <b>NIC</b>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Lit_cus_nic" runat="server"></asp:Literal>
                                    </td>--%>
                                     <div class="row">
                                                <div class="col-sm-4">
                                                 <b>NIC</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                         <asp:Literal ID="Lit_cus_nic" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                </tr>
                                 <br />  
                                <tr>
                                   <%-- <td>
                                        <b>Address</b>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Lit_address1" runat="server"></asp:Literal>
                                        &nbsp;<asp:Literal ID="Lit_address2" runat="server"></asp:Literal>
                                        &nbsp;<asp:Literal ID="Lit_address3" runat="server"></asp:Literal>
                                        &nbsp;<asp:Literal ID="Lit_address4" runat="server"></asp:Literal>
                                    </td>--%>
                                       <div class="row">
                                                <div class="col-sm-4">
                                                 <b>Address</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                         <asp:Literal ID="Lit_address1" runat="server"></asp:Literal>
                                        &nbsp;<asp:Literal ID="Lit_address2" runat="server"></asp:Literal>
                                        &nbsp;<asp:Literal ID="Lit_address3" runat="server"></asp:Literal>
                                        &nbsp;<asp:Literal ID="Lit_address4" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                </tr>
                                <br /> 
                                <tr>
                                   <%-- <td>
                                        <b>Residential Phone</b>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Lit_homePhone" runat="server"></asp:Literal>
                                    </td>--%>

                                     <div class="row">
                                                <div class="col-sm-4">
                                                 <b>Residential Phone</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                           <asp:Literal ID="Lit_homePhone" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                </tr>
                                <br /> 
                                <tr>
                                    <%--<td>
                                        <b>Mobile Phone</b>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Lit_mobilePhone" runat="server"></asp:Literal>
                                    </td>--%>

                                     <div class="row">
                                                <div class="col-sm-4">
                                               <b>Mobile Phone</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                            <asp:Literal ID="Lit_mobilePhone" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                </tr>
                                <br /> 
                                <tr>
                                    <%--<td>
                                        <b>Office Phone</b>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Lit_officePhone" runat="server"></asp:Literal>
                                    </td>--%>

                                      <div class="row">
                                                <div class="col-sm-4">
                                               <b>Office Phone</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                             <asp:Literal ID="Lit_officePhone" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                </tr>
                                <br /> 
                                <tr>
                                    <%--<td>
                                        <b>Email</b>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Lit_email" runat="server"></asp:Literal>
                                    </td>--%>
                                      <div class="row">
                                                <div class="col-sm-4">
                                               <b>Email</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                              <asp:Literal ID="Lit_email" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                </tr>

                                 
                                 <br /> 
                                 <tr>
                                   <%-- <td>
                                        <b>Profession</b>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Lit_profession" runat="server"></asp:Literal>
                                    </td>--%>

                                       <div class="row">
                                                <div class="col-sm-4">
                                               <b>Profession</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                               <asp:Literal ID="Lit_profession" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                </tr>

                                <br /> 

                                 <tr>
                                    <%--<td>
                                        <b>Risk Location</b>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Lit_riskLocAd1" runat="server"></asp:Literal>
                                    </td>--%>

                                                   <div class="row">
                                                <div class="col-sm-4">
                                               <b>Risk Location</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                               <asp:Literal ID="Lit_riskLocAd1" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                </tr>
                                <br /> 
                                 <tr>
                                 <%--   <td>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Lit_riskLocAd2" runat="server"></asp:Literal>
                                    </td>--%>
                                       <div class="row">
                                                <div class="col-sm-4">
                                               
                                                </div>
                                                
                                                <div class="col-sm-6">
                                               <asp:Literal ID="Lit_riskLocAd2" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                </tr>
                                <br /> 
                                <tr>
                                  <%--  <td>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Lit_riskLocAd3" runat="server"></asp:Literal>
                                    </td>--%>

                                        <div class="row">
                                                <div class="col-sm-4">
                                               
                                                </div>
                                                
                                                <div class="col-sm-6">
                                               <asp:Literal ID="Lit_riskLocAd3" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                </tr>
                                <br /> 
                                <tr>
                                   <%-- <td>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Lit_riskLocAd4" runat="server"></asp:Literal>
                                    </td>--%>

                                    <div class="row">
                                                <div class="col-sm-4">
                                               
                                                </div>
                                                
                                                <div class="col-sm-6">
                                               <asp:Literal ID="Lit_riskLocAd4" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                </tr>

                              <br /> 

                                <tr>
                               <%-- <td><b>Assignee</b></td>
                                <td><asp:Literal ID="Lit_asgnee" runat="server"></asp:Literal></td>--%>

                                 <div class="row">
                                                <div class="col-sm-4">
                                               <b>Assignee</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                              <asp:Literal ID="Lit_asgnee" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                </tr>

                               <br /> 

                                <tr>
                                   <%-- <td>  <b>Have you ever sustained loss from any of the perils to which the insurance is 
                apply?</b>
                                    </td>
                                    <td>
                                    <asp:Literal ID="Lit_losses" runat="server"></asp:Literal>
                                    <br />
                                    <asp:Literal ID="Lit_remarksLos" runat="server" Text="Reason : " 
                    Visible="False"></asp:Literal>
                <asp:Literal ID="Lit_rejReasonLos" runat="server"></asp:Literal>
                                    </td>--%>
                                         <div class="row">
                                                <div class="col-sm-4">
                                              <b>Have you ever sustained loss from any of the perils to which the insurance is 
                apply?</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                              <asp:Literal ID="Lit_losses" runat="server"></asp:Literal>
                                              <br />
                                              <asp:Literal ID="Lit_remarksLos" runat="server" Text="Reason : " 
                    Visible="False"></asp:Literal>
                <asp:Literal ID="Lit_rejReasonLos" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                </tr>
                                <br /> 
                             <%--   <tr>
                                <td></td>
                                <td></td>
                                </tr>--%>

                                <tr>
                              <%--  <td> <b>Has any insurance company declined to insure or renew policy or demand an 
                increased rate for renewal ?
                </b></td>
                                <td><asp:Literal ID="Lit_rejects" runat="server"></asp:Literal>
                                <br />
                                 <asp:Literal ID="Lit_remarks" runat="server" Text="Rejection Reason : " 
                    Visible="False"></asp:Literal>
                <asp:Literal ID="Lit_rejReason" runat="server"></asp:Literal>
                                
                                </td>--%>
                               <div class="row">
                                  <div class="col-sm-4">
                                              <b>Has any insurance company declined to insure or renew policy or demand an 
                increased rate for renewal ?</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                             <asp:Literal ID="Lit_rejects" runat="server"></asp:Literal>
                                                  <br />
                                 <asp:Literal ID="Lit_remarks" runat="server" Text="Rejection Reason : " 
                    Visible="False"></asp:Literal>
                <asp:Literal ID="Lit_rejReason" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                                </div>
                                </tr>
                                <br /> 
                                <tr>
                     <%--           <td><b>Plan</b></td>
                                <td><strong>
                Plan
                <asp:Literal ID="Lit_plan" runat="server"></asp:Literal>
            &nbsp; (<asp:Literal ID="Lit_SA" runat="server"></asp:Literal>
                )</strong></td>--%>

                            <div class="row">
                                  <div class="col-sm-4">
                                           <b>Plan</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                             <strong>
                Plan
                <asp:Literal ID="Lit_plan" runat="server"></asp:Literal>
            &nbsp; (<asp:Literal ID="Lit_SA" runat="server"></asp:Literal>
                )</strong>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                                </div>

                                </tr>

                                <br /> 

                                <tr>
                               <%-- <td><b>Basic Premium</b></td>
                                <td><asp:Literal ID="Lit_BasicPrem" runat="server"></asp:Literal></td>--%>
                                 <div class="row">
                                                <div class="col-sm-4">
                                               <b>Basic Premium</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                             <asp:Literal ID="Lit_BasicPrem" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                </tr>
                                <br /> 
                                <tr>
                                <%--<td><b>Policy Fee</b></td>
                                <td><asp:Literal ID="Lit_polFee" runat="server"></asp:Literal></td>--%>

                                    <div class="row">
                                                <div class="col-sm-4">
                                               <b>Policy Fee</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                             <asp:Literal ID="Lit_polFee" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                </tr>
                                <br /> 
                                <tr>
                              <%--  <td><b>Admin Fee</b></td>
                                <td><asp:Literal ID="Lit_adminFee" runat="server"></asp:Literal></td>--%>
                                 <div class="row">
                                                <div class="col-sm-4">
                                               <b>Admin Fee</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                             <asp:Literal ID="Lit_adminFee" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                </tr>
                                <br /> 
                                <tr>
                              <%--  <td><b>NBT</b></td>
                                <td><asp:Literal ID="Lit_nbt" runat="server"></asp:Literal></td>--%>

                                  <div class="row">
                                                <div class="col-sm-4">
                                              <b>NBT</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                             <asp:Literal ID="Lit_nbt" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                </tr>
                                <br /> 
                                <tr>
                               <%-- <td><b>VAT</b></td>
                                <td><asp:Literal ID="Lit_vat" runat="server"></asp:Literal></td>--%>

                                <div class="row">
                                                <div class="col-sm-4">
                                              <b>VAT</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                       <asp:Literal ID="Lit_vat" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                </tr>
                                <br /> 
                                 
                                 <tr>
                                <%--<td><b>Total Premium</b></td>
                                <td><strong><asp:Literal ID="Lit_TotalPrem" runat="server"></asp:Literal></strong></td>--%>

                                 <div class="row">
                                                <div class="col-sm-4">
                                              <b>Total Premium</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                       <strong><asp:Literal ID="Lit_TotalPrem" runat="server"></asp:Literal></strong>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                </tr>
                                <br /> 
                                 <tr>
                             <%--   <td><b>Commencement Date</b></td>
                                <td><asp:Literal ID="Lit_comDate" runat="server"></asp:Literal></td>--%>

                                <div class="row">
                                                <div class="col-sm-4">
                                              <b>Commencement Date</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                      <asp:Literal ID="Lit_comDate" runat="server"></asp:Literal>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                </tr>

                               <br /> 

                                <tr>
                                <%--<td></td>
                                <td>
                              

                                  <input type="button" value="Confirm"  data-toggle="modal" Class="btn btn-primary" font-weight:normal;" role="button" href="#myModal"/>
                                 </td>--%>
                                  <div class="row">
                                                <div class="col-sm-4">
                                                </div>
                                                
                                                <div class="col-sm-6">
                                      <input type="button" value="Confirm"  data-toggle="modal" Class="btn btn-primary" font-weight:normal;" role="button" href="#myModal"/>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                </tr>
                                <br /> 
                                <tr>
                               <%-- <td><asp:Label ID="Lit_msg" runat="server" CssClass="errorMsg_1"></asp:Label></td>
                                <td></td>--%>

                                 <div class="row">
                                                <div class="col-sm-4">
                                                </div>
                                                
                                                <div class="col-sm-6">
                                     <asp:Label ID="Lit_msg" runat="server" CssClass="errorMsg_1"></asp:Label> </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                </tr>
                                <br /> 
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="col-xs-1">
                </div>
            </div>




            <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h5 class="modal-title">Declaration :</h5>
        </div>
        <div class="modal-body">
    <%--       I certify the correctness of the statement and I hereby agree to give notice to the Company, immediately of any variation in my profession or occupation or of any changes in my health or habits and that this warranty and agreement shall be promissory and shall form the basis of the contract between the Company and myself, and to accept a policy in the usual printed form by the Company.
     <div style="font-family:Sandaya" align="justify">
        by; i|yka m%ldYj, ksrjµ;dj iy;sl lrñ' uf.a jD;a;sfha fyda /lshdfõ lsishï fjkila jqjfyd;a fyda uf.a fi!LHfha fyda .;s mej;=ïj, lsishï fjkila jQ úfgl iud.ug tA .ek jydu okajk njg .súiñ' fï m%;s{d.drh .súiqï fmdfrdkaÿjla nj yd iud.u yd ud w;/;s .súiqfï moku jk njg o iud.fuka ksl=;a lrkq ,nk idudkH uqøs; Tmamqjla ms&#60;s.ekSugo fuhska tlÛ fjñ' 
        
        </div>--%>
        <%--<img class="img-responsive" src="../../../images/SinhalaFont.PNG" alt="Chania">--%>
           
   <%--     <div class="col-xs-12 hidden-sm hidden-md hidden-lg">  
        <div id ="Testt_1"><img class="img-responsive" src="../../../images/SinhalaForMobile.jpg" /> </div>   <!--Mobile-->
       
          
        <div id ="Testt_3"><img class="img-responsive" src="../../../images/size2.JPG" /> </div>   <!--Mobile-->


        <div id ="Testt_5"><img class="img-responsive" src="../../../images/Size3.JPG" /> </div>   <!--Mobile-->
       
        </div>--%>
        
   
    <div class="hidden-xs col-sm-12 hidden-md hidden-lg">
        <img class="img-responsive" src="../../../images/SinhalaForTablet.jpg" />   <!--Tab-->
    </div>
    <div class="hidden-xs hidden-sm col-md-12 hidden-lg">
        <img class="img-responsive" src="../../../images/SinhalaForTablet.jpg" />   <!--Desktop-->
    </div>
    <div class="hidden-xs hidden-sm hidden-md col-lg-12">
        <img class="img-responsive" src="../../../images/SinhalaForTablet.jpg" />  <!--Large-->
    </div>
<br />
<br /><br />
<div class=row"></div>
 <asp:Button ID="Button2" runat="server" Text="Confirm" onclick="Button1_Click" CssClass="btn btn-success" />&nbsp;&nbsp;
        <button type="button" class="btn btn-danger pull-right"  data-dismiss="modal">Decline</button>

        </div>
        <%--<div class="modal-footer">
          
        </div>--%>
      </div>
      
    </div>
  </div>



             <br />
             <br />
              <br />
             <br />
        </div>
    </div>

</asp:Content>

