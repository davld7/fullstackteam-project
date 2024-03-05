using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data.Common;

namespace Ioon.Application.Loggin
{
    public class IoonLoggerProvider : ILoggerProvider
    {
        public readonly IoonLoggerOptions _options;
        public readonly DbConnection _connection;

        public IoonLoggerProvider(IOptions<IoonLoggerOptions> optionsLogger)
        {
            _options = optionsLogger.Value;
        
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new IoonLogger(this);
        }

        public void Dispose()
        {       
        }
    }
}
