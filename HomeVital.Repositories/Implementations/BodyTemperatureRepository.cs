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
            // query the database for body temperature records for the given patient ID
            var bodyTemperatures = await _dbContext.BodyTemperatures
                .Where(b => b.PatientID == patientId)
                .OrderByDescending(b => b.Date)
                .ToListAsync();

            if (bodyTemperatures == null || bodyTemperatures.Count() == 0)
            {
                throw new ResourceNotFoundException("No body temperature records found for this patient.");
            }

            return _mapper.Map<IEnumerable<BodyTemperatureDto>>(bodyTemperatures);
        }

        public async Task<BodyTemperatureDto> CreateBodyTemperature(int patientId, BodyTemperatureInputModel bodyTemperatureInputModel)
        {
            // check body temperature range
            // query the database for body temperature records for the given patient ID
            var bodyTemperatureRange = await _dbContext.BodyTemperatureRanges
                .FirstOrDefaultAsync(b => b.PatientID == patientId);

            if (bodyTemperatureRange == null)
            {
                throw new ResourceNotFoundException("Body temperature range not found for this patient.");
            }
            // check body temperature range
            bodyTemperatureInputModel.Status = CheckBodyTemperatureRange(bodyTemperatureInputModel, bodyTemperatureRange); 

            var bodyTemperature = _mapper.Map<BodyTemperature>(bodyTemperatureInputModel);
            bodyTemperature.PatientID = patientId;
            bodyTemperature.Date = DateTime.UtcNow;
            // add the body temperature to the database
            _dbContext.BodyTemperatures.Add(bodyTemperature);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BodyTemperatureDto>(bodyTemperature);
        } 

        public async Task<BodyTemperatureDto> UpdateBodyTemperature(int id, BodyTemperatureInputModel bodyTemperatureInputModel)
        {
            // check if the body temperature record exists
            // query the database for body temperature records for the given patient ID
            var bodyTemperature = await _dbContext.BodyTemperatures
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bodyTemperature == null)
            {
                throw new ResourceNotFoundException("Body temperature record not found.");
            }
            // check the date, if the date on the measurement is older than 1 day, throw an exception
            if (bodyTemperature.Date < DateTime.UtcNow.AddDays(-1))
            {
                throw new MethodNotAllowedException("Body temperature record is older than 1 day.");
            }

            if (bodyTemperature != null)
            {
                bodyTemperature.Temperature = bodyTemperatureInputModel.Temperature;

                var bodyTemperatureRange = await _dbContext.BodyTemperatureRanges
                    .FirstOrDefaultAsync(b => b.PatientID == bodyTemperature.PatientID);

                if (bodyTemperatureRange != null)
                {
                    // check body temperature range using CheckBodyTemperatureRange
                    bodyTemperatureInputModel.Status = CheckBodyTemperatureRange(bodyTemperatureInputModel, bodyTemperatureRange);
                }

                bodyTemperature.Status = bodyTemperatureInputModel.Status;
                await _dbContext.SaveChangesAsync();
            }

            return _mapper.Map<BodyTemperatureDto>(bodyTemperature);
        }

        public async Task<BodyTemperatureDto> DeleteBodyTemperature(int id)
        {
            // check if the body temperature record exists
            // query the database for body temperature records for the given patient ID
            var bodyTemperature = await _dbContext.BodyTemperatures
                .FirstOrDefaultAsync(b => b.ID == id);
            if (bodyTemperature == null)
            {
                throw new ResourceNotFoundException("Body temperature record not found.");
            }

            // check the date, if the date on the measurement is older than 1 day, throw an exception
            if (bodyTemperature.Date < DateTime.UtcNow.AddDays(-1))
            {
                throw new MethodNotAllowedException("Body temperature record is older than 1 day.");
            }

            if (bodyTemperature != null)
            {
                // delete the body temperature record from the database
                _dbContext.BodyTemperatures.Remove(bodyTemperature);
                await _dbContext.SaveChangesAsync();
            }

            return _mapper.Map<BodyTemperatureDto>(bodyTemperature);
        }

        /// <summary>
        /// Checks the body temperature range based on the input model and predefined temperature ranges.
        /// If the body temperature is below the "TemperatureUnderAverage", the status is "Raised".
        /// If the body temperature is below the "TemperatureGood", the status is "Normal".
        /// If the body temperature is within the range of "TemperatureGood" to "TemperatureNotOk", the status is "Normal".
        /// If the body temperature is below the "TemperatureCritical", the status is "Raised".
        /// If the body temperature exceeds the "TemperatureCritical", the status is "High".
        /// If the body temperature is invalid, the status is "Invalid".
        /// </summary>
        /// <param name="bodyTemperatureInputModel">The input model containing the body temperature</param>
        /// <param name="bodyTemperatureRange">The predefined body temperature range</param>
        /// <returns>The status of the body temperature</returns>
        private static string CheckBodyTemperatureRange(BodyTemperatureInputModel bodyTemperatureInputModel, BodyTemperatureRange bodyTemperatureRange)
        {
            // Check if the body temperature is within the range
            if (bodyTemperatureInputModel.Temperature < bodyTemperatureRange.TemperatureUnderAverage)
            {
                return VitalStatus.Raised.ToString();
            }
            else if (bodyTemperatureInputModel.Temperature < bodyTemperatureRange.TemperatureGood)
            {
                return VitalStatus.Normal.ToString();
            }
            else if (bodyTemperatureInputModel.Temperature < bodyTemperatureRange.TemperatureNotOk)
            {
                return VitalStatus.Normal.ToString();
            }
            else if (bodyTemperatureInputModel.Temperature < bodyTemperatureRange.TemperatureCritical)
            {
                return VitalStatus.Raised.ToString();
            }
            else if (bodyTemperatureInputModel.Temperature >= bodyTemperatureRange.TemperatureCritical)
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