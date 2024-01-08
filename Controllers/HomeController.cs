using Microsoft.AspNetCore.Mvc;
using SportLeader.Data;
using SportLeader.DTO;
using SportLeader.Services;


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
            var model = new LeaderInfoDTO();
            model.Histories = new List<HistoryDTO>();
            model.Certificates = new List<CertificateDTO>();

            return View(model);
        }

        [HttpPost("Register")]
        public IActionResult Register(LeaderInfoDTO leaderInfo)
        {
            if (ModelState.IsValid)
            {
                if (_sportLeaderService.AddLeaderInfo(leaderInfo))
                {
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
            }

            return View(leaderInfo);
        }

        [HttpGet("Detail")]
        public IActionResult Detail([FromQuery] string id)
        {
            var leader = _sportLeaderService.Test(id)
                         .FirstOrDefault(r => r.LeaderNo == id);

            var leaderDTO = new LeaderInfoDTO
            {
                LeaderNo = id,
                LeaderImage = leader.T_LeaderImage.LeaderImage,
                LeaderName = leader.LeaderName,
                SchoolNo = leader.T_School.SchoolName,
                SportsNo = leader.T_Sport.SportsName,
                Birthday = leader.Birthday,
                Gender = leader.Gender,
                TelNo = leader.TelNo,
                EmpDT = leader.EmpDT,

                Histories = leader.T_History
                    .Select(history => new HistoryDTO
                    {
                        SchoolName = history.SchoolName,
                        StartDT = history.StartDT,
                        EndDT = history.EndDT,
                        SportsNo = history.T_Sport.SportsName
                    }),

                Certificates = leader.T_Certificate
                    .Select(certificate => new CertificateDTO
                    {
                        CertificateName = certificate.CertificateName,
                        CertificateNo = certificate.CertificateNo,
                        CertificateDT = certificate.CertificateDT,
                        Organization = certificate.Organization
                    }),
            };

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
            var leader = _sportLeaderService.Test(id)
                         .FirstOrDefault(r => r.LeaderNo == id);

            var leaderDTO = new LeaderInfoDTO
            {
                LeaderNo = id,
                LeaderImage = leader.T_LeaderImage.LeaderImage,
                LeaderName = leader.LeaderName,
                SchoolNo = leader.T_School.SchoolNo,
                SchoolName = leader.T_School.SchoolName,
                SportsNo = leader.SportsNo,
                Birthday = leader.Birthday,
                Gender = leader.Gender,
                TelNo = leader.TelNo,
                EmpDT = leader.EmpDT,

                Histories = leader.T_History
                    .Select(history => new HistoryDTO
                    {
                        LeaderNo = leader.LeaderNo,
                        SchoolName = history.SchoolName,
                        StartDT = history.StartDT,
                        EndDT = history.EndDT,
                        SportsNo = history.SportsNo
                    }),

                Certificates = leader.T_Certificate
                    .Select(certificate => new CertificateDTO
                    {
                        LeaderNo = leader.LeaderNo,
                        CertificateName = certificate.CertificateName,
                        CertificateNo = certificate.CertificateNo,
                        CertificateDT = certificate.CertificateDT,
                        Organization = certificate.Organization
                    }),
            };

            return View("Update", leaderDTO); 
        }
        [HttpPost("Update")]
        public IActionResult Update([FromForm] LeaderInfoDTO model)
        {
            if (ModelState.IsValid)
            {
                _sportLeaderService.Update(model);
                ModelState.Clear();

                return RedirectToAction("Detail", "Home", new { id = model.LeaderNo });

            }

            return View("Update", model);
        }

    }
}
