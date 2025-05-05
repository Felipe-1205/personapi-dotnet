using System.Collections.Generic;
using System.Threading.Tasks;
using personapi_dotnet.Models;

namespace personapi_dotnet.Data.Interfaces
{
    public interface IEstudioRepository
    {
        Task<IEnumerable<Estudio>> GetAllEstudios();
        Task<Estudio> GetEstudioByIdAndCc(int idProf, int ccPer);
        Task AddEstudio(Estudio estudio);
        Task UpdateEstudio(Estudio estudio);
        Task DeleteEstudio(int idProf, int ccPer);
        bool EstudioExists(int id);
    }
}