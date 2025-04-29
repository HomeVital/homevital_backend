
using AutoMapper;
using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.Dtos;
using HomeVital.Models.Entities;

namespace HomeVital.Services.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly IHealthcareWorkerRepository _healthcareWorkerRepository;
        private readonly IPatientRepository _patientRepository;

        public TeamService(ITeamRepository teamRepository, IMapper mapper, IHealthcareWorkerRepository healthcareWorkerRepository, IPatientRepository patientRepository)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
            _healthcareWorkerRepository = healthcareWorkerRepository;
            _patientRepository = patientRepository;
        }

        // public async Task<TeamDto> CreateTeamAsync(TeamInputModel teamInputModel)
        // {
        //     // variable or list to store the workers and patients ids
        //     var workerIDs = new List<int>();
        //     var patientIDs = new List<int>();

        //     workerIDs = teamInputModel.WorkerIDs;
        //     patientIDs = teamInputModel.PatientIDs;

        //     var team = _mapper.Map<Team>(teamInputModel);
        //     var createdTeam = await _teamRepository.CreateTeamAsync(team);


        //     // if the team is created successfully, update the workers and patients team id
        //     // get the team id from the created team
        //     var teamID = createdTeam.ID;


        //     // check if the workers and patients exist
        //     foreach (var workerID in workerIDs)
        //     {
        //         var worker = await _healthcareWorkerRepository.GetHealthcareWorkerById(workerID);
        //         if (worker == null)
        //         {
        //             throw new KeyNotFoundException("Worker not found");
        //         }
        //         // update the worker's team id
        //         var healthcareWorkerInputModel = new HealthcareWorkerInputModel
        //         {
        //             // populate the input model with necessary properties
        //             // TeamIDs = teamID
        //             TeamIDs = worker.TeamIDs.Append(teamID).ToList()
        //             // TeamIDs = worker.TeamIDs
        //             // add other properties as needed
        //         };
        //         await _healthcareWorkerRepository.UpdateHealthcareWorker(workerID, healthcareWorkerInputModel);
        //     }
            

        //     foreach (var patientID in patientIDs)
        //     {
        //         var patient = await _patientRepository.GetPatientById(patientID);
        //         if (patient == null)
        //         {
        //             throw new KeyNotFoundException("Patient not found");
        //         }
        //         // if patient is found and team id is 0, update the patient's team id
        //         if (patient.TeamID == 0)
        //         {
        //             var patientInputModel = new PatientInputModel
        //             {
        //                 // populate the input model with necessary properties
        //                 TeamID = teamID
        //                 // add other properties as needed
        //             };
        //             await _patientRepository.UpdatePatient(patientID, patientInputModel);
        //         }
        //     }
            
        //     return _mapper.Map<TeamDto>(createdTeam);


        // }
        public async Task<TeamDto> CreateTeamAsync(TeamInputModel teamInputModel)
        {
            var team = _mapper.Map<Team>(teamInputModel);

            // Add HealthcareWorkers to the team
            if (teamInputModel.WorkerIDs != null && teamInputModel.WorkerIDs.Any())
            {
                foreach (var workerID in teamInputModel.WorkerIDs)
                {
                    var worker = await _healthcareWorkerRepository.GetHealthcareWorkerById(workerID);
                    if (worker != null)
                    {
                        team.HealthcareWorkers.Add(_mapper.Map<HealthcareWorker>(worker));
                    }
                }
            }

            // Add Patients to the team
            if (teamInputModel.PatientIDs != null && teamInputModel.PatientIDs.Any())
            {
                foreach (var patientID in teamInputModel.PatientIDs)
                {
                    var patient = await _patientRepository.GetPatientById(patientID);
                    if (patient != null)
                    {
                        team.Patients.Add(_mapper.Map<Patient>(patient));
                    }
                }
            }

            var createdTeam = await _teamRepository.CreateTeamAsync(team);
            return _mapper.Map<TeamDto>(createdTeam);
        }

        public async Task<TeamDto> GetTeamByIdAsync(int id)
        {
            var team = await _teamRepository.GetTeamByIdAsync(id);
            return _mapper.Map<TeamDto>(team);
        }

        // public async Task<TeamDto> UpdateTeamAsync(int id, TeamInputModel teamInputModel)
        // {
        //     var team = await _teamRepository.GetTeamByIdAsync(id);
        //     if (team == null)
        //     {
        //         throw new KeyNotFoundException("Team not found");
        //     }

        //     // Update only the fields that are provided in the input model
        //     if (!string.IsNullOrEmpty(teamInputModel.Name))
        //     {
        //         team.Name = teamInputModel.Name;
        //     }
        //     if (teamInputModel.WorkerIDs != null && teamInputModel.WorkerIDs.Any())
        //     {
        //         var newWorkerIDs = teamInputModel.WorkerIDs.Except(team.WorkerIDs).ToList();
        //         team.WorkerIDs.AddRange(newWorkerIDs);
        //     }

        //     if (teamInputModel.PatientIDs != null && teamInputModel.PatientIDs.Any())
        //     {
        //         var newPatientIDs = teamInputModel.PatientIDs.Except(team.PatientIDs).ToList();
        //         team.PatientIDs.AddRange(newPatientIDs);
        //     }

        //     var updatedTeam = await _teamRepository.UpdateTeamAsync(team);
        //     return _mapper.Map<TeamDto>(updatedTeam); 
        // }
        public async Task<TeamDto> UpdateTeamAsync(int id, TeamInputModel teamInputModel)
        {
            var team = await _teamRepository.GetTeamByIdAsync(id);
            if (team == null)
            {
                throw new KeyNotFoundException("Team not found");
            }

            // Update only the fields that are provided in the input model
            if (!string.IsNullOrEmpty(teamInputModel.Name))
            {
                team.Name = teamInputModel.Name;
            }

            // Update HealthcareWorkers
            if (teamInputModel.WorkerIDs != null && teamInputModel.WorkerIDs.Any())
            {
                var existingWorkerIDs = team.HealthcareWorkers.Select(hw => hw.ID).ToList();
                var newWorkerIDs = teamInputModel.WorkerIDs.Except(existingWorkerIDs).ToList();

                foreach (var workerID in newWorkerIDs)
                {
                    var worker = await _healthcareWorkerRepository.GetHealthcareWorkerById(workerID);
                    if (worker != null)
                    {
                        team.HealthcareWorkers.Add(_mapper.Map<HealthcareWorker>(worker));
                    }
                }
            }

            // Update Patients
            if (teamInputModel.PatientIDs != null && teamInputModel.PatientIDs.Any())
            {
                var existingPatientIDs = team.Patients.Select(p => p.ID).ToList();
                var newPatientIDs = teamInputModel.PatientIDs.Except(existingPatientIDs).ToList();

                foreach (var patientID in newPatientIDs)
                {
                    var patient = await _patientRepository.GetPatientById(patientID);
                    if (patient != null)
                    {
                        team.Patients.Add(_mapper.Map<Patient>(patient));
                    }
                }
            }

            var updatedTeam = await _teamRepository.UpdateTeamAsync(team);
            return _mapper.Map<TeamDto>(updatedTeam);
        }

        public async Task DeleteTeamAsync(int id)
        {
            await _teamRepository.DeleteTeamAsync(id);
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            var teams = await _teamRepository.GetAllTeamsAsync();
            return _mapper.Map<IEnumerable<TeamDto>>(teams);
        }

    }
}