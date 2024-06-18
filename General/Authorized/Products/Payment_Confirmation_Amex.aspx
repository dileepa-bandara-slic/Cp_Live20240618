<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payment_Confirmation_Amex.aspx.cs" Inherits="General_Authorized_Products_Payment_Confirmation_Amex" ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

    
    <script src="/js/recaptcha_en.js" type="text/javascript"></script>
   
    <link href="/css/error_msg.css" rel="stylesheet" type="text/css" />

    <script src="/js/jquery-3.5.1.js"></script>

     <link href="/css/fieldset.css" rel="stylesheet" />

      <%--  <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="viewport" content="width=device-width, initial-scale=1">--%>

    <title>Home | Sri Lanka Insurance ~ Like a father - Like a Mother ~</title>
<%--   
    <link rel="icon" href="assets/images/xfavicon.png" type="image/png">--%>

    <link rel="stylesheet" href="/assets/css/bootstrap_corp.min.css" />
    <link rel="stylesheet" href="/assets/css/font-awesome_corp.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/css/animate_corp.css" />
    <link rel="stylesheet" type="text/css" href="/assets/css/slick_corp.css" />
    <link rel="stylesheet" type="text/css" href="/assets/css/component_corp.css" />
    <link rel="stylesheet" type="text/css" href="/assets/css/index_corp.css" />
    <link rel="stylesheet" type="text/css" href="/assets/css/index-fix_corp.css" />
   
    <style>.scroll-down{bottom:15%}</style>
    <link href="/css/free.css" rel="stylesheet" />

    <link href="/css/jquery-ui.css" rel="stylesheet" />
    <script src="/js/jquery-ui.js"></script>

    <link href="/assets/font-awesome/4.1.0/css/font-awesome.min.css" rel="stylesheet" />

   <%-- <script type="text/javascript" language="javascript">
    //disabling back, needs more testing
        history.pushState(null, null, document.URL);

        
        window.addEventListener('popstate', function () {
        
            $('#myModal').modal('show');
            history.pushState(null, null, document.URL);
        });

        document.addEventListener("click", function(){
  document.getElementById("demo").innerHTML = "Hello World";
});

        
        $(document).ready(function () {
           
            $('#myModal').modal('hide');
        });

    </script>--%>

       <script type = "text/javascript" >
           history.pushState(null, null, location.href);
           history.back();
           history.forward();
           window.onpopstate = function ()
           { history.go(1); }; 

    </script>


    <noscript>

    <div class="noscriptmsg" style="text-align:center">
    <center>
    <div class="container">
		<div class="row header">
        <div id="tt" style="width:60% "><br/><br/>
<fieldset style="border: 1px solid #B87E7E; font-weight:normal; font-family:Times New Roman; "  class="bg-danger ">
<h5>Please enable javascripts and refresh the page</h5><br/>
</fieldset></div>
</div>
</div></center>
    </div>
</noscript>
 <script>
      function noSSL () {
         var str = window.location.href;
         var res = str.substring(0, 5);

         if (res == "https") {
       
         }
         else {

             var inputs = document.getElementsByTagName("INPUT");
             for (var i = 0; i < inputs.length; i++) {
                 
                  inputs[i].disabled = false;
              
             }
         }
     }
</script>
        <script language="javascript" type="text/javascript">

            $(function () {
                $('.banner').unslider({ loop: true, dots: true, speed: 800, delay: 5000 });
            });

            function setCookie(cname, cvalue, exdays) {
                var d = new Date();
                d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
                var expires = "expires=" + d.toUTCString();
                document.cookie = cname + "=" + cvalue + "; " + expires;
                document.getElementById('ribn').style.display = 'none';
                return false;

            }
            function getCookie(cname) {
                var name = cname + "=";
                var ca = document.cookie.split(';');
                for (var i = 0; i < ca.length; i++) {
                    var c = ca[i];
                    while (c.charAt(0) == ' ') c = c.substring(1);
                    if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
                }
                return "";
            }

            function hide(cname) {
                var x = getCookie(cname);
                if (x == 'Y') {
                    document.getElementById('ribn').style.display = 'none';

                }
                else {
                    $("#ribn").slideDown();
                }
            }
    </script>
   


</head>
<body>
    
      <link href="/css/modal.css" rel="stylesheet" />
        <form id="form1" runat="server">
    <%--<form id="form1" runat="server">
        <div>
        </div>
    </form>--%>
  
<%--<fieldset id="confirmation">
    <legend>Review Payment Details</legend>
    <div>
        <%
            foreach (var key in Request.Form.AllKeys)
            { 
                Response.Write("<div>");
                Response.Write("<span class=\"fieldName\">" + key + ":</span><span class=\"fieldValue\">" + Request.Params[key] + "</span>");
                Response.Write("</div>");
            }
        %>
    </div>
</fieldset>--%>

     <div class="main_wrapper">
                <div class="wrapping-header">


   <header class="default-header top-level-navigation">
        <div class="container custom_container">
            <div class="row">
                <div class="col-md-12">
                    <nav class="navbar navbar-expand-lg navbar-light">
                        <button class="navbar-toggler mainMenu_button collapsed" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
                            <span class="main_menu_text">Main menu</span><span class="navbar-toggler-icon"></span>
                        </button>
                        <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
                            <div class="navbar-nav">
                                     
                                 <a class="nav-item nav-link active" id="personal" href="">Personal <span class="sr-only">(current)</span></a>
                                        <a class="nav-item nav-link " id="personal" href="">Business <span class="sr-only">(current)</span></a>
                                        <a class="nav-item nav-link " id="personal" href="">About Us <span class="sr-only">(current)</span></a>
                            </div>
                        </div>
                    </nav>
                    <ul>
               



                    </ul>
                </div>
            </div>
        </div>
    </header>

    <header class="default-header first-level-navigation">
        <div id="menu_area" class="menu-area">
            <div class="container custom_container">
                <div class="row">
         

                     <a class="navbar-brand" href="/Default.aspx">
                                       <img src="/assets/images/logo.jpg" />
                    </a>
                    <div class="submenu_mobile">
                        <span></span>
                    </div>
            
                    <div class="main">

                           <nav id="cbp-hrmenu" class="cbp-hrmenu personal">

                                                <ul class="">
                                                    <li class="">
                                                             <a href="#" style="font-size:16px">Life </a>
                                                  
                                                        <div class="cbp-hrsub">
                                                            <div class="cbp-hrsub-inner">
                                                                <div>
                                                                    <ul>
                                                                        <li class="">
                                                                            <a href="/Life/Authorized/ClientHome.aspx" style="font-size:16px">Manage My Policies</a>
                                                                            <ul>
                                                                            </ul>
                                                                        </li>
                                                                        <li class="">
                                                                            <a href="/Life/Authorized/MakePayment.aspx" style="font-size:16px">Third Party Premiums</a>
                                                                            <ul>
                                                                            </ul>
                                                                        </li>

                                                                          <li class="">
                                                                            <a href="/Life/Authorized/PolicyRevival.aspx" style="font-size:16px">Policy Revivals</a>
                                                                            <ul>
                                                                            </ul>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                                
                                                    <li class="">
                                                        <a href="#" style="font-size:16px">General </a>
                                                        <%-- <a href="#">General </a>--%>
                                                        <div class="cbp-hrsub">
                                                            <div class="cbp-hrsub-inner">
                                                                <div>
                                                                    <ul>
                                                                        <li class="">
                                                                            <a href="/General/Authorized/ClientHome.aspx" style="font-size:16px">Manage Policies</a>
                                                                            <ul>
                                                                            </ul>
                                                                        </li>
                                                                        <li class="">
                                                                            <a href="/General/Authorized/General_Products.aspx" style="font-size:16px">Purchase Online</a>
                                                                            <ul>
                                                                            </ul>
                                                                        </li>

                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                             
                                                 <%--   <li class="">
                                                        <a href="/ContactUs.aspx">Contact us </a>
                                                    </li>--%>

                                                       <li class="">
                                                        <div>
                                                           
                                            <a href="/ContactUs.aspx" style="font-size:16px">Contact us</a>
                                                                      
                                                            </div>
                                                        </li>

                                         
                                                      <li style="float:right;padding-right: 9%">

                                                        <a href="#" style="font-size:16px"><span class="glyphicon glyphicon-cog" style="display:inline;"></span> Settings</a>

                                                        <%-- <a href="#">Profile </a>--%>
                                                        <div class="cbp-hrsub">
                                                            <div class="cbp-hrsub-inner">
                                                                <div>
                                                                    <ul>
                                                                        <li class="">
                                                                            <%--<span class="glyphicon glyphicon-edit">--%> <a href="/Authorized/EditProfile.aspx" style="font-size:16px"><i class="glyphicon glyphicon-edit"></i> Edit Profile</a><%--</span>--%><ul>
                                                                            </ul>
                                                                        </li>

                                                                        <li class="">
                                                                            <%--<span class="glyphicon glyphicon-transfer">--%> <a href="/Authorized/ChangePassword.aspx" style="font-size:16px"><i class="glyphicon glyphicon-transfer"></i> Change Password</a><%--</span>--%><ul>
                                                                            </ul>
                                                                        </li>

                                                                       <%-- <li>
                                                                             <asp:LoginStatus ID="HeadLoginStatus" style="font-size:16px" runat="server" LogoutAction="Redirect" LogoutText="<i class='glyphicon glyphicon-log-out'></i> Logout" LogoutPageUrl="~/Default.aspx" />
                                                                            
                                                                           
                                                                            
                                                                        </li>--%>

                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </li>
                                                    <%-- </ul>
                            </div>--%>
                                                </ul>
                                            </nav>
                  
<script src="/js/jquery-3.5.1.js"></script>

<script src="/js/modernizr.js"></script>

<script src="/js/bootstrap.min.js"></script>
              
<script src="/js/wow.js"></script>
                
<script src="/js/slick.min.js"></script>
          <script src="/js/cbpHorizontalMenu.min.js"></script>
     <script src="/js/index.js"></script>

<script type="text/javascript">
    $('#level1').change(function(){
        var id = $(this).val();
        $.ajax({
            type: "GET",
            url: base_url + "/supportmenu/"+id,
            dataType: 'json',
            success: function(res) {
                $('#level2').html(' ');
                if(res == ''){
                    $(".help-error").text("*Please select what you want");
                    $("#homePlans").hide();
                }else{
                    $(".help-error").text(" ");
                    res.forEach(function(e, i){
                        $('#level2').append($('<option></option>').val(e.mainDetails.id).text(e.mainDetails.title));
                    });
                }
            },
            error:function(request, status, error) {
                console.log("ajax call went wrong:" + request.responseText);
            }
        });
        $("#homePlans").show();
    });

    $('#submitButton').click( function() {
        var l1 = document.forms["searchService"]["level1"].value;
        var l2 = document.forms["searchService"]["service_opt"].value;
        if (l1==0 || l2==0)
        {
            $(".help-error").text("*Please select what you want");
            return false;
        }
        else{
            $.ajax({
                url: base_url + '/searchservice',
                type: 'post',
                data: $('form#searchService').serialize(),
                dataType: 'json',
                success: function(data) {
                    window.location.href = data.url;
                },
                error:function(request, status, error) {
                    console.log("ajax call went wrong:" + request.responseText);
                }
            });
        }
    });
</script>    <div class="modal fade news-modal" id="newsModalReadMore" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title news-title" id="exampleModalLongTitle"></h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="newsbody"></div>
            </div>

        </div>
    </div>
</div>


<script>
    $(document).ready(function() {
        $('.footer-icons img').attr('src', function(index, src) {
            return base_url + '/' +this.getAttribute('src');
        });
    });
</script>


<script>
    function newsDetailsView(id)
    {
        $.ajax({
            type: "GET",
            url: base_url + "/newsDetailsView/"+id,
            dataType: 'json',
            success: function(res) {
                // update modal content
                $('#exampleModalLongTitle').text(res.title);
                $('.modal-body .newsbody').html(res.body);
                appendBaseUrl();
                // show modal
                $('#newsModalReadMore').modal('show');

            },
            error:function(request, status, error) {
                console.log("ajax call went wrong:" + request.responseText);
            }
        });
    }
    function appendBaseUrl() {
        $('.newsbody img').attr('src', function(index, src) {
            return base_url + '/' +this.getAttribute('src');
        });
    }
</script>

            <%--     </LoggedInTemplate>
    </asp:LoginView>
                            </div>--%>

                                            </div>
                </div>
            </div>
        </div>
    </header>
</div>

        </div>

    <div id="frm_tag" style="min-height:600px">

         <asp:Panel ID="pnlEncryptedData" runat="server" style="display:none;visibility:hidden">
                        <table>
                            <tr>
                                <td> IPG Server URL </td>
                                <td> 
                                    <asp:Label ID="lblSvrUrl" runat="server" Text="Label"></asp:Label></td>
                            </tr>  
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>                                                  
                            <tr>
                                <td>
                                    Plain Text Invoice</td>
                                <td>
                                    <asp:TextBox ID="txtPlainInvoice" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    Encrypted Invoice</td>
                                <td>
                                    <asp:TextBox ID="encryptedInvoicePay" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                        </table>
                    </asp:Panel>
   
<%--            //if (Request.Form.Count > 0)
            //{
            //    Response.Redirect("/Default.aspx");
            //}
            //else
            //{


            //IDictionary<string, string> parameters = new Dictionary<string, string>();
            //foreach (var key in Request.Form.AllKeys)
            //{
            //    Response.Write("<input type=\"hidden\" id=\"" + key + "\" name=\"" + key + "\" value=\"" + Request.Params[key] + "\"/>\n");
            //    parameters.Add(key, Request.Params[key]);


            //}

            //try
            //{
            //    Response.Write("<input type=\"hidden\" id=\"signature\" name=\"signature\" value=\"" + Security.sign(parameters,"G") + "\"/>\n");
            //}
            //catch
            //{
            //     Response.Redirect("/General/Authorized/Products/Payment.aspx");
            //}--%>
                 <%

                     if (Request.Params["txn_amt"] == null || Request.Params["cur"] == null)
                     {
                         Response.Redirect("/Default.aspx");
                     }
                     else
                     {
                         //IShroff objIShroff = new IShroff();
                         //if(objIShroff.getErrorCode().ToString() != null && !objIShroff.getErrorCode().ToString().Equals("00"))
                          if(hdf_Amex_Err_State.Value == "TRUE")
                         {
                             //if (Convert.ToInt32(objIShroff.getErrorCode()) < 0)
                             //{
                             //    Response.Write("</br></br>");
                             Response.Write("</br></br></br>");
                             Response.Write("<div>");
                             Response.Write("<strong><p style=\"text-align:center;\"><font color=\"red\">Payment Processing Issue </font></p></strong>");
                             Response.Write("</div>");
                             Response.Write("</br></br></br>");
                             //}
                             //else
                             //{

                             //}
                         }
                         if(hdf_Amex_Err_State.Value == "FALSE")
                         {
                             Response.Write("</br></br>");
                             Response.Write("<div>");
                             Response.Write("<h2 style=\"text-align:center;\"><font color=\"green\">Payment Summary </font></h2>");
                             Response.Write("</div>");
                             Response.Write("</br></br></br>");
                             Response.Write("<div>");
                             Response.Write("<strong><p style=\"text-align:center;\"><font color=\"blue\">Payment Amount : </font>" + Request.Params["txn_amt"] + "</p></strong>");
                             Response.Write("</div>");
                             Response.Write("<div>");
                             Response.Write("<strong><p style=\"text-align:center;\"><font color=\"blue\">Currency :  </font>" + Request.Params["cur"] + "</p></strong>");
                             Response.Write("</div>");

                         }
                     }





  %>

   <p style="text-align:center;"><asp:button id="btnSubmit" runat="server" Text="Submit" PostBackUrl="~/Response.aspx" Visible="False"  class="btn btn-primary" /><asp:label id="lblTransType" runat="server" Visible="False"></asp:label>
       <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CausesValidation="False"  class="btn btn-primary" />
     </p>
       
        </div>

                                 <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
  <div class="modal-dialog" role="document">
 <center>
    <div class="modal-content  modal-sm">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h5 class="modal-title" id="myModalLabel"><strong>Warning !</strong> </h5>
      </div>
      <div class="modal-body">
       Back button is disabled for this page. Please use the navigation links in the menu  to start a new transaction.
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
      </div>
    </div></center>
  </div>
</div>


     <footer>
    <div class="footer-cover">
        <div class="footer-links">
            <div class="container custom_container">
                <div class="row">
                    <div class="col-md-12 key-links">
                        <div class="footer-heading">Quick Links</div>
                        <ul>
                                                                                            <li><a href="http://www.motortraffic.wp.gov.lk" class=""target="">e-Revenue License</a></li>
                                                                                            <li><a href="https://www.srilankainsurance.net/LandingPage.aspx" class="" target=""><span class="glyphicon glyphicon-phone" style="display:inline;"></span> Customer App</a></li>
                                                                                          <%--  <li><a href="https://192.168.101.90/images/pdf/approved-garages-list-2019.pdf" class=""target="_blank">Approved Garages</a></li>
                                                                                            <li><a href="https://192.168.101.90/en/footer-menu/contact-us" class=""target="">Contact Us</a></li>
                                                                                            <li><a href="https://192.168.101.90/en/footer-menu/news-events" class=""target="">News &amp; Events</a></li>
                                                                                            <li><a href="https://192.168.101.90/en/footer-menu/branch-locator" class=""target="">Branch Locator</a></li>
                                                                                            <li><a href="https://www.srilankainsurance.com/en/tenders" class=""target="_blank">Tenders</a></li>--%>
                                                    </ul>
                    </div>
                </div>
                <hr>
                <div class="row">
                    <div class="col-md-4 bottom-margin">
<div class="footer-heading">Hotlines</div>
<div class="icon-links"><a class="footer-go-link no-arrow footer-phone" href="tel:+94112357357"><%--<span class="fas fa-phone"><img src="assets/images/phone-call.png" /></span>--%><i class='glyphicon glyphicon-earphone'></i> +94 11 235 7357</a></div>
<div class="icon-links"><a class="footer-go-link no-arrow footer-phone" href="tel:+94117357357"><%--<span class="footer-icons"><img src="assets/images/phone-call.png" /></span>--%><i class='glyphicon glyphicon-earphone'></i> +94 11 735 7357</a></div>
<div class="icon-links"><a class="footer-go-link no-arrow footer-phone" href="tel:+94115357357"><%--<span class="footer-icons"><img src="assets/images/phone-call.png" /></span>--%><i class='glyphicon glyphicon-earphone'></i> +94 11 535 7357</a></div>
<div class="icon-links"><a class="footer-go-link no-arrow footer-phone" href="tel:+94114357357"><%--<span class="footer-icons"><img src="assets/images/phone-call.png" /></span>--%><i class='glyphicon glyphicon-earphone'></i> +94 11 435 7357</a></div>
<div class="icon-links"><a class="footer-go-link no-arrow footer-plane" href="mailto:email@srilankainsurance.com"><%--<span class="footer-icons"><img src="assets/images/paper-plane.png" /></span>--%> <%--<i class='far fa-paper-plane'></i>--%> email@srilankainsurance.com</a></div>
</div>
<div class="col-md-4 bottom-margin">
<div class="footer-heading">Head Office</div> 	
<p class="address"><span class="footer-icons"><img runat="server" id="imgBuild" src="./assets/images/slic-building.png" /></span> Rakshana Mandiraya<br />No.21, <br />Vauxhall Street, <br />Colombo 02, <br /> Sri Lanka</p>
</div>                 <%--   <div class="col-md-3 bottom-margin">
                        <div class="footer-heading">Logins</div>
                        <ul>
						
                            <div class="icon-links"><a class="footer-go-link no-arrow" href="https://apps.srilankainsurance.com/onlinepayments/customerdetails.aspx" target="_blank">Online Payments</a></div>
                            <div class="icon-links"><a class="footer-go-link no-arrow" href="https://apps.srilankainsurance.com/PEGINEW.asp" target="_blank">Pegi Online</a></div>
                            <div class="icon-links"><a class="footer-go-link no-arrow" href="https://apps.srilankainsurance.com/marworks/log.htm" target="_blank">E-Marine</a></div>
                            <div class="icon-links"><a class="footer-go-link no-arrow" href="https://apps.srilankainsurance.com/customerportal/loginpage.aspx" target="_blank">SLI Easy Access</a></div>
                            <div class="icon-links"><a class="footer-go-link no-arrow" href="https://apps.srilankainsurance.com/agenworks/Signin.asp" target="_blank">Advisor-Broker Area</a></div>


					
                        </ul>
                    </div> --%>
                    <div class="col-md-4 bottom-margin">
                        <div class="footer-heading">Get Social with us on</div>
                        <ul class="social-media-icons">
                            <li><a href="https://www.facebook.com/SLICorporation" target="_blank" class="social-icon"><i class="fa fa-facebook" aria-hidden="true"></i></a></li>
                            <li><a href="https://twitter.com/SLICInsurance" target="_blank" class="social-icon"><i class="fa fa-twitter" aria-hidden="true"></i></a></li>
                            <li><a href="https://www.linkedin.com/company/sri-lanka-insurance-corporation-limited/" target="_blank" class="social-icon"><i class="fa fa-linkedin" aria-hidden="true"></i></a></li>
                            <li><a href="https://www.instagram.com/srilankainsuranceslic" target="_blank" class="social-icon"><i class="fa fa-instagram" aria-hidden="true"></i></a></li>
                            <li><a href="https://www.youtube.com/channel/UCzgCSmU5gVDsuahbhebeotg/featured" target="_blank" class="social-icon"><i class="fa fa-youtube" aria-hidden="true"></i></a></li>
                            
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="footer-copyrights">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="bottom_footer">&copy; All rights reserved. Sri Lanka Insurance. <%--Solution by <a href="http://www.affno.com/" target="_blank">Affno</a>--%></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</footer>
    
    <%--  <asp:HiddenField ID="hdf_order_amount" runat="server" />
      <asp:HiddenField ID="hdf_order_currency" runat="server" />--%>

            <asp:HiddenField ID="hdf_Amex_Err_State" runat="server" />
            
</form>
</body>

  <%--    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>--%>

   <%-- <script>
    $(document).ready(function() {
        function disableBack() { window.history.forward() }

        window.onload = disableBack();
        window.onpageshow = function(evt) { if (evt.persisted) disableBack() }
    });
</script>--%>
</html>
