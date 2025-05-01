using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Data.Interfaces;
using personapi_dotnet.Models;

namespace personapi_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaRepository _repo;

        public PersonaController(IPersonaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _repo.GetAllAsync());

        [HttpGet("{cc}")]
        public async Task<IActionResult> GetById(int cc)
        {
            var persona = await _repo.GetByIdAsync(cc);
            return persona == null ? NotFound() : Ok(persona);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Persona persona)
        {
            await _repo.AddAsync(persona);
            return CreatedAtAction(nameof(GetById), new { cc = persona.Cc }, persona);
        }

        [HttpPut("{cc}")]
        public async Task<IActionResult> Update(int cc, Persona persona)
        {
            if (cc != persona.Cc) return BadRequest();
            await _repo.UpdateAsync(persona);
            return NoContent();
        }

        [HttpDelete("{cc}")]
        public async Task<IActionResult> Delete(int cc)
        {
            await _repo.DeleteAsync(cc);
            return NoContent();
        }
    }
}
