using System.Collections.Generic;
using System.Threading.Tasks;
using personapi_dotnet.Models;

namespace personapi_dotnet.Data.Interfaces
{
    public interface IPersonaRepository
    {
        Task<IEnumerable<Persona>> GetAllPersonas();
        Task<Persona> GetPersonaByCc(int cc);
        Task<Persona> GetPersonaById(int id);
        Task AddPersona(Persona persona);
        Task UpdatePersona(Persona persona);
        Task DeletePersona(int id);
        bool PersonaExists(int id);
    }
}
