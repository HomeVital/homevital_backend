namespace HomeVital.Models.Entities
{
    public class BodyTemperature
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public Patient Patient { get; set; } = null!; // Navigation property
        public float Temperature { get; set; } = 0;
        public DateTime Date { get; set; } = new DateTime();
        public string Status { get; set; } = string.Empty;
    }
}