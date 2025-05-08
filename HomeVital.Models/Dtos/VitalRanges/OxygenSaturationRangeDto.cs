namespace HomeVital.Models.Dtos
{
    public class OxygenSaturationRangeDto
    {
        public int Id { get; set; }
        public int PatientID { get; set; }

        // Oxygen Saturation
        // 1 Over 95% is good

        public double OxygenSaturationGood{ get; set; }
        public double OxygenSaturationRaised{ get; set; }
        public double OxygenSaturationHigh{ get; set; }


        // 2 94% to 95% is ok
        // public double OxygenSaturationOkMin { get; set; }
        // public double OxygenSaturationOkMax { get; set; }
        // // 3 91% to 93% is not ok
        // public double OxygenSaturationNotOkMin { get; set; }
        // public double OxygenSaturationNotOkMax { get; set; }
        // // 4 Below 91% is critical
        // public double OxygenSaturationCriticalMin { get; set; }
        // public double OxygenSaturationCriticalMax { get; set; }

    }
}