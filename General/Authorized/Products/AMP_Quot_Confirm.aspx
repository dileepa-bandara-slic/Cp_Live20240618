<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true"
    CodeFile="AMP_Quot_Confirm.aspx.cs" Inherits="General_Authorized_Products_AMP_Quot_Confirm" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        
        .style2
        {
            width: 20%;
        }
        
        .style5
        {
            height: 17px;
            font-weight: bold;
            font-size: large;
        }
        
        .style6
        {
            width: 20%;
            height: 17px;
        }
        .style7
        {
            height: 17px;
        }
        
        .style8
        {
            color: #FF0000;
        }
    </style>
    <script type="text/javascript" src="/js/backfix.min.js"></script>
    <script type="text/javascript">


        bajb_backdetect.OnBack = function () {

            window.location.replace('/WebSiteForTest/General/Authorized/Products/AMP_Quotation.aspx');
            //window.location.replace('http://www.google.lk');


            e.preventDefault();

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container" id="main-container" style="min-height:600px">

            <link href="/css/footable.min.css" rel="stylesheet" type="text/css" />
            <script src="/js/footable.min.js" type="text/javascript"></script>

        <script type="text/javascript">
            $(function () {
                $('[id*=gvDependents]').footable();
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('[id*=gvDependents]').footable();

            });

//            var prm = Sys.WebForms.PageRequestManager.getInstance();
//            prm.add_endRequest(function () {
//                $('[id*=gvDependents]').footable();
//            });
        </script>
        <div class="container">
       <%--  <br />--%>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/General_Products.aspx">Purchase
                            online</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/Products/AMP_Quotation.aspx">
                            Medi Plus Plan Quotation</a></li>
                        <li class="breadcrumb-item active">Medi Plus Plan Quotation - Confirm</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                <center>
                     <h3>Confirm Quotation: Medi Plus Plan</h3>
                     </center>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <br/>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <div class="form-group">
                        <table class="table">
                            <tbody>
                               
                                <tr>
                                    <td>
                                        Quotation Number
                                    </td>
                                    <td>
                                        <asp:Literal ID="litQuotNo" runat="server"></asp:Literal>
                                        <asp:HyperLink ID="hlPrintA" runat="server" CssClass="btn btn-primary btn-xs" Font-Bold="True"
                                            Target="_blank">Print</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Plan
                                    </td>
                                    <td>
                                        <asp:Literal ID="litPlan" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td>
                                        Residential Phone
                                    </td>
                                    <td>
                                        <asp:Label ID="Lit_homePhone" runat="server" data-toggle="tooltip" data-placement="bottom"
                                            title="To change these values, please change profile information."></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       Plan
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                      Premium (Rs.)
                                    </td>
                                    <td>
                                        <asp:Literal ID="litPremium" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                      Sum Insured (Rs.)
                                    </td>
                                    <td>
                                        <asp:Literal ID="litPlanLimit" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                     Start Date
                                    </td>
                                    <td>
                                        <%--  <div class="form-inline">--%>
                                        <asp:TextBox ID="txtStrtDt" runat="server" AutoPostBack="True" class="form-control input-xs"
                                            placeholder="yyyy/mm/dd" MaxLength="10" OnTextChanged="txtStrtDt_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="strtDtRequired" runat="server" ControlToValidate="txtStrtDt"
                                            ErrorMessage="Start Date is required" ForeColor="Red" ToolTip="Start Date is required">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStrtDt"
                                            ErrorMessage="* yyyy/mm/dd" Font-Bold="False" ValidationExpression="[0-9y]{4}/[0-9m]{1,2}/[0-9d]{1,2}"
                                            CssClass="errorMsg_1"></asp:RegularExpressionValidator>
                                        </br>
                                        <asp:CustomValidator ID="StrtDtValidator" runat="server" ControlToValidate="txtStrtDt"
                                            ForeColor="Red" OnServerValidate="checkStrtDt"></asp:CustomValidator>
                                        <div class="alert-warning" id="start_date_msg" style="display: none; text-align: left;
                                            background-color: #FFFFFF; font-weight: bold">
                                        </div>
                                        <%--   </div>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Proposer&#39;s Name
                                    </td>
                                    <td>
                                        <asp:Literal ID="litTitle" runat="server"></asp:Literal>
                                        &nbsp;<asp:Literal ID="litName" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Address
                                    </td>
                                    <td>
                                        <asp:Literal ID="litAddress" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gender
                                    </td>
                                    <td>
                                        <asp:Literal ID="litGender" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Height (cm)
                                    </td>
                                    <td>
                                        <asp:Literal ID="litHeight" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Weight (kg)
                                    </td>
                                    <td>
                                        <asp:Literal ID="litWeight" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Mobile Number
                                    </td>
                                    <td>
                                        <asp:Literal ID="litMobileNo" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Home Number
                                    </td>
                                    <td>
                                        <asp:Literal ID="litHomeNo" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Office Number
                                    </td>
                                    <td>
                                        <asp:Literal ID="litOfficeNo" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email
                                    </td>
                                    <td>
                                        <asp:Literal ID="litEmail" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        NIC Number
                                    </td>
                                    <td>
                                        <asp:Literal ID="litNIC" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Passport Number
                                    </td>
                                    <td>
                                        <asp:Literal ID="litPPNo" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date of Birth
                                    </td>
                                    <td>
                                        <asp:Literal ID="litBirthDate" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Occupation<strong> *</strong>
                                    </td>
                                    <td>
                                        <%--<div class="form-inline">--%>
                                        <asp:TextBox ID="txtOccupation" runat="server" MaxLength="50" AutoPostBack="False"
                                            OnTextChanged="txtOccup_TextChanged" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="OccupRequired" runat="server" ControlToValidate="txtOccupation"
                                            ErrorMessage="Occupation is required" ForeColor="Red" ToolTip="Occupation is required">*</asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="OccupValidator" runat="server" ControlToValidate="txtOccupation"
                                            ForeColor="Red" OnServerValidate="checkOccup"></asp:CustomValidator><%--</div>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Nature of Occupation
                                    </td>
                                    <td>
                                        <%--<div class="form-inline">--%><asp:TextBox ID="txtNaturOccup" runat="server" MaxLength="50"
                                            AutoPostBack="False" OnTextChanged="txtNaturOccup_TextChanged" CssClass="form-control"></asp:TextBox>
                                        <asp:CustomValidator ID="NaturOccupValidator" runat="server" ControlToValidate="txtNaturOccup"
                                            ForeColor="Red" OnServerValidate="checkNaturOccup"></asp:CustomValidator><%--</div>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Employer Name
                                    </td>
                                    <td>
                                        <%--<div class="form-inline">--%>
                                        <asp:TextBox ID="txtEmplName" runat="server" MaxLength="50" AutoPostBack="False" OnTextChanged="txtEmplName_TextChanged"
                                            CssClass="form-control" placeholder="Name of the employer or work place"></asp:TextBox>
                                        <asp:CustomValidator ID="EmplNameValidator" runat="server" ControlToValidate="txtEmplName"
                                            ForeColor="Red" OnServerValidate="checkEmplName"></asp:CustomValidator><%--</div>--%>
                                    </td>
                                </tr>
                                

                                 </tbody>
                                 </table>
                                 </div>
                                 </div>
                                 <div class="col-xs-1">
                </div>
                </div>

                
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <div class="form-group">
                    
                        <table class="table">
                            <tbody>
                            <%if (gvDependents.Rows.Count > 0)
                              { %>
                                <tr>
                                    <td>
                                        Dependents
                                    </td>
                                    <td></td>
                                </tr>

                                <tr>
                                    <asp:GridView ID="gvDependents" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" class="footable"
                                        GridLines="Vertical">
                                        <AlternatingRowStyle BackColor="#F9F9F9" />
                                        <Columns>
                                            <asp:BoundField DataField="MemId" HeaderText="Number" SortExpression="MemId" />
                                            <asp:BoundField DataField="Category" HeaderText="Member Type" SortExpression="Category" />
                                            <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                            <asp:BoundField DataField="Dob" HeaderText="Date of Birth" SortExpression="Dob" />
                                            <asp:BoundField DataField="Age" HeaderText="Age (Yrs)" SortExpression="Age" />
                                            <asp:BoundField DataField="Height" HeaderText="Height (cm)" SortExpression="Height" />
                                            <asp:BoundField DataField="Weight" HeaderText="Weight (kg)" SortExpression="Weight" />
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtName"  runat="server" MaxLength="100" CssClass="form-control"
                                                        placeholder="Dependant's Name"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NIC No.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNIC" runat="server" MaxLength="12" CssClass="form-control" placeholder="(Optional for Non-Sri Lankans/Children)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Passport No.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPP" runat="server" MaxLength="20" CssClass="form-control" placeholder="(Optional for Sri Lankans)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle Font-Bold="True" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                </tr>
                                <%} %>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnConfirm" runat="server" Text="Confirm Quotation" class="btn btn-primary btn-xs"
                                            OnClick="btnConfirm_Click" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                <td> <asp:Label ID="lblErrMesg" runat="server" ForeColor="Red"></asp:Label></td>
                                <td></td>
                                </tr>

                                </br>
                            </tbody>
                        </table>
                    
                        
                    </div>
                </div>
                <div class="col-xs-1">
                </div>
            </div>

        </div>
        <script type="text/javascript" language="javascript">

            function display(msg) {
                //$("<p>").html(msg).appendTo(document.body);
                //$('<p>Text</p>').text('#start_date_msg');


                var myDate = new Date(msg);
                myDate.setFullYear(myDate.getFullYear() + 1);
                myDate.setDate(myDate.getDate() - 1);


                $("#start_date_msg").text("  Effective period will be from " + msg + " to " + myDate.getFullYear() + "/" + ("0" + (myDate.getMonth() + 1)).slice(-2) + "/" + ("0" + myDate.getDate()).slice(-2)
);
                $("#start_date_msg").fadeIn(500);
                // $("[data-toggle='tooltip']").tooltip('show');
            }

            $(function () {
                today = new Date();
                var month, day, year;
                year = today.getFullYear();
                month = today.getMonth();
                date = today.getDate();
                year = today.getFullYear() - 60;
                var backdate = new Date(year, month, date)
                var backdate1 = new Date(year, month, date)

                $("input[id$='txtStrtDt']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, maxDate: 14, minDate: 0,

                    onSelect: function (dateText) {
                        display(this.value);
                    }
                });

                //jQuery("input[id$='txtDOB']").datepicker();
            });

            //     $("input[id$='txtStrtDt']").focusout(function () {
            //         //$("[data-toggle='tooltip']").tooltip('show');
            //         $(document).tooltip();
            //     })


            $(document).ready(function () {

                //window.history.forward(1);
                // window.history.forward(-1);

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
                    $("input[id$='txtStrtDt']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, maxDate: 14, minDate: 0,
                        onSelect: function (dateText) {
                            display(this.value);
                        }

                    });

                }

            }); 

 

        </script>
    </div>
</asp:Content>
