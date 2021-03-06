﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Applications.aspx.cs" EnableEventValidation="false" Inherits="MigrationTool.Applications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="fonts/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header2">Applications</div>
    <table align="center" style="border: none" width="100%">        
        <tr>
            <td colspan="2"></td>
        </tr>  
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
                <asp:BoundField ItemStyle-Width="120px" DataField="Name" HeaderText="Name" SortExpression="Name" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Environment" HeaderText="Environment" SortExpression="Environment" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="175px" DataField="Owner_Primary" HeaderText="Primary Owner" SortExpression="Owner_Primary" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Owner_Secondary" HeaderText="Secondary Owner" SortExpression="Owner_Secondary" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="150px" DataField="In_Scope" HeaderText="In Scope" SortExpression="In_Scope" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Out_of_Scope_Justification" HeaderText="Justification" SortExpression="Out_of_Scope_Justification" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Analysis_Status" HeaderText="Analysis Status" SortExpression="Analysis_Status" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Description" HeaderText="Description" SortExpression="Description" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Technical_Contact_Primary" HeaderText="Primary Technical Contact" SortExpression="Technical_Contact_Primary" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Technical_Contact_Secondary" HeaderText="Secondary Technical Contact" SortExpression="Technical_Contact_Secondary" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Business_Unit" HeaderText="Business Unit" SortExpression="Business_Unit" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Version" HeaderText="Version" SortExpression="Version" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Business_Criticality" HeaderText="Business Criticality" SortExpression="Business_Criticality" ControlStyle-Width="90%" />
                <asp:BoundField ItemStyle-Width="120px" DataField="Comments" HeaderText="Comments" SortExpression="Comments" ControlStyle-Width="90%" />

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
