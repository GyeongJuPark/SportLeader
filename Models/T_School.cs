using System.ComponentModel.DataAnnotations;

namespace SportLeader.Models
{
    public class T_School
    {
        [Key]
        public string SchoolNo { get; set; }
        public string SchoolName { get; set; }

    }
}
