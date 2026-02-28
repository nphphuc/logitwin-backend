namespace The_Hirelo.Models
{
    public class InterviewSession
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }
        public Guid CandidateId { get; set; }

        public string? Status { get; set; }

        public DateTime? StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public Job Job { get; set; } = null!;
        public CandidateProfile Candidate { get; set; } = null!;

        public ICollection<InterviewQuestion> Questions { get; set; } = new List<InterviewQuestion>();
        public ICollection<CodeSubmission> CodeSubmissions { get; set; } = new List<CodeSubmission>();
        public ICollection<EmotionFrame> EmotionFrames { get; set; } = new List<EmotionFrame>();
        public Scorecard? Scorecard { get; set; }
        public InterviewReport? InterviewReport { get; set; }
    }
}
