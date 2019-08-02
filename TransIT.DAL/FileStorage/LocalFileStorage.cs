using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace TransIT.DAL.FileStorage
{
    internal class LocalFileStorage : IFileStorage
    {
        public string Create(IFormFile file)
        {
            var filePath = @"..\TransIT.DAL"+@"\LocalFileStorage";
            Directory.CreateDirectory(filePath);
            filePath = Path.Combine(filePath, DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss") + file.FileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.CreateNew))
            {
                 file.CopyTo(fileStream);
            }
            return filePath;
        }

        public void Delete(string FilePath)
        {
            var fileInfo = new FileInfo(FilePath);
            fileInfo.Delete();
        }

        public byte[] Download(string FilePath)=> File.ReadAllBytes(FilePath);

    }
}
