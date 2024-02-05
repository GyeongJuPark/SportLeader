using Microsoft.AspNetCore.Mvc;
using SportLeader.Application.SportsLeader;
using SportLeader.Controllers.Client.Request;
using SportLeader.DTO;


namespace SportLeader.Controllers
{
    [Route("/")]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ISportLeaderService _sportLeaderService;

        public HomeController(ISportLeaderService sportLeaderService)
        {
            _sportLeaderService = sportLeaderService;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            var model = new LeaderInfoDto();
            model.Histories = new List<HistoryDto>();
            model.Certificates = new List<CertificateDto>();

            return View(model);
        }

    [HttpPost("Register")]
    public IActionResult Register([FromForm] RegisterSportsLeaderRequest request)
    {
      if (ModelState.IsValid)
      {
        _sportLeaderService.Create(request);
        ModelState.Clear();
        return RedirectToAction("Index");
      }

      return View(request);
    }

    [HttpGet("Detail")]
        public IActionResult Detail([FromQuery] string id)
        {
            var leaderDTO = _sportLeaderService.Read(id);

            return View(leaderDTO);
        }


        [HttpDelete("Delete")]
        public IActionResult Delete([FromBody] string[] leaderNos)
        {
            _sportLeaderService.Delete(leaderNos);

            return RedirectToAction("Index");
        }

        [HttpGet("Update")]
        public IActionResult Update([FromQuery] string id)
        {
            var leaderDTO = _sportLeaderService.Update(id);

            return View("Update", leaderDTO);
        }

        [HttpPost("Update")]
        public IActionResult Update([FromForm] RegisterSportsLeaderRequest request)
        {
            if (ModelState.IsValid)
            {
                _sportLeaderService.Update(request);
                ModelState.Clear();

                return RedirectToAction("Detail", "Home", new { id = request.LeaderNo });
            }

            return View("Update", request);
        }

    }
}
