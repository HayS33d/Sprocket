using System.Data;
using System.Data.SqlClient;

namespace Sprocket
{
    public interface ISqlFilter
    {
        void OnRequest(ISqlRequest request, SqlCommand command);

        void OnResponse(ISqlResponse response, DataTable dataTable);
    }
}