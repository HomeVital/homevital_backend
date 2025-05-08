namespace HomeVital.Models.Entities
{
    public class BloodSugarRange 
    {
        public int Id { get; set; }
        public int PatientID { get; set; }

        // Blood Sugar in mmol/L
        // 1 4.0 to 6.0 is good (normal)
        public double BloodSugarLowered { get; set; } = 2.0;
        public double BloodSugarGood { get; set; } = 4.0;
        public double BloodSugarRaised { get; set; } = 6.1;
        public double BloodSugarHigh { get; set; } = 8.0;
        
        // // 1 4.0 to 6.0 is good (normal)
        // public double BloodSugarGoodMin { get; set; }
        // public double BloodSugarGoodMax { get; set; }
        // // 2 6.1 to 7.9 is not ok (raised)
        // public double BloodSugarNotOkMin { get; set; }
        // public double BloodSugarNotOkMax { get; set; }
        // // 3 8.0 to 10.0 is not ok (high)
        // public double BloodSugarCriticalMin { get; set; } 
        // public double BloodSugarCriticalMax { get; set; } 
        // // 4 2 to 3.9 is not ok (too low, high)
        // public double BloodSugarlowMin { get; set; }
        // public double BloodSugarlowMax { get; set; }
    }
}