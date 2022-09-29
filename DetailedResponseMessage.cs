using System.Data;

namespace Sprocket
{
    public class DetailedResponseMessage<T>
    {
        public Exception? Exception { get; set; }
        public Dictionary<string, object>? Parameters { get; set; }
        public DataTable? DataTable { get; set; }
        public string? StoredProcedure { get; set; }
        public T? Data { get; set; }
        public ISqlResponse? Response { get; set; }
        public bool IsSuccess() => Response is { IsSuccess: true };
    }
}
