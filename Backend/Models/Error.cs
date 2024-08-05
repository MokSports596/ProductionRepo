namespace MokSportsApp.Models
{
    public class Error
    {
        public int ErrorId { get; set; }
        public string Thread { get; set; } = string.Empty;
        public string Logger { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Exception { get; set; } = string.Empty;
        public string LogLevel { get; set; } = string.Empty;
        public DateTime LoggedAt { get; set; }
    }
}
