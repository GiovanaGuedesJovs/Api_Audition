
using RickAndMortyAPI.Models;

namespace RickAndMortyAPI.Intefaces
{
    public interface ILogRepository : IRepository<Logs>
    {
        public string Upload(IFormFile file);

        public int LocationCount(List<Episode> episodes);

        public int FemaleCharacterCount(List<Episode> episodes);

        public int MaleCharacterCount(List<Episode> episodes);

        public int GenderlessCharacterCount(List<Episode> episodes);

        public int UnknowCharacterCount(List<Episode> episodes);
    }
}