<%@ Page Title="" Language="C#" MasterPageFile="~/Life.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="PolicyRevival.aspx.cs" Inherits="Life_Authorized_PolicyRevival" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/fieldset.css" rel="stylesheet" />
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

              <style>

        @media screen and (max-width:1800px)
        {
           /*input[type=text]{width:50%}
           .select {
                width:50%;
           }*/
            input[type='text'], select{
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

                @media screen and (max-width:600px)
        {
           /*input[type=text]{width:100%}
             .select {
                width:100%;
           }*/
             input[type='text'] {
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container" id="main-container">

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
                            Policy Revivals</h3>
                    </center>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            
            <asp:UpdatePanel ID="Ggs14451" runat="server" >
                
                <ContentTemplate>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            
                            <div class="col-sm-9">
                                <span></span>
                            </div>
                            <asp:PlaceHolder ID="Panel3" runat="server" Visible="false">
                                <asp:GridView ID="gvRevPolicies" runat="server" AutoGenerateColumns="false" CellPadding="4"
                                    CssClass="footable" OnSelectedIndexChanged="gvRevPolicies_SelectedIndexChanged">
                                    <Columns>
                                        <asp:BoundField DataField="POLNO" HeaderText="Policy No." />
                                        <asp:BoundField DataField="NAME" HeaderText="Name" />
                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="30" ControlStyle-CssClass="btn btn-xs btn-primary" />
                                    </Columns>
                                </asp:GridView>
                            </asp:PlaceHolder>
                            <div class="form-group">
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-9">
                                    <asp:CustomValidator ID="PolNumValidator" runat="server" CssClass="errorMsg_1" ForeColor="Red"
                                        OnServerValidate="checkPolLoanNum" ValidationGroup="GP101"></asp:CustomValidator>
                                </div>
                            </div>

                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <div class="button101">
                                &nbsp;<strong>Request online policy revival</strong></div>
                        
                            <br />
                    
                            To make a request to revive a registered policy, please complete
                            the form, which can be filled <a href="PolicyRevivalRequest.aspx" target="_blank">
                                here</a>.
                        </div>
                        <div class="col-xs-1">
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-1">
                        </div>
                        <div class="col-xs-10">
                            <asp:Panel ID="Panel1" runat="server" Visible="false">

                            <div>
                            To consider the revival of your policy, please pay the below mentioned amount before expire date.
                            </div>
                            <br />
                                  <div style="align:center">
                                     <fieldset>
                                <legend>Policy Details :</legend>

                   
                                  <div class="row">
                                    <div class="col-sm-3">
                                        Policy Number
                                    </div>
                                       <div class="col-sm-9">
                                         <asp:Literal ID="litPolNumber" runat="server"></asp:Literal>
                                    </div>
                                
                                </div> 
                                            <br />
                                      <div class="row">
                                    <div class="col-sm-3">
                                         Customer Name
                                    </div>
                                       <div class="col-sm-9">
                                       <asp:Literal ID="litStatus" runat="server"></asp:Literal>
                                        <asp:Literal ID="litName" runat="server"></asp:Literal>
                                    </div>
                                   
                                </div>  

                               <br />

                                <div class="row">
                                    <div class="col-sm-3">
                                         Total Dues Amount (Rs.)
                                    </div>
                                       <div class="col-sm-9">
                                            <asp:Literal ID="litTotDueAmt" runat="server"></asp:Literal>
                                    </div>
                                   
                                </div> 
                                           <br />
                                
                                <div class="row">
                                    <div class="col-sm-3">
                                         Expired Date
                                    </div>
                                       <div class="col-sm-9">
                                           <asp:Literal ID="litExpiryDate" runat="server"></asp:Literal>
                                    </div>
                                   
                                </div>
                                      <br />
                                                              
                                <div class="row">
                                    <div class="col-sm-3">
                                          Payment Amount (Rs.)
                                    </div>
                                       <div class="col-sm-9">
                                             <asp:TextBox ID="txtPayAmt" runat="server" class="form-control" MaxLength="12"></asp:TextBox>
                                             <asp:CustomValidator ID="addtAmtValidator" runat="server" ControlToValidate="txtPayAmt"
                                             CssClass="errorMsg_1" ForeColor="Red" OnServerValidate="checkAddtAmt"></asp:CustomValidator>
                                                    
                                    </div>
                                   
                                </div>
                                           <br />


                                <div class="row">
                                    <div class="col-sm-3">
                                    </div>
                                       <div class="col-sm-9">
                                                 <asp:Button ID="btnPay" runat="server" class="btn btn-primary btn-xs pull-left" Text="Pay"
                                                            ValidationGroup="GP102" OnClick="btnPay_Click" />
                                                                 
                                    </div>
                                   
                                </div>
                                        <br />
                                   <div class="row">
                                    <div class="col-sm-3">
                                    </div>
                                       <div class="col-sm-9">
                                        <asp:CustomValidator ID="PayPremValidator" runat="server" CssClass="errorMsg_1" ForeColor="Red"></asp:CustomValidator>
                                    </div>
                                   
                                </div> 
                                
                                <div class="row">
                                    <div class="col-sm-3">
                                    </div>
                                       <div class="col-sm-9">
                                                 <asp:HiddenField ID="hdfSeqNo" runat="server" />
                                                                 
                                    </div>
                                   
                                </div>

                                </fieldset>
                                      </div>

                             </asp:Panel>                            
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
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="Ggs14451"
                        DisplayAfter="10">
                        <ProgressTemplate>
                            <div class="divWaiting" valign="middle">
                                <img src="/images/load.gif" style="position: absolute; left: 50%; top: 35%" />
                                <br />
                                <asp:Label ID="lblWait1" runat="server" Text=" Please wait... " Style="position: absolute;
                                    left: 50%; top: 40%" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger  ControlID="btnPay"  />
                    <asp:PostBackTrigger ControlID="gvRevPolicies"/>
                    <%--<asp:PostBackTrigger ControlID="gvRevPolicies" EventName="gvRevPolicies_SelectedIndexChanged" />--%>
                </Triggers>
            </asp:UpdatePanel>
            <br />
            <br />
            <br />
        </div>

          <link href="/css/footable.min.css"
            rel="stylesheet" type="text/css" />
       <script src="/js/footable.min.js" type="text/javascript"></script>

            <script type="text/javascript">
                $(function () {
                    $('[id*=gvRevPolicies]').footable();
                    $('[id*=gvMembers]').footable();
                });

                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    $('[id*=gvRevPolicies]').footable();
                    $('[id*=gvMembers]').footable();

                });
            </script>

    </div>
</asp:Content>
