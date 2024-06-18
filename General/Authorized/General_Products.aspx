<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true"
    CodeFile="General_Products.aspx.cs" Inherits="General_Authorized_General_Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <style>
        /*.btn, .btn-default, .btn:focus, .btn-default:focus, .btn:hover{
            background-color:#202340 !important;
        }*/
            
     .btn-dg,.btn-dg:hover
    { 
        background-color: #CF5D5D!important;
        /*border-color: #CF5D5D !important;*/
        color:White !important;
    }

          .btn-amp
        {
            background-color: #202340 !important;
            /*border-color: #202340 !important;*/
            color :white !important;
        }
        
        .btn-amp:hover, .btn-amp:focus, .btn-amp:active, .btn-amp.active
        {
            background-color: #202340 !important;
            /*border-color: #202340 !important;*/

            color :white !important;
        }
        
             .btn-trv
        {
            background-color: #01aebc !important;
            /*border-color: #01aebc !important;*/
            color :white !important;
        }
        
        .btn-trv:hover, .btn-trv:focus, .btn-trv:active, .btn-trv.active
        {
            background-color: #01aebc !important;
            /*border-color: #01aebc !important;*/
            color :white !important;
        }

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
    <%--    <div class="main-container" id="main-container">
        <div class="container">--%>
            <script type="text/javascript">
                try { ace.settings.check('navbar', 'fixed') } catch (e) { }
            </script>

        <div  style="overflow-x: hidden" class="home-banner">
    <div class="home-banner-text wow fadeInLeft" data-wow-delay="1s">
                    <span>Welcome to the world of</span><br>Sri Lanka Insurance
            </div>
    <div class="home-slider">
            <div class="slider-item slider-item1 wow fadeIn" data-wow-delay="0.2s" style="background-image:url(/assets/images/banner-image02.jpg);">
            <div class="slideinimage slideinimage1 wow fadeInDown" data-wow-delay="0.4s"><img src="/assets/images/banner-image-add1.png" /></div>
            <div class="slideinimage slideinimage2 wow fadeInUp" data-wow-delay="0.6s"><img src="/assets/images/banner-image-add2.png" /></div>
            <div class="slideinimage slideinimage3 wow fadeInLeft" data-wow-delay="0.8s"><img src="/assets/images/banner-image-add3.png" /></div>
     
        </div>
   
    </div>
</div>

            <br/>
            <div class="row">
                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                </div>
                <div class="col-xs-10 col-sm-5 col-md-5 col-lg-5">


                   <div class="panel panel-default">
                   

                        <div class="panel-heading panel-heading-custom1" style="border-radius:0px; background-color:#01aebc; color:#FFFFFF">
                         <b>Travel Protect Insurance</b></div>

                         <div class="panel-body" style="min-height:100px" align="justify"> 
                   

                       <span>  You do not deserve to be disturbed on your overseas journey! Let your business negotiations, excitement 
                             and holiday spirit continues and we will take care of you from any unforeseen circumstances like, flight delay, <span id="demo" class="collapse">  missed departure, loss of Passport, baggage delay/damage, financial emergency etc. Not only that, what about you falling ill, far away from home in an unknown country? We offer cover on hospital bills, emergency medical transportation etc.There is more, we protect your precious home, 
                             when you are away too! What else you need to spend your time away from home, peacefully ? </span></span>
                             
                             <br>

                             <a href="#demo" data-toggle="collapse" style="color:dodgerblue">Read More</a>
                           
                                 <div class="row">
                               
                                <div class="col-xs-6" style="text-align: center">
                                    <asp:HyperLink ID="HyperLink2" runat="server" class="btn btn-trv" NavigateUrl="Products/TRV_Brochure.aspx">View Details</asp:HyperLink>
                                </div>
                              
                                <div class="col-xs-6" style="text-align: center">
                                    <asp:HyperLink ID="HyperLink3" runat="server" class="btn btn-trv" NavigateUrl="Products/TRV_Proposal_New.aspx">Buy Policy</asp:HyperLink>
                                </div>
                             
                            </div>
                        </div>
                    </div>

                   
                   
                </div>
                <div class="col-xs-1 visible-xs">
                </div>
                <div class="row visible-xs">
                </div>
                <div class="col-xs-1 visible-xs">
                </div>

                <div class="col-xs-10  col-sm-5 col-md-5 col-lg-5">
           
                       <div class="panel panel-default" >
                

                        <div class="panel-heading panel-heading-custom1" style="border-radius:0px; background-color:#202340; color:#FFFFFF">
                         <b>Medi Plus Plan</b></div>

                         <div class="panel-body" style="min-height:100px" align="justify">
                        
                                SLI Medi Plus Plan is an annually renewable insurance plan, consisting of four
                                different packages, based on the sum insured. The policy covers a range of medical
                                expenses.<br />&nbsp;
                          

                                 <div class="row">
                             
                                <div class="col-xs-6" style="text-align: center">
                                    <asp:HyperLink ID="HyperLink1" runat="server" class="btn btn-amp" NavigateUrl="Products/AMP_Brochure.aspx" style="background-color:#202340">View Details</asp:HyperLink>
                                </div>
                                
                                <div class="col-xs-6" style="text-align: center">
                                    <asp:HyperLink ID="lnk_amp" runat="server" class="btn btn-amp" NavigateUrl="Products/AMP_Quotation.aspx">Buy Policy</asp:HyperLink>
                                </div>
                             
                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                </div>
            </div>


            	<div class="row">


                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                </div>
                <div class="col-xs-10 col-sm-5 col-md-5 col-lg-5">


                    <div class="panel panel-default">
                        <div class="panel-heading panel-heading-custom1" style="border-radius: 0px; background-color: #01aebc;
                            color: #FFFFFF">
                            <b>Home Protect Lite</b></div>
                        <div class="panel-body" style="min-height: 100px" align="justify">
                            <span>Sri Lanka Insurance presents "Home Protect Lite," a 1st Loss basic home insurance
                                policy designed to meet your specific needs. With Home Protect Lite, you can choose
                                from a range of five specified packages, providing coverage from 1 million to 5
                                million, all at an affordable premium. <span id="exp_hpl" class="collapse">Our trusted
                                    home insurance policy offers you the peace of mind of protecting not only your home
                                    but also your valuable belongings. We understand the importance of safeguarding
                                    your investment, and that is why we have tailored our "Home Protect Lite" policy
                                    to provide comprehensive coverage at a price that suits your budget.</span></span>
                            <a href="#exp_hpl" data-toggle="collapse" style="color: dodgerblue">Read More</a>
                            <div class="row">
                                <div class="col-xs-6" style="text-align: center">
                                    <asp:HyperLink ID="hypLnk_HPL_Product" runat="server" class="btn btn-amp" NavigateUrl="Products/HPL_Quotation.aspx"
                                        Style="background-color: #01aebc">View Details</asp:HyperLink>
                                </div>
                                <div class="col-xs-6" style="text-align: center">
                                    <asp:HyperLink ID="hypLnk_HPL_Product_Purchase" runat="server" class="btn btn-amp"
                                        NavigateUrl="Products/HPL_2023_Purchase.aspx">Buy Policy</asp:HyperLink>
                                </div>
                            </div>
                        </div>
                    </div>


                   
                   
                </div>


                <div class="col-xs-1 visible-xs">
                </div>
                <div class="row visible-xs">
                </div>
                <div class="col-xs-1 visible-xs">
                </div>

                <div class="col-xs-10  col-sm-5 col-md-5 col-lg-5" style="display:none">
                              <div class="panel panel-default">
                   

                        <div class="panel-heading panel-heading-custom1" style="border-radius:0px; background-color:#01aebc; color:#FFFFFF">
                         <b>Motor Thired Party (Description)</b></div>

                         <div class="panel-body" style="min-height:100px" align="justify"> 
                   

                       <span>  A description of something (such as an object, a person, or an event) is a written or spoken account presenting characteristics and aspects of that which is being described in sufficient detail that the audience can form a mental picture, impression, or understanding of it</span></span>
                             
                             <br />

                             <a href="#demo" data-toggle="collapse" style="color:dodgerblue">Read More</a>
                           
                                 <div class="row">
                               
                                <div class="col-xs-6" style="text-align: center">
                                    <asp:HyperLink ID="hypLnk1" runat="server" class="btn btn-trv" NavigateUrl="#">View Details</asp:HyperLink>
                                </div>
                              
                                <div class="col-xs-6" style="text-align: center">
                                  
                                    <asp:HyperLink ID="hypLnk2" runat="server" class="btn btn-trv" NavigateUrl="#">Buy Policy</asp:HyperLink>
                                </div>
                             
                            </div>
                        </div>
                    </div>


                </div>
                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                </div>
            </div>

                   
            
         <%--   </div>--%>
           <%-- <br/> <br/>--%>
      <%--  </div>--%>
   <%-- </div>
    </div>--%>
</asp:Content>
