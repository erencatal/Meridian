using System;
using System.Collections.Generic;

namespace InsuranceSystemWeb.Models
{
    // Base policy: identity, customer info, status and activity log
    // Pricing  -> Insurance.Pricing.cs
    // Display  -> Insurance.Display.cs
    public partial class Insurance : IInsurance
    {
        public string InsuranceId { get; private set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

        public CoverageTier Tier { get; set; }
        public DateTime CreatedDate { get; private set; }
        public List<string> ActivityLogs { get; private set; }

        public event InsuranceStatusChangedEventHandler StatusChanged;

        private InsuranceStatus _status;

        public InsuranceStatus Status
        {
            get => _status;
            set
            {
                if (_status == value) return;

                _status = value;
                StatusChanged?.Invoke(InsuranceId, _status);
            }
        }

        public Insurance(string customerName, string customerEmail, CoverageTier tier)
        {
            InsuranceId = "INS-" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            Tier = tier;
            CreatedDate = DateTime.Now;
            ActivityLogs = new List<string>();

            _status = InsuranceStatus.Pending;
            StatusChanged += (id, newStatus) => LogActivity($"Status updated to: {newStatus}");

            LogActivity("Insurance created with status: Pending.");
        }

        // Put back saved values when a policy is loaded from the JSON file
        internal void Restore(string insuranceId, DateTime createdDate, InsuranceStatus status, List<string> activityLogs)
        {
            InsuranceId = insuranceId;
            CreatedDate = createdDate;
            _status = status;
            ActivityLogs = activityLogs ?? new List<string>();
        }

        private void LogActivity(string message)
        {
            ActivityLogs.Add($"{DateTime.Now:yyyy-MM-dd HH:mm} - {message}");
        }
    }
}
