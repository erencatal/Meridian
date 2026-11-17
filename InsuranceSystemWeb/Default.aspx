<%@ Page Title="Meridian Insurance - Customer Portal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InsuranceSystemWeb.Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Quote & Application -->
    <div class="card">
        <h2>Configure &amp; Compare Your Insurance Plan (9 Plans Available)</h2>

        <div class="form-row">
            <label>Insurance Type:</label>
            <asp:DropDownList ID="ddlInsuranceType" runat="server" CssClass="input-control" AutoPostBack="true" OnSelectedIndexChanged="ddlInsuranceType_SelectedIndexChanged">
                <asp:ListItem Value="Device">Device Insurance</asp:ListItem>
                <asp:ListItem Value="Home">Home Insurance</asp:ListItem>
                <asp:ListItem Value="Travel">Travel Insurance</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-row">
            <label>Coverage Tier:</label>
            <asp:RadioButtonList ID="rblCoverageTier" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Basic" Selected="True">Basic (1x)</asp:ListItem>
                <asp:ListItem Value="Standard">Standard (2x)</asp:ListItem>
                <asp:ListItem Value="Premium">Premium (3x)</asp:ListItem>
            </asp:RadioButtonList>
        </div>

        <!-- Device fields -->
        <asp:Panel ID="pnlDeviceFields" runat="server">
            <div class="form-row">
                <label>Device Model:</label>
                <asp:TextBox ID="txtDeviceModel" runat="server" CssClass="input-control" placeholder="e.g. MacBook Pro M3"></asp:TextBox>
            </div>
            <div class="form-row">
                <label>Device Value ($):</label>
                <asp:TextBox ID="txtDeviceValue" runat="server" CssClass="input-control" Text="1000"></asp:TextBox>
            </div>
            <div class="form-row">
                <asp:CheckBox ID="chkDeviceScreenCare" runat="server" Text=" Include Accidental Screen Protection (+$4.50/mo)" />
            </div>
        </asp:Panel>

        <!-- Home fields -->
        <asp:Panel ID="pnlHomeFields" runat="server" Visible="false">
            <div class="form-row">
                <label>City:</label>
                <asp:TextBox ID="txtHomeCity" runat="server" CssClass="input-control" placeholder="e.g. Istanbul"></asp:TextBox>
            </div>
            <div class="form-row">
                <label>Home Area (m²):</label>
                <asp:TextBox ID="txtHomeArea" runat="server" CssClass="input-control" Text="100"></asp:TextBox>
            </div>
            <div class="form-row">
                <asp:CheckBox ID="chkHomeApplianceCare" runat="server" Text=" Include Home Appliance Care (+$8.00/mo)" />
            </div>
        </asp:Panel>

        <!-- Travel fields -->
        <asp:Panel ID="pnlTravelFields" runat="server" Visible="false">
            <div class="form-row">
                <label>Destination:</label>
                <asp:TextBox ID="txtTravelDestination" runat="server" CssClass="input-control" placeholder="e.g. Tokyo"></asp:TextBox>
            </div>
            <div class="form-row">
                <label>Trip Duration (Days):</label>
                <asp:TextBox ID="txtTravelDays" runat="server" CssClass="input-control" Text="7"></asp:TextBox>
            </div>
            <div class="form-row">
                <asp:CheckBox ID="chkTravelBaggageLoss" runat="server" Text=" Include Lost Baggage Coverage (+$6.00/mo)" />
            </div>
        </asp:Panel>

        <hr />

        <!-- Customer & billing -->
        <div class="form-row">
            <label>Customer Full Name:</label>
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="input-control" placeholder="Enter your full name"></asp:TextBox>
        </div>
        <div class="form-row">
            <label>Customer Email:</label>
            <asp:TextBox ID="txtCustomerEmail" runat="server" CssClass="input-control" placeholder="name@example.com"></asp:TextBox>
        </div>
        <div class="form-row">
            <asp:CheckBox ID="chkAnnualBilling" runat="server" Text=" Annual Billing (Save 15%)" />
        </div>
        <div class="form-row">
            <label>Promo Code (Optional - try SAVE10):</label>
            <asp:TextBox ID="txtPromoCode" runat="server" CssClass="input-control" placeholder="SAVE10"></asp:TextBox>
        </div>

        <div class="actions">
            <asp:Button ID="btnCalculateQuote" runat="server" Text="Calculate & Compare All Tiers" CssClass="btn btn-primary" OnClick="btnCalculateQuote_Click" />
            <asp:Button ID="btnSubmitApplication" runat="server" Text="Submit Insurance Application" CssClass="btn btn-success" OnClick="btnSubmitApplication_Click" />
        </div>

        <asp:Label ID="lblStatusMessage" runat="server" CssClass="status-msg"></asp:Label>
        <asp:Literal ID="litQuoteOutput" runat="server"></asp:Literal>
    </div>

    <!-- Tracking -->
    <div class="card">
        <h3>Track Existing Insurance by ID</h3>

        <div class="form-row">
            <label>Insurance ID (e.g., INS-XXXXXX):</label>
            <asp:TextBox ID="txtTrackingId" runat="server" CssClass="input-control" placeholder="INS-..."></asp:TextBox>
        </div>

        <asp:Button ID="btnTrackInsurance" runat="server" Text="Check Status & Logs" CssClass="btn btn-secondary" OnClick="btnTrackInsurance_Click" />
        <asp:Literal ID="litTrackingOutput" runat="server"></asp:Literal>
    </div>

</asp:Content>
