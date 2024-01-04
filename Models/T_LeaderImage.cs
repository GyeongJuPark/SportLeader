using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLeader.Models
{
    public class T_LeaderImage
    {
        [ForeignKey("T_LeaderWorkInfo")]
        public int LeaderSequence { get; set; }
        public byte[]  LeaderImage { get; set;}
        public T_LeaderWorkInfo T_LeaderWorkInfo { get; set; }
    }
}
