<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="TRV_Proposal.aspx.cs"
    Inherits="General_Authorized_Products_TRV_Proposal" MaintainScrollPositionOnPostback="true" smartNavigation="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="/css/GT_table.css" rel="stylesheet" type="text/css" />

<%--    <style type="text/css">
        .date-form {
            margin: 10px;
        }

        label.control-label span {
            cursor: pointer;
        }

        .divWaiting {
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            position: fixed;
            top: 0px;
            height: 100%;
            text-align: center;
            left: 0;
            width: 100%;
            padding-top: 20%;
        }

        .table_headerr101 {
            height: 28px;
            font-weight: bold;
            border: thin solid #FFFFFF;
            color: White;
            background-color: #454545;
        }

        .table_rowww101 {
            width: 100%;
            height: 30px;
            border: thin solid #DDDDDD;
        }


        .btn-xx {
            background-color: #47B862 !important;
            border-color: #47B862 !important;
        }

            .btn-xx:hover, .btn-xx:focus, .btn-xx:active, .btn-xx.active {
                background-color: #47B862 !important;
                border-color: #47B862 !important;
            }

        .btn-print {
            background-color: #899cce !important;
            border-color: #899cce !important;
        }

            .btn-print:hover, .btn-print:focus, .btn-print:active, .btn-print.active {
                background-color: #899cce !important;
                border-color: #899cce !important;
            }
    </style>--%>
  <%--  <style>
        @media (max-width:479px) {
            .navbar-fixed-top + .main-container {
                padding-top: 50px;
            }
        }

        .linkGVE {
            color: White;
        }

        .auto-style1 {
            width: 100%;
            max-width: 1024px;
            height: 1878px;
            font-size: 12px;
            margin: 0 auto;
            padding-left: 12px;
            padding-right: 12px;
            background: #FFFFFF;
        }

        .auto-style2 {
            overflow-x: auto;
            min-height: .01%;
            width: 100%;
            overflow-y: hidden;
            -ms-overflow-style: -ms-autohiding-scrollbar;
            height: 756px;
            border: 1px solid #ddd;
            margin-bottom: 15px;
        }

        .auto-style3 {
            left: 0px;
            top: 5px;
        }

        .auto-style4 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 95%;
            color: #3399FF;
            padding-left: 12px;
            padding-right: 12px;
            left: 0px;
            top: 0px;
        }
    </style>--%>

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
    </style>

    <style>

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

        
        .auto-style2 {
            overflow-x: auto;
            min-height: .01%;
            width: 100%;
            overflow-y: hidden;
            -ms-overflow-style: -ms-autohiding-scrollbar;
            height: 756px;
            border: 1px solid #ddd;
            margin-bottom: 15px;
        }

         .btn-xx {
            background-color: #47B862 !important;
            border-color: #47B862 !important;
        }

            .btn-xx:hover, .btn-xx:focus, .btn-xx:active, .btn-xx.active {
                background-color: #47B862 !important;
                border-color: #47B862 !important;
            }

        .btn-print {
            background-color: #899cce !important;
        }

            .btn-print:hover, .btn-print:focus, .btn-print:active, .btn-print.active {
                background-color: #899cce !important;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container" id="main-container">
             <div class="container">
        <div class="auto-style1">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <script type="text/javascript">
                var xPos, yPos;
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_beginRequest(BeginRequestHandler);
                prm.add_endRequest(EndRequestHandler);
                function BeginRequestHandler(sender, args) {
                    xPos = $get('scrollDiv').scrollLeft;
                    yPos = $get('scrollDiv').scrollTop;
                }
                function EndRequestHandler(sender, args) {
                    $get('scrollDiv').scrollLeft = xPos;
                    $get('scrollDiv').scrollTop = yPos;
                }
            </script>
     
           
            <link href="/css/footable.min.css" rel="stylesheet" type="text/css" />
            <script src="/js/footable.min.js" type="text/javascript"></script>

             <script type="text/javascript">
                $(function () {
                    $('[id*=gvVisitCntrs]').footable();
                    $('[id*=gvMembers]').footable();

                });

                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    $('[id*=gvVisitCntrs]').footable();
                    $('[id*=gvMembers]').footable();

                });
            </script>

                 <style>
   
        .ui-state-highlight, .ui-widget-content .ui-state-highlight, .ui-widget-header .ui-state-highlight {

            border: 1px solid #c5c5c5 !important;
            background: #f6f6f6 !important;
            color: white;
        }

 
         </style>

            <br />
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/General_Products.aspx">Purchase
                            online</a></li>
                        <li class="breadcrumb-item active">Travel Protect Quotation</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <asp:UpdatePanel ID="Ggs14451" runat="server"   UpdateMode="Conditional">

                <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server">
                           <div id="scrollDiv" style="overflow-y: auto;">
                        <div class="row">
                            <div class="col-xs-1">
                            </div>
                            <div class="col-xs-10">

                                 <div style="align:center">

                                      <fieldset>
                                <%--<div class="form-horizontal">--%>
      <%--                              <div class="panel panel-default">--%>
                                 <%--       <div class="panel-heading">--%>
                                                 
  <legend> Travel Protect - Quotation  </legend>
                                      
                                       <%-- </div>--%>
                                      <%--  <div class="panel-body">--%>
                                            <%--<div class="row">
                                                <div class="col-sm-2">
                                                    Destination
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="ddlDestination" runat="server" class="form-control" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDestination_SelectedIndexChanged">
                                                        <asp:ListItem Value="">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="DestRequired" runat="server" ControlToValidate="ddlDestination"
                                                        ErrorMessage="Destination required" ForeColor="Red" ToolTip="Destination is required.">Destination is required.</asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ID="DestValidator" runat="server" ControlToValidate="ddlDestination"
                                                        ForeColor="Red" OnServerValidate="checkDestination"></asp:CustomValidator>
                                                </div>
                                            </div>--%>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                </div>
                                                <div class="auto-style4">
                                                    <font color="dodgerblue" face="sans-serif" size="2"><b>For any special notes required by Embassy/High commission, please contact Sri Lanka Insurance Corporation General Ltd Hot Line - 011 235 7357, to contact Head office</b></font>
                                                    <%--<font color="blue" face="sans-serif" size="2"><b>For any special notes required by Embassy/High commission, please contact Sri Lanka Insurance Hot Line - 011 235 7357, to contact Head office</b></font>--%>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <br />
                                                </div>
                                                <div class="col-sm-10">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    Departure date
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtFrmDate" runat="server" class="form-control" MaxLength="10" OnTextChanged="txtFrmDate_TextChanged"
                                                        placeholder="yyyy/mm/dd"></asp:TextBox>
                                                    <asp:CustomValidator ID="frmDtValidator" runat="server" ControlToValidate="txtFrmDate"
                                                        ForeColor="Red" OnServerValidate="checkFrmDate"></asp:CustomValidator>
                                                    <asp:RequiredFieldValidator ID="frmDtRequired" runat="server" ControlToValidate="txtFrmDate"
                                                        ErrorMessage="From Date required" ForeColor="Red" ToolTip="From Date required">From Date is required</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    Arrival date 
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" MaxLength="10" AutoPostBack="true"
                                                        OnTextChanged="txtToDate_TextChanged" placeholder="yyyy/mm/dd"></asp:TextBox>
                                                    <asp:CustomValidator ID="toDtValidator" runat="server" ControlToValidate="txtToDate"
                                                        ForeColor="Red" OnServerValidate="checkToDate"></asp:CustomValidator>

                                                    <asp:RequiredFieldValidator ID="ToDateRequired" runat="server" ControlToValidate="txtToDate"
                                                        ErrorMessage="To Date required" ForeColor="Red" ToolTip="To Date required">To Date is required</asp:RequiredFieldValidator>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <asp:Label ID="lblnoofDays0" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-10">

                                                    <asp:Label ID="lblnoofDays" runat="server"></asp:Label>

                                                </div>
                                            </div>
                                            <div class="row">
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    Agent Code 
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtxAgtcd" runat="server" CssClass="form-control" MaxLength="10" AutoPostBack="true" OnTextChanged="txtxAgtcd_TextChanged" placeholder="If you have any Sri Lanka Insurance Corporation General Ltd Service Agent enter here."></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <asp:CustomValidator ID="agtValidator" runat="server" ControlToValidate="txtxAgtcd" ForeColor="Red" OnServerValidate="checkagent"></asp:CustomValidator>
                                                    &nbsp;
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:PlaceHolder ID="ph102" Visible="true" runat="server">
                                                        <%--<div class="alert alert-warning" id="p_messages" style="height:30px; padding:5px;">--%>
                                                        <asp:Label ID="lbl_shen" runat="server" Font-Bold="true"
                                                            Font-Names="Calibri" Font-Size="10pt" ForeColor="#993300" />
                                                        <%--</div>--%>
                                                    </asp:PlaceHolder>
                                                    <asp:Label ID="lblAgntNAme" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-10">
                                                    <strong>Visiting Countries</strong>
                                                </div>
                                                <div class="col-sm-10">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <asp:GridView ID="gvVisitCntrs" runat="server" AutoGenerateColumns="False" CssClass="footable"
                                                    OnRowCreated="Gridview1_RowCreated" data-filter="#filter" OnRowDataBound="gvVisitCntrs_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="RowNumber" />
                                                        <asp:TemplateField HeaderText="From Sri Lanka">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlFrmCntry" runat="server" AppendDataBoundItems="True" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlFrmCntry_SelectedIndexChanged">
                                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbRemove" runat="server" class="btn btn-danger btn-xs" OnClick="lbRemove_Click">Remove</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <br />
                                            <asp:Label ID="lblErrMesg1" runat="server" ForeColor="Red" Style="font-size: 12px"></asp:Label>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-10">
                                                    <strong>Travellers&#39; Details </strong>
                                                </div>
                                                <div class="col-sm-10">
                                                    <br />
                                                </div>
                                                <div class="col-sm-3">
                                                    Plan Type
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="ddlPlanType" runat="server" AutoPostBack="True" class="form-control"
                                                        OnSelectedIndexChanged="ddlPlanType_SelectedIndexChanged">
                                                        <asp:ListItem Value="F">Family</asp:ListItem>
                                                        <asp:ListItem Value="I">Individual</asp:ListItem>
                                                        <asp:ListItem Value="G">Group</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="PlanTypeRequired" runat="server" ControlToValidate="ddlPlanType"
                                                        ErrorMessage="Plan Type required" ForeColor="Red" ToolTip="Plan Type is required.">*</asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ID="planTypValidator" runat="server" ControlToValidate="ddlPlanType"
                                                        ForeColor="Red" OnServerValidate="checkPlanType"></asp:CustomValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    Category
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="ddlCategory" runat="server" class="form-control">
                                                        <asp:ListItem Value="M">Main Life</asp:ListItem>
                                                        <asp:ListItem Value="S">Spouse</asp:ListItem>
                                                        <asp:ListItem Value="C">Child</asp:ListItem>
                                                        <asp:ListItem Value="N">Not Applicable</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="CategoryRequired" runat="server" ControlToValidate="ddlCategory"
                                                        ErrorMessage="Category required" ForeColor="Red" ToolTip="Category is required.">*</asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ID="CategoryValidator" runat="server" ControlToValidate="ddlCategory"
                                                        ForeColor="Red" OnServerValidate="checkCategory"></asp:CustomValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    Gender
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal" Style="margin-left: 0px">
                                                        <asp:ListItem Value="M" style="margin-right: 10px">Male</asp:ListItem>
                                                        <asp:ListItem Value="F" style="margin-right: 10px">Female</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="GenderRequired" runat="server" ControlToValidate="rblGender"
                                                        ErrorMessage="Gender required" ForeColor="Red" ToolTip="Gender is required.">*</asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ID="GenderValidator" runat="server" ControlToValidate="rblGender"
                                                        ForeColor="Red" OnServerValidate="checkGender">
                                                    </asp:CustomValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    Date of Birth
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtDOB" runat="server" class="form-control" MaxLength="10" AutoPostBack="true" OnTextChanged="txtDOB_TextChanged"
                                                        placeholder="yyyy/mm/dd"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="DOBRequired" runat="server" ControlToValidate="txtDOB"
                                                        ErrorMessage="Date of Birth required" ForeColor="Red" ToolTip="Date of Birth is required">*</asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ID="DOBValidator" runat="server" ControlToValidate="txtDOB"
                                                        ForeColor="Red" OnServerValidate="checkDOB"></asp:CustomValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary btn-xs" OnClick="btnSubmit_Click"
                                                        Text="Add member to Trip" CssClass="auto-style3" />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblErrMesg" runat="server" ForeColor="Red" Style="font-size: 12px"></asp:Label>
                                                </div>
                                            </div>
                                            <br>
                                            <div class="row">
                                                <asp:GridView ID="gvMembers" runat="server" AutoGenerateColumns="False" CssClass="footable"
                                                    OnRowDeleting="gvMembers_RowDeleting" data-filter="#filter">
                                                    <AlternatingRowStyle BackColor="#F9F9F9" />
                                                    <Columns>
                                                        <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />
                                                        <asp:TemplateField HeaderText="Category">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCategory" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gender">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGender" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date Of Birth">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDOB" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Age (Yrs)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAge" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ControlStyle-CssClass="btn btn-danger btn-xs" ShowDeleteButton="True"
                                                            CausesValidation="false" />
                                                    </Columns>
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:GridView>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:Button ID="btnCalc" runat="server" class="btn btn-primary btn-xs" OnClick="btnCalc_Click"
                                                        Text="Get Quotation" Visible="False" />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblErrMesg2" runat="server" ForeColor="Red" Style="font-size: 12px"></asp:Label>
                                                    <asp:Label ID="lblnothing" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <br>
                                     <%--   </div>--%>




                     <%--               </div>--%>



                               <%-- </div>--%>
                                          </fieldset>
                                     </div>
                            </div>
                            <div class="col-xs-1">
                            </div>
                        </div>
                    </asp:Panel>
                    <%-- </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnCalc" />
                </Triggers>
            </asp:UpdatePanel>--%>
                    <asp:Panel ID="Panel2" runat="server" Visible="False">
                        <div class="auto-style2" style="overflow-y: scroll;">
                            <table class="table">
                                <asp:GridView runat="server" ID="gvPlanDetails" AutoGenerateColumns="False" CellPadding="10"
                                    CellSpacing="10" OnRowDataBound="gvPlanDetails_RowDataBound" CssClass="col-md-12 table-bordered table-striped table-condensed cf">
                                    <Columns>
                                        <asp:BoundField DataField="BENEFIT"
                                            HeaderText="Benefit (All amounts are in USD)" ItemStyle-Width="50%">
                                            <ItemStyle Width="50%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SUM_AS25" HeaderText="Sum Insured">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EXC_AS25" HeaderText="Excess">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SUM_BSC1" HeaderText="Sum Insured">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EXC_BSC1" HeaderText="Excess">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SUM_BSC2" HeaderText="Sum Insured">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EXC_BSC2" HeaderText="Excess">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SUM_STD1" HeaderText="Sum Insured">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EXC_STD1" HeaderText="Excess">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SUM_STD2" HeaderText="Sum Insured">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EXC_STD2" HeaderText="Excess">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SUM_EST1" HeaderText="Sum Insured">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EXC_EST1" HeaderText="Excess">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SUM_EST2" HeaderText="Sum Insured">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EXC_EST2" HeaderText="Excess">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SUM_EST3" HeaderText="Sum Insured">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EXC_EST3" HeaderText="Excess">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </table>
                        </div>


                        </div>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:HiddenField ID="HiddenField2" runat="server" />

                    </asp:Panel>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnCalc" />
                    <%--<asp:PostBackTrigger ControlID="btnSubmit" />--%>
                </Triggers>
            </asp:UpdatePanel>
             

<%--            <script src="/js/jquery-ui-1.10.4.custom.js"></script>
            <script src="/js/bootstrap.min.js"></script>--%>
   <%--         <script src="../assets/js/jquery-3.5.1.min.js"></script>
            <script src="../assets/js/bootstrap.min.js"></script>--%>

            <link href="/css/jquery-ui.css" rel="stylesheet" />
            <script src="/js/jquery-ui.js"></script>

            <script type="text/javascript" language="javascript">

                $(function () {
                    today = new Date();
                    var month, day, year;
                    year = today.getFullYear();
                    month = today.getMonth();
                    date = today.getDate();
                    year = today.getFullYear() - 0;
                    var backdate = new Date(year, month, date)
                    var backdate1 = new Date(year, month, date)

                    $("input[id$='txtFrmDate']").datepicker({
                        dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, numberOfMonths: 1, showButtonPanel: true, minDate: 0,

                        onSelect: function (date) {
                            var date2 = $("input[id$='txtFrmDate']").datepicker('getDate');
                            date2.setDate(date2.getDate() + 1);
                            var date3 = $("input[id$='txtFrmDate']").datepicker('getDate');
                            date3.setDate(date2.getDate() + 180);

                            $("input[id$='txtToDate']").datepicker('option', 'minDate', date2);
                            $("input[id$='txtToDate']").datepicker('option', 'maxDate', date3);
                        }
                    });
                    $("input[id$='txtToDate']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, numberOfMonths: 1, showButtonPanel: true, minDate: 0 });
                    $("input[id$='txtDOB']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, yearRange: '-100:+0' });
                    //jQuery("input[id$='txtDOB']").datepicker();
                });



                $(document).ready(function () {

                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);


                    today = new Date();
                    var month, day, year;
                    year = today.getFullYear();
                    month = today.getMonth();
                    date = today.getDate();
                    year = today.getFullYear() - 0;
                    var backdate = new Date(year, month, date)
                    var backdate1 = new Date(year, month, date)


                    function EndRequestHandler(sender, args) {

                        // alert("ss");
                        $("input[id$='txtFrmDate']").datepicker({
                            dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, numberOfMonths: 1, showButtonPanel: true, minDate: 0,

                            onSelect: function (date) {
                                var date2 = $("input[id$='txtFrmDate']").datepicker('getDate');
                                date2.setDate(date2.getDate() + 1);
                                var date3 = $("input[id$='txtFrmDate']").datepicker('getDate');
                                date3.setDate(date2.getDate() + 179);

                                $("input[id$='txtToDate']").datepicker('option', 'minDate', date2);
                                $("input[id$='txtToDate']").datepicker('option', 'maxDate', date3);
                            }

                        });
                        $("input[id$='txtToDate']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, numberOfMonths: 1, showButtonPanel: true, minDate: 0 });
                        $("input[id$='txtDOB']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, yearRange: '-100:+0' });
                    }

                });
            </script>
            <br />
            <script type="text/javascript" language="javascript">

                $(document).ready(function () {
                    //alert("in");
                    $("#p_messages").hide();
                    $("#p_messages").fadeIn(700);
                });
            </script>

            <br />
            <center>
            <asp:HyperLink ID="hpl1" runat="server" Target="_blank" NavigateUrl="/Documents/GT.pdf"
                CssClass="btn btn-primary btn-xs" Visible="False">Terms & Conditions</asp:HyperLink></center>
        </div>
                 </div>
    </div>
</asp:Content>

