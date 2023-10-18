
using Azure.Storage.Blobs;
using RickAndMortyAPI.Intefaces;
using RickAndMortyAPI.Interface;
using RickAndMortyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace RickAndMortyAPI.Data
{
    public class LogRepository : Repository<Logs>, ILogRepository
    {
        private readonly IJsonService _deserializeService;
        private readonly IConfiguration _configuration;

        public LogRepository(ApplicationDbContext context, IJsonService deserializeService, IConfiguration configuration) : base(context)
        {
            _deserializeService = deserializeService;
            _configuration = configuration;
        }

        public int LocationCount(List<Episode> episodes)
        {
            return episodes.SelectMany(episode => episode.Characters.Select(character => character.Location)).Distinct().ToList().Count();
        }

        public int FemaleCharacterCount(List<Episode> episodes)
        {
            var count = episodes.SelectMany(episode => episode.Characters).Count(character => character.Gender.Equals("Female", StringComparison.OrdinalIgnoreCase));
            return count;
        }

        public int MaleCharacterCount(List<Episode> episodes)
        {
            return episodes.SelectMany(episode => episode.Characters).Count(character => character.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase));
        }

        public int GenderlessCharacterCount(List<Episode> episodes)
        {
            return episodes.SelectMany(episode => episode.Characters).Count(character => string.Equals(character.Gender, "Unknown", StringComparison.OrdinalIgnoreCase)); ;
        }

        public int UnknowCharacterCount(List<Episode> episodes)
        {
            return episodes.SelectMany(episode => episode.Characters).Count(character => string.Equals(character.Name, "Unknown", StringComparison.OrdinalIgnoreCase)); ;
        }

        public string Upload(IFormFile file)
        {
            var connectionString = _configuration.GetConnectionString("Blob");
            var blobClient = new BlobClient(connectionString, "blob", file.FileName + Guid.NewGuid());

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;
                blobClient.Upload(stream);
            }

            return blobClient.Uri.AbsoluteUri;
        }
    }
}