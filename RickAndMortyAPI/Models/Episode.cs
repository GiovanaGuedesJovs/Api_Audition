namespace RickAndMortyAPI.Models
{
    public class Episode
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Episode_ { get; set; }

        public string Air_date { get; set; }

        public List<Character> Characters { get; set; }
    }
}