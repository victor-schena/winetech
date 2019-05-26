<%@ Page language="C#" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

    }

</script>


<meta http-equiv="X-UA-Compatible" content="IE=edge" >
    <meta charset=" character_set "/> 
    <meta charset="utf-8" /> 
    <style type="text/css">
        #form1 {
            height: 385px;
            width: 608px;
        }
    </style>


<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="True" EnableHistory="True" EnablePageMethods="True" EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Nome], [Quantidade] FROM [Produto]"></asp:SqlDataSource>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Chart ID="Chart1" runat="server" BackImageTransparentColor="Silver" BorderlineColor="Black" DataSourceID="SqlDataSource1" EnableViewState="True" ImageStorageMode="UseImageLocation" Width="347px">
        <series>
            <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Name="Series1" XValueMember="Nome" YValueMembers="Quantidade">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
                <Area3DStyle Enable3D="True" />
            </asp:ChartArea>
        </chartareas>
    </asp:Chart>
</form>


