using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Sprocket
{
    public class SqlRequest : ISqlRequest, IDisposable
    {
        public bool IsSuccess { get; private set; }
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private readonly string _sprocName;
        private readonly IEnumerable<ISqlFilter> _filters;
        private bool _convertFromXml = false;
        public Exception? Exception { get; private set; }
        public DataTable? DataTable { get; set; }

        public SqlRequest(string connectionString, string sprocName, List<ISqlFilter> filters)
        {
            SqlRequest request = this;

            _sprocName = sprocName;
            _connection = new SqlConnection(connectionString);
            _command = new SqlCommand(_sprocName, _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _filters = filters;
        }

        public DetailedResponseMessage<T> As<T>()
        {
            DetailedResponseMessage<T> message = AsMessage<T>();
            if (message.DataTable?.Rows.Count == 0)
                return new DetailedResponseMessage<T>();

            string? expectedJsonData = message.DataTable?.Rows[0][0].ToString();
            if (string.IsNullOrEmpty(expectedJsonData))
            {
                return new();
            }
            if (_convertFromXml)
            {
                System.Xml.XmlDocument xmlXmlPassThrough = new System.Xml.XmlDocument();
                xmlXmlPassThrough.LoadXml(expectedJsonData);
                expectedJsonData = JsonConvert.SerializeXmlNode(xmlXmlPassThrough).Normalize();
            }

            

            DetailedResponseMessage<T> result = new DetailedResponseMessage<T>
            {
                Data = JsonConvert.DeserializeObject<T>(expectedJsonData),
                Response = this
            };
            return result;
        }

        public ISqlRequest ResponseIsXml(bool isXml = true)
        {
            _convertFromXml = isXml;
            return this;
        }


        public ISqlRequest AddParameter(string name, object value)
        {
            _command.Parameters.Add(new SqlParameter(name, value));
            return this;
        }

        public ISqlRequest SetTimeout(int seconds)
        {
            _command.CommandTimeout = seconds;
            return this;
        }
        

        private DetailedResponseMessage<T> AsMessage<T>()
        {
            DataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(_command);
            SqlRequest request = this;
            DetailedResponseMessage<T> responseMessage;
            //Execute each Filter Request action
            foreach (ISqlFilter filter in request._filters)
                filter.OnRequest(request, _command);

            try
            {
                dataAdapter.Fill(DataTable);
                IsSuccess = true;
            }
            catch (NullDataException nde)
            {
                Exception = nde;
                IsSuccess = false;
            }
            catch (Exception e)
            {
                Exception = e;
                IsSuccess = false;
            }
            finally
            {
                responseMessage = new DetailedResponseMessage<T>()
                {
                    DataTable = DataTable,
                    Exception = Exception ??= null,
                    StoredProcedure = _sprocName
                };
               // foreach (var param in this._command.Parameters)
                 //   responseMessage?.Parameters?.Add(param.ToString(), param.);

                foreach (ISqlFilter filter in _filters)
                    filter.OnResponse(this, DataTable);
                
            }
            return responseMessage;


        }

        public void Dispose()
        {
            _connection.Dispose();
            _command.Dispose();
            DataTable?.Dispose();
        }
    }
}