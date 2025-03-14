namespace HomeVital.Models.Dtos
{
    public class BloodPressureRangeDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

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
    }
}