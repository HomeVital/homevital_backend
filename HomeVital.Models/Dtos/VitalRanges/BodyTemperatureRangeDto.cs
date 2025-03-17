namespace HomeVital.Models.Dtos
{
    public class BodyTemperatureRangeDto
    {
        public int Id { get; set; }
        public int PatientID { get; set; }

        // Body Temperature
        // 1 36.0° to 37.0° is good
        public double TemperatureGoodMin { get; set; } = 36.0;
        public double TemperatureGoodMax { get; set; } = 37.0;
        // 2 under 35.9° is under average
        public double TemperatureUnderAverage { get; set; } = 35.9;
        // 3 37.1° to 38.0° is over average
        public double TemperatureNotOkMin { get; set; } = 37.1;
        public double TemperatureNotOkMax { get; set; } = 38.0;
        // 4 38.1° and above is critical
        public double TemperatureCriticalMin { get; set; } = 38.1;
        public double TemperatureCriticalMax { get; set; } = 42.2;

    }
}