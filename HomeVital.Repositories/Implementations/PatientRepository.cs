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
}
