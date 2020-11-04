<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" enableEventValidation="false" AutoEventWireup="true" CodeBehind="Relationships.aspx.cs" Inherits="MigrationTool.Relationships" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/Javascript" language="javascript">
        function confirm_meth() {
            if (confirm("Are you sure to commit the data to project database?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
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
            AutoGenerateColumns="False" RowStyle-CssClass="rows" AllowPaging="True" AllowSorting="True" PageSize="20" Width="90%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <Columns>
                <asp:TemplateField ItemStyle-Width="60px" HeaderStyle-Width="60px" HeaderText="">
                    <EditItemTemplate>
                        <asp:ImageButton runat="server" ImageUrl="~/Images/Update.png" Height="25px" Width="25px" OnClick="OnUpdate" />
                        <asp:ImageButton runat="server" ImageUrl="~/Images/Cancel.png" Height="25px" Width="25px" OnClick="OnCancel" />
                    </EditItemTemplate>

<HeaderStyle Width="60px"></HeaderStyle>

<ItemStyle Width="60px"></ItemStyle>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="120px" DataField="Entity1_Name" HeaderText="Entity1 Name" SortExpression="Entity1_Name" ControlStyle-Width="90%" >
<ControlStyle Width="90%"></ControlStyle>

<ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="120px" DataField="Entity1_Type" HeaderText="Entity1 Type" SortExpression="Entity1_Type" ControlStyle-Width="90%" >
<ControlStyle Width="90%"></ControlStyle>

<ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="175px" DataField="Entity2_Name" HeaderText="Entity2 Name" SortExpression="Entity2_Name" ControlStyle-Width="90%" >
<ControlStyle Width="90%"></ControlStyle>

<ItemStyle Width="175px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="120px" DataField="Entity2_Type" HeaderText="Entity2 Type" SortExpression="Entity2_Type" ControlStyle-Width="90%" >
<ControlStyle Width="90%"></ControlStyle>

<ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="150px" DataField="Score" HeaderText="Score" SortExpression="Score" ControlStyle-Width="90%" >
<ControlStyle Width="90%"></ControlStyle>

<ItemStyle Width="150px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="120px" DataField="Migration_Type" HeaderText="Migration Type" SortExpression="Migration_Type" ControlStyle-Width="90%" >
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
    <br />
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label>
                <asp:Button ID="btnCommit" runat="server" Text="Commit Changes" OnClick="btnCommit_Click" OnClientClick=" return confirm_meth()"  CssClass="button7" Style="background-color: crimson"/>
                <asp:Button ID="btnRevert" runat="server" Text="Revert Changes" OnClick="btnRevert_Click" CssClass="button7" Style="background-color: #2979FF"/>
            </td>
            <td style="text-align: right;">
                <asp:Button ID="btnExportToCSV" runat="server" Text="Export to CSV" OnClick="btnExportToCSV_Click" CssClass="button7" Style="background-color: #CCCCCC; color: #000000"/>
                <asp:Button ID="btnDownload" runat="server" Text="Export to Excel" OnClick="btnDownload_Click" CssClass="button7" Style="background-color: #CCCCCC; color: #000000"/>
            </td>
        </tr>
    </table>
</asp:Content>
