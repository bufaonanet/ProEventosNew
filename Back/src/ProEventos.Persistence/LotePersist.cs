using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class LotePersist : ILotePersist
    {
        public ProEventosContext _context { get; set; }

        public LotePersist(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.Lotes
                .AsNoTracking()
                .Where(l => l.EventoId == eventoId);

            return await query
                .ToArrayAsync();           
        }

        public async Task<Lote> GetLoteByIdAsync(int eventoId, int id)
        {
            IQueryable<Lote> query = _context.Lotes
                .AsNoTracking()
                .Where(l => l.EventoId == eventoId && l.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}