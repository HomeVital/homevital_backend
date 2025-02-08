using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HomeVital.Models.Dtos;
using HomeVital.Models.Entities;


namespace HomeVital.Utilities.Mapper
{
    public class HomeVitalProfile : Profile
    {
        public HomeVitalProfile()
        {
            // Make the mapping
            CreateMap<User, UserDto>();
        }

    }
}