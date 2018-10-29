
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Relat.aspx.cs" Inherits="Admin.Relat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style2 {
            width: 518px;
        }
    </style>
</head>
<body>
    <form>
        <table>
            <asp:Chart runat="server">
                <series><asp:Series Name="Series1"></asp:Series></series>
                <chartareas><asp:ChartArea Name="ChartArea1"></asp:ChartArea></chartareas>
            </asp:Chart>
        </table>
    </form>
 </body>
</html>

