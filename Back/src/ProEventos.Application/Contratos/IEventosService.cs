using System;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application
{
    public interface IEventosService
    {
        Task<Evento> AddEvento(Evento model);
        Task<Evento> UpdateEvento(int id, Evento model);
        Task<bool> DeleteEvento(int id);

        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);
        Task<Evento> GetEventoByIdAsync(int PalestranteId, bool includePalestrantes);
    }
}
