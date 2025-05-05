using System.Collections.Generic;
using System.Threading.Tasks;
using personapi_dotnet.Models;

namespace personapi_dotnet.Data.Interfaces
{
    public interface ITelefonoRepository
    {
        Task<IEnumerable<Telefono>> GetAllTelefonos();
        Task<Telefono> GetTelefonoById(string id);
        Task AddTelefono(Telefono telefono);
        Task UpdateTelefono(Telefono telefono);
        Task DeleteTelefono(string id);
        bool TelefonoExists(string id);
    }
}