using System.Threading.Tasks;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Entities;
using HomeVital.Repositories.dbContext; // Add this using directive
using Microsoft.EntityFrameworkCore;

namespace HomeVital.Services
{
    public class BloodsugarService : IBloodsugarService
    {
        private readonly HomeVitalDbContext _context;

        public BloodsugarService(HomeVitalDbContext context)
        {
            _context = context;
        }
        public async Task<Bloodsugar?> GetBloodsugarByIdAsync(int id)
        {
            return await _context.Bloodsugars.FindAsync(id)!;
        }
        public async Task<Bloodsugar> CreateBloodsugarAsync(Bloodsugar bloodsugar)
        {
            _context.Bloodsugars.Add(bloodsugar);
            await _context.SaveChangesAsync();
            return bloodsugar;
        }
        public async Task<bool> DeleteBloodsugarAsync(int id)
        {
            var bloodsugar = await _context.Bloodsugars.FindAsync(id);
            if (bloodsugar == null)
            {
                return false;
            }
            _context.Bloodsugars.Remove(bloodsugar);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Bloodsugar?> UpdateBloodsugarAsync(int id, Bloodsugar updatedBloodsugar)
        {
            var bloodsugar = await _context.Bloodsugars.FindAsync(id);
            if (bloodsugar == null)
            {
                return null;
            }
            bloodsugar.PatientID = updatedBloodsugar.PatientID;
            bloodsugar.BloodsugarLevel = updatedBloodsugar.BloodsugarLevel;
            bloodsugar.Date = updatedBloodsugar.Date;
            await _context.SaveChangesAsync();
            return bloodsugar;
        }
    }
}