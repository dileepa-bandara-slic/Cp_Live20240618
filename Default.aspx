<%@ Page Title="Sri Lanka Insurance - Client Portal" Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        @media screen and (max-width: 992px) {
            p, .btn, input, div, span, h4 {
                font-size: 100%;
                font-family: Tahoma;
            }

            h1 {
                font-size: 40px;
                font-family: Tahoma;
            }

            h2 {
                font-size: 35px;
                font-family: Tahoma;
            }

            h3 {
                font-size: 30px;
                font-family: Tahoma;
            }

            .divSmall {
                display: none;
            }

            .divLarge {
                display: block;
            }
        }

        @media screen and (max-width: 768px) {
            p, .btn, input, div, span, h4 {
                font-size: 98%;
                font-family: Tahoma;
            }

            h1 {
                font-size: 30px;
                font-family: Tahoma;
            }

            h2 {
                font-size: 25px;
                font-family: Tahoma;
            }

            h3 {
                font-size: 20px;
                font-family: Tahoma;
            }

            .divSmall {
                display: block;
            }

            .divLarge {
                display: none;
            }
        }


        @media screen and (min-width: 992px) {
            p, .btn, input, div, span, h4 {
                font-size: 100%;
                font-family: Tahoma;
            }

            h1 {
                font-size: 40px;
                font-family: Tahoma;
            }

            h2 {
                font-size: 35px;
                font-family: Tahoma;
            }

            h3 {
                font-size: 30px;
                font-family: Tahoma;
            }

            .divSmall {
                display: none;
            }

            .divLarge {
                display: block;
            }
        }

        @media screen and (min-width: 1200px) {
            p, .btn, input, div, span, h4 {
                font-size: 100%;
                font-family: Tahoma;
            }

            h1 {
                font-size: 40px;
                font-family: Tahoma;
            }

            h2 {
                font-size: 35px;
                font-family: Tahoma;
            }

            h3 {
                font-size: 30px;
                font-family: Tahoma;
            }

            .divSmall {
                display: none;
            }

            .divLarge {
                display: block;
            }
        }


        #footerContent1 {
            border-top: 0px;
        }

        #footerContentDiv {
            text-align: left;
            line-height: 20px;
        }

        #div1 {
            position: relative;
            height: 20%;
            min-height: 200px;
            width: 50%;
        }

        #div2 {
            width: 25%;
            position: absolute;
            left: 75%;
            min-height: 200px;
            z-index: 100;
            opacity: 0.9;
            background: #eeeeee;
            font-size: 80%;
        }

        #div3 {
            width: 25%;
            position: absolute;
            left: 75%;
            min-height: 20px;
            z-index: 100;
            opacity: 0.9;
            background: #eeeeee;
            font-size: 80%;
        }

        .carousel-inner > .item > img, .carousel-inner > .item > a > img {
            width: 100%;
            margin: auto;
        }

        @media screen and (max-width: 480px) {
            .slideinimage1 {
                background-image : url("/assets/images/banner-image-add1.png");
                width: 50%;
                height: 50%;
            }
        }


    </style>

      
        <script language="javascript" type="text/javascript">

        function randomNumberFromRange(min, max) {
            return Math.floor(Math.random() * (max - min + 1) + min);
        }

        $(document).ready(function () {
            var minNumber = 999;
            var maxNumber = 100
            var randomNumber = randomNumberFromRange(minNumber, maxNumber);
            $("input[id$='hdn_client']").attr('value', randomNumber);
            //alert("test");
            $("input[id$='LoginButton']").click(function () {
                //alert("test");
                var pwd = $("input[id$='Password']").val();
                var pwd_en1 = $.base64.encode(pwd);
                var pwd_hashed_cl = pwd_en1.concat($("input[id$='hdn_client']").val());
                var pwd_hashed_ser = pwd_hashed_cl.concat($("input[id$='hdn_server']").val());
                var int_end = $.base64.encode(pwd_hashed_ser);
                var finalenc = $.base64.encode(int_end);

                $("input[id$='hdn_pwd_sbmt']").attr('value', finalenc);

                var len = $("input[id$='Password']").val().length;

                var boru = "";

                var i = 0;
                while (len != i) {
                    boru = boru.concat('*');
                    i++;
                }

                $("input[id$='Password']").val(boru);
                $("input[id$='hdn_server']").val(boru);

            });
        });


        //This is the suggested way by KPMG 
//        $(document).ready(function () {
//            var minNumber = 999;
//            var maxNumber = 100

//            var randomNumber = randomNumberFromRange(minNumber, maxNumber);

//            $("input[id$='hdn_client']").attr('value', randomNumber);


//            $("input[id$='LoginButton']").click(function () {
//                

//                var pwd = $("input[id$='Password']").val();
//                var pwd_hashed = sha512(pwd);
//                var pwd_hashed_cl = pwd_hashed.concat($("input[id$='hdn_client']").val());
//                var pwd_hashed_ser = pwd_hashed_cl.concat($("input[id$='hdn_server']").val());
//                var all_hashed = sha512(pwd_hashed_ser);
//           

//                $("input[id$='hdn_pwd_sbmt']").attr('value', all_hashed);

//                var len = $("input[id$='Password']").val().length;

//                var boru = "";

//                var i = 0;
//                while (len != i) {
//                    boru = boru.concat('*');
//                    i++;
//                }

//                $("input[id$='Password']").val(boru);
//                $("input[id$='hdn_server']").val(boru);
//            });
//        });
    </script>
    <style>
        .liremove {
            background-color: transparent;
        }
        
        .btn-link:hover, .btn-link:focus, .btn-link:active, .btn-link.active {
            color: #337ab7 !important;
        }
        
          .mainmenu a, .navbar-default .navbar-nav > li > a, .mainmenu ul li a, .navbar-expand-lg .navbar-nav .nav-link {
            font-weight: bold;
        }
    </style>
    <script type="text/javascript" src="js/sha512.js"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript">
    //document.body.onload = function () { noSSL(); }
</script>

    <script language="javascript" type="text/javascript">  
            function a(j) {
                var itemhash = $('.cbp-hrmenu > ul > li > a').attr('href');
                            alert(itemhash);
  if (itemhash == "#") {
    if (d !== -1) {
                b.eq(d).removeClass("cbp-hropen")
            }
            var i = $(j.currentTarget).parent("li"),
              h = i.index();
    if (d === h) {
                i.removeClass("cbp-hropen");
            d = -1
    } else {
                i.addClass("cbp-hropen");
            d = h;
            c.on("click", e)
          }
          return false
  } else {
    return true;
          }
      }
                                 
    </script>
           <style>

body {
  max-width: 100%;
  overflow-x: hidden;
}

</style>

    
<script type="text/javascript">
  
    var str = window.location.href;
    var str2 = str.substring(str.length - 6, str.length);
    if (str2 == "mobile") {
        
        $(window).load(function()
        {
            $('#myModal').modal('show');
            
        });
    }
</script>

    <script>
    function noSSL() {
        var str = window.location.href;
        var res = str.substring(0, 5);

        if (res == "https") {
            var btn = document.getElementById("ContentPlaceHolder1_ContentPlaceHolder1_LoginView1_LoginUser_LoginButton");
            
            btn.disabled = false;
        }
        else {

            var inputs = document.getElementsByTagName("INPUT");
            for (var i = 0; i < inputs.length; i++) {
                //if (inputs[i].type === 'submit') {
                inputs[i].disabled = true;
                // }
            }
            window.location = "/httpswarning.htm";
        }
    }
</script>

<%--     <script language="javascript" type="text/javascript">  
          $('.nav').on('click', '> li > a', function(){
          var $li = $(this).parent();
          $li.siblings().not($li.addClass('cbp-hropen')).removeClass('cbp-hropen');
    });
         </script>--%>

    <%--<script type="text/javascript">
    //const urlParams = new URLSearchParams(window.location.search);
   // const myParam = urlParams.get('dev');
    // response.write(myParam);

    //var foo = getUrlParameter('dev');
    var str = window.location.href;
    var str2 = str.substring(str.length - 6, str.length);
    if (str2 == "mobile") {
        
         //window.alert("Please use your App Login credentials to login to Customer portal.");
        $(window).load(function()
        {
            $('#myModal').modal('show');
            
        });
    }
</script>--%>




      <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
        <%--  <button type="button" class="close" data-dismiss="modal">&times;</button>--%>
          <h4 class="modal-title"> Customer Portal Login </h4>
        </div>
        <div class="modal-body">
          <p>Please use your SLIC Customer App Login credentials (Username / Password) to login to Customer portal.</p>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>


    <div class="row">

<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
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
                       
                             <a class="navbar-brand" href="">
                                 <img src="assets/images/logo.jpg" />
                    </a>
                    <div class="submenu_mobile">
                        <span></span>
                    </div>

                            <div class="main">

                                <div id="ss">
                                    <asp:LoginView ID="LoginView2" runat="server">
                                        <LoggedInTemplate>




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
                                                                      
                                                                           <li class="">
                                                                            <a href="/Life/Authorized/ProposalPayment.aspx" style="font-size:16px">Proposal Payments</a>
                                                                            <ul>
                                                                            </ul>
                                                                           </li>
                                                                       

                                                                       <%-- </li>
                                                                            <li class="">
                                                                            <a href="/Life/Authorized/ProposalPayment.aspx" style="font-size:16px">Proposal Payments</a>
                                                                            <ul>
                                                                            </ul>
                                                                        </li>--%>


                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                           
                                                    <li class="">
                                                        <a href="#" style="font-size:16px">General </a>
                                                     
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
                                             
<%--                                                    <li>  
                                                                 
                                                       <asp:HyperLink ID="HyperLink2"  NavigateUrl="~/ContactUs.aspx" runat="server">Contact us</asp:HyperLink>
                                                    </li>--%>

                                                    <li class="">
                                                        <div>
                                                           
                                            <a href="/ContactUs.aspx" style="font-size:16px">Contact us</a>
                                                                      
                                                            </div>
                                                        </li>

                                                    	<%--	<li class=""><asp:HyperLink ID="HyperLink2"  NavigateUrl="~/ContactUs.aspx" runat="server">Contact us</asp:HyperLink></li>
                                                 --%>
                                         
                                                      <li style="float:right;padding-right: 9%">

                                                        <a href="#" style="font-size:16px"><span class="glyphicon glyphicon-cog"></span> Settings</a>

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

                                                                        <li>
                                                                           
                                                                                <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" style="font-size:16px" LogoutText="<i class='glyphicon glyphicon-log-out'></i> Logout" LogoutPageUrl="~/Default.aspx" />
                                                                            
                                                                            
                                                                        </li>

                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </li>
                                                    <%-- </ul>
                            </div>--%>
                                                </ul>
                                            </nav>

                                        </LoggedInTemplate>
                                    </asp:LoginView>

  
                                     <script src="assets/js/jquery-3.5.1.min.js"></script>
                                    <script src="assets/js/modernizr.js"></script>
                                    <script src="assets/js/bootstrap.min.js"></script>
                                    <script src="assets/js/wow.js"></script>
                                    <script src="assets/js/slick.min.js"></script>
                                    <script src="assets/js/cbpHorizontalMenu.min.js"></script>
                                    <script src="assets/js/index.js"></script>

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
                                    </script>
                                    <div class="modal fade news-modal" id="newsModalReadMore" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
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


                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </header>
        </div>

    </div>

        <div class="row">
       <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
    <div class="home-banner">
        <div class="home-banner-text wow fadeInLeft" data-wow-delay="1s">
            <span>Welcome to the world of</span><br>
            Sri Lanka Insurance
        </div>
        <div class="home-slider">

             
                    <div id="myCarousel" class="carousel slide" data-ride="carousel">
                        <asp:LoginView ID="LoginView1" runat="server">
                            <AnonymousTemplate>
                                <asp:Login ID="LoginUser" runat="server" OnAuthenticate="LoginUser_Authenticate"
                                    EnableViewState="False" FailureText="Your login attempt was not successful. Please visit below link if you have forgotten password.">
                                    <LayoutTemplate>
                                        <div id="div2" class="divLarge" align="center" style="font-size: 100%">
                                            <br>
                                            <div class="row">
                                                <div class="col-xs-1 col-sm-1 visible-sm col-md-1 visible-md col-lg-1 visible-lg">
                                                </div>
                                                <div class="col-xs-12 visible-xs col-sm-10 visible-sm col-md-10 visible-md col-lg-10 visible-lg">
                                                    <asp:TextBox ID="UserName" type="text" runat="server" MaxLength="15" autocomplete="new-password"
                                                        class="form-control input-sm" placeholder="Username" ValidationGroup="Login1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Required"
                                                        ControlToValidate="username" ValidationGroup="Login1" Font-Bold="True" Font-Names="Calibri"
                                                        Font-Size="8pt" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-xs-1 col-sm-1 visible-sm col-md-1 visible-md col-lg-1 visible-lg">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-1 col-sm-1 visible-sm col-md-1 visible-md col-lg-1 visible-lg">
                                                </div>
                                                <div class="col-xs-12 visible-xs col-sm-10 visible-sm col-md-10 visible-md col-lg-10 visible-lg">
                                                    <asp:TextBox ID="Password" runat="server" class="form-control input-sm" TextMode="Password"
                                                        MaxLength="15" placeholder="Password" ValidationGroup="Login1" autocomplete="new-password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Required"
                                                        ControlToValidate="Password" ValidationGroup="Login1" Font-Bold="True" Font-Names="Calibri"
                                                        Font-Size="8pt" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-xs-1 col-sm-1 visible-sm col-md-1 visible-md col-lg-1 visible-lg">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-1 col-sm-1 visible-sm col-md-1 visible-md col-lg-1 visible-lg">
                                                </div>
                                                <div class="col-xs-12 visible-xs col-sm-10 visible-sm col-md-10 visible-md col-lg-10 visible-lg">
                                             <%--       <asp:Button ID="LoginButton" CssClass="btn btn-primary btn-block" runat="server" CommandName="Login"
                                                        Text="Login" ValidationGroup="Login1" Enabled = "false"/>--%>

                                                           <asp:Button ID="LoginButton" CssClass="btn btn-primary btn-block" runat="server" CommandName="Login"
                                                        Text="Login" ValidationGroup="Login1"/>


                                                    <asp:PlaceHolder runat="server" ID="logn" Visible="false">
                                                        <p class="img-rounded btn-danger" style="background-color: #F54947; padding: 5px;">
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="#FFFFFF"></asp:Label>
                                                        </p>
                                                    </asp:PlaceHolder>
                                                </div>
                                                <div class="col-xs-1 col-sm-1 visible-sm col-md-1 visible-md col-lg-1 visible-lg">
                                                </div>
                                            </div>

                                            <div class="row">
                                                <asp:PlaceHolder ID="plh_error" runat="server" Visible="false">
                                                    <div class="col-xs-12 visible-xs col-sm-12 visible-sm col-md-12 visible-md col-lg-12 visible-lg">
                                                        <br />
                                                        <p class=" btn-danger" style="background-color: #F54947; padding: 0px;">
                                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></p>
                                                    </div>
                                                </asp:PlaceHolder>
                                            </div>

                                            <div class="row">
                                                <div class="col-xs-12 visible-xs col-sm-12 visible-sm col-md-12 visible-md col-lg-12 visible-lg">
                                                    <asp:HyperLink ID="RegisterHyperLink" CssClass="btn btn-link" runat="server" align="center"
                                                        EnableViewState="false" NavigateUrl="~/Register.aspx"><img alt='' src="images/reguser.png" /> Register for service</asp:HyperLink>
                                                </div>
                                            </div>



                                            <div class="row">
                                                <div class="col-xs-1 col-sm-1 visible-sm col-md-1 visible-md col-lg-1 visible-lg">
                                                </div>
                                                <div class="col-xs-12 visible-xs col-sm-10 visible-sm col-md-10 visible-md col-lg-10 visible-lg">
                                                    <small>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ForgotPassword.aspx">Forgot Password?</asp:HyperLink></small>
                                                    <asp:HiddenField ID="hdn_server" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hdn_client" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hdn_pwd_sbmt" runat="server" ClientIDMode="Static" />
                                                </div>
                                                <div class="col-xs-1 col-sm-1 visible-sm col-md-1 visible-md col-lg-1 visible-lg">
                                                </div>
                                            </div>
                                        </div>
                                        <div id="div3" class="divSmall" align="center" style="font-size: 100%">
                                            <div class="row">
                                                <div class="col-xs-12 visible-xs col-sm-12 visible-sm col-md-12 visible-md col-lg-12 visible-lg">
                                                    <a href="Login.aspx"><b>Login </b></a><b>| </b><a href="Register.aspx"><b>Register</b></a>
                                                </div>
                                            </div>
                                    </LayoutTemplate>
                                </asp:Login>
                            </AnonymousTemplate>

                        </asp:LoginView>
                    </div>

               <div class="row">
       <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="home-banner">
                        <div class="home-banner-text wow fadeInLeft" data-wow-delay="1s">
                            <span>Welcome to the world of</span><br>
                            Sri Lanka Insurance
                        </div>

                       <%-- <div class="home-slider">--%>
               

               
            <div class="slider-item slider-item1 wow fadeIn" data-wow-delay="0.2s" style="background-image:url(assets/images/banner-image02.jpg);">
            <div class="slideinimage slideinimage1 wow fadeInDown" data-wow-delay="0.4s"><img src="assets/images/banner-image-add1.png" /></div>
            <div class="slideinimage slideinimage2 wow fadeInUp" data-wow-delay="0.6s"><img src="assets/images/banner-image-add2.png" /></div>
            <div class="slideinimage slideinimage3 wow fadeInLeft" data-wow-delay="0.8s"><img src="assets/images/banner-image-add3.png" /></div>
     
        </div>

<input type="hidden" id="testID"  runat="server" />



<%--                        </div>--%>

                    </div>
                   </div>  
                   </div>

                </div>

            </div>
        </div>

    </div>

    <br />
    <div class="row">
        <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
        </div>
        <div class="col-xs-10 col-sm-5 col-md-5 col-lg-5">
            <div class="panel panel-default">
                <a href="/Life/Authorized/Default.aspx">
                    <%--  <img class="img-responsive" src="images/life.png" alt="Life">--%>
                    <img src="images/life.png" alt="Chania" style="width: 100%; height: 100%" />
                </a>
                <div class="panel-body" style="min-height: 100px" align="justify">
                    Sri Lanka Insurance Life offers a range of life insurance plans to suit your protection and investment requirements, income and life style.
                </div>
            </div>
        </div>
        <div class="col-xs-1 visible-xs">
        </div>
        <div class="row visible-xs">
        </div>
        <div class="col-xs-1 visible-xs">
        </div>
        <div class="col-xs-10 col-sm-5 col-md-5 col-lg-5">
            <%-- <div class="panel panel-default">
                        <a href="ClientHome.aspx">
                           <img src="images/Buttons/life_mg.png" alt="Chania" style="width: 100%;height:100%"/>
                           </a>
                      <div class="panel-body" style="min-height:100px" align="justify">
                            Pay and manage your own policy.
                        </div>
                    </div>--%>

            <div class="panel panel-default">
                <a href="/General/Authorized/Default.aspx">
                    <%--  <img class="img-responsive" src="images/general.png" alt="General">--%>
                    <img src="images/general.png" alt="Chania" style="width: 100%; height: 100%" />
                </a>
                <div class="panel-body" style="min-height: 100px" align="justify">
                    General insurance solutions of Sri Lanka Insurance consist of customized solutions to protect yourself, your loved ones and your assets against unexpected incidents.
                </div>
            </div>
        </div>
        <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
        </div>
    </div>

        </div>
  </div>
      <script type="text/javascript" src="js/jquery.base64.min.js"></script> 
</asp:Content>
