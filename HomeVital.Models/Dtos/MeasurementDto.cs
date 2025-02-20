namespace HomeVital.Models.Dtos
// Dto for getting all measurements for a patient
// Measurement:
// Blood pressure
// Heart rate
// Oxygen saturation
// Temperature
// Weight 
// 
{
    public class MeasurementDto
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        // list of blood pressure measurements
        public List<BloodPressureDto> BloodPressure { get; set; } = new List<BloodPressureDto>();
        // list of blood sugar measurements
        public List<BloodsugarDto> BloodSugar { get; set; } = new List<BloodsugarDto>();
        // list of body weight measurements
        public List<BodyWeightDto> BodyWeight { get; set; } = new List<BodyWeightDto>();
        // list of oxygen saturation measurements
        // public List<OxygenSaturationDto> OxygenSaturation { get; set; }
        // list of temperature measurements
        // public List<TemperatureDto> Temperature { get; set; }
        
    }
}
