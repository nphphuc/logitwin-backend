namespace The_Hirelo.Models
{
    public class Scorecard
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }

        public double TechnicalScore { get; set; }
        public double CommunicationScore { get; set; }
        public double ProblemSolvingScore { get; set; }
        public double OverallScore { get; set; }

        public InterviewSession Session { get; set; } = null!;
    }
}
