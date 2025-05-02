// service for patient

using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.Dtos;
using HomeVital.Models.Entities;
using AutoMapper;

namespace HomeVital.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;

        public PatientService(IPatientRepository patientRepository, IMapper mapper, ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
            _patientRepository = patientRepository;
        }

        public async Task<PatientDto> CreatePatient(PatientInputModel patient)
        {
            return await _patientRepository.CreatePatient(patient);
        }

        public async Task<IEnumerable<PatientDto>> GetPatients()
        {
            return await _patientRepository.GetPatients();
        }

        public async Task<PatientDto> GetPatientById(int id)
        {
            return await _patientRepository.GetPatientById(id);
        }

        public async Task<PatientDto> DeletePatient(int id)
        {
            return await _patientRepository.DeletePatient(id);
        }

        // public async Task<PatientDto> UpdatePatient(int id, PatientInputModel patient)
        // {
        //     // Check if the patient exists before updating
        //     var existingPatient = await _patientRepository.GetPatientById(id);
        //     if (existingPatient == null)
        //     {
        //         throw new KeyNotFoundException($"Patient with ID {id} not found.");
        //     }

        //     // Update the patient details
        //     if (!string.IsNullOrEmpty(patient.Name))
        //     {
        //         existingPatient.Name = patient.Name;
        //     }
        //     if (!string.IsNullOrEmpty(patient.Kennitala))
        //     {
        //         existingPatient.Kennitala = patient.Kennitala;
        //     }
        //     if (patient.TeamID != 0)
        //     {
        //         existingPatient.TeamID = patient.TeamID;
        //     }
        //     if (!string.IsNullOrEmpty(patient.Phone))
        //     {
        //         existingPatient.Phone = patient.Phone;
        //     }
        //     if (!string.IsNullOrEmpty(patient.Address))
        //     {
        //         existingPatient.Address = patient.Address;
        //     }
        //     if (!string.IsNullOrEmpty(patient.Status))
        //     {
        //         existingPatient.Status = patient.Status;
        //     }

        //     var updatedPatient = _mapper.Map<Patient>(existingPatient);
        //     return await _patientRepository.UpdatePatient(id, updatedPatient);

                // }
       public async Task<PatientDto> UpdatePatient(int id, PatientInputModel patient)
        {
            // Retrieve the existing patient from the database
            var existingPatientDto = await _patientRepository.GetPatientById(id);
            if (existingPatientDto == null)
            {
                throw new KeyNotFoundException($"Patient with ID {id} not found.");
            }

            // Create an update model with only the fields that need to be updated
            var updateModel = new PatientInputModel{};

            // Update only the fields provided in the input model
            if (!string.IsNullOrEmpty(patient.Name))
            {
                updateModel.Name = patient.Name;
            }
            if (!string.IsNullOrEmpty(patient.Phone))
            {
                updateModel.Phone = patient.Phone;
            }
            if (!string.IsNullOrEmpty(patient.Address))
            {
                updateModel.Address = patient.Address;
            }
            if (!string.IsNullOrEmpty(patient.Status))
            {
                updateModel.Status = patient.Status;
            }
            if (patient.TeamID != 0) // Only update if a valid TeamID is provided
            {
                var teamExists = await _teamRepository.GetTeamByIdAsync(patient.TeamID) != null;
                if (!teamExists)
                {
                    throw new KeyNotFoundException($"Team with ID {patient.TeamID} not found.");
                }
                updateModel.TeamID = patient.TeamID;
            }

            // Save changes to the entity using the update model
            await _patientRepository.UpdatePatient(id, updateModel);

            // The repository method should return the updated DTO
            return await _patientRepository.GetPatientById(id);
        }
    }
}