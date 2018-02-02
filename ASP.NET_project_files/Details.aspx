<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Aaron_eCommerce2017.Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/Details.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Indie+Flower|Roboto|Saira" rel="stylesheet"/>
    <script>
    function Confim() {
        var result = window.confirm('Are you sure?');
           if (result == true)
            return true;
        else
            return false;
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="Table1" runat="server" CssClass="CellStyle" Height="123px" Width="300px" BackColor="#CDECFE" BorderStyle="Dashed"></asp:Table>
        <br />
        <asp:Label ID="LblTotal" runat="server" CssClass="LabelTotal" Text="0.00"></asp:Label>
        <asp:CheckBox ID="ChkMailingList" runat="server" CssClass="Checkboxes" AutoPostback="false" Text="Add me to the Mailing List" OnCheckedChanged="AddMe" />
        <asp:Button ID="BtnPay" runat="server" Text="Pay for my Order" CssClass="Buttons" style="left:10px;bottom:20px" OnClick="PayForOrder"/>
        <asp:Button ID="ConfirmAction" OnClientClick="return Confim();" CssClass="Buttons" style="left:200px;bottom:20px" runat="server" Text="Confirm" OnClick="ConfirmAction_Click" />
    </div>
    </form>
</body>
</html>
