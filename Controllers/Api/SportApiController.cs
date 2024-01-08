using Microsoft.AspNetCore.Mvc;
using SportLeader.Data;
using SportLeader.DTO;
using SportLeader.Services;

namespace SportLeader.Controllers
{
    [ApiController]
    [Route("api/sports")]
    public class SportApiController : ControllerBase
    {
        private readonly ISportLeaderService _sportLeaderService;

        public SportApiController(ISportLeaderService sportLeaderService)
        {
            _sportLeaderService = sportLeaderService;
        }
        // 종목 컨트롤러
        [HttpGet()]
        public IActionResult GetSports()
        {
            var sports = _sportLeaderService.GetSportList();

            var sportDTO = sports.Select(sp => new SportDTO
            {
                SportsNo = sp.SportsNo,
                SportsName = sp.SportsName
            });

            return Ok(sportDTO);
        }
    }
}
