using AutoMapper;
using PhoroRestApi.DTOs;
using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.Profiles
{
    public class AdminsProfile : Profile
    {
        public AdminsProfile()
        {
            CreateMap<Admin, AdminReadDto>();
            CreateMap<AdminCreateDto, Admin>();
        }
    }
}
