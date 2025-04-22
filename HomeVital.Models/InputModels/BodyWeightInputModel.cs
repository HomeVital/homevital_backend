namespace HomeVital.Models.InputModels
{
    public class BodyWeightInputModel
    {
        public int PatientID { get; set; }
        public float Weight { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}