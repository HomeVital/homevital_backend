namespace HomeVital.Models.Dtos
{
    public class BloodSugarRangeDto
    {
        public int Id { get; set; }
        public int PatientID { get; set; }

        // Blood Sugar
        // 1 80 to 100 is good (Good)
        public double BloodSugarGoodMin { get; set; } = 80;
        public double BloodSugarGoodMax { get; set; } = 100;
        // 2 101 to 125 is not ok (elevated)
        public double BloodSugarNotOkMin { get; set; } = 101;
        public double BloodSugarNotOkMax { get; set; } = 125;
        // 3 over 126  is critical (high)
        public double BloodSugarCriticalMin { get; set; } = 126;
        public double BloodSugarCriticalMax { get; set; } = 1000;
        // 4 under 80 is not ok (low)
        public double BloodSugarlowMin { get; set; } = 0;
        public double BloodSugarlowMax { get; set; } = 79;

    }
}



// namespace HomeVital.Models.Dtos
// {
//     public class BloodSugarRangeDto
//     {
//         public int Id { get; set; }
//         public int PatientID { get; set; }

//         public BloodSugarRange BloodSugar { get; set; } = new BloodSugarRange();

//         public class BloodSugarRange
//         {
//             public Range Normal { get; set; } = new Range();
//             public Range Raised { get; set; } = new Range();
//             public double High { get; set; }

//             public class Range
//             {
//                 public double Min { get; set; }
//                 public double Max { get; set; }
//             }
//         }
//     }
// }