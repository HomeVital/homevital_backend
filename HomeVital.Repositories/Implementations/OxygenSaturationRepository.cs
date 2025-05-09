using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Entities;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Models.Enums;
using HomeVital.Models.Exceptions;

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
            if (oxygenSaturations == null || !oxygenSaturations.Any())
            {
                throw new ResourceNotFoundException("No oxygen saturation records found for the patient with ID: " + patientId);
            }

            return _mapper.Map<IEnumerable<OxygenSaturationDto>>(oxygenSaturations);
        }

        public async Task<OxygenSaturationDto> CreateOxygenSaturation(int patientId, OxygenSaturationInputModel oxygenSaturationInputModel)
        {
            // check oxygen saturation range

            var oxygenSaturationRange = await _dbContext.OxygenSaturationRanges
                .FirstOrDefaultAsync(b => b.PatientID == patientId);

            if (oxygenSaturationRange == null)
            {
                throw new ResourceNotFoundException("Oxygen saturation range not found for the patient with ID: " + patientId);
            }

            oxygenSaturationInputModel.Status = CheckOxygenSaturationRange(oxygenSaturationInputModel, oxygenSaturationRange);


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

                var oxygenSaturationRange = await _dbContext.OxygenSaturationRanges
                    .FirstOrDefaultAsync(b => b.PatientID == oxygenSaturation.PatientID);

                if (oxygenSaturationRange != null)
                {
                    oxygenSaturationInputModel.Status = CheckOxygenSaturationRange(oxygenSaturationInputModel, oxygenSaturationRange);
                }
                oxygenSaturation.Status = oxygenSaturationInputModel.Status;
                await _dbContext.SaveChangesAsync();
            }
            if (oxygenSaturation == null)
            {
                throw new ResourceNotFoundException("Oxygen saturation record not found with ID: " + id);
            }

            return _mapper.Map<OxygenSaturationDto>(oxygenSaturation);
        }

        public async Task<OxygenSaturationDto> DeleteOxygenSaturation(int id)
        {
            var oxygenSaturation = await _dbContext.OxygenSaturations
                .FirstOrDefaultAsync(b => b.ID == id);
            if (oxygenSaturation == null)
            {
                throw new ResourceNotFoundException("Oxygen saturation record not found with ID: " + id);
            }
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
            if (oxygenSaturation == null)
            {
                throw new ResourceNotFoundException("Oxygen saturation record not found with ID: " + id);
            }

            return _mapper.Map<OxygenSaturationDto>(oxygenSaturation);
        }


        private static string CheckOxygenSaturationRange(OxygenSaturationInputModel oxygenSaturationInputModel, OxygenSaturationRange oxygenSaturationRange)
        {

            // check range

            if (oxygenSaturationInputModel.OxygenSaturationValue > oxygenSaturationRange.OxygenSaturationGood)
            {
                return VitalStatus.Normal.ToString();
            }
            else if (oxygenSaturationInputModel.OxygenSaturationValue >= oxygenSaturationRange.OxygenSaturationRaised)
            {
                return VitalStatus.Raised.ToString();
            }
            else if (oxygenSaturationInputModel.OxygenSaturationValue > oxygenSaturationRange.OxygenSaturationHigh)
            {
                return VitalStatus.Raised.ToString();
            }
            else if (oxygenSaturationInputModel.OxygenSaturationValue <= oxygenSaturationRange.OxygenSaturationHigh)
            {
                return VitalStatus.High.ToString();
            }
            else
            {
                return VitalStatus.Invalid.ToString();
            
            }
        }
    }
}
