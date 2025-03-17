namespace HomeVital.Models.InputModels
{
    public class BloodSugarRangeInputModel
    {
        public int PatientId { get; set; }

        // Blood Sugar
        // 1 80 to 100 is good (Good)
        public double? BloodSugarGoodMin { get; set; }
        public double? BloodSugarGoodMax { get; set; }
        // 2 101 to 125 is not ok (elevated)
        public double? BloodSugarNotOkMin { get; set; }
        public double? BloodSugarNotOkMax { get; set; }
        // 3 over 126  is critical (high)
        public double? BloodSugarCriticalMin { get; set; }
        public double? BloodSugarCriticalMax { get; set; }
        // 4 under 80 is not ok (low)
        public double? BloodSugarlowMin { get; set; }
        public double? BloodSugarlowMax { get; set; }
    }
}