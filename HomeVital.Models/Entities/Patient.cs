using HomeVital.Models.Enums;
namespace HomeVital.Models.Entities
{
    public class Patient
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        // public string Status { get; set; } = string.Empty;
        public string Status { get; set; } = ActivityStatus.Inactive.ToString(); // Default status is "Inactive"
        public string Address { get; set; } = string.Empty;
        public int? TeamID { get; set; } = 0;
        public Team Team { get; set; } = null!; // Navigation property

        public ICollection<Bloodsugar> Bloodsugars {get; set; } = new List<Bloodsugar>();
        public ICollection<BloodPressure> BloodPressures {get; set; } = new List<BloodPressure>();
        public ICollection<OxygenSaturation> OxygenSaturations {get; set; } = new List<OxygenSaturation>();
        public ICollection<BodyWeight> BodyWeights {get; set; } = new List<BodyWeight>();
        public ICollection<BodyTemperature> BodyTemperatures {get; set; } = new List<BodyTemperature>();
        public ICollection<PatientPlan> PatientPlans { get; set; } = new List<PatientPlan>();

        public void UpdateStatusBasedOnPlans()
        {
            // If there's at least one active plan, set status to "Active"
            // Otherwise, set status to "Inactive"
            Status = PatientPlans.Any(p => p.IsActive) ? "Active" : "Inactive";
        }

    }
}