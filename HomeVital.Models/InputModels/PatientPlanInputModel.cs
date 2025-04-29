namespace HomeVital.Models.InputModels
{
    public class PatientPlanInputModel
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PatientID { get; set; }
        public string Instructions { get; set; } = string.Empty;
        public int TeamID { get; set; } = 0; // Team ID for the plan

        public int WeightMeasurementFrequency { get; set; } // Number of times Weight is measured per week
        public int BloodSugarMeasurementFrequency { get; set; } // Number of times Blood Sugar is measured per week
        public int BloodPressureMeasurementFrequency { get; set; } // Number of times Blood Pressure is measured per week
        public int OxygenSaturationMeasurementFrequency { get; set; } // Number of times Oxygen Saturation is measured per week
        public int BodyTemperatureMeasurementFrequency { get; set; } // Number of times Body Temperature is measured per week
    }        
}