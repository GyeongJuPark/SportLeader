using SportLeader.DTO;
using SportLeader.Models;

namespace SportLeader.ViewModels
    {
        public class LeaderViewModel
        {
            public List<LeaderDTO> Leaders { get; set; }
            public IEnumerable<T_Sport> Sports { get; set; }
            public IEnumerable<T_School> Schools { get; set; }
        }
    }
