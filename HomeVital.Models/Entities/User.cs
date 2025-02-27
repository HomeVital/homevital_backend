

namespace HomeVital.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Kennitala { get; set; } = string.Empty;
        public int? PatientID { get; set; }
        public int? HealthcareWorkerID { get; set; }
    }
}
