using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Entities;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using System.Threading.Tasks;
using AutoMapper;


namespace HomeVital.Services
{
    public class BodyWeightService : IBodyWeightService
    {
    private readonly IBodyWeightRepository _bodyWeightRepository;
    private readonly IMapper _mapper;

    public BodyWeightService(IBodyWeightRepository bodyWeightRepository, IMapper mapper)
    {
        _bodyWeightRepository = bodyWeightRepository;
        _mapper = mapper;
    }
  
    public async Task<IEnumerable<BodyWeightDto>> GetBodyWeightsByPatientId(int patientId)
    {
        var bodyWeights = await _bodyWeightRepository.GetBodyWeightsByPatientId(patientId);
        return _mapper.Map<IEnumerable<BodyWeightDto>>(bodyWeights);

    }
    
    public async Task<BodyWeightDto> CreateBodyWeight(int patientId, BodyWeightInputModel bodyWeightInputModel)
    {
        return await _bodyWeightRepository.CreateBodyWeight(patientId, bodyWeightInputModel);
    }

    public async Task<BodyWeightDto> UpdateBodyWeight(int id, BodyWeightInputModel bodyWeightInputModel)
    {
        // get the body weight record by id 
        var bodyWeight = await _bodyWeightRepository.GetBodyWeightById(id);
        // if the body weight record is not found, throw an exception
        if (bodyWeight == null)
        {
            // throw new ArgumentException("Body weight record not found");
        }
        
        return await _bodyWeightRepository.UpdateBodyWeight(id, bodyWeightInputModel);
    }

    public async Task<BodyWeightDto> DeleteBodyWeight(int id)
    {
        return await _bodyWeightRepository.DeleteBodyWeight(id);
    }

    public async Task<BodyWeightDto> GetBodyWeightById(int id)
    {
        return await _bodyWeightRepository.GetBodyWeightById(id);

    }

    }
}