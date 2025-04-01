namespace HomeVital.Models.Entities
{
    public class BloodPressureRange 
    {
        public int Id { get; set; }
        public int PatientID { get; set; }

        // Blood Pressure
        // 1 systolic under 120 and diastolic under 80 is good
        public int SystolicGoodMax { get; set; } = 120;
        public int DiastolicGoodMax { get; set; } = 80;
        // 2 Systolic 120 - 129 and Diastolic less than 80 is ok
        public int SystolicOkMin { get; set; } = 120;
        public int SystolicOkMax { get; set; } = 129;
        public int DiastolicOkMin { get; set; } = 0;
        public int DiastolicOkMax { get; set; } = 80;
        // 3 Systolic 130 - 139 and Diastolic 80 - 89 is not ok
        public int SystolicNotOkMin { get; set; } = 130;
        public int SystolicNotOkMax { get; set; } = 139;
        public int DiastolicNotOkMin { get; set; } = 80;
        public int DiastolicNotOkMax { get; set; } = 89;

        // 4 Systolic 140 and above and Diastolic 90 and above is critical Stage 2 Hypertension
        public int SystolicCriticalMin { get; set; } = 140;
        public int SystolicCriticalMax { get; set; } = 140;
        public int DiastolicCriticalMin { get; set; } = 90;
        public int DiastolicCriticalMax { get; set; } = 90;

        // 5 Systolic 180 and above and Diastolic 120 and above is critical Stage 3 Hypertensive Crisis
        public int SystolicCriticalStage3Min { get; set; } = 180;
        public int SystolicCriticalStage3Max { get; set; } = 180;
        public int DiastolicCriticalStage3Min { get; set; } = 120;
        public int DiastolicCriticalStage3Max { get; set; } = 120;

    }
}