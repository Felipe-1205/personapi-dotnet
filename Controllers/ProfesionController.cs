using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data.Interfaces;
using personapi_dotnet.Models;

namespace personapi_dotnet.Controllers
{
    [Route("api/profesion")]
    [ApiController]
    public class ProfesionController : Controller
    {
        private readonly IProfesionRepository _profesionRepository;

        public ProfesionController(IProfesionRepository profesionRepository)
        {
            _profesionRepository = profesionRepository;
        }

        // GET: api/profesion
        [HttpGet("list")]
        [ActionName(nameof(Index))]
        public async Task<IActionResult> Index()
        {
            var profesion = await _profesionRepository.GetAllProfesiones();
            return View(profesiones);
        }

        // GET: api/profesion/{id}
        [HttpGet("details/{id}")]
        [ActionName(nameof(Details))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _profesionRepository.GetProfesionById(id.Value);
            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        // GET: api/profesion/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: api/profesion/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Create))]
        public async Task<IActionResult> Create([Bind("Id,Nom,Des")] Profesion profesion)
        {
            if (ModelState.IsValid)
            {
                await _profesionRepository.AddProfesion(profesion);
                return RedirectToAction(nameof(Index));
            }
            return View(profesion);
        }

        // GET: api/profesion/edit/{id}
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _profesionRepository.GetProfesionById(id.Value);
            if (profesion == null)
            {
                return NotFound();
            }
            return View(profesion);
        }

        // POST: api/profesion/edit/{id}
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Des")] Profesion profesion)
        {
            if (id != profesion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _profesionRepository.UpdateProfesion(profesion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_profesionRepository.ProfesionExists(profesion.Id))
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
            return View(profesion);
        }

        // GET: api/profesion/delete/{id}
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _personaRepository.GetProfesionById(id.Value);
            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        // POST: api/profesion/delete/{id}
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _profesionRepository.DeleteProfesion(id);
            return RedirectToAction(nameof(Index));
        }
    }
}