using System;

namespace InsuranceSystemWeb.Models
{
    // Cost calculation (subclasses override the monthly cost)
    public partial class Insurance
    {
        // Monthly cost: base rate x tier multiplier
        public virtual decimal CalculateTotalCost()
        {
            return PricingRates.BaseMonthlyRatePerTier * (int)Tier;
        }

        // Annual billing gets a 15% discount
        public decimal CalculateTotalCost(bool isAnnual)
        {
            decimal monthlyCost = CalculateTotalCost();

            if (!isAnnual) return monthlyCost;

            return Math.Round(monthlyCost * 12 * PricingRates.AnnualDiscountMultiplier, 2);
        }

        // A valid promo code gives an extra 10% discount
        public decimal CalculateTotalCost(bool isAnnual, string promoCode)
        {
            decimal cost = CalculateTotalCost(isAnnual);

            if (!IsPromoCodeValid(promoCode)) return cost;

            return Math.Round(cost * PricingRates.PromoDiscountMultiplier, 2);
        }

        private static bool IsPromoCodeValid(string promoCode)
        {
            return !string.IsNullOrWhiteSpace(promoCode)
                && string.Equals(promoCode.Trim(), PricingRates.PromoCode, StringComparison.OrdinalIgnoreCase);
        }
    }
}
