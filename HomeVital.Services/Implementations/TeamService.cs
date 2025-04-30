
// using AutoMapper;
// using HomeVital.Services.Interfaces;
// using HomeVital.Models.InputModels;
// using HomeVital.Repositories.Interfaces;
// using HomeVital.Models.Dtos;
// using HomeVital.Models.Entities;

// namespace HomeVital.Services.Implementations
// {
//     public class TeamService : ITeamService
//     {
//         private readonly ITeamRepository _teamRepository;
//         private readonly IMapper _mapper;
//         private readonly IHealthcareWorkerRepository _healthcareWorkerRepository;
//         private readonly IPatientRepository _patientRepository;

//         public TeamService(ITeamRepository teamRepository, IMapper mapper, IHealthcareWorkerRepository healthcareWorkerRepository, IPatientRepository patientRepository)
//         {
//             _teamRepository = teamRepository;
//             _mapper = mapper;
//             _healthcareWorkerRepository = healthcareWorkerRepository;
//             _patientRepository = patientRepository;
//         }

//         public async Task<TeamDto> CreateTeamAsync(TeamInputModel teamInputModel)
//         {
//             // Create a new Team entity
//             var team = new Team
//             {
//                 HealthcareWorkers = new List<HealthcareWorker>(), // Initialize the collection
//                 Patients = new List<Patient>(), // Initialize the collection
//                 Name = teamInputModel.Name ?? string.Empty
//             };


//             // Save the new team to the repository
//             var createdTeam = await _teamRepository.CreateTeamAsync(team);

//             // Map the created team to a TeamDto and return it
//             return _mapper.Map<TeamDto>(createdTeam);
            
//         }
       
     
//         public async Task<TeamDto> GetTeamByIdAsync(int id)
//         {
//             var team = await _teamRepository.GetTeamByIdAsync(id);
//             return _mapper.Map<TeamDto>(team);
//         }      

//         public async Task<TeamDto> UpdateTeamAsync(int id, TeamInputModel teamInputModel)
//         {
//             // Load the team with its related entities
//             var team = await _teamRepository.GetTeamWithRelationsAsync(id);
//             if (team == null)
//             {
//                 throw new KeyNotFoundException("Team not found");
//             }

//             // Update the team's name if provided
//             if (!string.IsNullOrEmpty(teamInputModel.Name))
//             {
//                 team.Name = teamInputModel.Name;
//             }
//             // if the input model only contains the name only, update the team name only
//             if (teamInputModel.WorkerIDs == null && teamInputModel.PatientIDs == null)
//             {
//                 var updatedTeamResult = await _teamRepository.UpdateTeamAsync(team);
//                 return _mapper.Map<TeamDto>(updatedTeamResult);
//             }

//             // Update HealthcareWorkers
//             if (teamInputModel.WorkerIDs != null)
//             {
//                 // Replace the existing workers with the new ones
//                 team.HealthcareWorkers = await _healthcareWorkerRepository.GetHealthcareWorkersByIdsAsync(teamInputModel.WorkerIDs);
//             }

//             // Update Patients
//             if (teamInputModel.PatientIDs != null)
//             {
//                 // Replace the existing patients with the new ones
//                 team.Patients = await _patientRepository.GetPatientsByIdsAsync(teamInputModel.PatientIDs);
//             }

//             // Save changes
//             var updatedTeam = await _teamRepository.UpdateTeamAsync(team);
//             return _mapper.Map<TeamDto>(updatedTeam);
//         }

//         // public async Task<TeamDto> UpdateTeamAsync(int id, TeamInputModel teamInputModel)
//         // {
//         //     // Load the team with its related entities
//         //     var team = await _teamRepository.GetTeamWithRelationsAsync(id);
//         //     if (team == null)
//         //     {
//         //         throw new KeyNotFoundException("Team not found");
//         //     }

//         //     // Update the team's name if provided
//         //     if (!string.IsNullOrEmpty(teamInputModel.Name))
//         //     {
//         //         team.Name = teamInputModel.Name;
//         //     }

//         //     // Update HealthcareWorkers
//         //     if (teamInputModel.WorkerIDs != null)
//         //     {
//         //         foreach (var workerID in teamInputModel.WorkerIDs)
//         //         {
//         //             var worker = await _healthcareWorkerRepository.GetHealthcareWorkerById(workerID);
//         //             if (worker == null)
//         //             {
//         //                 throw new KeyNotFoundException($"Worker with ID {workerID} not found");
//         //             }

//         //             // Check if the worker is already in the team
//         //             if (team.HealthcareWorkers.Any(hw => hw.ID == workerID))
//         //             {
//         //                 // Remove the worker from the team
//         //                 var workerEntity = _mapper.Map<HealthcareWorker>(worker);
//         //                 team.HealthcareWorkers.Remove(workerEntity);
//         //             }
//         //             else
//         //             {
//         //                 // Add the worker to the team
//         //                 var workerEntity = _mapper.Map<HealthcareWorker>(worker);
//         //                 team.HealthcareWorkers.Add(workerEntity);
//         //             }
//         //         }
//         //     }

//         //     // Update Patients
//         //     if (teamInputModel.PatientIDs != null)
//         //     {

//         //         foreach (var patientID in teamInputModel.PatientIDs)
//         //         {
//         //             var patient = await _patientRepository.GetPatientById(patientID);
//         //             if (patient == null)
//         //             {
//         //                 throw new KeyNotFoundException($"Patient with ID {patientID} not found");
//         //             }

//         //             // Check if the patient is already in the team
//         //             if (team.Patients.Any(p => p.ID == patientID))
//         //             {
//         //                 // Remove the patient from the team
//         //                 var patientEntity = _mapper.Map<Patient>(patient);
//         //                 team.Patients.Remove(patientEntity);
//         //             }
//         //             else
//         //             {
//         //                 // Ensure the patient is not already assigned to another team
//         //                 if (patient.TeamID != null && patient.TeamID != 0)
//         //                 {
//         //                     continue; // Skip adding the patient if they are already assigned to another team
//         //                 }

//         //                 // Add the patient to the team
//         //                 var patientEntity = _mapper.Map<Patient>(patient);
//         //                 team.Patients.Add(patientEntity);
//         //             }
//         //         }
//         //     }

//         //     // Save changes
//         //     var updatedTeam = await _teamRepository.UpdateTeamAsync(team);
//         //     return _mapper.Map<TeamDto>(updatedTeam);
//         // }
        
//         public async Task DeleteTeamAsync(int id)
//         {
//             await _teamRepository.DeleteTeamAsync(id);
//         }

//         public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
//         {
//             var teams = await _teamRepository.GetAllTeamsAsync();
//             return _mapper.Map<IEnumerable<TeamDto>>(teams);
//         }

//     }
// }

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

        public async Task<TeamDto> CreateTeamAsync(TeamInputModel teamInputModel)
        {
            // variable or list to store the workers and patients ids
            var workerIDs = new List<int>();
            var patientIDs = new List<int>();

            workerIDs = teamInputModel.WorkerIDs;
            patientIDs = teamInputModel.PatientIDs;

            var team = _mapper.Map<Team>(teamInputModel);
            var createdTeam = await _teamRepository.CreateTeamAsync(team);


            // if the team is created successfully, update the workers and patients team id
            // get the team id from the created team
            var teamID = createdTeam.ID;


            // check if the workers and patients exist
            foreach (var workerID in workerIDs)
            {
                var worker = await _healthcareWorkerRepository.GetHealthcareWorkerById(workerID);
                if (worker == null)
                {
                    throw new KeyNotFoundException("Worker not found");
                }
                // update the worker's team id
                var healthcareWorkerInputModel = new HealthcareWorkerInputModel
                {
                    // populate the input model with necessary properties
                    // TeamIDs = teamID
                    TeamIDs = worker.TeamIDs.Append(teamID).ToList()
                    // TeamIDs = worker.TeamIDs
                    // add other properties as needed
                };
                await _healthcareWorkerRepository.UpdateHealthcareWorker(workerID, healthcareWorkerInputModel);
            }
            

            foreach (var patientID in patientIDs)
            {
                var patient = await _patientRepository.GetPatientById(patientID);
                if (patient == null)
                {
                    throw new KeyNotFoundException("Patient not found");
                }
                // if patient is found and team id is 0, update the patient's team id
                if (patient.TeamID == 0)
                {
                    var patientInputModel = new PatientInputModel
                    {
                        // populate the input model with necessary properties
                        TeamID = teamID
                        // add other properties as needed
                    };
                    await _patientRepository.UpdatePatient(patientID, patientInputModel);
                }
            }
            
            return _mapper.Map<TeamDto>(createdTeam);


        }

        public async Task<TeamDto> GetTeamByIdAsync(int id)
        {
            var team = await _teamRepository.GetTeamByIdAsync(id);
            return _mapper.Map<TeamDto>(team);
        }
        public async Task<TeamDto> UpdateTeamAsync(int id, TeamInputModel teamInputModel)
        {
            var team = await _teamRepository.GetTeamWithRelationsAsync(id);
            if (team == null)
            {
                throw new KeyNotFoundException("Team not found");
            }

            // Update the team's name if provided
            if (!string.IsNullOrEmpty(teamInputModel.Name))
            {
                team.Name = teamInputModel.Name;
            }

            // Update HealthcareWorkers
            if (teamInputModel.WorkerIDs != null)
            {
                foreach (var workerID in teamInputModel.WorkerIDs)
                {
                    var worker = await _healthcareWorkerRepository.GetHealthcareWorkerById(workerID);
                    if (worker == null)
                    {
                        throw new KeyNotFoundException($"Worker with ID {workerID} not found");
                    }

                    // Check if the worker is already in the team
                    var existingWorker = team.HealthcareWorkers.FirstOrDefault(hw => hw.ID == workerID);
                    if (existingWorker != null)
                    {
                        // Remove the worker from the team
                        team.HealthcareWorkers.Remove(existingWorker);
                    }
                    else
                    {
                        // Add the worker to the team
                        team.HealthcareWorkers.Add(_mapper.Map<HealthcareWorker>(worker));
                    }
                }
            }

            // Update Patients
            if (teamInputModel.PatientIDs != null)
            {
                foreach (var patientID in teamInputModel.PatientIDs)
                {
                    var patient = await _patientRepository.GetPatientById(patientID);
                    if (patient == null)
                    {
                        throw new KeyNotFoundException($"Patient with ID {patientID} not found");
                    }

                    // Check if the patient is already in the team
                    var existingPatient = team.Patients.FirstOrDefault(p => p.ID == patientID);
                    if (existingPatient != null)
                    {
                        // Remove the patient from the team
                        team.Patients.Remove(existingPatient);
                    }
                    else
                    {
                        // Ensure the patient is not already assigned to another team
                        if (patient.TeamID != null && patient.TeamID != 0)
                        {
                            throw new InvalidOperationException($"Patient with ID {patientID} is already assigned to another team");
                        }

                        // Add the patient to the team
                        team.Patients.Add(_mapper.Map<Patient>(patient));
                    }
                }
            }

            // Save changes
            var updatedTeam = await _teamRepository.UpdateTeamAsync(team);
            return _mapper.Map<TeamDto>(updatedTeam);
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
        //         // var newWorkerIDs = teamInputModel.WorkerIDs.Except(team.WorkerIDs).ToList();
        //         // team.WorkerIDs.AddRange(newWorkerIDs);
        //         var newWorkerIDs = teamInputModel.WorkerIDs.Except(team.WorkerIDs);
        //         team.HealthcareWorkers = team.HealthcareWorkers
        //             .Concat(newWorkerIDs.Select(id => new HealthcareWorker { ID = id }))
        //             .ToList(); // Convert back to a list if necessary for EF Core
        //     }

        //     if (teamInputModel.PatientIDs != null && teamInputModel.PatientIDs.Any())
        //     {
        //         // var newPatientIDs = teamInputModel.PatientIDs.Except(team.PatientIDs).ToList();
        //         // team.PatientIDs.AddRange(newPatientIDs);
        //         var newPatientIDs = teamInputModel.PatientIDs.Except(team.PatientIDs);
        //         team.Patients = team.Patients
        //             .Concat(newPatientIDs.Select(id => new Patient { ID = id }))
        //             .ToList(); // Convert back to a list if necessary for EF Core
        //     }

        //     var updatedTeam = await _teamRepository.UpdateTeamAsync(team);
        //     return _mapper.Map<TeamDto>(updatedTeam); 
        // }

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