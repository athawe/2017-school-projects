<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="Aaron_eCommerce2017.Catalog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Catalog</title>
    <link href="Styles/Catalog.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Indie+Flower|Roboto|Saira" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="tblCatalog" CssClass="CellStyle" Height="123px" Width="567px" BackColor="#FFCCCC" runat="server" BorderStyle="Solid"></asp:Table>
        <asp:Button ID="btnCatalog" runat="server" style="visibility:hidden" Text="Button" OnClick="btnCatalog_Click" />
    </div>
    </form>
</body>
</html>
