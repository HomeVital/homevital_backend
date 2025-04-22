namespace HomeVital.Models.Entities
{
    public class Measurement
    {
        public int ID { get; set; }

        public int PatientID { get; set; }
        public Patient Patient { get; set; } = null!;
        
        public List<BloodPressure> BloodPressure { get; set; } = new List<BloodPressure>();
        public List<Bloodsugar> BloodSugar { get; set; } = new List<Bloodsugar>();
        public List<BodyWeight> BodyWeight { get; set; } = new List<BodyWeight>();
        public List<BodyTemperature> BodyTemperature { get; set; } = new List<BodyTemperature>();
        
    }
}