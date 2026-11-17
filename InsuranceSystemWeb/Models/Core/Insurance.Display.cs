using System.Text;

namespace InsuranceSystemWeb.Models
{
    // Display texts and HTML output
    public partial class Insurance
    {
        public virtual string GetCategoryName() => "General Insurance";

        public string GetSummaryHtml()
        {
            var html = new StringBuilder();

            html.Append("<div class='summary-box'>");
            html.Append($"<h3>Insurance ID: {InsuranceId} ({GetCategoryName()})</h3>");
            html.Append($"<p><strong>Customer:</strong> {CustomerName} ({CustomerEmail})</p>");
            html.Append($"<p><strong>Coverage Tier:</strong> {Tier}</p>");
            html.Append($"<p><strong>Status:</strong> {Status}</p>");
            html.Append($"<p><strong>Standard Monthly Fee:</strong> ${CalculateTotalCost():F2}</p>");
            html.Append("</div>");

            return html.ToString();
        }
    }
}
