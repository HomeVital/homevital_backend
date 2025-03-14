namespace HomeVital.Models.Dtos
{
    public class VitalRangeDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

        // Oxygen Saturation
        public double OxygenSaturationGoodMin { get; set; }
        public double OxygenSaturationGoodMax { get; set; }
        public double OxygenSaturationOkMin { get; set; }
        public double OxygenSaturationOkMax { get; set; }
        public double OxygenSaturationNotOkMin { get; set; }
        public double OxygenSaturationNotOkMax { get; set; }
        public double OxygenSaturationCriticalMin { get; set; }
        public double OxygenSaturationCriticalMax { get; set; }

        // Blood Pressure
        public double SystolicGoodMin { get; set; }
        public double SystolicGoodMax { get; set; }
        public double SystolicOkMin { get; set; }
        public double SystolicOkMax { get; set; }
        public double SystolicNotOkMin { get; set; }
        public double SystolicNotOkMax { get; set; }
        public double SystolicCriticalMin { get; set; }
        public double SystolicCriticalMax { get; set; }

        public double DiastolicGoodMin { get; set; }
        public double DiastolicGoodMax { get; set; }
        public double DiastolicOkMin { get; set; }
        public double DiastolicOkMax { get; set; }
        public double DiastolicNotOkMin { get; set; }
        public double DiastolicNotOkMax { get; set; }
        public double DiastolicCriticalMin { get; set; }
        public double DiastolicCriticalMax { get; set; }

        // Heart Rate
        public double HeartRateGoodMin { get; set; }
        public double HeartRateGoodMax { get; set; }
        public double HeartRateOkMin { get; set; }
        public double HeartRateOkMax { get; set; }
        public double HeartRateNotOkMin { get; set; }
        public double HeartRateNotOkMax { get; set; }
        public double HeartRateCriticalMin { get; set; }
        public double HeartRateCriticalMax { get; set; }

        // Temperature
        public double TemperatureGoodMin { get; set; }
        public double TemperatureGoodMax { get; set; }
        public double TemperatureOkMin { get; set; }
        public double TemperatureOkMax { get; set; }
        public double TemperatureNotOkMin { get; set; }
        public double TemperatureNotOkMax { get; set; }
        public double TemperatureCriticalMin { get; set; }
        public double TemperatureCriticalMax { get; set; }

        // Blood Sugar
        public double BloodSugarGoodMin { get; set; }
        public double BloodSugarGoodMax { get; set; }
        public double BloodSugarOkMin { get; set; }
        public double BloodSugarOkMax { get; set; }
        public double BloodSugarNotOkMin { get; set; }
        public double BloodSugarNotOkMax { get; set; }
        public double BloodSugarCriticalMin { get; set; }
        public double BloodSugarCriticalMax { get; set; }

        // Body Weight
        public double BodyWeightGoodMin { get; set; }
        public double BodyWeightGoodMax { get; set; }
        public double BodyWeightOkMin { get; set; }
        public double BodyWeightOkMax { get; set; }
        public double BodyWeightNotOkMin { get; set; }
        public double BodyWeightNotOkMax { get; set; }
        public double BodyWeightCriticalMin { get; set; }
        public double BodyWeightCriticalMax { get; set; }
    }
}