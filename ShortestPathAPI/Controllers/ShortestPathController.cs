using Microsoft.AspNetCore.Mvc;
using ShortestPathAPI.Models;
using ShortestPathAPI.Services;
using ShortestPathAPI.Services.IService;

namespace ShortestPathAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShortestPathController : Controller
    {
        private readonly IShortestPathService _service;

        public ShortestPathController(IShortestPathService shortestPathService)
        {
            _service = shortestPathService;
        }

        [HttpPost("calculate")]
        public IActionResult CalculateShortestPath([FromBody] GraphRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.FromNode) || string.IsNullOrEmpty(request.ToNode))
                    return BadRequest("Invalid input. Please provide 'FromNode' and 'ToNode'.");

                var result = _service.ShortestPath(request.FromNode, request.ToNode);
                var nodeNames = result.NodeNames.Select((name, index) =>
                                    index == result.NodeNames.Count - 1 ? name : $"{name},"
                                ).ToList();

                var pathStr =
                        $"From Node Name = '{request.FromNode}' " +
                        $"To Node Name = '{request.ToNode}' " +
                        $": {string.Join(" ", nodeNames)}";
                return Ok(new { result, pathStr });
            }
            catch (Exception ex)
            {
                return BadRequest("Internal server error.");
            }


        }
    }
}
