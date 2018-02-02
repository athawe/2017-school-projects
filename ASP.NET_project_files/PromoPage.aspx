<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PromoPage.aspx.cs" Inherits="Aaron_eCommerce2017.PromoPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/PromoPage.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Indie+Flower|Roboto|Saira" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="Panel1" CssClass="Panels" style="left: 10px; top: 10px; height: 400px; right: 10px;" runat="server">
                <h1>Promo Page</h1>
                <h3 style="width:300px;">This is our current special, we run 10% off of our most expensive item!</h3>
                <asp:Label ID="lblName" CssClass="Labels" runat="server" Text=""></asp:Label><br />
                <asp:Label ID="lblDesc" CssClass="Labels" runat="server" style="width:300px;" Text="Label"></asp:Label><br /><br /><br />
                <asp:Image ID="imgPictures" style="position:absolute; left:600px; top: 30px; width:210px; height:289px; " runat="server" />
                <asp:Label ID="lblOldPrice" CssClass="Labels" runat="server" Text="Label"></asp:Label><br />
                <asp:Label ID="lblNewPrice" CssClass="Labels" runat="server" Text="Label"></asp:Label><br />            
            </asp:Panel>
        </div>
    </form>
</body>
</html>
