using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data.Interfaces;
using personapi_dotnet.Models;

namespace personapi_dotnet.Data.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly PersonaDbContext _context;

        public PersonaRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            return await _context.Personas
                .Include(p => p.Estudios)
                .Include(p => p.Telefonos)
                .ToListAsync();
        }

        public async Task<Persona?> GetByIdAsync(int cc)
        {
            return await _context.Personas
                .Include(p => p.Estudios)
                .Include(p => p.Telefonos)
                .FirstOrDefaultAsync(p => p.Cc == cc);
        }

        public async Task AddAsync(Persona persona)
        {
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Persona persona)
        {
            _context.Entry(persona).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int cc)
        {
            var persona = await _context.Personas.FindAsync(cc);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
            }
        }
    }
}
