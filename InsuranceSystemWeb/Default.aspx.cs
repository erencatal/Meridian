using System;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using InsuranceSystemWeb.Models;

namespace InsuranceSystemWeb
{
    // Customer portal: compare quotes, submit applications, track policies
    public partial class Default : Page
    {
        private const string DeviceType = "Device";
        private const string HomeType = "Home";
        private const string TravelType = "Travel";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Show only the fields of the selected insurance type
        protected void ddlInsuranceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = ddlInsuranceType.SelectedValue;

            pnlDeviceFields.Visible = type == DeviceType;
            pnlHomeFields.Visible = type == HomeType;
            pnlTravelFields.Visible = type == TravelType;

            lblStatusMessage.Text = string.Empty;
            litQuoteOutput.Text = string.Empty;
        }

        protected void btnCalculateQuote_Click(object sender, EventArgs e)
        {
            lblStatusMessage.Text = string.Empty;
            litQuoteOutput.Text = BuildTierComparisonHtml();
        }

        protected void btnSubmitApplication_Click(object sender, EventArgs e)
        {
            if (!HasCustomerInfo())
            {
                ShowStatus("Please enter your full name and email address before submitting.", Color.Red);
                return;
            }

            Insurance insurance = CreateInsuranceFromForm(GetSelectedTier());
            InsuranceRepository.AddInsurance(insurance);

            decimal finalCost = insurance.CalculateTotalCost(chkAnnualBilling.Checked, txtPromoCode.Text.Trim());
            string billing = chkAnnualBilling.Checked ? "Annual" : "Monthly";

            ShowStatus($"Application submitted! Save your tracking ID: {insurance.InsuranceId}", Color.Green);
            litQuoteOutput.Text = insurance.GetSummaryHtml()
                                + $"<p><strong>Final Calculated Fee ({billing}):</strong> ${finalCost:F2}</p>";
        }

        protected void btnTrackInsurance_Click(object sender, EventArgs e)
        {
            Insurance insurance = InsuranceRepository.FindById(txtTrackingId.Text);

            litTrackingOutput.Text = insurance == null
                ? "<p class='error-text'>No insurance record found with that ID.</p>"
                : BuildTrackingHtml(insurance);
        }

        // Build the right insurance subclass from the form inputs
        private Insurance CreateInsuranceFromForm(CoverageTier tier)
        {
            string name = GetTextOrDefault(txtCustomerName, "Guest Customer");
            string email = GetTextOrDefault(txtCustomerEmail, "guest@example.com");

            switch (ddlInsuranceType.SelectedValue)
            {
                case HomeType:
                    string city = GetTextOrDefault(txtHomeCity, "Istanbul");
                    int area = Math.Max(10, ParseInt(txtHomeArea, 100));
                    return new HomeInsurance(name, email, tier, city, area, chkHomeApplianceCare.Checked);

                case TravelType:
                    string destination = GetTextOrDefault(txtTravelDestination, "London");
                    int days = Math.Max(1, ParseInt(txtTravelDays, 7));
                    return new TravelInsurance(name, email, tier, destination, days, chkTravelBaggageLoss.Checked);

                default:
                    string model = GetTextOrDefault(txtDeviceModel, "Smartphone");
                    decimal value = Math.Max(50M, ParseDecimal(txtDeviceValue, 1000M));
                    return new DeviceInsurance(name, email, tier, model, value, chkDeviceScreenCare.Checked);
            }
        }

        // Price table for all tiers, selected tier highlighted
        private string BuildTierComparisonHtml()
        {
            bool isAnnual = chkAnnualBilling.Checked;
            string promoCode = txtPromoCode.Text.Trim();
            CoverageTier selectedTier = GetSelectedTier();

            var html = new StringBuilder();

            html.Append("<div class='summary-box'>");
            html.Append($"<h4>Instant Tier Comparison ({ddlInsuranceType.SelectedItem.Text})</h4>");
            html.Append("<table class='grid-table'>");
            html.Append($"<tr><th>Tier</th><th>Standard Monthly</th><th>Your Billing ({(isAnnual ? "Annual -15%" : "Monthly")})</th></tr>");

            foreach (CoverageTier tier in Enum.GetValues(typeof(CoverageTier)))
            {
                Insurance plan = CreateInsuranceFromForm(tier);
                string rowClass = tier == selectedTier ? " class='highlight-row'" : "";

                html.Append($"<tr{rowClass}><td>{tier}</td>");
                html.Append($"<td>${plan.CalculateTotalCost():F2} / mo</td>");
                html.Append($"<td>${plan.CalculateTotalCost(isAnnual, promoCode):F2}</td></tr>");
            }

            html.Append("</table></div>");

            return html.ToString();
        }

        // Policy summary + activity log list
        private static string BuildTrackingHtml(Insurance insurance)
        {
            var html = new StringBuilder();

            html.Append(insurance.GetSummaryHtml());
            html.Append("<h4>Activity Logs (Event History):</h4><ul>");

            foreach (string log in insurance.ActivityLogs)
            {
                html.Append($"<li>{log}</li>");
            }

            html.Append("</ul>");

            return html.ToString();
        }

        // Form helpers
        private CoverageTier GetSelectedTier()
        {
            return (CoverageTier)Enum.Parse(typeof(CoverageTier), rblCoverageTier.SelectedValue);
        }

        private bool HasCustomerInfo()
        {
            return !string.IsNullOrWhiteSpace(txtCustomerName.Text)
                && !string.IsNullOrWhiteSpace(txtCustomerEmail.Text);
        }

        private void ShowStatus(string message, Color color)
        {
            lblStatusMessage.ForeColor = color;
            lblStatusMessage.Text = message;
        }

        private static string GetTextOrDefault(TextBox input, string fallback)
        {
            return string.IsNullOrWhiteSpace(input.Text) ? fallback : input.Text.Trim();
        }

        private static int ParseInt(TextBox input, int fallback)
        {
            return int.TryParse(input.Text, out int result) ? result : fallback;
        }

        private static decimal ParseDecimal(TextBox input, decimal fallback)
        {
            return decimal.TryParse(input.Text, out decimal result) ? result : fallback;
        }
    }
}
