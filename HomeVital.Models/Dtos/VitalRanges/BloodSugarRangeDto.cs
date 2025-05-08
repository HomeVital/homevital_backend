namespace HomeVital.Models.Dtos
{
    public class BloodSugarRangeDto
    {
        public int Id { get; set; }
        public int PatientID { get; set; }

        // Blood Sugar in mmol/L
        public double BloodSugarGood { get; set; }
        public double BloodSugarRaised { get; set; }
        public double BloodSugarHigh { get; set; }

    }
}
