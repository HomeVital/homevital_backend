namespace HomeVital.Models.Dtos
{
    public class BloodsugarDto
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int BloodsugarLevel { get; set; }
        public DateTime Date { get; set; }
        // public string Status { get; set; } = string.Empty;
    }
}