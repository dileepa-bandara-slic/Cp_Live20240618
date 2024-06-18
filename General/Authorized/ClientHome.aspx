<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true"
    CodeFile="ClientHome.aspx.cs" Inherits="General_Authorized_ClientHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/assets/css/fieldset.css" rel="stylesheet" />
    <script src="/js/jquery-3.5.1.min.js"></script>
    <script type="text/javascript" src="/js/cbpHorizontalMenu.min.js"></script>

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
        
        }
    </style>

    <style>

        @media screen and (max-width:600px)
        {
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
            
        }
        .auto-style1 {
            position: relative;
            width: 25%;
            min-height: 1px;
            -webkit-box-flex: 0;
            -ms-flex: 0 0 25%;
            flex: 0 0 25%;
            max-width: 25%;
            float: left;
            left: 0px;
            top: 0px;
            padding-left: 12px;
            padding-right: 12px;
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

            <script>

    function getResolution() {

        alert("Your screen resolution is: " + screen.width + "x" + screen.height);

    }

    </script>

  

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container" id="main-container" style="min-height:600px">
     
        <link href="/css/footable.min.css" rel="stylesheet" type="text/css" />

   <script type="text/javascript" src="/js/footable.min.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=GridPendPols]').footable();
                $('[id*=GridMotPols]').footable();
                $('[id*=GridGenPols]').footable();
            });
        </script>
        <!--<div class="main-content">-->
        <div class="container">
         <%--   </br>--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                        <li class="breadcrumb-item active">Manage Policies</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <%--<br />--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">

                    <div style="align:center">
                     <fieldset>
  <legend>Add Policies:</legend>
                 
                  <%--  <div class="panel" style="border-bottom-color: #ddd; border-left-color:#ddd; border-right-color:#ddd; border-top-color:#ddd;">--%>
                

                       <%-- <div class="panel-heading panel-heading-custom2" style="color:#000000; background-color: #f5f5f5; font-size:medium; border-color: #ddd;">
                            <center>
                              <strong> <h4> Add Policies </h4>  </strong>
                            </center>
                        </div>--%>
                  <%--  <div class="panel-body">--%>
                        <div class="row">

                            <div class="auto-style1">
                                <asp:Literal ID="litLabel" runat="server"></asp:Literal></div>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtPolNum" runat="server" placeholder="Enter policy number" class="form-control"
                                    MaxLength="18"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="polNumRequired" runat="server" ControlToValidate="txtPolNum"
                                    ErrorMessage="Policy number is required" ToolTip="Policy number is required"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                Policy Type
                            </div>
                            <div class="col-sm-9">
                                <asp:DropDownList ID="ddlPolType" runat="server" class="form-control">
                                    <asp:ListItem Value="">Select</asp:ListItem>
                                    <asp:ListItem Value="M">Motor (Comprehensive)</asp:ListItem>
                                    <asp:ListItem Value="G">Non-Motor</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="polTypeRequired" runat="server" ControlToValidate="ddlPolType"
                                    ErrorMessage="Policy Type should be selected" ToolTip="Policy Type should be selected"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <%--</br>--%>
                        <%--   <div class="form-group">
                                <label class="col-xs-3 control-label" for="submit">
                                </label>
                                <div class="col-xs-6">
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" class="btn btn-primary"
                                        Text="Add Policy" />
                                           <asp:CustomValidator ID="PolNumValidator" runat="server" CssClass="errorMsg_1 alert alert-danger"
                                    ControlToValidate="txtPolNum" OnServerValidate="checkPolNum" Width="100%" Font-Bold="True"></asp:CustomValidator>
                                </div>
                                <div class="col-xs-3"></div>
                            </div>--%>
                        <div class="row">
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-9">
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Class="btn btn-primary btn-xs pull-left"
                                    Style="min-width: 120px;" Text="Add Policy"  />
                                <%--        <asp:CustomValidator ID="PolNumValidator" runat="server" CssClass="errorMsg_1 alert alert-danger"
                                        ControlToValidate="txtPolNum" OnServerValidate="checkPolNum" Font-Bold="false"></asp:CustomValidator>
                                --%></div>
                            <%-- <div class="col-sm-3">
                                </div>--%>
                        </div>
                       <%-- <br />--%>
                        <div class="row">
                              <div class="col-sm-3">
                            </div>
                            <div class="col-sm-9">
                                <asp:CustomValidator ID="PolNumValidator" runat="server" CssClass="errorMsg_1 alert alert-danger"
                                    ControlToValidate="txtPolNum" OnServerValidate="checkPolNum" Width="100%" Font-Bold="false"></asp:CustomValidator>
                            </div>
                               <div class="col-sm-3">
                            </div>
                            
                        </div>
                 <%--   </div>--%>
                         </fieldset>
                        </div>
                 <%--   </div>--%>


                   <%-- </form>--%>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <div class="alert alert-warning" id="p_messages" >
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
                    <label for="inputPolicyNo" class="col-sm-3 control-label" id="lblCurrPols" runat="server"
                        visible="false">
                        <b>Your Current Policies</b></label>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <label for="inputPolicyNo" class="col-sm-3 control-label" id="lblProps" runat="server"
                        visible="false">
                        Pending policies</label>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <asp:GridView ID="GridPendPols" runat="server" AutoGenerateColumns="False" 
                        CssClass="footable" onrowdatabound="GridPendPols_RowDataBound">
                        <Columns>
                            <asp:HyperLinkField HeaderText="Ref No." HeaderStyle-ForeColor="Black" DataNavigateUrlFields="REF_NO" 
                                  DataNavigateUrlFormatString="Quotation_Reprint.aspx?refNo={0}" DataTextField="REF_NO" 
                                SortExpression="REF_NO">
                                <HeaderStyle ForeColor="Black" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="PTSNA" HeaderText="Policy Type" HeaderStyle-ForeColor="#FFFFFF"
                                SortExpression="PTSNA">
                                <HeaderStyle ForeColor="Black" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PLAN" HeaderText="Plan" SortExpression="PLAN" HeaderStyle-ForeColor="#FFFFFF">
                                <HeaderStyle ForeColor="Black" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SUM_ASSURD" HeaderText="Sum assured" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-ForeColor="#FFFFFF" SortExpression="SUM_ASSURD">
                                <HeaderStyle ForeColor="Black" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COM_DATE" HeaderText="Start Date" HeaderStyle-ForeColor="#FFFFFF"
                                SortExpression="COM_DATE">
                                <HeaderStyle ForeColor="Black" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ANU_PREMIUM" HeaderText="Premium Paid (Rs.)" HeaderStyle-ForeColor="#FFFFFF"
                                SortExpression="ANU_PREMIUM">
                                <HeaderStyle ForeColor="Black" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
       <%--     <br />--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <label for="inputPolicyNo" class="col-sm-3 control-label" id="lblMotPols" runat="server"
                        visible="false">
                        Motor policies</label>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <asp:GridView ID="GridMotPols" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridMotPols_RowDeleting"
                        CssClass="footable" OnRowDeleted="GridMotPols_RowDeleted">
                        <Columns>
                            <asp:TemplateField HeaderText="Name" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Policy Number" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblPolNum" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Policy Type" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblPolTyp" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="Sum Assured" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblSumAssurd" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblStrtDate" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vehicle No." HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblVehiNo" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="Premium" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblPremium" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:CommandField ShowDeleteButton="True" ItemStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" />--%>

                                <asp:CommandField ShowDeleteButton="True" ItemStyle-Font-Bold="true"
                                    HeaderStyle-ForeColor="#000000" DeleteText="Remove From Profile">
                                   <%-- <ItemStyle Font-Bold="True"></ItemStyle>--%>
                                   <ControlStyle Font-Bold="true" />
                                </asp:CommandField>
                            <%--<asp:HyperLinkField Text="Renew" />--%>
                            <asp:TemplateField HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server"><b>Renew</b></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink2" runat="server"><b>Temporary Cover Note</b></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <%--<br />--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <label for="inputPolicyNo" class="col-sm-3 control-label" id="lblGenPols" runat="server"
                        visible="false">
                        Non-Motor policies</label>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <asp:GridView ID="GridGenPols" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridGenPols_RowDeleting"
                        CssClass="footable" OnRowDeleted="GridGenPols_RowDeleted">
                        <Columns>
                            <asp:TemplateField HeaderText="Name" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblName2" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Policy Number" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblPolNum2" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Policy Type" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblPolTyp2" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="Sum Assured" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblSumAssurd2" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblStrtDate2" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblEndDate2" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="Premium" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblPremium2" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus2" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          <%--  <asp:CommandField ShowDeleteButton="True" ItemStyle-Font-Bold="true" HeaderStyle-ForeColor="Black">
                                <ItemStyle Font-Bold="True"></ItemStyle>
                            </asp:CommandField>--%>

                                 <asp:CommandField ShowDeleteButton="True" ItemStyle-Font-Bold="true"
                                    HeaderStyle-ForeColor="#000000" DeleteText="Remove From Profile">
                                   <%-- <ItemStyle Font-Bold="True"></ItemStyle>--%>
                                   <ControlStyle Font-Bold="true" />
                                </asp:CommandField>

                            <%--<asp:HyperLinkField Text="Renew" />--%>
                            <asp:TemplateField HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink2" runat="server"><b>Renew</b></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink3" runat="server"><b>Policy Schedule</b></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <%--  </div>--%>
            </br> </br> </br> </br> </br>
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
