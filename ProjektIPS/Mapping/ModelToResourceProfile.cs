using AutoMapper;
using ProjektIPS.Models;
using ProjektIPS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektIPS.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Face, FaceViewModel>();
            CreateMap<Photo, PhotoSimpleViewModel>();
            CreateMap<Photo, PhotoViewModel>();
        }
    }
}
