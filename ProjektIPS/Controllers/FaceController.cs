using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektIPS.Domain.Services;
using ProjektIPS.Models;

namespace ProjektIPS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaceController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public FaceController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        // GET api/Face/All/5   
        [HttpGet("All/{photoId}")]
        public async Task<ActionResult<IEnumerable<Face>>> All(int photoId)
        {
            var photo = await _photoService.GetAsync(photoId);
            var faces = photo.Faces.Where(q => q.PhotoId == photoId).ToList();

            return faces;
        }
    }
}
