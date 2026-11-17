namespace InsuranceSystemWeb.Models
{
    // Every price, fee and discount used in the system
    public static class PricingRates
    {
        // General
        public const decimal BaseMonthlyRatePerTier = 10.00M;
        public const decimal AnnualDiscountMultiplier = 0.85M;
        public const decimal PromoDiscountMultiplier = 0.90M;
        public const string PromoCode = "SAVE10";

        // Device
        public const decimal DeviceValueRate = 0.015M;
        public const decimal DeviceScreenCareFee = 4.50M;

        // Home
        public const decimal HomeRatePerSqMeter = 0.12M;
        public const decimal HomeApplianceCareFee = 8.00M;

        // Travel
        public const decimal TravelDailyRate = 1.25M;
        public const decimal TravelBaggageLossFee = 6.00M;
    }
}
