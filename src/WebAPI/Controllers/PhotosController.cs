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
    public class PhotosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public PhotosController(ApplicationDbContext context, IPhotoService photoService, IMapper mapper)
        {
            _context = context;
            _photoService = photoService;
            _mapper = mapper;
        }

        // GET latest publicated photos
        // GET: api/Photo/Latest/5
        [HttpGet("Latest/{numberOfImages:int?}")]
        public async Task<IEnumerable<PhotoSimpleViewModel>> GetPhotos(int num = 3)
        {
            var photos = await _photoService.ListAsync();
            var latestPublicatedPhotos = photos.Where(x => x.Publicate == true).OrderByDescending(s => s.PublicationTime).Take(num).ToList();

            var resources = _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoSimpleViewModel>> (latestPublicatedPhotos);

            return resources;
        }

        // GET: api/Photo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoViewModel>> GetPhoto(int id)
        {
            var photo = await _photoService.GetAsync(id);

            if (photo == null)
            {
                return NotFound();
            }

            var resource = _mapper.Map<Photo, PhotoViewModel>(photo);

            return resource;
        }

        // PUT: api/Photo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoto(int id,[FromBody]PhotoViewModel source)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Model not valid");
            }

            var photo = _mapper.Map<PhotoViewModel, Photo>(source);
            var result = await _photoService.UpdateAsync(id, photo);

            if (!result.Success)
                return BadRequest(result.Message);

            var responseResource = _mapper.Map<Photo, PhotoViewModel>(result.Photo);

            return Ok(responseResource);
        }

        // POST: api/Photo
        [HttpPost]
        public async Task<ActionResult<PhotoViewModel>> Upload()
        {
            if (!ModelState.IsValid)
                return BadRequest("Model not valid");

            var file = Request.Form.Files[0];
            var result = await _photoService.UploadAsync(file);

            if (!result.Success)
                return BadRequest(result.Message);

            var resource = _mapper.Map<Photo, PhotoViewModel>(result.Photo);

            return Ok(resource);
        }
    }
}
