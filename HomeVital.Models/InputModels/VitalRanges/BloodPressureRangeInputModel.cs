namespace HomeVital.Models.InputModels
{
    public class BloodPressureRangeInputModel
    {
        public int PatientID { get; set; }

        // Blood Pressure
        public int SystolicLowered { get; set; }
        public int SystolicGood { get; set; } 
        public int SystolicRaised { get; set; } 
        public int SystolicHigh { get; set; } 
        
        public int DiastolicLowered { get; set; }
        public int DiastolicGood { get; set; }
        public int DiastolicRaised { get; set; }
        public int DiastolicHigh { get; set; }
    }
}