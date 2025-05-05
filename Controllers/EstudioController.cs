using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IPersonaRepository _personaRepository;
        private readonly IProfesionRepository _profesionRepository;

        public EstudioController(IEstudioRepository estudioRepository, IPersonaRepository personaRepository, IProfesionRepository profesionRepository)
        {
            _estudioRepository = estudioRepository;
            _personaRepository = personaRepository;
            _profesionRepository = profesionRepository;
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
        public async Task<IActionResult> Details(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _estudioRepository.GetEstudioByIdAndCc(idProf.Value, ccPer.Value);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // GET: api/estudio/create
        [HttpGet("create")]
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["CcPer"] = new SelectList(await _personaRepository.GetAllPersonas(), "Cc", "Cc");
            ViewData["IdProf"] = new SelectList(await _profesionRepository.GetAllProfesiones(), "Id", "Id");
            return View();
        }

        // POST: api/estudio/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Create))]
        public async Task<IActionResult> Create([Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            // Realizar la validación del número de identificación de la persona y la existencia del profesor
            var persona = await _personaRepository.GetPersonaByCc(estudio.CcPer);
            var profesion = await _profesionRepository.GetProfesionById(estudio.IdProf);

            estudio.IdProfNavigation = profesion;
            estudio.CcPerNavigation = persona;

            ModelState.Clear();
            TryValidateModel(estudio);

            if (ModelState.IsValid)
            {
                await _estudioRepository.AddEstudio(estudio);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CcPer"] = new SelectList(await _personaRepository.GetAllPersonas(), "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesionRepository.GetAllProfesiones(), "Id", "Id", estudio.IdProf);

            return View(estudio);
        }

        // GET: api/estudio/edit/{id}
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _estudioRepository.GetEstudioByIdAndCc(idProf.Value, ccPer.Value);
            if (estudio == null)
            {
                return NotFound();
            }
            ViewData["CcPer"] = new SelectList(await _personaRepository.GetAllPersonas(), "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesionRepository.GetAllProfesiones(), "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // POST: api/estudio/edit/{id}
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> Edit(int? idProf, int? ccPer, [Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            if (idProf != estudio.IdProf || ccPer != estudio.CcPer)
            {
                return NotFound();
            }

            // Realizar la validación del número de identificación de la persona y la existencia del profesor
            var persona = await _personaRepository.GetPersonaByCc(estudio.CcPer);
            var profesion = await _profesionRepository.GetProfesionById(estudio.IdProf);

            estudio.IdProfNavigation = profesion;
            estudio.CcPerNavigation = persona;

            ModelState.Clear();
            TryValidateModel(estudio);

            if (ModelState.IsValid)
            {
                try
                {
                    await _estudioRepository.UpdateEstudio(estudio);
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
            ViewData["CcPer"] = new SelectList(await _personaRepository.GetAllPersonas(), "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesionRepository.GetAllProfesiones(), "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // GET: api/persona/delete/{id}
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _estudioRepository.GetEstudioByIdAndCc(idProf.Value, ccPer.Value);
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
        public async Task<IActionResult> DeleteConfirmed(int? idProf, int? ccPer)
        {
            await _estudioRepository.DeleteEstudio(idProf.Value, ccPer.Value);
            return RedirectToAction(nameof(Index));
        }
    }
}