using GymSystemBLL.Sevice.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Sevice.Classes
{
    public class AttachmentService : IAttachmentService
    {
        private readonly long maxfilesize = 5 * 1024 * 1024;
        private readonly string[] allowedExtention = { ".jpg", ".jpeg", ".png" };
        private readonly ILogger<AttachmentService> logger;
        private readonly IWebHostEnvironment env;

        public AttachmentService(ILogger<AttachmentService>logger ,IWebHostEnvironment env)
        {
            this.logger = logger;
            this.env = env;
        }
        public bool Delete(string filename, string foldername)
        {
            if (string.IsNullOrEmpty(filename) || string.IsNullOrEmpty(foldername)) return false;
            try
            {
                var fullpath = Path.Combine(env.WebRootPath, foldername, filename);
                if (!File.Exists(fullpath)) return false;
                File.Delete(fullpath);
                return true;
            }
            catch 
            {
                logger.LogError("Failed to delete");
                return false;
            }
        }

        public (Stream stream, string contenttype)? GetFile(string filename, string foldername)
        {
            var fullPath = Path.Combine(env.WebRootPath, foldername, filename);

            if (!File.Exists(fullPath)) return null;

            var contentType = Path.GetExtension(fullPath).ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _=>"application/octet-stream"
            };

            var stream = new FileStream(fullPath , FileMode.Open , FileAccess.Read , FileShare.Read);
            return (stream, contentType);
        }

        public async Task<string?> UploadAsync(Stream filestream, string filename, string foldername, CancellationToken ct = default)
        {
            if (filestream is null || !filestream.CanRead) return null;
            if (filestream.Length == 0) return null;
            if(filestream.Length>maxfilesize)
            {
                logger.LogWarning("Reject file too large");
                return null;
            }
            var extention  = Path.GetExtension(filename);
            if (string.IsNullOrEmpty(extention) || !allowedExtention.Contains(extention.ToLower()))
            {
                logger.LogWarning("Reject wring extention");
                return null;
            }
            var UploadedFolder = Path.Combine(env.WebRootPath, foldername);
            Directory.CreateDirectory(UploadedFolder);
            var storedfilename = $"{Guid.NewGuid()}{extention}";
            var filepath = Path.Combine(UploadedFolder, storedfilename);
            try 
            {
                await using var fs = new FileStream(filepath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                await filestream.CopyToAsync(fs);
                return storedfilename;
            }
            catch(Exception ex)
            {
                logger.LogError("failed to upload file");
                return null;
            }

        }
    }
}
