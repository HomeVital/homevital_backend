using AutoMapper;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;
using Microsoft.EntityFrameworkCore;
using HomeVital.Models.Entities;

namespace HomeVital.Repositories.Implementations;

public class PatientRepository : IPatientRepository
{
    private readonly HomeVitalDbContext _dbContext;
    private readonly IMapper _mapper; // Add this line to inject the mapper
    
    public PatientRepository(HomeVitalDbContext dbContext, IMapper mapper) // Add IMapper to the constructor
    {
        _dbContext = dbContext;
        _mapper = mapper; // Add this line to inject the mapper
        
    }

    public async Task<PatientDto> CreatePatient(PatientInputModel patient)
    {
        var newPatient = new Patient
        {
            Name = patient.Name,
            Phone = patient.Phone,
            Status = patient.Status,
            Address = patient.Address,
            TeamID = patient.TeamID
        };

        _dbContext.Patients.Add(newPatient);
        await _dbContext.SaveChangesAsync();

        var patientDto = _mapper.Map<PatientDto>(newPatient); // Add this line to map the entity to a DTO
        return patientDto; // Add this line to return the created patient DTO
    }

    public async Task<IEnumerable<PatientDto>> GetPatients()
    {
        var patients = await _dbContext.Patients.ToListAsync();
        var patientDtos = _mapper.Map<IEnumerable<PatientDto>>(patients); // Add this line to map the entities to DTOs
        return patientDtos; // Add this line to return the list of patient DTOs
    }

    public async Task<PatientDto> GetPatientById(int id)
    {
        var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.ID == id);
        var patientDto = _mapper.Map<PatientDto>(patient); // Add this line to map the entity to a DTO
        return patientDto; // Add this line to return the patient DTO
    }

    public async Task<PatientDto> DeletePatient(int id)
    {
        var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.ID == id);
        if (patient != null) // Add this null check
        {
            _dbContext.Patients.Remove(patient);
            await _dbContext.SaveChangesAsync();
        }
        var patientDto = _mapper.Map<PatientDto>(patient); // Add this line to map the entity to a DTO
        return patientDto; // Add this line to return the deleted patient DTO
    }

    public async Task<PatientDto> UpdatePatient(int id, PatientInputModel patient)
    {
        var existingPatient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.ID == id);
        if (existingPatient != null) // Add this null check
        {
            existingPatient.Name = patient.Name;
            existingPatient.Phone = patient.Phone;
            existingPatient.Status = patient.Status;
            existingPatient.Address = patient.Address;
            existingPatient.TeamID = patient.TeamID;
            await _dbContext.SaveChangesAsync();
        }
        var patientDto = _mapper.Map<PatientDto>(existingPatient); // Add this line to map the entity to a DTO
        return patientDto; // Add this line to return the updated patient DTO
    }
}
