using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using realstate.Services.DTOs;
using realstate.Services.Repositories.Interfaces;

namespace realstate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _service;

        public PropertiesController(IPropertyService service)
        {
            _service = service;
        }

        [HttpGet]
        [EnableRateLimiting("DefaultPolicy")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 20)
            => Ok(await _service.GetAllAsync(page, pageSize));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var prop = await _service.GetByIdAsync(id);
            if (prop == null) return NotFound();
            return Ok(prop);
        }

        [HttpPost]
        [Authorize]
        [EnableRateLimiting("CreatePolicy")]
        public async Task<IActionResult> Create([FromBody] CreatePropertyDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePropertyDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
