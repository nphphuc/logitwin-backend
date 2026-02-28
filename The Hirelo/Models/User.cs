namespace The_Hirelo.Models;
using System;
using System.Collections.Generic;

public class User
{
    public Guid Id { get; set; }

    public string? CognitoSub { get; set; }

    public string Email { get; set; } = null!;

    public string Role { get; set; } = "Candidate";

    public DateTime CreatedAt { get; set; }

    // Navigation
    public RecruiterProfile? RecruiterProfile { get; set; }
    public CandidateProfile? CandidateProfile { get; set; }
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
