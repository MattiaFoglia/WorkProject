namespace GameProject;
public record class Launcher
    (
        string Name,
        string Description,
        Uri? Link
    )
{
    public static Launcher Steam { get; }
    public static Launcher Nsw { get; }
    public static Launcher EpicGames { get; }
    public static Launcher UbisoftConnect { get; }
    public static Launcher PSX { get; }
    public static Launcher XboxLauncher { get; }
    public static Launcher EaConnect { get; }
    public static Launcher AmazonGames { get; }

    //This works only in signgle thread
    private static readonly Dictionary<string, Launcher> cache;

    static Launcher()
    {
        Steam = new("Steam", "nnn", new("https://store.steampowered.com/"));
        Nsw = new("Nintendo Switch", "nnn", new("https://www.nintendo.com/it-it/Nintendo-eShop/Nintendo-eShop-1806894.html"));
        EpicGames = new("Epic Games", "nnn", new("https://www.epicgames.com/site/it/home"));
        UbisoftConnect = new("Ubisoft Connect", "nnn", new("https://www.ubisoft.com/en-us/ubisoft-connect"));
        PSX = new("Playsation", "nnn", new("https://www.playstation.com/it-it/playstation-network/"));
        XboxLauncher = new("Xbox Launcher", "nnn", new("https://apps.microsoft.com/games?hl=en-us&gl=US"));
        EaConnect = new("EaC onnect", "nnn", new("https://www.ea.com/it-it"));
        AmazonGames = new("Amazon Games", "nnn", new("https://www.amazon.it/?tag=wwwbingcom07-21&ref=pd_sl_7qhc95m3oa_e&adgrpid=1233652364972904&hvadid=77103437818587&hvnetw=o&hvqmt=e&hvbmt=be&hvdev=c&hvlocint=&hvlocphy=1844&hvtargid=kwd-77103517078644:loc-93&hydadcr=10841_1834695&msclkid=5952c7890d411186e3e7a10fa785f408"));
        cache = new Dictionary<string, Launcher>();
        cache.AddKnownValuesToCache(s => s.Name);

    }

    public static Launcher GetOrCreateLauncher(string name, string? description, Uri? link) =>
     cache.GetOrCreateValue(name, () => new Launcher(name, description!, link));

    public static IEnumerable<Launcher> GetAllStores() =>
        cache.Values;


}