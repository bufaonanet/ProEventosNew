using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersist
    {       
        Task<Evento[]> GetAllEventosAsync(int userId, bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes);
        Task<Evento> GetEventoByIdAsync(int userId, int PalestranteId, bool includePalestrantes);
    }
}