using System.Net;
using System.Text.Json.Serialization;

namespace Ioon.Application.Common.DTO
{
    [Serializable]
    public partial class ApplicationResponse : IDisposable
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public bool? IsSuccessful { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
        public object? Data { get; set; }

        public void Dispose() { }
       

    }
}
