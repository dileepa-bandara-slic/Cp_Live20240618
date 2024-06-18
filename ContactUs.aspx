<%@ Page Title="" Language="C#" MasterPageFile="~/Common.master" AutoEventWireup="true"
    CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
     <script src="/js/jquery-3.5.1.min.js"></script>
    <script src="/js/jquery-3.5.1.js"></script>
    <style>
        #map_canvas
        {
            min-width: 200px;
            width: 100%;
            min-height: 348px;
            height: 100%;
        }
        
        .style2
        {
            width: 100%;
        }
        
         /*.container
        {
            max-width: 1024px;
            width: 100%;
            margin: 0 auto;
            height: auto;
            background: #FFFFFF;
            font-size: 12px;
            margin-right: auto;
            margin-left: auto;
        }*/
        
    </style>
        
    <script src="https://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <script>

        function initialize() {
            var map_canvas = document.getElementById('map_canvas');
            var map_options = { center: new google.maps.LatLng(6.923809, 79.853742), zoom: 18, mapTypeId: google.maps.MapTypeId.ROADMAP
            }

            var map = new google.maps.Map(map_canvas, map_options)
            var marker = new google.maps.Marker({
                position: { lat: 6.923809, lng: 79.853742 },
                map: map
            });
        }
        google.maps.event.addDomListener(window, 'load', initialize);   
    </script>
    <script>
        $(document).ready(function () {
            $(".nav-tabs a").click(function () {
                $(this).tab('show');
            });
            $('.nav-tabs a').on('shown.bs.tab', function (event) {
                var x = $(event.target).text();
                var y = $(event.relatedTarget).text();
                $(".act span").text(x);
                $(".prev span").text(y);
            });
        });
    </script>
    <style>
        .containerClass .ajax__html_editor_extender_container
        {
            width: 100% !important; /*important is really important at here*/
            height: 100% !important;
           /* max-height: 170px !important;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <div class="main-container">
    <br />
    </br>
        <div class="container">
        <%--    <br />--%>
            <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </ajaxToolkit:ToolkitScriptManager>
           <%-- <div class="panel-group">--%>
                    <div class="row">
                    <div class="col-sm-6">
                        <div class="row">
                        <div class="col-sm-12">
                        <div class="panel panel-default">
                            <div class="panel-body">
                            <div class="containerClass">
                           
                         
                                <div class="row">
                                    <div id="map_canvas">
                                    </div>
                                    <input id="load" type="button" style="visibility: hidden; width: 100%; height: 100%"
                                        value="Load" />
                                </div>
                                
                                <div class="row">
                                    <p>
                                        <strong class="color_blue">Head Office :</strong><br />
                                        Rakshana Mandiraya,<br />
                                        No.21 Vauxhall Street,
                                        <br />
                                        Colombo 2,
                                        <br />
                                        Sri Lanka
                                    </p>
                                </div>
              
                         </div>
                            
                            </div>
                        </div>
                        </div>
                        </div>

                       <div class="row">
                       <div class="col-sm-12">
                            <div class="panel panel-default">
                            <div class="panel-body">
                         <div class="row">
    <div class="col-sm-6">
        <p>

        <span class="color_blue"><strong>Hotline :</strong></span> +94 11 2357357,
        <br />
        <span class="color_blue" style="color:#fff;"><strong>Hotline :</strong></span> 
        +94 11 7357357,<br />
        <span class="color_blue" style="color:#fff;"><strong>Hotline :</strong></span> 
        +94 11 5357357,
        <br />
        <span class="color_blue" style="color:#fff;"><strong>Hotline :</strong></span> 
        +94 11 4357357,
        </p>
    
    
    </div>
    <div class="col-sm-6">
    <p>
      <span class="color_blue"><strong>General No :</strong></span> +94 11 2357000,<br />
        <strong style="color:#fff;">General No :</strong> +94 11 7357000,<br />
        <strong style="color:#fff;">General No :</strong> +94 11 5357000,
        <br />
        <strong style="color:#fff;">General No :</strong> +94 11 4357000,<br />
        </p>
    
    </div>
    </div>
    <div class="row">
    <div class="col-sm-4">
    <p>
    <span class="color_blue"><strong>Fax :</strong></span> +94 112 447742
    </p>
    </div>
    </div>
    </div>
    </div>
                       </div>
                       </div>
                    </div>

                     <div class="col-sm-6">
                        <%--<div class="panel panel-default">--%>
                        <div class="panel panel-default">
                            <div class="panel-body">
                           <div class="form-group">
                                <div class="row">
                                    <label for="inputEmail3" class="col-sm-3 control-label">
                                       Name
                                    </label>
                                    <div class="col-sm-9">
                                         <asp:TextBox ID="txt_senderName" runat="server" MaxLength="15" class="form-control"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txt_senderName" CssClass="errorMsg_1" 
                        ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                    </div>
                               
                                </div>
                                </div>
                                <div class="form-group">
                                 <div class="row">
                                    <label for="inputEmail3" class="col-sm-3 control-label">
                                       Email Address
                                    </label>
                                    <div class="col-sm-9">
                                         <asp:TextBox ID="txt_emailAdd" runat="server" MaxLength="100" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txt_emailAdd" CssClass="errorMsg_1" 
                        ErrorMessage="* Required"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                        ControlToValidate="txt_emailAdd" CssClass="errorMsg_1" ErrorMessage="*Invalid" 
                        Font-Bold="False" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                               
                                </div>
                                </div>

                                <div class="form-group">
                                  <div class="row">
                                    <label for="inputEmail3" class="col-sm-3 control-label">
                                       Subject
                                    </label>
                                    <div class="col-sm-9">
                                     <asp:TextBox ID="txt_subject" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                               
                                </div>
                                </div>
                                </br>

                                <div>
                                <div class="row">
                                            <div class="col-xs-12">
                                <div class="containerClass">
                                    <asp:UpdatePanel ID="updatePanel1" runat="server">
                                        <ContentTemplate>

                                            <asp:TextBox runat="server" ID="txt_mail_body" TextMode="MultiLine" Rows="7" Style="width: 100%" /><br />
                                           <%-- <div class="row">
                                            <div class="col-xs-12">--%>
                                            <ajaxToolkit:HtmlEditorExtender ID="htmlEditorExtender2" TargetControlID="txt_mail_body"
                                                EnableSanitization="false" runat="server" DisplaySourceTab="True">
                                                <Toolbar>
                                                    <ajaxToolkit:Undo />
                                                    <ajaxToolkit:Redo />
                                                    <ajaxToolkit:Bold />
                                                    <ajaxToolkit:Italic />
                                                    <ajaxToolkit:Underline />
                                                    <ajaxToolkit:StrikeThrough />
                                                    <ajaxToolkit:Subscript />
                                                    <ajaxToolkit:Superscript />
                                                    <ajaxToolkit:JustifyLeft />
                                                    <ajaxToolkit:JustifyCenter />
                                                    <ajaxToolkit:JustifyRight />
                                                    <ajaxToolkit:JustifyFull />
                                                    <ajaxToolkit:InsertOrderedList />
                                                    <ajaxToolkit:InsertUnorderedList />
                                                    <ajaxToolkit:CreateLink />
                                                    <ajaxToolkit:UnLink />
                                                    <ajaxToolkit:RemoveFormat />
                                                    <ajaxToolkit:SelectAll />
                                                    <ajaxToolkit:UnSelect />
                                                    <ajaxToolkit:Delete />
                                                    <ajaxToolkit:Cut />
                                                    <ajaxToolkit:Copy />
                                                    <ajaxToolkit:Paste />
                                                    <ajaxToolkit:BackgroundColorSelector />
                                                    <ajaxToolkit:ForeColorSelector />
                                                    <ajaxToolkit:FontNameSelector />
                                                    <ajaxToolkit:FontSizeSelector />
                                                    <ajaxToolkit:Indent />
                                                    <ajaxToolkit:Outdent />
                                                    <ajaxToolkit:InsertHorizontalRule />
                                                    <ajaxToolkit:HorizontalSeparator />
                                                </Toolbar>
                                            </ajaxToolkit:HtmlEditorExtender>
                                         <%--   </div>
                                            </div>--%>
                            <br />




           <%--     <div class="row">
                <div class="col-xs-3 col-sm-3">
                </div>
                <div class="col-xs-8 col-sm-6">
                   
                      <asp:Image ID="Image1" runat="server" ImageUrl="~/CImage.aspx" class="img-responsive" width="100%" height="100%"/>
                </div>
                <div class="col-xs-1 visible-xs">
                </div>
                <div class="row visible-xs">
          
                </div>
                </br>
                <div class="col-xs-3 visible-xs">
                </div>
                <div class="col-xs-2 col-sm-3">
                 
                     <asp:ImageButton ID="ChangeImage" runat="server" ImageUrl="images/reg_refresh_sm.png" style="max-width:45px" OnClick="ChangeImage_Click" CausesValidation="False"/>
                        
                </div>
                  <div class="col-xs-7 visible-xs">
                </div>
      
                </div>--%>
      

                <div class="row">
             <%--   <div class="col-xs-3 col-sm-3">
                </div>
                <div class="col-xs-8 col-sm-6">
                   
                      <asp:Image ID="Image1" runat="server" ImageUrl="~/CImage.aspx" class="img-responsive" width="100%" height="100%"/>
                </div>
                <div class="col-xs-1 visible-xs">
                </div>
                <div class="row visible-xs">
          
                </div>
                </br>
                <div class="col-xs-3 visible-xs">
                </div>
                <div class="col-xs-2 col-sm-3">
                 
                     <asp:ImageButton ID="ChangeImage" runat="server" ImageUrl="images/reg_refresh_sm.png" style="max-width:45px" OnClick="ChangeImage_Click" CausesValidation="False"/>
                        
                </div>
                  <div class="col-xs-7 visible-xs">
                </div>--%>
           <div class="col-sm-3"></div>
                        <div class="col-sm-9">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/CImage.aspx" class="img-responsive"/>
                <asp:ImageButton ID="ChangeImage" runat="server" ImageUrl="images/reg_refresh_sm.png" style="max-width:45px" OnClick="ChangeImage_Click" CausesValidation="False"/>
                        </div>
                </div>



                  <%--  <div class="row">
                        <div class="col-xs-3">
                        </div>
                        <div class="col-xs-3">
                          
                              <asp:Button  ID="ChangeImage" runat="server" Text="Change Image" onclick="ChangeImage_Click"  CssClass="btn btn-primary btn-xs pull-left"
                                CausesValidation="False" />

                                 </div>
                        <div class="col-xs-6">
                        </div>
                    </div>--%>
                    <br />
                         <div class="row">
                 <%--       <label for="inputText" class="col-sm-3 control-label">
                            Enter text above</label>--%>
                            <div class="col-sm-3">Enter text above</div>
                        <div class="col-sm-9">
                          <asp:TextBox ID="txtimgcode" runat="server" MaxLength="50" class="form-control" placeholder="Enter the text above"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="MobileNumRequired0" runat="server" 
                                ControlToValidate="txtimgcode" ErrorMessage="Text is required" ForeColor="Red" 
                                ToolTip="Text is required" Font-Size="Medium">*</asp:RequiredFieldValidator>
                        </div>
                       
                    </div>
                    <div class="row">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-9">
                    
                        <asp:CustomValidator ID="ImageValidator" runat="server" ValidateEmptyText="true"
                                ControlToValidate="txtimgcode" ForeColor="Red" onservervalidate="checkImageCode"></asp:CustomValidator>
                       
                    </div>
                    </div>
                    <div class="row">
                       <div class="col-sm-3">
                                              
                       </div>
                       <div class="col-sm-9">
                    <%--    <asp:Button ID="submit" runat="server" Text="Email" class="btn btn-primary btn-xs align-left"
                            onclick="Button1_Click" style="text-align:center"/>--%>

                               <asp:Button ID="submit" runat="server" Text="Email" class="btn btn-primary"
                            onclick="Button1_Click" style="text-align:center"/>
                            <asp:Label ID="message" runat="server" CssClass="errorMsg_1" Font-Bold="True" 
            Font-Names="Calibri" Font-Size="10pt" ForeColor="Red"></asp:Label>
                       </div>

                    </div>
                    <br />
        
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="submit" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                </div>
                                </div>
                                </div>
                            

                            </div>
                        </div>
                    </div>

                </div>
             
            </div>
        </div>
        </br>
</asp:Content>
