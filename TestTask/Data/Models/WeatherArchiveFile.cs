namespace TestTask.Data.Models;

/// <summary>
/// Представляет файл Excel с архивом погодных условий 
/// </summary>
public class WeatherArchiveFile
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public virtual ICollection<ArchiveSheet>? WeatherArchivesFileSheets { get; set; }
}
