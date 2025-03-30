using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Entities;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Models.Enums;

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
            var vitalRangeBloodsugar = await _dbContext.BloodSugarRanges
                .FirstOrDefaultAsync(b => b.PatientID == patientId);

            if (vitalRangeBloodsugar == null)
            {
                throw new System.ArgumentException("Bloodsugar range not found");
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
                throw new System.ArgumentException("Bloodsugar record not found");
            }

            _dbContext.Bloodsugars.Remove(bloodsugar);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BloodsugarDto>(bloodsugar);
        }

        private static string CheckBloodSugarRange(BloodsugarInputModel bloodsugarInputModel, BloodSugarRange vitalRangeBloodsugar)
        {
            // check blood sugar range
            //  1 80 to 100 is good (Good)
            if (bloodsugarInputModel.BloodsugarLevel >= vitalRangeBloodsugar.BloodSugarGoodMin 
            && bloodsugarInputModel.BloodsugarLevel <= vitalRangeBloodsugar.BloodSugarGoodMax)
            {
                return VitalStatus.Normal.ToString();
            }
            // 2 101 to 125 is not ok (elevated)
            else if (bloodsugarInputModel.BloodsugarLevel >= vitalRangeBloodsugar.BloodSugarNotOkMin 
            && bloodsugarInputModel.BloodsugarLevel <= vitalRangeBloodsugar.BloodSugarNotOkMax)
            {
                return VitalStatus.Raised.ToString();
            }
            else if (bloodsugarInputModel.BloodsugarLevel >= vitalRangeBloodsugar.BloodSugarCriticalMin 
            && bloodsugarInputModel.BloodsugarLevel <= vitalRangeBloodsugar.BloodSugarCriticalMax)
            {
                return VitalStatus.High.ToString();
            }
            else if (bloodsugarInputModel.BloodsugarLevel >= vitalRangeBloodsugar.BloodSugarlowMin 
            && bloodsugarInputModel.BloodsugarLevel <= vitalRangeBloodsugar.BloodSugarlowMax)
            {
                return VitalStatus.Critical.ToString();
            }
            else
            {
                return VitalStatus.Invalid.ToString();
            }
        }

    }
}