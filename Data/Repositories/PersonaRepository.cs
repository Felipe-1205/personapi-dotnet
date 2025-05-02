using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data.Interfaces;
using personapi_dotnet.Models;

namespace personapi_dotnet.Data.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly MasterContext _context;

        public PersonaRepository(MasterContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Persona>> GetAllPersonas()
        {
            return await _context.Personas.ToListAsync();
        }

        public async Task<Persona> GetPersonaByCc(int cc)
        {
            return await _context.Personas.FirstOrDefaultAsync(p => p.Cc == cc);
        }

        public async Task<Persona> GetPersonaById(int id)
        {
            return await _context.Personas.FindAsync(id);
        }

        public async Task AddPersona(Persona persona)
        {
            _context.Add(persona);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersona(Persona persona)
        {
            _context.Update(persona);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
            }
        }

        public bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.Cc == id);
        }
    }
}
