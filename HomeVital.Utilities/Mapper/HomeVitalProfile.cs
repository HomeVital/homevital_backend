using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;


namespace HomeVital.Utilities.Mapper
{
    public class HomeVitalProfile : Profile
    {
        private readonly IMapper _mapper;

        public HomeVitalProfile()
        {
            // Make the mapping
        }

    }
}