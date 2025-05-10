using HomeVital.Models.Enums;

namespace HomeVital.Models.InputModels
{
    public class BloodPressureInputModel
    {
        // user measure hand (left, right)
        // user body position (sitting, lying down)
        public MeasuredHand MeasuredHand { get; set; } // Using enum instead of string
        public BodyPosition BodyPosition { get; set; } // Using enum instead of string
        public int Systolic { get; set; } = 0;
        public int Diastolic { get; set; } = 0;
        public int Pulse { get; set; } = 0;
        public string Status { get; set; } = string.Empty;
    }
}