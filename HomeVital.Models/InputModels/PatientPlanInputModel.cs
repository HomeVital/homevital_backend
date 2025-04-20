namespace HomeVital.Models.InputModels
{
    public class PatientPlanInputModel
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PatientID { get; set; }
        public string Instructions { get; set; } = string.Empty;

        public List<MeasurementPlanInputModel> MeasurementPlans { get; set; } = new List<MeasurementPlanInputModel>();
    }

    public class MeasurementPlanInputModel
    {
        public string MeasurementType { get; set; } = string.Empty;
        public int MeasurementFrequency { get; set; }
        public string MeasurementSchedule { get; set; } = string.Empty;
    }
}