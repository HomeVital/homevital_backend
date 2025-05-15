
namespace HomeVital.Models.Entities
{
    public class BloodPressure
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public Patient Patient { get; set; } = null!; // Navigation property
        public string MeasuredHand { get; set; } = string.Empty;
        public string BodyPosition { get; set; } = string.Empty;
        public int Systolic { get; set; } = 0;
        public int Diastolic { get; set; } = 0;
        public DateTime Date { get; set; } = new DateTime();
        public string Status { get; set; } = string.Empty;
        public int Pulse { get; set; } = 0;

        // ACK
        public bool IsAcknowledged { get; set; } = false;
        public int? AcknowledgedByWorkerID { get; set; }
        public DateTime? AcknowledgedDate { get; set; }
        public string ResolutionNotes { get; set; } = string.Empty;

        // Saga
        public bool IsStoredInSaga { get; set; } = false;
    }
}