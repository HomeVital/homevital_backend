// Services implementation for HealthcareWorker

using System.Threading.Tasks;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Entities;
using HomeVital.Repositories.dbContext; // Add this using directive
using Microsoft.EntityFrameworkCore;
namespace HomeVital.Services
{
    public class HealthcareWorkerService : IHealthcareWorkerService
    {
        private readonly HomeVitalDbContext _context;

        public HealthcareWorkerService(HomeVitalDbContext context)
        {
            _context = context;
        }
        public async Task<HealthcareWorker[]> GetHealthcareWorkersAsync()
        {
            return await _context.HealthcareWorkers.ToArrayAsync();
        }
        public async Task<HealthcareWorker?> GetHealthcareWorkerByIdAsync(int id)
        {
            return await _context.HealthcareWorkers.FindAsync(id)!;
        }
        public async Task<HealthcareWorker> CreateHealthcareWorkerAsync(HealthcareWorker worker)
        {
            _context.HealthcareWorkers.Add(worker);
            await _context.SaveChangesAsync();
            return worker;
        }
        public async Task<bool> DeleteHealthcareWorkerAsync(int id)
        {
            var worker = await _context.HealthcareWorkers.FindAsync(id);
            if (worker == null)
            {
                return false;
            }
            _context.HealthcareWorkers.Remove(worker);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<HealthcareWorker?> UpdateHealthcareWorkerAsync(int id, HealthcareWorker updatedWorker)
        {
            var worker = await _context.HealthcareWorkers.FindAsync(id);
            if (worker == null)
            {
                return null;
            }
            worker.Name = updatedWorker.Name;
            worker.Phone = updatedWorker.Phone;
            worker.TeamID = updatedWorker.TeamID;
            worker.Status = updatedWorker.Status;
            await _context.SaveChangesAsync();
            return worker;
        }
    }
}
