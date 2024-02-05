using FluentValidation;
using SportLeader.Controllers.Client.Request;
using SportLeader.Models;
using System.Globalization;

namespace SportLeader.Application.SportsLeader.Validators
{
  public class SportLeaderValidator : AbstractValidator<RegisterSportsLeaderRequest>
  {
    public SportLeaderValidator()
    {
      const string telNoRegex = "^[0-9]{3}$";
      const string leaderCodeRegex = "^JB[0-9]{2}[-]+[0-9]{3}$";
      const string schoolCodeRegex = "^SC[0-9]{4}$";
      const string koreanEnglishRegex = "^[가-힣a-zA-Z]+$";
      const string dateRegex = "^[0-9]{4}[-]+[0-9]{2}[-]+[0-9]{2}$";
      const string certificateNameRegex = "^[가-힣a-zA-Z0-9]+$";
      const string certificateNoRegex = "^[a-zA-Z0-9]+$";

      RuleFor(x => x.LeaderNo)
          .Matches(leaderCodeRegex)
          .WithMessage("식별코드를 입력해주세요.( ex) JB19-???(? -> 숫자 1~9)");

      RuleFor(x => x.SchoolNo)
          .Matches(schoolCodeRegex)
          .WithMessage("잘못된 학교 식별코드 형태입니다.( ex) SC????(? -> 숫자 1~9)");

      RuleFor(x => x.LeaderName)
          .NotEmpty().WithMessage("성명은 필수입력값입니다.")
          .Matches(koreanEnglishRegex).WithMessage("한글 및 영어만 입력해주세요.");

      RuleFor(x => x.TelNo)
          .Matches(telNoRegex)
          .WithMessage("숫자만 입력 가능합니다(3자리-3자리-4자리)");

      RuleFor(x => x.TelNo2)
          .Matches(telNoRegex)
          .WithMessage("숫자만 입력 가능합니다(3자리-3자리-4자리)");

      RuleFor(x => x.TelNo3)
          .Matches("^[0-9]{4}$")
          .WithMessage("숫자만 입력 가능합니다(3자리-3자리-4자리)");

      RuleFor(x => x.Birthday)
          .NotEmpty()
          .WithMessage("생년월일을 입력해주세요.");

      RuleFor(x => x.EmpDT)
          .NotEmpty()
          .WithMessage("최초채용일을 올바른 형식으로 입력해주세요.(yyyy-MM-dd)");



      RuleFor(x => x.Gender)
          .NotEmpty()
          .WithMessage("성별을 선택해주세요.");

      RuleFor(x => x.SportsNo)
          .NotEmpty()
          .WithMessage("종목을 선택해주세요.");

      RuleForEach(x => x.Histories)
          .ChildRules(history =>
          {
            history.RuleFor(h => h.SchoolName)
                      .Matches(koreanEnglishRegex)
                      .WithMessage("근무기관을 입력해주세요.");

            history.RuleFor(h => h.StartDT)
                .NotEmpty()
                .WithMessage("근무시작일을 입력해주세요.")
                .Must((history, endDT) =>
                    history.StartDT == null || (endDT != null && endDT <= history.StartDT))
                .WithMessage("근무시작일은 근무종료일보다 이전이어야 합니다.");


            history.RuleFor(h => h.EndDT)
                .NotEmpty()
                .WithMessage("근무종료일을 입력해주세요.")
                .Must((history, endDT) =>
                    history.StartDT == null || (endDT != null && endDT >= history.StartDT))
                .WithMessage("근무종료일은 근무시작일보다 이후이어야 합니다.");



            history.RuleFor(h => h.SportsNo)
                      .NotEmpty()
                      .WithMessage("근무종목을 선택해주세요.");
          });

      RuleForEach(x => x.Certificates)
          .ChildRules(certificate =>
          {
            certificate.RuleFor(c => c.CertificateName)
                      .Matches(certificateNameRegex)
                      .WithMessage("자격/면허명을 한글, 영문, 숫자로 입력해주세요.");

            certificate.RuleFor(c => c.CertificateNo)
                      .Matches(certificateNoRegex)
                      .WithMessage("자격번호는 영문, 숫자로만 입력해주세요.");

            certificate.RuleFor(c => c.CertificateDT)
                      .NotEmpty()
                      .WithMessage("취득일자를 선택해주세요.");

            certificate.RuleFor(c => c.Organization)
                      .Matches(koreanEnglishRegex)
                      .WithMessage("발급기관을 한글 또는 영문으로 입력해주세요.");
          });
    }
  }
}
