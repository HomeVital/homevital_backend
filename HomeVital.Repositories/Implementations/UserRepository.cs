
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HomeVital.Repositories.dbContext;
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

    public async Task <UserDto?> MockLogin(RegisterInputModel registerInputModel)
    {
        // var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Kennitala == registerInputModel.Kennitala);
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Kennitala == registerInputModel.Kennitala);
        if (user == null)
        {
            return null;
        }
        // map the model so id is not exposed
        user.Id = 0;
        return _mapper.Map<UserDto>(user);

    }

    public async Task <UserDto?> Login(RegisterInputModel registerInputModel)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Kennitala == registerInputModel.Kennitala);
        if (user == null)
        {
            return null;
        }
        return _mapper.Map<UserDto>(user);
    }

}