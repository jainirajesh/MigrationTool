<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="MigrationTool.Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="text-align: center;">
        <table width="95%">
            <tr>
                <td>
                    <ajax:CollapsiblePanelExtender ID="cpe" runat="Server"
                        TargetControlID="pnl1"
                        CollapsedSize="0"
                        ExpandedSize="300"
                        Collapsed="True"
                        ExpandControlID="LinkButton1"
                        CollapseControlID="LinkButton1"
                        AutoCollapse="False"
                        AutoExpand="False"
                        ScrollContents="True"
                        TextLabelID="Label1"
                        CollapsedText="Show Details..."
                        ExpandedText="Hide Details"
                        ImageControlID="Image1"
                        ExpandedImage="~/images/collapse.jpg"
                        CollapsedImage="~/images/expand.jpg"
                        ExpandDirection="Vertical" />
                    <asp:Panel runat="server" ID="pnl1" Width="100%" Height="50px">
                        <asp:LinkButton ID="LinkButton1" runat="server">Expand/Collapse</asp:LinkButton>
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </asp:Panel>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
            </tr>
        </table>
    </div>
</asp:Content>
