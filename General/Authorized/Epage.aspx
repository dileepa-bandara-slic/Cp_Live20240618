<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="Epage.aspx.cs" Inherits="General_Authorized_Epage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
                             <asp:Label ID="lblMesg" runat="server" ForeColor="Red" 
                    style="font-size: medium; color: #000000;"></asp:Label>
                        </div>
                     
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-1">
        </div>
    </div>
</asp:Content>

