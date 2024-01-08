using Microsoft.AspNetCore.Mvc;
using SportLeader.Data;
using SportLeader.DTO;
using SportLeader.Services;

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

            var schoolDTO = schools.Select(sp => new SchoolDTO
            {
                SchoolNo = sp.SchoolNo,
                SchoolName = sp.SchoolName
            });

            return Ok(schoolDTO);
        }

    }
}
