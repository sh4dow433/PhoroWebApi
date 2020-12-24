using AutoMapper;
using PhoroRestApi.BusinessLogic;
using PhoroRestApi.DTOs;
using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.Profiles
{
    public class PhotosProfile : Profile
    {
        public PhotosProfile()
        {
            CreateMap<Photo, PhotoReadDto>();
            //.ForMember(dest => dest.ImgFile,
            //            opt => opt.MapFrom(
            //                src => photoLoader.LoadPhoto(src.DiskName)));
            CreateMap<PhotoCreateDto, Photo>();
                    //.ForMember(dest => dest.DiskName,
                    //            opt => opt.MapFrom(
                    //                src => photoSaver.SavePhoto(src.ImgFile)));
            CreateMap<PhotoUpdateDto, Photo>();
            CreateMap<Photo, PhotoUpdateDto>();
        }
    }
}
