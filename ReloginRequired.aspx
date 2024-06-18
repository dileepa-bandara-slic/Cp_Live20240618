<%@ Page Title="" Language="C#" MasterPageFile="~/RegisterSite.Master" AutoEventWireup="true" CodeFile="ReloginRequired.aspx.cs" Inherits="ReloginRequired" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<%--<link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <form id="ReloginRequiredForm" runat="server">
  
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
                      <p style="font-size: large">Your credentials successfully changed! Please <a href="Login.aspx">Relogin</a> to continue.</p>
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

