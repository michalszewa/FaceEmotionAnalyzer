using ProjektIPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektIPS.Domain.Services
{
    public interface IFaceApiService
    {
        Task<IEnumerable<Face>> MakeRequest(string path);
    }
}
