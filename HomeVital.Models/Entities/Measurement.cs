namespace HomeVital.Models.Entities
{
    public class Measurement
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public List<BloodPressure> BloodPressure { get; set; } = new List<BloodPressure>();
        public List<Bloodsugar> BloodSugar { get; set; } = new List<Bloodsugar>();
        public List<BodyWeight> BodyWeight { get; set; } = new List<BodyWeight>();
        // public List<OxygenSaturation> OxygenSaturation { get; set; } = new List<OxygenSaturation>();
        // public List<Temperature> Temperature { get; set; } = new List<Temperature>();
        // public List<Weight> Weight { get; set; } = new List<Weight>();
        
    }
}