using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;
using Pspcl.Services.Options;
using Microsoft.Extensions.Configuration;
using Pspcl.Services.Interfaces;
using Microsoft.Extensions.Options;
using Pspcl.DBConnect;
using Microsoft.EntityFrameworkCore;

namespace Pspcl.Services
{
    public class BlobStorageService: IBlobStorageService
    {
        private readonly AzureOptions _azureOptions;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbcontext;


        public BlobStorageService(ApplicationDbContext dbContext, IOptions<AzureOptions> azureOptions, IConfiguration configuration)
        {
            _dbcontext = dbContext;
            _azureOptions = azureOptions.Value;
            _configuration = configuration;
        }
        public string UploadImageToAzure(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            using MemoryStream fileUploadStream = new MemoryStream();
            {
                file.CopyTo(fileUploadStream);
                fileUploadStream.Position = 0;
                BlobContainerClient blobContainerClient = new BlobContainerClient(
                    _azureOptions.ConnectionString,
                    _azureOptions.Container);

                var uniqueName = Guid.NewGuid().ToString() + fileExtension;

                try
                {
                    //azure package is required for below line
                    BlobClient blobClient = blobContainerClient.GetBlobClient(uniqueName);
                    blobClient.Upload(fileUploadStream, new BlobUploadOptions()
                    {
                        HttpHeaders = new BlobHttpHeaders
                        {
                            ContentType = "image/bitmap"
                        }
                    }, cancellationToken: default);

                }
                catch (RequestFailedException ex)
                {
                    // An exception occurred during the upload
                    Console.WriteLine("Upload failed. Error message: " + ex.Message);
                }

                return uniqueName;
            }

        }
        public string DownloadFileFromBlob(string fileName)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=pspclstorage;AccountKey=b4bgeAkYvbYW1W50h03HzZ5B4HWS71fmLgdCwMCTif1gfOmJ8no9eewFjL2oTONwV0kz+NkG0owT+AStlsTshQ==;EndpointSuffix=core.windows.net");
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("pspcl-images");
            CloudBlob blob = container.GetBlobReference(fileName);

            int counter = 0;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string fileExtension = Path.GetExtension(fileName);
            string localFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", fileName);

            while (File.Exists(localFilePath))
            {
                counter++;
                int copyIndex = fileNameWithoutExtension.LastIndexOf("-Copy");
                if (copyIndex != -1)
                {
                    fileNameWithoutExtension = fileNameWithoutExtension.Substring(0, copyIndex);
                }

                fileName = $"{fileNameWithoutExtension}-Copy({counter}){fileExtension}";
                localFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", fileName);
            }

            using (var fileStream = File.OpenWrite(localFilePath))
            {
                blob.DownloadToStream(fileStream);
            }

            if (File.Exists(localFilePath))
            {
                return fileName; // File downloaded successfully
            }
            else
            {
                return "DownloadFailed"; // Failed to download the file
            }
        }

    }
}
