using PROV_TP_FOLIO_API.Models;

namespace PROV_TP_FOLIO_API.Services.Interfaces
{
    public interface IProvTpFolioService
    {
        Task<List<ProvTpFolio>> GetAsync();
        Task<ProvTpFolio?> GetAsync(int id);
        Task AddFolioAsync(ProvTpFolio folio);
        Task UpdateFolioAsync(ProvTpFolio folio);
        Task DeleteFolioAsync(int id);
    }
}
