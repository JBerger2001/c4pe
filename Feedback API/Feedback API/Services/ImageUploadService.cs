using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Feedback_API.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using Feedback_API.Models.Domain;

namespace Feedback_API.Services
{
    public class ImageUploadService : IImageUploadService
    {
        public static readonly string INVALID_MESSAGE = "Invalid image. Maximum Image size is 8MB, supported file types are .jpg, .jpeg, .png and .bmp";

        private readonly long MAX_SIZE = 8000000;
        private readonly string[] EXTENSIONS =
        {
            "jpg",
            "jpeg",
            "png",
            "bmp"
        };
        private readonly string[] CONTENT_TYPES =
        {
            "image/jpg",
            "image/jpeg",
            "image/png",
            "image/bmp"
        };

        private readonly FeedbackContext _context;

        public ImageUploadService(FeedbackContext context)
        {
            _context = context;
        }

        public bool IsValid(IFormFile file)
        {
            return file != null
                && IsValidSize(file)
                && IsValidExtension(file)
                && IsValidContentType(file)
                && IsValidFileContent(file);
        }

        private bool IsValidSize(IFormFile file)
        {
            return file.Length > 0
                && file.Length <= MAX_SIZE;
        }

        private bool IsValidExtension(IFormFile file)
        {
            return EXTENSIONS.Any(ext => file.FileName.ToLower().EndsWith($".{ext}"));
        }

        private bool IsValidContentType(IFormFile file)
        {
            return file.ContentType != null
                ? CONTENT_TYPES.Contains(file.ContentType?.ToLower())
                : true;
        }

        private bool IsValidFileContent(IFormFile file)
        {
            try
            {
                IImageFormat imageFormat;

                using Image image = Image.Load(file.OpenReadStream(), out imageFormat);

                var validMimeType = CONTENT_TYPES.Contains(imageFormat.DefaultMimeType);
                var validFileType = EXTENSIONS.Contains(imageFormat.Name.ToLower());

                if (!validMimeType || !validFileType)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
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

        public void RemoveImage(string path)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public async Task<string> SavePlaceImage(IFormFile file, long placeId, long imageId)
        {
            var fileName = Guid.NewGuid().ToString();
            var filePath = Path.Join("images", fileName);
            var uriPath = $"images/{fileName}";

            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            var image = await _context.PlaceImages.FindAsync(placeId, imageId);

            if (image != null)
            {
                var oldImage = image.URI;

                image.URI = uriPath;

                _context.Entry(image).State = EntityState.Modified;

                if (oldImage != null)
                {
                    RemoveImage(oldImage);
                }
            }
            else
            {
                image = new PlaceImage()
                {
                    ID = imageId,
                    PlaceID = placeId,
                    URI = uriPath
                };

                _context.PlaceImages.Add(image);
            }

            await _context.SaveChangesAsync();


            return uriPath;
        }
    }
}
