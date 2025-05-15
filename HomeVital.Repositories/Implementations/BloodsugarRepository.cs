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
    /// <summary>
    /// Repository for managing blood sugar records in the database.
    /// Provides methods to retrieve, create, update, and delete blood sugar data.
    /// </summary>
    public class BloodsugarRepository : IBloodsugarRepository
    {
        private readonly HomeVitalDbContext _dbContext;
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="BloodsugarRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context for accessing blood sugar data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        public BloodsugarRepository(HomeVitalDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves all blood sugar records for a specific patient, ordered by date (descending).
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <returns>A collection of <see cref="BloodsugarDto"/> objects.</returns>
        /// <exception cref="ResourceNotFoundException">Thrown if no records are found for the patient.</exception>
        public async Task<IEnumerable<BloodsugarDto>> GetBloodsugarsByPatientId(int patientId)
        {
            // order by date
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

        /// <summary>
        /// Creates a new blood sugar record for a patient.
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <param name="bloodsugarInputModel">The input model containing blood sugar data.</param>
        /// <returns>The created <see cref="BloodsugarDto"/> object.</returns>
        /// <exception cref="ResourceNotFoundException">Thrown if the blood sugar range for the patient is not found.</exception>
        public async Task<BloodsugarDto> CreateBloodsugar(int patientId, BloodsugarInputModel bloodsugarInputModel)
        {
            var vitalRangeBloodsugar = await _dbContext.BloodSugarRanges
                .FirstOrDefaultAsync(b => b.PatientID == patientId);

            if (vitalRangeBloodsugar == null)
            {
                throw new ResourceNotFoundException("Blood sugar range not found for this patient.");
            }

            // Calls the CheckBloodSugarRange method to determine the status of the blood sugar level
            bloodsugarInputModel.Status = CheckBloodSugarRange(bloodsugarInputModel, vitalRangeBloodsugar);

            var bloodsugar = _mapper.Map<Bloodsugar>(bloodsugarInputModel);
            bloodsugar.PatientID = patientId;
            bloodsugar.Date = DateTime.UtcNow;

            _dbContext.Bloodsugars.Add(bloodsugar);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BloodsugarDto>(bloodsugar);
        }

        /// <summary>
        /// Updates an existing blood sugar record.
        /// </summary>
        /// <param name="id">The ID of the blood sugar record to update.</param>
        /// <param name="bloodsugarInputModel">The input model containing updated blood sugar data.</param>
        /// <returns>The updated <see cref="BloodsugarDto"/> object.</returns>
        /// <exception cref="ResourceNotFoundException">Thrown if the blood sugar record is not found.</exception>
        public async Task<BloodsugarDto> UpdateBloodsugar(int id, BloodsugarInputModel bloodsugarInputModel)
        {
            var bloodsugar = await _dbContext.Bloodsugars
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bloodsugar == null)
            {
                throw new ResourceNotFoundException("Blood sugar record not found.");
            }
            // check the date, if the date on the measurement is older than 1 day, throw an exception
            if (bloodsugar.Date < DateTime.UtcNow.AddDays(-1))
            {
                throw new MethodNotAllowedException("Blood sugar record is older than 1 day.");
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

        /// <summary>
        /// Deletes a blood sugar record by ID.
        /// </summary>
        /// <param name="id">The ID of the blood sugar record to delete.</param>
        /// <returns>The deleted <see cref="BloodsugarDto"/> object.</returns>
        /// <exception cref="ResourceNotFoundException">Thrown if the blood sugar record is not found.</exception>
        public async Task<BloodsugarDto> DeleteBloodsugar(int id)
        {
            var bloodsugar = await _dbContext.Bloodsugars
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bloodsugar == null)
            {
                throw new ResourceNotFoundException("Blood sugar record not found.");
            }
            // check the date, if the date on the measurement is older than 1 day, throw an exception
            if (bloodsugar.Date < DateTime.UtcNow.AddDays(-1))
            {
                throw new MethodNotAllowedException("Blood sugar record is older than 1 day.");
            }

            _dbContext.Bloodsugars.Remove(bloodsugar);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BloodsugarDto>(bloodsugar);
        }

        /// <summary>
        /// Determines the status of a blood sugar level based on the patient's vital range.
        /// if the blood sugar level is below the lower limit, it is considered "Raised".
        /// if the blood sugar level is between the lower limit and the good limit, it is considered "Normal".
        /// if the blood sugar level is between the good limit and the raised limit, it is considered "Raised".
        /// if the blood sugar level is above the high limit, it is considered "High".
        /// if the blood sugar level is invalid, it is considered "Invalid".
        /// </summary>
        /// <param name="bloodsugarInputModel">The input model containing the blood sugar level.</param>
        /// <param name="vitalRangeBloodsugar">The patient's blood sugar range.</param>
        /// <returns>A string representing the blood sugar status.</returns>
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
            else if (bloodsugarInputModel.BloodsugarLevel >= vitalRangeBloodsugar.BloodSugarHigh)
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