using System.Security.Claims;

namespace The_Hirelo.Extensions;

public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Get Cognito Sub (user ID) from claims
    /// </summary>
    public static string? GetCognitoSub(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value 
            ?? user.FindFirst("sub")?.Value;
    }

    /// <summary>
    /// Get email from claims
    /// </summary>
    public static string? GetEmail(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Email)?.Value 
            ?? user.FindFirst("email")?.Value;
    }

    /// <summary>
    /// Get username from claims
    /// </summary>
    public static string? GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirst("cognito:username")?.Value 
            ?? user.FindFirst(ClaimTypes.Name)?.Value;
    }

    /// <summary>
    /// Get Cognito groups from claims
    /// </summary>
    public static IEnumerable<string> GetCognitoGroups(this ClaimsPrincipal user)
    {
        return user.FindAll("cognito:groups").Select(c => c.Value);
    }
}
