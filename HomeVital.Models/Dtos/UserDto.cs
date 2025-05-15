namespace HomeVital.Models.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Kennitala { get; set; } = string.Empty;
    public int? PatientID { get; set; }
    public int? HealthcareWorkerID { get; set; }
    public string[]? Roles { get; set; }
}