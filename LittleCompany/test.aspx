<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="LittleCompany.GUI._testdb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr><td>Users</td><td><asp:Label runat="server" ID="UI_CountOfUsers" /></td></tr>
        <tr><td>logged in users</td><td><asp:Label runat="server" ID="UI_CountOfCurrentUsers" /></td></tr>
    </table>
    </div>
    </form>
</body>
</html>
