using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace TransIT.DAL.FileStorage
{
    internal class LocalFileStorage : IFileStorage
    {
        private LocalStorageOptions _localOptions;

        public LocalFileStorage(IOptions<LocalStorageOptions> options)
        {
            _localOptions = options.Value;
        }

        public string Create(IFormFile file)
        {
            Directory.CreateDirectory(_localOptions.FolderPath);
            var documentPath = Path.Combine(_localOptions.FolderPath, DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss") + file.FileName);

            using (FileStream fileStream = new FileStream(documentPath, FileMode.CreateNew))
            {
                 file.CopyTo(fileStream);
            }
            return documentPath;
        }

        public void Delete(string FilePath)
        {
            var fileInfo = new FileInfo(FilePath);
            fileInfo.Delete();
        }

        public byte[] Download(string FilePath)=> File.ReadAllBytes(FilePath);

    }
}
