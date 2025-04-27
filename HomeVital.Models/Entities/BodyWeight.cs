namespace HomeVital.Models.Entities
{
    public class BodyWeight
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public Patient Patient { get; set; } = null!; // Navigation property
        public decimal Weight { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}