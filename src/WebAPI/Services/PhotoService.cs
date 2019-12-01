using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProjektIPS.Domain.Repositories;
using ProjektIPS.Domain.SeedWork;
using ProjektIPS.Domain.Services;
using ProjektIPS.Domain.Services.Communication;
using ProjektIPS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProjektIPS.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _env;
        private readonly IFaceApiService _faceApiService;
        public PhotoService(IPhotoRepository photoRepository, IUnitOfWork unitOfWork, IHostingEnvironment env, IFaceApiService faceApiService)
        {
            _photoRepository = photoRepository;
            _unitOfWork = unitOfWork;
            _env = env;
            _faceApiService = faceApiService;
        }

        public async Task<PhotoResponse> DeleteAsync(int id)
        {
            var existingPhoto = await _photoRepository.FindAsync(id);

            if (existingPhoto == null)
                return new PhotoResponse("Photo not found.");

            try
            {
                _photoRepository.Remove(existingPhoto);
                await _unitOfWork.CompleteAsync();

                return new PhotoResponse(existingPhoto);
            }
            catch (Exception ex)
            {
                return new PhotoResponse($"Error occured when deleting photo: {ex.Message}");
            }
        }

        public async Task<Photo> GetAsync(int id)
        {
            return await _photoRepository.FindAsync(id);
        }

        public async Task<IEnumerable<Photo>> ListAsync()
        {
            return await _photoRepository.ListAsync();
        }

        public async Task<PhotoResponse> SaveAsync(Photo photo)
        {
            try
            {
                await _photoRepository.AddAsync(photo);
                await _unitOfWork.CompleteAsync();

                return new PhotoResponse(photo);
            }
            catch (Exception ex)
            {
                return new PhotoResponse($"Error occured when saving photo: {ex.Message}");
            }
        }

        public async Task<PhotoResponse> UploadAsync(IFormFile photoFromForm)
        {
            try
            {
                var folderName = Path.Combine("PhotoResources", "Photos");
                var pathToSave = Path.Combine(_env.ContentRootPath, folderName);

                if (photoFromForm.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(photoFromForm.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        photoFromForm.CopyTo(stream);
                    }

                    var faces = await _faceApiService.MakeRequest(fullPath);
                    

                    Photo photo = new Photo
                    {
                        Path = dbPath,
                        Faces = faces.ToList(),
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

                    PopulateEmotions(photo);

                    await _photoRepository.AddAsync(photo);
                    await _unitOfWork.CompleteAsync();

                    return new PhotoResponse(photo);
                }
                else
                {
                    return new PhotoResponse("Error occured when uploading photo. Photo not send correctly from form.");
                }
            }
            catch (Exception ex)
            {
                return new PhotoResponse($"Error occured when uploading photo: {ex.Message}");
            }
        }

        public async Task<PhotoResponse> UpdateAsync(int id, Photo photo)
        {
            var existingPhoto = await _photoRepository.FindAsync(id);

            if (existingPhoto == null)
                return new PhotoResponse("Photo not found.");

            existingPhoto.Publicate = photo.Publicate;

            try
            {
                _photoRepository.Update(existingPhoto);
                await _unitOfWork.CompleteAsync();

                return new PhotoResponse(photo);
            }
            catch (Exception ex)
            {
                return new PhotoResponse($"Error occured when updating photo: {ex.Message}");
            }
        }

        private void PopulateEmotions(Photo source)
        {
            foreach (Face face in source.Faces)
            {
                switch (face.Emotion)
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
