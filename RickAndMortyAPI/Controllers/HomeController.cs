using Microsoft.AspNetCore.Mvc;
using RickAndMortyAPI.Intefaces;
using RickAndMortyAPI.Interface;
using RickAndMortyAPI.Models;

namespace UploadCSVToAzureBlobStorage.Controllers
{
    [ApiController]
    [Route("Home")]
    public class UploadController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogRepository modelRepository;
        private readonly IJsonService deserializeService;

        public UploadController(ILogRepository log, IJsonService ds)
        {
            _httpClient = new HttpClient();
            modelRepository = log;
            deserializeService = ds;
        }

        [HttpPost]
        [Route("CSV")]
        public async Task<Root> Upload2(IFormFile file)
        {
            var url = modelRepository.Upload(file);
            List<Episode> episodes = new List<Episode>();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        using (var stream = await response.Content.ReadAsStreamAsync())
                        using (var reader = new StreamReader(stream))
                        {
                            reader.ReadLine();
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                var parts = line.Split(',');
                                if (parts.Length == 4 && !string.IsNullOrEmpty(parts[0]))
                                {
                                    int episodeId = int.Parse(parts[0]);
                                    var ep = await deserializeService.JsonEpisode(episodeId);
                                    episodes.Add(ep);
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }


            var log = new Logs()
            {
                FilePath = file.FileName,
                Id = new Guid(),
            };
            await modelRepository.Create(log);

            var root = new Root()
            {
                Episodes = episodes,
                FemaleCharacterCount = modelRepository.FemaleCharacterCount(episodes),
                GenderlessCharacterCount = modelRepository.GenderlessCharacterCount(episodes),
                GenderUnknownCharacterCount = modelRepository.GenderlessCharacterCount(episodes),
                LocationCount = modelRepository.LocationCount(episodes),
                MaleCharacterCount = modelRepository.MaleCharacterCount(episodes),
                UploadeFilePath = file.FileName,
            };

            return root;
        }


        [HttpGet]
        [Route("Log")]
        public async Task<IActionResult> Log()
        {
            var log = await modelRepository.GetAll();
            return Ok(log);
        }
    }
}