using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProjektIPS.Helpers;
using ProjektIPS.Models;
using System.Linq;

namespace ProjektIPS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        private readonly FaceApiConfig _faceApiConfig;
        private readonly ApplicationDbContext _context;

        public UploadController(IHostingEnvironment env, IOptions<FaceApiConfig> faceConfig, ApplicationDbContext context)
        {
            _env = env;
            _faceApiConfig = faceConfig.Value;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(_env.ContentRootPath, folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    List<Face> faces = await FaceApiHelper.MakeRequest(_faceApiConfig, fullPath);

                    Image image = new Image
                    {
                        Path = dbPath,
                        Faces = faces,
                        Anger = 0,
                        Contempt = 0,
                        Disgust = 0,
                        Fear = 0,
                        Happiness = 0,
                        Neutral = 0,
                        Sadness = 0,
                        Surprise = 0,
                        PublicationTime = System.DateTime.Now
                    };

                    PopulateEmotions(image);

                    _context.Images.Add(image);
                    _context.SaveChanges();

                    return Ok(new { image.Id});

                }
                else
                {
                    return BadRequest("Cos nie tak");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private void PopulateEmotions(Image source)
        {
            foreach(Face face in source.Faces)
            {
                switch(face.Emotion)
                {
                    case "Anger":
                        source.Anger++;
                        break;
                    case "Contempt":
                        source.Contempt++;
                        break;
                    case "Disgust":
                        source.Disgust++;
                        break;
                    case "Fear":
                        source.Fear++;
                        break;
                    case "Happiness":
                        source.Happiness++;
                        break;
                    case "Neutral":
                        source.Neutral++;
                        break;
                    case "Sadness":
                        source.Sadness++;
                        break;
                    case "Surprise":
                        source.Surprise++;
                        break;
                }
            }
        }


    }
}
