using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data.Interfaces;
using personapi_dotnet.Models;

namespace personapi_dotnet.Controllers
{
    [Route("api/persona")]
    [ApiController]
    public class PersonasController : Controller
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonasController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        // GET: api/persona
        [HttpGet("list")]
        [ActionName(nameof(Index))]
        public async Task<IActionResult> Index()
        {
            var personas = await _personaRepository.GetAllPersonas();
            return View(personas);
        }

        // GET: api/persona/{id}
        [HttpGet("details/{id}")]
        [ActionName(nameof(Details))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _personaRepository.GetPersonaById(id.Value);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: api/persona/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: api/persona/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Create))]
        public async Task<IActionResult> Create([Bind("Cc,Nombre,Apellido,Genero,Edad")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                await _personaRepository.AddPersona(persona);
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: api/persona/edit/{id}
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _personaRepository.GetPersonaById(id.Value);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: api/persona/edit/{id}
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> Edit(int id, [Bind("Cc,Nombre,Apellido,Genero,Edad")] Persona persona)
        {
            if (id != persona.Cc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _personaRepository.UpdatePersona(persona);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_personaRepository.PersonaExists(persona.Cc))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: api/persona/delete/{id}
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _personaRepository.GetPersonaById(id.Value);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: api/persona/delete/{id}
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _personaRepository.DeletePersona(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
