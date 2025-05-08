namespace HomeVital.Models.InputModels

{
    public class BodyTemperatureRangeInputModel
    {
        public int PatientID { get; set; }

        // Body Temperature
        public double TemperatureUnderAverage { get; set; } // Raised
        public double TemperatureGood { get; set; }// Normal
        public double TemperatureNotOk { get; set; } // Raised
        public double TemperatureCritical { get; set; } // High
        
 
    }
}