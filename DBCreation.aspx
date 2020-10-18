<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DBCreation.aspx.cs" Inherits="MigrationTool.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<style>
        body {
            color: #999;
            background: #fafafa;
            font-family: 'Roboto', sans-serif;
        }

        .form-control {
            min-height: 41px;
            box-shadow: none;
            border-color: #e6e6e6;
        }

            .form-control:focus {
                border-color: #00c1c0;
            }

        .form-control, .btn {
            border-radius: 3px;
        }

        .signup-form {
            width: 425px;
            margin: 0 auto;
            padding: 30px 0;
        }

            .signup-form h2 {
                color: #333;
                font-weight: bold;
                margin: 0 0 25px;
            }

            .signup-form form {
                margin-bottom: 15px;
                background: #fff;
                border: 1px solid #f4f4f4;
                box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
                padding: 40px 50px;
            }

            .signup-form .form-group {
                margin-bottom: 20px;
            }

            .signup-form label {
                font-weight: normal;
                font-size: 14px;
            }

            .signup-form input[type="checkbox"] {
                position: relative;
                top: 1px;
            }

            .signup-form .btn, .signup-form .btn:active {
                font-size: 16px;
                font-weight: bold;
                background: #00c1c0 !important;
                border: none;
                min-width: 140px;
            }

                .signup-form .btn:hover, .signup-form .btn:focus {
                    background: #00b3b3 !important;
                }

            .signup-form a {
                color: #00c1c0;
                text-decoration: none;
            }

                .signup-form a:hover {
                    text-decoration: underline;
                }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header2">Create Project</div>
    <div>
        <table align="center" cellspacing="5" cellpadding="5">
            <tr>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Enter the Project name to be created :</td>
                <td>
                    <asp:TextBox ID="DatabaseBox1" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Insert Data into Table :</td>
                <td>
                    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged"></asp:CheckBox></td>

            </tr>
            <tr>
                <td>&nbsp;</td>
                <td align="center">&nbsp;</td>

            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Create Database" OnClick="Button1_Click"></asp:Button></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label1" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label2" runat="server"></asp:Label></td>
            </tr>
        </table>
    </div>
</asp:Content>
