using System;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.IO;

namespace TransIT.DAL.FileStorage
{
    public class AzureFileStorage : IFileStorage
    {
        private CloudStorageAccount storageAccount = null;
        private AzureStorageOptions _azureOptions;

        public AzureFileStorage(IOptions<AzureStorageOptions> azureOptions)
        {
            _azureOptions = azureOptions.Value;
        }

        public string Create(IFormFile file) => CreateAsync(file).Result.ToString();
        public void Delete(string FilePath) => DeleteAsync(FilePath).ConfigureAwait(false);    
        public byte[] Download(string FilePath) => DownloadAsync(FilePath).Result;

        
        private async Task<Uri> CreateAsync(IFormFile file)
        {
            if (CloudStorageAccount.TryParse(_azureOptions.ConnectionString, out storageAccount))
            {
                var client = storageAccount.CreateCloudBlobClient();
                var container = client.GetContainerReference(_azureOptions.AccountName);
                await container.CreateIfNotExistsAsync();
                if (await container.CreateIfNotExistsAsync())
                    await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                
                CloudBlockBlob blob = container.GetBlockBlobReference(DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss") + file.FileName);

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    long streamlen = memoryStream.Length;
                    memoryStream.Position = 0;
                    await blob.UploadFromStreamAsync(memoryStream);
                    return blob.Uri;
                }
            }
            return null;
        }

        public async Task DeleteAsync(string path)
        {
            if (CloudStorageAccount.TryParse(_azureOptions.ConnectionString, out storageAccount))
            {
                var client = storageAccount.CreateCloudBlobClient();
                var container = client.GetContainerReference(_azureOptions.AccountName);
                CloudBlockBlob _blockBlob = container.GetBlockBlobReference(Path.GetFileName(path));
                await _blockBlob.DeleteAsync();

            }

        }

        public async Task<byte[]> DownloadAsync(string path)
        {
            byte[] result;
            if (!CloudStorageAccount.TryParse(_azureOptions.ConnectionString, out storageAccount)) return null;
                var client = storageAccount.CreateCloudBlobClient();
                var container = client.GetContainerReference(_azureOptions.AccountName);
            CloudBlockBlob _blockBlob = container.GetBlockBlobReference(Path.GetFileName(path));
            using (var mStream = new MemoryStream())
            {
                await _blockBlob.DownloadToStreamAsync(mStream);
                result = mStream.ToArray();
            }
             return result;
        }
    }
}