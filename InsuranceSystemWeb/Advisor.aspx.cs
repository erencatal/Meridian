using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using InsuranceSystemWeb.Models;

namespace InsuranceSystemWeb
{
    // Advisor dashboard: view stats, filter applications, update statuses and inspect logs
    public partial class Advisor : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Only logged-in advisors can see this page
            if (Session[Login.SessionKey] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindDashboard();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove(Login.SessionKey);
            Response.Redirect("Login.aspx");
        }

        protected void ddlStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblActionMsg.Text = string.Empty;
            litSelectedLogs.Text = string.Empty;
            BindDashboard();
        }

        // Handle Activate, Cancel and Logs button commands for a policy row
        protected void gvInsurances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string insuranceId = e.CommandArgument.ToString();
            Insurance target = InsuranceRepository.FindById(insuranceId);

            if (target == null) return;

            switch (e.CommandName)
            {
                case "SetActive":
                    InsuranceRepository.UpdateStatus(insuranceId, InsuranceStatus.Active);
                    ShowActionMessage($"Insurance {insuranceId} has been set to Active.", Color.Green);
                    break;

                case "SetCancelled":
                    InsuranceRepository.UpdateStatus(insuranceId, InsuranceStatus.Cancelled);
                    ShowActionMessage($"Insurance {insuranceId} has been Cancelled.", Color.Red);
                    break;

                case "ViewLogs":
                    litSelectedLogs.Text = BuildLogsHtml(target);
                    break;
            }

            BindDashboard();
        }

        // Refresh summary statistics and filtered table rows
        private void BindDashboard()
        {
            List<Insurance> allRecords = InsuranceRepository.GetAll();

            litStats.Text = BuildStatsHtml(allRecords);
            gvInsurances.DataSource = FilterBySelectedStatus(allRecords);
            gvInsurances.DataBind();
        }

        // Summary metrics for total applications, active policies and active monthly revenue
        private static string BuildStatsHtml(List<Insurance> records)
        {
            int totalCount = records.Count;
            int activeCount = records.Count(x => x.Status == InsuranceStatus.Active);
            decimal activeRevenue = records
                .Where(x => x.Status == InsuranceStatus.Active)
                .Sum(x => x.CalculateTotalCost());

            return $"<p><strong>Total Applications:</strong> {totalCount} &nbsp;|&nbsp; "
                 + $"<strong>Active Policies:</strong> {activeCount} &nbsp;|&nbsp; "
                 + $"<strong>Active Monthly Revenue:</strong> ${activeRevenue:F2}</p>";
        }

        // Filter records by the selected status dropdown value
        private List<Insurance> FilterBySelectedStatus(List<Insurance> records)
        {
            string selectedFilter = ddlStatusFilter.SelectedValue;

            if (selectedFilter == "All") return records;

            var status = (InsuranceStatus)Enum.Parse(typeof(InsuranceStatus), selectedFilter);
            return records.Where(x => x.Status == status).ToList();
        }

        // Render activity log history for the selected policy
        private static string BuildLogsHtml(Insurance insurance)
        {
            var html = new StringBuilder();

            html.Append("<div class='summary-box'>");
            html.Append($"<h4>Event Activity History for {insurance.InsuranceId} ({insurance.CustomerName})</h4><ul>");

            foreach (string log in insurance.ActivityLogs)
            {
                html.Append($"<li>{log}</li>");
            }

            html.Append("</ul></div>");

            return html.ToString();
        }

        private void ShowActionMessage(string message, Color color)
        {
            lblActionMsg.ForeColor = color;
            lblActionMsg.Text = message;
        }
    }
}
