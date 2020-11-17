<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Users.aspx.cs" Inherits="MigrationTool.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .mydatagrid {
            width: 100%;
            border: solid 2px black;
            min-width: 100%;
        }

        .header1 {
            background-color: #6a6a6a;
            font-family: Arial;
            color: White;
            height: 20px;
            text-align: center;
            font-size: 14px;
        }

        .rows {
            background-color: #fff;
            font-family: Arial;
            font-size: 12px;
            color: #000;
            min-height: 15px;
            text-align: left;
        }

            .rows:hover {
                background-color: darkgray;
                color: #000;
            }

        .mydatagrid a /** FOR THE PAGING ICONS **/ {
            background-color: Transparent;
            padding: 3px 3px 3px 3px;
            color: #fff;
            text-decoration: none;
            font-weight: bold;
        }

            .mydatagrid a:hover /** FOR THE PAGING ICONS HOVER STYLES**/ {
                background-color: #fff;
                color: #000;
            }

        .mydatagrid span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
            padding: 5px 5px 5px 5px;
            background-color: #000;
            color: #fff;
        }

        .pager {
            background-color: darkgray;
            font-family: Arial;
            color: White;
            height: 15px;
            text-align: left;
        }

        .mydatagrid td {
            padding: 5px;
        }

        .mydatagrid th {
            padding: 5px;
        }

        .auto-style1 {
            width: 120px;
            height: 31px;
        }

        .auto-style2 {
            height: 31px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header2">Users</div>
    <table align="center" style="border: none" width="100%">
        <tr>
            <td style="text-align: left;" class="auto-style1"></td>
            <td style="text-align: left;" class="auto-style2"></td>
        </tr>
        <tr>
            <td style="text-align: left"></td>
            <td style="text-align: right" class="auto-style3">
                <asp:TextBox ID="txtSearch" runat="server" />
                <asp:Button Text="Search" ID="btnSearch" runat="server" OnClick="Search" /></td>
        </tr>
    </table>
    <div style="overflow-x: auto; width: 100%">
        <asp:GridView ID="gvExcelFile" runat="server" CssClass="mydatagrid" OnPageIndexChanging="gvExcelFile_PageIndexChanging" PagerStyle-CssClass="pager" OnSorting="gvExcelFile_Sorting"
            OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" HeaderStyle-CssClass="header1" EmptyDataText="No records to display."
            AutoGenerateColumns="False" RowStyle-CssClass="rows" AllowPaging="True" AllowSorting="True" PageSize="100" Width="90%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <Columns>
                <asp:TemplateField ItemStyle-Width="60px" HeaderStyle-Width="60px" HeaderText="">
                    <EditItemTemplate>
                        <asp:ImageButton runat="server" ImageUrl="~/Images/Update.png" Height="25px" Width="25px" OnClick="OnUpdate" />
                        <asp:ImageButton runat="server" ImageUrl="~/Images/Cancel.png" Height="25px" Width="25px" OnClick="OnCancel" />
                    </EditItemTemplate>

<HeaderStyle Width="60px"></HeaderStyle>

<ItemStyle Width="60px"></ItemStyle>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="120px" DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" ControlStyle-Width="90%" >
<ControlStyle Width="90%"></ControlStyle>

<ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="120px" DataField="LastName" HeaderText="Last Name" SortExpression="LastName" ControlStyle-Width="90%" >
<ControlStyle Width="90%"></ControlStyle>

<ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="175px" DataField="UserName" HeaderText="User Name" SortExpression="UserName" ControlStyle-Width="90%" >
<ControlStyle Width="90%"></ControlStyle>

<ItemStyle Width="175px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="120px" DataField="Password" HeaderText="Password" SortExpression="Password" ControlStyle-Width="90%" >
<ControlStyle Width="90%"></ControlStyle>

<ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="150px" DataField="Email" HeaderText="Email" SortExpression="Email" ControlStyle-Width="90%" >
<ControlStyle Width="90%"></ControlStyle>

<ItemStyle Width="150px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="120px" DataField="UserRole" HeaderText="User Role" SortExpression="UserRole" ControlStyle-Width="90%" >
<ControlStyle Width="90%"></ControlStyle>

<ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="120px" DataField="CreatedDate" HeaderText="Created Date" SortExpression="CreatedDate" ControlStyle-Width="90%" >
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
        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    </div>
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td style="text-align: right;">
                <asp:Button ID="btnExportToCSV" runat="server" Text="Export to CSV" OnClick="btnExportToCSV_Click" CssClass="button7" Style="background-color: #CCCCCC; color: #000000"/>
                <asp:Button ID="btnDownload" runat="server" Text="Export to Excel" OnClick="btnDownload_Click" CssClass="button7" Style="background-color: #CCCCCC; color: #000000"/>
            </td>
        </tr>
    </table>
</asp:Content>
