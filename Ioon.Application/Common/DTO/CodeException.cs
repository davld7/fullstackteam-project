namespace Ioon.Application.Common.DTO
{
    public class CodeException
    {
        public string Message { get; set; } = string.Empty;
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public string StackTrace { get; set; } = string.Empty;
        public string InnerException { get; set; } = string.Empty;
        public string ExceptionType { get; set; } = string.Empty;
    }
}
