using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLeader.Models
{
    public class T_Certificate
    {
        [Key]
        public string LeaderNo { get; set; }
        public int CertificateSequence { get; set; }
        public string CertificateName { get; set; }
        public string CertificateNo { get; set; }
        public DateTime? CertificateDT { get; set; }
        public string Organization { get; set; }
        public T_LeaderWorkInfo T_LeaderWorkInfo { get; set; }

    }
}
