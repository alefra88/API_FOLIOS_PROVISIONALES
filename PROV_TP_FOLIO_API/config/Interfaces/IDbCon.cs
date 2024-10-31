using System.Data.SqlClient;

namespace PROV_TP_FOLIO_API.config.Interfaces
{
    public interface IDbCon
    {
        Task<SqlConnection> GetConAsync();
    }
}
