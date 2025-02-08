using System.Threading.Tasks;
using HomeVital.Models.Entities;

namespace HomeVital.Services.Interfaces
{
    public interface IBloodsugarService
    {
        Task<Bloodsugar?> GetBloodsugarByIdAsync(int id);
        Task<Bloodsugar> CreateBloodsugarAsync(Bloodsugar bloodsugar);
        Task<bool> DeleteBloodsugarAsync(int id);
        Task<Bloodsugar?> UpdateBloodsugarAsync(int id, Bloodsugar updatedBloodsugar);
    }
}