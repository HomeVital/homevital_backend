namespace HomeVital.Models.Entities
{
    public class BloodPressureRange 
    {
        public int Id { get; set; }
        public int PatientID { get; set; }

        public int SystolicLowered { get; set; } = 90;
        public int SystolicGood { get; set; } = 120;
        public int SystolicRaised { get; set; } = 140;
        public int SystolicHigh { get; set; } = 160;

        public int DiastolicLowered { get; set; } = 60;
        public int DiastolicGood { get; set; } = 80;
        public int DiastolicRaised { get; set; } = 90;
        public int DiastolicHigh { get; set; } = 100;
    }
}