using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Entities;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Implementations
{
    public class BodyTemperatureRepository : IBodyTemperatureRepository
    {
        private readonly HomeVitalDbContext _dbContext;
        private readonly IMapper _mapper;

        public BodyTemperatureRepository(HomeVitalDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
       
        public async Task<IEnumerable<BodyTemperatureDto>> GetBodyTemperaturesByPatientId(int patientId)
        {
            var bodyTemperatures = await _dbContext.BodyTemperatures
                .Where(b => b.PatientID == patientId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BodyTemperatureDto>>(bodyTemperatures);
        }

        public async Task<BodyTemperatureDto> CreateBodyTemperature(int patientId, BodyTemperatureInputModel bodyTemperatureInputModel)
        {
            var bodyTemperature = _mapper.Map<BodyTemperature>(bodyTemperatureInputModel);
            bodyTemperature.PatientID = patientId;
            bodyTemperature.Date = DateTime.UtcNow;

            _dbContext.BodyTemperatures.Add(bodyTemperature);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BodyTemperatureDto>(bodyTemperature);
        }

        public async Task<BodyTemperatureDto> UpdateBodyTemperature(int id, BodyTemperatureInputModel bodyTemperatureInputModel)
        {
            var bodyTemperature = await _dbContext.BodyTemperatures
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bodyTemperature != null)
            {
                bodyTemperature.Temperature = bodyTemperatureInputModel.Temperature;
                bodyTemperature.Date = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
            }

            return _mapper.Map<BodyTemperatureDto>(bodyTemperature);
        }

        public async Task<BodyTemperatureDto> DeleteBodyTemperature(int id)
        {
            var bodyTemperature = await _dbContext.BodyTemperatures
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bodyTemperature != null)
            {
                _dbContext.BodyTemperatures.Remove(bodyTemperature);
                await _dbContext.SaveChangesAsync();
            }

            return _mapper.Map<BodyTemperatureDto>(bodyTemperature);
        }
    }
}