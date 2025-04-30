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

        public int[] WeightMeasurementDays { get; set; } = new int[7];
        public int[] BloodSugarMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        public int[] BloodPressureMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        public int[] OxygenSaturationMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        public int[] BodyTemperatureMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
    }        
}