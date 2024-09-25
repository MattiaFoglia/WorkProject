namespace GameProject;
public record class GameTransaction
(
    Game Game,
    Platform Platform,
    Store Store,
    MediaFormat Format,
    Launcher Launcher,
    DateOnly Date,
    decimal Price
);


