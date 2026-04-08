namespace IMDB.API.Services.Implementations
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Http;
    using Supabase;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    //     public class SupabaseService
    //     {
    //         private readonly Client _client;
    //         private readonly IConfiguration _config;

    //         public SupabaseService(Client client, IConfiguration config)
    //         {
    //             _client = client;
    //             _config = config;
    //         }

    //         public async Task<string> UploadFile(IFormFile file)
    //         {
    //             var bucket = _config["Supabase:Bucket"];

    //             var extension = Path.GetExtension(file.FileName);
    //             var fileName = $"movies/{Guid.NewGuid():N}{extension}";

    //             using var ms = new MemoryStream();
    //             await file.CopyToAsync(ms);

    //             await _client.Storage
    //                 .From(bucket)
    //                 .Upload(ms.ToArray(), fileName);

    //             return _client.Storage
    //                 .From(bucket)
    //                 .GetPublicUrl(fileName);
    //         }

    //         public async Task DeleteFile(string fileUrl)
    //         {
    //             var bucket = _config["Supabase:Bucket"];

    //             var fileName = GetStoragePath(fileUrl, bucket);

    //             await _client.Storage
    //                 .From(bucket)
    //                 .Remove(new List<string> { fileName });
    //         }

    //         private static string GetStoragePath(string fileUrl, string bucket)
    //         {
    //             if (!Uri.TryCreate(fileUrl, UriKind.Absolute, out var uri))
    //             {
    //                 return fileUrl;
    //             }

    //             var marker = $"/object/public/{bucket}/";
    //             var markerIndex = uri.AbsolutePath.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
    //             if (markerIndex < 0)
    //             {
    //                 return Uri.UnescapeDataString(uri.Segments.Last());
    //             }

    //             var storagePath = uri.AbsolutePath.Substring(markerIndex + marker.Length);
    //             return Uri.UnescapeDataString(storagePath);
    //         }
    //     }
    public class SupabaseService
    {
        private readonly Client _client;
        private readonly IConfiguration _config;

        public SupabaseService(Client client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            var bucket = _config["Supabase:Bucket"];
            var fileName = file.FileName;

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            await _client.Storage
                .From(bucket)
                .Upload(ms.ToArray(), fileName);

            return _client.Storage
                .From(bucket)
                .GetPublicUrl(fileName);
        }

        public async Task DeleteFile(string fileUrl)
        {
            var bucket = _config["Supabase:Bucket"];
            var fileName = fileUrl.Split("/").Last();

            await _client.Storage
                .From(bucket)
                .Remove(new List<string> { fileName });
        }
    }
}

