namespace HomeVital.Models.Dtos

{
    public class BodyWeightDto
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public float Weight { get; set; } = 0;
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}