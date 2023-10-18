using RickAndMortyAPI.Models;

namespace RickAndMortyAPI.Interface
{
    public interface IJsonService
    {
        Task<Episode> JsonEpisode(int episodeId);

        Task<List<Character>> JsonCharacter(List<string> characters);

        Task<string> Json(string url);
    }
}
