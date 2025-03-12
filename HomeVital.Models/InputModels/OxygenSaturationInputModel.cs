namespace HomeVital.Models.InputModels
{
    public class OxygenSaturationInputModel
    {
        public int PatientID { get; set; }
        public int OxygenSaturationValue { get; set; } = 0;
    }
}