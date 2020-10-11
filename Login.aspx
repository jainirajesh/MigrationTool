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
    <div align="center" style="padding-top: 150px;">
       
        <fieldset style="width: 500px; height: 250px;">

            <br />
            <asp:TextBox ID="txtusername" placeholder="Username" runat="server"
                Width="280px" CssClass="input"></asp:TextBox>
            <br />

            <asp:TextBox ID="txtpassword" placeholder="Password" runat="server"
                Width="280px" TextMode="Password" CssClass="input"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnsubmit" runat="server" Text="Login"
                Width="150px" OnClick="btnsubmit_Click" CssClass="button" />
            <br />
        </fieldset>
    </div>
</asp:Content>
