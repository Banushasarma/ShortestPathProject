namespace ShortestPathAPI.Models
{
    public class Node
    {
        public string Name { get; set; }
        public Dictionary<string, int> Neighbors { get; set; } = new();
    }

    public class Graph
    {
        public List<Node> graphNodes { get; set; } = new List<Node>
        {
            new Node { Name = "A", Neighbors = new Dictionary<string, int> { { "B", 4 }, { "C", 6 } } },
            new Node { Name = "B", Neighbors = new Dictionary<string, int> { { "F", 2 }, { "A", 4 } } },
            new Node { Name = "C", Neighbors = new Dictionary<string, int> { { "D", 8 }, { "A", 6 } } },
            new Node { Name = "D", Neighbors = new Dictionary<string, int> { { "A", 6 }, { "E", 4 }, { "G", 1 } } },
            new Node { Name = "E", Neighbors = new Dictionary<string, int> { { "B", 2 }, { "D", 4 }, { "F", 3 }, { "I", 8 } } },
            new Node { Name = "F", Neighbors = new Dictionary<string, int> { { "B", 2 }, { "E", 3 }, { "H", 6 }, { "G", 4 } } },
            new Node { Name = "G", Neighbors = new Dictionary<string, int> { { "D", 1 }, { "F", 4 }, { "H", 5 }, { "I", 5 } } },
            new Node { Name = "H", Neighbors = new Dictionary<string, int> { { "F", 6 }, { "G", 5 } } },
            new Node { Name = "I", Neighbors = new Dictionary<string, int> { { "E", 8 }, { "G", 5 } } }
        };
    }
}
