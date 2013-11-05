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

        <style>

            @import url(http://fonts.googleapis.com/css?family=Open+Sans:400italic,400,300,600,700);

            body {
                background-color: #ebecec;
                font-family: 'Open Sans', sans-serif;
            }

            .favorites-tile {
                width: 340px;
                height: 460px;

                border-right: 1px solid #999;
                border-bottom: 1px solid #999;
                background-color: #fff;
            
                margin: 20px;    
            }

            .favorites-tile .tile-title {
                background-color: #666;
                text-align: right;
                
                padding-right: 10px;
                padding-top: 2px;
                height: 18px;


                color: #fff;

                font-size: 0.8em;
                text-transform: uppercase;
                font-weight: 300;
            }

            .favorites-tile .starred-item  {
                height: 60px;
                border-bottom: 1px solid #ccc;
            }

            .favorites-tile .starred-item table {
                width: 100%;
            }
            
            .favorites-tile .starred-item table, .favorites-tile .starred-item table tr, .favorites-tile .starred-item table tr td {
                padding: 0;
                margin: 0;
            }

            .favorites-tile .starred-item .icon {
                width: 40px;
                height: 40px;
                padding: 10px;
                background-color: #ccc;
            }
            
            .favorites-tile .starred-item .star {
                width: 40px;
                height: 40px;
                padding: 10px;
                background-color: #ccc;
                text-align: center;
            }

        </style>

        <div class="favorites-tile">
            <div class="tile-title">Starred Items</div>
            <div class="starred-item">
                <table>
                    <tr>
                        <td class="icon">icon</td>
                        <td class="itemtext">
                            <span class="itemtitle">Title</span>
                            <span class="itemtype">Type</span>
                        </td>
                        <td class="star">*</td>
                    </tr>
                </table>
            </div>
        </div>


    </form>
</body>
</html>
