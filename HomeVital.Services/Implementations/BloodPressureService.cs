using HomeVital.Services.Interfaces;
using HomeVital.Models.Entities;
using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using System.Threading.Tasks;
using AutoMapper;

namespace HomeVital.Services
{
    public class BloodPressureService : IBloodPressureService
    {
        private readonly IBloodPressureRepository _bloodPressureRepository;
        private readonly IMapper _mapper;

        public BloodPressureService(IBloodPressureRepository bloodPressureRepository, IMapper mapper)
        {
            _bloodPressureRepository = bloodPressureRepository;
            _mapper = mapper;
        }

    }
}