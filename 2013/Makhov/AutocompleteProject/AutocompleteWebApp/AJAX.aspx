<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AJAX.aspx.cs" Inherits="AutocompleteProject.AJAX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form" runat="server">
        <div>
            <script type="text/javascript">
                function makeRequest(url) {
                    var httpRequest;

                    if (window.XMLHttpRequest) { // Mozilla, Safari, ...
                        httpRequest = new XMLHttpRequest();
                        if (httpRequest.overrideMimeType) {
                            httpRequest.overrideMimeType('text/xml');
                            // See note below about this line
                        }
                    }
                    else if (window.ActiveXObject) { // IE
                        try {
                            httpRequest = new ActiveXObject("Msxml2.XMLHTTP");
                        }
                        catch (e) {
                            try {
                                httpRequest = new ActiveXObject("Microsoft.XMLHTTP");
                            }
                            catch (e) { }
                        }
                    }

                    if (!httpRequest) {
                        alert('Giving up :( Cannot create an XMLHTTP instance');
                        return false;
                    }
                    httpRequest.onreadystatechange = function () { alertContents(httpRequest); };
                    httpRequest.open('GET', url, true);
                    httpRequest.send('');
                }

                function alertContents(httpRequest) {
                    if (httpRequest.readyState == 4) {
                        if (httpRequest.status == 200) {
                            var xmldoc = httpRequest.responseXML;
                            var root_node = xmldoc.getElementsByTagName('root').item(0);
                            alert(root_node.firstChild.data);
                        } else {
                            alert('There was a problem with the request.');
                        }
                    }
                }
            </script>
            
            <span style="cursor: pointer; text-decoration: underline" onclick="makeRequest('test.xml')">Make a request</span>
        </div>

        <br />
        <br />
        <br />

        <asp:Button ID="GoToDefaultPage" OnClick="GoToDefaultPage_Click" runat="server" Text="Go To The Default Page" />
    </form>
</body>
</html>
