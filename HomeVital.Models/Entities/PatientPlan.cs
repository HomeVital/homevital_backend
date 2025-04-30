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
        public Team Team { get; set; } = null!; // Navigation property

        public bool IsActive => EndDate == null || EndDate > DateTime.UtcNow;

        // boolean property if the measurement Weight is in the plan
        // public int WeightMeasurementFrequency { get; set; } // Number of times Weight is measured per week
        // array of length 7 for each day of the week, 0 = no measurement, 1 = morning, 2 = evening
        public int[] WeightMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        // public int BloodSugarMeasurementFrequency { get; set; } // Number of times Blood Sugar is measured per week
        public int[] BloodSugarMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        // public int BloodPressureMeasurementFrequency { get; set; } // Number of times Blood Pressure is measured per week
        public int[] BloodPressureMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        // public int OxygenSaturationMeasurementFrequency { get; set; } // Number of times Oxygen Saturation is measured per week
        public int[] OxygenSaturationMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        // public int BodyTemperatureMeasurementFrequency { get; set; } // Number of times Body Temperature is measured per week
        public int[] BodyTemperatureMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
    }
}