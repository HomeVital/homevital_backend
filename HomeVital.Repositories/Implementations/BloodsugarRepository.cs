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

        public async Task<IEnumerable<BloodsugarDto>> GetBloodsugarsByPatientId(int patientId)
        {
            //order by date
            var bloodsugars = await _dbContext.Bloodsugars
                .Where(b => b.PatientID == patientId)
                .OrderByDescending(b => b.Date)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BloodsugarDto>>(bloodsugars);
        }

        public async Task<BloodsugarDto> CreateBloodsugar(int patientId, BloodsugarInputModel bloodsugarInputModel)
        {
            var bloodsugar = _mapper.Map<Bloodsugar>(bloodsugarInputModel);
            bloodsugar.PatientID = patientId;
            bloodsugar.Date = DateTime.UtcNow;

            _dbContext.Bloodsugars.Add(bloodsugar);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BloodsugarDto>(bloodsugar);
        }

        public async Task<BloodsugarDto> UpdateBloodsugar(int id, BloodsugarInputModel bloodsugarInputModel)
        {
            var bloodsugar = await _dbContext.Bloodsugars
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bloodsugar != null)
            {
                bloodsugar.BloodsugarLevel = bloodsugarInputModel.BloodsugarLevel;
                bloodsugar.Date = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
            }

            return _mapper.Map<BloodsugarDto>(bloodsugar);
        }

        public async Task<BloodsugarDto> DeleteBloodsugar(int id)
        {
            var bloodsugar = await _dbContext.Bloodsugars
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bloodsugar == null)
            {
                throw new System.ArgumentException("Bloodsugar record not found");
            }

            _dbContext.Bloodsugars.Remove(bloodsugar);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BloodsugarDto>(bloodsugar);
        }



    }
}