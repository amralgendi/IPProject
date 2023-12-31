using System.Diagnostics.CodeAnalysis;

namespace Pictionary.Domain.Models;

[UnconditionalSuppressMessage("CustomExceptions", "RCS1194:ImplementExceptionConstructors.",
    Justification = "Only one Constructor")]
public abstract class CustomException : Exception
{
    protected CustomException(string title, string details, int status, string? type = null, Exception? innerException = null)
        : base($"{title}: {details}", innerException)
    {
        Title = title;
        Details = details;
        Status = status;
        Type = type;
    }

    public string Title { get; }

    public string Details { get; }

    public int Status { get; }

    public string? Type { get; }
}