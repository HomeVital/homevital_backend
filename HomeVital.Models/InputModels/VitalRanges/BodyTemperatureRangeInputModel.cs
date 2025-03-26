namespace HomeVital.Models.InputModels

{
    public class BodyTemperatureRangeInputModel
    {
        public int PatientID { get; set; }

        // Body Temperature
        // 1 36.0° to 37.0° is good
        public double? TemperatureGoodMin { get; set; }
        public double? TemperatureGoodMax { get; set; }
        // 2 Under 35.9° is under average
        public double? TemperatureUnderAverage { get; set; }
        // 3 37.1° to 38.0° is over average
        public double? TemperatureNotOkMin { get; set; }
        public double? TemperatureNotOkMax { get; set; }
        // 4 38.1° and above is critical
        public double? TemperatureCriticalMin { get; set; }
        public double? TemperatureCriticalMax { get; set; }
 
    }
}