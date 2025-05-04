using System.Collections.Generic;
using System.Threading.Tasks;
using personapi_dotnet.Models;

namespace personapi_dotnet.Data.Interfaces
{
    public interface ITelefonoRepository
    {
        Task<IEnumerable<Telefono>> GetAllTelefonos();
        Task<Telefono> GetTelefonoById(int id);
        Task AddTelefono(Telefono persona);
        Task UpdateTelefono(Telefono persona);
        Task DeleteTelefono(int id);
        bool TelefonoExists(int id);
    }
}