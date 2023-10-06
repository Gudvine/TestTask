namespace TestTask.Data.Models
{
    /// <summary>
    /// Представляет файл в системе и в БД
    /// </summary>
    public class FileInstance
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public byte[]? Content { get; set; }
        public long Size { get; set; }
    }
}
