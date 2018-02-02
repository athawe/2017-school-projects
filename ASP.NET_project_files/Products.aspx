<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Aaron_eCommerce2017.Products" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="Styles/Products.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Indie+Flower|Roboto|Saira" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="Panel1" CssClass="Panels" style="left: 10px; top: 10px; height: 400px; right: 10px;" runat="server">
                <asp:Label ID="lblProductNumber" CssClass="Labels" style="left: 10px; top: 20px" runat="server" Text="Product #"></asp:Label>
                <asp:TextBox ID="txtProductNumber" CssClass="TextBoxes" style="top: 20px; left: 150px; width: 111px; color:red" runat="server"></asp:TextBox>

                <asp:Label ID="lblManufactCode" CssClass="Labels" style="left: 10px; top: 50px" runat="server" Text="Manufact Code"></asp:Label>
                <asp:TextBox ID="txtManufactCode" CssClass="TextBoxes" style="left: 150px; top: 50px; width: 200px" runat="server"></asp:TextBox>

                <asp:Label ID="lblDesc" CssClass="Labels" style="left: 10px; top: 80px" runat="server" Text="Description"></asp:Label>
                <asp:TextBox ID="txtDesc" CssClass="TextBoxes" style="left: 150px; top: 80px; width: 200px" runat="server"></asp:TextBox>

                <asp:Label ID="lblPict" CssClass="Labels" style="left: 10px; top: 110px" runat="server" Text="Picture"></asp:Label>
                <asp:TextBox ID="txtPict" CssClass="TextBoxes" style="left: 150px; top: 110px; width: 200px" runat="server" Text="" ></asp:TextBox>

                <asp:Label ID="lblQuan" CssClass="Labels" style="left: 10px; top: 140px" runat="server" Text="QOH"></asp:Label>
                <asp:TextBox ID="txtQuan" CssClass="TextBoxes" style="left: 150px; top: 140px; width: 120px" runat="server"></asp:TextBox>

                <asp:Label ID="lblPrice" CssClass="Labels" style="left: 10px; top: 170px" runat="server" Text="Price"></asp:Label>
                <asp:TextBox ID="txtPrice" CssClass="TextBoxes" style="left: 150px; top: 170px; width: 120px" runat="server"></asp:TextBox>
            
                <asp:Button ID="btnNewProduct" CssClass="Buttons" style="left: 10px; bottom: 10px; width:60px;" runat="server" Text="New" ToolTip="Create a new product" OnClick="btnNewProduct_Click"  />
                <asp:Button ID="btnAddProduct" CssClass="Buttons" style="left: 80px; bottom: 10px; width:60px;" runat="server" Text="Add" ToolTip="Create a new product" OnClick="btnAddProduct_Click"  />
                <asp:Button ID="btnUpdateProduct" CssClass="Buttons" style="left: 150px; bottom: 10px; width:80px;" runat="server" Text="Update" ToolTip="Create a new product" Enabled="False" OnClick="btnUpdateProduct_Click" />
                <asp:Button ID="btnDeleteProduct" CssClass="Buttons" style="left: 240px; bottom: 10px; width:70px;" runat="server" Text="Delete" ToolTip="Create a new product" Enabled="False" OnClick="btnDeleteProduct_Click" />
                <asp:Button ID="btnFindProduct" CssClass="Buttons" style="left: 320px; bottom: 10px; width:60px;" runat="server" Text="Find" ToolTip="Find a product" OnClick="btnFindProduct_Click" />
                <asp:Image ID="imgPictures" style="position:absolute; left:400px; top: 50px; width:210px; height:289px; " runat="server" />
                <asp:ListBox ID="lstImages" runat="server" AutoPostBack="True"  style="position:absolute; left:620px; top: 50px; height: 175px; width:180px" OnSelectedIndexChanged="lstImages_SelectedIndexChanged" />
            </asp:Panel>    
            <asp:Label ID="lblMessages" runat="server" CssClass="Messages" style="left:10px; top:300px; right:10px; height:16px; background-color:cyan" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
