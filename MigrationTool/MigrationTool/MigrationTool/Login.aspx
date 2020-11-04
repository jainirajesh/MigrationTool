<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MigrationTool.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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
                margin-left: 20px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center" style="padding-top:100px;">
    <fieldset style ="width:500px; height:250px;">
    <legend>Login</legend>
        <br />
        <asp:TextBox ID="txtusername" placeholder="username" runat="server"
            Width="280px"></asp:TextBox>
        <br /><br />
        <br />
        <asp:TextBox ID="txtpassword" placeholder="password" runat="server"
            Width="280px" TextMode="Password"></asp:TextBox>
        <br />
        <br /><br />
        <asp:Button ID="btnsubmit" runat="server" Text="Submit"
           Width="120px" onclick="btnsubmit_Click" />
            <br />
           <br /><br />
    </fieldset>
    </div>
</asp:Content>
