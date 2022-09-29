namespace Sprocket
{
    public class SprocketClient : IDisposable
    {
        private readonly List<ISqlFilter> _filters;
        private readonly string _connectionString;
        private bool _isDisposed;

        public SprocketClient(string connection) :this(new List<ISqlFilter>(){new StandardSqlFilter()}, connection)
        {
           
        }
        public SprocketClient(List<ISqlFilter> filters, string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _filters = filters ?? new List<ISqlFilter>();
        }

        public ISqlRequest Request(string sproc) => new SqlRequest(_connectionString, sproc, _filters);

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
                return;
            if (isDisposing)
                _isDisposed = true;
        }
    }
}