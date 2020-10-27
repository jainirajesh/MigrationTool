<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" enableEventValidation="false" AutoEventWireup="true" CodeBehind="Relationships.aspx.cs" Inherits="MigrationTool.Relationships" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header2">Relationships</div>
    <table align="center" style="border: none;" width="100%">
        <tr>
            <td style="text-align: left"></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtSearch" runat="server" />
                <asp:Button Text="Search" ID="btnSearch" runat="server" OnClick="Search" /></td>
        </tr>
    </table>
    <div style="overflow-x: auto; width: 100%; padding-top: 20px;">
        <asp:GridView ID="gvExcelFile" runat="server" CssClass="mydatagrid" OnPageIndexChanging="gvExcelFile_PageIndexChanging" PagerStyle-CssClass="pager" OnSorting="gvExcelFile_Sorting"
            OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" HeaderStyle-CssClass="header1" EmptyDataText="No records has been added."
            AutoGenerateColumns="false" RowStyle-CssClass="rows" AllowPaging="True" AllowSorting="true" PageSize="20" Width="90%">
            <Columns>
                <asp:TemplateField ItemStyle-Width="60px" HeaderStyle-Width="60px" HeaderText="">
                    <EditItemTemplate>
                        <asp:ImageButton runat="server" ImageUrl="~/Images/Update.png" Height="25px" Width="25px" OnClick="OnUpdate" />
                        <asp:ImageButton runat="server" ImageUrl="~/Images/Cancel.png" Height="25px" Width="25px" OnClick="OnCancel" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="120px" DataField="Entity1_Name" HeaderText="Entity1 Name" SortExpression="Entity1_Name" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Entity1_Type" HeaderText="Entity1 Type" SortExpression="Entity1_Type" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="175px" DataField="Entity2_Name" HeaderText="Entity2 Name" SortExpression="Entity2_Name" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Entity2_Type" HeaderText="Entity2 Type" SortExpression="Entity2_Type" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="150px" DataField="Score" HeaderText="Score" SortExpression="Score" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Migration_Type" HeaderText="Migration Type" SortExpression="Migration_Type" ControlStyle-Width="90%" />
            </Columns>
            <HeaderStyle CssClass="header1" Wrap="False"></HeaderStyle>
            <PagerStyle CssClass="pager"></PagerStyle>
            <RowStyle CssClass="rows" Wrap="False"></RowStyle>
        </asp:GridView>
        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    </div>
    <br />
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
