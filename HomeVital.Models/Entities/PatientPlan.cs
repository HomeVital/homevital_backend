using HomeVital.Models.Enums;
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
        // Team
        public int TeamID { get; set; }
        public Team Team { get; set; } = null!; 

        public bool IsActive => EndDate == null || EndDate > DateTime.UtcNow;

        // array of length 7 for each day of the week, 0 = no measurement, 1 = measurement
        public int[] WeightMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        public int[] BloodSugarMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        public int[] BloodPressureMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        public int[] OxygenSaturationMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        public int[] BodyTemperatureMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
    }
}