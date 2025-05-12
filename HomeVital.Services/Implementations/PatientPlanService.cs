using HomeVital.Services.Interfaces;
using HomeVital.Models.Entities;
using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using System.Threading.Tasks;
using AutoMapper;
using HomeVital.Models.Exceptions;

namespace HomeVital.Services
{
    public class PatientPlanService : IPatientPlanService
    {
        private readonly IPatientPlanRepository _patientPlanRepository;
        private readonly IMapper _mapper;
        private readonly IPatientRepository _patientRepository;

        public PatientPlanService(IPatientPlanRepository patientPlanRepository, IMapper mapper, IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
            _patientPlanRepository = patientPlanRepository;
            _mapper = mapper;
        }

        public async Task<PatientPlanDto> CreatePatientPlanAsync( int patientId, PatientPlanInputModel patientPlanInputModel)
        {
            // Create a new patient plan

            // check if the patient exists
            var patient = await _patientRepository.GetPatientById(patientId);
            if (patient == null)
            {
                throw new ResourceNotFoundException("Patient not found with ID: " + patientId);
            }
            // check if the team exists
            var team = await _patientPlanRepository.GetPatientPlanByIdAsync(patientId);
            if (team == null)
            {
                throw new ResourceNotFoundException("Team not found for patient with ID: " + patientId);
            }

            
            var patientPlan = await _patientPlanRepository.CreatePatientPlanAsync(patientPlanInputModel, patientId);

            if (patientPlan == null)
            {
                throw new HomeVitalInvalidOperationException("Failed to create patient plan.");
            }

            // if the patient plan is created successfully and the plan is active, update the patient status to active
            if (patientPlan.IsActive)
            {
                var existingPatient = await _patientRepository.GetPatientById(patientId);
                if (existingPatient != null)
                {
                    patient.Status = "Active";
                    var patientInputModel = new PatientInputModel
                    {
                        Status = existingPatient.Status
                    };
                    await _patientRepository.UpdatePatient(patientId, patientInputModel);
                }
            }

            return patientPlan;
        }
    

        public async Task<PatientPlanDto> GetPatientPlanByIdAsync(int id)
        {
            var patientPlan = await _patientPlanRepository.GetPatientPlanByIdAsync(id);
            return _mapper.Map<PatientPlanDto>(patientPlan);
        }

        public async Task<List<PatientPlanDto>> GetPatientPlansByPatientIdAsync(int patientId)
        {
            var patientPlans = await _patientPlanRepository.GetPatientPlansByPatientIdAsync(patientId);
            return _mapper.Map<List<PatientPlanDto>>(patientPlans);
        }

    }
}