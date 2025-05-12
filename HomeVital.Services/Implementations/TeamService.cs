
using AutoMapper;
using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.Dtos;
using HomeVital.Models.Entities;
using HomeVital.Models.Exceptions;

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

            // workerIDs = teamInputModel.WorkerIDs;
            // patientIDs = teamInputModel.PatientIDs;

            var team = _mapper.Map<Team>(teamInputModel);
            var createdTeam = await _teamRepository.CreateTeamAsync(team);


            // if the team is created successfully, update the workers and patients team id
            // get the team id from the created team
            var teamID = createdTeam.ID;

            // check if the team leader exists
            if (teamInputModel.TeamLeaderID != null)
            {
                var teamLeader = await _healthcareWorkerRepository.GetHealthcareWorkerById(teamInputModel.TeamLeaderID.Value);
                if (teamLeader == null)
                {
                    throw new ResourceNotFoundException("Team leader not found with ID: " + teamInputModel.TeamLeaderID.Value);
                }
                // update the team leader id
                createdTeam.TeamLeaderID = teamInputModel.TeamLeaderID;
                // add the team leader id to the list of workers
                // workerIDs.Add(teamInputModel.TeamLeaderID.Value);
                // add the team leader name to the team
                createdTeam.TeamLeaderName = teamLeader.Name;
            }
            // add the team leader to the list of workers
            if (teamInputModel.TeamLeaderID != null)
            {
                workerIDs.Add(teamInputModel.TeamLeaderID.Value);
            }
            // // add team leader name to the team
            // createdTeam.TeamLeaderName = teamLeader.Name;

            // check if the workers and patients exist
            foreach (var workerID in workerIDs)
            {
                var worker = await _healthcareWorkerRepository.GetHealthcareWorkerById(workerID);
                if (worker == null)
                {
                    throw new ResourceNotFoundException("Healthcare worker not found with ID: " + workerID);

                }

                // update the worker's team id
                var healthcareWorkerInputModel = new HealthcareWorkerInputModel
                {
                    // populate the input model with necessary properties
                    Name = worker.Name,
                    Phone = worker.Phone,
                    TeamIDs = worker.TeamIDs.Append(teamID).ToList(),
                    Status = worker.Status,
                    
                };
                await _healthcareWorkerRepository.UpdateHealthcareWorker(worker.ID, healthcareWorkerInputModel);
            }
            

            foreach (var patientID in patientIDs)
            {
                var patient = await _patientRepository.GetPatientById(patientID);
                if (patient == null)
                {
                    throw new ResourceNotFoundException("Patient not found with ID: " + patientID);
                }
                // if patient is found and team id is 0, update the patient's team id
                if (patient.TeamID == 0)
                {
                    var patientInputModel = new PatientInputModel
                    {
                        TeamID = teamID
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
                throw new ResourceNotFoundException("Team not found with ID: " + id);
            }

            // Update the team's name if provided
            if (!string.IsNullOrEmpty(teamInputModel.Name))
            {
                team.Name = teamInputModel.Name;
            }

            // Update the team leader if provided
            // Check if the team leader exists
            if (teamInputModel.TeamLeaderID != null && teamInputModel.TeamLeaderID != 0)
            {
                var teamLeader = await _healthcareWorkerRepository.GetHealthcareWorkerById(teamInputModel.TeamLeaderID.Value);
                if (teamLeader == null)
                {
                    throw new ResourceNotFoundException("Team leader not found with ID: " + teamInputModel.TeamLeaderID.Value);
                }
                team.TeamLeaderID = teamInputModel.TeamLeaderID;
                team.TeamLeaderName = teamLeader.Name;
            }
            int teamLeaderID = team.TeamLeaderID ?? 0;

            if (teamInputModel.TeamLeaderID != null)
            {
                var teamLeader = await _healthcareWorkerRepository.GetHealthcareWorkerById(teamInputModel.TeamLeaderID.Value);
                if (teamLeader == null)
                {
                    throw new ResourceNotFoundException("Team leader not found with ID: " + teamInputModel.TeamLeaderID.Value);
                }
                // update the teamids for the team leader 
                var healthcareWorkerInputModel = new HealthcareWorkerInputModel
                {
                    // populate the input model with necessary properties
                    // TeamIDs = teamID
                    TeamIDs = teamLeader.TeamIDs.Append(id).ToList()

                };
                await _healthcareWorkerRepository.UpdateHealthcareWorker(teamLeaderID, healthcareWorkerInputModel);
            }

            // Save changes
            var updatedTeam = await _teamRepository.UpdateTeamAsync(team);
            return _mapper.Map<TeamDto>(updatedTeam);
        }
      
        public async Task DeleteTeamAsync(int id)
        {
            // Check if the team exists
            var team = await _teamRepository.GetTeamByIdAsync(id);
            if (team == null)
            {
                throw new ResourceNotFoundException("Team not found with ID: " + id);
            }

            await _teamRepository.DeleteTeamAsync(id);
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            var teams = await _teamRepository.GetAllTeamsAsync();
            return _mapper.Map<IEnumerable<TeamDto>>(teams);
        }

    }
}