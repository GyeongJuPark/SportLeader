using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLeader.Models
{
    public class T_History
    {
        [ForeignKey("T_LeaderWorkInfo")]
        public int LeaderSequence { get; set; }

        [Key]
        public int HistorySequence { get; set; }
        public DateTime StartDT { get; set; }
        public DateTime EndDT { get; set; }
        public string SchoolName { get; set; }

        public string SportsNo { get; set; }
        public T_LeaderWorkInfo T_LeaderWorkInfo { get; set; }
        public T_Sport T_Sport { get; set; }
    }
}
