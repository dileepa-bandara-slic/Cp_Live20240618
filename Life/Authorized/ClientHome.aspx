<%@ Page Title="" Language="C#" MasterPageFile="~/Life.master" AutoEventWireup="true"
    CodeFile="ClientHome.aspx.cs" Inherits="Life_Authorized_ClientHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/fieldset.css" rel="stylesheet" />

    <style>
@media (max-width:479px)
{
 .navbar-fixed-top+.main-container
 {
     padding-top:40px
  }
}
</style>

        <style>

        @media screen and (max-width:1800px)
        {
           /*input[type=text]{width:50%}
           .select {
                width:50%;
           }*/
            input[type="text"], select{
                width:60%;
            }

            .form-control{
                width:60%;
            }

            span{
                width:60%;
            }
              element{
                width:60% !important;
            }

            legend {
                font-size: 100%;
                font-weight:bold;
                /*font-family: Tahoma;*/
            }

            fieldset{
                width:75%;
                margin: auto;
            }
            /*.alert
            {
                 width:75%;
                margin: auto;
            }*/
            /*select {
                 width:50%;
             }*/

        }

                @media screen and (max-width:600px)
        {
           /*input[type=text]{width:100%}
             .select {
                width:100%;
           }*/
             input[type=text] {
                width:100%;
            }
             select {
                 width:100%;
             }
              .form-control{
                width:100%;
            }

              span{
                width:100%;
            }
          
              element{
                width:100% !important;
            }

              legend {
                font-size: 90%;
                font-weight:bold
                /*font-family: Tahoma;*/
            }
              fieldset{
                width:100%;
            }
               /*.alert
            {
                 width:100%;
                margin: auto;
            }*/
        }
    </style>

<script type="text/javascript" language="javascript">
    function Confirm_to_delete(item) {

        if (confirm("Are you sure to delete policy : " + item + "?") == true) {
            return true;
        }
        else {
            return false;
        }

    }

    $(document).ready(function () {
        $('.alert').show()
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            $('.alert').show()
        }
    });
   
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container" id="main-container" style="min-height:600px">

        <link href="/css/footable.min.css"
            rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="/js/footable.min.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=GridLifePols]').footable();
            });
        </script>
        <%--<div class="main-content">--%>
        <div class="container">
       <%-- </br>--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/Life/Authorized/">Life</a></li>
                        <li class="breadcrumb-item active">Manage My Policies</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
       
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <form class="form-horizontal">
                           <div style="align:center">
                                     <fieldset>
  <legend>Add Policies:</legend>
                    <%--<div class="panel panel-default">--%>
                  <%--      <div class="panel-heading">
                        <center>
                           <h3>Add Policies 
                           </center>

                        </div>--%>
                       <%-- <div class="panel-body">--%>
                            <div class="row">
                                <div class="col-sm-3"><asp:Literal ID="litLabel" runat="server"></asp:Literal> </div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtPolNum" runat="server" placeholder="Enter policy number" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="polNumRequired" runat="server" ControlToValidate="txtPolNum"
                                        ErrorMessage="Policy number is required" ToolTip="Policy number is required"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" class="btn btn-primary btn-xs pull-left"
                                        Text="Add Policy"  />
                                </div>
                                <div class="col-sm-3">
                                </div>
                            </div>
                            <br/>
                            <div class="row">
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-9">
                                    <asp:CustomValidator ID="PolNumValidator" runat="server" CssClass="errorMsg_1" ControlToValidate="txtPolNum"
                                        ForeColor="Red" OnServerValidate="checkPolNum"></asp:CustomValidator>
                                </div>
                            </div>
                        <%--</div>--%>
                   <%-- </div>--%>
                                         </fieldset>
                               </div>
                    </form>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <!--</div>-->
            <!--<div class="col-lg-2 col-md-3 col-sm-2 col-xs-1">
                </div>-->
            <!--</div>-->
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <div class="alert alert-warning" id="p_messages">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <asp:Label ID="lblMesg" runat="server" Font-Bold="false" Style="font-size: 100%"></asp:Label>
                    </div>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <div class="col-sm-3" id="lblCurrPols" runat="server"
                        visible="false">
                        <b>Your Current Policies</b></div>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                        <div class="col-sm-3" id="lblLifePols" runat="server"
                            visible="false">
                            Life policies</div>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                   
                        <asp:GridView ID="GridLifePols" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridLifePols_RowDeleting"
                            CssClass="footable" OnRowDeleted="GridLifePols_RowDeleted"
                            Visible="true">
                            <Columns>
                                <asp:TemplateField HeaderText="Name" HeaderStyle-ForeColor="#000000">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Policy Number" HeaderStyle-ForeColor="#000000">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPolNum" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Policy Type" HeaderStyle-ForeColor="#000000">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPolTyp" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Term" HeaderStyle-ForeColor="#000000">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTerm" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start Date" HeaderStyle-ForeColor="#000000">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStrtDate" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-ForeColor="#000000">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" ItemStyle-Font-Bold="true"
                                    HeaderStyle-ForeColor="#000000" DeleteText="Remove From Profile">
                                   <%-- <ItemStyle Font-Bold="True"></ItemStyle>--%>
                                   <ControlStyle Font-Bold="true" />
                                </asp:CommandField>
                                <%--<asp:HyperLinkField Text="Renew" />--%>
                                <asp:TemplateField HeaderStyle-ForeColor="#000000">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server"><b>Pay</b></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-ForeColor="#000000">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink2" runat="server">
                            <b>View Details</b></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
     
                <div class="col-xs-1">
                </div>
            </div>

              <div class="row">
            </div>

              <div class="row">
            </div>
            <br />
            <br />
            <br />
        </div>
    </div>

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            //alert("in");
            $("#p_messages").hide();
            $("#p_messages").fadeIn(500);
        });
    </script>
</asp:Content>
