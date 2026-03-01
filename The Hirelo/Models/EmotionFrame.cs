namespace The_Hirelo.Models
{
    public class EmotionFrame
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }

        public string? Emotion { get; set; }

        public double Confidence { get; set; }

        public DateTime? CapturedAt { get; set; }

        public InterviewSession Session { get; set; } = null!;
    }
}
