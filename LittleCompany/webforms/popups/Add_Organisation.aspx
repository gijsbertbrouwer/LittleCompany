<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Organisation.aspx.cs" Inherits="LittleCompany.GUI.webforms.popups.Add_Organisation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Organisatie aanmaken</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Organisatie toevoegen<br />

        <asp:TextBox runat="server" ID="UI_Name" />
        <asp:Label runat="server" ID="UI_Respons" />
        <asp:Button runat="server" ID="UI_btn_add" OnClick="UI_btn_add_Click" Text="Opslaan" />

    </div>
    </form>
</body>
</html>
