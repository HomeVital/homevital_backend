using System.ComponentModel.DataAnnotations;

namespace HomeVital.Models.InputModels;

public class RegisterInputModel
{
    [Required]
    [MinLength(5)]
    public string UserName { get; set; } = null!;
}