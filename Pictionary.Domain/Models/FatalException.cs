using System.Diagnostics.CodeAnalysis;

namespace Pictionary.Domain.Models;

[UnconditionalSuppressMessage("CustomExceptions", "RCS1194:ImplementExceptionConstructors.",
    Justification = "Only one Constructor")]
public class FatalException : Exception
{

    public FatalException(string title, string details, string? type = null, Exception? innerException = null)
        : base($"{title}: {details}", innerException)
    {
        Title = title;
        Details = details;
        Type = type;
    }
    public static readonly int Status = 500;

    public string Title { get; }

    public string Details { get; }

    public string? Type { get; }
}