namespace HomeVital.Models.Entities
{
    public class BodyWeight
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public Patient Patient { get; set; } = null!; // Navigation property
        public float Weight { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;

        // ACK
        public bool IsAcknowledged { get; set; } = false;
        public int? AcknowledgedByWorkerID { get; set; }
        public DateTime? AcknowledgedDate { get; set; }
        public string ResolutionNotes { get; set; } = string.Empty;
    }
}