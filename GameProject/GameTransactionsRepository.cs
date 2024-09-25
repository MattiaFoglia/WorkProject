using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.Text;

namespace GameProject;
public record class GameTransactionsRepository
(
    List<GameTransaction> GameTransactionsList

)
{
    public void ImportdataFroExternalFile(string externalFile)
    {
        string[] lines = File.ReadAllLines(externalFile);
        ImportdataFroExternalFile(lines);
            
    }

    public void ImportdataFroExternalFile(string[] lines)
    {
       
        var gamesDto =
            lines
            .SplitLines
            (
                s => s.StartsWith("----------------------"),
                s => string.IsNullOrWhiteSpace(s) || s.Trim().StartsWith("#")
            )
            .Select(x => x.ToGameImportDto())
            .ToList();
        SameDlcWithDifferentGameTest(gamesDto);
        gamesDto
            .ForEach
            (
              gameDto =>
              {
                  Game game = ExtractGameFromDto(gameDto);
                  Platform platform = ExtractPlatformFromDto(gameDto);
                  Store store = ExtractStoreFromDto(gameDto);
                  MediaFormat mediaFormat = ExtractMediaFormatFromDto(gameDto);
                  Launcher launcher = ExtractLauncherFromDto(gameDto);
                  DateOnly date = ExtractDateFromDto(gameDto);
                  decimal price = ExtractpriceFromDto(gameDto);
                  GameTransaction gameT = BuildGameTransaction(game, platform, store, mediaFormat, launcher, date, price);
                  AddTransaction(gameT);
              }
            );
    }

    public void SameDlcWithDifferentGameTest(List<GameImportDto> gameImportDtoList)
    {
        var anomaliesList = 
            gameImportDtoList
            .GroupBy(x => x.Title)
            .Where(x => x.Count() > 1 && !x.All(y => string.IsNullOrEmpty(y.MainGame)))
            .Where(x => x.DistinctBy(y => y.MainGame).Count() > 1)
            .ToList();

        StringBuilder sB = new StringBuilder();
        foreach(var g in anomaliesList)
        {
            sB.AppendLine($"{g.Key} is present {g.Count()} times with these main titles : {string.Join(", ", g.Select(x => x.MainGame))}");
        }
        if (sB.Length != 0)
            throw new invalidDlcException("The same dlc has been assosiceted to two different games");




    }
    public GameTransaction BuildGameTransaction
        (
        Game gameTmp,
        Platform platformTmp,
        Store storeTmp,
        MediaFormat mediaFormatTmp,
        Launcher launcherTmp,
        DateOnly dateTmp,
        decimal priceTmp
        ) =>
            new GameTransaction(gameTmp, platformTmp, storeTmp, mediaFormatTmp, launcherTmp, dateTmp, priceTmp);

    private Game ExtractGameFromDto(GameImportDto gameDto)
    {

        if (string.IsNullOrEmpty(gameDto.MainGame))
        {
            Game game = new Game(gameDto.Title, gameDto.Description);
            return game;
        }

        Game? maingame = GameTransactionsList.FirstOrDefault(g => string.Equals(g.Game.Name, gameDto.MainGame, StringComparison.OrdinalIgnoreCase))?.Game;
        if (maingame is null)
            throw new MainGameNotFoundException("The main game title has not been insterted yet");
        Game? found = maingame.GetAllDlc().FirstOrDefault(g => string.Equals(g.Name, gameDto.MainGame, StringComparison.OrdinalIgnoreCase))?.MainGame;
        if (found is not null)
            return found;
        return maingame.AddDLC(gameDto.Title, gameDto.Description);
    }

    private Platform ExtractPlatformFromDto(GameImportDto gameDto) =>
           Platform.GetOrCreatePlatform(gameDto.Platform, gameDto.PlatformDescription);

    private Store ExtractStoreFromDto(GameImportDto gameDto) =>
          Store.GetOrCreateStore(gameDto.Store, gameDto.StoreDescription, gameDto.StoreLink);

    private MediaFormat ExtractMediaFormatFromDto(GameImportDto gameDto) =>
          MediaFormat.StringToMediaFormat(gameDto.MediaFormat);
    private Launcher ExtractLauncherFromDto(GameImportDto gameDto) =>
           Launcher.GetOrCreateLauncher(gameDto.Launcher, gameDto.LauncherDescription, gameDto.LauncherLink);

    private DateOnly ExtractDateFromDto(GameImportDto gameDto) =>
          gameDto.AquiredDate;

    private decimal ExtractpriceFromDto(GameImportDto gameDto) =>
         gameDto.Price;

    public void AddTransaction(GameTransaction gT)
    {
        GameTransactionsList.Add(gT);
    }




}



