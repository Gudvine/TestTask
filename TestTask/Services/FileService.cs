using Blazor.DownloadFileFast.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using TestTask.Data.Models;

namespace TestTask.Services;

/// <summary>
/// Содержит набор методов для создания и загрузки файлов
/// </summary>
public class FileService
{
    private ISnackbar _snackbar;
    private IBlazorDownloadFileService _downloadService;
    private const int MaxAllowedSize = int.MaxValue;

    public FileService(ISnackbar snackbar, IBlazorDownloadFileService downloadService) 
    {
        _snackbar = snackbar;
        _downloadService = downloadService;
    }

    /// <summary>
    /// Формирует объект обертки файла FileInstance для последующей его загрузки в БД и получения иего из БД
    /// </summary>
    /// <param name="file">IBrowserFile представление загруженног файла</param>
    /// <param name="fileName">Имя файла</param>
    /// <returns></returns>
    public async Task<FileInstance> GetFileInstanceFromBrowserFile(IBrowserFile file, string fileName)
    {
        return new FileInstance()
        {
            Content = await GetByteArrayFromBrowserFile(file),
            FileName = fileName,
            Size = file.Size,
        };
    }

    // Возвращает от заданного IBrowserFile представления загруженного файла
    // его представление в виде массива байтов
    async Task<byte[]> GetByteArrayFromBrowserFile(IBrowserFile file)
    {
        var buffer = new byte[file.Size];

        try
        {
            using var rs = file.OpenReadStream(MaxAllowedSize);
            await rs.ReadAsync(buffer);
        }
        catch (Exception ex)
        {
            _snackbar.Add("Произошла ошибка! Первышен максимальный допустимый размер файла", Severity.Error);
        }

        return buffer;
    }

    /// <summary>
    /// Позволяет скачать заданный файл с сервера на основе его представления в виде массива байтов
    /// </summary>
    /// <param name="file">Объект обертки файла</param>
    /// <returns></returns>
    public async Task DownloadFile(FileInstance file)
    {
        if (file?.Content is null)
            return;

        await _downloadService.DownloadFileAsync(file.FileName, file.Content);
    }
}
