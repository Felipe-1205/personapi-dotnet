using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data.Interfaces;
using personapi_dotnet.Models;

namespace personapi_dotnet.Data.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        private readonly MasterContext _context;

        public EstudioRepository(MasterContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudio>> GetAllEstudios()
        {
            return await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .ToListAsync();
        }

        public async Task<Estudio> GetEstudioByIdAndCc(int idProf, int ccPer)
        {
            return await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .FirstOrDefaultAsync(m => m.IdProf == idProf && m.CcPer == ccPer);
        }

        public async Task AddEstudio(Estudio estudio)
        {
            _context.Add(estudio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEstudio(Estudio estudio)
        {
            _context.Update(estudio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEstudio(int idProf, int ccPer)
        {
            var estudio = await _context.Estudios.FindAsync(idProf, ccPer);
            if(estudio != null)
            {
                _context.Estudios.Remove(estudio);
                await _context.SaveChangesAsync();
            }
        }

        public bool EstudioExists(int id)
        {
            return _context.Estudios.Any(e => e.IdProf == id);
        }
    }
}