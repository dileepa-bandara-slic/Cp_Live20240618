<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="GT_Brochure_2.aspx.cs" Inherits="General_Authorized_Products_GT_Brochure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="/js/jquery-3.5.1.min.js"></script>
    <script src="/js/bootstrap.min.js" type="text/javascript"></script>
    <style>
        /*@media (max-width:479px)
        {
            .navbar-fixed-top + .main-container
            {
                padding-top: 40px;
            }
        }*/
        
        .test22
        {
            font-size: 100%;
            font-family: Tahoma;
        }
        
        .divWaiting
        {
            position: absolute;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 1024px;
            width: 100%;
            padding-top: 20%;
        }
        
     .style10
    {
        text-align: center;
        height: 25px;
        border: thin solid #DDDDDD;
        font-weight: bold;
        background-color: #454545;
        color: #FFFFFF;
    }
    .style11
    {
        color: #FFFFFF;
    }
    .style12
    {
        text-align: center;
        height: 25px;
        border: thin solid #DDDDDD;
        font-weight: bold;
        color: #FFFFFF;
        background-color: #454545;
    }

        .style13
    {
        height: 25px;
        font-weight: bold;
        border: thin solid #DDDDDD;
        background-color: #DCDCDC;
    }
    
    .style2
    {
            width: 80%;            
    }
    
    .table-bordered
    {
        border: 1px solid #FFFFFF !important;
    }
    
  
    .td 
    {
    border: 1px solid #FFFFFF !important;
    border-top:1px solid white !important;
    border-bottom:1px solid white !important;
    border-right:1px solid white !important;
    border-left:1px solid white !important;
    }
    
    .G500
    {
        background-color:#d2dcf0;
        color:Black;
        border: 1px solid;
        text-align:center;
    }
    
     .G100
    {
       
        text-align:center;
        background-color:#ede4ed;
        color:Black;
        border: 1px solid; 
    }
    
    .G50
    {
        background-color:#f7d5ae;
        color:Black;
        text-align:center;
        border: 1px solid;
    }
    
    .S100
    {
        background-color:#f9f8ce;
        color:Black; 
        border: 1px solid;
        text-align:center;
    }
    
    .S50
    {
         background-color:#dbe8d0;
         color:Black; 
         border: 1px solid;
         text-align:center;
    }
    
    .A25
    {
        background-color:#c7d7e9;
        color:Black;
        border: 1px solid;
        text-align:center;
    }
    
    .GTBenefit
    {
        background-color:#b9c4d9;
        color:Black;
        border: 1px solid;
    }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
         <%--   </br>--%>
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
                        <li class="breadcrumb-item active">Globe Trotter</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
           <%-- </br>--%>
             <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                     <center><h3>Benefit Table of Globe Trotter Individual Travel Insurance Cover</h3></center>
                </div>
                <div class="col-xs-1">
                </div>
                 
            </div>
             </br>

              <asp:Panel ID="Panel2" runat="server" Visible="True">
                <div class="table-responsive">
                   <%-- <table class="table table-bordered">--%>
                    <table class="table"  style="border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                        <%-- <table class="style2 table   table-hover">--%>
                        <%--<tr style="border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">--%>
                        <tr>
                            <th rowspan="4" style="width:16%;  background-color:#b9c4d9; color:Black; border: 1px solid; border-bottom-color:Gray; border-left-color:Gray; border-right-color:Gray; border-top-color:Gray; vertical-align:middle; text-align:center">
                            Benefits</th>
                            <td colspan="2" style="width:7%; background-color:#d2dcf0; color:#494e9c; border: 1px solid; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              <strong>Global 500</strong> 
                            </td>


                            <td colspan="2"  style="width:7%; background-color:#ede4ed; color:#494e9c; border: 1px solid; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              <strong>Global 100</strong> 
                            </td>


                            <td colspan="2" style="width:7%; background-color:#f7d5ae; color:#494e9c; border: 1px solid; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              <strong>Global 50</strong> 
                            </td>

                            <td colspan="2"  style="width:7%; background-color:#f9f8ce; color:#494e9c; border: 1px solid; border-bottom-color:Gray;  border-right-color:White; border-top-color:Gray; text-align:center">
                              <strong>Standard 100</strong> 
                            </td>


                            <td colspan="2" style="width:7%; background-color:#dbe8d0; color:#494e9c; border: 1px solid; border-bottom-color:Gray;  border-right-color:White; border-top-color:Gray; text-align:center">
                                <strong>Standard 50</strong> 
                            </td>
                          
                           
                            <td colspan="2" style="width:7%; background-color:#c7d7e9; color:#494e9c; border: 1px solid; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                                <strong>Asia 25</strong>
                            </td>
                            
                        </tr>
                        <tr>
                            <td colspan="2" class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray">
                              <strong>worldwide</strong> 
                            </td>

                            
                            <td colspan="2" class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              <strong>worldwide</strong> 
                            </td>

                            

                            <td colspan="2" class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              <strong>worldwide</strong> 
                            </td>

                            <td colspan="2" class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              <strong>Excluding USA, Canada</strong> 
                            </td>

                         
                            <td colspan="2" class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                                <strong>Excluding USA, Canada</strong>
                            </td>
                         
                            <td colspan="2" class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                                <strong>Asia Excluding Japan</strong>
                            </td>
                           
                        </tr>
                        <tr>
                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              <strong>Sum Insured</strong> 
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              <strong>Excess</strong> 
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray;  text-align:center">
                              <strong>Sum Insured</strong> 
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              <strong>Excess</strong> 
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              <strong>Sum Insured</strong> 
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              <strong>Excess</strong> 
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              <strong>Sum Insured</strong> 
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                                <strong>Excess</strong> 
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                                <strong>Sum Insured</strong>
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                               <strong>Excess</strong>
                            </td>
                            <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                                <strong>Sum Insured</strong>
                            </td>
                             <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                                <strong>Excess</strong>
                            </td>
                        </tr>
                        <tr>

                        <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              <strong>US $</strong> 
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              <strong>US $</strong> 
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray;  text-align:center">
                              <strong>US $</strong> 
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              <strong>US $</strong> 
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              <strong>US $</strong> 
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              <strong>US $</strong> 
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              <strong>US $</strong> 
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                                <strong>US $</strong> 
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                                <strong>US $</strong>
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                               <strong>US $</strong>
                            </td>
                            <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                                <strong>US $</strong>
                            </td>
                             <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                                <strong>US $</strong>
                            </td>
                        </tr>

                         <tr>

                          <td class="GTBenefit" style="width:16%; border-bottom-color:Gray; border-left-color:Gray; border-right-color:Gray; border-top-color:Gray">
                              <strong>a) Medical expenses including:</strong> 
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              500,000
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              100 
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              100,000
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              100
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              50,000
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              100
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              100,000
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                               75
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                               50,000
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                               75
                            </td>
                            <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                               25,000
                            </td>
                             <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                               75
                            </td>
                        </tr>

                         <tr>

                          <td class="GTBenefit" style="width:16%; border-bottom-color:Gray; border-left-color:Gray; border-right-color:Gray; border-top-color:Gray">
                              <strong><p style="padding-left:5px">i) Dental treatment</p></strong> 
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              100 
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              100
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              100
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>
                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              75
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              75
                            </td>
                            <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>
                             <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              75
                            </td>
                        </tr>
                       
                         <tr>
                          <td class="GTBenefit" style="width:16%; border-bottom-color:Gray; border-left-color:Gray; border-right-color:Gray; border-top-color:Gray">
                              <strong><p style="padding-left:5px">ii) Transport of Mortal remains or burial at local place</p></strong> 
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              7000
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              100 
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              7000
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              100
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              7000
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              100
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              7000
                            </td>
                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              75
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              7000
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              75
                            </td>
                            <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              7000
                            </td>
                             <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              75
                            </td>
                        </tr>

                         <tr>
                          <td class="GTBenefit" style="width:16%; border-bottom-color:Gray; border-left-color:Gray; border-right-color:Gray; border-top-color:Gray">
                              <strong>b) Hospital Daily Allowance</strong> 
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              30/day for 20 days
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              48 hrs
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              30/day for 20 days
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              48 hrs
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              30/day for 20 days
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              48 hrs
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              30/day for 20 days
                            </td>
                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              48 hrs
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                             30/day for 20 days
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              48 hrs
                            </td>
                            <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              30/day for 20 days
                            </td>
                             <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              48 hrs
                            </td>
                        </tr>


                         <tr>
                          <td class="GTBenefit" style="width:16%; border-bottom-color:Gray; border-left-color:Gray; border-right-color:Gray; border-top-color:Gray">
                             <strong>c) Total loss of Checked Baggage</strong> 
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              1200
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              1200
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              1200
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              1200
                            </td>
                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                             1200
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>
                            <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              1200
                            </td>
                             <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              -
                            </td>
                        </tr>


                         <tr>
                          <td class="GTBenefit" style="width:16%; border-bottom-color:Gray; border-left-color:Gray; border-right-color:Gray; border-top-color:Gray">
                             <strong>d) Delay of Checked Baggage</strong>  
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              150
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              12 hrs
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              150
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              12 hrs
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              150
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              12 hrs
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              150
                            </td>
                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              12 hrs
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                             150
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              12 hrs
                            </td>
                            <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              150
                            </td>
                             <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              12 hrs
                            </td>
                        </tr>
                       
                         <tr>
                          <td class="GTBenefit" style="width:16%; border-bottom-color:Gray; border-left-color:Gray; border-right-color:Gray; border-top-color:Gray">
                              <strong>e) Loss of Passport</strong>  
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>
                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                             250
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>
                            <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>
                             <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              -
                            </td>
                        </tr>
                     
                      <tr>
                          <td class="GTBenefit" style="width:16%; border-bottom-color:Gray; border-left-color:Gray; border-right-color:Gray; border-top-color:Gray">
                              <strong>f) Hijacked distress Allowance</strong>  
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              125 / day for 7 days
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              12 hrs
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              125 / day for 7 days
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              12 hrs
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              125 / day for 7 days
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              12 hrs
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              125 / day for 7 days
                            </td>
                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              12 hrs
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              125 / day for 7 days
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              12 hrs
                            </td>
                            <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              125 / day for 7 days
                            </td>
                             <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              12 hrs
                            </td>
                        </tr>


                        <tr>
                          <td class="GTBenefit" style="width:16%; border-bottom-color:Gray; border-left-color:Gray; border-right-color:Gray; border-top-color:Gray">
                              <strong>g) Financial Emergency Assistance</strong>
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>
                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>
                            <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              250
                            </td>
                             <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              -
                            </td>
                        </tr>


                         <tr>
                          <td class="GTBenefit" style="width:16%; border-bottom-color:Gray; border-left-color:Gray; border-right-color:Gray; border-top-color:Gray">
                             <strong>h) Personal Accident during travel</strong> 
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              25,000
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              25,000
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              25,000
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              25,000
                            </td>
                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              25,000
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>
                            <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              15,000
                            </td>
                             <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              -
                            </td>
                        </tr>

                       
                         <tr>
                          <td class="GTBenefit" style="width:16%; border-bottom-color:Gray; border-left-color:Gray; border-right-color:Gray; border-top-color:Gray">
                             <strong>i) Personal liability</strong> 
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              50,000
                            </td>

                            <td class="G500" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              50,000
                            </td>

                            <td class="G100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              50,000
                            </td>

                            <td class="G50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>

                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                               50,000
                            </td>
                            <td class="S100" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              50,000
                            </td>
                            <td class="S50" style="width:7%; border-bottom-color:Gray; border-right-color:White; border-top-color:Gray; text-align:center">
                              -
                            </td>
                            <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              30,000
                            </td>
                             <td class="A25" style="width:7%; border-bottom-color:Gray; border-right-color:Gray; border-top-color:Gray; text-align:center">
                              -
                            </td>
                        </tr>

                      
                    </table>
                </div>
               <br />
              

                
            </asp:Panel>

             <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                   Please note:
                </div>
                <div class="col-xs-1">
                </div>
            </div>
             <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                   -    Maxium duration per trip: 180 days
                </div>
                <div class="col-xs-1">
                </div>
            </div>
             <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                   -    Maxium age of insured persons: 70 years
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                   -    Non-insurable person: professional and semi-professional sportsmen
                </div>
                <div class="col-xs-1">
                </div>
            </div>

            </br>
            </div>
       </div>

</asp:Content>

