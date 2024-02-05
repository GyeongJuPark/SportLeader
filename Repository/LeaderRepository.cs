using Microsoft.EntityFrameworkCore;
using SportLeader.Controllers.Client.Request;
using SportLeader.Infra.DB;
using SportLeader.Models;

namespace SportLeader.Repository
{
  public class LeaderRepository : ILeaderRepository
  {
    private readonly SpotrsLeaderDBContext _dBContext;

    public LeaderRepository(SpotrsLeaderDBContext dBContext)
    {
      _dBContext = dBContext;
    }

    // 1. 지도자 등록
    public bool Create(RegisterSportsLeaderRequest LeaderWorkInfo)
    {
      var _transaction = _dBContext.Database.BeginTransaction();
      try
      {
        T_LeaderWorkInfo t_leaderWorkInfo = new T_LeaderWorkInfo
        {
          LeaderNo = LeaderWorkInfo.LeaderNo,
          LeaderName = LeaderWorkInfo.LeaderName,
          Birthday = LeaderWorkInfo.Birthday,
          Gender = LeaderWorkInfo.Gender,
          SportsNo = LeaderWorkInfo.SportsNo,
          SchoolNo = LeaderWorkInfo.SchoolNo,
          TelNo = $"{LeaderWorkInfo.TelNo}-{LeaderWorkInfo.TelNo2}-{LeaderWorkInfo.TelNo3}",
          EmpDT = LeaderWorkInfo.EmpDT
        };

        List<T_History> historyList = new List<T_History>();
        foreach (var historyItems in LeaderWorkInfo.Histories)
        {
          T_History t_history = new T_History
          {
            LeaderNo = LeaderWorkInfo.LeaderNo,
            StartDT = historyItems.StartDT,
            EndDT = historyItems.EndDT,
            SchoolName = historyItems.SchoolName,
            SportsNo = historyItems.SportsNo
          };
          historyList.Add(t_history);
        }

        List<T_Certificate> certificateList = new List<T_Certificate>();
        foreach (var certificateItems in LeaderWorkInfo.Certificates)
        {
          T_Certificate t_certificate = new T_Certificate
          {
            LeaderNo = LeaderWorkInfo.LeaderNo,
            CertificateName = certificateItems.CertificateName,
            CertificateNo = certificateItems.CertificateNo,
            CertificateDT = certificateItems.CertificateDT,
            Organization = certificateItems.Organization
          };
          certificateList.Add(t_certificate);
        }

        T_LeaderImage t_leaderImage = new T_LeaderImage
        {
          LeaderNo = LeaderWorkInfo.LeaderNo,
          LeaderImage = LeaderWorkInfo.LeaderImage
        };

        _dBContext.T_LeaderWorkInfo.Add(t_leaderWorkInfo);
        _dBContext.SaveChanges();

        _dBContext.T_History.AddRange(historyList);
        _dBContext.T_Certificate.AddRange(certificateList);
        _dBContext.T_LeaderImage.Add(t_leaderImage);

        _dBContext.SaveChanges();
        _transaction.Commit();
        return true;
      }
      catch (Exception ex)
      {
        _transaction.Rollback();
        throw new Exception("생성 중에 뭐때문에 오류가 발생했다.");
        return false;
      }
    }

    // 2. 지도자 수정
    public bool Modify(RegisterSportsLeaderRequest LeaderWorkInfo)
    {
      var entity = _dBContext.T_LeaderWorkInfo
          .Include(l => l.T_History)
          .Include(l => l.T_Certificate)
          .Include(l => l.T_LeaderImage)
          .FirstOrDefault(l => l.LeaderNo == LeaderWorkInfo.LeaderNo);

      if (entity != null)
      {
        // 1. 기존 근무이력, 자격사항 삭제
        _dBContext.T_History.RemoveRange(entity.T_History);
        _dBContext.T_Certificate.RemoveRange(entity.T_Certificate);

        // 2. 지도자 정보 수정된 내용 반영
        entity.LeaderName = LeaderWorkInfo.LeaderName;
        entity.Birthday = LeaderWorkInfo.Birthday;
        entity.Gender = LeaderWorkInfo.Gender;
        entity.SportsNo = LeaderWorkInfo.SportsNo;
        entity.SchoolNo = LeaderWorkInfo.SchoolNo;
        entity.TelNo = $"{LeaderWorkInfo.TelNo}-{LeaderWorkInfo.TelNo2}-{LeaderWorkInfo.TelNo3}";
        entity.EmpDT = LeaderWorkInfo.EmpDT;

        // 3. 근무이력 새로 생성
        entity.T_History = LeaderWorkInfo.Histories.Select(historyItem => new T_History
        {
          LeaderNo = LeaderWorkInfo.LeaderNo,
          StartDT = historyItem.StartDT,
          EndDT = historyItem.EndDT,
          SchoolName = historyItem.SchoolName,
          SportsNo = historyItem.SportsNo
        }).ToList();

        // 4. 자격사항 새로 생성
        entity.T_Certificate = LeaderWorkInfo.Certificates.Select(certificateItem => new T_Certificate
        {
          LeaderNo = LeaderWorkInfo.LeaderNo,
          CertificateName = certificateItem.CertificateName,
          CertificateNo = certificateItem.CertificateNo,
          CertificateDT = certificateItem.CertificateDT,
          Organization = certificateItem.Organization
        }).ToList();

        entity.T_LeaderImage = new T_LeaderImage
        {
          LeaderNo = LeaderWorkInfo.LeaderNo,
          LeaderImage = LeaderWorkInfo.LeaderImage
        };

        _dBContext.Update(entity);
        _dBContext.SaveChanges();

        return true;
      }

      return false;
    }

    // 3. 지도자 삭제
    public void Delete(string[] leaderNos)
    {
      //1.넘어온 값이 실제로 db에 있는지 체크

      //string[]에 있는 값과 LeaderNo가 비교가 됨 그래서 포함된 것만 return
      //1,2,3,4,5 -> db에 실제로 있는 것만 리턴됨 1,2,3
      var entity = _dBContext.T_LeaderWorkInfo
          .Include(lw => lw.T_History)
          .Include(lw => lw.T_Certificate)
          .Include(lw => lw.T_LeaderImage)
          .Where(x => leaderNos.Contains(x.LeaderNo))
          .ToList();

      //1-2.삭제 할 데이터가 있는지 확인
      if (entity.Any())
      {
        //2.있는 데이터만 삭제
        foreach (var leader in entity)
        {
          _dBContext.T_History.RemoveRange(leader.T_History);
          _dBContext.T_Certificate.RemoveRange(leader.T_Certificate);
          _dBContext.T_LeaderImage.Remove(leader.T_LeaderImage);
          _dBContext.SaveChanges();
          _dBContext.T_LeaderWorkInfo.RemoveRange(leader);
          _dBContext.SaveChanges();
        }
      }
    }
    public IEnumerable<T_Sport> GetSportList()
    {
      return _dBContext.T_Sport.ToList();
    }

    public IEnumerable<T_Leader> GetLeaderList()
    {
      return _dBContext.T_Leader.ToList();
    }

    public IEnumerable<T_LeaderWorkInfo> GetAllList()
    {
      return _dBContext.T_LeaderWorkInfo
          .Include(r => r.T_LeaderImage)
          .Include(r => r.T_History)
                                          .ThenInclude(r => r.T_Sport)
          .Include(r => r.T_Certificate)
          .Include(r => r.T_School)
          .Include(r => r.T_Sport)
          .ToList();
    }

    public IEnumerable<T_LeaderWorkInfo> GetAllList(string LeaderNo)
    {
      return _dBContext.T_LeaderWorkInfo
          .Include(r => r.T_History)
              .ThenInclude(r => r.T_Sport)
          .Include(r => r.T_Certificate)
          .Include(r => r.T_Sport)
          .Include(r => r.T_School)
          .Include(r => r.T_LeaderImage)
          .ToList();
    }

    public IEnumerable<T_School> GetSchoolList()
    {
      return _dBContext.T_School.ToList();
    }


  }
}


