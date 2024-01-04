namespace SportLeader.DTO
{
    public class LeaderDTO
    {
        public string No { get; set; }
        public string Name { get; set; }
    }
    public class SportDTO
    {
        public string No { get; set; }
        public string Name { get; set; }
    }

    public class LeaderWorkInfoDTO
    {
        public int Seq { get; set; }
        public string LNO { get; set; }
        public string SCN { get; set; }
        public string SPN { get; set; }
        public string LNAME { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime StartDT { get; set; }
        public string TelNo { get; set; }
        public DateTime EndDT { get; set; }
    }
}
