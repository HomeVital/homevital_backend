namespace HomeVital.Models.InputModels
{
    public class BodyTemperatureInputModel
    {
        public int PatientID { get; set; }
        public float Temperature { get; set; } = 0;
    }
}