using System;
using System.Collections.Generic;

namespace InsuranceSystemWeb.Models
{
    // Fired when an insurance policy's status changes
    public delegate void InsuranceStatusChangedEventHandler(string insuranceId, InsuranceStatus newStatus);

    // Contract that every insurance type must fulfill
    public interface IInsurance
    {
        string InsuranceId { get; }
        string CustomerName { get; set; }
        string CustomerEmail { get; set; }

        CoverageTier Tier { get; set; }
        InsuranceStatus Status { get; set; }
        DateTime CreatedDate { get; }
        List<string> ActivityLogs { get; }

        event InsuranceStatusChangedEventHandler StatusChanged;

        decimal CalculateTotalCost();
        decimal CalculateTotalCost(bool isAnnual);
        decimal CalculateTotalCost(bool isAnnual, string promoCode);

        string GetCategoryName();
        string GetSummaryHtml();
    }
}
