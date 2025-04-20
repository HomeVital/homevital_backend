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
        public string MeasurementType { get; set; } = string.Empty; // e.g., Blood Pressure, Blood Sugar, etc.
        public string MeasurementSchedule { get; set; } = string.Empty; // e.g., Morning, Afternoon, Evening
        public int MeasurementFrequency { get; set; } // e.g., 1 for once a day, 2 for twice a day

        
    }

    public class MeasurementTypes
    {
        public const string OxygenSaturation = "OxygenSaturation";
        public const string BloodSugar = "BloodSugar";
        public const string BloodPressure = "BloodPressure";
        public const string BodyTemperature = "BodyTemperature";
        public const string Weight = "Weight";
    }
    public class MeasurementSchedule
    {
        public const string Morning = "Morning";
        public const string Afternoon = "Afternoon";
        public const string Evening = "Evening";
    }
    public class MeasurementFrequency
    {
        public const int OnceADay = 1;
        public const int TwiceADay = 2;
        public const int ThreeTimesADay = 3;
    }
    public class MeasurementPlanStatus
    {
        public const string Active = "Active";
        public const string Inactive = "Inactive";
        public const string Completed = "Completed";
    }
}