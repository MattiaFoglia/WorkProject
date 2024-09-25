namespace GameProject;
public record class Game
(
    string Name,
    string? Description
)
{
    private Dictionary<string, Game> _dictionaryDlc = new Dictionary<string, Game>(StringComparer.InvariantCultureIgnoreCase);
    public Game? MainGame { get; private set; }
    public Game AddDLC(string name, string? description)
    {
        if (_dictionaryDlc.ContainsKey(name))
            throw new DlcAlreadyAddedException($"Dlc {name} already added in {name}");
        if (IsDLc)
            throw new InvalidOperationException("You can't add a dlc to another dlc");

        Game dlc = new Game(name, description);
        dlc.MainGame = this;

        _dictionaryDlc.Add(dlc.Name, dlc);
        return dlc;
    }
    public bool IsDLc =>
        MainGame is not null;

    public IEnumerable<Game> GetAllDlc() =>
        _dictionaryDlc.Values;


}
