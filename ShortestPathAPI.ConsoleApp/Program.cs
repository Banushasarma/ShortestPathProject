using ShortestPathAPI.Models;
using ShortestPathAPI.Services;
using ShortestPathAPI.Services.IService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShortestPathConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Shortest Path Calculator");

            // Collect user input
            Console.Write("Enter the starting node (FromNode): ");
            string fromNode = Console.ReadLine();

            Console.Write("Enter the destination node (ToNode): ");
            string toNode = Console.ReadLine();


            try
            {

                // Send the request to the API and get the response
                ShortestPathData shortestPathData = new ShortestPathService().ShortestPath(fromNode, toNode);

                if (shortestPathData != null)
                {
                    Console.WriteLine("\nShortest Path Result:");
                    Console.WriteLine($"Nodes traversed: {string.Join(", ", shortestPathData.NodeNames)}");
                    Console.WriteLine($"Total Distance: {shortestPathData.Distance}");
                }
                else
                {
                    Console.WriteLine("\nNo valid path found or API returned an error.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
        }


    }

    // DTOs for API interaction
    public class GraphRequest
    {
        public string FromNode { get; set; }
        public string ToNode { get; set; }
    }


    public class ShortestPathResponse
    {
        public List<string> NodeNames { get; set; }
        public int Distance { get; set; }
    }
    public class ShortestPathResultResponse
    {
        public string pathStr { get; set; }
        public ShortestPathResponse result { get; set; }
    }


}
