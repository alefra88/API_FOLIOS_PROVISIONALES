using PROV_TP_FOLIO_API.Models;

namespace PROV_TP_FOLIO_API.Repositories.Interfaces
{
    public interface IProvTpFolioRepository
    {
        Task<List<ProvTpFolio>> GetAsync();
        Task<ProvTpFolio?> GetAsync(int id);
        Task AddFolioAsync(ProvTpFolio folio);
        Task UpdateFolioAsync(ProvTpFolio folio);
        Task DeleteFolioAsync(int id);
    }
}
