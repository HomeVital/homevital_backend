// Services implementation for HealthcareWorker

using System.Threading.Tasks;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Entities;
using HomeVital.Repositories.dbContext; // Add this using directive

namespace HomeVital.Services
{
    public class HealthcareWorkerService : IHealthcareWorkerService
    {
        private readonly HomeVitalDbContext _context;

        public HealthcareWorkerService(HomeVitalDbContext context)
        {
            _context = context;
        }

        public async Task<HealthcareWorker> GetHealthcareWorkerByIdAsync(int id)
        {
            return await _context.HealthcareWorkers.FindAsync(id);
        }

        public async Task<HealthcareWorker> CreateHealthcareWorkerAsync(HealthcareWorker worker)
        {
            _context.HealthcareWorkers.Add(worker);
            await _context.SaveChangesAsync();
            return worker;
        }
    }
}
