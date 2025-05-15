namespace HomeVital.Models.Dtos
{
    public class BodyTemperatureRangeDto
    {
        public int Id { get; set; }
        public int PatientID { get; set; }

        // Body Temperature
        public double TemperatureUnderAverage { get; set; } // Raised
        public double TemperatureGood { get; set; }// Normal
        public double TemperatureNotOk { get; set; } // Raised
        public double TemperatureCritical { get; set; } // High
    }
}