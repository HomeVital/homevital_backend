namespace HomeVital.Models.Entities
{
    public class PatientPlan
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PatientID { get; set; }
        public Patient Patient { get; set; } = null!;
        public string Instructions { get; set; } = string.Empty;

        public List<MeasurementPlan> MeasurementPlans { get; set; } = new List<MeasurementPlan>();
        public string Status { get; set; } = string.Empty; // e.g., Active, Inactive, Completed
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }

    public class MeasurementPlan
    {
        public int ID { get; set; }
        public int TimesPerWeek { get; set; }
        public int PatientPlanID { get; set; }
        public PatientPlan PatientPlan { get; set; } = null!;
        // list of measurement types
        public List<string> MeasurementTypes { get; set; } = new List<string>();
    }

    public static class MeasurementTypes
    {
        public const string OxygenSaturation = "OxygenSaturation";
        public const string BloodSugar = "BloodSugar";
        public const string BloodPressure = "BloodPressure";
        public const string BodyTemperature = "BodyTemperature";
        public const string Weight = "Weight";
    }
}