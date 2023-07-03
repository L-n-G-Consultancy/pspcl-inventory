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
using System;
using System.IO;

namespace Pspcl.Services
{
    public class BlobStorageService: IBlobStorageService
    {
        private readonly AzureOptions _azureOptions;
     
        public BlobStorageService(IOptions<AzureOptions> azureOptions)
        {
            _azureOptions = azureOptions.Value;
        }
        public string UploadImageToAzure(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            if(fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".jfif")
            {
                string contentType = GetContentType(fileExtension);

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
                                ContentType = contentType
                            }
                        }, cancellationToken: default);
                        return uniqueName;
                    }
                    catch (RequestFailedException ex)
                    {
                        // An exception occurred during the upload
                        Console.WriteLine("Upload failed. Error message: " + ex.Message);
                        return "failure";
                    }
                }        
                
            }
            else
            {
                return "WrongFileType"; // Failed to download the file
            }

        }
        public string DownloadFileFromBlob(string fileName)
        
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(_azureOptions.ConnectionString);
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(_azureOptions.Container);
            CloudBlob blob = container.GetBlobReference(fileName);

            int counter = 0;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string fileExtension = Path.GetExtension(fileName);
            string localFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), _azureOptions.downloadsSubdirectory, fileName);

            while (File.Exists(localFilePath))
            {
                counter++;
                int copyIndex = fileNameWithoutExtension.LastIndexOf("-Copy");
                if (copyIndex != -1)
                {
                    fileNameWithoutExtension = fileNameWithoutExtension.Substring(0, copyIndex);
                }

                fileName = $"{fileNameWithoutExtension}-Copy({counter}){fileExtension}";
                localFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), _azureOptions.downloadsSubdirectory, fileName);
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

        public string GetContentType(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                default:
                    return "application/octet-stream";
            }
        }

    }
}
