namespace The_Hirelo.Models
{
    public class CodeSubmission
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }

        public string? Language { get; set; }

        public string? SourceCode { get; set; }

        public DateTime? SubmittedAt { get; set; }

        public InterviewSession Session { get; set; } = null!;

        public CodeEvaluation? CodeEvaluation { get; set; }
    }
}
