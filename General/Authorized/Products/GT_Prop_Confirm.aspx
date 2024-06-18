<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" MaintainScrollPositionOnPostback = "true"
    CodeFile="GT_Prop_Confirm.aspx.cs" Inherits="General_Authorized_Products_GT_Prop_Confirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="../assets/js/jquery-3.5.1.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/js/jquery-3.5.1.js"></script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container" id="main-container" style="min-height:600px">
        <div class="container">
          
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
           
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
                        <li class="breadcrumb-item">Globe Trotter Quotation</li>
                        <li class="breadcrumb-item active">Globe Trotter Quotation - Confirm</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
      <%--      </br>--%>

               <asp:UpdatePanel ID="Ggs14451" runat="server">
                <ContentTemplate>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                <center>
                    <h3>
                        Confirm Quotation: Globe Trotter Plan</h3>
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
                                        <asp:HyperLink ID="hlPrint" runat="server" class="btn btn-primary btn-xs" Font-Bold="True"
                                            Target="_blank">Print</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Plan
                                    </td>
                                    <td>
                                        <asp:Literal ID="litPlan" runat="server"></asp:Literal>
                                        <asp:HiddenField ID="hdn_plan" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Sum Insured (USD)
                                    </td>
                                    <td>
                                        <asp:Literal ID="litSumIns" runat="server"></asp:Literal>
                                    </td>
                                </tr>
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
                                        Start Date
                                    </td>
                                    <td>
                                        <asp:Literal ID="litStrtDt" runat="server"></asp:Literal>
                                    </td>
                                </tr>

                                 <tr>
                                    <td>
                                        Purpose of visit
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddr_purpose" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="ddrPurposeValidator" runat="server" ControlToValidate="ddr_purpose"
                                            ForeColor="Red" OnServerValidate="checkDdrPurpose" CssClass="errorMsg_1"></asp:CustomValidator>
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
                                <td>NIC Number
                                </td>

                                <td><asp:Literal ID="litNIC" runat="server"></asp:Literal>
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
                                        Address
                                    </td>
                                    <td>
                                        <asp:Literal ID="litAddress" runat="server"></asp:Literal>
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
                                        Leaving Sri Lanka on
                                    </td>
                                    <td>
                                        <asp:Literal ID="lit_leaving" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Returning to Sri Lanka on
                                    </td>
                                    <td>
                                        <asp:Literal ID="lit_returning" runat="server"></asp:Literal>
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_shen" runat="server" Font-Bold="True" 
                                                    Font-Names="Calibri" Font-Size="10pt" ForeColor="#993300" />
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
                    Details of Journey (Visiting Countries)
                </div>
                <div class="col-xs-1">
                </div>
            </div>
              <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                <asp:GridView ID="GridView1" class="footable" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Row Number">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="From">
                            <ItemTemplate>
                                <asp:Label ID="txt_from" runat="server" MaxLength="100" placeholder="From"><%# Eval("FROM") %></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="To">
                            <ItemTemplate>
                                <asp:Label ID="txt_to" runat="server" MaxLength="100" placeholder="To"><%# Eval("TO") %></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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
                            <caption>
                                <strong>Contact person / beneficiary in case of an emergency </strong>
                                <br>
                                </tr>
                                <tr>
                                    <td>
                                        Name
                                    </td>
                                    <td>
                                        <%--<div class="form-inline">--%>
                                        <asp:TextBox ID="txt_ConName" runat="server" class="form-control" 
                                            MaxLength="100" OnTextChanged="txt_ConName_TextChanged" placeholder="Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="cntNamRequired" runat="server" 
                                            ControlToValidate="txt_ConName" CssClass="errorMsg_1" 
                                            ErrorMessage="Contact Name is required" ForeColor="Red" 
                                            ToolTip="Contact Name is required"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="cntNameValidator" runat="server" 
                                            ControlToValidate="txt_ConName" CssClass="errorMsg_1" ForeColor="Red" 
                                            OnServerValidate="checkCntName"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Address
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_ConAdd1" runat="server" class="form-control" 
                                            MaxLength="30" OnTextChanged="txt_ConAdd1_TextChanged" 
                                            placeholder="Address Line 1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="contAdrs1Required" runat="server" 
                                            ControlToValidate="txt_ConAdd1" CssClass="errorMsg_1" 
                                            ErrorMessage="Address Line 1 is required" ForeColor="Red" 
                                            ToolTip="Address Line 1 is required"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="adrs1Validator" runat="server" 
                                            ControlToValidate="txt_ConAdd1" CssClass="errorMsg_1" ForeColor="Red" 
                                            OnServerValidate="checkCntAdrs1"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_ConAdd2" runat="server" class="form-control" 
                                            MaxLength="30" OnTextChanged="txt_ConAdd2_TextChanged" 
                                            placeholder="Address Line 2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="contAdrs2Required" runat="server" 
                                            ControlToValidate="txt_ConAdd2" CssClass="errorMsg_1" 
                                            ErrorMessage="Address Line 2 is required" ForeColor="Red" 
                                            ToolTip="Address Line 2 is required"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="adrs2Validator" runat="server" 
                                            ControlToValidate="txt_ConAdd2" CssClass="errorMsg_1" ForeColor="Red" 
                                            OnServerValidate="checkCntAdrs2"></asp:CustomValidator>
                                    </td>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_ConAdd3" runat="server" class="form-control" 
                                                MaxLength="30" OnTextChanged="txt_ConAdd3_TextChanged" 
                                                placeholder="Address Line 3"></asp:TextBox>
                                            <asp:CustomValidator ID="adrs3Validator" runat="server" 
                                                ControlToValidate="txt_ConAdd3" CssClass="errorMsg_1" ForeColor="Red" 
                                                OnServerValidate="checkCntAdrs3"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_ConAdd4" runat="server" class="form-control" 
                                                MaxLength="30" placeholder="Address Line 4"></asp:TextBox>
                                            <asp:CustomValidator ID="adrs4Validator" runat="server" 
                                                ControlToValidate="txt_ConAdd4" CssClass="errorMsg_1" ForeColor="Red" 
                                                OnServerValidate="checkCntAdrs4"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Telephone
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_ConTel1" runat="server" class="form-control" 
                                                MaxLength="15" OnTextChanged="txt_ConTel1_TextChanged" 
                                                placeholder="Telephone No. 1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="tel1Required" runat="server" 
                                                ControlToValidate="txt_ConTel1" CssClass="errorMsg_1" 
                                                ErrorMessage="Contact number is required" ForeColor="Red" 
                                                ToolTip="Contact number is required"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="tel1Validator" runat="server" 
                                                ControlToValidate="txt_ConTel1" CssClass="errorMsg_1" ForeColor="Red" 
                                                OnServerValidate="checkCntTel1"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_ConTel2" runat="server" class="form-control" 
                                                MaxLength="15" OnTextChanged="txt_ConTel2_TextChanged" 
                                                placeholder="Telephone No. 2"></asp:TextBox>
                                            <asp:CustomValidator ID="tel2Validator" runat="server" 
                                                ControlToValidate="txt_ConTel2" CssClass="errorMsg_1" ForeColor="Red" 
                                                OnServerValidate="checkCntTel2"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    </tbody>
                                </tr>
                            </caption>
                        </table>
                    </div>
                </div>
                <div class="col-xs-1">
                </div>
                </div>
                                    <div class="row">
                                        <div class="col-xs-1"></div>
                                        <div class="col-xs-10">
                                            <strong>All members</strong>
                                        </div>
                                         <div class="col-xs-1"></div>
                                    </div>
                                     <div class="row">
                                        <div class="col-xs-1"></div>
                                        <div class="col-xs-10">
                                        <asp:GridView ID="gvMembers" class="footable" runat="server" AutoGenerateColumns="false"
                                            ForeColor="Black">
                                            <AlternatingRowStyle BackColor="#F9F9F9" />
                                            <Columns>
                                                <asp:BoundField DataField="member_id" HeaderText="Number" SortExpression="member_id" />
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddTitle" runat="server" placeholder="Title">
                                                            <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
                                                            <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
                                                            <asp:ListItem Value="Miss.">Miss.</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name as in Passport">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtName" runat="server" MaxLength="200" CssClass="form-control"
                                                            placeholder="Name as in Passport"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Passport No.">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPP" runat="server" MaxLength="20" CssClass="form-control" placeholder="Passport No."></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="genderDesc" HeaderText="Gender" SortExpression="genderDesc" />
                                                <asp:BoundField DataField="dob" HeaderText="Date of Birth" SortExpression="dob" />
                                                <asp:BoundField DataField="age" HeaderText="Age" SortExpression="age" />
                                                <asp:BoundField DataField="memType_Desc" HeaderText="Member Type" SortExpression="membType_Desc" />
                                            </Columns>
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                   </div>
                                   <div class="col-xs-1"></div>
                                   </div>
                                   <br>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10" style="text-align:center">
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-xs" 
                                OnClick="Button1_Click" Text="Submit" />
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Label ID="lblErrMesg" runat="server" CssClass="errorMsg_1" ForeColor="Red"></asp:Label>
                        </div>
                    </div>

                    </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="Button1" />
                </Triggers>
            </asp:UpdatePanel>
              <br />
              <br />
              <br />
        </div>
 

         <link href="/css/footable.min.css" rel="stylesheet" type="text/css" />
            <script src="/js/footable.min.js" type="text/javascript"></script>

            <script type="text/javascript">
                $(function () {
                    $('[id*=GridView1]').footable();
                    $('[id*=gvMembers]').footable();
                });

                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    $('[id*=GridView1]').footable();
                    $('[id*=gvMembers]').footable();

                });
            </script>

    </div>


</asp:Content>
