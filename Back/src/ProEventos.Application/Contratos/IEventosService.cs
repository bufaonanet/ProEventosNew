using System.Threading.Tasks;
using ProEventos.Application.DTOs;

namespace ProEventos.Application
{
    public interface IEventosService
    {
        Task<EventoDto> AddEvento(EventoDto model);
        Task<EventoDto> UpdateEvento(int id, EventoDto model);
        Task<bool> DeleteEvento(int id);

        Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);
        Task<EventoDto> GetEventoByIdAsync(int PalestranteId, bool includePalestrantes);
    }
}
