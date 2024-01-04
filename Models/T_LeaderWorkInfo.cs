using System.ComponentModel.DataAnnotations;

namespace SportLeader.Models
{
    public class T_LeaderWorkInfo
    {
        public string LeaderNo { get; set; }

        public string SchoolNo { get; set; }

        public string SportsNo { get; set; }


        public string LeaderName { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        public string TelNo { get; set; }

        public DateTime EmpDT { get; set; }

        public virtual T_Leader T_Leader { get; set; }

        public virtual T_Sport T_Sport { get; set; }

        public virtual T_School T_School { get; set; }

        public virtual IEnumerable<T_History>? T_History { get; set; }

        public virtual IEnumerable<T_Certificate>? T_Certificate { get; set; }
        public virtual T_LeaderImage T_LeaderImage { get; set; }
    }
}
