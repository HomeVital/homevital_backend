namespace HomeVital.Models.Dtos
{
    public class BloodSugarRangeDto
    {
        public int Id { get; set; }
        public int PatientID { get; set; }

        // Blood Sugar in mmol/L
        // 1 4.0 to 6.0 is good (normal)
        public double BloodSugarGoodMin { get; set; } = 4;
        public double BloodSugarGoodMax { get; set; } = 6;
        // 2 6.1 to 7.9 is not ok (raised)
        public double BloodSugarNotOkMin { get; set; } = 6.01;
        public double BloodSugarNotOkMax { get; set; } = 7.9;  
        // 3 8.0 to 10.0 is not ok (high)
        public double BloodSugarCriticalMin { get; set; } = 8.0;
        public double BloodSugarCriticalMax { get; set; } = 10;
        // 4 2 to 3.9 is not ok (too low, high)
        public double BloodSugarlowMin { get; set; } = 2;
        public double BloodSugarlowMax { get; set; } = 3.99;

    }
}
