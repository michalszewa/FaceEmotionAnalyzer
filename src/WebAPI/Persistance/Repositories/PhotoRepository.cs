using Microsoft.EntityFrameworkCore;
using ProjektIPS.Domain.Repositories;
using ProjektIPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektIPS.Persistance.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly ApplicationDbContext _context;
        public PhotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Photo photo)
        {
            await _context.Photos.AddAsync(photo);
        }

        public async Task<Photo> FindAsync(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            await _context.Entry(photo).Collection(p => p.Faces).LoadAsync();
            return photo;

        }

        public async Task<IEnumerable<Photo>> ListAsync()
        {
            return await _context.Photos.ToListAsync();
        }

        public void Remove(Photo photo)
        {
            _context.Remove(photo);
        }

        public void Update(Photo photo)
        {
            _context.Update(photo);
        }
    }
}
