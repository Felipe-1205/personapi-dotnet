using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data.Interfaces;
using personapi_dotnet.Models;

namespace personapi_dotnet.Controllers
{
    [Route("api/telefono")]
    [ApiController]
    public class TelefonoController:Controller
    {
        private readonly ITelefonoRepository _telefonoRepository;

        public TelefonoController(ITelefonoRepository telefonoRepository)
        {
            _telefonoRepository = telefonoRepository;
        }

        // GET: api/telefono
        [HttpGet("list")]
        [ActionName(nameof(Index))]
        public async Task<IActionResult> Index()
        {
            var telefonos = await _telefonoRepository.GetAllTelefonos();
            return View(telefonos);
        }

        // GET: api/telefono/{id}
        [HttpGet("details/{id}")]
        [ActionName(nameof(Details))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var telefono = await _telefonoRepository.GetTelefonoById(id.Value);
            if (telefono == null)
            {
                return NotFound();
            }
            return View(telefono);
        }

        // GET: api/telefono/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: api/telefono/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Create))]
        public async Task<IActionResult> Create([Bind("Id,Numero,Tipo")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                await _telefonoRepository.AddTelefono(telefono);
                return RedirectToAction(nameof(Index));
            }
            return View(telefono);
        }

        // GET: api/telefono/edit/{id}
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var telefono = await _telefonoRepository.GetTelefonoById(id.Value);
            if (telefono == null)
            {
                return NotFound();
            }
            return View(telefono);
        }

        // POST: api/telefono/edit/{id}
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Tipo")] Telefono telefono)
        {
            if (id != telefono.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _telefonoRepository.UpdateTelefono(telefono);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TelefonoExists(telefono.Id))
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
            return View(telefono);
        }

        // GET: api/telefono/delete/{id}
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var telefono = await _telefonoRepository.GetTelefonoById(id.Value);
            if (telefono == null)
            {
                return NotFound();
            }
            return View(telefono);
        }

        // POST: api/telefono/delete/{id}
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var telefono = await _telefonoRepository.GetTelefonoById(id);
            if (telefono != null)
            {
                await _telefonoRepository.DeleteTelefono(telefono);
            }
            return RedirectToAction(nameof(Index));
        }
    }

}
