using Microsoft.AspNetCore.Mvc;
using Projectopdracht.DTOs;
using Projectopdracht.Interface;

namespace Projectopdracht.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransportsController : ControllerBase
    {
        private readonly ILogisticsService _service;

        public TransportsController(ILogisticsService service)
        {
            _service = service;
        }

        // GET: api/transports
        [HttpGet]
        public async Task<ActionResult<List<TransportReadDto>>> Get()
        {
            var transports = await _service.GetAllTransportsAsync();
            return Ok(transports);
        }

        // GET: api/transports/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TransportReadDto>> GetById(int id)
        {
            var transport = await _service.GetTransportByIdAsync(id);
            if (transport == null)
                return NotFound(new { message = $"Transport {id} niet gevonden." });

            return Ok(transport);
        }

        // POST: api/transports
        [HttpPost]
        public async Task<ActionResult<TransportReadDto>> Create(TransportCreateDto transportDto)
        {
            if (string.IsNullOrWhiteSpace(transportDto.TruckLicensePlate))
                return BadRequest(new { message = "Nummerplaat van de vrachtwagen is verplicht." });

            if (transportDto.AppointmentTime == default)
                return BadRequest(new { message = "Een geldig afspraakmoment is verplicht." });

            var container = await _service.GetContainerByIdAsync(transportDto.ContainerId);
            if (container == null)
                return BadRequest(new { message = "Transport kan niet aangemaakt worden voor een onbestaande container." });

            var createdTransport = await _service.AddTransportAsync(transportDto);

            var dto = await _service.GetTransportByIdAsync(createdTransport.Id);
            return CreatedAtAction(nameof(GetById), new { id = createdTransport.Id }, dto);
        }

        // PUT: api/transports/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, TransportUpdateDto dto)
        {
            var success = await _service.UpdateTransportAsync(id, dto);

            if (!success)
            {
                return BadRequest("Update mislukt: Transport niet gevonden of ContainerId is ongeldig.");
            }

            return NoContent();
        }

        // DELETE: api/transports/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteTransportAsync(id);

            if (!success)
            {
                return NotFound($"Transport met ID {id} is niet gevonden.");
            }

            return NoContent(); 
        }
    }
}
