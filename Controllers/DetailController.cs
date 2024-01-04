using Microsoft.AspNetCore.Mvc;

namespace SportLeader.Controllers
{
    [Route("detail1")]
    public class DetailController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("Detail", "Home", new { id="JB19-002"});
        }
    }
}
