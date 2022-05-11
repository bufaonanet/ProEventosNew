using ProEventos.Application.DTOs;
using System.Threading.Tasks;

namespace ProEventos.Application
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateDto userUpdateDto);
    }
}
