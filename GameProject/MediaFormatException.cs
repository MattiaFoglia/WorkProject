namespace GameProject;

public class MediaFormatException : Exception
{
    public MediaFormatException(string? message) : base(message)
    {
    }

    public MediaFormatException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

