using AutoMapper;
using PhoroRestApi.DTOs;
using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.Profiles
{
    public class SellersProfile : Profile
    {
        public SellersProfile()
        {
            CreateMap<Seller, SellerReadFullDto>();
            CreateMap<Seller, SellerReadInfoDto>();
            CreateMap<SellerCreateDto, Seller>();
            CreateMap<SellerUpdateDto, Seller>();
            CreateMap<Seller, SellerUpdateDto>();
        }
    }
}
