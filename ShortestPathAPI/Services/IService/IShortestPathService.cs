using ShortestPathAPI.Models;

namespace ShortestPathAPI.Services.IService
{
    public interface IShortestPathService
    {
        ShortestPathData ShortestPath(string fromNodeName, string toNodeName);
    }
}
