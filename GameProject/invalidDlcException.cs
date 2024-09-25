namespace GameProject;

public class invalidDlcException : Exception
{
    public invalidDlcException(string? message) : base(message)
    {
    }

    public invalidDlcException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}



