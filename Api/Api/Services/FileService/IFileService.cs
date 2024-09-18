using Api.Models;

namespace Api.Services.FileService
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(UploadedFile uploadedFile);
        void DeleteFile(UploadedFile uploadedFile);
    }
}
