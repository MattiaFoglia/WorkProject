namespace GameProject;

public class GameImportException : Exception
{
    public GameImportException(string? message) : base(message)
    {
    }

    public GameImportException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
