namespace HomeVital.Models.InputModels
{
    public class BloodPressureRangeInputModel
    {
        public int PatientID { get; set; }

        // Blood Pressure
        // 1 Systolic less than 120 and diastolic less than 80 is good
        public int? SystolicGoodMax { get; set; }
        public int? DiastolicGoodMax { get; set; }
        // 2 Systolic 120 - 129 and Diastolic less than 80 is ok
        public int? SystolicOkMin { get; set; }
        public int? SystolicOkMax { get; set; }
        public int? DiastolicOkMin { get; set; }
        public int? DiastolicOkMax { get; set; }
        // 3 Systolic 130 - 139 and Diastolic 80 - 89 is not ok
        public int? SystolicNotOkMin { get; set; }
        public int? SystolicNotOkMax { get; set; }
        public int? DiastolicNotOkMin { get; set; }
        public int? DiastolicNotOkMax { get; set; }
        // 4 Systolic 140 and above and Diastolic 90 and above is critical Stage 2 Hypertension
        public int? SystolicCriticalMin { get; set; }
        public int? SystolicCriticalMax { get; set; }
        public int? DiastolicCriticalMin { get; set; }
        public int? DiastolicCriticalMax { get; set; }
        // 5 Systolic 180 and above and Diastolic 120 and above is critical Stage 3 Hypertensive Crisis
        public int? SystolicCriticalStage3Min { get; set; }
        public int? SystolicCriticalStage3Max { get; set; }
        public int? DiastolicCriticalStage3Min { get; set; }
        public int? DiastolicCriticalStage3Max { get; set; }
    }
}