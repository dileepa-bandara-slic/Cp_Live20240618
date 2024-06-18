<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true"
    CodeFile="AMP_Quotation.aspx.cs" Inherits="General_Authorized_Products_AMP_Quotation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="/js/jquery-3.5.1.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>

    <style>
        @media (max-width:479px)
        {
            .navbar-fixed-top + .main-container
            {
                padding-top: 40px;
            }
        }
        
        .test22
        {
            font-size: 100%;
            font-family: Tahoma;
        }
        
        .divWaiting
        {
            position: absolute;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 1024px;
            width: 100%;
            padding-top: 20%;
        }
        
     .style10
    {
        text-align: center;
        height: 25px;
        border: thin solid #DDDDDD;
        font-weight: bold;
        background-color: #a19d99;
        color: #000000;
    }
    .style11
    {
        color: #000000;
    }
    .style12
    {
        text-align: center;
        height: 25px;
        border: thin solid #DDDDDD;
        font-weight: bold;
        color: #000000;
        background-color: #a19d99;
    }

        .style13
    {
        height: 25px;
        font-weight: bold;
        border: thin solid #DDDDDD;
        background-color: #DCDCDC;
    }
    
    .style2
    {
            width: 80%;            
    }
     .btn-xx
    {
        background-color: #47B862 !important;
        color :white;
    }
   
    .btn-xx:hover, .btn-xx:focus, .btn-xx:active, .btn-xx.active
    {
        background-color: #47B862 !important;
        color :white;
    } 
    
     .btn-print
    {
        background-color: #899cce !important;
        color :white;
    }
   
    .btn-print:hover, .btn-print:focus, .btn-print:active, .btn-print.active
    {
        background-color: #899cce !important;
        color :white;
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
                font-weight:bold;
                /*font-family: Tahoma;*/
            }
              fieldset{
                width:100%;
            }
             
        }

        /*legend {
    display: block;
    width: 100%;
    padding: 0;
    margin-bottom: 20px;
    font-size: 21px;
    line-height: inherit;
    color: #333;
    border: 0;
    border-bottom: 1px solid #e5e5e5
}*/
        
    </style>

         <style>
     .modal-backdrop {
                 z-index: 0;
             }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container" id="main-container" style="min-height:600px">


            <link href="/css/footable.min.css" rel="stylesheet" type="text/css" />
            <script src="/js/footable.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=gvMembers]').footable();

            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('[id*=gvMembers]').footable();
            });

        </script>
   <%--     <script type='text/javascript'>
    $(function(){ $("#BrowserWidth").val($(window).width());});
</script>--%>

        <div class="container">
 <%--           </br>--%>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/General_Products.aspx">Purchase
                            online</a></li>
                        <li class="breadcrumb-item active">Medi Plus Plan Quotation</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            

<%--            </br>--%>
            <asp:UpdatePanel ID="Ggs14451" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="row">
                            <div class="col-xs-1">
                            </div>
                            <div class="col-xs-10">
                           <%--     <div class="form-horizontal">--%>
                               <%-- <div class="panel panel-default">--%>
                                <%--    <div class="panel-heading">--%>
                               <%--     <center>
                                        <h3> Annual Medical Plan - Quotation </h3>
                                        </center>--%>
                                   <%-- </div>--%>
                                   <%-- <div class="panel-body">--%>
                                   <div style="align:center">
                     <fieldset>
  <legend>Medi Plus Plan - Quotation:</legend>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                Category</div>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" Enabled="False">
                                                    <asp:ListItem Value="M">Main Life</asp:ListItem>
                                                    <asp:ListItem Value="S">Spouse</asp:ListItem>
                                                    <asp:ListItem Value="C">Child</asp:ListItem>
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
                                                <asp:RadioButtonList ID="rblGender" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                    Style="margin-left: 0px" Enabled="False">
                                                    <asp:ListItem Value="M" style="margin-right:10px">Male</asp:ListItem>
                                                    <asp:ListItem Value="F" style="margin-right:10px">Female</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="GenderRequired" runat="server" ControlToValidate="rblGender"
                                                    ErrorMessage="Gender required" ForeColor="Red" ToolTip="Gender is required.">*</asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="GenderValidator" runat="server" ControlToValidate="rblGender"
                                                    ForeColor="Red" OnServerValidate="checkGender"></asp:CustomValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                Date of Birth</div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtDOB" runat="server" AutoPostBack="True" CssClass="form-control"
                                                    MaxLength="10" OnTextChanged="txtDOB_TextChanged" placeholder="yyyy/mm/dd" Enabled="False"></asp:TextBox>
                                                <asp:Label ID="lblDOBMsg" runat="server" ForeColor="Red"></asp:Label>
                                                <asp:CustomValidator ID="DOBValidator" runat="server" ControlToValidate="txtDOB"
                                                    ForeColor="Red" OnServerValidate="checkDOB"></asp:CustomValidator>
                                            </div>
                                        </div>
                                        <br>
                                      
                                        <div class="row">
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary btn-xs" OnClick="btnSubmit_Click"
                                                    Text="Submit" />
                                            </div>
                                           
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:Label ID="lblErrMesg" runat="server" ForeColor="Red" Style="font-size: 12px"></asp:Label>
                                            </div>
                                        </div>
                                        <br>
                                        <div class="row">
                                            <asp:GridView ID="gvMembers" runat="server" AutoGenerateColumns="False" class="footable"
                                                OnRowDeleting="gvMembers_RowDeleting">
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
                                                    <asp:TemplateField HeaderText="Age">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAge" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                  
                                                    <asp:CommandField ControlStyle-CssClass="btn btn-danger btn-xs" ShowDeleteButton="True" />
                                                </Columns>
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <HeaderStyle Font-Bold="True" />
                                            </asp:GridView>
                                        </div>
                                        <br>
                                        <div class="row">
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:Button ID="btnCalc" runat="server" CssClass="btn btn-primary btn-xs" OnClick="btnCalc_Click"
                                                    Text="Get Quotation" Visible="False" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:Label ID="lblErrMesg2" runat="server" ForeColor="Red" Style="font-size: 12px"></asp:Label>
                                            </div>
                                        </div>
                                        <br>
                         </fieldset>
                                  <%--  </div>--%>

                               <%-- </div>--%>
                            </div>
                            </div>
                        </div>
                        <div class="col-xs-1">
                        </div>
                        </div>
                    </asp:Panel>
                    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="Ggs14451">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <img src="/images/load.gif" />
                                <asp:Label ID="lblWait" runat="server" Text=" Please wait... " />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <br />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnCalc" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:Panel ID="Panel2" runat="server" Visible="False">                
                <div class="table-responsive">
                    <table class="table table-bordered">
                        <%-- <table class="style2 table   table-hover">--%>
                        <tr>
                            <td class="style6">
                            </td>
                            <td class="style12">
                                <span class="style11">Plan A</span>
                            </td>
                            <td class="style10">
                                Plan B
                            </td>
                            <td class="style10">
                                Plan C
                            </td>
                            <td class="style10">
                                Plan D
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="style6">
                                Premium (Rs.)
                            </td>
                            <td class="style9" align="right">
                                <asp:Label ID="lblPremiumA" runat="server"></asp:Label>
                            </td>
                            <td class="style9" align="right">
                                <asp:Label ID="lblPremiumB" runat="server"></asp:Label>
                            </td>
                            <td class="style9" align="right">
                                <asp:Label ID="lblPremiumC" runat="server"></asp:Label>
                            </td>
                            <td class="style9" align="right">
                                <asp:Label ID="lblPremiumD" runat="server"></asp:Label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="style6">
                                &nbsp;
                            </td>
                            <td class="style7" align="center">
                                <asp:HyperLink ID="hlBuyA" runat="server" OnPreRender="hlBuyA_PreRender" CssClass="btn btn-xx btn-xs"
                                    Style="width: 60px" Font-Bold="True">Buy</asp:HyperLink>
                            </td>
                            <td class="style7" align="center">
                                <asp:HyperLink ID="hlBuyB" runat="server" CssClass="btn btn-xx btn-xs" Style="width: 60px"
                                    Font-Bold="True">Buy</asp:HyperLink>
                            </td>
                            <td class="style7" align="center">
                                <asp:HyperLink ID="hlBuyC" runat="server" CssClass="btn btn-xx btn-xs" Style="width: 60px"
                                    Font-Bold="True">Buy</asp:HyperLink>
                            </td>
                            <td class="style7" align="center">
                                <asp:HyperLink ID="hlBuyD" runat="server" CssClass="btn btn-xx btn-xs" Style="width: 60px"
                                    Font-Bold="True">Buy</asp:HyperLink>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="style6">
                                &nbsp;
                            </td>
                            <td class="style7" align="center">
                                <asp:HyperLink ID="hlPrintA" runat="server" CssClass="btn btn-print btn-xs" Style="width: 60px"
                                    Font-Bold="True">Print</asp:HyperLink>
                            </td>
                            <td class="style7" align="center">
                                <asp:HyperLink ID="hlPrintB" runat="server" CssClass="btn btn-print btn-xs" Style="width: 60px"
                                    Font-Bold="True">Print</asp:HyperLink>
                            </td>
                            <td class="style7" align="center">
                                <asp:HyperLink ID="hlPrintC" runat="server" CssClass="btn btn-print btn-xs" Style="width: 60px"
                                    Font-Bold="True">Print</asp:HyperLink>
                            </td>
                            <td class="style7" align="center">
                                <asp:HyperLink ID="hlPrintD" runat="server" CssClass="btn btn-print btn-xs" Style="width: 60px"
                                    Font-Bold="True">Print</asp:HyperLink>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="style6">
                                Sum Insured (Rs.)
                            </td>
                            <td class="style7" align="right">
                                250,000.00
                            </td>
                            <td class="style7" align="right">
                                500,000.00
                            </td>
                            <td class="style7" align="right">
                                750,000.00
                            </td>
                            <td class="style7" align="right">
                                1,000,000.00
                            </td>
                        </tr>
                        <tr>
                            <td class="style13" colspan="5">
                                Hospitalisation Benefit - Private Hospitals Only (Minimum Hospitalisation of 24
                                hours)
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                Hospital Room and Board (Ward/ICU per day) (Subject to a maximum 30% of annual sum
                                insured per year)
                            </td>
                            <td class="style7" align="right">
                                7,500.00
                            </td>
                            <td class="style7" align="right">
                                8,500.00
                            </td>
                            <td class="style7" align="right">
                                12,500.00
                            </td>
                            <td class="style7" align="right">
                                15,000.00
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                Surgeon, Anesthetist, Medical Practitioner, Consultant and Specialist fees etc (
                                Maximum Benefit)
                            </td>
                            <td class="style7" align="right">
                                75,000.00
                            </td>
                            <td class="style7" align="right">
                                150,000.00
                            </td>
                            <td class="style7" align="right">
                                225,000.00
                            </td>
                            <td class="style7" align="right">
                                300,000.00
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                Operation Theatre, medicines, laboratory tests, blood and Oxygen charges (Maximum Benefit)
                            </td>
                            <td class="style7" align="right">
                                112,500.00
                            </td>
                            <td class="style7" align="right">
                                225,000.00
                            </td>
                            <td class="style7" align="right">
                                337,500.00
                            </td>
                            <td class="style7" align="right">
                                450,000.00
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                **Ambulance Charges (Maximum Benefit)
                            </td>
                            <td class="style7" align="right">
                                2,500.00
                            </td>
                            <td class="style7" align="right">
                                3,000.00
                            </td>
                            <td class="style7" align="right">
                                4,000.00
                            </td>
                            <td class="style7" align="right">
                                5,000.00
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                *Maternity Benefits - Abnormal Childbirth; including cesarean surgery (Maximum benefit)
                            </td>
                            <td class="style7" align="right">
                                100,000.00
                            </td>
                            <td class="style7" align="right">
                                200,000.00
                            </td>
                            <td class="style7" align="right">
                                250,000.00
                            </td>
                            <td class="style7" align="right">
                                300,000.00
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                *Maternity Benefits - Normal Childbirth (Maximum benefit)
                            </td>
                            <td class="style7" align="right">
                                75,000.00
                            </td>
                            <td class="style7" align="right">
                                100,000.00
                            </td>
                            <td class="style7" align="right">
                                125,000.00
                            </td>
                            <td class="style7" align="right">
                                150,000.00
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                Pre and Post Hospitalisation Benefit
                            </td>
                            <td class="style7" colspan="4">
                                As charged up to a maximum of 5% of sum insured, limited to 30 days, prior/after hospitalization
                            </td>
                        </tr>
                        <tr>
                            <td class="style13" colspan="5">
                                Hospitalisation Benefit - Government Hospitals Only (Minimum Hospitalisation of
                                24 hours)
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                Hospitalized in a non paying ward - Daily allowance maximum upto 15 days
                            </td>
                            <td class="style7" align="right">
                                5,000.00
                            </td>
                            <td class="style7" align="right">
                                5,000.00
                            </td>
                            <td class="style7" align="right">
                                5,000.00
                            </td>
                            <td class="style7" align="right">
                                5,000.00
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                Medicines, Consumables, OT Charges etc. (Maximum benefit)
                            </td>
                            <td class="style7" align="right">
                                100,000.00
                            </td>
                            <td class="style7" align="right">
                                200,000.00
                            </td>
                            <td class="style7" align="right">
                                300,000.00
                            </td>
                            <td class="style7" align="right">
                                400,000.00
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                *Maternity Benefit (Normal Child birth/Abnormal Childbirth, including
                                cesarean surgery)
                            </td>
                            <td class="style7" align="right">
                                10,000.00
                            </td>
                            <td class="style7" align="right">
                                15,000.00
                            </td>
                            <td class="style7" align="right">
                                20,000.00
                            </td>
                            <td class="style7" align="right">
                                25,000.00
                            </td>
                        </tr>
                        <tr>
                            <td class="style13" colspan="5">
                                Epidemic and Pandemic cover (Covid 19)
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                Government Hospital per day allowance (Maximum 15 days within above government hospital allowance limit)
                            </td>
                            <td class="style7" align="right">
                                1,000.00
                            </td>
                            <td class="style7" align="right">
                                1,000.00
                            </td>
                            <td class="style7" align="right">
                                1,000.00
                            </td>
                            <td class="style7" align="right">
                                1,000.00
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                Private Hospitals/Intermediary care centre managed by approved health ministry registered private hospitals.
                            </td>
                            <td class="style7" align="right">
                                100,000.00
                            </td>
                            <td class="style7" align="right">
                                125,000.00
                            </td>
                            <td class="style7" align="right">
                                150,000.00
                            </td>
                            <td class="style7" align="right">
                                150,000.00
                            </td>
                        </tr>
                        <tr>
                            <td class="style13" colspan="5">
                                Additional Benefits (within annual sum Insured)
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                Treatment for Cataract (Including lenses)
                            </td>
                            <td class="style7" colspan="4">
                                20% of the sum insured, subject to a maximum of LKR 75,000
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="style6">
                                Day Care Surgeries
                            </td>
                            <td class="style7" colspan="4">
                                Covered subjected to hospitalisation for less than 24 hours; 135 Day Care Procedures
                                covered
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                Organ Donor Expenses
                            </td>
                            <td class="style7" colspan="4">
                                Covered within the sum insured of the donee
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                No Claim Bonus
                            </td>
                            <td class="style7" colspan="4">
                                Increase in basic sum insured by 5%, for every claim free year, upto a maximum of
                                50%. In case of a claim, Benefit limit will be reduced by 10%
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                Family Coverage
                            </td>
                            <td class="style7" colspan="4">
                                Self, Spouse and Maximum of five Children
                            </td>
                        </tr>
                        <tr>
                            <td class="style6" colspan="5">
                                Notes: Maternity Cover (Both Private and Government Hospitals): Only receivable
                                after two years waiting period for policies of female lives renewed without any
                                break
                                <br />
                                **Subject to a licensed ambulance service being used
                                <br />
                                *Critical illness cover with an inpatient limit
                                <br />
                                *Medical reports are not required upto age limit of 55 years
                                
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- Modal -->
                <div class="modal fade" id="myModal" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h5 class="modal-title">
                                    Indicative List</h5>
                            </div>
                            <div class="modal-body">
                                <ol>
                                    <li><span>Deviated Nasal Septum/ Nasal &amp; Paranasal Sinus Disorders (except Malignancy),
                                        Treatment for Chronic Suppurative Otitis Media (CSOM) and Serous Otitis Media (Grommet
                                        Insertion)<o:p></o:p></span> </li>
                                    <li><span>Medical or Surgical management of diseases of Tonsils / Adenoids (except Malignancy)<o:p></o:p></span>
                                    </li>
                                    <li><span>Surgery of Thyroid Gland excluding for the reason of Malignancy<o:p></o:p></span>
                                    </li>
                                    <li><span>All types of Hernias<o:p></o:p></span> </li>
                                    <li><span>Hydrocoele / Varicocoele / Spermatocoele<o:p></o:p></span> </li>
                                    <li><span>Piles / Fissure / Fistula-in-Ano / Rectal Prolapse / Pilonidal Sinus<o:p></o:p></span>
                                    </li>
                                    <li><span>Benign Prostatic Hypertrophy<o:p></o:p></span> </li>
                                    <li><span>Treatment of all gynaecological conditions (Such as but not limited to Uterine
                                        Fibroid, Dysfunctional Uterine Bleeding, Hysterectomy, Uterine Prolapse, Endometriosis,
                                        Adenomyosis Uteri, Ovarian Cyst etc) except those arising from malignancy<o:p></o:p></span>
                                    </li>
                                    <li><span>Prolapsed Intervertebral Disc<o:p></o:p></span> </li>
                                    <li><span>Hypertension and related complications<o:p></o:p></span> </li>
                                    <li><span>Skin and all internal cysts/tumors/nodules/ polyps/ganglions/lipomas of any
                                        kind unless malignant<o:p></o:p></span> </li>
                                    <li><span>Calculus Diseases of any aetiology<o:p></o:p></span> </li>
                                    <li><span>Retinopathy / Retinal Detachment<o:p></o:p></span> </li>
                                    <li><span>Peripheral Vascular Disease due to Diabetes / Diabetic Foot<o:p></o:p></span>
                                    </li>
                                    <li><span>All types of CRF and acute on chronic Renal Failures but not ARF, including
                                        Renal Failure due to Diabetes<o:p></o:p></span> </li>
                                    <li><span>Osteoporosis / Pathological Fracture / Degenerative Joint Diseases<o:p></o:p></span>
                                    </li>
                                    <li><span>Cataract<o:p></o:p></span> </li>
                                    <li><span>Treatment for degenerative joint conditions including joint replacement surgeries.
                                        However, joint surgeries necessitated due to accidents would not be a part of this
                                        exclusion<o:p></o:p></span> </li>
                                    <li><span>Treatment for benign breast disorders like fibroadenoma, fibrocystic disease
                                        etc<o:p></o:p></span> </li>
                                    <li><span>Treatment for Carpal tunnel syndrome<o:p></o:p></span> </li>
                                    <li><span>Treatment for Peripheral Vascular disease including varicose veins</span>
                                    </li>
                                </ol>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary btn-xs" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <br />
            <br />
            <br />
        </div>
    </div>
         <link href="/css/jquery-ui.css" rel="stylesheet" />
         <script src="/js/jquery-ui.js"></script>
    <script type="text/javascript" language="javascript">

        $(function () {
            today = new Date();
            var month, day, year;
            year = today.getFullYear();
            month = today.getMonth();
            date = today.getDate();
            year = today.getFullYear() - 60;
            var backdate = new Date(year, month, date)
            var backdate1 = new Date(year, month, date)

            $("input[id$='txtDOB']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, defaultDate: backdate, yearRange: '-100:+0' });

            //jQuery("input[id$='txtDOB']").datepicker();
        });



        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);



            today = new Date();
            var month, day, year;
            year = today.getFullYear();
            month = today.getMonth();
            date = today.getDate();
            year = today.getFullYear() - 60;
            var backdate = new Date(year, month, date)
            var backdate1 = new Date(year, month, date)


            function EndRequestHandler(sender, args) {

                // alert("ss");
                $("input[id$='txtDOB']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, defaultDate: backdate, yearRange: '-100:+0' });

            }

        }); 

 

    </script>
</asp:Content>
