namespace HomeVital.Models.Dtos
{
    public class BloodPressureRangeDto
    {
        public int Id { get; set; }
        public int PatientID { get; set; }

        // Blood Pressure
        // 1 Systolic less than 120 and Diastolic less than 80 is good
        public double SystolicGoodMax { get; set; } = 120;
        public double DiastolicGoodMax { get; set; } = 80;
        // 2 Systolic 120 - 129 and Diastolic less than 80 is ok
        public double SystolicOkMin { get; set; } = 120;
        public double SystolicOkMax { get; set; } = 129;
        public double DiastolicOkMin { get; set; } = 0;
        public double DiastolicOkMax { get; set; } = 80;

        // 3 Systolic 130 - 139 and Diastolic 80 - 89 is not ok
        public double SystolicNotOkMin { get; set; } = 130;
        public double SystolicNotOkMax { get; set; } = 139;
        public double DiastolicNotOkMin { get; set; } = 80;
        public double DiastolicNotOkMax { get; set; } = 89;

        // 4 Systolic 140 and above and Diastolic 90 and above is critical Stage 2 Hypertension
        public double SystolicCriticalMin { get; set; } = 140;
        public double SystolicCriticalMax { get; set; } = 1000;
        public double DiastolicCriticalMin { get; set; } = 90;
        public double DiastolicCriticalMax { get; set; } = 1000;

        // 5 Systolic 180 and above and Diastolic 120 and above is critical Stage 3 Hypertensive Crisis
        public double SystolicCriticalStage3Min { get; set; } = 180;
        public double SystolicCriticalStage3Max { get; set; } = 1000;
        public double DiastolicCriticalStage3Min { get; set; } = 120;
        public double DiastolicCriticalStage3Max { get; set; } = 1000;

    }
}