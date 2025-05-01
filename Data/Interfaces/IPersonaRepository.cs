using System.Collections.Generic;
using System.Threading.Tasks;
using personapi_dotnet.Models;

namespace personapi_dotnet.Data.Interfaces
{
    public interface IPersonaRepository
    {
        Task<IEnumerable<Persona>> GetAllAsync();
        Task<Persona?> GetByIdAsync(int cc);
        Task AddAsync(Persona persona);
        Task UpdateAsync(Persona persona);
        Task DeleteAsync(int cc);
    }
}
