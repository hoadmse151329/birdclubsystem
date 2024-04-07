using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IMediaService
    {
        Task<List<BlobContentInfo>> UploadFiles(List<IFormFile> files);
        Task<List<BlobItem>> GetUploadedBlob();
    }
}
