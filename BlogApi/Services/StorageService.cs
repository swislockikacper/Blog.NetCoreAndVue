using BlogApi.Constants;
using BlogApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlogApi.Services
{
    public class StorageService : IStorageService
    {
        private readonly CloudStorageAccount storageAccount;
        private readonly CloudBlobClient blobClient;
        private readonly CloudBlobContainer blobContainer;

        private readonly IConfiguration configuration;

        public StorageService(IConfiguration configuration)
        {
            this.configuration = configuration;

            storageAccount = CloudStorageAccount.Parse(configuration.GetValue<string>(Storage.ConnectionStringPath));
            blobClient = storageAccount.CreateCloudBlobClient();
            blobContainer = blobClient.GetContainerReference(Storage.Container);
        }

        public async Task UploadFile(int blogId, IFormFile file)
        {
            if (file == null)
                throw new ArgumentNullException("File cannot be null");
            
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);

                await UploadToBlob(file.FileName, memoryStream.ToArray());
            }
        }

        private async Task UploadToBlob(string fileName, byte[] file)
        {
            await blobContainer.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cloudBlockBlob = blobContainer.GetBlockBlobReference(fileName);

            if (file != null)
                await cloudBlockBlob.UploadFromByteArrayAsync(file, 0, file.Length);
        }
    }
}
