namespace GameProject;
public record class MediaFormat(
    string MediaF
)
{
    public static MediaFormat Digital { get; } = new("Digital");
    public static MediaFormat Physical { get; } = new("Physical");


    public static MediaFormat StringToMediaFormat(string s)
    {
        if (string.Equals(s, "Digital", StringComparison.OrdinalIgnoreCase))
            return Digital;
        if (string.Equals(s, "Physical", StringComparison.OrdinalIgnoreCase))
            return Physical;
        else
            throw new GameImportException("Media format not valid");

    }

}

