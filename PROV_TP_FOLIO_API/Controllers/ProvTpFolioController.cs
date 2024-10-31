using Microsoft.AspNetCore.Mvc;
using PROV_TP_FOLIO_API.Models;
using PROV_TP_FOLIO_API.Repositories.Imp;
using PROV_TP_FOLIO_API.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PROV_TP_FOLIO_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvTpFolioController : ControllerBase
    {
        private readonly IProvTpFolioService _provTpFolioService;
        public ProvTpFolioController(IProvTpFolioService provTpFolioService)
        {
            _provTpFolioService = provTpFolioService;
        }
        // GET: api/<ProvTpFolioController>

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var folios = await _provTpFolioService.GetAsync();
            return Ok(folios);
        }
     

        // GET api/<ProvTpFolioController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var folio = await _provTpFolioService.GetAsync(id);
           
            return Ok(folio);
        }

        // POST api/<ProvTpFolioController>
        [HttpPost]
        public async Task Post([FromBody] ProvTpFolio folio)
        {
           await _provTpFolioService.AddFolioAsync(folio);
        }

        // PUT api/<ProvTpFolioController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ProvTpFolio folio)
        {
            await _provTpFolioService.GetAsync(id);
               
        }

        // DELETE api/<ProvTpFolioController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _provTpFolioService.DeleteFolioAsync(id);
        }
    }
}
