namespace HomeVital.Models.Dtos
{
    public class OxygenSaturationRangeDto
    {
        public int Id { get; set; }
        public int PatientID { get; set; }

        // Oxygen Saturation
        public double OxygenSaturationGood{ get; set; }
        public double OxygenSaturationRaised{ get; set; }
        public double OxygenSaturationHigh{ get; set; }
    }
}