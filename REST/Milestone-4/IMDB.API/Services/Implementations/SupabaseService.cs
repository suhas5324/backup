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
    using IMDB.API.Services.Interfaces;
    public class SupabaseService : ISupabaseService
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

