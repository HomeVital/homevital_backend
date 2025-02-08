namespace HomeVital.Models.InputModels
{
    public class BloodPressureInputModel
    {
        public int PatientID { get; set; }
        public string MeasureHand { get; set; } = string.Empty;
        public string BodyPosition { get; set; } = string.Empty;
        public int Systolic { get; set; } = 0;
        public int Diastolic { get; set; } = 0;
        public int Pulse { get; set; } = 0;
        public DateTime Date { get; set; } = new DateTime();
        public string Status { get; set; } = string.Empty;
    }
}