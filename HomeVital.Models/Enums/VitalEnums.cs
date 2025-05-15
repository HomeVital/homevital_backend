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
        Invalid // 3 // an invalid range of vital signs
    }

    


}