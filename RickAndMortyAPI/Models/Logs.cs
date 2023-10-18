namespace RickAndMortyAPI.Models
{
    public class Logs : Entity
    {

        public string FilePath { get; set; }

        public DateTime DateNow { get; set; } = DateTime.Now;

    }
}
