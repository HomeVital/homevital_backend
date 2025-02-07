using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces;

public interface IUserRepository
{
    Task<UserDto> Register(RegisterInputModel inputModel);
}