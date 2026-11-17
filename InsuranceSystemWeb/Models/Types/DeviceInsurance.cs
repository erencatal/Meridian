using System;

namespace InsuranceSystemWeb.Models
{
    // Electronic device coverage; price scales with device value
    public class DeviceInsurance : Insurance
    {
        public string DeviceModel { get; set; }
        public decimal DeviceValue { get; set; }
        public bool IncludeScreenCare { get; set; }

        public DeviceInsurance(string customerName, string customerEmail, CoverageTier tier,
                               string deviceModel, decimal deviceValue, bool includeScreenCare)
            : base(customerName, customerEmail, tier)
        {
            DeviceModel = deviceModel;
            DeviceValue = deviceValue;
            IncludeScreenCare = includeScreenCare;
        }

        public override string GetCategoryName() => "Device Insurance";

        public override decimal CalculateTotalCost()
        {
            decimal cost = base.CalculateTotalCost() + DeviceValue * PricingRates.DeviceValueRate;

            if (IncludeScreenCare) cost += PricingRates.DeviceScreenCareFee;

            return Math.Round(cost, 2);
        }
    }
}
