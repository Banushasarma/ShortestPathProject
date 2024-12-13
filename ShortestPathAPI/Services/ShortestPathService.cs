using ShortestPathAPI.Models;
using ShortestPathAPI.Services.IService;

namespace ShortestPathAPI.Services
{
    public class ShortestPathService : IShortestPathService
    {
        public ShortestPathData ShortestPath(string fromNodeName, string toNodeName)
        {
            List<Node> graphNodes = new Graph().graphNodes;
            var distances = new Dictionary<string, int>();
            var previousNodes = new Dictionary<string, string?>();
            var unvisited = new HashSet<string>();

            // Initialize distances and unvisited nodes
            foreach (var node in graphNodes)
            {
                distances[node.Name] = int.MaxValue;
                unvisited.Add(node.Name);
                previousNodes[node.Name] = null;
            }

            // Set distance to the source node as 0
            distances[fromNodeName] = 0;

            while (unvisited.Count > 0)
            {
                // Get the node with the smallest distance
                var currentNode = unvisited.OrderBy(n => distances[n]).First();
                unvisited.Remove(currentNode);

                // Stop processing if the smallest distance is still int.MaxValue (unreachable)
                if (distances[currentNode] == int.MaxValue)
                    break;

                // If we reached the destination node, stop processing
                if (currentNode == toNodeName)
                    break;

                var currentGraphNode = graphNodes.FirstOrDefault(n => n.Name == currentNode);
                if (currentGraphNode == null) continue;

                // Update distances for neighbors
                foreach (var neighbor in currentGraphNode.Neighbors)
                {
                    if (!unvisited.Contains(neighbor.Key)) continue;

                    var newDist = distances[currentNode] + neighbor.Value;
                    if (newDist < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = newDist;
                        previousNodes[neighbor.Key] = currentNode;
                    }
                }
            }

            // Build the path from destination to source
            var path = new List<string>();
            var currentNodeName = toNodeName;
            while (currentNodeName != null)
            {
                path.Insert(0, currentNodeName);
                currentNodeName = previousNodes[currentNodeName];
            }

            // Validate if the path starts from the source node
            if (path.First() != fromNodeName)
            {
                return new ShortestPathData
                {
                    NodeNames = new List<string>(),
                    Distance = int.MaxValue
                };
            }

            return new ShortestPathData
            {
                NodeNames = path,
                Distance = distances[toNodeName]
            };

        }
    }
}
