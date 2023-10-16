namespace TestTask.Data.Models;

/// <summary>
/// Представляет архив погодных условий
/// </summary>
public class WeatherArchive
{
    public int Id { get; set; }
    public virtual ArchiveSheet? ArchiveSheet { get; set; }
    public DateTime Date { get; set; }
    public string MoscowTime { get; set; } = string.Empty;
    public double T { get; set; }
    public double AirHumidity { get; set; }
    public double Td { get; set; }
    public double Pressure { get; set; }
    public string AirDirection { get; set; } = string.Empty;
    public double AirSpeed { get; set; }
    public double Cloudness { get; set; }
    public double H { get; set; }
    public double VV { get; set; }
    public string Phenomena { get; set; } = string.Empty;
}
