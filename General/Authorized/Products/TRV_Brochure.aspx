<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="TRV_Brochure.aspx.cs" Inherits="General_Authorized_Products_TRV_Brochure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <style>
       
             .btn-default, .btn-default:focus, .btn-default.active, .btn-default.active:focus, .btn-default:hover {
    color: #fff !important;
}
        
        .modal-backdrop{
            z-index: 0;
        }

        .test22 {
            font-size: 100%;
            font-family: Tahoma;
        }

        .divWaiting {
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

        .table-bordered {
            border: 1px solid #FFFFFF !important;
        }


        .td {
            border: 1px solid #FFFFFF !important;
            border-top: 1px solid white !important;
            border-bottom: 1px solid white !important;
            border-right: 1px solid white !important;
            border-left: 1px solid white !important;
        }

        .G500 {
            background-color: #d2dcf0;
            color: Black;
            border: 1px solid;
            text-align: center;
        }

        .G100 {
            text-align: center;
            background-color: #ede4ed;
            color: Black;
            border: 1px solid;
        }

        .G50 {
            background-color: #f7d5ae;
            color: Black;
            text-align: center;
            border: 1px solid;
        }

        .S100 {
            background-color: #f9f8ce;
            color: Black;
            border: 1px solid;
            text-align: center;
        }

        .S50 {
            background-color: #dbe8d0;
            color: Black;
            border: 1px solid;
            text-align: center;
        }

        .A25 {
            background-color: #c7d7e9;
            color: Black;
            border: 1px solid;
            text-align: center;
        }

        .GTBenefit {
            background-color: #b9c4d9;
            color: Black;
            border: 1px solid;
        }


        .button102 {
            width: 300px;
            height: 26px;
            background: #6fc16f;
            border: 0px solid #6fc16f;
            position: relative;
            color: White;
            padding-left: 10px;
        }

            .button102::before {
                width: 0;
                height: 0;
                border: 13px solid transparent;
                border-left: 8px solid #6fc16f;
                content: '';
                position: absolute;
                top: 0px;
                left: 300px;
            }

            .button102::after {
                width: 0;
                height: 0;
                border: 13px solid transparent;
                border-left: 8px solid #6fc16f;
                content: '';
                position: absolute;
                top: 0px;
                left: 300px;
            }




        /*.button101 {
            width: 280px;
            height: 26px;
            background: #61ade7;
            border: 0px solid #61ade7;
            position: relative;
            color: White;
            padding-left: 10px;
        }*/

            .button101 {
            width: 280px;
            height: 26px;
            background: #01aebc;
            border: 0px solid #01aebc;
            position: relative;
            color: White;
            padding-left: 10px;
        }

        /*#01aebc*/


            .button101::before {
                width: 0;
                height: 0;
                border: 13px solid transparent;
                border-left: 8px solid #01aebc;
                content: '';
                position: absolute;
                top: 0px;
                left: 280px;
            }

            .button101::after {
                width: 0;
                height: 0;
                border: 13px solid transparent;
                border-left: 8px solid #01aebc;
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

        .auto-style1 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 83.333%;
            left: 0px;
            top: 0px;
            padding-left: 12px;
            padding-right: 12px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container" id="main-container">

           <link href="/css/modal.css" rel="stylesheet" />

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
          <%--  </br>--%>
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
             <div class="row">
                 <div class="col-xs-1">
                 </div>
                 <div class="col-md-1 col-sm-2 col-xs-3">
                     <img src="/images/TravelLogo.png" height="100px" class="re" />
                 </div>
                 <div class="col-md-9  col-sm-8 col-xs-7">
                     
                     <h3>Travel Protect Policy</h3>

                 </div>
                 <div class="col-xs-1">
                 </div>

             </div>
            <br />
            <br />
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    Sri Lanka Insurance Corporation General Ltd offers a comprehensive insurance package for travelers who visit abroad for various purposes except medical treatments and sports. Click  <a data-toggle="modal" href="#myModal2">here</a> to view the approved list of travel purposes.
                    <br />
                    <br />
                    <p class="MsoNormal">
                        <b><span>Special features of our Travel Protect policy
                            <o:p></o:p>
                        </span></b>
                    </p>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ul>
                        <li>Children below 18 years are charged 50% of normal rate when travelling with parents.</li>
                        <li>No medical test required up to age 80 </li>
                        <li>24x7 emergency assistance </li>
                        <li>Special home safety cover up to 30 days from the departure</li>
                        <li>Special golf cover (only for golf equipment) for golf players travelling overseas </li>
                        <li>Hassle-free online purchasing </li>
                    </ul>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10" style="font-size: 1.0em">
                    <strong>The policy provides benefits for any occurrence during travel period and as summarized below.</strong>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <div class="button101">
                        <strong>Health Benefits </strong>
                    </div>
                    <br />
                    <ol>
                        <li>Emergency accident and medical expenses incurred overseas </li>

                        <ul>
                            <li>Outpatient treatment, inpatient treatment, medical aid, emergency transportation for medical attention.</li>
                        </ul>
                        <br />
                        <span><strong>Note: Non-emergency medical treatments, cosmetic treatments, treatments & checkups of pregnancy, mental & psychiatric disorders are generally excluded)</strong></span>
                        <li>Follow up treatment in Sri Lanka upon return </li>

                        <ul>
                            <li>Maximum of 30 days to continue appropriate treatments within Sri Lanka  for covered injury/sickness</li>
                        </ul>
                        <li>Emergency medical evacuation </li>
                        <li>Repatriation of mortal remains </li>
                        <ul>
                            <li>Extra cost for medically necessary and prescribed transportation, accompanying person on medical necessity & transporting mortal remains
                            </li>
                        </ul>
                        <li>Hospital daily allowance for covered treatment and for hospitalization more than two days</li>
                        <li>Emergency telephone charges </li>
                    </ol>

                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <div class="button101">
                        <strong>Accident Benefits  </strong>
                    </div>
                    <br />
                    <ol>
                        <li>Accidental death and permanent dismemberment (ADPD)</li>
                        <ul>
                            <li>Accidental death or subsequent disablement of insured person on trip abroad.
                            </li>
                        </ul>
                        <br />
                        <span><strong>Note: Natural death, accidents due to mental disorders or disturbances, 
                            death or injury due to curative measures not followed by accidents is not covered.</strong></span>
                        <li>ADPD common carrier double cover </li>
                        <li>Local burial expenses </li>
                        <li>Child education protection </li>
                        <li>Compassionate visit </li>
                        <li>Return of minor children </li>
                    </ol>

                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="auto-style1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <div class="button101">
                        <strong>Personal Property Benefits </strong>
                    </div>
                    <br />
                    <ol>
                        <li>Reimbursement of total loss of baggage by a carrier on market value less reasonable amount for usage, 
                             wear & tear (subject to police report and other reports concerning the loss)</li>
                        <li>Costs for necessary emergency purchases of essential items for temporary loss due to delay by carrier of checked baggage </li>
                        .<br />
                        <span><strong>Note: Valuable, money, securities, tickets, partial losses of baggage, and loss due to delay, detention, confiscation or 
                            distribution by custom or public authorities are not covered)</strong></span>
                        <li>Loss of passport and travel documents, reasonable and necessarily incurring reproduction cost of passport 
                            (subject to reporting to the police immediately)</li>
                        <br />
                        <span><strong>Note: Losses due to left unattended, delay, detention, confiscation or distribution by custom or public authorities are not covered</strong></span>
                    </ol>

                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <div class="button101">
                        <strong>Travel Inconvenience Benefits  </strong>
                    </div>
                    <br />
                    <ol>
                        <li>Insured person getting into a financial emergency due to theft of travel funds kept in the 
                            personal custody of the insured (subject to reporting to police immediately)</li>
                        <li>Trip postponement or cancellation, curtailment, trip delay, flight diversion, 
                            overbooked flight, missed departure is covered. </li>
                        <br />
                        <span><strong>Note: Loss of travelers cheques not reported to the local branches/agents immediately, 
                            Shortage, loss not reported to us within 30 days from the date of incident is not covered.</strong></span>
                    </ol>

                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <div class="button101">
                        <strong>Additional Benefits  </strong>
                    </div>
                    <br />
                    <ol>
                        <li>Insured person become legally liable to third party under statutory liability 
                            provisions in civil law for accidental death or injury and damage to property. </li>
                        <li>Hijack </li>
                        <li>Golf Advantage </li>
                        <ul>
                            <li>Loss or damage of Golf Equipment     </li>
                            <li>Special prize for hole-in-one     </li>
                        </ul>
                        <%--<p class="auto-style1" style="mso-add-space: auto; mso-list: l0 level1 lfo1; mso-layout-grid-align: none; text-autospace: none">
                                <![if !supportLists]>
									<span>-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    </span>
                                <![endif]>
									<span>
                                        <o:p></o:p>
                                    </span>

                            </p>
                            <p class="auto-style1" style="mso-add-space: auto; mso-list: l0 level1 lfo1; mso-layout-grid-align: none; text-autospace: none">
                                <![if !supportLists]>
									<span>-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    </span>
                                <![endif]>
                                    <span>Special prize for hole-in-one </span>
                            </p>--%>

                        <%-- </li>--%>


                        <li>Home safety cover
                            <ul>
                                <li>Special cover for insured’s home property while travelling overseas covering damages from fire, explosion, and water damage.
                                </li>

                            </ul>

                            <li>Legal fees</li>
                        <li>Rental vehicle excess </li>
                        <li>Automatic extension of travel period </li>
                        <li>24 hour emergency travel and medical assistance</li>
                        <ul>
                            <li>Pre-trip assistance service </li>
                            <li>Medical servicee</li>
                            <li>Emergency ticket service </li>
                            <li>General assistance service </li>
                            <li><span>Evacuation and Repatriation service</span></li>
                            <li><span>Baggage service</span></li>
                            <li><span>Legal service</span></li>
                        </ul>
                    </ol>
                    <div><strong>Please refer policy document for more detailed coverage, exclusions, conditions and claim procedures.</strong></div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
            <%--         <a href="Documents/TRV_PB.pdf" target="_blank">
                    <input type="button" class="btn btn-default" value="Policy Booklet" style="background-color: #61ade7 !important; border-color: #61ade7 !important; font-weight: bold; width: 18%; left: 2px; top: -1px;" /></a>
                <a href="Documents/TRV_Claims.pdf" target="_blank">
                    <input type="button" class="btn btn-default" value="Claim Procedure" style="background-color: #61ade7 !important; border-color: #61ade7 !important; font-weight: bold; width: 18%" /></a>
                <a data-toggle="modal" href="#myModal">
                    <input type="button" class="btn btn-default" value="Service Provider" style="background-color: #61ade7 !important; border-color: #61ade7 !important; font-weight: bold; width: 18%" /></a>
                <a href="TRV_Proposal.aspx" target="_blank">
                    <input type="button" class="btn btn-default" value="Buy Policy" style="background-color: #61ade7 !important; border-color: #61ade7 !important; font-weight: bold; width: 18%" /></a>--%>
          

                         <a href="Documents/TRV_PB.pdf" ><input type="button" class="btn btn-default" value="Policy Booklet" style="background-color:#202340 !important; border-color:#202340 !important; font-weight:bold; /*width:17%*/ " /></a>
                       <a href="Documents/TRV_Claims.pdf"><input type="button" class="btn btn-default" value="Claim Procedure" style="background-color:#202340 !important; border-color:#202340 !important; font-weight:bold; /*width:17%*/" /></a>
                        <a data-toggle="modal" href="#myModal"><input type="button" class="btn btn-default" value="Service Provider" style="background-color:#202340 !important; border-color:#202340 !important; font-weight:bold; /*width:17%*/" /></a>
                        <a href="TRV_Proposal_New.aspx"><input type="button" class="btn btn-default" value="Buy Policy" style="background-color:#202340 !important; border-color:#202340 !important; font-weight:bold; /*width:17%*/" /></a>
     <%--                   <a href="Documents/AMP_Policy_book.pdf" target="_blank" ><input type="button" class="btn btn-default" value="Policy Book" style="background-color:#202340 !important; border-color:#202340 !important; font-weight:bold; /*width:17%*/" /></a>--%>


                </div>
            </div>
            <br/>
            <% 
                if (dtconts.Rows.Count > 0)
                {
            %>
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
                                <strong><%=dtconts.Rows[0].ItemArray[0].ToString() %></strong> 
                                <br />
                                <%=dtconts.Rows[0].ItemArray[1].ToString() %><br />
                                <%=dtconts.Rows[0].ItemArray[2].ToString() %><br />
                                <%=dtconts.Rows[0].ItemArray[3].ToString() %><br />
                                <%=dtconts.Rows[0].ItemArray[4].ToString() %><br />
                                <%=dtconts.Rows[0].ItemArray[5].ToString() %>
                                <br /><br />

                               <%=dtconts.Rows[0].ItemArray[6].ToString() %><br />
                                <%=dtconts.Rows[0].ItemArray[9].ToString() %><br />
                                <%=dtconts.Rows[0].ItemArray[10].ToString() %><br />
                                
 
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary btn-xs" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
           <%
               }
               if (dtPurposes.Rows.Count > 0)
               {
           %>

            <div class="modal fade" id="myModal2" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h5 class="modal-title">
                                   <strong>Accepted travelling purposes for Travel Protect.</strong></h5>
                            </div>
                            <div class="modal-body">

                                <ul>
                                <%for (int a = 0; a < dtPurposes.Rows.Count; a++)
                                        {
                                 %>
                                <li><%=dtPurposes.Rows[a].ItemArray[1].ToString() %></li>
                                 <%
                                        }
                                 %>
                                <%--<li>Exhibition</li>
                                <li>Fair</li>
                                <li>Study</li>
                                <li>Conference</li>
                                <li>Seminar</li>
                                <li>Training</li>
                                <li>Business</li>--%>
                                </ul>
                                
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary btn-xs" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            <%
                }
            %>
        </div>

    </div>
</asp:Content>

