namespace The_Hirelo.Models
{
    public class Notification
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string? Type { get; set; }

        public string? Message { get; set; }

        public DateTime? CreatedAt { get; set; }

        public User User { get; set; } = null!;
    }
}
