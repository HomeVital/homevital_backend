namespace HomeVital.Models.Dtos
{
    public class BloodPressureDto
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public string MeasureHand { get; set; } = string.Empty; // which hand was used to measure the blood pressure (left, right)
        public string BodyPosition { get; set; } = string.Empty; // body position of the patient when the blood pressure was taken (sitting, standing, lying down)
        public float Systolic { get; set; } = 0; // systolic --> SYS (top number)
        public float Diastolic { get; set; } = 0; // diastolic --> DIA (bottom number)
        public float Pulse { get; set; } = 0; // pulse --> HR (heart rate) in beats per minute (bpm)
        public DateTime Date { get; set; } = new DateTime(); // date and time the blood pressure was taken
        public string Status { get; set; } = string.Empty; // status of the blood pressure reading --> normal, high, low
    }
}