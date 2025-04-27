using AutoMapper;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;
using Microsoft.EntityFrameworkCore;
using HomeVital.Models.Entities;
using HomeVital.Models.Enums;


namespace HomeVital.Repositories.Implementations{



    public class BodyWeightRepository : IBodyWeightRepository
    {

        private readonly HomeVitalDbContext _dbContext;
        private readonly IMapper _mapper;

        public BodyWeightRepository(HomeVitalDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BodyWeightDto>> GetBodyWeightsByPatientId(int patientId)
        {
            var bodyWeights = await _dbContext.BodyWeights
                .Where(b => b.PatientID == patientId)
                .OrderByDescending(b => b.Date)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BodyWeightDto>>(bodyWeights);
        }

        public async Task<BodyWeightDto> CreateBodyWeight(int patientId, BodyWeightInputModel bodyWeightInputModel)
        {
            var bodyWeightRange = await _dbContext.BodyWeightRanges
                .FirstOrDefaultAsync(b => b.PatientID == patientId);

            if (bodyWeightRange == null)
            {
                throw new System.ArgumentException("BodyWeight range not found");
            }

            // check body weight range
            bodyWeightInputModel.Status = CheckBodyWeightRange(patientId, bodyWeightInputModel.Weight, bodyWeightRange);


            var bodyWeight = _mapper.Map<BodyWeight>(bodyWeightInputModel);
            bodyWeight.PatientID = patientId;
            bodyWeight.Date = DateTime.UtcNow;

            _dbContext.BodyWeights.Add(bodyWeight);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BodyWeightDto>(bodyWeight);
        }

        public async Task<BodyWeightDto> UpdateBodyWeight(int id, BodyWeightInputModel bodyWeightInputModel)
        {
            var bodyWeight = await _dbContext.BodyWeights
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bodyWeight != null)
            {
                bodyWeight.Weight = bodyWeightInputModel.Weight;
                
                var bodyWeightRange = await _dbContext.BodyWeightRanges
                    .FirstOrDefaultAsync(b => b.PatientID == bodyWeight.PatientID);

                if (bodyWeightRange != null)
                {
                    bodyWeightInputModel.Status = CheckBodyWeightRange(bodyWeight.PatientID, bodyWeightInputModel.Weight, bodyWeightRange);
                }

                bodyWeight.Status = bodyWeightInputModel.Status;

                await _dbContext.SaveChangesAsync();
            }

            return _mapper.Map<BodyWeightDto>(bodyWeight);
        }

        public async Task<BodyWeightDto> DeleteBodyWeight(int id)
        {
            var bodyWeight = await _dbContext.BodyWeights
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bodyWeight != null)
            {
                _dbContext.BodyWeights.Remove(bodyWeight);
                await _dbContext.SaveChangesAsync();
            }

            return _mapper.Map<BodyWeightDto>(bodyWeight);
        }

        public async Task<BodyWeightDto> GetBodyWeightById(int id)
        {
            var bodyWeight = await _dbContext.BodyWeights
                .FirstOrDefaultAsync(b => b.ID == id);
        
            if (bodyWeight == null)
            {
                throw new KeyNotFoundException($"BodyWeight with ID {id} not found.");
            }
        
            return _mapper.Map<BodyWeightDto>(bodyWeight);
        }

        /// <summary>
        /// Checks the body weight range based on the percentage change of the last 30 days of the patient or last 30 records of the patient.
        /// If the body weight is increased by 5% or more, the status is "Critical".
        /// If the body weight is decreased by 5% or more, the status is "Critical".
        /// If the body weight is within the range of 5% increase or decrease, the status is "Normal".
        /// </summary>
        /// <param name="id">The patient ID</param>
        /// <param name="currentWeight">The current body weight</param>
        /// <param name="bodyWeightRange">The body weight range</param>
        /// <returns>The status of the body weight</returns>
        private string CheckBodyWeightRange(int id, double currentWeight, BodyWeightRange bodyWeightRange)
        {
            // Get the last 30 body weights of the patient
            var bodyWeights = _dbContext.BodyWeights
                .Where(b => b.PatientID == id)
                .OrderByDescending(b => b.Date)
                .Take(30)
                .ToList();

            if (bodyWeights.Count == 0)
            {
                return VitalStatus.Normal.ToString();
            }

            // Calculate the average weight
            var totalWeight = bodyWeights.Sum(b => b.Weight);
            var averageWeight = totalWeight / bodyWeights.Count;

            // Calculate the percentage change
            var percentageChange = ((currentWeight - averageWeight) / averageWeight) * 100;
        

            if (percentageChange >= bodyWeightRange.WeightGainPercentageGoodMax)
            {
                return VitalStatus.Critical.ToString();
            }
            else if (percentageChange <= bodyWeightRange.WeightLossFluctuationPercentageGood)
            {
                return VitalStatus.Critical.ToString();
            }
            else
            {
                return VitalStatus.Normal.ToString();
            }
        }

    }
}