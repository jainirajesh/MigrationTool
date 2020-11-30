<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Upload.aspx.cs" Inherits="MigrationTool.Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/Javascript" language="javascript">
        function confirm_meth() {
            if (confirm("Are you sure to upload the data to project database?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header2">Upload Data</div>
    <table align="center" style="border: none" width="100%">
        <tr>
            <td style="text-align: left; width: 120px;">&nbsp;</td>
            <td style="text-align: left;">&nbsp;</td>
            <td style="text-align: left;">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left; vertical-align: top;">Data Type :</td>
            <td style="text-align: left;">
                <asp:RadioButtonList ID="rdoDataType" AutoPostBack="true" OnSelectedIndexChanged="rdoDataType_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">Servers</asp:ListItem>
                    <asp:ListItem>Applications</asp:ListItem>
                    <asp:ListItem>Storage</asp:ListItem>
                    <asp:ListItem>Databases</asp:ListItem>
                    <asp:ListItem>Dependencies</asp:ListItem>
                    <asp:ListItem>Users</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="text-align: left;">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left;">&nbsp;</td>
            <td style="text-align: left;">&nbsp;</td>
            <td style="text-align: left;">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left">Select File
                :</td>
            <td style="text-align: left" class="auto-style3">
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
            <td style="text-align: left" class="auto-style3">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left">&nbsp;</td>
            <td style="text-align: left" class="auto-style3">&nbsp;</td>
            <td style="text-align: left" class="auto-style3">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left">&nbsp;</td>
            <td style="text-align: left" class="auto-style3">
                <asp:Button ID="btnFetchData" runat="server" Text="Fetch Data" CssClass="button7" Style="background-color: #2979FF" OnClick="btnFetchData_Click" OnClientClick="javascript:ShowProgressBar()"/>&nbsp;&nbsp;
                 &nbsp;&nbsp;
                <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" CssClass="button7" Style="background-color: crimson" OnClientClick=" return confirm_meth()" Text="Upload" />
            </td>
            <td style="text-align: right" class="auto-style3">
                <asp:TextBox ID="txtSearch" runat="server" />
                <asp:Button Text="Search" ID="btnSearch" runat="server" OnClick="Search" /></td>
        </tr>
    </table>
    <div style="overflow-x: auto; width: 100%">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvExcelFile" runat="server" CssClass="mydatagrid" OnPageIndexChanging="gvExcelFile_PageIndexChanging" PagerStyle-CssClass="pager" OnSorting="gvExcelFile_Sorting"
                    OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" HeaderStyle-CssClass="header1" EmptyDataText="No records to display." RowStyle-CssClass="rows" AllowPaging="True" AllowSorting="True" PageSize="20" Width="90%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <%--                <asp:BoundField ItemStyle-Width="120px" DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="LastName" HeaderText="Last Name" SortExpression="LastName" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="175px" DataField="UserName" HeaderText="User Name" SortExpression="UserName" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Password" HeaderText="Password" SortExpression="Password" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="150px" DataField="Email" HeaderText="Email" SortExpression="Email" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="UserRole" HeaderText="User Role" SortExpression="UserRole" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="CreatedDate" HeaderText="Created Date" SortExpression="CreatedDate" ControlStyle-Width="90%" />
                        --%>
                        <asp:TemplateField ItemStyle-Width="60px" HeaderStyle-Width="60px" HeaderText="">
                            <EditItemTemplate>
                                <asp:ImageButton runat="server" ImageUrl="~/Images/Update.png" Height="25px" Width="25px" OnClick="OnUpdate" />
                                <asp:ImageButton runat="server" ImageUrl="~/Images/Cancel.png" Height="25px" Width="25px" OnClick="OnCancel" />
                            </EditItemTemplate>

                            <HeaderStyle Width="60px"></HeaderStyle>

                            <ItemStyle Width="60px"></ItemStyle>
                        </asp:TemplateField>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    </div>
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td style="text-align: right;">
                <%--<asp:Button ID="btnExportToCSV" runat="server" Text="Export to CSV" OnClick="imgbtnCSV_Click" CssClass="button7" Style="background-color: #CCCCCC; color: #000000" />--%>
                <asp:ImageButton ID="imgbtnCSV" runat="server" AlternateText="Download CSV" ToolTip="Download CSV" ImageUrl="~/Images/CSV.png" Height="30px" Width="30px" OnClick="imgbtnCSV_Click" />
                <%--<asp:Button ID="btnDownload" runat="server" Text="Export to Excel" OnClick="imgbtnExcel_Click" CssClass="button7" Style="background-color: #CCCCCC; color: #000000" />--%>
                <asp:ImageButton ID="imgbtnExcel" runat="server" AlternateText="Download Excel" ToolTip="Download Excel" ImageUrl="~/Images/Excel.png" Height="30px" Width="30px" OnClick="imgbtnExcel_Click" />
            </td>
        </tr>
    </table>

</asp:Content>
