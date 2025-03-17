namespace HomeVital.Models.Entities
{
    public class OxygenSaturationRange 
    {
        public int Id { get; set; }
        public int PatientID { get; set; }
        // Oxygen Saturation
        // 1 Over 95% is good
        public double OxygenSaturationGood { get; set; } = 95;
        // 2 94% to 95% is ok
        public double OxygenSaturationOkMin { get; set; } = 94;
        public double OxygenSaturationOkMax { get; set; } = 95;
        // 3 91% to 93% is not ok
        public double OxygenSaturationNotOkMin { get; set; } = 91;
        public double OxygenSaturationNotOkMax { get; set; } = 93;
        // 4 Below 91% is critical
        public double OxygenSaturationCriticalMin { get; set; } = 0;
        public double OxygenSaturationCriticalMax { get; set; } = 90;
    }
}