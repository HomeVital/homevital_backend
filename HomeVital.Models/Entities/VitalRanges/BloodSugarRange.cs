namespace HomeVital.Models.Entities
{
    public class BloodSugarRange 
    {
        public int Id { get; set; }
        public int PatientID { get; set; }

        // Blood Sugar in mmol/L
        public double BloodSugarLowered { get; set; } = 2.0;
        public double BloodSugarGood { get; set; } = 4.0;
        public double BloodSugarRaised { get; set; } = 6.1;
        public double BloodSugarHigh { get; set; } = 8.0;
    }
       
}