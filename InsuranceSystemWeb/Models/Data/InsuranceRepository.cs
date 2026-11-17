using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace InsuranceSystemWeb.Models
{
    // Policy storage, saved to App_Data/insurances.json so data survives a restart
    public static class InsuranceRepository
    {
        private static readonly List<Insurance> _insurances = new List<Insurance>();
        private static readonly string _filePath = HostingEnvironment.MapPath("~/App_Data/insurances.json");
        private static readonly object _fileLock = new object();

        // Load saved data, or seed sample data on first run
        static InsuranceRepository()
        {
            if (File.Exists(_filePath))
            {
                LoadFromFile();
                return;
            }

            var device = new DeviceInsurance("Ahmet Yilmaz", "ahmet@example.com", CoverageTier.Standard, "iPhone 16 Pro", 1200M, true);
            var home = new HomeInsurance("Zeynep Kaya", "zeynep@example.com", CoverageTier.Premium, "Istanbul", 140, true);

            device.Status = InsuranceStatus.Active;

            _insurances.Add(device);
            _insurances.Add(home);

            SaveToFile();
        }

        public static List<Insurance> GetAll() => _insurances;

        public static List<Insurance> GetAllInsurances() => _insurances;

        public static void AddInsurance(Insurance insurance)
        {
            if (insurance == null) return;

            _insurances.Add(insurance);
            SaveToFile();
        }

        // Case-insensitive search by tracking ID
        public static Insurance FindById(string insuranceId)
        {
            if (string.IsNullOrWhiteSpace(insuranceId)) return null;

            string id = insuranceId.Trim();

            return _insurances.FirstOrDefault(i => string.Equals(i.InsuranceId, id, StringComparison.OrdinalIgnoreCase));
        }

        public static bool UpdateStatus(string insuranceId, InsuranceStatus newStatus)
        {
            Insurance insurance = FindById(insuranceId);

            if (insurance == null) return false;

            insurance.Status = newStatus;
            SaveToFile();
            return true;
        }

        public static bool DeleteInsurance(string insuranceId)
        {
            Insurance insurance = FindById(insuranceId);

            if (insurance == null || !_insurances.Remove(insurance)) return false;

            SaveToFile();
            return true;
        }

        // JSON file helpers
        private static void LoadFromFile()
        {
            string json = File.ReadAllText(_filePath);
            var records = new JavaScriptSerializer().Deserialize<List<InsuranceRecord>>(json);

            if (records == null) return;

            foreach (InsuranceRecord record in records)
            {
                _insurances.Add(record.ToInsurance());
            }
        }

        private static void SaveToFile()
        {
            lock (_fileLock)
            {
                List<InsuranceRecord> records = _insurances.Select(InsuranceRecord.FromInsurance).ToList();
                string json = new JavaScriptSerializer().Serialize(records);

                Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
                File.WriteAllText(_filePath, json);
            }
        }
    }
}
