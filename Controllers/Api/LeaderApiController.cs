using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SportLeader.Application.SportsLeader;
using SportLeader.Controllers.Client.Request;
using SportLeader.Infra.DB;
[ApiController]
[Route("api/leaders")]
public class LeaderApiController : ControllerBase
{
  private readonly ISportLeaderService _sportLeaderService;
  private readonly SpotrsLeaderDBContext _dBContext;

  public LeaderApiController(ISportLeaderService sportLeaderService, SpotrsLeaderDBContext dBContext)
  {
    _sportLeaderService = sportLeaderService;
    _dBContext = dBContext;
  }

  // 지도자(식별코드) 목록 컨트롤러
  [HttpGet()]
  public IActionResult GetLeaders()
  {
    var leaders = _sportLeaderService.GetLeaderList();

    return Ok(leaders);
  }

  // 전체 지도자 목록 컨트롤러
  [HttpGet("list")]
  public IActionResult GetAllLeaders()
  {
    var allLeaders = _sportLeaderService.GetAllList();

    return Ok(allLeaders);
  }

  //특정 LeaderNo에 해당하는 지도자 데이터 컨트롤러
  [HttpGet("{leaderNo}")]
  public IActionResult GetLeaderByLeaderNo(string leaderNo)
  {
    var leader = _sportLeaderService.GetAllList(leaderNo)
        .FirstOrDefault(r => r.LeaderNo == leaderNo);

    return Ok(leader);
  }

  // 지도자 등록
  [HttpPost()]
  public IActionResult CreateLeader(
    [FromBody] RegisterSportsLeaderRequest request,
    [FromServices] IValidator<RegisterSportsLeaderRequest> validator)
  {
    var validationResult = validator.Validate(request);
    if (validationResult.IsValid)
    {
      _sportLeaderService.Create(request);
      return Ok();
    }
    else
    {
      return BadRequest(validationResult.Errors);
    }
  }

  // 지도자 삭제
  [HttpDelete()]
  public IActionResult Delete([FromBody] string[] leaderNo)
  {
    _sportLeaderService.Delete(leaderNo);
    return Ok();
  }

  // 지도자 수정
  [HttpPatch()]
  public async Task<IActionResult> Update(
    [FromBody] RegisterSportsLeaderRequest request,
    [FromServices] IValidator<RegisterSportsLeaderRequest> validator
    )
  {
    var validationResult = validator.Validate(request);
    if (validationResult.IsValid)
    {
      _sportLeaderService.Update(request);
      return Ok();
    }
    else
    {
      return BadRequest(validationResult.Errors);
    }
  }

}
