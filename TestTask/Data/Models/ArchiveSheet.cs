namespace TestTask.Data.Models;

/// <summary>
/// Представляет лист с месяцем архива
/// </summary>
public class ArchiveSheet
{
    public int Id { get; set; }
    public Month Month { get; set; }
    public int Year { get; set; }
    public virtual WeatherArchiveFile? WeatherArchiveFile { get; set; }
    public virtual ICollection<WeatherArchive>? SheetWeatherArchives { get; set; }
}
