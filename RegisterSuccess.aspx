<%@ Page Title="" Language="C#" MasterPageFile="~/RegisterSite.Master" AutoEventWireup="true" CodeFile="RegisterSuccess.aspx.cs" Inherits="RegisterSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<%--<link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>--%>
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
        <br />
        <br />
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
                      <p style="height: 12px;" class="style3">Information successfully submitted. </p>
                      <br />
            <p class="style3">Please check your <span class="style4">email</span> for the activation link.</p>
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

