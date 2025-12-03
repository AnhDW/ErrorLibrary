using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        [HttpGet("value/{value}")]
        public IActionResult GetTest(string value)
        {
            return Ok($"Test successful/{value}");
        }

        [HttpPost]
        public IActionResult PostTest([FromBody]string value)
        {
            return Ok($"Received: {value}");
        }
    }
}
