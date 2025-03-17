namespace HomeVital.Models.Dtos
{
    public class VitalRangeDto
    {
        public BodyTemperatureRangeDto? BodyTemperatureRange { get; set; }
        public BloodPressureRangeDto? BloodPressureRange { get; set; }
        public BloodSugarRangeDto? BloodSugarRange { get; set; }
        public BodyWeightRangeDto? BodyWeightRange { get; set; }
        public OxygenSaturationRangeDto? OxygenSaturationRange { get; set; }
    }
}