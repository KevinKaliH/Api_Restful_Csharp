using AutoMapper;
using SocialMediaCore.DTOs;
using SocialMediaCore.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaInfraestructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();


            CreateMap<Security, SecurityDTO>().ReverseMap();
        }
    }
}
