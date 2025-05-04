using System.Collections.Generic;
using System.Threading.Tasks;
using personapi_dotnet.Models;

namespace personapi_dotnet.Data.Interfaces
{
    public interface IProfesionRepository
    {
        Task<IEnumerable<Profesion>> GetAllProfesiones();
        Task<Profesion> GetProfesionById(int id);
        Task AddProfesion(Profesion profesion);
        Task UpdateProfesion(Profesion profesion);
        Task DeleteProfesion(int id);
        bool ProfesionExists(int id);
    }
}