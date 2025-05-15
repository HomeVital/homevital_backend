using System.ComponentModel.DataAnnotations;

namespace HomeVital.Models.InputModels;

public class RegisterInputModel
{
    [Required]
    [MinLength(10)]
    [MaxLength(10)]
    public string Kennitala { get; set; } = null!;
}