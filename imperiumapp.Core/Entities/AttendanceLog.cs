namespace imperiumapp.Core.Entities
{
    public class AttendanceLog
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public DateTime ScanTime { get; set; } = DateTime.Now;
    }
}