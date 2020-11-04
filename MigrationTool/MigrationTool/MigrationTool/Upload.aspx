<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="MigrationTool.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }

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
                background-color: #fadd7a;
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
            background-color: #fadd7a;
            font-family: Arial;
            color: White;
            height: 20px;
            text-align: left;
        }

        .mydatagrid td {
            padding: 5px;
        }

        .mydatagrid th {
            padding: 5px;
        }

        .auto-style2 {
            height: 39px;
        }

        .auto-style3 {
            height: 33px;
        }

        .auto-style4 {
            height: 31px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="center" class="style1" style="border: none" width="100%">
        <tr>
            <td class="style2"
                style="text-align: center; font-weight: bold; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #008080;" colspan="2">Upload Data</td>
        </tr>
        <tr>
            <td style="text-align: left; width: 20%">Data Type</td>
            <td style="text-align: left;">
                <asp:DropDownList ID="drpDataType" runat="server">
                    <%--<asp:ListItem>--Select--</asp:ListItem>--%>
                    <asp:ListItem>Users List</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left" class="auto-style3">Select File
            </td>
            <td style="text-align: left" class="auto-style3">
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left" class="auto-style3"></td>
            <td style="text-align: right" class="auto-style3">
                <asp:TextBox ID="txtSearch" runat="server" />
                <asp:Button Text="Search" ID="btnSearch" runat="server" OnClick="Search" /></td>
        </tr>
    </table>

    <asp:GridView ID="gvExcelFile" runat="server" CssClass="mydatagrid" OnPageIndexChanging="gvExcelFile_PageIndexChanging" PagerStyle-CssClass="pager" OnSorting="gvExcelFile_Sorting"
        HeaderStyle-CssClass="header1" AutoGenerateColumns="false" RowStyle-CssClass="rows" AllowPaging="True" AllowSorting="true" PageIndex="10" Width="100%">
        <Columns>
            <asp:BoundField ItemStyle-Width="150px" DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
            <asp:BoundField ItemStyle-Width="150px" DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
            <asp:BoundField ItemStyle-Width="200px" DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
            <asp:BoundField ItemStyle-Width="150px" DataField="Password" HeaderText="Password" SortExpression="Password" />
            <asp:BoundField ItemStyle-Width="150px" DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField ItemStyle-Width="150px" DataField="CreatedDate" HeaderText="Created Date" SortExpression="CreatedDate" />
        </Columns>
    </asp:GridView>
    <table width="100%">
        <tr>
            <td>
                 <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td style="text-align: right;">
                <asp:Button ID="btnExportToCSV" runat="server" Text="Export to CSV" OnClick="btnExportToCSV_Click" /> <asp:Button ID="btnDownload" runat="server" Text="Export to Excel" OnClick="btnDownload_Click" />
            </td>
        </tr>
    </table>
   
</asp:Content>
