<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="MigrationTool.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div>
           

            <asp:GridView ID="GridView1" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
    runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound"
    OnRowEditing="OnRowEditing">
    <Columns>
        <asp:BoundField ItemStyle-Width="150px" DataField="CUST_CODE" HeaderText="Customer ID" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="CUST_NAME" HeaderText="Contact Name" />
                  
        <asp:TemplateField>
        <EditItemTemplate>
            <asp:LinkButton Text="Update" runat="server" OnClick = "OnUpdate" />
            <asp:LinkButton Text="Cancel" runat="server" OnClick = "OnCancel" />
        </EditItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
        </div>

</asp:Content>
