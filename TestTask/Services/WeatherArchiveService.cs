using Microsoft.AspNetCore.Components.Forms;
using NPOI.SS.UserModel;
using NPOI.Util;
using NPOI.XSSF.UserModel;
using TestTask.Data.Models;

namespace TestTask.Services;

/// <summary>
/// Содержит набор методов для работы с Excel документами
/// </summary>
public class WeatherArchiveService
{
    private IWebHostEnvironment _environment;

    private const int MaxAllowedSize = int.MaxValue;

    public WeatherArchiveService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    /// <summary>
    /// Парсит файл Excel с архивом погодных условий и возвращает объектную модель содержимого
    /// </summary>
    /// <returns></returns>
    public async Task<WeatherArchiveFile?> ParseAndGetWeatherArchiveFile(IBrowserFile file, string fileName)
    {
        IWorkbook workbook;

        try
        {
            await SaveFileInServerStorage(file);
            using var fs = new FileStream(Path.Combine(_environment.WebRootPath, "files", file.Name), FileMode.Open, FileAccess.Read);

            workbook = new XSSFWorkbook(fs);

            WeatherArchiveFile archiveFile = ParseWorkbook(workbook, Path.GetFileNameWithoutExtension(file.Name));

            return archiveFile;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ошибка {ex.Message}");
        }

        return null;
    }

    // Сохраняет на серверном диске файл для дальнейшего чтения в FileStream (Нужен для чтения с библиотеки NPOI)
    async Task SaveFileInServerStorage(IBrowserFile file)
    {
        try
        {
            using var rs = file.OpenReadStream(MaxAllowedSize);

            var filePath = Path.Combine(_environment.WebRootPath, "files", file.Name);

            CheckAndDeleteFileInServerStorage(filePath);

            using FileStream saveStream = new(filePath, FileMode.Create, FileAccess.Write);

            await rs.CopyToAsync(saveStream);

            IWorkbook workbook = new XSSFWorkbook();
            workbook.Write(saveStream);
        }
        catch (Exception)
        {
        }
    }

    // Удаляет файл из серверного хранилища и заменяет его одноименным
    void CheckAndDeleteFileInServerStorage(string filePath)
    {
        if (!File.Exists(filePath))
            return;

        File.Delete(filePath);
    }

    // парсит структуру Excel документа с архивом погодных условий
    WeatherArchiveFile ParseWorkbook(IWorkbook workbook, string archiveName)
    {
        var numberOfSheets = workbook.NumberOfSheets;
        WeatherArchiveFile archiveFile = new()
        {
            FileName = archiveName,
            WeatherArchivesFileSheets = new List<ArchiveSheet>(),
        };

        for (int i = 0; i < numberOfSheets; i++)
        {
            ISheet sheet = workbook.GetSheetAt(i);

            archiveFile.WeatherArchivesFileSheets.Add(GetWeatherArchiveSheetFromSheet(sheet));
        }

        return archiveFile;
    }

    // Возвращает лист месяца из архива
    ArchiveSheet GetWeatherArchiveSheetFromSheet(ISheet sheet)
    {
        var arcSheet = new ArchiveSheet()
        {
            Month = GetMonth(sheet.SheetName.Split(" ")[0]),
            Year = int.Parse(sheet.SheetName.Split(" ")[1]),
            SheetWeatherArchives = new List<WeatherArchive>(),
        };

        for (int i = 7; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            arcSheet.SheetWeatherArchives.Add(GetWeatherArchiveFromRow(row));
        }

        return arcSheet;
    }

    // Получает информацию о строке из архива
    WeatherArchive GetWeatherArchiveFromRow(IRow row)
    {
        WeatherArchive archive = new();

        try
        {
            archive.Date = DateTime.Parse(row.GetCell(0).StringCellValue).ToUniversalTime();
            archive.MoscowTime = row.GetCell(1).CellType == CellType.String ? row.GetCell(1).StringCellValue : row.GetCell(1).NumericCellValue.ToString();
            archive.T = row.GetCell(2).CellType == CellType.String ? 0 : row.GetCell(2).NumericCellValue;
            archive.AirHumidity = row.GetCell(3).CellType == CellType.String ? 0 : row.GetCell(4).NumericCellValue;
            archive.Td = row.GetCell(4).CellType == CellType.String ? 0 : row.GetCell(4).NumericCellValue;
            archive.Pressure = row.GetCell(5).CellType == CellType.String ? 0 : row.GetCell(5).NumericCellValue;
            archive.AirDirection = row.GetCell(6).StringCellValue;
            archive.AirSpeed = row.GetCell(7).CellType == CellType.String ? 0 : row.GetCell(7).NumericCellValue;
            archive.Cloudness = row.GetCell(8).CellType == CellType.String ? 0 : row.GetCell(8).NumericCellValue;
            archive.H = row.GetCell(9).CellType == CellType.String ? 0 : row.GetCell(9).NumericCellValue;
            archive.VV = row.GetCell(10).CellType == CellType.String ? 0 : row.GetCell(10).NumericCellValue;
            archive.Phenomena = row.GetCell(11) is not null ? row.GetCell(11).StringCellValue : " ";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ошибка в {row.RowNum}, {ex.Message}");
        }

        return archive;
    }

    // Возвращает месяц, относительно названия листа в архиве
    Month GetMonth(string month)
    {
        switch (month)
        {
            case "Январь":
                return Month.January;
            case "Февраль":
                return Month.February;
            case "Март":
                return Month.March;
            case "Апрель":
                return Month.April;
            case "Май":
                return Month.May;
            case "Июнь":
                return Month.June;
            case "Июль":
                return Month.July;
            case "Август":
                return Month.August;
            case "Сентябрь":
                return Month.September;
            case "Октябрь":
                return Month.October;
            case "Ноябрь":
                return Month.November;
            case "Декабрь":
                return Month.December;
            default:
                return Month.January;
        }
    }
}