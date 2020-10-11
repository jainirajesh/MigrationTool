<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayData.aspx.cs" EnableEventValidation="false"
    Inherits="MigrationTool.DisplayData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound"
                OnRowEditing="OnRowEditing">
                <Columns>
                    <asp:BoundField ItemStyle-Width="150px" DataField="CUST_CODE" HeaderText="Customer ID" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="CUST_NAME" HeaderText="Contact Name" />

                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:LinkButton Text="Update" runat="server" OnClick="OnUpdate" />
                            <asp:LinkButton Text="Cancel" runat="server" OnClick="OnCancel" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
        </div>
    </form>
</body>
</html>
