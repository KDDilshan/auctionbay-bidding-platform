
using Api.Models;

namespace Api.Services.FileService
{
    public class FileService(IWebHostEnvironment environment) : IFileService
    {
        public void DeleteFile(string fileNameWithExtension)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                throw new ArgumentNullException(nameof(fileNameWithExtension));
            }
            var webRootPath = environment.WebRootPath;
            var path = Path.Combine(webRootPath, $"uploads", fileNameWithExtension);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Invalid file path");
            }
            File.Delete(path);
        }

        public async Task<string> SaveFileAsync(UploadedFile uploadedFile)
        {
            if (uploadedFile.file == null)
            {
                throw new ArgumentNullException(nameof(uploadedFile.file));
            }

            var RootPath = uploadedFile.isPublic? environment.WebRootPath:environment.ContentRootPath;
            var path = Path.Combine(RootPath, "uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Check the allowed extenstions
            var ext = Path.GetExtension(uploadedFile.file.FileName);
            if (!uploadedFile.formats.Contains(ext))
            {
                throw new ArgumentException($"Only {string.Join(",", uploadedFile.formats)} are allowed.");
            }

            // generate a unique filename
            var fileName = $"{Guid.NewGuid().ToString()}{ext}";
            var fileNameWithPath = Path.Combine(path, fileName);
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await uploadedFile.file.CopyToAsync(stream);
            return fileName;
        }
    }
}
