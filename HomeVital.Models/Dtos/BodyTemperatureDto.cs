namespace HomeVital.Models.Dtos
{
    public class BodyTemperatureDto
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public float Temperature { get; set; } = 0;
        public DateTime Date { get; set; } = new DateTime();
    }
}