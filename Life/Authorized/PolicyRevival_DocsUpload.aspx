<%@ Page Title="" Language="C#" MasterPageFile="~/Life.master" AutoEventWireup="true" CodeFile="PolicyRevival_DocsUpload.aspx.cs" Inherits="Life_Authorized_PolicyRevival_DocsUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
        @media (max-width:479px)
        {
            .navbar-fixed-top + .main-container
            {
                padding-top: 40px;
            }
        }
        .test11
        {
            font-size: 100%;
        }
    </style>
    <style>
        .divWaiting
        {
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            position: absolute;
            text-align: center;
            top: 0;
            left: 0;
            height: 1024px;
            width: 100%;
        }
        
        
        .button101
        {
            width: 230px;
            height: 26px;
            background: #428bca;
            border: 0px solid #428bca;
            position: relative;
            color: White;
            padding-left: 10px;
            vertical-align: bottom;
        }
        
        .button101::before
        {
            width: 0;
            height: 0;
            border: 13px solid transparent;
            border-left: 8px solid #428bca;
            content: '';
            position: absolute;
            top: 0px;
            left: 230px;
        }
        .button101::after
        {
            width: 0;
            height: 0;
            border: 13px solid transparent;
            border-left: 8px solid #428bca;
            content: '';
            position: absolute;
            top: 0px;
            left: 230px;
        }
        .arrow-right
        {
            width: 0;
            height: 0;
            border-top: 5px solid transparent;
            border-bottom: 5px solid transparent;
            border-left: 5px solid #393939;
        }
        
        .style1
        {
            height: 45px;
        }
        
        .style2
        {
            height: 161px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="main-container" id="main-container">
        <link href="/css/footable.min.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="/js/footable.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=gvDemands]').footable();
                $('[id*=gvPolicies').footable();
            });
        </script>
        <%--
                <link href="/css/footable.min.css"rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="/js/footable.min.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=]').footable();
            });
        </script>--%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="container">
            <br />
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/Life/Authorized/">Life</a></li>
                        <li class="breadcrumb-item active">Policy Revivals</li>
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
                        <h3>
                            Life Policy Revivals - Documents Upload</h3>
                    </center>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <br />
            
            
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            
                            <div class="col-sm-9">
                                <span></span>
                            </div>
                      
                            <div class="form-group">
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-9">
                                    
                                </div>
                            </div>

                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        
                        <div class="col-xs-1">
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                           
                                <div class="row">
                                    <div class="col-sm-3">
                                        <strong></strong>
                                    </div>
                                    <div class="col-sm-9">
                                    </div>
                                </div>                                
                                <div class="table-responsive">
                                    <table class="table">
                                        <tbody>
                                        <tr>
                                                <td width="35%">
                                                    Policy Number
                                                </td>
                                                <td width="65%">
                                                         <asp:Literal ID="ltrPolNo" runat="server"></asp:Literal>                                        
                                                </td>
                                               
                                            </tr>
                                              <tr>
                                                <td width="35%">
                                                    Policy Holder's Name
                                                </td>
                                                <td width="65%">
                                                <asp:Literal ID="ltrPHStatus" runat="server"></asp:Literal> 
                                                                <asp:Literal ID="ltrPHName" runat="server"></asp:Literal>                                 
                                                </td>
                                               
                                            </tr>
                                              <tr>
                                                <td width="35%">
                                                    Total Due Amount
                                                </td>
                                                <td width="65%">
                                                                <asp:Literal ID="ltrTotalDue" runat="server"></asp:Literal>                                 
                                                </td>
                                               
                                            </tr>
                                              <tr>
                                                <td width="35%">
                                                    Payment Amount (Rs.)
                                                </td>
                                                <td width="65%">
                                                      <asp:Literal ID="ltrPayAmount" runat="server"></asp:Literal>                                           
                                                </td>
                                               
                                            </tr>

                                            <tr>
                                                <td colspan="2" style="text-decoration: underline; font-weight: bold">
                                                    Upload Documents :
                                                </td>
                                            </tr>
                                            <%
                                                if (view_15E_1)
                                                {
                                                    
                                                 %>

                                            <asp:UpdatePanel ID="up1" runat="server" >                
                                                <ContentTemplate>                                            
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_15E_doc_for_1" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_15E_name_1" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_15E_1" class="btn btn-xs btn-primary"  runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_15E_upload_1" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_15E_upload_1_Click"/>  
                                                 <asp:Literal ID="ltr_15E_uploaded_1" runat="server" Visible="False"></asp:Literal> 
                                                 <asp:Label ID="lbl_15E_error_1" runat="server" Visible="true"></asp:Label>                                                
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_15E_upload_1"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%
                                                }
                                                if (view_15E_2)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up2" runat="server" >                
                                                      <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_15E_doc_for_2" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_15E_name_2" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_15E_2" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_15E_upload_2" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_15E_upload_2_Click"/>                                                    
                                                <asp:Literal ID="ltr_15E_uploaded_2" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_15E_error_2" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_15E_upload_2"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%
                                                }
                                                if(view_15E_4)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up3" runat="server" >                
                                                       <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_15E_doc_for_4" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_15E_name_4" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_15E_4" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_15E_upload_4" class="btn btn-xs btn-primary" runat="server" Text="Upload"  OnClick="btn_15E_upload_4_Click"/>                                                   
                                                <asp:Literal ID="ltr_15E_uploaded_4" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_15E_error_4" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_15E_upload_4"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                             <%
                                                }
                                                if(view_15E_5)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up4" runat="server" >                
                                              <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_15E_doc_for_5" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_15E_name_5" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_15E_5" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_15E_upload_5" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_15E_upload_5_Click"/>                                                   
                                                <asp:Literal ID="ltr_15E_uploaded_5" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_15E_error_5" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_15E_upload_5"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                             <%
                                                }
                                                if(view_15E_6)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up5" runat="server" >
                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_15E_doc_for_6" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_15E_name_6" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_15E_6" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_15E_upload_6" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_15E_upload_6_Click" />                                                   
                                                <asp:Literal ID="ltr_15E_uploaded_6" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_15E_error_6" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_15E_upload_6"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                             <%
                                                }
                                                if(view_15E_7)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up6" runat="server" >
                
                                              <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_15E_doc_for_7" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_15E_name_7" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_15E_7" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_15E_upload_7" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_15E_upload_7_Click"/>                                                   
                                                <asp:Literal ID="ltr_15E_uploaded_7" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_15E_error_7" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_15E_upload_7"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                             <%
                                                }
                                                 if (view_15E_8)
                                                 {
                                                     %>
                                            <asp:UpdatePanel ID="up7" runat="server" Visible="False" >                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_15E_doc_for_8" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_15E_name_8" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_15E_8" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_15E_upload_8" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_15E_upload_8_Click"/>                                                   
                                                <asp:Literal ID="ltr_15E_uploaded_8" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_15E_error_8" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_15E_upload_8"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%
                                                 }
                                                 
                                                 %>
                                                 
                                                  <%
                                                if (view_12E_1)
                                                {
                                                    
                                                 %>

                                            <asp:UpdatePanel ID="up19" runat="server" >                
                                                <ContentTemplate>                                            
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_12E_doc_for_1" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_12E_name_1" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_12E_1" class="btn btn-xs btn-primary"  runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_12E_upload_1" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_12E_upload_1_Click" />
                                                 <asp:Literal ID="ltr_12E_uploaded_1" runat="server" Visible="False"></asp:Literal> 
                                                 <asp:Label ID="lbl_12E_error_1" runat="server" Visible="true"></asp:Label>                                                
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_12E_upload_1"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%
                                                }
                                                if (view_12E_2)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up20" runat="server" >                
                                                      <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_12E_doc_for_2" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_12E_name_2" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_12E_2" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_12E_upload_2" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_12E_upload_2_Click"/>                                                    
                                                <asp:Literal ID="ltr_12E_uploaded_2" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_12E_error_2" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_12E_upload_2"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%
                                                }
                                                if(view_12E_3)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up21" runat="server" >                
                                                       <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_12E_doc_for_3" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_12E_name_3" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_12E_3" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_12E_upload_3" class="btn btn-xs btn-primary" runat="server" Text="Upload"  OnClick="btn_12E_upload_3_Click"/>                                                   
                                                <asp:Literal ID="ltr_12E_uploaded_3" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_12E_error_3" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_12E_upload_3"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                             <%
                                                }
                                                if(view_12E_4)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up22" runat="server" >                
                                              <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_12E_doc_for_4" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_12E_name_4" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_12E_4" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_12E_upload_4" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_12E_upload_4_Click"/>                                                   
                                                <asp:Literal ID="ltr_12E_uploaded_4" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_12E_error_4" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_12E_upload_4"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                             <%
                                                }
                                                if(view_12E_5)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up23" runat="server" >
                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_12E_doc_for_5" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_12E_name_5" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_12E_5" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_12E_upload_5" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_12E_upload_5_Click" />                                                   
                                                <asp:Literal ID="ltr_12E_uploaded_5" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_12E_error_5" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_12E_upload_5"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                             <%
                                                }
                                                if(view_12E_6)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up24" runat="server" >
                
                                              <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_12E_doc_for_6" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_12E_name_6" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_12E_6" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_12E_upload_6" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_12E_upload_6_Click" />                                                   
                                                <asp:Literal ID="ltr_12E_uploaded_6" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_12E_error_6" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_12E_upload_6"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                             <%
                                                }
                                                 if (view_12E_7)
                                                 {
                                                     %>
                                            <asp:UpdatePanel ID="up25" runat="server" Visible="False" >                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_12E_doc_for_7" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_12E_name_7" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_12E_7" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_12E_upload_7" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_12E_upload_7_Click" />                                                   
                                                <asp:Literal ID="ltr_12E_uploaded_7" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_12E_error_7" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_12E_upload_7"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%
                                                 }
                                                 
                                                 
                                                if (view_RESQ_1)
                                                {
                                                    
                                                 %>

                                            <asp:UpdatePanel ID="up26" runat="server" >                
                                                <ContentTemplate>                                            
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_RESQ_doc_for_1" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_RESQ_name_1" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_RESQ_1" class="btn btn-xs btn-primary"  runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_RESQ_upload_1" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_RESQ_upload_1_Click" />  
                                                 <asp:Literal ID="ltr_RESQ_uploaded_1" runat="server" Visible="False"></asp:Literal> 
                                                 <asp:Label ID="lbl_RESQ_error_1" runat="server" Visible="true"></asp:Label>                                                
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_RESQ_upload_1"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%
                                                }
                                                if (view_RESQ_2)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up27" runat="server" >                
                                                      <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_RESQ_doc_for_2" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_RESQ_name_2" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_RESQ_2" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_RESQ_upload_2" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_RESQ_upload_2_Click" />                                                    
                                                <asp:Literal ID="ltr_RESQ_uploaded_2" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_RESQ_error_2" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_RESQ_upload_2"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%
                                                }
                                                if (view_RESQ_3)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up28" runat="server" >                
                                                       <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_RESQ_doc_for_3" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_RESQ_name_3" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_RESQ_3" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_RESQ_upload_3" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_RESQ_upload_3_Click" />                                                   
                                                <asp:Literal ID="ltr_RESQ_uploaded_3" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_RESQ_error_3" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_RESQ_upload_3"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                             <%
                                                }
                                                 if (view_RESQ_4)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up29" runat="server" >                
                                              <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_RESQ_doc_for_4" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_RESQ_name_4" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_RESQ_4" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_RESQ_upload_4" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_RESQ_upload_4_Click" />                                                   
                                                <asp:Literal ID="ltr_RESQ_uploaded_4" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_RESQ_error_4" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_RESQ_upload_4"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                             <%
                                                }
                                                 if (view_RESQ_5)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up30" runat="server" >
                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_RESQ_doc_for_5" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_RESQ_name_5" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_RESQ_5" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_RESQ_upload_5" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_RESQ_upload_5_Click"  />                                                   
                                                <asp:Literal ID="ltr_RESQ_uploaded_5" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_RESQ_error_5" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_RESQ_upload_5"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                             <%
                                                }
                                                 if (view_RESQ_6)
                                                {
                                                     %>
                                            <asp:UpdatePanel ID="up31" runat="server" >
                
                                              <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_RESQ_doc_for_6" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_RESQ_name_6" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_RESQ_6" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_RESQ_upload_6" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_RESQ_upload_6_Click" />                                                   
                                                <asp:Literal ID="ltr_RESQ_uploaded_6" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_RESQ_error_6" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_RESQ_upload_6"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                             <%
                                                }
                                                 if (view_RESQ_7)
                                                 {
                                                     %>
                                            <asp:UpdatePanel ID="up32" runat="server" Visible="False" >                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_RESQ_doc_for_7" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_RESQ_name_7" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_RESQ_7" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_RESQ_upload_7" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_RESQ_upload_7_Click"/>                                                   
                                                <asp:Literal ID="ltr_RESQ_uploaded_7" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_RESQ_error_7" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_RESQ_upload_7"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%
                                                 }
                                                 
                                                 if(view_9E_4)
                                                 {
                                                  %>
                                            <asp:UpdatePanel ID="up8" runat="server" >                
                                                <ContentTemplate>
                                              <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_9E_doc_for_4" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_9E_name_4" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_9E_4" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_9E_upload_4" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_9E_upload_4_Click"/>                                                   
                                                <asp:Literal ID="ltr_9E_uploaded_4" runat="server" Visible="False"></asp:Literal> 
                                                <asp:Label ID="lbl_9E_error_4" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_9E_upload_4"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                              <%
                                                 }
                                                 if(view_9E_5)
                                                 {
                                                  %>
                                            <asp:UpdatePanel ID="up9" runat="server" >                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_9E_doc_for_5" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_9E_name_5" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_9E_5" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_9E_upload_5" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_9E_upload_5_Click"/>                                                   
                                                <asp:Literal ID="ltr_9E_uploaded_5" runat="server" Visible="False"></asp:Literal>
                                                <asp:Label ID="lbl_9E_error_5" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_9E_upload_5"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                              <%
                                                 }
                                                 if(view_9E_6)
                                                 {
                                                  %>
                                            <asp:UpdatePanel ID="up10" runat="server" >                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_9E_doc_for_6" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_9E_name_6" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_9E_6" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_9E_upload_6" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_9E_upload_6_Click"/>                                                   
                                                <asp:Literal ID="ltr_9E_uploaded_6" runat="server" Visible="False"></asp:Literal>
                                                <asp:Label ID="lbl_9E_error_6" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_9E_upload_6"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                              <%
                                                 }
                                                 if(view_9E_7)
                                                 {
                                                  %>
                                            <asp:UpdatePanel ID="up11" runat="server" >                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_9E_doc_for_7" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_9E_name_7" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_9E_7" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_9E_upload_7" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_9E_upload_7_Click"/>                                                   
                                                <asp:Literal ID="ltr_9E_uploaded_7" runat="server" Visible="False"></asp:Literal>
                                                <asp:Label ID="lbl_9E_error_7" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_9E_upload_7"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                              <%
                                                 }
                                                  if (view_9E_8)
                                                  {
                                                  %>
                                            <asp:UpdatePanel ID="up12" runat="server" >                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_9E_doc_for_8" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_9E_name_8" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_9E_8" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_9E_upload_8" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_9E_upload_8_Click"/>                                                   
                                                <asp:Literal ID="ltr_9E_uploaded_8" runat="server" Visible="False"></asp:Literal>
                                                <asp:Label ID="lbl_9E_error_8" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_9E_upload_8"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%
                                                  }
                                                  if(view_JMER_4)
                                                  {
                                                      
                                                   %>
                                            <asp:UpdatePanel ID="up13" runat="server" >                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_JMER_doc_for_4" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_JMER_name_4" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_JMER_4" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_JMER_upload_4" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_JMER_upload_4_Click"/>                                                   
                                                <asp:Literal ID="ltr_JMER_uploaded_4" runat="server" Visible="False"></asp:Literal>
                                                <asp:Label ID="lbl_JMER_error_4" runat="server" Visible="true"></asp:Label> 
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_JMER_upload_4"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                                <%
                                                  }
                                                  if(view_JMER_5)
                                                  {
                                                      
                                                   %>
                                            <asp:UpdatePanel ID="up14" runat="server" >                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_JMER_doc_for_5" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_JMER_name_5" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_JMER_5" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_JMER_upload_5" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_JMER_upload_5_Click"/>                                                   
                                                <asp:Literal ID="ltr_JMER_uploaded_5" runat="server" Visible="False"></asp:Literal>
                                                <asp:Label ID="lbl_JMER_error_5" runat="server" Visible="true"></asp:Label>
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_JMER_upload_5"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                                <%
                                                  }
                                                  if(view_JMER_6)
                                                  {
                                                      
                                                   %>
                                            <asp:UpdatePanel ID="up15" runat="server" >                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_JMER_doc_for_6" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_JMER_name_6" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_JMER_6" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_JMER_upload_6" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_JMER_upload_6_Click"/>                                                   
                                                <asp:Literal ID="ltr_JMER_uploaded_6" runat="server" Visible="False"></asp:Literal>
                                                <asp:Label ID="lbl_JMER_error_6" runat="server" Visible="true"></asp:Label>
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_JMER_upload_6"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                                <%
                                                  }
                                                  if(view_JMER_7)
                                                  {
                                                      
                                                   %>
                                            <asp:UpdatePanel ID="up16" runat="server" >                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_JMER_doc_for_7" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_JMER_name_7" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_JMER_7" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_JMER_upload_7" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_JMER_upload_7_Click"/>                                                   
                                                <asp:Literal ID="ltr_JMER_uploaded_7" runat="server" Visible="False"></asp:Literal>
                                                <asp:Label ID="lbl_JMER_error_7" runat="server" Visible="true"></asp:Label>
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_JMER_upload_7"  />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                                <%
                                                  }
                                                    if (view_JMER_8)
                                                    {
                                                      
                                                   %>
                                            <asp:UpdatePanel ID="up17" runat="server" >                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_JMER_doc_for_8" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_JMER_name_8" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_JMER_8" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_JMER_upload_8" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_JMER_upload_8_Click" />                                                   
                                                <asp:Literal ID="ltr_JMER_uploaded_8" runat="server" Visible="False"></asp:Literal>
                                                <asp:Label ID="lbl_JMER_error_8" runat="server" Visible="true"></asp:Label>
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_JMER_upload_8"  />
                                                </Triggers>
                                            </asp:UpdatePanel>                                         
                                            <%
                                                    }
                                                    if (view_covid19)
                                                {

                                                    %>

                                                    <asp:UpdatePanel ID="up18" runat="server" >                
                                                <ContentTemplate>
                                            <tr>
                                                <td width="40%">
                                                <asp:Literal ID="lt_covid_doc_for" runat="server"></asp:Literal>
                                                    <asp:Literal ID="lt_covid_name" runat="server"></asp:Literal>
                                                </td>
                                                <td width="35%">
                                                <asp:FileUpload ID="fu_covid" class="btn btn-xs btn-primary" runat="server" />                                                    
                                                </td>
                                                <td width="25%">
                                                <asp:Button ID="btn_covid_upload" class="btn btn-xs btn-primary" runat="server" Text="Upload" OnClick="btn_covid_upload_Click" />                                                   
                                                <asp:Literal ID="ltr_covid_uploaded" runat="server" Visible="False"></asp:Literal>
                                                <asp:Label ID="lbl_covid_error" runat="server" Visible="true"></asp:Label>
                                                </td>
                                            </tr>
                                            </ContentTemplate>
                                            <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btn_covid_upload"  />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                                    <%
                                                    }

                                                if (!ltrPayAmount.Text.Equals("0.00"))
                                                {
                                                     %>

                                                     <tr>
                                                <td colspan="2" style="text-decoration: underline; font-weight: bold">
                                                    <asp:Button ID="btnPay" runat="server" class="btn btn-primary btn-xs pull-left" 
                                                        Text="Pay" OnClick="btnPay_Click" />
                                                </td>
                                            </tr>
                                            <%
                                                }
                                                 %>
                                                     
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
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <%--<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="Ggs14451"
                        DisplayAfter="10">
                        <ProgressTemplate>
                            <div class="divWaiting" valign="middle">
                                <img src="/images/load.gif" style="position: absolute; left: 50%; top: 35%" />
                                <br />
                                <asp:Label ID="lblWait1" runat="server" Text=" Please wait... " Style="position: absolute;
                                    left: 50%; top: 40%" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>
         
            <br />
            
            <br />
            <br />

            <asp:HiddenField ID="hdfSeqNo" runat="server"/>
            <asp:HiddenField ID="hdfTotlDue" runat="server"/>
            <asp:HiddenField ID="hdfPayAmount" runat="server"/>

        </div>
    </div>


</asp:Content>

