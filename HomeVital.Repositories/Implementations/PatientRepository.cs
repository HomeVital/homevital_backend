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
    private readonly IMapper _mapper; 
    
    public PatientRepository(HomeVitalDbContext dbContext, IMapper mapper) 
    {
        _dbContext = dbContext;
        _mapper = mapper; 
        
    }

    public async Task<PatientDto> CreatePatient(PatientInputModel patient)
    {
        var newPatient = _mapper.Map<Patient>(patient); 
        _dbContext.Patients.Add(newPatient);
        await _dbContext.SaveChangesAsync();

        // init UserID for patient
        var newUser = new User
        {
            PatientID = newPatient.ID,
            Kennitala = patient.Kennitala
        };
        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync();

        // init patients measurements vital ranges
        var newVitalRangeBodyTemp = new BodyTemperatureRange
        {
            PatientID = newPatient.ID  
        };
        var newVitalRangeBloodPressure = new BloodPressureRange
        {
            PatientID = newPatient.ID  
        };
        var newVitalRangeBloodSugar = new BloodSugarRange
        {
            PatientID = newPatient.ID  
        };
        var newVitalRangeBodyWeight = new BodyWeightRange
        {
            PatientID = newPatient.ID  
        };
        var newVitalRangeOxygenSaturation = new OxygenSaturationRange
        {
            PatientID = newPatient.ID  
        };

        _dbContext.BodyTemperatureRanges.Add(newVitalRangeBodyTemp);
        _dbContext.BloodPressureRanges.Add(newVitalRangeBloodPressure);
        _dbContext.BloodSugarRanges.Add(newVitalRangeBloodSugar);
        _dbContext.BodyWeightRanges.Add(newVitalRangeBodyWeight);
        _dbContext.OxygenSaturationRanges.Add(newVitalRangeOxygenSaturation);

        await _dbContext.SaveChangesAsync();
        var patientDto = _mapper.Map<PatientDto>(newPatient); 
        return patientDto; 
    }

    public async Task<IEnumerable<PatientDto>> GetPatients()
    {
        var patients = await _dbContext.Patients.ToListAsync();
        var patientDtos = _mapper.Map<IEnumerable<PatientDto>>(patients);  
        return patientDtos; 
    }

    public async Task<PatientDto> GetPatientById(int id)
    {
        var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.ID == id);

        

        var patientDto = _mapper.Map<PatientDto>(patient); 


        return patientDto; 
    }

    public async Task<PatientDto> DeletePatient(int id)
    {
        // delete patient
        var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.ID == id);
        if (patient != null) 
        {
            _dbContext.Patients.Remove(patient);
            await _dbContext.SaveChangesAsync();
        }
        var patientDto = _mapper.Map<PatientDto>(patient);
        return patientDto;
    }

    public async Task<PatientDto> UpdatePatient(int id, PatientInputModel patient)
    {
        var existingPatient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.ID == id);
        if (existingPatient != null) 
        {
            existingPatient.Name = patient.Name;
            existingPatient.Phone = patient.Phone;
            existingPatient.Status = patient.Status;
            existingPatient.Address = patient.Address;
            existingPatient.TeamID = patient.TeamID;
            await _dbContext.SaveChangesAsync();
        }
        var patientDto = _mapper.Map<PatientDto>(existingPatient); 
        return patientDto; 
    }

}
