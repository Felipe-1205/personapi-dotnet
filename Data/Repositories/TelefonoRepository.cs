using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data.Interfaces;
using personapi_dotnet.Models;


namespace personapi_dotnet.Data.Repocitories
{
    public class TelefonoRepository : ITelefonoRepository
    {
        private readonly MasterContext _context;

        public TelefonoRepository(MasterContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Telefono>> GetAllTelefonos()
        {
            return await _context.Telefonos.ToListAsync();
        }
        public async Task<Telefono> GetTelefonoById(int id)
        {
            return await _context.Telefonos.FindAsync(id);
        }
        public async Task AddTelefono(Telefono telefono)
        {
            _context.Add(telefono);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTelefono(Telefono telefono)
        {
            _context.Update(telefono);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTelefono(int id)
        {
            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
                await _context.SaveChangesAsync();
            }
        }
        public bool TelefonoExists(int id)
        {
            return _context.Telefonos.Any(e => e.Id == id);
        }
    }
}