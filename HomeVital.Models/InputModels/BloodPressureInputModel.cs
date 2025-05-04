using HomeVital.Models.Enums;

namespace HomeVital.Models.InputModels
{
    public class BloodPressureInputModel
    {
        // public int PatientID { get; set; }
        // user measure hand (left, right)
        // user body position (sitting, standing, lying down)
        // public string MeasureHand { get; set; } = string.Empty;
        // public string BodyPosition { get; set; } = string.Empty;
        public MeasuredHand MeasuredHand { get; set; } // Using enum instead of string
        public BodyPosition BodyPosition { get; set; } // Using enum instead of string
        public int Systolic { get; set; } = 0;
        public int Diastolic { get; set; } = 0;
        public int Pulse { get; set; } = 0;
        // public DateTime Date { get; set; } = new DateTime();
        public string Status { get; set; } = string.Empty;
    }
}