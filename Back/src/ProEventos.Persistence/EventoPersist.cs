using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        public ProEventosContext _context { get; set; }

        public EventoPersist(ProEventosContext context)
        {
            _context = context;
        }        

        public async Task<Evento[]> GetAllEventosAsync(int userId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.Where(e => e.UserId == userId).OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int userId, int PalestranteId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query
                .Where(e => e.Id == PalestranteId && e.UserId == userId)
                .OrderBy(e => e.Id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
               .Include(e => e.Lotes)
               .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query
                .Where(e => e.Tema.ToLower().Contains(tema.ToLower()) && 
                            e.UserId == userId)
                .OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }
    }
}