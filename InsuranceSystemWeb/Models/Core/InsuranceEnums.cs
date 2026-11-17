namespace InsuranceSystemWeb.Models
{
    // Lifecycle state of a policy
    public enum InsuranceStatus
    {
        Pending,
        Active,
        Expired,
        Cancelled
    }

    // Coverage level; the value is used as a price multiplier
    public enum CoverageTier
    {
        Basic = 1,
        Standard = 2,
        Premium = 3
    }
}
