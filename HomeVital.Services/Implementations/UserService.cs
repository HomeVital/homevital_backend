
using HomeVital.Services.Interfaces;
using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;

namespace HomeVital.Services.Implementations;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> MockLogin(RegisterInputModel registerInputModel)
    {
        return await _userRepository.MockLogin(registerInputModel);
    }

    public async Task<UserDto?> Login(RegisterInputModel registerInputModel)
    {
        return await _userRepository.Login(registerInputModel);
    }
}