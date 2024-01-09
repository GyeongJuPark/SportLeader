using Microsoft.AspNetCore.Mvc;
using SportLeader.Application.SportsLeader;
using SportLeader.DTO;

namespace SportLeader.Controllers
{
    [ApiController]
    [Route("api/schools")]
    public class SchoolApiController : ControllerBase
    {
        private readonly ISportLeaderService _sportLeaderService;

        public SchoolApiController(ISportLeaderService sportLeaderService)
        {
            _sportLeaderService = sportLeaderService;
        }
        // 학교명 컨트롤러
        [HttpGet()]
        public IActionResult GetSchools()
        {
            var schools = _sportLeaderService.GetSchoolList();
            return Ok(schools);
        }

    }
}
