using Microsoft.AspNetCore.Mvc;
using Projectopdracht.DTOs;
using Projectopdracht.Interface;
using Projectopdracht.Models;

namespace Projectopdracht.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContainersController : ControllerBase
    {
        private readonly ILogisticsService _service;

        public ContainersController(ILogisticsService service)
        {
            _service = service;
        }

        // GET: api/containers
        [HttpGet]
        public async Task<ActionResult<List<ContainerReadDto>>> Get()
        {
            var containers = await _service.GetAllContainersAsync();
            return Ok(containers);
        }

        // GET: api/containers/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Container>> GetById(int id)
        {
            var container = await _service.GetContainerByIdAsync(id);
            if (container == null)
            {
                return NotFound(new { message = $"Container {id} niet gevonden." });
            }
            return Ok(container);
        }

        // POST: api/containers
        [HttpPost]
        public async Task<ActionResult<Container>> Create(ContainerCreateDto containerDto)
        {
            var depot = await _service.GetDepotByIdAsync(containerDto.DepotId);
            if (depot == null)
            {
                return BadRequest(new { message = "Het opgegeven DepotId bestaat niet." });
            }

            var createdContainer = await _service.AddContainerAsync(containerDto);

            return CreatedAtAction(nameof(GetById), new { id = createdContainer.Id }, createdContainer);
        }

        // PUT: api/containers/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContainerUpdateDto containerDto)
        {
            var success = await _service.UpdateContainerAsync(id, containerDto);

            if (!success)
            {
                return NotFound(new { message = $"Container {id} niet gevonden om bij te werken." });
            }

            return NoContent();
        }

        // DELETE: api/containers/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteContainerAsync(id);

            if (!success)
            {
                return NotFound(new { message = $"Container {id} niet gevonden." });
            }

            return Ok(new { message = $"Container {id} is succesvol verwijderd." });
        }
    }
}