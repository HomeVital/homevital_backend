using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces;

public interface IUserRepository
{
    Task<UserDto?> MockLogin(RegisterInputModel registerInputModel);
    // Login
    Task<UserDto?> Login(RegisterInputModel registerInputModel);
}