using Microsoft.AspNetCore.Mvc;
using SportLeader.Data;
using SportLeader.DTO;
using SportLeader.Services;

namespace SportLeader.Controllers
{
    [ApiController]
    [Route("api/leaders")]
    public class LeaderApiController : ControllerBase
    {
        private readonly ISportLeaderService _sportLeaderService;

        public LeaderApiController(ISportLeaderService sportLeaderService)
        {
            _sportLeaderService = sportLeaderService;
        }

        // 지도자(식별코드) 컨트롤러
        [HttpGet()]
        public IActionResult GetLeaders()
        {
            var leaders = _sportLeaderService.GetLeaderList();

            var sportDTO = leaders.Select(sp => new LeaderDTO
            {
                LeaderNo = sp.LeaderNo,
                LeaderName = sp.LeaderName
            });

            return Ok(leaders);
        }
        //[HttpGet]
        //public IActionResult GetLeaders()
        //{
        //    //api/leaders
        //    return Ok();
        //}
    }
}
