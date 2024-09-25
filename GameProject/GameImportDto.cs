namespace GameProject;

public record class GameImportDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Platform { get; set; } = null!;
    public string? PlatformDescription { get; set; }
    public string Store { get; set; } = null!;
    public string? StoreDescription { get; set; }
    public Uri? StoreLink { get; set; }
    public string MediaFormat { get; set; } = null!;
    public string Launcher { get; set; } = null!;
    public string? LauncherDescription { get; set; }
    public Uri? LauncherLink { get; set; }
    public DateOnly AquiredDate { get; set; }
    public decimal Price { get; set; }
    public string? MainGame { get; set; } = null;

};



