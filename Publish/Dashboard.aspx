<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="MigrationTool.Dashboard" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <style>
        .collapsible {
            background-color: #777;
            background-image: url(Images/HeaderBg.JPG);
            color: white;
            cursor: pointer;
            padding: 6px;
            width: 100%;
            border: none;
            text-align: left;
            outline: none;
            font-size: 16px;
            font-family: 'Bookman Old Style';
        }

            .active, .collapsible:hover {
                background-color: #555;
            }

        .content {
            padding: 0 16px;
            display: none;
            overflow: hidden;
            background-color: #f1f1f1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center;">
        <table width="100%">
            <tr>
                <td height="200px;" colspan="2">
                    <asp:Chart ID="Chart2" runat="server">
                        <Series>
                            <asp:Series Name="Series1"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <asp:Chart ID="Chart3" runat="server">
                        <Series>
                            <asp:Series Name="Series1"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>               
                    <asp:Chart ID="Chart1" runat="server">
                        <Series>
                            <asp:Series Name="Series1"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <asp:Chart ID="Chart4" runat="server">
                        <Series>
                            <asp:Series Name="Series1"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </td>
            </tr>
            <tr>
                <td width="50%" style="vertical-align: top;">
                    <button type="button" class="collapsible">Hosts</button>
                    <div class="content" style="display: block; height: 180px;">
                        <p>
                            <asp:GridView ID="Hosts" runat="server" CssClass="mydatagrid" PagerStyle-CssClass="pager"
                                HeaderStyle-CssClass="header1" EmptyDataText="No records to display."
                                AutoGenerateColumns="False" RowStyle-CssClass="rows" Width="100%" OnRowDataBound="OnRowDataBound" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Name" HeaderText="Name" SortExpression="Name" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="OS" HeaderText="OS" SortExpression="OS" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Model" HeaderText="Model" SortExpression="Model" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Environment" HeaderText="Environment" SortExpression="Environment" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle CssClass="header1" Wrap="False" BackColor="#006699" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                <PagerStyle CssClass="pager" BackColor="White" ForeColor="#000066" HorizontalAlign="Left"></PagerStyle>
                                <RowStyle CssClass="rows" Wrap="False" ForeColor="#000066"></RowStyle>
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                            <div style="width: 100%; text-align:right;">
                                <asp:HyperLink ID="lnkHosts" runat="server" Font-Size="Small" Font-Italic="true" NavigateUrl="~/Hosts.aspx">View all Hosts..</asp:HyperLink>
                            </div>
                        </p>
                    </div>
                    <br />
                    <button type="button" class="collapsible">Storage</button>
                    <div class="content" style="display: block; height: 180px;">
                        <p>
                            <asp:GridView ID="Storage" runat="server" CssClass="mydatagrid" PagerStyle-CssClass="pager"
                                HeaderStyle-CssClass="header1" EmptyDataText="No records to display."
                                AutoGenerateColumns="False" RowStyle-CssClass="rows" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Name" HeaderText="Name" SortExpression="Name" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Type" HeaderText="Type" SortExpression="Type" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="175px" DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="175px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Model" HeaderText="Model" SortExpression="Model" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Environment" HeaderText="Environment" SortExpression="Environment" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle CssClass="header1" Wrap="False" BackColor="#006699" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                <PagerStyle CssClass="pager" BackColor="White" ForeColor="#000066" HorizontalAlign="Left"></PagerStyle>
                                <RowStyle CssClass="rows" Wrap="False" ForeColor="#000066"></RowStyle>
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                             <div style="width: 100%; text-align:right;">
                                <asp:HyperLink ID="lnkStorage" runat="server" Font-Size="Small" Font-Italic="true" NavigateUrl="~/Storage.aspx">View all Storages..</asp:HyperLink>
                            </div>
                        </p>
                    </div>
                </td>
                <td style="vertical-align: top;">
                    <button type="button" class="collapsible">Applications</button>
                    <div class="content" style="display: block; height:180px;">
                        <p>
                            <asp:GridView ID="Applications" runat="server" CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header1" EmptyDataText="No records to display."
                                AutoGenerateColumns="False" RowStyle-CssClass="rows" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Name" HeaderText="Name" SortExpression="Name" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Environment" HeaderText="Environment" SortExpression="Environment" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Business_Unit" HeaderText="Business Unit" SortExpression="Business_Unit" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Version" HeaderText="Version" SortExpression="Version" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Business_Criticality" HeaderText="Criticality" SortExpression="Business_Criticality" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle CssClass="header1" Wrap="False" BackColor="#006699" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                <PagerStyle CssClass="pager" BackColor="White" ForeColor="#000066" HorizontalAlign="Left"></PagerStyle>
                                <RowStyle CssClass="rows" Wrap="False" ForeColor="#000066"></RowStyle>
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                             <div style="width: 100%; text-align:right;">
                                <asp:HyperLink ID="lnkApplications" runat="server" Font-Size="Small" Font-Italic="true" NavigateUrl="~/Applications.aspx">View all Applications..</asp:HyperLink>
                            </div>
                        </p>
                    </div>
                    <br />
                    <button type="button" class="collapsible">Databases</button>
                    <div class="content" style="display: block; height: 180px;">
                        <p>
                            <asp:GridView ID="Databases" runat="server" CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header1" EmptyDataText="No records to display."
                                AutoGenerateColumns="False" RowStyle-CssClass="rows" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Name" HeaderText="Name" SortExpression="Name" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="DB_Type" HeaderText="DB Type" SortExpression="DB_Type" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="DB_Server_Name" HeaderText="DB Server Name" SortExpression="DB_Server_Name" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="DB_Size_GB" HeaderText="DB Size GB" SortExpression="DB_Size_GB" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="120px" DataField="Environment" HeaderText="Environment" SortExpression="Environment" ControlStyle-Width="90%">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle CssClass="header1" Wrap="False" BackColor="#006699" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                <PagerStyle CssClass="pager" BackColor="White" ForeColor="#000066" HorizontalAlign="Left"></PagerStyle>
                                <RowStyle CssClass="rows" Wrap="False" ForeColor="#000066"></RowStyle>
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                             <div style="width: 100%; text-align:right;">
                                <asp:HyperLink ID="lnkDatabases" runat="server" Font-Size="Small" Font-Italic="true" NavigateUrl="~/Databases.aspx">View all Databases..</asp:HyperLink>
                            </div>
                        </p>
                    </div>
                </td>
            </tr>
            
            <tr>
                <td></td>
                <td></td>
            </tr>
        </table>
    </div>
    <script>
        var coll = document.getElementsByClassName("collapsible");
        var i;

        for (i = 0; i < coll.length; i++) {
            coll[i].addEventListener("click", function () {
                this.classList.toggle("active");
                var content = this.nextElementSibling;
                if (content.style.display === "block") {
                    content.style.display = "none";
                } else {
                    content.style.display = "block";
                }
            });
        }
    </script>
</asp:Content>
