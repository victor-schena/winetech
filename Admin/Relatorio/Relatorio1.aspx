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


<form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnableCdn="True" EnableHistory="True" EnablePageMethods="True" EnableScriptGlobalization="True">
    </asp:ScriptManager>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Chart ID="Chart1" runat="server" BackImageTransparentColor="Silver" BorderlineColor="Black" DataSourceID="SqlDataSource2" EnableViewState="True" ImageStorageMode="UseImageLocation" Width="433px" Height="346px" SuppressExceptions="True">
        <series>
            <asp:Series ChartArea="ChartArea2" ChartType="StackedColumn" Name="Valores" XValueMember="Expr1" YValueMembers="Expr2" IsXValueIndexed="True" Legend="Legend1">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea2">
                <Area3DStyle Enable3D="True" />
            </asp:ChartArea>
        </chartareas>
        <Legends>
            <asp:Legend LegendStyle="Column" Name="Legend1">
            </asp:Legend>
        </Legends>
        <Annotations>
            <asp:LineAnnotation AxisXName="ChartArea2\rX" ClipToChartArea="ChartArea2" Name="LineAnnotation1" YAxisName="ChartArea2\rY">
            </asp:LineAnnotation>
        </Annotations>
    </asp:Chart>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT CAST(DataPedido AS DATE) AS Expr1, SUM(Total) AS Expr2 FROM Pedido WHERE (CAST(DataPedido AS DATE) = CAST(DataPedido AS DATE)) GROUP BY CAST(DataPedido AS DATE)"></asp:SqlDataSource>
</form>


