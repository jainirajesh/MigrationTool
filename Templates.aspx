<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Templates.aspx.cs" Inherits="MigrationTool.Templates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header2">Download Templates</div>
    <table cellspacing="5" cellpadding="5" width="80%" style="vertical-align: middle;">
        <tr>
            <td></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td width="10%"></td>
            <td width="200px">
                <asp:Label runat="server" ID="lbl1" Text="File Name" Font-Underline="true" Font-Bold="true"></asp:Label></td>
            <td>
                <asp:Label runat="server" ID="Label6" Text="Download Link" Font-Bold="true" Font-Underline="true"></asp:Label></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label runat="server" ID="Label1" Text="Hosts"></asp:Label></td>
            <td>
                <asp:LinkButton ID="lnkHosts" runat="server" OnClick="lnkHosts_Click">Hosts.xlsx</asp:LinkButton></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label runat="server" ID="Label2" Text="Applications"></asp:Label></td>
            <td>
                <asp:LinkButton ID="lnkApplications" runat="server" OnClick="lnkApplications_Click">Applications.xlsx</asp:LinkButton></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label runat="server" ID="Label3" Text="Storage"></asp:Label></td>
            <td>
                <asp:LinkButton ID="lnkStorage" runat="server" OnClick="lnkStorage_Click">Storage.xlsx</asp:LinkButton></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label runat="server" ID="Label4" Text="Databases"></asp:Label></td>
            <td>
                <asp:LinkButton ID="lnkDatabase" runat="server" OnClick="lnkDatabase_Click">Databases.xlsx</asp:LinkButton></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label runat="server" ID="Label5" Text="Relations"></asp:Label></td>
            <td>
                <asp:LinkButton ID="lnkRelations" runat="server" OnClick="lnkRelations_Click">Relations.xlsx</asp:LinkButton></td>
            <td>&nbsp;</td>
        </tr>

    </table>
</asp:Content>
