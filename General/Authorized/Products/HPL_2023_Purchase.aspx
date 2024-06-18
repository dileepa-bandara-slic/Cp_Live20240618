<%@ Page Title="HPLPurchase" Language="C#" MaintainScrollPositionOnPostback="true"  MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="HPL_2023_Purchase.aspx.cs" Inherits="General_Authorized_Products_HPL_2023_Purchase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        
     .Table_tdplan{
            text-align: center;
        }

        .Table_tdplan_ErPos{
            padding-left:3%;
        }

        .marginga_top{
            margin-top:3px;
        }

        .label_sig{
            font-weight:400;
        }

        .error_control {
            color: #D50000;
            font-size: 0.80em;
            font-weight: 600;
        }

        .error_control_Name {
            color: #D50000;
            font-size: 0.80em;
            font-weight: 600;
            padding-left:11%;
            position:static;
        }

        .checkboxList {
            margin-top: 3px;
            margin-left: 0px;
        }
              .checkboxList label {
                  margin-left: 5px;
                  font-weight: 200;
              }

        .Table_desc {
            border: 1px solid black;
            border-collapse: collapse;
            text-align: left;
            padding: 7px 5px 7px 5px;
            text-align: justify; 
            text-justify: inter-word;
        }

        .Table_td {
            border: 1px solid black;
            border-collapse: collapse;
            text-align: center;
        } 
        
         .modalBackground
        {
            background-color: #424242;
            filter: alpha(opacity=90);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #fff;
            border: 2px solid #ccc;
            padding: 10px;
            width: 50vw;
        }      
    </style>

    <script type="text/javascript">

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function SingleSelection(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }


        function GetSelectedPerils() {
            var checkBoxList = document.getElementById("<%=chk_perils.ClientID%>");
            var checkBoxes = checkBoxList.getElementsByTagName("INPUT");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked) {
                    var value = checkBoxes[i].value;

                    if (value == 'Y') {
                        document.getElementById("<%=txt_PerilsReson.ClientID%>").removeAttribute("disabled");
                        document.getElementById("<%=Rfv_PerilsReson.ClientID%>").enabled = true;
                        ShowModalPopup();
                        btnUpload_onclick1();
                    }
                    else {
                        DissablePerilsReson();
                        HideModalPopup();
                    }
                }
            }
        }


        function DissablePerilsReson() {
            document.getElementById("<%=txt_PerilsReson.ClientID%>").disabled = true;
            document.getElementById("<%=Rfv_PerilsReson.ClientID%>").enabled = false;
            document.getElementById("<%=Rfv_PerilsReson.ClientID%>").innerHTML = '';
            document.getElementById('<%= txt_PerilsReson.ClientID %>').value = '';

            var checkBoxList = document.getElementById("<%=chk_perils.ClientID%>");
            var checkBoxes = checkBoxList.getElementsByTagName("INPUT");
            checkBoxes[0].checked = false;
            checkBoxes[1].checked = true;
        }

        function ValidateCheckBoxchk_Perils(sender, args) {
            var checkBoxList = document.getElementById("<%=chk_perils.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }

        function ValidateCheckBoxchk_Declinned(sender, args) {
            var checkBoxList = document.getElementById("<%=chk_declinned.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }

        function ValidateCheckBoxchk_Plan(sender, args) {
            var checkBoxList = document.getElementById("<%=chk_perils.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");

            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }

        function ValidateChkBoxSignature(sender, args) {
            if (document.getElementById("<%=chk_signature.ClientID %>").checked == true) {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }

        function ValidateChkBoxSignature_ActFunction(sender, args) {
            if (document.getElementById("<%=chk_signature.ClientID %>").checked == true) {
                document.getElementById("<%=btnSubmit.ClientID%>").removeAttribute("disabled");
            } else {
                document.getElementById("<%=btnSubmit.ClientID%>").disabled = true;
            }
        }

        function ResetSelection() {
            var checkBoxList = document.getElementById("<%=chk_perils.ClientID%>");
            var checkBoxes = checkBoxList.getElementsByTagName("INPUT");
            checkBoxes[0].checked = false;
            document.getElementById("<%=txt_PerilsReson.ClientID%>").disabled = true;
            document.getElementById("<%=Rfv_PerilsReson.ClientID%>").enabled = false;
        }

        function ShowModalPopup() {
            $find("sust_mpe").show();
            return false;
        }

        function HideModalPopup() {
            $find("sust_mpe").hide();
            return false;
        }


        function EnableDisableAgtValidation(txtid) {
            var btnAgt_validate = document.getElementById("<%=btn_agtValidate.ClientID%>");
            var agt_Text= document.getElementById("<%=hpl_message.ClientID%>");
            
            if (txtid.value.trim() != "" ) {
                btnAgt_validate.removeAttribute("disabled");
                document.getElementById("<%=chk_signature.ClientID%>").disabled = true;
                agt_Text.innerHTML = '';
                
            } else {
                btnAgt_validate.disabled = true;
                document.getElementById("<%=chk_signature.ClientID%>").disabled = false;
                agt_Text.innerHTML = '';
            }
        };

 
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {

            }
        });
    </script>
 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
    </ajaxToolkit:ToolkitScriptManager>

    <div class="row">
        <div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="/">Home</a></li>
                <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                <li class="breadcrumb-item"><a href="/General/Authorized/General_Products.aspx">Purchase
                    online</a></li>
                <li class="breadcrumb-item active">Home protect lite purchase</li>
            </ol>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-7 offset-lg-2">
            <div class="form-group row">
                <label for="inputPassword" class="col-sm-5 col-form-label" style="width: 400px">
                    Is logged user and proposer are the same?</label>
                <div class="col-sm-7" style="padding-top: 5px;">
                    <asp:CheckBoxList ID="chk_UserConfirm" runat="server" Width="24%" RepeatDirection="Horizontal"
                        RepeatLayout="Table" CssClass="checkboxList Table_tdplan" class="form-control Table_tdplan"
                        OnSelectedIndexChanged="OnCheckBox_Changed" AutoPostBack="true">
                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                    </asp:CheckBoxList>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="FormValidation_01" ControlToValidate="txtPeriod" runat="server" ErrorMessage="*Required" CssClass="error_control" Display="Dynamic" />               --%>
                </div>
            </div>
            <div class="form-group">
                <label>
                    Name of Proposer in Full</label>
                <div class="form-inline">
                    <select class="form-control" style="width: 10%; height: 34px;" id="ch_Title" runat="server">
                        <option selected="selected" value="0">Title</option>
                        <option value="Mr.">Mr.</option>
                        <option value="Mrs.">Mrs.</option>
                        <option value="Miss.">Miss.</option>
                    </select>
                    &nbsp;
                    <input id="txtcusName" type="text" runat="server" placeholder="Enter your name" style="width: 89%" maxlength="200"
                        class="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="*Required"
                        ControlToValidate="ch_Title" InitialValue="0" runat="server" CssClass="error_control"
                        Display="Dynamic" ValidationGroup="FormValidation_01" />
                    <asp:RequiredFieldValidator ID="rfv_cusName" ValidationGroup="FormValidation_01"
                        ControlToValidate="txtcusName" runat="server" ErrorMessage="*Required" CssClass="error_control_Name"
                        Display="Dynamic" />
                </div>
            </div>
            <div class="form-group row">
                <label for="inputPassword" class="col-sm-3 col-form-label">
                    Postal Address</label>
                <div class="col-sm-9">
                    <input type="text" class="form-control" id="txtaddress_1" aria-describedby="addressHelp" maxlength="30"
                        placeholder="Enter address 1" runat="server">
                    <asp:RequiredFieldValidator ID="rfv_cusAdd" ValidationGroup="FormValidation_01" ControlToValidate="txtaddress_1"
                        runat="server" ErrorMessage="*Required" CssClass="error_control" Display="Dynamic" />
                    <input type="text" class="form-control marginga_top" id="txtaddress_2" aria-describedby="addressHelp" maxlength="30"
                        placeholder="Enter address 2" runat="server">
                    <asp:RequiredFieldValidator ID="rfv_cusAdd2" ValidationGroup="FormValidation_01"
                        ControlToValidate="txtaddress_2" runat="server" ErrorMessage="*Required" CssClass="error_control"
                        Display="Dynamic" />
                    <input type="text" class="form-control marginga_top" id="txtaddress_3" aria-describedby="addressHelp" maxlength="30"
                        placeholder="Enter address 3" runat="server">
                    <asp:RequiredFieldValidator ID="rfv_cusAdd3" ValidationGroup="FormValidation_01"
                        ControlToValidate="txtaddress_3" runat="server" ErrorMessage="*Required" CssClass="error_control"
                        Display="Dynamic" />
                    <input type="text" class="form-control marginga_top" id="txtaddress_4" aria-describedby="addressHelp" maxlength="30"
                        placeholder="Enter address 4" runat="server">
                    <small id="addressHelp" class="form-text text-muted">We'll never share your postal
                        address with anyone else.</small>
                </div>
            </div>
            <div class="form-group row">
                <label for="inputPassword" class="col-sm-3 col-form-label">
                    Contact Number (Mobile)</label>
                <div class="col-sm-9">
                    <input type="text" class="form-control" id="txt_contactno" aria-describedby="contactnoHelp"
                        placeholder="Enter contact number" runat="server" maxlength="10" onkeypress="return isNumber(event)">
                    <small id="contactnoHelp" class="form-text text-muted">We'll never share your contact
                        number with anyone else.</small>
                    <asp:RequiredFieldValidator ID="rfv_ContactNo" ValidationGroup="FormValidation_01"
                        ControlToValidate="txt_contactno" runat="server" ErrorMessage="*Required" CssClass="error_control"
                        Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="RegularExp_contact" runat="server" Display="Dynamic"
                        CssClass="error_control" ErrorMessage="Invalid contact number" ControlToValidate="txt_contactno"
                        ValidationGroup="FormValidation_01" ValidationExpression="07[0-9]{8}"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-group row">
                <label for="inputPassword" class="col-sm-3 col-form-label">
                    E-mail</label>
                <div class="col-sm-9">
                    <input type="text" class="form-control" id="txt_email" aria-describedby="emailHelp" maxlength="100"
                        placeholder="Enter email" runat="server">
                    <small id="emailHelp" class="form-text text-muted">We'll never share your email with
                        anyone else.</small>
                    <asp:RequiredFieldValidator ID="rfv_Email" ValidationGroup="FormValidation_01" ControlToValidate="txt_email"
                        runat="server" ErrorMessage="*Required" CssClass="error_control" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="RegularExp_Email" runat="server" Display="Dynamic"
                        CssClass="error_control" ErrorMessage="Invalid E-Mail address" ControlToValidate="txt_email"
                        ValidationGroup="FormValidation_01" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-group row">
                <label for="inputPassword" class="col-sm-3 col-form-label">
                    NIC No</label>
                <div class="col-sm-9">
                    <input type="text" class="form-control" id="txt_nic" aria-describedby="niclHelp"
                        placeholder="Enter national identity card number" runat="server" maxlength="12">
                    <small id="niclHelp" class="form-text text-muted">We'll never share your national identity
                        card number with anyone else.</small>
                    <asp:RequiredFieldValidator ID="rfv_NIC" ValidationGroup="FormValidation_01" ControlToValidate="txt_nic"
                        runat="server" ErrorMessage="*Required" CssClass="error_control" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="RegularExp_NIC" runat="server" Display="Dynamic"
                        CssClass="error_control" ErrorMessage="Invalid national identity card number"
                        ControlToValidate="txt_nic" ValidationGroup="FormValidation_01" ValidationExpression="^[0-9]{9}[V,X,v,x]$|[1-2]{1}[0-9]{11}$"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-group row">
                <label for="inputPassword" class="col-sm-3 col-form-label">
                    Profession/Occupation</label>
                <div class="col-sm-9">
                    <input type="text" class="form-control" id="txt_occupation" aria-describedby="occupationHelp" maxlength="50"
                        placeholder="Enter profession of occupation" runat="server">
                    <asp:RequiredFieldValidator ID="rfv_txt_occupation" ValidationGroup="FormValidation_01"
                        ControlToValidate="txt_occupation" runat="server" ErrorMessage="*Required" CssClass="error_control"
                        Display="Dynamic" />
                </div>
            </div>
            <div class="form-group row">
                <label for="inputPassword" class="col-sm-3 col-form-label">
                    Address of the property to be Insured</label>
                <div class="col-sm-9">
                    <input type="text" class="form-control marginga_top" id="r_address_1" aria-describedby="risk_addressHelp" maxlength="30"
                        placeholder="Risk address 1" runat="server">
                    <asp:RequiredFieldValidator ID="rfv_Add1" ValidationGroup="FormValidation_01" ControlToValidate="r_address_1" 
                        runat="server" ErrorMessage="*Required" CssClass="error_control" Display="Dynamic" />
                    <input type="text" class="form-control marginga_top" id="r_address_2" aria-describedby="risk_addressHelp" maxlength="30"
                        placeholder="Risk address 2" runat="server">
                    <asp:RequiredFieldValidator ID="rfv_Add2" ValidationGroup="FormValidation_01" ControlToValidate="r_address_2"
                        runat="server" ErrorMessage="*Required" CssClass="error_control" Display="Dynamic" />
                    <input type="text" class="form-control marginga_top" id="r_address_3" aria-describedby="risk_addressHelp" maxlength="30"
                        placeholder="Risk address 3" runat="server">
                    <asp:RequiredFieldValidator ID="rfv_Add3" ValidationGroup="FormValidation_01" ControlToValidate="r_address_3"
                        runat="server" ErrorMessage="*Required" CssClass="error_control" Display="Dynamic" />
                    <input type="text" class="form-control marginga_top" id="r_address_4" aria-describedby="risk_addressHelp" maxlength="30"
                        placeholder="Risk address 4" runat="server">
                </div>
            </div>

            <div class="form-group">
                <label for="label assignee">
                    Whether Bank, Government institution or other interest required?</label>
                <input type="text" class="form-control" id="txt_assignees" aria-describedby="assigneesHelp" maxlength="120"
                        placeholder="Enter assignees" runat="server">
                    <%--<asp:RequiredFieldValidator ID="rfv_Assignee" ValidationGroup="FormValidation_01"
                        ControlToValidate="txt_assignees" runat="server" ErrorMessage="*Required" CssClass="error_control"
                        Display="Dynamic" />--%>
            </div>       
            <div class="form-group">
                <label for="exampleInputEmail1">
                    Have you ever sustained loss from any of the perils to which the insurance is to
                    apply? If "Yes" please give particulars.
                </label>
                <asp:CheckBoxList ID="chk_perils" runat="server" Width="30%" CssClass="checkboxList"
                    class="form-control">
                    <asp:ListItem Text="Yes" Value="Y" />
                    <asp:ListItem Text="No" Value="N" />
                </asp:CheckBoxList>
                <asp:CustomValidator ID="Rfv_cusValidatorPerils" runat="server" ErrorMessage="*Required"
                    ClientValidationFunction="ValidateCheckBoxchk_Perils" ValidationGroup="FormValidation_01"
                    CssClass="error_control" Display="Dynamic"></asp:CustomValidator>
            </div>


            <div class="form-group">
                <label for="Inputdeclinned">
                    Has any insurance company declinned to insure or renew policy or demanded and increased.</label>
                <asp:CheckBoxList ID="chk_declinned" runat="server" Width="30%" CssClass="checkboxList"
                    class="form-control">
                    <asp:ListItem Text="Yes" Value="Y" />
                    <asp:ListItem Text="No" Value="N" />
                </asp:CheckBoxList>
                <asp:CustomValidator ID="Rfv_cusValidatorDeclinned" runat="server" ErrorMessage="*Required"
                    ClientValidationFunction="ValidateCheckBoxchk_Declinned" ValidationGroup="FormValidation_01"
                    CssClass="error_control" Display="Dynamic"></asp:CustomValidator>
            </div>
            <div class="form-group">
                <label for="exampleInputEmail1">
                    Limitation of Liability Required</label>
                   <asp:HyperLink id="hyperlink1" NavigateUrl="#" Text="[Vieiw Plan]" runat="server"/> 
                <div style="margin-top: 10px;">
                    <asp:CheckBoxList ID="chkListPlan" runat="server" Width="70%" CssClass="checkboxList Table_tdplan"
                        class="form-control Table_tdplan" RepeatDirection="Horizontal" RepeatLayout="Table">
                        <asp:ListItem Text="Plan 01" Value="1" />
                        <asp:ListItem Text="Plan 02" Value="2" />
                        <asp:ListItem Text="Plan 03" Value="3" />
                        <asp:ListItem Text="Plan 04" Value="4" />
                        <asp:ListItem Text="Plan 05" Value="5" />
                    </asp:CheckBoxList>
                </div>
                <asp:CustomValidator ID="rfv_ValidatorPlan" runat="server" ErrorMessage="*Required"
                    ClientValidationFunction="ValidateCheckBoxchk_Plan" ValidationGroup="FormValidation_01"
                    CssClass="error_control Table_tdplan_ErPos" Display="Dynamic"></asp:CustomValidator>
            </div>
            <div class="form-group">
                <div class="form-inline">
                    <label for="lblPeriod">
                        Period of Insurance (One year from)</label>
                    <input type="text" class="form-control" id="txtPeriod" placeholder="" style="width: 28%;
                        margin-left: 20px;" runat="server" />
                    <asp:RequiredFieldValidator ID="rfv_period" ValidationGroup="FormValidation_01" ControlToValidate="txtPeriod"
                        runat="server" ErrorMessage="*Required" CssClass="error_control" Display="Dynamic" />
                    <input type="text" class="form-control" id="txtPeriod_to" placeholder="" style="width: 28%;
                        margin-left: 20px;" runat="server" />
                    <asp:RequiredFieldValidator ID="rfv_period_to" ValidationGroup="FormValidation_01"
                        ControlToValidate="txtPeriod_to" runat="server" ErrorMessage="*Required" CssClass="error_control"
                        Display="Dynamic" />
                </div>
            </div>
            <br />
            <div class="form-group row">
                <label for="inputPassword" class="col-sm-7 col-form-label">
                    If you have any Sri Lanka Insurance Corporation General (Ltd) Agent- Please Enter Code</label>
                <div class="form-inline col-sm-5">
                    <input type="text" class="form-control" id="txt_agent_Code" placeholder="" style="width: 30%;"
                        runat="server" onkeyup="return EnableDisableAgtValidation(this)" onkeypress="return isNumber(event)"
                        maxlength="6" />
                    <%--<asp:RequiredFieldValidator ID="rfv_agtVal" ValidationGroup="FormValidation_01" ControlToValidate="txt_agent_Code" runat="server" ErrorMessage="*Required" CssClass="error_control" Display="Dynamic" />--%>
                    &nbsp;
                    <asp:Button ID="btn_agtValidate" runat="server" Text="Verify" class="btn btn-primary btn-sm"
                        OnClick="btnVerify_Click" />
                </div>
            </div>
            <div class="form-group row" style="padding-left: 7px;">
                <div id="hpl_message" runat="server" style="padding: 5px; color: #e57373; font-weight: 600;">
                </div>
            </div>
            <br />
            <div class="form-group" style="border-radius: 5px 5px 5px 5px; border: 1px solid #212121;
                padding: 5px;">
                <p style="text-align: justify">
                    I/We hereby declare and warrant the above statements are true and complete. I/We
                    desire to effect an insurance with the SRI LANKA INSURANCE CORPORATION GENERAL (LTD). I/We
                    agree that this proposal and declaration shall be the basis of the contract between
                    me/us and the Company. I/We agree to accept a policy, subject to the condition prescribed
                    by the Company and to render at the end of each period of insurance a statement
                    fill the form required by the Company of all money conveyed and to pay premium on
                    the amount in excess of the amount estimated above.
                </p>
            </div>
            <br />
            <div class="row justify-content-md-center">
                <div class="col-md-8" style="float: left; border: 0px solid #212121;">
                    <div class="form-group">
                        <asp:CheckBox ID="chk_signature" runat="server" Text="Signature" CssClass="checkboxList Table_tdplan"
                            class="form-control Table_tdplan" />
                        <asp:CustomValidator ID="rfv_Signature" runat="server" ErrorMessage="*Required" ClientValidationFunction="ValidateChkBoxSignature"
                            ValidationGroup="FormValidation_01" CssClass="error_control Table_tdplan_ErPos"
                            Display="Dynamic"></asp:CustomValidator>
                    </div>
                </div>
                <div class="col-md-4" style="float: left; border: 0px solid #212121;">
                    <div class="form-group" style="text-align: center">
                        <p>
                            Date Time :
                            <%= DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") %></p>
                    </div>
                </div>
            </div>
            <div style="text-align: right;">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="FormValidation_01" OnClick="btnSubmit_Click" class="btn btn-primary btn-sm" PostBackUrl="~/General/Authorized/Products/HPL_2023_PurchaseConfirm.aspx" />
                <asp:HiddenField ID="btnShow" runat="server" />
                <asp:HiddenField ID="hid_rejection" runat="server" />
            </div>
        </div>
    </div>
 

        <!-- ModalPopupExtender -->
        <ajaxToolkit:modalpopupextender id="mp1" runat="server" popupcontrolid="Panel1" targetcontrolid="btnShow" BehaviorID="sust_mpe"
            cancelcontrolid="btnClose" backgroundcssclass="modalBackground">
        </ajaxToolkit:modalpopupextender>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
            <div style="text-align: left; font-weight: 500">
                Please confirm again whether you have sustained a loss or not? If "Yes" write down
                the reson please
            </div>
            <div style="margin-top: 5px">
                <asp:TextBox ID="txt_PerilsReson" runat="server" TextMode="MultiLine" Height="100px"
                    MaxLength="400" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="Rfv_PerilsReson" ValidationGroup="FormValidation_02"
                    ControlToValidate="txt_PerilsReson" runat="server" ErrorMessage="*Required" CssClass="error_control"
                    Display="Dynamic" Enabled="false" />
            </div>
            <div class="form-inline" style="text-align:right; margin-top: 5px">
             <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                        <asp:Button ID="btn_Sent" runat="server" Text="Send" OnClick="btnSend_Click"  ValidationGroup="FormValidation_02"/>
                  <%--  </ContentTemplate>
                </asp:UpdatePanel>--%>
                &nbsp;
                <asp:Button ID="btnClose" runat="server" Text="Close" OnClientClick="DissablePerilsReson()" />
            </div>
        </asp:Panel>
        <!-- ModalPopupExtender -->



         <!-- ModalPopupExtender -->
        <ajaxToolkit:modalpopupextender id="mp2" runat="server" popupcontrolid="Panel2" targetcontrolid="hid_rejection" BehaviorID="sust_clsrej"
            cancelcontrolid="btn_clsrej" backgroundcssclass="modalBackground">
        </ajaxToolkit:modalpopupextender>

        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" Style="display: none">
            <div style="text-align: left; font-weight: 500">
                "Thank you for reaching Sri Lanka Insurance Corporation General (Ltd). Our representative will contact you with in 03 working days to obtain more information on this request."
            </div>
            
            <div class="form-inline" style="text-align: left; margin-top: 5px">
                <asp:Button ID="btn_clsrej" runat="server" Text="Close"  />
            </div>
        </asp:Panel>
        <!-- ModalPopupExtender -->


          <!-- ModalPopupExtender -->
        <ajaxToolkit:modalpopupextender id="me3" runat="server" popupcontrolid="Panel3" targetcontrolid="hyperlink1" 
            cancelcontrolid="btn_me3_close" backgroundcssclass="modalBackground">
        </ajaxToolkit:modalpopupextender>

        <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" align="center" Style="display: none">
            <div style="text-align: left; font-weight: 500">
                <div>
                    <table class="nav-justified Table_td">
                        <tr style="background-color: #80DEEA; font-weight: bold">
                            <td class="Table_desc">
                                Limits of Liability
                            </td>
                            <td class="Table_td">
                                Plan 01 (Rs.)
                            </td>
                            <td class="Table_td">
                                Plan 02 (Rs.)
                            </td>
                            <td class="Table_td ">
                                Plan 03 (Rs.)
                            </td>
                            <td class="Table_td">
                                Plan 04 (Rs.)
                            </td>
                            <td class="Table_td">
                                Plan 05 (Rs.)
                            </td>
                        </tr>
                        <tr style="background-color: #B2EBF2">
                            <td class="Table_desc">
                                Building, permanent fixtures and fittings
                            </td>
                            <td class="Table_td">
                                0.75Mn
                            </td>
                            <td class="Table_td">
                                1.5Mn
                            </td>
                            <td class="Table_td">
                                2.25Mn
                            </td>
                            <td class="Table_td">
                                3Mn
                            </td>
                            <td class="Table_td">
                                3.75Mn
                            </td>
                        </tr>
                        <tr style="background-color: #80DEEA">
                            <td class="Table_desc">
                                Contents excluding personal effects such as jewellery, money, professional equipment
                            </td>
                            <td class="Table_td">
                                0.25Mn
                            </td>
                            <td class="Table_td">
                                0.5Mn
                            </td>
                            <td class="Table_td">
                                0.75Mn
                            </td>
                            <td class="Table_td">
                                1Mn
                            </td>
                            <td class="Table_td">
                                1.25Mn
                            </td>
                        </tr>
                        <tr style="background-color: #B2EBF2">
                            <td class="Table_desc">
                                Sub Limit - Removal of debris
                            </td>
                            <td class="Table_td">
                                25,000
                            </td>
                            <td class="Table_td">
                                25,000
                            </td>
                            <td class="Table_td">
                                25,000
                            </td>
                            <td class="Table_td">
                                25,000
                            </td>
                            <td class="Table_td">
                                25,000
                            </td>
                        </tr>
                        <tr style="background-color: #80DEEA">
                            <td class="Table_desc">
                                Sub Limit - Professional fee for reinstating the building
                            </td>
                            <td class="Table_td">
                                25,000
                            </td>
                            <td class="Table_td">
                                25,000
                            </td>
                            <td class="Table_td">
                                25,000
                            </td>
                            <td class="Table_td">
                                25,000
                            </td>
                            <td class="Table_td">
                                25,000
                            </td>
                        </tr>
                        <tr style="background-color: #B2EBF2">
                            <td class="Table_desc">
                                Sub Limit - Burglary
                            </td>
                            <td class="Table_td">
                                25,000
                            </td>
                            <td class="Table_td">
                                25,000
                            </td>
                            <td class="Table_td">
                                50,000
                            </td>
                            <td class="Table_td">
                                75,000
                            </td>
                            <td class="Table_td">
                                100,000
                            </td>
                        </tr>
                        <tr style="background-color: #80DEEA">
                            <td class="Table_desc">
                                Sub Limit - Electrical Damage cover
                            </td>
                            <td class="Table_td">
                                25,000
                            </td>
                            <td class="Table_td">
                                25,000
                            </td>
                            <td class="Table_td">
                                50,000
                            </td>
                            <td class="Table_td">
                                75,000
                            </td>
                            <td class="Table_td">
                                100,000
                            </td>
                        </tr>
                        <tr style="background-color: #80DEEA">
                            <td class="Table_desc">
                                <b>Limit</b>
                            </td>
                            <td class="Table_td">
                                <b>1Mn</b>
                            </td>
                            <td class="Table_td">
                                <b>2Mn</b>
                            </td>
                            <td class="Table_td">
                                <b>3Mn</b>
                            </td>
                            <td class="Table_td">
                                <b>4Mn</b>
                            </td>
                            <td class="Table_td">
                                <b>5Mn</b>
                            </td>
                        </tr>
                        <tr style="background-color: #80DEEA">
                            <td class="Table_desc">
                                <b>Annual premium with taxes</b>
                            </td>
                            <td class="Table_td">
                                <b>1,900/=</b>
                            </td>
                            <td class="Table_td">
                                <b>2,500/=</b>
                            </td>
                            <td class="Table_td">
                                <b>3,400/=</b>
                            </td>
                            <td class="Table_td">
                                <b>4,300/=</b>
                            </td>
                            <td class="Table_td">
                                <b>5,300/=</b>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="form-inline" style="text-align: right; margin-top: 5px">
                <asp:Button ID="btn_me3_close" runat="server" Text="Close"  />
            </div>
        </asp:Panel>
        <!-- ModalPopupExtender -->

    <br />
    <br />


    
</asp:Content>

