using AutoMapper;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;
using Microsoft.EntityFrameworkCore;
using HomeVital.Models.Entities;
using HomeVital.Models.Enums;
using HomeVital.Models.Exceptions;


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

            if (bodyWeights == null || bodyWeights.Count() == 0)
            {
                throw new ResourceNotFoundException("No body weight records found for this patient.");
            }

            return _mapper.Map<IEnumerable<BodyWeightDto>>(bodyWeights);
        }

        public async Task<BodyWeightDto> CreateBodyWeight(int patientId, BodyWeightInputModel bodyWeightInputModel)
        {
            var bodyWeightRange = await _dbContext.BodyWeightRanges
                .FirstOrDefaultAsync(b => b.PatientID == patientId);

            if (bodyWeightRange == null)
            {
                throw new ResourceNotFoundException("Body weight range not found for this patient.");
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

            if (bodyWeight == null)
            {
                throw new ResourceNotFoundException("Body weight record not found.");
            }
            // check the date, if the date on the measurement is older than 1 day, throw an exception
            if (bodyWeight.Date < DateTime.UtcNow.AddDays(-1))
            {
                throw new MethodNotAllowedException("Body weight record is older than 1 day with ID: " + id);
            }

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
            if (bodyWeight == null)
            {
                throw new ResourceNotFoundException("Body weight record not found with ID: " + id);
            }
            // check the date, if the date on the measurement is older than 1 day, throw an exception
            if (bodyWeight.Date < DateTime.UtcNow.AddDays(-1))
            {
                throw new MethodNotAllowedException("Body weight record is older than 1 day with ID: " + id);
            }

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
                throw new ResourceNotFoundException("Body weight record not found with ID: " + id);
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
        private string CheckBodyWeightRange(int id, float currentWeight, BodyWeightRange bodyWeightRange)
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


            var weightLossFluctStatus = "";
            var weightGainFluctStatus = "";

            // get the weight from 30 days ago or the oldest weight measurement
            var oldestBodyWeight = bodyWeights.LastOrDefault();
            if (oldestBodyWeight == null)
            {
                return VitalStatus.Invalid.ToString();
            }
            

            // calculate the percentage change from the oldest body weight to the current weight
            var percentageChangeFromOldest = ((currentWeight - oldestBodyWeight.Weight) / oldestBodyWeight.Weight) * 100;
            // check if negative or positive
            if (percentageChangeFromOldest < 0)
            {
                // if negative make positive
                percentageChangeFromOldest = Math.Abs(percentageChangeFromOldest);
                // if negative, check the weight loss fluctuation percentage
                if (percentageChangeFromOldest < bodyWeightRange.WeightLossFluctuationPercentageGood)
                {
                    weightLossFluctStatus = VitalStatus.Normal.ToString();
                }
                else if (percentageChangeFromOldest > bodyWeightRange.WeightLossFluctuationPercentageGood)
                {
                    weightLossFluctStatus = VitalStatus.High.ToString();
                }
            } 
            else if (percentageChangeFromOldest > 0)
            {
                // if positive, check the weight gain fluctuation percentage
                if (percentageChangeFromOldest < bodyWeightRange.WeightGainFluctuationPercentageGood)
                {
                    weightGainFluctStatus = VitalStatus.Normal.ToString();
                }
                else if (percentageChangeFromOldest > bodyWeightRange.WeightGainFluctuationPercentageGood)
                {
                    weightGainFluctStatus = VitalStatus.High.ToString();
                }
            }

            // get most recent body weight and check if it is within the range WeightGainPercentageGoodMax 
            var mostRecentBodyWeight = bodyWeights.FirstOrDefault();

            if (mostRecentBodyWeight == null)
            {
                return VitalStatus.Invalid.ToString();
            }
            // calculate the percentage change for the most recent body weight
            var percentageChange = ((currentWeight - mostRecentBodyWeight.Weight) / mostRecentBodyWeight.Weight) * 100;

            // check if the change between the current weight and the most recent body weight is within the range
            if (percentageChange < bodyWeightRange.WeightGainPercentageGoodMax)
            {
                // check if the weight loss fluctuation status is high
                if (weightLossFluctStatus == VitalStatus.High.ToString())
                {
                    return VitalStatus.High.ToString();
                }
                // check if the weight gain fluctuation status is high
                if (weightGainFluctStatus == VitalStatus.High.ToString())
                {
                    return VitalStatus.High.ToString();
                }
                return VitalStatus.Normal.ToString();

            } 
            else if (percentageChange > bodyWeightRange.WeightGainPercentageGoodMax)
            {
                return VitalStatus.High.ToString();
            }

            return VitalStatus.Invalid.ToString();
        }

    }
}