using System.Threading.Tasks;
using ProEventos.Application.DTOs;

namespace ProEventos.Application
{
    public interface IEventoService
    {
        Task<EventoDto> AddEvento(int userId, EventoDto model);
        Task<EventoDto> UpdateEvento(int userId, int id, EventoDto model);
        Task<bool> DeleteEvento(int userId, int id);

        Task<EventoDto[]> GetAllEventosAsync(int userId, bool includePalestrantes = false);
        Task<EventoDto[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes);
        Task<EventoDto> GetEventoByIdAsync(int userId, int PalestranteId, bool includePalestrantes);
    }
}
