

using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Services.Interfaces;

public interface IUserService
{
    Task<UserDto?> MockLogin(RegisterInputModel registerInputModel);
}