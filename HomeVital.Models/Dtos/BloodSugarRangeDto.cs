namespace HomeVital.Models.Dtos
{
    public class BloodSugarRangeDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

        // Blood Sugar
        public double BloodSugarGoodMin { get; set; }
        public double BloodSugarGoodMax { get; set; }
        public double BloodSugarOkMin { get; set; }
        public double BloodSugarOkMax { get; set; }
        public double BloodSugarNotOkMin { get; set; }
        public double BloodSugarNotOkMax { get; set; }
        public double BloodSugarCriticalMin { get; set; }
        public double BloodSugarCriticalMax { get; set; }

    }
}