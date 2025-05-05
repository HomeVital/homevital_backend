namespace HomeVital.Models.Entities
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? TeamLeaderID { get; set; }
        public string? TeamLeaderName { get; set; } = string.Empty;

        public ICollection<HealthcareWorker> HealthcareWorkers { get; set; } = new List<HealthcareWorker>();
        public ICollection<Patient> Patients { get; set; } = new List<Patient>();

             // Computed properties
        public IEnumerable<int> WorkerIDs => HealthcareWorkers.Select(hw => hw.ID);
        public IEnumerable<int> PatientIDs => Patients.Select(p => p.ID);
    }

}

