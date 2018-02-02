<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="Aaron_eCommerce2017.Customers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customers</title>
    <link href="Styles/Customers.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Indie+Flower|Roboto|Saira" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" CssClass="Panels" style="left: 10px; top: 10px; height: 340px; right: 10px;" runat="server">
            <asp:Label ID="lblCustomerNumber" CssClass="Labels" style="left: 10px; top: 20px" runat="server" Text="Customer #"></asp:Label>
            <asp:TextBox ID="txtCustomerNumber" CssClass="TextBoxes" style="top: 20px; left: 110px; width: 111px; color:red" runat="server"></asp:TextBox>

            <asp:Label ID="lblFirstName" CssClass="Labels" style="left: 10px; top: 50px" runat="server" Text="First Name"></asp:Label>
            <asp:TextBox ID="txtFirstName" CssClass="TextBoxes" style="left: 110px; top: 50px; width: 200px" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvFirstName" CssClass="Validation" runat="server" 
                ErrorMessage="Please enter your first name." 
                ControlToValidate="txtFirstName" 
                EnableClientScript="False" 
                ValidateRequestMode="Disabled"
                style="left: 335px; top: 50px;">
            </asp:RequiredFieldValidator>

            <asp:Label ID="lblLastName" CssClass="Labels" style="left: 10px; top: 80px" runat="server" Text="Last Name"></asp:Label>
            <asp:TextBox ID="txtLastName" CssClass="TextBoxes" style="left: 110px; top: 80px; width: 200px" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvLastName" CssClass="Validation" runat="server" 
                ErrorMessage="Please enter your last name." 
                ControlToValidate="txtLastName" 
                EnableClientScript="False" 
                ValidateRequestMode="Disabled"
                style="left: 335px; top: 80px;">
            </asp:RequiredFieldValidator>


            <asp:Label ID="lblAddress" CssClass="Labels" style="left: 10px; top: 110px" runat="server" Text="Address"></asp:Label>
            <asp:TextBox ID="txtAddress" CssClass="TextBoxes" style="left: 110px; top: 110px; width: 200px" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAddress" CssClass="Validation" runat="server" 
                ErrorMessage="Please enter your address." 
                ControlToValidate="txtAddress" 
                EnableClientScript="False" 
                ValidateRequestMode="Disabled"
                style="left: 335px; top: 110px;">
            </asp:RequiredFieldValidator>

            <asp:Label ID="lblCity" CssClass="Labels" style="left: 10px; top: 140px" runat="server" Text="City"></asp:Label>
            <asp:TextBox ID="txtCity" CssClass="TextBoxes" style="left: 110px; top: 140px; width: 200px" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCity" CssClass="Validation" runat="server" 
                ErrorMessage="Please enter your city." 
                ControlToValidate="txtCity" 
                EnableClientScript="False" 
                ValidateRequestMode="Disabled"
                style="left: 335px; top: 140px;">
            </asp:RequiredFieldValidator>

            <asp:Label ID="lblProvince" CssClass="Labels" style="left: 10px; top: 170px" runat="server" Text="Province"></asp:Label>
            <asp:TextBox ID="txtProvince" CssClass="TextBoxes" style="left: 110px; top: 170px; width: 200px" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvProvince" CssClass="Validation" runat="server" 
                ErrorMessage="Please enter your Province." 
                ControlToValidate="txtProvince" 
                EnableClientScript="False" 
                ValidateRequestMode="Disabled"
                style="left: 335px; top: 170px;">
            </asp:RequiredFieldValidator>

            <asp:Label ID="lblPostal" CssClass="Labels" style="left: 10px; top: 200px" runat="server" Text="Postal"></asp:Label>
            <asp:TextBox ID="txtPostal" CssClass="TextBoxes" style="left: 110px; top: 200px; width: 200px" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPostal" CssClass="Validation" runat="server" 
                ErrorMessage="Please enter your postal code." 
                ControlToValidate="txtPostal" 
                EnableClientScript="False" 
                ValidateRequestMode="Disabled"
                style="left: 335px; top: 200px;">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rgvPostal" runat="server" CssClass="Validation"
                ErrorMessage="The format of your postal code is not acceptable." 
                ControlToValidate="txtPostal"
                EnableClientScript="False"
                ValidateRequestMode="Disabled"
                style="left: 335px; top: 200px;" ValidationExpression="[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]"></asp:RegularExpressionValidator>

            <asp:Button ID="btnNewCustomer" CssClass="Buttons" style="left: 10px; bottom: 10px; width:60px;" runat="server" Text="New" ToolTip="Create a new customer" OnClick="btnNewCustomer_Click"  />
            <asp:Button ID="btnAddCustomer" CssClass="Buttons" style="left: 80px; bottom: 10px; width:60px;" runat="server" Text="Add" ToolTip="Create a new customer" OnClick="btnAddCustomer_Click"  />
            <asp:Button ID="btnUpdateCustomer" CssClass="Buttons" style="left: 150px; bottom: 10px; width:80px;" runat="server" Text="Update" ToolTip="Create a new customer" Enabled="False" OnClick="btnUpdateCustomer_Click"  />
            <asp:Button ID="btnDeleteCustomer" CssClass="Buttons" style="left: 240px; bottom: 10px; width:70px;" runat="server" Text="Delete" ToolTip="Create a new customer" Enabled="False" OnClick="btnDeleteCustomer_Click"  />
            <asp:Button ID="btnFindCustomer" CssClass="Buttons" style="left: 320px; bottom: 10px; width:60px;" runat="server" Text="Find" ToolTip="Find a customer" OnClick="btnFindCustomer_Click"   />
        </asp:Panel>
        <asp:Label ID="lblMessages" runat="server" CssClass="Messages" style="left:10px; top:300px; right:10px; height:16px; background-color:cyan" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>

