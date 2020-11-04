<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="MigrationTool.CreateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header2">Create User</div>
    <div class="container">
        <table align="center" width="600px" cellspacing="0" cellpadding="3">
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">First Name :
                    <br />
                    <asp:TextBox ID="FirstName" runat="server" CssClass="textbox1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FirstName" ErrorMessage="FirstName is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">Last Name :<br />
                    <asp:TextBox ID="LastName" runat="server" CssClass="textbox1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="LastName" ErrorMessage="LastName is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">User Name :
                    <br />
                    <asp:TextBox ID="UserName" runat="server" CssClass="textbox1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="UserName" ErrorMessage="UserName is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">Password :<br />
                    <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="textbox1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" ErrorMessage="UserName is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Password length must be between 7 to 10 characters" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{7,10}$" ControlToValidate="Password" ForeColor="Red">*</asp:RegularExpressionValidator>--%>
                </td>
            </tr>
            <tr>
                <td colspan="2">Email : <br /><asp:TextBox ID="Email" runat="server" CssClass="textbox1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Email" ErrorMessage="UserName is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email Format" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="Email" ForeColor="Red">*</asp:RegularExpressionValidator>
             </td>
            </tr>
            <tr>
                <td colspan="2">User Role :<br /><asp:DropDownList ID="UserRole" runat="server" CssClass="textbox1" DataValueField="Admin,User" DataTextField="values">
                        <asp:ListItem Selected="True">User</asp:ListItem>
                        <asp:ListItem Value="Admin"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="UserRole" ErrorMessage="UserRole is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Button ID="SignUp" runat="server" Text="Create User" OnClick="SignUp_Click" CssClass="button7" Style="background-color: #2979FF" /></td>
                <td style="text-align: left;">
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="150px" CausesValidation="false" OnClick="btnClear_Click" CssClass="button7" Style="background-color: #CCCCCC; color: #000000" /></td>
            </tr>
            <tr style="display:none;">
                <td colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" Visible="false" />
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
