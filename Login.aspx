﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MigrationTool.WebForm3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            width: 400px;
            height: 250px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }

        body, html {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
            display: table;
        }

        body {
            display: table-cell;
            vertical-align: middle;
        }

        form {
            display: table; /* shrinks to fit content */
            margin: auto;
        }

        /*.centered {
            position: fixed;
            top: 50%;
            left: 50%;
          
            transform: translate(-50%, -50%);
        }*/


        fieldset {
            font-family: sans-serif;
            border: 5px solid #1F497D;
            background: #fff;
            border-radius: 5px;
            padding: 15px;
        }

            fieldset legend {
                background: #1F497D;
                color: #fff;
                padding: 5px 10px;
                font-size: 32px;
                border-radius: 5px;
                box-shadow: 0 0 0 5px #ddd;
            }

        .input {
            width: 100%;
            padding: 12px 20px;
            margin: 8px 0;
            box-sizing: border-box;
        }

        .button {
            background-color: #4CAF50;
            border: none;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div align="center">
        <table style="width: 100%; height: 100%; text-align: center; vertical-align: middle;" class="centered">
            <tr>
                <td width="10%"></td>
                <td width="40%" style="text-align: right;">
                    <asp:Image ID="Image1" runat="server" Width="300px" Height="300px" ImageUrl="~/Images/Logo1.jpg" />
                </td>
                <td width="40%" style="text-align: left;">
                    <fieldset style="width: 350px; height: 260px; text-align: center;">
                        <br />
                        <asp:TextBox ID="txtusername" placeholder="Username" runat="server"
                            Width="280px" CssClass="input"></asp:TextBox>
                        <br />
                        <asp:TextBox ID="txtpassword" placeholder="Password" runat="server"
                            Width="280px" TextMode="Password" CssClass="input"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="btnsubmit" runat="server" Text="Login"
                            Width="150px" OnClick="btnsubmit_Click" CssClass="button" /><br />
                        <asp:LinkButton runat="server" ID="lnkFSignUp" Text="SignUp" ForeColor="Black" Font-Size="Smaller" Font-Italic="true"></asp:LinkButton>&nbsp;
                        <asp:LinkButton runat="server" ID="lnkForgotPassword" Text="Forgot Password?" ForeColor="Black" Font-Size="Smaller" Font-Italic="true"></asp:LinkButton>
                        <br />
                    </fieldset>
                </td>
                <td width="10%"></td>
            </tr>
        </table>
        <!-- ModalPopupExtender -->

        <ajax:ModalPopupExtender ID="mpResetPassword" runat="server" PopupControlID="Panl1" TargetControlID="lnkForgotPassword"
            BackgroundCssClass="Background">
        </ajax:ModalPopupExtender>

        <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Width="550px" Height="350px" style="display:none;">
            <table style="text-align: left; width: 525px;" cellpadding="5" cellspacing="5">
                <tr>
                    <td colspan="2">
                        <div class="header2">Change Password</div>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>User Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtResetUsername" runat="server" Font-Size="14px" ValidationGroup="create"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="create" runat="server" ControlToValidate="txtResetUsername" ErrorMessage="*" ForeColor="Red">*</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td>Email
                    </td>
                    <td>
                        <asp:TextBox ID="txtResetEmail" runat="server" Font-Size="14px" ValidationGroup="create"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="create" runat="server" ControlToValidate="txtResetEmail" ErrorMessage="*" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>New Password
                    </td>
                    <td>
                        <asp:TextBox ID="txtResetPassword" runat="server" Font-Size="14px" ValidationGroup="create"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="create" runat="server" ControlToValidate="txtResetPassword" ErrorMessage="*" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Confirm New Password
                    </td>
                    <td>
                        <asp:TextBox ID="txtResetConfirmPass" runat="server" Font-Size="14px" ValidationGroup="create"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="create" runat="server" ControlToValidate="txtResetConfirmPass" ErrorMessage="*" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="comparevalida" runat="server" ValidationGroup="create" ControlToValidate="txtResetConfirmPass" ControlToCompare="txtResetPassword" ErrorMessage="*" ForeColor="Red"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Label runat="server" ID="lblErrPassword"></asp:Label></td>
                </tr>
            </table>
            <asp:Button ID="btnSubmitReset" runat="server" ValidationGroup="create" CausesValidation="true" Text="Submit" OnClick="btnSubmitReset_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="Close" OnClick="Button2_Click" />
        </asp:Panel>

        <!-- ModalPopupExtender -->

        <ajax:ModalPopupExtender ID="mdlSignUp" runat="server" PopupControlID="pnlSignUp" TargetControlID="lnkFSignUp"
            CancelControlID="btnSignUpCancel" BackgroundCssClass="Background">
        </ajax:ModalPopupExtender>

        <asp:Panel ID="pnlSignUp" runat="server" CssClass="Popup" align="center" Width="550px" Height="550px" style="display:none;">
            <table align="center" cellspacing="5" cellpadding="5" width="525px">
                <tr>
                    <td colspan="2">
                        <div class="header2">SignUp</div>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>First Name : </td>
                    <td>
                        <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FirstName" ErrorMessage="First Name is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Last Name : </td>
                    <td>
                        <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="LastName" ErrorMessage="LastName is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>User Name : </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="UserName" ErrorMessage="UserName is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Password :</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="Password" ErrorMessage="Password is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Password length must be between 7 to 10 characters" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{7,10}$" ControlToValidate="Password" ForeColor="Red">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>Email :</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="Email" ErrorMessage="Email is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email Format" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="Email" ForeColor="Red">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>User Role : </td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="UserRole" runat="server" DataValueField="Admin,User" DataTextField="values" Width="165px">
                            <asp:ListItem Selected="True">User</asp:ListItem>
                            <asp:ListItem Value="Admin"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="UserRole" ErrorMessage="*" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Button ID="btnSignUp" runat="server" Text="SignUp" OnClick="btnSignUp_Click" /></td>
                    <td style="text-align: left;">
                        <asp:Button ID="btnSignUpCancel" runat="server" Text="Cancel" OnClick="btnSignUpCancel_Click" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                        <asp:Label ID="Label4" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
