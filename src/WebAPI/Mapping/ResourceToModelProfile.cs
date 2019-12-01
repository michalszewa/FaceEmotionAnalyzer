using AutoMapper;
using ProjektIPS.Models;
using ProjektIPS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektIPS.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        ResourceToModelProfile()
        {
            CreateMap<PhotoViewModel, Photo>();
        }
    }
}
