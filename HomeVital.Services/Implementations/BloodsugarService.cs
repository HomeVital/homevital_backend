using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.Dtos;
using AutoMapper;

namespace HomeVital.Services
{
    public class BloodsugarService : IBloodsugarService
    {
        private readonly IMapper _mapper;
        private readonly IBloodsugarRepository _bloodsugarRepository;

        public BloodsugarService(IMapper mapper, IBloodsugarRepository bloodsugarRepository)
        {
            _mapper = mapper;
            _bloodsugarRepository = bloodsugarRepository;
        }
    }

    public interface IBloodsugarService
    {
    }
}