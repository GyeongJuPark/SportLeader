using Microsoft.AspNetCore.Mvc;
using SportLeader.Data;
using SportLeader.DTO;
using SportLeader.Services;

namespace SportLeader.Controllers
{
    [ApiController]
    [Route("api/schools")]
    public class SchoolAPIController : ControllerBase
    {
        private readonly SpotrsLeaderDBContext _context;
        private readonly ISportLeaderService _sportLeaderService;

        public SchoolAPIController(SpotrsLeaderDBContext context,
            ISportLeaderService sportLeaderService)
        {
            _context = context;
            _sportLeaderService = sportLeaderService;
        }
        // 학교명 컨트롤러
        [HttpGet()]
        public IActionResult GetSchools()
        {
            var schools = _sportLeaderService.GetSchoolList();

            var sportDTO = schools.Select(sp => new SchoolDTO
            {
                SchoolNo = sp.SchoolNo,
                SchoolName = sp.SchoolName
            });

            return Ok(schools);
        }

    }
}
