namespace HomeVital.Models.InputModels
{
    public class OxygenSaturationRangeInputModel
    {
        public int PatientID { get; set; }

        // Oxygen Saturation
        public double? OxygenSaturationGood { get; set; }
        public double? OxygenSaturationRaised { get; set; }
        public double? OxygenSaturationHigh { get; set; }
    }
}