using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Models;

namespace ProEventos.Persistence
{
    public class EventoPersist : GeralPersist, IEventoPersist
    {
        private readonly ILogger _logger;
        public EventoPersist(ProEventosContext context, 
                             ILoggerFactory logger) : base(context)
        {
            _logger = logger.CreateLogger("Database");
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

            var evento = await query.FirstOrDefaultAsync();
          
            _logger.LogDebug("Consultando evento {evento} consultando todos os livros", evento);

            return evento;
        }

        public async Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false)
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
                .Where(e => (e.Tema.ToLower().Contains(pageParams.Term.ToLower()) ||
                             e.Local.ToLower().Contains(pageParams.Term.ToLower())) &&
                             e.UserId == userId)
                .OrderBy(e => e.Id);

            _logger.LogInformation("Retornando todos os eventos");
            _logger.LogDebug("Usuario {userId} consultando todos os eventos", userId);

            return await PageList<Evento>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }
    }
}