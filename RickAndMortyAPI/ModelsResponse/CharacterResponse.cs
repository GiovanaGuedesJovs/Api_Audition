namespace RickAndMortyAPI.Models.Response
{
    public class CharacterResponse
    {
        public int Id { get; set; }
       
        public string Name { get; set; }

        public string Status { get; set; }

        public string Species { get; set; }

        public string Type { get; set; }

        public string Gender { get; set; }

        public OriginResponse Origin { get; set; }
        
        public LocationResponse Location { get; set; }
    }
}
