using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace TransIT.DAL.FileStorage
{
    class FileSystemStorage : IFileStorage
    {
        public string Create(IFormFile file)
        {
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "//source//" + "TransportITDocuments";
            //  var filePath = Directory.GetCurrentDirectory() + "\\wwwroot\\" + "TransportITDocuments";
            Directory.CreateDirectory(filePath);
            filePath = Path.Combine(filePath, DateTime.Now.ToString("MM/dd/yyyy/HH/mm/ss") + file.FileName);

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
