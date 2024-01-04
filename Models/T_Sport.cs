using System.ComponentModel.DataAnnotations;

namespace SportLeader.Models
{
    public class T_Sport
    {
        [Key]
        public string SportsNo { get; set; }
        
        public string SportsName { get; set; }
    }
}
