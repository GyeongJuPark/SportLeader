using System.ComponentModel.DataAnnotations;
namespace SportLeader.DTO
{
  public class LeaderImageDto
  {
    public string LeaderImage { get; set; }
  }

  public class LeaderDto
  {
    public string LeaderNo { get; set; }
    public string LeaderName { get; set; }
  }

  public class SportDto
  {
    public string SportsNo { get; set; }
    public string SportsName { get; set; }
  }

  public class SchoolDto
  {
    public string SchoolNo { get; set; }
    public string SchoolName { get; set; }
  }

  public class LeaderInfoDto
  {
    [Required]
    [RegularExpression("^JB[0-9]{2}[-]+[0-9]{3}$", ErrorMessage = "JB19-??? 형식이어야 합니다.")]
    public string LeaderNo { get; set; }

    [Required]
    [RegularExpression("^[가-힣a-zA-Z]+$", ErrorMessage = "한글 또는 영문자만 입력 가능합니다.")]
    public string LeaderName { get; set; }

    [Required(ErrorMessage = "사진을 선택해주세요.")]
    public string LeaderImage { get; set; }

    [Required(ErrorMessage = "생년월일을 선택해주세요.")]
    public DateTime Birthday { get; set; }

    [Required(ErrorMessage = "성별을 선택해주세요.")]
    public string Gender { get; set; }

    [Required]
    [RegularExpression("^CN[0-9]{4}$")]
    public string SportsNo { get; set; }

    [Required]
    public string SportsName { get; set; }

    [Required]
    [RegularExpression("^SC[0-9]{4}$")]
    public string SchoolNo { get; set; }

    [Required]
    [RegularExpression("^[가-힣a-zA-Z]+$", ErrorMessage = "한글 또는 영문자만 입력 가능합니다.")]
    public string SchoolName { get; set; }

    [Required]
    [RegularExpression("[0-9]{3}$", ErrorMessage = "전화번호를 제대로 입력해주세요.(예시 : 063-123-1234)")]
    public string TelNo { get; set; }

    [Required]
    [RegularExpression("[0-9]{3}$", ErrorMessage = "전화번호를 제대로 입력해주세요.(예시 : 063-123-1234)")]
    public string TelNo2 { get; set; }

    [Required]
    [RegularExpression("[0-9]{4}$", ErrorMessage = "전화번호를 제대로 입력해주세요.(예시 : 063-123-1234)")]
    public string TelNo3 { get; set; }

    [Required(ErrorMessage = "최초채용일을 선택해주세요.")]
    public DateTime EmpDT { get; set; }

    public IEnumerable<HistoryDto> Histories { get; set; }
    public IEnumerable<CertificateDto> Certificates { get; set; }
  }

  public class TestDto
  {
    public string LeaderNo { get; set; }

    public string LeaderName { get; set; }

  }
  public class CertificateDto
  {
    [Required]
    public string LeaderNo { get; set; }

    [Required]
    public string CertificateName { get; set; }

    [Required]
    public string CertificateNo { get; set; }

    public DateTime? CertificateDT { get; set; }

    [Required]
    public string Organization { get; set; }
  }

  public class HistoryDto
  {
    [Required]
    public string LeaderNo { get; set; }

    public DateTime? StartDT { get; set; }

    public DateTime? EndDT { get; set; }

    [Required]
    //[RegularExpression("^[가-힣a-zA-Z]+$", ErrorMessage = "한글 또는 영문자만 입력 가능합니다.")]
    public string SchoolName { get; set; }

    [Required]
    [RegularExpression("^CN[0-9]{4}$")]
    public string SportsNo { get; set; }
    public string SportsName { get; set; }

  }
}
