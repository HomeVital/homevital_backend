namespace HomeVital.Models.Dtos
{
    public class MeasurementDto
    {
        // list of measurements
        public List<Measurements> Measurements { get; set; } = new List<Measurements>();

    }

    public class Measurements
    {
        public int UID { get; set; }
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int TeamID { get; set; }
        public string MeasurementType { get; set; } = string.Empty;
        public DateTime MeasurementDate { get; set; }
        public MeasurementValues MeasurementValues { get; set; } = new MeasurementValues();

    }

    public class MeasurementValues
    {
        // if type is blood pressure
        public int? Systolic { get; set; }
        public int? Diastolic { get; set; }
        public int? BPM { get; set; }
        public string? MeasuredHand { get; set; } = string.Empty;
        public string? BodyPosition { get; set; } = string.Empty;

        // if type is blood sugar then rest of the fields will be null
        public float? BloodSugar { get; set; } = 0;
        public float? Weight { get; set; } = 0;
        public float? Temperature { get; set; } = 0;
        public int? OxygenSaturation { get; set; } = 0;
       
        public string? Status { get; set; } = string.Empty;

        // ACK
        // properties for resolution tracking
        public bool IsAcknowledged { get; set; } = false;
        public int? AcknowledgedByWorkerID { get; set; }
        public string? AcknowledgedByWorkerName { get; set; } = string.Empty;
        public DateTime? AcknowledgedDate { get; set; }
        public string? ResolutionNotes { get; set; } = string.Empty;

    }
}
