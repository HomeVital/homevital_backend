namespace HomeVital.Models.InputModels
{
    public class BloodSugarRangeInputModel
    {
        public int PatientId { get; set; }

        // Blood Sugar in mmol/L
        public double BloodSugarGood { get; set; }
        public double BloodSugarRaised { get; set; }
        public double BloodSugarHigh { get; set; }
    }
}