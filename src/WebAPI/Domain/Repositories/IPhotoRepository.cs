using ProjektIPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektIPS.Domain.Repositories
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> ListAsync();
        Task AddAsync(Photo photo);
        Task<Photo> FindAsync(int id);
        void Update(Photo photo);
        void Remove(Photo photo);
    }
}
