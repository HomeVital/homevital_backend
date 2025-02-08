
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

    public async Task<UserDto> Register(RegisterInputModel inputModel)
    {
        return await _userRepository.Register(inputModel);
    }
}