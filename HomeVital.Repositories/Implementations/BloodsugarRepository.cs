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

            if (bloodsugars == null || bloodsugars.Count() == 0)
            {
                throw new ResourceNotFoundException("No blood sugar records found for this patient.");
            }

            return _mapper.Map<IEnumerable<BloodsugarDto>>(bloodsugars);
        }

        public async Task<BloodsugarDto> CreateBloodsugar(int patientId, BloodsugarInputModel bloodsugarInputModel)
        {
            var vitalRangeBloodsugar = await _dbContext.BloodSugarRanges
                .FirstOrDefaultAsync(b => b.PatientID == patientId);

            if (vitalRangeBloodsugar == null)
            {
                throw new ResourceNotFoundException("Blood sugar range not found for this patient.");
            }

            // check blood sugar range
            bloodsugarInputModel.Status = CheckBloodSugarRange(bloodsugarInputModel, vitalRangeBloodsugar);

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

            if (bloodsugar == null)
            {
                throw new ResourceNotFoundException("Blood sugar record not found.");
            }

            if (bloodsugar != null)
            {
                bloodsugar.BloodsugarLevel = bloodsugarInputModel.BloodsugarLevel;
                
                var vitalRangeBloodsugar = await _dbContext.BloodSugarRanges
                    .FirstOrDefaultAsync(b => b.PatientID == bloodsugar.PatientID);

                if (vitalRangeBloodsugar != null)
                {
                    bloodsugarInputModel.Status = CheckBloodSugarRange(bloodsugarInputModel, vitalRangeBloodsugar);
                }

                bloodsugar.Status = bloodsugarInputModel.Status;
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
                throw new ResourceNotFoundException("Blood sugar record not found.");
            }

            _dbContext.Bloodsugars.Remove(bloodsugar);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BloodsugarDto>(bloodsugar);
        }

        private static string CheckBloodSugarRange(BloodsugarInputModel bloodsugarInputModel, BloodSugarRange vitalRangeBloodsugar)
        {
            // check blood sugar range 
            if (bloodsugarInputModel.BloodsugarLevel < vitalRangeBloodsugar.BloodSugarLowered)
            {
                return VitalStatus.Raised.ToString();
            }
            else if (bloodsugarInputModel.BloodsugarLevel < vitalRangeBloodsugar.BloodSugarGood)
            {
                return VitalStatus.Normal.ToString();
            }
            else if (bloodsugarInputModel.BloodsugarLevel < vitalRangeBloodsugar.BloodSugarRaised)
            {
                return VitalStatus.Raised.ToString();
            }
            else if (bloodsugarInputModel.BloodsugarLevel > vitalRangeBloodsugar.BloodSugarHigh)
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