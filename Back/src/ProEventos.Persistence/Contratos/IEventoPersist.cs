using System.Threading.Tasks;
using ProEventos.Domain;
using ProEventos.Persistence.Models;

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersist : IGeralPersist
    {  
        Task<Evento> GetEventoByIdAsync(int userId, int PalestranteId, bool includePalestrantes);
        Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes);
    }
}