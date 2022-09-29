using System.Data;
using System.Data.SqlClient;

namespace Sprocket
{
    public class StandardSqlFilter : ISqlFilter
    {
        public void OnRequest(ISqlRequest request, SqlCommand command)
        {
        }

        public void OnResponse(ISqlResponse response, DataTable dataTable)
        {
            //If you want to scrub or replace any data in the datatable do it here.
            //dataTable.Rows[0][0] = dataTable.Rows[0][0].ToString.Replace("bad word", "clean word");
        }
    }
}