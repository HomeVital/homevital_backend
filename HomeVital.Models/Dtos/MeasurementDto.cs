using System.Security.Cryptography.X509Certificates;

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
        // list of measurements
        public List<Measurements> Measurements { get; set; } = new List<Measurements>();
        
    }

    public class Measurements
    {
        public int ID { get; set; }
        public int MeasurementID { get; set; }
        public string MeasurementType { get; set; } = string.Empty;
        public List<MeasurementValues> MeasurementValues { get; set; } = new List<MeasurementValues>();
        public DateTime MeasurementDate { get; set; }
    }

    public class MeasurementValues
    {
        // if type is blood pressure
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public int BPM { get; set; }
        public string MeasureHand { get; set; } = string.Empty;
        public string BodyPosition { get; set; } = string.Empty;

        // if type is blood sugar then rest of the fields will be null
        
        public float BloodSugar { get; set; } = 0;
        // if type is body weight
        public float Weight { get; set; } = 0;
        // if type is body temperature
        public float Temperature { get; set; } = 0;
        // if type is oxygen saturation
        public int OxygenSaturation { get; set; } = 0;

    }
}
