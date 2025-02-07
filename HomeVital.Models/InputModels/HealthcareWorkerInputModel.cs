namespace HomeVital.Models.InputModels
{
    public class HealthcareWorkerInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Team { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}