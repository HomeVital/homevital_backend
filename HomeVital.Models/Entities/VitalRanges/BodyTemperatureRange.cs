namespace HomeVital.Models.Entities
{
    public class BodyTemperatureRange 
    {
        public int Id { get; set; }
        public int PatientID { get; set; }
        
        // Body Temperature
        public double TemperatureUnderAverage { get; set; } = 35.9; // Raised
        public double TemperatureGood { get; set; } = 36.0; // Normal
        public double TemperatureNotOk { get; set; } = 37.1; // Raised
        public double TemperatureCritical { get; set; } = 38.1; // High
    }
}