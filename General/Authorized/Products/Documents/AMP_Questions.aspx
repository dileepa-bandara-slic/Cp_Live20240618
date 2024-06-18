﻿<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="AMP_Questions.aspx.cs" Inherits="General_Authorized_Products_AMP_Questions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="/js/jquery-3.5.1.min.js"></script>
  
    <link href="/css/jquery-ui.css" rel="stylesheet" />
    <script src="/js/jquery-ui.js"></script>


         <style type="text/css">

        .q_box 
        {
            color: #FF5050;
            height:100px;
            width:100%;
            border: 1px solid #FF0000;
        }
        
        .success_ans
        {
            
            /*border: 1px solid #31DA83;*/
            background: url("/css/smoothness/images/bk.png") repeat-x ; 
        }
         .failure_ans
        {
            
            /*border: 1px solid #31DA83;*/
            background: url("/css/smoothness/images/bks.png") repeat-x ; 
        }
        .success_ans h3
        {
            
            border: 1px solid #31DA83;
        }
        
        .errorr
        {
            color:#FF0000;
            font-size:12px; 
             font-weight:normal;
font-family:Tahoma; 
color:#FF0000;
        }
        
        
        #overlay 
        {
            text-align:center;
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
    background-color:#FFF;
    z-index: 901;
    position: relative;
    min-height:100px;
    font-size:12px; 
    display: block;
}

#pnl1 {
    background-color:#FFF;
    z-index: 800;
    position: relative;
    min-height:100px;
    display: block;
    font-size:12px; 
font-family:Tahoma; 
color:#565656;
}

#pnl2 {
    background-color:#FFF;
    z-index: 800;
    position: relative;
    min-height:100px;
    display: block;
    font-size:12px; 
font-family:Tahoma; 
color:#565656;
}
#pnl3 {
    background-color:#FFF;
    z-index: 800;
    position: relative;
    min-height:100px;
    display: block;
    font-size:12px; 
font-family:Tahoma; 
color:#565656;
}
#pnl3_1 {
    background-color:#FFF;
    z-index: 800;
    position: relative;
    min-height:100px;
    display: block;
    font-size:12px; 
font-family:Tahoma; 
color:#565656;
}
#pnl3_2 {
    background-color:#FFF;
    z-index: 800;
    position: relative;
    min-height:100px;
    display: block;
    font-size:12px; 
font-family:Tahoma; 
color:#565656;
}
#pnl3_3 {
    background-color:#FFF;
    z-index: 800;
    position: relative;
    min-height:100px;
    display: block;
    font-size:12px; 
font-family:Tahoma; 
color:#565656;
}
#pnl3_4 {
    background-color:#FFF;
    z-index: 800;
    position: relative;
    min-height:100px;
    display: block;
    font-size:12px; 
font-family:Tahoma; 
color:#565656;
}
#pnl3_5 {
    background-color:#FFF;
    z-index: 800;
    position: relative;
    min-height:100px;
    display: block;
    font-size:12px; 
font-family:Tahoma; 
color:#565656;
}
#pnl3_6 {
    background-color:#FFF;
    z-index: 800;
    position: relative;
    min-height:100px;
    display: block;
    font-size:12px; 
font-family:Tahoma; 
color:#565656;
}
#pnl3_7 {
    background-color:#FFF;
    z-index: 800;
    position: relative;
    min-height:100px;
    display: block;
    font-size:12px; 
font-family:Tahoma; 
color:#565656;
}
#pnl3_8 {
    background-color:#FFF;
    z-index: 800;
    position: relative;
    min-height:100px;
    display: block;
    font-size:12px; 
font-family:Tahoma; 
color:#565656;
}
#pnl3_9 {
    background-color:#FFF;
    z-index: 800;
    position: relative;
    min-height:100px;
    display: block;
    font-size:12px; 
font-family:Tahoma; 
color:#565656;
}
#HeaderDiv {
    height:100%;
    width:100%;
    background-color:Blue;
}

        .ModalPopupBG
        {
            background-color: #000;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .HellowWorldPopup
        {
             min-width:750px;
            min-height:550px;
            background:white;
        }
          p {
        font-size:13px; 
font-family:Tahoma; 
color:#565656;
            }
            
            ul
            {font-size:12px; 
font-family:Tahoma; 
color:#565656;
                }
                
                .succ
                {
                font-size:11px; 
font-family:Tahoma; 
color:#565656;
font-weight:bold;
                    }
                    @font-face {
    font-family: 'Sandaya12';
    src: url('sa_____.TTF');
}

        @media (min-width: 300px)
        {
            #Testt_1
            {
                display: block;
            }
            
            #Testt_3
            {
                display: none;
            }
            
            #Testt_5
            {
                display: none;
            }
           
            
        }
        
        @media (min-width: 400px)
        {
            #Testt_1
            {
                display: none;
            }
           
            #Testt_3
            {
                display: block;
            }
           
            #Testt_5
            {
                display: none;
            }
        }


        @media (min-width: 500px)
        {
            #Testt_1
            {
                display: none;
            }
           
            #Testt_3
            {
                display: block;
            }
            
            #Testt_5
            {
                display: none;
            }
           
            
        }
        
        @media (min-width: 600px) and (min-width: 699px)
        {
            #Testt_1
            {
                display: none;
            }
            
            #Testt_3
            {
                display: none;
            }
           
            #Testt_5
            {
                display: block;
            }
           
        }
        
        
        @media (min-width: 700px)
        {
            #Testt_1
            {
                display: none;
            }
           
            #Testt_3
            {
                display: none;
            }
            
            #Testt_5
            {
                display: block;
            }
           
        }
    </style>
        <script  type="text/javascript" language="javascript">

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
        /**
        * Copyright (c) 2007-2015 Ariel Flesler - aflesler<a>gmail<d>com | http://flesler.blogspot.com
        * Licensed under MIT
        * @author Ariel Flesler
        * @version 2.1.1
        */
        //     (function (f) { "use strict"; "function" === typeof define && define.amd ? define(["jquery"], f) : "undefined" !== typeof module && module.exports ? module.exports = f(require("jquery")) : f(jQuery) })(function ($) { "use strict"; function n(a) { return !a.nodeName || -1 !== $.inArray(a.nodeName.toLowerCase(), ["iframe", "#document", "html", "body"]) } function h(a) { return $.isFunction(a) || $.isPlainObject(a) ? a : { top: a, left: a} } var p = $.scrollTo = function (a, d, b) { return $(window).scrollTo(a, d, b) }; p.defaults = { axis: "xy", duration: 0, limit: !0 }; $.fn.scrollTo = function (a, d, b) { "object" === typeof d && (b = d, d = 0); "function" === typeof b && (b = { onAfter: b }); "max" === a && (a = 9E9); b = $.extend({}, p.defaults, b); d = d || b.duration; var u = b.queue && 1 < b.axis.length; u && (d /= 2); b.offset = h(b.offset); b.over = h(b.over); return this.each(function () { function k(a) { var k = $.extend({}, b, { queue: !0, duration: d, complete: a && function () { a.call(q, e, b) } }); r.animate(f, k) } if (null !== a) { var l = n(this), q = l ? this.contentWindow || window : this, r = $(q), e = a, f = {}, t; switch (typeof e) { case "number": case "string": if (/^([+-]=?)?\d+(\.\d+)?(px|%)?$/.test(e)) { e = h(e); break } e = l ? $(e) : $(e, q); if (!e.length) return; case "object": if (e.is || e.style) t = (e = $(e)).offset() } var v = $.isFunction(b.offset) && b.offset(q, e) || b.offset; $.each(b.axis.split(""), function (a, c) { var d = "x" === c ? "Left" : "Top", m = d.toLowerCase(), g = "scroll" + d, h = r[g](), n = p.max(q, c); t ? (f[g] = t[m] + (l ? 0 : h - r.offset()[m]), b.margin && (f[g] -= parseInt(e.css("margin" + d), 10) || 0, f[g] -= parseInt(e.css("border" + d + "Width"), 10) || 0), f[g] += v[m] || 0, b.over[m] && (f[g] += e["x" === c ? "width" : "height"]() * b.over[m])) : (d = e[m], f[g] = d.slice && "%" === d.slice(-1) ? parseFloat(d) / 100 * n : d); b.limit && /^\d+$/.test(f[g]) && (f[g] = 0 >= f[g] ? 0 : Math.min(f[g], n)); !a && 1 < b.axis.length && (h === f[g] ? f = {} : u && (k(b.onAfterFirst), f = {})) }); k(b.onAfter) } }) }; p.max = function (a, d) { var b = "x" === d ? "Width" : "Height", h = "scroll" + b; if (!n(a)) return a[h] - $(a)[b.toLowerCase()](); var b = "client" + b, k = a.ownerDocument || a.document, l = k.documentElement, k = k.body; return Math.max(l[h], k[h]) - Math.min(l[b], k[b]) }; $.Tween.propHooks.scrollLeft = $.Tween.propHooks.scrollTop = { get: function (a) { return $(a.elem)[a.prop]() }, set: function (a) { var d = this.get(a); if (a.options.interrupt && a._last && a._last !== d) return $(a.elem).stop(); var b = Math.round(a.now); d !== b && ($(a.elem)[a.prop](b), a._last = this.get(a)) } }; return p });

        //     
        //     function showall() {

        //         $("#overlay").show();
        //         $("#pnl").show();
        //         $("#pnl1").show();
        //     }
     
     
    </script>
    
    <style type="text/css">
.HeaderActive{background-color: #009ee1;padding: 10px 30px 10px 15px;cursor: pointer;position: relative;margin-top: 10px;}
.HeaderNotActive{background-color: #949495;color: white;;padding: 0px 0px 0px 0px;cursor: pointer;position: relative;margin-top: 10px;}

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../../assets/js/jquery-3.5.1.min.js"></script>
<div class="main-container" id="main-container">
        <div class="container">
         <br />
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


    <%--<div class="success_ans">dd</div>--%>

   

  <script>

      //      $("[id$='Rdo_gudHlth_Y']").click(function () {

      //      });

      //      $(function () {



      //          $("div.HeaderDiv").not(":eq(O)").click(function (event) {

      //              if ($(this).parent().prev().find(":text").first().val().trim().length == 0)
      //                  event.stopPropagation();
      //          })
      //      })
         </script>

<center>
<br />
    <h3>Annual Medical Plan Questionnaire</h3>
<br />
<div style=" border : 0px solid #AAA; text-align:left;" >
 <table class="table">
<tr>
<td width="20%">Selected Plan</td>
<td  width="80%">&nbsp;: <asp:Literal ID="lbl_plan" runat="server"></asp:Literal></td>
</tr>
<tr>
<td>Plan Limit</td>
<td>&nbsp;: <asp:Literal ID="lbl_planLim" runat="server"></asp:Literal></td>
</tr>
<tr>
<td>Premium (Rs.)</td>
<td>&nbsp;: <asp:Literal ID="lbl_premium" runat="server"></asp:Literal></td>
</tr>
</table>
<br />

<div id="accordion">
  <h3>1</h3>
  <div  id ='pnl'>
    <p>
    <b>Are you and each dependant listed above, now in good health?</b>
    <br />
    <asp:RadioButton ID="Rdo_gudHlth_Y" runat="server" Text="Yes" 
        GroupName="Rdo_gudHlth" />
    <asp:RadioButton ID="Rdo_gudHlth_N" runat="server" Text="No" 
        GroupName="Rdo_gudHlth" />
    <div id ="wnl" class="errorr" style="display:none">
    You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
    </div>
    <div id ="bnl" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
    </p>
  </div>
  <h3>2</h3>
  <div  id ='pnl1'>
    <p>
    <b>Do you or any dependant have any defect or infirmity?</b>
    <br />
    <asp:RadioButton ID="Rdo_Deformity_Y" runat="server" Text="Yes" 
        GroupName="Rdo_Deformity" />
    <asp:RadioButton ID="Rdo_Deformity_N" runat="server" Text="No" 
        GroupName="Rdo_Deformity" />
    <div id ="wnl1" class="errorr" style="display:none">
    You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
    </div>
    <div id ="bnl1" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
    </p>
  </div>
  <h3>3</h3>
  <div  id ='pnl2'>
    <p>
    <b>Have you or any dependant undergone any surgical operation within the last 5 years or been advised to do so?&nbsp;</b>
    <br />
    <asp:RadioButton ID="Rdo_Surgical_Y" runat="server" Text="Yes" 
        GroupName="Rdo_Surgical" />
    <asp:RadioButton ID="Rdo_Surgical_N" runat="server" Text="No" 
        GroupName="Rdo_Surgical" />
       <div id ="wnl2" class="errorr" style="display:none">
       You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
       </div>
    <div id ="bnl2" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
    </p>
  </div>
  <h3>4</h3>
  <div  id ='pnl3'>
    <p>
    
    <b>Have you or any of the above dependants ever had or are now currently receiving any medical treatment or surgical treatment, awaiting medical or surgical consultation, test or investigation for any of the following illnesses or medical conditions:&nbsp;</b><br />
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
    <br/>
    <li>
    <span id="leda2">
    Diseases of the circulatory system (e.g. high blood pressure, angina, chest discomfort or pain, heart attack, raised cholesterol, heart murmur, valve disorders, rheumatic fever, stroke, irregular or fast heart rate or disease of the arteries and veins)?
    </span>
    </li>
    <br/>
    <li>
    <span id="leda3">
    Diseases of the endocrine system (e.g. diabetes, hyperthyroidism or goiter)?
    </span>
    </li>
      <br/>
    <li>
    <span id="leda4">
    Diseases of the respiratory system (e.g. tuberculosis, asthma, bronchitis, persistent cough, coughing with blood, pneumonia, hay fever or breathlessness)?
    </span>
    </li>
      <br/>
    <li>
    <span id="leda5">
    Diseases of the nervous system or mental disorders (e.g. epilepsy, fits, fainting spells, dizziness, frequent or prolonged headaches, poliomyelitis, numbness of limbs, paralysis, anxiety, depression, psychiatric ailment or nervous breakdown)?
    </span>
    </li>
      <br/>
    <li>
    <span id="leda6">
    Diseases of the genito-urinary system (e.g. kidney/bladder stones, protein or blood or sugar in the urine, infections of the kidneys, urinary or genital organs, urinary tract infection, prostate problem, incontinence, sexually transmitted diseases, syphilis, gonorrhea, herpes, non-specific arthritis or any treatment or investigation for erectile dysfunction)?
    </span>
    </li>
      <br/>
    <li>
    <span id="leda7">
    Diseases of the musculo-skeletal system (e.g. gout, arthritis, slipped disc, persistent back/neck pain, osteoporosis, systemic lupus erythematosus, rheumatism, or any diseases or disordesr of the spine bones, limbs, muscle or connective tissues)?
    </span>
    </li>
      <br/>
    <li>
    <span id="leda8">
    Diseases of the gastro-intestinal system (e.g. digestive disorders, gastric or duodenal ulcer, ulcerative colitis, fistula, piles, hepatitis B or C, hernia, polyp, chronic diarrhea, irritable bowel, rectal bleeding or liver or gallbladder disorder)?
    </span>
    </li>
      <br/>
    <li>
    <span id="leda9">
    Diseases of the blood (e.g. anaemia, thalassaemia or hemophilia); advised to abstain from donating blood or received blood transfusion or blood products?
    </span>
    </li>
      <br/>
    <li>
    <span id="leda10">
    Diseases of the ear, nose, eye, throat and skin (e.g. ear discharge, nose bleeds, rhinitis, sinusitis, nasal polyp, double vision, impaired sight, hearing defect, speech defect, cataracts, glaucoma, detached retina, floaters, eczema or dermatitis)?
    </span>
    </li>
      <br/>
    <li>
    <span id="leda11">
    Any other illness, disorder, operation, physical defects/deformities, disability, congenital anomalies, drug allergy or accident, not mentioned above, or premature birth of any of the above dependants?
    </span>
    </li>
      <br/>
    </ul>
     <div id ="wnl3" class="errorr" style="display:none">
     You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
     </div>
    <div id ="bnl3" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
    </p>
  </div>
  <h3>5</h3>
  <div  id ='pnl3_1'>
  <p>
  <b>For Females Only
          Do you have any of the following complecations? </b><br />
      <asp:RadioButton ID="Rdo_fem_Y" runat="server" Text="Yes" 
        GroupName="Rdo_fem"/>
    <asp:RadioButton ID="Rdo_fem_N" runat="server" Text="No" 
        GroupName="Rdo_fem"   />
          
    <ul>
    <li>
    <span id="gleda1">
    Have you or any of the above dependants suffered from or are you aware of any breast lumps or any other disorders of your breasts, suffered from irregular or painful or unusually heavy menstruation, fibroids, cysts or any other disorder of the female organs?</span>
    </li>
     <br/>
    <li>
    <span id="gleda2">
    Have you or any of the above dependants ever had any abnormal pap smear test or been told by any doctor to have a repeat pap smear within the next six months, been advised to have a mammogram, biopsy, operation of the breasts, ultrasound of the pelvis or any other gynaecological investigations?
    </span>
    </li>
     <br/>
    <li>
    <span id="gleda3">
    Are you or any of the above dependants now pregnant?
    </span>
    </li>
     <br/>
    </ul>
    <div id ="wnl4" class="errorr" style="display:none">
    You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
    </div>
    <div id ="bnl4" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>    
  </div>
    <h3>6</h3>
  <div  id ='pnl3_2'>
  <p><b>Have you or your spouse been told to have / received any medical advice, counselling or treatment, in connection with sexually transmitted diseases, AIDS, AIDS Related Complex or any other AIDS related condition or ever had HIV testing done or been refused as a blood donor?&nbsp;</b><br />
  <asp:RadioButton ID="Rdo_aids_Y" runat="server" Text="Yes" 
        GroupName="Rdo_aids"/>
    <asp:RadioButton ID="Rdo_aids_N" runat="server" Text="No" 
        GroupName="Rdo_aids"   />
    <div id ="wnl5" class="errorr" style="display:none">
    You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
    </div>
    <div id ="bnl5" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>7</h3>
  <div  id ='pnl3_3'>
  <p><b>Have you or any of the above dependants ever used any habit forming drugs or narcotics or been treated for drug habits or consumed alcohol excessively or been treated for alcoholism?&nbsp;</b><br />
  <asp:RadioButton ID="Rdo_alco_Y" runat="server" Text="Yes" 
        GroupName="Rdo_alco"/>
    <asp:RadioButton ID="Rdo_alco_N" runat="server" Text="No" 
        GroupName="Rdo_alco"   />
    <div id ="wnl6" class="errorr" style="display:none">
    You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
    </div>
    <div id ="bnl6" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>8</h3>
  <div id="pnl3_4">
  <p><b>In the past 5 years, have you or any of the above dependants had any (other than for immunisation or vaccination) of the following tests done? Blood test, biopsy, chest X-Ray, CT scan, ECGs, immunisation, cholesterol, liver function tests, pap smear, ultrasound, urine and/or others? Please specify.&nbsp;</b><br />
  <asp:RadioButton ID="Rdo_scan_Y" runat="server" Text="Yes" 
        GroupName="Rdo_scan"/>
    <asp:RadioButton ID="Rdo_scan_N" runat="server" Text="No" 
        GroupName="Rdo_scan"   />
    <div id ="wnl7" class="errorr" style="display:none">
    You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
    </div>
    <div id ="bnl7" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>9</h3>
  <div id="pnl3_5">
  <p><b>In the last 3 months, have you had any of the following symptoms for more than one week, continuously: fatigue, weight loss, diarrhea, enlarged nodes or unusual skin lesions? If yes, please state reason and results</b><br />
  <asp:RadioButton ID="Rdo_weigh_Y" runat="server" Text="Yes" 
        GroupName="Rdo_weigh"/>
    <asp:RadioButton ID="Rdo_weigh_N" runat="server" Text="No" 
        GroupName="Rdo_weigh"   />
    <div id ="wnl8" class="errorr" style="display:none">
    You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
    </div>
    <div id ="bnl8" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>10</h3>
  <div id="pnl3_6">
  <p><b>Have either your or any of the above dependants natural parents or any siblings died or suffered from cancer, heart disease, stroke, high blood pressure, diabetes, kidney diseases, mental disorder, tuberculosis or any hereditary disease? If yes, please provide details of present conditions, present age and age of onset.&nbsp;</b><br />
   <asp:RadioButton ID="Rdo_death_Y" runat="server" Text="Yes" 
        GroupName="Rdo_death"/>
    <asp:RadioButton ID="Rdo_death_N" runat="server" Text="No" 
        GroupName="Rdo_death"   />
    <div id ="wnl9" class="errorr" style="display:none">
    You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
    </div>
    <div id ="bnl9" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>11  </h3>
  <div id="pnl3_7">
  <p><b>Is there anything hazardous or unhealthy, connected with your occupation, or habits of life or those of any of the above dependants, which render you or them, especially liable to injury or sickness or general ill-health?&nbsp;</b><br />
  <asp:RadioButton ID="Rdo_occu_Y" runat="server" Text="Yes" 
        GroupName="Rdo_occu"/>
    <asp:RadioButton ID="Rdo_occu_N" runat="server" Text="No" 
        GroupName="Rdo_occu"   />
    <div id ="wnl10" class="errorr" style="display:none">
    You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
    </div>
    <div id ="bnl10" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>12</h3>
  <div id="pnl3_8">
  <p>
  <b>Has insurance for you or any of the dependants ever been postponed, declined, accepted on special terms, cancelled or renewal has been refused for life, accident, sickness or hospital expenses?&nbsp;</b><br />
  <asp:RadioButton ID="Rdo_declined_Y" runat="server" Text="Yes" 
        GroupName="Rdo_declined"/>
    <asp:RadioButton ID="Rdo_declined_N" runat="server" Text="No" 
        GroupName="Rdo_declined"   />
    <div id ="wnl11" class="errorr" style="display:none">
    You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
    </div>
    <div id ="bnl11" class="succ" style="display:none" onclick="openNextAccordionPanel_1();">NEXT</div>
  </p>
  </div>
  <h3>13</h3>
  <div id="pnl3_9">
  <p><b>Have you or any of the above dependants ever made a claim under an accident, sickness or medical expenses policy from Sri Lanka Insurance or any other insurance company?&nbsp;</b><br />
  <asp:RadioButton ID="Rdo_slic_Y" runat="server" Text="Yes" 
        GroupName="Rdo_slic"/>
    <asp:RadioButton ID="Rdo_slic_N" runat="server" Text="No" 
        GroupName="Rdo_slic"   />
    <div id ="wnl12" class="errorr" style="display:none">
    You cannot obtain this policy online, please <a href="/ContactUs.aspx"  target="_blank"><span style="font-weight:bold; color:#8C8C8C;">contact us</span></a>  for assistance.
    </div>
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

        I hereby declare that the particulars given by me in this proposal are true and complete and I have not withheld any information whatever, material to this proposal.  I agree that this proposal, declaration and the truth of the answers herein contained shall be the basis of contract between Sri Lanka Insurance Corporation Ltd and me.  I undertake to give immediate notice of any changes in the information given above.
        <br />
        <br />
        <strong>I further confirm I am aware:</strong>
        <br /><p style="font-size:1.0em;">
        <ul>
                            
        <li>that a waiting period of 30 days is applicable for claims, effective from the date of commencement of the Policy other than for claims due to accidental injuries.</li>
        <li>that pre-existing medical conditions are not covered.</li>
        <li>of and agree to the terms, conditions and exclusions of the Policy.</li>

        </ul>
        </p>
    <%--       I certify the correctness of the statement and I hereby agree to give notice to the Company, immediately of any variation in my profession or occupation or of any changes in my health or habits and that this warranty and agreement shall be promissory and shall form the basis of the contract between the Company and myself, and to accept a policy in the usual printed form by the Company.
     <div style="font-family:Sandaya" align="justify">
        by; i|yka m%ldYj, ksrjµ;dj iy;sl lrñ' uf.a jD;a;sfha fyda /lshdfõ lsishï fjkila jqjfyd;a fyda uf.a fi!LHfha fyda .;s mej;=ïj, lsishï fjkila jQ úfgl iud.ug tA .ek jydu okajk njg .súiñ' fï m%;s{d.drh .súiqï fmdfrdkaÿjla nj yd iud.u yd ud w;/;s .súiqfï moku jk njg o iud.fuka ksl=;a lrkq ,nk idudkH uqøs; Tmamqjla ms&#60;s.ekSugo fuhska tlÛ fjñ' 
        
        </div>--%>
        <%--<img class="img-responsive" src="../../../images/SinhalaFont.PNG" alt="Chania">--%>
           
       <%-- %> <div class="col-xs-12 hidden-sm hidden-md hidden-lg">  
        <div id ="Testt_1"><img class="img-responsive" src="../../../images/SinhalaForMobile.jpg" /> </div>   <!--Mobile-->
       
          
        <div id ="Testt_3"><img class="img-responsive" src="../../../images/size2.JPG" /> </div>   <!--Mobile-->


        <div id ="Testt_5"><img class="img-responsive" src="../../../images/Size3.JPG" /> </div>   <!--Mobile-->
       
        </div>
        
   
    <div class="hidden-xs col-sm-12 hidden-md hidden-lg">
        <img class="img-responsive" src="../../../images/SinhalaForTablet.jpg" />   <!--Tab-->
    </div>
    <div class="hidden-xs hidden-sm col-md-12 hidden-lg">
        <img class="img-responsive" src="../../../images/SinhalaForTablet.jpg" />   <!--Desktop-->
    </div>
    <div class="hidden-xs hidden-sm hidden-md col-lg-12">
        <img class="img-responsive" src="../../../images/SinhalaForTablet.jpg" />  <!--Large-->
    </div>
<br /> --%>

<br /><br />
<div class="row">
 <asp:Button ID="Button1" runat="server" Text="Confirm" onclick="Button1_Click" CssClass="btn btn-success btn-xs" />&nbsp;&nbsp;
        <button type="button" class="btn btn-danger pull-right btn-xs"  data-dismiss="modal">Decline</button>
</div>
        </div>
        <%--<div class="modal-footer">
          
        </div>--%>
      </div>
      
    </div>
  </div>

  <br /><br />
  <br />
  <br />
</div>


</center>
</div>
</div>
 <script>
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


</asp:Content>

