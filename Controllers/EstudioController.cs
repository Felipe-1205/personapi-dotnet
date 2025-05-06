using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using personapi_dotnet.Data.Interfaces;
using personapi_dotnet.Models;

namespace personapi_dotnet.Controllers
{
    [Route("api/estudio")]
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

        // GET: api/estudio/list
        [HttpGet("list")]
        public async Task<IActionResult> Index()
        {
            var estudios = await _estudioRepository.GetAllEstudios();
            return View(estudios);
        }

        // GET: api/estudio/details/{idProf}/{ccPer}
        [HttpGet("details/{idProf}/{ccPer}")]
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
        public async Task<IActionResult> Create()
        {
            ViewData["CcPer"] = new SelectList(await _personaRepository.GetAllPersonas(), "Cc", "Cc");
            ViewData["IdProf"] = new SelectList(await _profesionRepository.GetAllProfesiones(), "Id", "Id");
            return View();
        }

        // POST: api/estudio/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
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

        // GET: api/estudio/edit/{idProf}/{ccPer}
        [HttpGet("edit/{idProf}/{ccPer}")]
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

        // POST: api/estudio/edit/{idProf}/{ccPer}
        [HttpPost("edit/{idProf}/{ccPer}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idProf, int ccPer, [Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            if (idProf != estudio.IdProf || ccPer != estudio.CcPer)
            {
                return NotFound();
            }

            var persona = await _personaRepository.GetPersonaByCc(estudio.CcPer);
            var profesion = await _profesionRepository.GetProfesionById(estudio.IdProf);

            estudio.IdProfNavigation = profesion;
            estudio.CcPerNavigation = persona;

            ModelState.Clear();
            TryValidateModel(estudio);

            if (ModelState.IsValid)
            {
                await _estudioRepository.UpdateEstudio(estudio);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CcPer"] = new SelectList(await _personaRepository.GetAllPersonas(), "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesionRepository.GetAllProfesiones(), "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // GET: api/estudio/delete/{idProf}/{ccPer}
        [HttpGet("delete/{idProf}/{ccPer}")]
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

        // POST: api/estudio/delete/{idProf}/{ccPer}
        [HttpPost("delete/{idProf}/{ccPer}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idProf, int ccPer)
        {
            await _estudioRepository.DeleteEstudio(idProf, ccPer);
            return RedirectToAction(nameof(Index));
        }
    }
}