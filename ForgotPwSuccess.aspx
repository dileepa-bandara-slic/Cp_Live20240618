<%@ Page Title="" Language="C#" MasterPageFile="~/RegisterSite.Master" AutoEventWireup="true" CodeFile="ForgotPwSuccess.aspx.cs" Inherits="RegisterSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

 <style type="text/css">
        .style3
        {
            color: #009F50;
            font-size: medium;
            font-weight: bold;
        }
        .style4
        {
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <form id="RegisterSuccessForm" runat="server">
  
<div class="main-container" id="main-container">
        <div class="container">
    <div class="row">
        <div class="col-xs-1">
        </div>
        <div class="col-xs-10" style="margin: 10% 0%">
            <div class="panel panel-default">
                <!-- <div class="panel-heading">
                    Password Recovery
                </div>-->
                <div class="panel-body" align="center">
                    <div class="row">
                      <div class="col-xs-12">
                      <p style="height: 12px;" class="style3">Email sent successfully. </p>
            <p class="style3">Please check your email for instructions.</p>
                     </div>
                    </div>
                    

                </div>


            </div>
        </div>
        <div class="col-xs-1">
        </div>
    </div>
    </div>
    </div>
    </form>

</asp:Content>

