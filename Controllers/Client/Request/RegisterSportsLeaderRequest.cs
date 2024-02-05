using System.ComponentModel.DataAnnotations;
using SportLeader.DTO;

namespace SportLeader.Controllers.Client.Request
{
    public class RegisterSportsLeaderRequest
    {
        public string LeaderNo { get; set; }

        public string LeaderName { get; set; }

        [Required(ErrorMessage = "사진을 선택해주세요.")]
        public string LeaderImage { get; set; }

        //[Required(ErrorMessage = "생년월일을 선택해주세요.")]
        public DateTime? Birthday { get; set; }

        //[Required(ErrorMessage = "성별을 선택해주세요.")]
        public string Gender { get; set; }

        [Required]
        [RegularExpression("^CN[0-9]{4}$")]
        public string SportsNo { get; set; }

        [Required]
        [RegularExpression("^SC[0-9]{4}$")]
        public string SchoolNo { get; set; }

        [Required]
        [RegularExpression("^[가-힣a-zA-Z]+$", ErrorMessage = "한글 또는 영문자만 입력 가능합니다.")]
        public string SchoolName { get; set; }

        [Required]
        public string TelNo { get; set; }

        [Required]
        public string TelNo2 { get; set; }

        [Required]
        public string TelNo3 { get; set; }

        //[Required(ErrorMessage = "최초채용일을 선택해주세요.")]
        public DateTime? EmpDT { get; set; }

        public IEnumerable<HistoryDto> Histories { get; set; }
        public IEnumerable<CertificateDto> Certificates { get; set; }
    }
}
