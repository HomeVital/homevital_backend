namespace HomeVital.Models.Entities
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<int> WorkerIDs { get; set; } = new List<int>();
        public List<int> PatientIDs { get; set; } = new List<int>();
    }

}

