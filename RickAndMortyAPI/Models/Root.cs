namespace RickAndMortyAPI.Models
{
    public class Root
    {
        public List<Episode> Episodes { get; set; }

        public int LocationCount { get; set; }

        public int FemaleCharacterCount { get; set; }

        public int MaleCharacterCount { get; set; }

        public int GenderlessCharacterCount { get; set; }

        public int GenderUnknownCharacterCount{ get; set; }

        public string UploadeFilePath { get; set; }

    }
}