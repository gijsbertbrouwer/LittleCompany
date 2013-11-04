<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LittleCompany.GUI.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Ondor</title>
    
    <link href="webresources/style/login.css" rel="stylesheet" />
    <link href="webresources/style/main.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
    <div class="loginscreen">
        <div class="header">
            <div class="logo">Ondor</div>
            <div class="login">
                <asp:TextBox runat="server" id="UI_UserName" />
                <asp:TextBox runat="server" id="UI_Password" />
                <asp:Label runat="server" ID="UI_Respons" />
                <asp:Button runat="server" ID="UI_Login" OnClick="UI_Login_Click" Text="Inloggen" />
            </div>
        </div>

         <div class="content">
            <div class="signupform">
                <asp:TextBox Cssclass="username_signup" runat="server" Id="UI_SignUp_EmailAdress" placeholder="e-mailadres"/>
                 <asp:TextBox Cssclass="password_signup" runat="server" Id="UI_SignUp_Password" placeholder="wachtwoord"/>
                 <asp:TextBox Cssclass="company_signup" runat="server" Id="UI_SignUp_OrganisationName" placeholder="organisatie" />
                 <asp:Label runat="server" ID="UI_SignUp_Respons" />
                 <asp:Button Cssclass="submit_signup" Text="Aanmelden"  runat="server" ID="UI_SignUp" OnClick="UI_SignUp_Click"  /> 


            </div>
        </div>


 
    </div>
    </form>
</body>
</html>
