// service for patient

using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.Dtos;
using HomeVital.Models.Entities;
using AutoMapper;
using HomeVital.Models.Exceptions;

namespace HomeVital.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly IPatientPlanRepository _patientPlanRepository;

        public PatientService(IPatientRepository patientRepository, IMapper mapper, ITeamRepository teamRepository,  IPatientPlanRepository patientPlanRepository)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
            _patientRepository = patientRepository;
            _patientPlanRepository = patientPlanRepository;
        }

        public async Task<PatientDto?> CreatePatient(PatientInputModel patient)
        {
            // check if the teamID is 0
            if (patient.TeamID == 0)
            {
                // throw new BadRequestException("TeamID cannot be 0");
                return null;
            }
            // check if the team exists
            var teamExists = await _teamRepository.GetTeamByIdAsync(patient.TeamID) != null;
            if (!teamExists)
            {
                // throw new NotFoundException($"Team with ID {patient.TeamID} not found.");
                return null;
            }
            return await _patientRepository.CreatePatient(patient);
        }

        public async Task<IEnumerable<PatientDto?>> GetPatients()
        {
            // return await _patientRepository.GetPatients();

            // Get all patients 
            // check Status for each patient and update the status if needed
            var patients = await _patientRepository.GetPatients();
            if (patients == null || !patients.Any())
            {
                return null;
                // throw new NotFoundException("No patients found");
            }


            // check if the patient has an active patientPlan
            foreach (var patient in patients)
            {
                var hasActivePlan = await CheckIfPatientHasActivePlan(patient.ID);
                if (hasActivePlan)
                {
                    patient.Status = "Active";
                }
                else
                {
                    patient.Status = "Inactive";
                }
                // update the patient status in the database
                var patientInputModel = new PatientInputModel
                {
                    Status = patient.Status
                };
                await _patientRepository.UpdatePatient(patient.ID, patientInputModel);

            }
            // return the updated patients
            return patients;


        }

        public async Task<PatientDto?> GetPatientById(int id)
        {
            // return await _patientRepository.GetPatientById(id);

            // check if the patient exists
            var patient = await _patientRepository.GetPatientById(id);
            if (patient == null)
            {
                return null;
                // throw new NotFoundException($"Patient with ID {id} not found.");
            }
            // check if the patient has an active patientPlan
            var hasActivePlan = await CheckIfPatientHasActivePlan(patient.ID);
            if (hasActivePlan)
            {
                patient.Status = "Active";
            }
            else
            {
                patient.Status = "Inactive";
            }
            // update the patient status in the database
            var patientInputModel = new PatientInputModel
            {
                Status = patient.Status
            };
            await _patientRepository.UpdatePatient(patient.ID, patientInputModel);
            // return the updated patient
            return patient;
        }

        public async Task<PatientDto> DeletePatient(int id)
        {
            return await _patientRepository.DeletePatient(id);
        }
        
       public async Task<PatientDto?> UpdatePatient(int id, PatientInputModel patient)
        {
            // Retrieve the existing patient from the database
            var existingPatient = await _patientRepository.GetPatientById(id);
            if (existingPatient == null)
            {
                // throw new KeyNotFoundException($"Patient with ID {id} not found.");
                return null;
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
                    // throw new KeyNotFoundException($"Team with ID {patient.TeamID} not found.");
                    return null;
                }
                updateModel.TeamID = patient.TeamID;
            }

            // Save changes to the entity using the update model
            await _patientRepository.UpdatePatient(id, updateModel);

            // The repository method should return the updated DTO
            return await _patientRepository.GetPatientById(id);
        }
        
        // private function to check if the patient has an active patientPlan
        private async Task<bool> CheckIfPatientHasActivePlan(int patientId)
        {
            var patientPlans = await _patientPlanRepository.GetPatientPlansByPatientIdAsync(patientId);
           
            if (patientPlans == null || !patientPlans.Any())
            {
                return false;
            }
            // Check if any of the plans are active
            foreach (var plan in patientPlans)
            {
                if (plan.IsActive)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
