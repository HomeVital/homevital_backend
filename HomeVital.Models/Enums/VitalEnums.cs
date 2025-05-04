namespace HomeVital.Models.Enums
{
    public enum VitalType
    {
        OxygenSaturation,
        BloodPressure,
        BloodSugar,
        Temperature,
        Weight,
        
    }

    public enum VitalStatus
    {
        Normal, // 0 // a normal range of vital signs
        Raised, // 1 // a raised range of vital signs
        High, // 2 // a high range of vital signs
        Critical, // 3 // a critical range of vital signs
        CriticalHigh, // 4 // a critical high range of vital signs
        Invalid // 5 // an invalid range of vital signs
    }

    


}