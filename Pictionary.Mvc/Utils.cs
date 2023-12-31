using System.Security.Claims;

namespace Pictionary.Mvc;

public static class Utils
{
    public static string GetEmail(this HttpContext context)
    {
        return context.User.FindFirstValue(ClaimTypes.Email)
            ?? throw new Exception("Context not Found");
    }

    public static string GetId(this HttpContext context)
    {
        return context.User.FindFirstValue(ClaimTypes.Sid)
            ?? throw new Exception("Context not Found");
    }

    public static string GetRole(this HttpContext context)
    {
        return context.User.FindFirstValue(ClaimTypes.Role)
            ?? throw new Exception("Context not Found");
    }
}