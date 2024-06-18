<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="TRV_Questionnaire2.aspx.cs" Inherits="General_Authorized_Products_TRV_Questionnaire2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link href="/css/modal.css" rel="stylesheet" />
    <style type="text/css">
    
        .q_box {
            color: #FF5050;
            height: 100px;
            width: 100%;
            border: 1px solid #FF0000;
        }

        .success_ans {
            /*border: 1px solid #31DA83;*/
            background: url("/css/smoothness/images/bk.png") repeat-x;
        }

        .failure_ans {
            /*border: 1px solid #31DA83;*/
            background: url("/css/smoothness/images/bks.png") repeat-x;
        }

        .success_ans h3 {
            border: 1px solid #31DA83;
        }

        .errorr {
            color: #FF0000;
            font-size: 12px;
            font-weight: normal;
            font-family: Tahoma;
            color: #FF0000;
        }


        #overlay {
            text-align: center;
            background-color: rgba(0, 0, 0, 0.3);
            z-index: 900;
            position: absolute;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            display: none;
        }

        #pnl {
            background-color: #FFF;
            z-index: 901;
            position: relative;
            min-height: 100px;
            font-size: 12px;
            display: block;
        }

        #pnl1 {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }

        #pnl2 {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }

        #pnl3 {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }

        #pnl3_1 {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }

        #pnl3_2 {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }

        #pnl3_3 {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }

        #pnl3_4 {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }

        #pnl3_5 {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }

        #pnl3_6 {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }

        #pnl3_7 {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }

        #pnl3_8 {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }

        #pnl3_9 {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }

        #HeaderDiv {
            height: 100%;
            width: 100%;
            background-color: Blue;
        }

        .ModalPopupBG {
            background-color: #000;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .HellowWorldPopup {
            min-width: 750px;
            min-height: 550px;
            background: white;
        }

        p {
            font-size: 13px;
            font-family: Tahoma;
            color: #565656;
        }

        ul {
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }

        .succ {
            font-size: 11px;
            font-family: Tahoma;
            color: #565656;
            font-weight: bold;
        }

        @font-face {
            font-family: 'Sandaya12';
            src: url('sa_____.TTF');
        }

        @media (min-width: 300px) {
            #Testt_1 {
                display: block;
            }

            #Testt_3 {
                display: none;
            }

            #Testt_5 {
                display: none;
            }
        }

        @media (min-width: 400px) {
            #Testt_1 {
                display: none;
            }

            #Testt_3 {
                display: block;
            }

            #Testt_5 {
                display: none;
            }
        }


        @media (min-width: 500px) {
            #Testt_1 {
                display: none;
            }

            #Testt_3 {
                display: block;
            }

            #Testt_5 {
                display: none;
            }
        }

        @media (min-width: 600px) and (min-width: 699px) {
            #Testt_1 {
                display: none;
            }

            #Testt_3 {
                display: none;
            }

            #Testt_5 {
                display: block;
            }
        }


        @media (min-width: 700px) {
            #Testt_1 {
                display: none;
            }

            #Testt_3 {
                display: none;
            }

            #Testt_5 {
                display: block;
            }
        }
    </style>
    <script type="text/javascript" language="javascript">

        window.onload = function () {

        }

        function check_all_ok() {
            var current = $accordion.accordion("option", "active");
            maximum = $accordion.find("h3").length;

            for (i = current; i <= maximum + 1; i++) {

                $($("#accordion > h3")[i]).addClass("ui-state-disabled");
            }

        }


        $(document).ready(function () {


            $("[id$='Rdo_gudHlth_Y']").click(function () {
                openNextAccordionPanel();
                $("#wnl").hide();
                $("#bnl").show();
            });

            $("[id$='Rdo_gudHlth_N']").click(function () {
                check_all_ok();
                $("#wnl").show();
                $("#bnl").hide();

            });

            $("[id$='Rdo_Deformity_N']").click(function () {
                openNextAccordionPanel();
                $("#wnl1").hide();
                $("#bnl1").show();
            });
            $("[id$='Rdo_Deformity_Y']").click(function () {
                check_all_ok();
                $("#wnl1").show();
                $("#bnl1").hide();
            });

             $("[id$='Rdo_gvmnt_Y']").click(function () {
                openNextAccordionPanel();
                $("#wn2").hide();
                $("#bn2").show();
            });

            $("[id$='Rdo_gvmnt_N']").click(function () {
                check_all_ok();
                $("#wn2").show();
                $("#bn2").hide();

            });

            $("[id$='Rdo_Surgical_Y']").click(function () {
                check_all_ok();
                $("#wnl2").show();
                $("#bnl2").hide();
            });

            $("[id$='Rdo_Surgical_N']").click(function () {
                openNextAccordionPanel();
                $("#wnl2").hide();
                $("#bnl2").show();
            });


            $("[id$='Rdo_can_N']").click(function () {
                openNextAccordionPanel();
                $("#wnl3").hide();
                $("#bnl3").show();
            });
            $("[id$='Rdo_can_Y']").click(function () {
                check_all_ok();
                $("#wnl3").show();
                $("#bnl3").hide();
            });



            $("[id$='Rdo_fem_N']").click(function () {
                openNextAccordionPanel();
                $("#wnl4").hide();
                $("#bnl4").show();
            });
            $("[id$='Rdo_fem_Y']").click(function () {
                check_all_ok();
                $("#wnl4").show();
                $("#bnl4").hide();
            });


            $("[id$='Rdo_aids_N']").click(function () {
                openNextAccordionPanel();
                $("#wnl5").hide();
                $("#bnl5").show();
            });
            $("[id$='Rdo_aids_Y']").click(function () {
                check_all_ok();
                $("#wnl5").show();
                $("#bnl5").hide();
            });


            $("[id$='Rdo_alco_N']").click(function () {
                openNextAccordionPanel();
                $("#wnl6").hide();
                $("#bnl6").show();
            });
            $("[id$='Rdo_alco_Y']").click(function () {
                check_all_ok();
                $("#wnl6").show();
                $("#bnl6").hide();
            });


            $("[id$='Rdo_scan_N']").click(function () {
                openNextAccordionPanel();
                $("#wnl7").hide();
                $("#bnl7").show();
            });
            $("[id$='Rdo_scan_Y']").click(function () {
                check_all_ok();
                $("#wnl7").show();
                $("#bnl7").hide();
            });


            $("[id$='Rdo_weigh_N']").click(function () {
                openNextAccordionPanel();
                $("#wnl8").hide();
                $("#bnl8").show();
            });
            $("[id$='Rdo_weigh_Y']").click(function () {
                check_all_ok();
                $("#wnl8").show();
                $("#bnl8").hide();
            });


            $("[id$='Rdo_death_N']").click(function () {
                openNextAccordionPanel();
                $("#wnl9").hide();
                $("#bnl9").show();
            });

            $("[id$='Rdo_death_Y']").click(function () {
                check_all_ok();
                $("#wnl9").show();
                $("#bnl9").hide();
            });


            $("[id$='Rdo_occu_N']").click(function () {
                openNextAccordionPanel();
                $("#wnl10").hide();
                $("#bnl10").show();
            });
            $("[id$='Rdo_occu_Y']").click(function () {
                check_all_ok();
                $("#wnl10").show();
                $("#bnl10").hide();
            });


            $("[id$='Rdo_declined_N']").click(function () {
                openNextAccordionPanel();
                $("#wnl11").hide();
                $("#bnl11").show();
            });
            $("[id$='Rdo_declined_Y']").click(function () {
                check_all_ok();
                $("#wnl11").show();
                $("#bnl11").hide();
            });


            $("[id$='Rdo_slic_N']").click(function () {
                openNextAccordionPanel();
                $("#wnl12").hide();
                $("#bnl12").show();
            });
            $("[id$='Rdo_slic_Y']").click(function () {
                check_all_ok();
                $("#wnl12").show();
                $("#bnl12").hide();
            });
        });


    </script>
    <style type="text/css">
        .HeaderActive {
            background-color: #009ee1;
            padding: 10px 30px 10px 15px;
            cursor: pointer;
            position: relative;
            margin-top: 10px;
        }

        .HeaderNotActive {
            background-color: #949495;
            color: white;
            padding: 0px 0px 0px 0px;
            cursor: pointer;
            position: relative;
            margin-top: 10px;
        }

        .auto-style1 {
            left: 0px;
            top: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    

    <style>
 .ui-state-active, .ui-widget-content .ui-state-active, .ui-widget-header .ui-state-active, a.ui-button:active, .ui-button:active, .ui-button.ui-state-active:hover {
    border: 1px solid #c5c5c5 !important;
    background: #f6f6f6 !important;
    font-weight: normal;
    color: #ffffff;
}
        </style>

    <div class="main-container" id="main-container">
        <div class="container">
          <%--  <br />--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/General_Products.aspx">Purchase
                            online</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/Products/TRV_Proposal.aspx">Travel Protect Quotation</a></li>
                        <li class="breadcrumb-item active">Travel Protect Questionnaire</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>

          <%--  <br />--%>
            <div style="text-align: center">
                <h3>Travel Protect Questionnaire</h3>
            </div>
            <br />
            <div style="border: 0px solid #AAA; text-align: left; align-content: center">
                <table class="table">
                    <tr>
                        <td width="20%">Reference No</td>
                        <td width="80%">&nbsp;:
                        <asp:Literal ID="LitRef" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td width="20%">Selected Plan</td>
                        <td width="80%">&nbsp;:
                        <asp:Literal ID="lbl_plan" runat="server"></asp:Literal></td>
                    </tr>
                    <% if (planselected == "BSC2")
                        {
                      %>
                    <tr>
                        <td>&nbsp;</td>
                        <td><font color="red" face="sans-serif" size="2"><b>(Government staff - Including Public services, Forces, Police, Ministries .....etc. and Excluding Semi government institutions, Corporations, State banks .....etc.)</b></font>
                            <br />
                        </td>
                    </tr>
                    <%
                        }%>
                    <tr>
                        <td>Premium (Rs.)</td>
                        <td>&nbsp;:
                        <asp:Literal ID="lbl_premium" runat="server"></asp:Literal></td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="accordion">
                <h3>1</h3>
                <div id='pnl' class="auto-style1">
                    <p>
                        <b>Are you and each dependant listed above, now in good health?</b>
                        <br />
                        <asp:RadioButton ID="Rdo_gudHlth_Y" runat="server" Text="Yes"
                            GroupName="Rdo_gudHlth" />
                        <asp:RadioButton ID="Rdo_gudHlth_N" runat="server" Text="No"
                            GroupName="Rdo_gudHlth" />
                        <div id="wnl" class="errorr" style="display: none">
                            You cannot obtain this policy online, please <a href="/ContactUs.aspx" target="_blank"><span style="font-weight: bold; color: #8C8C8C;">contact us</span></a>  for assistance.
                        </div>
                        <div id="bnl" class="succ" style="display: none" onclick="openNextAccordionPanel_1();">NEXT</div>
                    </p>
                </div>
                <h3>2</h3>
                <div id='pnl1'>
                    <p>
                        <b>Do you or any dependant <font face="sans-serif" size="2">listed above, </font>have any defect or infirmity?</b>
                        <br />
                        <asp:RadioButton ID="Rdo_Deformity_Y" runat="server" Text="Yes"
                            GroupName="Rdo_Deformity" />
                        <asp:RadioButton ID="Rdo_Deformity_N" runat="server" Text="No"
                            GroupName="Rdo_Deformity" />
                        <div id="wnl1" class="errorr" style="display: none">
                            You cannot obtain this policy online, please <a href="/ContactUs.aspx" target="_blank"><span style="font-weight: bold; color: #8C8C8C;">contact us</span></a>  for assistance.
                        </div>
                        <div id="bnl1" class="succ" style="display: none" onclick="openNextAccordionPanel_1();">NEXT</div>
                    </p>
                </div>
                 <% if (planselected == "BSC2")
                     {
                      %>
               <h3>3</h3>
                <div id='pnl1' class="auto-style1">
                    <p>
                        <b>Are you a government employee ?</b>
                        <br />
                        <asp:RadioButton ID="Rdo_gvmnt_Y" runat="server" Text="Yes"
                            GroupName="Rdo_gvmnt" />
                        <asp:RadioButton ID="Rdo_gvmnt_N" runat="server" Text="No"
                            GroupName="Rdo_gvmnt" />
                        <div id="wn2" class="errorr" style="display: none">
                            You cannot obtain this policy online, please <a href="/ContactUs.aspx" target="_blank"><span style="font-weight: bold; color: #8C8C8C;">contact us</span></a>  for assistance.
                        </div>
                        <div id="bn2" class="succ" style="display: none" onclick="openNextAccordionPanel_1();">NEXT</div>
                    </p>
                </div>

                <%
                    }
                %>

                <h3>Next</h3>
                <div id="pnl13" style="text-align: center;">
                    <center>
      <input type="button" value="Confirm"  data-toggle="modal" Class="btn btn-primary btn-block" font-weight:normal;" role="button" href="#myModal"/>
  </center>
                </div>
            </div>
            <!-- Modal -->

            <div class="modal" tabindex="-1" role="dialog">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Modal title</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <p>Modal body text goes here.</p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-primary">Save changes</button>
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>
            <br />
            <br />
            <br />
            <br />

        </div>
    </div>

    
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h5 class="modal-title">Declaration :</h5>
                        </div>
                        <div class="modal-body">
                            I hereby declare that the particulars given by me in this proposal are true and complete and I have not withheld any information whatever, material to this proposal. I agree that this proposal, declaration and the truth of the answers herein contained shall be basis of contract between Sri lanka Insurance Corporation Ltd and me.
        <br />
                            <br />
                            <strong>I further confirm I am aware:</strong>
                            <br />
                            <p style="font-size: 1.0em;">
                                <ul>

                                    <li>that immediate notice should be given to Service Provider or Sri Lanka Insurance Corporation General Ltd., in the event of a claim, that may arise under the policy. </li>
                                    <li>that pre-existing medical conditions are not covered.</li>
                                    <li>of and agree to the terms, conditions and exclusions of the Policy. </li>

                                </ul>
                            </p>


                            <br />
                            <br />
                            <div class="row">
                                <asp:Button ID="Button1" runat="server" Text="Confirm" OnClick="Button1_Click" CssClass="btn btn-success btn-xs" UseSubmitBehavior="false"/>&nbsp;&nbsp;
        <button type="button" class="btn btn-danger pull-right btn-xs" data-dismiss="modal">Decline</button>
                            </div>
                        </div>
                    
                    </div>

                </div>
            </div>

<%--    <script src="/js/jquery-3.5.1.min.js"></script>--%>
      
   <%-- <script src="/js/jquery-3.5.1.min.js"></script>
    <script src="../assets/js/jquery-3.5.1.js"></script>--%>

      <link href="/css/jquery-ui.css" rel="stylesheet" />
                <script src="/js/jquery-ui.js"></script>

     <script type="text/javascript">
        $('#form1').submit(function () {
          
            var i = true;

            if (!$("[id$='Rdo_gudHlth_Y']").is(':checked')) {
                
                $($("#accordion > h3")[0]).removeClass("success_ans");
                $($("#accordion > h3")[0]).addClass("failure_ans");
                i = false;
            }

            if (!$("[id$='Rdo_Deformity_N']").is(':checked')) {
                $($("#accordion > h3")[1]).removeClass("success_ans");
                $($("#accordion > h3")[1]).addClass("failure_ans");
                i = false;
            }

            if (!$("[id$='Rdo_Surgical_N']").is(':checked')) {
                $($("#accordion > h3")[2]).removeClass("success_ans");
                $($("#accordion > h3")[2]).addClass("failure_ans");
                i = false;
            }

            if (!$("[id$='Rdo_can_N']").is(':checked')) {
                $($("#accordion > h3")[3]).removeClass("success_ans");
                $($("#accordion > h3")[3]).addClass("failure_ans");
                i = false;
            }

            if (!$("[id$='Rdo_fem_N']").is(':checked')) {
                $($("#accordion > h3")[4]).removeClass("success_ans");
                $($("#accordion > h3")[4]).addClass("failure_ans");
                i = false;
            }

            if (!$("[id$='Rdo_aids_N']").is(':checked')) {
                $($("#accordion > h3")[5]).removeClass("success_ans");
                $($("#accordion > h3")[5]).addClass("failure_ans");
                i = false;
            }

            if (!$("[id$='Rdo_alco_N']").is(':checked')) {
                $($("#accordion > h3")[6]).removeClass("success_ans");
                $($("#accordion > h3")[6]).addClass("failure_ans");
                i = false;
            }

            if (!$("[id$='Rdo_scan_N']").is(':checked')) {
                $($("#accordion > h3")[7]).removeClass("success_ans");
                $($("#accordion > h3")[7]).addClass("failure_ans");
                i = false;
            }

            if (!$("[id$='Rdo_weigh_N']").is(':checked')) {
                $($("#accordion > h3")[8]).removeClass("success_ans");
                $($("#accordion > h3")[8]).addClass("failure_ans");
                i = false;
            }

            if (!$("[id$='Rdo_death_N']").is(':checked')) {
                $($("#accordion > h3")[9]).removeClass("success_ans");
                $($("#accordion > h3")[9]).addClass("failure_ans");
                i = false;
            }

            if (!$("[id$='Rdo_occu_N']").is(':checked')) {
                $($("#accordion > h3")[10]).removeClass("success_ans");
                $($("#accordion > h3")[10]).addClass("failure_ans");
                i = false;
            }

            if (!$("[id$='Rdo_declined_N']").is(':checked')) {
                $($("#accordion > h3")[11]).removeClass("success_ans");
                $($("#accordion > h3")[11]).addClass("failure_ans");
                i = false;
            }

            if (!$("[id$='Rdo_slic_N']").is(':checked')) {
                $($("#accordion > h3")[12]).removeClass("success_ans");
                $($("#accordion > h3")[12]).addClass("failure_ans");
                i = false;
            }

            return i;

        });

        $($("#accordion > h3")).addClass("ui-state-disabled");
        $($("#accordion > h3")[0]).removeClass("ui-state-disabled");

        var $accordion = $("#accordion").accordion({
            heightStyle: "content"
        });
        function openNextAccordionPanel() {
            //
            var current = $accordion.accordion("option", "active"),
                maximum = $accordion.find("h3").length,
                next = current + 1 === maximum ? 0 : current + 1;
            //$accordion.accordion("activate",next); // pre jQuery UI 1.10
            $accordion.accordion("option", "active", next);
            //alert(current);
            $($("#accordion > h3")[current]).removeClass("ui-state-disabled");
            $($("#accordion > h3")[current]).addClass("success_ans");
        }

        function openNextAccordionPanel_1() {
            //
            var current = $accordion.accordion("option", "active"),
                maximum = $accordion.find("h3").length,
                next = current + 1 === maximum ? 0 : current + 1;
            //$accordion.accordion("activate",next); // pre jQuery UI 1.10
            $accordion.accordion("option", "active", next);
            //alert(current);
            //$($("#accordion > h3")[current]).removeClass("ui-state-disabled");
            //$($("#accordion > h3")[current]).addClass("success_ans");
        }

        function openNextAccordionPanel2() {
            //
            var current = $accordion.accordion("option", "active"),
                maximum = $accordion.find("h3").length,
                next = current + 1 === maximum ? 0 : current + 1;
            //$accordion.accordion("activate",next); // pre jQuery UI 1.10
            while (next < 13) {
                next++;
                current++;
                $accordion.accordion("option", "active", next);
                $($("#accordion > h3")[current]).addClass("ui-state-disabled");
            }

            // $accordion.accordion("option", "active", next);
            //alert(current);
            //   $($("#accordion > h3")[current]).addClass("ui-state-disabled");
            //$($("#accordion > h3")[current]).addClass("success_ans");
            //openNextAccordionPanel2();
        }

        $('.disabled .ui-accordion-header').addClass('ui-state-disabled').on('click', function () {
            return false;
        });
    </script>

   <%--  <script src="/js/jquery-3.5.1.min.js"></script>
    <script src="/js/jquery-3.5.1.js"></script>--%>
<%--      <script src="/js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="/js/jquery-1.9.1.js" type="text/javascript"></script>--%>

</asp:Content>

