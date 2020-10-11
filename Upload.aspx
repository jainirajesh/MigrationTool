<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Upload.aspx.cs" Inherits="MigrationTool.WebForm2" %>

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

        .auto-style3 {
            height: 33px;
        }

        .auto-style5 {
            width: 8%;
        }
        .auto-style6 {
            height: 33px;
            width: 8%;
        }

        .auto-style7 {
            height: 34px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="center" class="style1" style="border: none" width="100%">
        <tr>
            <td class="auto-style7"
                style="text-align: center; font-weight: bold; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #008080;" colspan="2">Upload Data</td>
        </tr>
      
        <tr>
            <td style="text-align: left; " class="auto-style5">&nbsp;</td>
            <td style="text-align: left;">
                &nbsp;</td>
        </tr>
      
        <tr>
            <td style="text-align: left; " class="auto-style5">Data Type :</td>
            <td style="text-align: left;">
                <asp:RadioButtonList ID="rdoDataType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rdoDataType_SelectedIndexChanged" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">Users</asp:ListItem>
                    <asp:ListItem>Hosts</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left" class="auto-style6">Select File
                :</td>
            <td style="text-align: left" class="auto-style3">
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left" class="auto-style6"></td>
            <td style="text-align: right" class="auto-style3">
                <asp:TextBox ID="txtSearch" runat="server" />
                <asp:Button Text="Search" ID="btnSearch" runat="server" OnClick="Search" /></td>
        </tr>
    </table>
    <div style="overflow-x: auto; width: 100%">
        <asp:GridView ID="gvExcelFile" runat="server" CssClass="mydatagrid" OnPageIndexChanging="gvExcelFile_PageIndexChanging" PagerStyle-CssClass="pager" OnSorting="gvExcelFile_Sorting"
            OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" HeaderStyle-CssClass="header1"  EmptyDataText="No records has been added."
            AutoGenerateColumns="true" RowStyle-CssClass="rows" AllowPaging="True" AllowSorting="true" PageIndex="10" Width="100%">
            <%--  <Columns>
            <asp:BoundField ItemStyle-Width="150px" DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
            <asp:BoundField ItemStyle-Width="150px" DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
            <asp:BoundField ItemStyle-Width="200px" DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
            <asp:BoundField ItemStyle-Width="150px" DataField="Password" HeaderText="Password" SortExpression="Password" />
            <asp:BoundField ItemStyle-Width="150px" DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField ItemStyle-Width="150px" DataField="CreatedDate" HeaderText="Created Date" SortExpression="CreatedDate" />
        </Columns>--%>
          <Columns>
                <asp:TemplateField ItemStyle-Width="120px" HeaderStyle-Width="120px"  HeaderText="">
                    <EditItemTemplate>
                        <asp:ImageButton runat="server" ImageUrl="~/Images/Update.png" Height="25px" Width="25px" OnClick="OnUpdate" />
                        <asp:ImageButton runat="server" ImageUrl="~/Images/Cancel.png" Height="25px" Width="25px" OnClick="OnCancel" />
                        <%--<asp:LinkButton Text="Update" runat="server" OnClick="OnUpdate" />
                        <asp:LinkButton Text="Cancel" runat="server" OnClick="OnCancel" />--%>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="header1" Wrap="False"></HeaderStyle>
            <PagerStyle CssClass="pager"></PagerStyle>
            <RowStyle CssClass="rows" Wrap="False"></RowStyle>
        </asp:GridView>
        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    </div>
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td style="text-align: right;">
                <asp:Button ID="btnExportToCSV" runat="server" Text="Export to CSV" OnClick="btnExportToCSV_Click" />
                <asp:Button ID="btnDownload" runat="server" Text="Export to Excel" OnClick="btnDownload_Click" />
            </td>
        </tr>
    </table>

</asp:Content>
