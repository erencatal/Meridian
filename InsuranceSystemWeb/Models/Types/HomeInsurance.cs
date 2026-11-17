using System;

namespace InsuranceSystemWeb.Models
{
    // Home coverage; price scales with living area
    public class HomeInsurance : Insurance
    {
        public string City { get; set; }
        public int AreaSqM { get; set; }
        public bool IncludeApplianceCare { get; set; }

        public HomeInsurance(string customerName, string customerEmail, CoverageTier tier,
                             string city, int areaSqM, bool includeApplianceCare)
            : base(customerName, customerEmail, tier)
        {
            City = city;
            AreaSqM = areaSqM;
            IncludeApplianceCare = includeApplianceCare;
        }

        public override string GetCategoryName() => "Home Insurance";

        public override decimal CalculateTotalCost()
        {
            decimal cost = base.CalculateTotalCost() + AreaSqM * PricingRates.HomeRatePerSqMeter;

            if (IncludeApplianceCare) cost += PricingRates.HomeApplianceCareFee;

            return Math.Round(cost, 2);
        }
    }
}
