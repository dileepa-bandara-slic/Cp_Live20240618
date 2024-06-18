<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true"
    CodeFile="AMP_Questions_Test.aspx.cs" Inherits="General_Authorized_Products_AMP_Questions_Test " %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <script src="/js/jquery-3.5.1.min.js"></script>
    <script src="/js/jquery-3.5.1.js"></script>
    <script src="/js/bootstrap.min.js" type="text/javascript"></script>
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


    <style type="text/css">
.HeaderActive{background-color: #009ee1;padding: 10px 30px 10px 15px;cursor: pointer;position: relative;margin-top: 10px;}
.HeaderNotActive{background-color: #949495;color: white;;padding: 0px 0px 0px 0px;cursor: pointer;position: relative;margin-top: 10px;}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="main-container" id="main-container" style="min-height:600px">
        <div class="container">
    <%--    </br>--%>
              <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/General_Products.aspx">Purchase
                            online</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/Products/AMP_Quotation.aspx">Annual Medical Plan Quotation</a></li>
                        <li class="breadcrumb-item active">Annual Medical Plan Questionnaire</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="text-center">
            <center>
                <h3>
                    Annual Medical Plan Questionnaire</h3>
                    </center>
                <div class="form-group">
                    <table class="table">
                        <tbody>
                            <tr>
                                <td>
                                    Selected Plan
                                </td>
                                <td>
                                    <asp:Literal ID="lbl_plan" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Plan Limit
                                </td>
                                <td>
                                    <asp:Literal ID="lbl_planLim" runat="server"></asp:Literal>
                                </td>
                            </tr>

                            <tr>
                            <td>Premium (Rs.)</td>
                            <td><asp:Literal ID="lbl_premium" runat="server"></asp:Literal></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
               <div id="accordion">
  <h3>1</h3>
  <div  id ='pnl'>
    <p>
    Are you and each dependant listed above, now in good health?
    <br />
    <asp:RadioButton ID="Rdo_gudHlth_Y" runat="server" Text="Yes" 
        GroupName="Rdo_gudHlth" />
    <asp:RadioButton ID="Rdo_gudHlth_N" runat="server" Text="No" 
        GroupName="Rdo_gudHlth" />
    <div id ="wnl" class="errorr" style="display:none">You can not obtain this policy from the system, please contact an agent for assistance</div>
    <div id ="bnl" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
    </p>
  </div>
  <h3>2</h3>
  <div  id ='pnl1'>
    <p>
    Do you or any dependant have any defect or infirmity?
    <br />
    <asp:RadioButton ID="Rdo_Deformity_Y" runat="server" Text="Yes" 
        GroupName="Rdo_Deformity" />
    <asp:RadioButton ID="Rdo_Deformity_N" runat="server" Text="No" 
        GroupName="Rdo_Deformity" />
    <div id ="wnl1" class="errorr" style="display:none">You can not obtain this policy from the system, please contact an agent for assistance</div>
    <div id ="bnl1" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
    </p>
  </div>
  <h3>3</h3>
  <div  id ='pnl2'>
    <p>
    Have you or any dependant undergone any surgical operation within the last 5 years or been advised to do so?&nbsp;
    <br />
    <asp:RadioButton ID="Rdo_Surgical_Y" runat="server" Text="Yes" 
        GroupName="Rdo_Surgical" />
    <asp:RadioButton ID="Rdo_Surgical_N" runat="server" Text="No" 
        GroupName="Rdo_Surgical" />
       <div id ="wnl2" class="errorr" style="display:none">You can not obtain this policy from the system, please contact an agent for assistance</div>
    <div id ="bnl2" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
    </p>
  </div>
  <h3>4</h3>
  <div  id ='pnl3'>
    <p>
    
    Have you or any of the above dependants ever had or are now currently receiving any medical treatment or surgical treatment, awaiting medical or surgical consultation, test or investigation for any of the following illnesses or medical conditions:&nbsp;<br />
    <asp:RadioButton ID="Rdo_can_Y" runat="server" Text="Yes" 
        GroupName="Rdo_can"/>
    <asp:RadioButton ID="Rdo_can_N" runat="server" Text="No" 
        GroupName="Rdo_can"   />
    <br />
    <ul>
    <li>
    <span id="leda1">
    Cancer, tumors, cysts or growths of any kind?</span>
    </li>
    <li>
    <span id="leda2">
    Diseases of the circulatory system (e.g. high blood pressure, angina, chest discomfort or pain, heart attack, raised cholesterol, heart murmur, valve disorders, rheumatic fever, stroke, irregular or fast heart rate or disease of the arteries and veins)?
    </span>
    </li>
    <li>
    <span id="leda3">
    Diseases of the endocrine system (e.g. diabetes, hyperthyroidism or goiter)?
    </span>
    </li>
    <li>
    <span id="leda4">
    Diseases of the respiratory system (e.g. tuberculosis, asthma, bronchitis, persistent cough, coughing with blood, pneumonia, hay fever or breathlessness)?
    </span>
    </li>
    <li>
    <span id="leda5">
    Diseases of the nervous system or mental disorders (e.g. epilepsy, fits, fainting spells, dizziness, frequent or prolonged headaches, poliomyelitis, numbness of limbs, paralysis, anxiety, depression, psychiatric ailment or nervous breakdown)?
    </span>
    </li>
    <li>
    <span id="leda6">
    Diseases of the genito-urinary system (e.g. kidney/bladder stones, protein or blood or sugar in the urine, infections of the kidneys, urinary or genital organs, urinary tract infection, prostate problem, incontinence, sexually transmitted diseases, syphilis, gonorrhea, herpes, non-specific arthritis or any treatment or investigation for erectile dysfunction)?
    </span>
    </li>
    <li>
    <span id="leda7">
    Diseases of the musculo-skeletal system (e.g. gout, arthritis, slipped disc, persistent back/neck pain, osteoporosis, systemic lupus erythematosus, rheumatism, or any diseases or disordesr of the spine bones, limbs, muscle or connective tissues)?
    </span>
    </li>
    <li>
    <span id="leda8">
    Diseases of the gastro-intestinal system (e.g. digestive disorders, gastric or duodenal ulcer, ulcerative colitis, fistula, piles, hepatitis B or C, hernia, polyp, chronic diarrhea, irritable bowel, rectal bleeding or liver or gallbladder disorder)?
    </span>
    </li>
    <li>
    <span id="leda9">
    Diseases of the blood (e.g. anaemia, thalassaemia or hemophilia); advised to abstain from donating blood or received blood transfusion or blood products?
    </span>
    </li>
    <li>
    <span id="leda10">
    Diseases of the ear, nose, eye, throat and skin (e.g. ear discharge, nose bleeds, rhinitis, sinusitis, nasal polyp, double vision, impaired sight, hearing defect, speech defect, cataracts, glaucoma, detached retina, floaters, eczema or dermatitis)?
    </span>
    </li>
    <li>
    <span id="leda11">
    Any other illness, disorder, operation, physical defects/deformities, disability, congenital anomalies, drug allergy or accident, not mentioned above, or premature birth of any of the above dependants?
    </span>
    </li>
    </ul>
     <div id ="wnl3" class="errorr" style="display:none">You can not obtain this policy from the system, please contact an agent for assistance</div>
    <div id ="bnl3" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
    </p>
  </div>
  <h3>5</h3>
  <div  id ='pnl4'>
  <p>
  For Females Only
          Do you have any of the following complecations? <br />
      <asp:RadioButton ID="Rdo_fem_Y" runat="server" Text="Yes" 
        GroupName="Rdo_fem"/>
    <asp:RadioButton ID="Rdo_fem_N" runat="server" Text="No" 
        GroupName="Rdo_fem"   />
          
    <ul>
    <li>
    <span id="gleda1">
    Have you or any of the above dependants suffered from or are you aware of any breast lumps or any other disorders of your breasts, suffered from irregular or painful or unusually heavy menstruation, fibroids, cysts or any other disorder of the female organs?</span>
    </li>
    <li>
    <span id="gleda2">
    Have you or any of the above dependants ever had any abnormal pap smear test or been told by any doctor to have a repeat pap smear within the next six months, been advised to have a mammogram, biopsy, operation of the breasts, ultrasound of the pelvis or any other gynaecological investigations?
    </span>
    </li>
    <li>
    <span id="gleda3">
    Are you or any of the above dependants now pregnant?
    </span>
    </li>
    </ul>
    <div id ="wnl4" class="errorr" style="display:none">You can not obtain this policy from the system, please contact an agent for assistance</div>
    <div id ="bnl4" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>    
  </div>
    <h3>6</h3>
  <div  id ='pnl5'>
  <p>Have you or your spouse been told to have / received any medical advice, counselling or treatment, in connection with sexually transmitted diseases, AIDS, AIDS Related Complex or any other AIDS related condition or ever had HIV testing done or been refused as a blood donor?&nbsp;<br />
  <asp:RadioButton ID="Rdo_aids_Y" runat="server" Text="Yes" 
        GroupName="Rdo_aids"/>
    <asp:RadioButton ID="Rdo_aids_N" runat="server" Text="No" 
        GroupName="Rdo_aids"   />
    <div id ="wnl5" class="errorr" style="display:none">You can not obtain this policy from the system, please contact an agent for assistance</div>
    <div id ="bnl5" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>7</h3>
  <div  id ='pnl6'>
  <p>Have you or any of the above dependants ever used any habit forming drugs or narcotics or been treated for drug habits or consumed alcohol excessively or been treated for alcoholism?&nbsp;<br />
  <asp:RadioButton ID="Rdo_alco_Y" runat="server" Text="Yes" 
        GroupName="Rdo_alco"/>
    <asp:RadioButton ID="Rdo_alco_N" runat="server" Text="No" 
        GroupName="Rdo_alco"   />
    <div id ="wnl6" class="errorr" style="display:none">You can not obtain this policy from the system, please contact an agent for assistance</div>
    <div id ="bnl6" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>8</h3>
  <div id="pnl7">
  <p>In the past 5 years, have you or any of the above dependants had any (other than for immunisation or vaccination) of the following tests done? Blood test, biopsy, chest X-Ray, CT scan, ECGs, immunisation, cholesterol, liver function tests, pap smear, ultrasound, urine and/or others? Please specify.&nbsp;<br />
  <asp:RadioButton ID="Rdo_scan_Y" runat="server" Text="Yes" 
        GroupName="Rdo_scan"/>
    <asp:RadioButton ID="Rdo_scan_N" runat="server" Text="No" 
        GroupName="Rdo_scan"   />
    <div id ="wnl7" class="errorr" style="display:none">You can not obtain this policy from the system, please contact an agent for assistance</div>
    <div id ="bnl7" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>9</h3>
  <div id="pnl8">
  <p>In the last 3 months, have you had any of the following symptoms for more than one week, continuously: fatigue, weight loss, diarrhea, enlarged nodes or unusual skin lesions? If yes, please state reason and results<br />
  <asp:RadioButton ID="Rdo_weigh_Y" runat="server" Text="Yes" 
        GroupName="Rdo_weigh"/>
    <asp:RadioButton ID="Rdo_weigh_N" runat="server" Text="No" 
        GroupName="Rdo_weigh"   />
    <div id ="wnl8" class="errorr" style="display:none">You can not obtain this policy from the system, please contact an agent for assistance</div>
    <div id ="bnl8" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>10</h3>
  <div id="pnl9">
  <p>Have either your or any of the above dependants natural parents or any siblings died or suffered from cancer, heart disease, stroke, high blood pressure, diabetes, kidney diseases, mental disorder, tuberculosis or any hereditary disease? If yes, please provide details of present conditions, present age and age of onset.&nbsp;<br />
   <asp:RadioButton ID="Rdo_death_Y" runat="server" Text="Yes" 
        GroupName="Rdo_death"/>
    <asp:RadioButton ID="Rdo_death_N" runat="server" Text="No" 
        GroupName="Rdo_death"   />
    <div id ="wnl9" class="errorr" style="display:none">You can not obtain this policy from the system, please contact an agent for assistance</div>
    <div id ="bnl9" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>11  </h3>
  <div id="pnl10">
  <p>Is there anything hazardous or unhealthy, connected with your occupation, or habits of life or those of any of the above dependants, which render you or them, especially liable to injury or sickness or general ill-health?&nbsp;<br />
  <asp:RadioButton ID="Rdo_occu_Y" runat="server" Text="Yes" 
        GroupName="Rdo_occu"/>
    <asp:RadioButton ID="Rdo_occu_N" runat="server" Text="No" 
        GroupName="Rdo_occu"   />
    <div id ="wnl10" class="errorr" style="display:none">You can not obtain this policy from the system, please contact an agent for assistance</div>
    <div id ="bnl10" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>12</h3>
  <div id="pnl11">
  <p>
  Has insurance for you or any of the dependants ever been postponed, declined, accepted on special terms, cancelled or renewal has been refused for life, accident, sickness or hospital expenses?&nbsp;<br />
  <asp:RadioButton ID="Rdo_declined_Y" runat="server" Text="Yes" 
        GroupName="Rdo_declined"/>
    <asp:RadioButton ID="Rdo_declined_N" runat="server" Text="No" 
        GroupName="Rdo_declined"   />
    <div id ="wnl11" class="errorr" style="display:none">You can not obtain this policy from the system, please contact an agent for assistance</div>
    <div id ="bnl11" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>13</h3>
  <div id="pnl12">
  <p>Have you or any of the above dependants ever made a claim under an accident, sickness or medical expenses policy from Sri Lanka Insurance or any other insurance company?&nbsp;<br />
  <asp:RadioButton ID="Rdo_slic_Y" runat="server" Text="Yes" 
        GroupName="Rdo_slic"/>
    <asp:RadioButton ID="Rdo_slic_N" runat="server" Text="No" 
        GroupName="Rdo_slic"   />
    <div id ="wnl12" class="errorr" style="display:none">You can not obtain this policy from the system, please contact an agent for assistance</div>
    <div id ="bnl12" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>Next</h3>
  <div id="pnl13" style="text-align:center;">
  <center>
      <input type="button" value="Confirm"  data-toggle="modal" Class="btn btn-primary btn-block" font-weight:normal;" role="button" href="#myModal"/>
  </center>
  </div>
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
        I certify the correctness of the statement and I hereby agree to give notice to the Company, immediately of any variation in my profession or occupation or of any changes in my health or habits and that this warranty and agreement shall be promissory and shall form the basis of the contract between the Company and myself, and to accept a policy in the usual printed form by the Company.
        <div style="font-family:Sandaya">
        by; i|yka m%ldYj, ksrjµ;dj iy;sl lrñ' uf.a jD;a;sfha fyda /lshdfõ lsishï fjkila jqjfyd;a fyda uf.a fi!LHfha fyda .;s mej;=ïj, lsishï fjkila jQ úfgl iud.ug tA .ek jydu okajk njg .súiñ' fï m%;s{d.drh .súiqï fmdfrdkaÿjla nj yd iud.u yd ud w;/;s .súiqfï moku jk njg o iud.fuka ksl=;a lrkq ,nk idudkH uqøs; Tmamqjla ms&#60;s.ekSugo fuhska tlÛ fjñ' 
        
        </div>
<br />
<br /><br />
 <asp:Button ID="Button1" runat="server" Text="Confirm" onclick="Button1_Click" CssClass="btn btn-success btn-xs" />&nbsp;&nbsp;
        <button type="button" class="btn btn-danger pull-right btn-xs"  data-dismiss="modal">Decline</button>

        </div>
        <%--<div class="modal-footer">
          
        </div>--%>
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
            <script src="/js/jquery-3.5.1.min.js"></script>
            <script src="/js/jquery-3.5.1.js"></script>
            <script src="/js/bootstrap.min.js" type="text/javascript"></script>
        </div>
    </div>
</asp:Content>
