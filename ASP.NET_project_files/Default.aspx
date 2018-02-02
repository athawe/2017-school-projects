<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Aaron_eCommerce2017.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Aaron's eCommerce Site</title>
    <link href="Styles/Default.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Indie+Flower|Roboto|Saira" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <img src="Images/Logo.jpg" class="topBar"/>
        <asp:Panel ID="topPanel" class="topBar" runat="server">
            <asp:Button ID="btnPromo" CssClass="Buttons" runat="server" style="left:100px;" Text="Promo Page" OnClick="btnPromo_Click" />
            <asp:Button ID="btnCatalog" CssClass="Buttons" runat="server" style="left:220px;width:100px;" Text="Catalog" OnClick="btnCatalog_Click" />
            <asp:Button ID="btnCart" CssClass="Buttons" runat="server" Text="Cart" style="left:335px;width:100px;" OnClick="btnCart_Click" />
            <asp:Button ID="btnWeather" CssClass="Buttons" runat="server" style="left:450px;width:100px;" Text="Weather" OnClick="btnWeather_Click" />
            <asp:Button ID="btnCustomers" CssClass="Buttons" runat="server" style="left:565px;" Text="Customers" OnClick="btnCustomers_Click" />
            <asp:Button ID="btnProducts" CssClass="Buttons" runat="server" style="left:680px;" Text="Products" OnClick="btnProducts_Click" />
        </asp:Panel>
        <iframe id="MyFrame" class="MainFrame" src="" runat="server"
            style="left: 10px; top: 100px; height: 600px;">
            <asp:Image ID="imgFace" runat="server" />
        </iframe>
    </div>
    </form>
</body>
</html>
