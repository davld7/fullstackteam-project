namespace Ioon.Domain;

public partial class LogsServer
{
    public int LogId { get; set; }

    public string LogValues { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
}
