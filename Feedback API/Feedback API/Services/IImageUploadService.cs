using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Services
{
    public interface IImageUploadService
    {
        bool IsValid(IFormFile file);
        Task<string> SaveAvatar(IFormFile file, long userId);
        Task<string> SavePlaceImage(IFormFile file, long placeId, long imageId);
        public void RemoveImage(string path);
    }
}
