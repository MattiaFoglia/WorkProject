using System.Collections.Generic;

namespace GameProject;

public static class Extensions
{
    internal static void AddKnownValuesToCache<T>(this Dictionary<string, T> cache, Func<T, string> keyExtractor)
    {
        var knownValue =
            typeof(T)
            .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .Select(p => p.GetValue(null))
            .Cast<T>();

        foreach (var item in knownValue)
        {
            cache.Add(keyExtractor(item), item);
        }
    }

    public static T GetOrCreateValue<T>(this Dictionary<string, T> cache, string name, Func<T> instantietor)
    {
        if (cache.TryGetValue(name, out T? value))
            return value;
        value = instantietor();
        cache.Add(name, value);
        return value;
    }
    public static T[][] SplitLines<T>(this IEnumerable<T> lines, Func<T, bool> isSeparatorFX, Func<T, bool>? isEmptyFX = null)
    {
        List<T[]> result = new List<T[]>();
        List<T> part = new List<T>();
        foreach (var line in lines)
        {
            if (isSeparatorFX(line))
            {
                if (part.Any())
                {
                    result.Add(part.ToArray());
                    part = new List<T>();
                }
            }
            else if (!(isEmptyFX?.Invoke(line) ?? false))
                part.Add(line);
        }
        if (part.Any())
        {
            result.Add(part.ToArray());
        }
        return result.ToArray();

    }
    public static bool HasText(this string s) =>
        (s?.Trim().Length ?? 0) > 0;

    public static GameImportDto ToGameImportDto(this string[] keyValues)
    {
        #region Private Functions
        bool isValue(string s, string value) => (string.Equals(s, value, StringComparison.InvariantCultureIgnoreCase));

        bool isTitle(string s) => isValue(s, "Title");
        bool isMainGame(string s) => isValue(s, "Main Title");
        bool isDescription(string s) => isValue(s, "Description");
        bool isPlatform(string s) => isValue(s, "Platform");
        bool isPlatformDescription(string s) => isValue(s, "Platform Description");
        bool isStore(string s) => isValue(s, "Store");
        bool isStoreDescription(string s) => isValue(s, "Store Description");
        bool isStoreLink(string s) => isValue(s, "Store Link");
        bool isMediaFormat(string s) => isValue(s, "Media Format");
        bool isLauncher(string s) => isValue(s, "Launcher");
        bool isLauncherDescription(string s) => isValue(s, "Launcher Description");
        bool isLauncherLink(string s) => isValue(s, "Launcher Link");
        bool isAquiredDate(string s) => isValue(s, "Aquired date");
        bool isPrice(string s) => isValue(s, "Price");

        void checkGameDto(GameImportDto gameDto)
        {
            if (!(gameDto.Title.HasText()))
                throw new GameImportException("Invalid game title");

            if (!(gameDto.Platform.HasText()))
                throw new GameImportException("Invalid game platform");
            if (!(gameDto.Store.HasText()))
                throw new GameImportException("Invalid store");
            if (!(gameDto.MediaFormat.HasText()))
                throw new GameImportException("Invalid media format");
            if (!(gameDto.Launcher.HasText()))
                throw new GameImportException("Invalid game launcher");
            if ((gameDto.AquiredDate.Year < 1971))
                throw new GameImportException("Invalid aquisition date");
            if ((gameDto.Price < 0))
                throw new GameImportException("Invalid game price");



}
        #endregion

        GameImportDto gameDto = new GameImportDto();
        foreach (var line in keyValues)
        {

            int pos = line.IndexOf(":");
            if (pos < 0)

                throw new GameImportException($"the line{line} does not contain :");

            string key = line.Substring(0, pos).Trim();
            string value = line.Substring(pos + 1).Trim();
            if (isTitle(key))
                gameDto.Title = value;
            else if (isMainGame(key))
                gameDto.MainGame = value;
            else if (isDescription(key))
                gameDto.Description = value;
            else if (isPlatform(key))
                gameDto.Platform = value;
            else if (isPlatformDescription(key))
                gameDto.PlatformDescription = value;
            else if (isStore(key))
                gameDto.Store = value;
            else if (isStoreDescription(key))
                gameDto.StoreDescription = value;
            else if (isStoreLink(key))
                gameDto.StoreLink = value == "" ? null : new Uri(value);
            else if (isMediaFormat(key))
                gameDto.MediaFormat = value;
            else if (isLauncher(key))
                gameDto.Launcher = value;
            else if (isLauncherDescription(key))
                gameDto.LauncherDescription = value;
            else if (isLauncherLink(key))
                gameDto.LauncherLink = value == "" ? null : new Uri(value);
            else if (isAquiredDate(key))
                gameDto.AquiredDate = DateOnly.ParseExact(value, "yyyy-MM-dd");
            else if (isPrice(key))
                gameDto.Price = decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
            else
                throw new GameImportException($"invalid {key} is not valid");
        }
        checkGameDto(gameDto);
        return gameDto;
    }
}
