using RickAndMortyAPI.Interface;
using RickAndMortyAPI.Models;
using RickAndMortyAPI.Models.Response;
using Newtonsoft.Json;

namespace RickAndMortyAPI.Service
{
    public class JsonService : IJsonService
    {
        private readonly HttpClient _httpClient;

        public JsonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Location>JsonLocation(string url)
        {
            var conteudo = await Json(url);
            var location = JsonConvert.DeserializeObject<Location>(conteudo);
            return location;
        }
        public async Task<Origin> JsonOrigin(string url)
        {
            var conteudo = await Json(url);
            var origin = JsonConvert.DeserializeObject<Origin>(conteudo);
            return origin;
        }
        public async Task<string> Json(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<List<Character>> JsonCharacter(List<string> characters)
        {
            var lista = new List<Character>();
            var location = new Location();
            var origin = new Origin();
            try
            {
                foreach (var item in characters)
                {
                    string url = item;
                    var conteudo = await Json(url);
                    var characterResponse = JsonConvert.DeserializeObject<CharacterResponse>(conteudo);
                    
                    location = !string.IsNullOrEmpty(characterResponse.Location.Url) ? await JsonLocation(characterResponse.Location.Url) : new Location();
                    origin = !string.IsNullOrEmpty(characterResponse.Origin.Url) ? await JsonOrigin(characterResponse.Origin.Url) : new Origin();

                    var character = new Character()
                    {
                        Id = characterResponse.Id,
                        Name = characterResponse.Name,
                        Species = characterResponse.Species,
                        Status = characterResponse.Status,
                        Type = characterResponse.Type,
                        Gender = characterResponse.Gender,
                        Location = new Location()
                        {
                            Dimension = location.Dimension,
                            Id = location.Id,
                            Name = location.Name,
                            Type = location.Type
                        },
                        Origin = new Origin()
                        {
                            Dimension = origin.Dimension,
                            Id = origin.Id,
                            Name = origin.Name,
                            Type = origin.Type,
                        }
                    };
                    lista.Add(character);
                }

                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Episode> JsonEpisode(int episodeId)
        {
            try
            {
                string apiUrl = $"https://rickandmortyapi.com/api/episode/{episodeId}";

                var content = await Json(apiUrl);

                var episodeInfo = JsonConvert.DeserializeObject<EpisodeResponse>(content);
                var caracters = await JsonCharacter(episodeInfo.Characters);
                var episode = new Episode()
                {
                    Id = episodeInfo.Id,
                    Name = episodeInfo.Name,
                    Air_date = episodeInfo.Air_date,
                    Characters = caracters,
                    Episode_ = episodeInfo.Episode,
                };
                return episode;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
