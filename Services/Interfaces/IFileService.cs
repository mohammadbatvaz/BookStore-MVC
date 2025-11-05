using Microsoft.AspNetCore.Http;

namespace Services.Interfaces
{
    public interface IFileService
    {
        string Upload(IFormFile file, string folder);
        void Delete(string fileName);
    }
}
