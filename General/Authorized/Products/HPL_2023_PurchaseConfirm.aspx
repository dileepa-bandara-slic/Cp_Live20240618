<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="HPL_2023_PurchaseConfirm.aspx.cs" Inherits="General_Authorized_Products_HPL_2023_PurchaseConfirm" %>
<%@ PreviousPageType VirtualPath="~/General/Authorized/Products/HPL_2023_Purchase.aspx" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">  
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
        
        .marginga_top{
            margin-top:3px;
        }      
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
            }
        });
    </script>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:HiddenField ID="HID_ChPlpolTyp" runat="server" />
        <asp:HiddenField ID="HID_ChPltitle" runat="server" />  
        <asp:HiddenField ID="HID_ChPlfullNam" runat="server" />       
        <asp:HiddenField ID="HID_ChPladrs1" runat="server" />
        <asp:HiddenField ID="HID_ChPladrs2" runat="server" />
        <asp:HiddenField ID="HID_ChPladrs3" runat="server" />
        <asp:HiddenField ID="HID_ChPladrs4" runat="server" />
        <asp:HiddenField ID="HID_ChPlmobNo" runat="server" />
        <asp:HiddenField ID="HID_ChPlhomeNo" runat="server" />
        <asp:HiddenField ID="HID_ChPlofcNo" runat="server" />
        <asp:HiddenField ID="HID_ChPlemail" runat="server" />
        <asp:HiddenField ID="HID_ChPlnic" runat="server" />        
        <asp:HiddenField ID="HID_ChPlassignee" runat="server" />
        <asp:HiddenField ID="HID_ChPlsustained" runat="server" />
        <asp:HiddenField ID="HID_ChPldeclinned" runat="server" />
        <asp:HiddenField ID="HID_ChPldmgBefore" runat="server" />
        <asp:HiddenField ID="HID_ChPlrejBefore" runat="server" />
        <asp:HiddenField ID="HID_ChPlrejResn" runat="server" />
        <asp:HiddenField ID="HID_ChPlplan" runat="server" />
        <asp:HiddenField ID="HID_ChPlstatus" runat="server" />
        <asp:HiddenField ID="HID_ChPlusername" runat="server" />
        <asp:HiddenField ID="HID_ChPlprodId" runat="server" />
        <asp:HiddenField ID="HID_ChPlprof" runat="server" />
        <asp:HiddenField ID="HID_ChPlpayMethod" runat="server" />
        <asp:HiddenField ID="HID_Comm_Date" runat="server" />
        <asp:HiddenField ID="HID_PolExp_Date" runat="server" />
        <asp:HiddenField ID="HID_Agt_Code" runat="server" />
    <div class="row">
        <div class="col-xs-10">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="/">Home</a></li>
                <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                <li class="breadcrumb-item"><a href="/General/Authorized/General_Products.aspx">Purchase
                    online</a></li>
                <li class="breadcrumb-item"><a href="/General/Authorized/Products/HPL_2023_Purchase.aspx">
                    Home protect lite - Purchase</a></li>
                <li class="breadcrumb-item active">Home protect lite purchase - Confirmation</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-7 offset-lg-2">
            <div class="form-group">
                <p>
                    <b><h3>Purchased Product Confirmation</h3></b>
                </p>
            </div>
            <div class="form-group">
                <label for="employeeDept">Customer Name</label>
                <input type="text" class="form-control is-invalid" ID="txt_custonName" runat="server" aria-describedby="txt_custonName_help" disabled>
                <small id="txt_custonName_help" class="form-text text-muted">Customer Name</small>
            </div>
            <div class="form-group">
                <label for="exampleInputEmail1">
                    Risk Location</label>
                <input type="text" class="form-control marginga_top" id="cr_address_1" aria-describedby="risk_addressHelp"
                    placeholder="Confirmed Risk address 1" runat="server" disabled>
                <asp:RequiredFieldValidator ID="rfv_confRl_1" ValidationGroup="FormValidation_rcl01"
                    ControlToValidate="cr_address_1" runat="server" ErrorMessage="*Required" CssClass="error_control"
                    Display="Dynamic" />
                <input type="text" class="form-control marginga_top" id="cr_address_2" aria-describedby="risk_addressHelp"
                    placeholder="Confirmed Risk address 2" runat="server" disabled>
                <asp:RequiredFieldValidator ID="rfv_confRl_2" ValidationGroup="FormValidation_rcl01"
                    ControlToValidate="cr_address_2" runat="server" ErrorMessage="*Required" CssClass="error_control"
                    Display="Dynamic" />
                <input type="text" class="form-control marginga_top" id="cr_address_3" aria-describedby="risk_addressHelp"
                    placeholder="Confirmed Risk address 3" runat="server" disabled>
                <asp:RequiredFieldValidator ID="rfv_confRl_3" ValidationGroup="FormValidation_rcl01"
                    ControlToValidate="cr_address_3" runat="server" ErrorMessage="*Required" CssClass="error_control"
                    Display="Dynamic" />
                <input type="text" class="form-control marginga_top" id="cr_address_4" aria-describedby="risk_addressHelp"
                    placeholder="Confirmed Risk address 4" runat="server" disabled>
                
                <div class="form-group" style=" margin-top:15px">
                <div class="form-inline">
                    <label for="lblPeriod">Period of Insurance (One year from)</label>
                    <input type="text" class="form-control" id="cr_txtPeriod" placeholder="" style="width: 20%; margin-left: 20px;" runat="server" disabled/>
                    <input type="text" class="form-control" id="cr_txtPeriod_to" placeholder="" style="width: 20%; margin-left: 20px;" runat="server" disabled/>                  
                </div>
            </div>
            </div>
            <br />
            <div>
                <table class="nav-justified Table_td">
                    <tr style="background-color: #80DEEA; font-weight: bold">
                        <td class="Table_desc">
                            Limits of Liability
                        </td>
                        <td class="Table_td" runat="server" id="PLANDESC">
                        </td>
                        <%--<td class="Table_td">
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
                        </td>--%>
                    </tr>
                    <tr style="background-color: #B2EBF2">
                        <td class="Table_desc">
                            Building, permanent fixtures and fittings
                        </td>
                        <td class="Table_td" runat="server" id="P1C1R1">
                        </td>
                       <%-- <td class="Table_td" runat="server" id="P2C2R1">
                        </td>
                        <td class="Table_td" runat="server" id="P3C3R1">
                        </td>
                        <td class="Table_td" runat="server" id="P4C4R1">
                        </td>
                        <td class="Table_td" runat="server" id="P5C5R1">
                        </td>--%>
                    </tr>
                    <tr style="background-color: #80DEEA">
                        <td class="Table_desc">
                            Contents excluding personal effects such as jewellery, money, professional equipment
                        </td>
                        <td class="Table_td" runat="server" id="P1C1R2">
                        </td>
                        <%--<td class="Table_td" runat="server" id="P2C2R2">
                        </td>
                        <td class="Table_td" runat="server" id="P3C3R2">
                        </td>
                        <td class="Table_td" runat="server" id="P4C4R2">
                        </td>
                        <td class="Table_td" runat="server" id="P5C5R2">
                        </td>--%>
                    </tr>
                    <tr style="background-color: #B2EBF2">
                        <td class="Table_desc">
                            Sub Limit-Removal of debris
                        </td>
                        <td class="Table_td" runat="server" id="P1C1R3">
                        </td>
                       <%-- <td class="Table_td" runat="server" id="P2C2R3">
                        </td>
                        <td class="Table_td" runat="server" id="P3C3R3">
                        </td>
                        <td class="Table_td" runat="server" id="P4C4R3">
                        </td>
                        <td class="Table_td" runat="server" id="P5C5R3">
                        </td>--%>
                    </tr>
                    <tr style="background-color: #80DEEA">
                        <td class="Table_desc">
                             Sub Limit-Professional fee for reinstating the building
                        </td>
                        <td class="Table_td" runat="server" id="P1C1R4">
                        </td>
                        <%--<td class="Table_td" runat="server" id="P2C2R4">
                        </td>
                        <td class="Table_td" runat="server" id="P3C3R4">
                        </td>
                        <td class="Table_td" runat="server" id="P4C4R4">
                        </td>
                        <td class="Table_td" runat="server" id="P5C5R4">
                        </td>--%>
                    </tr>
                    <tr style="background-color: #B2EBF2">
                        <td class="Table_desc">
                             Sub Limit-Burglary
                        </td>
                        <td class="Table_td" runat="server" id="P1C1R5">
                        </td>
                        <%--<td class="Table_td" runat="server" id="P2C2R5">
                        </td>
                        <td class="Table_td" runat="server" id="P3C3R5">
                        </td>
                        <td class="Table_td" runat="server" id="P4C4R5">
                        </td>
                        <td class="Table_td" runat="server" id="P5C5R5">
                        </td>--%>
                    </tr>
                    <tr style="background-color: #80DEEA">
                        <td class="Table_desc">
                             Sub Limit-Electrical damage cover
                        </td>
                        <td class="Table_td" runat="server" id="P1C1R6">
                        </td>
                        <%--<td class="Table_td" runat="server" id="P2C2R6">
                        </td>
                        <td class="Table_td" runat="server" id="P3C3R6">
                        </td>
                        <td class="Table_td" runat="server" id="P4C4R6">
                        </td>
                        <td class="Table_td" runat="server" id="P5C5R6">
                        </td>--%>
                    </tr>
                </table>
            </div>
            <div style="text-align: right; margin-top: 15px;">
                <asp:Button ID="btnConfirmPurchase" runat="server" Text="Buy Policy" class="btn btn-success btn-sm" ValidationGroup="FormValidation_rcl01" OnClick="btnConfirmPurchase_Click"  />
                <asp:Button ID="btnCancel_cd" runat="server" class="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
            </div>

            <br />
            <div class="col-lg-7 offset-lg-2" id="hpl_message" runat="server" style="padding:5px; color:#e57373; font-weight:600;" >           
            </div>
        </div>        
    </div>

<br />
<br />
</asp:Content>

