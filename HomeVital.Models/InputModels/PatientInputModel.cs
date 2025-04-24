namespace HomeVital.Models.InputModels
{
    public class PatientInputModel
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int TeamID { get; set; } = 0;
        public string Kennitala { get; set; } = string.Empty;
        // status
    }
}