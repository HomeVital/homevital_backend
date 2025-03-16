namespace HomeVital.Models.Entities
{
    public class Bloodsugar
    {
        public int ID { get; set; }
        public int PatientID { get; set; } 
        public float BloodsugarLevel { get; set; } = 0;
        public DateTime Date { get; set; } = new DateTime();
        // public string Status { get; set; } = string.Empty;
    }
}