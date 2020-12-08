<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Users.aspx.cs" Inherits="MigrationTool.Users" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="fonts/StyleSheet.css" rel="stylesheet" />
    <script type="text/Javascript" language="javascript">
        function confirm_meth() {
            if (confirm("Are you sure to Commit the data to project database?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <style>
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            padding-right: 10px;
            padding-bottom: 10px;
            width: 400px;
            height: 250px;
        }

        .auto-style2 {
            height: 31px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table align="center" style="border: none;" width="100%">
        <tr>
            <td style="text-align: left"><div class="header2">Users</div></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtSearch" runat="server" />
                <asp:Button Text="Search" ID="btnSearch" runat="server" OnClick="Search" /></td>
        </tr>
    </table>
    <div style="overflow-x: auto; width: 100%; padding-top: 10px;">
        <asp:GridView ID="gvExcelFile" runat="server" CssClass="mydatagrid" OnPageIndexChanging="gvExcelFile_PageIndexChanging" PagerStyle-CssClass="pager" OnSorting="gvExcelFile_Sorting"
            OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" HeaderStyle-CssClass="header1" EmptyDataText="No records to display."
            AutoGenerateColumns="False" RowStyle-CssClass="rows" AllowPaging="True" ShowHeaderWhenEmpty="true" AllowSorting="True" PageSize="20" Width="90%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <Columns>
                <asp:TemplateField ItemStyle-Width="60px" HeaderStyle-Width="60px" HeaderText="">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ImageUrl="~/Images/Delete.png" Height="15px" Width="15px" OnClick="OnDelete" />
                        <asp:ImageButton runat="server" ImageUrl="~/Images/Update.png" Height="15px" Width="15px" OnClick="OnEdit" ID="btnEdit" />
                        <%--<asp:ImageButton runat="server" ImageUrl="~/Images/Cancel.png" Height="15px" Width="15px" OnClick="OnCancel" />--%>
                    </ItemTemplate>
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="120px" DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" ControlStyle-Width="90%">
                    <ControlStyle Width="90%"></ControlStyle>
                    <ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="120px" DataField="LastName" HeaderText="Last Name" SortExpression="LastName" ControlStyle-Width="90%">
                    <ControlStyle Width="90%"></ControlStyle>
                    <ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="175px" DataField="UserName" HeaderText="User Name" SortExpression="UserName" ControlStyle-Width="90%">
                    <ControlStyle Width="90%"></ControlStyle>
                    <ItemStyle Width="175px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="120px" DataField="Password" HeaderText="Password" SortExpression="Password" ControlStyle-Width="90%">
                    <ControlStyle Width="90%"></ControlStyle>
                    <ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="150px" DataField="Email" HeaderText="Email" SortExpression="Email" ControlStyle-Width="90%">
                    <ControlStyle Width="90%"></ControlStyle>

                    <ItemStyle Width="150px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="120px" DataField="UserRole" HeaderText="User Role" SortExpression="UserRole" ControlStyle-Width="90%">
                    <ControlStyle Width="90%"></ControlStyle>

                    <ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField ItemStyle-Width="120px" DataField="CreatedDate" HeaderText="Created Date" SortExpression="CreatedDate" ControlStyle-Width="90%">
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
                <asp:Button ID="btnCommit" runat="server" Text="Commit Changes" OnClick="btnCommit_Click" OnClientClick=" return confirm_meth()" CssClass="button7" Style="background-color: crimson" />
                <asp:Button ID="btnRevert" runat="server" Text="Revert Changes" OnClick="btnRevert_Click" CssClass="button7" Style="background-color: #2979FF" />
            </td>
            <td style="text-align: right;">
                <%--<asp:Button ID="btnExportToCSV" runat="server" Text="Export to CSV" OnClick="imgbtnCSV_Click" CssClass="button7" Style="background-color: #CCCCCC; color: #000000" />--%>
                <asp:ImageButton ID="imgbtnCSV" runat="server" AlternateText="Download CSV" ToolTip="Download CSV" ImageUrl="~/Images/CSV.png" Height="30px" Width="30px" OnClick="imgbtnCSV_Click" />
                <%--<asp:Button ID="btnDownload" runat="server" Text="Export to Excel" OnClick="imgbtnExcel_Click" CssClass="button7" Style="background-color: #CCCCCC; color: #000000" />--%>
                <asp:ImageButton ID="imgbtnExcel" runat="server" AlternateText="Download Excel" ToolTip="Download Excel" ImageUrl="~/Images/Excel.png" Height="30px" Width="30px" OnClick="imgbtnExcel_Click" />
            </td>
        </tr>
    </table>

    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
        CancelControlID="btnCancel" BackgroundCssClass="Background">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" CssClass="Popup" Height="40%" Width="600px" Style="display: none">
        <table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%" cellpadding="2" cellspacing="2">
            <tr style="background-color: #D55500">
                <td colspan="2" style="height: 5%; color: White; font-weight: bold; font-size: larger" align="center">User Details</td>
            </tr>
            <tr>
                <td align="right">User Name:
                </td>
                <td>
                    <asp:Label ID="lblUsername" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">First Name:
                </td>
                <td>
                    <asp:TextBox ID="txtFirstname" runat="server" CssClass="textbox2" />
                </td>
            </tr>
            <tr>
                <td align="right">Last Name:
                </td>
                <td>
                    <asp:TextBox ID="txtLastname" runat="server" CssClass="textbox2" />
                </td>
            </tr>
            <tr>
                <td align="right">Password:
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox2" />
                </td>
            </tr>
            <tr>
                <td align="right">Email:
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox2" />
                </td>
            </tr>
            <tr>
                <td align="right">User Role:
                </td>
                <td>
                    <asp:DropDownList ID="drpUserRole" runat="server" CssClass="textbox2">
                        <asp:ListItem>Admin</asp:ListItem>
                        <asp:ListItem>User</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnUpdate" CommandName="Update" OnClick="OnUpdate" runat="server" Text="Update" CssClass="button7" Style="background-color: #2979FF" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button7" Style="background-color: #CCCCCC; color: #000000" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Button ID="btnShowDeletePopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowDeletePopup" PopupControlID="pnlpopup1"
        CancelControlID="btnCancel1" BackgroundCssClass="Background">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup1" runat="server" BackColor="White" CssClass="Popup" Height="200px" Width="400px" Style="display: none">
        <table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%" cellpadding="2" cellspacing="2">
            <tr style="background-color: #D55500">
                <td colspan="2" style="height: 5%; color: White; font-weight: bold; font-size: larger" align="center">Delete User</td>
            </tr>
            <tr>
                <td style="text-align: center">Are you sure to delete the User -
                    <asp:Label ID="lblUser" runat="server" Font-Bold="true"></asp:Label>
                    ?
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="2">
                    <asp:Button ID="btnDelete" OnClick="OnDeleteConfirm" runat="server" Text="Delete" CssClass="button7" Style="background-color: crimson" />
                    <asp:Button ID="btnCancel1" runat="server" Text="Cancel" CssClass="button7" Style="background-color: #CCCCCC; color: #000000" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
