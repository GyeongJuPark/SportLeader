using Microsoft.AspNetCore.Mvc;
using SportLeader.Data;
using SportLeader.DTO;
using SportLeader.Services;

namespace SportLeader.Controllers
{
    [ApiController]
    [Route("api/alls")]
    public class GetAllLeaderApiController : ControllerBase
    {
        private readonly ISportLeaderService _sportLeaderService;

        public GetAllLeaderApiController(ISportLeaderService sportLeaderService)
        {
            _sportLeaderService = sportLeaderService;
        }
        // 전체 지도자 목록 컨트롤러
        [HttpGet()]
        public IActionResult GetAllList()
        {
            var allLeaders = _sportLeaderService.GetAllList();
            var LeaderListDTO = new List<LeaderInfoDTO>();
            foreach (var leader in allLeaders)
            {
                var dto = new LeaderInfoDTO()
                {
                    LeaderNo = leader.LeaderNo,
                    LeaderName = leader.LeaderName,
                    SchoolNo = leader.T_School.SchoolName,
                    SportsNo = leader.T_Sport.SportsName,
                };
                LeaderListDTO.Add(dto);
            }
            return Ok(LeaderListDTO);
        }
    }
}
