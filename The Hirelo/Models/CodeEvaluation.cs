namespace The_Hirelo.Models
{
    public class CodeEvaluation
    {
        public Guid Id { get; set; }

        public Guid SubmissionId { get; set; }

        public bool Passed { get; set; }

        public int? RuntimeMs { get; set; }

        public int? MemoryKb { get; set; }

        public CodeSubmission Submission { get; set; } = null!;
    }
}
