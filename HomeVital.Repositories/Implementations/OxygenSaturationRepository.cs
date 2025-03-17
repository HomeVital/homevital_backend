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
            var oxygenSaturations = await _dbContext.OxygenSaturations
                .Where(b => b.PatientID == patientId)
                .OrderByDescending(b => b.Date)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OxygenSaturationDto>>(oxygenSaturations);
        }

        public async Task<OxygenSaturationDto> CreateOxygenSaturation(int patientId, OxygenSaturationInputModel oxygenSaturationInputModel)
        {
            var oxygenSaturation = _mapper.Map<OxygenSaturation>(oxygenSaturationInputModel);
            oxygenSaturation.PatientID = patientId;
            oxygenSaturation.Date = DateTime.UtcNow;

            _dbContext.OxygenSaturations.Add(oxygenSaturation);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<OxygenSaturationDto>(oxygenSaturation);
        }

        public async Task<OxygenSaturationDto> UpdateOxygenSaturation(int id, OxygenSaturationInputModel oxygenSaturationInputModel)
        {
            var oxygenSaturation = await _dbContext.OxygenSaturations
                .FirstOrDefaultAsync(b => b.ID == id);

            if (oxygenSaturation != null)
            {
                oxygenSaturation.OxygenSaturationValue = oxygenSaturationInputModel.OxygenSaturationValue;
                oxygenSaturation.Date = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
            }

            return _mapper.Map<OxygenSaturationDto>(oxygenSaturation);
        }

        public async Task<OxygenSaturationDto> DeleteOxygenSaturation(int id)
        {
            var oxygenSaturation = await _dbContext.OxygenSaturations
                .FirstOrDefaultAsync(b => b.ID == id);

            if (oxygenSaturation != null)
            {
                _dbContext.OxygenSaturations.Remove(oxygenSaturation);
                await _dbContext.SaveChangesAsync();
            }

            return _mapper.Map<OxygenSaturationDto>(oxygenSaturation);
        }

        public async Task<OxygenSaturationDto> GetOxygenSaturationById(int id)
        {
            var oxygenSaturation = await _dbContext.OxygenSaturations
                .FirstOrDefaultAsync(b => b.ID == id);

            return _mapper.Map<OxygenSaturationDto>(oxygenSaturation);
        }
    }
}