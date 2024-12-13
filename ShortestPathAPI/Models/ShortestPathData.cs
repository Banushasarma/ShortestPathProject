namespace ShortestPathAPI.Models
{
    public class ShortestPathData
    {
        public List<string> NodeNames { get; set; } = new();
        public int Distance { get; set; }
    }
    public class ShortestPathDataResult
    {
        public string pathStr { get; set; }
        public ShortestPathData result { get; set; }
    }
}
