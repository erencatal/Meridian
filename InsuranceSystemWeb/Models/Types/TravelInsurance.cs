using System;

namespace InsuranceSystemWeb.Models
{
    // Travel coverage; price scales with trip length
    public class TravelInsurance : Insurance
    {
        public string Destination { get; set; }
        public int DurationDays { get; set; }
        public bool IncludeBaggageLoss { get; set; }

        public TravelInsurance(string customerName, string customerEmail, CoverageTier tier,
                               string destination, int durationDays, bool includeBaggageLoss)
            : base(customerName, customerEmail, tier)
        {
            Destination = destination;
            DurationDays = durationDays;
            IncludeBaggageLoss = includeBaggageLoss;
        }

        public override string GetCategoryName() => "Travel Insurance";

        public override decimal CalculateTotalCost()
        {
            decimal cost = base.CalculateTotalCost() + DurationDays * PricingRates.TravelDailyRate;

            if (IncludeBaggageLoss) cost += PricingRates.TravelBaggageLossFee;

            return Math.Round(cost, 2);
        }
    }
}
