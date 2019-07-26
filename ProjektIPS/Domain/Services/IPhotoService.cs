using Microsoft.AspNetCore.Http;
using ProjektIPS.Domain.Services.Communication;
using ProjektIPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektIPS.Domain.Services
{
    public interface IPhotoService
    {
        Task<IEnumerable<Photo>> ListAsync();
        Task<PhotoResponse> SaveAsync(Photo photo);
        Task<PhotoResponse> UploadAsync(IFormFile photoFromForm);
        Task<Photo> GetAsync(int id);
        Task<PhotoResponse> UpdateAsync(int id, Photo photo);
        Task<PhotoResponse> DeleteAsync(int id);
    }
}
