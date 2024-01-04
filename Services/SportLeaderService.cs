using SportLeader.Data;
using SportLeader.Models;

namespace SportLeader.Services
{

    public interface ISportLeaderService
    {
        IEnumerable<T_Sport> GetSportList();
        IEnumerable<T_Leader> GetLeaderList();
        IEnumerable<T_LeaderWorkInfo> GetAllList();

    }
    public class SportLeaderService : ISportLeaderService
    {
        private readonly SpotrsLeaderDBContext _dBContext;

        public SportLeaderService(SpotrsLeaderDBContext dBContext)
        {
            _dBContext = dBContext;
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
            return _dBContext.T_LeaderWorkInfo.ToList();
        }
    }
}
