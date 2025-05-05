using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data.Interfaces;
using personapi_dotnet.Models;

namespace personapi_dotnet.Data.Repositories
{
    public class ProfesionRepository : IProfesionRepository
    {
        private readonly MasterContext _context;

        public ProfesionRepository(MasterContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profesion>> GetAllProfesiones()
        {
            return await _context.Profesiones.ToListAsync();
        }

        public async Task<Profesion> GetProfesionById(int id)
        {
            return await _context.Profesiones.FindAsync(id);
        }

        public async Task AddProfesion(Profesion profesion)
        {
            _context.Add(profesion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProfesion(Profesion profesion)
        {
            _context.Update(profesion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfesion(int id)
        {
            var profesion = await _context.Profesiones.FindAsync(id);
            if(profesion != null)
            {
                _context.Profesiones.Remove(profesion);
                await _context.SaveChangesAsync();
            }
        }

        public bool ProfesionExists(int id)
        {
            return _context.Profesiones.Any(e => e.Id == id);
        }
    }
}