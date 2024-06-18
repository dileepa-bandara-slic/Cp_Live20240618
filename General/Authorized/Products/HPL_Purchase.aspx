<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true"
    CodeFile="HPL_Purchase.aspx.cs" Inherits="General_Authorized_Products_HPL_Purchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<%--    <script src="/js/jquery-3.5.1.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>--%>
    <style>
        @media (max-width:479px)
        {
            .navbar-fixed-top + .main-container
            {
                padding-top: 50px;
            }
        }
          
            
    </style>
     <!-- Changes 2017/02/09 -->
     <style>
         .container
         {

        background-color:#CCC;             
          }
        
        
        
        
     </style>
     <!-- End of 2017/02/09 -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container" id="main-container"  style="min-height:600px">
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
                        <li class="breadcrumb-item active">Home protect lite purchase</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
        <%--    </br>--%>
            <asp:UpdatePanel ID="Ggs14451" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                          <center>
                                <h3>Home Protect Lite</h3>
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
                                            <%--<td>
                                                <b>Name in Full</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="Lit_cus_title" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                                &nbsp;<asp:Label ID="Lit_full_name" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                            </td>--%>

                                              <div class="row">
                                                <div class="col-sm-4">
                                                   <b>Name in Full</b></div>
                                            
                                                <div class="col-sm-6">
                                                   <asp:Label ID="Lit_cus_title" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                                &nbsp;<asp:Label ID="Lit_full_name" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                                
                                                 </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                        </tr>
                                      <br />
                                        <tr>
                                      <%--      <td>
                                                <b>NIC</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="Lit_cus_nic" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                            </td>--%>

                                            
                                              <div class="row">
                                                <div class="col-sm-4">
                                                  <b>NIC</b>
                                                 </div>
                                            
                                                <div class="col-sm-6">
                                                   <asp:Label ID="Lit_cus_nic" runat="server" data-toggle="tooltip" data-placement="bottom"   ReadOnly="true"
                                                    title="To change these values, please change profile information."></asp:Label>
                                                  
                                                 </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                        </tr>
                                            <br />
                                        <tr>
                                           <%-- <td>
                                                <b>Address</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="Lit_address1" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                                &nbsp;<asp:Label ID="Lit_address2" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                                &nbsp;<asp:Label ID="Lit_address3" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                                &nbsp;<asp:Label ID="Lit_address4" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                            </td>--%>

                                            
                                              <div class="row">
                                                <div class="col-sm-4">
                                                  <b>Address</b>
                                                 </div>
                                            
                                                <div class="col-sm-6">
                                                    <asp:Label ID="Lit_address1" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                                &nbsp;<asp:Label ID="Lit_address2" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                                &nbsp;<asp:Label ID="Lit_address3" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                                &nbsp;<asp:Label ID="Lit_address4" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                                
                                                 </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                        </tr>
                                          <br />
                                        <tr>
                                          <%--  <td>
                                                <b>Residential Phone</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="Lit_homePhone" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                            </td>--%>

                                                 <div class="row">
                                                <div class="col-sm-4">
                                                  <b>Residential Phone</b>
                                                 </div>
                                            
                                                <div class="col-sm-6">
                                                    <asp:Label ID="Lit_homePhone" Enabled="false" runat="server" data-toggle="tooltip" data-placement="bottom"  
                                                    title="To change these values, please change profile information."></asp:Label>
                                                  
                                                 </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                        </tr>
                                           <br />
                                        <tr>
                                            <%--<td>
                                               <b> Mobile Phone</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="Lit_mobilePhone" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                            </td>--%>

                                           <div class="row">
                                                <div class="col-sm-4">
                                                  <b> Mobile Phone</b>
                                                 </div>
                                            
                                                <div class="col-sm-6">
                                                    <asp:Label ID="Lit_mobilePhone" Enabled="false" runat="server" data-toggle="tooltip" data-placement="bottom"  
                                                    title="To change these values, please change profile information."></asp:Label>
                                                  
                                                 </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                        </tr>
                                          <br />
                                        <tr>
                                     <%--   <tr>
                                            <td>
                                               <b> Office Phone</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="Lit_officePhone" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                            </td>
                                        </tr>--%>

                                        <div class="row">
                                                <div class="col-sm-4">
                                                  <b> Office Phone</b>
                                                 </div>
                                            
                                                <div class="col-sm-6">
                                                     <asp:Label ID="Lit_officePhone" runat="server" data-toggle="tooltip" data-placement="bottom"  
                                                    title="To change these values, please change profile information."></asp:Label>
                                               
                                                 </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                        </tr> 
                                          <br />  
                                        <tr>

                                         <%--   <td>
                                                <b>Email</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="Lit_email" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                    title="To change these values, please change profile information."></asp:Label>
                                            </td>--%>

                                                  <div class="row">
                                                <div class="col-sm-4">
                                                  <b> Email</b>
                                                 </div>
                                            
                                                <div class="col-sm-6">
                                                      <asp:Label ID="Lit_email" runat="server" data-toggle="tooltip" data-placement="bottom" 
                                                    title="To change these values, please change profile information."></asp:Label>
                                                    
                                                 </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                        </tr>
                                           <br />

                                        <%--<tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>--%>
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
                              
                                        <tr>
                                    <%--        <td>
                                                <b>Profession</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_profession" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                            </td>--%>

                                            <div class="row">
                                                <div class="col-sm-4">
                                                  <b>Profession</b></div>
                                            
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txt_profession" runat="server" class="form-control" MaxLength="50"></asp:TextBox><br></div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                        </tr>
                                        <tr>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                  <b> Risk Location </b></div>
                                                <div class="col-sm-2">
                                                    Address Line 1</div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txt_rskLoc_address1" runat="server" class="form-control" MaxLength="30"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_rskLoc_address1"
                                                        CssClass="errorMsg_1" ErrorMessage="* Required"></asp:RequiredFieldValidator></div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                        </tr>
                                        <tr>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                </div>
                                                <div class="col-sm-2">
                                                    Address Line 2</div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txt_rskLoc_address2" runat="server" class="form-control" MaxLength="30"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_rskLoc_address2"
                                                        CssClass="errorMsg_1" ErrorMessage="* Required"></asp:RequiredFieldValidator></div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                      
                                        </tr>
                                        <tr>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                </div>
                                                <div class="col-sm-2">
                                                    Address Line 3</div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txt_rskLoc_address3" runat="server" class="form-control" MaxLength="30"></asp:TextBox>
                                                    <br></div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                        
                                        </tr>
                                        <tr>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                </div>
                                                <div class="col-sm-2">
                                                    Address Line 4</div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txt_rskLoc_address4" runat="server" class="form-control" MaxLength="30"></asp:TextBox>
                                                    <br></div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                           
                                        </tr>
                                        <tr>
                                           <%-- <td>
                                                <b>Assignee</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_asgnee" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_asgnee"
                                                    CssClass="errorMsg_1" ErrorMessage="* Required" Enabled="False"></asp:RequiredFieldValidator>
                                            </td>--%>

                                             <div class="row">
                                                <div class="col-sm-4">
                                              <b>Assignee</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                                     <asp:TextBox ID="txt_asgnee" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_asgnee"
                                                    CssClass="errorMsg_1" ErrorMessage="* Required" Enabled="False"></asp:RequiredFieldValidator></div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                        </tr>
                                       
                                        <tr>
                                            <%--<td>
                                                <b>Selected Plan</b>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rdo_Plan_1" runat="server" AutoPostBack="True" Checked="True"
                                                    GroupName="plan" OnCheckedChanged="rdo_Reject_Y_CheckedChanged" Text="Plan 01" /><span
                                                        style="font-size:x-small">(Sum Assured : Rs. 1,000,000.00)</span><br />
                                                <asp:RadioButton ID="rdo_plan_2" runat="server" AutoPostBack="True" GroupName="plan"
                                                    OnCheckedChanged="rdo_Reject_N_CheckedChanged" Text="Plan 02" /><span style="font-size:x-small">(Sum
                                                        Assured : Rs. 2,000,000.00)</span>
                                            </td>--%>

                                            
                                             <div class="row">
                                                <div class="col-sm-4">
                                               <b>Selected Plan</b>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                                       <asp:RadioButton ID="rdo_Plan_1" runat="server" AutoPostBack="True" Checked="True"  
                                                    GroupName="plan" OnCheckedChanged="rdo_Reject_Y_CheckedChanged" Text="Plan 01" /><span
                                                        style="font-size:x-small">(Sum Assured : Rs. 1,000,000.00)</span><br />
                                                <asp:RadioButton ID="rdo_plan_2" runat="server" AutoPostBack="True" GroupName="plan"
                                                    OnCheckedChanged="rdo_Reject_N_CheckedChanged" Text="Plan 02" /><span style="font-size:x-small">(Sum
                                                        Assured : Rs. 2,000,000.00)</span>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                            <br />

                                        </tr>
                                      
                                        <tr>
                                           <%-- <td>
                                                Have you ever sustained loss from any of the perils to which the insurance is apply?
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rdo_Loss_Y" runat="server" GroupName="lossGrp" Text="Yes" AutoPostBack="True"
                                                    OnCheckedChanged="rdo_Loss_Y_CheckedChanged" />
                                                <asp:RadioButton ID="rdo_Loss_N" runat="server" Checked="True" GroupName="lossGrp"
                                                    Text="No" AutoPostBack="True" OnCheckedChanged="rdo_Loss_N_CheckedChanged" />
                                            </td>--%>
                                              <div class="row">
                                                <div class="col-sm-4">
                                                Have you ever sustained loss from any of the perils to which the insurance is apply?
                                                </div>
                                                
                                                <div class="col-sm-6">
                                                    <asp:RadioButton ID="rdo_Loss_Y" runat="server" GroupName="lossGrp" Text="Yes" AutoPostBack="True"
                                                    OnCheckedChanged="rdo_Loss_Y_CheckedChanged" />
                                                <asp:RadioButton ID="rdo_Loss_N" runat="server" Checked="false" GroupName="lossGrp"
                                                    Text="No" AutoPostBack="True" OnCheckedChanged="rdo_Loss_N_CheckedChanged" />

                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="*Required" Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" ForeColor="Red" OnServerValidate="Check_select1" ClientValidationFunction="validateRdo2"/>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>

                                        </tr>
                                        <tr>
                                           <%-- <td>
                                                <asp:Literal ID="Lit_remarksdmg" runat="server" Text="Please Specify : " Visible="False"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_remarksdmg" runat="server" class="form-control" MaxLength="200"
                                                    TextMode="MultiLine" Visible="False"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_remarks"
                                                    CssClass="errorMsg_1" Enabled="False" ErrorMessage="* Required" Font-Bold="False"></asp:RequiredFieldValidator>
                                            </td>--%>

                                               <div class="row">
                                                <div class="col-sm-4">
                                               <%--<asp:Literal ID="Lit_remarksdmg" runat="server" Text="Please Specify : " Visible="False"></asp:Literal>--%>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                                <%--<asp:TextBox ID="txt_remarksdmg" runat="server" class="form-control" MaxLength="200"
                                                    TextMode="MultiLine" Visible="False"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_remarks"
                                                    CssClass="errorMsg_1" Enabled="False" ErrorMessage="* Required" Font-Bold="False"></asp:RequiredFieldValidator>--%>
                                                    <asp:Label ID="lblErr1" ForeColor="Red" Font-Bold="true" Font-Names="Calibri" Font-Size="10" runat="server"></asp:Label>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                        </tr>
                                    <tr>
                                    <div class="row">
                                                <div class="col-sm-12">&nbsp;</div></div>
                                    </tr>
                                        <tr>
                                           <%-- <td>
                                                Has any insurance company declined to insure or renew policy or demand an increased
                                                rate for renewal ?
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rdo_Reject_Y" runat="server" AutoPostBack="True" GroupName="rejGrp"
                                                    OnCheckedChanged="rdo_Reject_Y_CheckedChanged" Text="Yes" />
                                                <asp:RadioButton ID="rdo_Reject_N" runat="server" AutoPostBack="True" Checked="True"
                                                    GroupName="rejGrp" OnCheckedChanged="rdo_Reject_N_CheckedChanged" Text="No" />
                                                <br />
                                            </td>--%>

                                              <div class="row">
                                                <div class="col-sm-4">
                                              Has any insurance company declined to insure or renew policy or demand an increased
                                                rate for renewal ?
                                                </div>
                                                
                                                <div class="col-sm-6">
                                                <asp:RadioButton ID="rdo_Reject_Y" runat="server" AutoPostBack="True" GroupName="rejGrp"
                                                    OnCheckedChanged="rdo_Reject_Y_CheckedChanged" Text="Yes" />
                                                <asp:RadioButton ID="rdo_Reject_N" runat="server" AutoPostBack="True" Checked="false"
                                                    GroupName="rejGrp" OnCheckedChanged="rdo_Reject_N_CheckedChanged" Text="No" /> 
                                                    <asp:CustomValidator ID="CVlblErr2" runat="server" ErrorMessage="*Required" Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" ForeColor="Red" OnServerValidate="Check_select1" ClientValidationFunction="validateRdo1"/>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                        </tr>
                                        <tr>
                                    

                                        <tr>
                                           <%-- <td>
                                                <asp:Literal ID="Lit_remarks" runat="server" Text="Please Specify : " Visible="False"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_remarks" runat="server" TextMode="MultiLine" Visible="False"
                                                    class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_remarks"
                                                    CssClass="errorMsg_1" Enabled="False" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                            </td>--%>
                                              <div class="row">
                                                <div class="col-sm-4">
                                               <%--<asp:Literal ID="Lit_remarks" runat="server" Text="Please Specify : " Visible="False"></asp:Literal>--%>
                                                </div>
                                                
                                                <div class="col-sm-6">
                                               <%-- <asp:TextBox ID="txt_remarks" runat="server" TextMode="MultiLine" Visible="False"
                                                    class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_remarks"
                                                    CssClass="errorMsg_1" Enabled="False" ErrorMessage="* Required"></asp:RequiredFieldValidator>--%>

                                                    <asp:Label ID="lblErr2" ForeColor="Red" Font-Bold="true" Font-Names="Calibri" Font-Size="10" runat="server"></asp:Label>
                                                    
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>


                                        </tr>
                                    <div class="row">
                                                <div class="col-sm-12">&nbsp;</div></div>
                                    </tr>
                                        <tr>
                                                <div class="row">
                                                <div class="col-sm-4">
                                                <b>Commencement Date</b>
                                                </div>
                                                
                                                <div class="col-sm-6"> <asp:TextBox ID="Txt_comDate" runat="server" MaxLength="10" class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Txt_comDate"
                                                    CssClass="errorMsg_1" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                    ControlToValidate="Txt_comDate" ErrorMessage="* yyyy/mm/dd" Font-Bold="False"
                                                    ValidationExpression="[0-9y]{4}/[0-9m]{1,2}/[0-9d]{1,2}" CssClass="errorMsg_1"></asp:RegularExpressionValidator>
                                                    <br>
                                                <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="Txt_comDate"
                                                    ErrorMessage="RangeValidator" Font-Names="Calibri" OnInit="RangeValidator6_Init"
                                                    Type="Date" CssClass="errorMsg_1">*Past Dates not Allowed</asp:RangeValidator>
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                        </tr>
                                       
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                               
                                            </td>

                                            <div class="row">
                                                <div class="col-sm-4">
                                                </div>
                                                
                                                <div class="col-sm-6">
                                                
                                                <asp:Button ID="Button1" Enabled="false" runat="server" CssClass="btn btn-primary btn-xs" OnClick="Button1_Click"
                                                    PostBackUrl="~/General/Authorized/Products/HPL_PurchaseConf.aspx"  Text="Submit" /><br /><br />
                                                    
                                                    </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="col-xs-1">
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
                year = today.getFullYear() - 0;
                var backdate = new Date(year, month, date)
                var backdate1 = new Date(year, month, date)

                $("input[id$='Txt_comDate']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, defaultDate: backdate, minDate: 0 });


            });

            

   
    $(document).ready(function () {

        $('[data-toggle="tooltip"]').tooltip();

        //alert("Hi3");
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        $("#effect").hide();

        $("input[id$='txtPassword']").focusin(function () {
            $("#effect").show();

        }).focusout(function () {
            $("#effect").hide();
        });


        today = new Date();
        var month, day, year;
        year = today.getFullYear();
        month = today.getMonth();
        date = today.getDate();
        year = today.getFullYear() - 0;
        var backdate = new Date(year, month, date)
        var backdate1 = new Date(year, month, date)


        function EndRequestHandler(sender, args) {
            $("input[id$='Txt_comDate']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, defaultDate: backdate, minDate: 0 });
            $('[data-toggle="tooltip"]').tooltip();

        }

    });



    function validateRdo1(sender, args) {

        var y=document.getElementById("<%=rdo_Reject_Y.ClientID%>");
        var n=document.getElementById("<%=rdo_Reject_N.ClientID%>");
        if (y.checked == false && n.checked == false) {
            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }
    }

    function validateRdo2(sender, args) {

        var y = document.getElementById("<%=rdo_Loss_Y.ClientID%>");
        var n = document.getElementById("<%=rdo_Loss_N.ClientID%>");
        if (y.checked == false && n.checked == false) {
            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }
    } 

         </script>
         
</asp:Content>
