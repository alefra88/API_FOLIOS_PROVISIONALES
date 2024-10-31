using PROV_TP_FOLIO_API.config.Interfaces;
using System.Data.SqlClient;

namespace PROV_TP_FOLIO_API.config.Imp
{
    public class DbConnection : IDbCon
    {
        private readonly string _dbConString;

        public DbConnection(IConfiguration configuration) => _dbConString = configuration.GetConnectionString("Connection")
            ?? throw new ArgumentNullException(nameof(configuration));


        public async Task<SqlConnection> GetConAsync()
        {
           var con = new SqlConnection(_dbConString);
            await con.OpenAsync();
            return con;
        }
    }
}
