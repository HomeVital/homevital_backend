using System.Threading.Tasks;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Entities;
using HomeVital.Repositories.dbContext; // Add this using directive

namespace HomeVital.Services
{
    public class UserService : IUserService
    {
    //     private readonly HomeVitalDbContext _context;

    //     public UserService(HomeVitalDbContext context)
    //     {
    //         _context = context;
    //     }

    //     public async Task<bool> UserExistsAsync(string kennitala)
    //     {
    //         return await _context.Users.AnyAsync(u => u.Kennitala == kennitala);
    //     }
    }
}