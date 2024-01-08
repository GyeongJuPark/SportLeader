using Microsoft.AspNetCore.Mvc;

namespace SportLeader.Controllers
{
    [Route("[controller]")]
    public class DetailController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok($"DetailController Index Action, Id:");
            //return RedirectToAction("Detail", "Home", new { id="JB19-002"});
        }
    }
}
