<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="GT_Brochure.aspx.cs" Inherits="General_Authorized_Products_GT_Brochure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="/js/jquery-3.5.1.min.js"></script>
    <script src="/js/jquery-3.5.1.js"></script>
    <script src="/js/bootstrap.min.js" type="text/javascript"></script>
    <style>
        @media (max-width:479px)
        {
            .navbar-fixed-top + .main-container
            {
                padding-top: 40px;
            }
        }
        
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
    
    
    .button102{
 width: 300px;
   height: 26px;
   background: #6fc16f;
   border: 0px solid #6fc16f;
   position: relative;
   color:White;
   padding-left:10px;
}

.button102::before{
   width: 0;
   height: 0;
   border: 13px solid transparent;
   border-left: 8px solid #6fc16f;
   content: '';
   position: absolute;
   top: 0px;
   left: 300px;
   
  
}
.button102::after{
    width: 0;
   height: 0;
   border: 13px solid transparent;
   border-left: 8px solid #6fc16f;
   content: '';
   position: absolute;
   top: 0px;
   left: 300px;
  
}
    
    
    
    
    .button101{
 width: 280px;
   height: 26px;
   background: #61ade7;
   border: 0px solid #61ade7;
   position: relative;
   color:White;
   padding-left:10px;
}

.button101::before{
   width: 0;
   height: 0;
   border: 13px solid transparent;
   border-left: 8px solid #61ade7;
   content: '';
   position: absolute;
   top: 0px;
   left: 280px;
   
  
}
.button101::after{
 
 width: 0;
   height: 0;
   border: 13px solid transparent;
   border-left: 8px solid #61ade7;
   content: '';
   position: absolute;
   top: 0px;
   left: 280px;


}
.arrow-right {
  width: 0; 
  height: 0; 
  border-top: 5px solid transparent;
  border-bottom: 5px solid transparent;
  border-left: 5px solid #393939;
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
            <%--</br>--%>
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
     <%--       </br>--%>
             <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-md-1 col-sm-2 col-xs-3">
                <img src="../images/GTI.png" height="100px" class="re" />
                </div>
                <div class="col-md-9  col-sm-8 col-xs-7">
                     <br/>
                         <h3 >
                             Globe Trotter (Individual) - Travel Insurance Cover</h3>
                         
                </div>
                <div class="col-xs-1">
                </div>
                 
            </div>
             <br/><br/>

               <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                     Sri Lanka Insurance Globe Trotter is a comprehensive travel insurance package designed to make your travel experience as secure and trouble-free as possible. The policy caters to a variety of travelling purposes (excepts medical treatments and sports) matching with both business and personal needs of individuals. Click  <a data-toggle="modal" href="#myModal2">here</a> to view the approved list of travel purposes.
<br /><br /><strong>The special traveller-oriented features of Globe Trotter include,</strong> 
                </div>
                <div class="col-xs-1">
                </div> </div>

                
                <br />
                 <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                     <%--<div class="button102"><strong>Special features of Globe Trotter Individual</strong></div><br /> --%>
                     
                     
                     <ul>
                    <li>Hassle-free online purchasing</li>
                    <li>Insurance cover provided up to six months and up to 70 years without medical examinations</li>
                    <li>Wide range of benefits for affordable cost while covering all countries </li>
                    <li>Direct and reimbursement claims settlement  options</li>
                    <li>24X7 worldwide claims settlement service</li>
                    <li>Insurance policy meets the requirements of the Visa office</li>
                    <li>50% discount for children below 17 years travelling with immediate parents</li>
                     </ul>
                </div>
                <div class="col-xs-1">
                </div> </div>
                <br />
                <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10" style="font-size:1.0em">
                     <strong>The policy covers a wide range of occurences during travel period and the benefits include,</strong>
                </div>
                <div class="col-xs-1">
                </div> </div>

                <br />

                 <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                     
                </div>
                <div class="col-xs-1">
                </div> </div>

                <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                     <div class="button101"><strong>1. Health cover</strong></div>
                     <br />

                     <ul>
                    <li>Outpatient treatment, inpatient treatment, medical aid,  emergency transportation for medical attention</li>
                    <li>Dental treatments for acute anesthetic treatment and covered accidents</li>
                    <li>Extra cost for medically necessary and prescribed transportation, accompanying person on medical necessity &  transporting mortal remains</li>
                    <li>Maximum of 30 days to continue appropriate treatments within Sri Lanka  for covered injury/sickness</li>
                    <li>Hospital daily allowance for covered treatment and for hospitalization more than two days</li>                    
                     </ul>
                     <span>
                     (Medical expenses/services for pre existing conditions & travelling abroad for medical treatments, Non emergency medical treatments, cosmetic treatments, treatments & checkups of pregnancy, mental & psychiatric disorders are generally excluded)
                     </span>
                </div>
                <div class="col-xs-1">
                </div> </div>
                <br />
                <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                      <div class="button101"><strong>2. Total Loss of /delayed baggage</strong></div>
                     <br />
                     <ul>
                    <li>Reimbursement of total loss of baggage by a carrier on market value less reasonable amount for usage, wear & tear (subject to police report and other reports concerning the loss)</li>
                    <li>Costs for necessary emergency purchases of essential items for temporary loss due to delay by carrier of checked baggage </li>

                     </ul>

                     <span>
                     (Valuable, money, securities, tickets, partial losses of baggage, and loss due to delay, detention, confiscation or distribution by custom or public authorities are not covered)
                     </span>

                </div>
                <div class="col-xs-1">
                </div> </div>

             <br/>
             <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                      <div class="button101"><strong>3. Loss of passport</strong></div>
                     <br />
                     <ul>
                    <li> Reasonable and necessarily incurring reproduction cost of passport (subject to reporting to the police immediately)</li>
                     </ul>

                     <span>
                     Losses due to left unattended, delay, detention, confiscation or distribution by custom or public authorities are not covered.
                     </span>

                </div>
                <div class="col-xs-1">
                </div> </div>


                <br/>
             <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                      <div class="button101"><strong>4. Financial emergency assistance</strong></div>
                     <br />
                     <ul>
                    <li>Insured person getting into a financial  emergency due to theft of travel funds kept in the personal custody of the insured (subject to reporting to police immediately)</li>
                     </ul>

                     <span>
                     Note: Loss of travelers cheques not reported to the local branches/agents immediately, Shortage, loss not reported to us within 30 days from the date of incident is not covered
                     </span>

                </div>
                <div class="col-xs-1">
                </div> </div>

                <br/>
             <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                      <div class="button101"><strong>5. Personal Liability </strong></div>
                     <br />
                     <ul>
                    <li>Insured person become legally liable to third party under statutory liability provisions in civil law for accidental death or injury and damage to property </li>
                     </ul>

                     <span>
                     (Liability to insured's employees, family members, relations, travelling companion & personal, colleague , liability arising from use or ownership of 	motor vehicle or animals, land or building,  willful or malicious or unlawful acts , insanity, use of alcohol, drugs)
                     </span>

                </div>
                <div class="col-xs-1">
                </div> </div>

                <br/>
             <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                      <div class="button101"><strong>6. Personal Accident cover </strong></div>
                     <br />
                     <ul>
                    <li>Accidental death or subsequent disablement of insured person on trip abroad </li>
                     </ul>

                     <span>
                     (Natural death, accidents due to mental disorders or disturbances, death or injury due to curative measures not followed by accidents are not covered)
                     </span>

                </div>
                <div class="col-xs-1">
                </div> </div>

                 <br/>
             <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                     <strong>Please refer policy booklet for more detailed coverage, exclusions, conditions and claim procedures.</strong>
                </div>
                <div class="col-xs-1">
                </div> </div>

                <br /><br /><br />

              <asp:Panel ID="Panel2" runat="server" Visible="True">
                <div class="table-responsive">
                   <%-- <table class="table table-bordered">--%>
                    <table class="table"  style="border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                    
                    <tr>

                            <td colspan="7" 
                                style="width:40%; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White;">
                                <strong><span style=" font-size:1.1em">Plans for Worldwide Travels<br />
                                </span> </strong>Following policies are valid for all the countries.
                            </td>
                            </tr>
                              <tr>
                        <td colspan="7" style="width:40%; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                            &nbsp;
                        </td>
                        </tr>

                    <tr>


                     <td style="width:40%; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White; background-color:#FFFFFF; color:#FFFFFF;" 
                            rowspan="2">
                            <strong>Plans for worldwide travels</strong></td>
                            <td colspan="6" style="text-align:center; width:60%; background-color:#61ade7; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Worldwide</strong></td>
                            
                    
                    </tr>

                    <tr>


                            <td colspan="2" style="text-align:center; width:20%; background-color:#61ade7; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Global 500</strong></td>
                            <td  colspan="2" style="text-align:center; width:20%; background-color:#61ade7; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Global 100</strong></td>
                            <td  colspan="2" style="text-align:center; width:20%; background-color:#61ade7; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                               <strong>Global 50</strong>
                            </td>
                    
                    </tr>

                    <tr>
                     <td style="width:40%; border-bottom-color:White; border-left-color:White; border-right-color:White; color:#FFFFFF; border-top-color:White; background-color:#6fc16f;"> <strong>Benefits</strong>
                            </td>
                            <td colspan="1" style="text-align:center; width:10%; background-color:#6fc16f; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Sum Insured<br />USD</strong></td>
                            <td  colspan="1" style="text-align:center; width:10%; background-color:#6fc16f; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Excess<br />USD</strong></td>
                                <td colspan="1" style="text-align:center; width:10%; background-color:#6fc16f; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Sum Insured<br />USD</strong></td>
                            <td  colspan="1" style="text-align:center; width:10%; background-color:#6fc16f; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Excess<br />USD</strong></td>

                                <td colspan="1" style="text-align:center; width:10%; background-color:#6fc16f; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Sum Insured<br />USD</strong></td>
                            <td  colspan="1" style="text-align:center; width:10%; background-color:#6fc16f; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Excess<br />USD</strong></td>
                            
                    
                    </tr>

                     <tr>
                            <td style="width:40%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                a) Medical expenses including:
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-right">
                               500,000 
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-right">
                                100
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-right">
                               100,000
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-right">
                                100
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-right">
                                50,000 
                            </td>
                             <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-right">
                                100
                            </td>
                        </tr>

                        
                         <tr>
                            <td style="width:40%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                &nbsp;&nbsp;&nbsp;&nbsp;i) Dental treatment
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                              250 
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                                100
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                               250
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                                100
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                                250
                            </td>
                             <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                                100
                            </td>
                        </tr>
                         <tr>
                            <td style="width:40%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                &nbsp;&nbsp;&nbsp;&nbsp;ii) Transport of Mortal remains or burial at local place
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                               7,000
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                                100
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                               7,000
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                                100
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                                7,000
                            </td>
                             <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                                100
                            </td>
                        </tr>

                        <tr>
                            <td style="width:40%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                b) Hospital Daily Allowance
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-center">
                              30/day for 20 days 
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-center">
                                48 hrs
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-center">
                               30/day for 20 days 
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-center">
                                48 hrs
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-center">
                                30/day for 20 days 
                            </td>
                             <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-center">
                                48 hrs
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                c) Total loss of Checked Baggage
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                               1,200
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                -
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                               1,200
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                -
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                                1,200
                            </td>
                             <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                -
                            </td>
                        </tr>

                        <tr>
                            <td style="width:40%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                d) Delay of Checked Baggage
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                              150 
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                12 hrs 
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                               150
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                12 hrs 
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                                150
                            </td>
                             <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                12 hrs 
                            </td>
                        </tr>

                        <tr>
                            <td style="width:40%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                e) Loss of Passport
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                               250
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                -
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                               250
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                -
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                                250
                            </td>
                             <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                -
                            </td>
                        </tr>

                        <tr>
                            <td style="width:40%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                f) Hijacked distress Allowance
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-center">
                              125 / day for 7 days 
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-center">
                                12 hrs
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-center">
                               125 / day for 7 days 
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-center">
                                12 hrs
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-center">
                                125 / day for 7 days 
                            </td>
                             <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                class="text-center">
                                12 hrs
                            </td>
                        </tr>

                         <tr>
                            <td style="width:40%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                g) Financial Emergency Assistance
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                               250
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                -
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                               250
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                -
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                                250
                            </td>
                             <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                -
                            </td>
                        </tr>

                         <tr>
                            <td style="width:40%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                h) Personal Accident during travel
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                              25,000  
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                -
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                               25,000 
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                - 
                            </td>
                            <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                                25,000 
                            </td>
                             <td style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                -
                            </td>
                        </tr>

                        <tr>
                            <td style="width:40%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                i) Personal liability
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                               50,000 
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                -
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                               50,000 
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                -
                            </td>
                            <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-right">
                                50,000 
                            </td>
                             <td style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White" 
                                 class="text-center">
                                -
                            </td>
                        </tr>
                        
                    

                    <tr>
                        <td colspan="7" style="width:40%; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                            &nbsp;
                        </td>
                        </tr>
                        <tr>

                            <td colspan="7" 
                                style="width:40%; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White;">
                                <strong><span style=" font-size:1.1em">Plans for specific regions
                                <br />
                                </span> </strong>Following policies are valid for specified regions only.
                            </td>
                            </tr>
                              <tr>
                        <td colspan="7" style="width:40%; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                            &nbsp;
                        </td>
                        </tr>

                        <tr>
                            <td style="width:40%; background-color:#FFFFFF; color:#FFFFFF; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                            
                            </td>
                            <td colspan="2" 
                                style="text-align:center; width:20%; background-color:#61ade7; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Standard 100<br /> Excluding USA, Canada</strong></td>
                            <td colspan="2" 
                                style="text-align:center; width:20%; background-color:#61ade7; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Standard 50<br /> Excluding USA, Canada</strong></td>
                            <td colspan="2" 
                                style="text-align:center; width:20%; background-color:#61ade7; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Asia 25<br /> Asia Excluding Japan</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; border-bottom-color:White; border-left-color:White; border-right-color:White; color:#FFFFFF; border-top-color:White; background-color:#6fc16f;">
                                <strong>Benefits</strong>
                            </td>
                            <td colspan="1" 
                                style="text-align:center; width:10%; background-color:#6fc16f; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Sum Insured<br />USD</strong></td>
                            <td colspan="1" 
                                style="text-align:center; width:10%; background-color:#6fc16f; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Excess<br />USD</strong></td>
                            <td colspan="1" 
                                style="text-align:center; width:10%; background-color:#6fc16f; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Sum Insured<br />USD</strong></td>
                            <td colspan="1" 
                                style="text-align:center; width:10%; background-color:#6fc16f; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Excess<br />USD</strong></td>
                            <td colspan="1" 
                                style="text-align:center; width:10%; background-color:#6fc16f; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Sum Insured<br />USD</strong></td>
                            <td colspan="1" 
                                style="text-align:center; width:10%; background-color:#6fc16f; color:#FFFFFF; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                <strong>Excess<br />USD</strong></td>
                        </tr>
                        <tr>
                            <td style="width:40%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                a) Medical expenses including:
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                100,000
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                75
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                50,000&nbsp;
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                75
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                25,000</td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                75
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                &nbsp;&nbsp;&nbsp;&nbsp;i) Dental treatment
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                250
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                75
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                250
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                75
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                250
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                75</td>
                        </tr>
                        <tr>
                            <td style="width:40%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                &nbsp;&nbsp;&nbsp;&nbsp;ii) Transport of Mortal remains or burial at local place
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                7,000
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                75
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                7,000
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                75</td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                7,000
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                75</td>
                        </tr>
                        <tr>
                            <td style="width:40%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                b) Hospital Daily Allowance
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                30/day for 20 days
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                48 hrs
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                30/day for 20 days
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                48 hrs
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                30/day for 20 days
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                48 hrs
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                c) Total loss of Checked Baggage
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                1,200
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                -
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                1,200
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                -
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                1,200
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                -
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                d) Delay of Checked Baggage
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                150
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                12 hrs
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                150
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                12 hrs
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                150
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                12 hrs
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                e) Loss of Passport
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                250
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                -
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                250
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                -
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                250
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                -
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                f) Hijacked distress Allowance
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                125 / day for 7 days
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                12 hrs
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                125 / day for 7 days
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                12 hrs
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                125 / day for 7 days
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                12 hrs
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                g) Financial Emergency Assistance
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                250
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                -
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                250
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                -
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                250
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                -
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                h) Personal Accident during travel
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                25,000
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                -
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                25,000
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                -
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                15,000
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#fff; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                -
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                i) Personal liability
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                50,000
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                -
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                50,000
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                -
                            </td>
                            <td class="text-right" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
                                30,000
                            </td>
                            <td class="text-center" 
                                style="width:10%; background-color:#e7e7e8; border: 1px solid; border-bottom-color:White; border-left-color:White; border-right-color:White; border-top-color:White">
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

            <br />

            <div class="row">
                          <a href="Documents/GTI.pdf" target="_blank" ><input type="button" class="btn btn-default" value="Policy Booklet" style="background-color:#61ade7 !important; border-color:#61ade7 !important; font-weight:bold; width:15%" /></a>
                          <a href="Documents/GTIClaim.pdf" target="_blank" ><input type="button" class="btn btn-default" value="Claim Procedure" style="background-color:#61ade7 !important; border-color:#61ade7 !important; font-weight:bold; width:15%" /></a>
                           <a data-toggle="modal" href="#myModal" ><input type="button" class="btn btn-default" value="Service Provider" style="background-color:#61ade7 !important; border-color:#61ade7 !important; font-weight:bold; width:15% " /></a>
                       </div>

            <br/>

             <div class="modal fade" id="myModal" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h5 class="modal-title">
                                   <strong> Contact Details</strong></h5>
                            </div>
                            <div class="modal-body">
                                <strong>Paramount Healthcare Management Pvt. Ltd</strong> 
                                <br />
                                Travel Health Dept.<br />
                                401 - 402 Sumer Plaza,<br />
                                Marol Maroshi Road, Marol,<br />
                                Andheri (East)<br />
                                Mumbai 400 059.
                                <br /><br />

                                Tel : +91 22 4000 4207/4219<br />
Fax : +91 22 4000 4280<br />
USA Toll Free : 001 866 978 5205<br />
Dedicated Helpline Number for SLIC members : +91 22 4090 8314<br />
<br />
Email Id : travelhealth@paramount.healthcare
<br />
Whatsapp No +91 7718806681 (the message should include following details) <br />
Insured Name:<br />
Insurance Co. Name:<br />
Insurance Policy No.:<br />
Country Where Traveled:<br />
Type of Assistance Required:<br />
Email id of insured:<br />
<br />
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
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h5 class="modal-title">
                                   <strong>Accepted travelling purposes for Globe Trotter.</strong></h5>
                            </div>
                            <div class="modal-body">

                                <ul>
                                <li>Holiday</li>
                                <li>Exhibition</li>
                                <li>Fair</li>
                                <li>Study</li>
                                <li>Conference</li>
                                <li>Seminar</li>
                                <li>Training</li>
                                <li>Business</li>
                                </ul>
                                
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary btn-xs" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>

            <br/>
            <br/>
            </div>

            

       </div>

</asp:Content>

