﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data.Interfaces;
using personapi_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace personapi_dotnet.Controllers
{
    [Route("api/telefono")]
    public class TelefonoController:Controller
    {
        private readonly ITelefonoRepository _telefonoRepository;
        private readonly IPersonaRepository _personaRepository;

        public TelefonoController(ITelefonoRepository telefonoRepository, IPersonaRepository personaRepository)
        {
            _telefonoRepository = telefonoRepository;
            _personaRepository = personaRepository;
        }

        // GET: api/telefonos
        [HttpGet("list")]
        public async Task<IActionResult> Index()
        {
            var telefonos = await _telefonoRepository.GetAllTelefonos();
            return View(telefonos);
        }

        // GET: api/telefonos/details/{id}
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _telefonoRepository.GetTelefonoById(id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // GET: api/telefonos/create
        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var personas = await _personaRepository.GetAllPersonas();
            ViewData["Duenio"] = new SelectList(personas, "Cc", "Cc");
            return View();
        }

        // POST: api/telefonos/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Num,Oper,Duenio")] Telefono telefono)
        {
            var persona = await _personaRepository.GetPersonaByCc(telefono.Duenio);
            if (persona == null)
            {
                ModelState.AddModelError("Duenio", "La persona especificada como dueño no existe.");
            }

            telefono.DuenioNavigation = persona;

            ModelState.Clear();
            TryValidateModel(telefono);
            if (ModelState.IsValid)
            {
                await _telefonoRepository.AddTelefono(telefono);
                return RedirectToAction(nameof(Index));
            }

            var personas = await _personaRepository.GetAllPersonas();
            ViewData["Duenio"] = new SelectList(personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        // GET: api/telefonos/edit/{id}
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _telefonoRepository.GetTelefonoById(id);
            if (telefono == null)
            {
                return NotFound();
            }

            var personas = await _personaRepository.GetAllPersonas();
            ViewData["Duenio"] = new SelectList(personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        // POST: api/telefonos/edit/{id}
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Num,Oper,Duenio")] Telefono telefono)
        {
            if (id != telefono.Num)
            {
                return NotFound();
            }

            var persona = await _personaRepository.GetPersonaByCc(telefono.Duenio);
            if (persona == null)
            {
                ModelState.AddModelError("Duenio", "La persona especificada como dueño no existe.");
            }

            telefono.DuenioNavigation = persona;

            ModelState.Clear();
            TryValidateModel(telefono);
            if (ModelState.IsValid)
            {
                await _telefonoRepository.UpdateTelefono(telefono);
                return RedirectToAction(nameof(Index));
            }

            var personas = await _personaRepository.GetAllPersonas();
            ViewData["Duenio"] = new SelectList(personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        // GET: api/telefonos/delete/{id}
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _telefonoRepository.GetTelefonoById(id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // POST: api/telefonos/delete/{id}
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _telefonoRepository.DeleteTelefono(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
