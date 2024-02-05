using SportLeader.Controllers.Client.Request;
using SportLeader.Models;

namespace SportLeader.Repository
{
    public interface ILeaderRepository
    {
        bool Create(RegisterSportsLeaderRequest LeaderWorkInfo);
        void Delete(string[] leaderNos);
        IEnumerable<T_LeaderWorkInfo> GetAllList();
        IEnumerable<T_LeaderWorkInfo> GetAllList(string LeaderNo);
        IEnumerable<T_Leader> GetLeaderList();
        IEnumerable<T_School> GetSchoolList();
        IEnumerable<T_Sport> GetSportList();
        bool Modify(RegisterSportsLeaderRequest LeaderWorkInfo);
    }
}