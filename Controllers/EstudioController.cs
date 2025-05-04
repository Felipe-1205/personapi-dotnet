using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data.Interfaces;
using personapi_dotnet.Models;

namespace personapi_dotnet.Controllers
{
    [Route("api/estudio")]
    [ApiController]
    public class EstudioController : Controller
    {
        private readonly IEstudioRepository _estudioRepository;

        public EstudioController(IEstudioRepository estudioRepository)
        {
            _estudioRepository = estudioRepository;
        }

        // GET: api/estudio
        [HttpGet("list")]
        [ActionName(nameof(Index))]
        public async Task<IActionResult> Index()
        {
            var estudios = await _estudioRepository.GetAllEstudios();
            return View(estudios);
        }

        // GET: api/estudio/{id}
        [HttpGet("details/{id}/{cc}")]
        [ActionName(nameof(Details))]
        public async Task<IActionResult> Details(int? id, int? cc)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudio = await _estudioRepository.GetEstudioByIdAndCc(id.Value, cc.Value);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // GET: api/estudio/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: api/estudio/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Create))]
        public async Task<IActionResult> Create([Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            if (ModelState.IsValid)
            {
                await _estudioRepository.AddEstudio(estudio);
                return RedirectToAction(nameof(Index));
            }
            return View(estudio);
        }

        // GET: api/estudio/edit/{id}
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id, int? cc)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudio = await _estudioRepository.GetEstudioByIdAndCc(id.Value, cc.Value);
            if (estudio == null)
            {
                return NotFound();
            }
            return View(estudio);
        }

        // POST: api/estudio/edit/{id}
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> Edit(int id, Create([Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio))
        {
            if (id != persona.Cc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _estudioRepository.UpdatePersona(estudio);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_estudioRepository.EstudioExists(estudio.IdProf))
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
            return View(estudio);
        }

        // GET: api/persona/delete/{id}
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudio = await _estudioRepository.GetEstudioById(id.Value);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // POST: api/persona/delete/{id}
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _estudioRepository.DeleteEstudio(id);
            return RedirectToAction(nameof(Index));
        }
    }
}