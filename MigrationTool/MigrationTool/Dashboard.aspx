<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="MigrationTool.Dashboard" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <style>
        @media (min-width:768px) {
            .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9 {
                float: left
            }

            .col-sm-12 {
                width: 100%
            }

            .col-sm-11 {
                width: 91.66666667%
            }

            .col-sm-10 {
                width: 83.33333333%
            }

            .col-sm-9 {
                width: 75%
            }

            .col-sm-8 {
                width: 66.66666667%
            }

            .col-sm-7 {
                width: 58.33333333%
            }

            .col-sm-6 {
                width: 50%
            }

            .col-sm-5 {
                width: 41.66666667%
            }

            .col-sm-4 {
                width: 33.33333333%
            }

            .col-sm-3 {
                width: 25%
            }

            .col-sm-2 {
                width: 16.66666667%
            }

            .col-sm-1 {
                width: 8.33333333%
            }

            .col-sm-pull-12 {
                right: 100%
            }

            .col-sm-pull-11 {
                right: 91.66666667%
            }

            .col-sm-pull-10 {
                right: 83.33333333%
            }

            .col-sm-pull-9 {
                right: 75%
            }

            .col-sm-pull-8 {
                right: 66.66666667%
            }

            .col-sm-pull-7 {
                right: 58.33333333%
            }

            .col-sm-pull-6 {
                right: 50%
            }

            .col-sm-pull-5 {
                right: 41.66666667%
            }

            .col-sm-pull-4 {
                right: 33.33333333%
            }

            .col-sm-pull-3 {
                right: 25%
            }

            .col-sm-pull-2 {
                right: 16.66666667%
            }

            .col-sm-pull-1 {
                right: 8.33333333%
            }

            .col-sm-pull-0 {
                right: auto
            }

            .col-sm-push-12 {
                left: 100%
            }

            .col-sm-push-11 {
                left: 91.66666667%
            }

            .col-sm-push-10 {
                left: 83.33333333%
            }

            .col-sm-push-9 {
                left: 75%
            }

            .col-sm-push-8 {
                left: 66.66666667%
            }

            .col-sm-push-7 {
                left: 58.33333333%
            }

            .col-sm-push-6 {
                left: 50%
            }

            .col-sm-push-5 {
                left: 41.66666667%
            }

            .col-sm-push-4 {
                left: 33.33333333%
            }

            .col-sm-push-3 {
                left: 25%
            }

            .col-sm-push-2 {
                left: 16.66666667%
            }

            .col-sm-push-1 {
                left: 8.33333333%
            }

            .col-sm-push-0 {
                left: auto
            }

            .col-sm-offset-12 {
                margin-left: 100%
            }

            .col-sm-offset-11 {
                margin-left: 91.66666667%
            }

            .col-sm-offset-10 {
                margin-left: 83.33333333%
            }

            .col-sm-offset-9 {
                margin-left: 75%
            }

            .col-sm-offset-8 {
                margin-left: 66.66666667%
            }

            .col-sm-offset-7 {
                margin-left: 58.33333333%
            }

            .col-sm-offset-6 {
                margin-left: 50%
            }

            .col-sm-offset-5 {
                margin-left: 41.66666667%
            }

            .col-sm-offset-4 {
                margin-left: 33.33333333%
            }

            .col-sm-offset-3 {
                margin-left: 25%
            }

            .col-sm-offset-2 {
                margin-left: 16.66666667%
            }

            .col-sm-offset-1 {
                margin-left: 8.33333333%
            }

            .col-sm-offset-0 {
                margin-left: 0
            }
        }

        .col-xs-1, .col-xs-10, .col-xs-11, .col-xs-12, .col-xs-2, .col-xs-3, .col-xs-4, .col-xs-5, .col-xs-6, .col-xs-7, .col-xs-8, .col-xs-9 {
            float: left
        }

        .mini-stat {
            padding: 15px;
            margin-bottom: 20px;
        }

        .mini-stat-icon {
            width: 60px;
            height: 60px;
            display: inline-block;
            line-height: 60px;
            text-align: center;
            font-size: 30px;
            background: none repeat scroll 0% 0% #EEE;
            border-radius: 100%;
            float: left;
            margin-right: 10px;
            color: #FFF;
        }

        .mini-stat-info {
            font-size: 12px;
            padding-top: 2px;
        }


        .rounded {
            -webkit-border-radius: 3px !important;
            -moz-border-radius: 3px !important;
            border-radius: 3px !important;
        }

        .img-rounded {
            border-radius: 6px
        }

        .bg-twitter {
            background-color: #00a0d1 !important;
            border: 1px solid #00a0d1;
            color: white;
        }

        .bg-facebook {
            background-color: #3b5998 !important;
            border: 1px solid #3b5998;
            color: white;
        }

        .fg-twitter {
            color: #00a0d1 !important;
        }

        .fg-facebook {
            color: #3b5998 !important;
        }

        .mini-stat-info span {
            display: block;
            font-size: 30px;
            font-weight: 600;
            margin-bottom: 5px;
            margin-top: 7px;
        }

        .col-xs-12 {
            width: 100%
        }

        @media (min-width:992px) {
            .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9 {
                float: left
            }

            .col-md-12 {
                width: 100%
            }

            .col-md-11 {
                width: 91.66666667%
            }

            .col-md-10 {
                width: 83.33333333%
            }

            .col-md-9 {
                width: 75%
            }

            .col-md-8 {
                width: 66.66666667%
            }

            .col-md-7 {
                width: 58.33333333%
            }

            .col-md-6 {
                width: 50%
            }

            .col-md-5 {
                width: 41.66666667%
            }

            .col-md-4 {
                width: 33.33333333%
            }

            .col-md-3 {
                width: 25%
            }

            .col-md-2 {
                width: 16.66666667%
            }

            .col-md-1 {
                width: 8.33333333%
            }

            .col-md-pull-12 {
                right: 100%
            }

            .col-md-pull-11 {
                right: 91.66666667%
            }

            .col-md-pull-10 {
                right: 83.33333333%
            }

            .col-md-pull-9 {
                right: 75%
            }

            .col-md-pull-8 {
                right: 66.66666667%
            }

            .col-md-pull-7 {
                right: 58.33333333%
            }

            .col-md-pull-6 {
                right: 50%
            }

            .col-md-pull-5 {
                right: 41.66666667%
            }

            .col-md-pull-4 {
                right: 33.33333333%
            }

            .col-md-pull-3 {
                right: 25%
            }

            .col-md-pull-2 {
                right: 16.66666667%
            }

            .col-md-pull-1 {
                right: 8.33333333%
            }

            .col-md-pull-0 {
                right: auto
            }

            .col-md-push-12 {
                left: 100%
            }

            .col-md-push-11 {
                left: 91.66666667%
            }

            .col-md-push-10 {
                left: 83.33333333%
            }

            .col-md-push-9 {
                left: 75%
            }

            .col-md-push-8 {
                left: 66.66666667%
            }

            .col-md-push-7 {
                left: 58.33333333%
            }

            .col-md-push-6 {
                left: 50%
            }

            .col-md-push-5 {
                left: 41.66666667%
            }

            .col-md-push-4 {
                left: 33.33333333%
            }

            .col-md-push-3 {
                left: 25%
            }

            .col-md-push-2 {
                left: 16.66666667%
            }

            .col-md-push-1 {
                left: 8.33333333%
            }

            .col-md-push-0 {
                left: auto
            }

            .col-md-offset-12 {
                margin-left: 100%
            }

            .col-md-offset-11 {
                margin-left: 91.66666667%
            }

            .col-md-offset-10 {
                margin-left: 83.33333333%
            }

            .col-md-offset-9 {
                margin-left: 75%
            }

            .col-md-offset-8 {
                margin-left: 66.66666667%
            }

            .col-md-offset-7 {
                margin-left: 58.33333333%
            }

            .col-md-offset-6 {
                margin-left: 50%
            }

            .col-md-offset-5 {
                margin-left: 41.66666667%
            }

            .col-md-offset-4 {
                margin-left: 33.33333333%
            }

            .col-md-offset-3 {
                margin-left: 25%
            }

            .col-md-offset-2 {
                margin-left: 16.66666667%
            }

            .col-md-offset-1 {
                margin-left: 8.33333333%
            }

            .col-md-offset-0 {
                margin-left: 0
            }
        }

        .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-xs-1, .col-xs-10, .col-xs-11, .col-xs-12, .col-xs-2, .col-xs-3, .col-xs-4, .col-xs-5, .col-xs-6, .col-xs-7, .col-xs-8, .col-xs-9 {
            position: relative;
            min-height: 1px;
            /*padding-right: 15px;*/
            padding-left: 15px
        }








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
    <div style="text-align: center; width: 100%">
        <table width="100%" style="text-align: center;">
            <tr>
                <td width="1%"></td>
                <td>
                    <div class="col-md-3 col-sm-6 col-xs-12" style="width: 250px;">
                        <div class="mini-stat clearfix rounded" style="background-color: coral">
                            <span class="mini-stat-icon"><i class="fa fa-facebook fg-facebook">S</i></span>
                            <div class="mini-stat-info">
                                <span>
                                    <asp:Label ID="lblServers" runat="server" Text="0" Font-Bold="true" Font-Size="Large"></asp:Label></span>
                                <asp:Label ID="Label3" runat="server" Text="Servers" Font-Bold="true" Font-Size="Large"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12" style="width: 250px;">
                        <div class="mini-stat clearfix bg-facebook rounded">
                            <span class="mini-stat-icon"><i class="fa fa-facebook fg-facebook">A</i></span>
                            <div class="mini-stat-info">
                                <span>
                                    <asp:Label ID="lblApplications" runat="server" Text="0" Font-Bold="true" Font-Size="Large"></asp:Label></span>
                                <asp:Label ID="Label2" runat="server" Text="Applications" Font-Bold="true" Font-Size="Large"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12" style="width: 250px;">
                        <div class="mini-stat clearfix bg-twitter rounded">
                            <span class="mini-stat-icon"><i class="fa fa-facebook fg-facebook">S</i></span>
                            <div class="mini-stat-info">
                                <span>
                                    <asp:Label ID="lblStorage" runat="server" Text="0" Font-Bold="true" Font-Size="Large"></asp:Label></span>
                                <asp:Label ID="Label1" runat="server" Text="Storage" Font-Bold="true" Font-Size="Large"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12" style="width: 250px;">
                        <div class="mini-stat clearfix rounded" style="background-color: coral">
                            <span class="mini-stat-icon"><i class="fa fa-facebook fg-facebook">D</i></span>
                            <div class="mini-stat-info">
                                <span>
                                    <asp:Label ID="lblDatabases" runat="server" Text="0" Font-Bold="true" Font-Size="Large"></asp:Label></span>
                                <asp:Label ID="lbl" runat="server" Text="Databases" Font-Bold="true" Font-Size="Large"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12" style="width: 250px;">
                        <div class="mini-stat clearfix bg-facebook rounded">
                            <span class="mini-stat-icon"><i class="fa fa-twitter fg-twitter">D</i></span>
                            <div class="mini-stat-info">
                                <span>
                                    <asp:Label ID="lblDependencies" runat="server" Text="0" Font-Bold="true" Font-Size="Large"></asp:Label></span>
                                <asp:Label ID="Label4" runat="server" Text="Dependencies" Font-Bold="true" Font-Size="Large"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12" style="width: 250px;">
                        <div class="mini-stat clearfix bg-twitter rounded">
                            <span class="mini-stat-icon"><i class="fa fa-twitter fg-twitter">N</i></span>
                            <div class="mini-stat-info">
                                <span>
                                    <asp:Label ID="lblNetworks" runat="server" Text="0" Font-Bold="true" Font-Size="Large"></asp:Label></span>
                                <asp:Label ID="Label5" runat="server" Text="Networks" Font-Bold="true" Font-Size="Large"></asp:Label>
                            </div>
                        </div>
                    </div>
                </td>
                <td width="1%"></td>
            </tr>
        </table>
    </div>
    <div style="text-align: center;">
        <table width="100%">
            <tr>
                <td height="200px;" colspan="2" style="display: none;">
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
                    <button type="button" class="collapsible">Servers</button>
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
                            <div style="width: 100%; text-align: right;">
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
                            <div style="width: 100%; text-align: right;">
                                <asp:HyperLink ID="lnkStorage" runat="server" Font-Size="Small" Font-Italic="true" NavigateUrl="~/Storage.aspx">View all Storages..</asp:HyperLink>
                            </div>
                        </p>
                    </div>
                </td>
                <td style="vertical-align: top;">
                    <button type="button" class="collapsible">Applications</button>
                    <div class="content" style="display: block; height: 180px;">
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
                            <div style="width: 100%; text-align: right;">
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
                            <div style="width: 100%; text-align: right;">
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
