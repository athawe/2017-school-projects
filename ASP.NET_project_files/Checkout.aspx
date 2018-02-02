<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="Aaron_eCommerce2017.Checkout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/Checkout.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Indie+Flower|Roboto|Saira" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <iframe id="CustomerFrame" class="CheckoutFrame" src="Customers.aspx" runat="server"
            style="left:10px; top:80px">
        </iframe>
        <iframe id="DetailFrame" class="CheckoutFrame" src="Details.aspx" runat="server"
            style="left:440px; top:80px">
        </iframe>
    </div>
    </form>
</body>
</html>
