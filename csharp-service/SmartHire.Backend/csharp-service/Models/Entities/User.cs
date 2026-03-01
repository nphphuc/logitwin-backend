using System;
using System.Collections.Generic;

namespace csharp_service.Models.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string CognitoSub { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }
}
