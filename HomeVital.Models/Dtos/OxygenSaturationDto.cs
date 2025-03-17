namespace HomeVital.Models.Dtos
{
    public class OxygenSaturationDto
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public float OxygenSaturationLevel { get; set; } = 0;
        public DateTime Date { get; set; } = new DateTime();
        // public string Status { get; set; } = string.Empty;
    }
}