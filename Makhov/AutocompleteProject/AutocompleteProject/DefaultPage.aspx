<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultPage.aspx.cs" Inherits="AutocompleteProject.MainForm" %>

<%@ Register Assembly="MyControl" Namespace="MyControl" TagPrefix="mine" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <%-- Can't eliminate hardcode inside <div> tag.
             class="yui3-aclist-input" inside MyControlClass tag?--%>
        <div class="yui3-skin-sam">
            <label for="AutoC">
                Numbers from 0 to 20:
            </label>
            <br />
            <mine:MyControlClass ID="AutoC" runat="server" />
        </div>
    </form>
</body>
</html>
