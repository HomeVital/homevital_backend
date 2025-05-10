namespace HomeVital.Models.InputModels
{
    public class SagaAckInputModel
    {
        // Take in measurement type and ID
        public string MeasurementType { get; set; } = string.Empty; // "BloodPressure", "BloodSugar", etc.
        public int MeasurementID { get; set; }
    }
}