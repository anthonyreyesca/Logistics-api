using Microsoft.AspNetCore.Mvc;
using Projectopdracht.DTOs;
using Projectopdracht.Interface;

namespace Projectopdracht.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepotsController : ControllerBase
    {
        private readonly ILogisticsService _service;

        public DepotsController(ILogisticsService service)
        {
            _service = service;
        }

        // GET: api/depots
        [HttpGet]
        public async Task<ActionResult<List<DepotReadDto>>> Get()
        {
            var depots = await _service.GetAllDepotsAsync();
            return Ok(depots);
        }

        // GET: api/depots/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DepotReadDto>> GetById(int id)
        {
            var depot = await _service.GetDepotByIdAsync(id);
            if (depot == null)
                return NotFound(new { message = $"Depot {id} niet gevonden." });

            return Ok(depot);
        }

        // POST: api/depots
        [HttpPost]
        public async Task<ActionResult<DepotReadDto>> Create(DepotCreateDto depotDto)
        {
            if (string.IsNullOrWhiteSpace(depotDto.Name))
                return BadRequest(new { message = "Naam van het depot is verplicht." });

            if (string.IsNullOrWhiteSpace(depotDto.Location))
                return BadRequest(new { message = "Locatie van het depot is verplicht." });

            var createdDepot = await _service.AddDepotAsync(depotDto);
            return CreatedAtAction(nameof(GetById), new { id = createdDepot.Id }, createdDepot);
        }

        // PUT: api/depots/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, DepotUpdateDto dto)
        {
            var success = await _service.UpdateDepotAsync(id, dto);
            if (!success) return NotFound("Depot niet gevonden.");

            return NoContent();
        }

        // DELETE: api/depots/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteDepotAsync(id);
            if (!success)
            {
                return BadRequest("Kan depot niet verwijderen. Controleer of het depot bestaat en of er nog containers aan gekoppeld zijn.");
            }

            return NoContent();
        }
    }
}
