using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BAL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BAL.Services.Implements
{
    public class MediaService : IMediaService
    {
        BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        string azureConnectionString;
        public MediaService(IConfiguration configuration)
        {
            azureConnectionString = configuration.GetSection("BlobConnectionString").Value;
            _blobServiceClient = new BlobServiceClient(azureConnectionString);
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(configuration.GetSection("BlobContainerName").Value);
        }

        public async Task<List<BlobContentInfo>> UploadFiles(List<IFormFile> files)
        {
            var azureResponse = new List<BlobContentInfo>();
            foreach (var file in files)
            {
                string fileName = file.FileName;
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await _blobContainerClient.UploadBlobAsync(fileName, memoryStream, default);
                    azureResponse.Add(client);
                }
            }
            return azureResponse;
        }

        public async Task<List<BlobItem>> GetUploadedBlob()
        {
            var items = new List<BlobItem>();
            var uploadedFiles = _blobContainerClient.GetBlobsAsync();
            await foreach(BlobItem file in uploadedFiles)
            {
                items.Add(file);
            }
            return items;
        }
    }
}
