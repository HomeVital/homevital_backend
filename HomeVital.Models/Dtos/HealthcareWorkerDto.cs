namespace HomeVital.Models.Dtos
{
    public class HealthcareWorkerDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int TeamID { get; set; } = 0;
        public string Status { get; set; } = string.Empty;
    }
}