using AutoMapper;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;
using Microsoft.EntityFrameworkCore;

namespace HomeVital.Repositories.Implementations;

public class PatientRepository : IPatientRepository
{
    private readonly HomeVitalDbContext _dbContext;
    private readonly IMapper _mapper;
    public PatientRepository(HomeVitalDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public Task<PatientDto> CreatePatientAsync(PatientInputModel patient)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeletePatientAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PatientDto?> GetPatientByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<PatientDto[]> GetPatientsAsync()
    {
        var patients = await _dbContext.Patients.ToListAsync();
        var patientDtos = patients.Select(patient => new PatientDto
        {
            ID = patient.ID,
            Name = patient.Name,
            Phone = patient.Phone,
            TeamID = patient.TeamID,
            Status = patient.Status
        });

        return patientDtos.ToArray();
    }

    public Task<PatientDto?> UpdatePatientAsync(int id, PatientInputModel updatedPatient)
    {
        throw new NotImplementedException();
    }
}