using Microsoft.AspNetCore.Http;

namespace TransIT.DAL.FileStorage
{
    public interface IFileStorage
    {
        string Create(IFormFile file);
        byte[] Download(string FilePath);
        void Delete(string FilePath);
    }
}
