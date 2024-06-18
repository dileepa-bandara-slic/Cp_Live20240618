<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true"
    CodeFile="GT_Questions.aspx.cs" Inherits="General_Authorized_Products_GT_Questions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <%-- <script src="../assets/js/jquery-3.5.1.min.js"></script>
    <script src="../assets/js/jquery-3.5.1.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>--%>
    <style type="text/css">
        @media (max-width:479px)
        {
            .navbar-fixed-top + .main-container
            {
                padding-top: 45px;
            }
        }
        .q_box
        {
            color: #FF5050;
            height: 100px;
            width: 100%;
            border: 1px solid #FF0000;
        }
        
        .success_ans
        {
            /*border: 1px solid #31DA83;*/
            background: url("/css/smoothness/images/bk.png") repeat-x;
        }
        .failure_ans
        {
            /*border: 1px solid #31DA83;*/
            background: url("/css/smoothness/images/bks.png") repeat-x;
        }
        .success_ans h3
        {
            border: 1px solid #31DA83;
        }
        
        .errorr
        {
            color: #FF0000;
            font-size: 12px;
            font-weight: normal;
            font-family: Tahoma;
            color: #FF0000;
        }
        
        
        #overlay
        {
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
        #pnl
        {
            background-color: #FFF;
            z-index: 901;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }
        
        #pnl1
        {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }
        
        #pnl2
        {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }
        #pnl3
        {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
            font-size: 12px;
            font-family: Tahoma;
            color: #565656;
        }
        #pnl3_1
        {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
        }
        #pnl3_2
        {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
        }
        #pnl3_3
        {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
        }
        #pnl3_4
        {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
        }
        #pnl3_5
        {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
        }
        #pnl3_6
        {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
        }
        #pnl3_7
        {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
        }
        #pnl3_8
        {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
        }
        #pnl3_9
        {
            background-color: #FFF;
            z-index: 800;
            position: relative;
            min-height: 100px;
            display: block;
        }
        #HeaderDiv
        {
            height: 100%;
            width: 100%;
            background-color: Blue;
        }
        
        .ModalPopupBG
        {
            background-color: #000;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        
        .HellowWorldPopup
        {
            min-width: 750px;
            min-height: 550px;
            background: white;
        }
        p
        {
            font-size: 13px;
            font-family: Tahoma;
            color: #565656;
        }
        
        
        
        .succ
        {
            font-size: 11px;
            font-family: Tahoma;
            color: #565656;
            font-weight: bold;
        }
        
   
    </style>


    <script type="text/javascript" language="javascript">

        function check_all_ok() {
            var current = $accordion.accordion("option", "active");
            maximum = $accordion.find("h3").length;

            for (i = current; i <= maximum + 1; i++) {

                $($("#accordion > h3")[i]).addClass("ui-state-disabled");
            }

        }


        function check_all_ok_100() {
            var current = $accordion.accordion("option", "active");
            maximum = $accordion.find("h3").length;

            for (i = current; i <= maximum + 1; i++) {
                // $($("#accordion > h3")[i]).removeClass("ui-state-disabled");
            }

        }
        window.onload = function () {

        }
        //    $(document).ready(function () {

        //        $("#overlay").show();
        //        $("#id1").show();
        //        //$("#id2").();


        //        $("#as").click(function () {
        //            $("#overlay").show();
        //            $("#id2").show();
        //        });
        //    });

        $(document).ready(function () {


            $("[id$='Rdo_gudHlth_Y']").click(function () {
                //check_all_ok_100();
                openNextAccordionPanel();
                $("#wnl").hide();
                $("#bnl").show();
                //$("#btn_confrm").prop('disabled', false);

            });

            $("[id$='Rdo_gudHlth_N']").click(function () {
                check_all_ok();
                $("#wnl").show();
                $("#bnl").hide();
                //$("#btn_confrm").prop('disabled', true);

            });

            $("[id$='Rdo_Deformity_N']").click(function () {
                //check_all_ok_100();
                openNextAccordionPanel();
                $("#wnl1").hide();
                $("#bnl1").show();

            });
            $("[id$='Rdo_Deformity_Y']").click(function () {
                check_all_ok();
                $("#wnl1").show();
                $("#bnl1").hide();

            });


            $("[id$='Rdo_Surgical_Y']").click(function () {
                //check_all_ok_100();
                $("#wnl2").show();
                $("#bnl2").hide();

            });

            $("[id$='Rdo_Surgical_N']").click(function () {
                check_all_ok();
                openNextAccordionPanel();
                $("#wnl2").hide();
                $("#bnl2").show();

            });

      
        });
    
    </script>
<%--    <style>
     
    </style>--%>


    <style type="text/css">
.HeaderActive{background-color: #009ee1;padding: 10px 30px 10px 15px;cursor: pointer;position: relative;margin-top: 10px;}
.HeaderNotActive{background-color: #949495;color: white;;padding: 0px 0px 0px 0px;cursor: pointer;position: relative;margin-top: 10px;}

</style>

         <style>
     .modal-backdrop {
                 z-index: 0;
             }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
          <link href="/css/jquery-ui.css" rel="stylesheet" />
          <script src="/js/jquery-ui.js"></script>

    <div class="main-container" id="main-container"  style="min-height:600px">
        <div class="container">
      <%--  </br>--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/General_Products.aspx">Purchase
                            online</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/Products/GT_Proposal.aspx">
                            Globe Trotter Quotation</a></li>
                        <li class="breadcrumb-item active">Globe Trotter Questioner</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="text-center">
            <center>
                <h3>
                    Globe Trotter Questionnaire</h3>
                    </center>
                <div class="form-group">
                    <table class="table">
                        <tbody>
                            <tr>
                                <td width="20%" style="text-align:left;">
                                    Selected Plan
                                </td>
                                <td width="80%" style="text-align:left;">
                                    <asp:Literal ID="lbl_plan" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%" style="text-align:left;">
                                    Premium (Rs.)
                                </td>
                                <td width="80%" style="text-align:left;">
                                    <asp:Literal ID="lbl_premium" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div id="accordion">
                    <h3>
                        1</h3>
                    <div id='pnl'>
                        <p>
                            <b>Are you and each dependant listed above, presently in good health?</b>
                            <br />
                            <asp:RadioButton ID="Rdo_gudHlth_Y" runat="server" Text="Yes" GroupName="Rdo_gudHlth" />
                            <asp:RadioButton ID="Rdo_gudHlth_N" runat="server" Text="No" GroupName="Rdo_gudHlth" />
                            <div id="wnl" class="errorr" style="display: none">
                                You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
                            </div>
                            <div id="bnl" class="succ" style="display: none" onclick="openNextAccordionPanel_1();">
                                NEXT</div>
                        </p>
                    </div>
                    <h3>
                        2</h3>
                    <div id='pnl1'>
                        <p>
                          <b> Do you or each dependant listed above, have pre-exisiting medical complications?</b> 
                            <br />
                            <asp:RadioButton ID="Rdo_Deformity_Y" runat="server" Text="Yes" GroupName="Rdo_Deformity" />
                            <asp:RadioButton ID="Rdo_Deformity_N" runat="server" Text="No" GroupName="Rdo_Deformity" />
                            <div id="wnl1" class="errorr" style="display: none">
                                You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
                             </div>
                            <div id="bnl1" class="succ" style="display: none" onclick="openNextAccordionPanel_1();">
                                NEXT</div>
                        </p>
                    </div>
                    <h3>
                        3</h3>
                    <div id='pnl2'>
                        <p>
                           <b>  Are you or each dependant listed above, a professional sportsman or a semi-professional
                            sportsman?&nbsp;</b> 
                            <br />
                            <asp:RadioButton ID="Rdo_Surgical_Y" runat="server" Text="Yes" GroupName="Rdo_Surgical" />
                            <asp:RadioButton ID="Rdo_Surgical_N" runat="server" Text="No" GroupName="Rdo_Surgical" />
                            <div id="wnl2" class="errorr" style="display: none">
                                You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
                            </div>
                            <div id="bnl2" class="succ" style="display: none" onclick="openNextAccordionPanel_1();">
                                NEXT</div>
                        </p>
                    </div>
                    <h3>
                        Next</h3>
                    <div id="pnl4" style="text-align: center;">
                        <center>
                           <%-- <input type="button" id="btn_confrm" value="Confirm" data-toggle="modal" class="btn btn-primary btn-xs"
                                style="font-weight: normal;" role="button" href="#myModal" onclick="return btn_confrm_onclick()" />--%>
                                  <%--<input type="button" id="btn_confrm" value="Confirm"  data-toggle="modal" class="btn btn-primary btn-xs" style="font-weight:normal;" role="button" href="#myModal" onclick="return btn_confrm_onclick()" />--%>
                                      <input type="button" id="btn_confrm" value="Confirm"  data-toggle="modal" Class="btn btn-primary btn-block" style="font-weight:normal;" role="button" href="#myModal"/>
                        </center>
                    </div>
                </div>
                <br />
            </div>
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h5 class="modal-title">
                                <strong>Declaration :</strong></h5>
                        </div>
                        <div class="modal-body">
                            I hereby declare that the particulars given by me in this proposal are true and complete and I have not withheld any information whatever, material to this proposal.  I agree that this proposal, declaration and the truth of the answers herein contained shall be the basis of contract between Sri Lanka Insurance Corporation Ltd and me.  I undertake to give immediate notice of any changes in the information given above.
                            <br />
                            <br />
                            <strong>I further confirm I am aware:</strong>
                            <br /><p style="font-size:1.0em;">
                            <ul>
                            
                            <li>that pre-existing medical conditions will not be covered.</li>
                            <li>of and agree to the terms, conditions and exclusions of the Policy.</li>

                            </ul>
                            <%--* There will be no refund of premium if your insurance is cancelled
                            after commencement of the journey.<br />
                            * This insurance cover will operate only on acceptance of proposal
                            form and payment of the premium.<br />
                            * For person above 70 years of age declaration of health must
                            be completed.<br />
                            * Pre-existing medical conditions will not be covered--%></p>
                            <br />
                            <br />
                            <div class="row">
                                <asp:Button runat="server" ID="but1" Text="Confirm" OnClick="but1_Click" CssClass="btn btn-success btn-sm" />&nbsp;&nbsp;
                                <button type="button" class="btn btn-danger pull-right btn-sm" data-dismiss="modal">
                                    Decline</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script>


                //     function validate_inputs() {

                //         if (!$("[id$='Rdo_gudHlth_Y']").is(':checked')) { 
                //         alert("it's not checked"); }

                // }

                $('#form1').submit(function () {

                    var i = true;

                    if (!$("[id$='Rdo_gudHlth_Y']").is(':checked')) {
                        $($("#accordion > h3")[0]).removeClass("success_ans");
                        $($("#accordion > h3")[0]).addClass("failure_ans");
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
         <%--   <script src="../assets/js/jquery-3.5.1.min.js"></script>
            <script src="../assets/js/jquery-3.5.1.js"></script>--%>


    
           
        </div>
    </div>
</asp:Content>
