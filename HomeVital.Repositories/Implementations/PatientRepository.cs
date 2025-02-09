using AutoMapper;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;

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

    public Task<PatientDto[]> GetPatientsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PatientDto?> UpdatePatientAsync(int id, PatientInputModel updatedPatient)
    {
        throw new NotImplementedException();
    }
}