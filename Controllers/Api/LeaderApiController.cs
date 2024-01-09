using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportLeader.Application.SportsLeader;

[ApiController]
[Route("api/leaders")]
public class LeaderApiController : ControllerBase
{
    private readonly ISportLeaderService _sportLeaderService;

    public LeaderApiController(ISportLeaderService sportLeaderService)
    {
        _sportLeaderService = sportLeaderService;
    }

    // 지도자(식별코드) 목록 컨트롤러
    [HttpGet()]
    public IActionResult GetLeaders()
    {
        var leaders = _sportLeaderService.GetLeaderList();

        return Ok(leaders);
    }

    // 전체 지도자 목록 컨트롤러
    [HttpGet("all")]
    public IActionResult GetAllLeaders()
    {
        var allLeaders = _sportLeaderService.GetAllList();

        return Ok(allLeaders);
    }

    // 특정 LeaderNo에 해당하는 지도자 데이터 컨트롤러
    [HttpGet("{leaderNo}")]
    public IActionResult GetLeaderByLeaderNo(string leaderNo)
    {
        var leader = _sportLeaderService.GetAllList(leaderNo)
            .FirstOrDefault(r => r.LeaderNo == leaderNo);

        return Ok(leader);
    }
}
