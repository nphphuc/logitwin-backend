namespace The_Hirelo.Models
{
    public class CandidateProfile
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string? Seniority { get; set; }

        public User User { get; set; } = null!;

        public ICollection<InterviewSession> InterviewSessions { get; set; } = new List<InterviewSession>();
    }
}
