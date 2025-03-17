namespace HomeVital.Models.Dtos
{
    public class OxygenSaturationDto
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int OxygenSaturationValue { get; set; } = 0;
        public DateTime Date { get; set; } = new DateTime();
    }
}