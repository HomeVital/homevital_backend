namespace HomeVital.Models.Entities
{
    public class OxygenSaturation
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public Patient Patient { get; set; } = null!; // Navigation property
        public int OxygenSaturationValue { get; set; } = 0;
        public DateTime Date { get; set; } = new DateTime();
        public string Status { get; set; } = string.Empty;

    }
}