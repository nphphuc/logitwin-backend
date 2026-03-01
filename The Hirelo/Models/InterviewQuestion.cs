namespace The_Hirelo.Models
{
    public class InterviewQuestion
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }

        public string? QuestionText { get; set; }

        public DateTime? CreatedAt { get; set; }

        public InterviewSession Session { get; set; } = null!;

        public ICollection<InterviewAnswer> Answers { get; set; } = new List<InterviewAnswer>();
    }
}
