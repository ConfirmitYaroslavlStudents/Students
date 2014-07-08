<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AutocompleteProject.MainForm" %>

<%@ Register Assembly="AutocompleteControl" Namespace="AutocompleteControl" TagPrefix="ac" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form" runat="server">
        <label for="acNumbers">
            Numbers from 0 to 20:
        </label>
        <br />
        <ac:AutocompleteClass ID="acNumbers" runat="server" />

        <br />
        <br />
        <br />

        <label for="acColours">
            Some colours:
        </label>
        <br />
        <ac:AutocompleteClass ID="acColours" runat="server" />

        <br />
        <br />
        <br />

        <asp:Button ID="GoToAJAXpage" OnClick="GoToAJAXpage_Click" runat="server" Text="Go To The AJAX Page" />
    </form>
</body>
</html>
