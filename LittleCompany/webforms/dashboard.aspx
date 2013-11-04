<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="LittleCompany.GUI.webforms.dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ondor</title>
     <link href="../webresources/style/main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    
        
    <div class="header">
         <div class="logo">Ondor</div>
        <div style="float:right; margin-top:35px;"><asp:TextBox runat="server" ID="UI_Search" /> <asp:Button runat="server" ID="UI_BntSearch" Text="Zoeken" /></div>
    </div>

        <div>
            <span onclick="window.open('popups/add_organisation.aspx');">Voeg organisatie toe</span>
        </div>
        <br /> <br /> <br />
    <div>
        Favorieten:
        <asp:Table runat="server" ID="UI_Favorites"></asp:Table>
    </div>


    </form>
</body>
</html>
