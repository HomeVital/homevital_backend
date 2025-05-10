namespace HomeVital.Models.Entities
{
    public class OxygenSaturationRange 
    {
        public int Id { get; set; }
        public int PatientID { get; set; }

        // Oxygen Saturation
        public double OxygenSaturationGood { get; set; } = 95;
        public double OxygenSaturationRaised { get; set; } = 93;
        public double OxygenSaturationHigh { get; set; } = 91;
    }
        
}