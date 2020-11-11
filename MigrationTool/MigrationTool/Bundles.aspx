﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Bundles.aspx.cs" Inherits="MigrationTool.Bundles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        
    </style>

    <script type="text/javascript">
        function ShowProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = 'visible';
        }

        function HideProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = "hidden";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" onload="javascript:HideProgressBar()">
    <div class="header2">Bundles</div>
    <table align="center" style="border: none" width="100%">
        <tr>
            <td style="text-align: left;">&nbsp;</td>
            <td style="text-align: left;">&nbsp;</td>
            <td style="text-align: left;">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left; width: 300px;">
                <asp:RadioButtonList ID="rdoDataType" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoDataType_SelectedIndexChanged">
                    <asp:ListItem Selected="True">Create Bundle</asp:ListItem>
                    <asp:ListItem>View Bundles</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="text-align: left;">
                <asp:Button runat="server" ID="btnSubmit" CssClass="button7" Style="background-color: #2979FF;" Text="Create" OnClick="btnSubmit_Click" OnClientClick="javascript:ShowProgressBar()" />
            </td>
            <td style="text-align: left;">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="grdBundles" runat="server" Width="100%" AllowSorting="true" AutoGenerateColumns="False" CssClass="mydatagrid" PagerStyle-CssClass="pager"
                    HeaderStyle-CssClass="header1" OnSorting="grdBundles_Sorting" RowStyle-CssClass="rows" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <asp:BoundField DataField="Application" HeaderText="Application" SortExpression="Application"></asp:BoundField>
                        <asp:BoundField DataField="Server" HeaderText="Server" SortExpression="Server"></asp:BoundField>
                        <asp:BoundField ItemStyle-Width="175px" DataField="bundle" HeaderText="Bundle" SortExpression="bundle"></asp:BoundField>
                        <asp:BoundField DataField="Physical_or_Virtual" HeaderText="Physical/Virtual" SortExpression="Physical_or_Virtual"></asp:BoundField>
                        <asp:BoundField ItemStyle-Width="150px" DataField="In_Scope" HeaderText="In Scope" SortExpression="In_Scope"></asp:BoundField>
                        <asp:BoundField DataField="Source_DC" HeaderText="Source DC" SortExpression="Source_DC"></asp:BoundField>
                        <asp:BoundField DataField="Environment" HeaderText="Environment" SortExpression="Environment"></asp:BoundField>
                        <asp:BoundField DataField="Owner_Primary" HeaderText="Primary Owner" SortExpression="Owner_Primary"></asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <div>
        <div class="">
            <div id="dvProgressBar" style="visibility: hidden; margin: 0px; padding: 0px; position: fixed; right: 0px; top: 0px; width: 100%; height: 100%; background-color: white; z-index: 30001; opacity: 0.7;">
               <p style="position: absolute; color: White; top: 50%; left: 50%;"> <asp:Image runat="server" AlternateText="Please wait.." ImageUrl="~/Images/ajax-loader.gif"/>
                </p>
            </div>
        </div>
        <br style="clear: both" />
    </div>
</asp:Content>