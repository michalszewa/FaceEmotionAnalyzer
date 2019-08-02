using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektIPS.Domain.Services;
using ProjektIPS.Models;
using ProjektIPS.ViewModels;

namespace ProjektIPS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacesController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public FacesController(IPhotoService photoService, IMapper mapper)
        {
            _photoService = photoService;
            _mapper = mapper;
        }

        // GET api/Face/All/5   
        [HttpGet("All/{photoId}")]
        public async Task<IEnumerable<FaceViewModel>> All(int photoId)
        {
            var photos = await _photoService.GetAsync(photoId);

            var resources = _mapper.Map<IEnumerable<Face>, IEnumerable<FaceViewModel>>(photos.Faces);

            return resources;
        }
    }
}
