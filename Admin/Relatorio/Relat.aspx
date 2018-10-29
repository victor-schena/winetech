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
    <form id="form1" runat="server">
   <asp:SqlDataSource ID="sql3" runat="server" ConnectionString="<%$ ConnectionStrings:bpConnectionString %>" SelectCommand="SELECT [Total], [PessoaId] FROM [Pedido] ORDER BY [PessoaId]"></asp:SqlDataSource>
   <asp:SqlDataSource ID="sql4" runat="server" ConnectionString="<%$ ConnectionStrings:bpConnectionString %>" SelectCommand="SELECT [DataPedido], [Total] FROM [Pedido] ORDER BY [DataPedido]"></asp:SqlDataSource>
    
       
        
              <table style="width:100%">
                  <tr>
                      <td class="auto-style2">
                          <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:bpConnectionString %>" SelectCommand="SELECT [CustoUnitario], [Nome] FROM [Produto] ORDER BY [Nome]"></asp:SqlDataSource>
                          <asp:Chart ID="Chart1" runat="server" DataSourceID="SqlDataSource" Width="501px" BackColor="WhiteSmoke" Palette="Excel" Height="279px" style="margin-right: 15px" CssClass="auto-style3" BackGradientStyle="Center" BackHatchStyle="DarkDownwardDiagonal" ImageStorageMode="UseImageLocation">
                            <Series>
                                <asp:Series Name="Produto" XValueMember="Nome" YValueMembers="CustoUnitario" Legend="Legend1">
                                 </asp:Series>
                             </Series>
                         <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BackColor="White" BackGradientStyle="Center" ShadowColor="Black">
                             <Area3DStyle Enable3D="True" />
                             </asp:ChartArea>
                         </ChartAreas>
                        <Legends>
                            <asp:Legend BackColor="White" BackGradientStyle="Center" Name="Legend1" Title="Custo Unitario">
                             </asp:Legend>
                         </Legends>
                        
                         </asp:Chart>
                      </td>
                       <td>
<asp:SqlDataSource ID="Sql2" runat="server" ConnectionString="<%$ ConnectionStrings:bpConnectionString %>" SelectCommand="SELECT [Quantidade], [Nome] FROM [Produto] ORDER BY [Nome]"></asp:SqlDataSource>
   
                        <asp:Chart ID="Chart3" runat="server" Height="284px" CssClass="auto-style1" DataSourceID="Sql2" style="margin-left: 0px" Width="649px" BackColor="WhiteSmoke" BackGradientStyle="Center" BackHatchStyle="DarkDownwardDiagonal" Palette="Excel" ImageStorageMode="UseImageLocation">
                            <Series>
                                <asp:Series Name="Itens" ChartType="Line" XValueMember="Nome" YValueMembers="Quantidade">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                 <Area3DStyle Enable3D="True" />
                                </asp:ChartArea>
                            </ChartAreas>
                        <Legends>
                            <asp:Legend BackColor="White" BackGradientStyle="Center" Name="Legend1" Title="Quantidade de Itens por Produto">
                             </asp:Legend>
                         </Legends>
                        </asp:Chart>
                      </td>
                     
                  </tr>
<tr>
                      <td class="auto-style2">
                        <asp:Chart ID="Chart2" runat="server" DataSourceID="sql3" Height="323px" style="margin-top: 44px" Width="490px" BackColor="WhiteSmoke" BackGradientStyle="Center" BackHatchStyle="DarkDownwardDiagonal" Palette="Excel" ImageStorageMode="UseImageLocation">
                            <Series>
                                <asp:Series Name="Series1" ChartType="Pie" XValueMember="PessoaId" YValueMembers="Total">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <Area3DStyle Enable3D="True" />
                                </asp:ChartArea>
                            </ChartAreas>
                          <Legends>
                                 <asp:Legend BackColor="White" BackGradientStyle="Center" Name="Legend1" Title="Total de Pedido por Pessoa">
                             </asp:Legend>
                         </Legends>
                          </asp:Chart>
                      </td>
                      <td>
                          <asp:Chart ID="Chart4" runat="server" DataSourceID="sql4" CssClass="auto-style3" Width="657px" BackColor="WhiteSmoke" BackGradientStyle="DiagonalRight" BackHatchStyle="DarkDownwardDiagonal" Compression="50" Height="323px" IsMapAreaAttributesEncoded="True" Palette="Excel" PaletteCustomColors="SlateBlue; Blue; Lime" style="margin-left: 0px; margin-top: 0px" ImageStorageMode="UseImageLocation">
                              <Series>
                                  <asp:Series Name="Total" ChartType="StackedBar" XValueMember="DataPedido" YValueMembers="Total"></asp:Series>
                              </Series>
                              <ChartAreas>
                                  <asp:ChartArea Name="ChartArea1">
                                      <Area3DStyle Enable3D="True" />
                                  </asp:ChartArea>
                              </ChartAreas>
                                <Legends>
                                     <asp:Legend BackColor="White" BackGradientStyle="Center" Name="Legend1" Title="Total de Pedido por Período">
                                 </asp:Legend>
                            </Legends>
                              <BorderSkin BackColor="DarkRed" />
                          </asp:Chart>                  
                      </td>
                  </tr>                
               </table>
       
        
    </form>
</body>
</html>

