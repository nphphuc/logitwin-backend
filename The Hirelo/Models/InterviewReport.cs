namespace The_Hirelo.Models
{
    public class InterviewReport
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }

        public string? ReportJson { get; set; }
        public string? ReportHtml { get; set; }

        public InterviewSession Session { get; set; } = null!;
    }
}
