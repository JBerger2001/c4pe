using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Feedback_API.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Feedback_API.Services
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly long MAX_SIZE = 8000000;
        private readonly string[] EXTENSIONS = 
        {
            ".jpg",
            ".jpeg",
            ".png"
        };

        private readonly FeedbackContext _context;

        public ImageUploadService(FeedbackContext context)
        {
            _context = context;
        }

        public bool IsValid(IFormFile file)
        {
            return IsValidExtension(file) 
                && IsValidSize(file);
        }

        private bool IsValidSize(IFormFile file)
        {
            return file.Length > 0
                && file.Length <= MAX_SIZE;
        }

        private bool IsValidExtension(IFormFile file)
        {
            return EXTENSIONS.Any(ext => file.FileName.ToLower().EndsWith(ext));
        }

        // TODO: check if this is needed, implement some way to check magic bytes if necessary
        private bool IsValidFileContent(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveAvatar(IFormFile file, long userID)
        {
            var fileName = Guid.NewGuid().ToString();
            var filePath = Path.Join("images", fileName);
            var uriPath = $"images/{fileName}";

            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            var user = await _context.Users.FindAsync(userID);
            var oldAvatar = user.AvatarURI;

            user.AvatarURI = uriPath;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            if (oldAvatar != null)
            {
                RemoveImage(oldAvatar);
            }

            return uriPath;
        }

        private void RemoveImage(string path)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public Task<string> SavePlaceImage(IFormFile file, long placeId, long imageId)
        {
            throw new NotImplementedException();
        }
    }
}
