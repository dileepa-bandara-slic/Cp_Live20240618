<%@ Page Title="" Language="C#" MasterPageFile="~/Life.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Life_Authorized_Default" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        /*@media (max-width:479px) {
            .navbar-fixed-top + .main-container {
                padding-top: 40px
            }

            .container {
                max-width: 1024px;
                width: 100%;
                margin: 0 auto;
                height: auto;
                font-size: 12px;
                margin-right: auto;
                margin-left: auto;
            }
        }*/

             @media screen and (max-width: 480px) {
            .slideinimage1 {
                background-image : url("/assets/images/banner-image-add1.png");
                width: 50%;
                height: 50%;
            }
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <%--   <div class="main-container" id="main-container">--%>
        <script type="text/javascript">
            try { ace.settings.check('main-container', 'fixed') } catch (e) { }
        </script>
      <%--   <div class="container">--%>
       <%--     <div class="row">
                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                </div>
                <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10 visible-lg">
                    <img src="images/banner/LifeDefaultPage.jpg" alt="Chania" style="width: 100%;padding-top: 40px" >
                </div>
                  <div class="col-xs-10 col-sm-10 col-md-10 visible-md col-lg-10">
                    <img src="images/banner/LifeDefaultPage.jpg" alt="Chania" style="width: 100%;padding-top: 30px" >
                </div>
                  <div class="col-xs-10 col-sm-10 visible-sm col-md-10 col-lg-10">
                    <img src="images/banner/LifeDefaultPage.jpg" alt="Chania" style="width: 100%;padding-top: 20px" >
                </div>
                <div class="col-xs-10 visible-xs col-sm-10 col-md-10 col-lg-10">
                    <img src="images/banner/LifeDefaultPage.jpg" alt="Chania" style="width: 100%;padding-top: 20px" >
                </div>
                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                </div>
            </div>
             <br />--%>
         <%--   <div class="row">
                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                </div>
                <div class="col-xs-8 col-sm-5 col-md-5 col-lg-5">
                            <img src="images/life_pay.png" alt="Chania" style="width: 100%;height:50%">
                </div>
                <div class="col-xs-3 visible-xs">
                </div>
                <div class="row visible-xs">
             
                </div>
                <div class="col-xs-1 visible-xs">
                </div>
                <div class="col-xs-8 col-sm-5 col-md-5 col-lg-5">
                            <img src="images/life_mg.png" alt="Chania" style="width: 100%;height:50%">
                </div>
                <div class="col-xs-3 col-sm-1 col-md-1 col-lg-1">
                </div>
            </div>--%>
       



      <%--  </div>--%>
<%--    </div>--%>

    
    <div  style="overflow-x: hidden" class="home-banner">
        <div class="home-banner-text wow fadeInLeft" data-wow-delay="1s">
            <span>Welcome to the world of</span><br>Sri Lanka Insurance
        </div>
        <%--<div class="home-slider">
    
                    <div class="row">
           
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    
             
                      
                        <div class="carousel-inner" role="listbox">
                            <div class="item active" >
                                <img src="images/banner/banner-image02_corp.jpg" alt="Chania" style="width: 100%">
                            </div>
               
                        </div>

            
                     </div>

                    </div>
                </div>--%>

             <div class="home-slider">
                   <div class="row">
           
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                            <%--<img src="assets/images/banner-image02.jpg" />--%>
                            <%-- <div class="slider-item slider-item1 wow fadeIn" data-wow-delay="0.2s">
                                 <img src="assets/images/banner-image02.jpg" />--%>

                           <%-- url("paper.gif")--%>
                             <div class="slider-item slider-item1 wow fadeIn" data-wow-delay="0.2s" style="background-image: url(/assets/images/banner-image02.jpg);">
                           <%-- <div class="slider-item slider-item1 wow fadeIn" data-wow-delay="0.2s" style="background-image: url(https://192.168.101.90/assets/images/banner-image02.jpg);">--%>
                                <div class="slideinimage slideinimage1 wow fadeInDown" data-wow-delay="0.4s">
                                    <img src="/assets/images/banner-image-add1.png" />
                                <%--    <img src="https://192.168.101.90/assets/images/banner-image-add1.png" />--%>

                                </div>
                                <div class="slideinimage slideinimage2 wow fadeInUp" data-wow-delay="0.6s">
                                    <img src="/assets/images/banner-image-add2.png" />
                                  <%--  <img src="https://192.168.101.90/assets/images/banner-image-add2.png" />--%>

                                </div>
                                <div class="slideinimage slideinimage3 wow fadeInLeft" data-wow-delay="0.8s">
                                    <img src="/assets/images/banner-image-add3.png" />
                                   <%-- <img src="https://192.168.101.90/assets/images/banner-image-add3.png" />--%>

                                </div>

                            </div>

                        </div>
                       </div>
                 </div>
        
     </div>

   <%-- <div class="row">

    </div>--%>
    <br/>

         <div class="row">
                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                </div>
                <div class="col-xs-10 col-sm-5 col-md-5 col-lg-5">
                    <div class="panel panel-default">
                        <a href="MakePayment.aspx">

                             <img src="images/Buttons/life_pay.png" alt="Chania" style="width: 100%;height:100%"/>

                            </a>
                        <div class="panel-body" style="min-height:100px" align="justify">
                            Pay other registered policy holders’ premium.
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
                    <div class="panel panel-default">
                        <a href="ClientHome.aspx">
                           <img src="images/Buttons/life_mg.png" alt="Chania" style="width: 100%;height:100%"/>
                           </a>
                      <div class="panel-body" style="min-height:100px" align="justify">
                            Pay and manage your own policy.
                        </div>
                    </div>
                </div>
                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                </div>
            </div>

<%--      <div class="row">
                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                </div>
                <div class="col-xs-10 col-sm-5 col-md-5 col-lg-5">
                    <div class="panel panel-default">
                        <a href="General_Products.aspx">
                            <img src="images/Buttons/gen_pur.png" alt="Chania" style="width: 100%"></a>
                         <div class="panel-body" style="min-height:100px" align="justify">
                            Click here to obtain quotations and purchase General Insurance products online.
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
                    <div class="panel panel-default">
                        <a href="ClientHome.aspx">
                           <img src="images/Buttons/gen_mg.png" alt="Chania" style="width: 100%"></a>
                      <div class="panel-body" style="min-height:100px" align="justify">
                            Click here for policy renewals, request services and view/ manage current policies.
                        </div>
                    </div>
                </div>
                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                </div>
            </div>--%>



</asp:Content>
