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
    public class ImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;

        public ImageController(ApplicationDbContext context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }

        // GET latest publicated images
        // GET: api/Image/Latest/5
        [HttpGet("Latest/{numberOfImages:int?}")]
        public async Task<ActionResult<IEnumerable<Photo>>> GetImages(int num = 3)
        {
            var photos = await _photoService.ListAsync();
            var latestPublicatedPhotos = photos.Where(x => x.Publicate == true).OrderByDescending(s => s.PublicationTime).Take(num).ToList();
            return latestPublicatedPhotos;
        }

        // GET: api/Image/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Photo>> GetImage(int id)
        {
            var photo = await _photoService.GetAsync(id);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        // PUT: api/Image/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(int id,[FromBody]Photo resource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Model not valid");
            }

            var result = await _photoService.UpdateAsync(id, resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Photo);
        }

        // POST: api/Image
        [HttpPost]
        public async Task<ActionResult<Photo>> Upload()
        {
            if (!ModelState.IsValid)
                return BadRequest("Model not valid");

            var file = Request.Form.Files[0];
            var result = await _photoService.UploadAsync(file);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Photo);
        }
    }
}
