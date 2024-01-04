using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLeader.Models
{
    public class T_Leader
    {
        [Key]
        public string LeaderNo { get; set; }

        public string LeaderName { get; set;}
    }
}

