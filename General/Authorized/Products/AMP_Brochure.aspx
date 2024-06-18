<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="AMP_Brochure.aspx.cs" Inherits="General_Authorized_Products_AMP_Brochure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style>
     .modal-backdrop {
                 z-index: 0;
             }

     .btn-default, .btn-default:focus, .btn-default.active, .btn-default.active:focus, .btn-default:hover {
    color: #fff !important;
}
    
        .auto-style1 {
            width: 50%;
            height: 73px;
        }
        .auto-style2 {
            width: 10%;
            height: 73px;
        }
    
   </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <link href="/css/modal.css" rel="stylesheet" />
 <div class="main-container" id="main-container" style="min-height:600px">
           <link href="/css/footable.min.css" rel="stylesheet" type="text/css" />
            <script src="/js/footable.min.js" type="text/javascript"></script>

        <script type="text/javascript">
            $(function () {
                $('[id*=gvMembers]').footable();

            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('[id*=gvMembers]').footable();
            });

        </script>
        <div class="container">
            
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/General_Products.aspx">Purchase
                            online</a></li>
                        <li class="breadcrumb-item active">Annual Medical Plan Quotation</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
             <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                     <center><h3>Benefits and Limits of the Sri Lanka Insurance 'Medi Plus Plan'</h3></center>
                </div>
                <div class="col-xs-1">
                </div>
                 
            </div>
            <br />

            

              <asp:Panel ID="Panel2" runat="server" Visible="True">
                <div class="table-responsive">
                   <%-- <table class="table table-bordered">--%>
                    <table class="table"  style="border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                        <%-- <table class="style2 table   table-hover">--%>
                        <%--<tr style="border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">--%>
                        <tr>
                            <td style="width:50%; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                            </td>
                            <td style="text-align:center; width:10%; background-color:#4b5083; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                              <strong>Plan A</strong> 
                            </td>
                            <td style="text-align:center; width:10%; background-color:#4b5083; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Plan B</strong> 
                            </td>
                            <td style="text-align:center; width:10%; background-color:#4b5083; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Plan C</strong>
                            </td>
                            <td style="text-align:center; width:10%; background-color:#4b5083; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                               <strong>Plan D</strong>
                            </td>
                           
                        </tr>
                        <%--<tr style="border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">--%>
                          <tr>  
                            <td style="width:50%; background-color:#EE9B6E; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                               <strong>Annual Sum Insured <br />
                                1. Hospitalisation Benefit - Private Hospitals Only (Minimum hospitalisation of 24-hours)
                           </strong>  </td>                            
                            <td style="width:10%; background-color:#EE9B6E; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                               Rs. 250,000/-
                                <%--<asp:Label ID="lblPremiumB" runat="server"></asp:Label>--%>
                            </td>
                            <td style="width:10%; background-color:#EE9B6E; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 500,000/-
                                <%--<asp:Label ID="lblPremiumC" runat="server"></asp:Label>--%>
                            </td>
                            <td style="width:10%; background-color:#EE9B6E; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                 Rs. 750,000/-
                                <%--<asp:Label ID="lblPremiumD" runat="server"></asp:Label>--%>
                            </td>
                            <td style="width:10%; background-color:#EE9B6E; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                               Rs.1,000,000/-
                                <%--<asp:Label ID="lblPremiumE" runat="server"></asp:Label>--%>
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="style6" style="border-bottom-style: none">
                                &nbsp;
                            </td>
                            <td class="style7">
                                <asp:HyperLink ID="hlBuyA" runat="server" OnPreRender="hlBuyA_PreRender" CssClass="btn btn-primary btn-xs"
                                    Style="width: 60px" Font-Bold="True">Buy</asp:HyperLink>
                            </td>
                            <td class="style7">
                                <asp:HyperLink ID="hlBuyB" runat="server" CssClass="btn btn-primary btn-xs" Style="width: 60px"
                                    Font-Bold="True">Buy</asp:HyperLink>
                            </td>
                            <td class="style7">
                                <asp:HyperLink ID="hlBuyC" runat="server" CssClass="btn btn-primary btn-xs" Style="width: 60px"
                                    Font-Bold="True">Buy</asp:HyperLink>
                            </td>
                            <td class="style7">
                                <asp:HyperLink ID="hlBuyD" runat="server" CssClass="btn btn-primary btn-xs" Style="width: 60px"
                                    Font-Bold="True">Buy</asp:HyperLink>
                            </td>
                            <td class="style7">
                                <asp:HyperLink ID="hlBuyE" runat="server" CssClass="btn btn-primary btn-xs" Style="width: 60px"
                                    Font-Bold="True">Buy</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="style6" style="border-top-style: none">
                                &nbsp;
                            </td>
                            <td class="style7">
                                <asp:HyperLink ID="hlPrintA" runat="server" CssClass="btn btn-primary btn-xs" Style="width: 60px"
                                    Font-Bold="True">Print</asp:HyperLink>
                            </td>
                            <td class="style7">
                                <asp:HyperLink ID="hlPrintB" runat="server" CssClass="btn btn-primary btn-xs" Style="width: 60px"
                                    Font-Bold="True">Print</asp:HyperLink>
                            </td>
                            <td class="style7">
                                <asp:HyperLink ID="hlPrintC" runat="server" CssClass="btn btn-primary btn-xs" Style="width: 60px"
                                    Font-Bold="True">Print</asp:HyperLink>
                            </td>
                            <td class="style7">
                                <asp:HyperLink ID="hlPrintD" runat="server" CssClass="btn btn-primary btn-xs" Style="width: 60px"
                                    Font-Bold="True">Print</asp:HyperLink>
                            </td>
                            <td class="style7">
                                <asp:HyperLink ID="hlPrintE" runat="server" CssClass="btn btn-primary btn-xs" Style="width: 60px"
                                    Font-Bold="True">Print</asp:HyperLink>
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="width:50%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                1.1 Hospital room and board (Ward/ICU) (subject to a maximum 30% of the annual sum insured per year)
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 7,500/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 8,500/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 12,500/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 15,000/-
                            </td>
                           
                        </tr>
                       <%-- <tr>
                            <td class="style13" colspan="6">
                                Hospitalisation Benefit - Private Hospitals Only (Minimum Hospitalisation of 24
                                hours)
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="border: 1px solid White; " class="auto-style1">
                                1.2 Surgeon, Anaesthetist, Medical Practitioner, Consultant and Specialist fees (Maximum benefit)
                            </td>
                            <td style="background-color:#e7e7e8; border: 1px solid White; " class="auto-style2">
                                Rs. 75,000/-
                            </td>
                            <td style="background-color:#e7e7e8; border: 1px solid White; " class="auto-style2">
                                Rs. 150,000/-
                            </td>
                            <td style="background-color:#e7e7e8; border: 1px solid White; " class="auto-style2">
                                Rs. 225,000/-
                            </td>
                            <td style="background-color:#e7e7e8; border: 1px solid White; " class="auto-style2">
                                Rs. 300,000/-
                            </td>
                           
                        </tr>
                        <tr>
                            <td style="width:50%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                1.3 Operation theatre, medicines, laboratory tests, blood and Oxygen charges (Maximum benefit)
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 112,500/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 225,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 337,500/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 450,000/-
                            </td>
                           
                        </tr>
                        <tr>
                            <td style="width:50%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                1.4 **Ambulance charges (Maximum benefit)
                            </td>
                            <td style="width:10%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 2,500/-
                            </td>
                            <td style="width:10%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 3,000/-
                            </td>
                            <td style="width:10%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 4,000/-
                            </td>
                            <td style="width:10%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 5,000/-
                            </td>
                            
                        </tr>
                        <tr>
                            <td style="width:50%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                1.5 *Maternity Benefits - Abnormal childbirth; including caesarean surgery (Maximum benefit)
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 100,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 200,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 250,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 300,000/-
                            </td>
                            
                        </tr>
                        <tr>
                            <td style="width:50%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                1.6 *Maternity Benefits - Normal childbirth (Maximum benefit)
                            </td>
                            <td style="width:10%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 75,000/-
                            </td>
                            <td style="width:10%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 100,000/-
                            </td>
                            <td style="width:10%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 125,000/-
                            </td>
                            <td style="width:10%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 150,000/-
                            </td>
                            
                        </tr>
                        <tr>
                            <td style="width:50%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                1.7 Pre and Post-Hospitalisation Benefit
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="5">
                                As charged up to a maximum of 5% of the sum insured, limited to 30 days, prior/after hospitalisation.
                            </td>
                        
                        </tr>
                        
                        <tr>
                            <td style="width:50%; background-color:#EE9B6E; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="6">
                               <strong> 2. Hospitalisation Benefit - Government Hospitals Only (Minimum hospitalisation of 24-hours)</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                2.1 Hospitalised in a non paying ward - Daily allowance maximum up to 15 days
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 5,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 5,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 5,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 5,000/-
                            </td>
                            
                        </tr>
                        <tr>
                            <td style="width:50%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                2.2 Medicines, consumables, OT charges etc. (Maximum benefit)
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 100,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 200,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 300,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 400,000/-
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                2.3 *Maternity benefit (Normal childbirth/Abnormal childbirth, including caesarean surgery)
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 10,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 15,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 20,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 25,000/-
                            </td>                            
                        </tr>
                        <tr>
                            <td style="width:50%; background-color:#EE9B6E; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="6">
                               <strong> 3. Epidemic and Pandemic cover (Covid 19)</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                3.1 Government Hospital per day allowance (Maximum 15 days within above government hospital allowance limit)
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 1,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 1,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 1,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 1,000/-
                            </td>                            
                        </tr>
                        <tr>
                            <td style="width:50%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                3.2 Private Hospitals/ Intermediary care centre managed by approved health ministry registered private hospitals  
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 100,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 125,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 150,000/-
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                Rs. 150,000/-
                            </td>                            
                        </tr>
                        <tr>
                            <td style="width:50%; background-color:#EE9B6E; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="6">
                               <strong>4. Additional Benefits (within annual sum insured)</strong>
                            </td>
                        </tr>
                         <tr>
                            <td style="width:50%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="6">
                               Benefits applicable to treatment for:
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                4.1 Cataract (Including lenses)
                            </td>
                            <td style="width:10%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="5">
                                20% of the sum insured, subject to a maximum of LKR 75,000.
                            </td>
                      
                        </tr>
                        
                        <tr>
                            <td style="width:50%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                4.2 Day care surgeries
                            </td>
                            <td style="width:10%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="5">
                                Covered subjected to hospitalisation for less than 24-hours:135 Day Care Procedures covered.
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                4.3 Organ donor expenses
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="5">
                                Covered within the sum insured of the donee.
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                4.4 No claim bonus
                            </td>
                            <td style="width:10%; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="5">
                                Increase in basic sum insured by 5%, for every claim free year, upto a maximum of
                                50%. In case of a claim, NCB would be reduced by 10%.
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                4.5 Family coverage
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="5">
                                Self, spouse and a maximum of five children.
                            </td>
                        </tr>
                       <tr> 
                       <td  td style="border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="6"></td>
                       </tr>
                       <tr> 
                       <td style="border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="6"></td>
                       </tr>
                        <tr>
                            <td style="border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White; font-size:11px" colspan="6">

                            Note: <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*Maternity Cover (both Private and Government hospitals): only receivable after two years waiting period for policies of female lives
                                    renewed without any break.<br />
                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**Subject to a licensed ambulance service being used                               
                                <br />
                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*Critical illness cover with an inpatient limit<br />
                               <br />
                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*Medical reports are not required upto age limit of 55 years
                                <p align="right">*Conditions apply</p>
                             
                            </td>
                        </tr>

                       <%--<tr> 
                       <td  td style="border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="6"></td>
                       </tr>--%>
                       <tr> 
                       <td  td style="border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="6">
                      <%--     <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">First Year Exclusions</button>
                               <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal2">General Exclusions</button>
                                   <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal3">Hospital List</button>
                                       <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal4">Declined Diseases</button>
                           <a href="Documents/AMP_Policy_book.pdf" target="_blank" ><input type="button" class="btn btn-primary" value="Policy Book" style="background-color:#61ade7 !important; border-color:#61ade7 !important; font-weight:bold; width:15%" /></a>--%>
             <%--                              <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">Policy Book</button>--%>
                       <a data-toggle="modal" href="#myModal" ><input type="button" class="btn btn-default" value="First Year Exclusions" style="background-color:#202340 !important; border-color:#202340 !important; font-weight:bold; /*width:17%*/ " /></a>
                       <a data-toggle="modal" href="#myModal2"><input type="button" class="btn btn-default" value="General Exclusions" style="background-color:#202340 !important; border-color:#202340 !important; font-weight:bold; /*width:17%*/" /></a>
                        <a data-toggle="modal" href="#myModal3"><input type="button" class="btn btn-default" value="Hospital List" style="background-color:#202340 !important; border-color:#202340 !important; font-weight:bold; /*width:17%*/" /></a>
                        <a data-toggle="modal" href="#myModal4"><input type="button" class="btn btn-default" value="Declined Diseases" style="background-color:#202340 !important; border-color:#202340 !important; font-weight:bold; /*width:17%*/" /></a>
                        <a href="Documents/AMP_Policy_book.pdf" target="_blank" ><input type="button" class="btn btn-default" value="Policy Book" style="background-color:#202340 !important; border-color:#202340 !important; font-weight:bold; /*width:17%*/" /></a>
                       
                       </td>
                       </tr>

                       <tr> 
                       <td style="border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" colspan="6"></td>
                       </tr>

                        
                    </table>
                </div>
               <br />
               <br />
                     

                <div class="modal fade" id="myModal" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                
                                <h5 class="modal-title">
                                    First year exclusions of the policy</h5><button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                            </div>
                            <div class="modal-body">
                                <ol>
                                    <li><span>1. Deviated Nasal Septum/ Nasal &amp; Paranasal Sinus Disorders (except Malignancy), Treatment for Chronic Suppurative Otitis Media (CSOM) and Serous Otitis Media (Grommet Insertion)<o:p></o:p></span> </li>
                                    <li><span>2. Medical or Surgical management of diseases of Tonsils / Adenoids (except Malignancy)<o:p></o:p></span>
                                    </li>
                                    <li><span>3. Surgery of Thyroid Gland excluding for the reason of Malignancy<o:p></o:p></span>
                                    </li>
                                    <li><span>4. All types of Hernias<o:p></o:p></span> </li>
                                    <li><span>5. Hydrocoele / Varicocoele / Spermatocoele<o:p></o:p></span> </li>
                                    <li><span>6. Piles / Fissure / Fistula-in-Ano / Rectal Prolapse / Pilonidal Sinus<o:p></o:p></span>
                                    </li>
                                    <li><span>7. Benign Prostatic Hypertrophy<o:p></o:p></span> </li>
                                    <li><span>8. Treatment of all gynaecological conditions (Such as but not limited to Uterine Fibroid, Dysfunctional Uterine Bleeding, Hysterectomy, Uterine Prolapse, Endometriosis, Adenomyosis Uteri, Ovarian Cyst etc) except those arising from malignancy<o:p></o:p></span>
                                    </li>
                                    <li><span>9. Prolapsed Intervertebral Disc<o:p></o:p></span> </li>
                                    <li><span>10. Hypertension and related complications<o:p></o:p></span> </li>
                                    <li><span>11. Skin and all internal cysts/tumors/nodules/ polyps/ganglions/lipomas of any kind unless malignant<o:p></o:p></span> </li>
                                    <li><span>12. Calculus Diseases of any aetiology<o:p></o:p></span> </li>
                                    <li><span>13. Retinopathy / Retinal Detachment<o:p></o:p></span> </li>
                                    <li><span>14. Peripheral Vascular Disease due to Diabetes / Diabetic Foot<o:p></o:p></span>
                                    </li>
                                    <li><span>15. All types of CRF and acute on chronic Renal Failures but not ARF, including Renal Failure due to Diabetes<o:p></o:p></span> </li>
                                    <li><span>16. Osteoporosis / Pathological Fracture / Degenerative Joint Diseases<o:p></o:p></span>
                                    </li>
                                    <li><span>17. Cataract<o:p></o:p></span> </li>
                                    <li><span>18. Treatment for degenerative joint conditions including joint replacement surgeries. However, joint surgeries necessitated due to accidents would not be a part of this exclusion<o:p></o:p></span> </li>
                                    <li><span>19. Treatment for benign breast disorders like fibroadenoma, fibrocystic disease etc<o:p></o:p></span> </li>
                                    <li><span>20. Treatment for Carpal tunnel syndrome<o:p></o:p></span> </li>
                                    <li><span>21. Treatment for Peripheral Vascular disease including varicose veins</span>
                                    </li>
                                </ol>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary btn-xs" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>

                 <div class="modal fade" id="myModal2" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                
                                <h5 class="modal-title">
                                    General exclusions of the policy</h5><button type="button" class="close" data-dismiss="modal">
                                    &times;</button>

                                    </div>
                            <div class="modal-body">
                            The company shall not be liable to make any payment if hospitalization or claims are attributable to, or based on, or arise out of, or are directly or indirectly connected to any of the followings:<br /><br />
                            <ol>
                                <li><span>1. Any pre-existing illnesses, diseases, injuries, symptoms or impairments (“Pre-existing Conditions”) from which the Life Insured is suffering prior to the Commencement Date of Insurance of the Life Insured, are excluded from the scope of the cover of the policy.  Complications arising out of pre-existing conditions would also be deemed as pre-existing.<o:p></o:p></span> </li>
                                <li><span>2. Medical expenses incurred for treatment undertaken for disease or illness within 30 days of the date of inception or revival of the policy, except for accidental injuries.<o:p></o:p></span> </li>
                                <li><span>3. Hospitalization / Medical expenses not directly related to the specific illness or injury for which hospitalization took place and the expenses which are not approved by the attending doctor.<o:p></o:p></span> </li>
                                <li><span>4. Any treatment not performed by a doctor<o:p></o:p></span> </li>
                                <li><span>5. Expenses which are not for actual, necessary and reasonable expenses incurred in the treatment of the Illness or Physical Injury, or any elective surgery or treatment which is not medically necessary.<o:p></o:p></span> </li>
                                <li><span>6. Sterility, treatment whether to effect or to treat infertility, any fertility, sub fertility or assisted conception procedure, surrogate or vicarious pregnancy, birth control, contraceptive supplies or services including complication arising due to supplying services.<o:p></o:p></span> </li>
                                <li><span>7. Any diagnosis or treatment arising from or traceable to pregnancy or child birth, miscarriage, abortion or complications of any of these including caesarean section, voluntary medical termination of pregnancy and/or any treatment related to pre and post natal care of the mother or the new born, unless specified otherwise under policy terms and conditions.<o:p></o:p></span> </li>
                                 <li><span>8. Any medical or non-medical expenses incurred in respect of harvesting and storage of stem cells when carried out as a preventive measure against possible future ailments.<o:p></o:p></span> </li>
                                 <li><span>9. Hospitalization for correction of birth defects or congenital anomalies<o:p></o:p></span> </li>
                                 <li><span>10. Any sexually transmitted diseases or any condition directly or indirectly caused by or associated with Human Immune Deficiency Virus (HIV) or any Syndrome or condition of a similar kind commonly referred to as AIDS (Acquired Immune Deficiency Syndrome).<o:p></o:p></span> </li>
                                <li><span>11. Dental treatment or surgery of any kind unless necessitated by an Accident. <o:p></o:p></span> </li>
                                 <li><span>12. Cost of spectacles contact lenses hearing aids and the cost of treatment for vision correction.<o:p></o:p></span> </li>
                                 <li><span>13. Self affected injuries or conditions (attempted suicide) and or the treatment directly or indirectly arising from alcoholism or drug abuse and any Illness or Physical Injury which may be suffered after consumption of intoxication liquors or drugs.<o:p></o:p></span> </li>
                                <li><span>14. Non-allopathic methods of surgery and treatment.<o:p></o:p></span> </li>
                                <li><span>15. Hospitalization for donation of an organ unless specified otherwise and subject to terms and conditions of the policy.<o:p></o:p></span> </li>
                                <li><span>16. Medical expenses incurred due to Ventral / Incisional Hernia unless the Company has paid for the first operation.<o:p></o:p></span> </li>
                                <li><span>17. Medical or surgical treatment for weight reduction or weight improvement regardless of whether the same is caused (directly or indirectly) by a medical condition.<o:p></o:p></span> </li>
                                 <li><span>18. Psychiatric, mental disorders (including mental health treatments), Parkinson and Alzheimer’s disease, general debility or exhaustion (“run-down conditions”), stem cell implantation or surgery, or growth hormone therapy.<o:p></o:p></span> </li>
                                 <li><span>19. Medical expenses relating to any Hospitalization primarily for diagnostic, X-ray or any other investigations.<o:p></o:p></span> </li>
                                <li><span>20. Any experimental or unproven procedures or treatments, devices or pharmacological regimens of any description.<o:p></o:p></span> </li>
                                 <li><span>21. Stay in Hospital for domestic reason where no active regular treatment is given by a Doctor.<o:p></o:p></span> </li>
                                 <li><span>22. Charges for services received in convalescent home and nursing homes, nature cure clinics and similar establishments.<o:p></o:p></span> </li>
                                 <li><span>23. Circumcision unless necessary for treatment due to an accident or ailment and subject to terms and conditions of the policy.<o:p></o:p></span> </li>
                                 <li><span>24. Plastic surgery or cosmetic surgery unless necessary as a part of medically necessary treatment certified by the attending Medical Practitioner for reconstruction following an Accident or illness, Lasik or laser surgery.<o:p></o:p></span> </li>
                                 <li><span>25. Any treatment related to sleep disorder or sleep apnoea syndrome. <o:p></o:p></span> </li>
                                <li><span>26. Expenses for any routine or prescribed medical check up or examination, external and or durable Medical / Non medical equipment of any kind used for diagnosis and/or treatment and/or treatment and/or monitoring and/or maintenance and/ or support including CPAP, CAPD, Infusion pump, oxygen concentrator etc, ambulatory devices like walker, crutches, belts, collars, caps, splints, stings, braces, stockings, gloves, hand soaps etc. of any kind, Diabetic footwear, glucometer/ thermometer and similar related items and also any medical equipment, which are subsequently used at home, administrative fees, biomedical waste fees, medical records charges and any luxury taxes. <o:p></o:p></span> </li>
                                <li><span>27. Any kind of service charges, surcharges, admission fees, registration charges etc. levied by the Hospital.<o:p></o:p></span> </li>
                                 <li><span>28. Any natural peril (including but not limited to avalanche, earthquake, volcanic eruptions, or any kind of natural hazard), nuclear disaster, radioactive contamination and/or release of nuclear or atomic energy.<o:p></o:p></span> </li>
                                 <li><span>29. War, invasion, acts of foreign enemies, hostilities (whether war be declared or not), civil war, terrorism, rebellion, active participation in strikes, riots or civil commotion, revolution, insurrection or military or usurped power, and full-time service in any of the armed forces.<o:p></o:p></span> </li>
                                 <li><span>30. Naval or military operations ( including duties of peace time ) of the armed forces or air force and participation in operation requiring the use of arms or which are ordered by military authorities for combating terrorists, rebels and the like.<o:p></o:p></span> </li>
                                 <li><span>31. Participation in any hazardous activity or sports including but not limited to racing scuba dividing, aerial sports, bungee jumping or mountaineering, activities such as hang-gliding, ballooning, and any other hazardous activities or sports unless agreed by special endorsement.<o:p></o:p></span> </li>
                                 <li><span>32. Expenses incurred for procurement of a replacement organ, transportation costs of the replacement organ and associated administration costs. Expenses incurred by the donor would be covered for hospitalization only and within the overall sum insured of the donee.<o:p></o:p></span> </li>
                                 <li><span>33. Any Insured Person committing or attempting to commit a criminal or illegal act while sane or insane.<o:p></o:p></span> </li>
                                 <li><span>34. Non Medical expenses including Personal comfort and convenience items or services such as telephone, television, personal attendant or barber or beauty services, diet charges, food, cosmetics, napkins, toiletry items, guest services and similar incidental expenses or services.<o:p></o:p></span> </li>
                                 <li><span>35. Any hospitalization or medical expenses incurred outside of Republic of Sri Lanka and any domiciliary treatment.<o:p></o:p></span> </li>
                               

                                    </ol>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary btn-xs" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
                  
 

                 <div class="modal fade" id="myModal3" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">                                
                                <h5 class="modal-title">
                                     Approved Hospitals for Sri Lanka Insurance Medi Plus Plan</h5>
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                    </div>
                            <div class="modal-body">
                                <table>
                                
                                <tr>
                                <td rowspan="13"><strong>Colombo</strong></td>
                                    <td class="style1" rowspan="13">
                                        &nbsp;</td>
                                <td>Lanka Hospital Colombo 05</td>
                                </tr>

                                
                                <tr>
                                <td>The Central Hospital Colombo.10 (Asha)</td>
                                </tr>

                                <tr>
                                <td>Asiri Hospitals - Colombo 05</td>
                                </tr>

                                    <tr>
                                        <td>
                                            Asiri Surgical - Colombo 05</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Durdans Hospital Colombo 03 (Card is not entertained for Heart Centre)</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Nawaloka Hospital Colombo 02</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            New Delmon Hospital-Colombo 06</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Oasis Hospital Ltd Colombo 05</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Melsta Hospital - Colombo 7</td>
                                    </tr>                                    
                                    <tr>
                                        <td>
                                            Sri Jayewardenapura General Hospital</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Ninewells Care Hospital (Pvt) Ltd.</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Golden Key EENT Hospital</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Dr. Neville Fernando Teaching Hospital - Kaduwela.</td>
                                    </tr>

                                    <tr>
                                        <td colspan="3"><hr />
                                            </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2">
                                            <b>Gampaha</b></td>
                                        <td class="style1" rowspan="2">
                                            &nbsp;</td>
                                        <td>
                                            Leesons Hospital - Ragama</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Hemas Hospital Ltd.Wattala</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                           <hr /></td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2">
                                            <b>Galle</b></td>
                                        <td class="style1" rowspan="2">
                                            &nbsp;</td>
                                        <td>
                                            Asiri Hospital-Galle</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Ruhunu Hospital - Galle</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <hr /></td>
                                    </tr>
                                    <tr>
                                        <td rowspan="3">
                                            <b>Kandy</b></td>
                                        <td class="style1" rowspan="3">
                                            &nbsp;</td>
                                        <td>
                                            Asiri Hospital - Kandy</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Lake Side Hospitals Kandy</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Suwa Sewana Hospital Kandy</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <hr /></td>
                                    </tr>
                                    <tr>
                                        <td rowspan="3">
                                            <b>Matara</b></td>
                                        <td class="style1" rowspan="3">
                                            &nbsp;</td>
                                        <td>
                                            Mohotti (Pvt) Hospital -Matara</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Asiri Hospital Matara ( Pvt ) Ltd - Anagarika Dharmapala Mawatha, Matara</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Corporative Hospital Matara</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                           <hr /></td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2">
                                            <b>Negombo</b></td>
                                        <td class="style1" rowspan="2">
                                            &nbsp;</td>
                                        <td>
                                            Nawaloka Hospital Negombo</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Ave Maria Hospital-Negombo</td>
                                    </tr>
                                     <tr>
                                        <td colspan="3">
                                           <hr /></td>
                                    </tr>

                                </table>
                            </div>

                            <div class="modal-header">
                                <h5 class="modal-title">
                                     Unapproved Hospitals for both Cashless and reimbursement</h5>

                                    </div>
                            <div class="modal-body">
                                <table>
                                
                                <tr>
                                <td>1. Borella Pvt Hospital</td>
                                </tr>                                
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>                                
                                    <tr>
                                        <td>
                                            2. Kolonnawa Nursing Home</td>
                                    </tr>                                   

                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>                                   

                                    <tr>
                                        <td>
                                            3. Nugegoda Nursing Home</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4. Horana Pvt Hospital</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            5. Navodya Hospital - Embilipitiya</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            6. Wish Fertility & IVF Clinic Udahamulla , Nugegoda</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            7. Osteo Clinic ,No. 531/1, Athurugiriya Road Malabe</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            8. Medi Master (PVT) LTD, No. 531/1A, Athurugiriya Road Malabe</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            9. Sirinimaa Private Nursing Home(Pvt)Ltd,No. 52/1,Main st,Ruwanwella</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td><b>
                                            Unapproved Pharmacy’s:</b></td>
                                    </tr>
                                    <tr>                                        
                                        <td>
                                            NEW LANKA MEDICARE (PVT) LTD, NO:679,Arpico Super Center,Peradeniya Road,Kandy</td>
                                    </tr>
                                    

                                </table>
                            </div>

                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary btn-xs" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>

                 <div class="modal fade" id="myModal4" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">                                
                                <h5 class="modal-title">
                                    Declined Diseases</h5><button type="button" class="close" data-dismiss="modal">
                                    &times;</button>

                                    </div>
                            <div class="modal-body">
                                
                                <ol>
                                    <li>
                                        <p class="MsoNormal">
                                            Abnormal hemoglobin</p>
                                    </li>        
                                    <li>
                                        <p class="MsoNormal">
                                            Alcoholic</p>
                                    </li>
                                    <li>
                                        <p class="MsoNormal">
                                            Alcoholic gastritis</p>
                                    </li>
                                    <li>
                                        <p class="MsoNormal">
                                            Alcoholic liver disease</p>
                                    </li>
                                    <li>
                                        <p class="MsoNormal">
                                            Amputation not related with injury</p>
                                    </li>                             
                                    <li>
                                        <p class="MsoNormal">
                                            Arteriosclerosis
                                        </p>
                                    </li>
                                    <li>
                                        <p class="MsoNormal">
                                            Autism</p>
                                    </li>
                                    <li>
                                        <p class="MsoNormal">
                                            Cancer/Carcinoma unspecified</p>
                                    </li>                                 
                                   <li>
                                        <p class="MsoNormal">
                                            Congenital Heart Diseases</p>
                                    </li>
                                    <li>
                                        <p class="MsoNormal">
                                            Coronary Heart Diseases<span>&nbsp; </span>Respectively coronary insufficiency</p>
                                    </li>
                                    <li>
                                        <p class="MsoNormal">
                                            Coughing Blood</p>
                                    </li>
                                    <li>
                                        <p class="MsoNormal">
                                            Diabetes mellitus (Type I and Type II)</p>
                                    </li>
                                    <li>
                                        <p class="MsoNormal">
                                            Heart Failure</p>
                                    </li>
                                    <li>
                                        <p class="MsoNormal">
                                            Heart Hypertrophy
                                        </p>
                                    </li>
                                    <li>
                                        <p class="MsoNormal">
                                            Heart Valve Defects</p>
                                    </li>
                                    <li>
                                        <p class="MsoNormal">
                                            Hepatomegaly<span>&nbsp; </span>(Enlargement of the liver)</p>
                                    </li>
                                    <li>
                                        <p class="MsoNormal">
                                            Myocarditis Rheumatic Heart Disease</p>
                                    </li>                                    
                                    <li>
                                        <p class="MsoNormal">
                                            Poliomyelitis</p>
                                    </li>
                                    <li>
                                        <p class="MsoNormal">
                                            Polyarthritis
                                        </p>
                                    </li>                             
                                    <li>
                                        <p class="MsoNormal">
                                            Spondylosis</p>
                                    </li>
                                   
                                </ol>
                                
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary btn-xs" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>

                  </span>

            </asp:Panel>

            </div>
       </div>

</asp:Content>

