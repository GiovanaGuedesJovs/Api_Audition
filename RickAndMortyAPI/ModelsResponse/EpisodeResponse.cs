namespace RickAndMortyAPI.Models.Response
{
    public class EpisodeResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Episode { get; set; }

        public string Air_date { get; set; }
        
        public List<string> Characters { get; set; }

        public string Url { get; set; }

        public DateTime Created { get; set; }
    }
}