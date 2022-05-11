using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public class UserPersist : GeralPersist, IUserPersist
    {

        public UserPersist(ProEventosContext context)
            : base(context) { }


        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users
                .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => u.UserName == userName);
        }

    }
}
