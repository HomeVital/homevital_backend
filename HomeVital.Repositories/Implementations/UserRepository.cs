using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

using HomeVital.Repositories.dbContext;
using HomeVital.Models.Entities;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;


namespace HomeVital.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly HomeVitalDbContext _dbContext;
    private readonly IMapper _mapper;
    public UserRepository(HomeVitalDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task <UserDto> Register(RegisterInputModel inputModel)
    {
        if (_dbContext.Users.Any(u => u.UserName == inputModel.UserName))
        {
            throw new Exception($"{inputModel.UserName} is already registered");
        }

        User? user = new User
        {
            UserName = inputModel.UserName,
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        User? storedUser = await _dbContext.Users
        .FirstOrDefaultAsync(u => u.Id == user.Id);

        var userDto = _mapper.Map<UserDto>(storedUser);

        return userDto;
    }


}