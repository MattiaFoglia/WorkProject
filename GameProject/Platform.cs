namespace GameProject;


public record class Platform
(
    string Name,
    string Description
)
{
    public static Platform PC { get; } 
    public static Platform NSW { get; } 
    public static Platform PS5 { get; } 
    public static Platform PS4 { get; } 
    public static Platform Xbox { get; } 
    public static Platform Steamdeck { get; }

    //This works only in single thread
    private static readonly Dictionary<string, Platform> cache;

    static Platform()
    {
        PC = new("PC", "nnn");
        NSW = new("Steam", "nnn");
        PS5 = new("Playstation 5", "nnn");
        PS4 = new("Playstation 4", "nnn");
        Xbox = new("Xbox", "nnn");
        Steamdeck = new("Steamdeck", "nnn");
        cache = new Dictionary<string, Platform>();
        cache.AddKnownValuesToCache(s => s.Name);
    }
    public static Platform GetOrCreatePlatform(string name, string? description) =>
       cache.GetOrCreateValue(name, () => new Platform(name, description!));

    public static IEnumerable<Platform> GetAllStores() =>
        cache.Values;
}





