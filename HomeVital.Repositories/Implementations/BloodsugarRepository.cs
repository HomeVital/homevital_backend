using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Entities;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Implementations
{
    public class BloodsugarRepository : IBloodsugarRepository
    {
        private readonly HomeVitalDbContext _dbContext;
        private readonly IMapper _mapper;

        public BloodsugarRepository(HomeVitalDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<BloodsugarDto> CreateBloodsugarAsync(BloodsugarInputModel bloodsugar)
        {
            var newBloodsugar = _mapper.Map<Bloodsugar>(bloodsugar);
            await _dbContext.Bloodsugars.AddAsync(newBloodsugar);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BloodsugarDto>(newBloodsugar);
        }

        public async Task<BloodsugarDto[]> GetBloodsugarsByPatientId(int patientId)
        {
            var bloodsugars = await _dbContext.Bloodsugars
                .Where(b => b.PatientID == patientId)
                .ToArrayAsync();

            return _mapper.Map<BloodsugarDto[]>(bloodsugars);
        }

        public async Task<BloodsugarDto?> UpdateBloodsugarAsync(int id, BloodsugarInputModel updatedBloodsugar)
        {
            var bloodsugar = await _dbContext.Bloodsugars.FirstOrDefaultAsync(b => b.ID == id);
            if (bloodsugar == null)
            {
                return null;
            }

            _mapper.Map(updatedBloodsugar, bloodsugar);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BloodsugarDto>(bloodsugar);
        }

        public async Task<BloodsugarDto?> GetBloodsugarByIdAsync(int id)
        {
            var bloodsugar = await _dbContext.Bloodsugars.FirstOrDefaultAsync(b => b.ID == id);
            return _mapper.Map<BloodsugarDto>(bloodsugar);
        }

        public async Task<bool> DeleteBloodsugarAsync(int id)
        {
            var bloodsugar = await _dbContext.Bloodsugars.FirstOrDefaultAsync(b => b.ID == id);
            if (bloodsugar == null)
            {
                return false;
            }

            _dbContext.Bloodsugars.Remove(bloodsugar);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<BloodsugarDto?> GetBloodsugarByUserIdAsync(int userId)
        {
            var bloodsugar = await _dbContext.Bloodsugars.FirstOrDefaultAsync(b => b.PatientID == userId);
            return _mapper.Map<BloodsugarDto>(bloodsugar);
        }
    }
}