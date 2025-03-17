namespace HomeVital.Models.InputModels

{
    public class BodyTemperatureRangeInputModel
    {
        public int PatientID { get; set; }

        // Body Temperature
        // 1 36.0° to 37.0° is good
        public double? BodyTemperatureGoodMin { get; set; }
        public double? BodyTemperatureGoodMax { get; set; }
        // 2 Under 35.9° is under average
        public double? BodyTemperatureUnderAverage { get; set; }
        // 3 37.1° to 38.0° is over average
        public double? BodyTemperatureNotOkMin { get; set; }
        public double? BodyTemperatureNotOkMax { get; set; }
        // 4 38.1° and above is critical
        public double? BodyTemperatureCriticalMin { get; set; }
        public double? BodyTemperatureCriticalMax { get; set; }
 
    }
}