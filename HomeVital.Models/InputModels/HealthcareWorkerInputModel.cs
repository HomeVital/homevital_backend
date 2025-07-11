namespace HomeVital.Models.InputModels
{
    public class HealthcareWorkerInputModel
    {
        // public int ID { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        // TeamID is a list of team ids
        public List<int>? TeamIDs { get; set; } = new List<int>();
        public string? Status { get; set; } = string.Empty;
        public string? Kennitala { get; set; } = string.Empty;
    }
}