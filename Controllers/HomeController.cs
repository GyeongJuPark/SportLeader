using Microsoft.AspNetCore.Mvc;
using SportLeader.Data;
using SportLeader.DTO;
using SportLeader.Services;
using SportLeader.ViewModels;

namespace SportLeader.Controllers
{
    public class HomeController : Controller
    {
        private readonly SpotrsLeaderDBContext _context;
        private readonly ISportLeaderService _sportLeaderService;

        public HomeController(SpotrsLeaderDBContext context,
            ISportLeaderService sportLeaderService)
        {
            _context = context;
            _sportLeaderService = sportLeaderService;
        }
        
        public IActionResult Index()
        {
            var leaders = _sportLeaderService.GetLeaderList();
            var leaderDto = leaders.Select(ap => new LeaderDTO
            {
                No = ap.LeaderNo,
                Name = ap.LeaderName
            });

            var viewModel = new LeaderViewModel
            {
                Leaders = leaderDto.ToList(),
            };

            return View(viewModel);
        }


        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Detail() 
        {
            return View(); 
        }
    }
}
