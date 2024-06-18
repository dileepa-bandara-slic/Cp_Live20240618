<%@ Page Title="" Language="C#" MasterPageFile="~/RegisterSite.Master" AutoEventWireup="true"
    CodeFile="ConfirmRegister.aspx.cs" Inherits="ConfirmRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="ConfirmRegisterForm" runat="server">
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
                            <asp:Label ID="lblErrMesg" runat="server" ForeColor="Red" Font-Size="Large"></asp:Label>
                            &nbsp;<asp:HyperLink ID="hlRegister" runat="server">[hlRegister]</asp:HyperLink>
                        </div>
                     
                    </div>
                    <div class="row">
                        
                        <div class="col-xs-12">
                            <asp:Label ID="lblSuccessMesg" runat="server" Font-Size="Large" ForeColor="Green"></asp:Label>
                        </div>
                    
                            <asp:HyperLink ID="hlLogin" runat="server"></asp:HyperLink>
                    
                    </div>
                    <div class="row">
                   
                        <div class="col-xs-12">
                            <asp:Label ID="lblPostErrMesg" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    
                    </div>

                </div>
            </div>
        </div>
        <div class="col-xs-1">
        </div>
    </div>
    </form>
</asp:Content>
