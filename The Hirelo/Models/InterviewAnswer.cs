namespace The_Hirelo.Models
{
    public class InterviewAnswer
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        public string? Transcript { get; set; }

        public string? AudioUrl { get; set; }

        public InterviewQuestion Question { get; set; } = null!;
    }
}
