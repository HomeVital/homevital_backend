using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Entities;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Implementations
{
    public class OxygenSaturationRepository : IOxygenSaturationRepository
    {
        private readonly HomeVitalDbContext _dbContext;
        private readonly IMapper _mapper;

        public OxygenSaturationRepository(HomeVitalDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OxygenSaturationDto>> GetOxygenSaturationsByPatientId(int patientId)
        {
            var oxygensaturations = await _dbContext.OxygenSaturations
                .Where(b => b.PatientID == patientId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OxygenSaturationDto>>(oxygensaturations);
        }

        public async Task<OxygenSaturationDto> CreateOxygenSaturation(int patientId, OxygenSaturationInputModel oxygensaturationInputModel)
        {
            var oxygensaturation = _mapper.Map<OxygenSaturation>(oxygensaturationInputModel);
            oxygensaturation.PatientID = patientId;
            oxygensaturation.Date = DateTime.UtcNow;

            _dbContext.OxygenSaturations.Add(oxygensaturation);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<OxygenSaturationDto>(oxygensaturation);
        }

        public async Task<OxygenSaturationDto> UpdateOxygenSaturation(int id, OxygenSaturationInputModel oxygensaturationInputModel)
        {
            var oxygensaturation = await _dbContext.OxygenSaturations
                .FirstOrDefaultAsync(b => b.ID == id);

            if (oxygensaturation != null)
            {
                oxygensaturation.OxygenSaturationLevel = oxygensaturationInputModel.OxygenSaturationLevel;
                oxygensaturation.Date = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
            }

            return _mapper.Map<OxygenSaturationDto>(oxygensaturation);
        }

        public async Task<OxygenSaturationDto> DeleteOxygenSaturation(int id)
        {
            var oxygensaturation = await _dbContext.OxygenSaturations
                .FirstOrDefaultAsync(b => b.ID == id);

            if (oxygensaturation == null)
            {
                throw new System.ArgumentException("OxygenSaturation record not found");
            }

            _dbContext.OxygenSaturations.Remove(oxygensaturation);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<OxygenSaturationDto>(oxygensaturation);
        }



    }
}