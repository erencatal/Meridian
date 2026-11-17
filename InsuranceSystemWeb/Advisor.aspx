<%@ Page Title="Meridian Insurance - Advisor Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Advisor.aspx.cs" Inherits="InsuranceSystemWeb.Advisor" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Summary statistics -->
    <div class="card">
        <h2>Advisor Management Dashboard</h2>
        <asp:Literal ID="litStats" runat="server"></asp:Literal>
        <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-secondary" OnClick="btnLogout_Click" />
    </div>

    <!-- Filter & insurance records -->
    <div class="card">
        <h3>Manage Insurance Applications</h3>

        <div class="form-row">
            <label>Filter by Status:</label>
            <asp:DropDownList ID="ddlStatusFilter" runat="server" CssClass="input-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatusFilter_SelectedIndexChanged">
                <asp:ListItem Value="All">All Statuses</asp:ListItem>
                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                <asp:ListItem Value="Active">Active</asp:ListItem>
                <asp:ListItem Value="Expired">Expired</asp:ListItem>
                <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
            </asp:DropDownList>
        </div>

        <asp:Label ID="lblActionMsg" runat="server" CssClass="status-msg"></asp:Label>

        <asp:GridView ID="gvInsurances" runat="server" AutoGenerateColumns="False" CssClass="grid-table"
            DataKeyNames="InsuranceId" OnRowCommand="gvInsurances_RowCommand" EmptyDataText="No insurance records found for the selected filter.">
            <Columns>
                <asp:BoundField DataField="InsuranceId" HeaderText="ID" />
                <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <%# ((InsuranceSystemWeb.Models.Insurance)Container.DataItem).GetCategoryName() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Tier" HeaderText="Tier" />
                <asp:TemplateField HeaderText="Monthly Fee">
                    <ItemTemplate>
                        $<%# ((InsuranceSystemWeb.Models.Insurance)Container.DataItem).CalculateTotalCost().ToString("F2") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:TemplateField HeaderText="Advisor Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnActivate" runat="server" Text="Activate" CommandName="SetActive"
                            CommandArgument='<%# Eval("InsuranceId") %>' CssClass="btn btn-success" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="SetCancelled"
                            CommandArgument='<%# Eval("InsuranceId") %>' CssClass="btn btn-danger" />
                        <asp:Button ID="btnLogs" runat="server" Text="Logs" CommandName="ViewLogs"
                            CommandArgument='<%# Eval("InsuranceId") %>' CssClass="btn btn-secondary" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!-- Selected policy event logs -->
        <asp:Literal ID="litSelectedLogs" runat="server"></asp:Literal>
    </div>

</asp:Content>
