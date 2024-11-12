using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BeDesi.Core.Repository
{
    public class BaseRepository
    {

        public string ReadDbNullStringSafely(SqlDataReader reader, int columnNum)
        {
            if (reader.IsDBNull(columnNum))
                return string.Empty;

            return reader.GetString(columnNum);
        }
    }
}
