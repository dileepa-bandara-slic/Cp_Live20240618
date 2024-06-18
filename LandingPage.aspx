<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LandingPage.aspx.cs" Inherits="index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        body {
            background: #ffffff
        }

        .container {
            width: 100%;
            padding-right: 15px;
            padding-left: 15px;
            margin-right: auto;
            margin-left: auto;
        }

        @media (max-width: 499px) {
            .container {
                max-width: 100vw;
                padding-right: 0;
                padding-left: 0;
                margin-top: -.1vw;
            }

            #divTitile {
                min-width: 100vw;
                min-height: 50vw;
                line-height: 4.5vw;
                height: 100vh;
            }

            .describTable {
                min-width: 100vw;
                font-size: 4vw;
            }

            .mrgn {
                margin-top: -.5vw;
            }

            #bannerID {
                max-width: 20vw
            }

            #h1 {
                font-size: 5vw
            }

            .heightFull {
                min-height: 95vh;
            }

            .imgStore {
                max-height: 2vw;
            }

            .footer_img {
                left: 15vw;
                bottom: .5vw;
                bottom: 10vw;
                height: 10vw
            }

            .footer_Txt {
                font-size: 1.7vw;
                left: -10vw;
                display: none;
            }

            h2 {
                font-size: 4.8vw
            }

            .parallax {
                display: none;
            }

            .parallax2 {
                display: normal;
            }
            /*.content2{
                padding: 100px 0 0 0;
            }*/
        }

        @media (max-width: 500px) {
            .container {
                max-width: 100vw;
                padding-right: 0;
                padding-left: 0;
                margin-top: -.02vw;
            }

            #divTitile {
                min-width: 100vw;
                min-height: 50vw;
                line-height: 4.5vw;
            }

            .describTable {
                min-width: 100vw;
                font-size: 2.8vw;
            }

            .mrgn {
                margin-top: -.5vw;
            }

            #bannerID {
                max-width: 25vw
            }

            #h1 {
                font-size: 4vw
            }

            .heightFull {
                min-height: 92vh;
            }

            .imgStore {
                max-height: 2vw;
            }

            .footer_img {
                left: 25vw;
                bottom: 5vw;
            }

            .footer_Txt {
                font-size: 1.7vw;
                left: -10vw;
                display: none;
            }

            h2 {
                font-size: 4.5vw
            }

            .parallax {
                display: none;
            }

            .parallax2 {
                display: normal;
            }
            /*.content2{
                padding: 100px 0 0 0;
            }*/
        }

        @media (max-width: 768px) {
            .container {
                max-width: 100vw;
                padding-right: 0;
                padding-left: 0;
                margin-top: -.02vw;
            }

            #divTitile {
                min-width: 90vw;
                min-height: 50vw;
                line-height: 6vw;
                height: 100vh;
            }

            .describTable {
                min-width: 95vw;
                font-size: 3.8vw;
                padding: 0vw
            }

            .mrgn {
                margin-top: -.9vw;
            }

            #bannerID {
                max-width: 20vw
            }

            #h1 {
                font-size: 4vw;
                line-height: 4.3vw
            }

            .heightFull {
                min-height: 90vh;
            }

            .imgStore {
                max-height: 15vw;
            }

            .footer_img {
                left: 25vw;
                bottom: 5.2vw;
                height: 0vw
            }

            .footer_Txt {
                font-size: 2vw;
                left: -10vw;
                display: initial;
            }

            h2 {
                font-size: 3.5vw
            }

            .parallax {
                display: none;
            }

            .parallax2 {
                display: normal;
            }
            /*.content2{
                padding: 0;
            }*/
        }

        @media (max-width: 992px) {
            .container {
                max-width: 100vw;
                padding-right: 0;
                padding-left: 0;
                margin-top: -.1vw;
            }

            #divTitile {
                min-width: 90vw;
                min-height: 50vw;
                line-height: 2vw;
            }

            .describTable {
                min-width: 90vw;
                font-size: 2vw;
            }

            .mrgn {
                margin-top: -.2vw;
            }

            #bannerID {
                max-width: 18.8vw
            }

            #h1 {
                font-size: 3.5vw
            }

            .heightFull {
                min-height: 88vh;
            }

            .imgStore {
                max-height: 15vw;
            }

            .footer_img {
                left: 30vw;
                bottom: .2vw;
                height: 0vw
            }

            .footer_Txt {
                font-size: 1.5vw;
                bottom: 20vw;
                display: initial;
            }

            h2 {
                font-size: 3vw
            }


            .parallax {
                display: none;
            }

            .parallax2 {
                display: normal;
            }
            /*.content2{
                padding: 5vw 0 0 0;
            }*/
            p, li {
                font-size: 4vw;
            }
        }


        @media (max-width: 1200px) {
            .container {
                max-width: 100vw;
                padding-right: 0;
                padding-left: 0;
                margin-top: -.1vw;
            }

            #divTitile {
                min-width: 50vw;
                min-height: 60vw;
                line-height: 2vw;
            }

            .heightFull {
                min-height: 92vh;
            }

            .describTable {
                min-width: 90vw;
                font-size: 2vw;
            }

            .mrgn {
                margin-top: -.2vw;
            }

            #bannerID {
                max-width: 18.8vw
            }

            #h1 {
                font-size: 3.2vw
            }

            h2 {
                font-size: 3vw
            }

            .imgStore {
                max-height: 7vw;
            }

            .footer_img {
                left: 30vw;
                bottom: 0vw;
                height: 5vw
            }

            .footer_Txt {
                font-size: 1.5vw;
                bottom: 20vw;
                display: initial;
            }

            .parallax {
                display: normal;
            }

            .parallax2 {
                display: normal;
            }
            /*.content2{
                padding: 5vw 0 0 0;
            }*/
            p, li {
                font-size: 2vw;
            }
        }

        @media (max-width: 1500px) {
            .container {
                max-width: 100vw;
                padding-right: 0;
                padding-left: 0;
                margin-top: -.1vw;
            }

            #divTitile {
                min-width: 50vw;
                max-height: 40vw;
                line-height: 2vw;
            }

            .heightFull {
                min-height: 92vh;
            }

            .describTable {
                min-width: 90vw;
                font-size: 2vw;
            }

            .mrgn {
                margin-top: -.2vw;
            }

            #bannerID {
                max-width: 18.8vw
            }

            #h1 {
                font-size: 3.2vw
            }

            h2 {
                font-size: 3vw
            }

            .imgStore {
                max-height: 10vw;
            }

            .footer_img {
                left: 30vw;
                bottom: 0vw;
                height: 5vw
            }

            .footer_Txt {
                font-size: 1.5vw;
                bottom: 20vw;
                display: initial;
            }

            .parallax {
                display: normal;
            }

            .parallax2 {
                display: normal;
            }


            p, li {
                font-size: 2vw;
            }
        }

        .container-fluid {
            width: 100%;
            padding-right: 15px;
            padding-left: 15px;
            margin-right: auto;
            margin-left: auto;
        }

        td {
            font-family: arial;
            color: #000;
            padding: 5px
        }

        #grad1 {
            background-color: #ffffff; /* For browsers that do not support gradients */
            background-image: linear-gradient(#00adbb, #ffffff, #ffffff, #ffffff); /* Standard syntax (must be last) */
        }

        /*.imgStore {
            max-height: 5vw;
        }*/

        /*.footer_img {
            left: 30vw;
            bottom: .2vw;
        }*/

        #footer {
            position: fixed;
            width: 100%;
        }

        #footerTxt {
            position: fixed;
            width: 100%;
            bottom: 0;
        }

        #divTitile {
            position: fixed;
            top: 0;
            z-index: 1;
            width: 100%;
        }

        }
    </style>

    <script>
        function myFunction() {
            var x = "Total Width: " + screen.width + "px";
            document.getElementById("demo").innerHTML = x;
        }

    </script>

    <script>
        $(document).ready(function () {
            var scroll_pos = 0;
            $("#left-panel").scroll(function () {
                scroll_pos = $(this).scrollTop();
                if (scroll_pos > 0) {
                    $("#left-panel").css('background-color', '#1A1A1A');
                } else {
                    $("#left-panel").css('background-color', 'red');
                }
                console.log(scroll_pos);
            });
        });
    </script>
</head>
<body class="container" onload="myFunction()">


    <form id="form1" runat="server">


        <style>
            body {
                margin: 0;
                font-size: 28px;
                font-family: Arial, Helvetica, sans-serif;
            }

            .header {
                position: fixed;
                top: 0;
                z-index: 1;
                width: 100%;
                background-color: #f1f1f1;
            }

                .header h2 {
                    text-align: center;
                }

            .progress-container {
                width: 100%;
                height: 8px;
                background: #ccc;
                margin-top: 3vw
            }

            .progress-bar {
                height: 8px;
                background-image: linear-gradient(to bottom right, #a3bcb2, #36bae8);
                width: 0%;
            }

            .content {
                margin: 50px auto 0 auto;
                margin-top: -100px;
                width: 80%;
            }

            .parallax2 {
                /* The image used */
                background-image: url("./images/slic_icon.jpg") !important;
                /* Set a specific height */
                height: 450px !important;
                /* Create the parallax scrolling effect */
                background-attachment: fixed;
                background-position: center center;
                background-repeat: no-repeat;
                background-size: auto;
                -moz-background-size: auto;
                -webkit-background-size: auto;
                background-position: top;
                /*background-size: auto;*/
                display: normal;
            }

            .parallax {
                /* The image used */
                background-image: url("./images/iphone-jpg.jpg") !important;
                /* Set a specific height */
                height: 350px !important;
                /* Create the parallax scrolling effect */
                background-attachment: fixed;
                background-position: center center;
                background-repeat: no-repeat;
                background-size: auto;
                -moz-background-size: auto;
                -webkit-background-size: auto;
                /*background-attachment: scroll;*/
                background-position: top;
                /*background-size: auto;*/
                display: normal;
            }

            .content3 {
                padding: 10vw 0;
            }

            .content2 {
                padding: 5vw 0;
            }

            p {
                text-align: justify
            }

            /*div.sticky {
                position: -webkit-sticky;
                position: sticky;
                top: 100px;
                background-color: yellow;
                padding: 15px;
                font-size: 20px;
            }*/
            /*#login
            {
                background: #000;
            }*/
            li {
                padding: 5px;
            }
        </style>


        <div class="header" style="color: #ffffff; background-image: linear-gradient(to bottom right, #ffffff,#00adbb, #00adbb);">
            <div style="float: left;">
                <img id="bannerID" src="./images/SLICLOGO.png" />
            </div>
            <div style="text-align: left; padding-left: 30vw">
                <h1 style="font-family: arial; text-transform: uppercase" id="h1">SLIC Customer Mobile App
                    <span id="height" style="display: none">0</span>
                </h1>
            </div>

            <div class="progress-container">
                <div class="progress-bar" id="myBar"></div>
            </div>
        </div>



        <div class="parallax2"></div>


        <div class="content content3">
            <%--  <h2 style="background: #00adbb; text-align: center; font-family: arial; color: #fff; padding: 5px; text-transform: uppercase">Now You can Download from</h2>--%>

            <div class="footer_img" style="text-align: center">

                <span style="padding: 5px 0px 5px 0px">
                    <button style="background: none; border: none" id="btnPlaystore" runat="server" onserverclick="btnPlaystore_ServerClick">
                        <img src="./images/playStore.png" class="imgStore" />
                    </button>

                </span>
                <span style="padding: 5px 0px 5px 0px">
                    <button style="background: none; border: none" id="btnAppstore" runat="server" onserverclick="btnAppstore_ServerClick">
                        <img src="./images/appStore.png" class="imgStore" />
                    </button>
                </span>


            </div>
        </div>
        <div class="parallax"></div>

        <div class="content content2">
            <h2 style="background: #00adbb; text-align: center; font-family: arial; color: #fff; padding: 5px; text-transform: uppercase">what's in it for you</h2>

            <p>
                Keep track of your insurance policies on the go.<b> SLIC mobile app </b>provides an interface for <b>Sri Lanka Insurance customers </b>to manage their Life / General Insurance policies with ease.
            </p>
            <p>Making premium payments and viewing details of claims made, payments, insurance covers and earned bonus are brought to your fingertips.</p>

            <p>
                Also, you can be informed of the discount schemes and notifications from your trusted insurer and enjoy the wide array of value-added services.
                <p>
                    <p>Select the best insurance product which suits your needs.</p>
<p>Find the garages and SLIC branch locations nearest to you with no hassle.</p>
        </div>

        <div class="content content2">
            <h2 style="background: #00adbb; text-align: center; font-family: arial; color: #fff; padding: 5px; text-transform: uppercase">how to get started</h2>


            <div id="sticky" class="sticky" style="padding: 10px 0">
                <p style="text-align: justify">
                    <b>Already have an account in the customer portal?</b>
                </p>

            </div>

            <p>You can just sign in to SLIC App using the same login credentials used to login to <b>SLIC Customer portal</b>.</p>

            <div id="sticky2" class="sticky2" style="padding: 10px 0">
                <p style="text-align: justify">
                    <b>New User?</b>
                </p>

            </div>

           <%-- <p style="text-align: justify">--%>
                You can register for an account using the App. 
                <ol>
                    <li>Click on the ‘Join Now ’ link at the bottom of the App Login page
                    </li>
                    <li>Enter the required information and submit.
                    </li>
                    <li>An account activation link will be sent to your registered email
                    </li>
                    <li>Click on the link to activate your account
                    </li>
                    <li>Once your account is activated, you can login to your account from the SLIC App.
                    </li>

                </ol>
           <%-- </p>--%>

        </div>

        <script>
            // When the user scrolls the page, execute myFunction 
            window.onscroll = function () { myFunction() };

            function myFunction() {
                var winScrollHeight = document.body.scrollHeight || document.documentElement.scrollHeight;
                var winScroll = document.body.scrollTop || document.documentElement.scrollTop;
                var height = document.documentElement.scrollHeight - document.documentElement.clientHeight;
                var width = document.documentElement.scrollHeight - document.documentElement.clientWidth;
                var scrolled = (winScroll / height) * 100;
                document.getElementById("myBar").style.width = scrolled + "%";

                var result;
                var topv = scrolled;
                var bot = 5;
                result = topv/bot;
                var scrolledVal = Math.floor(result) * bot;

                var x = document.getElementById("height").innerText = scrolledVal;
                if (height > width) {

                    if (winScrollHeight > 2120) {
                        //mac book
                        if (winScrollHeight == 2501) {
                            if (x > 95) {
                                sticky.style.cssText = "width: 100%;position: -webkit-sticky;position: sticky;top: 98px;text-align:center;color: #fff;text-transform: uppercase;background: #00adbb;-webkit-transition: color: .5s linear;background .5s linear;-moz-transition: color: .5s linear;background .5s linear;-ms-transition: color: .5s linear;background .5s linear;-o-transition: color: .5s linear;background .5s linear;transition: color: .5s linear;background .5s linear"

                            }
                            else {
                                sticky.style.cssText = "position: sticky;font-weight:bold;background: #fff;-webkit-transition: background .5s linear;-webkit-transition: color: .5s linear;background .5s linear;-moz-transition: color: .5s linear;background .5s linear;-ms-transition: color: .5s linear;background .5s linear;-o-transition: color: .5s linear;background .5s linear;transition: color: .5s linear;background .5s linear"

                            }
                            //if (x > 98) {

                            //    sticky2.style.cssText = "padding: 10px;position: -webkit-sticky;position: sticky;top: 98px;text-align:center;color: #fff;text-transform: uppercase;background: #00adbb;-webkit-transition: color: .5s linear;background .5s linear;-moz-transition: color: .5s linear;background .5s linear;-ms-transition: color: .5s linear;background .5s linear;-o-transition: color: .5s linear;background .5s linear;transition: color: .5s linear;background .5s linear"
                            //}
                            //else {

                            //    sticky2.style.cssText = "font-weight:bold;background: #fff;-webkit-transition: background .5s linear;-webkit-transition: color: .5s linear;background .5s linear;-moz-transition: color: .5s linear;background .5s linear;-ms-transition: color: .5s linear;background .5s linear;-o-transition: color: .5s linear;background .5s linear;transition: color: .5s linear;background .5s linear"
                            //}
                        }
                        else {
                            if (x > 85) {
                               // sticky.style.cssText = "padding-left: 10%;position: -webkit-sticky;position: sticky;top: 127px;text-align:center;color: #fff;text-transform: uppercase;background: #00adbb;-webkit-transition: padding: .5s linear;color: .5s linear;background .5s linear;-moz-transition: padding: .5s linear;color: .5s linear;background .5s linear;-ms-transition: padding: .5s linear;color: .5s linear;background .5s linear;-o-transition: padding: .5s linear;color: .5s linear;background .5s linear;transition: padding: .5s linear;color: .5s linear;background .5s linear"

                            }
                            else {
                                sticky.style.cssText = "position: sticky;font-weight:bold;background: #fff;-webkit-transition: background .5s linear;-webkit-transition: padding: .5s linear;color: .5s linear;background .5s linear;-moz-transition: padding: .5s linear;color: .5s linear;background .5s linear;-ms-transition: padding: .5s linear;color: .5s linear;background .5s linear;-o-transition: padding: .5s linear;color: .5s linear;background .5s linear;transition: padding: .5s linear;color: .5s linear;background .5s linear"

                            }
                            if (x > 75) {

                                 sticky2.style.cssText = "padding-left: 10%;position: -webkit-sticky;position: sticky;top: 127px;text-align:center;color: #fff;text-transform: uppercase;background: #00adbb;-webkit-transition: padding: .5s linear;color: .5s linear;background .5s linear;-moz-transition: padding: .5s linear;color: .5s linear;background .5s linear;-ms-transition: padding: .5s linear;color: .5s linear;background .5s linear;-o-transition: padding: .5s linear;color: .5s linear;background .5s linear;transition: padding: .5s linear;color: .5s linear;background .5s linear"
                            }
                            else {

                                sticky2.style.cssText = "position: sticky;font-weight:bold;background: #fff;-webkit-transition: padding: .5s linear;color: .5s linear;background .5s linear;-moz-transition: padding: .5s linear;color: .5s linear;background .5s linear;-ms-transition: padding: .5s linear;color: .5s linear;background .5s linear;-o-transition: padding: .5s linear;color: .5s linear;background .5s linear;transition: padding: .5s linear;color: .5s linear;background .5s linear"
                            }
                        }

                    }

                }

                else {
                    if (winScrollHeight > 2100) {
                        if (x > 85) {
                            sticky.style.cssText = "padding-left: 10%;position: -webkit-sticky;position: sticky;top: 127px;text-align:center;color: #fff;text-transform: uppercase;background: #00adbb;-webkit-transition: padding: .5s linear;color: .5s linear;background .5s linear;-moz-transition: padding: .5s linear;color: .5s linear;background .5s linear;-ms-transition: padding: .5s linear;color: .5s linear;background .5s linear;-o-transition: padding: .5s linear;color: .5s linear;background .5s linear;transition: padding: .5s linear;color: .5s linear;background .5s linear"

                        }
                        else {
                            sticky.style.cssText = "position: sticky;font-weight:bold;background: #fff;-webkit-transition: padding: .5s linear;color: .5s linear;background .5s linear;-moz-transition: padding: .5s linear;color: .5s linear;background .5s linear;-ms-transition: padding: .5s linear;color: .5s linear;background .5s linear;-o-transition: padding: .5s linear;color: .5s linear;background .5s linear;transition: padding: .5s linear;color: .5s linear;background .5s linear"

                        }
                        if (x > 95) {

                            sticky2.style.cssText = "padding-left: 10%;position: -webkit-sticky;position: sticky;top: 127px;text-align:center;color: #fff;text-transform: uppercase;background: #00adbb;-webkit-transition: padding: .5s linear;color: .5s linear;background .5s linear;-moz-transition: padding: .5s linear;color: .5s linear;background .5s linear;-ms-transition: padding: .5s linear;color: .5s linear;background .5s linear;-o-transition: padding: .5s linear;color: .5s linear;background .5s linear;transition: padding: .5s linear;color: .5s linear;background .5s linear"
                        }
                        else {

                            sticky2.style.cssText = "position: sticky;font-weight:bold;background: #fff;-webkit-transition: padding: .5s linear;color: .5s linear;background .5s linear;-moz-transition: padding: .5s linear;color: .5s linear;background .5s linear;-ms-transition: padding: .5s linear;color: .5s linear;background .5s linear;-o-transition: padding: .5s linear;color: .5s linear;background .5s linear;transition: padding: .5s linear;color: .5s linear;background .5s linear"
                        }
                    }
                }



            }

        </script>
    </form>
</body>
</html>
