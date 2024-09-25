namespace GameProject;

public class MainGameNotFoundException : Exception
{
    public MainGameNotFoundException(string? message) : base(message)
    {
    }

    public MainGameNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}



