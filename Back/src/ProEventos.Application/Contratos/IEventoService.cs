using System.Threading.Tasks;
using ProEventos.Application.DTOs;
using ProEventos.Persistence.Models;

namespace ProEventos.Application
{
    public interface IEventoService
    {
        Task<EventoDto> AddEvento(int userId, EventoDto model);
        Task<EventoDto> UpdateEvento(int userId, int id, EventoDto model);
        Task<bool> DeleteEvento(int userId, int id);

        Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false);
        Task<PageList<EventoDto>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false);
    }
}
