namespace HomeVital.Models.Entities
{
    public class BodyWeight
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public double Weight { get; set; }
        public DateTime Date { get; set; }
    }
}