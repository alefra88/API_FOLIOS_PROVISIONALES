using PROV_TP_FOLIO_API.Models;
using System.Data;
using System.Data.SqlClient;
using PROV_TP_FOLIO_API.config.Interfaces;
using PROV_TP_FOLIO_API.Services.Interfaces;
using PROV_TP_FOLIO_API.Repositories.Interfaces;

namespace PROV_TP_FOLIO_API.Services.Imp
{
    public class ProvTpFolioService : IProvTpFolioService
    {
        private readonly IProvTpFolioRepository _repository;
        public ProvTpFolioService(IProvTpFolioRepository repository)
        {
            _repository = repository;
        }

        public async Task AddFolioAsync(ProvTpFolio folio)
        {
           if(folio != null)
            {
               await _repository.AddFolioAsync(folio);
            }
            
        }

        public async Task DeleteFolioAsync(int id)
        {
            await _repository.DeleteFolioAsync(id);
        }

        public async Task<List<ProvTpFolio>> GetAsync()
        {

            return await _repository.GetAsync();
        }

        public async Task<ProvTpFolio?> GetAsync(int id)
        {
           return await _repository.GetAsync(id);
        }

        public async Task UpdateFolioAsync(ProvTpFolio folio)
        {
            await _repository.UpdateFolioAsync(folio);
        }

    }
}
