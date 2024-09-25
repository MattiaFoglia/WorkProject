namespace GameProject;

public class DlcAlreadyAddedException : Exception
{
    public DlcAlreadyAddedException(string? message) : base(message)
    {
    }

    public DlcAlreadyAddedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
