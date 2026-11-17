<%@ Page Title="Meridian Insurance - Advisor Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="InsuranceSystemWeb.Login" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Advisor login -->
    <div class="card">
        <h2>Advisor Login</h2>

        <div class="form-row">
            <label>Username:</label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="input-control"></asp:TextBox>
        </div>
        <div class="form-row">
            <label>Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="input-control" TextMode="Password"></asp:TextBox>
        </div>

        <div class="actions">
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
        </div>

        <asp:Label ID="lblLoginMsg" runat="server" CssClass="status-msg error-text"></asp:Label>
    </div>

</asp:Content>
