<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error_.aspx.cs" Inherits="ErrorPages_Error_" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
        <form id="form1" runat="server">
        <div id ="MainDiv" runat="server" style="margin-top:25vh; margin-left:25%; padding:5px 5px 5px 5px; border : 2px solid #F93154; width:50vw; height:40vh; background-color:white; border-radius: 10px;">
           <div style="padding-left: 5px; margin-top:0px; margin-bottom:7px">
            <asp:PlaceHolder runat ="server" ID="NoAutority_Panel" Visible="false">
              <div id="NoAutority" style="text-align:center">
                  <h2 style="margin-top:10%"><asp:Literal ID="e_hedding" runat="server"></asp:Literal></h2>
                  <h5 style="color:#11398a"><asp:Literal ID="_error" runat="server"></asp:Literal></h5>
                  
              </div>
             </asp:PlaceHolder>
           </div>
        </div>
    </form>
</body>
</html>