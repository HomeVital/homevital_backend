namespace HomeVital.Models.Dtos
{
    public class HealthcareWorkerDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Team { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}