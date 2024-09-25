namespace GameProject;

public record class Store
(
    string Name,
    string Description,
    Uri? Link
)
{

    public static Store Steam { get; }
    public static Store Eshop { get; }
    public static Store EpicGames { get; }
    public static Store UbisoftStore { get; }
    public static Store PSN { get; }
    public static Store MicrosoftStore { get; }
    public static Store PlayStore { get; }
    public static Store Ea { get; }
    public static Store Amazon { get; }


    //This works only in signgle thread
    private static readonly Dictionary<string, Store> cache;
    static Store()
    {

        Steam = new("Steam", "nnn", new("https://store.steampowered.com/"));
        Eshop = new("Eshop", "nnn", new("https://www.nintendo.com/it-it/Nintendo-eShop/Nintendo-eShop-1806894.html"));
        EpicGames = new("Epic Games", "nnn", new("https://www.epicgames.com/site/it/home"));
        UbisoftStore = new("Ubisoft Store", "nnn", new("https://www.ubisoft.com/en-us/ubisoft-connect"));
        PSN = new("PSN", "nnn", new("https://www.playstation.com/it-it/playstation-network/"));
        MicrosoftStore = new("Microsoft Store", "nnn", new("https://apps.microsoft.com/games?hl=en-us&gl=US"));
        PlayStore = new("Play Store", "nnn", new("https://play.google.com/store/games?device=windows&pli=1"));
        Ea = new("Ea", "nnn", new("https://www.ea.com/it-it"));
        Amazon = new("Amazon", "nnn", new("https://www.amazon.it/?tag=wwwbingcom07-21&ref=pd_sl_7qhc95m3oa_e&adgrpid=1233652364972904&hvadid=77103437818587&hvnetw=o&hvqmt=e&hvbmt=be&hvdev=c&hvlocint=&hvlocphy=1844&hvtargid=kwd-77103517078644:loc-93&hydadcr=10841_1834695&msclkid=5952c7890d411186e3e7a10fa785f408"));
        cache = new Dictionary<string, Store>();
        cache.AddKnownValuesToCache(s => s.Name);
    }

    public static Store GetOrCreateStore(string name, string? description, Uri? link) =>
        cache.GetOrCreateValue(name, () => new Store(name, description!, link));

    public static IEnumerable<Store> GetAllStores() =>
        cache.Values;

}

