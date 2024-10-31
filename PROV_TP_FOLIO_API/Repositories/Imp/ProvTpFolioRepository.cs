using PROV_TP_FOLIO_API.config.Interfaces;
using PROV_TP_FOLIO_API.Models;
using PROV_TP_FOLIO_API.Services.Interfaces;
using System.Data.SqlClient;
using System.Data;
using PROV_TP_FOLIO_API.Repositories.Interfaces;

namespace PROV_TP_FOLIO_API.Repositories.Imp
{
    public class ProvTpFolioRepository : IProvTpFolioRepository
    {
        private readonly IDbCon _dbCon;
        public ProvTpFolioRepository(IDbCon dbCon)
        {
            _dbCon = dbCon;
        }

        public async Task AddFolioAsync(ProvTpFolio folio)
        {
            var query = @"INSERT INTO Folios (
                     FOLIO_LLAV_PR, TIPO_SOLICITUD, ESTATUS, NUMERO_SOLICITUDES, NS_ACEPTADAS, 
                     NS_RECHAZADAS, NOTIFICACION, USUA_NLLAV_PR, USUA_MAILS, CECO_LLAV_PR, 
                     NEGO_LLAV_PR, USUA_CLLAV_PR, USUA_LLAV_PR, TMPO_LLAV_PR, NS_AUTORIZADAS)
                  VALUES (
                     @FOLIO_LLAV_PR, @TIPO_SOLICITUD, @ESTATUS, @NUMERO_SOLICITUDES, @NS_ACEPTADAS, 
                     @NS_RECHAZADAS, @NOTIFICACION, @USUA_NLLAV_PR, @USUA_MAILS, @CECO_LLAV_PR, 
                     @NEGO_LLAV_PR, @USUA_CLLAV_PR, @USUA_LLAV_PR, @TMPO_LLAV_PR, @NS_AUTORIZADAS)";
            using var con = await _dbCon.GetConAsync();
            using var command = new SqlCommand(query, con);
            AddParameters(command, folio);
            if (con.State == ConnectionState.Closed)
                await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteFolioAsync(int id)
        {
            var query = @"DELETE FROM PROV_TP_FOLIO WHERE FOLIO_LLAV_PR = @FOLIO_LLAV_PR";
            using var con = await _dbCon.GetConAsync();
            using var command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@FOLIO_LLAV_PR", id);
            if (con.State == ConnectionState.Closed) await con.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<List<ProvTpFolio>> GetAsync()
        {
            var folios = new List<ProvTpFolio>();
            using (SqlConnection con = await _dbCon.GetConAsync())
            {
                var command = con.CreateCommand();
                command.CommandText = "SELECT * FROM PROV_TP_FOLIO";

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var folio = MapReaderProvTpFolio(reader);
                    folios.Add(folio);
                }
            }
            return folios;
        }

        public async Task<ProvTpFolio?> GetAsync(int id)
        {
            using var con = await _dbCon.GetConAsync();
            var command = con.CreateCommand();
            command.CommandText = "SELECT * FROM PROV_TP_FOLIO WHERE FOLIO_LLAV_PR = @Id";
            command.Parameters.AddWithValue("@Id", id);
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapReaderProvTpFolio(reader);
            }
            return null;
        }

        public async Task UpdateFolioAsync(ProvTpFolio folio)
        {
            var query = @"UPDATE PROV_TP_FOLIO
          SET TIPO_SOLICITUD = @TIPO_SOLICITUD, ESTATUS = @ESTATUS, NUMERO_SOLICITUDES = @NUMERO_SOLICITUDES,
              NS_ACEPTADAS = @NS_ACEPTADAS, NS_RECHAZADAS = @NS_RECHAZADAS, NOTIFICACION = @NOTIFICACION,
              USUA_NLLAV_PR = @USUA_NLLAV_PR, USUA_MAILS = @USUA_MAILS, CECO_LLAV_PR = @CECO_LLAV_PR,
              NEGO_LLAV_PR = @NEGO_LLAV_PR, USUA_CLLAV_PR = @USUA_CLLAV_PR, USUA_LLAV_PR = @USUA_LLAV_PR,
              TMPO_LLAV_PR = @TMPO_LLAV_PR, NS_AUTORIZADAS = @NS_AUTORIZADAS
          WHERE FOLIO_LLAV_PR = @FOLIO_LLAV_PR";
            using var con = await _dbCon.GetConAsync();
            using var command = new SqlCommand(query, con);
            AddParameters(command, folio);
            command.Parameters.AddWithValue("@FOLIO_LLAV_PR", folio.FolioLlavPr);
            await command.ExecuteNonQueryAsync();
        }

        private static void AddParameters(SqlCommand command, ProvTpFolio folio)
        {
            command.Parameters.AddWithValue("@FOLIO_LLAV_PR", folio.FolioLlavPr);
            command.Parameters.AddWithValue("@TIPO_SOLICITUD", folio.TipoSolicitud);
            command.Parameters.AddWithValue("@ESTATUS", folio.Estatus);
            command.Parameters.AddWithValue("@NUMERO_SOLICITUDES", folio.NumeroSolicitudes ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@NS_ACEPTADAS", folio.NsAceptadas ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@NS_RECHAZADAS", folio.NsRechazadas ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@NOTIFICACION", folio.Notificacion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@USUA_NLLAV_PR", folio.UsuaNllavPr ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@USUA_MAILS", folio.UsuaMails ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@CECO_LLAV_PR", folio.CecoLlavPr ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@NEGO_LLAV_PR", folio.NegoLlavPr ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@USUA_CLLAV_PR", folio.UsuaCllavPr ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@USUA_LLAV_PR", folio.UsuaLlavPr ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@TMPO_LLAV_PR", folio.TmpoLlavPr ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@NS_AUTORIZADAS", folio.NsAutorizadas ?? (object)DBNull.Value);
        }

        private static ProvTpFolio MapReaderProvTpFolio(SqlDataReader reader)
        {
            return new ProvTpFolio
            {
                FolioLlavPr = reader.GetInt32(reader.GetOrdinal("FOLIO_LLAV_PR")),
                TipoSolicitud = reader.GetInt32(reader.GetOrdinal("TIPO_SOLICITUD")),
                Estatus = reader.GetInt32(reader.GetOrdinal("ESTATUS")),
                NumeroSolicitudes = reader.IsDBNull(reader.GetOrdinal("NUMERO_SOLICITUDES")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("NUMERO_SOLICITUDES")),
                NsAceptadas = reader.IsDBNull(reader.GetOrdinal("NS_ACEPTADAS")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("NS_ACEPTADAS")),
                NsRechazadas = reader.IsDBNull(reader.GetOrdinal("NS_RECHAZADAS")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("NS_RECHAZADAS")),
                Notificacion = reader.IsDBNull(reader.GetOrdinal("NOTIFICACION")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("NOTIFICACION")),
                UsuaNllavPr = reader.IsDBNull(reader.GetOrdinal("USUA_NLLAV_PR")) ? null : reader.GetString(reader.GetOrdinal("USUA_NLLAV_PR")),
                UsuaMails = reader.IsDBNull(reader.GetOrdinal("USUA_MAILS")) ? null : reader.GetString(reader.GetOrdinal("USUA_MAILS")),
                CecoLlavPr = reader.IsDBNull(reader.GetOrdinal("CECO_LLAV_PR")) ? null : reader.GetString(reader.GetOrdinal("CECO_LLAV_PR")),
                NegoLlavPr = reader.IsDBNull(reader.GetOrdinal("NEGO_LLAV_PR")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("NEGO_LLAV_PR")),
                UsuaCllavPr = reader.IsDBNull(reader.GetOrdinal("USUA_CLLAV_PR")) ? null : reader.GetString(reader.GetOrdinal("USUA_CLLAV_PR")),
                UsuaLlavPr = reader.IsDBNull(reader.GetOrdinal("USUA_LLAV_PR")) ? null : reader.GetString(reader.GetOrdinal("USUA_LLAV_PR")),
                TmpoLlavPr = reader.IsDBNull(reader.GetOrdinal("TMPO_LLAV_PR")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("TMPO_LLAV_PR")),
                NsAutorizadas = reader.IsDBNull(reader.GetOrdinal("NS_AUTORIZADAS")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("NS_AUTORIZADAS")),
            };
        }
    }
}
