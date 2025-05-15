namespace HomeVital.Models.InputModels
{
    public class MeasurementAckInputModel
    {
        public string MeasurementType { get; set; } = string.Empty; // "BloodPressure", "BloodSugar", etc.
        public int MeasurementID { get; set; }
        public int WorkerID { get; set; }
        public string ResolutionNotes { get; set; } = string.Empty;
    }
}