using System;
using System.Collections.Generic;

namespace InsuranceSystemWeb.Models
{
    // Flat copy of a policy, used only for saving to / loading from the JSON file
    public class InsuranceRecord
    {
        public string Type { get; set; }
        public string InsuranceId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public CoverageTier Tier { get; set; }
        public InsuranceStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> ActivityLogs { get; set; }

        // Device fields
        public string DeviceModel { get; set; }
        public decimal DeviceValue { get; set; }
        public bool IncludeScreenCare { get; set; }

        // Home fields
        public string City { get; set; }
        public int AreaSqM { get; set; }
        public bool IncludeApplianceCare { get; set; }

        // Travel fields
        public string Destination { get; set; }
        public int DurationDays { get; set; }
        public bool IncludeBaggageLoss { get; set; }

        // Policy -> record (before saving)
        public static InsuranceRecord FromInsurance(Insurance insurance)
        {
            var record = new InsuranceRecord
            {
                InsuranceId = insurance.InsuranceId,
                CustomerName = insurance.CustomerName,
                CustomerEmail = insurance.CustomerEmail,
                Tier = insurance.Tier,
                Status = insurance.Status,
                CreatedDate = insurance.CreatedDate,
                ActivityLogs = insurance.ActivityLogs
            };

            if (insurance is DeviceInsurance device)
            {
                record.Type = "Device";
                record.DeviceModel = device.DeviceModel;
                record.DeviceValue = device.DeviceValue;
                record.IncludeScreenCare = device.IncludeScreenCare;
            }
            else if (insurance is HomeInsurance home)
            {
                record.Type = "Home";
                record.City = home.City;
                record.AreaSqM = home.AreaSqM;
                record.IncludeApplianceCare = home.IncludeApplianceCare;
            }
            else if (insurance is TravelInsurance travel)
            {
                record.Type = "Travel";
                record.Destination = travel.Destination;
                record.DurationDays = travel.DurationDays;
                record.IncludeBaggageLoss = travel.IncludeBaggageLoss;
            }

            return record;
        }

        // Record -> policy (after loading)
        public Insurance ToInsurance()
        {
            Insurance insurance;

            switch (Type)
            {
                case "Home":
                    insurance = new HomeInsurance(CustomerName, CustomerEmail, Tier, City, AreaSqM, IncludeApplianceCare);
                    break;

                case "Travel":
                    insurance = new TravelInsurance(CustomerName, CustomerEmail, Tier, Destination, DurationDays, IncludeBaggageLoss);
                    break;

                default:
                    insurance = new DeviceInsurance(CustomerName, CustomerEmail, Tier, DeviceModel, DeviceValue, IncludeScreenCare);
                    break;
            }

            // JSON dates are read back as UTC, so convert to local time
            insurance.Restore(InsuranceId, CreatedDate.ToLocalTime(), Status, ActivityLogs);

            return insurance;
        }
    }
}
