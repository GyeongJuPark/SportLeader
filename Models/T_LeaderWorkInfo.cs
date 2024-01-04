using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLeader.Models
{
    public class T_LeaderWorkInfo
    {
        [Key]
        public int LeaderSequence { get; set; }
        [ForeignKey("T_Leader")]
        public string LeaderNo { get; set; }

        [ForeignKey("T_School")]
        public string SchoolNo { get; set; }

        [ForeignKey("T_Sport")]
        public string SportsNo { get; set; }
        public string LeaderName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime StartDT { get; set; }
        public string TelNo { get; set; }
        public DateTime EmpDT { get; set; }
        public T_Leader T_Leader { get; set; }
        public T_Sport T_Sport { get; set; }
        public T_School T_School { get; set; }
        public ICollection<T_History> T_History { get; set; }
        public ICollection<T_Certificate> T_Certificate { get; set; }
    }
}
