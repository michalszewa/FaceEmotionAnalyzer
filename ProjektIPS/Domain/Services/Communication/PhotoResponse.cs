using ProjektIPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektIPS.Domain.Services.Communication
{
    public class PhotoResponse : BaseResponse
    {
        public Photo Photo { get; set; }
        public PhotoResponse(bool success, string message, Photo photo) : base(success, message)
        {
            Photo = photo;
        }

        public PhotoResponse(Photo photo) : this (true, string.Empty, photo)
        {

        }

        public PhotoResponse(String errorMsg) : this(false, errorMsg, null)
        {

        }
    }
}
