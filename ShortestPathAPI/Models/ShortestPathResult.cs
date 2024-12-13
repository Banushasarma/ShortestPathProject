namespace ShortestPathAPI.Models
{
    public class ShortestPathResult
    {
        public List<string> NodeNames { get; set; } = new();
        public int Distance { get; set; }
    }
}
