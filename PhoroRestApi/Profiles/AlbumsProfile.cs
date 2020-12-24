using AutoMapper;
using PhoroRestApi.DTOs;
using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.Profiles
{
    public class AlbumsProfile : Profile
    {
        public AlbumsProfile()
        {
            CreateMap<Album, AlbumReadDto>();
            CreateMap<AlbumCreateDto, Album>();
            CreateMap<AlbumUpdateDto, Album>();
            CreateMap<Album, AlbumUpdateDto>();
        }
    }
}
