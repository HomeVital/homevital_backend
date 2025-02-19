using System.ComponentModel.DataAnnotations;

namespace HomeVital.Models.InputModels;

public class RegisterInputModel
{ // 250700-2210
    [Required]
    [MinLength(10)]
    [MaxLength(10)]
    public string Kennitala { get; set; } = null!;
}